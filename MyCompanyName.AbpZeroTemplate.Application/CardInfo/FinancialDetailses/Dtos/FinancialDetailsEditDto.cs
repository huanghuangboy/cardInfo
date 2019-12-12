                          
  
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
// Copyright © YoYoCms@China.2019-07-30T10:25:45. All Rights Reserved.
//<生成时间>2019-07-30T10:25:45</生成时间>
	#endregion
namespace MyCompanyName.AbpZeroTemplate.Card.Dtos
{
    /// <summary>
    /// 财务明细编辑用Dto
    /// </summary>
    [AutoMap(typeof(FinancialDetails))]
    public class FinancialDetailsEditDto 
    {

	/// <summary>
    ///   主键Id
    /// </summary>
    [DisplayName("主键Id")]
	public long? Id{get;set;}

        /// <summary>
        /// 0收入2支出
        /// </summary>
        [DisplayName("0收入2支出")]
        public   int  Type { get; set; }

        /// <summary>
        /// 0系统
        /// </summary>
        [DisplayName("0系统")]
        public   int  PayType { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        [DisplayName("交易金额")]
        public   int  Money { get; set; }

        /// <summary>
        /// 交易说明
        /// </summary>
        [DisplayName("交易说明")]
        public   string  Desc { get; set; }

        /// <summary>
        /// 当前余额
        /// </summary>
        [DisplayName("当前余额")]
        public   int  NowMoney { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public   string  Mark { get; set; }

    }
}
