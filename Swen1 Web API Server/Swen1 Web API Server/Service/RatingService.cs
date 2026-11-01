using Semester_Projekt.Data_Access;
using Semester_Projekt.Models;

namespace Swen1_Web_API_Server.Service
{
    public class RatingService
    {
        private readonly RatingRepository _ratingRepository;
        private readonly UserRepository _userRepository;
        private readonly PostRepositories _postRepository;
        public RatingService(RatingRepository ratingRepository, UserRepository userRepository, PostRepositories postRepository)
        {
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public void CreateRating(Ratings ratings)
        {
            if (ratings.Rating < 1 || ratings.Rating > 5)
            {
                throw new Exception("Die Bewertung muss zwischen 1 und 5 Sternen liegen.");
            }

            var user = _userRepository.GetUserByID(ratings.User_Id);
            if (user == null)
            {
                throw new Exception("Der angebende Benutzer existiert nicht oder ist nicht berechtigt.");
            }

            var post = _postRepository.GetPostByID(ratings.Post_Id);
            if (post == null)
            {
                throw new Exception("Der zu bewertende Post existiert nicht.");
            }

            _ratingRepository.AddRating(ratings);
        }

        public void UpdateRating(Ratings ratings)
        {
            var existingRating = _ratingRepository.GetRatingByID(ratings.Rating_id);
            if (existingRating == null)
            {
                throw new Exception("Die Bewertung existiert nicht.");
            }

            if (ratings.Rating < 1 || ratings.Rating > 5)
            {
                throw new Exception("Die Bewertung muss zwischen 1 und 5 Sternen liegen.");
            }

            _ratingRepository.UpdateRating(ratings);

        }
        public List<Ratings> GetAllRatingsFromPostID(int post_id)
        {
            return _ratingRepository.GetRatingsByPost(post_id);
        }

        public List<Ratings> GetAllRatingsFromUser(User user) 
        {
            return _ratingRepository.GetRatingByUser(user.User_id);
        }

        public void DeleteRating(int rating_id)
        {
            var existingRating = _ratingRepository.GetRatingByID(rating_id);
            if (existingRating == null)
            {
                throw new Exception("Bewertung existiert nicht und kann nicht gelöscht werden.");
            }
            else
            {
                _ratingRepository.DeleteRatingByID(rating_id);
            }
        }
    }
}
