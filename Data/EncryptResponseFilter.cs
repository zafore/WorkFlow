
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WorkFlow.Data;

public class EncryptResponseFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var encryptionService = context.HttpContext.RequestServices.GetService<EncryptionService>();

            if (context.Result is ObjectResult objectResult && objectResult.Value is string plainText)
            {
                var encryptedValue = encryptionService.Encrypt(plainText);
                objectResult.Value = encryptedValue;
            }
        }
    }

