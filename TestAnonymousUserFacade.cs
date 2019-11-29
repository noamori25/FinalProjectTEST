using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForFlightManagmentSystem
{
    /// <summary>
    /// Summary description For TestForCountry
    /// </summary>
    [TestClass]
    public class TestAnonymousUserFacade
    {

        [TestMethod]
        public void GetAllAirlineCompaniesTest()
        {
            Assert.AreEqual(TestCenter.AnonymousFacade.GetAllAirlineCompanies().Count, 1);
            Country country = new Country("Japan");
            TestCenter.AdminFacade.CreateNewCountry(TestCenter.AdminToken, country);
            AirlineCompany airline = new AirlineCompany("Easy-Jet", "easy12", "easy123", country.Id);
            TestCenter.AdminFacade.CreateNewAirLine(TestCenter.AdminToken, airline);
            Assert.AreEqual(TestCenter.AnonymousFacade.GetAllAirlineCompanies().Count, 2);
        }

        [TestMethod]
        public void GetAllFlightsTest()
        {
            Assert.AreEqual(TestCenter.AnonymousFacade.GetAllFlights().Count, 0);
            Flight F = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode,
                TestCenter.AirlineToken.User.CountryCode, new DateTime(2019, 10, 10, 10, 00, 00), new DateTime(2019, 10, 10, 10, 00, 00), 100);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, F);
            Assert.AreEqual(TestCenter.AdminFacade.GetAllFlights().Count, 1);
        }

        [TestMethod]
        public void GetAllVacanyFlightsTest()
        {
            Flight F = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode,
               TestCenter.AirlineToken.User.CountryCode, new DateTime(2019, 10, 10, 10, 00, 00), new DateTime(2019, 10, 10, 10, 00, 00), 100);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, F);
            Assert.AreEqual(TestCenter.AnonymousFacade.GetAllVacancyFlights().Count, 1);
            Flight Flight = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode, new DateTime(2019, 10, 10, 10, 30, 00), new DateTime(2019, 10, 10, 10, 30, 00), 0);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, Flight);
            Assert.AreEqual(TestCenter.AnonymousFacade.GetAllVacancyFlights().Count, 1);
        }

        [TestMethod]
        public void GetFlightByIdTest()
        {
            Assert.IsNull(TestCenter.AdminFacade.GetFlightById(2));
            Flight F = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode,
               TestCenter.AirlineToken.User.CountryCode, new DateTime(2019, 10, 10, 10, 00, 00), new DateTime(2019, 10, 10, 10, 00, 00), 100);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, F);
            Assert.IsNotNull(TestCenter.AnonymousFacade.GetFlightById(F.Id));
        }

        [TestMethod]
        public void GetFlightsByDepartureDateTest()
        {
            Flight F = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode,
            TestCenter.AirlineToken.User.CountryCode, new DateTime(2019, 10, 10, 10, 00, 00), new DateTime(2019, 10, 10, 10, 00, 00), 100);
            Assert.AreEqual(TestCenter.AnonymousFacade.GetFlightsByDepartureDate(F.DepartureTime).Count, 0);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, F);
            Assert.AreEqual(TestCenter.AnonymousFacade.GetFlightsByDepartureDate(F.DepartureTime).Count, 1);
        }

        [TestMethod]
        public void GetFlightsByLandingDateTest()
        {
            Flight F = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode,
            TestCenter.AirlineToken.User.CountryCode, new DateTime(2019, 10, 10, 10, 00, 00), new DateTime(2019, 10, 10, 10, 00, 00), 100);
            Assert.AreEqual(TestCenter.AnonymousFacade.GetFlightsByLandingDate(F.LandingTime).Count, 0);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, F);
            Assert.AreEqual(TestCenter.AnonymousFacade.GetFlightsByLandingDate(F.LandingTime).Count, 1);
        }

        [TestMethod]
        public void GetFlightsByDestinationCountryTest()
        {
            Flight F = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode,
            TestCenter.AirlineToken.User.CountryCode, new DateTime(2019, 10, 10, 10, 00, 00), new DateTime(2019, 10, 10, 10, 00, 00), 100);
            Assert.AreEqual(TestCenter.AnonymousFacade.GetFlightsByDestinationCountry(F.DestinationCountryCode).Count, 0);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, F);
            Assert.AreEqual(TestCenter.AnonymousFacade.GetFlightsByDestinationCountry(F.DestinationCountryCode).Count, 1);
        }

        [TestMethod]
        public void GetFlightsByOriginCountryTest()
        {
            Flight F = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode,
            TestCenter.AirlineToken.User.CountryCode, new DateTime(2019, 10, 10, 10, 00, 00), new DateTime(2019, 10, 10, 10, 00, 00), 100);
            Assert.AreEqual(TestCenter.AnonymousFacade.GetFlightsByOriginCountry(F.OriginCountryCode).Count, 0);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, F);
            Assert.AreEqual(TestCenter.AnonymousFacade.GetFlightsByOriginCountry(F.OriginCountryCode).Count, 1);
        }
    }
}
