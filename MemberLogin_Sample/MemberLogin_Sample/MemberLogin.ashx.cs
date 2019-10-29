using System.IO;
using System.Web;
using System.Web.SessionState;

namespace MemberLogin_Sample
{
    /// <summary>
    /// Summary description for MemberLogin
    /// </summary>
    public class MemberLogin : IHttpHandler,IRequiresSessionState
    {
        readonly MemberService _memberService = new MemberService();

        public void ProcessRequest(HttpContext context)
        {
            var password = context.Request["password"];
            var userName = context.Request["userName"];

            if (_memberService.IsUserLogin(userName, password))
            {
                context.Session["IsLogin"] = true;
                context.Response.Redirect("~/MemberInfo.ashx");
            }
            else
            {
               var loginPageHtml = File.ReadAllText(context.Server.MapPath("~/loginPage.html"))
                   .Replace("<LoginFail/>", "登入失敗!");
                context.Response.Write(loginPageHtml);
                context.Response.ContentType = "text/html";
            }
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