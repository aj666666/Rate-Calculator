using NUnit.Framework;
using RateCalculator.Loans;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RateCalculator.Tests
{
    [TestFixture]
    public class QuoteCalculationTests
    {
        private IQuoteCalculation quoteCalculation;

        [SetUp]

        public void BeforeEachTest()
        {
            quoteCalculation = new QuoteCalculation();
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWhenWhenOffersAreNull()
        {

            Assert.That(() => quoteCalculation.GetQuote(1000, null), Throws.TypeOf<ArgumentNullException>());

        }

              

        [Test]
        [TestCaseSource("NotEnoughOffersTestCase")]

        public ComputedQuote ShouldReturnNullIfThereAreNotEnoughOffers(int loanAmount, List<LenderOffer> offers)
        {

            var quote = quoteCalculation.GetQuote(loanAmount, offers);
           
            Assert.That(quote, Is.Null);
            return quote;

        }


        [Test]
        [TestCaseSource("TotalRepaymentTestCases")]
        public decimal ShouldCalculateTheTotalRepaymentForTheRequestedLoanAmount(int loanAmount, List<LenderOffer> offers)
        {
            var quote = quoteCalculation.GetQuote(loanAmount, offers);

            Assert.That(quote, !Is.Null);

            return quote.TotalRepayment;

        }

        [Test]
        [TestCaseSource("RateTestCases")]
        public decimal ShouldCalculateTheRateForTheRequestedLoanAmount(int loanAmount, List<LenderOffer> offers)
        {
            var quote = quoteCalculation.GetQuote(loanAmount, offers);

            Assert.That(quote, !Is.Null);

            return quote.Rate;

        }

        [Test]
        [TestCaseSource("MonthlyRepaymentTestCase")]

        public decimal ShouldReturnTheCorrectMonthlyAmount(int loanAmount, List<LenderOffer> offers)
        {

            var quote = quoteCalculation.GetQuote(loanAmount, offers);

            Assert.That(quote, !Is.Null);
            return quote.MonthlyRepayment;

        }


        public static IEnumerable NotEnoughOffersTestCase
        {
            get
            {
                yield return new TestCaseData(1000,
                        new List<LenderOffer>
                            {
                                    new LenderOffer { LenderName = "Test", LenderAmount = 900, LenderRate = 0.070m }
                            }).Returns(null);

            }
        }

        public static IEnumerable MonthlyRepaymentTestCase
        {
            get
            {
                yield return new TestCaseData(1000,
                        new List<LenderOffer>
                            {
                                    new LenderOffer { LenderName = "Test", LenderAmount = 1000, LenderRate = 0.075m }
                            }).Returns(34.76);

            }
        }

        public static IEnumerable TotalRepaymentTestCases
        {
            get
            {
                yield return new TestCaseData(1000,
                        new List<LenderOffer>
                            {
                                    new LenderOffer { LenderName = "Test", LenderAmount = 1000, LenderRate = 0.075m }
                            }).Returns(1251.45m);


                yield return new TestCaseData(1000,
                        new List<LenderOffer>
                            {
                                    new LenderOffer { LenderName = "Test", LenderAmount = 1200, LenderRate = 0.075m }
                            }).Returns(1251.45m);


                yield return new TestCaseData(1200,
                        new List<LenderOffer>
                            {
                                    new LenderOffer { LenderName = "Test1", LenderAmount = 1000, LenderRate = 0.075m },
                                    new LenderOffer { LenderName = "Test2", LenderAmount = 500, LenderRate = 0.077m }
                            }).Returns(1503.23m);

                yield return new TestCaseData(1200,
                       new List<LenderOffer>
                           {
                                    new LenderOffer { LenderName = "Test1", LenderAmount = 1000, LenderRate = 0.075m },
                                    new LenderOffer { LenderName = "Test2", LenderAmount = 200, LenderRate = 0.077m }
                           }).Returns(1503.23m);

                yield return new TestCaseData(1500,
                        new List<LenderOffer>
                            {
                                    new LenderOffer { LenderName = "Test1", LenderAmount = 1000, LenderRate = 0.075m },
                                    new LenderOffer { LenderName = "Test2", LenderAmount = 200, LenderRate = 0.077m },
                                     new LenderOffer { LenderName = "Test3", LenderAmount = 300, LenderRate = 0.097m }
                            }).Returns(1904.09m);

                yield return new TestCaseData(1800,
                       new List<LenderOffer>
                           {
                                    new LenderOffer { LenderName = "Test1", LenderAmount = 1000, LenderRate = 0.075m },
                                    new LenderOffer { LenderName = "Test2", LenderAmount = 200, LenderRate = 0.077m },
                                     new LenderOffer { LenderName = "Test3", LenderAmount = 300, LenderRate = 0.097m },
                                       new LenderOffer { LenderName = "Test4", LenderAmount = 500, LenderRate = 0.10m }
                           }).Returns(2308.55);

                yield return new TestCaseData(2800,
               new List<LenderOffer>
                   {
                                    new LenderOffer { LenderName = "Test1", LenderAmount = 1000, LenderRate = 0.075m },
                                    new LenderOffer { LenderName = "Test2", LenderAmount = 200, LenderRate = 0.077m },
                                     new LenderOffer { LenderName = "Test3", LenderAmount = 300, LenderRate = 0.097m },
                                       new LenderOffer { LenderName = "Test4", LenderAmount = 500, LenderRate = 0.10m },
                                       new LenderOffer { LenderName = "Test5", LenderAmount = 800, LenderRate = 0.11m }
                   }).Returns(3689.29);

            }
        }

        public static IEnumerable RateTestCases
        {
            get
            {
                yield return new TestCaseData(1000,
                        new List<LenderOffer>
                            {
                                    new LenderOffer { LenderName = "Test", LenderAmount = 1000, LenderRate = 0.075m }
                            }).Returns(0.078m);


                yield return new TestCaseData(1000,
                        new List<LenderOffer>
                            {
                                    new LenderOffer { LenderName = "Test", LenderAmount = 1200, LenderRate = 0.075m }
                            }).Returns(0.078m);


                yield return new TestCaseData(1200,
                        new List<LenderOffer>
                            {
                                    new LenderOffer { LenderName = "Test1", LenderAmount = 1000, LenderRate = 0.075m },
                                    new LenderOffer { LenderName = "Test2", LenderAmount = 500, LenderRate = 0.077m }
                            }).Returns(0.078m);

                yield return new TestCaseData(1200,
                       new List<LenderOffer>
                           {
                                    new LenderOffer { LenderName = "Test1", LenderAmount = 1000, LenderRate = 0.075m },
                                    new LenderOffer { LenderName = "Test2", LenderAmount = 200, LenderRate = 0.077m }
                           }).Returns(0.078m);

                yield return new TestCaseData(1500,
                        new List<LenderOffer>
                            {
                                    new LenderOffer { LenderName = "Test1", LenderAmount = 1000, LenderRate = 0.075m },
                                    new LenderOffer { LenderName = "Test2", LenderAmount = 200, LenderRate = 0.077m },
                                     new LenderOffer { LenderName = "Test3", LenderAmount = 300, LenderRate = 0.097m }
                            }).Returns(0.083m);

                yield return new TestCaseData(1800,
                       new List<LenderOffer>
                           {
                                    new LenderOffer { LenderName = "Test1", LenderAmount = 1000, LenderRate = 0.075m },
                                    new LenderOffer { LenderName = "Test2", LenderAmount = 200, LenderRate = 0.077m },
                                     new LenderOffer { LenderName = "Test3", LenderAmount = 300, LenderRate = 0.097m },
                                       new LenderOffer { LenderName = "Test4", LenderAmount = 500, LenderRate = 0.10m }
                           }).Returns(0.086m);

                yield return new TestCaseData(2800,
               new List<LenderOffer>
                   {
                                    new LenderOffer { LenderName = "Test1", LenderAmount = 1000, LenderRate = 0.075m },
                                    new LenderOffer { LenderName = "Test2", LenderAmount = 200, LenderRate = 0.077m },
                                     new LenderOffer { LenderName = "Test3", LenderAmount = 300, LenderRate = 0.097m },
                                       new LenderOffer { LenderName = "Test4", LenderAmount = 500, LenderRate = 0.10m },
                                       new LenderOffer { LenderName = "Test5", LenderAmount = 800, LenderRate = 0.11m }
                   }).Returns(0.096);

            }
        }

    }
}




