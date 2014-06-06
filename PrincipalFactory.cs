using System.Security.Principal;
using NGS.Security;

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

