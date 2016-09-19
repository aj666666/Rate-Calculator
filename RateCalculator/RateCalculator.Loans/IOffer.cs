using System.Collections.Generic;

namespace RateCalculator.Loans
{
    public interface IOffer
    {
        IList<LenderOffer> Load(string fileName);
    
    }
}
