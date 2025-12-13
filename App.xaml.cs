using System.Globalization;
using System.IO;
using System.Speech.Synthesis;
using System.Windows;

namespace wpf_legado_moyu {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            this.Startup += new StartupEventHandler(App_Startup);
        }
        Mutex mutex;
        private void App_Startup(object sender, StartupEventArgs e) {
            mutex = new Mutex(true, typeof(App).Namespace, out var ret);
            if (!ret) {
                Environment.Exit(0);
            }
        }
        private static SpeechSynthesizer? _synthesizer;
        static CultureInfo cultureInfo = new CultureInfo("zh-CN");
        static Prompt? prompt;
        public static void SpeakText(string text) {
            if (text == string.Empty) {
                return;
            }
            if (_synthesizer == null) {
                _synthesizer = new SpeechSynthesizer();
                _synthesizer.SetOutputToDefaultAudioDevice();
                _synthesizer.SpeakCompleted += (obj, e) => {
                    if (e.Cancelled) {
                        return;
                    }
                    if (AppData.Data.IsAuto && AppData.Data.IsShow) {
                        AppData.Data.CurBook.NextLine();
                    }
                };
            }
            if (prompt != null) {
                _synthesizer.SpeakAsyncCancel(prompt);
            }
            _synthesizer.Rate = AppData.Data.Config.Rate;
            var builder = new PromptBuilder();
            builder.StartVoice(cultureInfo);
            builder.AppendText(text);
            builder.EndVoice();
            prompt = _synthesizer.SpeakAsync(builder);
        }

        protected override void OnStartup(StartupEventArgs e) {
            RegisterEvents();
            base.OnStartup(e);
        }

        private void RegisterEvents() {
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;//Task异常 

            //UI线程未捕获异常处理事件（UI主线程）
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e) {
            try {
                var exception = e.Exception as Exception;
                if (exception != null) {
                    HandleException(exception);
                }
            } catch (Exception ex) {
                HandleException(ex);
            } finally {
                e.SetObserved();
            }
        }

        //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)      
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            try {
                var exception = e.ExceptionObject as Exception;
                if (exception != null) {
                    HandleException(exception);
                }
            } catch (Exception ex) {
                HandleException(ex);
            } finally {
                //ignore
            }
        }

        //UI线程未捕获异常处理事件（UI主线程）
        private static void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            try {
                HandleException(e.Exception);
            } catch (Exception ex) {
                HandleException(ex);
            } finally {
                e.Handled = true;
            }
        }
        private static void HandleException(Exception ex) {
            // MessageBox.Show("出错了，请与开发人员联系："+ ex.Message);
            //记录日志
            LogWrite(ex);
        }

        private static readonly ReaderWriterLockSlim LogWriteLock = new ReaderWriterLockSlim();
        public static void LogWrite(Exception ex) {
            if (!Directory.Exists("Log")) {
                Directory.CreateDirectory("Log");
            }
            var now = DateTime.Now;
            var logpath = @"Log\" + now.Year + "" + now.Month + "" + now.Day + ".log";
            var log = "\r\n----------------------" + DateTime.Now + " --------------------------\r\n"
                      + ex.Message
                      + "\r\n"
                      + ex.InnerException
                      + "\r\n"
                      + ex.StackTrace
                      + "\r\n----------------------footer--------------------------\r\n";
            try {
                //设置读写锁为写入模式独占资源，其他写入请求需要等待本次写入结束之后才能继续写入
                LogWriteLock.EnterWriteLock();
                File.AppendAllText(logpath, log);
            } finally {
                //退出写入模式，释放资源占用
                LogWriteLock.ExitWriteLock();
            }
        }
    }
}
