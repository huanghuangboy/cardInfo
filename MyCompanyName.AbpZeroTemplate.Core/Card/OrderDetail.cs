using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.Card
{
    /// <summary>
    /// 订单明细表
    /// </summary>
    public class OrderDetail : Entity<long>, IAudited
    {
        public long UserId { get; set; }
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 验证时间
        /// </summary>
        public DateTime? CheckTime { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// 当前余额
        /// </summary>
        public int CurrMoney { get; set; }

        /// <summary>
        /// 验证结果 01:实名认证正确 其他代码
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 无法验证原因，如"无法验证！【军人转业，户口迁移等】
        /// </summary>
        public string StatusMsg { get; set; }
        /// <summary>
        /// 请求IP
        /// </summary>
        public string RequestUrl { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 是否从API中获取的数据
        /// </summary>
        public bool IsApiData { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
