namespace AGSPlugin.Folder
{
	public partial class MainWindow : AGS.Types.EditorContentPanel, System.ComponentModel.INotifyPropertyChanged
	{
		private string _GameDir = string.Empty;
		public string GameDir { get { return _GameDir; } set { CheckSetAndSend(ref _GameDir, value); } }


		private string _FilePathIcon = string.Empty;
		public string FilePathIcon { get { return _FilePathIcon; } set { CheckSetAndSend(ref _FilePathIcon, value); } }


		private bool _Enable = false;
		public bool Enable { get { return _Enable; } set { CheckSetAndSend(ref _Enable, value); } }


		private System.DateTime _DT = System.DateTime.Now;
		public System.DateTime DT { get { return _DT; } set { CheckSetAndSend(ref _DT, value); } }


		private string _Comment = string.Empty;
		public string Comment { get { return _Comment; } set { CheckSetAndSend(ref _Comment, value); } }


		private string DesktopIni = "desktop.ini";

		public MainWindow()
		{
			InitializeComponent();

			System.Windows.Forms.Binding bindE = new System.Windows.Forms.Binding("Checked", this, nameof(Enable), false, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged);
			chkDecoration.DataBindings.Add(bindE);

			System.Windows.Forms.Binding bindC = new System.Windows.Forms.Binding("Text", this, nameof(Comment), false, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged);
			txtComment.DataBindings.Add(bindC);

			System.Windows.Forms.Binding bindF = new System.Windows.Forms.Binding("Text", this, nameof(FilePathIcon), false, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged);
			txtPath.DataBindings.Add(bindF);
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
			Comment = txtComment.Text;

			if ( chkDecoration.Checked )
			{
				HandleDirectory(GameDir, true);
			}
			else
			{
				HandleDirectory(GameDir, false);
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
			string fpi = System.IO.Path.Combine(gameDir, iconFileName);

			if ( !System.IO.File.Exists(fpi) )
			{
				System.IO.File.Copy(iconFilePath, fpi);
			}

			FilePathIcon = fpi;
		}

		#region INotifyPropertyChanged Members

		public new event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;

			if ( handler != null )
			{
				handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		protected bool CheckSetAndSend<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
		{
			if ( System.Collections.Generic.EqualityComparer<T>.Default.Equals(field, value) )
			{
				return false;
			}

			field = value;

			OnPropertyChanged(propertyName);

			return true;
		}

		#endregion INotifyPropertyChanged Members
	}
}
