namespace TEST_Library
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
      this.components = new System.ComponentModel.Container();
      this.button1 = new System.Windows.Forms.Button();
      this.RTB = new System.Windows.Forms.RichTextBox();
      this.lblFocus = new System.Windows.Forms.Label();
      this.lblDPI = new System.Windows.Forms.Label();
      this.txCAS = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txALT = new System.Windows.Forms.TextBox();
      this.btCalcTAS = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.txTAS = new System.Windows.Forms.TextBox();
      this.btReadEnc = new System.Windows.Forms.Button();
      this.btWriteEnc = new System.Windows.Forms.Button();
      this.btSendKeyA = new System.Windows.Forms.Button();
      this.btSendKeyCV = new System.Windows.Forms.Button();
      this.btJobRunner = new System.Windows.Forms.Button();
      this.cbxBlocking = new System.Windows.Forms.CheckBox();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.btJobRunnerDispose = new System.Windows.Forms.Button();
      this.btTool = new System.Windows.Forms.Button();
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
      this.lblFocus.Location = new System.Drawing.Point(566, 23);
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
      // txCAS
      // 
      this.txCAS.Location = new System.Drawing.Point(489, 104);
      this.txCAS.Name = "txCAS";
      this.txCAS.Size = new System.Drawing.Size(81, 20);
      this.txCAS.TabIndex = 4;
      this.txCAS.Text = "50";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(486, 88);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(28, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "CAS";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(573, 88);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(27, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "ALT";
      // 
      // txALT
      // 
      this.txALT.Location = new System.Drawing.Point(576, 104);
      this.txALT.Name = "txALT";
      this.txALT.Size = new System.Drawing.Size(81, 20);
      this.txALT.TabIndex = 6;
      this.txALT.Text = "20000";
      // 
      // btCalcTAS
      // 
      this.btCalcTAS.Location = new System.Drawing.Point(663, 104);
      this.btCalcTAS.Name = "btCalcTAS";
      this.btCalcTAS.Size = new System.Drawing.Size(94, 20);
      this.btCalcTAS.TabIndex = 8;
      this.btCalcTAS.Text = "Calc TAS";
      this.btCalcTAS.UseVisualStyleBackColor = true;
      this.btCalcTAS.Click += new System.EventHandler(this.btCalcTAS_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(486, 126);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(28, 13);
      this.label3.TabIndex = 10;
      this.label3.Text = "TAS";
      // 
      // txTAS
      // 
      this.txTAS.Location = new System.Drawing.Point(489, 142);
      this.txTAS.Name = "txTAS";
      this.txTAS.Size = new System.Drawing.Size(81, 20);
      this.txTAS.TabIndex = 9;
      this.txTAS.Text = "..";
      // 
      // btReadEnc
      // 
      this.btReadEnc.Location = new System.Drawing.Point(463, 238);
      this.btReadEnc.Name = "btReadEnc";
      this.btReadEnc.Size = new System.Drawing.Size(94, 42);
      this.btReadEnc.TabIndex = 11;
      this.btReadEnc.Text = "Read enc";
      this.btReadEnc.UseVisualStyleBackColor = true;
      this.btReadEnc.Click += new System.EventHandler(this.btReadEnc_Click);
      // 
      // btWriteEnc
      // 
      this.btWriteEnc.Location = new System.Drawing.Point(563, 238);
      this.btWriteEnc.Name = "btWriteEnc";
      this.btWriteEnc.Size = new System.Drawing.Size(94, 42);
      this.btWriteEnc.TabIndex = 12;
      this.btWriteEnc.Text = "Write enc";
      this.btWriteEnc.UseVisualStyleBackColor = true;
      this.btWriteEnc.Click += new System.EventHandler(this.btWriteEnc_Click);
      // 
      // btSendKeyA
      // 
      this.btSendKeyA.Location = new System.Drawing.Point(463, 325);
      this.btSendKeyA.Name = "btSendKeyA";
      this.btSendKeyA.Size = new System.Drawing.Size(94, 42);
      this.btSendKeyA.TabIndex = 13;
      this.btSendKeyA.Text = "Send Keys AB";
      this.btSendKeyA.UseVisualStyleBackColor = true;
      this.btSendKeyA.Click += new System.EventHandler(this.btSendKeyA_Click);
      // 
      // btSendKeyCV
      // 
      this.btSendKeyCV.Location = new System.Drawing.Point(563, 325);
      this.btSendKeyCV.Name = "btSendKeyCV";
      this.btSendKeyCV.Size = new System.Drawing.Size(94, 42);
      this.btSendKeyCV.TabIndex = 14;
      this.btSendKeyCV.Text = "Send Keys CtrlV";
      this.btSendKeyCV.UseVisualStyleBackColor = true;
      this.btSendKeyCV.Click += new System.EventHandler(this.btSendKeyCV_Click);
      // 
      // btJobRunner
      // 
      this.btJobRunner.Location = new System.Drawing.Point(463, 373);
      this.btJobRunner.Name = "btJobRunner";
      this.btJobRunner.Size = new System.Drawing.Size(94, 42);
      this.btJobRunner.TabIndex = 13;
      this.btJobRunner.Text = "JobRunner";
      this.btJobRunner.UseVisualStyleBackColor = true;
      this.btJobRunner.Click += new System.EventHandler(this.btJobRunner_Click);
      // 
      // cbxBlocking
      // 
      this.cbxBlocking.AutoSize = true;
      this.cbxBlocking.Location = new System.Drawing.Point(685, 338);
      this.cbxBlocking.Name = "cbxBlocking";
      this.cbxBlocking.Size = new System.Drawing.Size(66, 17);
      this.cbxBlocking.TabIndex = 15;
      this.cbxBlocking.Text = "blocking";
      this.cbxBlocking.UseVisualStyleBackColor = true;
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // btJobRunnerDispose
      // 
      this.btJobRunnerDispose.Location = new System.Drawing.Point(563, 373);
      this.btJobRunnerDispose.Name = "btJobRunnerDispose";
      this.btJobRunnerDispose.Size = new System.Drawing.Size(94, 42);
      this.btJobRunnerDispose.TabIndex = 16;
      this.btJobRunnerDispose.Text = "JobRunner Dispose";
      this.btJobRunnerDispose.UseVisualStyleBackColor = true;
      this.btJobRunnerDispose.Click += new System.EventHandler(this.btJobRunnerDispose_Click);
      // 
      // btTool
      // 
      this.btTool.Location = new System.Drawing.Point(685, 192);
      this.btTool.Name = "btTool";
      this.btTool.Padding = new System.Windows.Forms.Padding(1, 2, 3, 4);
      this.btTool.Size = new System.Drawing.Size(94, 30);
      this.btTool.TabIndex = 17;
      this.btTool.Text = "TOOL";
      this.btTool.UseVisualStyleBackColor = true;
      this.btTool.Click += new System.EventHandler(this.btTool_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.btTool);
      this.Controls.Add(this.btJobRunnerDispose);
      this.Controls.Add(this.cbxBlocking);
      this.Controls.Add(this.btSendKeyCV);
      this.Controls.Add(this.btJobRunner);
      this.Controls.Add(this.btSendKeyA);
      this.Controls.Add(this.btWriteEnc);
      this.Controls.Add(this.btReadEnc);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txTAS);
      this.Controls.Add(this.btCalcTAS);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txALT);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txCAS);
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
    private System.Windows.Forms.TextBox txCAS;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txALT;
    private System.Windows.Forms.Button btCalcTAS;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txTAS;
    private System.Windows.Forms.Button btReadEnc;
    private System.Windows.Forms.Button btWriteEnc;
    private System.Windows.Forms.Button btSendKeyA;
    private System.Windows.Forms.Button btSendKeyCV;
    private System.Windows.Forms.Button btJobRunner;
    private System.Windows.Forms.CheckBox cbxBlocking;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Button btJobRunnerDispose;
    private System.Windows.Forms.Button btTool;
  }
}

