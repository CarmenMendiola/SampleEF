using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using NLog;

namespace Sample.Service.Service.Extensions
{
    /// <summary>
    /// Secrets manager.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class SecretsManager
    {
        #region :: Properties ::

        /// <summary>
        /// looger
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger(); // creates a logger using the class name

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Gets the secret.
        /// </summary>
        /// <returns>The secret.</returns>
        public static string GetSecret()
        {
            string? secretName = Environment.GetEnvironmentVariable("AWS_SECRETS_MANAGER_NAME");

            string? region = Environment.GetEnvironmentVariable("AWS_SECRETS_MANAGER_REGION");

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = Environment.GetEnvironmentVariable("AWS_SECRETS_MANAGER_VERSION_STAGE") // VersionStage defaults to AWSCURRENT if unspecified.
            };

            GetSecretValueResponse? response = null;

            // In this sample we only handle the specific exceptions for the 'GetSecretValue' API.
            // See https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
            // We rethrow the exception by default.

            try
            {
                response = client.GetSecretValueAsync(request).Result;
            }
            catch (DecryptionFailureException e)
            {
                // Secrets Manager can’t decrypt the protected secret text using the provided KMS key.
                // Deal with the exception here, and/or rethrow at your discretion.
                logger.Error(e);

                throw;
            }
            catch (InternalServiceErrorException e)
            {
                // An error occurred on the server side.
                // Deal with the exception here, and/or rethrow at your discretion.
                logger.Error(e);

                throw;
            }
            catch (InvalidParameterException e)
            {
                // You provided an invalid value for a parameter.
                // Deal with the exception here, and/or rethrow at your discretion
                logger.Error(e);

                throw;
            }
            catch (InvalidRequestException e)
            {
                // You provided a parameter value that is not valid for the current state of the resource.
                // Deal with the exception here, and/or rethrow at your discretion.
                logger.Error(e);

                throw;
            }
            catch (ResourceNotFoundException e)
            {
                // We can’t find the resource that you asked for.
                // Deal with the exception here, and/or rethrow at your discretion.
                logger.Error(e);

                throw;
            }
            catch (AggregateException ae)
            {
                // More than one of the above exceptions were triggered.
                // Deal with the exception here, and/or rethrow at your discretion.
                logger.Error(ae);

                throw;
            }

            // Decrypts secret using the associated KMS CMK.
            // Depending on whether the secret is a string or binary, one of these fields will be populated.
            if (response.SecretString != null)
            {
                return response.SecretString;
            }

            var memoryStream = response.SecretBinary;

            StreamReader reader = new StreamReader(memoryStream);

            string decodedBinarySecret = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));

            return decodedBinarySecret;
        }

        #endregion
    }
}
