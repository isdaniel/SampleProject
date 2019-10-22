using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ado.Net_Sample
{
    /// <summary>
    /// Summary description for MemberLogin
    /// </summary>
    public class MemberLogin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var password = context.Request.Form["password"];
            var userName = context.Request.Form["userName"];
            //todo get password from database by username;
          
            //todo compare with password.

            context.Response.Redirect("~/MemberInfo.ashx");
            context.Response.ContentType = "text/html";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}