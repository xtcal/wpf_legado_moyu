using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
namespace wpf_legado_moyu {
    public class BookData {
        public BookListItem Base { get; set; } = new BookListItem();
        string BookUrl => Url.Encode(Base.bookUrl);
        [JsonIgnore]
        public BookChapterListItem[] ChapterList { get; set; } = [];
        [JsonIgnore]
        public List<string> ContentLines { get; set; } = new List<string>();
        [JsonIgnore]
        public string CurContent {
            get {
                if (ContentLines.Count == 0) {
                    return string.Empty;
                }
                return ContentLines[AppData.Data.CurLine];
            }
        }
        [JsonIgnore]
        BookChapterListItem CurChapter => ChapterList[Base.durChapterIndex];
        public void SetBaseAsync(BookListItem book) {
            Base = book;
            RefreshChapterList();
        }
        async void RefreshChapterList(Action? cb = null) {
            var req = await $"{AppData.GetChapterList}?url={BookUrl}".GetJsonAsync<BookChapterListReq>();
            AppData.Data.CurBook.ChapterList = req.data;
            AppData.Data.CurBook.ShowChapterAsync(cb);
        }
        public async void ShowChapterAsync(Action? cb = null) {
            if (ChapterList.Length == 0) {
                return;
            }
            var durChapterIndex = Base.durChapterIndex;
            var obj = await $"{AppData.GetBookContent}?url={BookUrl}&index={durChapterIndex}".GetJsonAsync<BookContentReq>();
            var list = obj.data.Split('\n').Select(s => s.Trim()).Where(s => s != string.Empty).ToList();
            list.Insert(0, CurChapter.title);
            ContentLines = list;
            cb?.Invoke();
            SaveProgress();
            AppData.Data.CurChapterIdx = durChapterIndex;
            AppData.Data.RefreshCurText();
        }
        void SaveProgress() {
            var data = new {
                author = Base.author,
                durChapterIndex = Base.durChapterIndex,
                durChapterPos = 0,
                durChapterTime = Tools.DateTimeToLongTimeStamp(),
                durChapterTitle = CurChapter.title,
                name = Base.name,
            };
            AppData.SaveBookProgress.PostStringAsync(data.ToJson());
        }
        public void NextLine() {
            if (ContentLines.Count == 0) {
                return;
            }
            if (AppData.Data.CurLine < ContentLines.Count - 1) {
                AppData.Data.CurLine += 1;
                AppData.Data.RefreshCurText();
            } else {
                Base.durChapterIndex += 1;
                AppData.Data.CurLine = 0;
                ShowChapterAsync();
            }
        }
        public void LastLine() {
            if (ContentLines.Count == 0) {
                return;
            }
            if (AppData.Data.CurLine > 0) {
                AppData.Data.CurLine -= 1;
                AppData.Data.RefreshCurText();
            } else {
                Base.durChapterIndex -= 1;
                AppData.Data.CurLine = 0;
                ShowChapterAsync(() => {
                    AppData.Data.CurLine = ContentLines.Count - 1;
                });
            }
        }

        internal void LastChap() {
            Base.durChapterIndex -= 1;
            AppData.Data.CurLine = 0;
            ShowChapterAsync();
        }

        internal void NextChap() {
            Base.durChapterIndex += 1;
            AppData.Data.CurLine = 0;
            ShowChapterAsync();
        }
    }
}