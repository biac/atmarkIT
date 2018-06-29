namespace dotNetTips1238WinFormCS
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.UrlTextBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.MessageTextBox = new System.Windows.Forms.TextBox();
      this.WebView1 = new Microsoft.Toolkit.Win32.UI.Controls.WinForms.WebView();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.WebView1)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.AutoSize = true;
      this.panel1.Controls.Add(this.UrlTextBox);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.button1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(800, 29);
      this.panel1.TabIndex = 0;
      // 
      // UrlTextBox
      // 
      this.UrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.UrlTextBox.Location = new System.Drawing.Point(38, 5);
      this.UrlTextBox.Name = "UrlTextBox";
      this.UrlTextBox.Size = new System.Drawing.Size(681, 19);
      this.UrlTextBox.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(5, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(27, 12);
      this.label1.TabIndex = 1;
      this.label1.Text = "URL";
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.Location = new System.Drawing.Point(722, 3);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Go";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.Button1_Click);
      // 
      // MessageTextBox
      // 
      this.MessageTextBox.AcceptsReturn = true;
      this.MessageTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.MessageTextBox.Location = new System.Drawing.Point(0, 385);
      this.MessageTextBox.Multiline = true;
      this.MessageTextBox.Name = "MessageTextBox";
      this.MessageTextBox.ReadOnly = true;
      this.MessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.MessageTextBox.Size = new System.Drawing.Size(800, 65);
      this.MessageTextBox.TabIndex = 1;
      // 
      // WebView1
      // 
      this.WebView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.WebView1.Location = new System.Drawing.Point(0, 29);
      this.WebView1.MinimumSize = new System.Drawing.Size(20, 20);
      this.WebView1.Name = "WebView1";
      this.WebView1.Size = new System.Drawing.Size(800, 356);
      this.WebView1.Source = new System.Uri("http://bluewatersoft.jp/", System.UriKind.Absolute);
      this.WebView1.TabIndex = 2;
      this.WebView1.NavigationCompleted += new System.EventHandler<Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationCompletedEventArgs>(this.WebView1_NavigationCompleted);
      this.WebView1.NavigationStarting += new System.EventHandler<Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationStartingEventArgs>(this.WebView1_NavigationStarting);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.WebView1);
      this.Controls.Add(this.MessageTextBox);
      this.Controls.Add(this.panel1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.WebView1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox UrlTextBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox MessageTextBox;
    private Microsoft.Toolkit.Win32.UI.Controls.WinForms.WebView WebView1;
  }
}

