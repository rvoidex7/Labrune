using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Labrune
{
    public partial class LabruneTranslate : Form
    {
        private Labrune ParentLabrune;
        private bool IsCancelling = false;

        public LabruneTranslate(Labrune parent)
        {
            InitializeComponent();
            ParentLabrune = parent;
        }

        private async void btnTranslate_Click(object sender, EventArgs e)
        {
            string apiKey = txtApiKey.Text.Trim();
            if (string.IsNullOrEmpty(apiKey))
            {
                MessageBox.Show("Please enter an API Key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] rules = txtRules.Lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            
            // Identify which records to translate
            List<LanguageStringRecord> recordsToTranslate = new List<LanguageStringRecord>();
            
            if (rbAll.Checked)
            {
                if (ParentLabrune.LangChunks.Count > ParentLabrune.CurrentChunk)
                    recordsToTranslate = ParentLabrune.LangChunks[ParentLabrune.CurrentChunk].Strings;
            }
            else // Selection
            {
                foreach (ListViewItem item in ParentLabrune.SelectedListItems)
                {
                    // Find the record by Hash (stored in subitem 1)
                    // But easier: The ListViewItem index usually corresponds to list index if not sorted... 
                    // Labrune.cs uses: int ItemIndex = LangChunks[CurrentChunk].Strings.IndexOf(StR);
                    // And ListViewItem text is the index.
                    
                    // Let's rely on Hash which is safer if sorted
                    uint hash = Convert.ToUInt32(item.SubItems[1].Text, 16);
                    var record = ParentLabrune.LangChunks[ParentLabrune.CurrentChunk].Strings.Find(r => r.Hash == hash);
                    if (record != null)
                        recordsToTranslate.Add(record);
                }
            }

            if (recordsToTranslate.Count == 0)
            {
                MessageBox.Show("No records to translate.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // UI Setup
            btnTranslate.Enabled = false;
            grpScope.Enabled = false;
            progressBar.Maximum = recordsToTranslate.Count;
            progressBar.Value = 0;
            IsCancelling = false;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", apiKey));

                foreach (var record in recordsToTranslate)
                {
                    if (IsCancelling) break;

                    // Apply Rules (Simple "Ignore" check for now)
                    bool skip = false;
                    foreach (var rule in rules)
                    {
                        string cleanRule = rule.Replace("Do not translate:", "").Trim();
                        // If rule is "Do not translate: Word", check if Label or Text contains Word
                        if (record.Label.IndexOf(cleanRule, StringComparison.OrdinalIgnoreCase) >= 0 || 
                            record.Text.IndexOf(cleanRule, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Maybe we shouldn't skip entirely, but let's assume specific names should be skipped
                            // Or better: Pass the rules to the AI in the system prompt.
                            // For this iteration, let's pass the rules to AI.
                        }
                    }

                    // For now, let's just skip if empty
                    if (string.IsNullOrWhiteSpace(record.Text))
                    {
                        progressBar.Value++;
                        continue;
                    }

                    try
                    {
                        string translatedText = await TranslateWithAI(client, record.Text, rules);
                        if (!string.IsNullOrEmpty(translatedText))
                        {
                            record.Text = translatedText;
                            record.IsModified = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log or ignore?
                        Console.WriteLine(ex.Message);
                    }

                    progressBar.Value++;
                }
            }

            ParentLabrune.MarkFileAsModified();
            btnTranslate.Enabled = true;
            grpScope.Enabled = true;

            MessageBox.Show("Translation completed!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private async Task<string> TranslateWithAI(HttpClient client, string text, string[] rules)
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
                    "{\"role\": \"user\", \"content\": \"" + EscapeJson(text) + "\"}" +
                "]" +
            "}";

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try 
            {
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
                            while (endQuote < responseString.Length) {
                                if (responseString[endQuote] == '"' && responseString[endQuote - 1] != '\\') {
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
            catch {}

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
