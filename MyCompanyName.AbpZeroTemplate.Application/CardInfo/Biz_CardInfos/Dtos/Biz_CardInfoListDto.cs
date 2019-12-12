
using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyCompanyName.AbpZeroTemplate.Card;
namespace MyCompanyName.AbpZeroTemplate.Card.Dtos
{
    /// <summary>
    /// 订单表列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Biz_CardInfo))]
    public class Biz_CardInfoListDto : EntityDto<long>
    {
        public string OrderNum { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [DisplayName("身份证号码")]
        public string IdCard { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string RealName { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [DisplayName("出生日期")]
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 验证结果01:实名认证正确其他代码
        /// </summary>
        [DisplayName("验证结果01:实名认证正确其他代码")]
        public string Status { get; set; }
        /// <summary>
        /// 无法验证原因，如"无法验证！【军人转业，户口迁移等】
        /// </summary>
        [DisplayName("无法验证原因，如无法验证！【军人转业，户口迁移等】")]
        public string StatusMsg { get; set; }
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
        /// 请求IP
        /// </summary>
        [DisplayName("请求IP")]
        public string RequestUrl { get; set; }

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
        /// 地区代码
        /// </summary>
        public string AddrCode { get; set; }
    }
}
