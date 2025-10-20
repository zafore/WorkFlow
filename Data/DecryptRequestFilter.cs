using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WorkFlow.Data;



public class DecryptRequestFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var encryptionService = context.HttpContext.RequestServices.GetService<EncryptionService>();

            foreach (var key in context.ActionArguments.Keys.ToList())
            {
                if (context.ActionArguments[key] is string encryptedValue)
                {
                    var decryptedValue = encryptionService.Decrypt(encryptedValue);
                    context.ActionArguments[key] = decryptedValue;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }

