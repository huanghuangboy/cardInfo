using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.Card
{
   public class CardIdCodes : Entity<long>
    {
        /// <summary>
        /// 身份证号前6位
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string Town { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

    }
}
