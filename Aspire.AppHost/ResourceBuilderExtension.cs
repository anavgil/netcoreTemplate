using System.Diagnostics;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Aspire.AppHost;

internal static class ResourceBuilderExtension
{
    public static IResourceBuilder<T> WithScalar<T>(this IResourceBuilder<T> builder)
        where T : IResourceWithEndpoints
    {
        return builder.WithOpenApiDocs("scalar-docs", "Scalar Api Documentation", "scalar/v1");
    }


    private static IResourceBuilder<T> WithOpenApiDocs<T>(this IResourceBuilder<T> builder,
        string name,
        string displayName,
        string openApiUIPath)
        where T : IResourceWithEndpoints
    {
        return builder.WithCommand<T>(name, displayName, executeCommand: async _ =>
        {
            try
            {
                var endpoint = builder.GetEndpoint("https");
                var url = $"{endpoint.Url}/{openApiUIPath}";

                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

                //return new ExecuteCommandResult { Success = true };
                return await Task.FromResult(new ExecuteCommandResult
                {
                    Success = true
                });
            }
            catch (Exception ex)
            {
                //return new ExecuteCommandResult { Success = false, ErrorMessage = ex.Message };
                return await Task.FromResult(new ExecuteCommandResult
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        },commandOptions: new CommandOptions()
        {
            ConfirmationMessage = "Are you sure you want to open the OpenAPI UI?", 
        });
    }
}
