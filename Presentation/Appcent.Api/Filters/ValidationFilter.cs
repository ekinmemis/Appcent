using System.Collections.Generic;
using System.Linq;

using Appcent.Api.Models.Errors;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Appcent.Api.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorModel errorModel = new();
                errorModel.Status = 400;

                IEnumerable<ModelError> modelErrors = context.ModelState.Values.SelectMany(f => f.Errors);
                modelErrors.ToList().ForEach(f =>
                {
                    errorModel.Errors.Add(f.ErrorMessage);
                });

                context.Result = new BadRequestObjectResult(errorModel);
            }
        }
    }
}