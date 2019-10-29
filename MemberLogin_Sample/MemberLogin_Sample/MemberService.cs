using System;
using System.Collections.Generic;

namespace MemberLogin_Sample
{
    public class MemberAccountModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }

    public class MemberService
    {
        MemberDao _memberDao = new MemberDao(); 

        public IEnumerable<MemberInfoModel> GetMemberInfo(string memberName)
        {
            return _memberDao.GetMemberInfo(memberName);
        }

        public bool IsUserLogin(string userName,string passWord)
        {
            var userAccount = _memberDao.GetUserAccount(userName);
            
            return userAccount != null && string.Equals(userAccount.PassWord,passWord,StringComparison.OrdinalIgnoreCase);
        }

        public bool CreateMemberAccount(MemberAccountModel model)
        {
            return _memberDao.CreateMemberAccount(model) > 0;
        }
    }
}