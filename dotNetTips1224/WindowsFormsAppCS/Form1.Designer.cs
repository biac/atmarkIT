namespace WindowsFormsAppCS
{
  partial class Form1
  {
    /// <summary>
    /// 必要なデザイナー変数です。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 使用中のリソースをすべてクリーンアップします。
    /// </summary>
    /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows フォーム デザイナーで生成されたコード

    /// <summary>
    /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
    /// コード エディターで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.button1 = new System.Windows.Forms.Button();
      this.Image1 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.BarcodeFormatText = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.TextText = new System.Windows.Forms.TextBox();
      this.flowLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.Image1)).BeginInit();
      this.SuspendLayout();
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this.button1);
      this.flowLayoutPanel1.Controls.Add(this.label1);
      this.flowLayoutPanel1.Controls.Add(this.BarcodeFormatText);
      this.flowLayoutPanel1.Controls.Add(this.label2);
      this.flowLayoutPanel1.Controls.Add(this.TextText);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
      this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(480, 0);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(120, 480);
      this.flowLayoutPanel1.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(0, 10);
      this.button1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(120, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Open Image File";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.Button_Click);
      // 
      // Image1
      // 
      this.Image1.BackColor = System.Drawing.Color.LightGray;
      this.Image1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Image1.Location = new System.Drawing.Point(0, 0);
      this.Image1.Name = "Image1";
      this.Image1.Size = new System.Drawing.Size(480, 480);
      this.Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.Image1.TabIndex = 1;
      this.Image1.TabStop = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(0, 43);
      this.label1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(83, 12);
      this.label1.TabIndex = 1;
      this.label1.Text = "BarcodeFormat";
      // 
      // BarcodeFormatText
      // 
      this.BarcodeFormatText.BackColor = System.Drawing.Color.Azure;
      this.BarcodeFormatText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.BarcodeFormatText.Location = new System.Drawing.Point(0, 55);
      this.BarcodeFormatText.Margin = new System.Windows.Forms.Padding(0);
      this.BarcodeFormatText.Multiline = true;
      this.BarcodeFormatText.Name = "BarcodeFormatText";
      this.BarcodeFormatText.ReadOnly = true;
      this.BarcodeFormatText.Size = new System.Drawing.Size(120, 19);
      this.BarcodeFormatText.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(0, 84);
      this.label2.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(28, 12);
      this.label2.TabIndex = 3;
      this.label2.Text = "Text";
      // 
      // TextText
      // 
      this.TextText.BackColor = System.Drawing.Color.Azure;
      this.TextText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.TextText.Location = new System.Drawing.Point(0, 96);
      this.TextText.Margin = new System.Windows.Forms.Padding(0);
      this.TextText.Multiline = true;
      this.TextText.Name = "TextText";
      this.TextText.ReadOnly = true;
      this.TextText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TextText.Size = new System.Drawing.Size(120, 80);
      this.TextText.TabIndex = 4;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(600, 480);
      this.Controls.Add(this.Image1);
      this.Controls.Add(this.flowLayoutPanel1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.Image1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.PictureBox Image1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox BarcodeFormatText;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox TextText;
  }
}

