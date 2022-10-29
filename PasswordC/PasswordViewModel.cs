using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
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
        {PasswordType.Bad, "Ваш пароль ужасен"},
        {PasswordType.Weak, "Ваш пароль плохой"},
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
                if (!StrongPassword.CheckPassword(_password2))
                {
                    ErrorMessage = "Пароли не совпадают";
                    PassStrongMessage = "";
                }
                else
                {
                    ErrorMessage = "";
                    PassStrongMessage = typeToMessage[StrongPassword.CheckPasswordOnStrong()];
                }
            }
            else
            {
                PassStrongMessage = "";
                ErrorMessage = "";
                _password1 = "";
            }

            NotifyPropertyChanged("PassStrongMessage");
            NotifyPropertyChanged("ErrorMessage");
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
                    PassStrongMessage = "";
                }
                else
                {
                    ErrorMessage = "";
                    PassStrongMessage = typeToMessage[StrongPassword.CheckPasswordOnStrong()];
                }
            }
            else
            {
                PassStrongMessage = "";
                ErrorMessage = "";
                _password2 = "";
            }

            NotifyPropertyChanged("PassStrongMessage");
            NotifyPropertyChanged("ErrorMessage");
        }
    }

    public string ErrorMessage { get; set; } = "";
    public string PassStrongMessage { get; set; } = "";

    public void Encrypt()
    {
        if (_password1 == _password2 && _password1.Trim() != "")
            using (StreamWriter writer = new("pass.txt"))
            {
                var offset = new Random().Next(-100, 100);
                // var offset = 3;
                writer.WriteLine(offset);
                writer.Write(new EncryptPassword(_password1).Encrypt(offset));
            }
    }

    public void Decode()
    {
        if (_password1 == _password2 && _password1.Trim() != "")
            using (StreamReader reader = new("pass.txt"))
            {
                var offset = int.Parse(reader.ReadLine()!);
                var savedPass = new EncryptPassword(reader.ReadLine()!).Decode(offset);
                ErrorMessage = "";
                if (savedPass != _password1)
                    ErrorMessage = "Пароль не совпадает с сохранённым";
                else
                    MessageBox.Show("Поздравляю, вы угадали пароль:)");
                NotifyPropertyChanged("ErrorMessage");
            }
    }
}