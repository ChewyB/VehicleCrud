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
    public class VehiclePersistance
    {
        private SqlConnection sql_conn;

        public VehiclePersistance()
        {
            //string myConnectionString = "server=127.0.0.1;uid=root;pwd=515764;database=mitchell_vehicles";
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

        public int saveVehicle(Vehicle p)
        {
            string returnID = "SELECT id as ID_UNIQUE FROM vehicles WHERE id = @@Identity;";
            string sqlString = "INSERT INTO vehicles (year, make, model) VALUES (" + p.Year + ",'" + p.Make + "','" + p.Model + "');" + returnID;
            SqlCommand cmd = new SqlCommand(sqlString, sql_conn);

            int id = Convert.ToInt32(cmd.ExecuteScalar());

            return id;
        }

        public ArrayList getAllVehicles()
        {
            ArrayList personArray = new ArrayList();
            SqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM vehicles";
            SqlCommand cmd = new SqlCommand(sqlString, sql_conn);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read()) //If we got back the data then get the first column that came back
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

        public Vehicle getVehicle(int ID)
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

        public bool deleteVehicle(int ID)
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

        public bool updateVehicle(int ID, Vehicle p)
        {
            SqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM vehicles WHERE ID = " + ID.ToString();

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