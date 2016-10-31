using HelloWorldCore.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloWorldCore.PostModels
{
    public abstract class Post : Entity
    {
        public Post()
        {
            Comments = new HashSet<Comment>();        
        }

        public ICollection<Comment> Comments { get; private set; }          
    }
    public class UrlPost : Post
    {
        private string[] AllowedExtensions = { "gif", "gifv", "png", "jpg" };

        public UrlPost(string url)
        {
            SetUrl(url);
        }

        public string Url { get; private set; }
        private void SetUrl(string url)
        {
            foreach(var ext in AllowedExtensions)
            {
                if (url.EndsWith(ext))
                {
                    Url = url;
                    break;
                }
            }
            if(Url == null)
            {
                throw new Exception("URL extension not supported");
            }
        }

        public async Task Publish(ISaver<UrlPost> postSaver)
        {
            await postSaver.GoAsync(this);
        }
    }
    public class Comment : Entity
    { 
        //private constructor for EF      
        private Comment()
        {

        }

        public Comment(string userName, int postId, string value)
        {
            UserName = userName;
            PostId = postId;
            Value = value;
            PostDateTimeUtc = DateTime.UtcNow;
        }

        public int PostId { get; private set; }
        public string UserName { get; private set; }
        public string Value { get; private set; }
        public DateTime PostDateTimeUtc { get; private set; }
        public DateTime LastUpdatedDateTimeUtc { get; private set; }
        public async Task PublishAsync(int pingId, ISaver<Comment> commentSaver)
        {
            await commentSaver.GoAsync(this);
        }              
        public async Task EditAsync(string newValue, ISaver<Comment> commentSaver)
        {
            Value = newValue;
            LastUpdatedDateTimeUtc = DateTime.UtcNow;
            await commentSaver.GoAsync(this);
        }
    }
    public abstract class Entity
    {
        public int Id { get; private set; }
    }
}
