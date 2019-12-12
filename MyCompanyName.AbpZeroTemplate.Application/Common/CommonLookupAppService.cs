using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using MyCompanyName.AbpZeroTemplate.Common.Dto;
using MyCompanyName.AbpZeroTemplate.Editions;

namespace MyCompanyName.AbpZeroTemplate.Common
{
    [AbpAuthorize]
    public class CommonLookupAppService : AbpZeroTemplateAppServiceBase, ICommonLookupAppService
    {
        private readonly EditionManager _editionManager;
        private readonly IRepository<User, long> _userRepository;
        public CommonLookupAppService(EditionManager editionManager, IRepository<User, long> userRepository)
        {
            _editionManager = editionManager;
            _userRepository = userRepository;
        }

        public async Task<ListResultDto<ComboboxItemDto>> GetEditionsForCombobox()
        {
            var editions = await _editionManager.Editions.ToListAsync();
            return new ListResultDto<ComboboxItemDto>(
                editions.Select(e => new ComboboxItemDto(e.Id.ToString(), e.DisplayName)).ToList()
                );
        }

        public async Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input)
        {
            if (AbpSession.TenantId != null)
            {
                //Prevent tenants to get other tenant's users.
                input.TenantId = AbpSession.TenantId;
            }

            using (CurrentUnitOfWork.SetTenantId(input.TenantId))
            {
                var query = UserManager.Users
                    .WhereIf(
                        !input.Filter.IsNullOrWhiteSpace(),
                        u =>
                            u.Name.Contains(input.Filter) ||
                            u.Surname.Contains(input.Filter) ||
                            u.UserName.Contains(input.Filter) ||
                            u.EmailAddress.Contains(input.Filter)
                    );

                var userCount = await query.CountAsync();
                var users = await query
                    .OrderBy(u => u.Name)
                    .ThenBy(u => u.Surname)
                    .PageBy(input)
                    .ToListAsync();

                return new PagedResultDto<NameValueDto>(
                    userCount,
                    users.Select(u =>
                        new NameValueDto(
                            u.FullName + " (" + u.EmailAddress + ")",
                            u.Id.ToString()
                            )
                        ).ToList()
                    );
            }
        }

        public string GetDefaultEditionName()
        {
            return EditionManager.DefaultEditionName;
        }

        public CurrentUserDto GetCurrentUserInfo()
        {
            var userId = AbpSession.UserId.Value;
            var user = _userRepository.Get(userId);
            if (user != null)
            {
                return new CurrentUserDto
                {
                    UserName = user.UserName,
                    CustName = user.UserName,
                    NowMoney = user.NowMoney
                };
            }
            return new CurrentUserDto();
        }
    }
}
