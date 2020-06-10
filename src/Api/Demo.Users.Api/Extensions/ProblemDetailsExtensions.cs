using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Users.Api.Extensions
{
    public static class ProblemDetailsExtensions
    {
        public static IEnumerable<ProblemDetails> ToProblemDetails(this IDictionary<string, string[]> validationErrors, int? status)
        {
            var res = new List<ProblemDetails>();

            foreach (var item in validationErrors)
            {
                res.Add(new ProblemDetails
                {
                    Detail = string.Join($".{Environment.NewLine}", item.Value),
                    Status = status,
                    Title = $"Validation failed for member {item.Key}",
                });
            }

            return res;
        }
    }
}
