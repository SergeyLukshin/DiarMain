using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SQLite;

namespace DiarMain
{
    public partial class PrintFillabilityMessageForm : DevExpress.XtraEditors.XtraForm
    {
        public double m_fProcent;

        public PrintFillabilityMessageForm()
        {
            InitializeComponent();
        }

        private void PrintFillabilityMessageForm_Load(object sender, EventArgs e)
        {
            lMessage.Text = "Не заполнено " + m_fProcent.ToString("0.##%") + " данных";
        }

        private void bFillData_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bContinue_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}