using System.Text;
using static DownloadMusic.YtModel;
using static System.Windows.Forms.LinkLabel;

namespace DownloadMusic
{
    public static class Utility
    {
        public class StringArgs : EventArgs
        {
            public string Title { get; set; }
        }

        public static string ProcessBannedCharacter(string name)
        {
            string[] bannedCharacters = { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };
            List<string> strings = new();
            foreach (var bannedCharacter in bannedCharacters)
            {
                while (name.IndexOf(bannedCharacter) != -1)
                {
                    int value = name.IndexOf(bannedCharacter);
                    if (value != -1)
                    {
                        StringBuilder sb = new(name);
                        sb.Remove(value, 1);
                        name = sb.ToString();
                    }
                }
            }
            return name;
        }

        public static string RequestFunc(HttpClient client, string url)
        {
            // Make the GET request.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Check the status code of the response.
            var statusCode = response.StatusCode;

            // The request was successful.
            return response.Content.ReadAsStringAsync().Result;
        }

        public static string GetTheBody(string responseContent, int contentIndex, int headerIndex)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append("{");
            for (int i = contentIndex; i < headerIndex; i++)
            {
                stringBuilder.Append(responseContent[i]);
            }
            stringBuilder[stringBuilder.Length - 1] = '}';
            return stringBuilder.ToString();
        }

        public static List<Content5> VideoFinalData(Root root)
        {
            return root.contents.twoColumnBrowseResultsRenderer.tabs[0]
                                        .tabRenderer
                                        .content
                                        .sectionListRenderer
                                        .contents[0]
                                        .itemSectionRenderer
                                        .contents[0]
                                        .playlistVideoListRenderer
                                        .contents;
        }

        public static async Task<bool> ValidateLinkAsync(string Link)
        {
            bool isOkay = true;
            try
            {
                HttpRequestMessage msg = new()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(Link)
                };
                var client = new HttpClient();
                var response = await client.SendAsync(msg);
            }
            catch (Exception)
            {
                //if (response.StatusCode != HttpStatusCode.OK)
                //{
                //StringArgs stringArgs1 = new() { Title = "Not a valid link" };
                //WorkCompleted?.Invoke(this, stringArgs1);

                isOkay = false;
                //}
            }
            return isOkay;
        }
    }
}
