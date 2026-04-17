using System.Windows;
using System.Windows.Input;
using GreetingLibrary;

namespace GreetingWpfApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private void BtnGreet_Click(object sender, RoutedEventArgs e) => ShowGreeting();

    private void TxtUsername_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            ShowGreeting();
            e.Handled = true;
        }
    }
    
    private void ShowGreeting() => MessageBox.Show(GreetingService.GetGreeting(txtUsername.Text));
}
