using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ArtworksSearcher.GUI
{
    public class ImageVM : BaseViewModel
    {
        #region Bindings

        private int _number;
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
