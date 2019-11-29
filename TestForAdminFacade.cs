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
    public class TestForAdminFacade
    {
     [TestMethod]
    public void CreateNewAirlineTest()
    {
        Assert.AreEqual(TestCenter.AirlineFacade.GetAllAirlineCompanies().Count, 1);
        AirlineCompany airline = new AirlineCompany("EasyJet", "easy12", "123easy", TestCenter.AirlineToken.User.CountryCode);
        TestCenter.AdminFacade.CreateNewAirLine(TestCenter.AdminToken, airline);
        Assert.AreEqual(TestCenter.AdminFacade.GetAllAirlineCompanies().Count, 2);
    }

    [TestMethod]
    public void CreateNewCustomerTest()
    {
        CustomerDAOMSSQL c = new CustomerDAOMSSQL();
        Assert.AreEqual(c.GetAll().Count, 1);
        Customer customer = new Customer("Israel", "Mori", "Israel12", "Israel123", "Yehud", "0545292907", "123456787");
        TestCenter.AdminFacade.CreateNewCustomer(TestCenter.AdminToken, customer);
        Assert.AreEqual(c.GetAll().Count, 2);
    }

    [TestMethod]
    public void RemoveAirlineTest()
    {
        Assert.IsNotNull(TestCenter.AdminFacade.GetAirlineByUserName(TestCenter.AdminToken, TestCenter.AirlineToken.User.UserName));
        TestCenter.AdminFacade.RemoveAirline(TestCenter.AdminToken, TestCenter.AirlineToken.User);
        Assert.IsNull(TestCenter.AdminFacade.GetAirlineByUserName(TestCenter.AdminToken, TestCenter.AirlineToken.User.UserName));
    }

    [TestMethod]
    public void RemoveCustomerTest()
    {
        CustomerDAOMSSQL c = new CustomerDAOMSSQL();
        Assert.IsNotNull(c.Get(TestCenter.CustomerToken.User.Id));
        TestCenter.AdminFacade.RemoveCustomer(TestCenter.AdminToken, TestCenter.CustomerToken.User);
        Assert.IsNull(c.Get(TestCenter.CustomerToken.User.Id));
    }

    [TestMethod]
    public void UpdateAirlineTest()
    {
        // test = new TestCenter();
        AirlineDAOMSSQL ad = new AirlineDAOMSSQL();
        Assert.AreEqual(TestCenter.AirlineToken.User, ad.GetAirlineByUserName(TestCenter.AirlineToken.User.UserName));
        TestCenter.AirlineToken.User.UserName = "Change";
        Assert.AreNotEqual(TestCenter.AirlineToken.User, ad.GetAirlineByUserName(TestCenter.AirlineToken.User.UserName));
        TestCenter.AdminFacade.UpdateAirlineDetails(TestCenter.AdminToken, TestCenter.AirlineToken.User);
        Assert.AreEqual(TestCenter.AirlineToken.User, ad.GetAirlineByUserName(TestCenter.AirlineToken.User.UserName));
    }

    [TestMethod]
    public void UpdateCustomerTest()
    {
        CustomerDAOMSSQL c = new CustomerDAOMSSQL();
        Assert.AreEqual(TestCenter.CustomerToken.User, c.GetCustomerByUserName(TestCenter.CustomerToken.User.UserName));
        TestCenter.CustomerToken.User.UserName = "Change";
        Assert.AreNotEqual(TestCenter.CustomerToken.User, c.GetCustomerByUserName(TestCenter.CustomerToken.User.UserName));
        TestCenter.AdminFacade.UpdateCustomerDetails(TestCenter.AdminToken, TestCenter.CustomerToken.User);
        Assert.AreEqual(TestCenter.CustomerToken.User, c.GetCustomerByUserName(TestCenter.CustomerToken.User.UserName));
    }
    [TestMethod]
    public void GetCountryByNameTest()
    {
        CountryDAOMSSQL c = new CountryDAOMSSQL();
        Country country = c.Get(TestCenter.AirlineToken.User.CountryCode);
        Assert.AreEqual(country, TestCenter.AdminFacade.GetCountryByName(TestCenter.AdminToken, country.CountryName));
    }

    [TestMethod]
    public void CreateNewCountryTest()
    {
        CountryDAOMSSQL c = new CountryDAOMSSQL();
        Country country = new Country("Las-Vegas");
        Assert.AreEqual(c.GetAll().Count, 1);
        TestCenter.AdminFacade.CreateNewCountry(TestCenter.AdminToken, country);
        Assert.AreEqual(c.GetAll().Count, 2);
    }

    [TestMethod]
    public void GetAirlineByUserNameTest()
    {
        Assert.AreEqual(TestCenter.AirlineToken.User, TestCenter.AdminFacade.GetAirlineByUserName(TestCenter.AdminToken, TestCenter.AirlineToken.User.UserName));
    }

    [TestMethod]
    public void DeleteCountryTest()
    {
        Country country = new Country("Las-Vegas");
        CountryDAOMSSQL c = new CountryDAOMSSQL();
        Assert.AreEqual(c.GetAll().Count, 1);
        TestCenter.AdminFacade.CreateNewCountry(TestCenter.AdminToken, country);
        Assert.AreEqual(c.GetAll().Count, 2);
        TestCenter.AdminFacade.DeleteCountry(TestCenter.AdminToken, country);
        Assert.AreEqual(c.GetAll().Count, 1);

    }

    [TestMethod]
    [ExpectedException(typeof(CountryInUseException))]
    public void DeleteCountryExceptionTest()
    {
        CountryDAOMSSQL c = new CountryDAOMSSQL();
        Country country = c.Get(TestCenter.AirlineToken.User.CountryCode);
        TestCenter.AdminFacade.DeleteCountry(TestCenter.AdminToken, country);

    }


}
}
