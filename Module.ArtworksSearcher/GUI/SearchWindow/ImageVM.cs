using System.Windows.Media.Imaging;
using PropertyChanged;

namespace Module.ArtworksSearcher.GUI.SearchWindow
{
    [AddINotifyPropertyChangedInterface]
    public class ImageVM
    {
        public int Number { get; }
        public BitmapImage Image { get; set; }
        
        public ImageVM(int number)
        {
            Number = number;
        }
    }
}
