using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Root.MusicBeeApi.Abstract
{
    public interface IMusicBeeApi : IBaseMusicBeeApi
    {
        IntPtr MB_GetWindowHandle();
        ToolStripItem MB_AddMenuItem(string menuPath, string hotkeyDescription, EventHandler handler);
        void MB_CreateBackgroundTask(ThreadStart taskCallback, Form owner);
        void MB_RegisterCommand(string command, EventHandler handler);
        Font Setting_GetDefaultFont();
        Rectangle MB_GetPanelBounds(PluginPanelDock dock);
        Control MB_AddPanel(Control panel, PluginPanelDock dock);
        void MB_RemovePanel(Control panel);
        void MB_CreateParameterisedBackgroundTask(ParameterizedThreadStart taskCallback, object parameters, Form owner);
        bool MB_SetPanelScrollableArea(Control panel, Size scrollArea, bool alwaysShowScrollBar);
        bool MB_InvokeCommand(Command command, object parameter);
        bool Setting_GetValue(SettingId settingId, out object value);
        bool Library_GetSyncDelta(string[] cachedFiles, DateTime updatedSince, LibraryCategory categories, out string[] newFiles, out string[] updatedFiles, out string[] deletedFiles);
        bool MB_AddTreeNode(string treePath, string name, Bitmap icon, EventHandler openHandler, EventHandler closeHandler);
    }
}
