using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Labrune.Translators;

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
            cmbProvider.SelectedIndex = 0;
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

            ITranslator translator = TranslatorFactory.CreateTranslator(cmbProvider.SelectedItem.ToString(), apiKey);

            using (HttpClient client = new HttpClient())
            {
                foreach (var record in recordsToTranslate)
                {
                    if (IsCancelling) break;

                    if (string.IsNullOrWhiteSpace(record.Text))
                    {
                        progressBar.Value++;
                        continue;
                    }

                    try
                    {
                        string translatedText = await translator.Translate(client, record.Text, rules);
                        if (!string.IsNullOrEmpty(translatedText))
                        {
                            record.Text = translatedText;
                            record.IsModified = true;
                        }
                    }
                    catch (Exception ex)
                    {
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
    }
}