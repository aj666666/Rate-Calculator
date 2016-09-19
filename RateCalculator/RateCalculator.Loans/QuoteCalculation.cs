using RateCalculator.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace RateCalculator.Loans
{
    public class QuoteCalculation : IQuoteCalculation
    {

        private static readonly int CompoundedPerYear = Common.CompoundedPerYear;
        private static readonly int Years = Common.Years;


        public ComputedQuote GetQuote(int loanAmount, IList<LenderOffer> loanOffers)
        {

            if (loanOffers == null) { throw new ArgumentNullException(nameof(loanOffers)); }

            if (loanOffers.Count == 0) { return null; }

            if (loanOffers.Sum(x => x.LenderAmount) < loanAmount) { return null; }

            var totalPayBack = ComputeTotalPayBack(loanAmount, loanOffers); 
           
            // Formula to get the monthly compounded interest rate
            var rate = Pow((double)totalPayBack / loanAmount, (1.0 / ((CompoundedPerYear * Years) / CompoundedPerYear))) - 1;
            
            var monthlyPayment = totalPayBack / (CompoundedPerYear * Years);

            totalPayBack = Round(totalPayBack, 2);
            rate = Round(rate, 3);
            monthlyPayment = Round(monthlyPayment, 2);

            return new ComputedQuote
            {

                RequestedAmount = loanAmount,
                Rate = (decimal)rate,
                TotalRepayment = totalPayBack,
                MonthlyRepayment = monthlyPayment

            };

        }

        private decimal ComputeTotalPayBack(int loanAmount, IList<LenderOffer> loanOffers)
        {
            decimal totalPayBack = 0;


            foreach (var loanOffer in loanOffers.OrderBy(o => o.LenderRate).ThenByDescending(o => o.LenderAmount))
            {
                totalPayBack = CalculatePayBackForEachOffer(totalPayBack, loanOffer, loanAmount);

                if (loanAmount >= loanOffer.LenderAmount)
                {
                    loanAmount -= loanOffer.LenderAmount;

                    if (loanAmount <= 0) { break; }
                }

                else
                {
                    break;
                }
            }

            return totalPayBack;
        }

        private decimal CalculatePayBackForEachOffer(decimal totalPayBack, LenderOffer loanOffer, int loanAmount)
        {
            decimal amountToUse;

            // Use the lower of the offer amount or the reducing loan amount
            amountToUse = loanAmount >= loanOffer.LenderAmount ? loanOffer.LenderAmount : loanAmount;

            // Formula for monthly compounded interest
            return totalPayBack += amountToUse * (decimal)(Pow(1 + ((double)loanOffer.LenderRate / CompoundedPerYear), (CompoundedPerYear * Years)));

        }
    }
}
