using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

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
        return builder.WithCommand(name, displayName,executeCommand : async _ =>
        {
            try
            {
                var endpoint = builder.GetEndpoint("https");
                var url = $"{endpoint.Url}/{openApiUIPath}";

                
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

                return new ExecuteCommandResult { Success = true };
            }
            catch (Exception ex)
            {
                return new ExecuteCommandResult { Success = false , ErrorMessage = ex.Message };
            }
        },
        updateState: context => context.ResourceSnapshot.HealthStatus == HealthStatus.Healthy ? ResourceCommandState.Enabled : ResourceCommandState.Disabled);
    }
}   
