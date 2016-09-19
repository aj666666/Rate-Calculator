using Microsoft.Practices.Unity;
using RateCalculator.Loans;
using System;
using System.Globalization;
using static System.Globalization.CultureInfo;
using static System.Console;

namespace RateCalculator.Console
{
    class Program
    {
        static IUnityContainer container;

        static void Main(string[] args)
        {
            try
            {
                DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
                DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentCulture;

                if (args.Length == 0) { WriteLine($"Inputs should be in the form of [market_file]<string>] [loan-amount]<int>"); }


                else
                {

                    LoadContainer();
                    var worker = container.Resolve<ICalculationController>();
                    worker.Start(args);
                }
            }

            catch (Exception ex)
            {


                WriteLine($"An error has occured {ex.Message} ");

            }
        }


        private static void LoadContainer()
        {
            container = new UnityContainer();

            container.RegisterType<ICalculationController, CalculationController>();
            container.RegisterType<IOffer, LoadOffersCSV>();
            container.RegisterType<IOutputQuote, OutputQuote>();
            container.RegisterType<IQuoteCalculation, QuoteCalculation>();

        }
    }
}
