using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.AsyncWork.Dto
{
    public class GetCardInfos
    {
        public List<GetCardInfoDto> Info { get; set; }

    }
    public class GetCardInfoDto
    {
        /// <summary>
        /// 订单表ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }


    }
}
