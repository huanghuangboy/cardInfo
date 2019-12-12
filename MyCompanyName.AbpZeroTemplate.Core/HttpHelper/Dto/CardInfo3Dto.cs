using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.HttpHelper.Dto
{
   public class CardInfo3Dto
    {
        public string code { get; set; }

        public string message { get; set; }
        public result result { get; set; }

    }
    public class result {

        public string name { get; set; }
        public string idcard { get; set; }
        public string res { get; set; }
        public string description { get; set; }

        public string sex { get; set; }

        public string birthday { get; set; }

        public string address { get; set; }



    }
}
