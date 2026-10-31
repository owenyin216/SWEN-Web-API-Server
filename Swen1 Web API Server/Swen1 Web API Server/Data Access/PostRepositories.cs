using Npgsql;
using Semester_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Semester_Projekt.Data_Access
{
    public class PostRepositories
    {
        private readonly string _connectionString;

        public PostRepositories(string connectionString)
        {
            _connectionString = connectionString;
        }


        public List<Post> GetAllPosts()
        {
            var AllPosts = new List<Post>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from posts;";
            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                AllPosts.Add(new Post
                {
                    Post_id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Body = reader.GetString(3),
                    Poster_Id = reader.GetInt32(4),
                    //Status = reader.GetString(5),
                    Created_at = reader.GetDateTime(6),

                });
            }

            return AllPosts;
        }
        public void UploadPost(Post post) {
            //with status
            //"INSERT INTO posts (title, description, body, user_id, status, created_at) " +
            //"VALUES (@title, @description, @body, @user_id, @status, @created_at)";
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "INSERT INTO posts (title, description, body, user_id, created_at) " +
                "VALUES (@title, @description, @body, @user_id, @created_at)";
            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("title", post.Title);
            command.Parameters.AddWithValue("description", post.Description);
            command.Parameters.AddWithValue("body", post.Body);
            command.Parameters.AddWithValue("user_id", post.Poster_Id);
            //command.Parameters.AddWithValue("status", post.Sta);
            command.Parameters.AddWithValue("created_at", post.Created_at);

            command.ExecuteNonQuery();

        }

        public Post? GetPostByID(int post_id) {

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from posts where post_id = @post_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("post_id", post_id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var NewPost = new Post
                {
                    Post_id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Body = reader.GetString(3),
                    Poster_Id = reader.GetInt32(4),
                    //Status = reader.GetString(5),
                    Created_at = reader.GetDateTime(6),

                };
                return NewPost;
            }
            return null;
        }

        public void DeletePost(int postId) {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = " DELETE FROM posts WHERE post_id = @post_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("post_id", postId);
            command.ExecuteNonQuery();
        }
        

        public void EditPost(Post post) {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "Update posts " +
                "set title = @title, description = @description, body = @body, user_id= @user_id, created_at = @created_at  " +
                "where post_id = @post_id;";
            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("title", post.Title);
            command.Parameters.AddWithValue("description", post.Description);
            command.Parameters.AddWithValue("body", post.Body);
            command.Parameters.AddWithValue("post_id", post.Poster_Id);
            //command.Parameters.AddWithValue("status", post.Sta);
            command.Parameters.AddWithValue("created_at", post.Created_at);

            command.ExecuteNonQuery();
        }

        public List<Post>? GetPostsByUser(User user)
        {
            List<Post> AllPosts = new List<Post>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from posts where user_id = @user_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("user_id", user.User_id);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                AllPosts.Add(new Post
                {
                    Post_id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Body = reader.GetString(3),
                    Poster_Id = reader.GetInt32(4),
                    //Status = reader.GetString(5),
                    Created_at = reader.GetDateTime(6),

                });
               
            }
            return null;
        }

        public List<Post>? GetPostsByUser(int user_id)
        {
            List<Post> AllPosts = new List<Post>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from posts where user_id = @user_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("user_id", user_id);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                AllPosts.Add(new Post
                {
                    Post_id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Body = reader.GetString(3),
                    Poster_Id = reader.GetInt32(4),
                    //Status = reader.GetString(5),
                    Created_at = reader.GetDateTime(6),

                });

            }
            return null;
        }

        //to do getallfavbyuser

    }
}
