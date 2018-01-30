using System.Collections.Generic;
using System.Web.Mvc;
using Contoso.Service.Common;

namespace Contoso.Web.Helpers
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Adds a model errors for each validation result from the business service.
        /// </summary>
        /// <param name="validationResults">The validation results from a business service.</param>
        /// <param name="modelState">The model state dictionary used to add errors.</param>
        /// <param name="defaultErrorKey">The default key to use if a field is not specified in a business service valdiation result.</param>
        public static void AddModelErrors(this Controller controller, IEnumerable<ValidationResult> validationResults, string defaultErrorKey = null)
        {
            if (validationResults != null)
            {
                foreach (var validationResult in validationResults)
                {
                    if (!string.IsNullOrEmpty(validationResult.MemberName))
                    {
                        controller.ModelState.AddModelError(validationResult.MemberName, validationResult.Message);
                    }
                    else if (defaultErrorKey != null)
                    {
                        controller.ModelState.AddModelError(defaultErrorKey, validationResult.Message);
                    }
                    else
                    {
                        controller.ModelState.AddModelError(string.Empty, validationResult.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Adds a model errors for each validation result from the business service.
        /// </summary>
        /// <param name="validationResults">The validation results from a business service.</param>
        /// <param name="modelState">The model state dictionary used to add errors.</param>
        /// <param name="defaultErrorKey">The default key to use if a field is not specified in a business service valdiation result.</param>
        public static void AddModelErrors(this ModelStateDictionary modelState, IEnumerable<ValidationResult> validationResults, string defaultErrorKey = null)
        {
            if (validationResults == null) return;

            foreach (var validationResult in validationResults)
            {
                string key = validationResult.MemberName ?? defaultErrorKey ?? string.Empty;
                modelState.AddModelError(key, validationResult.Message);
            }
        }
    }
}