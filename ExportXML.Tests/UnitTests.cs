using System;
using System.Collections.Generic;
using Xunit;
using System.IO;

namespace ExportXML.Tests
{
    public class UnitTests
    {
        private ProductionReport CreateSampleProductionReport()
        {
            List<Feature> firstCarFeatures = new List<Feature>{
                new Feature("SZW", "Sportowe zawieszenie"),
                new Feature("SZD", "Szyberdach")};

            List<Feature> secondCarFeatures = new List<Feature>{
                new Feature("ASB", "Automatyczna skrzynia biegow")};

            List<Car> cars = new List<Car>{
                new Car("321ZAYC024", "2017", "TT", firstCarFeatures),
                new Car("YC024321ZA", "2018", "A4", secondCarFeatures)};

            List<Factory> factories = new List<Factory>{
                new Factory("Neckersulm Fab", cars)};

            ProductionReport productionReport = new ProductionReport("Audi", "2017-14-13T12:32:02.8036669+01:00", factories);
            return productionReport;
        }

        [Fact]
        public void DeserializeProductionReport_EqualObjects_ReturnTrue()
        {
            string reportXML = "TestFiles/SampleProductionResults.xml";
            ProductionReport productionReport = CreateSampleProductionReport();

            ProductionReport deserializedProductionReport = ProductionReport.DeserializeProductionReport(reportXML);

            Assert.True(productionReport.Equals(deserializedProductionReport));
        }

        [Fact]
        public void DeserializeProductionReport_NotEqualObjects_ReturnFalse()
        {
            string reportXML = "TestFiles/SampleProductionResults.xml";
            ProductionReport productionReport = CreateSampleProductionReport();
            productionReport.Manufacturer = "Auudi";
            ProductionReport deserializedProductionReport = ProductionReport.DeserializeProductionReport(reportXML);

            Assert.False(productionReport.Equals(deserializedProductionReport));
        }

        [Fact]
        public void CarSerialize_EqualOutput_ReturnTrue()
        {
            string fileName = Path.Combine(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin")),"TestFiles/SampleCars.xml");
            string expectedCars = File.ReadAllText(fileName);

            ProductionReport productionReport = CreateSampleProductionReport();
            List<Car> cars = productionReport.GetProducedCarsList();
            string serializedCars = Car.Serialize(cars, " ", false);

            Assert.Equal(serializedCars,expectedCars);
        }

        [Fact]
        public void CarSerialize_NotEqualOutput_ReturnFalse()
        {
            string fileName = Path.Combine(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin")), "TestFiles/SampleCars.xml");
            string expectedCars = File.ReadAllText(fileName);

            ProductionReport productionReport = CreateSampleProductionReport();
            List<Car> cars = productionReport.GetProducedCarsList();
            cars.RemoveAt(0);
            string serializedCars = Car.Serialize(cars, " ", false);

            Assert.NotEqual(serializedCars, expectedCars);
        }

        
        [Theory]
        [CsvData("TestFiles/ExpectedCars.csv")]
        public void XMLConvertToCSV_EqualOutput_ReturnTrue(string vin, string productionYear, string model, int idx)
        {
            string resultXML = "TestFiles/SampleCars.xml";
            string expectedCSV = Converter.XMLConvertToCSV(resultXML,"", false);
            string[] cars = expectedCSV.Split('\n');
            string[] currentCar = cars[idx].Split(',');

            Assert.True(currentCar[0] == vin);
            Assert.True(currentCar[1] == productionYear);
            Assert.True(currentCar[2] == model);
        }



    }
}
