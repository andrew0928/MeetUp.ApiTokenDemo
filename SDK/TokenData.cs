using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.ApiTokenDemo.SDK
{
    /// <summary>
    /// 如何自訂 TokenData ?
    /// 
    /// 1. 繼承自 TokenData
    /// 2. 加上你的自訂項目，標上 [JsonProperty]
    /// 3. 覆寫 (override) bool IsValidate( ), 自訂你的授權驗證邏輯
    /// 
    /// 完成
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class TokenData
    {
        internal TokenData() { }

        /// <summary>
        /// 對應 TokenData 衍生類別的 Type Name
        /// </summary>
        [JsonProperty]
        public string TypeName { get; internal set; }

        [JsonProperty]
        public DateTime ExpireDate { get; set; }
        

        /// <summary>
        /// (可覆寫) 驗證 Token Data 資料是否合法
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValidate()
        {
            if (this.GetType().FullName != this.TypeName) return false;
            if (DateTime.Now > this.ExpireDate) return false;
            return true;
        }
    }
}
