using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace firstAWS;

public class Function
{
    public APIGatewayHttpApiV2ProxyResponse FunctionHandler(
        APIGatewayProxyRequest request,
        ILambdaContext context)
    {
        var parameters = request.QueryStringParameters;

        if (parameters == null ||
            !parameters.ContainsKey("name") ||
            !parameters.ContainsKey("class"))
        {
            return new APIGatewayHttpApiV2ProxyResponse
            {
                Body = JsonSerializer.Serialize(new
                {
                    error = "Parameters 'name' and 'class' are required"
                }),
                StatusCode = 400
            };
        }

        string name = parameters["name"];
        string characterClass = parameters["class"];

        var random = new Random();

        string[] weapons =
        {
            "Iron Sword",
            "Magic Staff",
            "Elven Bow",
            "Dark Dagger"
        };

        string[] missions =
        {
            "Defeat the Forest Guardian",
            "Find the Lost Treasure",
            "Protect the Ancient Castle",
            "Explore the Dark Cave"
        };

        var result = new
        {
            name = name,
            characterClass = characterClass,
            level = random.Next(1, 11),
            health = random.Next(80, 151),
            strength = random.Next(50, 101),
            weapon = weapons[random.Next(weapons.Length)],
            mission = missions[random.Next(missions.Length)]
        };

        return new APIGatewayHttpApiV2ProxyResponse
        {
            Body = JsonSerializer.Serialize(result),
            StatusCode = 200,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            }
        };
    }
}