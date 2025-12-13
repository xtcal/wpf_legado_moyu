using Flurl.Http;
using System.Windows;
using System.Windows.Input;

namespace wpf_legado_moyu {
    public partial class BookListWindow : Window {
        public BookListWindow() {
            InitializeComponent();
            InitBookListAsync();
            KeyDown += BookListWindow_KeyDown; ;
        }

        void BookListWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                Close();
            }
        }

        async void InitBookListAsync() {
            var obj = await AppData.GetBookShelf.GetJsonAsync<BookListData>();
            listView1.ItemsSource = obj?.data;
        }

        void Read_Click(object sender, RoutedEventArgs e) {
            BookListItem book = (BookListItem)listView1.SelectedItem;
            if (book != null) {
                SetBookContent(book);
            }
        }

        void SetBookContent(BookListItem book) {
            AppData.Data.CurBookUrl = book.bookUrl;
            AppData.Data.CurChapterIdx = book.durChapterIndex;
            AppData.Data.CurLine = 0;
            AppData.Data.CurBook = new BookData();
            AppData.Data.CurBook.SetBaseAsync(book);
            Close();
        }

        void Menu_Click(object sender, RoutedEventArgs e) {

        }

        private void listView1_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            BookListItem book = (BookListItem)listView1.SelectedItem;
            if (book != null) {
                SetBookContent(book);
            }
        }
    }
}
