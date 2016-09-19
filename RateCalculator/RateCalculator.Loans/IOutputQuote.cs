namespace RateCalculator.Loans
{
    public interface IOutputQuote
    {
        void QuoteResult(ComputedQuote computedQuote);
        
        void InsufficientOffers();

    }
}
