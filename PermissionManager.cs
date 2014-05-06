using NGS.Extensibility;
using NGS.Security;
using System.Linq;
using System.Security.Principal;
using System;
using System.Linq.Expressions;

namespace Revenj.Authorization
{
	[Service(InstanceScope.Singleton)]
	public class PermissionManager : IPermissionManager
	{
		public IQueryable<T> ApplyFilters<T>(IPrincipal user, IQueryable<T> data)
		{
			return data;
		}

		public bool CanAccess(string identifier, System.Security.Principal.IPrincipal user)
		{
			return true;
		}

		private class Unregister : IDisposable
		{
			public void Dispose() { }
		}

		public IDisposable RegisterFilter<T>(Expression<Func<T, bool>> filter, string role, bool inverse)
		{
			return new Unregister();
		}

		public T[] ApplyFilters<T>(IPrincipal user, T[] data)
		{
			return data;
		}
	}
}
