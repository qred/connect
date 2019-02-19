using System.Collections.Generic;

namespace Qred.Connect
{
    public class FullOrganization:SimpleOrganization 
    { 
        public int? NumberOfEmployees { get; set; }

        public string CurrentMonthlyTurnover { get; set; }

        public List<OrganizationOwner> Owners { get; set; }

        public string Iban { get; set; }
    }
}
