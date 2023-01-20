namespace Module.VkAudioDownloader.TagReplacer
{
    sealed class MBTagReplacer
    {
        public enum Tag
        {
            Index1 = 0, Index2 = 1, Artist = 2, Title = 3
        }

        public static readonly string[] AvailableTags = new string[]
        {
            "i1", "i2", "artist", "title"
        };
        public static char OpenBracket => TagReplacer.OpenBracket;
        public static char CloseBracket => TagReplacer.CloseBracket;

        private TagReplacer _replacer = new TagReplacer();

        public MBTagReplacer()
        {
            foreach (string tag in AvailableTags)
                _replacer.SetTagReplace(tag, "");
        }
        public void SetValues(string i1, string i2, string artist, string title)
        {
            SetReplaceValue(Tag.Index1, i1);
            SetReplaceValue(Tag.Index2, i2);
            SetReplaceValue(Tag.Artist, artist);
            SetReplaceValue(Tag.Title, title);
        }

        public void SetReplaceValue(Tag tag, string value)
        {
            _replacer.SetTagReplace(AvailableTags[(int)tag], value);
        }

        public string Prepare(string template)
        {
            return _replacer.Prepare(template);
        }

    }
}
