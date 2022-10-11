using System;
namespace findaroundAPI.Exceptions
{
	public class LoginUserException : Exception
	{
		public LoginUserException(string msg) : base(msg)
		{
		}
	}
}

