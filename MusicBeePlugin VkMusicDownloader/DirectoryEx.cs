using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBeePlugin
{
    static class DirectoryEx
    {
        public static bool TryCreateDirectory(string dirPath)
        {
            if (Directory.Exists(dirPath))
                return true;

            try
            {
                Directory.CreateDirectory(dirPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
