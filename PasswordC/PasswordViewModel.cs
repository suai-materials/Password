using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PasswordLibrary;

namespace PasswordC;

public class PasswordViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void NotifyPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    public Dictionary<PasswordType, string> typeToMessage = new()
    {
        {PasswordType.Bad, "Ваш пароль ужасен"}, {PasswordType.Weak, "Ваш пароль плохой"},
        {PasswordType.Medium, "Ваш пароль средней сложности"},
        {PasswordType.Good, "Ваш пароль хороший"},
        {PasswordType.Strong, "Ваш пароль сильный"}
    };

    private string _password1 = "";
    private string _password2 = "";

    public StrongPassword StrongPassword = new("");

    public string Password1
    {
        get => _password1;
        set
        {
            if (value.Trim() != "")
            {
                _password1 = value;
                StrongPassword = new StrongPassword(_password1);
            }
            else
            {
                PassStrongMessage = "";
                ErrorMessage = "";
                NotifyPropertyChanged("PassStrongMessage");
                NotifyPropertyChanged("ErrorMessage");
            }
        }
    }

    public string Password2
    {
        get => _password2;
        set
        {
            if (value.Trim() != "")
            {
                _password2 = value;
                if (!StrongPassword.CheckPassword(_password2))
                {
                    ErrorMessage = "Пароли не совпадают";
                    NotifyPropertyChanged("ErrorMessage");
                }
                else
                {
                    ErrorMessage = "";
                    PassStrongMessage = typeToMessage[StrongPassword.CheckPasswordOnStrong()];
                    NotifyPropertyChanged("PassStrongMessage");
                }
            }
            else
            {
                PassStrongMessage = "";
                ErrorMessage = "";
                NotifyPropertyChanged("PassStrongMessage");
                NotifyPropertyChanged("ErrorMessage");
            }
        }
    }

    public string ErrorMessage { get; set; } = "";
    public string PassStrongMessage { get; set; } = "";
}