using System.Configuration;

namespace RateCalculator.Console
{
    public static class Common
    {
        public static int LoanMaximumValue { get; } = int.Parse(ConfigurationManager.AppSettings["Maximum"].ToString());

        public static int LoanMinimumValue { get; } = int.Parse(ConfigurationManager.AppSettings["Minimum"].ToString());

        public static int Years { get; } = int.Parse(ConfigurationManager.AppSettings["Years"].ToString());
        public static int LoanStep { get; } = int.Parse(ConfigurationManager.AppSettings["Step"].ToString());

        public static int CompoundedPerYear { get; } = 12;

    }
}
