using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Sample.Service.Service.Extensions
{
    /// <summary>
    /// Utils functions.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class StartupUtils
    {
        #region :: Methods ::

        /// <summary>
        /// Get the value of a key in secrets manager values.
        /// </summary>
        /// <param name="key">Key of the value to get.</param>
        /// <param name="secrets">Dictionary with the secrets manager values.</param>
        /// <returns>The value.</returns>
        public static string GetSecretsValue(string key, Dictionary<string, string> secrets)
        {
            string value = string.Empty;
            if (secrets != null && secrets.ContainsKey(key))
            {
                value = secrets[key];
            }

            return value;
        }

        /// <summary>
        /// Get the version of the assembly.
        /// </summary>
        /// <returns>Version.</returns>
        public static string GetAssemblyVersion()
        {
            return typeof(Startup).Assembly.GetName().Version?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Health response writer.
        /// </summary>
        /// <param name="context">Http context.</param>
        /// <param name="healthReport">Health report.</param>
        /// <returns>The async.</returns>
        public static async Task HealthResponseWriter(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new System.Text.Json.JsonWriterOptions
            {
                Indented = true
            };

            using (var stream = new MemoryStream())
            {
                using (var writer = new System.Text.Json.Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    writer.WriteString("version", GetAssemblyVersion());
                    writer.WriteString("status", healthReport.Status.Equals(HealthStatus.Healthy) ? HttpStatusCode.OK.ToString() : "internal_server_error");

                    if (!healthReport.Status.Equals(HealthStatus.Healthy) && healthReport.Entries != null && healthReport.Entries.Count > 0)
                    {
                        writer.WriteStartObject("details");
                        foreach (var entry in healthReport.Entries)
                        {
                            writer.WriteStartObject(entry.Key);
                            writer.WriteString("status", entry.Value.Status.ToString());

                            if (entry.Value.Data != null && entry.Value.Data.Count > 0)
                            {
                                writer.WriteStartObject("data");
                                foreach (var item in entry.Value.Data)
                                {
                                    writer.WritePropertyName(item.Key);
                                    System.Text.Json.JsonSerializer.Serialize(
                                        writer, item.Value, item.Value?.GetType() ??
                                        typeof(object));
                                }
                                writer.WriteEndObject();
                            }

                            writer.WriteEndObject();
                        }

                        writer.WriteEndObject();
                    }

                    writer.WriteEndObject();
                }

                var json = Encoding.UTF8.GetString(stream.ToArray());

                await context.Response.WriteAsync(json);
            }
        }

        #endregion
    }
}
