using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface IExperienceVoucherService
    {
        ExperienceVoucher GetExperienceVoucherByCode(string code);
        IList<ExperienceVoucher> GetExperienceVoucherList();

        void Insert(ExperienceVoucher v);

    }
}
