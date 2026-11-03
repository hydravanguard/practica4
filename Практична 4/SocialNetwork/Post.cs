using System;
using System.Collections.Generic;

class Post
{
    public DateTime CreatedAt;
    public string Text;
    public int Likes;
    public List<string> Comments;

    public Post(string text)
    {
        CreatedAt = DateTime.Now;
        Text = text;
        Likes = 0;
        Comments = new List<string>();
    }
}