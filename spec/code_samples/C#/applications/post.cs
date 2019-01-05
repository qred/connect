/**
create a new .net project:

```
dotnet new console -o ./
```

add Identity Model package:

``` 
dotnet add package IdentityModel 
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

namespace AwesomeBusinessApp
{
  public class TypeNameSerializationBinder : ISerializationBinder
  {
    public TypeNameSerializationBinder()
    {
      var typeNames = new Dictionary<Type, string>
            {
                { typeof(ApplicationRequest),"simple" },
                { typeof(ApplicationApplicant),"simple" },
                { typeof(ApplicationOrganization),"simple" },
            };
      foreach (var typeName in typeNames)
      {
        Map(typeName.Key, typeName.Value);
      }
    }

    readonly Dictionary<Type, string> typeToName = new Dictionary<Type, string>();

    public void Map(Type type, string name) => this.typeToName.Add(type, name);

    public void BindToName(Type serializedType, out string assemblyName, out string typeName)
    {
      var name = typeToName.GetValueOrDefault(serializedType, null);
      if (name != null)
      {
        assemblyName = null;
        typeName = name;
      }
      else
      {
        assemblyName = null;
        typeName = null;
      }
    }

    public Type BindToType(string assemblyName, string typeName) => null;
  }

  public class ApplicationApplicant
  {
    public string Email { get; set; }

    public string Phone { get; set; }

    public string NationalIdentificationNumber { get; set; }

    public string GivenName { get; set; }

    public string AdditionalName { get; set; }

    public string FamilyName { get; set; }
  }

  public class ApplicationOrganization
  {
    public string NationalOrganizationNumber { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Url { get; set; }
  }

  public class ApplicationRequest
  {
    public decimal? Amount { get; set; }

    public string PromoCode { get; set; }

    public decimal? Term { get; set; }

    public string PurposeOfLoan { get; set; }

    public ApplicationOrganization Organization { get; set; }

    public ApplicationApplicant Applicant { get; set; }

    public List<UploadBase64File> Files { get; set; }

    /// <summary>
    /// Get the JSON string presentation of the application request
    /// </summary>
    /// <returns>JSON string presentation of the application request</returns>
    public string ToJson()
    {
      return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
      {
        SerializationBinder = new TypeNameSerializationBinder(),
        TypeNameHandling = TypeNameHandling.Objects,
        ContractResolver = new DefaultContractResolver
        {
          NamingStrategy = new CamelCaseNamingStrategy
          {
            OverrideSpecifiedNames = false
          }
        }
      });
    }
  }

  public class UploadBase64File
  {
    public List<string> EncodingFormat { get; set; }

    public string Filename { get; set; }

    public byte[] Base64Content { get; set; }
  }
  public class ApplicationCreateResponse
  {
    public string Id { get; set; }
    public string Url { get; set; }

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
      var sb = new StringBuilder();
      sb.Append("ApplicationCreateResponse {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Url: ").Append(Url).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }
  }
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
      client.DefaultRequestHeaders.Add("api_key", tokenResponse.AccessToken);
      var payload = new ApplicationRequest
      {
        Amount = 5000,
        Organization = new ApplicationOrganization { Email = "admin@a-company.com", NationalOrganizationNumber = "2344473928" },
        Applicant = new ApplicationApplicant { Email = "joe.smith@a-company.com", Phone = "+4688675309", NationalIdentificationNumber = "2344473928" }
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


