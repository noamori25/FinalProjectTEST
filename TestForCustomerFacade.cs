using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagmentSystem.DAO;
using ProjectManagmentSystem.Exceptions;
using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForFlightManagmentSystem
{
    [TestClass]
    public class TestForCustomerFacade
    {


        [TestMethod]
        public void CancleTicketTest()
        {
            Flight Flight = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode, new DateTime(2020, 10, 10, 10, 00, 00), new DateTime(2020, 10, 11, 10, 00, 00), 100);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, Flight);
            Ticket ticket = new Ticket(Flight.Id, TestCenter.CustomerToken.User.Id);
            ticket.Id = TestCenter.CustomerFacade.PurchaseTicket(TestCenter.CustomerToken, Flight).Id;
            TicketDAOMSSQL t = new TicketDAOMSSQL();
            Assert.AreEqual(t.GetAll().Count, 1);
            Assert.AreEqual(Flight.RemainingTickets - 1, TestCenter.CustomerFacade.GetFlightById(Flight.Id).RemainingTickets);
            TestCenter.CustomerFacade.CancelTicket(TestCenter.CustomerToken, ticket);
            Assert.AreEqual(TestCenter.CustomerFacade.GetAllMyFlights(TestCenter.CustomerToken).Count, 0);
            Assert.AreEqual(Flight.RemainingTickets, TestCenter.CustomerFacade.GetFlightById(Flight.Id).RemainingTickets);
        }

        [TestMethod]
        public void GetAllMyFlightTest()
        {
            Flight Flight = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode, new DateTime(2020, 10, 10, 10, 00, 00), new DateTime(2020, 10, 11, 10, 00, 00), 100);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, Flight);
            Assert.AreEqual(TestCenter.CustomerFacade.GetAllMyFlights(TestCenter.CustomerToken).Count, 0);
            TestCenter.CustomerFacade.PurchaseTicket(TestCenter.CustomerToken, Flight);
            Assert.AreEqual(TestCenter.CustomerFacade.GetAllMyFlights(TestCenter.CustomerToken).Count, 1);
        }

        [TestMethod]
        public void PurchaseTicketTest()
        {
            TicketDAOMSSQL t = new TicketDAOMSSQL();
            Assert.AreEqual(t.GetAll().Count, 0);
            Flight Flight = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode, new DateTime(2020, 10, 10, 10, 00, 00), new DateTime(2020, 10, 11, 10, 00, 00), 100);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, Flight);
            Assert.AreEqual(TestCenter.AdminFacade.GetFlightById(Flight.Id).RemainingTickets, Flight.RemainingTickets);
            TestCenter.CustomerFacade.PurchaseTicket(TestCenter.CustomerToken, Flight);
            Assert.AreEqual(t.GetAll().Count, 1);
            Assert.AreEqual(TestCenter.AdminFacade.GetFlightById(Flight.Id).RemainingTickets, Flight.RemainingTickets - 1);
        }

        [TestMethod]
        [ExpectedException(typeof(TicketsSoldOutException))]
        public void PurchaseTicketException()
        {
            TicketDAOMSSQL t = new TicketDAOMSSQL();
            Flight Flight = new Flight(TestCenter.AirlineToken.User.Id, TestCenter.AirlineToken.User.CountryCode, TestCenter.AirlineToken.User.CountryCode, new DateTime(2020, 10, 10, 10, 00, 00), new DateTime(2020, 10, 11, 10, 00, 00), 0);
            TestCenter.AirlineFacade.CreateFlight(TestCenter.AirlineToken, Flight);
            TestCenter.CustomerFacade.PurchaseTicket(TestCenter.CustomerToken, Flight);

        }


        [TestMethod]
        public void DeleteMyAccountTest()
        {
            CustomerDAOMSSQL c = new CustomerDAOMSSQL();
            Assert.IsNotNull(c.Get(TestCenter.CustomerToken.User.Id));
            TestCenter.CustomerFacade.DeleteMyAccount(TestCenter.CustomerToken, TestCenter.CustomerToken.User);
            Assert.IsNull(c.Get(TestCenter.CustomerToken.User.Id));
        }

    }
}
