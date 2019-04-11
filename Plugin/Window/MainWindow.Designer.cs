namespace AGS.Plugin.Folder
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if ( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.chkDecoration = new System.Windows.Forms.CheckBox();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.lblPath = new System.Windows.Forms.Label();
			this.btnLoad = new System.Windows.Forms.Button();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.lblComment = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// chkDecoration
			// 
			this.chkDecoration.AutoSize = true;
			this.chkDecoration.Location = new System.Drawing.Point(49, 153);
			this.chkDecoration.Name = "chkDecoration";
			this.chkDecoration.Size = new System.Drawing.Size(98, 17);
			this.chkDecoration.TabIndex = 1;
			this.chkDecoration.Text = "Use decoration";
			this.chkDecoration.UseVisualStyleBackColor = true;
			this.chkDecoration.CheckedChanged += new System.EventHandler(this.Decoration_CheckedChanged);
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(49, 51);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(324, 20);
			this.txtPath.TabIndex = 2;
			this.txtPath.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Path_PreviewKeyDown);
			// 
			// lblPath
			// 
			this.lblPath.AutoSize = true;
			this.lblPath.Location = new System.Drawing.Point(46, 35);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(31, 13);
			this.lblPath.TabIndex = 3;
			this.lblPath.Text = "Icon:";
			// 
			// btnLoad
			// 
			this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoad.Location = new System.Drawing.Point(393, 49);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(75, 23);
			this.btnLoad.TabIndex = 4;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.Load_Click);
			// 
			// txtComment
			// 
			this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtComment.Location = new System.Drawing.Point(49, 103);
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(324, 20);
			this.txtComment.TabIndex = 2;
			this.txtComment.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Comment_PreviewKeyDown);
			// 
			// lblComment
			// 
			this.lblComment.AutoSize = true;
			this.lblComment.Location = new System.Drawing.Point(46, 87);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(54, 13);
			this.lblComment.TabIndex = 3;
			this.lblComment.Text = "Comment:";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.lblComment);
			this.Controls.Add(this.lblPath);
			this.Controls.Add(this.txtComment);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.chkDecoration);
			this.Name = "MainWindow";
			this.Size = new System.Drawing.Size(503, 442);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chkDecoration;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Label lblPath;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Label lblComment;
	}
}
