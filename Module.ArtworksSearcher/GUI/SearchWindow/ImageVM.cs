﻿using System.Windows.Media.Imaging;
using Root.MVVM;

namespace Module.ArtworksSearcher.GUI.SearchWindow
{
    public class ImageVM : BaseViewModel
    {
        #region Bindings

        private readonly int _number;
        public int Number => _number;

        private BitmapImage _image;
        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                NotifyPropChanged(nameof(Image));
            }
        }

        #endregion

        public ImageVM(int number)
        {
            _number = number;
        }
    }
}