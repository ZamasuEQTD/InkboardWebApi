using System.Text.RegularExpressions;

namespace Domain.Media.Utils { 
    public static class YoutubeUtil
    {
        private static readonly Regex YOUTUBE_LINK_DETECTOR = new Regex(@"^(https?://)?(www\.)?(youtube\.com|youtu\.?be)/.+$");
        private static readonly Regex YOUTUBE_VIDEO_SHORT_DETECTOR = new Regex(@"^(https?://)?(www\.)?(youtube\.com|youtu\.?be)/.+$");
        private static readonly Regex YOUTUBE_VIDEO_SHORT_ID_EXTRACTOR = new Regex(@"(?:youtube\.com\/(?:[^\/]+\/?v=|v\/|embed\/)|youtu\.be\/)([^#\&\?]+)");
        public static string GetVideoThumbnailFromId(string videoId) => $"https://img.youtube.com/vi/{videoId}/hqdefault.jpg";
        public static string GetVideoThumbnailFromUrl(string url) => GetVideoThumbnailFromId(GetVideoId(url));
        public static string GetVideoId(string url) => YOUTUBE_VIDEO_SHORT_ID_EXTRACTOR.Match(url).Groups[1].Value;
        public static bool IsShortOrVideo(string url) => YOUTUBE_VIDEO_SHORT_DETECTOR.IsMatch(url);
        public static bool IsYoutubeLink(string url) => YOUTUBE_LINK_DETECTOR.IsMatch(url);
    }
}
