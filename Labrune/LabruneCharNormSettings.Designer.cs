namespace Labrune
{
    partial class LabruneCharNormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.dgvRules = new System.Windows.Forms.DataGridView();
            this.colFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnResetDefaults = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).BeginInit();
            this.SuspendLayout();
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(12, 12);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(197, 17);
            this.chkEnabled.TabIndex = 0;
            this.chkEnabled.Text = "Karakter Normalizasyonunu Aktifleştir";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // dgvRules
            // 
            this.dgvRules.AllowUserToResizeRows = false;
            this.dgvRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFrom,
            this.colTo});
            this.dgvRules.Location = new System.Drawing.Point(12, 58);
            this.dgvRules.Name = "dgvRules";
            this.dgvRules.Size = new System.Drawing.Size(360, 250);
            this.dgvRules.TabIndex = 1;
            // 
            // colFrom
            // 
            this.colFrom.HeaderText = "Kaynak Karakter";
            this.colFrom.Name = "colFrom";
            this.colFrom.Width = 120;
            // 
            // colTo
            // 
            this.colTo.HeaderText = "Hedef Karakter";
            this.colTo.Name = "colTo";
            this.colTo.Width = 120;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(216, 314);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(297, 314);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(93, 314);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Sil";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnResetDefaults
            // 
            this.btnResetDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetDefaults.Location = new System.Drawing.Point(12, 314);
            this.btnResetDefaults.Name = "btnResetDefaults";
            this.btnResetDefaults.Size = new System.Drawing.Size(75, 23);
            this.btnResetDefaults.TabIndex = 5;
            this.btnResetDefaults.Text = "Varsayılan";
            this.btnResetDefaults.UseVisualStyleBackColor = true;
            this.btnResetDefaults.Click += new System.EventHandler(this.btnResetDefaults_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Karakter Dönüşüm Kuralları:";
            // 
            // LabruneCharNormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 349);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnResetDefaults);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvRules);
            this.Controls.Add(this.chkEnabled);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LabruneCharNormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Karakter Normalizasyon Ayarları";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.DataGridView dgvRules;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnResetDefaults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTo;
        private System.Windows.Forms.Label label1;
    }
}
