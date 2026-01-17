using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Labrune.Translators;
using Labrune.Properties; // For Settings

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // Restore Settings
            string lastProvider = Settings.Default.LastProvider;
            if (cmbProvider.Items.Contains(lastProvider))
                cmbProvider.SelectedItem = lastProvider;
            else
                cmbProvider.SelectedIndex = 0;

            txtApiKey.Text = Settings.Default.ApiKey;
            chkSaveKey.Checked = !string.IsNullOrEmpty(txtApiKey.Text);
            
            string savedPrompt = Settings.Default.CustomPrompt;
            if (!string.IsNullOrEmpty(savedPrompt)) txtCustomPrompt.Text = savedPrompt;
            
            string savedModel = Settings.Default.ModelName;
            if (!string.IsNullOrEmpty(savedModel)) txtModelName.Text = savedModel;

            string savedBaseUrl = Settings.Default.BaseUrl;
            if (!string.IsNullOrEmpty(savedBaseUrl)) txtBaseUrl.Text = savedBaseUrl;

            string savedTargetLang = Settings.Default.TargetLang;
            if (!string.IsNullOrEmpty(savedTargetLang)) cmbTargetLang.Text = savedTargetLang;

            UpdateUI();
        }

        private void cmbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            string provider = cmbProvider.SelectedItem.ToString();
            
            // Default visibility
            bool needsApiKey = true;
            bool needsPrompt = true;
            bool needsModel = true;
            bool needsBaseUrl = true;
            bool needsTargetLang = false;

            switch (provider)
            {
                case "OpenAI":
                    needsBaseUrl = false; // Optional, usually default
                    needsTargetLang = false;
                    break;
                 case "Grok":
                    needsBaseUrl = true;
                    needsTargetLang = false;
                    break;
                case "DeepL":
                    needsPrompt = false;
                    needsModel = false;
                    needsBaseUrl = false;
                    needsTargetLang = true;
                    break;
                case "Google Gemini":
                    needsBaseUrl = false;
                    needsTargetLang = false;
                    break;
                 case "Google (Web)":
                    needsApiKey = false;
                    needsPrompt = false;
                    needsModel = false;
                    needsBaseUrl = false;
                    needsTargetLang = true;
                    break;
            }

            txtApiKey.Enabled = needsApiKey;
            chkSaveKey.Enabled = needsApiKey;
            btnTestConnection.Enabled = needsApiKey || provider == "Google (Web)";
            
            lblCustomPrompt.Visible = needsPrompt;
            txtCustomPrompt.Visible = needsPrompt;
            
            lblModelName.Visible = needsModel;
            txtModelName.Visible = needsModel;

            lblBaseUrl.Visible = needsBaseUrl;
            txtBaseUrl.Visible = needsBaseUrl;

            lblTargetLang.Visible = needsTargetLang;
            cmbTargetLang.Visible = needsTargetLang;
        }

        private async void btnTestConnection_Click(object sender, EventArgs e)
        {
            btnTestConnection.Enabled = false;
            LabruneLog.Instance.Show();
            LabruneLog.Instance.Log("Testing connection to " + cmbProvider.SelectedItem.ToString() + "...");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ITranslator translator = GetConfiguredTranslator();
                    string testText = "Hello World";
                    string result = await translator.Translate(client, testText, txtCustomPrompt.Text, txtModelName.Text, GetExtraParams());
                    
                    LabruneLog.Instance.Log("Test Result: " + result);
                    
                    if (result == testText && cmbProvider.SelectedItem.ToString() != "Google (Web)") 
                    {
                         // If result is same, might be error or just skipped.
                         // But usually we expect translation to Turkish (default prompt) or TR 
                         LabruneLog.Instance.Log("Warning: Result is identical to input.");
                         MessageBox.Show("Connection test finished. Result was identical to input (check log).", "Test", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                         MessageBox.Show("Connection successful! Output: " + result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                 LabruneLog.Instance.Log("Test Error: " + ex.Message);
                 MessageBox.Show("Connection failed. Check log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTestConnection.Enabled = true;
            }
        }

        private ITranslator GetConfiguredTranslator()
        {
            return TranslatorFactory.CreateTranslator(cmbProvider.SelectedItem.ToString(), txtApiKey.Text.Trim());
        }

        private string GetExtraParams()
        {
             string provider = cmbProvider.SelectedItem.ToString();
             if (provider == "DeepL" || provider == "Google (Web)")
                return cmbTargetLang.Text; // Use Target Lang
             if (provider == "Grok" || provider == "OpenAI")
                return txtBaseUrl.Text; // Use Base URL
             
             return "";
        }

        private async void btnTranslate_Click(object sender, EventArgs e)
        {
            // Validation
            string provider = cmbProvider.SelectedItem.ToString();
            string apiKey = txtApiKey.Text.Trim();
            
            if ((provider != "Google (Web)") && string.IsNullOrEmpty(apiKey))
            {
                 MessageBox.Show("Please enter an API Key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
            }

            // Save Settings
            Settings.Default.LastProvider = provider;
            Settings.Default.ApiKey = chkSaveKey.Checked ? apiKey : "";
            Settings.Default.CustomPrompt = txtCustomPrompt.Text;
            Settings.Default.ModelName = txtModelName.Text;
            Settings.Default.BaseUrl = txtBaseUrl.Text;
            Settings.Default.TargetLang = cmbTargetLang.Text;
            Settings.Default.Save();

            // Identify records
            List<LanguageStringRecord> recordsToTranslate = new List<LanguageStringRecord>();
            if (rbAll.Checked)
            {
                if (ParentLabrune.LangChunks.Count > ParentLabrune.CurrentChunk)
                    recordsToTranslate = ParentLabrune.LangChunks[ParentLabrune.CurrentChunk].Strings;
            }
            else
            {
                foreach (ListViewItem item in ParentLabrune.SelectedListItems)
                {
                    uint hash = Convert.ToUInt32(item.SubItems[1].Text, 16);
                    var record = ParentLabrune.LangChunks[ParentLabrune.CurrentChunk].Strings.Find(r => r.Hash == hash);
                    if (record != null) recordsToTranslate.Add(record);
                }
            }

            if (recordsToTranslate.Count == 0)
            {
                MessageBox.Show("No records to translate.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // UI Lock
            btnTranslate.Enabled = false;
            grpScope.Enabled = false;
            progressBar.Maximum = recordsToTranslate.Count;
            progressBar.Value = 0;
            IsCancelling = false;
            
            LabruneLog.Instance.Show();
            LabruneLog.Instance.Log("Starting Batch Translation: " + recordsToTranslate.Count + " items.");

            using (HttpClient client = new HttpClient())
            {
                ITranslator translator = GetConfiguredTranslator();
                string prompt = txtCustomPrompt.Text;
                string model = txtModelName.Text;
                string extra = GetExtraParams();

                foreach (var record in recordsToTranslate)
                {
                    if (IsCancelling) break;
                    
                    // Log EVERYTHING for debugging
                    LabruneLog.Instance.Log("=== Processing Record #" + progressBar.Value + " ===");
                    LabruneLog.Instance.Log("Label: " + record.Label);
                    LabruneLog.Instance.Log("Text: " + (string.IsNullOrWhiteSpace(record.Text) ? "[EMPTY]" : record.Text));
                    
                    // Skip empty
                    if (string.IsNullOrWhiteSpace(record.Text)) 
                    { 
                        LabruneLog.Instance.Log(">> SKIPPING (Empty Text)");
                        progressBar.Value++; 
                        continue; 
                    }

                    try
                    {
                        LabruneLog.Instance.Log("Sending to Translator...");
                        string result = await translator.Translate(client, record.Text, prompt, model, extra);
                        
                        LabruneLog.Instance.Log("Result: " + (string.IsNullOrEmpty(result) ? "[EMPTY]" : result));

                        // Check if changed
                        if (!string.IsNullOrEmpty(result) && result != record.Text)
                        {
                            LabruneLog.Instance.Log(">> SUCCESS: Text Updated!");
                            record.Text = result;
                            record.IsModified = true;
                        }
                        else
                        {
                            LabruneLog.Instance.Log(">> NO CHANGE: " + (string.IsNullOrEmpty(result) ? "Empty Result" : "Same Text"));
                        }
                    }
                    catch (Exception ex)
                    {
                        LabruneLog.Instance.Log("!! ERROR: " + ex.Message);
                    }
                    
                    progressBar.Value++;
                }
            }

            ParentLabrune.MarkFileAsModified();
            ParentLabrune.RefreshStringView(); // Ensure UI updates!
            
            btnTranslate.Enabled = true;
            grpScope.Enabled = true;

            MessageBox.Show("Translation completed!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnShowLog_Click(object sender, EventArgs e)
        {
            LabruneLog.Instance.Show();
            LabruneLog.Instance.BringToFront();
        }
    }
}