                          
  
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using MyCompanyName.AbpZeroTemplate.Card;
   #region 代码生成器相关信息_ABP Code Generator Info
   //你好，我是ABP代码生成器的作者,欢迎您使用该工具，目前接受付费定制该工具，有需要的可以联系我
   //我的邮箱:werltm@hotmail.com
   // 官方网站:"http://www.yoyocms.com"
 // 交流QQ群：104390185  
 //微信公众号：角落的白板报
// 演示地址:"vue版本：http://vue.yoyocms.com angularJs版本:ng1.yoyocms.com"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>梁桐铭 ,微软MVP</Author-作者>
// Copyright © YoYoCms@China.2019-07-30T19:53:06. All Rights Reserved.
//<生成时间>2019-07-30T19:53:06</生成时间>
	#endregion
namespace MyCompanyName.AbpZeroTemplate.Card.Dtos
{
    /// <summary>
    /// 订单明细表编辑用Dto
    /// </summary>
    [AutoMap(typeof(OrderDetail))]
    public class OrderDetailEditDto 
    {

	/// <summary>
    ///   主键Id
    /// </summary>
    [DisplayName("主键Id")]
	public long? Id{get;set;}

        public   long  UserId { get; set; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        [DisplayName("客户订单号")]
        public   string  OrderNum { get; set; }

        /// <summary>
        /// 验证时间
        /// </summary>
        [DisplayName("验证时间")]
        public   DateTime?  CheckTime { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        public   int  Money { get; set; }

        /// <summary>
        /// 当前余额
        /// </summary>
        [DisplayName("当前余额")]
        public   int  CurrMoney { get; set; }

        /// <summary>
        /// 验证结果01:实名认证正确其他代码
        /// </summary>
        [DisplayName("验证结果01:实名认证正确其他代码")]
        public   string  Status { get; set; }

        /// <summary>
        /// 无法验证原因，如"无法验证！【军人转业，户口迁移等】
        /// </summary>
        [DisplayName("无法验证原因")]
        public   string  StatusMsg { get; set; }

        /// <summary>
        /// 请求IP
        /// </summary>
        [DisplayName("请求IP")]
        public   string  RequestUrl { get; set; }

    }
}
