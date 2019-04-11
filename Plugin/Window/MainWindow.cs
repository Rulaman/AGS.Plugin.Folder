namespace AGS.Plugin.Folder
{
	public partial class MainWindow : Types.EditorContentPanel
	{
		public string GameDir = string.Empty;
		public string FilePathIcon = string.Empty;
		public bool Enable = false;


		private string DesktopIni = "desktop.ini";

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Decoration_CheckedChanged(object sender, System.EventArgs e)
		{
			ApplySettings();
		}

		private void Path_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
		{
			switch ( e.KeyCode )
			{
				case System.Windows.Forms.Keys.Enter:
					ApplySettings();
					break;
			};
		}

		private void Comment_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
		{
			switch ( e.KeyCode )
			{
				case System.Windows.Forms.Keys.Enter:
					ApplySettings();
					break;
			};
		}

		private void Load_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
			ofd.Filter = "Icons (*.ico)|*.ico|Png (*.png)|*.png";

			if ( ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK )
			{
				txtPath.Text = ofd.FileName;

				ApplySettings();
			}
		}

		private void ApplySettings()
		{
			string iconName = System.IO.Path.GetFileName(txtPath.Text);

			HandleDesktopIni(iconName, GameDir, txtComment.Text);
			CopyIcon(GameDir, txtPath.Text, iconName);

			if ( chkDecoration.Checked )
			{
				HandleDirectory(GameDir, true);
			}
			else
			{
				HandleDirectory(GameDir, true);
			}
		}

		private void HandleDirectory(string gameDir, bool showSymbol)
		{
			if ( showSymbol )
			{
				/* Set the suitable attributes to the directory. */
				System.IO.FileAttributes fileAttribDir = System.IO.File.GetAttributes(gameDir);
				fileAttribDir |= System.IO.FileAttributes.ReadOnly;
				fileAttribDir &= ~System.IO.FileAttributes.Archive;
				System.IO.File.SetAttributes(gameDir, fileAttribDir);
			}
			else
			{
				/* Set the suitable attributes to the directory. */
				System.IO.FileAttributes fileAttribDir = System.IO.File.GetAttributes(gameDir);
				fileAttribDir &= ~System.IO.FileAttributes.ReadOnly;
				fileAttribDir |= System.IO.FileAttributes.Archive;
				System.IO.File.SetAttributes(gameDir, fileAttribDir);
			}
		}

		private void HandleDesktopIni(string iconFileName, string gameDir, string comment)
		{
			string desktopFileName = System.IO.Path.Combine(gameDir, DesktopIni);

			if ( System.IO.File.Exists(desktopFileName) )
			{
				System.IO.FileAttributes fileAttrib = System.IO.File.GetAttributes(desktopFileName);
				fileAttrib &= ~System.IO.FileAttributes.Hidden;
				System.IO.File.Delete(desktopFileName);
			}

			using ( System.IO.StreamWriter tw = new System.IO.StreamWriter(desktopFileName, false, System.Text.Encoding.GetEncoding(1250)) )
			{
				tw.WriteLine("[.ShellClassInfo]");
				tw.WriteLine(@"IconFile=" + iconFileName); // System.IO.Path.Combine(RepoDir, FlagDir, flagFileName));
				tw.WriteLine("IconIndex=0");
				tw.WriteLine("InfoTip=" + comment);
			}

			System.IO.FileAttributes fileAttrib2 = System.IO.File.GetAttributes(desktopFileName);
			fileAttrib2 |= System.IO.FileAttributes.Hidden;
			System.IO.File.SetAttributes(desktopFileName, fileAttrib2);
		}

		private void CopyIcon(string gameDir, string iconFilePath, string iconFileName)
		{
			if ( !System.IO.File.Exists(System.IO.Path.Combine(gameDir, iconFileName)) )
			{
				System.IO.File.Copy(iconFilePath, System.IO.Path.Combine(gameDir, iconFileName));
			}
		}
	}
}
