using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using DevExpress.Skins;

namespace DiarMain
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        
        /*[DllImport("kernel32.dll")]
        public static extern int GetVolumeInformation(
            string strPathName,
            StringBuilder strVolumeNameBuffer,
            int lngVolumeNameSize,
            out uint lngVolumeSerialNumber,
            out uint lngMaximumComponentLength,
            out uint lngFileSystemFlags,
            StringBuilder strFileSystemNameBuffer,
            int lngFileSystemNameSize);*/

        /*[SQLiteFunction(Name = "RU", FuncType = FunctionType.Collation)]
        class MySequence : SQLiteFunction
        {
            public override int Compare(string param1, string param2)
            {
                return String.Compare(param1, param2, true);
            }
        }*/
        [SQLiteFunction(Arguments = 2, FuncType = FunctionType.Scalar, Name = "COMPARE_STR")]
        class COMPARE_STR : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                string s1 = (args[0] as string).ToLower();
                string s2 = (args[1] as string).ToLower();
                return s1.IndexOf(s2);
            }
        }

        [SQLiteFunction(Arguments = 2, FuncType = FunctionType.Scalar, Name = "EQUAL_STR")]
        class EQUAL_STR : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                string s1 = (args[0] as string).ToLower();
                string s2 = (args[1] as string).ToLower();
                return s1 == s2 ? 0 : 1;
            }
        }

        //string m_FilterCellValue;
        //int m_FilterColumnIndex;
        bool m_bShowCloseMsg;
        string m_strFilterString = "";
        string m_strIDsString = "";
        public bool m_bAddPassportMessage = false;
        public string m_strLicenseCode;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


            /*const int MAX_SIZE = 256;
            //метка диска
            StringBuilder volname = new StringBuilder(MAX_SIZE);
            //серийный номер диска
            uint sn;
            uint maxcomplen;//максимальное кол-во компонент
            uint sysflags;//системные флаги
            StringBuilder sysname = new StringBuilder(MAX_SIZE);//файловая система

            string strDriveName = "";
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if (Environment.CurrentDirectory.IndexOf(di.Name) == 0)
                {
                    strDriveName = di.Name;
                    break;
                }
            }

            //Environment.CurrentDirectory
            GetVolumeInformation(strDriveName, volname, MAX_SIZE, out sn, out maxcomplen, out sysflags, sysname, MAX_SIZE);
            MyLocalizer.XtraMessageBoxShow(sn.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            */

            /*ObjectQuery winQuery = new ObjectQuery("SELECT * FROM CIM_Processor");//процессор

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(winQuery);
            
            foreach (ManagementObject item in searcher.Get())
            {

                foreach (PropertyData pr in item.Properties)
                {
                    if (pr.Value != null)
                        MyLocalizer.XtraMessageBoxShow(pr.Name + ": " + pr.Value.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }*/

            Localizer.Active = new MyLocalizer();

            int index = 0;
            foreach (SkinContainer cnt in SkinManager.Default.Skins)
            {
                if (cnt.SkinName.IndexOf("MySkin") < 0)
                {
                    bool bChecked = false;
                    if (cnt.SkinName == defaultSkin.LookAndFeel.SkinName) bChecked = true;
                    DevExpress.XtraBars.BarCheckItem bi = new DevExpress.XtraBars.BarCheckItem(main_menu, bChecked);
                    bi.Caption = cnt.SkinName;
                    bi.Name = "SettingSubMenu" + index.ToString();
                    bi.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    bi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bi_ItemClick);
                    StyleSubMenu.AddItem(bi);

                    index++;
                }
            }

            m_bShowCloseMsg = true;
            SQLiteConnection con = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
            try
            {
                con.Open();
                con.Close();


                m_strFilterString = "###";
                FindEquipments(-1);

                btnHARG.LookAndFeel.SetSkinStyle("MySkin_StyleHARG");
                btnFHA.LookAndFeel.SetSkinStyle("MySkin_StyleFHA");
                btnVisual.LookAndFeel.SetSkinStyle("MySkin_StyleVisual");
                btnWarm.LookAndFeel.SetSkinStyle("MySkin_StyleWarm");
                btnVibro.LookAndFeel.SetSkinStyle("MySkin_StyleVibro");
                //btnPassport3.LookAndFeel.SetSkinStyle("MySkin_StylePassport");
                btnPassport.LookAndFeel.SetSkinStyle("MySkin_StylePassport");
                btnPassportAdd.LookAndFeel.SetSkinStyle("MySkin_StyleAdd2");
                btnReport.LookAndFeel.SetSkinStyle("MySkin_StyleReport");

                /*toolTip.AddClientControl(btnHARG);
                toolTip.AddClientControl(btnFHA);
                toolTip.AddClientControl(btnVisual);
                toolTip.AddClientControl(btnWarm);
                toolTip.AddClientControl(btnVibro);*/
            }
            catch(SQLiteException ex)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось подключиться к базе данных. Описание: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_bShowCloseMsg = false;
                Close();
            }
        }

        void bi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (DevExpress.XtraBars.BarCheckItemLink bi in StyleSubMenu.ItemLinks)
            {
                ((DevExpress.XtraBars.BarCheckItem)bi.Item).Checked = false;
                if (bi.Caption == e.Item.Caption) ((DevExpress.XtraBars.BarCheckItem)bi.Item).Checked = true;
            }
            defaultSkin.LookAndFeel.SetSkinStyle(e.Item.Caption);
        }

        private void FindEquipments(long id)
        {
            /*SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
            connection.Open();
            SQLiteCommand com = new SQLiteCommand(connection);
            com.CommandText = "Select COUNT(*) AS Cnt from EquipmentKinds AS e " +
                "WHERE COMPARE_STR(e.EquipmentKindName, 'транс') >= 0";
            com.CommandType = CommandType.Text;
            SQLiteDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                long cnt = Convert.ToInt64(dr.GetValue(0));
            }
            dr.Close();*/

            if (id <= 0)
            {
                m_strIDsString = "";

                this.qMainEquipmentsTableAdapter.SetCommandText("SELECT e.EquipmentID, sb.SubjectName, b.BranchName, s.SubstationName, " +
                    "ek.EquipmentKindName, e.EquipmentName, e.EquipmentNumber, e.EquipmentKindID " +
                    "FROM Equipments AS e " +
                    "INNER JOIN Substations AS s ON s.SubstationID = e.SubstationID " +
                    "INNER JOIN Branches AS b ON b.BranchID = s.BranchID " +
                    "INNER JOIN Subjects AS sb ON sb.SubjectID = b.SubjectID " +
                    "INNER JOIN EquipmentKinds AS ek ON ek.EquipmentKindID = e.EquipmentKindID " +
                    "WHERE  " +
                    "COMPARE_STR(sb.SubjectName, ?) >= 0 " +
                    "OR COMPARE_STR(b.BranchName, ?) >= 0 " +
                    "OR COMPARE_STR(s.SubstationName, ?) >= 0 " +
                    "OR COMPARE_STR(e.EquipmentName, ?) >= 0 " +
                    "OR COMPARE_STR(e.EquipmentNumber, ?) >= 0 " +
                    "OR COMPARE_STR(ek.EquipmentKindName, ?) >= 0");
            }
            else
            {
                if (m_strIDsString == "") m_strIDsString = id.ToString();
                else m_strIDsString = m_strIDsString + "," + id.ToString();

                this.qMainEquipmentsTableAdapter.SetCommandText("SELECT e.EquipmentID, sb.SubjectName, b.BranchName, s.SubstationName, " +
                    "ek.EquipmentKindName, e.EquipmentName, e.EquipmentNumber, e.EquipmentKindID " +
                    "FROM Equipments AS e " +
                    "INNER JOIN Substations AS s ON s.SubstationID = e.SubstationID " +
                    "INNER JOIN Branches AS b ON b.BranchID = s.BranchID " +
                    "INNER JOIN Subjects AS sb ON sb.SubjectID = b.SubjectID " +
                    "INNER JOIN EquipmentKinds AS ek ON ek.EquipmentKindID = e.EquipmentKindID " +
                    "WHERE e.EquipmentID IN (" + m_strIDsString + ")" +
                    "OR COMPARE_STR(sb.SubjectName, ?) >= 0 " +
                    "OR COMPARE_STR(b.BranchName, ?) >= 0 " +
                    "OR COMPARE_STR(s.SubstationName, ?) >= 0 " +
                    "OR COMPARE_STR(e.EquipmentName, ?) >= 0 " +
                    "OR COMPARE_STR(e.EquipmentNumber, ?) >= 0 " +
                    "OR COMPARE_STR(ek.EquipmentKindName, ?) >= 0");

                //DataRowBuilder rb = new DataRowBuilder();
                //DataSetQuery.QMainEquipmentsRow row = new DataSetQuery.QMainEquipmentsRow(rb);
                //this.dataSetQuery.QMainEquipments.AddQMainEquipmentsRow(row);
            }

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            if (m_strFilterString != "")
            {
                string strFind = m_strFilterString;
                this.qMainEquipmentsTableAdapter.Fill(this.dataSetQuery.QMainEquipments, strFind, strFind, strFind, strFind, strFind, strFind);
            }
            else
                this.qMainEquipmentsTableAdapter.Fill(this.dataSetQuery.QMainEquipments, "", "", "", "", "", "");
            this.Cursor = System.Windows.Forms.Cursors.Default;

            RefreshButtons();
        }


        public void RefreshButtons()
        {
            if (this.dataSetQuery.QMainEquipments.Count == 0)
            {
                btnPassport.Enabled = false;
                //btnPassport.LookAndFeel.SetSkinStyle(this.LookAndFeel.ActiveSkinName);
                btnVisual.Enabled = false;
                btnWarm.Enabled = false;
                btnVibro.Enabled = false;
                btnHARG.Enabled = false;
                btnFHA.Enabled = false;
                btnReport.Enabled = false;
            }
            else
            {
                btnPassport.Enabled = true;
                //btnPassport.LookAndFeel.SetSkinStyle("MySkin_StylePassport");
                btnVisual.Enabled = true;
                btnWarm.Enabled = true;
                btnVibro.Enabled = true;
                btnHARG.Enabled = true;
                btnFHA.Enabled = true;
                btnReport.Enabled = true;
            }
        }

        private void bAcceptFind_Click(object sender, EventArgs e)
        {
            m_strFilterString = tFind.Text;
            FindEquipments(-1);
            MainGridView.ExpandAllGroups();

            if (MainGridView.RowCount == 0)
            {
                NoFindEquipmentMessageForm f = new NoFindEquipmentMessageForm();

                f.m_strMessage = "Оборудование по поиску \"" + m_strFilterString + "\" отсутствует в базе данных. Добавить оборудование?";

                if (f.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    long id = -1;

                    PassportDataForm rf = new PassportDataForm(id);
                    rf.m_bShowContinueMsg = true;
                    System.Windows.Forms.DialogResult dr = rf.ShowDialog(this);
                    id = rf.m_id;
                    if (dr == System.Windows.Forms.DialogResult.OK)
                        RefreshGridPos(id);

                    if (rf.m_bContinueNext)
                    {
                        ShowVisualForm(id, false);
                    }
                }
                else
                {
                    tFind.Focus();
                }
            }
        }

        private void tFind_KeyDown(object sender, KeyEventArgs e)
        {
            /*if ((e.KeyCode == Keys.Enter))
            {
                bAcceptFind_Click(sender, e);
            }*/
        }

        private void ClearMenuChecked()
        {
            for (int i = 0; i < main_menu.Items.Count; i++)
            {
                DevExpress.XtraBars.BarItem si = main_menu.Items[0];
                /*for (int j = 0; j < (si as ToolStripMenuItem).DropDownItems.Count; j++)
                {
                    ToolStripItem sd = (si as ToolStripMenuItem).DropDownItems[j];
                    (sd as ToolStripMenuItem).Checked = false;
                }*/
            }
        }

        private void FilterForSelect_Click(object sender, EventArgs e)
        {
            /*DevExpress.XtraGrid.Columns.ColumnFilterInfo filter = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(MainGridView.Columns[m_FilterColumnIndex].FieldName + " = '" + m_FilterCellValue + "'");
            MainGridView.ActiveFilter.Add(MainGridView.Columns[m_FilterColumnIndex], filter);

            for (int i = 0; i <= m_FilterColumnIndex; i++)
            {
            	if (!MainGridView.GroupedColumns.Contains(MainGridView.Columns[i]))
                {
                    MainGridView.Columns[i].Group();
                }
            }

            MainGridView.OptionsView.ShowGroupPanel = true;
            MainGridView.ExpandAllGroups();*/
        }

        private void MainGridView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                /*if (e.Column.AbsoluteIndex < 4)
                {
                    m_FilterCellValue = e.CellValue.ToString();
                    m_FilterColumnIndex = e.Column.AbsoluteIndex;

                    //mSeparator.Visible = true;
                    mFilterForSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    mFilterForSelect.Caption = "Добавить '" + m_FilterCellValue + "' в фильтр";
                }
                else*/
                {
                    //mSeparator.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    mFilterForSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                //Point p = new Point(e.Location.X, e.Location.Y);
                //p = panelControl2.PointToScreen(p);
                //popupMenu.ShowPopup(/*MainGridControl,*/ p);
            }
        }

        private void MainGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            /*if (MainGridView.ActiveFilterString == "" || MainGridView.ActiveFilterEnabled == false)
            {
                MainGridView.ClearGrouping();
                MainGridView.OptionsView.ShowGroupPanel = false;
            }*/
        }

        private void bCancelFind_Click(object sender, EventArgs e)
        {
            tFind.Text = "";
            m_strFilterString = "###";
            FindEquipments(-1);
            MainGridView.ExpandAllGroups();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_bShowCloseMsg && MyLocalizer.XtraMessageBoxShow("Вы действительно хотите выйти из программы?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void RefreshGridPos(long id)
        {
            int f_row = MainGridView.FocusedRowHandle;
            if (id <= 0)
            {
                /*if (f_row > 0) f_row--;
                FindEquipments();
                if (MainGridView.RowCount > f_row)
                {
                    MainGridView.ClearSelection();
                    MainGridView.SelectRow(f_row);
                    MainGridView.FocusedRowHandle = f_row;
                }*/

                return;
            }
            else
            {
                FindEquipments(id);

                for (int i = 0; i < MainGridView.RowCount/*this.dataSetQuery.QEquipments.Rows.Count*/; i++)
                {
                    //DataRow r = this.dataSetQuery.QEquipments.Rows[i];
                    //int id_ = Convert.ToInt64(r["EquipmentID"]);
                    long id_ = Convert.ToInt64(MainGridView.GetRowCellValue(i, "EquipmentID"));
                    if (id_ == id)
                    {
                        MainGridView.ClearSelection();
                        MainGridView.SelectRow(i);
                        MainGridView.FocusedRowHandle = i;
                        return;
                    }
                }
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
                    ShowVisualForm(id, false);
            }
        }

        public void UpdateRecord()
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            UpdatePassportData(id);
        }

        private void btnPassport_Click_1(object sender, EventArgs e)
        {
            UpdateRecord();
        }

        /*private void ShowParameterForm(long id, bool bAdd)
        {
            InspectionDataForm form = new InspectionDataForm(id, Inspection.InspectionType.Parameter, 0, -1);
            form.m_bShowContinueMsg = true;
            //if (!bAdd) form.m_dateForFind = DateTime.Now;
            DialogResult dr = form.ShowDialog(this);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (form.m_bContinueNext) ShowFHAForm(id, false);
                else
                {
                    if (form.m_bContinuePrev) UpdatePassportData(id);
                }
            }
        }*/

        private void ShowVisualForm(long id, bool bAdd)
        {
            //this.ShowInTaskbar = false;
            InspectionDataForm form = new InspectionDataForm(id, Inspection.InspectionType.Visual, 0, -1);
            form.m_bShowContinueMsg = true;
            //if (!bAdd) form.m_dateForFind = DateTime.Now;
            DialogResult dr = form.ShowDialog(this);
            //this.ShowInTaskbar = true;

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (form.m_bContinueNext) ShowFHAForm(id, false);
                else 
                {
                    if (form.m_bContinuePrev) UpdatePassportData(id);
                }
            }
        }

        private void ShowFHAForm(long id, bool bAdd)
        {
            //this.ShowInTaskbar = false;
            InspectionDataForm form = new InspectionDataForm(id, Inspection.InspectionType.FHA, 0, -1);
            form.m_bShowContinueMsg = true;
            //if (!bAdd) form.m_dateForFind = DateTime.Now;
            DialogResult dr = form.ShowDialog(this);
            //this.ShowInTaskbar = true;

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (form.m_bContinueNext) ShowHARGForm(id, false);
                else
                {
                    if (form.m_bContinuePrev) ShowVisualForm(id, false);
                }
            }
        }

        private void ShowHARGForm(long id, bool bAdd)
        {
            //this.ShowInTaskbar = false;
            InspectionDataForm form = new InspectionDataForm(id, Inspection.InspectionType.HARG, 0, -1);
            form.m_bShowContinueMsg = true;
            //if (!bAdd) form.m_dateForFind = DateTime.Now;
            DialogResult dr = form.ShowDialog(this);
            //this.ShowInTaskbar = true;

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (form.m_bContinueNext) ShowWarmForm(id, false);
                else
                {
                    if (form.m_bContinuePrev) ShowFHAForm(id, false);
                }
            }
        }

        private void ShowWarmForm(long id, bool bAdd)
        {
            //this.ShowInTaskbar = false;
            InspectionDataForm form = new InspectionDataForm(id, Inspection.InspectionType.Warm, 0, -1);
            form.m_bShowContinueMsg = true;
            //if (!bAdd) form.m_dateForFind = DateTime.Now;
            DialogResult dr = form.ShowDialog(this);
            //this.ShowInTaskbar = true;

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (form.m_bContinueNext) ShowVibroForm(id, false);
                else
                {
                    if (form.m_bContinuePrev) ShowHARGForm(id, false);
                }
            }
        }

        private void ShowVibroForm(long id, bool bAdd)
        {
            //this.ShowInTaskbar = false;
            InspectionDataForm form = new InspectionDataForm(id, Inspection.InspectionType.Vibro, 0, -1);
            form.m_bShowContinueMsg = true;
            //if (!bAdd) form.m_dateForFind = DateTime.Now;
            DialogResult dr = form.ShowDialog(this);
            //this.ShowInTaskbar = true;

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (form.m_bContinueNext)
                {
                    List<ReportInfo.Equipment> m_listEquipments = new List<ReportInfo.Equipment>();
                    Dictionary<Inspection.InspectionType, List<ReportInfo.InspectionSubType_>> m_list_sub_types = new Dictionary<Inspection.InspectionType, List<ReportInfo.InspectionSubType_>>();
                    Dictionary<Inspection.InspectionType, List<long?>> m_dictCommonSubTypes = new Dictionary<Inspection.InspectionType, List<long?>>();
                    Dictionary<Inspection.InspectionType, double> m_InspectionTypeFillability = new Dictionary<Inspection.InspectionType, double>();
                    double fCommonFillability = 0;

                    m_listEquipments.Add(new ReportInfo.Equipment(id, form.m_EquipmentKindID));

                    if (!ReportInfo.GetData(-1, m_listEquipments, m_dictCommonSubTypes, m_list_sub_types, 0))
                        return;

                    fCommonFillability = ReportInfo.GetFillability(form.m_EquipmentKindID, m_listEquipments, m_dictCommonSubTypes, 0, m_InspectionTypeFillability);

                    if (Math.Abs(1.0 - fCommonFillability) > 0.0009)
                    {
                        //PrintFillabilityMessageForm f = new PrintFillabilityMessageForm();
                        //f.m_fProcent = fCommonFillability;

                        fCommonFillability = 1.0 - fCommonFillability;
                        //if (fCommonFillability < 0.01) fCommonFillability = 0.01;
                        DialogResult res = MyLocalizer.XtraMessageBoxShow("Не заполнено " + fCommonFillability.ToString("0.#%") + " данных", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, "Заполнить данные", "Сформировать протокол");

                        if (/*f.ShowDialog() == System.Windows.Forms.DialogResult.Cancel*/res == System.Windows.Forms.DialogResult.No)
                        {
                            PrintProtocol(id, form.m_EquipmentKindID);
                        }
                        else
                        {
                            // ищем 
                            foreach (Inspection.InspectionType type in Enum.GetValues(typeof(Inspection.InspectionType)))
                            {
                                if (Math.Abs(1.0 - m_InspectionTypeFillability[type]) > 0.0009)
                                {
                                    switch (type)
                                    {
                                        case Inspection.InspectionType.Vibro:
                                            ShowVibroForm(id, false);
                                            break;
                                        case Inspection.InspectionType.FHA:
                                            ShowFHAForm(id, false);
                                            break;
                                        case Inspection.InspectionType.HARG:
                                            ShowHARGForm(id, false);
                                            break;
                                        case Inspection.InspectionType.Visual:
                                            ShowVisualForm(id, false);
                                            break;
                                        case Inspection.InspectionType.Warm:
                                            ShowWarmForm(id, false);
                                            break;
                                    }
                                    return;
                                }
                            }
                        }
                    }
                    else
                        PrintProtocol(id, form.m_EquipmentKindID);
                }
                else
                {
                    if (form.m_bContinuePrev) ShowWarmForm(id, false);
                }
            }
        }

        private void btnVisual_Click(object sender, EventArgs e)
        {
            /*if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);
            ShowVisualForm(id, true);
             * */

            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Visual);
            form.ShowDialog(this);
        }

        private void btnFHA_Click(object sender, EventArgs e)
        {
            /*if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);
            ShowFHAForm(id, true);*/

            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.FHA);
            form.ShowDialog(this);
        }

        private void btnHARG_Click(object sender, EventArgs e)
        {
            /*if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);
            ShowHARGForm(id, true);*/

            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.HARG);
            form.ShowDialog(this);
        }

        private void btnWarm_Click(object sender, EventArgs e)
        {
            /*if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);
            ShowWarmForm(id, true);*/

            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Warm);
            form.ShowDialog(this);
        }

        private void btnVibro_Click(object sender, EventArgs e)
        {
            /*if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);
            ShowVibroForm(id, true);
             * */

            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Vibro);
            form.ShowDialog(this);
        }

        private void tFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                bAcceptFind_Click(sender, e);
            }
        }

        private void SubjectSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SubjectForm f = new SubjectForm();
            f.ShowDialog(this);
            m_strFilterString = "###";
            FindEquipments(-1);
        }

        private void BranchSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BranchForm f = new BranchForm();
            f.ShowDialog(this);
            m_strFilterString = "###";
            FindEquipments(-1);
        }

        private void SubstationSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SubstationForm f = new SubstationForm();
            f.ShowDialog(this);
            m_strFilterString = "###";
            FindEquipments(-1);
        }

        private void ManufactureSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ManufacturerForm f = new ManufacturerForm();
            f.ShowDialog(this);
        }

        private void EquipmentTypeSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EquipmentTypeForm f = new EquipmentTypeForm();
            f.ShowDialog(this);
        }

        private void EquipmentSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EquipmentForm f = new EquipmentForm();
            f.ShowDialog(this);
            //m_strFilterString = "###";
            //FindEquipments(-1);
        }

        private void VisualSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Visual);
            form.ShowDialog(this);
        }

        private void FHASubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.FHA);
            form.ShowDialog(this);
        }

        private void HARGSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.HARG);
            form.ShowDialog(this);
        }

        private void WarmSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Warm);
            form.ShowDialog(this);
        }

        private void VibroSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Vibro);
            form.ShowDialog(this);
        }

        private void MainGridView_DoubleClick(object sender, EventArgs e)
        {
            UpdateRecord();
        }

        /*private void btnAddEquipment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            long id = -1;

            PassportDataForm rf = new PassportDataForm(id);
            DialogResult dr = rf.ShowDialog(this);
            id = rf.m_id;
            if (dr == System.Windows.Forms.DialogResult.OK)
                RefreshGridPos(id);
        }*/

        private void mFilterForSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*DevExpress.XtraGrid.Columns.ColumnFilterInfo filter = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(MainGridView.Columns[m_FilterColumnIndex].FieldName + " = '" + m_FilterCellValue + "'");
            MainGridView.ActiveFilter.Add(MainGridView.Columns[m_FilterColumnIndex], filter);

            for (int i = 0; i <= m_FilterColumnIndex; i++)
            {
                if (!MainGridView.GroupedColumns.Contains(MainGridView.Columns[i]))
                {
                    MainGridView.Columns[i].Group();
                }
            }

            MainGridView.OptionsView.ShowGroupPanel = true;
            MainGridView.ExpandAllGroups();*/
        }

        private void PassportDataPopupMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateRecord();
        }

        private void VisualPopupMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Visual);
            form.ShowDialog(this);
        }

        private void FHAPopupMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.FHA);
            form.ShowDialog(this);
        }

        private void HARGPopupMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.HARG);
            form.ShowDialog(this);
        }

        private void WarmPopupMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Warm);
            form.ShowDialog(this);
        }

        private void VibroPopupMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);

            InspectionForm form = new InspectionForm(id, Inspection.InspectionType.Vibro);
            form.ShowDialog(this);
        }

        private void btnPassportAdd_Click(object sender, EventArgs e)
        {
            DialogResult dr = System.Windows.Forms.DialogResult.OK;
            if (!m_bAddPassportMessage)
            {
                AddPassportMessageForm f = new AddPassportMessageForm();
                f.m_bAddPassportMessage = m_bAddPassportMessage;
                f.m_strLicenseCode = m_strLicenseCode;
                dr = f.ShowDialog(this);
                m_bAddPassportMessage = f.m_bAddPassportMessage;
            }

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                long id = -1;

                PassportDataForm rf = new PassportDataForm(id);
                rf.m_bShowContinueMsg = true;
                dr = rf.ShowDialog(this);
                id = rf.m_id;
                if (dr == System.Windows.Forms.DialogResult.OK)
                    RefreshGridPos(id);

                if (rf.m_bContinueNext)
                {
                    ShowVisualForm(id, false);
                }
            }
            else
            {
                tFind.Focus();
            }
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void tFind_Enter(object sender, EventArgs e)
        {
        }

        private void PrintProtocol(long id, long EquipmentKindID)
        {
            WaitingForm wf = new WaitingForm(Inspection.ReportType.ProtocolTransformer);
            ReportInfo.Equipment eq = new ReportInfo.Equipment(id, EquipmentKindID);
            wf.m_listEquipments.Add(eq);
            wf.ShowDialog(this);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать оборудование.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainEquipmentsBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["EquipmentID"]);
            long kind_id = Convert.ToInt64(drv.Row["EquipmentKindID"]);

            PrintProtocol(id, kind_id);
        }

        private void tFind_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Report1SubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void RPNTypeSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RPNTypeForm f = new RPNTypeForm();
            f.ShowDialog(this);
        }

        private void InputTypeSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InputVoltageTypeForm f = new InputVoltageTypeForm();
            f.ShowDialog(this);
        }

        private void ManufacturerInputSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ManufacturerInputForm f = new ManufacturerInputForm();
            f.ShowDialog(this);
        }

        private void MainGridView_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {

        }
    }
}

