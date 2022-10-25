namespace PasswordLibrary;

public class Password
{
    public string password = "";

    public bool CheckPassword(string password)
    {
        return password == this.password;
    }
}