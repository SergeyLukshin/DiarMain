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
    public partial class SubjectForm : DevExpress.XtraEditors.XtraForm
    {
        bool m_bAcceptChanges = true;
        bool m_bUpdateID = false;
        BindingList<DataSourceString> listYesNo = new BindingList<DataSourceString>();
        public bool m_bCanSelect = false;
        public long m_SelectID = 0;

        public SubjectForm()
        {
            InitializeComponent();
        }

        private void SubjectForm_Load(object sender, EventArgs e)
        {
            this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);
            listYesNo.Add(new DataSourceString(0, ""));
            listYesNo.Add(new DataSourceString(1, "да"));
            repYesNo.DataSource = listYesNo;
            repYesNo.DisplayMember = "VAL";
            repYesNo.ValueMember = "KEY";

            this.dataSetQuery.QSubjects.QSubjectsRowDeleting += new DataSetQuery.QSubjectsRowChangeEventHandler(QSubjects_QSubjectsRowDeleting);
            this.dataSetQuery.QSubjects.QSubjectsRowDeleted += new DataSetQuery.QSubjectsRowChangeEventHandler(QSubjects_QSubjectsRowDeleted);
            this.dataSetQuery.QSubjects.QSubjectsRowChanged += new DataSetQuery.QSubjectsRowChangeEventHandler(QSubjects_QSubjectsRowChanged);
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
        }

        void QSubjects_QSubjectsRowChanged(object sender, DataSetQuery.QSubjectsRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Add || e.Action == DataRowAction.Change)
            {
                /*if (!m_bAcceptChanges)
                    e.Row.RejectChanges();
                else
                {*/
                    try
                    {
                        if (e.Action == DataRowAction.Change && m_bUpdateID)
                        {
                            this.dataSetQuery.QSubjects.AcceptChanges();
                            m_bUpdateID = false;
                            return;
                        }
                        else
                            using (var cmdBuilder = new SQLiteCommandBuilder(this.qSubjectsTableAdapter.Adapter)) this.qSubjectsTableAdapter.Adapter.Update(this.dataSetQuery.QSubjects);

                        if (e.Action == DataRowAction.Add)
                        {
                            SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                            connection.Open();
                            SQLiteCommand com = new SQLiteCommand(connection);
                            com.CommandText = "select seq from sqlite_sequence where name = 'Subjects'";
                            com.CommandType = CommandType.Text;
                            SQLiteDataReader dr = com.ExecuteReader();

                            long id = 0;
                            while (dr.Read())
                            {
                                id = Convert.ToInt64(dr["seq"]);
                            }
                            dr.Close();

                            m_bUpdateID = true;
                            ((DataRowView)(qSubjectsBindingSource.Current)).Row["SubjectID"] = id;

                            // добавляем филиал <без филиала>
                            com.CommandText = "INSERT INTO Branches (SubjectID, BranchName, ReadOnly) VALUES (" + id.ToString() + ", '<без филиала>', 1)";
                            com.ExecuteNonQuery();

                            connection.Close();
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
                //}
            }
        }

        void QSubjects_QSubjectsRowDeleted(object sender, DataSetQuery.QSubjectsRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Delete)
            {
                if (!m_bAcceptChanges)
                {
                    e.Row.RejectChanges();
                }
                else
                {
                    try
                    {
                        using (var cmdBuilder = new SQLiteCommandBuilder(this.qSubjectsTableAdapter.Adapter)) this.qSubjectsTableAdapter.Adapter.Update(this.dataSetQuery.QSubjects);
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

        void QSubjects_QSubjectsRowDeleting(object sender, DataSetQuery.QSubjectsRowChangeEvent e)
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
                        long id = Convert.ToInt64(e.Row["SubjectID"]);

                        SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                        connection.Open();
                        SQLiteCommand com = new SQLiteCommand(connection);
                        com.CommandText = "Select COUNT(*) AS Cnt from Equipments AS e " +
                            "INNER JOIN Substations AS s ON s.SubstationID = e.SubstationID " +
                            "INNER JOIN Branches AS b ON b.BranchID = s.BranchID " +
                            "WHERE b.SubjectID = ?";
                        com.CommandType = CommandType.Text;
                        SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.Int64);
                        param1.Value = id;
                        com.Parameters.Add(param1);
                        SQLiteDataReader dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            if (Convert.ToInt64(dr["Cnt"]) > 0)
                            {
                                MyLocalizer.XtraMessageBoxShow("Существует оборудование, зарегистрированное на данный субъект.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                m_bAcceptChanges = false;
                                dr.Close();
                                connection.Close();
                                return;
                            }
                        }
                        dr.Close();

                        com.CommandText = "Select COUNT(*) AS Cnt from Branches AS b WHERE b.SubjectID = ?";
                        SQLiteDataReader dr2 = com.ExecuteReader();
                        long iCntBranches = 0;
                        while (dr2.Read())
                        {
                            iCntBranches = Convert.ToInt64(dr2["Cnt"]);
                            if (iCntBranches > 0)
                            {
                                if (MyLocalizer.XtraMessageBoxShow("Данный субъект содержит филиалы. Удалить их вместе с субъектом?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                {
                                    m_bAcceptChanges = false;
                                    dr2.Close();
                                    connection.Close();
                                    return;
                                }
                            }
                        }
                        dr2.Close();

                        if (iCntBranches > 0)
                        {
                            //com.CommandText = "DELETE FROM Checks WHERE SubstationID IN (SELECT SubstationID FROM Substations WHERE BranchID IN (SELECT BranchID FROM Branches WHERE SubjectID = ?)))";
                            //com.ExecuteScalar();
                            com.CommandText = "Delete from Substations WHERE BranchID IN (SELECT BranchID FROM Branches WHERE SubjectID = ?)";
                            com.ExecuteNonQuery();
                            com.CommandText = "Delete from Branches WHERE SubjectID = ?";
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

        void QSubjects_QSubjectsRowChanging(object sender, DataSetQuery.QSubjectsRowChangeEvent e)
        {
            /*if (e.Action == DataRowAction.Add || e.Action == DataRowAction.Change)
            {
                int id = 0;

                currentRow = GridView.FocusedRowHandle;

                m_bAcceptChanges = true;

                if (e.Action == DataRowAction.Change)
                {
                    if (Convert.ToInt64(e.Row["ReadOnly"]) != 0)
                    {
                        MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        m_bAcceptChanges = false;
                        return;
                    }

                    id = Convert.ToInt64(e.Row["SubjectID"]);
                }

                if (e.Row["SubjectName"].ToString().Trim() == "")
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать наименование субъекта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_bAcceptChanges = false;
                    return;
                }

                SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                connection.Open();
                SQLiteCommand com = new SQLiteCommand(connection);
                com.CommandText = "Select * from Subjects WHERE SubjectName = ? AND SubjectID <> ?";
                com.CommandType = CommandType.Text;
                SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.String);
                param1.Value = e.Row["SubjectName"].ToString().Trim();
                SQLiteParameter param2 = new SQLiteParameter("@Param2", DbType.Int64);
                param2.Value = id;
                com.Parameters.Add(param1);
                com.Parameters.Add(param2);
                SQLiteDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    MyLocalizer.XtraMessageBoxShow("Субъект с таким наименованием уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_bAcceptChanges = false;
                }
                connection.Close();
            }*/
        }

        private void cbCanEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCanEdit.Checked) GridView.OptionsBehavior.Editable = true;
            else GridView.OptionsBehavior.Editable = false;
        }

        private void SubjectView_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbCanEdit.Checked)
            {
                if (e.KeyCode == Keys.Delete && qSubjectsBindingSource.Current != null)
                {
                    ((DataRowView)(qSubjectsBindingSource.Current)).Row.Delete();
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

        private void SubjectView_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (qSubjectsBindingSource.Current != null)
            {
                if (Convert.ToInt64(((DataRowView)(qSubjectsBindingSource.Current)).Row["ReadOnly"]) != 0)
                {
                    //MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void SubjectView_DoubleClick(object sender, EventArgs e)
        {
            if (cbCanEdit.Checked && qSubjectsBindingSource.Current != null)
            {
                if (Convert.ToInt64(((DataRowView)(qSubjectsBindingSource.Current)).Row["ReadOnly"]) != 0)
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

                if (qSubjectsBindingSource.Current == null) return;

                DataRowView row = (DataRowView)(qSubjectsBindingSource.Current);

                if (!row.IsNew)
                {
                    if (Convert.ToInt64(row["ReadOnly"]) != 0)
                    {
                        MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        return;
                    }

                    id = Convert.ToInt64(row["SubjectID"]);
                }

                string strName = row["SubjectName"].ToString();
                strName = strName.Trim();
                if (strName == "")
                {
                    e.ErrorText = "Необходимо указать наименование субъекта.";
                    e.Valid = false;
                    return;
                }

                SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                connection.Open();
                SQLiteCommand com = new SQLiteCommand(connection);
                com.CommandText = "Select * from Subjects WHERE EQUAL_STR(SubjectName, ?) = 0 AND SubjectID <> ?";
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
                    e.ErrorText = "Субъект с таким наименованием уже существует.";
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
            if (qSubjectsBindingSource.Current != null)
            {
                m_SelectID = Convert.ToInt64(((DataRowView)(qSubjectsBindingSource.Current)).Row["SubjectID"]);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо выбрать запись", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       
    }
}