namespace GreetingWinForm;

public partial class Form1 : Form
{
    public Form1() => InitializeComponent();

    private void BtnGreet_Click(object sender, EventArgs e) => MessageBox.Show($"Hello, {txtUsername.Text}");

    private void TxtUsername_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            MessageBox.Show($"Hello, {txtUsername.Text}");
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }
}
