using System;
using System.Collections.Generic;
using System.IO;

namespace ExportXML
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectDirectory = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));            

            string reportXML = "ReportsXML/OpelProductionResults.xml";
            string resultXML = Path.Combine(projectDirectory, "Results/OpelCars.xml");
            string resultCSV = Path.Combine(projectDirectory, "Results/OpelCars.csv");

            Console.WriteLine("1. Deserializacja XML\n--------------------------------");
            ProductionReport productionReport = ProductionReport.DeserializeProductionReport(reportXML);
            List<Car> producedCars = productionReport.GetProducedCarsList();
            Console.WriteLine(productionReport);

            Console.WriteLine("\n2. Serializacja obiektow Car\n--------------------------------");
            string serializedCars = Car.Serialize(producedCars, resultXML, true);
            Console.WriteLine(serializedCars);

            Console.WriteLine("\n3. Konwersja wygenerowanego XML do CSV - wykorzystano XSLT\n--------------------------------");
            string convertedCSV = Converter.XMLConvertToCSV(resultXML, resultCSV, true);
            Console.WriteLine(convertedCSV);
            Console.Read();
        }
    }
}
