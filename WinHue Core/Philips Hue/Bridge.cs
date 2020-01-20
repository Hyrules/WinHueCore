using System;
using System.Collections.Generic;
using System.Text;
using WinHue_Core.MVVM;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Net;

namespace WinHue_Core.Philips_Hue
{

    public class Bridge : ViewModelBase
    {
        private string _name;
        private string _swversion;
        private string _apiversion;
        private string _mac;
        private string _bridgeId;
        private bool _factoryNew;
        private string _replaceBridgeId;
        private string _modelId;
        private string _starterKitId;
        private string _datastoreversion;
        private IPAddress _ipAddress;

        public Bridge()
        {

        }

        [DataMember(Name="name")]
        public string Name { get => _name; set => SetProperty(ref _name,value); }
        [DataMember(Name="swversion")]
        public string Swversion { get => _swversion; set => SetProperty(ref _swversion,value); }
        [DataMember(Name="apiversion")]
        public string ApiVersion { get => _apiversion; set => SetProperty(ref _apiversion,value); }
        [DataMember(Name="mac")]
        public string Mac { get => _mac; set => SetProperty(ref _mac,value); }
        [DataMember(Name="bridgeid")]
        public string BridgeId { get => _bridgeId; set => SetProperty(ref _bridgeId,value); }
        [DataMember(Name="factorynew")]
        public bool FactoryNew { get => _factoryNew; set => SetProperty(ref _factoryNew,value); }
        [DataMember(Name = "replacebridgeid")]
        public string ReplaceBridgeId { get => _replaceBridgeId; set => SetProperty(ref _replaceBridgeId,value); }
        [DataMember(Name = "modelid")]
        public string ModelId { get => _modelId; set => SetProperty(ref _modelId,value); }
        [DataMember(Name = "starterkitid")]
        public string StarterKitId { get => _starterKitId; set => SetProperty(ref _starterKitId,value); }
        [DataMember(Name = "datastoreversion")]
        public string DatastoreVersion { get => _datastoreversion; set => SetProperty(ref _datastoreversion,value); }
    }
}
