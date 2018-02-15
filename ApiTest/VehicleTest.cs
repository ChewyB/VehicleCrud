using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vehicleRESTapi.Models;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Collections.Generic;
using vehicleRESTapi;
using System.Data.SqlClient;

namespace ApiTest
{
    [TestClass]
    public class VehicleTest
    {

        public VehicleTest()
        {
            string sqlConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\mitchell_vehicles.mdf;Integrated Security=True"; //C:\Users\kbq19\Documents\VehicleCrud\vehicleRESTapi\App_Data

            try
            {
                var sql_conn = new SqlConnection(sqlConnectionString);
                sql_conn.Open();
            }
            catch (SqlException ex)
            {
                new SystemException(ex.Message);
            }
        }

        [TestMethod]
        public void Test_IncorrectDelete()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.UploadValues("http://localhost:44391/api/vehicles/58", "DELETE", new NameValueCollection());
                }
                catch (WebException e)
                {
                    Assert.IsTrue(true);
                }
            }
        }


    }
}
