using System;
using System.Linq;
using System.Text;
using System.Web;

namespace Ado.Net_Sample
{
    public class MemberInfo : IHttpHandler
    {
        MemberService memberService = new MemberService();
        public void ProcessRequest(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table border='1'>");

            sb.Append("<tr>");
            sb.Append("<td>Id</td>");
            sb.Append("<td>Name</td>");
            sb.Append("<td>Age</td>");
            sb.Append("<td>AddressName</td>");
            sb.Append("<tr/>");

            foreach (var item in memberService.GetMemberInfo("Daniel"))
            {
                sb.Append("<tr>");
                sb.Append($"<td>{item.Id}</td>");
                sb.Append($"<td>{item.Name}</td>");
                sb.Append($"<td>{item.Age}</td>");
                sb.Append($"<td>{item.AddressName}</td>");
                sb.Append("<tr/>");
            }

            sb.Append("</table>");

            context.Response.Write(sb.ToString());
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