using NUnit.Framework;
using RateCalculator.Loans;
using System;
using System.IO;


namespace RateCalculator.Tests
{
    [TestFixture]
    public class OutputQuoteTests
    {
        private OutputQuote outputQuote;

        [SetUp]

        public void BeforeEachTest()
        {
            outputQuote = new OutputQuote();
        }


        [Test]
        public void ShouldThrowArgumentNullExceptionIfTheOutputQuoteIsNull()
        {
            
            Assert.That(() => outputQuote.QuoteResult(null), Throws.TypeOf<ArgumentNullException>());

        }

        [Test]
        public void ShouldDisplayMessageIfThereAreInsufficientOffers()
        {
            using (StringWriter sw = new StringWriter())
            {
                System.Console.SetOut(sw);

                outputQuote.InsufficientOffers();

                string expected = $"Not possible to provide a quote at this time" + Environment.NewLine;

                Assert.AreEqual(expected,sw.ToString());
            }
        }

        [Test]
        public void ShouldDisplayCorrectOutputForAComputedQuote()
        {
            using (StringWriter sw = new StringWriter())
            {
                System.Console.SetOut(sw);

                var quote = new ComputedQuote { MonthlyRepayment = 35.25m, Rate = 0.075m, RequestedAmount = 1000, TotalRepayment = 1250 };

                outputQuote.QuoteResult(quote);


                string expected = $"Requested amount: {quote.RequestedAmount}" + Environment.NewLine +
                                  $"Rate: {quote.Rate:P1}" + Environment.NewLine +
                                  $"Monthly repayment: {quote.MonthlyRepayment}" + Environment.NewLine +
                                  $"Total repayment: {quote.TotalRepayment}" + Environment.NewLine; ;


               

                Assert.AreEqual(expected, sw.ToString());
                               
            }
        }
    }
}
