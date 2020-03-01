using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WinHue_Core.MVVM;
using WinHue_Core.Philips_Hue;

namespace WinHue_Core.MainForm
{
    public partial class MainFormViewModel : ViewModelBase
    {
        private ObservableCollection<Bridge> _listBridges;

        public ObservableCollection<Bridge> ListBridges { get => _listBridges; set => SetProperty(ref _listBridges, value); }
    }
}
