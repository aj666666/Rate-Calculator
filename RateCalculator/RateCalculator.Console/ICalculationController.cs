using System.Collections.Generic;

namespace RateCalculator.Console
{
    public interface ICalculationController
    {
        void Start(IList<string> parameters);
    }
}
