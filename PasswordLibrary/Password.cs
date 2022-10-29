namespace PasswordLibrary;

public class Password
{
    protected string _password;

    public Password(string password)
    {
        _password = password;
    }

    public bool CheckPassword(string password)
    {
        return password == _password;
    }
}