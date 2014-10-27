using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Security;
using Revenj.Security;

namespace Revenj.Authorization
{
	class Authentication : IAuthentication<String>
	{
		private static readonly string User;
		private static readonly string ProjectID;

		static Authentication()
		{
			User = ConfigurationManager.AppSettings["User"];
			ProjectID = ConfigurationManager.AppSettings["ProjectID"];
			if (string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(ProjectID))
				throw new ConfigurationErrorsException("Check settings: User, ProjectID");
		}

		public bool IsAuthenticated(string user, SecureString password)
		{
			return user == User && ProjectID == Marshal.PtrToStringBSTR(Marshal.SecureStringToBSTR(password));
		}

		public bool IsAuthenticated(string user, string password)
		{
			return user == User && ProjectID == password;
		}
	}

}
