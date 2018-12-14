using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace AwesomeBusinessApp
{
  public class CreatedLoanApplication
  {
    public string Id { get; set; }
    public string Url { get; set; }
  }

  public class ConnectToQred
  {
    HttpClient client = new HttpClient(); // note: You might not want to do this but inject client through dependency injection
    public async Task<CreatedLoanApplication> Connect()
    {
      var payload = new {
          Amount=5000,
          Organization=new {TaxID="SE2344473928"},
          Applicant=new{Email="joe.smith@a-company.com", Telephone="408-867-5309"}
      };
      var result = await client.PostAsync("https://api.qred.com/loans/v1/applications", new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));
      result.EnsureSuccessStatusCode();

      return JsonConvert.DeserializeObject<CreatedLoanApplication>(await result.Content.ReadAsStringAsync());
    } 
  }
}


