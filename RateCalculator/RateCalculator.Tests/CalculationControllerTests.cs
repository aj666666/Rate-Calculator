
using Moq;
using NUnit.Framework;
using RateCalculator.Console;
using RateCalculator.Loans;
using System;
using System.Collections.Generic;

namespace RateCalculator.Tests
{
    [TestFixture]
    public class CalculationControllerTests
    {
        private Mock<IOffer> offers;
        private Mock<IOutputQuote> outputQuote;
        private Mock<IQuoteCalculation> calculateQuote;
       

        [SetUp]

        public void BeforeEachTest()
        {
            offers = new Mock<IOffer>();
            outputQuote = new Mock<IOutputQuote>();
            calculateQuote = new Mock<IQuoteCalculation>();
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWhenParametersAreNull()
        {
            var CalculationController = new CalculationController(offers.Object, outputQuote.Object, calculateQuote.Object);
            
            Assert.That(() => CalculationController.Start(null), Throws.TypeOf<ArgumentNullException>());
                       
        }

        [Test]
        public void ShouldThrowArgumentExceptionWhenParametersCountIsLessThanTwo()
        {
            var CalculationController = new CalculationController(offers.Object, outputQuote.Object, calculateQuote.Object);

            var parameters = new [] {"market.csv"};

            Assert.That(() => CalculationController.Start(parameters), Throws.TypeOf<ArgumentException>());

        }

        [Test]
        public void ShouldThrowArgumentExceptionWhenParametersCountIsMoreThanTwo()
        {
            var CalculationController = new CalculationController(offers.Object, outputQuote.Object, calculateQuote.Object);

            var parameters = new [] { "market.csv", "100", "100" };

            Assert.That(() => CalculationController.Start(parameters), Throws.TypeOf<ArgumentException>());

        }

        [Test]
        public void ShouldThrowArgumentExceptionWhenLoanAmountParameterIsNotValid()
        {
            var CalculationController = new CalculationController(offers.Object, outputQuote.Object, calculateQuote.Object);

            var parameters = new[] { "market.csv", "AS"};

            Assert.That(() => CalculationController.Start(parameters), Throws.TypeOf<ArgumentException>());

        }

        [Test]
        public void ShouldThrowArgumentOutOfRangeExceptionWhenLoanAmountIsLessThan1000()
        {
            var CalculationController = new CalculationController(offers.Object, outputQuote.Object, calculateQuote.Object);

            var parameters = new[] { "market.csv", 800.ToString() };

            Assert.That(() => CalculationController.Start(parameters), Throws.TypeOf<ArgumentOutOfRangeException>());

        }

        [Test]
        public void ShouldThrowArgumentOutOfRangeExceptionWhenLoanAmountIsGreaterThan15000()
        {
            var CalculationController = new CalculationController(offers.Object, outputQuote.Object, calculateQuote.Object);

            var parameters = new[] { "market.csv", 16000.ToString() };

            Assert.That(() => CalculationController.Start(parameters), Throws.TypeOf<ArgumentOutOfRangeException>());

        }

      

        [Test]
        [TestCase(1236), TestCase(11769), TestCase(1005), TestCase(14923)]
        public void ShouldThrowArgumentExceptionWhenLoanAmountIsNotDivisibleBy100(int loanAmount)
        {
            var CalculationController = new CalculationController(offers.Object, outputQuote.Object, calculateQuote.Object);

            var parameters = new[] { "market.csv", loanAmount.ToString() };

            Assert.That(() => CalculationController.Start(parameters), Throws.TypeOf<ArgumentException>());

        }

        [Test]
        public void ShouldLoadOffersFromMarketFile()
        {
           
            offers.Setup(x => x.Load(It.Is<string>(p => p == "market.csv")))
                .Returns(new List<LenderOffer> { new LenderOffer() })
                .Verifiable();

            
            var parameters = new[] { "market.csv", "1000" };

            var CalculationController = new CalculationController(offers.Object, outputQuote.Object, calculateQuote.Object);

            CalculationController.Start(parameters);

            offers.Verify();
        }

        [Test]
        public void ShouldDisplayResult()
        {

            var quoteCalculationResult = new ComputedQuote();

           
            outputQuote.Setup(x => x.QuoteResult(It.Is<ComputedQuote>(p => quoteCalculationResult.Equals(p))))
                .Verifiable();

                       
            calculateQuote.Setup(x => x.GetQuote(It.IsAny<int>(), It.IsAny<IList<LenderOffer>>()))
                .Returns(quoteCalculationResult);

            var parameters = new[] { "market.csv", 1000.ToString() };

            var CalculationController = new CalculationController(offers.Object, outputQuote.Object, calculateQuote.Object);
            CalculationController.Start(parameters);

            outputQuote.Verify();


        }

    }
}
