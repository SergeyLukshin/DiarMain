using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DiarMain
{
    public partial class PswForm : DevExpress.XtraEditors.XtraForm
    {
        public PswForm()
        {
            InitializeComponent();
        }

        private void bActivation_Click(object sender, EventArgs e)
        {
            if (tePsw.Text != "15041959")
            {
                MyLocalizer.XtraMessageBoxShow("Указан неверный пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
        }
    }
}