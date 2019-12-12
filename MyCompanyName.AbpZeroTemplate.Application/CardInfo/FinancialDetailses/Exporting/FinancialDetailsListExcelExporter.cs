


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.Card.Dtos;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
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
// Copyright © YoYoCms@China.2019-07-30T10:26:09. All Rights Reserved.
//<生成时间>2019-07-30T10:26:09</生成时间>
#endregion
namespace MyCompanyName.AbpZeroTemplate.Card
{
    /// <summary>
    /// 财务明细的导出EXCEL功能的实现
    /// </summary>
    public class FinancialDetailsListExcelExporter : EpPlusExcelExporterBase, IFinancialDetailsListExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public FinancialDetailsListExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }



        /// <summary>
        /// 导出财务明细到EXCEL文件
        /// <param name="financialDetailsListDtos">导出信息的DTO</param>
        /// </summary>
        public FileDto ExportFinancialDetailsToFile(List<FinancialDetailsListDto> financialDetailsListDtos)
        {


            var file = CreateExcelPackage("financialDetailsList.xlsx", excelPackage =>
            {

                var sheet = excelPackage.Workbook.Worksheets.Add(L("FinancialDetails"));
                sheet.OutLineApplyStyle = true;

                AddHeader(
                    sheet,
                      L("Type"),
 L("PayType"),
 L("Money"),
 L("NowMoney"),
 L("CreationTime")
                    );
                AddObjects(sheet, 2, financialDetailsListDtos,

             _ => _.Type,

             _ => _.PayType,

             _ => _.Money,

             _ => _.NowMoney,

        _ => _timeZoneConverter.Convert(_.CreationTime, _abpSession.TenantId, _abpSession.GetUserId())
       );
                //写个时间转换的吧
                //var creationTimeColumn = sheet.Column(10);
                //creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                for (var i = 1; i <= 5; i++)
                {
                    sheet.Column(i).AutoFit();
                }

            });
            return file;

        }







    }
}
