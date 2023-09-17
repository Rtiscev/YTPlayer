namespace DownloadMusic
{
    public class YtModel
    {
        public class Root
        {
            public Contents contents { get; set; }
        }

        public class Contents
        {
            public TwoColumnBrowseResultsRenderer twoColumnBrowseResultsRenderer { get; set; }
        }

        public class TwoColumnBrowseResultsRenderer
        {
            public List<Tab> tabs { get; set; }
        }

        public class Tab
        {
            public TabRenderer tabRenderer { get; set; }
        }

        public class TabRenderer
        {
            public bool selected { get; set; }
            public Content content { get; set; }
            public string trackingParams { get; set; }
        }

        public class Content
        {
            public SectionListRenderer sectionListRenderer { get; set; }
        }

        public class SectionListRenderer
        {
            public List<Content3> contents { get; set; }
            public string trackingParams { get; set; }
        }

        public class PlaylistVideoListRenderer
        {
            public List<Content5> contents { get; set; }
            public string playlistId { get; set; }
            public bool isEditable { get; set; }
            public bool canReorder { get; set; }
            public string trackingParams { get; set; }
            public string targetId { get; set; }
        }

        public class Content5
        {
            public PlaylistVideoRenderer playlistVideoRenderer { get; set; }
        }

        public class Content3
        {
            public ItemSectionRenderer itemSectionRenderer { get; set; }
        }

        public class ItemSectionRenderer
        {
            public List<Content4> contents { get; set; }
            public string trackingParams { get; set; }
        }

        public class Content4
        {
            public PlaylistVideoListRenderer playlistVideoListRenderer { get; set; }
        }

        public class Accessibility
        {
            public AccessibilityData accessibilityData { get; set; }
        }

        public class AccessibilityData
        {
            public string label { get; set; }
        }

        public class Action
        {
            public string clickTrackingParams { get; set; }
            public AddToPlaylistCommand addToPlaylistCommand { get; set; }
        }

        public class AddToPlaylistCommand
        {
            public bool openMiniplayer { get; set; }
            public string videoId { get; set; }
            public string listType { get; set; }
            public OnCreateListCommand onCreateListCommand { get; set; }
            public List<string> videoIds { get; set; }
        }

        public class BrowseEndpoint
        {
            public string browseId { get; set; }
            public string canonicalBaseUrl { get; set; }
        }

        public class Command
        {
            public string clickTrackingParams { get; set; }
            public OpenPopupAction openPopupAction { get; set; }
        }

        public class CommandMetadata
        {
            public WebCommandMetadata webCommandMetadata { get; set; }
        }

        public class CommonConfig
        {
            public string url { get; set; }
        }

        public class Content2
        {
            public ItemSectionRenderer itemSectionRenderer { get; set; }
            public PlaylistVideoListRenderer playlistVideoListRenderer { get; set; }
            public PlaylistVideoRenderer playlistVideoRenderer { get; set; }
            public TwoColumnBrowseResultsRenderer twoColumnBrowseResultsRenderer { get; set; }
        }

        public class PlaylistVideoRenderer
        {
            public string videoId { get; set; }
            public Thumbnail thumbnail { get; set; }
            public Title title { get; set; }
            public Index index { get; set; }
            public ShortBylineText shortBylineText { get; set; }
            public LengthText lengthText { get; set; }
            public NavigationEndpoint1 navigationEndpoint { get; set; }
            public string lengthSeconds { get; set; }
            public string trackingParams { get; set; }
            public bool isPlayable { get; set; }
            public Menu menu { get; set; }
            public List<ThumbnailOverlay> thumbnailOverlays { get; set; }
            public VideoInfo videoInfo { get; set; }
        }

        public class Thumbnail
        {
            public List<Thumbnail> thumbnails { get; set; }
        }

        public class Title
        {
            public List<Run> runs { get; set; }
            public Accessibility accessibility { get; set; }
        }

        public class Index
        {
            public string simpleText { get; set; }
        }

        public class ShortBylineText
        {
            public List<Run> runs { get; set; }
        }

        public class LengthText
        {
            public Accessibility accessibility { get; set; }
            public string simpleText { get; set; }
        }

        public class NavigationEndpoint1
        {
            public string clickTrackingParams { get; set; }
            public CommandMetadata commandMetadata { get; set; }
            public WatchEndpoint watchEndpoint { get; set; }
        }

        public class Menu
        {
            public MenuRenderer menuRenderer { get; set; }
        }

        public class ThumbnailOverlay
        {
            public ThumbnailOverlayTimeStatusRenderer thumbnailOverlayTimeStatusRenderer { get; set; }
            public ThumbnailOverlayNowPlayingRenderer thumbnailOverlayNowPlayingRenderer { get; set; }
        }

        public class CreatePlaylistServiceEndpoint
        {
            public List<string> videoIds { get; set; }
            public string @params { get; set; }
        }

        public class Html5PlaybackOnesieConfig
        {
            public CommonConfig commonConfig { get; set; }
        }

        public class Icon
        {
            public string iconType { get; set; }
        }

        public class Item
        {
            public MenuServiceItemRenderer menuServiceItemRenderer { get; set; }
        }

        public class LoggingContext
        {
            public VssLoggingContext vssLoggingContext { get; set; }
        }

        public class MenuRenderer
        {
            public List<Item> items { get; set; }
            public string trackingParams { get; set; }
            public Accessibility accessibility { get; set; }
        }

        public class MenuServiceItemRenderer
        {
            public Text text { get; set; }
            public Icon icon { get; set; }
            public ServiceEndpoint serviceEndpoint { get; set; }
            public string trackingParams { get; set; }
            public bool? hasSeparator { get; set; }
        }

        public class NavigationEndpoint
        {
            public string clickTrackingParams { get; set; }
            public CommandMetadata commandMetadata { get; set; }
            public BrowseEndpoint browseEndpoint { get; set; }
            public WatchEndpoint watchEndpoint { get; set; }
        }

        public class OnCreateListCommand
        {
            public string clickTrackingParams { get; set; }
            public CommandMetadata commandMetadata { get; set; }
            public CreatePlaylistServiceEndpoint createPlaylistServiceEndpoint { get; set; }
        }

        public class OpenPopupAction
        {
            public Popup popup { get; set; }
            public string popupType { get; set; }
            public bool beReused { get; set; }
        }

        public class Popup
        {
            public UnifiedSharePanelRenderer unifiedSharePanelRenderer { get; set; }
        }

        public class Run
        {
            public string text { get; set; }
            public NavigationEndpoint navigationEndpoint { get; set; }
        }

        public class ServiceEndpoint
        {
            public string clickTrackingParams { get; set; }
            public CommandMetadata commandMetadata { get; set; }
            public SignalServiceEndpoint signalServiceEndpoint { get; set; }
            public ShareEntityServiceEndpoint shareEntityServiceEndpoint { get; set; }
        }

        public class ShareEntityServiceEndpoint
        {
            public string serializedShareEntity { get; set; }
            public List<Command> commands { get; set; }
        }

        public class SignalServiceEndpoint
        {
            public string signal { get; set; }
            public List<Action> actions { get; set; }
        }

        public class Text
        {
            public List<Run> runs { get; set; }
            public Accessibility accessibility { get; set; }
            public string simpleText { get; set; }
        }

        public class Thumbnail2
        {
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class ThumbnailOverlayNowPlayingRenderer
        {
            public Text text { get; set; }
        }

        public class ThumbnailOverlayTimeStatusRenderer
        {
            public Text text { get; set; }
            public string style { get; set; }
        }

        public class UnifiedSharePanelRenderer
        {
            public string trackingParams { get; set; }
            public bool showLoadingSpinner { get; set; }
        }

        public class VideoInfo
        {
            public List<Run> runs { get; set; }
        }

        public class VssLoggingContext
        {
            public string serializedContextData { get; set; }
        }

        public class WatchEndpoint
        {
            public string videoId { get; set; }
            public string playlistId { get; set; }
            public int index { get; set; }
            public string @params { get; set; }
            public string playerParams { get; set; }
            public LoggingContext loggingContext { get; set; }
            public WatchEndpointSupportedOnesieConfig watchEndpointSupportedOnesieConfig { get; set; }
        }

        public class WatchEndpointSupportedOnesieConfig
        {
            public Html5PlaybackOnesieConfig html5PlaybackOnesieConfig { get; set; }
        }

        public class WebCommandMetadata
        {
            public string url { get; set; }
            public string webPageType { get; set; }
            public int rootVe { get; set; }
            public string apiUrl { get; set; }
            public bool sendPost { get; set; }
        }
    }
}
