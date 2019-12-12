using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.Card.Dtos;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Dto;
namespace MyCompanyName.AbpZeroTemplate.Card
{
    /// <summary>
    /// 订单明细表的导出EXCEL功能的实现
    /// </summary>
    public class OrderDetailListExcelExporter : EpPlusExcelExporterBase, IOrderDetailListExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public OrderDetailListExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }



        /// <summary>
        /// 导出订单明细表到EXCEL文件
        /// <param name="orderDetailListDtos">导出信息的DTO</param>
        /// </summary>
        public FileDto ExportOrderDetailToFile(List<OrderDetailListDto> orderDetailListDtos)
        {


            var file = CreateExcelPackage("orderDetailList.xlsx", excelPackage =>
            {

                var sheet = excelPackage.Workbook.Worksheets.Add(L("OrderDetail"));
                sheet.OutLineApplyStyle = true;

                AddHeader(
                    sheet,
                    "客户订单号",
                     "身份证号",
                     "姓名",
                     L("Money"),
                     L("CurrMoney"),
                     "验证结果",
                     "验证结果说明",
                     "验证时间",
                     "请求IP",
                     "是否由API获取"
                    );
                AddObjects(sheet, 2, orderDetailListDtos,
                _ => _.OrderNum,
                _ => _.IdCard,
                _ => _.RealName,
                _ => (_.Money * 0.01).ToString("f2"),
                _ => (_.CurrMoney * 0.01).ToString("f2"),
                 _ => _.Status,
                _ => _.StatusMsg,
                _ => _.CheckTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                _ => _.RequestUrl,
                 _ => _.IsApiData?"是":"否"
       );
                //写个时间转换的吧
                //var creationTimeColumn = sheet.Column(10);
                //creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                for (var i = 1; i <= 10; i++)
                {
                    sheet.Column(i).AutoFit();
                }

            });
            return file;

        }







    }
}
