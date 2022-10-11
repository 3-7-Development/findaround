using System;
namespace findaroundShared.Models.Dtos
{
	public class LoginUserDto
	{
		public string Login { get; set; }
		public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}

