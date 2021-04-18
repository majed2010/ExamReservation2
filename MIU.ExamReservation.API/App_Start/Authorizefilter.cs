using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Authentication.WebApi
{
    class Authorizefilter : IOAuthBearerAuthenticationProvider
    {
        public static string[] blackList = {"--",";--",";","/*","*/","@@",
                                           "nchar","varchar","nvarchar",
                                           " alter ","begin ","cast","create ","cursor "
                                           ,"declare ","delete ","drop ","exec","execute",
                                           "fetch","insert ","kill ","open ",
                                            "sysobjects","syscolumns",
                                           "table ","update "};
        public Task RequestToken(OAuthRequestTokenContext context)
        {

            return Task.FromResult<object>(null);
        }
        public Task ApplyChallenge(OAuthChallengeContext context)
        {
            return Task.FromResult<object>(null);
        }
        public Task ValidateIdentity(OAuthValidateIdentityContext context)
        {          
            System.Security.Claims.Claim claim = context.Ticket.Identity.Claims.Where(x => x.Type == "userGetwayIP" && x.Value.ToString() == HttpContext.Current.Request.UserHostAddress.ToString()).FirstOrDefault();
            if (claim == null)
            {
                context.SetError("Inform that you are a good boy :) don't try this step again");
                context.Rejected();
                context.Response.StatusCode = 400;
            }
            else if (checkInjection(context))
            {
                context.SetError("Not acceptable input");
                context.Rejected();
                context.Response.StatusCode = 406;
            }
            return Task.FromResult<object>(null);
        }
        private bool CheckInput(string parameter, string KEY, OAuthValidateIdentityContext context)
        {
            bool isInvalid = false;
            for (int i = 0; i < blackList.Length; i++)
            {
                if ((parameter.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                {                  
                    isInvalid = true;
                    break;
                }
            }
            return isInvalid;
        }
        bool checkInjection(OAuthValidateIdentityContext context)
        {
            bool isInvalid = false;
            foreach (string key in HttpContext.Current.Request.QueryString)
            {
                if (CheckInput(HttpContext.Current.Request.QueryString[key], key, context))
                {
                    isInvalid = true;
                    break;
                }
            }
            foreach (string key in HttpContext.Current.Request.Form)
            {
                if(CheckInput(HttpContext.Current.Request.Form[key], key, context))
                {
                    isInvalid = true;
                    break;
                }
            }
                
            foreach (string key in HttpContext.Current.Request.Cookies)
                if (CheckInput(HttpContext.Current.Request.Cookies[key].Value, key, context))
                {
                    isInvalid = true;
                    break;
                }
            return isInvalid;
        }
    }

}
