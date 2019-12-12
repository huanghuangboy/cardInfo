using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.HttpHelper.Dto
{
    public class CarInfoDto
    {
        /// <summary>
        /// 状态码:详见状态码说明 01 通过，02不通过
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 身份证所在地(参考)
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        public string prefecture { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 地区代码
        /// </summary>
        public string addrCode { get; set; }
        /// <summary>
        /// 身份证校验码
        /// </summary>
        public string lastCode { get; set; }
    }
}
