using System;
using System.IdentityModel.Selectors;
using NGS.Security;

namespace Revenj.Authorization
{
	public class LoginValidator: UserNamePasswordValidator
	{
		private readonly IAuthentication<string> Authentication = new Authentication();

		public override void Validate(string userName, string password)
		{
			if (!Authentication.IsAuthenticated(userName, password))
				throw new Exception("User " + userName + " is not allowed");
		}
	}
}
