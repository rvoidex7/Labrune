
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
            this.grpScope.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(13, 13);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(48, 13);
            this.lblApiKey.TabIndex = 0;
            this.lblApiKey.Text = "API Key:";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(16, 29);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(356, 20);
            this.txtApiKey.TabIndex = 1;
            // 
            // lblRules
            // 
            this.lblRules.AutoSize = true;
            this.lblRules.Location = new System.Drawing.Point(13, 62);
            this.lblRules.Name = "lblRules";
            this.lblRules.Size = new System.Drawing.Size(200, 13);
            this.lblRules.TabIndex = 2;
            this.lblRules.Text = "Translation Rules / Glossary (One per line):";
            // 
            // txtRules
            // 
            this.txtRules.AcceptsReturn = true;
            this.txtRules.Location = new System.Drawing.Point(16, 78);
            this.txtRules.Multiline = true;
            this.txtRules.Name = "txtRules";
            this.txtRules.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRules.Size = new System.Drawing.Size(356, 100);
            this.txtRules.TabIndex = 3;
            this.txtRules.Text = "Do not translate: NFS, BMW, Turbo, Nitrous";
            // 
            // grpScope
            // 
            this.grpScope.Controls.Add(this.rbAll);
            this.grpScope.Controls.Add(this.rbSelection);
            this.grpScope.Location = new System.Drawing.Point(16, 194);
            this.grpScope.Name = "grpScope";
            this.grpScope.Size = new System.Drawing.Size(356, 50);
            this.grpScope.TabIndex = 4;
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
            this.progressBar.Location = new System.Drawing.Point(16, 250);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(356, 23);
            this.progressBar.TabIndex = 5;
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(216, 289);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(75, 23);
            this.btnTranslate.TabIndex = 6;
            this.btnTranslate.Text = "Translate";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(297, 289);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // LabruneTranslate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 324);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.grpScope);
            this.Controls.Add(this.txtRules);
            this.Controls.Add(this.lblRules);
            this.Controls.Add(this.txtApiKey);
            this.Controls.Add(this.lblApiKey);
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
    }
}
