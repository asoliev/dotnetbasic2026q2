using System.Windows;
using System.Windows.Input;

namespace GreetingWpfApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();
    private void BtnGreet_Click(object sender, RoutedEventArgs e) => MessageBox.Show($"Hello, {txtUsername.Text}");

    private void TxtUsername_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            MessageBox.Show($"Hello, {txtUsername.Text}");
            e.Handled = true;
        }
    }
}
