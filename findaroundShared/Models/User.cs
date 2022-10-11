using System;
namespace findaroundShared.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string ProfileImage { get; set; }

		public readonly bool loggedIn;
		public bool LoggedIn { get => loggedIn; }
	}
}

