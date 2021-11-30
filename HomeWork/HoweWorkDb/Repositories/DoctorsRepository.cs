using HoweWorkDb.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HoweWorkDb.Repositories
{
    public class DoctorsRepository
    {
        private readonly HomeWorkContext _db;
        public DoctorsRepository(HomeWorkContext db)
        {
            _db = db;
        }



        public List<Doctors> GetAll()
        {
            List<Doctors> list = new List<Doctors>();
            using (MySqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from doctors", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Doctors()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            AcademicTitle = reader["AcademicTitle"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = Convert.ToInt32(reader["PhoneNumber"]),
                            Specialization = reader["Specialization"].ToString()
                        });
                    }
                }
            }
            return list;
        }
        public List<Doctors> Filter(string name)
        {
            List<Doctors> list = new List<Doctors>();
            using (MySqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM doctors WHERE FirstName = @name", conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);




                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Doctors()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                AcademicTitle = reader["AcademicTitle"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = Convert.ToInt32(reader["PhoneNumber"]),
                                Specialization = reader["Specialization"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public void Insert(Doctors doctor)
        {
            using (MySqlConnection conn = _db.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO `doctors` (`Id`, `FirstName`, `LastName`, `AcademicTitle`, `Email`, `PhoneNumber`, `Specialization`) " +
                    "VALUES (NULL, @doctor.FirstName, @doctor.LastName, @doctor.AcademicTitle, @doctor.Email, @doctor.PhoneNumber, @doctor.Specialization);"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@doctor.FirstName", doctor.FirstName);
                        cmd.Parameters.AddWithValue("@doctor.LastName", doctor.LastName);
                        cmd.Parameters.AddWithValue("@doctor.AcademicTitle", doctor.AcademicTitle);
                        cmd.Parameters.AddWithValue("@doctor.Email", doctor.Email);
                        cmd.Parameters.AddWithValue("@doctor.PhoneNumber", doctor.PhoneNumber);
                        cmd.Parameters.AddWithValue("@doctor.Specialization", doctor.Specialization);
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }
        public void Update(Doctors doctor)
        {
            using (MySqlConnection conn = _db.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(
                    "UPDATE Doctors Set Id = @doctor.Id, FirstName = @doctor.FirstName, LastName = @doctor.LastName, AcademicTitle = @doctor.AcademicTitle, " +
                    "Email = @doctor.Email, PhoneNumber = @doctor.PhoneNumber, Specialization = @doctor.Specialization WHERE Id = @doctor.Id"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@doctor.Id", doctor.Id);
                        cmd.Parameters.AddWithValue("@doctor.FirstName", doctor.FirstName);
                        cmd.Parameters.AddWithValue("@doctor.LastName", doctor.LastName);
                        cmd.Parameters.AddWithValue("@doctor.AcademicTitle", doctor.AcademicTitle);
                        cmd.Parameters.AddWithValue("@doctor.Email", doctor.Email);
                        cmd.Parameters.AddWithValue("@doctor.PhoneNumber", doctor.PhoneNumber);
                        cmd.Parameters.AddWithValue("@doctor.Specialization", doctor.Specialization);
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
                    "Delete FROM Doctors Where Id = @id"))
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
