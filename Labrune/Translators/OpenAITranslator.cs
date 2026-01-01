using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Labrune.Translators
{
    public class OpenAITranslator : ITranslator
    {
        private string ApiKey;

        public OpenAITranslator(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        public async Task<string> Translate(HttpClient client, string text, string[] rules)
        {
            // Prepare System Prompt
            StringBuilder systemPrompt = new StringBuilder();
            systemPrompt.AppendLine("You are a translator for a racing game. Translate the following text to Turkish.");
            systemPrompt.AppendLine("Do not translate special names, car brands, or technical terms found in the glossary.");
            if (rules.Length > 0)
            {
                systemPrompt.AppendLine("Glossary / Rules:");
                foreach (var r in rules) systemPrompt.AppendLine("- " + r);
            }
            systemPrompt.AppendLine("Return ONLY the translated text, no quotes, no explanations.");

            // Simple manually constructed JSON to avoid dependencies
            string jsonContent = "{" +
                "\"model\": \"gpt-3.5-turbo\"," + // Or gpt-4
                "\"messages\": [" +
                    "{\"role\": \"system\", \"content\": \"" + EscapeJson(systemPrompt.ToString()) + "\"}," +
                    "{\"role\": \"user\", \"content\": \"" + EscapeJson(text) + "\"}]" +
            "}";

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", this.ApiKey));
                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Parse "content": "..."
                    // Very hacky parser for demo purposes
                    int contentIndex = responseString.IndexOf("\"content\":");
                    if (contentIndex > 0)
                    {
                        int startQuote = responseString.IndexOf("\"", contentIndex + 10);
                        if (startQuote > 0)
                        {
                            // Find end quote, watching for escapes
                            int endQuote = startQuote + 1;
                            while (endQuote < responseString.Length)
                            {
                                if (responseString[endQuote] == '"' && responseString[endQuote - 1] != '\\')
                                {
                                    break;
                                }
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
            }
            catch { }

            return text; // Return original on failure
        }

        private string EscapeJson(string s)
        {
            if (s == null) return "";
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
        }

        private string UnescapeJson(string s)
        {
            if (s == null) return "";
            return s.Replace("\\\"", "\"").Replace("\\n", "\n").Replace("\\\\", "\\");
        }
    }
}
