namespace Sql_Injection
{
    public class MemberInfoModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string AddressName { get; set; }
    }

    public class MemberAccount
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PassWord{ get; set; }
    }
}