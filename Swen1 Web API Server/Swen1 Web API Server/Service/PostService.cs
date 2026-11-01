using Semester_Projekt.Data_Access;
using Semester_Projekt.Models;

namespace Swen1_Web_API_Server.Service
{
    public class PostService
    {
        private readonly PostRepositories _postRepository;
        public PostService(PostRepositories postRepository)
        {
            _postRepository = postRepository;
        }

        public void CreatePost(Post post)
        {
            if (String.IsNullOrEmpty(post.Title) && String.IsNullOrEmpty(post.Description) && String.IsNullOrEmpty(post.Body))
            {
                throw new Exception("Titel, Describtion und Body durfen nicht leer sein");
            }
            else
            {
                post.Created_at = DateTime.UtcNow;
                _postRepository.UploadPost(post);
            }
        }

        public Post? GetPostByID(int post_id) 
        { 
            return _postRepository.GetPostByID(post_id);
        }
        public List<Post> GetAllPosts()
        {
            return _postRepository.GetAllPosts();
        }

        public void UpdatePost(Post updatedPost)
        {
            var existingPost = _postRepository.GetPostByID(updatedPost.Post_id);
            if (existingPost == null)
            {
                throw new Exception("Post existiert nicht.");
            }

            if (String.IsNullOrEmpty(updatedPost.Title)&& String.IsNullOrEmpty(updatedPost.Description) && String.IsNullOrEmpty(updatedPost.Body))
            {
                throw new Exception("Der Titel darf nicht leer sein.");
            }

            _postRepository.EditPost(updatedPost);
        }

        public void DeletePostByID(int postId)
        {
            var existingPost = _postRepository.GetPostByID(postId);
            if (existingPost == null)
            {
                throw new Exception("Post existiert nicht und kann nicht gelöscht werden.");
            }

            _postRepository.DeletePost(postId);
        }
    }
}
