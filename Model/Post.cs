namespace Tryneboka.Models {
    public class Post {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public string Content { get; set; } = "";
    }
}