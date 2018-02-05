using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vehicleRESTapi.Models;

namespace vehicleRESTapi
{
    public class VehiclePersistance
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public VehiclePersistance()
        {
            string myConnectionString = "server=127.0.0.1;uid=root;pwd=515764;database=mitchell_vehicles";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
        }

        public long saveVehicle(Vehicle p)
        {
            string sqlString = "INSERT INTO vehicles (year, make, model) VALUES (" + p.Year + ",'" + p.Make + "','" + p.Model + "')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            long id = cmd.LastInsertedId;
            return id;
        }

        public ArrayList getAllVehicles()
        {
            ArrayList personArray = new ArrayList();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM vehicles";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

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

        public Vehicle getVehicle(long ID)
        {
            Vehicle p = new Vehicle();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM vehicles WHERE ID = " + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

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

        public bool deleteVehicle(long ID)
        {
            Vehicle p = new Vehicle();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM vehicles WHERE ID = " + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read()) //If the record exists, continue to delete it
            {
                mySQLReader.Close();
                sqlString = "DELETE FROM vehicles WHERE ID = " + ID.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updateVehicle(long ID, Vehicle p)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM vehicles WHERE ID = " + ID.ToString();

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read()) //If the record exists, continue for an update
            {
                mySQLReader.Close();
                sqlString = "UPDATE vehicles SET year=" + p.Year + ", make='" + p.Make + "', model='" + p.Model +"' WHERE id = " + ID.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

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