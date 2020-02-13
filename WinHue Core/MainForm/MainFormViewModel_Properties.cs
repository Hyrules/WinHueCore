using System;
using System.Collections.Generic;
using System.Text;
using WinHue_Core.MVVM;
using WinHue_Core.Philips_Hue;

namespace WinHue_Core.MainForm
{
    public partial class MainFormViewModel : ViewModelBase
    {
        private List<Bridge> _listBridges;

        public MainFormViewModel()
        {
            _listBridges = new List<Bridge>();
        }
    }
}
