using System;

public abstract class User
{
    // Properties

    public string Name { get; set; }
    public string Email { get; set; }
    private string Password { get; set; }

    // Constructor

    protected User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    // Methods

    public bool Login(string email, string password)
	{
		return Email == email && Password == password;
	}
}