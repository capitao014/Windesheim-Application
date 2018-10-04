using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.Runtime.CompilerServices;
using WindesheimApp.Interfaces;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using WindesheimApp.iOS.Helpers;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(WindesheimApp.iOS.Helpers.Authenticator))]
namespace WindesheimApp.iOS.Helpers
{
    public class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters(controller);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
            return authResult;
        }
    }
}