using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Diner;

namespace Diner.Models
{
    public class Joint
    {
        private string _name;
        private int _id;
        private int _cuisineId;
        private string _notes;

        public Joint(string name, int cuisineId, string notes, int id = 0)
        {
            _name = name;
            _cuisineId = cuisineId;
            _id = id;
            _notes = notes;
        }

        public Joint(string name, int cuisineId, int id = 0)
        {
            _name = name;
            _cuisineId = cuisineId;
            _id = id;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string newName)
        {
            _name = newName;
        }

        public int GetId()
        {
            return _id;
        }

        public int GetCuisineId()
        {
          return _cuisineId;
        }

        public void SetNotes(string newNotes)
        {
          _notes = newNotes;
        }

        public string GetNotes()
        {
          return _notes;
        }




    public static List<Joint> GetAll()
      {
        List<Joint> allJoints = new List<Joint> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM joints;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int jointId = rdr.GetInt32(0);
          string jointName = rdr.GetString(1);
          int jointCuisineId = rdr.GetInt32(2);
          Joint newJoint = new Joint(jointName, jointCuisineId, jointId);
          allJoints.Add(newJoint);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allJoints;
      }


    public static Joint Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM joints WHERE id = (@searchId);";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int jointId = 0;
        string jointName = "";
        int jointCuisineId = 0;
        string jointNotes = "";
        while(rdr.Read())
        {
          jointId = rdr.GetInt32(0);
          jointName = rdr.GetString(1);
          jointCuisineId = rdr.GetInt32(2);
          jointNotes = rdr.GetString(3);
        }
        Joint newJoint = new Joint(jointName, jointCuisineId, jointNotes, jointId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newJoint;
      }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO joints (name, cuisine_id, notes) VALUES (@name, @cuisine_id, @notes);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);
            MySqlParameter cuisineId = new MySqlParameter();
            cuisineId.ParameterName = "@cuisine_id";
            cuisineId.Value = this._cuisineId;
            cmd.Parameters.Add(cuisineId);
            MySqlParameter notes = new MySqlParameter();
            notes.ParameterName = "@notes";
            notes.Value = this._notes;
            cmd.Parameters.Add(notes);
            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }
        }

        public static void Delete(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM joints WHERE id = @thisId;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit (string newName, string newNotes)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE joints SET name = @newName, notes = @newNotes WHERE id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);
            MySqlParameter notes = new MySqlParameter();
            notes.ParameterName = "@newNotes";
            notes.Value = newNotes;
            cmd.Parameters.Add(notes);
            cmd.ExecuteNonQuery();
            _name = newName;
            _notes = newNotes;

            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
        }

        public static void ClearAll()
          {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM joints;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }
          }

    }
}
