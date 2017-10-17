using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SQLite;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
//using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Drawing;
using System.ComponentModel;

namespace DiarMain
{
    public partial class CheckDataForm : DevExpress.XtraEditors.XtraForm
    {
        public long m_CheckID = -1;
        public long m_SubjectID = -1;
        public long m_BranchID = -1;
        public long m_SubstationID = -1;
        SQLiteConnection m_con = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);

        Dictionary<long, Dictionary<long, Inspection.RangeResult>> m_Data = new Dictionary<long, Dictionary<long, Inspection.RangeResult>>();
        List<KeyValuePair<Point, Point>> m_listButtonsPoints = new List<KeyValuePair<Point, Point>>();
        //BindingList<DataSourceString> m_listEquipmentKinds = new BindingList<DataSourceString>();


        bool m_bDataLoadEnd = false;
        bool m_bChangeData = false;

        int m_singleLineHeight = 0;
        int m_singleLineDiff = 0;

        public CheckDataForm(long checkID, long subjectID, long branchID, long substationID)
        {
            m_CheckID = checkID;
            m_SubjectID = subjectID;
            m_BranchID = branchID;
            m_SubstationID = substationID;
            InitializeComponent();
        }

        private void AddParam(SQLiteCommand com, string name, DbType type, object value)
        {
            SQLiteParameter param = new SQLiteParameter(name, type);
            param.Value = value;
            com.Parameters.Add(param);
        }

        private void CreateGrid()
        {
            Graphics formGraphics = this.CreateGraphics();

            GridViewInfo viewInfo = GridView.GetViewInfo() as GridViewInfo;
            m_singleLineHeight = viewInfo.ColumnRowHeight;
            int single_height = GetColumnBestHeight("Ww", 1000);
            m_singleLineDiff = m_singleLineHeight - single_height;
            m_singleLineHeight = single_height;


            for (int i = 0; i < GridView.Columns.Count; i++)
            {
                GridView.Columns[i].Width = (int)(GridView.Columns[i].Width * formGraphics.DpiX / 96);
            }


            GridView.BeginUpdate();

            foreach (KeyValuePair<Inspection.InspectionType, KeyValuePair<string, string>> pairInsp in Inspection.m_dictInspections)
            {
                foreach (KeyValuePair<Equipment.EquipmentKind, string> pair in Inspection.m_dictActualEquipmentKinds)
                {
                    if (Inspection.m_listEquipmentInspections.ContainsKey(pair.Key))
                    {
                        if (Inspection.m_listEquipmentInspections[pair.Key].IndexOf(pairInsp.Key) >= 0)
                        {
                            DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
                            col.Caption = pairInsp.Value.Key;
                            col.Name = pairInsp.Value.Value;
                            col.Width = (int)(150 * formGraphics.DpiX / 96);
                            col.OptionsColumn.AllowMove = false;
                            col.OptionsColumn.AllowEdit = false;
                            col.OptionsFilter.AllowFilter = false;
                            col.AppearanceHeader.Options.UseTextOptions = true;
                            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                            col.Visible = true;
                            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
                            col.VisibleIndex = GridView.Columns.Count;
                            GridView.Columns.Add(col);

                            break;
                        }
                    }
                }
            }

            /*DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
            col.Caption = "Визуальное обследование";
            col.Name = "Visual";
            col.Width = (int)(150 * formGraphics.DpiX / 96);
            col.OptionsColumn.AllowMove = false;
            col.OptionsColumn.AllowEdit = false;
            col.OptionsFilter.AllowFilter = false;
            col.AppearanceHeader.Options.UseTextOptions = true;
            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            col.Visible = true;
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            col.VisibleIndex = GridView.Columns.Count;
            GridView.Columns.Add(col);

            col = new DevExpress.XtraGrid.Columns.GridColumn();
            col.Caption = "ФХА";
            col.Name = "FHA";
            col.Width = (int)(150 * formGraphics.DpiX / 96);
            col.OptionsColumn.AllowMove = false;
            col.OptionsColumn.AllowEdit = false;
            col.OptionsFilter.AllowFilter = false;
            col.AppearanceHeader.Options.UseTextOptions = true;
            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            col.Visible = true;
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            col.VisibleIndex = GridView.Columns.Count;
            GridView.Columns.Add(col);

            col = new DevExpress.XtraGrid.Columns.GridColumn();
            col.Caption = "ХАРГ";
            col.Name = "HARG";
            col.Width = (int)(150 * formGraphics.DpiX / 96);
            col.OptionsColumn.AllowMove = false;
            col.OptionsColumn.AllowEdit = false;
            col.OptionsFilter.AllowFilter = false;
            col.AppearanceHeader.Options.UseTextOptions = true;
            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            col.Visible = true;
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            col.VisibleIndex = GridView.Columns.Count;
            GridView.Columns.Add(col);

            col = new DevExpress.XtraGrid.Columns.GridColumn();
            col.Caption = "Тепловизионный контроль";
            col.Name = "Warm";
            col.Width = (int)(150 * formGraphics.DpiX / 96);
            col.OptionsColumn.AllowMove = false;
            col.OptionsColumn.AllowEdit = false;
            col.OptionsFilter.AllowFilter = false;
            col.AppearanceHeader.Options.UseTextOptions = true;
            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            col.Visible = true;
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            col.VisibleIndex = GridView.Columns.Count;
            GridView.Columns.Add(col);

            col = new DevExpress.XtraGrid.Columns.GridColumn();
            col.Caption = "Вибрационное обследование";
            col.Name = "Vibro";
            col.Width = (int)(160 * formGraphics.DpiX / 96);
            col.OptionsColumn.AllowMove = false;
            col.OptionsColumn.AllowEdit = false;
            col.OptionsFilter.AllowFilter = false;
            col.AppearanceHeader.Options.UseTextOptions = true;
            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            col.Visible = true;
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            col.VisibleIndex = GridView.Columns.Count;
            GridView.Columns.Add(col);

            col = new DevExpress.XtraGrid.Columns.GridColumn();
            col.Caption = "Определение характеристик";
            col.Name = "Parameter";
            col.Width = (int)(150 * formGraphics.DpiX / 96);
            col.OptionsColumn.AllowMove = false;
            col.OptionsColumn.AllowEdit = false;
            col.OptionsFilter.AllowFilter = false;
            col.AppearanceHeader.Options.UseTextOptions = true;
            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            col.Visible = true;
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            col.VisibleIndex = GridView.Columns.Count;
            GridView.Columns.Add(col);*/

            DevExpress.XtraGrid.Columns.GridColumn colResult = new DevExpress.XtraGrid.Columns.GridColumn();
            colResult.Caption = "Результат";
            colResult.Name = "Result";
            colResult.Width = (int)(100 * formGraphics.DpiX / 96);
            colResult.OptionsColumn.AllowMove = false;
            colResult.OptionsColumn.AllowEdit = false;
            colResult.OptionsFilter.AllowFilter = false;
            colResult.AppearanceHeader.Options.UseTextOptions = true;
            colResult.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colResult.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            colResult.Visible = true;
            colResult.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            colResult.VisibleIndex = GridView.Columns.Count;
            GridView.Columns.Add(colResult);

            int max_row = -1;
            for (int i = 0; i < GridView.Columns.Count; i++)
            {
                int height = GetColumnBestHeight(GridView.Columns[i], GridView.Columns[i].Width);
                int row = height / m_singleLineHeight;

                if (row > max_row) max_row = row;
            }

            GridView.ColumnPanelRowHeight = (max_row) * m_singleLineHeight + m_singleLineDiff;

            GridView.EndUpdate();

            if (Inspection.m_dictActualEquipmentKinds.Count <= 1)
            {
                pBlack.Visible = false;
                lBlack.Visible = false;
            }
        }

        private void RefreshData()
        {
            try
            {
                long iSubstationID = -1;
                if (cbSubstation.EditValue != null) iSubstationID = (long)cbSubstation.EditValue;

                m_con.Open();

                SQLiteCommand com = new SQLiteCommand(m_con);
                com.CommandType = CommandType.Text;

                com.CommandText = "SELECT MAX(fsr.Result) AS MaxResult, i.EquipmentID, i.InspectionType " + 
                    "FROM FunctionalSystemResults AS fsr " + 
                    "INNER JOIN Inspections AS i ON i.InspectionID = fsr.InspectionID " + 
                    "WHERE CheckID = @cid GROUP BY i.EquipmentID, i.InspectionType";

                com.Parameters.Clear();
                AddParam(com, "@cid", DbType.Int64, (long)m_CheckID);

                m_Data.Clear();
                SQLiteDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    cbBranch.Properties.ReadOnly = true;
                    cbBranch.Properties.Buttons[1].Enabled = false;
                    cbSubject.Properties.ReadOnly = true;
                    cbSubject.Properties.Buttons[1].Enabled = false;
                    cbSubstation.Properties.ReadOnly = true;
                    cbSubstation.Properties.Buttons[1].Enabled = false;

                    while (dr.Read())
                    {
                        long MaxResult = Convert.ToInt64(dr["MaxResult"]);
                        long EquipmentID = Convert.ToInt64(dr["EquipmentID"]);
                        long InspectionType = Convert.ToInt64(dr["InspectionType"]);

                        if (!m_Data.ContainsKey(EquipmentID))
                        {
                            m_Data[EquipmentID] = new Dictionary<long, Inspection.RangeResult>();
                            m_Data[EquipmentID][-1] = (Inspection.RangeResult)MaxResult;
                        }
                        else
                        {
                            if (m_Data[EquipmentID][-1] < (Inspection.RangeResult)MaxResult)
                                m_Data[EquipmentID][-1] = (Inspection.RangeResult)MaxResult;
                        }
                        m_Data[EquipmentID][InspectionType] = (Inspection.RangeResult)MaxResult;
                    }
                }
                dr.Close();

                m_con.Close();
            }
            catch (SQLiteException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DBConcurrencyException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckDataForm_Load(object sender, EventArgs e)
        {
            pGreen.BackColor = Color.Green;
            pYellow.BackColor = Color.Yellow;
            pOrange.BackColor = Color.Orange;
            pRed.BackColor = Color.Red;
            pGray.BackColor = Color.DarkGray;
            pBlack.BackColor = Color.Black;

            /*m_listEquipmentKinds.Add(new DataSourceString(0, "Все"));

            foreach (KeyValuePair<Equipment.EquipmentKind, string> pair in Inspection.m_dictActualEquipmentKinds)
            {
                m_listEquipmentKinds.Add(new DataSourceString((long)pair.Key, pair.Value));
            }

            cbEquipmentKind.Properties.DataSource = m_listEquipmentKinds;
            cbEquipmentKind.EditValue = (long)0;
            cbEquipmentKind.Properties.DropDownRows = m_listEquipmentKinds.Count;

            if (m_listEquipmentKinds.Count <= 2) panelEquipmentKind.Visible = false;*/

            // TODO: This line of code loads data into the 'dataSetQuery.QSubjects' table. You can move, or remove it, as needed.
            this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);
            btnHARG.LookAndFeel.SetSkinStyle("MySkin_StyleHARG");
            btnFHA.LookAndFeel.SetSkinStyle("MySkin_StyleFHA");
            btnVisual.LookAndFeel.SetSkinStyle("MySkin_StyleVisual");
            btnWarm.LookAndFeel.SetSkinStyle("MySkin_StyleWarm");
            btnVibro.LookAndFeel.SetSkinStyle("MySkin_StyleVibro");
            btnParameter.LookAndFeel.SetSkinStyle("MySkin_StyleParameter");
            btnElectrical.LookAndFeel.SetSkinStyle("MySkin_StyleElectrical");
            //btnProtocol.LookAndFeel.SetSkinStyle("MySkin_StyleReport");
            //btnPassportAdd.LookAndFeel.SetSkinStyle("MySkin_StyleAdd2");

            if (m_CheckID <= 0) Text = "Добавить данные по проверке";
            else Text = "Изменить данные по проверке";

            SQLiteConnection con = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
            try
            {
                con.Open();
                con.Close();

                this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);
                this.qBranchesBySubjectTableAdapter.Fill(this.dataSetQuery.QBranchesBySubject, -1);
                this.qSubstationsByBranchTableAdapter.Fill(this.dataSetQuery.QSubstationsByBranch, -1);

                if (this.dataSetQuery.QSubjects.Rows.Count < 7)
                    cbSubject.Properties.DropDownRows = this.dataSetQuery.QSubjects.Rows.Count;
                else
                    cbSubject.Properties.DropDownRows = 7;

                if (this.dataSetQuery.QBranchesBySubject.Rows.Count < 7)
                    cbBranch.Properties.DropDownRows = this.dataSetQuery.QBranchesBySubject.Rows.Count;
                else
                    cbBranch.Properties.DropDownRows = 7;

                if (this.dataSetQuery.QSubstationsByBranch.Rows.Count < 7)
                    cbSubstation.Properties.DropDownRows = this.dataSetQuery.QSubstationsByBranch.Rows.Count;
                else
                    cbSubstation.Properties.DropDownRows = 7;

                if (m_CheckID > 0)
                {
                    try
                    {
                        SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                        connection.Open();
                        SQLiteCommand com = new SQLiteCommand(connection);

                        // проверка на пересечение дат в этом расположении
                        com.CommandText = "SELECT c.*, b.BranchID, b.SubjectID FROM Checks AS c INNER JOIN Substations AS s ON s.SubstationID = c.SubstationID " +
                            "INNER JOIN Branches AS b ON b.BranchID = s.BranchID WHERE CheckID = @id";
                        com.CommandType = CommandType.Text;
                        com.Parameters.Clear();
                        AddParam(com, "@id", DbType.Int64, m_CheckID);

                        SQLiteDataReader dr = com.ExecuteReader();
                        if (dr.HasRows)
                        {
                            m_SubjectID = Convert.ToInt64(dr["SubjectID"]);
                            m_BranchID = Convert.ToInt64(dr["BranchID"]);
                            m_SubstationID = Convert.ToInt64(dr["SubstationID"]);

                            cbDateBegin.EditValue = Convert.ToDateTime(dr["CheckDateBegin"]);
                            cbDateEnd.EditValue = Convert.ToDateTime(dr["CheckDateEnd"]);
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

                if (m_SubjectID > 0)
                {
                    cbSubject.EditValue = m_SubjectID;
                }

                if (m_BranchID > 0)
                {
                    cbBranch.EditValue = m_BranchID;
                }

                if (m_SubstationID > 0)
                {
                    cbSubstation.EditValue = m_SubstationID;
                }

                this.qEquipmentsInSubstationTableAdapter.Fill(this.dataSetQuery.QEquipmentsInSubstation, m_SubstationID, /*Convert.ToInt64(cbEquipmentKind.EditValue)*/0);
                EnablePrintButton();
                CreateGrid();
                RefreshButtons();
                RefreshData();
            }
            catch (SQLiteException ex)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось подключиться к базе данных. Описание: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            m_bDataLoadEnd = true;
        }

        private void GridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.AbsoluteIndex >= 3)
            {
                //e.Appearance.BackColor = Color.Orange;
                string strColName = e.Column.Name;
                long EquipmentID = Convert.ToInt64(GridView.GetRowCellValue(e.RowHandle, "EquipmentID"));
                long EquipmentKindID = Convert.ToInt64(GridView.GetRowCellValue(e.RowHandle, "EquipmentKindID"));
                Inspection.InspectionType type = Inspection.InspectionType.Visual;

                if (strColName != "Result")
                {
                    if (strColName == "Visual") type = Inspection.InspectionType.Visual;
                    else if (strColName == "FHA") type = Inspection.InspectionType.FHA;
                    else if (strColName == "HARG") type = Inspection.InspectionType.HARG;
                    else if (strColName == "Vibro") type = Inspection.InspectionType.Vibro;
                    else if (strColName == "Warm") type = Inspection.InspectionType.Warm;
                    else if (strColName == "Parameter") type = Inspection.InspectionType.Parameter;
                    else if (strColName == "Electrical") type = Inspection.InspectionType.Electrical;

                    if (Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID].IndexOf(type) < 0)
                    {
                        e.Appearance.BackColor = Color.Black;
                        return;
                    }
                }

                if (m_Data.ContainsKey(EquipmentID))
                {
                    long it = -1;
                    if (strColName == "Visual") it = (long)Inspection.InspectionType.Visual;
                    else if (strColName == "FHA") it = (long)Inspection.InspectionType.FHA;
                    else if (strColName == "HARG") it = (long)Inspection.InspectionType.HARG;
                    else if (strColName == "Vibro") it = (long)Inspection.InspectionType.Vibro;
                    else if (strColName == "Warm") it = (long)Inspection.InspectionType.Warm;
                    else if (strColName == "Parameter") it = (long)Inspection.InspectionType.Parameter;
                    else if (strColName == "Electrical") it = (long)Inspection.InspectionType.Electrical;
                    else if (strColName == "Result") it = -1;

                    if (m_Data[EquipmentID].ContainsKey(it))
                    {
                        switch (m_Data[EquipmentID][it])
                        {
                            case Inspection.RangeResult.Green:
                                e.Appearance.BackColor = Color.Green;
                                break;
                            case Inspection.RangeResult.Yellow:
                                e.Appearance.BackColor = Color.Yellow;
                                break;
                            case Inspection.RangeResult.Orange:
                                e.Appearance.BackColor = Color.Orange;
                                break;
                            case Inspection.RangeResult.Red:
                                e.Appearance.BackColor = Color.Red;
                                break;
                            default:
                                e.Appearance.BackColor = Color.DarkGray;
                                break;
                        }
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.DarkGray;
                    }
                }
                else
                {
                    e.Appearance.BackColor = Color.DarkGray;
                }
            }
        }

        private void EnablePrintButton()
        {
            // если все оборудование одного класса, то включаем кнопку печать
            /*long old_kind_id = 0;
            for (int i = 0; i < this.dataSetQuery.QEquipmentsInSubstation.Count; i++)
            {
                long kind_id = Convert.ToInt64(this.dataSetQuery.QEquipmentsInSubstation.Rows[i]["EquipmentKindID"]);

                if (kind_id != old_kind_id && old_kind_id > 0)
                {
                    btnReport.Enabled = false;
                    return;
                }
                else
                    old_kind_id = kind_id;
            }*/
            if (this.dataSetQuery.QEquipmentsInSubstation.Count == 0)
                btnReport.Enabled = false;
            else
                btnReport.Enabled = true;
        }

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            if (GridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string strColName = GridView.FocusedColumn.Name;
            long EquipmentID = Convert.ToInt64(GridView.GetRowCellValue(GridView.FocusedRowHandle, "EquipmentID"));

            if (strColName == "Visual")
            {
                ShowVisualForm(EquipmentID);
                RefreshData();
            }
            if (strColName == "FHA")
            {
                ShowFHAForm(EquipmentID);
                RefreshData();
            }
            if (strColName == "HARG")
            {
                ShowHARGForm(EquipmentID);
                RefreshData();
            }
            if (strColName == "Vibro")
            {
                ShowVibroForm(EquipmentID);
                RefreshData();
            }
            if (strColName == "Warm")
            {
                ShowWarmForm(EquipmentID);
                RefreshData();
            }
            if (strColName == "Parameter")
            {
                ShowParameterForm(EquipmentID);
                RefreshData();
            }
            if (strColName == "Electrical")
            {
                ShowElectricalForm(EquipmentID);
                RefreshData();
            }
        }

        private void RefreshGridPos(long id)
        {
            long iSubstationID = -1;
            if (cbSubstation.EditValue != null) iSubstationID = (long)cbSubstation.EditValue;

            int f_row = GridView.FocusedRowHandle;
            if (id <= 0)
            {
                if (f_row > 0) f_row--;

                this.qEquipmentsInSubstationTableAdapter.Fill(this.dataSetQuery.QEquipmentsInSubstation, iSubstationID, /*Convert.ToInt64(cbEquipmentKind.EditValue)*/0);
                EnablePrintButton();
                RefreshButtons();
                RefreshData();
                if (GridView.RowCount > f_row)
                {
                    GridView.ClearSelection();
                    GridView.SelectRow(f_row);
                    GridView.FocusedRowHandle = f_row;
                }
            }
            else
            {
                this.qEquipmentsInSubstationTableAdapter.Fill(this.dataSetQuery.QEquipmentsInSubstation, iSubstationID, /*Convert.ToInt64(cbEquipmentKind.EditValue)*/0);
                EnablePrintButton();
                RefreshButtons();
                RefreshData();

                for (int i = 0; i < GridView.RowCount; i++)
                {
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

        private void CheckDataForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Form f = this.Owner;
                while (f.Owner != null)
                {
                    f.Hide();
                    f = f.Owner;
                }
                f.Hide();

                /*Form f = this.Owner;
                while (f.Owner != null)
                {
                    f.Hide();
                    f = f.Owner;
                }
                f.Hide();*/
            }
            if (this.WindowState != FormWindowState.Minimized && m_bDataLoadEnd /*&& this.ShowInTaskbar*/)
            {
                Form f = this.Owner;
                while (f.Owner != null)
                {
                    if (!f.Visible) f.Show();
                    f = f.Owner;
                }

                if (!f.Visible) f.Show();
                //this.ShowInTaskbar = false;
            }
        }

        private void CheckDataForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void cbSubject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void cbSubject_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;

            long iSubjectID = -1;
            if (cbSubject.EditValue != null)
                iSubjectID = Convert.ToInt64(cbSubject.EditValue);
            this.qBranchesBySubjectTableAdapter.Fill(this.dataSetQuery.QBranchesBySubject, iSubjectID);
            this.qSubstationsByBranchTableAdapter.Fill(this.dataSetQuery.QSubstationsByBranch, -1);

            if (this.dataSetQuery.QBranchesBySubject.Rows.Count < 7)
                cbBranch.Properties.DropDownRows = this.dataSetQuery.QBranchesBySubject.Rows.Count;
            else
                cbBranch.Properties.DropDownRows = 7;

            if (this.dataSetQuery.QSubstationsByBranch.Rows.Count < 7)
                cbSubstation.Properties.DropDownRows = this.dataSetQuery.QSubstationsByBranch.Rows.Count;
            else
                cbSubstation.Properties.DropDownRows = 7;
        }

        private void cbSubject_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) && ((LookUpEdit)sender).Text == "")
            {
                ((LookUpEdit)sender).ClosePopup();
                ((LookUpEdit)sender).EditValue = null;
            }
        }

        private void cbBranch_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;

            long iSubjectID = -1;
            if (cbSubject.EditValue != null)
                iSubjectID = Convert.ToInt64(cbSubject.EditValue);

            long iBranchID = -1;
            if (cbBranch.EditValue != null)
                iBranchID = Convert.ToInt64(cbBranch.EditValue);

            this.qSubstationsByBranchTableAdapter.Fill(this.dataSetQuery.QSubstationsByBranch, iBranchID);

            if (this.dataSetQuery.QSubstationsByBranch.Rows.Count < 7)
                cbSubstation.Properties.DropDownRows = this.dataSetQuery.QSubstationsByBranch.Rows.Count;
            else
                cbSubstation.Properties.DropDownRows = 7;
        }

        public void RefreshButtons()
        {
            if (this.dataSetQuery.QEquipmentsInSubstation.Count == 0)
            {
                btnVisual.Enabled = false;
                btnWarm.Enabled = false;
                btnVibro.Enabled = false;
                btnHARG.Enabled = false;
                btnFHA.Enabled = false;
                btnParameter.Enabled = false;
                btnElectrical.Enabled = false;
                //btnReport.Enabled = false;
                //btnProtocol.Enabled = false;
            }
            else
            {
                btnVisual.Enabled = true;
                btnWarm.Enabled = true;
                btnVibro.Enabled = true;
                btnHARG.Enabled = true;
                btnFHA.Enabled = true;
                btnParameter.Enabled = true;
                btnElectrical.Enabled = true;
                //btnReport.Enabled = true;
                //btnProtocol.Enabled = true;
            }
        }

        private bool SaveData()
        {
            DateTime dateBegin;
            DateTime dateEnd;
            if (cbDateBegin.EditValue == null)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать дату начала проверки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbDateBegin.Focus();
                return false;
            }
            if (cbDateEnd.EditValue == null)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать дату окончания проверки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbDateEnd.Focus();
                return false;
            }

            dateBegin = (DateTime)cbDateBegin.EditValue;
            dateEnd = (DateTime)cbDateEnd.EditValue;

            dateBegin = dateBegin.Date;
            dateEnd = dateEnd.Date;

            if (dateBegin > dateEnd)
            {
                MyLocalizer.XtraMessageBoxShow("Дата начала проверки не должна превышать дату окончания.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbDateBegin.Focus();
                return false;
            }

            if (cbSubstation.EditValue == null)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо уточнить объект проверки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbSubstation.Focus();
                return false;
            }

            long substationID = Convert.ToInt64(cbSubstation.EditValue);

            try
            {
                SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                connection.Open();
                SQLiteCommand com = new SQLiteCommand(connection);

                // проверка на то, что даты всех обследований этой проверки надходятся в пределах дат самой проверки
                com.CommandText = "SELECT InspectionID FROM Inspections WHERE CheckID = @id AND (InspectionDate < @date_begin OR InspectionDate > @date_end)";
                com.CommandType = CommandType.Text;
                com.Parameters.Clear();
                AddParam(com, "@date_begin", DbType.DateTime, dateBegin);
                AddParam(com, "@date_end", DbType.DateTime, dateEnd);
                AddParam(com, "@id", DbType.Int64, m_CheckID);
                SQLiteDataReader dr_insp = com.ExecuteReader();
                if (dr_insp.HasRows)
                {
                    dr_insp.Close();
                    MyLocalizer.XtraMessageBoxShow("Существуют обследования, дата которых выходит за сроки проверки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbDateBegin.Focus();
                    return false;
                }
                dr_insp.Close();


                // проверка на пересечение дат в этом расположении
                com.CommandText = "SELECT CheckID FROM Checks WHERE CheckDateBegin <= @date_end AND CheckDateEnd >= @date_begin AND SubstationID = @sid AND CheckID <> @id";
                com.CommandType = CommandType.Text;
                com.Parameters.Clear();
                AddParam(com, "@date_begin", DbType.DateTime, dateBegin);
                AddParam(com, "@date_end", DbType.DateTime, dateEnd);
                AddParam(com, "@sid", DbType.Int64, substationID);
                AddParam(com, "@id", DbType.Int64, m_CheckID);

                SQLiteDataReader dr_ = com.ExecuteReader();
                if (dr_.HasRows)
                {
                    dr_.Close();
                    MyLocalizer.XtraMessageBoxShow("Проверка не должна пересекаться по срокам проведения с другими проверками в этом объекте.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbDateBegin.Focus();
                    return false;
                }
                dr_.Close();

                if (m_CheckID <= 0)
                    com.CommandText = "INSERT INTO Checks (CheckDateBegin, CheckDateEnd, SubstationID) VALUES (@date_begin, @date_end, @sid)";
                else
                    com.CommandText = "UPDATE Checks SET CheckDateBegin = @date_begin, CheckDateEnd = @date_end, SubstationID = @sid WHERE CheckID = @id";

                com.Parameters.Clear();
                AddParam(com, "@date_begin", DbType.DateTime, dateBegin);
                AddParam(com, "@date_end", DbType.DateTime, dateEnd);
                AddParam(com, "@sid", DbType.Int64, substationID);
                if (m_CheckID > 0)
                {
                    AddParam(com, "@id", DbType.Int64, m_CheckID);
                }
                com.ExecuteNonQuery();

                if (m_CheckID <= 0)
                {
                    com.Parameters.Clear();
                    com.CommandText = "select seq from sqlite_sequence where name = 'Checks'";
                    com.CommandType = CommandType.Text;
                    SQLiteDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        m_CheckID = Convert.ToInt64(dr["seq"]);
                    }
                    dr.Close();
                }

                connection.Close();

                m_bChangeData = false;
            }
            catch (SQLiteException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DBConcurrencyException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;
        }

        private void btnPassportAdd_Click(object sender, EventArgs e)
        {
            if (cbSubstation.EditValue != null)
            {
                if (!SaveData()) return;

                long id = -1;

                long subjectID = -1;
                long branchID = -1;
                long substationID = -1;
                if (cbSubject.EditValue != null) subjectID = Convert.ToInt64(cbSubject.EditValue);
                if (cbBranch.EditValue != null) branchID = Convert.ToInt64(cbBranch.EditValue);
                if (cbSubstation.EditValue != null) substationID = Convert.ToInt64(cbSubstation.EditValue);

                PassportDataForm rf = new PassportDataForm(id, subjectID, branchID, substationID);
                rf.m_bShowContinueMsg = true;
                DialogResult dr = rf.ShowDialog(this);
                id = rf.m_id;
                if (dr == System.Windows.Forms.DialogResult.OK)
                    RefreshGridPos(id);

                if (rf.m_bContinueNext)
                {
                    ShowVisualForm(id);
                    RefreshData();
                }
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо уточнить объект проверки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSubstation.Focus();
            }
        }

        void UpdatePassportData(long id)
        {
            PassportDataForm rf = new PassportDataForm(id);
            rf.m_bShowContinueMsg = true;
            DialogResult dr = rf.ShowDialog(this);
            if (dr == System.Windows.Forms.DialogResult.OK)
                RefreshGridPos(id);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (rf.m_bContinueNext)
                    ShowVisualForm(id);
            }
        }

        private void ShowNextForm(long EquipmentKindID, long id, Inspection.InspectionType cur_type)
        {
            Inspection.InspectionType next_type = Inspection.InspectionType.Visual;
            bool bEnd = false;
            int pos = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID].IndexOf((Inspection.InspectionType)cur_type);
            if (pos >= 0)
            {
                if (Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID].Count > pos + 1)
                    next_type = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID][pos + 1];
                else
                    bEnd = true;
            }
            else return;

            if (bEnd)
            {
                VerifyEnd(EquipmentKindID, id);
            }
            else
            {
                switch (next_type)
                {
                    case Inspection.InspectionType.FHA:
                        ShowFHAForm(id);
                        break;
                    case Inspection.InspectionType.HARG:
                        ShowHARGForm(id);
                        break;
                    case Inspection.InspectionType.Vibro:
                        ShowVibroForm(id);
                        break;
                    case Inspection.InspectionType.Visual:
                        ShowVisualForm(id);
                        break;
                    case Inspection.InspectionType.Warm:
                        ShowWarmForm(id);
                        break;
                    case Inspection.InspectionType.Parameter:
                        ShowParameterForm(id);
                        break;
                    case Inspection.InspectionType.Electrical:
                        ShowElectricalForm(id);
                        break;
                }
            }
        }

        private void ShowPrevForm(long EquipmentKindID, long id, Inspection.InspectionType cur_type)
        {
            Inspection.InspectionType prev_type = Inspection.InspectionType.Visual;
            bool bPassport = false;
            int pos = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID].IndexOf((Inspection.InspectionType)cur_type);
            if (pos >= 0)
            {
                if (pos > 0)
                    prev_type = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID][pos - 1];
                else
                    bPassport = true;
            }
            else return;

            if (bPassport)
            {
                UpdatePassportData(id);
            }
            else
            {
                switch (prev_type)
                {
                    case Inspection.InspectionType.FHA:
                        ShowFHAForm(id);
                        break;
                    case Inspection.InspectionType.HARG:
                        ShowHARGForm(id);
                        break;
                    case Inspection.InspectionType.Vibro:
                        ShowVibroForm(id);
                        break;
                    case Inspection.InspectionType.Visual:
                        ShowVisualForm(id);
                        break;
                    case Inspection.InspectionType.Warm:
                        ShowWarmForm(id);
                        break;
                    case Inspection.InspectionType.Parameter:
                        ShowParameterForm(id);
                        break;
                    case Inspection.InspectionType.Electrical:
                        ShowElectricalForm(id);
                        break;
                }
            }
        }


        private DialogResult ShowForm(long id, Inspection.InspectionType type, out long EquipmentKindID, out bool bContinueNext, out bool bContinuePrev)
        {
            bContinueNext = false;
            bContinuePrev = false;

            //this.ShowInTaskbar = false;
            InspectionDataForm form = new InspectionDataForm(id, type, 0, m_CheckID, (DateTime)cbDateBegin.EditValue, (DateTime)cbDateEnd.EditValue);
            form.m_bShowContinueMsg = true;
            EquipmentKindID = form.m_EquipmentKindID;

            // если у данного оборудования нет обследования этого типа, то ничего не делаем
            if (Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID].IndexOf(type) < 0)
                return System.Windows.Forms.DialogResult.Cancel;

            DialogResult dr = form.ShowDialog(this);
            bContinueNext = form.m_bContinueNext;
            bContinuePrev = form.m_bContinuePrev;

            //this.ShowInTaskbar = true;

            return dr;
        }

        private void ShowVisualForm(long id)
        {
            bool bContinueNext = false;
            bool bContinuePrev = false;
            long EquipmentKindID = 0;
            DialogResult dr = ShowForm(id, Inspection.InspectionType.Visual, out EquipmentKindID, out bContinueNext, out bContinuePrev);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (bContinueNext)
                {
                    ShowNextForm(EquipmentKindID, id, Inspection.InspectionType.Visual);
                }
                else
                {
                    if (bContinuePrev)
                    {
                        ShowPrevForm(EquipmentKindID, id, Inspection.InspectionType.Visual);
                    }
                }
            }
        }

        private void ShowElectricalForm(long id)
        {
            bool bContinueNext = false;
            bool bContinuePrev = false;
            long EquipmentKindID = 0;
            DialogResult dr = ShowForm(id, Inspection.InspectionType.Electrical, out EquipmentKindID, out bContinueNext, out bContinuePrev);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (bContinueNext)
                {
                    ShowNextForm(EquipmentKindID, id, Inspection.InspectionType.Electrical);
                }
                else
                {
                    if (bContinuePrev)
                    {
                        ShowPrevForm(EquipmentKindID, id, Inspection.InspectionType.Electrical);
                    }
                }
            }
        }

        private void ShowParameterForm(long id)
        {
            bool bContinueNext = false;
            bool bContinuePrev = false;
            long EquipmentKindID = 0;
            DialogResult dr = ShowForm(id, Inspection.InspectionType.Parameter, out EquipmentKindID, out bContinueNext, out bContinuePrev);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (bContinueNext)
                {
                    ShowNextForm(EquipmentKindID, id, Inspection.InspectionType.Parameter);
                }
                else
                {
                    if (bContinuePrev)
                    {
                        ShowPrevForm(EquipmentKindID, id, Inspection.InspectionType.Parameter);
                    }
                }
            }
        }

        private void ShowFHAForm(long id)
        {
            bool bContinueNext = false;
            bool bContinuePrev = false;
            long EquipmentKindID = 0;
            DialogResult dr = ShowForm(id, Inspection.InspectionType.FHA, out EquipmentKindID, out bContinueNext, out bContinuePrev);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (bContinueNext)
                {
                    ShowNextForm(EquipmentKindID, id, Inspection.InspectionType.FHA);
                }
                else
                {
                    if (bContinuePrev)
                    {
                        ShowPrevForm(EquipmentKindID, id, Inspection.InspectionType.FHA);
                    }
                }
            }
        }

        private void ShowHARGForm(long id)
        {
            bool bContinueNext = false;
            bool bContinuePrev = false;
            long EquipmentKindID = 0;
            DialogResult dr = ShowForm(id, Inspection.InspectionType.HARG, out EquipmentKindID, out bContinueNext, out bContinuePrev);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (bContinueNext)
                {
                    ShowNextForm(EquipmentKindID, id, Inspection.InspectionType.HARG);
                }
                else
                {
                    if (bContinuePrev)
                    {
                        ShowPrevForm(EquipmentKindID, id, Inspection.InspectionType.HARG);
                    }
                }
            }
        }

        private void ShowWarmForm(long id)
        {
            bool bContinueNext = false;
            bool bContinuePrev = false;
            long EquipmentKindID = 0;
            DialogResult dr = ShowForm(id, Inspection.InspectionType.Warm, out EquipmentKindID, out bContinueNext, out bContinuePrev);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (bContinueNext)
                {
                    ShowNextForm(EquipmentKindID, id, Inspection.InspectionType.Warm);
                }
                else
                {
                    if (bContinuePrev)
                    {
                        ShowPrevForm(EquipmentKindID, id, Inspection.InspectionType.Warm);
                    }
                }
            }
        }

        /*private void PrintProtocol(long id)
        {
            if (m_CheckID <= 0)
            {
                MyLocalizer.XtraMessageBoxShow("Нет данных для вывода протокола.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            WaitingFrom wf = new WaitingFrom(Inspection.ReportType.Protocol);
            ReportInfo.Equipment eq = new ReportInfo.Equipment(id);
            wf.m_listEquipments.Add(eq);
            wf.m_CheckID = m_CheckID;
            wf.ShowDialog(this);
        }*/

        private void ShowVibroForm(long id)
        {
            bool bContinueNext = false;
            bool bContinuePrev = false;
            long EquipmentKindID = 0;
            DialogResult dr = ShowForm(id, Inspection.InspectionType.Vibro, out EquipmentKindID, out bContinueNext, out bContinuePrev);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (bContinueNext)
                {
                    ShowNextForm(EquipmentKindID, id, Inspection.InspectionType.Vibro);
                }
                else
                {
                    if (bContinuePrev)
                    {
                        ShowPrevForm(EquipmentKindID, id, Inspection.InspectionType.Vibro);
                    }
                }
            }
        }

        private void VerifyEnd(long EquipmentKindID, long id)
        {
            List<ReportInfo.Equipment> m_listEquipments = new List<ReportInfo.Equipment>();
            Dictionary<Inspection.InspectionType, List<ReportInfo.InspectionSubType_>> m_list_sub_types = new Dictionary<Inspection.InspectionType, List<ReportInfo.InspectionSubType_>>();
            Dictionary<Inspection.InspectionType, List<long?>> m_dictCommonSubTypes = new Dictionary<Inspection.InspectionType, List<long?>>();
            Dictionary<Inspection.InspectionType, double> m_InspectionTypeFillability = new Dictionary<Inspection.InspectionType, double>();
            double fCommonFillability = 0;

            m_listEquipments.Add(new ReportInfo.Equipment(id, EquipmentKindID));

            if (!ReportInfo.GetData(m_CheckID, m_listEquipments, m_dictCommonSubTypes, m_list_sub_types, 0))
                return;

            fCommonFillability = ReportInfo.GetFillability(EquipmentKindID, m_listEquipments, m_dictCommonSubTypes, 0, m_InspectionTypeFillability);

            if (Math.Abs(1.0 - fCommonFillability) > 0.0009)
            {
                //PrintFillabilityMessageForm f = new PrintFillabilityMessageForm();
                //f.m_fProcent = fCommonFillability;

                fCommonFillability = 1.0 - fCommonFillability;
                //if (fCommonFillability < 0.01) fCommonFillability = 0.01;
                DialogResult res = MyLocalizer.XtraMessageBoxShow("Не заполнено " + fCommonFillability.ToString("0.#%") + " данных", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, "Ввести недостающие данные", "Завершить ввод данных");

                if (res == System.Windows.Forms.DialogResult.No)
                {
                    //PrintProtocol(id);
                }
                else
                {
                    // ищем 
                    //foreach (Inspection.InspectionType type in Enum.GetValues(typeof(Inspection.InspectionType)))
                    for (int i = 0; i < Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID].Count; i++)
                    {
                        Inspection.InspectionType type = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID][i];
                        if (Math.Abs(1.0 - m_InspectionTypeFillability[type]) > 0.0009)
                        {
                            switch (type)
                            {
                                case Inspection.InspectionType.Vibro:
                                    ShowVibroForm(id);
                                    break;
                                case Inspection.InspectionType.FHA:
                                    ShowFHAForm(id);
                                    break;
                                case Inspection.InspectionType.HARG:
                                    ShowHARGForm(id);
                                    break;
                                case Inspection.InspectionType.Visual:
                                    ShowVisualForm(id);
                                    break;
                                case Inspection.InspectionType.Warm:
                                    ShowWarmForm(id);
                                    break;
                                case Inspection.InspectionType.Parameter:
                                    ShowParameterForm(id);
                                    break;
                                case Inspection.InspectionType.Electrical:
                                    ShowElectricalForm(id);
                                    break;
                            }
                            return;
                        }
                    }
                }
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if (!SaveData())
            {
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void cbSubstation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                SubstationForm f = new SubstationForm();
                f.m_bCanSelect = true;
                DialogResult res = f.ShowDialog(this);

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);
                    if (cbBranch.EditValue != null && (long)cbBranch.EditValue == f.m_BranchID)
                    {
                        this.qSubstationsByBranchTableAdapter.Fill(this.dataSetQuery.QSubstationsByBranch, f.m_BranchID);
                    }

                    cbSubject.EditValue = f.m_SubjectID;
                    cbBranch.EditValue = f.m_BranchID;
                    cbSubstation.EditValue = f.m_SelectID;
                }
            }
        }

        private void cbSubject_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                SubjectForm f = new SubjectForm();
                f.m_bCanSelect = true;
                DialogResult res = f.ShowDialog(this);

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);

                    cbSubject.EditValue = f.m_SelectID;
                }
            }
        }

        private void cbBranch_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                BranchForm f = new BranchForm();
                f.m_bCanSelect = true;
                DialogResult res = f.ShowDialog(this);

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);
                    if (cbSubject.EditValue != null && (long)cbSubject.EditValue == f.m_SubjectID)
                    {
                        this.qBranchesBySubjectTableAdapter.Fill(this.dataSetQuery.QBranchesBySubject, f.m_SubjectID);
                    }

                    cbSubject.EditValue = f.m_SubjectID;
                    cbBranch.EditValue = f.m_SelectID;
                }
            }
        }

        private void cbSubstation_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;

            RefreshGridPos(-1);
        }

        private void btnVisual_Click(object sender, EventArgs e)
        {
            if (cbSubstation.EditValue != null)
            {
                if (GridView.FocusedRowHandle < 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!SaveData()) return;

                DataRowView drv = (DataRowView)(qEquipmentsInSubstationBindingSource.Current);
                long id = Convert.ToInt64(drv.Row["EquipmentID"]);

                ShowVisualForm(id);

                RefreshData();
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо уточнить объект проверки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSubstation.Focus();
            }
        }

        private void btnFHA_Click(object sender, EventArgs e)
        {
            if (cbSubstation.EditValue != null)
            {
                if (GridView.FocusedRowHandle < 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!SaveData()) return;

                DataRowView drv = (DataRowView)(qEquipmentsInSubstationBindingSource.Current);
                long id = Convert.ToInt64(drv.Row["EquipmentID"]);

                ShowFHAForm(id);

                RefreshData();
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо уточнить объект проверки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSubstation.Focus();
            }
        }

        private void btnHARG_Click(object sender, EventArgs e)
        {
            if (cbSubstation.EditValue != null)
            {
                if (GridView.FocusedRowHandle < 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!SaveData()) return;

                DataRowView drv = (DataRowView)(qEquipmentsInSubstationBindingSource.Current);
                long id = Convert.ToInt64(drv.Row["EquipmentID"]);

                ShowHARGForm(id);

                RefreshData();
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо уточнить объект проверки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSubstation.Focus();
            }
        }

        private void btnWarm_Click(object sender, EventArgs e)
        {
            if (cbSubstation.EditValue != null)
            {
                if (GridView.FocusedRowHandle < 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!SaveData()) return;

                DataRowView drv = (DataRowView)(qEquipmentsInSubstationBindingSource.Current);
                long id = Convert.ToInt64(drv.Row["EquipmentID"]);

                ShowWarmForm(id);

                RefreshData();
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо уточнить объект проверки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSubstation.Focus();
            }
        }

        private void btnVibro_Click(object sender, EventArgs e)
        {
            if (cbSubstation.EditValue != null)
            {
                if (GridView.FocusedRowHandle < 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!SaveData()) return;

                DataRowView drv = (DataRowView)(qEquipmentsInSubstationBindingSource.Current);
                long id = Convert.ToInt64(drv.Row["EquipmentID"]);

                ShowVibroForm(id);

                RefreshData();
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо уточнить объект проверки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSubstation.Focus();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (m_CheckID <= 0)
            {
                MyLocalizer.XtraMessageBoxShow("Нет данных для вывода отчета.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Dictionary<long, List<ReportInfo.Equipment>> dictEquipments = new Dictionary<long, List<ReportInfo.Equipment>>();
            for (int i = 0; i < this.dataSetQuery.QEquipmentsInSubstation.Count; i++)
            {
                long id = Convert.ToInt64(this.dataSetQuery.QEquipmentsInSubstation.Rows[i]["EquipmentID"]);
                long kind_id = Convert.ToInt64(this.dataSetQuery.QEquipmentsInSubstation.Rows[i]["EquipmentKindID"]);

                if (!dictEquipments.ContainsKey(kind_id)) dictEquipments[kind_id] = new List<ReportInfo.Equipment>();
                ReportInfo.Equipment eq = new ReportInfo.Equipment(id, kind_id);
                dictEquipments[kind_id].Add(eq);
            }

            if (dictEquipments.Count == 0)
            {
                return;
            }

            if (dictEquipments.Count == 1)
            {
                WaitingForm wf = new WaitingForm();
                List<long> keyList = new List<long>(dictEquipments.Keys);

                switch ((Equipment.EquipmentKind)keyList[0])
                {
                    case Equipment.EquipmentKind.Transformer:
                        wf.m_reportType = Inspection.ReportType.ReportTransformer;
                        break;
                    case Equipment.EquipmentKind.AirSwitch:
                        wf.m_reportType = Inspection.ReportType.ReportAirSwitch;
                        break;
                    case Equipment.EquipmentKind.OilLessSwitch:
                        wf.m_reportType = Inspection.ReportType.ReportOilLessSwitch;
                        break;
                    case Equipment.EquipmentKind.OilTankSwitch:
                        wf.m_reportType = Inspection.ReportType.ReportOilTankSwitch;
                        break;
                    default:
                        MyLocalizer.XtraMessageBoxShow("Отчет для данного вида оборудования недоступен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                wf.m_listEquipments = dictEquipments[keyList[0]];
                wf.m_CheckID = m_CheckID;
                wf.ShowDialog(this);

                if (wf.m_Word != null)
                {
                    wf.m_Word.SetVisible(true);
                    wf.m_Word.DestroyWord();
                }
            }
            else
            {
                if (MyLocalizer.XtraMessageBoxShow("В проверке объекта участвует несколько видов оборудования.\nСоздать отчёты по всем видам оборудования (отчёт по каждому виду оборудования в отдельном файле)?", "Вывод отчета", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    List<Word> listWord = new List<Word>();

                    foreach (KeyValuePair<long, List<ReportInfo.Equipment>> pair in dictEquipments)
                    {
                        WaitingForm wf = new WaitingForm();
                        List<long> keyList = new List<long>(dictEquipments.Keys);

                        switch ((Equipment.EquipmentKind)pair.Key)
                        {
                            case Equipment.EquipmentKind.Transformer:
                                wf.m_reportType = Inspection.ReportType.ReportTransformer;
                                break;
                            case Equipment.EquipmentKind.AirSwitch:
                                wf.m_reportType = Inspection.ReportType.ReportAirSwitch;
                                break;
                            case Equipment.EquipmentKind.OilLessSwitch:
                                wf.m_reportType = Inspection.ReportType.ReportOilLessSwitch;
                                break;
                            case Equipment.EquipmentKind.OilTankSwitch:
                                wf.m_reportType = Inspection.ReportType.ReportOilTankSwitch;
                                break;
                            default:
                                MyLocalizer.XtraMessageBoxShow("Отчет для данного вида оборудования недоступен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                        }

                        wf.m_listEquipments = pair.Value;
                        wf.m_CheckID = m_CheckID;
                        wf.ShowDialog(this);

                        if (wf.m_Word != null) listWord.Add(wf.m_Word);
                    }

                    for (int i = 0; i < listWord.Count; i++)
                    {
                        listWord[i].SetVisible(true);
                        listWord[i].DestroyWord();
                    }
                }
                else
                {
                    PrePrintForm wf = new PrePrintForm();
                    wf.m_dictEquipments = dictEquipments;
                    wf.m_CheckID = m_CheckID;
                    wf.ShowDialog(this);
                }
            }
        }

        private void cbDateBegin_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;
        }

        private void cbDateEnd_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;
        }

        private void CheckDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cbDateBegin.Focus();
            if (DialogResult != System.Windows.Forms.DialogResult.OK && m_bChangeData)
            {
                if (MyLocalizer.XtraMessageBoxShow("Сохранить изменения?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!SaveData())
                    {
                        e.Cancel = true;
                        return;
                    }
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private int GetColumnBestHeight(string strCaption, int width/*, GridBand parent_column*/)
        {
            GridColumnInfoArgs ex = null;
            GridViewInfo viewInfo = GridView.GetViewInfo() as GridViewInfo;
            viewInfo.GInfo.AddGraphics(null);
            ex = new GridColumnInfoArgs(viewInfo.GInfo.Cache, null);
            try
            {
                ex.InnerElements.Add(new DrawElementInfo(new GlyphElementPainter(),
                                                        new GlyphElementInfoArgs(viewInfo.View.Images, 0, null),
                                                        StringAlignment.Near));
                ex.SetAppearance(GridView.Appearance.HeaderPanel);
                ex.Caption = strCaption;
                ex.CaptionRect = new Rectangle(0, 0, width, 1000);
            }
            finally
            {
                viewInfo.GInfo.ReleaseGraphics();
            }

            GraphicsInfo grInfo = new GraphicsInfo();
            grInfo.AddGraphics(null);
            ex.Cache = grInfo.Cache;
            Size captionSize = CalcCaptionTextSize(grInfo.Cache, ex as HeaderObjectInfoArgs, strCaption);
            return captionSize.Height;
        }

        private int GetColumnBestHeight(DevExpress.XtraGrid.Columns.GridColumn column, int width/*, GridBand parent_column*/)
        {
            return GetColumnBestHeight(column.Caption, width);            
        }

        Size CalcCaptionTextSize(GraphicsCache cache, HeaderObjectInfoArgs ee, string caption)
        {
            Size captionSize = ee.Appearance.CalcTextSize(cache, caption, ee.CaptionRect.Width).ToSize();
            return captionSize;
        }

        private void SetColumnHeight(DevExpress.XtraGrid.Columns.GridColumn column)
        {
            GridView.BeginUpdate();

            int max_row = -1;
            for (int i = 0; i < GridView.Columns.Count; i++)
            {
                int height = GetColumnBestHeight(GridView.Columns[i], GridView.Columns[i].Width);
                int row = height / m_singleLineHeight;

                if (row > max_row) max_row = row;
            }

            GridView.ColumnPanelRowHeight = (max_row) * m_singleLineHeight + m_singleLineDiff;

            GridView.EndUpdate();
        }

        private void GridView_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            SetColumnHeight(e.Column);
        }

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (GridView.FocusedRowHandle >= 0)
            {
                long EquipmentID = Convert.ToInt64(GridView.GetRowCellValue(GridView.FocusedRowHandle, "EquipmentID"));

                if (m_Data.ContainsKey(EquipmentID))
                {
                    if (m_Data[EquipmentID].ContainsKey((long)Inspection.InspectionType.Visual))
                        btnVisual.ToolTip = "Изменить визуальное обследование";
                    else
                        btnVisual.ToolTip = "Добавить визуальное обследование";

                    if (m_Data[EquipmentID].ContainsKey((long)Inspection.InspectionType.FHA))
                        btnFHA.ToolTip = "Изменить обследование ФХА";
                    else
                        btnFHA.ToolTip = "Добавить обследование ФХА";

                    if (m_Data[EquipmentID].ContainsKey((long)Inspection.InspectionType.HARG))
                        btnHARG.ToolTip = "Изменить обследование ХАРГ";
                    else
                        btnHARG.ToolTip = "Добавить обследование ХАРГ";

                    if (m_Data[EquipmentID].ContainsKey((long)Inspection.InspectionType.Vibro))
                        btnVibro.ToolTip = "Изменить вибрационное обследование";
                    else
                        btnVibro.ToolTip = "Добавить вибрационное обследование";

                    if (m_Data[EquipmentID].ContainsKey((long)Inspection.InspectionType.Warm))
                        btnWarm.ToolTip = "Изменить тепловизионный контроль";
                    else
                        btnWarm.ToolTip = "Добавить тепловизионный контроль";

                    if (m_Data[EquipmentID].ContainsKey((long)Inspection.InspectionType.Parameter))
                        btnParameter.ToolTip = "Изменить определение характеристик выключателя";
                    else
                        btnParameter.ToolTip = "Добавить определение характеристик выключателя";

                    if (m_Data[EquipmentID].ContainsKey((long)Inspection.InspectionType.Electrical))
                        btnElectrical.ToolTip = "Изменить электрические измерения выключателя";
                    else
                        btnElectrical.ToolTip = "Добавить электрические измерения выключателя";
                }
                else
                {
                    btnVisual.ToolTip = "Добавить визуальное обследование";
                    btnFHA.ToolTip = "Добавить обследование ФХА";
                    btnHARG.ToolTip = "Добавить обследование ХАРГ";
                    btnVibro.ToolTip = "Добавить вибрационное обследование";
                    btnWarm.ToolTip = "Добавить тепловизионный контроль";
                    btnParameter.ToolTip = "Добавить определение характеристик выключателя";
                    btnElectrical.ToolTip = "Добавить электрические измерения выключателя";
                }

                if (m_listButtonsPoints.Count == 0)
                {
                    m_listButtonsPoints.Add(new KeyValuePair<Point, Point>(btnVisual.Location, lblVisual.Location));
                    m_listButtonsPoints.Add(new KeyValuePair<Point, Point>(btnFHA.Location, lblFHA.Location));
                    m_listButtonsPoints.Add(new KeyValuePair<Point, Point>(btnHARG.Location, lblHARG.Location));
                    m_listButtonsPoints.Add(new KeyValuePair<Point, Point>(btnWarm.Location, lblWarm.Location));
                    m_listButtonsPoints.Add(new KeyValuePair<Point, Point>(btnVibro.Location, lblVibro.Location));
                    m_listButtonsPoints.Add(new KeyValuePair<Point, Point>(btnParameter.Location, lblParameter.Location));
                    m_listButtonsPoints.Add(new KeyValuePair<Point, Point>(btnElectrical.Location, lblElectrical.Location));
                }

                int beg_index = 0;

                btnVisual.Visible = false;
                btnFHA.Visible = false;
                btnHARG.Visible = false;
                btnWarm.Visible = false;
                btnVibro.Visible = false;
                btnParameter.Visible = false;
                btnElectrical.Visible = false;

                lblVisual.Visible = false;
                lblFHA.Visible = false;
                lblHARG.Visible = false;
                lblWarm.Visible = false;
                lblVibro.Visible = false;
                lblParameter.Visible = false;
                lblElectrical.Visible = false;

                Equipment.EquipmentKind EquipmentKindID = (Equipment.EquipmentKind)Convert.ToInt64(GridView.GetRowCellValue(GridView.FocusedRowHandle, "EquipmentKindID"));
                for (int i = 0; i < Inspection.m_listEquipmentInspections[EquipmentKindID].Count; i++)
                {
                    switch (Inspection.m_listEquipmentInspections[EquipmentKindID][i])
                    {
                        case Inspection.InspectionType.Visual:
                            btnVisual.Location = m_listButtonsPoints[beg_index].Key;
                            btnVisual.Visible = true;

                            lblVisual.Location = m_listButtonsPoints[beg_index].Value;
                            lblVisual.Visible = true;
                            break;
                        case Inspection.InspectionType.FHA:
                            btnFHA.Location = m_listButtonsPoints[beg_index].Key;
                            btnFHA.Visible = true;

                            lblFHA.Location = m_listButtonsPoints[beg_index].Value;
                            lblFHA.Visible = true;
                            break;
                        case Inspection.InspectionType.HARG:
                            btnHARG.Location = m_listButtonsPoints[beg_index].Key;
                            btnHARG.Visible = true;

                            lblHARG.Location = m_listButtonsPoints[beg_index].Value;
                            lblHARG.Visible = true;
                            break;
                        case Inspection.InspectionType.Warm:
                            btnWarm.Location = m_listButtonsPoints[beg_index].Key;
                            btnWarm.Visible = true;

                            lblWarm.Location = m_listButtonsPoints[beg_index].Value;
                            lblWarm.Visible = true;
                            break;
                        case Inspection.InspectionType.Vibro:
                            btnVibro.Location = m_listButtonsPoints[beg_index].Key;
                            btnVibro.Visible = true;

                            lblVibro.Location = m_listButtonsPoints[beg_index].Value;
                            lblVibro.Visible = true;
                            break;
                        case Inspection.InspectionType.Parameter:
                            btnParameter.Location = m_listButtonsPoints[beg_index].Key;
                            btnParameter.Visible = true;

                            lblParameter.Location = m_listButtonsPoints[beg_index].Value;
                            lblParameter.Visible = true;
                            break;
                        case Inspection.InspectionType.Electrical:
                            btnElectrical.Location = m_listButtonsPoints[beg_index].Key;
                            btnElectrical.Visible = true;

                            lblElectrical.Location = m_listButtonsPoints[beg_index].Value;
                            lblElectrical.Visible = true;
                            break;
                    }

                    beg_index++;
                }

                int dist = m_listButtonsPoints[1].Key.X - m_listButtonsPoints[0].Key.X - btnVisual.Size.Width;
                dist *= 6;
                btnReport.Location = new Point(m_listButtonsPoints[beg_index - 1].Key.X + btnVisual.Size.Width + dist, btnReport.Location.Y);
                lblReport.Location = new Point(btnReport.Location.X + btnReport.Size.Width / 2 - lblReport.Width / 2, lblReport.Location.Y);
            }
        }

        private void btnParameter_Click(object sender, EventArgs e)
        {
            if (cbSubstation.EditValue != null)
            {
                if (GridView.FocusedRowHandle < 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!SaveData()) return;

                DataRowView drv = (DataRowView)(qEquipmentsInSubstationBindingSource.Current);
                long id = Convert.ToInt64(drv.Row["EquipmentID"]);

                ShowParameterForm(id);

                RefreshData();
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо уточнить объект проверки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSubstation.Focus();
            }
        }

        /*private void cbEquipmentKind_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd)
            {
                RefreshGridPos(-1);
                //RefreshData();
                //this.qEquipmentsInSubstationTableAdapter.Fill(this.dataSetQuery.QEquipmentsInSubstation, m_SubstationID, Convert.ToInt64(cbEquipmentKind.EditValue));
                //EnablePrintButton();
            }
        }*/

        private void btnElectrical_Click(object sender, EventArgs e)
        {
            if (cbSubstation.EditValue != null)
            {
                if (GridView.FocusedRowHandle < 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!SaveData()) return;

                DataRowView drv = (DataRowView)(qEquipmentsInSubstationBindingSource.Current);
                long id = Convert.ToInt64(drv.Row["EquipmentID"]);

                ShowElectricalForm(id);

                RefreshData();
            }
            else
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо уточнить объект проверки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSubstation.Focus();
            }
        }

        /*private void btnProtocol_Click(object sender, EventArgs e)
        {
            if (GridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!SaveData()) return;

            DataRowView drv = (DataRowView)(qEquipmentsInSubstationBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            PrintProtocol(id);
        }*/
    }
}