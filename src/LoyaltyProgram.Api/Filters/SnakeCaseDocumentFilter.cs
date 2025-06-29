using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.RegularExpressions;

namespace LoyaltyProgram.Api.Filters
{
    public class SnakeCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.ToDictionary(entry => ToSnakeCase(entry.Key), entry => entry.Value);

            swaggerDoc.Paths.Clear();
            foreach (var entry in paths)
            {
                swaggerDoc.Paths.Add(entry.Key, entry.Value);
            }
        }

        private string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            var result = Regex.Replace(input,  @"/([A-Z][a-z]+)", match => match.Value.ToLowerInvariant());
            return result;
        }
    }
}