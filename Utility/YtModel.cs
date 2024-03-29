﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using CodeBeautify;
//
//    var welcome10 = Welcome10.FromJson(jsonString);

namespace CodeBeautify
{
	using System;
	using System.Collections.Generic;

	using System.Globalization;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	public partial class Welcome10
	{
		[JsonProperty("contents")]
		public Contents Contents { get; set; }
	}

	public partial class Contents
	{
		[JsonProperty("twoColumnBrowseResultsRenderer")]
		public TwoColumnBrowseResultsRenderer TwoColumnBrowseResultsRenderer { get; set; }
	}

	public partial class TwoColumnBrowseResultsRenderer
	{
		[JsonProperty("tabs")]
		public Tab[] Tabs { get; set; }
	}

	public partial class Tab
	{
		[JsonProperty("tabRenderer")]
		public TabRenderer TabRenderer { get; set; }
	}

	public partial class TabRenderer
	{
		[JsonProperty("selected")]
		public bool Selected { get; set; }

		[JsonProperty("content")]
		public TabRendererContent Content { get; set; }

		[JsonProperty("trackingParams")]
		public string TrackingParams { get; set; }
	}

	public partial class TabRendererContent
	{
		[JsonProperty("sectionListRenderer")]
		public SectionListRenderer SectionListRenderer { get; set; }
	}

	public partial class SectionListRenderer
	{
		[JsonProperty("contents")]
		public SectionListRendererContent[] Contents { get; set; }

		[JsonProperty("trackingParams")]
		public string TrackingParams { get; set; }

		[JsonProperty("targetId")]
		public string TargetId { get; set; }
	}

	public partial class SectionListRendererContent
	{
		[JsonProperty("itemSectionRenderer", NullValueHandling = NullValueHandling.Ignore)]
		public ItemSectionRenderer ItemSectionRenderer { get; set; }

		[JsonProperty("continuationItemRenderer", NullValueHandling = NullValueHandling.Ignore)]
		public ContinuationItemRenderer ContinuationItemRenderer { get; set; }
	}

	public partial class ContinuationItemRenderer
	{
		[JsonProperty("trigger")]
		public string Trigger { get; set; }

		[JsonProperty("continuationEndpoint")]
		public ContinuationEndpoint ContinuationEndpoint { get; set; }
	}

	public partial class ContinuationEndpoint
	{
		[JsonProperty("clickTrackingParams")]
		public string ClickTrackingParams { get; set; }

		[JsonProperty("commandMetadata")]
		public ContinuationEndpointCommandMetadata CommandMetadata { get; set; }

		[JsonProperty("continuationCommand")]
		public ContinuationCommand ContinuationCommand { get; set; }
	}

	public partial class ContinuationEndpointCommandMetadata
	{
		[JsonProperty("webCommandMetadata")]
		public PurpleWebCommandMetadata WebCommandMetadata { get; set; }
	}

	public partial class PurpleWebCommandMetadata
	{
		[JsonProperty("sendPost")]
		public bool SendPost { get; set; }

		[JsonProperty("apiUrl", NullValueHandling = NullValueHandling.Ignore)]
		public ApiUrl? ApiUrl { get; set; }
	}

	public partial class ContinuationCommand
	{
		[JsonProperty("token")]
		public string Token { get; set; }

		[JsonProperty("request")]
		public string Request { get; set; }
	}

	public partial class ItemSectionRenderer
	{
		[JsonProperty("contents")]
		public ItemSectionRendererContent[] Contents { get; set; }

		[JsonProperty("trackingParams")]
		public string TrackingParams { get; set; }
	}

	public partial class ItemSectionRendererContent
	{
		[JsonProperty("playlistVideoListRenderer")]
		public PlaylistVideoListRenderer PlaylistVideoListRenderer { get; set; }
	}

	public partial class PlaylistVideoListRenderer
	{
		[JsonProperty("contents")]
		public PlaylistVideoListRendererContent[] Contents { get; set; }

		[JsonProperty("playlistId")]
		public TId PlaylistId { get; set; }

		[JsonProperty("isEditable")]
		public bool IsEditable { get; set; }

		[JsonProperty("canReorder")]
		public bool CanReorder { get; set; }

		[JsonProperty("trackingParams")]
		public string TrackingParams { get; set; }

		[JsonProperty("targetId")]
		public TId TargetId { get; set; }
	}

	public partial class PlaylistVideoListRendererContent
	{
		[JsonProperty("playlistVideoRenderer")]
		public PlaylistVideoRenderer PlaylistVideoRenderer { get; set; }
	}

	public partial class PlaylistVideoRenderer
	{
		[JsonProperty("videoId")]
		public string VideoId { get; set; }

		[JsonProperty("thumbnail")]
		public PlaylistVideoRendererThumbnail Thumbnail { get; set; }

		[JsonProperty("title")]
		public Title Title { get; set; }

		[JsonProperty("index")]
		public Index Index { get; set; }

		[JsonProperty("shortBylineText")]
		public ShortBylineText ShortBylineText { get; set; }

		[JsonProperty("lengthText")]
		public Text LengthText { get; set; }

		[JsonProperty("navigationEndpoint")]
		public PlaylistVideoRendererNavigationEndpoint NavigationEndpoint { get; set; }

		[JsonProperty("lengthSeconds")]
		[JsonConverter(typeof(ParseStringConverter))]
		public long LengthSeconds { get; set; }

		[JsonProperty("trackingParams")]
		public string TrackingParams { get; set; }

		[JsonProperty("isPlayable")]
		public bool IsPlayable { get; set; }

		[JsonProperty("menu")]
		public Menu Menu { get; set; }

		[JsonProperty("thumbnailOverlays")]
		public ThumbnailOverlay[] ThumbnailOverlays { get; set; }

		[JsonProperty("videoInfo")]
		public VideoInfo VideoInfo { get; set; }
	}

	public partial class Index
	{
		[JsonProperty("simpleText")]
		[JsonConverter(typeof(ParseStringConverter))]
		public long SimpleText { get; set; }
	}

	public partial class Text
	{
		[JsonProperty("accessibility")]
		public Accessibility Accessibility { get; set; }

		[JsonProperty("simpleText")]
		public string SimpleText { get; set; }
	}

	public partial class Accessibility
	{
		[JsonProperty("accessibilityData")]
		public AccessibilityData AccessibilityData { get; set; }
	}

	public partial class AccessibilityData
	{
		[JsonProperty("label")]
		public string Label { get; set; }
	}

	public partial class Menu
	{
		[JsonProperty("menuRenderer")]
		public MenuRenderer MenuRenderer { get; set; }
	}

	public partial class MenuRenderer
	{
		[JsonProperty("items")]
		public Item[] Items { get; set; }

		[JsonProperty("trackingParams")]
		public string TrackingParams { get; set; }

		[JsonProperty("accessibility")]
		public Accessibility Accessibility { get; set; }
	}

	public partial class Item
	{
		[JsonProperty("menuServiceItemRenderer")]
		public MenuServiceItemRenderer MenuServiceItemRenderer { get; set; }
	}

	public partial class MenuServiceItemRenderer
	{
		[JsonProperty("text")]
		public VideoInfo Text { get; set; }

		[JsonProperty("icon")]
		public Icon Icon { get; set; }

		[JsonProperty("serviceEndpoint")]
		public ServiceEndpoint ServiceEndpoint { get; set; }

		[JsonProperty("trackingParams")]
		public string TrackingParams { get; set; }

		[JsonProperty("hasSeparator", NullValueHandling = NullValueHandling.Ignore)]
		public bool? HasSeparator { get; set; }
	}

	public partial class Icon
	{
		[JsonProperty("iconType")]
		public IconType IconType { get; set; }
	}

	public partial class ServiceEndpoint
	{
		[JsonProperty("clickTrackingParams")]
		public string ClickTrackingParams { get; set; }

		[JsonProperty("commandMetadata")]
		public ContinuationEndpointCommandMetadata CommandMetadata { get; set; }

		[JsonProperty("signalServiceEndpoint", NullValueHandling = NullValueHandling.Ignore)]
		public SignalServiceEndpoint SignalServiceEndpoint { get; set; }

		[JsonProperty("shareEntityServiceEndpoint", NullValueHandling = NullValueHandling.Ignore)]
		public ShareEntityServiceEndpoint ShareEntityServiceEndpoint { get; set; }
	}

	public partial class ShareEntityServiceEndpoint
	{
		[JsonProperty("serializedShareEntity")]
		public string SerializedShareEntity { get; set; }

		[JsonProperty("commands")]
		public Command[] Commands { get; set; }
	}

	public partial class Command
	{
		[JsonProperty("clickTrackingParams")]
		public string ClickTrackingParams { get; set; }

		[JsonProperty("openPopupAction")]
		public OpenPopupAction OpenPopupAction { get; set; }
	}

	public partial class OpenPopupAction
	{
		[JsonProperty("popup")]
		public Popup Popup { get; set; }

		[JsonProperty("popupType")]
		public PopupType PopupType { get; set; }

		[JsonProperty("beReused")]
		public bool BeReused { get; set; }
	}

	public partial class Popup
	{
		[JsonProperty("unifiedSharePanelRenderer")]
		public UnifiedSharePanelRenderer UnifiedSharePanelRenderer { get; set; }
	}

	public partial class UnifiedSharePanelRenderer
	{
		[JsonProperty("trackingParams")]
		public string TrackingParams { get; set; }

		[JsonProperty("showLoadingSpinner")]
		public bool ShowLoadingSpinner { get; set; }
	}

	public partial class SignalServiceEndpoint
	{
		[JsonProperty("signal")]
		public Signal Signal { get; set; }

		[JsonProperty("actions")]
		public Action[] Actions { get; set; }
	}

	public partial class Action
	{
		[JsonProperty("clickTrackingParams")]
		public string ClickTrackingParams { get; set; }

		[JsonProperty("addToPlaylistCommand")]
		public AddToPlaylistCommand AddToPlaylistCommand { get; set; }
	}

	public partial class AddToPlaylistCommand
	{
		[JsonProperty("openMiniplayer")]
		public bool OpenMiniplayer { get; set; }

		[JsonProperty("videoId")]
		public string VideoId { get; set; }

		[JsonProperty("listType")]
		public ListType ListType { get; set; }

		[JsonProperty("onCreateListCommand")]
		public OnCreateListCommand OnCreateListCommand { get; set; }

		[JsonProperty("videoIds")]
		public string[] VideoIds { get; set; }
	}

	public partial class OnCreateListCommand
	{
		[JsonProperty("clickTrackingParams")]
		public string ClickTrackingParams { get; set; }

		[JsonProperty("commandMetadata")]
		public ContinuationEndpointCommandMetadata CommandMetadata { get; set; }

		[JsonProperty("createPlaylistServiceEndpoint")]
		public CreatePlaylistServiceEndpoint CreatePlaylistServiceEndpoint { get; set; }
	}

	public partial class CreatePlaylistServiceEndpoint
	{
		[JsonProperty("videoIds")]
		public string[] VideoIds { get; set; }

		[JsonProperty("params")]
		public CreatePlaylistServiceEndpointParams Params { get; set; }
	}

	public partial class VideoInfo
	{
		[JsonProperty("runs")]
		public VideoInfoRun[] Runs { get; set; }
	}

	public partial class VideoInfoRun
	{
		[JsonProperty("text")]
		public string Text { get; set; }
	}

	public partial class PlaylistVideoRendererNavigationEndpoint
	{
		[JsonProperty("clickTrackingParams")]
		public string ClickTrackingParams { get; set; }

		[JsonProperty("commandMetadata")]
		public NavigationEndpointCommandMetadata CommandMetadata { get; set; }

		[JsonProperty("watchEndpoint")]
		public WatchEndpoint WatchEndpoint { get; set; }
	}

	public partial class NavigationEndpointCommandMetadata
	{
		[JsonProperty("webCommandMetadata")]
		public FluffyWebCommandMetadata WebCommandMetadata { get; set; }
	}

	public partial class FluffyWebCommandMetadata
	{
		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("webPageType")]
		public WebPageType WebPageType { get; set; }

		[JsonProperty("rootVe")]
		public long RootVe { get; set; }

		[JsonProperty("apiUrl", NullValueHandling = NullValueHandling.Ignore)]
		public ApiUrl? ApiUrl { get; set; }
	}

	public partial class WatchEndpoint
	{
		[JsonProperty("videoId")]
		public string VideoId { get; set; }

		[JsonProperty("playlistId")]
		public TId PlaylistId { get; set; }

		[JsonProperty("index")]
		public long Index { get; set; }

		[JsonProperty("params")]
		public WatchEndpointParams Params { get; set; }

		[JsonProperty("playerParams")]
		public PlayerParams PlayerParams { get; set; }

		[JsonProperty("loggingContext")]
		public LoggingContext LoggingContext { get; set; }

		[JsonProperty("watchEndpointSupportedOnesieConfig")]
		public WatchEndpointSupportedOnesieConfig WatchEndpointSupportedOnesieConfig { get; set; }
	}

	public partial class LoggingContext
	{
		[JsonProperty("vssLoggingContext")]
		public VssLoggingContext VssLoggingContext { get; set; }
	}

	public partial class VssLoggingContext
	{
		[JsonProperty("serializedContextData")]
		public SerializedContextData SerializedContextData { get; set; }
	}

	public partial class WatchEndpointSupportedOnesieConfig
	{
		[JsonProperty("html5PlaybackOnesieConfig")]
		public Html5PlaybackOnesieConfig Html5PlaybackOnesieConfig { get; set; }
	}

	public partial class Html5PlaybackOnesieConfig
	{
		[JsonProperty("commonConfig")]
		public CommonConfig CommonConfig { get; set; }
	}

	public partial class CommonConfig
	{
		[JsonProperty("url")]
		public Uri Url { get; set; }
	}

	public partial class ShortBylineText
	{
		[JsonProperty("runs")]
		public ShortBylineTextRun[] Runs { get; set; }
	}

	public partial class ShortBylineTextRun
	{
		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("navigationEndpoint")]
		public RunNavigationEndpoint NavigationEndpoint { get; set; }
	}

	public partial class RunNavigationEndpoint
	{
		[JsonProperty("clickTrackingParams")]
		public string ClickTrackingParams { get; set; }

		[JsonProperty("commandMetadata")]
		public NavigationEndpointCommandMetadata CommandMetadata { get; set; }

		[JsonProperty("browseEndpoint")]
		public BrowseEndpoint BrowseEndpoint { get; set; }
	}

	public partial class BrowseEndpoint
	{
		[JsonProperty("browseId")]
		public string BrowseId { get; set; }

		[JsonProperty("canonicalBaseUrl")]
		public string CanonicalBaseUrl { get; set; }
	}

	public partial class PlaylistVideoRendererThumbnail
	{
		[JsonProperty("thumbnails")]
		public ThumbnailElement[] Thumbnails { get; set; }
	}

	public partial class ThumbnailElement
	{
		[JsonProperty("url")]
		public Uri Url { get; set; }

		[JsonProperty("width")]
		public long Width { get; set; }

		[JsonProperty("height")]
		public long Height { get; set; }
	}

	public partial class ThumbnailOverlay
	{
		[JsonProperty("thumbnailOverlayTimeStatusRenderer", NullValueHandling = NullValueHandling.Ignore)]
		public ThumbnailOverlayTimeStatusRenderer ThumbnailOverlayTimeStatusRenderer { get; set; }

		[JsonProperty("thumbnailOverlayNowPlayingRenderer", NullValueHandling = NullValueHandling.Ignore)]
		public ThumbnailOverlayNowPlayingRenderer ThumbnailOverlayNowPlayingRenderer { get; set; }
	}

	public partial class ThumbnailOverlayNowPlayingRenderer
	{
		[JsonProperty("text")]
		public VideoInfo Text { get; set; }
	}

	public partial class ThumbnailOverlayTimeStatusRenderer
	{
		[JsonProperty("text")]
		public Text Text { get; set; }

		[JsonProperty("style")]
		public Style Style { get; set; }
	}

	public partial class Title
	{
		[JsonProperty("runs")]
		public VideoInfoRun[] Runs { get; set; }

		[JsonProperty("accessibility")]
		public Accessibility Accessibility { get; set; }
	}

	public enum ApiUrl { YoutubeiV1Browse, YoutubeiV1PlaylistCreate, YoutubeiV1ShareGetSharePanel };

	public enum IconType { AddToQueueTail, Share };

	public enum PopupType { Dialog };

	public enum ListType { PlaylistEditListTypeQueue };

	public enum CreatePlaylistServiceEndpointParams { Caq3D };

	public enum Signal { ClientSignal };

	public enum WebPageType { WebPageTypeChannel, WebPageTypeWatch };

	public enum SerializedContextData { GiJqthb3WfJsNmUzenRfRzJSbC1TckxIb0RhdllweTjJaXdp };

	public enum WatchEndpointParams { OAI3D };

	public enum PlayerParams { IAqb8Aub };

	public enum TId { PLpwXRl6E3ZtG2RlMrLHoDavYpy2Iiwi };

	public enum Style { Default };

	public partial class Welcome10
	{
		public static Welcome10 FromJson(string json) => JsonConvert.DeserializeObject<Welcome10>(json, CodeBeautify.Converter.Settings);
	}

	public static class Serialize
	{
		public static string ToJson(this Welcome10 self) => JsonConvert.SerializeObject(self, CodeBeautify.Converter.Settings);
	}

	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				ApiUrlConverter.Singleton,
				IconTypeConverter.Singleton,
				PopupTypeConverter.Singleton,
				ListTypeConverter.Singleton,
				CreatePlaylistServiceEndpointParamsConverter.Singleton,
				SignalConverter.Singleton,
				WebPageTypeConverter.Singleton,
				SerializedContextDataConverter.Singleton,
				WatchEndpointParamsConverter.Singleton,
				PlayerParamsConverter.Singleton,
				TIdConverter.Singleton,
				StyleConverter.Singleton,
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}

	internal class ApiUrlConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(ApiUrl) || t == typeof(ApiUrl?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			switch (value)
			{
				case "/youtubei/v1/browse":
					return ApiUrl.YoutubeiV1Browse;
				case "/youtubei/v1/playlist/create":
					return ApiUrl.YoutubeiV1PlaylistCreate;
				case "/youtubei/v1/share/get_share_panel":
					return ApiUrl.YoutubeiV1ShareGetSharePanel;
			}
			throw new Exception("Cannot unmarshal type ApiUrl");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (ApiUrl)untypedValue;
			switch (value)
			{
				case ApiUrl.YoutubeiV1Browse:
					serializer.Serialize(writer, "/youtubei/v1/browse");
					return;
				case ApiUrl.YoutubeiV1PlaylistCreate:
					serializer.Serialize(writer, "/youtubei/v1/playlist/create");
					return;
				case ApiUrl.YoutubeiV1ShareGetSharePanel:
					serializer.Serialize(writer, "/youtubei/v1/share/get_share_panel");
					return;
			}
			throw new Exception("Cannot marshal type ApiUrl");
		}

		public static readonly ApiUrlConverter Singleton = new ApiUrlConverter();
	}

	internal class ParseStringConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			long l;
			if (Int64.TryParse(value, out l))
			{
				return l;
			}
			throw new Exception("Cannot unmarshal type long");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (long)untypedValue;
			serializer.Serialize(writer, value.ToString());
			return;
		}

		public static readonly ParseStringConverter Singleton = new ParseStringConverter();
	}

	internal class IconTypeConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(IconType) || t == typeof(IconType?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			switch (value)
			{
				case "ADD_TO_QUEUE_TAIL":
					return IconType.AddToQueueTail;
				case "SHARE":
					return IconType.Share;
			}
			throw new Exception("Cannot unmarshal type IconType");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (IconType)untypedValue;
			switch (value)
			{
				case IconType.AddToQueueTail:
					serializer.Serialize(writer, "ADD_TO_QUEUE_TAIL");
					return;
				case IconType.Share:
					serializer.Serialize(writer, "SHARE");
					return;
			}
			throw new Exception("Cannot marshal type IconType");
		}

		public static readonly IconTypeConverter Singleton = new IconTypeConverter();
	}

	internal class PopupTypeConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(PopupType) || t == typeof(PopupType?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			if (value == "DIALOG")
			{
				return PopupType.Dialog;
			}
			throw new Exception("Cannot unmarshal type PopupType");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (PopupType)untypedValue;
			if (value == PopupType.Dialog)
			{
				serializer.Serialize(writer, "DIALOG");
				return;
			}
			throw new Exception("Cannot marshal type PopupType");
		}

		public static readonly PopupTypeConverter Singleton = new PopupTypeConverter();
	}

	internal class ListTypeConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(ListType) || t == typeof(ListType?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			if (value == "PLAYLIST_EDIT_LIST_TYPE_QUEUE")
			{
				return ListType.PlaylistEditListTypeQueue;
			}
			throw new Exception("Cannot unmarshal type ListType");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (ListType)untypedValue;
			if (value == ListType.PlaylistEditListTypeQueue)
			{
				serializer.Serialize(writer, "PLAYLIST_EDIT_LIST_TYPE_QUEUE");
				return;
			}
			throw new Exception("Cannot marshal type ListType");
		}

		public static readonly ListTypeConverter Singleton = new ListTypeConverter();
	}

	internal class CreatePlaylistServiceEndpointParamsConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(CreatePlaylistServiceEndpointParams) || t == typeof(CreatePlaylistServiceEndpointParams?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			if (value == "CAQ%3D")
			{
				return CreatePlaylistServiceEndpointParams.Caq3D;
			}
			throw new Exception("Cannot unmarshal type CreatePlaylistServiceEndpointParams");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (CreatePlaylistServiceEndpointParams)untypedValue;
			if (value == CreatePlaylistServiceEndpointParams.Caq3D)
			{
				serializer.Serialize(writer, "CAQ%3D");
				return;
			}
			throw new Exception("Cannot marshal type CreatePlaylistServiceEndpointParams");
		}

		public static readonly CreatePlaylistServiceEndpointParamsConverter Singleton = new CreatePlaylistServiceEndpointParamsConverter();
	}

	internal class SignalConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(Signal) || t == typeof(Signal?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			if (value == "CLIENT_SIGNAL")
			{
				return Signal.ClientSignal;
			}
			throw new Exception("Cannot unmarshal type Signal");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (Signal)untypedValue;
			if (value == Signal.ClientSignal)
			{
				serializer.Serialize(writer, "CLIENT_SIGNAL");
				return;
			}
			throw new Exception("Cannot marshal type Signal");
		}

		public static readonly SignalConverter Singleton = new SignalConverter();
	}

	internal class WebPageTypeConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(WebPageType) || t == typeof(WebPageType?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			switch (value)
			{
				case "WEB_PAGE_TYPE_CHANNEL":
					return WebPageType.WebPageTypeChannel;
				case "WEB_PAGE_TYPE_WATCH":
					return WebPageType.WebPageTypeWatch;
			}
			throw new Exception("Cannot unmarshal type WebPageType");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (WebPageType)untypedValue;
			switch (value)
			{
				case WebPageType.WebPageTypeChannel:
					serializer.Serialize(writer, "WEB_PAGE_TYPE_CHANNEL");
					return;
				case WebPageType.WebPageTypeWatch:
					serializer.Serialize(writer, "WEB_PAGE_TYPE_WATCH");
					return;
			}
			throw new Exception("Cannot marshal type WebPageType");
		}

		public static readonly WebPageTypeConverter Singleton = new WebPageTypeConverter();
	}

	internal class SerializedContextDataConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(SerializedContextData) || t == typeof(SerializedContextData?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			if (value == "GiJQTHB3WFJsNmUzenRfRzJSbC1tckxIb0RhdllweTJJaXdp")
			{
				return SerializedContextData.GiJqthb3WfJsNmUzenRfRzJSbC1TckxIb0RhdllweTjJaXdp;
			}
			throw new Exception("Cannot unmarshal type SerializedContextData");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (SerializedContextData)untypedValue;
			if (value == SerializedContextData.GiJqthb3WfJsNmUzenRfRzJSbC1TckxIb0RhdllweTjJaXdp)
			{
				serializer.Serialize(writer, "GiJQTHB3WFJsNmUzenRfRzJSbC1tckxIb0RhdllweTJJaXdp");
				return;
			}
			throw new Exception("Cannot marshal type SerializedContextData");
		}

		public static readonly SerializedContextDataConverter Singleton = new SerializedContextDataConverter();
	}

	internal class WatchEndpointParamsConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(WatchEndpointParams) || t == typeof(WatchEndpointParams?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			if (value == "OAI%3D")
			{
				return WatchEndpointParams.OAI3D;
			}
			throw new Exception("Cannot unmarshal type WatchEndpointParams");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (WatchEndpointParams)untypedValue;
			if (value == WatchEndpointParams.OAI3D)
			{
				serializer.Serialize(writer, "OAI%3D");
				return;
			}
			throw new Exception("Cannot marshal type WatchEndpointParams");
		}

		public static readonly WatchEndpointParamsConverter Singleton = new WatchEndpointParamsConverter();
	}

	internal class PlayerParamsConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(PlayerParams) || t == typeof(PlayerParams?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			if (value == "iAQB8AUB")
			{
				return PlayerParams.IAqb8Aub;
			}
			throw new Exception("Cannot unmarshal type PlayerParams");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (PlayerParams)untypedValue;
			if (value == PlayerParams.IAqb8Aub)
			{
				serializer.Serialize(writer, "iAQB8AUB");
				return;
			}
			throw new Exception("Cannot marshal type PlayerParams");
		}

		public static readonly PlayerParamsConverter Singleton = new PlayerParamsConverter();
	}

	internal class TIdConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(TId) || t == typeof(TId?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			if (value == "PLpwXRl6e3zt_G2Rl-mrLHoDavYpy2Iiwi")
			{
				return TId.PLpwXRl6E3ZtG2RlMrLHoDavYpy2Iiwi;
			}
			throw new Exception("Cannot unmarshal type TId");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (TId)untypedValue;
			if (value == TId.PLpwXRl6E3ZtG2RlMrLHoDavYpy2Iiwi)
			{
				serializer.Serialize(writer, "PLpwXRl6e3zt_G2Rl-mrLHoDavYpy2Iiwi");
				return;
			}
			throw new Exception("Cannot marshal type TId");
		}

		public static readonly TIdConverter Singleton = new TIdConverter();
	}

	internal class StyleConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(Style) || t == typeof(Style?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			if (value == "DEFAULT")
			{
				return Style.Default;
			}
			throw new Exception("Cannot unmarshal type Style");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (Style)untypedValue;
			if (value == Style.Default)
			{
				serializer.Serialize(writer, "DEFAULT");
				return;
			}
			throw new Exception("Cannot marshal type Style");
		}

		public static readonly StyleConverter Singleton = new StyleConverter();
	}
}
