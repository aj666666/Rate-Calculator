using System.Collections.Generic;

namespace RateCalculator.Loans
{
    public interface IQuoteCalculation
    {
        ComputedQuote GetQuote(int loanAmount, IList<LenderOffer> offers);
    }
}
