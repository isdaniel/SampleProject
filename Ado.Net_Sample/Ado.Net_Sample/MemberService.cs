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
    }
}