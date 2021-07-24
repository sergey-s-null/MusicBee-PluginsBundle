using MusicBeePlugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VkMusicDownloader.Ex;
using VkNet;
using VkNet.Model.Attachments;

#pragma warning disable CS4014

namespace VkMusicDownloader.GUI
{
    public class MainWindowVM : BaseViewModel
    {
        #region Bindings

        private AddingVkVM _addingVkVm;
        public AddingVkVM AddingVkVM => _addingVkVm;

        private AddingIncomingVM _addingIncomingVm;
        public AddingIncomingVM AddingIncomingVM => _addingIncomingVm;
        
        #endregion

        public MainWindowVM(AddingVkVM addingVkVm,
            AddingIncomingVM addingIncomingVm)
        {
            _addingVkVm = addingVkVm;
            _addingIncomingVm = addingIncomingVm;
        }
    }
}
