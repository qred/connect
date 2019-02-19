using System.Collections.Generic;

namespace Qred.Connect
{
    public partial class FullApplicationRequest : ApplicationRequest
    { 
        public new FullApplicationOrganization Organization { get => (FullApplicationOrganization)base.Organization; set => base.Organization = value; }
        public new FullApplicationApplicant Applicant { get => (FullApplicationApplicant)base.Applicant; set => base.Applicant = value; }

        public List<ApplicationPoliticallyExposedPerson> PoliticallyExposedPersons { get; set; }
    }
}
