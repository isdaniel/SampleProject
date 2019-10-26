using System;
using System.Collections.Generic;

namespace Ado.Net_Sample
{
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
    }
}