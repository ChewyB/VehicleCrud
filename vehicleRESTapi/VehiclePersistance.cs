using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using vehicleRESTapi.Models;

namespace vehicleRESTapi
{
    public class VehicleActions
    {
        private SqlConnection sql_conn;

        /// <summary>
        /// Creates a connection to the database every time we create/call a new vehicle action
        /// </summary>
        public VehicleActions()
        {
            string sqlConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\mitchell_vehicles.mdf;Integrated Security=True"; //C:\Users\kbq19\Documents\VehicleCrud\vehicleRESTapi\App_Data

            try
            {
                sql_conn = new SqlConnection(sqlConnectionString);
                sql_conn.Open();
            }
            catch (SqlException ex)
            {
                new SystemException(ex.Message);
            }
        }

        /// <summary>
        /// Saves the vehicle to the databse using POST action
        /// </summary>
        /// <param name="vehicleToSave"></param>
        /// <returns></returns>
        public int SaveVehicle(Vehicle vehicleToSave)
        {
            //Check that the values from the request are acceptable
            if(vehicleToSave.Year > 2050 || vehicleToSave.Year < 1950
               || vehicleToSave.Model=="" || vehicleToSave.Make=="")
            {
                return -1;
            }

            string returnID = "SELECT id as ID_UNIQUE FROM vehicles WHERE id = @@Identity;";
            string sqlString = "INSERT INTO vehicles (year, make, model) VALUES (" + vehicleToSave.Year + ",'" + vehicleToSave.Make + "','" + vehicleToSave.Model + "');" + returnID;
            SqlCommand cmd = new SqlCommand(sqlString, sql_conn);

            int id = Convert.ToInt32(cmd.ExecuteScalar());

            return id;
        }

        /// <summary>
        /// Get all vehicles inserted into the table by ready all rows from the database
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllVehicles()
        {
            ArrayList personArray = new ArrayList();
            SqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM vehicles";
            SqlCommand cmd = new SqlCommand(sqlString, sql_conn);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                Vehicle p = new Vehicle();
                p.Id = mySQLReader.GetInt32(0);
                p.Year = mySQLReader.GetInt32(1);
                p.Make = mySQLReader.GetString(2);
                p.Model = mySQLReader.GetString(3);
                personArray.Add(p);
            }
            return personArray;
        }

        /// <summary>
        /// Get the vehicle by ID by checking the records exists and then extract all the information from the database (id, year, make, model)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Vehicle GetVehicle(int ID)
        {
            Vehicle p = new Vehicle();
            SqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM vehicles WHERE id = " + ID.ToString();
            SqlCommand cmd = new SqlCommand(sqlString, sql_conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read()) //If we got back the data then get the first column that came back
            {

                p.Id = mySQLReader.GetInt32(0);
                p.Year = mySQLReader.GetInt32(1);
                p.Make = mySQLReader.GetString(2);
                p.Model = mySQLReader.GetString(3);


                return p;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Check if the records exists before executing the delete query
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteVehicle(int ID)
        {
            Vehicle p = new Vehicle();
            SqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM vehicles WHERE ID = " + ID.ToString();
            SqlCommand cmd = new SqlCommand(sqlString, sql_conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read()) //If the record exists, continue to delete it
            {
                mySQLReader.Close();
                sqlString = "DELETE FROM vehicles WHERE ID = " + ID.ToString();
                cmd = new SqlCommand(sqlString, sql_conn);

                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check that the record actually exists before executing the update query
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool UpdateVehicle(int ID, Vehicle p)
        {
            SqlDataReader mySQLReader = null;

            string sqlString = "SELECT * FROM vehicles WHERE ID = " + ID.ToString();

            SqlCommand cmd = new SqlCommand(sqlString, sql_conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read()) //If the record exists, continue for an update
            {
                mySQLReader.Close();
                sqlString = "UPDATE vehicles SET year=" + p.Year + ", make='" + p.Make + "', model='" + p.Model +"' WHERE id = " + ID.ToString();
                cmd = new SqlCommand(sqlString, sql_conn);

                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}