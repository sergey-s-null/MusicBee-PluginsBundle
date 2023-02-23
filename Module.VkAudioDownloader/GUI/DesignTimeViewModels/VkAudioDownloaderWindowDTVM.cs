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

    public ICommand Download { get; } =
        new RelayCommand(_ => { });

    public VkAudioDownloaderWindowDTVM()
    {
        Audios = new IVkAudioVM[]
        {
            new VkAudioDTVM(),
            new VkAudioDTVM(
                2346346,
                "AC/DC",
                "Shoot To Thrill",
                new VkAudioUrlDTVM("www.example.com", false),
                false,
                null
            ),
            new VkAudioDTVM(
                48219095,
                "Gulag",
                "Rosa Branca",
                new VkAudioUrlDTVM("www.example.com", false),
                false,
                null
            ),
            new VkAudioDTVM(
                5679,
                "Hundred Little Reasons",
                "Someday",
                new VkAudioUrlDTVM("www.example.com", false),
                false,
                null
            ) { IsSelected = true },
            new VkAudioDTVM(
                95789,
                "Алиса",
                "Моё поколение",
                null,
                false,
                new[] { "Url is null." }
            ) { IsSelected = true },
            new VkAudioDTVM(
                348254,
                "Goose house",
                "Oto no Naru Hou e",
                new VkAudioUrlDTVM("www.example.com", false),
                false,
                null
            ),
            new VkAudioDTVM(
                3456,
                "Stone Sour",
                "Unfinished",
                new VkAudioUrlDTVM("www.example.com", true),
                false,
                null
            ),
            new VkAudioDTVM(
                3456878,
                "Rei Kagaya",
                "4 April",
                new VkAudioUrlDTVM("www.example.com", true),
                false,
                null
            ),
            new VkAudioDTVM(
                234576435,
                "Ryuichi Sakamoto",
                "Merry Christmas Mr. Lawrence",
                null,
                true,
                new[] { "Audio already in Incoming", "Url is null." }
            ),
            new VkAudioDTVM(
                34574353,
                "CunninLynguists",
                "War",
                new VkAudioUrlDTVM("www.example.com", true),
                true,
                new[] { "Audio already in Incoming" }
            ),
            new VkAudioDTVM(
                84563246,
                "Anti-Flag",
                "Broken Bones",
                new VkAudioUrlDTVM("www.example.com", true),
                true,
                new[] { "Audio already in Incoming" }
            ),
        };
    }
}