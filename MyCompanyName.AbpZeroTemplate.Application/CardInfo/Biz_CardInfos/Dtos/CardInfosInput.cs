using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.CardInfo.Biz_CardInfos.Dtos
{
    public class CardInfosInput
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }
}
