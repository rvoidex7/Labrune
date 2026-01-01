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
            this.lblRules = new System.Windows.Forms.Label();
            this.txtRules = new System.Windows.Forms.TextBox();
            this.grpScope = new System.Windows.Forms.GroupBox();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbSelection = new System.Windows.Forms.RadioButton();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblProvider = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
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
            "DeepL",
            "Google Gemini"});
            this.cmbProvider.Location = new System.Drawing.Point(16, 29);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(356, 21);
            this.cmbProvider.TabIndex = 1;
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
            // lblRules
            // 
            this.lblRules.AutoSize = true;
            this.lblRules.Location = new System.Drawing.Point(13, 111);
            this.lblRules.Name = "lblRules";
            this.lblRules.Size = new System.Drawing.Size(200, 13);
            this.lblRules.TabIndex = 4;
            this.lblRules.Text = "Translation Rules / Glossary (One per line):";
            // 
            // txtRules
            // 
            this.txtRules.AcceptsReturn = true;
            this.txtRules.Location = new System.Drawing.Point(16, 127);
            this.txtRules.Multiline = true;
            this.txtRules.Name = "txtRules";
            this.txtRules.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRules.Size = new System.Drawing.Size(356, 100);
            this.txtRules.TabIndex = 5;
            this.txtRules.Text = "Do not translate: NFS, BMW, Turbo, Nitrous";
            // 
            // grpScope
            // 
            this.grpScope.Controls.Add(this.rbAll);
            this.grpScope.Controls.Add(this.rbSelection);
            this.grpScope.Location = new System.Drawing.Point(16, 243);
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
            this.progressBar.Location = new System.Drawing.Point(16, 299);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(356, 23);
            this.progressBar.TabIndex = 7;
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(216, 338);
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
            this.btnCancel.Location = new System.Drawing.Point(297, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // LabruneTranslate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 373);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.grpScope);
            this.Controls.Add(this.txtRules);
            this.Controls.Add(this.lblRules);
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
        private System.Windows.Forms.Label lblRules;
        private System.Windows.Forms.TextBox txtRules;
        private System.Windows.Forms.GroupBox grpScope;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbSelection;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.ComboBox cmbProvider;
    }
}