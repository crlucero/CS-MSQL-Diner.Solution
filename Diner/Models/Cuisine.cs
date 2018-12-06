using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Diner;

namespace Diner.Models
{
    public class Cuisine
    { 
        private int _id;

        private string _name;

        public Cuisine(string name, int id = 0)
        {
            _id = id;
            _name = name;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

         public List<Joint> GetJoints()
        {
            List<Joint> allCuisineJoints = new List<Joint> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM joints WHERE cuisine_id = @cuisine_id;";
            MySqlParameter cuisineId = new MySqlParameter();
            cuisineId.ParameterName = "@cuisine_id";
            cuisineId.Value = this._id;
            cmd.Parameters.Add(cuisineId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int jointId = rdr.GetInt32(0);
                string jointName = rdr.GetString(1);
                int jointCuisineId = rdr.GetInt32(2);
                Joint newJoint = new Joint(jointName, jointCuisineId, jointId);
                allCuisineJoints.Add(newJoint);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCuisineJoints;
        }


        public static List<Cuisine> GetAll()
        {
            List<Cuisine> allCuisines = new List<Cuisine> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM cuisines;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int CuisineId = rdr.GetInt32(0);
                string CuisineName = rdr.GetString(1);
                Cuisine newCuisine = new Cuisine(CuisineName, CuisineId);
                allCuisines.Add(newCuisine);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCuisines;
        }

        public static Cuisine Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM cuisines WHERE id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int CuisineId = 0;
            string CuisineName = "";
            while (rdr.Read())
            {
                CuisineId = rdr.GetInt32(0);
                CuisineName = rdr.GetString(1);
            }
            Cuisine newCuisine = new Cuisine(CuisineName, CuisineId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newCuisine;
        }


        public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO cuisines (name) VALUES (@name);";
        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@name";
        name.Value = this._name;
        cmd.Parameters.Add(name);
        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

    }
}
