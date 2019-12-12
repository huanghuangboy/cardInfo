                           
 
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Card.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
namespace MyCompanyName.AbpZeroTemplate.Card
{
	/// <summary>
    /// 订单表的数据导出功能 
    /// </summary>
    public interface IBiz_CardInfoListExcelExporter
    {
        
//## 可以将下面的这个实体类，作为filedto来进行接收 


    //public class FileDto
    //{
    //    [Required]
    //    public string FileName { get; set; }

    //    [Required]
    //    public string FileType { get; set; }

    //    [Required]
    //    public string FileToken { get; set; }

    //    public FileDto()
    //    {
            
    //    }

    //    public FileDto(string fileName, string fileType)
    //    {
    //        FileName = fileName;
    //        FileType = fileType;
    //        FileToken = Guid.NewGuid().ToString("N");
    //    }
    //}

        /// <summary>
        /// 导出订单表到EXCEL文件
        /// <param name="biz_CardInfoListDtos">导出信息的DTO</param>
        /// </summary>
        FileDto ExportBiz_CardInfoToFile(List<Biz_CardInfoListDto> biz_CardInfoListDtos);



    }
}
