using MySql.Data.MySqlClient;
using project1.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI.WebControls;

namespace project1.Models.DAO
{
    public class UserDAO
    {
        public UserDTO updateUser { get; private set; }

        /// <summary>
        /// Insert an user into user table
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A string can be success on table</returns>
        public string InsertUser(UserDTO user)
        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Users (name, email) VALUES (@pName, @pEmail)";

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@pName", user.Name);
                        command.Parameters.AddWithValue("@pEmail", user.Email);

                        int rowsAffected = command.ExecuteNonQuery();

                       if(rowsAffected > 0)
                        {
                            response = "Success";
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error in project1.Models.DTO.InsertUser " + ex.Message);
            }


            return response;
        }

        /// <summary>
        /// Return all user on the table
        /// </summary>
        /// <param name="user"></param>
        /// <returns>List</returns>
        public List<UserDTO> ReadUsers()
        {
            List<UserDTO> users = new List<UserDTO>();  

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM Users";

                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using(MySqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                UserDTO user = new UserDTO();
                                user.Id = reader.GetInt32("id");
                                user.Name = reader.GetString("name");
                                user.Email = reader.GetString("email");
                                users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in project1.Models.DTO.InsertUser " + ex.Message);
            }
            return users;
        }

        public UserDTO GetUserById(int id)
        {
            using(MySqlConnection connection = Config.GetConnection())
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Users WHERE id=@pId";

                using(MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@pId", id);

                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            UserDTO user = new UserDTO();
                            user.Id = reader.GetInt32("id");
                            user.Name = reader.GetString("name");
                            user.Email = reader.GetString("email");
                            return user;
                        }
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// Quiz #1
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string UpdateUser(UserDTO user, int id)
        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string updateQuery = "UPDATE Users SET name = @pName, email = @pEmail WHERE id=@pId";

                    using(MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@pName", user.Name);
                        command.Parameters.AddWithValue("@pEmail", user.Email);
                        command.Parameters.AddWithValue("@pId", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if(rowsAffected > 0)
                        {
                            response = "Success";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            
            return response;
        }

        /// <summary>
        /// Quiz #1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public string DeleteUser(int id)
        {
            string response = "Failed";

            try
            {
                using (MySqlConnection connection = Config.GetConnection())
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM Users WHERE id = @pId";

                    using(MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@pId", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if(rowsAffected > 0)
                        {
                            response = "Success";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            return response;
        }
    }
}