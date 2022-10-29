using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    [DataContract]
    public class ServiceResult
    {
        public ServiceResult(int code, object data, string message, string newToken = null)
        {
            Code = code;
            Data = data;
            Message = message;
            NewToken = newToken;
        }

        /// <summary>
        /// ErrorCode
        /// </summary>
        [JsonProperty("Code")]
        public int Code { get; }

        /// <summary>
        ///  Result data of service execution
        /// </summary>
        [JsonProperty("Data")]
        public object Data { get; }

        /// <summary>
        /// Current message
        /// </summary>
        [JsonProperty("Message")]
        public string Message { get; }
        [JsonProperty("NewToken")]
        public string NewToken { get; }
    }
}
