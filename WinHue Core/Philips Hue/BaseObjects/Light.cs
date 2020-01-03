using System;
using System.Collections.Generic;
using System.Text;
using WinHue_Core.MVVM;

namespace WinHue_Core.Philips_Hue.LightObject
{
    public class Light : ViewModelBase, IHueObject
    {
        private string _name;

        public string Name { get => _name; set => SetProperty(ref _name,value); }
    }
}
