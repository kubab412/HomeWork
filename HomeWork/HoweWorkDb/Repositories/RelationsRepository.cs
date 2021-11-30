using HoweWorkDb.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HoweWorkDb.Repositories
{
    public class RelationsRepository
    {
        private readonly HomeWorkContext _db;
        private readonly DoctorsRepository _doctors;
        private readonly WorkPlaceRepository _workPlace;
        public RelationsRepository(HomeWorkContext db, DoctorsRepository doctors, WorkPlaceRepository workPlace)
        {
            _db = db;
            _doctors = doctors;
            _workPlace = workPlace;
        }

        public List<Relations> GetAll()
        {
            List<Relations> list = new List<Relations>();
            using (MySqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT relations.Id, doctors.FirstName, doctors.LastName, workplace.Name From ((doctors " +
                    "INNER JOIN relations ON doctors.Id = relations.DoctorId)" +
                    "INNER JOIN workplace ON workplace.Id = relations.WorkPlaceId)", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Relations()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Name = reader["Name"].ToString(),

                        });
                    }
                }
            }
            return list;
        }

        public void Insert(string name, string placeName)
        {
            using (MySqlConnection conn = _db.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO `relations` (`Id`, `DoctorId`, `WorkPlaceId`) " +
                    "VALUES (NULL, @DoctorId, @WorkPlaceId);"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        var doctor = _doctors.GetAll().Where(x => x.FirstName == name).Select(x => x.Id).FirstOrDefault();
                        var workPlace = _workPlace.GetAll().Where(x => x.Name == placeName).Select(x => x.Id).FirstOrDefault();
                        cmd.Parameters.AddWithValue("@DoctorId", doctor);
                        cmd.Parameters.AddWithValue("@WorkPlaceId", workPlace);
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
                    "Delete FROM relations Where Id = @id"))
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
