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
        public RelationsRepository(HomeWorkContext db)
        {
            _db = db;
        }

        public List<Relations> GetAll()
        {
            List<Relations> list = new List<Relations>();
            using (MySqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT relations.Id, doctors.DoctorsId, workplace.WorkPlaceId, doctors.FirstName, doctors.LastName, doctors.AcademicTitle, " +
                    "doctors.Email, doctors.PhoneNumber, doctors.Specialization, workplace.Name, workplace.Street, workplace.HouseNumber, " +
                    "workplace.Place, workplace.ZipCode, workplace.Voivodeship From ((doctors " +
                    "INNER JOIN relations ON doctors.DoctorsId = relations.doctorId) " +
                    "INNER JOIN workplace ON workplace.WorkPlaceId = relations.workPlaceId)", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Relations()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            DoctorId = Convert.ToInt32(reader["DoctorsId"]),
                            WorkPlaceId = Convert.ToInt32(reader["WorkPlaceId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Name = reader["Name"].ToString(),

                        });
                    }
                }
            }
            return list;
        }

        public void Insert(int doctorId, int workPlaceId)
        {
            using (MySqlConnection conn = _db.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO `relations` (Id, doctorId, workPlaceId) " +
                    "VALUES (NULL, @DoctorId, @WorkPlaceId);"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@DoctorId", doctorId);
                        cmd.Parameters.AddWithValue("@WorkPlaceId", workPlaceId);
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
