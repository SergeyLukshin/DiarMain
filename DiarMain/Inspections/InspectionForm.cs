using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SQLite;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Drawing;


namespace DiarMain
{
    public partial class InspectionForm : DevExpress.XtraEditors.XtraForm
    {
        class SystemItem
        {
            public SystemItem(long SystemItemID, string strSystemItemName)
            {
                m_SystemItemID = SystemItemID;
                m_strSystemItemName = strSystemItemName;
            }

            public long m_SystemItemID;
            public string m_strSystemItemName;
        }

        class FunctionalSystem
        {
            public FunctionalSystem(long FunctionalSystemID, string strFunctionalSystemName)
            {
                m_FunctionalSystemID = FunctionalSystemID;
                m_strFunctionalSystemName = strFunctionalSystemName;
            }

            public long m_FunctionalSystemID;
            public string m_strFunctionalSystemName;

            public List<SystemItem> m_listSystemItems = new List<SystemItem>();
        }

        Inspection.InspectionType m_type;
        long m_EquipmentID = -1;
        SQLiteConnection m_con = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);

        List<FunctionalSystem> m_list = new List<FunctionalSystem>();
        Dictionary<long, int> dictFunctionalSystemPos = new Dictionary<long, int>();
        //Dictionary<long, int> dictSystemItemPos = new Dictionary<long, int>();
        //Dictionary<long, long> dictSystemItemsFunctionalSystems = new Dictionary<long, long>();

        Dictionary<long, long> m_dictSystemItemExclude = new Dictionary<long, long>();
        Dictionary<long, Dictionary<long, long>> m_Data = new Dictionary<long, Dictionary<long, long>>();
        public Dictionary<long, long> dictInputVoltageIndexes = new Dictionary<long, long>();
        public long m_RPNCount = 0;

        int m_singleLineHeight = 0;
        bool m_bDataLoadEnd = false;
        long m_EquipmentKindID = 0;

        public long? m_ProtectionOilType = null;
        public long m_CoolingSystemTypeID = -1;
        public long m_RPNKind = -1;

        public InspectionForm(long equipmentID, Inspection.InspectionType type)
        {
            m_EquipmentID = equipmentID;
            m_type = type;
            InitializeComponent();
        }

        Dictionary<string, int> dictBandHeigths = new Dictionary<string, int>();

        private void AddParam(SQLiteCommand com, string name, DbType type, object value)
        {
            SQLiteParameter param = new SQLiteParameter(name, type);
            param.Value = value;
            com.Parameters.Add(param);
        }

        private void LoadData()
        {
            try
            {
                m_con.Open();
                SQLiteCommand com = new SQLiteCommand(m_con);
                com.CommandType = CommandType.Text;

                com.CommandText = "Select e.EquipmentName, e.EquipmentNumber, " +
                    "ihA.InputKind as InputKindHighA, ihB.InputKind as InputKindHighB, ihC.InputKind as InputKindHighC, " +
                    "imA.InputKind as InputKindMiddleA, imB.InputKind as InputKindMiddleB, imC.InputKind as InputKindMiddleC, iNN.InputKind as InputKindNeutral, " +
                    "e.RPNCnt, e.ProtectionOilTypeID, e.CoolingSystemTypeID, e.RPNKind, e.EquipmentKindID " +
                    "FROM Equipments as e " +
                    "LEFT JOIN Inputs as ihA ON ihA.InputID = e.InputIDHighA " +
                    "LEFT JOIN Inputs as ihB ON ihB.InputID = e.InputIDHighB " +
                    "LEFT JOIN Inputs as ihC ON ihC.InputID = e.InputIDHighC " +
                    "LEFT JOIN Inputs as imA ON imA.InputID = e.InputIDMiddleA " +
                    "LEFT JOIN Inputs as imB ON imB.InputID = e.InputIDMiddleB " +
                    "LEFT JOIN Inputs as imC ON imC.InputID = e.InputIDMiddleC " +
                    "LEFT JOIN Inputs as iNN ON iNN.InputID = e.InputIDNeutral " +
                    "WHERE EquipmentID = @id";
                AddParam(com, "@id", DbType.Int64, m_EquipmentID);

                SQLiteDataReader drEquip = com.ExecuteReader();
                if (drEquip.HasRows)
                {
                    while (drEquip.Read())
                    {
                        teEquipmentName.Text = drEquip["EquipmentName"].ToString();
                        teEquipmentNumber.Text = drEquip["EquipmentNumber"].ToString();
                        m_EquipmentKindID = Convert.ToInt64(drEquip["EquipmentKindID"]);

                        if (drEquip["InputKindHighA"] != DBNull.Value) dictInputVoltageIndexes[1] = Convert.ToInt64(drEquip["InputKindHighA"]);
                        else dictInputVoltageIndexes[1] = -1;
                        if (drEquip["InputKindHighB"] != DBNull.Value) dictInputVoltageIndexes[2] = Convert.ToInt64(drEquip["InputKindHighB"]);
                        else dictInputVoltageIndexes[2] = -1;
                        if (drEquip["InputKindHighC"] != DBNull.Value) dictInputVoltageIndexes[3] = Convert.ToInt64(drEquip["InputKindHighC"]);
                        else dictInputVoltageIndexes[3] = -1;
                        if (drEquip["InputKindMiddleA"] != DBNull.Value) dictInputVoltageIndexes[4] = Convert.ToInt64(drEquip["InputKindMiddleA"]);
                        else dictInputVoltageIndexes[4] = -1;
                        if (drEquip["InputKindMiddleB"] != DBNull.Value) dictInputVoltageIndexes[5] = Convert.ToInt64(drEquip["InputKindMiddleB"]);
                        else dictInputVoltageIndexes[5] = -1;
                        if (drEquip["InputKindMiddleC"] != DBNull.Value) dictInputVoltageIndexes[6] = Convert.ToInt64(drEquip["InputKindMiddleC"]);
                        else dictInputVoltageIndexes[6] = -1;
                        if (drEquip["InputKindNeutral"] != DBNull.Value) dictInputVoltageIndexes[7] = Convert.ToInt64(drEquip["InputKindNeutral"]);
                        else dictInputVoltageIndexes[7] = -1;

                        if (drEquip["RPNCnt"] != DBNull.Value && Convert.ToInt64(drEquip["RPNCnt"]) != 0)
                            m_RPNCount = Convert.ToInt64(drEquip["RPNCnt"]);

                        if (drEquip["ProtectionOilTypeID"] != DBNull.Value)
                            m_ProtectionOilType = Convert.ToInt64(drEquip["ProtectionOilTypeID"]);

                        if (drEquip["CoolingSystemTypeID"] != DBNull.Value)
                            m_CoolingSystemTypeID = Convert.ToInt64(drEquip["CoolingSystemTypeID"]);

                        if (drEquip["RPNKind"] != DBNull.Value)
                            m_RPNKind = Convert.ToInt64(drEquip["RPNKind"]);

                        break;
                    }
                }
                drEquip.Close();

                // ---------------------
                // Данные об исключении подсистем или параметров в списке
                // ExcludeType: 1 – исп. для объекта “Тип системы охлаждения”, 2 – исп. для объекта Тип РПН, 3 – исп. для объекта “Тип защиты масла”, 4 – исп. для объекта “Вид ввода”
                com.CommandText = "SELECT SystemItemID, ParameterID, ExcludeType, ExcludeObjectID FROM ParameterExcludes WHERE (" +
                    " ExcludeType = 1 AND COALESCE(ExcludeObjectID, -1) = @csid " +
                    " OR ExcludeType = 2 AND COALESCE(ExcludeObjectID, -1) = @rpn_kind " +
                    " OR ExcludeType = 3 AND COALESCE(ExcludeObjectID, -1) = @potid " +
                    " OR ExcludeType = 4 AND COALESCE(ExcludeObjectID, -1) = @vkid1 " +
                    " OR ExcludeType = 5 AND COALESCE(ExcludeObjectID, -1) = @vkid2 " +
                    " OR ExcludeType = 6 AND COALESCE(ExcludeObjectID, -1) = @vkid3 " +
                    " OR ExcludeType = 7 AND COALESCE(ExcludeObjectID, -1) = @vkid4 " +
                    " OR ExcludeType = 8 AND COALESCE(ExcludeObjectID, -1) = @vkid5 " +
                    " OR ExcludeType = 9 AND COALESCE(ExcludeObjectID, -1) = @vkid6 " +
                    " OR ExcludeType = 10 AND COALESCE(ExcludeObjectID, -1) = @vkid7 " +
                ") AND EquipmentKindID = @eq_kind";

                com.Parameters.Clear();
                AddParam(com, "@csid", DbType.Int64, m_CoolingSystemTypeID);
                AddParam(com, "@rpn_kind", DbType.Int64, m_RPNKind);
                AddParam(com, "@potid", DbType.Int64, m_ProtectionOilType);
                AddParam(com, "@eq_kind", DbType.Int64, m_EquipmentKindID);
                AddParam(com, "@vkid1", DbType.Int64, dictInputVoltageIndexes[1]);
                AddParam(com, "@vkid2", DbType.Int64, dictInputVoltageIndexes[2]);
                AddParam(com, "@vkid3", DbType.Int64, dictInputVoltageIndexes[3]);
                AddParam(com, "@vkid4", DbType.Int64, dictInputVoltageIndexes[4]);
                AddParam(com, "@vkid5", DbType.Int64, dictInputVoltageIndexes[5]);
                AddParam(com, "@vkid6", DbType.Int64, dictInputVoltageIndexes[6]);
                AddParam(com, "@vkid7", DbType.Int64, dictInputVoltageIndexes[7]);

                SQLiteDataReader drParameterExclude = com.ExecuteReader();
                if (drParameterExclude.HasRows)
                {
                    while (drParameterExclude.Read())
                    {
                        long SystemItemID = -1;
                        if (drParameterExclude["SystemItemID"] != DBNull.Value)
                            SystemItemID = Convert.ToInt64(drParameterExclude["SystemItemID"]);
                        long ParameterID = -1;
                        if (drParameterExclude["ParameterID"] != DBNull.Value)
                            ParameterID = Convert.ToInt64(drParameterExclude["ParameterID"]);

                        if (SystemItemID > 0) m_dictSystemItemExclude[SystemItemID] = 1;
                    }
                }
                drParameterExclude.Close();

                // ---------------------

                com.CommandText = "Select FunctionalSystemID, FunctionalSystemName " +
                    " FROM FunctionalSystems WHERE EXISTS (SELECT * FROM SystemItems " +
                    "WHERE SystemItems.FunctionalSystemID = FunctionalSystems.FunctionalSystemID " +
                    "AND (UseInVisual <> 0 AND @type = 0 OR UseInHARG <> 0 AND @type = 1 OR UseInFHA <> 0 AND @type = 2 " +
                    " OR UseInWarm <> 0 AND @type = 3  OR UseInVibro <> 0 AND @type = 4 OR UseInParameter <> 0 AND @type = 5 OR UseInElectrical <> 0 AND @type = 6) AND EquipmentKindID = @eq_kind) AND EquipmentKindID = @eq_kind ORDER BY Sort";

                com.Parameters.Clear();
                AddParam(com, "@type", DbType.Int64, (long)m_type);
                AddParam(com, "@eq_kind", DbType.Int64, (long)m_EquipmentKindID);

                SQLiteDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        long FunctionalSystemID = Convert.ToInt64(dr["FunctionalSystemID"]);
                        string strFunctionalSystemName = dr["FunctionalSystemName"].ToString();

                        m_list.Add(new FunctionalSystem(FunctionalSystemID, strFunctionalSystemName));
                        dictFunctionalSystemPos[FunctionalSystemID] = m_list.Count - 1;
                    }
                }
                dr.Close();

                com.CommandText = "Select SystemItemID, FunctionalSystemID, SystemItemName, MinRPNCnt, InputVoltageIndex " +
                    " FROM SystemItems WHERE (UseInVisual <> 0 AND @type = 0 OR UseInHARG <> 0 AND @type = 1 OR UseInFHA <> 0 AND @type = 2 " +
                    " OR UseInWarm <> 0 AND @type = 3  OR UseInVibro <> 0 AND @type = 4 OR UseInParameter <> 0 AND @type = 5 OR UseInElectrical <> 0 AND @type = 6) AND EquipmentKindID = @eq_kind ORDER BY SystemItemName";

                SQLiteDataReader drSystemItem = com.ExecuteReader();
                if (drSystemItem.HasRows)
                {
                    while (drSystemItem.Read())
                    {
                        long SystemItemID = Convert.ToInt64(drSystemItem["SystemItemID"]);
                        long FunctionalSystemID = Convert.ToInt64(drSystemItem["FunctionalSystemID"]);
                        string strSystemItemName = drSystemItem["SystemItemName"].ToString();

                        long? MinRPNCount = null;
                        if (drSystemItem["MinRPNCnt"] != DBNull.Value)
                            MinRPNCount = Convert.ToInt64(drSystemItem["MinRPNCnt"]);
                        long? InputVoltageIndex = null;
                        if (drSystemItem["InputVoltageIndex"] != DBNull.Value)
                            InputVoltageIndex = Convert.ToInt64(drSystemItem["InputVoltageIndex"]);

                        // ------------------------
                        // ограничения
                        // ------------------------
                        if (MinRPNCount != null && MinRPNCount > m_RPNCount) continue;
                        if (InputVoltageIndex != null && dictInputVoltageIndexes[(long)InputVoltageIndex] <= 0) continue;
                        if (m_dictSystemItemExclude.ContainsKey(SystemItemID)) continue;

                        m_list[dictFunctionalSystemPos[FunctionalSystemID]].m_listSystemItems.Add(new SystemItem(SystemItemID, strSystemItemName));
                    }
                }
                drSystemItem.Close();

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

        private void CreateGrid()
        {
            int iCnt = 1;
            Graphics formGraphics = this.CreateGraphics();

            for (int i = 0; i < GridView.Columns.Count; i++)
            {            	
                GridView.Columns[i].Width = (int)(GridView.Columns[i].Width * formGraphics.DpiX / 96);
            }

            for (int i = 0; i < m_list.Count; i++)
            {
                if (m_list[i].m_listSystemItems.Count == 0) continue;

                DevExpress.XtraGrid.Views.BandedGrid.GridBand gb = GridView.Bands.AddBand(m_list[i].m_strFunctionalSystemName);
                gb.OptionsBand.AllowMove = false;
                gb.AppearanceHeader.Options.UseTextOptions = true;
                gb.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gb.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                gb.AutoFillDown = true;
                gb.Name = "band_" + i.ToString();

                for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                {
                    BandedGridColumn col = new BandedGridColumn();
                    col.Name = m_list[i].m_listSystemItems[j].m_SystemItemID.ToString();
                    col.Caption = m_list[i].m_listSystemItems[j].m_strSystemItemName;
                    col.OptionsColumn.AllowMove = false;
                    col.OptionsColumn.AllowEdit = false;
                    col.Width = (int)(150 * formGraphics.DpiX / 96);
                    col.Visible = true;
                    col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;

                    if (m_list[i].m_listSystemItems[j].m_strSystemItemName != m_list[i].m_strFunctionalSystemName)
                    {
                        DevExpress.XtraGrid.Views.BandedGrid.GridBand gb_ = gb.Children.AddBand(m_list[i].m_listSystemItems[j].m_strSystemItemName);
                        gb_.OptionsBand.AllowMove = false;
                        gb_.Width = (int)(150 * formGraphics.DpiX / 96);

                        gb_.Columns.Add(col);
                        gb_.AppearanceHeader.Options.UseTextOptions = true;
                        gb_.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gb_.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                        gb_.AutoFillDown = true;
                        gb_.Name = "band_" + i.ToString() + "_" + j.ToString();
                    }
                    else
                    {
                        gb.Columns.Add(col);
                    }

                    iCnt++;
                }
            }

            /*GridBandInfoArgs ex = null;
            BandedGridViewInfo viewInfo = GridView.GetViewInfo() as BandedGridViewInfo;
            viewInfo.GInfo.AddGraphics(null);
            ex = new GridBandInfoArgs(null, viewInfo.GInfo.Cache);
            try
            {
                ex.InnerElements.Add(new DrawElementInfo(new GlyphElementPainter(),
                                                        new GlyphElementInfoArgs(viewInfo.View.Images, 0, null),
                                                        StringAlignment.Near));
                ex.SetAppearance(GridView.Appearance.BandPanel);
                ex.Caption = "А";
                ex.CaptionRect = new Rectangle(0, 0, 100, 100);
            }
            finally
            {
                viewInfo.GInfo.ReleaseGraphics();
            }

            GraphicsInfo grInfo = new GraphicsInfo();
            grInfo.AddGraphics(null);
            ex.Cache = grInfo.Cache;
            Size captionSize = CalcCaptionTextSize(grInfo.Cache, ex as HeaderObjectInfoArgs, "А");
            m_singleLineHeight = captionSize.Height;*/

            BandedGridViewInfo viewInfo = GridView.GetViewInfo() as BandedGridViewInfo;
            m_singleLineHeight = viewInfo.BandRowHeight;

            GridView.BeginUpdate();

            for (int i = 0; i < GridView.Bands.Count; i++)
            {
                int Width = 0;
                for (int j = 0; j < GridView.Bands[i].Children.Count; j++)
                {
                    GridBand b = GridView.Bands[i].Children[j];
                    int child_height = GetColumnBestHeight(b, b.Width);
                    b.RowCount = child_height / m_singleLineHeight;
                    dictBandHeigths[b.Name] = child_height / m_singleLineHeight;

                    Width += b.Width;
                }

                if (GridView.Bands[i].Children.Count > 0)
                {
                    int parent_height = GetColumnBestHeight(GridView.Bands[i], Width);
                    GridView.Bands[i].RowCount = parent_height / m_singleLineHeight;
                    dictBandHeigths[GridView.Bands[i].Name] = parent_height / m_singleLineHeight;
                }
            }

            int max_parent_row = -1;
            int max_row = -1;
            for (int i = 0; i < GridView.Bands.Count; i++)
            {
                if (GridView.Bands[i].Children.Count > 0)
                {
                    if (dictBandHeigths[GridView.Bands[i].Name] > max_parent_row) max_parent_row = dictBandHeigths[GridView.Bands[i].Name];
                }

                for (int j = 0; j < GridView.Bands[i].Children.Count; j++)
                {
                    GridBand b = GridView.Bands[i].Children[j];

                    if (dictBandHeigths[b.Name] > max_row) max_row = dictBandHeigths[b.Name];
                }
            }

            for (int i = 0; i < GridView.Bands.Count; i++)
            {
                if (GridView.Bands[i].Children.Count > 0)
                {
                    GridView.Bands[i].RowCount = max_parent_row;
                }

                for (int j = 0; j < GridView.Bands[i].Children.Count; j++)
                {
                    GridBand b = GridView.Bands[i].Children[j];

                    b.RowCount = max_row;
                }
            }

            GridView.EndUpdate();
        }

        private void RefreshData()
        {
            try
            {
                m_con.Open();

                SQLiteCommand com = new SQLiteCommand(m_con);
                com.CommandType = CommandType.Text;

                com.CommandText = "SELECT SystemItemResults.InspectionID, SystemItemResults.SystemItemID, SystemItemResults.Result FROM SystemItemResults INNER JOIN Inspections ON " +
                       "Inspections.InspectionID = SystemItemResults.InspectionID WHERE Inspections.InspectionType = @type AND Inspections.EquipmentID = @eq_id";

                com.Parameters.Clear();
                AddParam(com, "@type", DbType.Int64, (long)m_type);
                AddParam(com, "@eq_id", DbType.Int64, (long)m_EquipmentID);

                m_Data.Clear();
                SQLiteDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        long InspectionID = Convert.ToInt64(dr["InspectionID"]);
                        long SystemItemID = Convert.ToInt64(dr["SystemItemID"]);
                        long Result = Convert.ToInt64(dr["Result"]);

                        if (!m_Data.ContainsKey(InspectionID)) m_Data[InspectionID] = new Dictionary<long, long>();
                        m_Data[InspectionID][SystemItemID] = Result;
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

        private void InspectionForm_Load(object sender, EventArgs e)
        {
            pGreen.BackColor = Color.Green;
            pYellow.BackColor = Color.Yellow;
            pOrange.BackColor = Color.Orange;
            pRed.BackColor = Color.Red;
            pGray.BackColor = Color.DarkGray;
            pBlack.BackColor = Color.Black;

            // TODO: This line of code loads data into the 'dataSetInspection.Inspections' table. You can move, or remove it, as needed.
            this.qInspectionsTableAdapter.Fill(this.dataSetQuery.QInspections, m_EquipmentID, (long)m_type);

            LoadData();
            CreateGrid();
            RefreshData();

            ControlNavigatorButtons cnb = controlNavigator1.Buttons;
            if (GridView.FocusedRowHandle < 0)
                cnb.CustomButtons[0].Enabled = false;// cnb.Remove.Enabled;
            else
                cnb.CustomButtons[0].Enabled = true;

            switch (m_type)
            {
                case Inspection.InspectionType.Visual:
                    gcInspectionTime.Text = "Список визуальных обследований";
                    Text = "Визуальные обследования";
                    break;
                case Inspection.InspectionType.HARG:
                    gcInspectionTime.Text = "Список обследований ХАРГ";
                    Text = "ХАРГ";
                    break;
                case Inspection.InspectionType.FHA:
                    gcInspectionTime.Text = "Список обследований ФХА";
                    Text = "ФХА";
                    break;
                case Inspection.InspectionType.Warm:
                    gcInspectionTime.Text = "Список тепловизионных обследований";
                    Text = "Тепловизионные обследования";
                    break;
                case Inspection.InspectionType.Vibro:
                    gcInspectionTime.Text = "Список вибрационных обследований";
                    Text = "Вибрационные обследования";
                    break;
                case Inspection.InspectionType.Parameter:
                    gcInspectionTime.Text = "Список определений характеристик выключателя";
                    Text = "Определение характеристик выключателя";
                    break;
                case Inspection.InspectionType.Electrical:
                    gcInspectionTime.Text = "Список электрических измерений выключателя";
                    Text = "Электрические измерения выключателя";
                    break;
                default:
                    break;
            }

            m_bDataLoadEnd = true;
        }

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ControlNavigatorButtons cnb = controlNavigator1.Buttons;
            if (e.FocusedRowHandle < 0)
                cnb.CustomButtons[0].Enabled = false;// cnb.Remove.Enabled;
            else
                cnb.CustomButtons[0].Enabled = true;
        }

        private void GridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.AbsoluteIndex >= 4)
            {
                //e.Appearance.BackColor = Color.Orange;
                long SystemItemID = Convert.ToInt64(e.Column.Name);
                long InspectionID = Convert.ToInt64(GridView.GetRowCellValue(e.RowHandle, "InspectionID"));

                if (m_Data.ContainsKey(InspectionID))
                {
                    if (m_Data[InspectionID].ContainsKey(SystemItemID))
                    {
                        switch (m_Data[InspectionID][SystemItemID])
                        {
                            case 0:
                                e.Appearance.BackColor = Color.Green;
                                break;
                            case 1:
                                e.Appearance.BackColor = Color.Yellow;
                                break;
                            case 2:
                                e.Appearance.BackColor = Color.Orange;
                                break;
                            case 3:
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

        private void GridView_KeyDown(object sender, KeyEventArgs e)
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

                this.qInspectionsTableAdapter.Fill(this.dataSetQuery.QInspections, m_EquipmentID, (long)m_type);
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
                this.qInspectionsTableAdapter.Fill(this.dataSetQuery.QInspections, m_EquipmentID, (long)m_type);
                RefreshData();

                for (int i = 0; i < GridView.RowCount/*this.dataSetQuery.QEquipments.Rows.Count*/; i++)
                {
                    //DataRow r = this.dataSetQuery.QEquipments.Rows[i];
                    //int id_ = Convert.ToInt64(r["EquipmentID"]);
                    long id_ = Convert.ToInt64(GridView.GetRowCellValue(i, "InspectionID"));
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

                DataRowView drv = (DataRowView)(this.qInspectionsBindingSource.Current);

                if (MyLocalizer.XtraMessageBoxShow("Удалить обследование?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    long id = Convert.ToInt64(drv.Row["InspectionID"]);

                    SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                    connection.Open();
                    SQLiteCommand com = new SQLiteCommand(connection);
                    com.CommandType = CommandType.Text;

                    SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.Int64);
                    param1.Value = id;
                    com.Parameters.Add(param1);

                    com.CommandText = "DELETE FROM SystemItemResults WHERE InspectionID = ?";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM FunctionalSystemResults WHERE InspectionID = ?";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM InspectionDatas WHERE InspectionID = ?";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM Inspections WHERE InspectionID = ?";
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
            //this.ShowInTaskbar = false;
            InspectionDataForm form = new InspectionDataForm(m_EquipmentID, m_type, 0, -1);
            DialogResult dr = form.ShowDialog(this);
            //this.ShowInTaskbar = true;
            long id = form.m_InspectionID;
            if (dr == System.Windows.Forms.DialogResult.OK)
                RefreshGridPos(id);
        }

        public void UpdateRecord()
        {
            if (GridView.FocusedRowHandle < 0) return;

            DataRowView drv = (DataRowView)(this.qInspectionsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["InspectionID"]);

            //this.ShowInTaskbar = false;
            InspectionDataForm form = new InspectionDataForm(m_EquipmentID, m_type, id, -1);
            DialogResult dr = form.ShowDialog(this);
            //this.ShowInTaskbar = true;
            if (dr == System.Windows.Forms.DialogResult.OK)
                RefreshGridPos(id);
        }

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

        private int GetColumnBestHeight(GridBand column, int width/*, GridBand parent_column*/)
        {
            /*GridBandInfoArgs ex = null;

            if (parent_column == null)
                ex = viewInfo.BandsInfo[column];
            else
                ex = viewInfo.BandsInfo[parent_column].Children[column];

            GraphicsInfo grInfo = new GraphicsInfo();
            grInfo.AddGraphics(null);
            ex.Cache = grInfo.Cache;
            bool canDrawMore = true;
            Size captionSize = CalcCaptionTextSize(grInfo.Cache, ex as HeaderObjectInfoArgs, column.Caption);
            Size res = ex.InnerElements.CalcMinSize(grInfo.Graphics, ref canDrawMore);
            res.Height = Math.Max(res.Height, captionSize.Height);
            res.Width += captionSize.Width;
            //res = viewInfo.Painter.ElementsPainter.Column.CalcBoundsByClientRectangle(ex, new Rectangle(Point.Empty, res)).Size;
            grInfo.ReleaseGraphics();
            return res.Height;*/

            GridBandInfoArgs ex = null;
            BandedGridViewInfo viewInfo = GridView.GetViewInfo() as BandedGridViewInfo;
            viewInfo.GInfo.AddGraphics(null);
            ex = new GridBandInfoArgs(null, viewInfo.GInfo.Cache);
            try
            {
                ex.InnerElements.Add(new DrawElementInfo(new GlyphElementPainter(),
                                                        new GlyphElementInfoArgs(viewInfo.View.Images, 0, null),
                                                        StringAlignment.Near));
                ex.SetAppearance(GridView.Appearance.BandPanel);
                ex.Caption = column.Caption;
                ex.CaptionRect = new Rectangle(0, 0, width, 1000);
            }
            finally
            {
                viewInfo.GInfo.ReleaseGraphics();
            }

            GraphicsInfo grInfo = new GraphicsInfo();
            grInfo.AddGraphics(null);
            ex.Cache = grInfo.Cache;
            Size captionSize = CalcCaptionTextSize(grInfo.Cache, ex as HeaderObjectInfoArgs, column.Caption);
            return captionSize.Height;
        }

        Size CalcCaptionTextSize(GraphicsCache cache, HeaderObjectInfoArgs ee, string caption)
        {
            Size captionSize = ee.Appearance.CalcTextSize(cache, caption, ee.CaptionRect.Width).ToSize();
            //captionSize.Height++; captionSize.Width++;
            return captionSize;
        }

        private void SetBandHeight(GridBand band)
        {
            int parent_height = -1;
            int child_height = -1;
            int parent_row = -1;
            int child_row = -1;


            GridBand parent_band = null;
            GridBand child_band = null;

            int width = 0;

            if (band.ParentBand != null)
            {
                parent_band = band.ParentBand;
                child_band = band;
            }
            if (band.ParentBand == null && band.Children.Count > 0)
            {
                parent_band = band;
                child_band = band.Children[band.Children.Count - 1];
            }
            if (band.ParentBand == null && band.Children.Count == 0)
            {
                return;
            }

            for (int i = 0; i < parent_band.Children.Count; i++)
            {
                width += parent_band.Children[i].Width;                
            }

            GridView.BeginUpdate();

            parent_height = GetColumnBestHeight(parent_band, width);
            if (child_band != null)
                child_height = GetColumnBestHeight(child_band, child_band.Width);

            parent_row = (parent_height / m_singleLineHeight);
            parent_band.RowCount = parent_row;

            dictBandHeigths[parent_band.Name] = parent_band.RowCount;

            if (child_band != null)
            {
                child_row = (child_height / m_singleLineHeight);
                child_band.RowCount = child_row;

                dictBandHeigths[child_band.Name] = child_band.RowCount;
            }

            int max_parent_row = -1;
            int max_row = -1;
            for (int i = 0; i < GridView.Bands.Count; i++)
            {
                if (GridView.Bands[i].Children.Count > 0)
                {
                    if (dictBandHeigths[GridView.Bands[i].Name] > max_parent_row) max_parent_row = dictBandHeigths[GridView.Bands[i].Name];
                }
                
                for (int j = 0; j < GridView.Bands[i].Children.Count; j++)
                {
                    GridBand b = GridView.Bands[i].Children[j];

                    if (dictBandHeigths[b.Name] > max_row) max_row = dictBandHeigths[b.Name];
                }
            }

            if (parent_row > max_parent_row) max_parent_row = parent_row;
            if (child_row > max_row) max_row = child_row;

            for (int i = 0; i < GridView.Bands.Count; i++)
            {
                if (GridView.Bands[i].Children.Count > 0)
                {
                    GridView.Bands[i].RowCount = max_parent_row;
                }

                for (int j = 0; j < GridView.Bands[i].Children.Count; j++)
                {
                    GridBand b = GridView.Bands[i].Children[j];

                    b.RowCount = max_row;
                }
            }

            GridView.EndUpdate();
        }

        private void GridView_BandWidthChanged(object sender, BandEventArgs e)
        {
            SetBandHeight(e.Band);
        }

        private void InspectionForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Form f = this.Owner;
                while (f.Owner != null)
                {
                    f.Hide();
                    f = f.Owner;
                }
                f.Hide();// = FormWindowState.Minimized;
                //this.ShowInTaskbar = true;

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

        private void InspectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }       
    }
}