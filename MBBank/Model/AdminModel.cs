using System;
using System.Collections.Generic;
using MBBank.Entity;
using MBBank.Util;
using MySql.Data.MySqlClient;

namespace MBBank.Model
{
    public class AdminModel: IAdminModel
    {
        private readonly string _insertCommand = $"INSERT INTO admins (username, password_hash, fullname, salt, phone, createdAt) VALUES (@username, @password_hash, @fullname, @salt, @phone, @createdAt)";
        private readonly string _selectByUsernameCommand = $"SELECT * FROM admins WHERE username = @username";
        public Admin Save(Admin admin)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    //cnn.Open();
                    var mySqlCommand =
                        new MySqlCommand(_insertCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("username", admin.UserName);
                    mySqlCommand.Parameters.AddWithValue("password_hash", admin.PasswordHash);
                    mySqlCommand.Parameters.AddWithValue("fullname", admin.FullName);
                    mySqlCommand.Parameters.AddWithValue("salt", admin.Salt);
                    mySqlCommand.Parameters.AddWithValue("phone", admin.Phone);
                    mySqlCommand.Parameters.AddWithValue("createdAt", admin.CreatedAt);
                    mySqlCommand.Prepare();
                    var result = mySqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return admin;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
        }

        public bool Update(string id, Admin updateAdmin)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public Admin FindByUsername(string username)
        {
            try
            {
                using ( var cnn = ConnectionHelper.GetInstance())
                {
                    //cnn.Open();
                    var sqlCmd = new MySqlCommand(_selectByUsernameCommand, cnn);
                    sqlCmd.Parameters.AddWithValue("@username", username);
                    sqlCmd.Prepare();
                    var reader = sqlCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        var admin = new Admin()
                        {
                            UserName = reader.GetString("username"),
                            PasswordHash = reader.GetString("password_hash"),
                            Salt = reader.GetString("salt")
                        };
                        return admin;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
        }

        public Admin FindById(string id)
        {
            throw new System.NotImplementedException();
        }

        public List<Admin> FindAll(int page, int limit)
        {
            throw new System.NotImplementedException();
        }
    }
}