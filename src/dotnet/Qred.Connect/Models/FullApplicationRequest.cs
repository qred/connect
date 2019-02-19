using System.Collections.Generic;

namespace Qred.Connect
{
    public partial class FullApplicationRequest : ApplicationRequest
    { 
        public new FullOrganization Organization { get => (FullOrganization)base.Organization; set => base.Organization = value; }
        public new FullApplicant Applicant { get => (FullApplicant)base.Applicant; set => base.Applicant = value; }

        public List<PoliticallyExposedPerson> PoliticallyExposedPersons { get; set; }
    }
}
