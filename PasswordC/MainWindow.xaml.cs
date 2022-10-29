using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordC;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public PasswordViewModel passwordViewModel = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = passwordViewModel;
    }


    private void CheckOnClick(object sender, RoutedEventArgs e)
    {
        passwordViewModel.Decode();
    }

    private void EncryptOnClick(object sender, RoutedEventArgs e)
    {
        passwordViewModel.Encrypt();
    }
}