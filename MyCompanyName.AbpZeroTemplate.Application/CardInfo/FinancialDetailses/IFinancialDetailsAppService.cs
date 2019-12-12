

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Card.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
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
// Copyright © YoYoCms@China.2019-07-30T10:25:54. All Rights Reserved.
//<生成时间>2019-07-30T10:25:54</生成时间>
#endregion
namespace MyCompanyName.AbpZeroTemplate.Card
{
    /// <summary>
    /// 财务明细服务接口
    /// </summary>
    public interface IFinancialDetailsAppService : IApplicationService
    {
        #region 财务明细管理

        /// <summary>
        /// 根据查询条件获取财务明细分页列表
        /// </summary>
        Task<PagedResultDto<FinancialDetailsListDto>> GetPagedFinancialDetailssAsync(GetFinancialDetailsInput input);

        /// <summary>
        /// 通过Id获取财务明细信息进行编辑或修改 
        /// </summary>
        Task<GetFinancialDetailsForEditOutput> GetFinancialDetailsForEditAsync(NullableIdDto<long> input);

        /// <summary>
        /// 通过指定id获取财务明细ListDto信息
        /// </summary>
        Task<FinancialDetailsListDto> GetFinancialDetailsByIdAsync(EntityDto<long> input);
        #endregion

        #region Excel导出功能



        /// <summary>
        /// 获取财务明细信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetFinancialDetailsToExcel();

        #endregion





    }
}
