using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eur.Abp.Elbe
{
    public static class IQueryableExt
    {
        public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
          this IQueryable<TOuter> outer,
          IQueryable<TInner> inner,
          Expression<Func<TOuter, TKey>> outerKeySelector,
          Expression<Func<TInner, TKey>> innerKeySelector,
          Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {

            // generic methods
            var selectManies = typeof(Queryable).GetMethods()
                .Where(x => x.Name == "SelectMany" && x.GetParameters().Length == 3)
                .OrderBy(x => x.ToString().Length)
                .ToList();
            var selectMany = selectManies.First();
            var select = typeof(Queryable).GetMethods().First(x => x.Name == "Select" && x.GetParameters().Length == 2);
            var where = typeof(Queryable).GetMethods().First(x => x.Name == "Where" && x.GetParameters().Length == 2);
            var groupJoin = typeof(Queryable).GetMethods().First(x => x.Name == "GroupJoin" && x.GetParameters().Length == 5);
            var defaultIfEmpty = typeof(Queryable).GetMethods().First(x => x.Name == "DefaultIfEmpty" && x.GetParameters().Length == 1);

            // need anonymous type here or let's use Tuple
            // prepares for:
            // var q2 = Queryable.GroupJoin(db.A, db.B, a => a.Id, b => b.IdA, (a, b) => new { a, groupB = b.DefaultIfEmpty() });
            var tuple = typeof(Tuple<,>).MakeGenericType(
                typeof(TOuter),
                typeof(IQueryable<>).MakeGenericType(
                    typeof(TInner)
                    )
                );
            var paramOuter = Expression.Parameter(typeof(TOuter));
            var paramInner = Expression.Parameter(typeof(IEnumerable<TInner>));
            var groupJoinExpression = Expression.Call(
                null,
                groupJoin.MakeGenericMethod(typeof(TOuter), typeof(TInner), typeof(TKey), tuple),
                new Expression[]
                    {
                    Expression.Constant(outer),
                    Expression.Constant(inner),
                    outerKeySelector,
                    innerKeySelector,
                    Expression.Lambda(
                        Expression.New(
                            tuple.GetConstructor(tuple.GetGenericArguments()),
                            new Expression[]
                                {
                                    paramOuter,
                                    Expression.Call(
                                        null,
                                        defaultIfEmpty.MakeGenericMethod(typeof (TInner)),
                                        new Expression[]
                                            {
                                                Expression.Convert(paramInner, typeof (IQueryable<TInner>))
                                            }
                                )
                                },
                            tuple.GetProperties()
                            ),
                        new[] {paramOuter, paramInner}
                )
                    }
                );

            // prepares for:
            // var q3 = Queryable.SelectMany(q2, x => x.groupB, (a, b) => new { a.a, b });
            var tuple2 = typeof(Tuple<,>).MakeGenericType(typeof(TOuter), typeof(TInner));
            var paramTuple2 = Expression.Parameter(tuple);
            var paramInner2 = Expression.Parameter(typeof(TInner));
            var paramGroup = Expression.Parameter(tuple);
            var selectMany1Result = Expression.Call(
                null,
                selectMany.MakeGenericMethod(tuple, typeof(TInner), tuple2),
                new Expression[]
                    {
                    groupJoinExpression,
                    Expression.Lambda(
                        Expression.Convert(Expression.MakeMemberAccess(paramGroup, tuple.GetProperty("Item2")),
                                           typeof (IEnumerable<TInner>)),
                        paramGroup
                ),
                    Expression.Lambda(
                        Expression.New(
                            tuple2.GetConstructor(tuple2.GetGenericArguments()),
                            new Expression[]
                                {
                                    Expression.MakeMemberAccess(paramTuple2, paramTuple2.Type.GetProperty("Item1")),
                                    paramInner2
                                },
                            tuple2.GetProperties()
                            ),
                        new[]
                            {
                                paramTuple2,
                                paramInner2
                            }
                )
                    }
                );

            // prepares for final step, combine all expressinos together and invoke:
            // var q4 = Queryable.SelectMany(db.A, a => q3.Where(x => x.a == a).Select(x => x.b), (a, b) => new { a, b });
            var paramTuple3 = Expression.Parameter(tuple2);
            var paramTuple4 = Expression.Parameter(tuple2);
            var paramOuter3 = Expression.Parameter(typeof(TOuter));
            var selectManyResult2 = selectMany
                .MakeGenericMethod(
                    typeof(TOuter),
                    typeof(TInner),
                    typeof(TResult)
                )
                .Invoke(
                    null,
                    new object[]
                        {
                        outer,
                        Expression.Lambda(
                            Expression.Convert(
                                Expression.Call(
                                    null,
                                    select.MakeGenericMethod(tuple2, typeof(TInner)),
                                    new Expression[]
                                        {
                                            Expression.Call(
                                                null,
                                                where.MakeGenericMethod(tuple2),
                                                new Expression[]
                                                    {
                                                        selectMany1Result,
                                                        Expression.Lambda(
                                                            Expression.Equal(
                                                                paramOuter3,
                                                                Expression.MakeMemberAccess(paramTuple4, paramTuple4.Type.GetProperty("Item1"))
                                                            ),
                                                            paramTuple4
                                                        )
                                                    }
                                            ),
                                            Expression.Lambda(
                                                Expression.MakeMemberAccess(paramTuple3, paramTuple3.Type.GetProperty("Item2")),
                                                paramTuple3
                                            )
                                        }
                                ),
                                typeof(IEnumerable<TInner>)
                            ),
                            paramOuter3
                        ),
                        resultSelector
                        }
                );

            return (IQueryable<TResult>)selectManyResult2;
        }


        /// <summary>
        /// 扩展select方法，可以根据Dto的字段查询需要的对象实体，只能是单表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        public static Expression<Func<TEntity, TDto>> GetSelectFunc<TEntity, TDto>(Tuple<Type, string>[] linkEntityTypes = null)
        {
            ParameterExpression left = Expression.Parameter(typeof(TEntity), "a");
            Expression<Func<TEntity, TDto>> dt1 = Expression.Lambda<Func<TEntity, TDto>>(CreateMemberInitExpression(left, typeof(TEntity), typeof(TDto), linkEntityTypes), new ParameterExpression[] { left });
            return dt1;

        }
        private static MemberInitExpression CreateMemberInitExpression(ParameterExpression left, Type entityType, Type dtoType, Tuple<Type, string>[] linkEntityNames)
        {
            System.Linq.Expressions.NewExpression newAnimal = System.Linq.Expressions.Expression.New(dtoType);

            List<MemberBinding> bindings = new List<MemberBinding>();
            var members = dtoType.GetProperties();//获取Dto的各个属性
            members.ToList().ForEach(p =>
            {
                try
                {
                    if (linkEntityNames != null && linkEntityNames.Length > 0)
                    {
                        var container = linkEntityNames.ToList().FirstOrDefault(x => p.Name.Contains(x.Item2));
                        if (container != null)
                        {
                            var rightStr = p.Name.Replace(container.Item2, "");

                            Expression right = Expression.Property(left, entityType.GetProperty(container.Item2));

                            MemberExpression mem = Expression.Property(right, rightStr);
                            //这里传的mem是 用 a.name给它赋值
                            System.Linq.Expressions.MemberBinding speciesMemberBinding = System.Linq.Expressions.Expression.Bind(p, mem);
                            bindings.Add(speciesMemberBinding);
                        }
                        else
                        {
                            MemberExpression mem = Expression.Property(left, p.Name);
                            //这里传的mem是 用 a.name给它赋值
                            System.Linq.Expressions.MemberBinding speciesMemberBinding = System.Linq.Expressions.Expression.Bind(p, mem);
                            bindings.Add(speciesMemberBinding);
                        }

                    }
                    else
                    {
                        try
                        {
                            MemberExpression mem = Expression.Property(left, p.Name);
                            //这里传的mem是 用 a.name给它赋值
                            System.Linq.Expressions.MemberBinding speciesMemberBinding = System.Linq.Expressions.Expression.Bind(p, mem);
                            bindings.Add(speciesMemberBinding);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                catch
                {

                }
            });

            System.Linq.Expressions.MemberInitExpression memberInitExpression = System.Linq.Expressions.Expression.MemberInit(newAnimal, bindings.ToArray());

            return memberInitExpression;
        }
    }
}
