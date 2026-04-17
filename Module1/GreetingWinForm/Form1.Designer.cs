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
        btnGreet = new Button();
        txtUsername = new TextBox();
        labelUsername = new Label();
        SuspendLayout();
        // 
        // btnGreet
        // 
        btnGreet.Location = new Point(86, 86);
        btnGreet.Margin = new Padding(4, 5, 4, 5);
        btnGreet.Name = "btnGreet";
        btnGreet.Size = new Size(96, 32);
        btnGreet.TabIndex = 1;
        btnGreet.Text = "Greet";
        btnGreet.UseVisualStyleBackColor = true;
        btnGreet.Click += BtnGreet_Click;
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(13, 50);
        txtUsername.Margin = new Padding(4, 5, 4, 5);
        txtUsername.MaxLength = 20;
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(250, 29);
        txtUsername.TabIndex = 0;
        txtUsername.KeyDown += TxtUsername_KeyDown;
        // 
        // labelUsername
        // 
        labelUsername.AutoSize = true;
        labelUsername.Location = new Point(13, 24);
        labelUsername.Margin = new Padding(4, 0, 4, 0);
        labelUsername.Name = "labelUsername";
        labelUsername.Size = new Size(128, 21);
        labelUsername.TabIndex = 2;
        labelUsername.Text = "Enter your name:";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(9F, 21F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(284, 161);
        Controls.Add(labelUsername);
        Controls.Add(txtUsername);
        Controls.Add(btnGreet);
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

    private Button btnGreet;
    private TextBox txtUsername;
    private Label labelUsername;
}
