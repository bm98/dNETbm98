﻿namespace TEST_Library
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if (disposing && (components != null)) {
        components.Dispose( );
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
      this.button1 = new System.Windows.Forms.Button();
      this.RTB = new System.Windows.Forms.RichTextBox();
      this.lblFocus = new System.Windows.Forms.Label();
      this.lblDPI = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(55, 35);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(94, 55);
      this.button1.TabIndex = 0;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // RTB
      // 
      this.RTB.Location = new System.Drawing.Point(62, 127);
      this.RTB.Name = "RTB";
      this.RTB.Size = new System.Drawing.Size(343, 274);
      this.RTB.TabIndex = 1;
      this.RTB.Text = "";
      // 
      // lblFocus
      // 
      this.lblFocus.AutoSize = true;
      this.lblFocus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFocus.Location = new System.Drawing.Point(553, 101);
      this.lblFocus.Name = "lblFocus";
      this.lblFocus.Size = new System.Drawing.Size(80, 24);
      this.lblFocus.TabIndex = 2;
      this.lblFocus.Text = "FOCUS";
      this.lblFocus.MouseEnter += new System.EventHandler(this.lblFocus_MouseEnter);
      this.lblFocus.MouseLeave += new System.EventHandler(this.lblFocus_MouseLeave);
      // 
      // lblDPI
      // 
      this.lblDPI.AutoSize = true;
      this.lblDPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDPI.Location = new System.Drawing.Point(275, 9);
      this.lblDPI.Name = "lblDPI";
      this.lblDPI.Size = new System.Drawing.Size(39, 24);
      this.lblDPI.TabIndex = 3;
      this.lblDPI.Text = "DPI";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.lblDPI);
      this.Controls.Add(this.lblFocus);
      this.Controls.Add(this.RTB);
      this.Controls.Add(this.button1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.RichTextBox RTB;
    private System.Windows.Forms.Label lblFocus;
    private System.Windows.Forms.Label lblDPI;
  }
}

