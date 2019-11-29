using ProjectManagmentSystem;
using ProjectManagmentSystem.BLL;
using ProjectManagmentSystem.FACADE;
using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForFlightManagmentSystem
{
    public static class TestCenter
    {
        public static LoginToken<Administrator> AdminToken { get; set; }
        public static LoggedInAdministratorFacade AdminFacade { get; set; }
        public static LoginToken<AirlineCompany> AirlineToken { get; set; }
        public static LoggedInAirlineFacade AirlineFacade { get; set; }
        public static LoginToken<Customer> CustomerToken { get; set; }
        public static LoggedInCustomerFacade CustomerFacade { get; set; }
        public static AnonymousUserFacade AnonymousFacade { get; set; }
        public static FlyingCenterSystem F;


        static TestCenter()
        {
            CleanAllDataBase();
            F = FlyingCenterSystem.GetInstance();
            AdminToken = (LoginToken<Administrator>)F.Login(FlightCenterConfig.ADMIN_USER, FlightCenterConfig.ADMIN_PASSWORD);
            AdminFacade = (LoggedInAdministratorFacade)F.GetFacade(AdminToken);
            Customer customer = CreateCustomerForTest();
            CustomerToken = (LoginToken<Customer>)F.Login(customer.UserName, customer.Password);
            CustomerFacade = (LoggedInCustomerFacade)F.GetFacade(CustomerToken);
            AirlineCompany airlineCompany = CreateAirlineAndCountryForTest();
            AirlineToken = (LoginToken<AirlineCompany>)F.Login(airlineCompany.UserName, airlineCompany.Password);
            AirlineFacade = (LoggedInAirlineFacade)F.GetFacade(AirlineToken);
            AnonymousFacade = (AnonymousUserFacade)F.GetFacade(null);
        }



        public static Customer CreateCustomerForTest()
        {
            Customer c = new Customer
                ("Noam", "Mori", "noammori", "noam2510",
                "askenazi 23 yehud", "0542040469", "123456789");
            AdminFacade.CreateNewCustomer(AdminToken, c);
            return c;
        }

        public static AirlineCompany CreateAirlineAndCountryForTest()
        {
            Country country = new Country("Israel");
            country.Id = AdminFacade.CreateNewCountry(AdminToken, country);
            AirlineCompany a = new AirlineCompany("Delta", "Delta12", "123De", country.Id);
            AdminFacade.CreateNewAirLine(AdminToken, a);
            return a;
        }

        public static void CleanAllDataBase()
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CLEAN_ALL_DB", con))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }


    }
}
