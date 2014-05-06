using System.Security.Principal;


namespace Revenj.Authorization
{
	class Principal: IPrincipal
	{
		private readonly IIdentity User;

		public Principal(IIdentity user)
		{
			User = user;
		}

		public IIdentity Identity {
			get {
				return User;
			}
		}

		public bool IsInRole(string role)
		{
			return User.Name == role;
		}
	}
}

