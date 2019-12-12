using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.HttpHelper.Dto
{
    public class CarInfo2Dto
    {
        /// <summary>
        /// 状态码:详见状态码说明 01 通过，02不通过
        /// </summary>
        public string error_code { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string reason { get; set; }

        public Result result { get; set; }
    }

    public class Result
    {
        public string realname { get; set; }
        public string idcard { get; set; }
        public bool isok { get; set; }
        public IdCardInfor IdCardInfor { get; set; }

    }

    public class IdCardInfor
    {
        public string area { get; set; }
        public string sex { get; set; }
        public string birthday { get; set; }
    }
}
