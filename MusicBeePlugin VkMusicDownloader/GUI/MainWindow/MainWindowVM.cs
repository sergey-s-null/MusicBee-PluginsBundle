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
    class MainWindowVM : BaseViewModel
    {
        #region Bindings

        private AddingVkVM _addingVkVM;
        public AddingVkVM AddingVkVM =>
            _addingVkVM ?? (_addingVkVM = new AddingVkVM());

        private AddingIncomingVM _addingIncomingVM;
        public AddingIncomingVM AddingIncomingVM
            => _addingIncomingVM ?? (_addingIncomingVM = new AddingIncomingVM());


        private object _field;
        public object Field
        {
            get => _field;
            set
            {
                _field = value;
                NotifyPropChanged(nameof(Field));
            }
        }

        #endregion


    }
}
