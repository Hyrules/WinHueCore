using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Media;
using WinHue_Core.MVVM;
using WinHue_Core.Philips_Hue.Exceptions;

namespace WinHue_Core.Philips_Hue.BaseObjects
{
    public class Light : ViewModelBase, IHueObject
    {
        private string id;
        private ImageSource image;
        private State state;
        private Swupdate swupdate;
        private string type;
        private string name;
        private string modelid;
        private string productname;
        private Capabilities capabilities;
        private LightConfig config;
        private string uniqueid;
        private readonly Bridge bridge;
        private string swversion;

        public Light(ref Bridge bridge)
        {
            this.bridge = bridge;
        }

        [JsonIgnore]
        public string Id { get => id; set => SetProperty(ref id, value); }

        [JsonIgnore]
        public ImageSource Image { get => image; set => SetProperty(ref image, value); }

        [JsonPropertyName("state")]
        public State State { get => state; private set => SetProperty(ref state, value); }

        [JsonPropertyName("swupdate")]
        public Swupdate Swupdate { get => swupdate; set => SetProperty(ref swupdate, value); }

        [JsonPropertyName("type")]
        public string Type { get => type; set => SetProperty(ref type, value); }

        [JsonPropertyName("name")]
        public string Name { get => name; set => SetProperty(ref name, value); }

        [JsonPropertyName("modelid")]
        public string Modelid { get => modelid; set => SetProperty(ref modelid, value); }

        [JsonPropertyName("manufacturername")]
        public string Manufacturername { get; set; }

        [JsonPropertyName("productname")]
        public string Productname { get => productname; set => SetProperty(ref productname, value); }

        [JsonPropertyName("capabilities")]
        public Capabilities Capabilities { get => capabilities; set => SetProperty(ref capabilities, value); }

        [JsonPropertyName("config")]
        public LightConfig Config { get => config; set => SetProperty(ref config, value); }

        [JsonPropertyName("uniqueid")]
        public string Uniqueid { get => uniqueid; set => SetProperty(ref uniqueid, value); }

        [JsonPropertyName("swversion")]
        public string Swversion { get => swversion; set => SetProperty(ref swversion,value); }

        public void SetState(params Tuple<string,object>[] properties)
        {
            if(state is null) { State = new State(); }

            foreach(Tuple<string,object> p in properties)
            {
                PropertyInfo pi = state.GetType().GetProperty(p.Item1);
                if (pi == null) continue;
                pi.SetValue(state, p.Item2);

            }

        }

        public void SetState(State state)
        {
            State = state;
        }
    }
}
