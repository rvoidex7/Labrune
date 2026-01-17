namespace Labrune
{
    partial class LabruneTranslate
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblApiKey = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.lblCustomPrompt = new System.Windows.Forms.Label();
            this.txtCustomPrompt = new System.Windows.Forms.TextBox();
            this.grpScope = new System.Windows.Forms.GroupBox();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbSelection = new System.Windows.Forms.RadioButton();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblProvider = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.chkSaveKey = new System.Windows.Forms.CheckBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.lblModelName = new System.Windows.Forms.Label();
            this.txtModelName = new System.Windows.Forms.TextBox();
            this.lblBaseUrl = new System.Windows.Forms.Label();
            this.txtBaseUrl = new System.Windows.Forms.TextBox();
            this.lblTargetLang = new System.Windows.Forms.Label();
            this.cmbTargetLang = new System.Windows.Forms.ComboBox();
            this.btnShowLog = new System.Windows.Forms.Button();
            this.grpScope.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.Location = new System.Drawing.Point(13, 13);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(50, 13);
            this.lblProvider.TabIndex = 0;
            this.lblProvider.Text = "Provider:";
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Items.AddRange(new object[] {
            "OpenAI",
            "Grok",
            "DeepL",
            "Google Gemini",
            "Google (Web)"});
            this.cmbProvider.Location = new System.Drawing.Point(16, 29);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(200, 21);
            this.cmbProvider.TabIndex = 1;
            this.cmbProvider.SelectedIndexChanged += new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(13, 62);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(48, 13);
            this.lblApiKey.TabIndex = 2;
            this.lblApiKey.Text = "API Key:";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(16, 78);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(356, 20);
            this.txtApiKey.TabIndex = 3;
            // 
            // chkSaveKey
            // 
            this.chkSaveKey.AutoSize = true;
            this.chkSaveKey.Location = new System.Drawing.Point(232, 61);
            this.chkSaveKey.Name = "chkSaveKey";
            this.chkSaveKey.Size = new System.Drawing.Size(72, 17);
            this.chkSaveKey.TabIndex = 10;
            this.chkSaveKey.Text = "Save Key";
            this.chkSaveKey.UseVisualStyleBackColor = true;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(310, 58);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(62, 23);
            this.btnTestConnection.TabIndex = 11;
            this.btnTestConnection.Text = "Test";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // lblModelName
            // 
            this.lblModelName.AutoSize = true;
            this.lblModelName.Location = new System.Drawing.Point(230, 13);
            this.lblModelName.Name = "lblModelName";
            this.lblModelName.Size = new System.Drawing.Size(39, 13);
            this.lblModelName.TabIndex = 12;
            this.lblModelName.Text = "Model:";
            // 
            // txtModelName
            // 
            this.txtModelName.Location = new System.Drawing.Point(233, 29);
            this.txtModelName.Name = "txtModelName";
            this.txtModelName.Size = new System.Drawing.Size(139, 20);
            this.txtModelName.TabIndex = 13;
            // 
            // lblBaseUrl
            // 
            this.lblBaseUrl.AutoSize = true;
            this.lblBaseUrl.Location = new System.Drawing.Point(13, 110);
            this.lblBaseUrl.Name = "lblBaseUrl";
            this.lblBaseUrl.Size = new System.Drawing.Size(59, 13);
            this.lblBaseUrl.TabIndex = 14;
            this.lblBaseUrl.Text = "Base URL:";
            // 
            // txtBaseUrl
            // 
            this.txtBaseUrl.Location = new System.Drawing.Point(16, 126);
            this.txtBaseUrl.Name = "txtBaseUrl";
            this.txtBaseUrl.Size = new System.Drawing.Size(356, 20);
            this.txtBaseUrl.TabIndex = 15;
            // 
            // lblTargetLang
            // 
            this.lblTargetLang.AutoSize = true;
            this.lblTargetLang.Location = new System.Drawing.Point(230, 110);
            this.lblTargetLang.Name = "lblTargetLang";
            this.lblTargetLang.Size = new System.Drawing.Size(69, 13);
            this.lblTargetLang.TabIndex = 16;
            this.lblTargetLang.Text = "Target Lang:";
            this.lblTargetLang.Visible = false;
            // 
            // cmbTargetLang
            // 
            this.cmbTargetLang.FormattingEnabled = true;
            this.cmbTargetLang.Items.AddRange(new object[] {
            "TR",
            "EN-US",
            "DE",
            "FR",
            "ES"});
            this.cmbTargetLang.Location = new System.Drawing.Point(305, 107);
            this.cmbTargetLang.Name = "cmbTargetLang";
            this.cmbTargetLang.Size = new System.Drawing.Size(67, 21);
            this.cmbTargetLang.TabIndex = 17;
            this.cmbTargetLang.Text = "TR";
            this.cmbTargetLang.Visible = false;
            // 
            // lblCustomPrompt
            // 
            this.lblCustomPrompt.AutoSize = true;
            this.lblCustomPrompt.Location = new System.Drawing.Point(13, 158);
            this.lblCustomPrompt.Name = "lblCustomPrompt";
            this.lblCustomPrompt.Size = new System.Drawing.Size(81, 13);
            this.lblCustomPrompt.TabIndex = 4;
            this.lblCustomPrompt.Text = "Custom Prompt:";
            // 
            // txtCustomPrompt
            // 
            this.txtCustomPrompt.AcceptsReturn = true;
            this.txtCustomPrompt.Location = new System.Drawing.Point(16, 174);
            this.txtCustomPrompt.Multiline = true;
            this.txtCustomPrompt.Name = "txtCustomPrompt";
            this.txtCustomPrompt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCustomPrompt.Size = new System.Drawing.Size(356, 80);
            this.txtCustomPrompt.TabIndex = 5;
            this.txtCustomPrompt.Text = "You are a translator. Translate the text to Turkish.";
            // 
            // grpScope
            // 
            this.grpScope.Controls.Add(this.rbAll);
            this.grpScope.Controls.Add(this.rbSelection);
            this.grpScope.Location = new System.Drawing.Point(16, 260);
            this.grpScope.Name = "grpScope";
            this.grpScope.Size = new System.Drawing.Size(356, 50);
            this.grpScope.TabIndex = 6;
            this.grpScope.TabStop = false;
            this.grpScope.Text = "Scope";
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(166, 19);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 1;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // rbSelection
            // 
            this.rbSelection.AutoSize = true;
            this.rbSelection.Checked = true;
            this.rbSelection.Location = new System.Drawing.Point(16, 19);
            this.rbSelection.Name = "rbSelection";
            this.rbSelection.Size = new System.Drawing.Size(69, 17);
            this.rbSelection.TabIndex = 0;
            this.rbSelection.TabStop = true;
            this.rbSelection.Text = "Selection";
            this.rbSelection.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(16, 316);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(356, 23);
            this.progressBar.TabIndex = 7;
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(216, 355);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(75, 23);
            this.btnTranslate.TabIndex = 8;
            this.btnTranslate.Text = "Translate";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(297, 355);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnShowLog
            // 
            this.btnShowLog.Location = new System.Drawing.Point(16, 355);
            this.btnShowLog.Name = "btnShowLog";
            this.btnShowLog.Size = new System.Drawing.Size(75, 23);
            this.btnShowLog.TabIndex = 18;
            this.btnShowLog.Text = "Show Log";
            this.btnShowLog.UseVisualStyleBackColor = true;
            this.btnShowLog.Click += new System.EventHandler(this.btnShowLog_Click);
            // 
            // LabruneTranslate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 390);
            this.Controls.Add(this.btnShowLog);
            this.Controls.Add(this.cmbTargetLang);
            this.Controls.Add(this.lblTargetLang);
            this.Controls.Add(this.txtBaseUrl);
            this.Controls.Add(this.lblBaseUrl);
            this.Controls.Add(this.txtModelName);
            this.Controls.Add(this.lblModelName);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.chkSaveKey);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.grpScope);
            this.Controls.Add(this.txtCustomPrompt);
            this.Controls.Add(this.lblCustomPrompt);
            this.Controls.Add(this.txtApiKey);
            this.Controls.Add(this.lblApiKey);
            this.Controls.Add(this.cmbProvider);
            this.Controls.Add(this.lblProvider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LabruneTranslate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AI Translate";
            this.grpScope.ResumeLayout(false);
            this.grpScope.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblApiKey;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label lblCustomPrompt;
        private System.Windows.Forms.TextBox txtCustomPrompt;
        private System.Windows.Forms.GroupBox grpScope;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbSelection;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.CheckBox chkSaveKey;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Label lblModelName;
        private System.Windows.Forms.TextBox txtModelName;
        private System.Windows.Forms.Label lblBaseUrl;
        private System.Windows.Forms.TextBox txtBaseUrl;
        private System.Windows.Forms.Label lblTargetLang;
        private System.Windows.Forms.ComboBox cmbTargetLang;
        private System.Windows.Forms.Button btnShowLog;
    }
}