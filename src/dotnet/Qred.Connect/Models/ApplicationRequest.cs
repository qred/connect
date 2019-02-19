using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Qred.Connect
{
    public class ApplicationRequest
    {
        public decimal? Amount { get; set; }

        public string PromoCode { get; set; }

        public int? Term { get; set; }

        public string PurposeOfLoan { get; set; }

        public SimpleOrganization Organization { get; set; }

        public SimpleApplicant Applicant { get; set; }

        public List<Base64File> Files { get; set; }

        /// <summary>
        /// Get the JSON string presentation of the application request
        /// </summary>
        /// <returns>JSON string presentation of the application request</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                SerializationBinder = new TypeNameSerializationBinder(),
                TypeNameHandling= TypeNameHandling.Objects,
                ContractResolver= new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        OverrideSpecifiedNames = false
                    }
                }
            });
        }
    }
}
