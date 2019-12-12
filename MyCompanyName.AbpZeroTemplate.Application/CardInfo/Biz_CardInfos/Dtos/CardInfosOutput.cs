using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.CardInfo.Biz_CardInfos.Dtos
{
    public class CardInfosOutput
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public CardInfos CardInfos { get; set; }
    }

    public class CardInfos
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
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 验证结果01:实名认证正确其他代码
        /// </summary>
        public string Status { get; set; }
        public string StatusMsg { get; set; }
        /// <summary>
        /// 所在省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 所在市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 所在区县
        /// </summary>
        public string Prefecture { get; set; }
        /// <summary>
        /// 发证地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 地区代码
        /// </summary>
        public string AddrCode { get; set; }

        /// <summary>
        /// 身份证校验码
        /// </summary>
        public string LastCode { get; set; }
    }
}
