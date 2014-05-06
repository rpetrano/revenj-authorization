using NGS.Security;
using System.Security.Principal;

namespace Revenj.Authorization
{
	class PrincipalFactory: IPrincipalFactory
	{
		public IPrincipal Create(IIdentity user)
		{
			return new Principal (user);
		}
	}
}

