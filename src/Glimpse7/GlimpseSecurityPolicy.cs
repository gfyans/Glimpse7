using System.Web;
using System.Web.Security;
using Glimpse.Core.Extensibility;
using Umbraco.Core.Security;

namespace Glimpse7
{
    public class GlimpseSecurityPolicy:IRuntimePolicy
    {
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            // You can perform a check like the one below to control Glimpse's permissions within your application.
            // More information about RuntimePolicies can be found at http://getglimpse.com/Help/Custom-Runtime-Policy
            FormsAuthenticationTicket ticket = new HttpContextWrapper(HttpContext.Current).GetUmbracoAuthTicket();

            if (string.IsNullOrWhiteSpace(ticket?.Name) || ticket.Expired)
            {
                return RuntimePolicy.Off;
            }

            return RuntimePolicy.On;
        }

        public RuntimeEvent ExecuteOn
        {
			// The RuntimeEvent.ExecuteResource is only needed in case you create a security policy
			// Have a look at http://blog.getglimpse.com/2013/12/09/protect-glimpse-axd-with-your-custom-runtime-policy/ for more details
            get { return RuntimeEvent.EndRequest | RuntimeEvent.ExecuteResource; }
        }
    }
}
