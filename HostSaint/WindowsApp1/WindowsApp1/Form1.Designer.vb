<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SendMsgboxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseConnectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileManagerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(800, 450)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendMsgboxToolStripMenuItem, Me.ClientToolStripMenuItem, Me.FileManagerToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(147, 70)
        '
        'SendMsgboxToolStripMenuItem
        '
        Me.SendMsgboxToolStripMenuItem.Name = "SendMsgboxToolStripMenuItem"
        Me.SendMsgboxToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.SendMsgboxToolStripMenuItem.Text = "Send Msgbox"
        '
        'ClientToolStripMenuItem
        '
        Me.ClientToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseConnectionToolStripMenuItem})
        Me.ClientToolStripMenuItem.Name = "ClientToolStripMenuItem"
        Me.ClientToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.ClientToolStripMenuItem.Text = "Client"
        '
        'CloseConnectionToolStripMenuItem
        '
        Me.CloseConnectionToolStripMenuItem.Name = "CloseConnectionToolStripMenuItem"
        Me.CloseConnectionToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.CloseConnectionToolStripMenuItem.Text = "Close Connection"
        '
        'FileManagerToolStripMenuItem
        '
        Me.FileManagerToolStripMenuItem.Name = "FileManagerToolStripMenuItem"
        Me.FileManagerToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.FileManagerToolStripMenuItem.Text = "File Manager"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ListView1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListView1 As ListView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SendMsgboxToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClientToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseConnectionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileManagerToolStripMenuItem As ToolStripMenuItem
End Class
