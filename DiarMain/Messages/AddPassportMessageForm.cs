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
    public partial class AddPassportMessageForm : DevExpress.XtraEditors.XtraForm
    {
        public bool m_bAddPassportMessage = false;
        public string m_strLicenseCode;

        public AddPassportMessageForm()
        {
            InitializeComponent();
        }

        private void AddPassportMessageForm_Load(object sender, EventArgs e)
        {

        }

        private void AddParam(SQLiteCommand com, string name, DbType type, object value)
        {
            SQLiteParameter param = new SQLiteParameter(name, type);
            param.Value = value;
            com.Parameters.Add(param);
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddPassportMessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cbShowMessage.Checked)
            {
                SQLiteConnection con = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                try
                {
                    con.Open();
                    SQLiteCommand com = new SQLiteCommand(con);
                    com.CommandType = CommandType.Text;
                    com.CommandText = "UPDATE Licenses SET AddPassportMessage = 1 WHERE Code = @val";
                    com.Parameters.Clear();
                    AddParam(com, "@val", DbType.String, m_strLicenseCode);
                    com.ExecuteNonQuery();
                    con.Close();

                    m_bAddPassportMessage = true;
                }
                catch (SQLiteException ex)
                {
                    MyLocalizer.XtraMessageBoxShow("Не удалось подключиться к базе данных. Описание: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}