using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.Authorization.Users.Dto
{
   public class ChangeMoneyDto
    {
        public int Money { get; set; }

        public string Mark { get; set; }

        public long UserId { get; set; }
    }
}
