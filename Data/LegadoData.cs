namespace wpf_legado_moyu {
    public class BookListData {
        public BookListItem[] data { get; set; } = [];
        public string errorMsg { get; set; } = string.Empty;
        public bool isSuccess { get; set; }
    }

    public class BookListItem {
        public string author { get; set; } = string.Empty;
        public string bookUrl { get; set; } = string.Empty;
        public bool canUpdate { get; set; }
        public string coverUrl { get; set; } = string.Empty;
        public int durChapterIndex { get; set; }
        public int durChapterPos { get; set; }
        public long durChapterTime { get; set; }
        public string durChapterTitle { get; set; } = string.Empty;
        public int group { get; set; }
        public string intro { get; set; } = string.Empty;
        public string kind { get; set; } = string.Empty;
        public int lastCheckCount { get; set; }
        public long lastCheckTime { get; set; }
        public long latestChapterTime { get; set; }
        public string latestChapterTitle { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public int order { get; set; }
        public string origin { get; set; } = string.Empty;
        public string originName { get; set; } = string.Empty;
        public int originOrder { get; set; }
        public BookListItemReadconfig? readConfig { get; set; }
        public string tocUrl { get; set; } = string.Empty;
        public int totalChapterNum { get; set; }
        public int type { get; set; }
        public string wordCount { get; set; } = string.Empty;
        public string variable { get; set; } = string.Empty;
        public string customCoverUrl { get; set; } = string.Empty;
    }

    public class BookListItemReadconfig {
        public int delTag { get; set; }
        public bool reSegment { get; set; }
        public bool reverseToc { get; set; }
        public bool splitLongChapter { get; set; }
        public string imageStyle { get; set; } = string.Empty;
        public int pageAnim { get; set; }
        public bool useReplaceRule { get; set; }
    }


    public class BookContentReq {
        public string data { get; set; } = string.Empty;
        public string errorMsg { get; set; } = string.Empty;
        public bool isSuccess { get; set; }
    }


    public class BookChapterListReq {
        public BookChapterListItem[] data { get; set; } = [];
        public string errorMsg { get; set; } = string.Empty;
        public bool isSuccess { get; set; }
    }

    public class BookChapterListItem {
        public string baseUrl { get; set; } = string.Empty;
        public string bookUrl { get; set; } = string.Empty;
        public int index { get; set; }
        public bool isPay { get; set; }
        public bool isVip { get; set; }
        public bool isVolume { get; set; }
        public string tag { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
    }
}
