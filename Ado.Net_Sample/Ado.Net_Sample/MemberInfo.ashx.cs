using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Ado.Net_Sample
{
    public class MemberInfo : IHttpHandler
    {
        readonly MemberService _memberService = new MemberService();

        public void ProcessRequest(HttpContext context)
        {
            #region orignal code.
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<table border='1'>");
            //sb.Append("<tr>");
            //sb.Append("<td>Id</td>");
            //sb.Append("<td>Name</td>");
            //sb.Append("<td>Age</td>");
            //sb.Append("<td>AddressName</td>");
            //sb.Append("<tr/>");
            //SetTableData(sb);
            //sb.Append("</table>");
            //context.Response.Write(sb.ToString()); 
            #endregion

            StringBuilder sb = new StringBuilder();
            SetTableData(sb);
            string filePath = context.Server.MapPath("~/Template/MemberList.template");
            string templateContent= File.ReadAllText(filePath);
            string replaceContent= templateContent.Replace("@{content}", sb.ToString());
            context.Response.Write(replaceContent); 

            context.Response.ContentType = "text/html";
        }

        private void SetTableData(StringBuilder sb)
        {
            foreach (var item in _memberService.GetMemberInfo("Daniel"))
            {
                sb.Append("<tr>");
                sb.Append($"<td>{item.Id}</td>");
                sb.Append($"<td>{item.Name}</td>");
                sb.Append($"<td>{item.Age}</td>");
                sb.Append($"<td>{item.AddressName}</td>");
                sb.Append("<tr/>");
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