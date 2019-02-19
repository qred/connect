/**
create a new .net project:

```
dotnet new console -o ./
```

add Identity Model package:

``` 
dotnet add package IdentityModel 
dotnet add package Qred.Connect
```

You should end up with something like:

```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="IdentityModel" Version="3.10.4" />
    <PackageReference Include="Qred.Connect" Version="0.2.0" />
  </ItemGroup>

</Project>
```

*/
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Qred.Connect;

namespace AwesomeBusinessApp
{
  public class Program
  {
    public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

    private static async Task MainAsync()
    {
      // discover endpoints from metadata
      var disco = await new DiscoveryClient("https://identity.qred.com").GetAsync();
      if (disco.IsError)
      {
        Console.Error.WriteLine("Error {0}", disco.Error);
        Console.Error.WriteLine("ErrorType {0}", disco.ErrorType);
        Console.Error.WriteLine(String.Join(", ",
            "Exception",
            "Message " + disco.Exception?.Message,
            "Source " + disco.Exception?.Source,
            "StackTrace " + disco.Exception?.StackTrace));
        return;
      }

      // request token
      var tokenClient = new TokenClient(disco.TokenEndpoint, "YOUR CLIENT ID", "YOUR API KEY");
      var tokenResponse = await tokenClient.RequestClientCredentialsAsync("connect");

      if (tokenResponse.IsError)
      {
        Console.WriteLine(tokenResponse.Error);
        return;
      }

      Console.WriteLine(tokenResponse.Json);
      Console.WriteLine("\n\n");

      // call api
      var client = new HttpClient();
      client.SetBearerToken(tokenResponse.AccessToken);
      var payload = new ApplicationRequest
      {
        Amount = 5000,
        Organization = new SimpleOrganization { 
          Email = "admin@a-company.com", 
          NationalOrganizationNumber = "2344473928" 
        },
        Applicant = new SimpleApplicant { 
          Email = "joe.smith@a-company.com", 
          Phone = "+4688675309"
        }
      };
      Console.WriteLine(payload.ToJson());
      var response = await client.PostAsync("https://api.qred.com/loans/v1/applications", new StringContent(payload.ToJson(), Encoding.UTF8, "application/json"));
      if (!response.IsSuccessStatusCode)
      {
        Console.WriteLine(response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
      }
      else
      {
        var content = await response.Content.ReadAsStringAsync();
        var applicationResponse = JsonConvert.DeserializeObject<ApplicationCreateResponse>(content);
        Console.WriteLine(applicationResponse);
      }
    }
  }
}


