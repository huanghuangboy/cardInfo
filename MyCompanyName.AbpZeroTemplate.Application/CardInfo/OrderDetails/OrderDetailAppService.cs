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
using MyCompanyName.AbpZeroTemplate.Authorization;
using MyCompanyName.AbpZeroTemplate.Card.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Card
{
    /// <summary>
    /// 订单明细表服务实现
    /// </summary>
    [AbpAuthorize(AppPermissions.OrderDetail)]
    public class OrderDetailAppService : AbpZeroTemplateAppServiceBase, IOrderDetailAppService
    {
        private readonly IRepository<OrderDetail, long> _orderDetailRepository;
        private readonly IOrderDetailListExcelExporter _orderDetailListExcelExporter;

        /// <summary>
        /// 构造方法
        /// </summary>
        public OrderDetailAppService(IRepository<OrderDetail, long> orderDetailRepository

      , IOrderDetailListExcelExporter orderDetailListExcelExporter
  )
        {
            _orderDetailRepository = orderDetailRepository;
            _orderDetailListExcelExporter = orderDetailListExcelExporter;
        }


        #region 实体的自定义扩展方法
        private IQueryable<OrderDetail> _orderDetailRepositoryAsNoTrack => _orderDetailRepository.GetAll().AsNoTracking();


        #endregion


        #region 订单明细表管理

        /// <summary>
        /// 根据查询条件获取订单明细表分页列表
        /// </summary>
        public async Task<PagedResultDto<OrderDetailListDto>> GetPagedOrderDetailsAsync(GetOrderDetailInput input)
        {
            var userid = AbpSession.UserId.Value;
            var query = _orderDetailRepositoryAsNoTrack.Include(m => m.User)
                .WhereIf(userid != 1, m => m.UserId == userid)
                .WhereIf(!string.IsNullOrEmpty(input.IdCard), m => m.IdCard == input.IdCard)
                .WhereIf(!string.IsNullOrEmpty(input.OrderNum), m => m.OrderNum == input.OrderNum)
                 .WhereIf(!string.IsNullOrEmpty(input.Status), m => m.Status == input.Status)
                  .WhereIf(input.IsApiData.HasValue, m => m.IsApiData == input.IsApiData)
                .WhereIf(!string.IsNullOrEmpty(input.RealName), m => m.RealName == input.RealName);
            //TODO:根据传入的参数添加过滤条件
            var orderDetailCount = await query.CountAsync();
            var orderDetails = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();
            var orderDetailListDtos = orderDetails.MapTo<List<OrderDetailListDto>>();
            return new PagedResultDto<OrderDetailListDto>(
            orderDetailCount,
            orderDetailListDtos
            );
        }

        #endregion
        #region 订单明细表的Excel导出功能
        public async Task<FileDto> GetOrderDetailToExcel(GetOrderDetailInput input)
        {
            var userid = AbpSession.UserId.Value;
            var entities = await _orderDetailRepository.GetAll()
                .WhereIf(userid != 1, m => m.UserId == userid)
                .WhereIf(!string.IsNullOrEmpty(input.IdCard), m => m.IdCard == input.IdCard)
                .WhereIf(!string.IsNullOrEmpty(input.OrderNum), m => m.OrderNum == input.OrderNum)
                 .WhereIf(!string.IsNullOrEmpty(input.Status), m => m.Status == input.Status)
                   .WhereIf(input.IsApiData.HasValue, m => m.IsApiData == input.IsApiData)
                .WhereIf(!string.IsNullOrEmpty(input.RealName), m => m.RealName == input.RealName)
                .ToListAsync();
            var dtos = entities.MapTo<List<OrderDetailListDto>>();
            var fileDto = _orderDetailListExcelExporter.ExportOrderDetailToFile(dtos);
            return fileDto;
        }


        #endregion










    }
}
