using Npgsql;
using Semester_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semester_Projekt.Data_Access
{
    public class RolesRepository
    {
        private readonly string _connectionString;

        public RolesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Roles> GetAllRoles()
        {
            var AllRoles = new List<Roles>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from roles;";
            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                AllRoles.Add(new Roles
                {
                    Roles_Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),


                });
            }

            return AllRoles;
        }

        public Roles GetRoleByID(int role_id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from roles where roles_id = @roles_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("roles_id", role_id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var NewRole = new Roles
                {
                    Roles_Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),

                };
                return NewRole;
            }
            return null;
        }

        public Roles GetRoleByName(string title)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from roles where title = @title;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("title", title);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var NewRole = new Roles
                {
                    Roles_Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),

                };
                return NewRole;
            }
            return null;
        }

        //add, delete, update role?
    }
}
