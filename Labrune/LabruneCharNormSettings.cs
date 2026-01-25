using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Labrune.Properties;

namespace Labrune
{
    public partial class LabruneCharNormSettings : Form
    {
        public LabruneCharNormSettings()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Load enabled state
            chkEnabled.Checked = Settings.Default.CharNormalizationEnabled;

            // Load rules into DataGridView
            var rules = CharacterNormalizer.ParseRules(Settings.Default.CharNormalizationRules);
            dgvRules.Rows.Clear();

            foreach (var rule in rules)
            {
                dgvRules.Rows.Add(rule.Key, rule.Value);
            }
        }

        private void SaveSettings()
        {
            // Save enabled state
            bool wasEnabled = Settings.Default.CharNormalizationEnabled;
            Settings.Default.CharNormalizationEnabled = chkEnabled.Checked;

            // Build rules string from DataGridView
            var rulesList = new List<string>();
            foreach (DataGridViewRow row in dgvRules.Rows)
            {
                if (row.IsNewRow) continue;

                string from = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : null;
                string to = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : null;

                if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
                {
                    rulesList.Add(from + ":" + to);
                }
            }

            Settings.Default.CharNormalizationRules = string.Join(",", rulesList.ToArray());
            Settings.Default.Save();

            // If toggled from OFF to ON, ask about batch conversion
            if (!wasEnabled && chkEnabled.Checked)
            {
                DialogResult result = MessageBox.Show(
                    "Normalizasyon özelliği aktifleştirildi.\n\nTüm mevcut kayıtlara normalizasyon kuralları uygulansin mi?",
                    "Toplu Dönüşüm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Signal to parent that batch conversion is requested
                    this.Tag = "BATCH_CONVERT";
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRules.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvRules.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dgvRules.Rows.Remove(row);
                    }
                }
            }
        }

        private void btnResetDefaults_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Varsayılan Türkçe karakter kuralları yüklensin mi?\n\nMevcut kurallar silinecek!",
                "Varsayılanlara Dön",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                dgvRules.Rows.Clear();
                
                // Load defaults
                string[] defaults = new string[] {
                    "ü:u", "Ü:U", "ğ:g", "Ğ:G", "ş:s", "Ş:S",
                    "ı:i", "İ:I", "ö:o", "Ö:O", "ç:c", "Ç:C"
                };

                foreach (string rule in defaults)
                {
                    string[] parts = rule.Split(':');
                    dgvRules.Rows.Add(parts[0], parts[1]);
                }
            }
        }
    }
}
