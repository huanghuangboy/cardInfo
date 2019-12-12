using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.BackgroundJobs;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using MyCompanyName.AbpZeroTemplate.AsyncWork;
using MyCompanyName.AbpZeroTemplate.AsyncWork.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using MyCompanyName.AbpZeroTemplate.Card.Dtos;
using MyCompanyName.AbpZeroTemplate.CardInfo.Biz_CardInfos.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.HttpHelper.Dto;
using MyCompanyName.AbpZeroTemplate.IDCard2Helpers;
using MyCompanyName.AbpZeroTemplate.IDCardHelpers;
using Newtonsoft.Json;

namespace MyCompanyName.AbpZeroTemplate.Card
{
    /// <summary>
    /// 订单表服务实现
    /// </summary>
    public class Biz_CardInfoAppService : AbpZeroTemplateAppServiceBase, IBiz_CardInfoAppService
    {
        private readonly IRepository<Biz_CardInfo, long> _biz_CardInfoRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IBiz_CardInfoListExcelExporter _biz_CardInfoListExcelExporter;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IClientInfoProvider _clientInfoProvider;
        private readonly IRepository<OrderDetail, long> _orderDetailRepository;
        private readonly IRepository<CardIdCodes, long> _cardIdCodesRepository;

        private static readonly object Locker = new object();
        /// <summary>
        /// 构造方法
        /// </summary>
        public Biz_CardInfoAppService(
            IRepository<Biz_CardInfo, long> biz_CardInfoRepository
            , IBiz_CardInfoListExcelExporter biz_CardInfoListExcelExporter
            , IBackgroundJobManager backgroundJobManager
            , IClientInfoProvider clientInfoProvider
            , IRepository<User, long> userRepository
            , IRepository<OrderDetail, long> orderDetailRepository
            , IRepository<CardIdCodes, long> cardIdCodesRepository
  )
        {
            _biz_CardInfoRepository = biz_CardInfoRepository;
            _biz_CardInfoListExcelExporter = biz_CardInfoListExcelExporter;
            _backgroundJobManager = backgroundJobManager;
            _clientInfoProvider = clientInfoProvider;
            _userRepository = userRepository;
            _orderDetailRepository = orderDetailRepository;
            _cardIdCodesRepository = cardIdCodesRepository;
        }
        #region 实体的自定义扩展方法
        private IQueryable<Biz_CardInfo> _biz_CardInfoRepositoryAsNoTrack => _biz_CardInfoRepository.GetAll().AsNoTracking();
        /// <summary>
        /// 根据查询条件获取订单表分页列表
        /// </summary>
        [AbpAuthorize(AppPermissions.Biz_CardInfo)]
        public async Task<PagedResultDto<Biz_CardInfoListDto>> GetPagedBiz_CardInfosAsync(GetBiz_CardInfoInput input)
        {
            var query = _biz_CardInfoRepositoryAsNoTrack
                .WhereIf(!string.IsNullOrEmpty(input.IdCard), m => m.IdCard.Contains(input.IdCard))
                .WhereIf(!string.IsNullOrEmpty(input.RealName), m => m.RealName.Contains(input.RealName));
            //TODO:根据传入的参数添加过滤条件
            var biz_CardInfoCount = await query.CountAsync();

            var biz_CardInfos = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();
            var biz_CardInfoListDtos = biz_CardInfos.MapTo<List<Biz_CardInfoListDto>>();
            return new PagedResultDto<Biz_CardInfoListDto>(
            biz_CardInfoCount,
            biz_CardInfoListDtos
            );
        }

        /// <summary>
        /// 通过Id获取订单表信息进行编辑或修改 
        /// </summary>
        [AbpAuthorize(AppPermissions.Biz_CardInfo)]
        public async Task<GetBiz_CardInfoForEditOutput> GetBiz_CardInfoForEditAsync(NullableIdDto<long> input)
        {
            var output = new GetBiz_CardInfoForEditOutput();

            Biz_CardInfoEditDto biz_CardInfoEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _biz_CardInfoRepository.GetAsync(input.Id.Value);
                biz_CardInfoEditDto = entity.MapTo<Biz_CardInfoEditDto>();
            }
            else
            {
                biz_CardInfoEditDto = new Biz_CardInfoEditDto();
            }

            output.Biz_CardInfo = biz_CardInfoEditDto;
            return output;
        }
        /// <summary>
        /// 通过指定id获取订单表ListDto信息
        /// </summary>
        [AbpAuthorize(AppPermissions.Biz_CardInfo)]
        public async Task<Biz_CardInfoListDto> GetBiz_CardInfoByIdAsync(EntityDto<long> input)
        {
            var entity = await _biz_CardInfoRepository.GetAsync(input.Id);

            return entity.MapTo<Biz_CardInfoListDto>();
        }
        /// <summary>
        /// 新增或更改订单表
        /// </summary>
        [AbpAuthorize(AppPermissions.Biz_CardInfo)]
        public async Task CreateOrUpdateBiz_CardInfoAsync(CreateOrUpdateBiz_CardInfoInput input)
        {
            if (input.Biz_CardInfoEditDto.Id.HasValue)
            {
                await UpdateBiz_CardInfoAsync(input.Biz_CardInfoEditDto);
            }
            else
            {
                await CreateBiz_CardInfoAsync(input.Biz_CardInfoEditDto);
            }
        }

        /// <summary>
        /// 新增订单表
        /// </summary>
        [AbpAuthorize(AppPermissions.Biz_CardInfo_CreateBiz_CardInfo)]
        public virtual async Task<Biz_CardInfoEditDto> CreateBiz_CardInfoAsync(Biz_CardInfoEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增
            var entity = input.MapTo<Biz_CardInfo>();
            var id = await _biz_CardInfoRepository.InsertAndGetIdAsync(entity);
            if (id > 0)
            {
                _backgroundJobManager.Enqueue<GetCardInfoJob, GetCardInfos>(new GetCardInfos { Info = new List<GetCardInfoDto> { new GetCardInfoDto { ID = id, IdCard = entity.IdCard, Name = entity.RealName } } });
            }
            return entity.MapTo<Biz_CardInfoEditDto>();
        }

        /// <summary>
        /// 编辑订单表
        /// </summary>
        [AbpAuthorize(AppPermissions.Biz_CardInfo_EditBiz_CardInfo)]
        public virtual async Task UpdateBiz_CardInfoAsync(Biz_CardInfoEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新
            var entity = await _biz_CardInfoRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            await _biz_CardInfoRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除订单表
        /// </summary>
        [AbpAuthorize(AppPermissions.Biz_CardInfo_DeleteBiz_CardInfo)]
        public async Task DeleteBiz_CardInfoAsync(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _biz_CardInfoRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除订单表
        /// </summary>
        [AbpAuthorize(AppPermissions.Biz_CardInfo_DeleteBiz_CardInfo)]
        public async Task BatchDeleteBiz_CardInfoAsync(List<long> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _biz_CardInfoRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion
        #region 订单表的Excel导出功能
        [AbpAuthorize(AppPermissions.Biz_CardInfo)]
        public async Task<FileDto> GetBiz_CardInfoToExcel(GetBiz_CardInfoInput input)
        {
            var entities = await _biz_CardInfoRepository.GetAll()
                 .WhereIf(!string.IsNullOrEmpty(input.IdCard), m => m.IdCard.Contains(input.IdCard))
                 .WhereIf(!string.IsNullOrEmpty(input.RealName), m => m.RealName.Contains(input.RealName))
                .ToListAsync();
            var dtos = entities.MapTo<List<Biz_CardInfoListDto>>();
            var fileDto = _biz_CardInfoListExcelExporter.ExportBiz_CardInfoToFile(dtos);
            return fileDto;
        }
        #endregion
        /// <summary>
        /// 提供获取身份证信息接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Biz_CardInfo_GetBiz_CardInfo)]
        public async Task<CardInfosOutput> GetCardInfoAsync(CardInfosInput input)
        {
            if (string.IsNullOrEmpty(input.Name) || string.IsNullOrEmpty(input.IdCard))
            {
                throw new UserFriendlyException("请求参数不完整");
            }
            long userid = 0;
            userid = AbpSession.UserId.Value;
            var user = _userRepository.Get(userid);
            if (user.NowMoney <= 0)
            {
                throw new UserFriendlyException("余额不足，请充值");
            }
            try
            {
                var detail = await _orderDetailRepository.GetAll().Where(m => m.IdCard == input.IdCard && m.RealName == input.Name && m.UserId == userid).FirstOrDefaultAsync();
                if (detail != null)
                {
                    var result = await _biz_CardInfoRepository.GetAll().Where(m => m.IdCard == input.IdCard && m.RealName == input.Name).FirstOrDefaultAsync();
                    var orderDetail = new OrderDetail
                    {
                        OrderNum = OrderHelper.GenerateId(),
                        IdCard = result.IdCard,
                        RealName = result.RealName,
                        Status = result.Status,
                        StatusMsg = result.StatusMsg,
                        CheckTime = DateTime.Now,
                        CurrMoney = user.NowMoney,
                        Money = 0,
                        RequestUrl = _clientInfoProvider.ClientIpAddress,
                        UserId = userid
                    };
                    _orderDetailRepository.Insert(orderDetail);
                    return new CardInfosOutput
                    {
                        IsSuccess = true,
                        CardInfos = new CardInfos
                        {
                            IdCard = result.IdCard,
                            Name = result.RealName,
                            Province = result.Province,
                            City = result.City,
                            Prefecture = result.Prefecture,
                            Address = result.Address,
                            AddrCode = result.AddrCode,
                            Sex = result.Sex,
                            Status = result.Status,
                            StatusMsg = result.StatusMsg,
                            Birthday = result.Birthday.HasValue ? result.Birthday.Value.ToString("yyyy-MM-dd") : "",
                            LastCode = result.LastCode
                        }
                    };
                }
                else
                {
                    lock (Locker)
                    {
                        var result = _biz_CardInfoRepository.GetAll().Where(m => m.IdCard == input.IdCard).ToList();
                        if (result.Any())
                        {
                            var card = result.Where(m => m.RealName == input.Name).FirstOrDefault();
                            if (card != null)
                            {
                                user.NowMoney = user.NowMoney - user.SinglePrice;
                                _userRepository.Update(user);
                                var odetail = new OrderDetail
                                {
                                    OrderNum = OrderHelper.GenerateId(),
                                    IdCard = card.IdCard,
                                    RealName = card.RealName,
                                    Status = card.Status,
                                    StatusMsg = card.StatusMsg,
                                    CheckTime = DateTime.Now,
                                    CurrMoney = user.NowMoney,
                                    Money = user.SinglePrice,
                                    RequestUrl = _clientInfoProvider.ClientIpAddress,
                                    UserId = userid
                                };
                                _orderDetailRepository.Insert(odetail);
                                return new CardInfosOutput
                                {
                                    IsSuccess = true,
                                    CardInfos = new CardInfos
                                    {

                                        IdCard = card.IdCard,
                                        Name = card.RealName,
                                        Province = card.Province,
                                        City = card.City,
                                        Prefecture = card.Prefecture,
                                        Address = card.Address,
                                        AddrCode = card.AddrCode,
                                        Sex = card.Sex,
                                        Status = card.Status,
                                        StatusMsg = card.StatusMsg,
                                        Birthday = card.Birthday.HasValue ? card.Birthday.Value.ToString("yyyy-MM-dd") : "",
                                        LastCode = card.LastCode
                                    }
                                };

                            }
                            else
                            {
                                return PushData(input, user);
                            }
                        }
                        else
                        {
                            return PushData(input, user);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                throw new UserFriendlyException("请求失败请重试");
            }
        }


        private CardInfosOutput PushData(CardInfosInput input, User user)
        {
            var result1 = Api1(input);
            if (result1 != null)
            {
                var data = new Biz_CardInfo
                {
                    IdCard = result1.idCard,
                    RealName = result1.name,
                    Province = result1.province,
                    City = result1.city,
                    Prefecture = result1.prefecture,
                    Address = result1.area,
                    AddrCode = result1.addrCode,
                    Sex = result1.sex,
                    Status = result1.status,
                    StatusMsg = result1.msg,
                    LastCode = result1.lastCode,
                    CheckTime = DateTime.Now
                };
                user.NowMoney = user.NowMoney - user.SinglePrice;
                _userRepository.Update(user);
                var orderDetail = new OrderDetail
                {
                    OrderNum = OrderHelper.GenerateId(),
                    IdCard = result1.idCard,
                    RealName = result1.name,
                    Status = result1.status,
                    StatusMsg = result1.msg,
                    CheckTime = DateTime.Now,
                    CurrMoney = user.NowMoney,
                    Money = user.SinglePrice,
                    RequestUrl = _clientInfoProvider.ClientIpAddress,
                    UserId = user.Id,
                    IsApiData = true
                };
                _orderDetailRepository.Insert(orderDetail);
                _biz_CardInfoRepository.Insert(data);
                return new CardInfosOutput
                {
                    IsSuccess = true,
                    CardInfos = new CardInfos
                    {
                        IdCard = result1.idCard,
                        Name = result1.name,
                        Province = result1.province,
                        City = result1.city,
                        Prefecture = result1.prefecture,
                        Address = result1.area,
                        AddrCode = result1.addrCode,
                        Sex = result1.sex,
                        Status = result1.status,
                        StatusMsg = result1.msg,
                        Birthday = result1.birthday,
                        LastCode = result1.lastCode
                    }
                };

            }
            var result2 = Api2(input);
            if (result2 != null)
            {
                var result= Api3(input);
                if (result == null)
                {
                    var data = new Biz_CardInfo
                    {
                        IdCard = input.IdCard,
                        RealName = input.Name,
                        Status = "01",
                        StatusMsg = "验证通过",
                        CheckTime = DateTime.Now
                    };
                    user.NowMoney = user.NowMoney - user.SinglePrice;
                    _userRepository.Update(user);
                    var orderDetail = new OrderDetail
                    {
                        OrderNum = OrderHelper.GenerateId(),
                        IdCard = input.IdCard,
                        RealName = input.Name,
                        Status = "01",
                        StatusMsg = "验证通过",
                        CheckTime = DateTime.Now,
                        CurrMoney = user.NowMoney,
                        Money = user.SinglePrice,
                        RequestUrl = _clientInfoProvider.ClientIpAddress,
                        UserId = user.Id,
                        IsApiData = true
                    };
                    _orderDetailRepository.Insert(orderDetail);
                    _biz_CardInfoRepository.Insert(data);
                    return new CardInfosOutput
                    {
                        IsSuccess = true,
                        CardInfos = new CardInfos
                        {
                            IdCard = input.IdCard,
                            Name = input.Name,
                            Status = "01",
                            StatusMsg = "验证通过"
                        }
                    };

                }
                else
                {
                    var data = new Biz_CardInfo
                    {
                        Province = result.province,
                        City = result.city,
                        Prefecture = result.prefecture,
                        Address = result.area,
                        IdCard = input.IdCard,
                        RealName = input.Name,
                        Status = "01",
                        StatusMsg = "验证通过",
                        CheckTime = DateTime.Now
                    };
                    user.NowMoney = user.NowMoney - user.SinglePrice;
                    _userRepository.Update(user);
                    var orderDetail = new OrderDetail
                    {
                        OrderNum = OrderHelper.GenerateId(),
                        IdCard = input.IdCard,
                        RealName = input.Name,
                        Status = "01",
                        StatusMsg = "验证通过",
                        CheckTime = DateTime.Now,
                        CurrMoney = user.NowMoney,
                        Money = user.SinglePrice,
                        RequestUrl = _clientInfoProvider.ClientIpAddress,
                        UserId = user.Id,
                        IsApiData = true
                    };
                    _orderDetailRepository.Insert(orderDetail);
                    _biz_CardInfoRepository.Insert(data);
                    return new CardInfosOutput
                    {
                        IsSuccess = true,
                        CardInfos = new CardInfos
                        {
                            IdCard = input.IdCard,
                            Name = input.Name,
                            Province = result.province,
                            City = result.city,
                            Prefecture = result.prefecture,
                            Address = result.area,
                            Status = "01",
                            StatusMsg = "验证通过"
                        }
                    };

                }

            }
            var data1 = new Biz_CardInfo
            {
                IdCard = input.IdCard,
                RealName = input.Name,
                Status = "02",
                StatusMsg = "实名认证不通过",
                CheckTime = DateTime.Now
            };
            user.NowMoney = user.NowMoney - user.SinglePrice;
            _userRepository.Update(user);
            var orderDetail1 = new OrderDetail
            {
                OrderNum = OrderHelper.GenerateId(),
                IdCard = input.IdCard,
                RealName = input.Name,
                Status = "02",
                StatusMsg = "实名认证不通过",
                CheckTime = DateTime.Now,
                CurrMoney = user.NowMoney,
                Money = user.SinglePrice,
                RequestUrl = _clientInfoProvider.ClientIpAddress,
                UserId = user.Id,
                IsApiData = true
            };
            _orderDetailRepository.Insert(orderDetail1);
            _biz_CardInfoRepository.Insert(data1);
            return new CardInfosOutput
            {
                IsSuccess = true,
                CardInfos = new CardInfos
                {
                    IdCard = input.IdCard,
                    Name = input.Name,
                    Status = "02",
                    StatusMsg = "实名认证不通过",
                }
            };
        }


        private CarInfoDto Api1(CardInfosInput input)
        {
            var cardInfo = IDCardHelper.GetCardInfo(input.IdCard, input.Name);
            if (string.IsNullOrEmpty(cardInfo)) { return null; }
            var cards = JsonConvert.DeserializeObject<CarInfoDto>(cardInfo);
            Logger.Info("在接口1中查询到数据" + cardInfo);
            if (cards.status != "202" && cards.status != "203")
            {
                return cards;
            }
            return null;
        }

        private CardInfo3Dto Api2(CardInfosInput input)
        {
            var temp = IDCard2Helper.GetCardInfo(input.IdCard, input.Name);
            Logger.Info("在接口1中查询到数据" + temp);
            if (string.IsNullOrEmpty(temp)) { return null; }
            var cards2 = JsonConvert.DeserializeObject<CardInfo3Dto>(temp);
            if (cards2.result != null && cards2.result.res=="1")
            {
                return cards2;
            }
            else
            {
                return null;
            }
        }

        private CarInfoDto Api3(CardInfosInput input)
        {
            var cardStr = input.IdCard.Substring(0, 6);
            var cardIdCode = _cardIdCodesRepository.GetAll().FirstOrDefault(m => m.Code == cardStr);
            if (cardIdCode != null)
            {
                Logger.Info("在cardIdCodes表中查询到数据" + input.IdCard + "姓名:" + input.Name);
                return new CarInfoDto
                {
                    province = cardIdCode.Province,
                    city = cardIdCode.City,
                    area = cardIdCode.Area,
                    prefecture = cardIdCode.Town
                };
            }
            else
            {
                Logger.Info("在cardIdCodes表中未查询到数据" + input.IdCard + "姓名:" + input.Name);
                return null;
            }

        }
    }
}
