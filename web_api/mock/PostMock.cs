using entities_library.comment;
using entities_library.login;
using entities_library.post;
using entities_library.reaction;

namespace web_api.mock;

public class PostMock
{
    private static PostMock? instance;

    private PostMock() { }

    public static PostMock Instance 
    {
        get
        {
            if(instance == null) instance = new PostMock();
            return instance;
        }
    }

    public List<Post> Posts { get; set; } = new List<Post>();

    public long CreatePost
    (long id,string text,List<Comment> comments,List<Reaction> reactions, DateTime dateTime, User user, PostStatus postStatus, ReportStatus reportStatus )
    {
        Post post = new Post
        {
            Id = id,
            Text = text,
            Comments = comments,
            Reactions = reactions,
            DateTime = dateTime,
            User = user,
            PostStatus = postStatus,
            ReportStatus = reportStatus 
        };

        post.Id = this.Posts.Count + 1;
        this.Posts.Add(post);

        return post.Id;
    }
}