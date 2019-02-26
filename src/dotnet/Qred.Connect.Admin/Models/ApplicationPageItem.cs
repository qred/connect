using System;

namespace Qred.Connect.Admin
{
  public class ApplicationPageItem
  {
    public string Id { get; set; }
    public string Source { get; set; }
    public Uri Url { get; set; }
    /// <summary>
    /// Not really simple organization, but should deserialize
    /// </summary>
    public SimpleOrganization Organization { get; set; }
    /// <summary>
    /// Not really simple applicant, but should deserialize
    /// </summary>
    public SimpleApplicant Applicant { get; set; }
    public string PurposeOfLoan { get; set; }
    public decimal? Amount { get; set; }
    public int? Term { get; set; }
  }
}