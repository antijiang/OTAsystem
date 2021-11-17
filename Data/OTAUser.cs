using Microsoft.AspNetCore.Identity;
using System;

namespace OTASystem.Data
{
    public class OTAUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 有效日期
        /// </summary>
        public DateTime Expired { get; set; }

        /// <summary>
        /// 最大次数 0 为不限制
        /// </summary>
        public int MaxOTACount {  get; set; }

        /// <summary>
        /// 当前已使用次数
        /// </summary>
        public int OTAUsed {  get; set; }
    }
}
