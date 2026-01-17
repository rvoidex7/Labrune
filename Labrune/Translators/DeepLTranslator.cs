using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Labrune.Translators
{
    public class DeepLTranslator : ITranslator
    {
        private string ApiKey;

        public DeepLTranslator(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        public bool RequiresApiKey { get { return true; } }
        public bool SupportsCustomPrompt { get { return false; } }
        public bool SupportsModelSelection { get { return false; } }

        public async Task<string> Translate(HttpClient client, string text, string customPrompt, string modelName, string extraParams)
        {
            // extraParams is used for TargetLang (e.g. "TR", "EN-US")
            string targetLang = string.IsNullOrEmpty(extraParams) ? "TR" : extraParams;

            // DeepL API Url (Free vs Pro)
            // If key ends with :fx, use free endpoint
            string baseUrl = this.ApiKey.EndsWith(":fx") 
                ? "https://api-free.deepl.com/v2/translate" 
                : "https://api.deepl.com/v2/translate";

            try
            {
                // Form Url Encoded content
                var collection = new List<KeyValuePair<string, string>>();
                collection.Add(new KeyValuePair<string, string>("auth_key", this.ApiKey));
                collection.Add(new KeyValuePair<string, string>("text", text));
                collection.Add(new KeyValuePair<string, string>("target_lang", targetLang.ToUpper()));

                var content = new FormUrlEncodedContent(collection);

                var response = await client.PostAsync(baseUrl, content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Parse {"translations":[{"detected_source_language":"EN","text":"Hello World"}]}
                    int textIndex = responseString.IndexOf("\"text\":");
                    if (textIndex > 0)
                    {
                        int startQuote = responseString.IndexOf("\"", textIndex + 7);
                        if (startQuote > 0)
                        {
                            int endQuote = startQuote + 1;
                            while (endQuote < responseString.Length)
                            {
                                if (responseString[endQuote] == '"' && responseString[endQuote - 1] != '\\')
                                    break;
                                endQuote++;
                            }

                            if (endQuote < responseString.Length)
                            {
                                string result = responseString.Substring(startQuote + 1, endQuote - startQuote - 1);
                                return UnescapeJson(result);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("DeepL Error: " + response.StatusCode + " " + responseString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeepL Exception: " + ex.Message);
            }

            return text;
        }

        private string UnescapeJson(string s)
        {
            if (s == null) return "";
            return s.Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\\\", "\\");
        }
    }
}
