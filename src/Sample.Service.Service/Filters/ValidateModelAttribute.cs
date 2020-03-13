using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Sample.Service.Service.Models;

namespace Sample.Service.Service.Filters
{
    /// <summary>
    /// Validate model attribute.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        #region :: Properties ::

        /// <summary>
        /// The custom errors.
        /// </summary>
        private readonly CustomErrors _customErrors;

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Service.Filters.ValidateModelAttribute"/> class.
        /// </summary>
        /// <param name="customErrors">Custom errors.</param>
        public ValidateModelAttribute(IOptions<CustomErrors> customErrors)
        {
            _customErrors = customErrors.Value;
        }

        #endregion

        #region :: Methods ::

        #region  Overrided Methods

        /// <summary>
        /// Ons the action executing.
        /// </summary>
        /// <param name="context">Context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);

                var code = HttpStatusCode.BadRequest;
                ErrorDetails errorDetails = _customErrors.Errors
                    .Where(item => item.httpStatuscode.Equals((int)code))
                    .Select(error => new ErrorDetails(error.type, code.ToString(), SetErrorMessages(context)))
                    .FirstOrDefault();

                ContentResult content = new ContentResult
                {
                    ContentType = "application/json",
                    StatusCode = (int)code,
                    Content = JsonSerializer.Serialize(errorDetails)
                };
                context.Result = content;

                base.OnActionExecuting(context);
            }
        }

        #endregion

        /// <summary>
        /// Sets the error messages.
        /// </summary>
        /// <returns>The error messages.</returns>
        /// <param name="context">Context.</param>
        public List<Error> SetErrorMessages(ActionContext context)
        {
            List<Error> errors = new List<Error>();

            foreach (var keyModelStatePair in context.ModelState)
            {
                Error error = new Error();
                error.parameter = keyModelStatePair.Key;
                var valueErrors = keyModelStatePair.Value.Errors;

                if (valueErrors != null && valueErrors.Any())
                {
                    error.code = valueErrors.ToString();
                    error.message = GetErrorMessage(valueErrors.FirstOrDefault());

                    errors.Add(error);
                }
            }

            return errors;
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <returns>The error message.</returns>
        /// <param name="error">Error.</param>
        string GetErrorMessage(ModelError error)
        {
            return string.IsNullOrEmpty(error.ErrorMessage) ? "The input was not valid." : error.ErrorMessage;
        }

        #endregion
    }
}
