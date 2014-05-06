using Revenj.Http;

namespace Revenj.Authorization
{
	public class Auth : HttpAuth
	{
		public Auth() : base(new PrincipalFactory(), new Authentication(), null) { }
	}
}
