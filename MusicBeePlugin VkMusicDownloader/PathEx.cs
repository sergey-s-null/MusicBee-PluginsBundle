using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBeePlugin
{
    static class PathEx
    {
        public static string RemoveInvalidDirChars(string dirPath)
        {
            return string.Concat(dirPath.Split(Path.GetInvalidPathChars()));
        }

        public static string RemoveInvalidFileNameChars(string fileName)
        {
            return string.Concat(fileName.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
