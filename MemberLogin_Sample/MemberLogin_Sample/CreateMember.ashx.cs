using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MemberLogin_Sample
{
    public class CreateMember : IHttpHandler
    {

        MemberService _memberService = new MemberService();
        public void ProcessRequest(HttpContext context)
        {
            string username  = context.Request["username"];
            string password = context.Request["password"];
            string confirmPassword  = context.Request["confirmPassword"];
           

            if (password != confirmPassword)
            {
                string createAccountFilePath = context.Server.MapPath("~/CreateAccount.html");
                string html = File.ReadAllText(createAccountFilePath);
                context.Response.Write("<script>alert('請確認密碼和密碼確認是否一樣')</script>");
                context.Response.Write(html);
                return;
            }

            MemberAccountModel memberAccountModel = new MemberAccountModel()
            {
                UserName = username,
                PassWord =  password
            };

            if (_memberService.CreateMemberAccount(memberAccountModel))
            {
                context.Response.Write("<script>alert('建立帳號成功,請重新登入!')</script>");
                string html = File.ReadAllText(context.Server.MapPath("~/loginPage.html"));
                context.Response.Write(html);
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