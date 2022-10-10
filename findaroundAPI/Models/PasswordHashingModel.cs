using System;
namespace findaroundAPI.Models
{
	public class PasswordHashingModel
	{
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}

