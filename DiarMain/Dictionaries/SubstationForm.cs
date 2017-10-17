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
    public partial class SubstationForm : DevExpress.XtraEditors.XtraForm
    {
        bool m_bAcceptChanges = true;
        bool m_bUpdateID = false;
        BindingList<DataSourceString> listYesNo = new BindingList<DataSourceString>();
        BindingList<DataSourceString> listSubstationType = new BindingList<DataSourceString>();
        public bool m_bCanSelect = false;
        public long m_SelectID = 0;
        public long m_SubjectID = 0;
        public long m_BranchID = 0;
        public Dictionary<long, long> dictSubjects = new Dictionary<long,long>();

        public SubstationForm()
        {
            InitializeComponent();
        }

        private void SubstationForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSetQuery.QSubstations' table. You can move, or remove it, as needed.
            this.qSubstationsTableAdapter.Fill(this.dataSetQuery.QSubstations);
            // TODO: This line of code loads data into the 'dataSetQuery.QSubjects' table. You can move, or remove it, as needed.
            this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);
            // TODO: This line of code loads data into the 'dataSetQuery.QBranchesSubjects' table. You can move, or remove it, as needed.
            this.qBranchesSubjectsTableAdapter.Fill(this.dataSetQuery.QBranchesSubjects);
            this.qSubstationsTableAdapter.Fill(this.dataSetQuery.QSubstations);
            listYesNo.Add(new DataSourceString(0, ""));
            listYesNo.Add(new DataSourceString(1, "да"));
            repYesNo.DataSource = listYesNo;
            repYesNo.DisplayMember = "VAL";
            repYesNo.ValueMember = "KEY";

            listSubstationType.Add(new DataSourceString(0, "подстанция"));
            listSubstationType.Add(new DataSourceString(1, "станция"));
            repSubstationType.DataSource = listSubstationType;
            repSubstationType.DisplayMember = "VAL";
            repSubstationType.ValueMember = "KEY";

            this.repSubstationType.DropDownRows = 2;

            if (this.dataSetQuery.QBranchesSubjects.Rows.Count < 7)
                this.repBranch.DropDownRows = this.dataSetQuery.QBranchesSubjects.Rows.Count;
            else
                this.repBranch.DropDownRows = 7;

            this.dataSetQuery.QSubstations.QSubstationsRowDeleting += new DataSetQuery.QSubstationsRowChangeEventHandler(QSubstations_QSubstationsRowDeleting);
            this.dataSetQuery.QSubstations.QSubstationsRowDeleted += new DataSetQuery.QSubstationsRowChangeEventHandler(QSubstations_QSubstationsRowDeleted);
            this.dataSetQuery.QSubstations.QSubstationsRowChanged += new DataSetQuery.QSubstationsRowChangeEventHandler(QSubstations_QSubstationsRowChanged);
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

            SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
            connection.Open();
            SQLiteCommand com = new SQLiteCommand(connection);
            com.CommandText = "SELECT BranchID, SubjectID FROM Branches";
            com.CommandType = CommandType.Text;
            SQLiteDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                dictSubjects[Convert.ToInt64(dr["BranchID"])] = Convert.ToInt64(dr["SubjectID"]);
            }
            dr.Close();

            GridView.BeginUpdate();
            for (int i = 0; i < this.dataSetQuery.QSubstations.Rows.Count; i++)
            {
                dataSetQuery.QSubstations.Rows[i]["SubjectID"] = dictSubjects[Convert.ToInt64(dataSetQuery.QSubstations.Rows[i]["BranchID"])];
            }
            GridView.EndUpdate();
        }

        void QSubstations_QSubstationsRowChanged(object sender, DataSetQuery.QSubstationsRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Add || e.Action == DataRowAction.Change)
            {
                try
                {
                    if (e.Action == DataRowAction.Change && m_bUpdateID)
                    {
                        this.dataSetQuery.QSubstations.AcceptChanges();
                        m_bUpdateID = false;
                        return;
                    }
                    else
                        using (var cmdBuilder = new SQLiteCommandBuilder(this.qSubstationsTableAdapter.Adapter)) this.qSubstationsTableAdapter.Adapter.Update(this.dataSetQuery.QSubstations);

                    if (e.Action == DataRowAction.Add)
                    {
                        SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                        connection.Open();
                        SQLiteCommand com = new SQLiteCommand(connection);
                        com.CommandText = "select seq from sqlite_sequence where name = 'Substations'";
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
                        ((DataRowView)(qSubstationsBindingSource.Current)).Row["SubstationID"] = id;                        
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

        void QSubstations_QSubstationsRowDeleted(object sender, DataSetQuery.QSubstationsRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Delete)
            {
                if (!m_bAcceptChanges)
                    e.Row.RejectChanges();
                else
                {
                    try
                    {
                        using (var cmdBuilder = new SQLiteCommandBuilder(this.qSubstationsTableAdapter.Adapter)) this.qSubstationsTableAdapter.Adapter.Update(this.dataSetQuery.QSubstations);
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

        void QSubstations_QSubstationsRowDeleting(object sender, DataSetQuery.QSubstationsRowChangeEvent e)
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
                        long id = Convert.ToInt64(e.Row["SubstationID"]);

                        SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                        connection.Open();
                        SQLiteCommand com = new SQLiteCommand(connection);
                        com.CommandText = "Select COUNT(*) AS Cnt from Equipments AS e WHERE e.SubstationID = ?";
                        com.CommandType = CommandType.Text;
                        SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.Int64);
                        param1.Value = id;
                        com.Parameters.Add(param1);
                        SQLiteDataReader dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            if (Convert.ToInt64(dr["Cnt"]) > 0)
                            {
                                MyLocalizer.XtraMessageBoxShow("Существует оборудование, зарегистрированное на данную подстанцию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                m_bAcceptChanges = false;
                                dr.Close();
                                connection.Close();
                                return;
                            }
                        }
                        dr.Close();

                        com.CommandText = "Select COUNT(*) AS Cnt from Checks AS c WHERE c.SubstationID = ?";
                        com.CommandType = CommandType.Text;
                        SQLiteDataReader dr2 = com.ExecuteReader();
                        while (dr2.Read())
                        {
                            if (Convert.ToInt64(dr2["Cnt"]) > 0)
                            {
                                if (MyLocalizer.XtraMessageBoxShow("Существуют комплексные обследования, проведенные на данной подстанции. Удалить их вместе с подстанцией?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    com.CommandText = "DELETE FROM Checks WHERE SubstationID = ?)";
                                    com.ExecuteNonQuery();
                                }
                                else
                                {
                                    m_bAcceptChanges = false;
                                    dr2.Close();
                                    connection.Close();
                                    return;
                                }
                            }
                        }
                        dr2.Close();

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
                if (e.KeyCode == Keys.Delete && qSubstationsBindingSource.Current != null)
                {
                    ((DataRowView)(qSubstationsBindingSource.Current)).Row.Delete();
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
            if (qSubstationsBindingSource.Current != null)
            {
                if (Convert.ToInt64(((DataRowView)(qSubstationsBindingSource.Current)).Row["ReadOnly"]) != 0)
                {
                    //MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            if (cbCanEdit.Checked && qSubstationsBindingSource.Current != null)
            {
                if (Convert.ToInt64(((DataRowView)(qSubstationsBindingSource.Current)).Row["ReadOnly"]) != 0)
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

                if (qSubstationsBindingSource.Current == null) return;

                DataRowView row = (DataRowView)(qSubstationsBindingSource.Current);

                bool bNew = row.IsNew;

                if (!bNew)
                {
                    if (Convert.ToInt64(row["ReadOnly"]) != 0)
                    {
                        MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        return;
                    }

                    id = Convert.ToInt64(row["SubstationID"]);
                }

                string strName = row["SubstationName"].ToString();
                strName = strName.Trim();
                if (strName == "")
                {
                    e.ErrorText = "Необходимо указать наименование подстанции.";
                    e.Valid = false;
                    return;
                }

                if (row["BranchID"] == DBNull.Value || Convert.ToInt64(row["BranchID"]) == 0)
                {
                    e.ErrorText = "Необходимо указать филиал.";
                    e.Valid = false;
                    return;
                }

                long branch_id = Convert.ToInt64(row["BranchID"]);

                SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                connection.Open();
                SQLiteCommand com = new SQLiteCommand(connection);
                com.CommandText = "Select * from Substations WHERE EQUAL_STR(SubstationName, ?) = 0 AND BranchID = ? AND SubstationID <> ?";
                com.CommandType = CommandType.Text;
                SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.String);
                param1.Value = strName;
                SQLiteParameter param2 = new SQLiteParameter("@Param2", DbType.Int64);
                param2.Value = branch_id;
                SQLiteParameter param3 = new SQLiteParameter("@Param3", DbType.Int64);
                param3.Value = id;
                com.Parameters.Add(param1);
                com.Parameters.Add(param2);
                com.Parameters.Add(param3);
                SQLiteDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    e.ErrorText = "Подстанция с таким наименованием в выбранном филиале уже существует.";
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
        }

        private void GridView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            MyLocalizer.XtraMessageBoxShow(e.ErrorText, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void bSelect_Click(object sender, EventArgs e)
        {
            if (qSubstationsBindingSource.Current != null)
            {
                m_SelectID = Convert.ToInt64(((DataRowView)(qSubstationsBindingSource.Current)).Row["SubstationID"]);
                m_BranchID = Convert.ToInt64(((DataRowView)(qSubstationsBindingSource.Current)).Row["BranchID"]);
                m_SubjectID = dictSubjects[m_BranchID];

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
                BranchForm f = new BranchForm();
                f.m_bCanSelect = true;
                DialogResult res = f.ShowDialog(this);
                
                this.qBranchesSubjectsTableAdapter.Fill(this.dataSetQuery.QBranchesSubjects);
                if (this.dataSetQuery.QBranchesSubjects.Rows.Count < 7)
                    this.repBranch.DropDownRows = this.dataSetQuery.QBranchesSubjects.Rows.Count;
                else
                    this.repBranch.DropDownRows = 7;

                this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);
                
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    GridView.BeginUpdate();
                    DataRowView drv = (DataRowView)(qSubstationsBindingSource.Current);
                    drv.Row["BranchID"] = f.m_SelectID;
                    drv.Row["SubjectID"] = f.m_SubjectID;
                    dictSubjects[f.m_SelectID] = f.m_SubjectID;
                    GridView.EndUpdate();
                }
            }
        }

        private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colBranchID")
            {
                DataRowView drv = (DataRowView)(qSubstationsBindingSource.Current);
                object val = drv.Row["BranchID"];
                GridView.BeginUpdate();
                if (val == null || val == DBNull.Value)
                {
                    drv.Row["SubjectID"] = DBNull.Value;
                }
                else
                {
                    SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                    connection.Open();
                    SQLiteCommand com = new SQLiteCommand(connection);
                    com.CommandText = "SELECT SubjectID FROM Branches WHERE BranchID = ?";
                    com.CommandType = CommandType.Text;
                    SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.Int64);
                    param1.Value = Convert.ToInt64(val);
                    com.Parameters.Add(param1);
                    SQLiteDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        drv.Row["SubjectID"] = dr["SubjectID"];
                        dictSubjects[Convert.ToInt64(val)] = Convert.ToInt64(dr["SubjectID"]);
                    }
                    dr.Close();
                }
                GridView.EndUpdate();
            }
        }
    }
}