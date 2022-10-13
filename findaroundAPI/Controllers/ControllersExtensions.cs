using System;
using findaroundAPI.Exceptions;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace findaroundAPI.Controllers
{
	public static class ControllersExtensions
	{
		public static ActionResult GetResult<TResult>(this Result<TResult> result)
		{
            return result.Match<ActionResult>(b =>
            {
                return new OkObjectResult(result.ToString());
            }, exception =>
            {
                var statusCode = 500;
                switch (exception)
                {
                    case LoginUserException e:
                        statusCode = 400;
                        break;

                    case ArgumentException e:
                        statusCode = 403;
                        break;
                }

                return new StatusCodeResult(statusCode);
            });
        }
	}
}

