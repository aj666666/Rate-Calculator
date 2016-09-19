using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RateCalculator.Loans
{
    public class LoadOffersCSV : IOffer
    {
        public IList<LenderOffer> Load(string fileName)
        {
            using (var csv = new CsvReader(File.OpenText(fileName)))
            {
                csv.Configuration.RegisterClassMap<CustomClassMap>();

                return csv.GetRecords<LenderOffer>().ToList();
            }
        }
    }

    public sealed class CustomClassMap : CsvClassMap<LenderOffer>
    {
        public CustomClassMap()
        {
            Map(m => m.LenderRate).Index(1);
            Map(m => m.LenderAmount).Index(2);

        }
    }
}
