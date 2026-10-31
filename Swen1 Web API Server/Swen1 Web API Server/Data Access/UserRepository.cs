using Npgsql;
using Semester_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Semester_Projekt.Data_Access
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "INSERT INTO users (username, passwords, roles_id, created_at, email, email_certified, birthdate, ageverified) " +
                "VALUES (@username, @passwords, @roles_id, @created_at, @email, @email_certified, @birthdate, @ageverified)";
            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("username", user.Username);
            command.Parameters.AddWithValue("passwords", user.Password);
            command.Parameters.AddWithValue("roles_id", user.Roles_Id);
            command.Parameters.AddWithValue("created_at", user.Created_At);
            command.Parameters.AddWithValue("email", user.Email);
            command.Parameters.AddWithValue("email_certified", user.Email_Certified);
            command.Parameters.AddWithValue("birthdate", user.Birthdate);
            command.Parameters.AddWithValue("ageverified", user.Ageverified);

            command.ExecuteNonQuery();
        }

        public User? GetUserByUsername(string username) {

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from users where username = @username;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("username", username);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var NewUser = new User
                {
                    User_id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Roles_Id = reader.GetInt32(3),
                    Created_At = reader.GetDateTime(4),
                    Email = reader.GetString(5),
                    Email_Certified = reader.GetBoolean(6),
                    Birthdate = DateOnly.FromDateTime(reader.GetDateTime(7)),
                    Ageverified = reader.GetBoolean(8),

                };
                return NewUser;
            }
            return null;
        }

        public User? GetUserByID(int use_id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from users where user_id = @user_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("user_id", use_id);
            using var reader = command.ExecuteReader();
            if(reader.Read())
            {
                var NewUser = new User
                {
                    User_id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Roles_Id = reader.GetInt32(3),
                    Created_At = reader.GetDateTime(4),
                    Email = reader.GetString(5),
                    Email_Certified = reader.GetBoolean(6),
                    Birthdate = DateOnly.FromDateTime(reader.GetDateTime(7)),
                    Ageverified = reader.GetBoolean(8),

                };
                return NewUser;
            }
            return null;
        }

        public List<User> GetAllUsers()
        {
            var AllUsers = new List<User>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from users;";
            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                AllUsers.Add(new User
                {
                    User_id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Roles_Id = reader.GetInt32(3),
                    Created_At = reader.GetDateTime(4),
                    Email = reader.GetString(5),
                    Email_Certified = reader.GetBoolean(6),
                    Birthdate = DateOnly.FromDateTime(reader.GetDateTime(7)),
                    Ageverified = reader.GetBoolean(8),

                });
            }

            return AllUsers;
        }

        public void UpdateUser(User user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "Update users " +
                "set username = @username, passwords = @passwords, roles_id = @roles_id, created_at= @created_at, email = @email, email_certified= @email_certified, birthdate= @birthdate, ageverified= @ageverified  " +
                "where user_id = @user_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("user_id", user.User_id);
            command.Parameters.AddWithValue("username", user.Username);
            command.Parameters.AddWithValue("passwords", user.Password);
            command.Parameters.AddWithValue("roles_id", user.Roles_Id);
            command.Parameters.AddWithValue("created_at", user.Created_At);
            command.Parameters.AddWithValue("email", user.Email);
            command.Parameters.AddWithValue("email_certified", user.Email_Certified);
            command.Parameters.AddWithValue("birthdate", user.Birthdate);
            command.Parameters.AddWithValue("ageverified", user.Ageverified);
            command.ExecuteNonQuery();
        }

        public void DeleteUser(User user) 
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = " DELETE FROM users WHERE user_id = @user_id and username = @username;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("user_id", user.User_id);
            command.Parameters.AddWithValue("username", user.Username);
            command.ExecuteNonQuery();
        }
        public void DeleteUserByID(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = " DELETE FROM users WHERE user_id = @user_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("user_id", id);
            command.ExecuteNonQuery();
        }

        public void DeleteUserByName(string name)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = " DELETE FROM users WHERE username = @username;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("username", name);
            command.ExecuteNonQuery();
        }

        //get all admins
        //get all users

        //to do fav add, remove
    }
}
