using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Net;
using System.Security.Principal;
using System.ServiceModel.Web;
using System.Text;
using Revenj.Security;

namespace Revenj.Authorization
{
	public class AuthorizationPolicy : IAuthorizationPolicy
	{
		private readonly IAuthentication<string> Authentication = new Authentication();

		public bool Evaluate(EvaluationContext evaluationContext, ref object state)
		{
			var client = GetClientIdentity(evaluationContext);

			evaluationContext.Properties["Principal"] = new GenericPrincipal(client, new[] { client.Name });
			Id = Guid.NewGuid().ToString();

			return true;
		}

		private class RestIdentity : IIdentity
		{
			public string AuthenticationType { get; internal set; }
			public bool IsAuthenticated { get; internal set; }
			public string Name { get; internal set; }
		}

		private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
		{
			object obj;
			if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
			{
				var authorization = WebOperationContext.Current.IncomingRequest.Headers[HttpRequestHeader.Authorization];
				if (authorization == null)
				{
					WebOperationContext.Current.OutgoingResponse.Headers["WWW-Authenticate"] = "Basic realm=\"site\"";
					throw new Exception("Authorization header not provided.");
				}

				var splt = authorization.Split(' ');
				var authType = splt[0];
				if (splt.Length != 2)
					throw new Exception("Invalid authorization header.");

				var cred = Encoding.UTF8.GetString(Convert.FromBase64String(splt[1])).Split(':');
				if (cred.Length != 2)
					throw new Exception("Invalid authorization header content.");

				var user = cred[0];

				if (string.IsNullOrEmpty(user))
					throw new Exception("User not specified in authorization header.");

				var identity = new RestIdentity
				{
					AuthenticationType = authType,
					IsAuthenticated = Authentication.IsAuthenticated(user, cred[1]),
					Name = user
				};
				if (!identity.IsAuthenticated)
					throw new Exception("User " + user + " is not allowed");
				return identity;
			}

			var identities = obj as IList<IIdentity>;
			if (identities == null || identities.Count < 1)
				throw new Exception("No Identity found");

			return identities[0];
		}

		public ClaimSet Issuer { get { return ClaimSet.System; } }
		public string Id { get; private set; }
	}
}
