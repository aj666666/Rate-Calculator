using System;
using static System.Console;

namespace RateCalculator.Loans
{

    public class OutputQuote : IOutputQuote
    {
        public void QuoteResult(ComputedQuote computedQuote)
        {
            if (computedQuote == null) { throw new ArgumentNullException(nameof(computedQuote)); }
          
            WriteLine($"Requested amount: {computedQuote.RequestedAmount}");
            WriteLine($"Rate: {computedQuote.Rate:P1}");
            WriteLine($"Monthly repayment: {computedQuote.MonthlyRepayment}");
            WriteLine($"Total repayment: {computedQuote.TotalRepayment}");
        }
    

        public void InsufficientOffers()
        {
            WriteLine("Not possible to provide a quote at this time"); 
        }

        
    }
}
