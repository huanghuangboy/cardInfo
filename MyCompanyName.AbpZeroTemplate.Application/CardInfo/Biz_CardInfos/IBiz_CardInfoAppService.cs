                           
 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Card.Dtos;
using MyCompanyName.AbpZeroTemplate.CardInfo.Biz_CardInfos.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Card
{
	/// <summary>
    /// 订单表服务接口
    /// </summary>
    public interface IBiz_CardInfoAppService : IApplicationService
    {
        #region 订单表管理

        /// <summary>
        /// 根据查询条件获取订单表分页列表
        /// </summary>
        Task<PagedResultDto<Biz_CardInfoListDto>> GetPagedBiz_CardInfosAsync(GetBiz_CardInfoInput input);

        /// <summary>
        /// 通过Id获取订单表信息进行编辑或修改 
        /// </summary>
        Task<GetBiz_CardInfoForEditOutput> GetBiz_CardInfoForEditAsync(NullableIdDto<long> input);

		  /// <summary>
        /// 通过指定id获取订单表ListDto信息
        /// </summary>
		Task<Biz_CardInfoListDto> GetBiz_CardInfoByIdAsync(EntityDto<long> input);



        /// <summary>
        /// 新增或更改订单表
        /// </summary>
        Task CreateOrUpdateBiz_CardInfoAsync(CreateOrUpdateBiz_CardInfoInput input);





        /// <summary>
        /// 新增订单表
        /// </summary>
        Task<Biz_CardInfoEditDto> CreateBiz_CardInfoAsync(Biz_CardInfoEditDto input);

        /// <summary>
        /// 更新订单表
        /// </summary>
        Task UpdateBiz_CardInfoAsync(Biz_CardInfoEditDto input);

        /// <summary>
        /// 删除订单表
        /// </summary>
        Task DeleteBiz_CardInfoAsync(EntityDto<long> input);

        /// <summary>
        /// 批量删除订单表
        /// </summary>
        Task BatchDeleteBiz_CardInfoAsync(List<long> input);
        /// <summary>
        /// 接口获取身份证信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CardInfosOutput> GetCardInfoAsync(CardInfosInput input);
        #endregion

        #region Excel导出功能



        /// <summary>
        /// 获取订单表信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetBiz_CardInfoToExcel(GetBiz_CardInfoInput input);

#endregion





    }
}
