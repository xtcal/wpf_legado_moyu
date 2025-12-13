using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Forms;

namespace wpf_legado_moyu {
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window {
        public SettingWindow() {
            InitializeComponent();
            DataContext = AppData.Data.Config;
            KeyDown += SettingWindow_KeyDown;
        }

        void SettingWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                Close();
            }
        }

        void bgColor_MouseDown(object sender, MouseButtonEventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                System.Drawing.SolidBrush sb = new System.Drawing.SolidBrush(colorDialog.Color);
                SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromArgb(sb.Color.A, sb.Color.R, sb.Color.G, sb.Color.B));
                AppData.Data.Config.BackgroundColor = solidColorBrush.ToString();
            }
        }

        void fontColor_MouseDown(object sender, MouseButtonEventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                System.Drawing.SolidBrush sb = new System.Drawing.SolidBrush(colorDialog.Color);
                SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromArgb(sb.Color.A, sb.Color.R, sb.Color.G, sb.Color.B));
                AppData.Data.Config.FontColor = solidColorBrush.ToString();
            }
        }

        void fontEffectColor_MouseDown(object sender, MouseButtonEventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                System.Drawing.SolidBrush sb = new System.Drawing.SolidBrush(colorDialog.Color);
                SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromArgb(sb.Color.A, sb.Color.R, sb.Color.G, sb.Color.B));
                AppData.Data.Config.FontEffectColor = solidColorBrush.ToString();
            }
        }

        void OnRefreshClick(object sender, RoutedEventArgs e) {
            AppData.InitBookAsync();
            Close();
        }
    }
}
