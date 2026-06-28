using InfnetStreaming.API.Extensions;
using System.Text.Json;

namespace InfnetStreaming.API.JsonPolicies
{
    public class JsonSnakeCasePolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
            => name.ToSnakeCase();
    }
}
