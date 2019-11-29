using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagmentSystem.DAO;
using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForFlightManagmentSystem
{
    [TestClass]
    public class TestForAirlineFacade
    {

        [TestMethod]
        public void CancelFlightTest()
        {
            Assert.AreEqual(TestCenter.AirlineFacade.GetAllFlights().Count, 0);
            Flight F = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode
                , new DateTime(2019, 10, 10, 10, 00, 00), new DateTime(2019, 10, 10, 10, 00, 00), 100);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, F);
            Assert.AreEqual(TestCenter.AirlineFacade.GetAllFlights().Count, 1);
            TestCenter.AirlineFacade.CancelFlight(TestCenter.AirlineToken, F);
            Assert.AreEqual(TestCenter.AirlineFacade.GetAllFlights().Count, 0);
        }

        [TestMethod]
        public void ChangeMyPasswordTest()
        {

            Assert.AreEqual(TestCenter.AdminFacade.GetAirlineByUserName(TestCenter.AdminToken, TestCenter.AirlineToken.User.UserName).Password, TestCenter.AirlineToken.User.Password);
            TestCenter.AirlineToken.User.Password = "Change";
            TestCenter.AirlineFacade.ChangeMyPassword(TestCenter.AirlineToken, TestCenter.AirlineToken.User.Password, "change");
            Assert.AreEqual(TestCenter.AdminFacade.GetAirlineByUserName(TestCenter.AdminToken, TestCenter.AirlineToken.User.UserName).Password, TestCenter.AirlineToken.User.Password);
        }

        [TestMethod]
        public void CreateFlightTest()
        {
            Flight Flight = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode, new DateTime(2020, 10, 10, 10, 00, 00), new DateTime(2020, 10, 11, 10, 00, 00), 100);
            Assert.AreEqual(TestCenter.AdminFacade.GetAllFlights().Count, 0);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, Flight);
            Assert.AreEqual(TestCenter.AdminFacade.GetAllFlights().Count, 1);
        }
        [TestMethod]
        public void GetAllFlightsTest()
        {
            Flight Flight = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode, new DateTime(2020, 10, 10, 10, 00, 00), new DateTime(2020, 10, 11, 10, 00, 00), 100);
            Assert.AreEqual(TestCenter.AdminFacade.GetAllFlights().Count, 0);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, Flight);
            Assert.AreEqual(TestCenter.AdminFacade.GetAllFlights().Count, 1);

        }

        [TestMethod]
        public void GetAllMyTicketsTest()
        {
            Flight Flight = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode, new DateTime(2020, 10, 10, 10, 00, 00), new DateTime(2020, 10, 11, 10, 00, 00), 100);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, Flight);
            Assert.AreEqual(TestCenter.AirlineFacade.GetAllMyTickets(TestCenter.AirlineToken).Count, 0);
            Ticket ticket = new Ticket(Flight.Id, TestCenter.CustomerToken.User.Id);
            TestCenter.CustomerFacade.PurchaseTicket(TestCenter.CustomerToken, Flight);
            Assert.AreEqual(TestCenter.AirlineFacade.GetAllMyTickets(TestCenter.AirlineToken).Count, 1);
        }

        [TestMethod]
        public void ModiFyAirlineTest()
        {
            TestCenter.AirlineToken.User.AirlineName = "Change";
            Assert.AreNotEqual(TestCenter.AdminFacade.GetAirlineByUserName(TestCenter.AdminToken, TestCenter.AirlineToken.User.UserName).AirlineName, TestCenter.AirlineToken.User.AirlineName);
            TestCenter.AirlineFacade.ModifyAirlineDetails(TestCenter.AirlineToken, TestCenter.AirlineToken.User);
            Assert.AreEqual(TestCenter.AdminFacade.GetAirlineByUserName(TestCenter.AdminToken, TestCenter.AirlineToken.User.UserName).AirlineName, TestCenter.AirlineToken.User.AirlineName);
        }

        [TestMethod]
        public void UpdateFlightTest()
        {
            Flight Flight = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode, new DateTime(2020, 10, 10, 10, 00, 00), new DateTime(2020, 10, 11, 10, 00, 00), 100);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, Flight);
            FlightDAOMSSQL F = new FlightDAOMSSQL();
            Flight.RemainingTickets = 200;
            Assert.AreNotEqual(F.Get(Flight.Id).RemainingTickets, Flight.RemainingTickets);
            TestCenter.AirlineFacade.UpdateFlight(TestCenter.AirlineToken, Flight);
            Assert.AreEqual(F.Get(Flight.Id).RemainingTickets, Flight.RemainingTickets);
        }

        [TestMethod]
        public void GetAllTicketsByFlight()
        {
            Flight Flight = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode, new DateTime(2020, 10, 10, 10, 00, 00), new DateTime(2020, 10, 11, 10, 00, 00), 100);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, Flight);
            Assert.AreEqual(TestCenter.AirlineFacade.GetAllTicketsByFlight(TestCenter.AirlineToken, Flight.Id).Count, 0);
            TestCenter.CustomerFacade.PurchaseTicket(TestCenter.CustomerToken, Flight);
            Assert.AreEqual(TestCenter.AirlineFacade.GetAllTicketsByFlight(TestCenter.AirlineToken, Flight.Id).Count, 1);
            Flight Flight1 = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode, new DateTime(2020, 10, 10, 12, 00, 00), new DateTime(2020, 10, 11, 11, 00, 00), 300);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, Flight1);
            Assert.AreEqual(TestCenter.AirlineFacade.GetAllTicketsByFlight(TestCenter.AirlineToken, Flight.Id).Count, 1);

        }

    }
}
