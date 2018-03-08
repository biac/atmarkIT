<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
  Inherits System.Windows.Forms.Form

  'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Windows フォーム デザイナーで必要です。
  Private components As System.ComponentModel.IContainer

  'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
  'Windows フォーム デザイナーを使用して変更できます。  
  'コード エディターを使って変更しないでください。
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.flowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()

    Me.button1 = New System.Windows.Forms.Button()
    Me.Image1 = New System.Windows.Forms.PictureBox()
    Me.label1 = New System.Windows.Forms.Label()
    Me.BarcodeFormatText = New System.Windows.Forms.TextBox()
    Me.label2 = New System.Windows.Forms.Label()
    Me.TextText = New System.Windows.Forms.TextBox()
    Me.flowLayoutPanel1.SuspendLayout()
    CType(Me.Image1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'FlowLayoutPanel1
    '
    Me.flowLayoutPanel1.Controls.Add(Me.button1)
    Me.flowLayoutPanel1.Controls.Add(Me.label1)
    Me.flowLayoutPanel1.Controls.Add(Me.BarcodeFormatText)
    Me.flowLayoutPanel1.Controls.Add(Me.label2)
    Me.flowLayoutPanel1.Controls.Add(Me.TextText)
    Me.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right
    Me.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown

    Me.flowLayoutPanel1.Location = New System.Drawing.Point(480, 0)
    Me.flowLayoutPanel1.Name = "flowLayoutPanel1"
    Me.flowLayoutPanel1.Size = New System.Drawing.Size(120, 480)
    Me.flowLayoutPanel1.TabIndex = 0

    'button1
    Me.button1.Location = New System.Drawing.Point(0, 10)
    Me.button1.Margin = New System.Windows.Forms.Padding(0, 10, 0, 0)
    Me.button1.Name = "button1"
    Me.button1.Size = New System.Drawing.Size(120, 23)
    Me.button1.TabIndex = 0
    Me.button1.Text = "Open Image File"
    Me.button1.UseVisualStyleBackColor = True
    AddHandler Me.button1.Click, New System.EventHandler(AddressOf Me.Button_Click)


    ' Image1
    Me.Image1.BackColor = System.Drawing.Color.LightGray
    Me.Image1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Image1.Location = New System.Drawing.Point(0, 0)
    Me.Image1.Name = "Image1"
    Me.Image1.Size = New System.Drawing.Size(480, 480)
    Me.Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
    Me.Image1.TabIndex = 1
    Me.Image1.TabStop = False

    ' label1
    Me.label1.AutoSize = True
    Me.label1.Location = New System.Drawing.Point(0, 43)
    Me.label1.Margin = New System.Windows.Forms.Padding(0, 10, 0, 0)
    Me.label1.Name = "label1"
    Me.label1.Size = New System.Drawing.Size(83, 12)
    Me.label1.TabIndex = 1
    Me.label1.Text = "BarcodeFormat"

    ' BarcodeFormatText
    Me.BarcodeFormatText.BackColor = System.Drawing.Color.Azure
    Me.BarcodeFormatText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.BarcodeFormatText.Location = New System.Drawing.Point(0, 55)
    Me.BarcodeFormatText.Margin = New System.Windows.Forms.Padding(0)
    Me.BarcodeFormatText.Multiline = True
    Me.BarcodeFormatText.Name = "BarcodeFormatText"
    Me.BarcodeFormatText.ReadOnly = True
    Me.BarcodeFormatText.Size = New System.Drawing.Size(120, 19)
    Me.BarcodeFormatText.TabIndex = 2

    ' label2
    Me.label2.AutoSize = True
    Me.label2.Location = New System.Drawing.Point(0, 84)
    Me.label2.Margin = New System.Windows.Forms.Padding(0, 10, 0, 0)
    Me.label2.Name = "label2"
    Me.label2.Size = New System.Drawing.Size(28, 12)
    Me.label2.TabIndex = 3
    Me.label2.Text = "Text"

    ' TextText
    Me.TextText.BackColor = System.Drawing.Color.Azure
    Me.TextText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.TextText.Location = New System.Drawing.Point(0, 96)
    Me.TextText.Margin = New System.Windows.Forms.Padding(0)
    Me.TextText.Multiline = True
    Me.TextText.Name = "TextText"
    Me.TextText.ReadOnly = True
    Me.TextText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.TextText.Size = New System.Drawing.Size(120, 80)
    Me.TextText.TabIndex = 4

    '
    'Form1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(600, 480)
    Me.Controls.Add(Me.Image1)
    Me.Controls.Add(Me.flowLayoutPanel1)
    Me.Name = "Form1"
    Me.Text = "Form1"
    Me.flowLayoutPanel1.ResumeLayout(False)
    Me.flowLayoutPanel1.PerformLayout()
    CType(Me.Image1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub


  Friend WithEvents flowLayoutPanel1 As FlowLayoutPanel
  Friend WithEvents button1 As Button
  Friend WithEvents Image1 As PictureBox
  Friend WithEvents label1 As Label
  Friend WithEvents BarcodeFormatText As TextBox
  Friend WithEvents label2 As Label
  Friend WithEvents TextText As TextBox

End Class
