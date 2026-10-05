using System;
using System.Collections.Generic;

class Comment
{
    private string _commenterName;
    private string _text;

    public Comment(string commenterName, string text)
    {
        _commenterName = commenterName;
        _text = text;
    }

    public string GetCommenterName() => _commenterName;
    public string GetText() => _text;
}

class Video
{
    private string _title;
    private string _author;
    private int _lengthInSeconds;
    private List<Comment> _comments = new List<Comment>();

    public Video(string title, string author, int lengthInSeconds)
    {
        _title = title;
        _author = author;
        _lengthInSeconds = lengthInSeconds;
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments() => _comments.Count;

    public string GetTitle() => _title;
    public string GetAuthor() => _author;
    public int GetLength() => _lengthInSeconds;
    public List<Comment> GetComments() => _comments;
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video v1 = new Video("10 C# Tips for Beginners", "CodeWithMosh", 720);
        v1.AddComment(new Comment("Alice", "This was super helpful, thanks!"));
        v1.AddComment(new Comment("Bob", "I never knew about the null-coalescing operator before."));
        v1.AddComment(new Comment("CarlosD", "Great pacing, easy to follow."));
        videos.Add(v1);

        Video v2 = new Video("Building a REST API in ASP.NET", "TechTalks", 1845);
        v2.AddComment(new Comment("Priya", "Best tutorial I've found on this topic."));
        v2.AddComment(new Comment("James", "The deployment section was a bit rushed though."));
        v2.AddComment(new Comment("SarahK", "Can you do one on gRPC next?"));
        v2.AddComment(new Comment("DevDude99", "Finally got mine working after following this. Thank you!"));
        videos.Add(v2);

        Video v3 = new Video("Object-Oriented Design Patterns", "CleanCodeAcademy", 2340);
        v3.AddComment(new Comment("Michael", "The factory pattern example was really clear."));
        v3.AddComment(new Comment("Yuki", "I wish you had covered the observer pattern too."));
        v3.AddComment(new Comment("TomH", "Bookmarking this for my team to watch."));
        videos.Add(v3);

        Video v4 = new Video("Intro to Git and GitHub", "OpenSourceGuru", 960);
        v4.AddComment(new Comment("Nadia", "This finally made branching click for me."));
        v4.AddComment(new Comment("LucasB", "I'd love a follow-up on rebasing."));
        v4.AddComment(new Comment("EmmaPW", "Short and to the point, love it."));
        videos.Add(v4);

        foreach (Video video in videos)
        {
            Console.WriteLine($"Title:    {video.GetTitle()}");
            Console.WriteLine($"Author:   {video.GetAuthor()}");
            Console.WriteLine($"Length:   {video.GetLength()} seconds");
            Console.WriteLine($"Comments: {video.GetNumberOfComments()}");
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  - {comment.GetCommenterName()}: {comment.GetText()}");
            }
            Console.WriteLine();
        }
    }
}
