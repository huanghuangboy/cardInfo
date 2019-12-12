using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.AsyncWork.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using MyCompanyName.AbpZeroTemplate.Card;
using MyCompanyName.AbpZeroTemplate.HttpHelper.Dto;
using MyCompanyName.AbpZeroTemplate.IDCard2Helpers;
using MyCompanyName.AbpZeroTemplate.IDCardHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.AsyncWork
{
    public class GetCardInfoJob : BackgroundJob<GetCardInfos>, ITransientDependency
    {
        // private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Biz_CardInfo, long> _cardInfoRepository;
        public GetCardInfoJob(IRepository<Biz_CardInfo, long> cardInfoRepository)
        {
            _cardInfoRepository = cardInfoRepository;
        }

        public override void Execute(GetCardInfos args)
        {
            var info = args.Info;
            info.ForEach(m =>
            {
                try
                {
                    var cardInfo = _cardInfoRepository.Get(m.ID);
                    if (cardInfo != null)
                    {
                        var result = IDCardHelper.GetCardInfo(cardInfo.IdCard, cardInfo.RealName);
                        Logger.Info(result);
                        var isSuccess = false;
                        var card = JsonConvert.DeserializeObject<CarInfoDto>(result);
                        var province = card.province;
                        var city = card.city;
                        var prefecture = card.prefecture;
                        var area = card.area;
                        var sex = card.sex;
                        var birthday = card.birthday;
                        if (card.status == "202" || card.status == "203")
                        {
                            //无法验证的情况下，只能再次向别的接口发起验证
                            var temp = IDCard2Helper.GetCardInfo(cardInfo.IdCard, cardInfo.RealName);
                            if (!string.IsNullOrEmpty(temp))
                            {
                                Logger.Info("接口" + temp);
                                var cards2 = JsonConvert.DeserializeObject<CarInfo2Dto>(temp);
                                if (cards2.result != null && cards2.result.IdCardInfor != null)
                                {                                                              
                                    try
                                    {
                                        sex = cards2.result.IdCardInfor.sex;
                                        birthday = cards2.result.IdCardInfor.birthday;
                                        area = cards2.result.IdCardInfor.area;
                                        var areaInfo = IDCard2Helper.getArea(area);
                                        province = areaInfo.Province;
                                        city = areaInfo.City;
                                        prefecture = areaInfo.Country;
                                        isSuccess = true;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                        cardInfo.Status = isSuccess?"01": card.status;
                        cardInfo.StatusMsg = isSuccess ? "验证通过" :card.msg;
                        cardInfo.Sex = sex;
                        cardInfo.Address = area;
                        cardInfo.Province = province;
                        cardInfo.City = city;
                        cardInfo.Prefecture = prefecture;
                        if (!string.IsNullOrEmpty(birthday))
                        {
                            cardInfo.Birthday = DateTime.Parse(birthday);
                        }
                        cardInfo.AddrCode = card.addrCode;
                        cardInfo.LastCode = card.lastCode;
                        cardInfo.CheckTime = DateTime.Now;
                        _cardInfoRepository.Update(cardInfo);
                    }
                }
                catch (Exception e)
                {
                    Logger.Info(e.ToString());
                }
            });

        }
    }
}
