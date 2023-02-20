using System.Windows.Input;
using Module.Mvvm.Extension;
using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.DesignTimeViewModels;

public sealed class VkAudioDownloaderWindowDTVM : IVkAudioDownloaderWindowVM
{
    public bool IsDownloading => false;

    public IList<IVkAudioVM> Audios { get; }

    public ICommand Refresh { get; } =
        new RelayCommand(_ => { });

    public ICommand ApplyCheckStateToSelected { get; } =
        new RelayCommand(_ => { });

    public ICommand Download { get; } =
        new RelayCommand(_ => { });

    public VkAudioDownloaderWindowDTVM()
    {
        Audios = new IVkAudioVM[]
        {
            new VkAudioDTVM(),
            new VkAudioDTVM(2346346, "AC/DC", "Shoot To Thrill", "www.example.com", false),
            new VkAudioDTVM(48219095, "Gulag", "Rosa Branca", "www.example.com", true),
            new VkAudioDTVM(5679, "Hundred Little Reasons", "Someday", "www.example.com", true) { IsSelected = true },
            new VkAudioDTVM(95789, "Алиса", "Моё поколение", "www.example.com", true) { IsSelected = true },
            new VkAudioDTVM(348254, "Goose house", "Oto no Naru Hou e", "www.example.com", true),
            new VkAudioDTVM(3456, "Stone Sour", "Unfinished", "www.example.com", true),
            new VkAudioDTVM(3456878, "Rei Kagaya", "4 April", "www.example.com", true),
            new VkAudioDTVM(234576435, "Ryuichi Sakamoto", "Merry Christmas Mr. Lawrence", "www.example.com", true),
            new VkAudioDTVM(34574353, "CunninLynguists", "War", "www.example.com", true),
            new VkAudioDTVM(84563246, "Anti-Flag", "Broken Bones", "www.example.com", true),
        };
    }
}