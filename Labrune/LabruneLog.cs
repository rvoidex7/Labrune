using System;
using System.IO;
using System.Windows.Forms;

namespace Labrune
{
    public partial class LabruneLog : Form
    {
        private static LabruneLog _instance;

        public static LabruneLog Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new LabruneLog();
                return _instance;
            }
        }

        public LabruneLog()
        {
            InitializeComponent();
        }

        public void Log(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(Log), message);
                return;
            }

            txtLog.AppendText(string.Format("[{0}] {1}{2}", DateTime.Now.ToString("HH:mm:ss"), message, Environment.NewLine));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files|*.txt";
            sfd.FileName = "LabruneLog_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(sfd.FileName, txtLog.Text);
                MessageBox.Show("Log saved.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }

        // Prevent disposal on close, just hide
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            base.OnFormClosing(e);
        }
    }
}
