using System;
using System.Windows;
using System.Windows.Input;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Module.Vk.Settings;
using Root.MVVM;
using VkNet.Abstractions;
using VkNet.Exception;

namespace Module.AudioSourcesComparer.GUI.ViewModels
{
    public sealed class VkAudioVM : IVkAudioVM
    {
        public event EventHandler<EventArgs>? DeleteRequested;

        public long Id { get; }
        public string Artist { get; }
        public string Title { get; }

        public ICommand SetArtistAndTitleToClipboardCmd =>
            _setArtistAndTitleToClipboardCmd ??= new RelayCommand(_ => CopyArtistAndTitle());

        private ICommand? _setArtistAndTitleToClipboardCmd;

        public ICommand DeleteCmd => _deleteCmd ??= new RelayCommand(_ => Delete());
        private ICommand? _deleteCmd;

        private readonly IVkApi _vkApi;
        private readonly IVkSettings _vkSettings;

        public VkAudioVM(
            long id,
            string artist,
            string title,
            // DI
            IVkApi vkApi,
            IVkSettings vkSettings)
        {
            Id = id;
            Artist = artist;
            Title = title;

            _vkApi = vkApi;
            _vkSettings = vkSettings;
        }

        private void CopyArtistAndTitle()
        {
            Clipboard.SetText($"{Artist} - {Title}");
        }

        private void Delete()
        {
            try
            {
                var deleted = _vkApi.Audio.Delete(Id, _vkSettings.UserId);
                if (!deleted)
                {
                    MessageBox.Show(
                        "Audio was NOT deleted. And there is NO any error.",
                        "Kavo?",
                        MessageBoxButton.OK
                    );
                    return;
                }

                DeleteRequested?.Invoke(this, EventArgs.Empty);
            }
            catch (VkApiException e)
            {
                MessageBox.Show(
                    "Error on delete audio from vk.\n\n" + e,
                    "Error!",
                    MessageBoxButton.OK
                );
            }
        }
    }
}