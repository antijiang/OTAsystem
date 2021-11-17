using System;

namespace OTASystem.Data
{
    public class OTARecord
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get;set; }

        /// <summary>
        /// mac地址
        /// </summary>
        public string Mac { get;set; }
        
        /// <summary>
        /// 密码, 多次计算密钥时当做缓存使用, 且不消耗次数
        /// </summary>
        public string Secret { get;set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime QueryTime { get; set; }

        /// <summary>
        /// 是否重复请求
        /// </summary>
        public bool IsComputed { get; set; }
    }
}
