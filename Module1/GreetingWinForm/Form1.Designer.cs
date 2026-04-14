namespace GreetingWinForm;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        buttonGreet = new Button();
        textBoxUsername = new TextBox();
        labelUsername = new Label();
        SuspendLayout();
        // 
        // buttonGreet
        // 
        buttonGreet.Location = new Point(86, 111);
        buttonGreet.Margin = new Padding(4, 5, 4, 5);
        buttonGreet.Name = "buttonGreet";
        buttonGreet.Size = new Size(96, 32);
        buttonGreet.TabIndex = 1;
        buttonGreet.Text = "Greet";
        buttonGreet.UseVisualStyleBackColor = true;
        buttonGreet.Click += buttonGreet_Click;
        // 
        // textBoxUsername
        // 
        textBoxUsername.Location = new Point(13, 75);
        textBoxUsername.Margin = new Padding(4, 5, 4, 5);
        textBoxUsername.MaxLength = 30;
        textBoxUsername.Name = "textBoxUsername";
        textBoxUsername.Size = new Size(256, 29);
        textBoxUsername.TabIndex = 0;
        // 
        // labelUsername
        // 
        labelUsername.AutoSize = true;
        labelUsername.Location = new Point(13, 49);
        labelUsername.Margin = new Padding(4, 0, 4, 0);
        labelUsername.Name = "labelUsername";
        labelUsername.Size = new Size(88, 21);
        labelUsername.TabIndex = 2;
        labelUsername.Text = "Your name:";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(284, 238);
        Controls.Add(labelUsername);
        Controls.Add(textBoxUsername);
        Controls.Add(buttonGreet);
        Font = new Font("Segoe UI", 12F);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Margin = new Padding(4, 5, 4, 5);
        Name = "Form1";
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "GreetingForm";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button buttonGreet;
    private TextBox textBoxUsername;
    private Label labelUsername;
}
