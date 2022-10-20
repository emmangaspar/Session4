using ServiceReference1;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace Session4.Tests
{
    [TestClass]
    public class SOAPTest
    {

        //Global Variable
        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient soapTest =
             new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void ListOfCountryNamesByCode()
        {
            // return ListOfCountryNamesByCode()
            var result = soapTest.ListOfCountryNamesByCode();

            // Sorting list order by sISOCode
            var sorted = result.OrderBy(a => a.sISOCode);

            // return country code in ascending order
            Assert.IsTrue(result.SequenceEqual(sorted), "Country is in Descending Order");
        }
        [TestMethod]
        public void ValidateInvalidCountryCode()
        {
            // Invalid Country Code
            var invalidCountryCode = "XX";

            // Invalid Country Code to Country Name
            var result = soapTest.CountryName(invalidCountryCode);

            // Asserting that both entries are not equal
            Assert.AreNotEqual(result, invalidCountryCode, "Country not found in the database");

        }
        [TestMethod]
        public void GetLastEntry()
        {
            // Getting the last entry from ListOfCountryNamesByCode()
            var result = soapTest.ListOfCountryNamesByCode().ToList();
            var lastEntry = result.Last();

            // Adding the last entry to Country Name()
            var countryName = soapTest.CountryName(lastEntry.sISOCode);

            // Asserting that both country names are equal
            Assert.AreEqual(lastEntry.sName, countryName, "Country Name is not the same");

        }

    }
}