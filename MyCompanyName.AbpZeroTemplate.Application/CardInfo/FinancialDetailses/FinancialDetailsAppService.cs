





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
using Eur.Abp.Elbe;
using MyCompanyName.AbpZeroTemplate.Authorization;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using MyCompanyName.AbpZeroTemplate.Card.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
namespace MyCompanyName.AbpZeroTemplate.Card
{
    /// <summary>
    /// 财务明细服务实现
    /// </summary>
    [AbpAuthorize(AppPermissions.FinancialDetails)]
    public class FinancialDetailsAppService : AbpZeroTemplateAppServiceBase, IFinancialDetailsAppService
    {
        private readonly IRepository<FinancialDetails, long> _financialDetailsRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IFinancialDetailsListExcelExporter _financialDetailsListExcelExporter;
        /// <summary>
        /// 构造方法
        /// </summary>
        public FinancialDetailsAppService(
            IRepository<FinancialDetails, long> financialDetailsRepository
            , IFinancialDetailsListExcelExporter financialDetailsListExcelExporter
            , IRepository<User, long> userRepository
  )
        {
            _financialDetailsRepository = financialDetailsRepository;
            _financialDetailsListExcelExporter = financialDetailsListExcelExporter;
            _userRepository = userRepository;
        }


        #region 实体的自定义扩展方法
        private IQueryable<FinancialDetails> _financialDetailsRepositoryAsNoTrack => _financialDetailsRepository.GetAll().AsNoTracking();


        #endregion


        #region 财务明细管理

        /// <summary>
        /// 根据查询条件获取财务明细分页列表
        /// </summary>
        public async Task<PagedResultDto<FinancialDetailsListDto>> GetPagedFinancialDetailssAsync(GetFinancialDetailsInput input)
        {

            var query = _financialDetailsRepositoryAsNoTrack.Include(m => m.User);
            //TODO:根据传入的参数添加过滤条件

            var financialDetailsCount = await query.CountAsync();

            //var financialDetailss = await query
            //    .LeftJoin(_userRepository.GetAll(), m => m.CreatorUserId, c => c.Id, (m, c) => new FinancialDetailsListDto
            //    {
            //        CreationTime = m.CreationTime,
            //        Mark = m.Mark,
            //        Desc = m.Desc,
            //        Money = m.Money,
            //        NowMoney = m.NowMoney,
            //        PayType = m.PayType,
            //        Type = m.Type,
            //      //  UserUserName = m.User.UserName,
            //        OperUserName = c.UserName
            //    })
            //.OrderBy(input.Sorting)
            //.PageBy(input)
            //.ToListAsync();
            var financialDetailss = await query               
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();
            var financialDetailsListDtos = financialDetailss.MapTo<List<FinancialDetailsListDto>>();
            return new PagedResultDto<FinancialDetailsListDto>(
            financialDetailsCount,
            financialDetailsListDtos
            );
        }

        /// <summary>
        /// 通过Id获取财务明细信息进行编辑或修改 
        /// </summary>
        public async Task<GetFinancialDetailsForEditOutput> GetFinancialDetailsForEditAsync(NullableIdDto<long> input)
        {
            var output = new GetFinancialDetailsForEditOutput();

            FinancialDetailsEditDto financialDetailsEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _financialDetailsRepository.GetAsync(input.Id.Value);
                financialDetailsEditDto = entity.MapTo<FinancialDetailsEditDto>();
            }
            else
            {
                financialDetailsEditDto = new FinancialDetailsEditDto();
            }

            output.FinancialDetails = financialDetailsEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取财务明细ListDto信息
        /// </summary>
        public async Task<FinancialDetailsListDto> GetFinancialDetailsByIdAsync(EntityDto<long> input)
        {
            var entity = await _financialDetailsRepository.GetAsync(input.Id);

            return entity.MapTo<FinancialDetailsListDto>();
        }

        #endregion
        #region 财务明细的Excel导出功能


        public async Task<FileDto> GetFinancialDetailsToExcel()
        {
            var entities = await _financialDetailsRepository.GetAll().ToListAsync();

            var dtos = entities.MapTo<List<FinancialDetailsListDto>>();

            var fileDto = _financialDetailsListExcelExporter.ExportFinancialDetailsToFile(dtos);



            return fileDto;
        }


        #endregion










    }
}
