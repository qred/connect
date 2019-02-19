namespace Qred.Connect
{
    public class OrganizationOwner
    {
        public string NationalIdentificationNumber { get; set; }

        public string GivenName { get; set; }

        public string AdditionalName { get; set; }

        public string FamilyName { get; set; }

        public int? OwnerShipPercent { get; set; }

        public string DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }
    }
}