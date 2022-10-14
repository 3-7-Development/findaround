using System;
using findaroundAPI.Exceptions;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace findaroundAPI.Controllers
{
	public static class ControllersExtensions
	{
		public static ActionResult GetResult<TResult>(this Result<TResult> result)
		{
            return result.Match<ActionResult>(b =>
            {
                return new OkObjectResult(b);
            }, exception =>
            {
                var statusCode = GetStatusCode(exception);
                return new StatusCodeResult(statusCode);
            });
        }

        private static int GetStatusCode(Exception exception)
        {
            var statusCode = 500;

            switch (exception)
            {
                case LoginUserException e:
                    statusCode = 400;
                    break;

                case UserNotLoggedInException e:
                    statusCode = 400;
                    break;

                case ForbidException e:
                    statusCode = 403;
                    break;

                case ArgumentException e:
                    statusCode = 400;
                    break;
            }

            return statusCode;
        }
	}
}

