namespace GreetingWinForm;

public partial class GreetingForm : Form
{
    public GreetingForm() => InitializeComponent();

    private void BtnGreet_Click(object sender, EventArgs e) => ShowGreeting();

    private void TxtUsername_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            ShowGreeting();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void ShowGreeting() => MessageBox.Show($"Hello, {txtUsername.Text}");
}
