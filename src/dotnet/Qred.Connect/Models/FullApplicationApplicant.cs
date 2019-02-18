namespace Qred.Connect
{
  /// <summary>
  /// 
  /// </summary>
  public partial class FullApplicationApplicant : ApplicationApplicant
    { 
        public string Email { get; set; }

        public string Phone { get; set; }

        public string NationalIdentificationNumber { get; set; }

        public string GivenName { get; set; }

        public string AdditionalName { get; set; }

        public string FamilyName { get; set; }

        public bool? PoliticallyExposedPerson { get; set; }

        public string DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }
    }
}
