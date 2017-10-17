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
    public partial class RPNTypeForm : DevExpress.XtraEditors.XtraForm
    {
        bool m_bAcceptChanges = true;
        bool m_bUpdateID = false;
        BindingList<DataSourceString> listYesNo = new BindingList<DataSourceString>();
        public bool m_bCanSelect = false;
        public long m_SelectID = 0;
        public long m_EquipmentKindID = 0;
        bool bAdding = false;

        public RPNTypeForm()
        {
            InitializeComponent();
        }

        private void RPNTypeForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSetMain.EquipmentKindsForTransformator' table. You can move, or remove it, as needed.
            this.equipmentKindsForTransformatorTableAdapter.Fill(this.dataSetMain.EquipmentKindsForTransformator);
            this.qRPNTypesTableAdapter.Fill(this.dataSetQuery.QRPNTypes, m_EquipmentKindID);
            listYesNo.Add(new DataSourceString(0, ""));
            listYesNo.Add(new DataSourceString(1, "да"));
            repYesNo.DataSource = listYesNo;
            repYesNo.DisplayMember = "VAL";
            repYesNo.ValueMember = "KEY";

            if (this.dataSetMain.EquipmentKindsForTransformator.Rows.Count < 7)
                this.repositoryItemLookUpEdit1.DropDownRows = this.dataSetMain.EquipmentKindsForTransformator.Rows.Count;
            else
                this.repositoryItemLookUpEdit1.DropDownRows = 7;

            this.dataSetQuery.QRPNTypes.QRPNTypesRowDeleting += new DataSetQuery.QRPNTypesRowChangeEventHandler(QRPNTypes_QRPNTypesRowDeleting);
            this.dataSetQuery.QRPNTypes.QRPNTypesRowDeleted += new DataSetQuery.QRPNTypesRowChangeEventHandler(QRPNTypes_QRPNTypesRowDeleted);
            this.dataSetQuery.QRPNTypes.QRPNTypesRowChanged += new DataSetQuery.QRPNTypesRowChangeEventHandler(QRPNTypes_QRPNTypesRowChanged);
            GridView.OptionsBehavior.Editable = false;

            if (m_bCanSelect)
            {
                cbCanEdit.Checked = true;
                panelSelect.Visible = true;
                colEquipmentKindID.OptionsColumn.AllowEdit = false;
                colEquipmentKindID.AppearanceCell.BackColor = Color.FromArgb(240, 240, 240);
            }
            else
            {
                panelSelect.Visible = false;
            }
        }

        /*void QRPNTypes_QRPNTypesRowChanging(object sender, DataSetQuery.QRPNTypesRowChangeEvent e)
        {
            //throw new NotImplementedException();
        }*/

        void QRPNTypes_QRPNTypesRowChanged(object sender, DataSetQuery.QRPNTypesRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Add || e.Action == DataRowAction.Change)
            {
                try
                {
                    if (e.Action == DataRowAction.Change && m_bUpdateID)
                    {
                        this.dataSetQuery.QRPNTypes.AcceptChanges();
                        m_bUpdateID = false;
                        return;
                    }
                    else
                        using (var cmdBuilder = new SQLiteCommandBuilder(this.qRPNTypesTableAdapter.Adapter)) this.qRPNTypesTableAdapter.Adapter.Update(this.dataSetQuery.QRPNTypes);

                    if (e.Action == DataRowAction.Add)
                    {
                        SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                        connection.Open();
                        SQLiteCommand com = new SQLiteCommand(connection);
                        com.CommandText = "select seq from sqlite_sequence where name = 'RPNTypes'";
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
                        ((DataRowView)(qRPNTypesBindingSource.Current)).Row["RPNTypeID"] = id;                        
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

        void QRPNTypes_QRPNTypesRowDeleted(object sender, DataSetQuery.QRPNTypesRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Delete)
            {
                if (!m_bAcceptChanges)
                    e.Row.RejectChanges();
                else
                {
                    try
                    {
                        using (var cmdBuilder = new SQLiteCommandBuilder(this.qRPNTypesTableAdapter.Adapter)) this.qRPNTypesTableAdapter.Adapter.Update(this.dataSetQuery.QRPNTypes);
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

        void QRPNTypes_QRPNTypesRowDeleting(object sender, DataSetQuery.QRPNTypesRowChangeEvent e)
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
                        long id = Convert.ToInt64(e.Row["RPNTypeID"]);

                        SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                        connection.Open();
                        SQLiteCommand com = new SQLiteCommand(connection);
                        com.CommandText = "Select COUNT(*) AS Cnt from Equipments WHERE RPNTypeID = ?";
                        com.CommandType = CommandType.Text;
                        SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.Int64);
                        param1.Value = id;
                        com.Parameters.Add(param1);
                        SQLiteDataReader dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            if (Convert.ToInt64(dr["Cnt"]) > 0)
                            {
                                MyLocalizer.XtraMessageBoxShow("Существует оборудование, имеющее РПН данного типа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                m_bAcceptChanges = false;
                                dr.Close();
                                connection.Close();
                                return;
                            }
                        }
                        dr.Close();

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
                if (e.KeyCode == Keys.Delete && qRPNTypesBindingSource.Current != null)
                {
                    ((DataRowView)(qRPNTypesBindingSource.Current)).Row.Delete();
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
            if (qRPNTypesBindingSource.Current != null)
            {
                if (Convert.ToInt64(((DataRowView)(qRPNTypesBindingSource.Current)).Row["ReadOnly"]) != 0)
                {
                    //MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            if (cbCanEdit.Checked && qRPNTypesBindingSource.Current != null)
            {
                if (Convert.ToInt64(((DataRowView)(qRPNTypesBindingSource.Current)).Row["ReadOnly"]) != 0)
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

                if (qRPNTypesBindingSource.Current == null) return;

                DataRowView row = (DataRowView)(qRPNTypesBindingSource.Current);

                bool bNew = row.IsNew;

                if (!bNew)
                {
                    if (Convert.ToInt64(row["ReadOnly"]) != 0)
                    {
                        MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        return;
                    }

                    id = Convert.ToInt64(row["RPNTypeID"]);
                }

                string strName = row["RPNTypeName"].ToString();
                strName = strName.Trim();
                if (strName == "")
                {
                    e.ErrorText = "Необходимо указать наименование типа РПН.";
                    e.Valid = false;
                    return;
                }

                if (row["EquipmentKindID"] == DBNull.Value || Convert.ToInt64(row["EquipmentKindID"]) == 0)
                {
                    e.ErrorText = "Необходимо указать категорию оборудования.";
                    e.Valid = false;
                    return;
                }

                long EquipmentKindID = Convert.ToInt64(row["EquipmentKindID"]);

                SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                connection.Open();
                SQLiteCommand com = new SQLiteCommand(connection);
                com.CommandText = "Select * from RPNTypes WHERE EQUAL_STR(RPNTypeName, ?) = 0 AND EquipmentKindID = ? AND RPNTypeID <> ?";
                com.CommandType = CommandType.Text;
                SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.String);
                param1.Value = strName;
                SQLiteParameter param2 = new SQLiteParameter("@Param2", DbType.Int64);
                param2.Value = EquipmentKindID;
                SQLiteParameter param3 = new SQLiteParameter("@Param3", DbType.Int64);
                param3.Value = id;
                com.Parameters.Add(param1);
                com.Parameters.Add(param2);
                com.Parameters.Add(param3);
                SQLiteDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    e.ErrorText = "Тип РПН с таким наименованием уже существует в указанном виде оборудования.";
                    e.Valid = false;
                    dr.Close();
                    connection.Close();
                    return;
                }
                dr.Close();

                // если меняется вид оборудования, то смотрим на существование оборудования
                if (!bNew)
                {
                    com.Parameters.Clear();
                    com.CommandText = "Select * from Equipments WHERE RPNTypeID = ? AND EquipmentKindID <> ?";
                    com.CommandType = CommandType.Text;
                    SQLiteParameter param1_ = new SQLiteParameter("@Param1", DbType.Int64);
                    param1_.Value = id;
                    SQLiteParameter param2_ = new SQLiteParameter("@Param2", DbType.Int64);
                    param2_.Value = EquipmentKindID;
                    com.Parameters.Add(param1_);
                    com.Parameters.Add(param2_);
                    SQLiteDataReader dr2 = com.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        e.ErrorText = "Существует оборудование, имеющее РПН данного типа.\nКатегорию оборудования у данного типа менять запрещено.";
                        e.Valid = false;
                        dr2.Close();
                        connection.Close();
                        return;
                    }
                    dr2.Close();
                }

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
            if (qRPNTypesBindingSource.Current != null)
            {
                m_SelectID = Convert.ToInt64(((DataRowView)(qRPNTypesBindingSource.Current)).Row["RPNTypeID"]);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо выбрать запись", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void qRPNTypesBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (m_EquipmentKindID > 0)
            {
                if (bAdding) return;
                bAdding = true;
                e.NewObject = (DataRowView)qRPNTypesBindingSource.AddNew();
                DataRowView drv = (DataRowView)(qRPNTypesBindingSource.Current);
                drv.Row["EquipmentKindID"] = m_EquipmentKindID;
                bAdding = false;
            }
            else
            {
                if (this.dataSetMain.EquipmentKindsForTransformator.Count > 0)
                {
                    if (bAdding) return;
                    bAdding = true;
                    e.NewObject = (DataRowView)qRPNTypesBindingSource.AddNew();
                    DataRowView drv = (DataRowView)(qRPNTypesBindingSource.Current);
                    drv.Row["EquipmentKindID"] = this.dataSetMain.EquipmentKindsForTransformator.Rows[0]["EquipmentKindID"];
                    bAdding = false;
                }
            }
        }
    }
}