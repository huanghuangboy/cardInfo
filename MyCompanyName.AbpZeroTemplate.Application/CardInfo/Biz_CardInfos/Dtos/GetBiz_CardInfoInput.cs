                           
 
using System;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using MyCompanyName.AbpZeroTemplate.Dto;
namespace MyCompanyName.AbpZeroTemplate.Card.Dtos
{
	/// <summary>
    /// 订单表查询Dto
    /// </summary>
    public class GetBiz_CardInfoInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

		/// <summary>
	    /// 模糊查询参数
		/// </summary>
		public string FilterText { get; set; }

        public string OrderNum { get; set; }

        public string IdCard { get; set; }

        public string RealName { get; set; }

        /// <summary>
        /// 用于排序的默认值
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
