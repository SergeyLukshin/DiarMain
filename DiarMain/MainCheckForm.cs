using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SQLite;
using DevExpress.XtraEditors.Controls;
using DevExpress.Skins;
using DevExpress.XtraBars;

namespace DiarMain
{
    public partial class MainCheckForm : DevExpress.XtraEditors.XtraForm
    {
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

        [SQLiteFunction(Arguments = 3, FuncType = FunctionType.Scalar, Name = "CMP_DATETIME_MONTH")]
        class CMP_DATETIME_MONTH : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
               // IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime d1 = DateTime.Parse(args[0] as string);
                DateTime d2 = DateTime.Parse(args[1] as string);
                long month = Int64.Parse((args[2] as string));

                if (d1.Month <= d2.Month && d1.Month <= month && d2.Month >= month) return 0;
                if (d1.Month > d2.Month && (d1.Month <= month || d2.Month >= month)) return 0;
                return 1;

                //return s1 == s2 ? 0 : 1;
            }
        }

        [SQLiteFunction(Arguments = 3, FuncType = FunctionType.Scalar, Name = "CMP_DATETIME_YEAR")]
        class CMP_DATETIME_YEAR : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                DateTime d1 = DateTime.Parse(args[0] as string);
                DateTime d2 = DateTime.Parse(args[1] as string);
                long year = Int64.Parse((args[2] as string));

                if (d1.Year <= year && d2.Year >= year) return 0;
                return 1;

                //return s1 == s2 ? 0 : 1;
            }
        }

        BarButtonItem btnSubject;
        BarButtonItem btnBranch;
        BarButtonItem btnSubstation;
        BarButtonItem btnEquipmentManufacturer;
        BarButtonItem btnInputManufacturer;
        BarButtonItem btnEquipmentType;
        BarButtonItem btnRPNType;
        BarButtonItem btnInputType;
        BarButtonItem btnSwitchDriveType;
        BarButtonItem btnEquipment;
        BarButtonItem btnCheck;

        //BindingList<DataSourceString> listSubjects = new BindingList<DataSourceString>();
        //BindingList<DataSourceString> listBranches = new BindingList<DataSourceString>();
        //BindingList<DataSourceString> listSubstations = new BindingList<DataSourceString>();
        BindingList<DataSourceString> listMonth = new BindingList<DataSourceString>();
        //bool m_bDataLoadEnd = false;
        bool m_bShowCloseMsg;
        string m_strIDsString = "";
        bool m_bNeedFilter = true;

        public MainCheckForm()
        {
            InitializeComponent();
            defaultSkin.LookAndFeel.SetSkinStyle("Black");
            CreateMenu();
        }

        /*void bi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                foreach (DevExpress.XtraBars.BarCheckItemLink bi in StyleSubMenu.ItemLinks)
                {
                    ((DevExpress.XtraBars.BarCheckItem)bi.Item).Checked = false;
                    if (bi.Caption == e.Item.Caption) ((DevExpress.XtraBars.BarCheckItem)bi.Item).Checked = true;
                }
                defaultSkin.LookAndFeel.SetSkinStyle(e.Item.Caption);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/

        public void CreateMenu()
        {
            BarManager barManager = new BarManager();
            barManager.Form = this;
            // Prevent excessive updates while adding and customizing bars and bar items.
            // The BeginUpdate must match the EndUpdate method.
            barManager.BeginUpdate();
            // Create two bars and dock them to the top of the form.
            // Bar1 - is a main menu, which is stretched to match the form's width.
            // Bar2 - is a regular bar.
            Bar bar1 = new Bar(barManager, "Главное меню");
            bar1.DockStyle = BarDockStyle.Top;
            bar1.DockRow = 0;
            barManager.MainMenu = bar1;
            bar1.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            bar1.Appearance.Options.UseFont = true;
            bar1.OptionsBar.MultiLine = true;
            bar1.OptionsBar.UseWholeRow = true;
            bar1.OptionsBar.AllowQuickCustomization = false;
            bar1.OptionsBar.AllowCollapse = false;

            // Create bar items for the bar1 and bar2
            BarSubItem subDictMenu = new BarSubItem(barManager, "Справочники");
            BarSubItem subSettingMenu = new BarSubItem(barManager, "Настройки");
            BarSubItem subAboutMenu = new BarSubItem(barManager, "О программе");

            btnSubject = new BarButtonItem(barManager, "Субъекты");
            btnSubject.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            btnSubject.Appearance.Options.UseFont = true;

            btnBranch = new BarButtonItem(barManager, "Филиалы");
            btnBranch.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            btnBranch.Appearance.Options.UseFont = true;

            btnSubstation = new BarButtonItem(barManager, "Подстанции/станции");
            btnSubstation.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            btnSubstation.Appearance.Options.UseFont = true;

            btnEquipmentManufacturer = new BarButtonItem(barManager, "Заводы-изготовители оборудования");
            btnEquipmentManufacturer.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            btnEquipmentManufacturer.Appearance.Options.UseFont = true;
            //((BarButtonItemLink)btnEquipmentManufacturer).BeginGroup = true;

            btnInputManufacturer = new BarButtonItem(barManager, "Заводы-изготовители вводов");
            btnInputManufacturer.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            btnInputManufacturer.Appearance.Options.UseFont = true;

            btnEquipmentType = new BarButtonItem(barManager, "Типы оборудования");
            btnEquipmentType.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            btnEquipmentType.Appearance.Options.UseFont = true;

            btnRPNType = new BarButtonItem(barManager, "Типы РПН");
            btnRPNType.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            btnRPNType.Appearance.Options.UseFont = true;

            btnInputType = new BarButtonItem(barManager, "Типы вводов");
            btnInputType.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            btnInputType.Appearance.Options.UseFont = true;

            btnSwitchDriveType = new BarButtonItem(barManager, "Типы привода");
            btnSwitchDriveType.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            btnSwitchDriveType.Appearance.Options.UseFont = true;

            btnEquipment = new BarButtonItem(barManager, "Оборудование");
            btnEquipment.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            btnEquipment.Appearance.Options.UseFont = true;
            //((BarButtonItemLink)btnEquipment).BeginGroup = true;

            btnCheck = new BarButtonItem(barManager, "Проверки");
            btnCheck.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            btnCheck.Appearance.Options.UseFont = true;

            subDictMenu.AddItems(new BarItem[] { btnSubject, btnBranch, btnSubstation, btnEquipmentManufacturer, btnInputManufacturer, btnEquipmentType, btnRPNType, btnInputType, btnSwitchDriveType, btnEquipment, btnCheck });
            subDictMenu.ItemLinks[3].BeginGroup = true;
            subDictMenu.ItemLinks[9].BeginGroup = true;

            //Add the sub-menus to the bar1
            bar1.AddItems(new BarItem[] { subDictMenu, subSettingMenu, subAboutMenu });

            btnSubject.ItemClick += new ItemClickEventHandler(SubjectSubMenu_ItemClick);
            btnBranch.ItemClick += new ItemClickEventHandler(BranchSubMenu_ItemClick);
            btnSubstation.ItemClick += new ItemClickEventHandler(SubstationSubMenu_ItemClick);
            btnEquipmentManufacturer.ItemClick += new ItemClickEventHandler(ManufactureSubMenu_ItemClick);
            btnInputManufacturer.ItemClick += new ItemClickEventHandler(ManufacturerInputSubMenu_ItemClick);
            btnEquipmentType.ItemClick += new ItemClickEventHandler(EquipmentTypeSubMenu_ItemClick);
            btnRPNType.ItemClick += new ItemClickEventHandler(RPNTypeSubMenu_ItemClick);
            btnInputType.ItemClick += new ItemClickEventHandler(InputTypeSubMenu_ItemClick);
            btnSwitchDriveType.ItemClick += new ItemClickEventHandler(SwitchDriveTypeSubMenu_ItemClick);
            btnEquipment.ItemClick += new ItemClickEventHandler(EquipmentSubMenu_ItemClick);
            btnCheck.ItemClick += new ItemClickEventHandler(CheckSubMenu_ItemClick);
            subSettingMenu.ItemClick += new ItemClickEventHandler(Settings_ItemClick);
            subAboutMenu.ItemClick += new ItemClickEventHandler(AboutMenu_ItemClick);

            barManager.EndUpdate();
        }

        private void MainCheckForm_Load(object sender, EventArgs e)
        {
            try
            {
                Localizer.Active = new MyLocalizer();

                // загружаем главную картинку
                try
                {
                    SQLiteConnection m_con = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                    m_con.Open();
                    SQLiteCommand com = new SQLiteCommand(m_con);
                    com.CommandType = CommandType.Text;
                    com.CommandText = "SELECT MainImage FROM Settings";

                    SQLiteDataReader drImage = com.ExecuteReader();
                    object imageBytes = null;
                    if (drImage.HasRows)
                    {
                        while (drImage.Read())
                        {
                            imageBytes = drImage["MainImage"];
                        }
                    }
                    drImage.Close();
                    if (imageBytes != DBNull.Value)
                    {
                        Image image = (Bitmap)((new ImageConverter()).ConvertFrom(imageBytes));
                        MainGridControl.BackgroundImage = image;
                    }
                }
                catch (Exception ex)
                {
                    MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //CreateMenu();

                // выключаем ненужные пункты меню
                if (!Inspection.m_dictActualEquipmentKinds.ContainsKey(Equipment.EquipmentKind.Transformer))
                {
                    btnRPNType.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                if (!Inspection.m_dictActualEquipmentKinds.ContainsKey(Equipment.EquipmentKind.AirSwitch)
                    && !Inspection.m_dictActualEquipmentKinds.ContainsKey(Equipment.EquipmentKind.OilTankSwitch)
                    && !Inspection.m_dictActualEquipmentKinds.ContainsKey(Equipment.EquipmentKind.OilTankSwitch))
                {
                    btnSwitchDriveType.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                if (!Inspection.m_dictActualEquipmentKinds.ContainsKey(Equipment.EquipmentKind.Transformer)
                    && !Inspection.m_dictActualEquipmentKinds.ContainsKey(Equipment.EquipmentKind.OilTankSwitch)
                    && !Inspection.m_dictActualEquipmentKinds.ContainsKey(Equipment.EquipmentKind.OilTankSwitch))
                {
                    btnInputType.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    btnInputManufacturer.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                int index = 0;
                /*foreach (SkinContainer cnt in SkinManager.Default.Skins)
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
                }*/

                // TODO: This line of code loads data into the 'dataSetQuery.QSubjects' table. You can move, or remove it, as needed.
                m_bShowCloseMsg = true;
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

                    listMonth.Add(new DataSourceString(1, "январь"));
                    listMonth.Add(new DataSourceString(2, "февраль"));
                    listMonth.Add(new DataSourceString(3, "март"));
                    listMonth.Add(new DataSourceString(4, "апрель"));
                    listMonth.Add(new DataSourceString(5, "май"));
                    listMonth.Add(new DataSourceString(6, "июнь"));
                    listMonth.Add(new DataSourceString(7, "июль"));
                    listMonth.Add(new DataSourceString(8, "август"));
                    listMonth.Add(new DataSourceString(9, "сентябрь"));
                    listMonth.Add(new DataSourceString(10, "октябрь"));
                    listMonth.Add(new DataSourceString(11, "ноябрь"));
                    listMonth.Add(new DataSourceString(12, "декабрь"));

                    cbMonth.Properties.DataSource = listMonth;
                    cbMonth.Properties.DisplayMember = "VAL";
                    cbMonth.Properties.ValueMember = "KEY";
                    cbMonth.Properties.DropDownRows = 12;
                    cbMonth.EditValue = null;

                    m_bNeedFilter = false;
                    FindChecks(-1);
                }
                catch (SQLiteException ex)
                {
                    MyLocalizer.XtraMessageBoxShow("Не удалось подключиться к базе данных. Описание: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_bShowCloseMsg = false;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewView_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cbSubject_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) && ((LookUpEdit)sender).Text == "")
                {
                    ((LookUpEdit)sender).ClosePopup();
                    ((LookUpEdit)sender).EditValue = null;
                }
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void FindChecks(long id)
        {
            try
            {
                if (id <= 0)
                {
                    m_strIDsString = "";

                    this.qMainChecksTableAdapter.SetCommandText("SELECT c.CheckID, sb.SubjectName, b.BranchName, s.SubstationName, " +
                        "c.CheckDateBegin, c.CheckDateEnd, c.SubstationID, " +
                        "(SELECT COUNT(*) FROM Equipments AS e WHERE e.SubstationID = c.SubstationID) AS CntEquipments " +
                        "FROM Checks AS c " +
                        "INNER JOIN Substations AS s ON s.SubstationID = c.SubstationID " +
                        "INNER JOIN Branches AS b ON b.BranchID = s.BranchID " +
                        "INNER JOIN Subjects AS sb ON sb.SubjectID = b.SubjectID " +
                        "WHERE  " +
                        "(@subject = '' OR EQUAL_STR(sb.SubjectName, @subject) = 0) " +
                        "AND (@branch = '' OR EQUAL_STR(b.BranchName, @branch) = 0) " +
                        "AND (@substation = '' OR EQUAL_STR(s.SubstationName, @substation) = 0) " +
                        "AND (@year = '' OR CMP_DATETIME_YEAR(c.CheckDateBegin, c.CheckDateEnd, @year) = 0) " +
                        "AND (@month = '' OR CMP_DATETIME_MONTH(c.CheckDateBegin, c.CheckDateEnd, @month) = 0)");
                }
                else
                {
                    if (m_strIDsString == "") m_strIDsString = id.ToString();
                    else m_strIDsString = m_strIDsString + "," + id.ToString();

                    this.qMainChecksTableAdapter.SetCommandText("SELECT c.CheckID, sb.SubjectName, b.BranchName, s.SubstationName, " +
                        "c.CheckDateBegin, c.CheckDateEnd, c.SubstationID, " +
                        "(SELECT COUNT(*) FROM Equipments AS e WHERE e.SubstationID = c.SubstationID) AS CntEquipments " +
                        "FROM Checks AS c " +
                        "INNER JOIN Substations AS s ON s.SubstationID = c.SubstationID " +
                        "INNER JOIN Branches AS b ON b.BranchID = s.BranchID " +
                        "INNER JOIN Subjects AS sb ON sb.SubjectID = b.SubjectID " +
                        "WHERE c.CheckID IN (" + m_strIDsString + ") " +
                        "OR ((@subject = '' OR EQUAL_STR(sb.SubjectName, @subject) = 0) " +
                        "AND (@branch = '' OR EQUAL_STR(b.BranchName, @branch) = 0) " +
                        "AND (@substation = '' OR EQUAL_STR(s.SubstationName, @substation) = 0) " +
                        "AND (@year = '' OR CMP_DATETIME_YEAR(c.CheckDateBegin, c.CheckDateEnd, @year) = 0) " +
                        "AND (@month = '' OR CMP_DATETIME_MONTH(c.CheckDateBegin, c.CheckDateEnd, @month) = 0))");
                }

                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                if (m_bNeedFilter)
                {
                    string strSubject = cbSubject.Text;
                    string strBranch = cbBranch.Text;
                    string strSubstation = cbSubstation.Text;
                    string strYear = teYear.Text;
                    string strMonth = "";
                    if (cbMonth.EditValue != null) strMonth = cbMonth.EditValue.ToString();
                    if (strYear == "") strYear = "";

                    this.qMainChecksTableAdapter.Fill(this.dataSetQuery.QMainChecks, strSubject, strBranch, strSubstation, strYear, strMonth);
                }
                else
                {
                    this.qMainChecksTableAdapter.Fill(this.dataSetQuery.QMainChecks, "", "", "", "0", "0");
                }
                this.Cursor = System.Windows.Forms.Cursors.Default;

                RefreshButtons();
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshButtons()
        {
            try
            {
                if (this.dataSetQuery.QMainChecks.Count == 0)
                {
                    btnEdit.Enabled = false;
                    //btnReport.Enabled = false;
                }
                else
                {
                    btnEdit.Enabled = true;
                    //btnReport.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbSubject_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
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
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbBranch_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
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
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbSubstation_EditValueChanged(object sender, EventArgs e)
        {
            //RefreshEquipments();
        }

        private void BranchSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                BranchForm f = new BranchForm();
                f.ShowDialog(this);
                m_bNeedFilter = false;
                FindChecks(-1);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SubjectSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SubjectForm f = new SubjectForm();
                f.ShowDialog(this);
                m_bNeedFilter = false;
                FindChecks(-1);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SubstationSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SubstationForm f = new SubstationForm();
                f.ShowDialog(this);
                m_bNeedFilter = false;
                FindChecks(-1);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManufactureSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ManufacturerForm f = new ManufacturerForm();
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManufacturerInputSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ManufacturerInputForm f = new ManufacturerInputForm();
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EquipmentTypeSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                EquipmentTypeForm f = new EquipmentTypeForm();
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RPNTypeSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                RPNTypeForm f = new RPNTypeForm();
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InputTypeSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                InputVoltageTypeForm f = new InputVoltageTypeForm();
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EquipmentSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                EquipmentForm f = new EquipmentForm();
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bCancelFind_Click(object sender, EventArgs e)
        {
            try
            {
                m_bNeedFilter = false;
                FindChecks(-1);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainCheckForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_bShowCloseMsg && MyLocalizer.XtraMessageBoxShow("Вы действительно хотите выйти из программы?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshGridPos(long id)
        {
            try
            {
                int f_row = MainGridView.FocusedRowHandle;
                if (id <= 0)
                {
                    return;
                }
                else
                {
                    FindChecks(id);

                    for (int i = 0; i < MainGridView.RowCount/*this.dataSetQuery.QEquipments.Rows.Count*/; i++)
                    {
                        //DataRow r = this.dataSetQuery.QEquipments.Rows[i];
                        //int id_ = Convert.ToInt64(r["EquipmentID"]);
                        long id_ = Convert.ToInt64(MainGridView.GetRowCellValue(i, "CheckID"));
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
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                m_bNeedFilter = true;
                FindChecks(-1);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                long id = -1;
                long subjectID = -1;
                long branchID = -1;
                long substationID = -1;
                if (cbSubject.EditValue != null) subjectID = Convert.ToInt64(cbSubject.EditValue);
                if (cbBranch.EditValue != null) branchID = Convert.ToInt64(cbBranch.EditValue);
                if (cbSubstation.EditValue != null) substationID = Convert.ToInt64(cbSubstation.EditValue);

                this.ShowInTaskbar = false;
                CheckDataForm cf = new CheckDataForm(id, subjectID, branchID, substationID);
                DialogResult dr = cf.ShowDialog(this);
                this.ShowInTaskbar = true;
                id = cf.m_CheckID;
                //if (dr == System.Windows.Forms.DialogResult.OK)
                RefreshGridPos(id);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRecord()
        {
            try
            {
                if (MainGridView.FocusedRowHandle < 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать проверку.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView drv = (DataRowView)(qMainChecksBindingSource.Current);
                long id = Convert.ToInt64(drv.Row["CheckID"]);

                this.ShowInTaskbar = false;
                CheckDataForm cf = new CheckDataForm(id, -1, -1, -1);
                DialogResult dr = cf.ShowDialog(this);
                this.ShowInTaskbar = true;
                //if (dr == System.Windows.Forms.DialogResult.OK)
                RefreshGridPos(id);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateRecord();
        }

        /*private void btnReport_Click(object sender, EventArgs e)
        {
            if (MainGridView.FocusedRowHandle < 0)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать проверку.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)(qMainChecksBindingSource.Current);
            long id = Convert.ToInt64(drv.Row["CheckID"]);
            long substationID = Convert.ToInt64(drv.Row["SubstationID"]);

            WaitingForm wf = new WaitingForm(Inspection.ReportType.ReportTransformer);

            try
            {
                SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                connection.Open();
                SQLiteCommand com = new SQLiteCommand(connection);

                // проверка на пересечение дат в этом расположении
                com.CommandType = CommandType.Text;
                com.CommandText = "SELECT e.EquipmentID, e.EquipmentKindID FROM Equipments AS e WHERE SubstationID = @sid";

                com.Parameters.Clear();
                SQLiteParameter param = new SQLiteParameter("@sid", DbType.Int64);
                param.Value = substationID;
                com.Parameters.Add(param);

                SQLiteDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        long eq_id = Convert.ToInt64(dr["EquipmentID"]);
                        long eq_kind_id = Convert.ToInt64(dr["EquipmentKindID"]);

                        if ((Equipment.EquipmentKind)eq_kind_id == Equipment.EquipmentKind.Transformer)
                        {
                            ReportInfo.Equipment eq = new ReportInfo.Equipment(eq_id, eq_kind_id);
                            wf.m_listEquipments.Add(eq);
                        }
                    }
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

            if (wf.m_listEquipments.Count > 12)
            {
                MyLocalizer.XtraMessageBoxShow("Кол-во трансформаторов в проверке для вывода отчета не должно превышать 12.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            wf.m_CheckID = id;
            wf.ShowDialog(this);
        }*/

        private void MainGridView_DoubleClick(object sender, EventArgs e)
        {
            UpdateRecord();
        }

        private void CheckSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                CheckForm f = new CheckForm();
                f.ShowDialog(this);

                m_bNeedFilter = false;
                FindChecks(-1);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainGridView_ShowFilterPopupListBox(object sender, DevExpress.XtraGrid.Views.Grid.FilterPopupListBoxEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AboutMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                AboutForm f = new AboutForm();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SwitchDriveTypeSubMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SwitchDriveTypeForm f = new SwitchDriveTypeForm();
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainCheckForm_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Settings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                PswForm f = new PswForm();
                if (f.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    SettingsForm fSettings = new SettingsForm();
                    if (fSettings.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        MyLocalizer.XtraMessageBoxShow("Необходимо перезапустить программу", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}