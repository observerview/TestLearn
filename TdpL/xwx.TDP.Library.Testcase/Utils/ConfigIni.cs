using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Library.Common.Utils
{
    public class ConfigIni
    {
        public string inipath;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.inipath);
        }

        /// <summary>
        /// 读出INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        public string IniReadValue(string Section, string Key, string defaultVal)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, defaultVal, temp, 500, this.inipath);
            return temp.ToString();
        }

        /// <summary>
        /// 验证文件是否存在
        /// </summary>
        /// <returns>布尔值</returns>
        public bool ExistINIFile()
        {
            return File.Exists(inipath);
        }

        public ConfigIni(string INIPath)
        {
            inipath = INIPath;
        }

        /// <summary>
        /// 传入要写入Log文件的内容
        /// </summary>
        /// <param name="AboutAPPLoog"></param>
        public void WriteAccessLog(string AboutAPPLoog)
        {
            IniWriteValue(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString(string.Format("yyyy-MM-dd HH:mm:ss/fffff")), AboutAPPLoog);
        }
    }
}
