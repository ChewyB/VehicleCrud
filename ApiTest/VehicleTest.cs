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
            Vehicle prius = new Vehicle
            {
                Id = 1,
                Make = "Toyota",
                Model = "Prius",
                Year = 1900
            };

            var vp = new VehiclePersistance();
            vp.SaveVehicle(prius);
        }

    }
}
