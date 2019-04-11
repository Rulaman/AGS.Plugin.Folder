using System;
using System.Collections.Generic;
using AGS.Types;


namespace AGSPlugin.Folder
{
	[RequiredAGSVersion("3.5.0.0")]
	public class PluginLoader : IAGSEditorPlugin
	{
		public PluginLoader(IAGSEditor editor)
		{
			editor.AddComponent(new Component(editor));
		}

		public void Dispose()
		{
		}
	}

	public static class Common
	{

		static Common()
		{

		}
	}


	public class Component : IEditorComponent
	{
		public string DateTimeFormatString = "yyyy.MM.dd HH:mm:ss,fff";
		public System.Globalization.NumberFormatInfo Nfi;

		private const string COMPONENT_ID = "PluginFolder";
		private const string COMPONENT_MENU_COMMAND = "PluginFolderMenuCommand";
		private const string CONTROL_ID_ROOT_NODE = "PluginFolderRoot";
		private const string CONTROL_ROOT_NAME = "Folder settings";
		private const string ICON_NAME = "PluginFolderIcon";
		private IAGSEditor LocalEditor;
		private Dictionary<int, ContentDocument> PaneDictionary = new Dictionary<int, ContentDocument>();
		private readonly MenuCommand MenuCommand;
		private MainWindow Window = null;
		private ContentDocument WindowPanel = null;

		public Component(IAGSEditor editor)
		{
			LocalEditor = editor;
			LocalEditor.GUIController.RegisterIcon(ICON_NAME, Properties.Resources.AGSTheme);
			LocalEditor.GUIController.ProjectTree.AddTreeRoot(this, CONTROL_ID_ROOT_NODE, COMPONENT_ID, ICON_NAME);
			LocalEditor.GUIController.ProjectTree.BeforeShowContextMenu += new BeforeShowContextMenuHandler(ProjectTree_BeforeShowContextMenu);
			this.MenuCommand = LocalEditor.GUIController.CreateMenuCommand(this, COMPONENT_MENU_COMMAND, CONTROL_ROOT_NAME);

			Init();
		}

		private void Init()
		{
			try
			{
				Window = Window ?? new MainWindow();
				WindowPanel = new ContentDocument(Window, "Folder settings", this, ICON_NAME);

				Nfi = new System.Globalization.NumberFormatInfo();
				Nfi.NumberDecimalDigits = 2;
				Nfi.NumberDecimalSeparator = ".";
			}
			catch { }
		}

		void ProjectTree_BeforeShowContextMenu(BeforeShowContextMenuEventArgs evArgs)
		{

		}

		string IEditorComponent.ComponentID
		{
			get { return COMPONENT_ID; }
		}
		IList<MenuCommand> IEditorComponent.GetContextMenu(string controlID)
		{
			/* rechte Maustaste auf dem Eintrag */
			MenuCommand mc = new MenuCommand("Settings", "Folder Settings");
			return new List<MenuCommand> { mc };
		}
		void IEditorComponent.CommandClick(string controlID)
		{
			switch ( controlID )
			{
				case CONTROL_ID_ROOT_NODE: // double click on Root node
				case "Settings": // click on context menu entry
					{
						LocalEditor.GUIController.AddOrShowPane(WindowPanel);
					}
					break;
			};
		}

		void IEditorComponent.PropertyChanged(string propertyName, object oldValue)
		{
		}

		void IEditorComponent.BeforeSaveGame()
		{
		}

		void IEditorComponent.RefreshDataFromGame()
		{
			Window.GameDir = LocalEditor.CurrentGame.DirectoryPath;
		}

		void IEditorComponent.GameSettingsChanged()
		{
		}

		void IEditorComponent.ToXml(System.Xml.XmlTextWriter writer)
		{
			writer.WriteStartElement("FolderIcon");

			writer.WriteStartElement("Enable");
			writer.WriteString(Window.Enable.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("Comment");
			writer.WriteString(Window.Comment);
			writer.WriteEndElement();

			writer.WriteStartElement("Date");
			writer.WriteString(DateTime.Now.ToString(DateTimeFormatString));
			writer.WriteEndElement();

			writer.WriteStartElement("IconPath");
			writer.WriteString(Window.FilePathIcon);
			writer.WriteEndElement();


			writer.WriteEndElement();
		}

		void IEditorComponent.FromXml(System.Xml.XmlNode node)
		{
			if ( null != node && node.InnerText != string.Empty)
			{
				try { if ( bool.TryParse(node.SelectSingleNode("FolderIcon/Enable").InnerText, out bool b) ) { Window.Enable = b; } }
				catch { }

				try { Window.DT = DateTime.ParseExact(node.SelectSingleNode("FolderIcon/Date").InnerText, DateTimeFormatString, Nfi, System.Globalization.DateTimeStyles.AssumeLocal); }
				catch { }

				try { Window.Comment = node.SelectSingleNode("FolderIcon/Comment").InnerText; }
				catch { }

				try { Window.FilePathIcon = node.SelectSingleNode("FolderIcon/IconPath").InnerText; }
				catch { }
			}
		}

		void IEditorComponent.EditorShutdown() { }
	}
}
