using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OTASystem.Data;
using OTASystem.Data.Vo;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OTASystem.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class OTAController : ControllerBase
    {
        private OTADb db { get; set; }

        public OTAController(OTADb db)
        {
            db = db;
        }

        // 计算 secret; 密钥如果是 byte[] 可以base64转码成字符串后返回, 到烧录程序转回来
        // 返回到前端的格式为json:  {Code: 200, Data: "sec12321312", Msg: "错误信息或者null"}
        [HttpPost("Secret")]
        public async Task<IActionResult> GetSecret(SecretReq req)
        {
            // 账号信息校验
            var user = db.OTAUsers.FirstOrDefault(x => x.UserName == req.UserName && x.Password == req.Password);
            if (user == null)
            {
                // 账号密码错误
                return Ok(Result<object>.Error(10001, "user info incorrect"));
            }
            // TODO: 这里可以做mac地址校验
            if(req.Mac.Length != 12)
            {
                return Ok(Result<object>.Error(10003, "mac address incorrect"));
            }
            if(user.MaxOTACount != 0 && user.OTAUsed >= user.MaxOTACount)
            {
                // 次数不足
                return Ok(Result<object>.Error(10002, "access rejected"));
            }

            // 有效期校验
            if(user.Expired < DateTime.Now)
            {
                return Ok(Result<object>.Error(10002, "user invalid"));
            }

            // 查找缓存(如果历史数据中有, 就直接返回, 不再计算)
            var rec = db.OTARecords.FirstOrDefault(x => x.UserName == req.UserName && req.Mac == x.Mac);
            if(rec != null)
            {
                // 新增记录
                var newrec = JsonConvert.DeserializeObject<OTARecord>(JsonConvert.SerializeObject(rec));
                newrec.QueryTime = DateTime.Now;
                newrec.IsComputed = true;
                db.OTARecords.Add(newrec);
                db.SaveChanges();
                return Ok(Result<string>.Success(rec.Secret));
            }
            // 开始计算
            var result = await calc(req);
            // 如果没有计算过, 就添加一条记录且更新用户已使用OTA次数
            rec = new OTARecord();
            rec.UserName = req.UserName;
            rec.Secret = result.Data;
            rec.QueryTime = DateTime.Now;
            rec.Mac = req.Mac;
            db.OTARecords.Add(rec);
            user.OTAUsed = user.OTAUsed + 1;
            db.SaveChanges();
            return Ok(result);
        }

        private async Task<Result<string>> calc(SecretReq req)
        {
            // TODO: 改逻辑
            var sec = req.Mac + "secsecsec";

            return Result<string>.Success(sec);
        }
    }
}
