using Newtonsoft.Json;
using NHotkey.Wpf;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace wpf_legado_moyu {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            InitGlobalHotKey();
            InitConfigs();
            DataContext = AppData.Data;
        }
        void InitConfigs() {
            AppData? cache = null;
            string configFile = "Config.json";
            if (File.Exists(configFile)) {
                var content = File.ReadAllText(configFile);
                cache = JsonConvert.DeserializeObject<AppData>(content);
            }
            Rect rect;
            if (cache != null) {
                AppData.Data = cache;
                rect = AppData.Data.Config.LastWindowPos;
            } else {
                rect = new Rect();
                rect.Size = new Size(976, 55);
                rect.Location = new Point(SystemParameters.PrimaryScreenWidth / 2f - (rect.Size.Width / 2), SystemParameters.PrimaryScreenHeight - 100);
            }
            Left = rect.Left;
            Top = rect.Top;
            Width = rect.Width;
            Height = rect.Height;
            AppData.InitBookAsync();
        }
        void SaveConfigs() {
            AppData.Data.Config.LastWindowPos = new Rect(Left, Top, Width, Height);
            File.WriteAllText("Config.json", AppData.Data.ToJson());
        }
        public void InitGlobalHotKey() {
            HotkeyManager.Current.AddOrReplace("下一页", new KeyGesture(Key.NumPad6, ModifierKeys.Windows), (obj, e) => {
                AppData.Data.CurBook.NextLine();
            });
            HotkeyManager.Current.AddOrReplace("上一页", new KeyGesture(Key.NumPad4, ModifierKeys.Windows), (obj, e) => {
                AppData.Data.CurBook.LastLine();
            });
            HotkeyManager.Current.AddOrReplace("隐藏", new KeyGesture(Key.NumPad5, ModifierKeys.Windows), (obj, e) => {
                AppData.Data.IsShow = !AppData.Data.IsShow;
            });
            HotkeyManager.Current.AddOrReplace("上一章", new KeyGesture(Key.NumPad7, ModifierKeys.Windows), (obj, e) => {
                AppData.Data.CurBook.LastChap();
            });
            HotkeyManager.Current.AddOrReplace("下一章", new KeyGesture(Key.NumPad9, ModifierKeys.Windows), (obj, e) => {
                AppData.Data.CurBook.NextChap();
            });
        }
        private void Window_Closed(object sender, EventArgs e) {
            SaveConfigs();
        }
        private void Window_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
        private void txt_curTxt_MouseDown(object sender, MouseButtonEventArgs e) {
            Close();
        }
        private void taskbarIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e) {
            AppData.Data.IsShow = !AppData.Data.IsShow;
        }
        private void OnMenuCloseClick(object sender, RoutedEventArgs e) {
            Close();
        }
        private void OnMenuSettingClick(object sender, RoutedEventArgs e) {
            new SettingWindow().Show();
        }
        private void OnMenuBookListClick(object sender, RoutedEventArgs e) {
            new BookListWindow().Show();
        }
        private void OnMenuTopmostClick(object sender, RoutedEventArgs e) {
            AppData.Data.IsTopmost = !AppData.Data.IsTopmost;
            Topmost = !Topmost;
        }
        private void OnMenuAlphaClick(object sender, RoutedEventArgs e) {
            AppData.Data.IsAlpha = !AppData.Data.IsAlpha;
        }
        private void OnMenuAutoClick(object sender, RoutedEventArgs e) {
            AppData.Data.IsAuto = !AppData.Data.IsAuto;
            if (AppData.Data.IsAuto) {
                App.SpeakText(AppData.Data.CurBook.CurContent);
            }
        }
    }
}