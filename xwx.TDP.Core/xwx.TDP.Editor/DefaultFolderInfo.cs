using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xwx.TDP.Editor.Properties;
using System.Windows.Forms;

namespace xwx.TDP.Editor
{
    internal class DefaultFolderInfo
    {
        public static readonly string Applications_Folder = Application.StartupPath + "\\" + Settings.Default.Internal_ApplicationFolder;

        public static readonly string Config_Folder = Application.StartupPath + "\\config";

        public static readonly string Config_DockConfigFile_Folder = Application.StartupPath + "\\config\\layout";

        public static readonly string Config_DockConfigFile_SuperLite = Application.StartupPath + "\\config\\layout\\superlite.xml";

        public static readonly string Config_DockConfigFile_Lite = Application.StartupPath + "\\config\\layout\\lite.xml";

        public static readonly string Config_DockConfigFile_Standard = Application.StartupPath + "\\config\\layout\\standard.xml";

        public static readonly string Config_DockConfigFile_Premium = Application.StartupPath + "\\config\\layout\\premium.xml";

        public static readonly string TestResult_Folder = Application.StartupPath + "\\test results";

        public static readonly string SequenceLibrary_Folder = Application.StartupPath + "\\sequences";
    }
}
