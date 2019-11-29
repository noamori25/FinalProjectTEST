using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagmentSystem.BLL;
using ProjectManagmentSystem.Exceptions;
using ProjectManagmentSystem.FACADE;
using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForFlightManagmentSystem
{
    [TestClass]
    public class TestFotLoginService
    {
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]

        public void TestForWrongPasswordExceptionCustomer()
        {
            Customer customer = new Customer("Noam", "Mori", "noammori", "noam25", "yehud", "05465", "12345");
            FlyingCenterSystem FlyingCenter = FlyingCenterSystem.GetInstance();
            LoginToken<Customer> CustomerToken = (LoginToken<Customer>)FlyingCenter.Login(customer.UserName, customer.Password);
            LoggedInCustomerFacade CustomerFacade = (LoggedInCustomerFacade)FlyingCenter.GetFacade(CustomerToken);

        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void TestForUserNotFountExceptionAirline()
        {
            FlyingCenterSystem F = FlyingCenterSystem.GetInstance();
            F.Login("1", "1");
        }

        [TestMethod]
        public void TestForLoginCustomerAirline()
        {
            FlyingCenterSystem F = FlyingCenterSystem.GetInstance();
            LoginToken<AirlineCompany> loginAirline = (LoginToken<AirlineCompany>)F.Login(TestCenter.AirlineToken.User.UserName, TestCenter.AirlineToken.User.Password);
            Assert.IsNotNull(loginAirline);
            Assert.IsNotNull(F.GetFacade(loginAirline));
        }

    }
}
