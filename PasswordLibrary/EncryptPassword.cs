using System.Text;

namespace PasswordLibrary;

public class EncryptPassword : Password
{
    public EncryptPassword(string password) : base(password)
    {
    }

    public string Encrypt(uint offset)
    {
        offset %= 26;
        var newPass = "";
        foreach (var ch in _password)
            if (ch >= 65 && ch <= 122)
            {
                if (ch <= 90)
                    newPass += char.ToString((char) (ch + offset >= 91
                        ? 65 + offset - (91 - ch)
                        : ch + offset));
                else if (ch >= 97)
                    newPass += char.ToString((char) (ch + offset > 122
                        ? 97 + offset - (123 - ch)
                        : ch + offset));
                else
                    newPass += ch;
            }
            else
            {
                newPass += ch;
            }

        return newPass;
    }

    public string Encrypt(int offset)
    {
        if (offset < 0)
            return Decode((uint) Math.Abs(offset));
        return Encrypt((uint) offset);
    }

    public string Decode(uint offset)
    {
        offset %= 26;

        var newPass = "";
        foreach (var ch in _password)
            if (ch >= 65 && ch <= 122)
            {
                if (ch <= 90)
                    newPass += char.ToString((char) (ch - offset < 65
                        ? 91 - (offset - (ch - 65))
                        : ch - offset));
                else if (ch >= 97)
                    newPass += char.ToString((char) (ch - offset < 97 ? 122 - (offset - (ch - 96)) : ch - offset));
                else
                    newPass += ch;
            }
            else
            {
                newPass += ch;
            }

        return newPass;
    }

    public string Decode(int offset)
    {
        if (offset < 0)
            return Encrypt((uint) Math.Abs(offset));
        return Decode((uint) offset);
    }
}