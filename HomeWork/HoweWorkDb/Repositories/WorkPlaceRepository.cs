using HoweWorkDb.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HoweWorkDb.Repositories
{
    public class WorkPlaceRepository
    {
        private readonly HomeWorkContext _db;
        public WorkPlaceRepository(HomeWorkContext db)
        {
            _db = db;
        }
        public List<WorkPlace> GetAll()
        {
            List<WorkPlace> list = new List<WorkPlace>();
            using (MySqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from workplace", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new WorkPlace()
                        {
                            Id = Convert.ToInt32(reader["WorkPlaceId"]),
                            Name = reader["Name"].ToString(),
                            Street = reader["Street"].ToString(),
                            HouseNumber = Convert.ToInt32(reader["HouseNumber"]),
                            Place = reader["Place"].ToString(),
                            ZipCode = Convert.ToInt32(reader["ZipCode"]),
                            Voivodeship = reader["Voivodeship"].ToString()
                        });
                    }
                }
            }
            return list;
        }


        public List<WorkPlace> Filter(string name)
        {
            List<WorkPlace> list = new List<WorkPlace>();
            using (MySqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM workplace", conn))
                {




                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new WorkPlace()
                            {
                                Id = Convert.ToInt32(reader["WorkPlaceId"]),
                                Name = reader["Name"].ToString(),
                                Street = reader["Street"].ToString(),
                                HouseNumber = Convert.ToInt32(reader["HouseNumber"]),
                                Place = reader["Place"].ToString(),
                                ZipCode = Convert.ToInt32(reader["ZipCode"]),
                                Voivodeship = reader["Voivodeship"].ToString()
                            });
                        }
                    }
                }
            }
            var filtered = list.Where(x => x.Name == name).ToList();

            return filtered;
        }

        public void Insert(WorkPlace workPlace)
        {
            using (MySqlConnection conn = _db.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO `workplace` (`WorkPlaceId`, `Name`, `Street`, `HouseNumber`, `Place`, `ZipCode`, `Voivodeship`) " +
                    "VALUES (NULL, @workPlace.Name, @workPlace.Street, @workPlace.HouseNumber, @workPlace.Place, @workPlace.ZipCode, @workPlace.Voivodeship);"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@workPlace.Name", workPlace.Name);
                        cmd.Parameters.AddWithValue("@workPlace.Street", workPlace.Street);
                        cmd.Parameters.AddWithValue("@workPlace.HouseNumber", workPlace.HouseNumber);
                        cmd.Parameters.AddWithValue("@workPlace.Place", workPlace.Place);
                        cmd.Parameters.AddWithValue("@workPlace.ZipCode", workPlace.ZipCode);
                        cmd.Parameters.AddWithValue("@workPlace.Voivodeship", workPlace.Voivodeship);
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }
        public void Update(WorkPlace workPlace)
        {
            using (MySqlConnection conn = _db.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(
                    "UPDATE workplace Set WorkPlaceId = @workPlace.Id, Name = @workPlace.Name, Street = @workPlace.Street, HouseNumber = @workPlace.HouseNumber, " +
                    "Place = @workPlace.Place, ZipCode = @workPlace.ZipCode, Voivodeship = @workPlace.Voivodeship WHERE WorkPlaceId = @workPlace.Id"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@workPlace.Id", workPlace.Id);
                        cmd.Parameters.AddWithValue("@workPlace.Name", workPlace.Name);
                        cmd.Parameters.AddWithValue("@workPlace.Street", workPlace.Street);
                        cmd.Parameters.AddWithValue("@workPlace.HouseNumber", workPlace.HouseNumber);
                        cmd.Parameters.AddWithValue("@workPlace.Place", workPlace.Place);
                        cmd.Parameters.AddWithValue("@workPlace.ZipCode", workPlace.ZipCode);
                        cmd.Parameters.AddWithValue("@workPlace.Voivodeship", workPlace.Voivodeship);
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }
        public void Remove(int id)
        {
            using (MySqlConnection conn = _db.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(
                    "Delete FROM workplace Where WorkPlaceId = @id"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }
    }
}
