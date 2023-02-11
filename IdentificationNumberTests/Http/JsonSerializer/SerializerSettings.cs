using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PINTests.Http.JsonSerializer;

internal static class SerializerSettings
{
    public static JsonSerializerSettings SnakeCaseSettings =>
        new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
}
