using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace TestManager.Utility.Misc
{
    public class FolderCompresserZip
    {
        public static bool Compress(string folderPath, string fileName, out string errMsg)
        {
            bool result = true;
            errMsg = string.Empty;
            try
            {
                FastZip val = new FastZip();
                val.CreateZip(fileName, folderPath, true, string.Empty);
            }
            catch (Exception ex)
            {
                result = false;
                errMsg = ex.Message;
            }

            return result;
        }

        public static bool DeCompress(string fileName, string folderPath, out string errMsg)
        {
            bool result = true;
            errMsg = string.Empty;
            try
            {
                FastZip val = new FastZip();
                val.ExtractZip(fileName, folderPath, string.Empty);
            }
            catch (Exception ex)
            {
                result = false;
                errMsg = ex.Message;
            }

            return result;
        }
    }
}
