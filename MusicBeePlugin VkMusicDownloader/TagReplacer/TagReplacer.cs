using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkMusicDownloader.Ex;

namespace VkMusicDownloader
{
    class TagReplacer
    {
        public static readonly char OpenBracket = '<';
        public static readonly char CloseBracket = '>';

        private Dictionary<string, string> _replaces = new Dictionary<string, string>();

        public void SetTagReplace(string tagName, string replaceValue)
        {
            if (_replaces.ContainsKey(tagName))
                _replaces.Remove(tagName);
            _replaces.Add(tagName, replaceValue);
        }

        public string Prepare(string template)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < template.Length; ++i)
            {
                foreach (KeyValuePair<string, string> pair in _replaces)
                {
                    if (ContainsTagAt(template, pair.Key, i))
                    {
                        builder.Append(pair.Value);
                        i += pair.Key.Length + 2;
                        break;
                    }
                }

                if (i < template.Length)
                    builder.Append(template[i]);
            }
            return builder.ToString();
        }

        private bool ContainsTagAt(string str, string tagName, int pos)
        {
            if (str.Length - pos < tagName.Length + 2)
                return false;

            if (str[pos] != OpenBracket)
                return false;
            if (str[pos + tagName.Length + 1] != CloseBracket)
                return false;
            return str.ContainsAt(tagName, pos + 1);
        }
    }
}
