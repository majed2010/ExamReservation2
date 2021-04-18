


using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Authentication.WebApi.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {


        public override Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            return base.GrantCustomExtension(context);
        }
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            return base.GrantClientCredentials(context);
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return base.TokenEndpoint(context);
        }
        public override Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        {
            return base.GrantAuthorizationCode(context);
        }
        public override Task AuthorizationEndpointResponse(OAuthAuthorizationEndpointResponseContext context)
        {
            return base.AuthorizationEndpointResponse(context);
        }
        public override Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            return base.AuthorizeEndpoint(context);
        }
        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {


            return base.MatchEndpoint(context);
        }
        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return base.ValidateTokenRequest(context);
        }
        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            return base.TokenEndpointResponse(context);
        }

        public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            return base.ValidateAuthorizeRequest(context);
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {


            context.Validated();
            return Task.FromResult<object>(null);
        }


        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return base.GrantRefreshToken(context);
        }

      
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            
            ////if (!ValidateUser(context.UserName, context.Password))
            ////{
            ////    //  context.SetError("invalid_grant", "The user name or password is incorrect.");
            ////    // return;
            ////}
            ////// else
            ////{

            //    app.LoadApplicantState(context.UserName);
            ////}
            ////if(!AdManager.GetUserLogin(context.UserName, context.Password))
            ////{
            ////    context.SetError("invalid_grant", "The user name or password is incorrect.");
            ////    return;
            ////}
            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("username", context.UserName));
            //identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            //identity.AddClaim(new Claim("StudentId", app.StudentID.ToString()));
            //identity.AddClaim(new Claim("AppCode", app.AppCode == null ? "" : app.AppCode));
            //identity.AddClaim(new Claim("FacultyID", app.FacultyID.ToString()));
            //identity.AddClaim(new Claim("userGetwayIP", GetClientIP()));
            //identity.AddClaim(new Claim("Status", app.AcademicStatusId.ToString()));
            ////var props = new AuthenticationProperties()
            ////{
            ////    IssuedUtc = DateTime.UtcNow,
            ////    ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            ////};
            ////identity.AddClaim(new Claim("StudentId", app.StudentID.ToString()));
            ////identity.AddClaim(new Claim("AppCode", app.AppCode.ToString()));
            ////identity.AddClaim(new Claim("FacultyID", app.FacultyID.ToString()));
            ////identity.AddClaim(new Claim("userGetwayIP", GetClientIP()));
            //var props = new AuthenticationProperties(new Dictionary<string, string>
            //    {
                   
            //        { 
            //            "userName", context.UserName
            //        }
            //    }
            //    );
            //var ticket = new AuthenticationTicket(identity, props);
            //context.Validated(ticket);
             
        }


        public string GetClientIP()
        {
            return HttpContext.Current.Request.UserHostAddress.ToString();

        }
    }
}