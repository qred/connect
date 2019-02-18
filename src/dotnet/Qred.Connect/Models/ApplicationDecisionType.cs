using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Qred.Connect
{
    /// <summary>
    /// Gets or Sets ApplicationDecisionType
    /// </summary>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum ApplicationDecisionType
    {

        /// <summary>
        /// Enum ApprovedEnum for Approved
        /// </summary>
        [EnumMember(Value = "Approved")]
        Approved = 1,

        /// <summary>
        /// Enum DeniedEnum for Denied
        /// </summary>
        [EnumMember(Value = "Denied")]
        Denied= 2,

        /// <summary>
        /// Enum ManualProcessEnum for ManualProcess
        /// </summary>
        [EnumMember(Value = "ManualProcess")]
        ManualProcess = 3
    }
}
