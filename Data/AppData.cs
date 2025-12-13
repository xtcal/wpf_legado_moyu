using Flurl.Http;
using Newtonsoft.Json;
namespace wpf_legado_moyu {
    public class AppData : NotifyPropertyBase {
        AppDataConfig config = new AppDataConfig();
        bool isTopmost = true;
        bool isAlpha = false;
        bool isShow = true;
        bool isAuto = false;
        public static AppData Data { get; set; } = new AppData();
        public static string Host => $"http://{Data.Config.Host}";
        public static string GetBookShelf => $"{Host}/getBookshelf";
        public static string GetChapterList => $"{Host}/getChapterList";
        public static string GetBookContent => $"{Host}/getBookContent";
        public static string SaveBookProgress => $"{Host}/saveBookProgress";
        public AppDataConfig Config { get => config; set { config = value; NotifyPropertyChanged(); } }
        public int CurLine { get; set; } = 0;
        public int CurChapterIdx { get; set; } = 0;
        public string CurBookUrl { get; set; } = string.Empty;
        public bool IsAlpha {
            get => isAlpha; set {
                isAlpha = value; NotifyPropertyChanged();
                Config.NotifyPropertyChanged(nameof(Config.BackgroundOpacity));
            }
        }
        [JsonIgnore]
        public BookData CurBook { get; set; } = new BookData();
        [JsonIgnore]
        public string CurText {
            set {
                NotifyPropertyChanged();
            }
            get {
                if (!IsShow) {
                    return string.Empty;
                }
                if (CurBook.CurContent != string.Empty) {
                    return CurBook.CurContent;
                }
                return "(<ゝω·)~☆kira";
            }
        }
        [JsonIgnore]
        public bool IsShow {
            get => isShow; set {
                isShow = value;
                NotifyPropertyChanged(nameof(CurText));
                Config.NotifyPropertyChanged(nameof(Config.BackgroundOpacity));
            }
        }
        [JsonIgnore]
        public bool IsAuto {
            get => isAuto; set {
                isAuto = value;
                NotifyPropertyChanged();
            }
        }
        [JsonIgnore]
        public bool IsTopmost { get => isTopmost; set { isTopmost = value; NotifyPropertyChanged(); } }
        public static async void InitBookAsync() {
            BookListData? bookListReq = null;
            try {
                bookListReq = await GetBookShelf.GetJsonAsync<BookListData>();
            } catch (Exception) {

            }
            if (bookListReq == null) {
                return;
            }
            var bookList = bookListReq.data;
            var book = bookListReq.data.Where(v => v.bookUrl == Data.CurBookUrl).FirstOrDefault();
            if (book != null) {
                if (Data.CurChapterIdx != book.durChapterIndex) {
                    Data.CurChapterIdx = book.durChapterIndex;
                    Data.CurLine = 0;
                }
                Data.CurBook = new BookData();
                Data.CurBook.SetBaseAsync(book);
            } else {
                Data.CurBookUrl = string.Empty;
                Data.CurChapterIdx = 0;
                Data.CurLine = 0;
                new BookListWindow().Show();
            }
        }
        public void RefreshCurText() {
            NotifyPropertyChanged(nameof(CurText));
            App.SpeakText(Data.CurText);
        }
    }
}