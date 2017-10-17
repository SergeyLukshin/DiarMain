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
    public partial class CheckForm : DevExpress.XtraEditors.XtraForm
    {
        //bool m_bAcceptChanges = true;
        //bool m_bUpdateID = false;

        public CheckForm()
        {
            InitializeComponent();
        }

        private void CheckForm_Load(object sender, EventArgs e)
        {
            this.qChecksTableAdapter.Fill(this.dataSetQuery.QChecks);

            ControlNavigatorButtons cnb = controlNavigator1.Buttons;
            if (GridView.FocusedRowHandle < 0)
            {
                cnb.CustomButtons[0].Enabled = false;// cnb.Remove.Enabled;
            }
            else
            {
                cnb.CustomButtons[0].Enabled = true;
            }
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
                this.qChecksTableAdapter.Fill(this.dataSetQuery.QChecks);
                if (GridView.RowCount > f_row)
                {
                    GridView.ClearSelection();
                    GridView.SelectRow(f_row);
                    GridView.FocusedRowHandle = f_row;
                }
            }
            else
            {
                this.qChecksTableAdapter.Fill(this.dataSetQuery.QChecks);

                for (int i = 0; i < GridView.RowCount; i++)
                {
                    long id_ = Convert.ToInt64(GridView.GetRowCellValue(i, "CheckID"));
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

                DataRowView drv = (DataRowView)(qChecksBindingSource.Current);

                if (MyLocalizer.XtraMessageBoxShow("Удалить запись? Все обследования, связанные с данной проверкой будут удалены.", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    long id = Convert.ToInt64(drv.Row["CheckID"]);

                    SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                    connection.Open();
                    SQLiteCommand com = new SQLiteCommand(connection);
                    com.CommandType = CommandType.Text;

                    SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.Int64);
                    param1.Value = id;
                    com.Parameters.Add(param1);

                    com.CommandText = "DELETE FROM SystemItemResults WHERE InspectionID IN (SELECT InspectionID FROM Inspections WHERE CheckID = ?)";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM FunctionalSystemResults WHERE InspectionID IN (SELECT InspectionID FROM Inspections WHERE CheckID = ?)";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM InspectionDatas WHERE InspectionID IN (SELECT InspectionID FROM Inspections WHERE CheckID = ?)";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM Inspections WHERE CheckID = ?";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM Checks WHERE CheckID = ?";
                    com.ExecuteNonQuery();

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

            CheckDataForm cf = new CheckDataForm(id, -1, -1, -1);
            DialogResult dr = cf.ShowDialog(this);
            id = cf.m_CheckID;
            RefreshGridPos(id);
        }

        public void UpdateRecord()
        {
            if (GridView.FocusedRowHandle < 0) return;

            DataRowView drv = (DataRowView)(qChecksBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["CheckID"]);

            CheckDataForm cf = new CheckDataForm(id, -1, -1, -1);
            DialogResult dr = cf.ShowDialog(this);
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
            if (e.FocusedRowHandle < 0)
            {
                cnb.CustomButtons[0].Enabled = false;// cnb.Remove.Enabled;
            }
            else
            {
                cnb.CustomButtons[0].Enabled = true;
            }
        }

        private void GridView_ShowFilterPopupListBox(object sender, DevExpress.XtraGrid.Views.Grid.FilterPopupListBoxEventArgs e)
        {
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

        private void controlNavigator1_Click(object sender, EventArgs e)
        {

        }
    }
}