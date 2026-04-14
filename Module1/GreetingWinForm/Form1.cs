namespace GreetingWinForm;

public partial class Form1 : Form
{
    public Form1() => InitializeComponent();

    private void buttonGreet_Click(object sender, EventArgs e) => MessageBox.Show($"Hello, {textBoxUsername.Text}", "Greeting");
}
