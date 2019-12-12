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
    /// 财务明细
    /// </summary>
    public class FinancialDetails : Entity<long>, IAudited
    {
        public long UserId { get; set; }
        /// <summary>
        /// 0 收入 2 支出
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 0 系统
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// 交易说明
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 当前余额
        /// </summary>
        public int NowMoney { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Mark { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
