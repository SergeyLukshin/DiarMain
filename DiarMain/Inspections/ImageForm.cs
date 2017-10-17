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
    public partial class ImageForm : DevExpress.XtraEditors.XtraForm
    {
        public object m_img = null;

        public ImageForm()
        {
            InitializeComponent();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            m_img = peImage.EditValue;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            peImage.LoadImage();
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
            peImage.EditValue = m_img;
        }
    }
}