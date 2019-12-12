using System;
using System.ComponentModel;
using Abp.AutoMapper;

namespace MyCompanyName.AbpZeroTemplate.Card.Dtos
{
    /// <summary>
    /// 订单表编辑用Dto
    /// </summary>
    [AutoMap(typeof(Biz_CardInfo))]
    public class Biz_CardInfoEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public long? Id { get; set; }

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
        /// 客户订单号
        /// </summary>
        public string OrderNum { get; set; }

    }
}
