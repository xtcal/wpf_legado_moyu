using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace wpf_legado_moyu {
    public class NotifyPropertyBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string info = "默认值") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}