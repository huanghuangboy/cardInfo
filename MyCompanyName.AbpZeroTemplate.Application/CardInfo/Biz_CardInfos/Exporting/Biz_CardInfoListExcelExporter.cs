using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.Card.Dtos;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Dto;
namespace MyCompanyName.AbpZeroTemplate.Card
{
    /// <summary>
    /// 订单表的导出EXCEL功能的实现
    /// </summary>
    public class Biz_CardInfoListExcelExporter : EpPlusExcelExporterBase, IBiz_CardInfoListExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public Biz_CardInfoListExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }



        /// <summary>
        /// 导出订单表到EXCEL文件
        /// <param name="biz_CardInfoListDtos">导出信息的DTO</param>
        /// </summary>
        public FileDto ExportBiz_CardInfoToFile(List<Biz_CardInfoListDto> biz_CardInfoListDtos)
        {
            var file = CreateExcelPackage("biz_CardInfoList.xlsx", excelPackage =>
            {
                var sheet = excelPackage.Workbook.Worksheets.Add(L("Biz_CardInfo"));
                sheet.OutLineApplyStyle = true;
                AddHeader(
                    sheet,
                     L("OrderNum"),
                     L("IdCard"),
                     L("RealName"),
                     L("Birthday"),
                     L("Status"),
                     L("StatusMsg"),
                     L("Province"),
                     L("City"),
                     L("Prefecture"),
                     L("Address"),
                     L("Sex"),
                     L("AddrCode"),
                     L("CheckTime"),
                     L("Money"),
                     L("CurrMoney"),
                     L("RequestUrl")
                    );
                AddObjects(sheet, 2, biz_CardInfoListDtos,
             _ => _.OrderNum,
             _ => _.IdCard,
             _ => _.RealName,
             _ => _timeZoneConverter.Convert(_.Birthday, _abpSession.TenantId, _abpSession.GetUserId()),
             _ => _.Status,
             _ => _.StatusMsg,
             _ => _.Province,
             _ => _.City,
             _ => _.Prefecture,
             _ => _.Address,
             _ => _.Sex,
             _ => _.AddrCode,
             _ => _timeZoneConverter.Convert(_.CheckTime, _abpSession.TenantId, _abpSession.GetUserId()),
             _ => _.Money,
             _ => _.CurrMoney,
             _ => _.RequestUrl
       );
                for (var i = 1; i <= 16; i++)
                {
                    sheet.Column(i).AutoFit();
                }

            });
            return file;
        }
    }
}
