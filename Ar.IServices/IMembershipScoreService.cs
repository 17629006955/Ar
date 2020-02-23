using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface IMembershipScoreService
    {
        MembershipScore GetMembershipScoreByCode(string code);
        IList<MembershipScore> GetMembershipScoreListByUserCode(string userCode);
        void Insert(MembershipScore membershipScore);

        void Update(string userCode, int score);
    }
}
