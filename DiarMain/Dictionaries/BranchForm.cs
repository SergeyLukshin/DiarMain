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
    public partial class BranchForm : DevExpress.XtraEditors.XtraForm
    {
        BindingList<DataSourceString> listYesNo = new BindingList<DataSourceString>();

        bool m_bAcceptChanges = true;
        bool m_bUpdateID = false;
        public bool m_bCanSelect = false;
        public long m_SelectID = 0;
        public long m_SubjectID = 0;

        public BranchForm()
        {
            InitializeComponent();
        }

        private void BranchForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSetMain.Subjects". При необходимости она может быть перемещена или удалена.
            this.subjectsTableAdapter.Fill(this.dataSetMain.Subjects);
            this.qBranchesTableAdapter.Fill(this.dataSetQuery.QBranches);
            listYesNo.Add(new DataSourceString(0, ""));
            listYesNo.Add(new DataSourceString(1, "да"));
            repYesNo.DataSource = listYesNo;
            repYesNo.DisplayMember = "VAL";
            repYesNo.ValueMember = "KEY";

            if (this.dataSetMain.Subjects.Rows.Count < 7)
                this.repositoryItemLookUpEdit1.DropDownRows = this.dataSetMain.Subjects.Rows.Count;
            else
                this.repositoryItemLookUpEdit1.DropDownRows = 7;
            
            this.dataSetQuery.QBranches.QBranchesRowDeleting += new DataSetQuery.QBranchesRowChangeEventHandler(QBranches_QBranchesRowDeleting);
            this.dataSetQuery.QBranches.QBranchesRowDeleted += new DataSetQuery.QBranchesRowChangeEventHandler(QBranches_QBranchesRowDeleted);
            this.dataSetQuery.QBranches.QBranchesRowChanged += new DataSetQuery.QBranchesRowChangeEventHandler(QBranches_QBranchesRowChanged);
            GridView.OptionsBehavior.Editable = false;

            if (m_bCanSelect)
            {
                cbCanEdit.Checked = true;
                panelSelect.Visible = true;
            }
            else
            {
                panelSelect.Visible = false;
            }

            //this.repositoryItemLookUpEdit1.ListChanged += new ListChangedEventHandler(repositoryItemLookUpEdit1_ListChanged);
            //this.repositoryItemLookUpEdit1.Popup += new EventHandler(repositoryItemLookUpEdit1_Popup);
        }

        /*void repositoryItemLookUpEdit1_Popup(object sender, EventArgs e)
        {
            if (this.dataSetMain.Subjects.Rows.Count < 7)
                this.repositoryItemLookUpEdit1.DropDownRows = this.dataSetMain.Subjects.Rows.Count;
            else
                this.repositoryItemLookUpEdit1.DropDownRows = 7;
            //throw new NotImplementedException();
        }

        void repositoryItemLookUpEdit1_ListChanged(object sender, ListChangedEventArgs e)
        {
            //this.repositoryItemLookUpEdit1.
            //throw new NotImplementedException();
        }*/

        void QBranches_QBranchesRowChanged(object sender, DataSetQuery.QBranchesRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Add || e.Action == DataRowAction.Change)
            {
                try
                {
                    if (e.Action == DataRowAction.Change && m_bUpdateID)
                    {
                        this.dataSetQuery.QBranches.AcceptChanges();
                        m_bUpdateID = false;
                        return;
                    }
                    else
                        using (var cmdBuilder = new SQLiteCommandBuilder(this.qBranchesTableAdapter.Adapter)) this.qBranchesTableAdapter.Adapter.Update(this.dataSetQuery.QBranches);

                    if (e.Action == DataRowAction.Add)
                    {
                        SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                        connection.Open();
                        SQLiteCommand com = new SQLiteCommand(connection);
                        com.CommandText = "select seq from sqlite_sequence where name = 'Branches'";
                        com.CommandType = CommandType.Text;
                        SQLiteDataReader dr = com.ExecuteReader();

                        long id = 0;
                        while (dr.Read())
                        {
                            id = Convert.ToInt64(dr["seq"]);
                        }
                        dr.Close();
                        connection.Close();

                        m_bUpdateID = true;
                        ((DataRowView)(qBranchesBindingSource.Current)).Row["BranchID"] = id;                        
                    }
                }
                catch (SQLiteException ex)
                {
                    MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (DBConcurrencyException ex)
                {
                    MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void QBranches_QBranchesRowDeleted(object sender, DataSetQuery.QBranchesRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Delete)
            {
                if (!m_bAcceptChanges)
                    e.Row.RejectChanges();
                else
                {
                    try
                    {
                        using (var cmdBuilder = new SQLiteCommandBuilder(this.qBranchesTableAdapter.Adapter)) this.qBranchesTableAdapter.Adapter.Update(this.dataSetQuery.QBranches);
                    }
                    catch (SQLiteException ex)
                    {
                        MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (DBConcurrencyException ex)
                    {
                        MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        void QBranches_QBranchesRowDeleting(object sender, DataSetQuery.QBranchesRowChangeEvent e)
        {
            try
            {
                if (e.Action == DataRowAction.Delete)
                {
                    if (Convert.ToInt64(e.Row["ReadOnly"]) != 0)
                    {
                        MyLocalizer.XtraMessageBoxShow("Недостаточно прав для удаления записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        m_bAcceptChanges = false;
                        return;
                    }

                    if (MyLocalizer.XtraMessageBoxShow("Удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    {
                        m_bAcceptChanges = false;
                        return;
                    }
                    else
                    {
                        long id = Convert.ToInt64(e.Row["BranchID"]);

                        SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                        connection.Open();
                        SQLiteCommand com = new SQLiteCommand(connection);
                        com.CommandText = "Select COUNT(*) AS Cnt from Equipments AS e " +
                            "INNER JOIN Substations AS s ON s.SubstationID = e.SubstationID " +
                            "WHERE s.BranchID = ?";
                        com.CommandType = CommandType.Text;
                        SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.Int64);
                        param1.Value = id;
                        com.Parameters.Add(param1);
                        SQLiteDataReader dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            if (Convert.ToInt64(dr["Cnt"]) > 0)
                            {
                                MyLocalizer.XtraMessageBoxShow("Существует оборудование, зарегистрированное на данный филиал.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                m_bAcceptChanges = false;
                                dr.Close();
                                connection.Close();
                                return;
                            }
                        }
                        dr.Close();

                        com.CommandText = "Select COUNT(*) AS Cnt from Substations AS s WHERE s.BranchID = ?";
                        SQLiteDataReader dr2 = com.ExecuteReader();
                        long iCntSubstations = 0;
                        while (dr2.Read())
                        {
                            iCntSubstations = Convert.ToInt64(dr2["Cnt"]);
                            if (iCntSubstations > 0)
                            {
                                if (MyLocalizer.XtraMessageBoxShow("Данный филиал содержит подстанции. Удалить их вместе с филиалом?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                {
                                    m_bAcceptChanges = false;
                                    dr2.Close();
                                    connection.Close();
                                    return;
                                }
                            }
                        }
                        dr2.Close();

                        if (iCntSubstations > 0)
                        {
                            com.CommandText = "Delete from Substations WHERE BranchID = ?";
                            com.ExecuteNonQuery();
                        }

                        m_bAcceptChanges = true;

                        connection.Close();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DBConcurrencyException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void cbCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCanEdit.Checked) GridView.OptionsBehavior.Editable = true;
            else GridView.OptionsBehavior.Editable = false;
        }

        private void GridViewView_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbCanEdit.Checked)
            {
                if (e.KeyCode == Keys.Delete && qBranchesBindingSource.Current != null)
                {
                    ((DataRowView)(qBranchesBindingSource.Current)).Row.Delete();
                }
            }

            if (!GridView.IsEditorFocused)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    Close();
                }
            }
        }

        private void GridView_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (qBranchesBindingSource.Current != null)
            {
                if (Convert.ToInt64(((DataRowView)(qBranchesBindingSource.Current)).Row["ReadOnly"]) != 0)
                {
                    //MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            if (cbCanEdit.Checked && qBranchesBindingSource.Current != null)
            {
                if (Convert.ToInt64(((DataRowView)(qBranchesBindingSource.Current)).Row["ReadOnly"]) != 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void GridView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                long id = 0;

                if (qBranchesBindingSource.Current == null) return;

                DataRowView row = (DataRowView)(qBranchesBindingSource.Current);

                bool bNew = row.IsNew;

                if (!bNew)
                {
                    if (Convert.ToInt64(row["ReadOnly"]) != 0)
                    {
                        MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        return;
                    }

                    id = Convert.ToInt64(row["BranchID"]);
                }

                string strName = row["BranchName"].ToString();
                strName = strName.Trim();
                if (strName == "")
                {
                    e.ErrorText = "Необходимо указать наименование филиала.";
                    e.Valid = false;
                    return;
                }

                if (row["SubjectID"] == DBNull.Value || Convert.ToInt64(row["SubjectID"]) == 0)
                {
                    e.ErrorText = "Необходимо указать субъект.";
                    e.Valid = false;
                    return;
                }

                SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                connection.Open();
                SQLiteCommand com = new SQLiteCommand(connection);
                com.CommandText = "Select * from Branches WHERE EQUAL_STR(BranchName, ?) = 0 AND BranchID <> ?";
                com.CommandType = CommandType.Text;
                SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.String);
                param1.Value = strName;
                SQLiteParameter param2 = new SQLiteParameter("@Param2", DbType.Int64);
                param2.Value = id;
                com.Parameters.Add(param1);
                com.Parameters.Add(param2);
                SQLiteDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    e.ErrorText = "Филиал с таким наименованием уже существует.";
                    e.Valid = false;
                    dr.Close();
                    connection.Close();
                    return;
                }
                dr.Close();
                connection.Close();
            }
            catch (SQLiteException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DBConcurrencyException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /*using (var cmdBuilder = new SQLiteCommandBuilder(this.qBranchesTableAdapter.Adapter)) this.qBranchesTableAdapter.Adapter.Update(this.dataSetQuery.QBranches);

            SQLiteConnection connection2 = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
            connection2.Open();
            SQLiteCommand com2 = new SQLiteCommand(connection2);
            if (bNew)
            {
                com2.CommandText = "select seq from sqlite_sequence where name = 'Branches'";
                com2.CommandType = CommandType.Text;
                com2.Parameters.Clear();
                SQLiteDataReader dr2 = com2.ExecuteReader();

                id = 0;
                while (dr2.Read())
                {
                    id = Convert.ToInt64(dr2.GetValue(0));
                }
                dr2.Close();

                ((DataRowView)(qBranchesBindingSource.Current)).Row["BranchID"] = id;
            }

            connection2.Close();*/
        }

        private void GridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            MyLocalizer.XtraMessageBoxShow(e.ErrorText, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void bSelect_Click(object sender, EventArgs e)
        {
            if (qBranchesBindingSource.Current != null)
            {
                m_SelectID = Convert.ToInt64(((DataRowView)(qBranchesBindingSource.Current)).Row["BranchID"]);
                m_SubjectID = Convert.ToInt64(((DataRowView)(qBranchesBindingSource.Current)).Row["SubjectID"]);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо выбрать запись", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void repositoryItemLookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                SubjectForm f = new SubjectForm();
                f.m_bCanSelect = true;
                DialogResult res = f.ShowDialog(this);
                
                this.subjectsTableAdapter.Fill(this.dataSetMain.Subjects);
                if (this.dataSetMain.Subjects.Rows.Count < 7)
                    this.repositoryItemLookUpEdit1.DropDownRows = this.dataSetMain.Subjects.Rows.Count;
                else
                    this.repositoryItemLookUpEdit1.DropDownRows = 7;

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    GridView.BeginUpdate();
                    DataRowView drv = (DataRowView)(qBranchesBindingSource.Current);
                    drv.Row["SubjectID"] = f.m_SelectID;
                    GridView.EndUpdate();
                    //GridView.Refresh();
                }
            }
        }
    }
}