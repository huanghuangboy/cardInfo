
using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
namespace MyCompanyName.AbpZeroTemplate.Card.Dtos
{
    /// <summary>
    /// 财务明细列表Dto
    /// </summary>
    [AutoMapFrom(typeof(FinancialDetails))]
    public class FinancialDetailsListDto : EntityDto<long>
    {
        /// <summary>
        /// 0收入2支出
        /// </summary>
        [DisplayName("0收入2支出")]
        public int Type { get; set; }
        /// <summary>
        /// 0系统
        /// </summary>
        [DisplayName("0系统")]
        public int PayType { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        [DisplayName("交易金额")]
        public int Money { get; set; }
        /// <summary>
        /// 当前余额
        /// </summary>
        [DisplayName("当前余额")]
        public int NowMoney { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }

        public string Desc { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Mark { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserUserName { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperUserName { get; set; }
}
}
