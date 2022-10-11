using System;
namespace findaroundAPI.Exceptions
{
	public class RegisterUserException : Exception
	{
		public RegisterUserException(string msg) : base(msg)
		{
		}
	}
}

