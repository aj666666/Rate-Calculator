using RateCalculator.Loans;
using System;
using System.Collections.Generic;
using static System.Math;

namespace RateCalculator.Console
{
    public class CalculationController : ICalculationController
    {

        private readonly IOffer lenderOffers;
        private readonly IOutputQuote outputQuote;
        private readonly IQuoteCalculation calculateQuote;

        private readonly int LoanMaximumValue = Common.LoanMaximumValue;
        private readonly int LoanMinimumValue = Common.LoanMinimumValue;
        private readonly int LoanStep = Common.LoanStep;


        public CalculationController(IOffer lenderOffers, IOutputQuote outputQuote, IQuoteCalculation calculateQuote)

        {
            this.lenderOffers = lenderOffers;
            this.outputQuote = outputQuote;
            this.calculateQuote = calculateQuote;

        }


        public void Start(IList<string> parameters)
        {

            ValidateParameters(parameters);

            var allOffers = lenderOffers.Load(parameters[0]);

            int loanAmount;

            int.TryParse(parameters[1], out loanAmount);

            var finalQuote = calculateQuote.GetQuote(loanAmount, allOffers);


            if (finalQuote == null)
            {
                outputQuote.InsufficientOffers();
            }

            else
            {
                outputQuote.QuoteResult(finalQuote);
            }


        }


        private void ValidateParameters(IList<string> parameters)
        {

            if (parameters == null) { throw new ArgumentNullException(nameof(parameters)); }

            if (parameters.Count < 2 || parameters.Count > 2) { throw new ArgumentException($": Only 2 parameters should be supplied not {parameters.Count} ", nameof(parameters)); }
            
            int loanAmount;
            int remainder;

            if (!int.TryParse(parameters[1], out loanAmount))
            {
                throw new ArgumentException("Invalid value for loan amount parameter.", nameof(loanAmount));
            }

            else

            {
                if (loanAmount < LoanMinimumValue || loanAmount > LoanMaximumValue)
                {
                    throw new ArgumentOutOfRangeException($": Loan amount must be greater than or equal to {LoanMinimumValue}, and less than or equal to {LoanMaximumValue}", nameof(loanAmount));
                }

                DivRem(loanAmount, LoanStep, out remainder);

                if (remainder != 0)
                {
                    throw new ArgumentException($": Loan amount must be in increments of {LoanStep}", nameof(loanAmount));
                }

            }


        }

    }
}
