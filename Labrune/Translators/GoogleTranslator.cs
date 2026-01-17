using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web; // Needed for UrlEncode? Or use Uri.EscapeDataString

namespace Labrune.Translators
{
    public class GoogleTranslator : ITranslator
    {
        public bool RequiresApiKey { get { return false; } }
        public bool SupportsCustomPrompt { get { return false; } }
        public bool SupportsModelSelection { get { return false; } }

        public async Task<string> Translate(HttpClient client, string text, string customPrompt, string modelName, string extraParams)
        {
            // extraParams used for TargetLang (default: TR)
            string targetLang = string.IsNullOrEmpty(extraParams) ? "tr" : extraParams.ToLower();
            
            // "sl" is source language, we assume auto
            string url = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl={0}&dt=t&q={1}", 
                targetLang, 
                Uri.EscapeDataString(text));

            LabruneLog.Instance.Log("[GoogleTranslator] URL: " + url.Substring(0, Math.Min(url.Length, 150)) + "...");

            try
            {
                client.DefaultRequestHeaders.Clear();
                // User-Agent might be required to avoid immediate blocking
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                LabruneLog.Instance.Log("[GoogleTranslator] Status: " + response.StatusCode);
                LabruneLog.Instance.Log("[GoogleTranslator] Response: " + json.Substring(0, Math.Min(json.Length, 200)) + "...");

                if (response.IsSuccessStatusCode)
                {
                    // Response is [[["Translated Text","Original Text",...]],...]]
                    // We need to parse valid JSON.
                    // Since we are adding new dependencies like Newtonsoft.Json might be heavy, let's use a very basic manual parser 
                    // or just String.Replace checks if simple.
                    // But Google JSON is nested arrays. 
                    // Let's try to find the first string inside the first array.
                    
                    // Example: [[["Merhaba","Hello",null,null,1]],...]
                    
                    // Simple logic:
                    // 1. Find first "
                    // 2. Read until next " taking escapes into account
                    if (json.StartsWith("[[["))
                    {
                        // The first string starts after [[["
                        int startQuote = json.IndexOf("\"");
                        if (startQuote > 0)
                        {
                            int endQuote = startQuote + 1;
                            while (endQuote < json.Length)
                            {
                                if (json[endQuote] == '"' && json[endQuote - 1] != '\\')
                                    break;
                                endQuote++;
                            }
                            
                            if (endQuote < json.Length)
                            {
                                string result = json.Substring(startQuote + 1, endQuote - startQuote - 1);
                                string unescaped = UnescapeJson(result);
                                LabruneLog.Instance.Log("[GoogleTranslator] Parsed Result: " + unescaped);
                                return unescaped;
                            }
                        }
                    }
                    LabruneLog.Instance.Log("[GoogleTranslator] Failed to parse response");
                }
                else
                {
                    LabruneLog.Instance.Log("[GoogleTranslator] HTTP Error: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                LabruneLog.Instance.Log("[GoogleTranslator] Exception: " + ex.Message);
            }

            return text; // Return original on failure
        }

        private string UnescapeJson(string s)
        {
            if (s == null) return "";
            return s.Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\\\", "\\");
        }
    }
}
