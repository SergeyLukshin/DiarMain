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
    public partial class EquipmentForm : DevExpress.XtraEditors.XtraForm
    {
        //bool m_bAcceptChanges = true;
        //bool m_bUpdateID = false;
        int m_cnWidth = 0;

        public EquipmentForm()
        {
            InitializeComponent();
        }

        private void EquipmentForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSetMain.EquipmentKinds". При необходимости она может быть перемещена или удалена.
            this.qEquipmentsTableAdapter.Fill(this.dataSetQuery.QEquipments);

            ControlNavigatorButtons cnb = controlNavigator1.Buttons;
            ControlNavigatorButtons cnb2 = controlNavigator2.Buttons;
            if (GridView.FocusedRowHandle < 0)
            {
                cnb.CustomButtons[0].Enabled = false;// cnb.Remove.Enabled;

                /*for (int i = 0; i < cnb2.CustomButtons.Count; i++)
                {
                    cnb2.CustomButtons[i].Enabled = false;
                }*/
            }
            else
            {
                cnb.CustomButtons[0].Enabled = true;

                /*for (int i = 0; i < cnb2.CustomButtons.Count; i++)
                {
                    cnb2.CustomButtons[i].Enabled = true;
                }*/
            }

            m_cnWidth = controlNavigator2.Size.Width / 6;
        }

        private void GridViewView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteRecord();
            }

            if (e.KeyCode == Keys.Enter)
            {
                UpdateRecord();
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
            if (Convert.ToInt64(((DataRowView)(qEquipmentsBindingSource.Current)).Row["ReadOnly"]) != 0)
            {
                //MyLocalizer.XtraMessageBoxShow("Недостаточно прав для редактирования записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            UpdateRecord();
        }

        private void RefreshGridPos(long id)
        {
            int f_row = GridView.FocusedRowHandle;
            if (id <= 0)
            {
                if (f_row > 0) f_row--;
                this.qEquipmentsTableAdapter.Fill(this.dataSetQuery.QEquipments);
                if (GridView.RowCount > f_row)
                {
                    GridView.ClearSelection();
                    GridView.SelectRow(f_row);
                    GridView.FocusedRowHandle = f_row;
                }
            }
            else
            {
                this.qEquipmentsTableAdapter.Fill(this.dataSetQuery.QEquipments);

                for (int i = 0; i < GridView.RowCount/*this.dataSetQuery.QEquipments.Rows.Count*/; i++)
                {
                    //DataRow r = this.dataSetQuery.QEquipments.Rows[i];
                    //int id_ = Convert.ToInt64(r["EquipmentID"]);
                    long id_ = Convert.ToInt64(GridView.GetRowCellValue(i, "EquipmentID"));
                    if (id_ == id)
                    {
                        GridView.ClearSelection();
                        GridView.SelectRow(i);
                        GridView.FocusedRowHandle = i;
                        return;
                    }
                }
            }
        }

        private void DeleteRecord()
        {
            try
            {
                if (GridView.FocusedRowHandle < 0) return;

                DataRowView drv = (DataRowView)(qEquipmentsBindingSource.Current);

                if (Convert.ToInt64(drv.Row["ReadOnly"]) != 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Недостаточно прав для удаления записи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MyLocalizer.XtraMessageBoxShow("Удалить запись? Все обследования, связанные с данным оборудованием будут удалены.", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    long id = Convert.ToInt64(drv.Row["EquipmentID"]);

                    SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                    connection.Open();
                    SQLiteCommand com = new SQLiteCommand(connection);
                    com.CommandType = CommandType.Text;

                    SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.Int64);
                    param1.Value = id;
                    com.Parameters.Add(param1);

                    com.CommandText = "DELETE FROM SystemItemResults WHERE InspectionID IN (SELECT InspectionID FROM Inspections WHERE EquipmentID = ?)";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM FunctionalSystemResults WHERE InspectionID IN (SELECT InspectionID FROM Inspections WHERE EquipmentID = ?)";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM InspectionDatas WHERE InspectionID IN (SELECT InspectionID FROM Inspections WHERE EquipmentID = ?)";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM Inspections WHERE EquipmentID = ?";
                    com.ExecuteNonQuery();

                    // удаляем вводы
                    com.CommandText = "SELECT InputIDHighA, InputIDHighB, InputIDHighC, InputIDMiddleA, InputIDMiddleB, InputIDMiddleC, InputIDNeutral FROM Equipments WHERE EquipmentID = ?";
                    SQLiteDataReader drInputs = com.ExecuteReader();
                    string strIDs = "";
                    if (drInputs.HasRows)
                    {
                        while (drInputs.Read())
                        {
                            if (drInputs["InputIDHighA"] != null && drInputs["InputIDHighA"] != DBNull.Value)
                            {
                                if (strIDs == "") strIDs = drInputs["InputIDHighA"].ToString();
                                else strIDs = strIDs + "," + drInputs["InputIDHighA"].ToString();
                            }
                            if (drInputs["InputIDHighB"] != null && drInputs["InputIDHighB"] != DBNull.Value)
                            {
                                if (strIDs == "") strIDs = drInputs["InputIDHighB"].ToString();
                                else strIDs = strIDs + "," + drInputs["InputIDHighB"].ToString();
                            }
                            if (drInputs["InputIDHighC"] != null && drInputs["InputIDHighC"] != DBNull.Value)
                            {
                                if (strIDs == "") strIDs = drInputs["InputIDHighC"].ToString();
                                else strIDs = strIDs + "," + drInputs["InputIDHighC"].ToString();
                            }
                            if (drInputs["InputIDMiddleA"] != null && drInputs["InputIDMiddleA"] != DBNull.Value)
                            {
                                if (strIDs == "") strIDs = drInputs["InputIDMiddleA"].ToString();
                                else strIDs = strIDs + "," + drInputs["InputIDMiddleA"].ToString();
                            }
                            if (drInputs["InputIDMiddleB"] != null && drInputs["InputIDMiddleB"] != DBNull.Value)
                            {
                                if (strIDs == "") strIDs = drInputs["InputIDMiddleB"].ToString();
                                else strIDs = strIDs + "," + drInputs["InputIDMiddleB"].ToString();
                            }
                            if (drInputs["InputIDMiddleC"] != null && drInputs["InputIDMiddleC"] != DBNull.Value)
                            {
                                if (strIDs == "") strIDs = drInputs["InputIDMiddleC"].ToString();
                                else strIDs = strIDs + "," + drInputs["InputIDMiddleC"].ToString();
                            }
                            if (drInputs["InputIDNeutral"] != null && drInputs["InputIDNeutral"] != DBNull.Value)
                            {
                                if (strIDs == "") strIDs = drInputs["InputIDNeutral"].ToString();
                                else strIDs = strIDs + "," + drInputs["InputIDNeutral"].ToString();
                            }
                        }

                    }
                    drInputs.Close();

                    com.CommandText = "DELETE FROM Equipments WHERE EquipmentID = ?";
                    com.ExecuteNonQuery();

                    if (strIDs != "")
                    {
                        com.CommandText = "DELETE FROM Inputs WHERE InputID IN (" + strIDs + ")";
                        com.ExecuteNonQuery();
                    }

                    connection.Close();

                    RefreshGridPos(-1);
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

        public void InsertRecord()
        {
            //if (GridView.FocusedRowHandle < 0) return;

            long id = -1;

            PassportDataForm rf = new PassportDataForm(id);
            DialogResult dr = rf.ShowDialog(this);
            id = rf.m_id;
            if (dr == System.Windows.Forms.DialogResult.OK)
                RefreshGridPos(id);
        }

        public void UpdateRecord()
        {
            if (GridView.FocusedRowHandle < 0) return;

            DataRowView drv = (DataRowView)(qEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            PassportDataForm rf = new PassportDataForm(id);
            DialogResult dr = rf.ShowDialog(this);
            if (dr == System.Windows.Forms.DialogResult.OK)
                RefreshGridPos(id);
        }

        /*public DialogResult InsertRecord(ref int id)
        {
            DialogResult dr = System.Windows.Forms.DialogResult.OK;
            return dr;
        }*/

        private void controlNavigator1_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Append)
            {
                e.Handled = true;
                InsertRecord();
                return;
            }
            if (e.Button.ButtonType == NavigatorButtonType.Custom)
            {
                e.Handled = true;
                UpdateRecord();
                return;
            }
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                e.Handled = true;
                DeleteRecord();
                return;
            }
        }

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ControlNavigatorButtons cnb = controlNavigator1.Buttons;
            ControlNavigatorButtons cnb2 = controlNavigator2.Buttons;
            if (e.FocusedRowHandle < 0)
            {
                cnb.CustomButtons[0].Enabled = false;// cnb.Remove.Enabled;

                controlNavigator2.Visible = false;
                /*for (int i = 0; i < cnb2.CustomButtons.Count; i++)
                {
                    cnb2.CustomButtons[i].Enabled = false;
                }*/
            }
            else
            {
                cnb.CustomButtons[0].Enabled = true;

                // проверяем существование обследований для типа
                Equipment.EquipmentKind EquipmentKindID = (Equipment.EquipmentKind)Convert.ToInt64(GridView.GetRowCellValue(GridView.FocusedRowHandle, "EquipmentKindID"));
                controlNavigator2.Visible = true;

                for (int i = 0; i < cnb2.CustomButtons.Count; i++)
                {
                    Inspection.InspectionType type = Inspection.InspectionType.Visual;
                    if (cnb2.CustomButtons[i].Tag.ToString() == "Visual")
                        type = Inspection.InspectionType.Visual;
                    if (cnb2.CustomButtons[i].Tag.ToString() == "FHA")
                        type = Inspection.InspectionType.FHA;
                    if (cnb2.CustomButtons[i].Tag.ToString() == "HARG")
                        type = Inspection.InspectionType.HARG;
                    if (cnb2.CustomButtons[i].Tag.ToString() == "Warm")
                        type = Inspection.InspectionType.Warm;
                    if (cnb2.CustomButtons[i].Tag.ToString() == "Vibro")
                        type = Inspection.InspectionType.Vibro;
                    if (cnb2.CustomButtons[i].Tag.ToString() == "Parameter")
                        type = Inspection.InspectionType.Parameter;
                    if (cnb2.CustomButtons[i].Tag.ToString() == "Electrical")
                        type = Inspection.InspectionType.Electrical;

                    if (Inspection.m_listEquipmentInspections[EquipmentKindID].IndexOf(type) >= 0)
                        cnb2.CustomButtons[i].Visible = true;
                    else
                        cnb2.CustomButtons[i].Visible = false;
                }

                controlNavigator2.Width = Inspection.m_listEquipmentInspections[EquipmentKindID].Count * m_cnWidth;
            }
        }

        private void controlNavigator1_Click(object sender, EventArgs e)
        {

        }

        private void controlNavigator2_Click(object sender, EventArgs e)
        {

        }

        private void controlNavigator2_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Custom)
            {
                e.Handled = true;

                if (GridView.FocusedRowHandle < 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView drv = (DataRowView)(qEquipmentsBindingSource.Current);
                long id = Convert.ToInt64(drv.Row["EquipmentID"]);

                if (e.Button.Tag.ToString() == "Visual")
                {
                    InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Visual);
                    form.ShowDialog(this);
                }
                if (e.Button.Tag.ToString() == "FHA")
                {
                    InspectionForm form = new InspectionForm(id, Inspection.InspectionType.FHA);
                    form.ShowDialog(this);
                }
                if (e.Button.Tag.ToString() == "HARG")
                {
                    InspectionForm form = new InspectionForm(id, Inspection.InspectionType.HARG);
                    form.ShowDialog(this);
                }
                if (e.Button.Tag.ToString() == "Warm")
                {
                    InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Warm);
                    form.ShowDialog(this);
                }
                if (e.Button.Tag.ToString() == "Vibro")
                {
                    InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Vibro);
                    form.ShowDialog(this);
                }
                if (e.Button.Tag.ToString() == "Parameter")
                {
                    InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Parameter);
                    form.ShowDialog(this);
                }
                if (e.Button.Tag.ToString() == "Electrical")
                {
                    InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Electrical);
                    form.ShowDialog(this);
                }

                return;
            }
        }

        private void GridView_ShowFilterPopupListBox(object sender, DevExpress.XtraGrid.Views.Grid.FilterPopupListBoxEventArgs e)
        {
            //GridView gv = sender as GridView;
            string custom = DevExpress.XtraGrid.Localization.GridLocalizer.Active.GetLocalizedString(DevExpress.XtraGrid.Localization.GridStringId.PopupFilterCustom);
            string blank = DevExpress.XtraGrid.Localization.GridLocalizer.Active.GetLocalizedString(DevExpress.XtraGrid.Localization.GridStringId.PopupFilterBlanks);
            string not_blank = DevExpress.XtraGrid.Localization.GridLocalizer.Active.GetLocalizedString(DevExpress.XtraGrid.Localization.GridStringId.PopupFilterNonBlanks);
            for (int i = e.ComboBox.Items.Count - 1; i >= 0; i--)
            {
                if (e.ComboBox.Items[i].ToString() == custom)
                {
                    e.ComboBox.Items.RemoveAt(i);
                    continue;
                }
                if (e.ComboBox.Items[i].ToString() == blank)
                {
                    e.ComboBox.Items.RemoveAt(i);
                    continue;
                }
                if (e.ComboBox.Items[i].ToString() == not_blank)
                {
                    e.ComboBox.Items.RemoveAt(i);
                    continue;
                }
            }
        }
    }
}