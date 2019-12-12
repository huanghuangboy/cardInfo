using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.Card
{
    /// <summary>
    /// 身份证基础信息表
    /// </summary>
    public class Biz_CardInfo : Entity<long>, IFullAudited
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 所在省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 所在市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 所在区县
        /// </summary>
        public string Prefecture { get; set; }
        /// <summary>
        /// 发证地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 地区代码
        /// </summary>
        public string AddrCode { get; set; }
        /// <summary>
        /// 身份证校验码
        /// </summary>
        public string LastCode { get; set; }
        /// <summary>
        /// 验证结果 01:实名认证正确 其他代码
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 无法验证原因，如"无法验证！【军人转业，户口迁移等】
        /// </summary>
        public string StatusMsg { get; set; }
        /// <summary>
        /// 验证时间
        /// </summary>
        public DateTime? CheckTime { get; set; }

        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
