using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
namespace MyCompanyName.AbpZeroTemplate.Card.Dtos
{
    /// <summary>
    /// 订单明细表列表Dto
    /// </summary>
    [AutoMapFrom(typeof(OrderDetail))]
    public class OrderDetailListDto : EntityDto<long>
    {
        /// <summary>
        /// 验证时间
        /// </summary>
        [DisplayName("验证时间")]
        public DateTime? CheckTime { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        public int Money { get; set; }
        /// <summary>
        /// 当前余额
        /// </summary>
        [DisplayName("当前余额")]
        public int CurrMoney { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }

        public string UserUserName { get; set; }

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

        /// <summary>
        /// 名字
        /// </summary>
        public string RealName { get; set; }

        public string OrderNum { get; set; }

        public string IdCard { get; set; }

        /// <summary>
        /// 是否从API中获取的数据
        /// </summary>
        public bool IsApiData { get; set; }
    }
}
