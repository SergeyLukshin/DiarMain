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
    public partial class InputVoltageTypeForm : DevExpress.XtraEditors.XtraForm
    {
        bool m_bAcceptChanges = true;
        bool m_bUpdateID = false;
        BindingList<DataSourceString> listYesNo = new BindingList<DataSourceString>();
        public bool m_bCanSelect = false;
        public long m_SelectID = 0;
        public long m_EquipmentKindID = 0;
        bool bAdding = false;

        public InputVoltageTypeForm()
        {
            InitializeComponent();
        }

        private void InputVoltageTypeForm_Load(object sender, EventArgs e)
        {
            this.equipmentKindsTableAdapter.Fill(this.dataSetMain.EquipmentKinds);
            this.qInputVoltageTypesTableAdapter.Fill(this.dataSetQuery.QInputVoltageTypes, m_EquipmentKindID);
            listYesNo.Add(new DataSourceString(0, ""));
            listYesNo.Add(new DataSourceString(1, "да"));
            repYesNo.DataSource = listYesNo;
            repYesNo.DisplayMember = "VAL";
            repYesNo.ValueMember = "KEY";

            if (this.dataSetMain.EquipmentKinds.Rows.Count < 7)
                this.repositoryItemLookUpEdit1.DropDownRows = this.dataSetMain.EquipmentKinds.Rows.Count;
            else
                this.repositoryItemLookUpEdit1.DropDownRows = 7;

            this.dataSetQuery.QInputVoltageTypes.QInputVoltageTypesRowDeleting += new DataSetQuery.QInputVoltageTypesRowChangeEventHandler(QInputVoltageTypes_QInputVoltageTypesRowDeleting);
            this.dataSetQuery.QInputVoltageTypes.QInputVoltageTypesRowDeleted += new DataSetQuery.QInputVoltageTypesRowChangeEventHandler(QInputVoltageTypes_QInputVoltageTypesRowDeleted);
            this.dataSetQuery.QInputVoltageTypes.QInputVoltageTypesRowChanged += new DataSetQuery.QInputVoltageTypesRowChangeEventHandler(QInputVoltageTypes_QInputVoltageTypesRowChanged);
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

        /*void InputVoltageTypes_InputVoltageTypesRowChanging(object sender, DataSetQuery.InputVoltageTypesRowChangeEvent e)
        {
            //throw new NotImplementedException();
        }*/

        void QInputVoltageTypes_QInputVoltageTypesRowChanged(object sender, DataSetQuery.QInputVoltageTypesRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Add || e.Action == DataRowAction.Change)
            {
                try
                {
                    if (e.Action == DataRowAction.Change && m_bUpdateID)
                    {
                        this.dataSetQuery.QInputVoltageTypes.AcceptChanges();
                        m_bUpdateID = false;
                        return;
                    }
                    else
                        using (var cmdBuilder = new SQLiteCommandBuilder(this.qInputVoltageTypesTableAdapter.Adapter)) this.qInputVoltageTypesTableAdapter.Adapter.Update(this.dataSetQuery.QInputVoltageTypes);

                    if (e.Action == DataRowAction.Add)
                    {
                        SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                        connection.Open();
                        SQLiteCommand com = new SQLiteCommand(connection);
                        com.CommandText = "select seq from sqlite_sequence where name = 'InputVoltageTypes'";
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
                        ((DataRowView)(qInputVoltageTypesBindingSource.Current)).Row["InputVoltageTypeID"] = id;                        
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

        void QInputVoltageTypes_QInputVoltageTypesRowDeleted(object sender, DataSetQuery.QInputVoltageTypesRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Delete)
            {
                if (!m_bAcceptChanges)
                    e.Row.RejectChanges();
                else
                {
                    try
                    {
                        using (var cmdBuilder = new SQLiteCommandBuilder(this.qInputVoltageTypesTableAdapter.Adapter)) this.qInputVoltageTypesTableAdapter.Adapter.Update(this.dataSetQuery.QInputVoltageTypes);
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

        void QInputVoltageTypes_QInputVoltageTypesRowDeleting(object sender, DataSetQuery.QInputVoltageTypesRowChangeEvent e)
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
                        long id = Convert.ToInt64(e.Row["InputVoltageTypeID"]);

                        SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                        connection.Open();
                        SQLiteCommand com = new SQLiteCommand(connection);
                        /*com.CommandText = "Select COUNT(*) AS Cnt from Equipments WHERE InputVoltageTypeID = ?";
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
                        dr.Close();*/

                        com.CommandText = "Select COUNT(*) AS Cnt from Inputs AS i WHERE i.InputTypeID = ?";
                        SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.Int64);
                        param1.Value = id;
                        com.Parameters.Add(param1);
                        SQLiteDataReader dr2 = com.ExecuteReader();
                        while (dr2.Read())
                        {
                            if (Convert.ToInt64(dr2["Cnt"]) > 0)
                            {
                                MyLocalizer.XtraMessageBoxShow("Существуют вводы данного типа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                m_bAcceptChanges = false;
                                dr2.Close();
                                connection.Close();
                                return;
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
                if (e.KeyCode == Keys.Delete && qInputVoltageTypesBindingSource.Current != null)
                {
                    ((DataRowView)(qInputVoltageTypesBindingSource.Current)).Row.Delete();
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
            if (qInputVoltageTypesBindingSource.Current != null)
            {
                if (Convert.ToInt64(((DataRowView)(qInputVoltageTypesBindingSource.Current)).Row["ReadOnly"]) != 0)
                {
                    //MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            if (cbCanEdit.Checked && qInputVoltageTypesBindingSource.Current != null)
            {
                if (Convert.ToInt64(((DataRowView)(qInputVoltageTypesBindingSource.Current)).Row["ReadOnly"]) != 0)
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

                if (qInputVoltageTypesBindingSource.Current == null) return;

                DataRowView row = (DataRowView)(qInputVoltageTypesBindingSource.Current);

                bool bNew = row.IsNew;

                if (!bNew)
                {
                    if (Convert.ToInt64(row["ReadOnly"]) != 0)
                    {
                        MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        return;
                    }

                    id = Convert.ToInt64(row["InputVoltageTypeID"]);
                }

                string strName = row["InputVoltageTypeName"].ToString();
                strName = strName.Trim();
                if (strName == "")
                {
                    e.ErrorText = "Необходимо указать наименование типа ввода.";
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
                com.CommandText = "Select * from InputVoltageTypes WHERE EQUAL_STR(InputVoltageTypeName, ?) = 0 AND EquipmentKindID = ? AND InputVoltageTypeID <> ?";
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
                    e.ErrorText = "Тип ввода с таким наименованием уже существует в указанном виде оборудования.";
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
                    SQLiteParameter param1_ = new SQLiteParameter("@input_type_id", DbType.Int64);
                    param1_.Value = id;
                    SQLiteParameter param2_ = new SQLiteParameter("@kind_id", DbType.Int64);
                    param2_.Value = EquipmentKindID;
                    com.Parameters.Add(param1_);
                    com.Parameters.Add(param2_);
                    /*com.CommandText = "SELECT COUNT(*) AS Cnt FROM Equipments WHERE (InputIDHighA IN (Select InputID AS Cnt from Inputs AS i WHERE i.InputTypeID = @input_id) OR " +
                        " InputIDHighB IN (Select InputID AS Cnt from Inputs AS i WHERE i.InputTypeID = @input_id) OR " + 
                        " InputIDHighC IN (Select InputID AS Cnt from Inputs AS i WHERE i.InputTypeID = @input_id) OR " +
                        " InputIDMiddleA IN (Select InputID AS Cnt from Inputs AS i WHERE i.InputTypeID = @input_id) OR " +
                        " InputIDMiddleB IN (Select InputID AS Cnt from Inputs AS i WHERE i.InputTypeID = @input_id) OR " +
                        " InputIDMiddleC IN (Select InputID AS Cnt from Inputs AS i WHERE i.InputTypeID = @input_id) OR " +
                        " InputIDNeutral IN (Select InputID AS Cnt from Inputs AS i WHERE i.InputTypeID = @input_id))" +
                        " AND EquipmentKindID <> @kind_id ";*/
                    com.CommandText = "Select i.InputID from Inputs AS i INNER JOIN Equipments AS e ON " +
                        "e.InputIDHighA = i.InputID OR e.InputIDHighB = i.InputID OR e.InputIDHighC = i.InputID OR e.InputIDMiddleA = i.InputID OR e.InputIDMiddleB = i.InputID OR e.InputIDMiddleC = i.InputID OR e.InputIDNeutral = i.InputID " +
                        "WHERE i.InputTypeID = @input_type_id AND e.EquipmentKindID <> @kind_id";

                    /*com.CommandText = "SELECT COUNT(*) AS Cnt FROM Equipments "
                        + "INNER JOIN (Select InputID from Inputs AS i WHERE i.InputTypeID = @input_type_id) AS tmp ON (tmp.InputID = InputIDHighA OR " + 
                        "tmp.InputID = InputIDHighB OR tmp.InputID = InputIDHighC OR tmp.InputID = InputIDMiddleA OR tmp.InputID = InputIDMiddleB OR " + 
                        "tmp.InputID = InputIDMiddleC OR tmp.InputID = InputIDNeutral) AND EquipmentKindID <> @kind_id";*/
                    SQLiteDataReader dr2 = com.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        e.ErrorText = "Существует оборудование, имеющее вводы данного типа.\nКатегорию оборудования у данного типа менять запрещено.";
                        e.Valid = false;
                        dr2.Close();
                        connection.Close();
                        return;
                    }

                    /*while (dr2.Read())
                    {
                        if (Convert.ToInt64(dr2["Cnt"]) > 0)
                        {
                            MyLocalizer.XtraMessageBoxShow("Существует оборудование, имеющее вводы данного типа.\nКатегорию оборудования у данного типа менять запрещено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            m_bAcceptChanges = false;
                            dr2.Close();
                            connection.Close();
                            return;
                        }
                    }*/
                    dr2.Close();

                    /*com.Parameters.Clear();
                    com.CommandText = "Select * from Equipments WHERE InputVoltageTypeID = ? AND EquipmentKindID <> ?";
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
                    dr2.Close();*/
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
            if (qInputVoltageTypesBindingSource.Current != null)
            {
                m_SelectID = Convert.ToInt64(((DataRowView)(qInputVoltageTypesBindingSource.Current)).Row["InputVoltageTypeID"]);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо выбрать запись", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void qInputVoltageTypesBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (m_EquipmentKindID > 0)
            {
                if (bAdding) return;
                bAdding = true;
                e.NewObject = (DataRowView)qInputVoltageTypesBindingSource.AddNew();
                DataRowView drv = (DataRowView)(qInputVoltageTypesBindingSource.Current);
                drv.Row["EquipmentKindID"] = m_EquipmentKindID;
                bAdding = false;
            }
            else
            {
                if (this.dataSetMain.EquipmentKinds.Count > 0)
                {
                    if (bAdding) return;
                    bAdding = true;
                    e.NewObject = (DataRowView)qInputVoltageTypesBindingSource.AddNew();
                    DataRowView drv = (DataRowView)(qInputVoltageTypesBindingSource.Current);
                    drv.Row["EquipmentKindID"] = this.dataSetMain.EquipmentKinds.Rows[0]["EquipmentKindID"];
                    bAdding = false;
                }
            }
        }
    }
}