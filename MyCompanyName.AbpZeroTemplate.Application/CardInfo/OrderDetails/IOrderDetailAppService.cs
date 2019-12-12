

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Card.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
namespace MyCompanyName.AbpZeroTemplate.Card
{
    /// <summary>
    /// 订单明细表服务接口
    /// </summary>
    public interface IOrderDetailAppService : IApplicationService
    {
        #region 订单明细表管理

        /// <summary>
        /// 根据查询条件获取订单明细表分页列表
        /// </summary>
        Task<PagedResultDto<OrderDetailListDto>> GetPagedOrderDetailsAsync(GetOrderDetailInput input);

        #endregion

        #region Excel导出功能



        /// <summary>
        /// 获取订单明细表信息转换为Excel
        /// </summary>
        /// <returns></returns>
        Task<FileDto> GetOrderDetailToExcel(GetOrderDetailInput input);

        #endregion





    }
}
