using System.Collections.Generic;

class User
{
    public string Name;
    public string Email;
    public string Nickname;
    public List<Post> Posts;

    public User(string name, string email, string nickname)
    {
        Name = name;
        Email = email;
        Nickname = nickname;
        Posts = new List<Post>();
    }
}