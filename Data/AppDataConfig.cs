using Newtonsoft.Json;
using System.Windows;
namespace wpf_legado_moyu {
    public class AppDataConfig : NotifyPropertyBase {
        double backgroundOpacity = 0.3;
        double fontEffectOpacity = 1;
        int fontSize = 13;
        string backgroundColor = "#000000";
        string fontColor = "#FF808080";
        string fontEffectColor = "#FFFFFF";
        string host = "192.168.1.1:1122";
        Rect lastWindowPos = new Rect(0, 0, 800, 130);
        int rate = 3;
        [JsonIgnore]
        public double BackgroundOpacity {
            get {
                if (AppData.Data.IsAlpha || !AppData.Data.IsShow) {
                    return 0;
                }
                return backgroundOpacity;
            }
            set { backgroundOpacity = value; NotifyPropertyChanged(); }
        }
        public int FontSize { get => fontSize; set { fontSize = value; NotifyPropertyChanged(); } }
        public string BackgroundColor { get => backgroundColor; set { backgroundColor = value; NotifyPropertyChanged(); } }
        public string FontColor { get => fontColor; set { fontColor = value; NotifyPropertyChanged(); } }
        public string FontEffectColor { get => fontEffectColor; set { fontEffectColor = value; NotifyPropertyChanged(); } }
        public double FontEffectOpacity { get => fontEffectOpacity; set { fontEffectOpacity = value; NotifyPropertyChanged(); } }
        public string Host { get => host; set { host = value; NotifyPropertyChanged(); } }
        public int Rate { get => rate; set { rate = value; NotifyPropertyChanged(); } }
        public Rect LastWindowPos { get => lastWindowPos; set => lastWindowPos = value; }
    }
}