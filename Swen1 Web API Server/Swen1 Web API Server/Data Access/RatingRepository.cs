using Npgsql;
using Semester_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semester_Projekt.Data_Access
{
    public class RatingRepository
    {
        public RatingRepository() { }
        private readonly string _connectionString;

        public RatingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddRating(Ratings rating)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "INSERT INTO rating (rating_id, rating_comment, user_id, post_id, rating) " +
                "VALUES (@rating_id, @rating_comment, @user_id, @post_id, @rating)";
            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("rating_id", rating.Rating_id);
            command.Parameters.AddWithValue("rating_comment", rating.Rating_comment);
            command.Parameters.AddWithValue("user_id", rating.User_Id);
            command.Parameters.AddWithValue("post_id", rating.Post_Id);
            command.Parameters.AddWithValue("rating", rating.Rating);

            command.ExecuteNonQuery();
        }

        public void UpdateRating(Ratings rating)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "Update rating " +
                "set rating_id = @rating_id, rating_comment = @rating_comment, user_id =@user_id, post_id= @post_id, rating= @rating  " +
                "where rating_id = @rating_id;";
            using var command = new NpgsqlCommand(sql, connection);
            
            command.Parameters.AddWithValue("rating_id", rating.Rating_id);
            command.Parameters.AddWithValue("rating_comment", rating.Rating_comment);
            command.Parameters.AddWithValue("user_id", rating.User_Id);
            command.Parameters.AddWithValue("post_id", rating.Post_Id);
            command.Parameters.AddWithValue("rating", rating.Rating);

            command.ExecuteNonQuery();
        }


        public List<Ratings> GetRatingsByPost(int post_id) {
            var AllRatings = new List<Ratings>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from rating where post_id = @post_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("post_id", post_id);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                AllRatings.Add(new Ratings
                {
                    Rating_id = reader.GetInt32(0),
                    Rating_comment = reader.GetString(1),
                    User_Id = reader.GetInt32(2),
                    Post_Id = reader.GetInt32(3),
                    Rating = reader.GetInt32(4),

                });
            }
            return AllRatings;
        }

        public List<Ratings> GetRatingByUserID(int user_id)
        {
            var AllRatings = new List<Ratings>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from rating where user_id = @user_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("user_id", user_id);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                AllRatings.Add(new Ratings
                {
                    Rating_id = reader.GetInt32(0),
                    Rating_comment = reader.GetString(1),
                    User_Id = reader.GetInt32(2),
                    Post_Id = reader.GetInt32(3),
                    Rating = reader.GetInt32(4),

                });
            }
            return AllRatings;
        }
        

        public List<Ratings> GetRatingByUser(int user_id) {
            var AllPosts = new List<Ratings>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from rating where user_id = @user_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("user_id", user_id);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                AllPosts.Add(new Ratings
                {
                    Rating_id = reader.GetInt32(0),
                    Rating_comment = reader.GetString(1),
                    User_Id = reader.GetInt32(2),
                    Post_Id = reader.GetInt32(3),
                    Rating = reader.GetInt32(4),

                });
            }
            return AllPosts;
        }

        public Ratings GetRatingByID(int rating_id) {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "select * from rating where rating_id = @rating_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("rating_id", rating_id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var rating = new Ratings
                {
                    Rating_id = reader.GetInt32(0),
                    Rating_comment = reader.GetString(1),
                    User_Id = reader.GetInt32(2),
                    Post_Id = reader.GetInt32(3),
                    Rating = reader.GetInt32(4),

                };
                return rating;
            }
            return null;
        }

        public double GetAverageScoreForPost(int post_Id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = "SELECT AVG(rating) FROM rating WHERE post_id = @post_Id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("post_Id", post_Id);
            using var reader = command.ExecuteReader();
            if (reader.Read() && !reader.IsDBNull(0))
            {
                var avg = reader.GetDouble(0);
                return avg;
            }
            return 0.0;

        }

        public void DeleteRatingByID(int rating_id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sql = " DELETE FROM rating WHERE rating_id = @rating_id;";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("rating_id", rating_id);
            command.ExecuteNonQuery();

        }


    }
}
