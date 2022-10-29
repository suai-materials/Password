using System.Text.RegularExpressions;

namespace PasswordLibrary;

public class StrongPassword : Password
{
    public Dictionary<PasswordType, string> PatternToType = new()
    {
        {PasswordType.Bad, @"[\S]{0,8}"},
        {PasswordType.Weak, @"[\S]{8,}"},
        {PasswordType.Medium, @"(?=.*[A-Z])(?=.*[a-z])(?=\S{8,})"},
        {PasswordType.Good, @"(?=.*[A-Z])(?=.*[a-z])(?=\S{8,})(?=.*[0-9])"},
        {PasswordType.Strong, @"(?=.*[A-Z])(?=.*[a-z])(?=\S{8,})(?=.*[0-9])(?=.*[!$#*])"}
    };

    public PasswordType CheckPasswordOnStrong()
    {
        var passwordType = PasswordType.Weak;
        foreach (var keyValue in PatternToType)
            if (Regex.IsMatch(_password, PatternToType[keyValue.Key]))
                passwordType = keyValue.Key;
        return passwordType;
    }

    public StrongPassword(string password) : base(password)
    {
    }
}