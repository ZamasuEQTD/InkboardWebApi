using System.Text.Json;

namespace WebAPI.Core
{
      public class JsonLowerCaseNamingPolicy : JsonNamingPolicy
    {
    public override string ConvertName(string name)
    {
        return name.ToLower();
    }
}
}