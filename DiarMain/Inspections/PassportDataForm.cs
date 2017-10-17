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
    public partial class PassportDataForm : DevExpress.XtraEditors.XtraForm
    {
        public long m_id;
        bool m_bDataLoad = true;
        long m_ReadOnly = 0;
        bool m_bDataLoadEnd = false;
        bool m_bChangeData = false;
        public bool m_bContinueNext = false;
        public bool m_bContinuePrev = false;
        public bool m_bShowContinueMsg = false;

        public long m_SubjectID = -1;
        public long m_BranchID = -1;
        public long m_SubstationID = -1;
        public long m_OldEquipmentKindID = -1;

        BindingList<DataSourceString> listConstructionType = new BindingList<DataSourceString>();
        BindingList<DataSourceString> listInputVoltageType = new BindingList<DataSourceString>();
        BindingList<DataSourceInt> listInputVoltageClass = new BindingList<DataSourceInt>();
        BindingList<DataSourceInt> listInputVoltageHighClass = new BindingList<DataSourceInt>();
        BindingList<DataSourceString> listRPNCnt = new BindingList<DataSourceString>();
        BindingList<DataSourceString> listProtectionOilType = new BindingList<DataSourceString>();
        BindingList<DataSourceString> listCoolingSystemType = new BindingList<DataSourceString>();
        BindingList<DataSourceString> listRPNKind = new BindingList<DataSourceString>();

        Dictionary<string, List<string>> m_dictInputs = new Dictionary<string, List<string>>();
        Dictionary<string, int> m_dictInputsControlsName = new Dictionary<string, int>();
        Dictionary<string, object> m_dictInputsOldValues = new Dictionary<string, object>();
        Dictionary<long, long> m_dictClassIDToKindID = new Dictionary<long, long>();

        public PassportDataForm(long id, long subjectID = -1, long branchID = -1, long substationID = -1)
        {
            InitializeComponent();
            m_id = id;

            m_SubjectID = subjectID;
            m_BranchID = branchID;
            m_SubstationID = substationID;
        }

        private void EquipmentRecordForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSetQuery.QEquipmentClasses' table. You can move, or remove it, as needed.
            /*
            Трансформаторы:
                Силовой трансформатор
                Автотрансформатор
                Реактор
            Выключатели масляные:
                Выключатель масляный баковый
                Выключатель маломасляный
            Выключатели воздушные
                Выключатель воздушный (должен быть по умолчанию)
            Гидрогенераторы
                Гидрогенератор (должен быть по умолчанию)
            Турбогенераторы
                Турбогенератор (должен быть по умолчанию)
             */ 


            this.WindowState = FormWindowState.Maximized;

            string strSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (strSeparator == ".") strSeparator = "\\.";
            this.repPower.Mask.EditMask = "(\\d+|\\d+" + strSeparator + "\\d)";

            listConstructionType.Add(new DataSourceString(1, "однофазный"));
            listConstructionType.Add(new DataSourceString(3, "трехфазный"));
            repConstructionType.DataSource = listConstructionType;
            repConstructionType.DisplayMember = "VAL";
            repConstructionType.ValueMember = "KEY";
            repConstructionType.DropDownRows = listConstructionType.Count;

            listInputVoltageType.Add(new DataSourceString(1, "масляный герметичный"));
            listInputVoltageType.Add(new DataSourceString(2, "масляный негерметичный"));
            listInputVoltageType.Add(new DataSourceString(3, "твердая изоляция"));
            repInputKind.DataSource = listInputVoltageType;
            repInputKind.DisplayMember = "VAL";
            repInputKind.ValueMember = "KEY";
            repInputKind.DropDownRows = listInputVoltageType.Count;

            listProtectionOilType.Add(new DataSourceString(1, "пленочная защита"));
            listProtectionOilType.Add(new DataSourceString(2, "азотная защита"));
            listProtectionOilType.Add(new DataSourceString(3, "свободное дыхание (воздухоосушитель)"));
            repProtectionOilType.DataSource = listProtectionOilType;
            repProtectionOilType.DisplayMember = "VAL";
            repProtectionOilType.ValueMember = "KEY";
            repProtectionOilType.DropDownRows = listProtectionOilType.Count;

            listCoolingSystemType.Add(new DataSourceString(1, "Д (Принудительная циркуляция воздуха и естественная циркуляция масла)"));
            listCoolingSystemType.Add(new DataSourceString(2, "ДЦ (Принудительная циркуляция воздуха и масла с ненаправленным потоком масла)"));
            listCoolingSystemType.Add(new DataSourceString(3, "М (Естественная циркуляция воздуха и масла)"));
            listCoolingSystemType.Add(new DataSourceString(4, "МЦ (Естественная циркуляция воздуха и принудительная циркуляция масла с ненаправленным потоком масла)"));
            listCoolingSystemType.Add(new DataSourceString(5, "НМЦ (Естественная циркуляция воздуха и принудительная циркуляция масла с направленным потоком масла)"));
            listCoolingSystemType.Add(new DataSourceString(6, "НДЦ (Принудительная циркуляция воздуха и масла с направленным потоком масла)"));
            listCoolingSystemType.Add(new DataSourceString(7, "Ц (Принудительная циркуляция воды и масла с ненаправленным потоком масла)"));
            listCoolingSystemType.Add(new DataSourceString(8, "НЦ (Принудительная циркуляция воды и масла с направленным потоком масла)"));
            listCoolingSystemType.Add(new DataSourceString(9, "ONAF (Принудительная циркуляция воздуха и естественная циркуляция масла)"));
            listCoolingSystemType.Add(new DataSourceString(10, "OFAF (Принудительная циркуляция воздуха и масла с ненаправленным потоком масла)"));
            listCoolingSystemType.Add(new DataSourceString(11, "ONAN (Естественная циркуляция воздуха и масла)"));
            listCoolingSystemType.Add(new DataSourceString(12, "OFAN (Естественная циркуляция воздуха и принудительная циркуляция масла с ненаправленным потоком масла)"));
            listCoolingSystemType.Add(new DataSourceString(13, "ODAN (Естественная циркуляция воздуха и принудительная циркуляция масла с направленным потоком масла)"));
            listCoolingSystemType.Add(new DataSourceString(14, "ODAF (Принудительная циркуляция воздуха и масла с направленным потоком масла)"));
            listCoolingSystemType.Add(new DataSourceString(15, "OFWF (Принудительная циркуляция воды и масла с ненаправленным потоком масла)"));
            listCoolingSystemType.Add(new DataSourceString(16, "ODWF (Принудительная циркуляция воды и масла с направленным потоком масла)"));
            repCoolingSystemType.DataSource = listCoolingSystemType;
            repCoolingSystemType.DisplayMember = "VAL";
            repCoolingSystemType.ValueMember = "KEY";
            repCoolingSystemType.DropDownRows = listCoolingSystemType.Count;

            listInputVoltageClass.Add(new DataSourceInt(1, 1));
            listInputVoltageClass.Add(new DataSourceInt(3, 3));
            listInputVoltageClass.Add(new DataSourceInt(6, 6));
            listInputVoltageClass.Add(new DataSourceInt(10, 10));
            listInputVoltageClass.Add(new DataSourceInt(15, 15));
            listInputVoltageClass.Add(new DataSourceInt(20, 20));
            listInputVoltageClass.Add(new DataSourceInt(24, 24));
            listInputVoltageClass.Add(new DataSourceInt(27, 27));
            listInputVoltageClass.Add(new DataSourceInt(35, 35));
            listInputVoltageClass.Add(new DataSourceInt(110, 110));
            listInputVoltageClass.Add(new DataSourceInt(150, 150));
            listInputVoltageClass.Add(new DataSourceInt(220, 220));
            listInputVoltageClass.Add(new DataSourceInt(330, 330));
            listInputVoltageClass.Add(new DataSourceInt(500, 500));
            listInputVoltageClass.Add(new DataSourceInt(750, 750));

            repNominalVoltageLow.DataSource = listInputVoltageClass;
            repNominalVoltageLow.DisplayMember = "VAL";
            repNominalVoltageLow.ValueMember = "KEY";
            repNominalVoltageLow.DropDownRows = listInputVoltageClass.Count;

            repNominalVoltageMid.DataSource = listInputVoltageClass;
            repNominalVoltageMid.DisplayMember = "VAL";
            repNominalVoltageMid.ValueMember = "KEY";
            repNominalVoltageMid.DropDownRows = listInputVoltageClass.Count;

            listInputVoltageHighClass.Add(new DataSourceInt(110, 110));
            listInputVoltageHighClass.Add(new DataSourceInt(150, 150));
            listInputVoltageHighClass.Add(new DataSourceInt(220, 220));
            listInputVoltageHighClass.Add(new DataSourceInt(330, 330));
            listInputVoltageHighClass.Add(new DataSourceInt(500, 500));
            listInputVoltageHighClass.Add(new DataSourceInt(750, 750));

            repNominalVoltageHigh.DataSource = listInputVoltageHighClass;
            repNominalVoltageHigh.DisplayMember = "VAL";
            repNominalVoltageHigh.ValueMember = "KEY";
            repNominalVoltageHigh.DropDownRows = listInputVoltageHighClass.Count;


            listRPNCnt.Add(new DataSourceString(0, "нет"));
            listRPNCnt.Add(new DataSourceString(1, "1"));
            listRPNCnt.Add(new DataSourceString(3, "3"));
            repRPNCnt.DataSource = listRPNCnt;
            repRPNCnt.DisplayMember = "VAL";
            repRPNCnt.ValueMember = "KEY";
            repRPNCnt.DropDownRows = listRPNCnt.Count;

            repRPNVoltage.DataSource = listInputVoltageClass;
            repRPNVoltage.DisplayMember = "VAL";
            repRPNVoltage.ValueMember = "KEY";
            repRPNVoltage.DropDownRows = listInputVoltageClass.Count;

            listRPNKind.Add(new DataSourceString(0, "погружной"));
            listRPNKind.Add(new DataSourceString(1, "в навесном баке"));

            repRPNKind.DataSource = listRPNKind;
            repRPNKind.DisplayMember = "VAL";
            repRPNKind.ValueMember = "KEY";
            repRPNKind.DropDownRows = listRPNKind.Count;

            m_dictInputs["HighA"] = new List<string>();
            m_dictInputs["HighA"].Add("InputTypeID");
            m_dictInputs["HighA"].Add("InputKind");
            m_dictInputs["HighA"].Add("InputManufacturerID");
            m_dictInputs["HighA"].Add("InputNumber");
            m_dictInputs["HighA"].Add("InputCreateYear");
            m_dictInputs["HighA"].Add("InputUseBeginYear");
            m_dictInputs["HighA"].Add("InputName");

            m_dictInputs["HighB"] = new List<string>();
            m_dictInputs["HighB"].Add("InputTypeID");
            m_dictInputs["HighB"].Add("InputKind");
            m_dictInputs["HighB"].Add("InputManufacturerID");
            m_dictInputs["HighB"].Add("InputNumber");
            m_dictInputs["HighB"].Add("InputCreateYear");
            m_dictInputs["HighB"].Add("InputUseBeginYear");
            m_dictInputs["HighB"].Add("InputName");

            m_dictInputs["HighC"] = new List<string>();
            m_dictInputs["HighC"].Add("InputTypeID");
            m_dictInputs["HighC"].Add("InputKind");
            m_dictInputs["HighC"].Add("InputManufacturerID");
            m_dictInputs["HighC"].Add("InputNumber");
            m_dictInputs["HighC"].Add("InputCreateYear");
            m_dictInputs["HighC"].Add("InputUseBeginYear");
            m_dictInputs["HighC"].Add("InputName");

            m_dictInputs["MiddleA"] = new List<string>();
            m_dictInputs["MiddleA"].Add("InputTypeID");
            m_dictInputs["MiddleA"].Add("InputKind");
            m_dictInputs["MiddleA"].Add("InputManufacturerID");
            m_dictInputs["MiddleA"].Add("InputNumber");
            m_dictInputs["MiddleA"].Add("InputCreateYear");
            m_dictInputs["MiddleA"].Add("InputUseBeginYear");
            m_dictInputs["MiddleA"].Add("InputName");

            m_dictInputs["MiddleB"] = new List<string>();
            m_dictInputs["MiddleB"].Add("InputTypeID");
            m_dictInputs["MiddleB"].Add("InputKind");
            m_dictInputs["MiddleB"].Add("InputManufacturerID");
            m_dictInputs["MiddleB"].Add("InputNumber");
            m_dictInputs["MiddleB"].Add("InputCreateYear");
            m_dictInputs["MiddleB"].Add("InputUseBeginYear");
            m_dictInputs["MiddleB"].Add("InputName");

            m_dictInputs["MiddleC"] = new List<string>();
            m_dictInputs["MiddleC"].Add("InputTypeID");
            m_dictInputs["MiddleC"].Add("InputKind");
            m_dictInputs["MiddleC"].Add("InputManufacturerID");
            m_dictInputs["MiddleC"].Add("InputNumber");
            m_dictInputs["MiddleC"].Add("InputCreateYear");
            m_dictInputs["MiddleC"].Add("InputUseBeginYear");
            m_dictInputs["MiddleC"].Add("InputName");

            m_dictInputs["Neutral"] = new List<string>();
            m_dictInputs["Neutral"].Add("InputTypeID");
            m_dictInputs["Neutral"].Add("InputKind");
            m_dictInputs["Neutral"].Add("InputManufacturerID");
            m_dictInputs["Neutral"].Add("InputNumber");
            m_dictInputs["Neutral"].Add("InputCreateYear");
            m_dictInputs["Neutral"].Add("InputUseBeginYear");
            m_dictInputs["Neutral"].Add("InputName");

            foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
            {
                for (int i = 0; i < p.Value.Count; i++)
                {
                    m_dictInputsControlsName["r" + p.Value[i] + p.Key] = 1;
                }
            }

            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSetQuery.QNominalVoltages". При необходимости она может быть перемещена или удалена.
            //this.qNominalVoltagesTableAdapter.Fill(this.dataSetQuery.QNominalVoltages);
            //this.manufacturersTableAdapter.Fill(this.dataSetMain.Manufacturers);


            //this.qSubstationsBranchesTableAdapter.Fill(this.dataSetQuery.QSubstationsBranches);
            this.equipmentKindsTableAdapter.Fill(this.dataSetMain.EquipmentKinds);
            this.qEquipmentRecordTableAdapter.Fill(this.dataSetQuery2.QEquipmentRecord, m_id);

            this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);

            long EquipmentKindID = -1;
            long SubjectID = -1;
            long BranchID = -1;


            DataRowView dgv = null;
            if (m_id > 0)
            {
                dgv = (DataRowView)(this.qEquipmentRecordBindingSource.Current);

                EquipmentKindID = Convert.ToInt64(dgv.Row["EquipmentKindID"]);
                long EquipmentClassID = Convert.ToInt64(dgv.Row["EquipmentClassID"]);
                m_ReadOnly = Convert.ToInt64(dgv.Row["ReadOnly"]);
                SubjectID = Convert.ToInt64(dgv.Row["SubjectID"]);
                BranchID = Convert.ToInt64(dgv.Row["BranchID"]);

                this.qEquipmentClassesTableAdapter.Fill(this.dataSetQuery.QEquipmentClasses, EquipmentClassID);

                this.Text = "Изменение паспортных данных оборудования";

                // если есть хотя бы одно обследование, которое принадлежит проверке, блокируем местоположение
                bool bCheckExists = false;

                try
                {
                    SQLiteConnection m_con = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                    m_con.Open();
                    SQLiteCommand com = new SQLiteCommand(m_con);
                    com.CommandType = CommandType.Text;
                    com.CommandText = "SELECT COUNT(*) AS Cnt FROM Inspections WHERE EquipmentID = @eqID AND NOT CheckID IS NULL";
                    SQLiteParameter param = new SQLiteParameter("@eqID", DbType.Int64);
                    param.Value = m_id;
                    com.Parameters.Add(param);

                    SQLiteDataReader drCheck = com.ExecuteReader();
                    if (drCheck.HasRows)
                    {
                        while (drCheck.Read())
                        {
                            if (drCheck["Cnt"] != DBNull.Value)
                            {
                                if (Convert.ToInt64(drCheck["Cnt"]) > 0)
                                {
                                    bCheckExists = true;
                                    break;
                                }
                            }
                        }
                    }
                    drCheck.Close();
                }
                catch (Exception ex)
                {
                    MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (bCheckExists)
                {
                    this.repSubject.ReadOnly = true;
                    this.repSubject.Buttons[1].Enabled = false;
                    GridVertical.GetRowByFieldName("SubjectID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                    GridVertical.GetRowByFieldName("SubjectID").Appearance.Options.UseBackColor = true;

                    this.repBranch.ReadOnly = true;
                    this.repBranch.Buttons[1].Enabled = false;
                    GridVertical.GetRowByFieldName("BranchID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                    GridVertical.GetRowByFieldName("BranchID").Appearance.Options.UseBackColor = true;

                    this.repSubstation.ReadOnly = true;
                    this.repSubstation.Buttons[1].Enabled = false;
                    GridVertical.GetRowByFieldName("SubstationID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                    GridVertical.GetRowByFieldName("SubstationID").Appearance.Options.UseBackColor = true;
                }
            }
            else
            {
                this.Text = "Добавление нового оборудования";

                this.qEquipmentClassesTableAdapter.Fill(this.dataSetQuery.QEquipmentClasses, 0);

                this.qEquipmentRecordBindingSource.AddNew();
                dgv = (DataRowView)(this.qEquipmentRecordBindingSource.Current);

                dgv.Row["ConstructionType"] = 1;
                dgv.Row["RPNCnt"] = 0;
                dgv.Row["ProtectionOilTypeID"] = 3;

                if (this.dataSetQuery.QEquipmentClasses.Count > 0)
                {
                    dgv.Row["EquipmentKindID"] = this.dataSetQuery.QEquipmentClasses.Rows[0]["EquipmentKindID"];
                    dgv.Row["EquipmentClassID"] = this.dataSetQuery.QEquipmentClasses.Rows[0]["EquipmentClassID"];
                    EquipmentKindID = Convert.ToInt64(dgv.Row["EquipmentKindID"]);
                    cbEquipmentClass.EditValue = Convert.ToInt64(dgv.Row["EquipmentClassID"]);
                }

                if (m_SubjectID > 0)
                {
                    dgv.Row["SubjectID"] = m_SubjectID;
                    SubjectID = m_SubjectID;

                    this.repSubject.ReadOnly = true;
                    this.repSubject.Buttons[1].Enabled = false;
                    GridVertical.GetRowByFieldName("SubjectID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                    GridVertical.GetRowByFieldName("SubjectID").Appearance.Options.UseBackColor = true;
                }

                if (m_BranchID > 0)
                {
                    dgv.Row["BranchID"] = m_BranchID;
                    BranchID = m_BranchID;

                    this.repBranch.ReadOnly = true;
                    this.repBranch.Buttons[1].Enabled = false;
                    GridVertical.GetRowByFieldName("BranchID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                    GridVertical.GetRowByFieldName("BranchID").Appearance.Options.UseBackColor = true;
                }

                if (m_SubstationID > 0)
                {
                    dgv.Row["SubstationID"] = m_SubstationID;

                    this.repSubstation.ReadOnly = true;
                    this.repSubstation.Buttons[1].Enabled = false;
                    GridVertical.GetRowByFieldName("SubstationID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                    GridVertical.GetRowByFieldName("SubstationID").Appearance.Options.UseBackColor = true;
                }

            }

            for (int i = 0; i < this.dataSetQuery.QEquipmentClasses.Rows.Count; i++)
            {
                m_dictClassIDToKindID[Convert.ToInt64(this.dataSetQuery.QEquipmentClasses.Rows[i]["EquipmentClassID"])] = Convert.ToInt64(this.dataSetQuery.QEquipmentClasses.Rows[i]["EquipmentKindID"]);
            }

            m_OldEquipmentKindID = EquipmentKindID;
            ShowHideParameters(EquipmentKindID);

            this.qEquipmentTypesByKindTableAdapter.Fill(this.dataSetQuery.QEquipmentTypesByKind, EquipmentKindID);
            this.qRPNTypesByKindTableAdapter.Fill(this.dataSetQuery.QRPNTypesByKind, EquipmentKindID);
            this.qInputVoltageTypesByKindTableAdapter.Fill(this.dataSetQuery.QInputVoltageTypesByKind, EquipmentKindID);
            this.qSwitchDriveTypesByKindTableAdapter.Fill(this.dataSetQuery.QSwitchDriveTypesByKind, EquipmentKindID);
            this.qManufacturersByKindTableAdapter.Fill(this.dataSetQuery.QManufacturersByKind, EquipmentKindID);
            this.qManufacturersInputsByKindTableAdapter.Fill(this.dataSetQuery.QManufacturersInputsByKind, EquipmentKindID);
            //this.qEquipmentClassesByKindTableAdapter.Fill(this.dataSetQuery.QEquipmentClassesByKind, EquipmentKindID);

            this.qBranchesBySubjectTableAdapter.Fill(this.dataSetQuery.QBranchesBySubject, SubjectID);
            this.qSubstationsByBranchTableAdapter.Fill(this.dataSetQuery.QSubstationsByBranch, BranchID);

            /*if (m_id <= 0 && this.dataSetQuery.QEquipmentClassesByKind.Count == 1)
            {
                dgv.Row["EquipmentClassID"] = this.dataSetQuery.QEquipmentClassesByKind.Rows[0]["EquipmentClassID"];
                long EquipmentClassID = Convert.ToInt64(dgv.Row["EquipmentClassID"]);
                cbEquipmentClass.EditValue = EquipmentClassID;
            }*/


            if (this.dataSetQuery.QSubstationsByBranch.Rows.Count < 7)
                this.repSubstation.DropDownRows = this.dataSetQuery.QSubstationsByBranch.Rows.Count;
            else
                this.repSubstation.DropDownRows = 7;

            if (this.dataSetQuery.QSubjects.Rows.Count < 7)
                this.repSubject.DropDownRows = this.dataSetQuery.QSubjects.Rows.Count;
            else
                this.repSubject.DropDownRows = 7;

            if (this.dataSetQuery.QBranchesBySubject.Rows.Count < 7)
                this.repBranch.DropDownRows = this.dataSetQuery.QBranchesBySubject.Rows.Count;
            else
                this.repBranch.DropDownRows = 7;

            /*if (this.dataSetMain.EquipmentKinds.Rows.Count < 7)
                cbEquipmentKind.Properties.DropDownRows = this.dataSetMain.EquipmentKinds.Rows.Count;
            else
                cbEquipmentKind.Properties.DropDownRows = 7;*/

            if (this.dataSetQuery.QEquipmentClasses.Rows.Count < 7)
                cbEquipmentClass.Properties.DropDownRows = this.dataSetQuery.QEquipmentClasses.Rows.Count;
            else
                cbEquipmentClass.Properties.DropDownRows = 7;

            if (this.dataSetMain.Manufacturers.Rows.Count < 7)
                this.repManufacturer.DropDownRows = this.dataSetMain.Manufacturers.Rows.Count;
            else
                this.repManufacturer.DropDownRows = 7;

            if (this.dataSetMain.ManufacturersInputs.Rows.Count < 7)
                this.repManufacturerInput.DropDownRows = this.dataSetMain.ManufacturersInputs.Rows.Count;
            else
                this.repManufacturerInput.DropDownRows = 7;

            if (this.dataSetQuery.QEquipmentTypesByKind.Rows.Count < 7)
                this.repEquipmentType.DropDownRows = this.dataSetQuery.QEquipmentTypesByKind.Rows.Count;
            else
                this.repEquipmentType.DropDownRows = 7;

            if (this.dataSetQuery.QRPNTypesByKind.Rows.Count < 7)
                this.repRPNType.DropDownRows = this.dataSetQuery.QRPNTypesByKind.Rows.Count;
            else
                this.repRPNType.DropDownRows = 7;

            if (this.dataSetQuery.QInputVoltageTypesByKind.Rows.Count < 7)
                this.repInputType.DropDownRows = this.dataSetQuery.QInputVoltageTypesByKind.Rows.Count;
            else
                this.repInputType.DropDownRows = 7;

            if (this.dataSetQuery.QSwitchDriveTypesByKind.Rows.Count < 7)
                this.repSwitchDriveType.DropDownRows = this.dataSetQuery.QSwitchDriveTypesByKind.Rows.Count;
            else
                this.repSwitchDriveType.DropDownRows = 7;

            if (this.dataSetQuery.QManufacturersByKind.Rows.Count < 7)
                this.repManufacturer.DropDownRows = this.dataSetQuery.QManufacturersByKind.Rows.Count;
            else
                this.repManufacturer.DropDownRows = 7;

            if (this.dataSetQuery.QManufacturersInputsByKind.Rows.Count < 7)
                this.repManufacturerInput.DropDownRows = this.dataSetQuery.QManufacturersInputsByKind.Rows.Count;
            else
                this.repManufacturerInput.DropDownRows = 7;

            foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
            {
                for (int i = 0; i < p.Value.Count; i++)
                {
                    m_dictInputsOldValues[p.Value[i] + p.Key] = dgv.Row[p.Value[i] + p.Key];
                }
            }

            if (m_ReadOnly != 0)
            {
                teEquipmentName.Properties.ReadOnly = true;
                teEquipmentNumber.Properties.ReadOnly = true;
                //cbEquipmentKind.Properties.ReadOnly = true;
                cbEquipmentClass.Properties.ReadOnly = true;

                GridVertical.GetRowByFieldName("SubjectID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("SubjectID").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("BranchID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("BranchID").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("SubstationID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("SubstationID").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("EquipmentTypeID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("EquipmentTypeID").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("ConstructionType").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("ConstructionType").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("CoolingSystemTypeID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("CoolingSystemTypeID").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("ManufacturerID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("ManufacturerID").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("NominalVoltageLow").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("NominalVoltageLow").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("NominalVoltageMiddle").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("NominalVoltageMiddle").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("NominalVoltageHigh").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("NominalVoltageHigh").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("NominalPower").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("NominalPower").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("NominalCurrent").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("NominalCurrent").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("SwitchDriveTypeID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("SwitchDriveTypeID").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("CreateYear").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("CreateYear").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("ProjectLifeTime").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("ProjectLifeTime").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("ProtectionOilTypeID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("ProtectionOilTypeID").Appearance.Options.UseBackColor = true;

                /*GridVertical.GetRowByFieldName("InputKindHighA").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("InputKindHighB").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("InputKindHighC").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("InputKindMiddleA").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("InputKindMiddleB").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("InputKindMiddleC").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("InputKindNeutral").Appearance.BackColor = Color.FromArgb(240, 240, 240);*/

                foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                {
                    for (int i = 0; i < p.Value.Count; i++)
                    {
                         GridVertical.GetRowByFieldName(p.Value[i] + p.Key).Appearance.BackColor = Color.FromArgb(240, 240, 240);
                         GridVertical.GetRowByFieldName(p.Value[i] + p.Key).Appearance.Options.UseBackColor = true;
                    }
                }

                GridVertical.GetRowByFieldName("RPNCnt").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("RPNCnt").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("RPNVoltage").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("RPNVoltage").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("RPNTypeID").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("RPNTypeID").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("RPNKind").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("RPNKind").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("RPNNumber").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("RPNNumber").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("RPNNumber2").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("RPNNumber2").Appearance.Options.UseBackColor = true;

                GridVertical.GetRowByFieldName("RPNNumber3").Appearance.BackColor = Color.FromArgb(240, 240, 240);
                GridVertical.GetRowByFieldName("RPNNumber3").Appearance.Options.UseBackColor = true;
            }

            m_bDataLoad = false;

            repNominalVoltageLow.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            repNominalVoltageMid.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            repNominalVoltageHigh.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            repRPNVoltage.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            repCoolingSystemType.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            repEquipmentType.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            //repManufacturer.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            //repConstructionType.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            //repInputVoltageType.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            //repProtectionOilType.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            //repRPNKind.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            //repRPNCnt.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            //repProtectionOilType.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            repSubstation.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            repBranch.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            repSubject.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            repRPNKind.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            repRPNType.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);
            repInputType.KeyDown += new KeyEventHandler(repNominalVoltageLow_KeyDown);

            /*UpdateInputVoltageCategory();
            UpdateRPNCnt();
            UpdateNominalVoltageNeutral();*/

            m_bDataLoadEnd = true;

            if (m_bShowContinueMsg && m_id > 0)
            {
                bNext.Visible = true;
            }
        }

        void repNominalVoltageLow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (!((LookUpEdit)sender).Properties.ReadOnly)
                {
                    DevExpress.XtraVerticalGrid.Rows.BaseRow row = GridVertical.FocusedRow;
                    GridVertical.BeginUpdate();
                    GridVertical.SetCellValue(row, 0, DBNull.Value);
                    GridVertical.EndUpdate();
                }
                //GridVertical.Refresh();
            }
        }

        private void UpdateNominalVoltageNeutral(bool bUpdateDB = true)
        {
            long EquipmentKindID = -1;
            if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
            {
                DataRowView drv_ = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                EquipmentKindID = Convert.ToInt64(drv_.Row["EquipmentKindID"]);
            }

            // только для трансформатора
            if ((Equipment.EquipmentKind)EquipmentKindID != Equipment.EquipmentKind.Transformer) return;

            DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);

            object val = drv.Row["NominalVoltageNeitral"];
            if (val != null && val != DBNull.Value)
            {
                long iNominalVoltageNeutral = Convert.ToInt64(val);

                GridVertical.BeginUpdate();

                if (iNominalVoltageNeutral < 110)
                {
                    GridVertical.Rows["rInputManufacturerIDNeutral"].OptionsRow.AllowFocus = false;
                    GridVertical.Rows["rInputNumberNeutral"].OptionsRow.AllowFocus = false;
                    GridVertical.Rows["rInputCreateYearNeutral"].OptionsRow.AllowFocus = false;
                    GridVertical.Rows["rInputUseBeginYearNeutral"].OptionsRow.AllowFocus = false;

                    GridVertical.Rows["rInputManufacturerIDNeutral"].Visible = false;
                    GridVertical.Rows["rInputNumberNeutral"].Visible = false;
                    GridVertical.Rows["rInputCreateYearNeutral"].Visible = false;
                    GridVertical.Rows["rInputUseBeginYearNeutral"].Visible = false;

                    if (bUpdateDB)
                    {
                        drv.Row["InputKindNeutral"] = 3;
                        drv.Row["InputManufacturerIDNeutral"] = DBNull.Value;
                        drv.Row["InputNumberNeutral"] = DBNull.Value;
                        drv.Row["InputCreateYearNeutral"] = DBNull.Value;
                        drv.Row["InputUseBeginYearNeutral"] = DBNull.Value;
                    }

                    GridVertical.Rows["rInputKindNeutral"].Properties.ReadOnly = true;
                    GridVertical.Rows["rInputKindNeutral"].Appearance.BackColor = Color.FromArgb(240, 240, 240);
                    GridVertical.Rows["rInputKindNeutral"].Appearance.Options.UseBackColor = true;
                }
                else
                {
                    GridVertical.Rows["rInputManufacturerIDNeutral"].OptionsRow.AllowFocus = true;
                    GridVertical.Rows["rInputNumberNeutral"].OptionsRow.AllowFocus = true;
                    GridVertical.Rows["rInputCreateYearNeutral"].OptionsRow.AllowFocus = true;
                    GridVertical.Rows["rInputUseBeginYearNeutral"].OptionsRow.AllowFocus = true;

                    GridVertical.Rows["rInputManufacturerIDNeutral"].Visible = true;
                    GridVertical.Rows["rInputNumberNeutral"].Visible = true;
                    GridVertical.Rows["rInputCreateYearNeutral"].Visible = true;
                    GridVertical.Rows["rInputUseBeginYearNeutral"].Visible = true;

                    GridVertical.Rows["rInputKindNeutral"].Properties.ReadOnly = false;
                    GridVertical.Rows["rInputKindNeutral"].Appearance.Options.UseBackColor = false;
                }

                GridVertical.EndUpdate();
            }
        }

        private void UpdateInputVoltageCategory(bool bUpdateDB = true)
        {
            long EquipmentKindID = -1;
            if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
            {
                DataRowView drv_ = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                EquipmentKindID = Convert.ToInt64(drv_.Row["EquipmentKindID"]);
            }

            // только для трансформатора
            if ((Equipment.EquipmentKind)EquipmentKindID != Equipment.EquipmentKind.Transformer) return;

            DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);


            long iNominalVoltageMiddle = -1;
            long iConstructionType = -1;

            object val = drv.Row["NominalVoltageMiddle"];
            if (val != null && val != DBNull.Value)
            {
                iNominalVoltageMiddle = Convert.ToInt64(val);
            }
            val = drv.Row["ConstructionType"];
            if (val != null && val != DBNull.Value)
            {
                iConstructionType = Convert.ToInt64(val);
            }

            if (iConstructionType != 3)
            {
                // для нетрехфазного тр-ра кол-во RPN должно быть 1 или 0
                val = drv.Row["RPNCnt"];
                if (val != null && val != DBNull.Value && Convert.ToInt64(val) > 1)
                {
                    drv.Row["RPNCnt"] = 1;
                    UpdateRPNCnt(bUpdateDB);
                }

                listRPNCnt.Clear();
                listRPNCnt.Add(new DataSourceString(0, "нет"));
                listRPNCnt.Add(new DataSourceString(1, "1"));
                repRPNCnt.DropDownRows = listRPNCnt.Count;
            }
            else
            {
                listRPNCnt.Clear();
                listRPNCnt.Add(new DataSourceString(0, "нет"));
                listRPNCnt.Add(new DataSourceString(1, "1"));
                listRPNCnt.Add(new DataSourceString(3, "3"));
                repRPNCnt.DropDownRows = listRPNCnt.Count;
            }

            if (iConstructionType != 3 && iConstructionType != 1 /*|| iNominalVoltageMiddle <= 0*/)
            {
                // прячем весь раздел
                GridVertical.BeginUpdate();

                foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                {
                    for (int i = 0; i < p.Value.Count; i++)
                    {
                        if (bUpdateDB)
                            drv.Row[p.Value[i] + p.Key] = DBNull.Value;
                        GridVertical.Rows["r" + p.Value[i] + p.Key].OptionsRow.AllowFocus = false;
                    }

                    GridVertical.Rows["catInput" + p.Key].Visible = false;
                }

                GridVertical.EndUpdate();
            }
            else
            {
                GridVertical.BeginUpdate();

                foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                {
                    for (int i = 0; i < p.Value.Count; i++)
                    {
                        GridVertical.Rows["r" + p.Value[i] + p.Key].OptionsRow.AllowFocus = true;
                    }

                    GridVertical.Rows["catInput" + p.Key].Visible = true;
                }

                List<string> listDisableInputs = new List<string>();

                if (iConstructionType == 3)
                {
                    // Если класс напряжения СН  110 кВ и выше, в поле «Тип ввода» появляются строки (для трёхфазного):
                    if (iNominalVoltageMiddle >= 110)
                    {
                    }
                    // Если СН 35 кВ и ниже, то тип ввода СН не появляется и строки будет всего четыре: три фазы и нейтраль.
                    else
                    {
                        listDisableInputs.Add("MiddleA");
                        listDisableInputs.Add("MiddleB");
                        listDisableInputs.Add("MiddleC");
                    }
                }
                if (iConstructionType == 1)
                {
                    // Если трансформатор однофазный и СН 110 кВ или больше , то
                    if (iNominalVoltageMiddle >= 110)
                    {
                        listDisableInputs.Add("HighB");
                        listDisableInputs.Add("HighC");
                        listDisableInputs.Add("MiddleB");
                        listDisableInputs.Add("MiddleC");
                    }
                    // Если СН 35 кВ и ниже, то тип ввода СН не появляется и строки будет всего две: фаза и нейтраль.
                    else
                    {
                        listDisableInputs.Add("HighB");
                        listDisableInputs.Add("HighC");
                        listDisableInputs.Add("MiddleA");
                        listDisableInputs.Add("MiddleB");
                        listDisableInputs.Add("MiddleC");
                    }
                }

                for (int i = 0; i < listDisableInputs.Count; i++)
                {
                    GridVertical.Rows["catInput" + listDisableInputs[i]].Visible = false;

                    for (int j = 0; j < m_dictInputs[listDisableInputs[i]].Count; j++)
                    {
                        string strFieldName = m_dictInputs[listDisableInputs[i]][j] + listDisableInputs[i];
                        if (bUpdateDB)
                            drv.Row[strFieldName] = DBNull.Value;
                        GridVertical.Rows["r" + strFieldName].OptionsRow.AllowFocus = false;
                    }
                }

                GridVertical.EndUpdate();
            }
        }

        private void GridVertical_ShowingEditor(object sender, CancelEventArgs e)
        {
            //if (GridVertical.FocusedRow.Name == "rBranch" || GridVertical.FocusedRow.Name == "rSubject")
            //    e.Cancel = true;

            if (m_ReadOnly != 0)
            {
                if (GridVertical.FocusedRow.Name == "rManufacturer"
                    || GridVertical.FocusedRow.Name == "rEquipmentType"
                    || GridVertical.FocusedRow.Name == "rBranch"
                    || GridVertical.FocusedRow.Name == "rSubject"
                    || GridVertical.FocusedRow.Name == "rSubstation"
                    || GridVertical.FocusedRow.Name == "rCoolingSystemType"
                    || GridVertical.FocusedRow.Name == "rConstructionType"
                    || GridVertical.FocusedRow.Name == "rNominalPower"
                    || GridVertical.FocusedRow.Name == "rNominalCurrent"
                    || GridVertical.FocusedRow.Name == "rSwitchDriveType"
                    || GridVertical.FocusedRow.Name == "rNominalVoltageLow"
                    || GridVertical.FocusedRow.Name == "rNominalVoltageMiddle"
                    || GridVertical.FocusedRow.Name == "rNominalVoltageHigh"
                    || GridVertical.FocusedRow.Name == "rProjectTimeLife"
                    || GridVertical.FocusedRow.Name == "rCreateYear"
                    || GridVertical.FocusedRow.Name == "rProtectionOilType"
                    /*|| GridVertical.FocusedRow.Name == "InputKindHighA"
                    || GridVertical.FocusedRow.Name == "InputKindHighB"
                    || GridVertical.FocusedRow.Name == "InputKindHighC"
                    || GridVertical.FocusedRow.Name == "InputKindMiddleA"
                    || GridVertical.FocusedRow.Name == "InputKindMiddleB"
                    || GridVertical.FocusedRow.Name == "InputKindMiddleC"
                    || GridVertical.FocusedRow.Name == "InputKindNeutral"*/
                    || GridVertical.FocusedRow.Name == "rRPNCnt"
                    || GridVertical.FocusedRow.Name == "rRPNVoltage"
                    || GridVertical.FocusedRow.Name == "rRPNTypeID"
                    || GridVertical.FocusedRow.Name == "rRPNKind"
                    || GridVertical.FocusedRow.Name == "rRPNNumber"
                    || GridVertical.FocusedRow.Name == "rRPNNumber2"
                    || GridVertical.FocusedRow.Name == "rRPNNumber3")
                {
                    e.Cancel = true;
                    //MyLocalizer.XtraMessageBoxShow("Данное поле недоступно для изменения.");
                }

                if (m_dictInputsControlsName.ContainsKey(GridVertical.FocusedRow.Name))
                {
                    e.Cancel = true;
                }
            }
        }

        /*private void cbEquipmentKind_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;

            if (!m_bDataLoad)
            {
                long EquipmentKindID = -1;
                if (cbEquipmentKind.EditValue != null && cbEquipmentKind.EditValue != DBNull.Value)
                {
                    EquipmentKindID = Convert.ToInt64(cbEquipmentKind.EditValue);
                }
                this.qEquipmentTypesByKindTableAdapter.Fill(this.dataSetQuery.QEquipmentTypesByKind, EquipmentKindID);
                this.qNominalPowersByKindTableAdapter.Fill(this.dataSetQuery.QNominalPowersByKind, EquipmentKindID);
                this.qRPNTypesByKindTableAdapter.Fill(this.dataSetQuery.QRPNTypesByKind, EquipmentKindID);
                this.qInputVoltageTypesByKindTableAdapter.Fill(this.dataSetQuery.QInputVoltageTypesByKind, EquipmentKindID);
                this.qEquipmentClassesByKindTableAdapter.Fill(this.dataSetQuery.QEquipmentClassesByKind, EquipmentKindID);

                if (this.dataSetQuery.QEquipmentTypesByKind.Rows.Count < 7)
                    this.repEquipmentType.DropDownRows = this.dataSetQuery.QEquipmentTypesByKind.Rows.Count;
                else
                    this.repEquipmentType.DropDownRows = 7;

                if (this.dataSetQuery.QNominalPowersByKind.Rows.Count < 7)
                    this.repNominalPower.DropDownRows = this.dataSetQuery.QNominalPowersByKind.Rows.Count;
                else
                    this.repNominalPower.DropDownRows = 7;

                if (this.dataSetQuery.QRPNTypesByKind.Rows.Count < 7)
                    this.repRPNType.DropDownRows = this.dataSetQuery.QRPNTypesByKind.Rows.Count;
                else
                    this.repRPNType.DropDownRows = 7;

                if (this.dataSetQuery.QInputVoltageTypesByKind.Rows.Count < 7)
                    this.repInputType.DropDownRows = this.dataSetQuery.QInputVoltageTypesByKind.Rows.Count;
                else
                    this.repInputType.DropDownRows = 7;

                if (this.dataSetQuery.QEquipmentClassesByKind.Rows.Count < 7)
                    cbEquipmentClass.Properties.DropDownRows = this.dataSetQuery.QEquipmentClassesByKind.Rows.Count;
                else
                    cbEquipmentClass.Properties.DropDownRows = 7;

                GridVertical.BeginUpdate();

                DevExpress.XtraVerticalGrid.Rows.BaseRow gridEquipmentTypeRow;
                gridEquipmentTypeRow = GridVertical.GetRowByFieldName("EquipmentTypeID");//.Rows[0].ChildRows[0];
                GridVertical.InvalidateRow(gridEquipmentTypeRow);

                DevExpress.XtraVerticalGrid.Rows.BaseRow gridCoolingSystemTypeRow;
                gridCoolingSystemTypeRow = GridVertical.GetRowByFieldName("CoolingSystemTypeID");//.Rows[0].ChildRows[0];
                GridVertical.InvalidateRow(gridCoolingSystemTypeRow);

                DevExpress.XtraVerticalGrid.Rows.BaseRow gridNominalPowerRow;
                gridNominalPowerRow = GridVertical.GetRowByFieldName("NominalPowerID");//.Rows[0].ChildRows[0];
                GridVertical.InvalidateRow(gridNominalPowerRow);

                DevExpress.XtraVerticalGrid.Rows.BaseRow gridProtectionOilTypeRow;
                gridProtectionOilTypeRow = GridVertical.GetRowByFieldName("ProtectionOilTypeID");
                GridVertical.InvalidateRow(gridProtectionOilTypeRow);

                if (this.dataSetQuery.QEquipmentClassesByKind.Count > 0)
                {
                    cbEquipmentClass.EditValue = this.dataSetQuery.QEquipmentClassesByKind.Rows[0]["EquipmentClassID"];
                }

                GridVertical.EndUpdate();
            }
        }*/

        private void bCancel_Click(object sender, EventArgs e)
        {
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if (!SaveData()) return;

            if (m_bShowContinueMsg)
            {
                if (MyLocalizer.XtraMessageBoxShow("Перейти к заполнению данных по обследованиям?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    m_bContinueNext = true;
                }
                else
                {
                    m_bDataLoad = true;
                    m_bDataLoadEnd = false;
                    GridVertical.BeginUpdate();
                    this.qEquipmentRecordTableAdapter.Fill(this.dataSetQuery2.QEquipmentRecord, m_id);
                    GridVertical.EndUpdate();
                    m_bDataLoad = false;
                    m_bDataLoadEnd = true;

                    return;
                }

                /*NextPrevForm form = new NextPrevForm();
                form.m_strPrev = "";
                form.m_strNext = "Данные визуального обследования";

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (form.m_bNext) m_bContinueNext = true;
                    else m_bContinuePrev = true;
                }*/
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private bool SaveData()
        {
            try
            {
                long EquipmentClassID = -1;
                long EquipmentKindID = -1;
                long EquipmentTypeID = -1;
                long SubstationID = -1;
                if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
                {
                    DataRowView drv_ = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                    EquipmentKindID = Convert.ToInt64(drv_.Row["EquipmentKindID"]);
                    EquipmentClassID = Convert.ToInt64(cbEquipmentClass.EditValue);
                }
                string strEquipmentName = teEquipmentName.Text.Trim();
                string strEquipmentNumber = teEquipmentNumber.Text.Trim();

                if (EquipmentClassID <= 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать вид оборудования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbEquipmentClass.Focus();
                    return false;
                }

                DataRowView drv = (DataRowView)qEquipmentRecordBindingSource.Current;
                if (drv == null) return false;

                //if (!drv.IsEdit) drv.BeginEdit();
                drv.Row["EquipmentKindID"] = EquipmentKindID;

                object val = drv.Row["SubstationID"];
                if (val != null && val != DBNull.Value)
                    SubstationID = Convert.ToInt64(val);

                long VoltageLow = -1;
                long VoltageMiddle = -1;
                long VoltageHigh = -1;
                long VoltageNeitral = -1;

                val = drv.Row["NominalVoltageLow"];
                if (val != null && val != DBNull.Value)
                    VoltageLow = Convert.ToInt64(val);

                val = drv.Row["NominalVoltageMiddle"];
                if (val != null && val != DBNull.Value)
                    VoltageMiddle = Convert.ToInt64(val);

                val = drv.Row["NominalVoltageHigh"];
                if (val != null && val != DBNull.Value)
                    VoltageHigh = Convert.ToInt64(val);

                val = drv.Row["NominalVoltageNeitral"];
                if (val != null && val != DBNull.Value)
                    VoltageNeitral = Convert.ToInt64(val);

                val = drv.Row["EquipmentTypeID"];
                if (val != null && val != DBNull.Value)
                    EquipmentTypeID = Convert.ToInt64(val);

                long CreateYear = -1;
                val = drv.Row["CreateYear"];
                if (val != null && val != DBNull.Value)
                    CreateYear = Convert.ToInt64(val);

                long UseBeginYear = -1;
                val = drv.Row["UseBeginYear"];
                if (val != null && val != DBNull.Value)
                    UseBeginYear = Convert.ToInt64(val);

                /*DevExpress.XtraVerticalGrid.Rows.BaseRow gridSubstationRow;
                gridSubstationRow = GridVertical.GetRowByFieldName("SubstationID");//.Rows[0].ChildRows[0];
                object val = GridVertical.GetCellValue(gridSubstationRow, 0);
                if (val != null && val != DBNull.Value)
                    SubstationID = Convert.ToInt64(val);*/

                if (strEquipmentName == "")
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать диспетчерское наименование оборудования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    teEquipmentName.Focus();
                    return false;
                }
                if (strEquipmentNumber == "")
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать заводской номер оборудования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    teEquipmentNumber.Focus();
                    return false;
                }

                if (EquipmentKindID <= 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Не удалось определить категорию оборудования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //cbEquipmentKind.Focus();
                    return false;
                }                

                if (SubstationID <= 0)
                {
                    GridVertical.FocusedRow = rSubstation;
                    GridVertical.MakeRowVisible(rSubstation);
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать подстанцию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GridVertical.ShowEditor();
                    return false;
                }

                if (UseBeginYear <= 0)
                {
                    GridVertical.FocusedRow = rUseBeginYear;
                    GridVertical.MakeRowVisible(rUseBeginYear);
                    MyLocalizer.XtraMessageBoxShow("Необходимо указать год ввода в эксплуатацию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GridVertical.ShowEditor();
                    return false;
                }

                if (CreateYear > 0 && UseBeginYear > 0 && CreateYear > UseBeginYear)
                {
                    GridVertical.FocusedRow = rCreateYear;
                    GridVertical.MakeRowVisible(rCreateYear);
                    MyLocalizer.XtraMessageBoxShow("Год изготовления не должен превышать год ввода в эксплуатацию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GridVertical.ShowEditor();
                    return false;
                }

                if (EquipmentTypeID <= 0)
                {
                    GridVertical.FocusedRow = rEquipmentType;
                    GridVertical.MakeRowVisible(rEquipmentType);
                    if ((Equipment.EquipmentKind)EquipmentKindID == Equipment.EquipmentKind.Transformer)
                        MyLocalizer.XtraMessageBoxShow("Необходимо указать тип трансформатора.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MyLocalizer.XtraMessageBoxShow("Необходимо указать тип выключателя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    GridVertical.ShowEditor();
                    return false;
                }

                if ((Equipment.EquipmentKind)EquipmentKindID == Equipment.EquipmentKind.Transformer)
                {
                    drv.Row["NominalCurrent"] = DBNull.Value;
                    drv.Row["SwitchDriveTypeID"] = DBNull.Value;

                    long CoolingSystemTypeID = -1;

                    val = drv.Row["CoolingSystemTypeID"];
                    if (val != null && val != DBNull.Value)
                        CoolingSystemTypeID = Convert.ToInt64(val);

                    if (CoolingSystemTypeID <= 0)
                    {
                        GridVertical.FocusedRow = rCoolingSystemType;
                        GridVertical.MakeRowVisible(rCoolingSystemType);
                        MyLocalizer.XtraMessageBoxShow("Необходимо указать тип системы охлаждения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        GridVertical.ShowEditor();
                        return false;
                    }

                    val = drv.Row["ConstructionType"];
                    if (val == null || val == DBNull.Value)
                    {
                        GridVertical.FocusedRow = rConstructionType;
                        GridVertical.MakeRowVisible(rConstructionType);
                        MyLocalizer.XtraMessageBoxShow("Необходимо указать тип исполнения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        GridVertical.ShowEditor();
                        return false;
                    }

                    val = drv.Row["ProtectionOilTypeID"];
                    if (val == null || val == DBNull.Value)
                    {
                        GridVertical.FocusedRow = rProtectionOilType;
                        GridVertical.MakeRowVisible(rProtectionOilType);
                        MyLocalizer.XtraMessageBoxShow("Необходимо указать тип защиты масла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        GridVertical.ShowEditor();
                        return false;
                    }

                    val = drv.Row["RPNCnt"];
                    if (val != null && val != DBNull.Value)
                    {
                        if (Convert.ToInt64(val) > 0)
                        {
                            val = drv.Row["RPNVoltage"];
                            if (val == null || val == DBNull.Value)
                            {
                                GridVertical.FocusedRow = rRPNVoltage;
                                GridVertical.MakeRowVisible(rRPNVoltage);
                                MyLocalizer.XtraMessageBoxShow("Необходимо указать класс напряжения РПН.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                GridVertical.ShowEditor();
                                return false;
                            }
                        }
                    }


                    if (catInputHighA.Visible || catInputHighB.Visible || catInputHighC.Visible)
                    {
                        if (VoltageHigh < 0)
                        {
                            GridVertical.FocusedRow = rNominalVoltageHigh;
                            GridVertical.MakeRowVisible(rNominalVoltageHigh);
                            MyLocalizer.XtraMessageBoxShow("Необходимо указать класс напряжения ВН.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            GridVertical.ShowEditor();
                            return false;
                        }
                    }

                    if (catInputMiddleA.Visible || catInputMiddleB.Visible || catInputMiddleC.Visible)
                    {
                        if (VoltageMiddle < 0)
                        {
                            GridVertical.FocusedRow = rNominalVoltageMiddle;
                            GridVertical.MakeRowVisible(rNominalVoltageMiddle);
                            MyLocalizer.XtraMessageBoxShow("Необходимо указать класс напряжения СН.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            GridVertical.ShowEditor();
                            return false;
                        }
                    }

                    if (catInputNeutral.Visible)
                    {
                        if (VoltageNeitral < 0)
                        {
                            GridVertical.FocusedRow = rNominalVoltageNeitral;
                            GridVertical.MakeRowVisible(rNominalVoltageNeitral);
                            MyLocalizer.XtraMessageBoxShow("Необходимо указать класс напряжения нейтрали.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            GridVertical.ShowEditor();
                            return false;
                        }
                    }

                    if (VoltageLow >= VoltageMiddle && VoltageMiddle != -1)
                    {
                        GridVertical.FocusedRow = rNominalVoltageLow;
                        GridVertical.MakeRowVisible(rNominalVoltageLow);
                        MyLocalizer.XtraMessageBoxShow("Класс напряжения НН должно быть меньше класса напряжения СН.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        GridVertical.ShowEditor();
                        return false;
                    }

                    if (VoltageLow >= VoltageHigh && VoltageMiddle == -1 && VoltageHigh != -1)
                    {
                        GridVertical.FocusedRow = rNominalVoltageLow;
                        GridVertical.MakeRowVisible(rNominalVoltageLow);
                        MyLocalizer.XtraMessageBoxShow("Класс напряжения НН должно быть меньше класса напряжения ВН.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); GridVertical.FocusedRow = rNominalVoltageLow;
                        GridVertical.ShowEditor();
                        return false;
                    }

                    if (VoltageMiddle >= VoltageHigh && VoltageHigh != -1)
                    {
                        GridVertical.FocusedRow = rNominalVoltageMiddle;
                        GridVertical.MakeRowVisible(rNominalVoltageMiddle);
                        MyLocalizer.XtraMessageBoxShow("Класс напряжения СН должно быть меньше класса напряжения ВН.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        GridVertical.ShowEditor();
                        return false;
                    }
                }
                else
                {
                    drv.Row["CoolingSystemTypeID"] = DBNull.Value;
                    drv.Row["ConstructionType"] = DBNull.Value;
                    drv.Row["ProtectionOilTypeID"] = DBNull.Value;
                    drv.Row["NominalVoltageLow"] = DBNull.Value;
                    drv.Row["NominalVoltageMiddle"] = DBNull.Value;
                    drv.Row["NominalVoltageNeitral"] = DBNull.Value;
                    drv.Row["RPNCnt"] = 0;
                    drv.Row["RPNVoltage"] = DBNull.Value;
                    drv.Row["RPNKind"] = DBNull.Value;
                    drv.Row["RPNTypeID"] = DBNull.Value;
                    drv.Row["RPNNumber"] = DBNull.Value;
                    drv.Row["RPNNumber2"] = DBNull.Value;
                    drv.Row["RPNNumber3"] = DBNull.Value;
                    drv.Row["NominalPower"] = DBNull.Value;

                    if (VoltageHigh < 0)
                    {
                        GridVertical.FocusedRow = rNominalVoltageHigh;
                        GridVertical.MakeRowVisible(rNominalVoltageHigh);
                        MyLocalizer.XtraMessageBoxShow("Необходимо указать класс напряжения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        GridVertical.ShowEditor();
                        return false;
                    }
                }

                // проверка на уникальность наименования и номера
                SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                connection.Open();
                SQLiteCommand com = new SQLiteCommand(connection);
                com.CommandText = "Select * from Equipments WHERE EQUAL_STR(EquipmentName, ?) = 0 AND EQUAL_STR(EquipmentNumber, ?) = 0 AND EquipmentID <> ?";
                com.CommandType = CommandType.Text;
                SQLiteParameter param1 = new SQLiteParameter("@Param1", DbType.String);
                param1.Value = strEquipmentName;
                SQLiteParameter param2 = new SQLiteParameter("@Param2", DbType.String);
                param2.Value = strEquipmentNumber;
                SQLiteParameter param3 = new SQLiteParameter("@Param3", DbType.Int64);
                param3.Value = m_id;
                com.Parameters.Add(param1);
                com.Parameters.Add(param2);
                com.Parameters.Add(param3);
                SQLiteDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    connection.Close();
                    MyLocalizer.XtraMessageBoxShow("Оборудование с таким наименованием и номером уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                dr.Close();

                // запретить менять категорию оборудования, если есть обследования
                if (m_id > 0)
                {
                    com.CommandText = "Select COUNT(*) AS Cnt from Inspections AS i INNER JOIN Equipments AS e ON i.EquipmentID = e.EquipmentID AND e.EquipmentID = ? AND e.EquipmentKindID <> ?";
                    com.CommandType = CommandType.Text;
                    SQLiteParameter param1_ = new SQLiteParameter("@Param1", DbType.Int64);
                    param1_.Value = m_id;
                    com.Parameters.Add(param1_);

                    SQLiteParameter param2_ = new SQLiteParameter("@Param2", DbType.Int64);
                    param2_.Value = EquipmentKindID;
                    com.Parameters.Add(param2_);

                    SQLiteDataReader dr_ = com.ExecuteReader();
                    if (dr_.HasRows)
                    {
                        while (dr_.Read())
                        {
                            if (dr_["Cnt"] != DBNull.Value)
                            {
                                if (Convert.ToInt64(dr_["Cnt"]) > 0)
                                {
                                    dr_.Close();
                                    connection.Close();
                                    MyLocalizer.XtraMessageBoxShow("Данное оборудование содержит обследования. Изменение категории оборудования запрещено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }
                        }
                    }
                    dr_.Close();
                }

                // проверка вводов
                foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                {
                    // если ввод видимый, то
                    if (GridVertical.Rows["catInput" + p.Key].Visible)
                    {
                        long CreateYear_ = -1;
                        val = drv.Row["InputCreateYear" + p.Key];
                        if (val != null && val != DBNull.Value)
                            CreateYear_ = Convert.ToInt64(val);

                        long UseBeginYear_ = -1;
                        val = drv.Row["InputUseBeginYear" + p.Key];
                        if (val != null && val != DBNull.Value)
                            UseBeginYear_ = Convert.ToInt64(val);

                        if (CreateYear_ > 0 && UseBeginYear_ > 0 && CreateYear_ > UseBeginYear_)
                        {
                            GridVertical.FocusedRow = GridVertical.Rows["rInputUseBeginYear" + p.Key];
                            GridVertical.MakeRowVisible(GridVertical.Rows["rInputUseBeginYear" + p.Key]);

                            if ((Equipment.EquipmentKind)EquipmentKindID == Equipment.EquipmentKind.Transformer)
                                MyLocalizer.XtraMessageBoxShow("Год изготовления для одного из вводов не должен превышать год ввода в эксплуатацию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                MyLocalizer.XtraMessageBoxShow("Год изготовления для одного из полюсов не должен превышать год ввода в эксплуатацию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            GridVertical.ShowEditor();
                            return false;
                        }

                        string strInputKindName = "InputKind" + p.Key;
                        val = drv.Row[strInputKindName];
                        if (val == null || val == DBNull.Value)
                        {
                            if (GridVertical.Rows["catInput" + p.Key].Visible)
                            {
                                GridVertical.FocusedRow = GridVertical.Rows["rInputKind" + p.Key];
                                GridVertical.MakeRowVisible(GridVertical.Rows["rInputKind" + p.Key]);

                                if ((Equipment.EquipmentKind)EquipmentKindID == Equipment.EquipmentKind.Transformer)
                                    MyLocalizer.XtraMessageBoxShow("Необходимо указать вид во всех вводах.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                    MyLocalizer.XtraMessageBoxShow("Необходимо указать вид во всех полюсах.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                GridVertical.ShowEditor();
                                return false;
                            }
                        }
                    }
                }

                string strInputIDs = "";

                bool bChangeInputs = false;

                foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                {
                    for (int i = 0; i < p.Value.Count; i++)
                    {
                        if (m_dictInputsOldValues[p.Value[i] + p.Key].ToString() != drv.Row[p.Value[i] + p.Key].ToString())
                        {
                            bChangeInputs = true;
                            break;
                        }
                    }

                    if (bChangeInputs) break;
                }

                if (bChangeInputs)
                {
                    string strInputKindName = "";
                    string strInputIDName = "";
                    string strInputManufacturerIDName = "";
                    string strInputNumberName = "";
                    string strInputTypeName = "";
                    string strInputCreateYearName = "";
                    string strInputUseBeginYearName = "";
                    string strInputName = "";
                    // удаляем инфу о вводах
                    foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                    {
                        // если ввод видимый, то
                        if (GridVertical.Rows["catInput" + p.Key].Visible)
                        {
                            strInputIDName = "InputID" + p.Key;
                            strInputTypeName = "InputTypeID" + p.Key;
                            strInputKindName = "InputKind" + p.Key;
                            strInputManufacturerIDName = "InputManufacturerID" + p.Key;
                            strInputNumberName = "InputNumber" + p.Key;
                            strInputCreateYearName = "InputCreateYear" + p.Key;
                            strInputUseBeginYearName = "InputUseBeginYear" + p.Key;
                            strInputName = "InputName" + p.Key;

                            val = drv.Row[strInputIDName];
                            if (val != null && val != DBNull.Value)
                            {
                                if (strInputIDs == "") strInputIDs = val.ToString();
                                else strInputIDs += "," + Convert.ToInt64(val);
                            }

                            drv.Row["InputID" + p.Key] = DBNull.Value;

                            val = drv.Row[strInputKindName];
                            if (val != null && val != DBNull.Value)
                            {
                                com.Parameters.Clear();

                                SQLiteParameter p_kind = new SQLiteParameter("@kind", DbType.Int64);
                                p_kind.Value = Convert.ToInt64(val);
                                com.Parameters.Add(p_kind);

                                SQLiteParameter p_type = new SQLiteParameter("@type_id", DbType.Int64);
                                val = drv.Row[strInputTypeName];
                                if (val != null && val != DBNull.Value) p_type.Value = Convert.ToInt64(val);
                                else p_type.Value = DBNull.Value;
                                com.Parameters.Add(p_type);

                                SQLiteParameter p_manufacturerID = new SQLiteParameter("@manufacturerID", DbType.Int64);
                                val = drv.Row[strInputManufacturerIDName];
                                if (val != null && val != DBNull.Value) p_manufacturerID.Value = Convert.ToInt64(val);
                                else p_manufacturerID.Value = DBNull.Value;
                                com.Parameters.Add(p_manufacturerID);

                                SQLiteParameter p_number = new SQLiteParameter("@number", DbType.String);
                                val = drv.Row[strInputNumberName];
                                if (val != null && val != DBNull.Value) p_number.Value = Convert.ToString(val);
                                else p_number.Value = DBNull.Value;
                                com.Parameters.Add(p_number);

                                SQLiteParameter p_create_year = new SQLiteParameter("@create_year", DbType.Int64);
                                val = drv.Row[strInputCreateYearName];
                                if (val != null && val != DBNull.Value) p_create_year.Value = Convert.ToInt64(val);
                                else p_create_year.Value = DBNull.Value;
                                com.Parameters.Add(p_create_year);

                                SQLiteParameter p_use_begin_year = new SQLiteParameter("@use_begin_year", DbType.Int64);
                                val = drv.Row[strInputUseBeginYearName];
                                if (val != null && val != DBNull.Value) p_use_begin_year.Value = Convert.ToInt64(val);
                                else p_use_begin_year.Value = DBNull.Value;
                                com.Parameters.Add(p_use_begin_year);

                                SQLiteParameter p_name = new SQLiteParameter("@name", DbType.String);
                                val = drv.Row[strInputName];
                                if (val != null && val != DBNull.Value) p_name.Value = Convert.ToString(val);
                                else p_name.Value = DBNull.Value;
                                com.Parameters.Add(p_name);

                                com.CommandText = "INSERT INTO Inputs (InputKind, InputTypeID, InputManufacturerID, InputNumber, InputCreateYear, InputUseBeginYear, InputName) " +
                                    " VALUES (@kind, @type_id, @manufacturerID, @number, @create_year, @use_begin_year, @name)";
                                com.ExecuteNonQuery();

                                com.CommandText = "select seq from sqlite_sequence where name = 'Inputs'";
                                com.Parameters.Clear();
                                SQLiteDataReader drInput = com.ExecuteReader();

                                long id = 0;
                                while (drInput.Read())
                                {
                                    id = Convert.ToInt64(drInput["seq"]);
                                }
                                drInput.Close();

                                drv.Row[strInputIDName] = id;
                            }
                        }
                    }
                }


                connection.Close();

                if (m_id > 0)
                {
                    //((DataRowView)qEquipmentRecordBindingSource.Current).BeginEdit();
                    ((DataRowView)qEquipmentRecordBindingSource.Current).EndEdit();

                    SQLiteCommand upd_com = new SQLiteCommand(this.qEquipmentRecordTableAdapter.Connection);
                    upd_com.CommandText = "UPDATE Equipments SET " +
                        "SubstationID = @param1, " +
                        "EquipmentKindID = @param2, " +
                        "EquipmentTypeID = @param3, " +
                        "ManufacturerID = @param4, " +
                        "ConstructionType = @param5, " +
                        "CoolingSystemTypeID = @param6, " +
                        "EquipmentName = @param7, " +
                        "EquipmentNumber = @param8, " +
                        "CreateYear = @param9, " +
                        "UseBeginYear = @param10, " +
                        "NominalVoltageLow = @param11, " +
                        "NominalVoltageMiddle = @param12, " +
                        "NominalVoltageHigh = @param13, " +
                        "NominalPower = @param14, " +
                        "ProjectLifeTime = @param15, " +
                        "ActualLifeTime = @param16, " +
                        "LastWorkoverYear = @param17, " +
                        "LastTechnicalServiceYear = @param18, " +
                        "TechnicalServiceDocument = @param19, " +
                        "TechnicalServiceConclusion = @param20, " +
                        "NextTechnicalServiceYear = @param21, " +
                        "TechnicalServiceCount = @param22, " +
                        "ProtectionOilTypeID = @param23, " +
                        "InputIDHighA = @param24, " +
                        "InputIDHighB = @param25, " +
                        "InputIDHighC = @param26, " +
                        "InputIDMiddleA = @param27, " +
                        "InputIDMiddleB = @param28, " +
                        "InputIDMiddleC = @param29, " +
                        "InputIDNeutral = @param30, " +
                        "RPNCnt = @param31, " +
                        "RPNVoltage = @param32, " +
                        "Image = @param33, " +
                        "NominalVoltageNeitral = @param34, " +
                        "RPNTypeID = @param35, " +
                        "RPNKind = @param36, " +
                        "RPNNumber = @param37, " +
                        "RPNNumber2 = @param38, " +
                        "RPNNumber3 = @param39, " +
                        "EquipmentClassID = @param40, " +
                        "NominalCurrent = @param41, " +
                        "SwitchDriveTypeID = @param42 " +
                        "WHERE EquipmentID = @param_id";
                    upd_com.Parameters.Add("@param1", DbType.Int64).SourceColumn = "SubstationID";
                    upd_com.Parameters.Add("@param2", DbType.Int64).SourceColumn = "EquipmentKindID";
                    upd_com.Parameters.Add("@param3", DbType.Int64).SourceColumn = "EquipmentTypeID";
                    upd_com.Parameters.Add("@param4", DbType.Int64).SourceColumn = "ManufacturerID";
                    upd_com.Parameters.Add("@param5", DbType.Int64).SourceColumn = "ConstructionType";
                    upd_com.Parameters.Add("@param6", DbType.Int64).SourceColumn = "CoolingSystemTypeID";
                    upd_com.Parameters.Add("@param7", DbType.String).SourceColumn = "EquipmentName";
                    upd_com.Parameters.Add("@param8", DbType.String).SourceColumn = "EquipmentNumber";
                    upd_com.Parameters.Add("@param9", DbType.Int64).SourceColumn = "CreateYear";
                    upd_com.Parameters.Add("@param10", DbType.Int64).SourceColumn = "UseBeginYear";
                    upd_com.Parameters.Add("@param11", DbType.Int64).SourceColumn = "NominalVoltageLow";
                    upd_com.Parameters.Add("@param12", DbType.Int64).SourceColumn = "NominalVoltageMiddle";
                    upd_com.Parameters.Add("@param13", DbType.Int64).SourceColumn = "NominalVoltageHigh";
                    upd_com.Parameters.Add("@param14", DbType.Decimal).SourceColumn = "NominalPower";
                    upd_com.Parameters.Add("@param15", DbType.Int64).SourceColumn = "ProjectLifeTime";
                    upd_com.Parameters.Add("@param16", DbType.Int64).SourceColumn = "ActualLifeTime";
                    upd_com.Parameters.Add("@param17", DbType.Int64).SourceColumn = "LastWorkoverYear";
                    upd_com.Parameters.Add("@param18", DbType.Int64).SourceColumn = "LastTechnicalServiceYear";
                    upd_com.Parameters.Add("@param19", DbType.String).SourceColumn = "TechnicalServiceDocument";
                    upd_com.Parameters.Add("@param20", DbType.String).SourceColumn = "TechnicalServiceConclusion";
                    upd_com.Parameters.Add("@param21", DbType.Int64).SourceColumn = "NextTechnicalServiceYear";
                    upd_com.Parameters.Add("@param22", DbType.Int64).SourceColumn = "TechnicalServiceCount";
                    upd_com.Parameters.Add("@param23", DbType.Int64).SourceColumn = "ProtectionOilTypeID";
                    upd_com.Parameters.Add("@param24", DbType.Int64).SourceColumn = "InputIDHighA";
                    upd_com.Parameters.Add("@param25", DbType.Int64).SourceColumn = "InputIDHighB";
                    upd_com.Parameters.Add("@param26", DbType.Int64).SourceColumn = "InputIDHighC";
                    upd_com.Parameters.Add("@param27", DbType.Int64).SourceColumn = "InputIDMiddleA";
                    upd_com.Parameters.Add("@param28", DbType.Int64).SourceColumn = "InputIDMiddleB";
                    upd_com.Parameters.Add("@param29", DbType.Int64).SourceColumn = "InputIDMiddleC";
                    upd_com.Parameters.Add("@param30", DbType.Int64).SourceColumn = "InputIDNeutral";
                    upd_com.Parameters.Add("@param31", DbType.Int64).SourceColumn = "RPNCnt";
                    upd_com.Parameters.Add("@param32", DbType.Int64).SourceColumn = "RPNVoltage";
                    SQLiteParameter paramImage = new SQLiteParameter("@param33", DbType.Object);
                    paramImage.Value = peImage.EditValue;
                    upd_com.Parameters.Add(paramImage);
                    upd_com.Parameters.Add("@param34", DbType.Int64).SourceColumn = "NominalVoltageNeitral";
                    upd_com.Parameters.Add("@param35", DbType.Int64).SourceColumn = "RPNTypeID";
                    upd_com.Parameters.Add("@param36", DbType.Int64).SourceColumn = "RPNKind";
                    upd_com.Parameters.Add("@param37", DbType.String).SourceColumn = "RPNNumber";
                    upd_com.Parameters.Add("@param38", DbType.String).SourceColumn = "RPNNumber2";
                    upd_com.Parameters.Add("@param39", DbType.String).SourceColumn = "RPNNumber3";
                    upd_com.Parameters.Add("@param40", DbType.Int64).SourceColumn = "EquipmentClassID";
                    upd_com.Parameters.Add("@param41", DbType.Int64).SourceColumn = "NominalCurrent";
                    upd_com.Parameters.Add("@param42", DbType.Int64).SourceColumn = "SwitchDriveTypeID";
                    upd_com.Parameters.Add("@param_id", DbType.Int64).SourceColumn = "EquipmentID";
                    this.qEquipmentRecordTableAdapter.Adapter.UpdateCommand = upd_com;
                }
                else
                {
                    if (((DataRowView)qEquipmentRecordBindingSource.Current).IsEdit)
                        ((DataRowView)qEquipmentRecordBindingSource.Current).EndEdit();

                    SQLiteCommand ins_com = new SQLiteCommand(this.qEquipmentRecordTableAdapter.Connection);
                    ins_com.CommandText = "INSERT INTO Equipments (" +
                        "SubstationID, " +
                        "EquipmentKindID, " +
                        "EquipmentTypeID, " +
                        "ManufacturerID, " +
                        "ConstructionType, " +
                        "CoolingSystemTypeID, " +
                        "EquipmentName, " +
                        "EquipmentNumber, " +
                        "CreateYear, " +
                        "UseBeginYear, " +
                        "NominalVoltageLow, " +
                        "NominalVoltageMiddle, " +
                        "NominalVoltageHigh, " +
                        "NominalPower, " +
                        "ProjectLifeTime, " +
                        "ActualLifeTime, " +
                        "LastWorkoverYear, " +
                        "LastTechnicalServiceYear, " +
                        "TechnicalServiceDocument, " +
                        "TechnicalServiceConclusion, " +
                        "NextTechnicalServiceYear, " +
                        "TechnicalServiceCount, " +
                        "ProtectionOilTypeID, " +
                        "InputIDHighA, " +
                        "InputIDHighB, " +
                        "InputIDHighC, " +
                        "InputIDMiddleA, " +
                        "InputIDMiddleB, " +
                        "InputIDMiddleC, " +
                        "InputIDNeutral, " +
                        "RPNCnt, " +
                        "RPNVoltage, " +
                        "Image, " +
                        "NominalVoltageNeitral, " +
                        "RPNTypeID, " +
                        "RPNKind, " +
                        "RPNNumber, " +
                        "RPNNumber2, " +
                        "RPNNumber3, " +
                        "EquipmentClassID," +
                        "NominalCurrent, " +
                        "SwitchDriveTypeID " +
                        ") VALUES (" +
                        "@param1, " +
                        "@param2, " +
                        "@param3, " +
                        "@param4, " +
                        "@param5, " +
                        "@param6, " +
                        "@param7, " +
                        "@param8, " +
                        "@param9, " +
                        "@param10, " +
                        "@param11, " +
                        "@param12, " +
                        "@param13, " +
                        "@param14, " +
                        "@param15, " +
                        "@param16, " +
                        "@param17, " +
                        "@param18, " +
                        "@param19, " +
                        "@param20, " +
                        "@param21, " +
                        "@param22, " +
                        "@param23, " + 
                        "@param24, " + 
                        "@param25, " + 
                        "@param26, " + 
                        "@param27, " + 
                        "@param28, " + 
                        "@param29, " +
                        "@param30, " +
                        "@param31, " +
                        "@param32, " + 
                        "@param33, " +
                        "@param34, " +
                        "@param35, " +
                        "@param36, " +
                        "@param37, " +
                        "@param38, " +
                        "@param39, " +
                        "@param40, " +
                        "@param41, " +
                        "@param42);";
                    ins_com.Parameters.Add("@param1", DbType.Int64).SourceColumn = "SubstationID";
                    ins_com.Parameters.Add("@param2", DbType.Int64).SourceColumn = "EquipmentKindID";
                    ins_com.Parameters.Add("@param3", DbType.Int64).SourceColumn = "EquipmentTypeID";
                    ins_com.Parameters.Add("@param4", DbType.Int64).SourceColumn = "ManufacturerID";
                    ins_com.Parameters.Add("@param5", DbType.Int64).SourceColumn = "ConstructionType";
                    ins_com.Parameters.Add("@param6", DbType.Int64).SourceColumn = "CoolingSystemTypeID";
                    ins_com.Parameters.Add("@param7", DbType.String).SourceColumn = "EquipmentName";
                    ins_com.Parameters.Add("@param8", DbType.String).SourceColumn = "EquipmentNumber";
                    ins_com.Parameters.Add("@param9", DbType.Int64).SourceColumn = "CreateYear";
                    ins_com.Parameters.Add("@param10", DbType.Int64).SourceColumn = "UseBeginYear";
                    ins_com.Parameters.Add("@param11", DbType.Int64).SourceColumn = "NominalVoltageLow";
                    ins_com.Parameters.Add("@param12", DbType.Int64).SourceColumn = "NominalVoltageMiddle";
                    ins_com.Parameters.Add("@param13", DbType.Int64).SourceColumn = "NominalVoltageHigh";
                    ins_com.Parameters.Add("@param14", DbType.Decimal).SourceColumn = "NominalPower";
                    ins_com.Parameters.Add("@param15", DbType.Int64).SourceColumn = "ProjectLifeTime";
                    ins_com.Parameters.Add("@param16", DbType.Int64).SourceColumn = "ActualLifeTime";
                    ins_com.Parameters.Add("@param17", DbType.Int64).SourceColumn = "LastWorkoverYear";
                    ins_com.Parameters.Add("@param18", DbType.Int64).SourceColumn = "LastTechnicalServiceYear";
                    ins_com.Parameters.Add("@param19", DbType.String).SourceColumn = "TechnicalServiceDocument";
                    ins_com.Parameters.Add("@param20", DbType.String).SourceColumn = "TechnicalServiceConclusion";
                    ins_com.Parameters.Add("@param21", DbType.Int64).SourceColumn = "NextTechnicalServiceYear";
                    ins_com.Parameters.Add("@param22", DbType.Int64).SourceColumn = "TechnicalServiceCount";
                    ins_com.Parameters.Add("@param23", DbType.Int64).SourceColumn = "ProtectionOilTypeID";
                    ins_com.Parameters.Add("@param24", DbType.Int64).SourceColumn = "InputIDHighA";
                    ins_com.Parameters.Add("@param25", DbType.Int64).SourceColumn = "InputIDHighB";
                    ins_com.Parameters.Add("@param26", DbType.Int64).SourceColumn = "InputIDHighC";
                    ins_com.Parameters.Add("@param27", DbType.Int64).SourceColumn = "InputIDMiddleA";
                    ins_com.Parameters.Add("@param28", DbType.Int64).SourceColumn = "InputIDMiddleB";
                    ins_com.Parameters.Add("@param29", DbType.Int64).SourceColumn = "InputIDMiddleC";
                    ins_com.Parameters.Add("@param30", DbType.Int64).SourceColumn = "InputIDNeutral";
                    ins_com.Parameters.Add("@param31", DbType.Int64).SourceColumn = "RPNCnt";
                    ins_com.Parameters.Add("@param32", DbType.Int64).SourceColumn = "RPNVoltage";
                    
                    SQLiteParameter paramImage = new SQLiteParameter("@param33", DbType.Object);
                    paramImage.Value = peImage.EditValue;
                    ins_com.Parameters.Add(paramImage);

                    ins_com.Parameters.Add("@param34", DbType.Int64).SourceColumn = "NominalVoltageNeitral";
                    ins_com.Parameters.Add("@param35", DbType.Int64).SourceColumn = "RPNTypeID";
                    ins_com.Parameters.Add("@param36", DbType.Int64).SourceColumn = "RPNKind";
                    ins_com.Parameters.Add("@param37", DbType.String).SourceColumn = "RPNNumber";
                    ins_com.Parameters.Add("@param38", DbType.String).SourceColumn = "RPNNumber2";
                    ins_com.Parameters.Add("@param39", DbType.String).SourceColumn = "RPNNumber3";
                    ins_com.Parameters.Add("@param40", DbType.Int64).SourceColumn = "EquipmentClassID";
                    ins_com.Parameters.Add("@param41", DbType.Int64).SourceColumn = "NominalCurrent";
                    ins_com.Parameters.Add("@param42", DbType.Int64).SourceColumn = "SwitchDriveTypeID";

                    this.qEquipmentRecordTableAdapter.Adapter.InsertCommand = ins_com;
                }

                this.qEquipmentRecordTableAdapter.Adapter.Update(dataSetQuery2.QEquipmentRecord);

                SQLiteConnection connection_ = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                connection_.Open();

                // удаляем инфу о вводах
                if (strInputIDs != "")
                {
                    SQLiteCommand com_ = new SQLiteCommand(connection_);
                    com_.CommandText = "DELETE FROM Inputs WHERE InputID IN (" + strInputIDs + ")";
                    com_.ExecuteNonQuery();
                }

                if (m_id <= 0)
                {
                    SQLiteCommand com_ = new SQLiteCommand(connection_);
                    com_.CommandText = "select seq from sqlite_sequence where name = 'Equipments'";
                    com_.CommandType = CommandType.Text;
                    SQLiteDataReader dr_ = com_.ExecuteReader();

                    while (dr_.Read())
                    {
                        m_id = Convert.ToInt64(dr_["seq"]);
                    }
                    dr_.Close();
                }

                connection_.Close();

                m_bChangeData = false;

                return true;
            }
            catch (SQLiteException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DBConcurrencyException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool VerifyDates(long CreateYear, string strUseBeginYearFieldName)
        {
            long UseBeginYear = -1;
            DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
            object val = drv.Row[strUseBeginYearFieldName];
            if (val == null || val == DBNull.Value) return true;
                
            UseBeginYear = Convert.ToInt64(val);
            if (UseBeginYear < CreateYear) return false;

            return true;
        }

        private bool VerifyDates(string strCreateYearFieldName, long UseBeginYear)
        {
            long CreateYear = -1;
            DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
            object val = drv.Row[strCreateYearFieldName];
            if (val == null || val == DBNull.Value) return true;

            CreateYear = Convert.ToInt64(val);
            if (UseBeginYear < CreateYear) return false;

            return true;
        }

        private void GridVertical_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            DevExpress.XtraVerticalGrid.Rows.BaseRow  row = GridVertical.FocusedRow;
            /*
            rCreateYear
            rUseBeginYear
            rProjectLifeTime
            rActualLifeTime
            rLastWorkoverYear
            rLastTechnicalServiceYear
            rNextTechnicalServiceYear
            rTechnicalServiceCount*/

            if (row.Name == "rNominalPower" &&
                e.Value != null && e.Value != DBNull.Value && e.Value.ToString() != "")
            {
                if (Convert.ToDecimal(e.Value) < 0 || Convert.ToDecimal(e.Value) > 1000)
                {
                    e.Valid = false;
                    e.ErrorText = "Значение номинальной мощности должно находиться между значениями 0 и 1000 МВА";
                    return;
                }
            }

            if (row.Name == "rNominalCurrent" &&
                e.Value != null && e.Value != DBNull.Value && e.Value.ToString() != "")
            {
                if (Convert.ToInt32(e.Value) < 0 || Convert.ToInt32(e.Value) > 10000)
                {
                    e.Valid = false;
                    e.ErrorText = "Значение номинального тока должно находиться между значениями 0 и 10000 А";
                    return;
                }
            }

            if ((row.Name == "rCreateYear" || row.Name == "rUseBeginYear" || row.Name == "rLastWorkoverYear" ||
                row.Name == "rLastTechnicalServiceYear" || row.Name == "rNextTechnicalServiceYear") &&
                e.Value != null && e.Value != DBNull.Value && e.Value.ToString() != "")
            {
                if (Convert.ToInt32(e.Value) <= 1900 || Convert.ToInt32(e.Value) >= 2100)
                {
                    e.Valid = false;
                    e.ErrorText = "Значение года должно находиться между значениями 1900 и 2100";
                    return;
                }
            }

            foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
            {
                if ((row.Name == "rInputCreateYear" + p.Key || row.Name == "rInputUseBeginYear" + p.Key) &&
                    e.Value != null && e.Value != DBNull.Value && e.Value.ToString() != "")
                {
                    if (Convert.ToInt32(e.Value) <= 1900 || Convert.ToInt32(e.Value) >= 2100)
                    {
                        e.Valid = false;
                        e.ErrorText = "Значение года должно находиться между значениями 1900 и 2100";
                        return;
                    }
                }
            }

            if ((row.Name == "rProjectLifeTime" || row.Name == "rActualLifeTime" || row.Name == "rTechnicalServiceCount") && 
                e.Value != null && e.Value != DBNull.Value && e.Value.ToString() != "" && Convert.ToInt32(e.Value) < 0)
            {
                e.Valid = false;
                e.ErrorText = "Значение не должно быть отрицательным";
                return;
            }

            if (e.Value != null && e.Value != DBNull.Value && e.Value.ToString() != "")
            {
                string strError = "";
                string strCreateYearFieldName = "";
                string strUseBeginYearFieldName = "";
                if (row.Name == "rCreateYear")
                {
                    strUseBeginYearFieldName = "UseBeginYear";
                    strError = "Год изготовления оборудования не должен превышать год ввода в эксплуатацию.";
                }
                if (row.Name == "rUseBeginYear")
                {
                    strCreateYearFieldName = "CreateYear";
                    strError = "Год изготовления оборудования не должен превышать год ввода в эксплуатацию.";
                }
                foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                {
                    if (row.Name == "rInputCreateYear" + p.Key)
                    {
                        strUseBeginYearFieldName = "InputUseBeginYear" + p.Key;
                        strError = "Год изготовления для одного из вводов не должен превышать год ввода в эксплуатацию.";
                        break;
                    }

                    if (row.Name == "rInputUseBeginYear" + p.Key)
                    {
                        strCreateYearFieldName = "InputCreateYear" + p.Key;
                        strError = "Год изготовления для одного из вводов не должен превышать год ввода в эксплуатацию.";
                        break;
                    }
                }

                if (strCreateYearFieldName == "" && strUseBeginYearFieldName != "")
                {
                    if (!VerifyDates(Convert.ToInt32(e.Value), strUseBeginYearFieldName))
                    {
                        e.Valid = false;
                        e.ErrorText = strError;
                    }

                    return;

                }
                if (strCreateYearFieldName != "" && strUseBeginYearFieldName == "")
                {
                    if (!VerifyDates(strCreateYearFieldName, Convert.ToInt32(e.Value)))
                    {
                        e.Valid = false;
                        e.ErrorText = strError;
                    }

                    return;
                }
            }

            if (e.Value != null && e.Value.ToString() == "") e.Value = DBNull.Value;
        }

        private void repManufacturer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                string FieldName = GridVertical.FocusedRow.Properties.FieldName;

                long EquipmentKindID = -1;
                if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
                {
                    DataRowView drv = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                    EquipmentKindID = Convert.ToInt64(drv.Row["EquipmentKindID"]);//Convert.ToInt64(cbEquipmentKind.EditValue);
                }

                if (EquipmentKindID <= 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо выбрать категорию оборудования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ManufacturerForm f = new ManufacturerForm();
                f.m_bCanSelect = true;
                f.m_EquipmentKindID = EquipmentKindID;
                DialogResult res = f.ShowDialog(this);

                this.qManufacturersByKindTableAdapter.Fill(this.dataSetQuery.QManufacturersByKind, EquipmentKindID);
                if (this.dataSetQuery.QManufacturersByKind.Rows.Count < 7)
                    this.repManufacturer.DropDownRows = this.dataSetQuery.QManufacturersByKind.Rows.Count;
                else
                    this.repManufacturer.DropDownRows = 7;

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    GridVertical.BeginUpdate();
                    DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
                    drv.Row[FieldName] = f.m_SelectID;
                    GridVertical.EndUpdate();
                    //GridVertical.Refresh();
                }
            }
        }

        private void repEquipmentType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                /*DataRowView dgv = (DataRowView)(this.qEquipmentRecordBindingSource.Current);

                if (dgv.Row["EquipmentKindID"] == DBNull.Value)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо выбрать категорию оборудования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long EquipmentKindID = Convert.ToInt64(dgv.Row["EquipmentKindID"]);*/

                long EquipmentKindID = -1;
                if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
                {
                    DataRowView drv = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                    EquipmentKindID = Convert.ToInt64(drv.Row["EquipmentKindID"]);//Convert.ToInt64(cbEquipmentKind.EditValue);
                }

                if (EquipmentKindID <= 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо выбрать категорию оборудования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EquipmentTypeForm f = new EquipmentTypeForm();
                f.m_bCanSelect = true;
                f.m_EquipmentKindID = EquipmentKindID;
                DialogResult res = f.ShowDialog(this);

                this.qEquipmentTypesByKindTableAdapter.Fill(this.dataSetQuery.QEquipmentTypesByKind, EquipmentKindID);
                if (this.dataSetQuery.QEquipmentTypesByKind.Rows.Count < 7)
                    this.repEquipmentType.DropDownRows = this.dataSetQuery.QEquipmentTypesByKind.Rows.Count;
                else
                    this.repEquipmentType.DropDownRows = 7;

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    GridVertical.BeginUpdate();
                    DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
                    drv.Row["EquipmentTypeID"] = f.m_SelectID;
                    GridVertical.EndUpdate();
                    //GridVertical.Refresh();
                }
            }
        }       

        private void RefreshSubject()
        {
            GridVertical.BeginUpdate();
            DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
            object val = drv.Row["SubjectID"];
            long iSubjectID = -1;
            if (val != null && val != DBNull.Value)
            {
                iSubjectID = Convert.ToInt64(val);
            }

            drv.Row["BranchID"] = DBNull.Value;
            drv.Row["SubstationID"] = DBNull.Value;

            this.qBranchesBySubjectTableAdapter.Fill(this.dataSetQuery.QBranchesBySubject, iSubjectID);
            this.qSubstationsByBranchTableAdapter.Fill(this.dataSetQuery.QSubstationsByBranch, -1);

            if (this.dataSetQuery.QSubstationsByBranch.Rows.Count < 7)
                this.repSubstation.DropDownRows = this.dataSetQuery.QSubstationsByBranch.Rows.Count;
            else
                this.repSubstation.DropDownRows = 7;

            if (this.dataSetQuery.QBranchesBySubject.Rows.Count < 7)
                this.repBranch.DropDownRows = this.dataSetQuery.QBranchesBySubject.Rows.Count;
            else
                this.repBranch.DropDownRows = 7;

            GridVertical.EndUpdate();
        }

        private void RefreshBranch()
        {
            GridVertical.BeginUpdate();
            DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
            object val = drv.Row["BranchID"];
            long iBranchID = -1;
            if (val != null && val != DBNull.Value)
            {
                iBranchID = Convert.ToInt64(val);
            }

            drv.Row["SubstationID"] = DBNull.Value;

            this.qSubstationsByBranchTableAdapter.Fill(this.dataSetQuery.QSubstationsByBranch, iBranchID);

            if (this.dataSetQuery.QSubstationsByBranch.Rows.Count < 7)
                this.repSubstation.DropDownRows = this.dataSetQuery.QSubstationsByBranch.Rows.Count;
            else
                this.repSubstation.DropDownRows = 7;

            GridVertical.EndUpdate();
        }

        private void vGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            try
            {
                if (m_bDataLoadEnd) m_bChangeData = true;

                if (e.Row.Name == "rSubject")
                {
                    RefreshSubject();
                }

                if (e.Row.Name == "rBranch")
                {
                    RefreshBranch();
                }

                if (e.Row.Name == "rConstructionType" || e.Row.Name == "rNominalVoltageMiddle")
                {
                    UpdateInputVoltageCategory();
                }

                if (e.Row.Name == "rRPNCnt")
                {
                    UpdateRPNCnt();
                }

                if (e.Row.Name == "rNominalVoltageNeitral")
                {
                    UpdateNominalVoltageNeutral();
                }

                //GridVertical.Refresh();
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

        private void repBranch_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                BranchForm f = new BranchForm();
                f.m_bCanSelect = true;
                DialogResult res = f.ShowDialog(this);

                this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);
                if (this.dataSetQuery.QSubjects.Rows.Count < 7)
                    this.repSubject.DropDownRows = this.dataSetQuery.QSubjects.Rows.Count;
                else
                    this.repSubject.DropDownRows = 7;

                this.qBranchesBySubjectTableAdapter.Fill(this.dataSetQuery.QBranchesBySubject, f.m_SubjectID);
                if (this.dataSetQuery.QBranchesBySubject.Rows.Count < 7)
                    this.repBranch.DropDownRows = this.dataSetQuery.QBranchesBySubject.Rows.Count;
                else
                    this.repBranch.DropDownRows = 7;

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    GridVertical.BeginUpdate();
                    DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
                    drv.Row["SubjectID"] = f.m_SubjectID;
                    RefreshSubject();
                    drv.Row["BranchID"] = f.m_SelectID;
                    RefreshBranch();
                    GridVertical.EndUpdate();
                }
            }
        }

        private void repSubject_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                SubjectForm f = new SubjectForm();
                f.m_bCanSelect = true;
                DialogResult res = f.ShowDialog(this);
                
                this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);
                if (this.dataSetQuery.QSubjects.Rows.Count < 7)
                    this.repSubject.DropDownRows = this.dataSetQuery.QSubjects.Rows.Count;
                else
                    this.repSubject.DropDownRows = 7;

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    GridVertical.BeginUpdate();
                    DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
                    drv.Row["SubjectID"] = f.m_SelectID;
                    RefreshSubject();
                    GridVertical.EndUpdate();
                }
            }
        }

        private void repSubstation_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                SubstationForm f = new SubstationForm();
                f.m_bCanSelect = true;
                DialogResult res = f.ShowDialog(this);

                this.qSubjectsTableAdapter.Fill(this.dataSetQuery.QSubjects);
                if (this.dataSetQuery.QSubjects.Rows.Count < 7)
                    this.repSubject.DropDownRows = this.dataSetQuery.QSubjects.Rows.Count;
                else
                    this.repSubject.DropDownRows = 7;

                this.qBranchesBySubjectTableAdapter.Fill(this.dataSetQuery.QBranchesBySubject, f.m_SubjectID);
                if (this.dataSetQuery.QBranchesBySubject.Rows.Count < 7)
                    this.repBranch.DropDownRows = this.dataSetQuery.QBranchesBySubject.Rows.Count;
                else
                    this.repBranch.DropDownRows = 7;

                this.qSubstationsByBranchTableAdapter.Fill(this.dataSetQuery.QSubstationsByBranch, f.m_BranchID);
                if (this.dataSetQuery.QSubstationsByBranch.Rows.Count < 7)
                    this.repSubstation.DropDownRows = this.dataSetQuery.QSubstationsByBranch.Rows.Count;
                else
                    this.repSubstation.DropDownRows = 7;

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    GridVertical.BeginUpdate();
                    DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
                    drv.Row["SubjectID"] = f.m_SubjectID;
                    RefreshSubject();
                    drv.Row["BranchID"] = f.m_BranchID;
                    RefreshBranch();
                    drv.Row["SubstationID"] = f.m_SelectID;
                    GridVertical.EndUpdate();
                }
            }
        }

        private void UpdateRPNCnt(bool bUpdateDB = true)
        {
            long EquipmentKindID = -1;
            if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
            {
                DataRowView drv_ = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                EquipmentKindID = Convert.ToInt64(drv_.Row["EquipmentKindID"]);
            }

            // только для трансформатора
            if ((Equipment.EquipmentKind)EquipmentKindID != Equipment.EquipmentKind.Transformer) return;

            GridVertical.BeginUpdate();
            DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
            object val = drv.Row["RPNCnt"];
            if (val == null || val == DBNull.Value || Convert.ToInt64(val) == 0)
            {
                if (bUpdateDB)
                {
                    drv.Row["RPNVoltage"] = DBNull.Value;
                    drv.Row["RPNTypeID"] = DBNull.Value;
                    drv.Row["RPNKind"] = DBNull.Value;
                    drv.Row["RPNNumber"] = DBNull.Value;
                    drv.Row["RPNNumber2"] = DBNull.Value;
                    drv.Row["RPNNumber3"] = DBNull.Value;
                }
                rRPNVoltage.Visible = false;
                rRPNVoltage.OptionsRow.AllowFocus = false;

                rRPNType.Visible = false;
                rRPNType.OptionsRow.AllowFocus = false;

                rRPNKind.Visible = false;
                rRPNKind.OptionsRow.AllowFocus = false;

                rRPNNumber.Visible = false;
                rRPNNumber.OptionsRow.AllowFocus = false;

                rRPNNumber2.Visible = false;
                rRPNNumber2.OptionsRow.AllowFocus = false;

                rRPNNumber3.Visible = false;
                rRPNNumber3.OptionsRow.AllowFocus = false;
            }
            else
            {
                rRPNVoltage.Visible = true;
                rRPNVoltage.OptionsRow.AllowFocus = true;

                rRPNType.Visible = true;
                rRPNType.OptionsRow.AllowFocus = true;

                rRPNKind.Visible = true;
                rRPNKind.OptionsRow.AllowFocus = true;

                rRPNNumber.Visible = true;
                rRPNNumber.OptionsRow.AllowFocus = true;

                if (Convert.ToInt64(val) == 1)
                {
                    rRPNNumber.Properties.Caption = "Заводской номер";

                    rRPNNumber2.Visible = false;
                    rRPNNumber2.OptionsRow.AllowFocus = false;

                    rRPNNumber3.Visible = false;
                    rRPNNumber3.OptionsRow.AllowFocus = false;
                }
                else
                {
                    rRPNNumber.Properties.Caption = "Заводской номер (фаза A)";

                    rRPNNumber2.Visible = true;
                    rRPNNumber2.OptionsRow.AllowFocus = true;

                    rRPNNumber3.Visible = true;
                    rRPNNumber3.OptionsRow.AllowFocus = true;
                }
            }
            GridVertical.EndUpdate();
        }

        private void GridVertical_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            if (!e.Row.OptionsRow.AllowFocus)
            {
                ((DevExpress.XtraVerticalGrid.Rows.BaseRow)e.Row).Visible = false;
                GridVertical.FocusedRow = e.OldRow;
            }
        }

        private void btnClearPicture_Click(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;
            peImage.EditValue = null;
        }

        private void peImage_DoubleClick(object sender, EventArgs e)
        {
            ChangePicture();
        }

        private void btnChangePicture_Click(object sender, EventArgs e)
        {
            ChangePicture();
        }

        private void ChangePicture()
        {
            ImageForm f = new ImageForm();
            f.m_img = peImage.EditValue;
            DialogResult res = f.ShowDialog(this);
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                m_bChangeData = true;
                peImage.EditValue = f.m_img;
            }
        }

        private void PassportDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            teEquipmentName.Focus();
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

        private void teEquipmentName_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;
        }

        private void teEquipmentNumber_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;
        }

        private void repCoolingSystemType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void repLookUp_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) && ((LookUpEdit)sender).Text == "")
            {
                if (!((LookUpEdit)sender).Properties.ReadOnly)
                {
                    ((LookUpEdit)sender).ClosePopup();
                    ((LookUpEdit)sender).EditValue = null;
                }
            }
        }

        private void bNext_Click(object sender, EventArgs e)
        {
            if (m_bShowContinueMsg)
            {
                m_bContinueNext = true;

                if (m_bChangeData)
                {
                    if (MyLocalizer.XtraMessageBoxShow("Сохранить изменения?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!SaveData())
                        {
                            return;
                        }
                    }
                }

                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();

                /*NextPrevForm form = new NextPrevForm();
                form.m_strPrev = "";
                form.m_strNext = "Данные визуального обследования";

                if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    if (form.m_bNext) m_bContinueNext = true;
                    else m_bContinuePrev = true;
                }*/
            }            
        }

        private void PassportDataForm_SizeChanged(object sender, EventArgs e)
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
                DevExpress.XtraEditors.XtraForm f = (DevExpress.XtraEditors.XtraForm)this.Owner;
                while (f.Owner != null)
                {
                    if (!f.Visible) f.Show();
                    f = (DevExpress.XtraEditors.XtraForm)f.Owner;
                }

                if (!f.Visible) f.Show();
                //this.ShowInTaskbar = false;
            }
        }

        private void GridVertical_CustomDrawRowValueCell(object sender, DevExpress.XtraVerticalGrid.Events.CustomDrawRowValueCellEventArgs e)
        {
            string str = e.CellText;
            Rectangle rect = e.Bounds;
            rect.X += 3;
            rect.Width -= 6;

            if (e.Row.Appearance.Options.UseBackColor && !e.Row.HasChildren)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.White) /*.FromArgb(180, 180, 180))*/, e.Bounds);
            }

            if (str == "")
            {
                e.Graphics.DrawString("данные отсутствуют", e.Appearance.Font, new SolidBrush(Color.Gray), rect, e.Appearance.GetStringFormat());
            }
            else
                e.Graphics.DrawString(str, e.Appearance.Font, new SolidBrush(Color.Black), rect, e.Appearance.GetStringFormat());

            e.Handled = true;
        }

        private void repRPNType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                /*DataRowView dgv = (DataRowView)(this.qEquipmentRecordBindingSource.Current);

                if (dgv.Row["EquipmentKindID"] == DBNull.Value)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо выбрать категорию оборудования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long EquipmentKindID = Convert.ToInt64(dgv.Row["EquipmentKindID"]);*/

                long EquipmentKindID = -1;
                if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
                {
                    DataRowView drv = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                    EquipmentKindID = Convert.ToInt64(drv.Row["EquipmentKindID"]);//Convert.ToInt64(cbEquipmentKind.EditValue);
                }
                if (EquipmentKindID <= 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо выбрать категорию оборудования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                RPNTypeForm f = new RPNTypeForm();
                f.m_bCanSelect = true;
                f.m_EquipmentKindID = EquipmentKindID;
                DialogResult res = f.ShowDialog(this);

                this.qRPNTypesByKindTableAdapter.Fill(this.dataSetQuery.QRPNTypesByKind, EquipmentKindID);
                if (this.dataSetQuery.QRPNTypesByKind.Rows.Count < 7)
                    this.repRPNType.DropDownRows = this.dataSetQuery.QRPNTypesByKind.Rows.Count;
                else
                    this.repRPNType.DropDownRows = 7;

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    GridVertical.BeginUpdate();
                    DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
                    drv.Row["RPNTypeID"] = f.m_SelectID;
                    GridVertical.EndUpdate();
                    //GridVertical.Refresh();
                }
            }
        }

        private void repInputType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                string FieldName = GridVertical.FocusedRow.Properties.FieldName;

                /*DataRowView dgv = (DataRowView)(this.qEquipmentRecordBindingSource.Current);

                if (dgv.Row["EquipmentKindID"] == DBNull.Value)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо выбрать категорию оборудования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long EquipmentKindID = Convert.ToInt64(dgv.Row["EquipmentKindID"]);*/
                long EquipmentKindID = -1;
                if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
                {
                    DataRowView drv = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                    EquipmentKindID = Convert.ToInt64(drv.Row["EquipmentKindID"]);//Convert.ToInt64(cbEquipmentKind.EditValue);
                }
                if (EquipmentKindID <= 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо выбрать категорию оборудования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                InputVoltageTypeForm f = new InputVoltageTypeForm();
                f.m_bCanSelect = true;
                f.m_EquipmentKindID = EquipmentKindID;
                DialogResult res = f.ShowDialog(this);

                this.qInputVoltageTypesByKindTableAdapter.Fill(this.dataSetQuery.QInputVoltageTypesByKind, EquipmentKindID);
                if (this.dataSetQuery.QInputVoltageTypesByKind.Rows.Count < 7)
                    this.repInputType.DropDownRows = this.dataSetQuery.QInputVoltageTypesByKind.Rows.Count;
                else
                    this.repInputType.DropDownRows = 7;

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    GridVertical.BeginUpdate();
                    DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
                    drv.Row[FieldName] = f.m_SelectID;
                    GridVertical.EndUpdate();
                    //GridVertical.Refresh();
                }
            }
        }

        private void repManufacturerInput_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                string FieldName = GridVertical.FocusedRow.Properties.FieldName;

                long EquipmentKindID = -1;
                if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
                {
                    DataRowView drv = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                    EquipmentKindID = Convert.ToInt64(drv.Row["EquipmentKindID"]);//Convert.ToInt64(cbEquipmentKind.EditValue);
                }

                if (EquipmentKindID <= 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо выбрать категорию оборудования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ManufacturerInputForm f = new ManufacturerInputForm();
                f.m_bCanSelect = true;
                f.m_EquipmentKindID = EquipmentKindID;
                DialogResult res = f.ShowDialog(this);

                this.qManufacturersInputsByKindTableAdapter.Fill(this.dataSetQuery.QManufacturersInputsByKind, EquipmentKindID);
                if (this.dataSetQuery.QManufacturersInputsByKind.Rows.Count < 7)
                    this.repManufacturerInput.DropDownRows = this.dataSetQuery.QManufacturersInputsByKind.Rows.Count;
                else
                    this.repManufacturerInput.DropDownRows = 7;

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    GridVertical.BeginUpdate();
                    DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
                    drv.Row[FieldName] = f.m_SelectID;
                    GridVertical.EndUpdate();
                    //GridVertical.Refresh();
                }
            }
        }

        private void ShowHideParameters(long EquipmentKindID)
        {
            /*long EquipmentKindID = -1;
            if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
            {
                DataRowView drv_ = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                EquipmentKindID = Convert.ToInt64(drv_.Row["EquipmentKindID"]);
            }*/

            //m_bDataLoad = true;

            switch ((Equipment.EquipmentKind)EquipmentKindID)
            {
                case Equipment.EquipmentKind.Transformer:

                    GridVertical.BeginUpdate();
                    catRPN.Visible = true;
                    catVoltage.Visible = true;

                    if (categoryRow3.ChildRows.IndexOf(rNominalVoltageHigh) >= 0)
                        categoryRow3.ChildRows.Remove(rNominalVoltageHigh);
                    if (catVoltage.ChildRows.IndexOf(rNominalVoltageHigh) < 0)
                        catVoltage.ChildRows.Add(rNominalVoltageHigh);
                    rNominalVoltageHigh.Properties.Caption = "Класс напряжения ВН, кВ";

                    foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                    {
                        if (p.Key == "HighA") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод ВН фаза А";
                        if (p.Key == "HighB") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод ВН фаза В";
                        if (p.Key == "HighC") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод ВН фаза С";
                        if (p.Key == "MiddleA") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод СН фаза А";
                        if (p.Key == "MiddleB") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод СН фаза В";
                        if (p.Key == "MiddleC") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод СН фаза С";
                        if (p.Key == "Neutral") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод нейтраль";

                        GridVertical.Rows["rInputName" + p.Key].OptionsRow.AllowFocus = false;
                        GridVertical.Rows["rInputName" + p.Key].Visible = false;
                    }

                    rEquipmentType.Properties.Caption = "Тип (марка) трансформатора";

                    rConstructionType.OptionsRow.AllowFocus = true;
                    rConstructionType.Visible = true;

                    rProtectionOilType.OptionsRow.AllowFocus = true;
                    rProtectionOilType.Visible = true;

                    rCoolingSystemType.OptionsRow.AllowFocus = true;
                    rCoolingSystemType.Visible = true;

                    rNominalPower.OptionsRow.AllowFocus = true;
                    rNominalPower.Visible = true;

                    rNominalCurrent.OptionsRow.AllowFocus = false;
                    rNominalCurrent.Visible = false;

                    rSwitchDriveType.OptionsRow.AllowFocus = false;
                    rSwitchDriveType.Visible = false;

                    GridVertical.EndUpdate();

                    UpdateInputVoltageCategory();
                    UpdateRPNCnt();
                    UpdateNominalVoltageNeutral();
                    break;
                case Equipment.EquipmentKind.AirSwitch:
                    GridVertical.BeginUpdate();
                    catRPN.Visible = false;
                    catVoltage.Visible = false;

                    if (categoryRow3.ChildRows.IndexOf(rNominalVoltageHigh) < 0)
                        categoryRow3.ChildRows.Add(rNominalVoltageHigh);
                    if (catVoltage.ChildRows.IndexOf(rNominalVoltageHigh) >= 0)
                        catVoltage.ChildRows.Remove(rNominalVoltageHigh);
                    rNominalVoltageHigh.Properties.Caption = "Класс напряжения, кВ";

                    foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                    {
                        for (int i = 0; i < p.Value.Count; i++)
                        {
                            GridVertical.Rows["r" + p.Value[i] + p.Key].OptionsRow.AllowFocus = false;
                        }

                        GridVertical.Rows["catInput" + p.Key].Visible = false;
                    }

                    rEquipmentType.Properties.Caption = "Тип (марка) выключателя";

                    rConstructionType.OptionsRow.AllowFocus = false;
                    rConstructionType.Visible = false;

                    rProtectionOilType.OptionsRow.AllowFocus = false;
                    rProtectionOilType.Visible = false;

                    rCoolingSystemType.OptionsRow.AllowFocus = false;
                    rCoolingSystemType.Visible = false;

                    rNominalPower.OptionsRow.AllowFocus = false;
                    rNominalPower.Visible = false;

                    rNominalCurrent.OptionsRow.AllowFocus = true;
                    rNominalCurrent.Visible = true;

                    rSwitchDriveType.OptionsRow.AllowFocus = true;
                    rSwitchDriveType.Visible = true;

                    GridVertical.EndUpdate();
                    break;
                case Equipment.EquipmentKind.OilLessSwitch:
                    GridVertical.BeginUpdate();
                    catRPN.Visible = false;
                    catVoltage.Visible = false;

                    if (categoryRow3.ChildRows.IndexOf(rNominalVoltageHigh) < 0)
                        categoryRow3.ChildRows.Add(rNominalVoltageHigh);
                    if (catVoltage.ChildRows.IndexOf(rNominalVoltageHigh) >= 0)
                        catVoltage.ChildRows.Remove(rNominalVoltageHigh);
                    rNominalVoltageHigh.Properties.Caption = "Класс напряжения, кВ";

                    foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                    {
                        for (int i = 0; i < p.Value.Count; i++)
                        {
                            GridVertical.Rows["r" + p.Value[i] + p.Key].OptionsRow.AllowFocus = false;
                        }

                        GridVertical.Rows["catInput" + p.Key].Visible = false;
                    }

                    rEquipmentType.Properties.Caption = "Тип (марка) выключателя";

                    rConstructionType.OptionsRow.AllowFocus = false;
                    rConstructionType.Visible = false;

                    rProtectionOilType.OptionsRow.AllowFocus = false;
                    rProtectionOilType.Visible = false;

                    rCoolingSystemType.OptionsRow.AllowFocus = false;
                    rCoolingSystemType.Visible = false;

                    rNominalPower.OptionsRow.AllowFocus = false;
                    rNominalPower.Visible = false;

                    rNominalCurrent.OptionsRow.AllowFocus = true;
                    rNominalCurrent.Visible = true;

                    rSwitchDriveType.OptionsRow.AllowFocus = true;
                    rSwitchDriveType.Visible = true;

                    GridVertical.EndUpdate();
                    break;
                case Equipment.EquipmentKind.OilTankSwitch:
                    GridVertical.BeginUpdate();
                    catRPN.Visible = false;
                    catVoltage.Visible = false;

                    if (categoryRow3.ChildRows.IndexOf(rNominalVoltageHigh) < 0)
                        categoryRow3.ChildRows.Add(rNominalVoltageHigh);
                    if (catVoltage.ChildRows.IndexOf(rNominalVoltageHigh) >= 0)
                        catVoltage.ChildRows.Remove(rNominalVoltageHigh);
                    rNominalVoltageHigh.Properties.Caption = "Класс напряжения, кВ";

                    foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                    {
                        if (p.Key != "Neutral")
                        {
                            for (int i = 0; i < p.Value.Count; i++)
                            {
                                GridVertical.Rows["r" + p.Value[i] + p.Key].OptionsRow.AllowFocus = true;
                            }

                            GridVertical.Rows["catInput" + p.Key].Visible = true;

                            if (p.Key == "HighA") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод полюс А сторона 1";
                            if (p.Key == "HighB") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод полюс А сторона 2";
                            if (p.Key == "HighC") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод полюс В сторона 1";
                            if (p.Key == "MiddleA") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод полюс В сторона 2";
                            if (p.Key == "MiddleB") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод полюс С сторона 1";
                            if (p.Key == "MiddleC") GridVertical.Rows["catInput" + p.Key].Properties.Caption = "Ввод полюс С сторона 2";

                            GridVertical.Rows["rInputName" + p.Key].OptionsRow.AllowFocus = true;
                            GridVertical.Rows["rInputName" + p.Key].Visible = true;
                        }
                        else
                        {
                            for (int i = 0; i < p.Value.Count; i++)
                            {
                                GridVertical.Rows["r" + p.Value[i] + p.Key].OptionsRow.AllowFocus = false;
                            }

                            GridVertical.Rows["catInput" + p.Key].Visible = false;
                        }
                    }

                    rEquipmentType.Properties.Caption = "Тип (марка) выключателя";

                    rConstructionType.OptionsRow.AllowFocus = false;
                    rConstructionType.Visible = false;

                    rProtectionOilType.OptionsRow.AllowFocus = false;
                    rProtectionOilType.Visible = false;

                    rCoolingSystemType.OptionsRow.AllowFocus = false;
                    rCoolingSystemType.Visible = false;

                    rNominalPower.OptionsRow.AllowFocus = false;
                    rNominalPower.Visible = false;

                    rNominalCurrent.OptionsRow.AllowFocus = true;
                    rNominalCurrent.Visible = true;

                    rSwitchDriveType.OptionsRow.AllowFocus = true;
                    rSwitchDriveType.Visible = true;

                    GridVertical.EndUpdate();
                    break;
            }

            //m_bDataLoad = false;
        }

        private void cbEquipmentClass_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;

            if (!m_bDataLoad)
            {
                long EquipmentKindID = -1;
                if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
                {
                    DataRowView drv = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                    EquipmentKindID = Convert.ToInt64(drv.Row["EquipmentKindID"]);//Convert.ToInt64(cbEquipmentKind.EditValue);
                }
                if (EquipmentKindID != m_OldEquipmentKindID)
                {
                    this.qEquipmentTypesByKindTableAdapter.Fill(this.dataSetQuery.QEquipmentTypesByKind, EquipmentKindID);
                    //this.qNominalPowersByKindTableAdapter.Fill(this.dataSetQuery.QNominalPowersByKind, EquipmentKindID);
                    //this.qNominalCurrentsByKindTableAdapter.Fill(this.dataSetQuery.QNominalCurrentsByKind, EquipmentKindID);
                    this.qRPNTypesByKindTableAdapter.Fill(this.dataSetQuery.QRPNTypesByKind, EquipmentKindID);
                    this.qInputVoltageTypesByKindTableAdapter.Fill(this.dataSetQuery.QInputVoltageTypesByKind, EquipmentKindID);
                    this.qSwitchDriveTypesByKindTableAdapter.Fill(this.dataSetQuery.QSwitchDriveTypesByKind, EquipmentKindID);
                    this.qManufacturersByKindTableAdapter.Fill(this.dataSetQuery.QManufacturersByKind, EquipmentKindID);
                    this.qManufacturersInputsByKindTableAdapter.Fill(this.dataSetQuery.QManufacturersInputsByKind, EquipmentKindID);

                    //this.qEquipmentClassesByKindTableAdapter.Fill(this.dataSetQuery.QEquipmentClassesByKind, EquipmentKindID);

                    if (this.dataSetQuery.QEquipmentTypesByKind.Rows.Count < 7)
                        this.repEquipmentType.DropDownRows = this.dataSetQuery.QEquipmentTypesByKind.Rows.Count;
                    else
                        this.repEquipmentType.DropDownRows = 7;

                    /*if (this.dataSetQuery.QNominalPowersByKind.Rows.Count < 7)
                        this.repNominalPower.DropDownRows = this.dataSetQuery.QNominalPowersByKind.Rows.Count;
                    else
                        this.repNominalPower.DropDownRows = 7;

                    if (this.dataSetQuery.QNominalCurrentsByKind.Rows.Count < 7)
                        this.repNominalCurrent.DropDownRows = this.dataSetQuery.QNominalCurrentsByKind.Rows.Count;
                    else
                        this.repNominalCurrent.DropDownRows = 7;*/

                    if (this.dataSetQuery.QRPNTypesByKind.Rows.Count < 7)
                        this.repRPNType.DropDownRows = this.dataSetQuery.QRPNTypesByKind.Rows.Count;
                    else
                        this.repRPNType.DropDownRows = 7;

                    if (this.dataSetQuery.QInputVoltageTypesByKind.Rows.Count < 7)
                        this.repInputType.DropDownRows = this.dataSetQuery.QInputVoltageTypesByKind.Rows.Count;
                    else
                        this.repInputType.DropDownRows = 7;

                    if (this.dataSetQuery.QSwitchDriveTypesByKind.Rows.Count < 7)
                        this.repSwitchDriveType.DropDownRows = this.dataSetQuery.QSwitchDriveTypesByKind.Rows.Count;
                    else
                        this.repSwitchDriveType.DropDownRows = 7;

                    if (this.dataSetQuery.QManufacturersByKind.Rows.Count < 7)
                        this.repManufacturer.DropDownRows = this.dataSetQuery.QManufacturersByKind.Rows.Count;
                    else
                        this.repManufacturer.DropDownRows = 7;

                    if (this.dataSetQuery.QManufacturersInputsByKind.Rows.Count < 7)
                        this.repManufacturerInput.DropDownRows = this.dataSetQuery.QManufacturersInputsByKind.Rows.Count;
                    else
                        this.repManufacturerInput.DropDownRows = 7;

                    m_OldEquipmentKindID = EquipmentKindID;
                }

                //GridVertical.BeginUpdate();

                //DevExpress.XtraVerticalGrid.Rows.BaseRow gridEquipmentTypeRow;
                //gridEquipmentTypeRow = GridVertical.GetRowByFieldName("EquipmentTypeID");//.Rows[0].ChildRows[0];
                //GridVertical.InvalidateRow(gridEquipmentTypeRow);

                //DevExpress.XtraVerticalGrid.Rows.BaseRow gridRPNTypeRow;
                //gridRPNTypeRow = GridVertical.GetRowByFieldName("RPNTypeID");//.Rows[0].ChildRows[0];
                //GridVertical.InvalidateRow(gridRPNTypeRow);

                //DevExpress.XtraVerticalGrid.Rows.BaseRow gridInputVoltageTypeRow;
                //gridInputVoltageTypeRow = GridVertical.GetRowByFieldName("InputVoltageTypeID");//.Rows[0].ChildRows[0];
                //GridVertical.InvalidateRow(gridInputVoltageTypeRow);

                //DevExpress.XtraVerticalGrid.Rows.BaseRow gridCoolingSystemTypeRow;
                //gridCoolingSystemTypeRow = GridVertical.GetRowByFieldName("CoolingSystemTypeID");//.Rows[0].ChildRows[0];
                //GridVertical.InvalidateRow(gridCoolingSystemTypeRow);

                //DevExpress.XtraVerticalGrid.Rows.BaseRow gridNominalPowerRow;
                //gridNominalPowerRow = GridVertical.GetRowByFieldName("NominalPowerID");//.Rows[0].ChildRows[0];
                //GridVertical.InvalidateRow(gridNominalPowerRow);

                //DevExpress.XtraVerticalGrid.Rows.BaseRow gridProtectionOilTypeRow;
                //gridProtectionOilTypeRow = GridVertical.GetRowByFieldName("ProtectionOilTypeID");
                //GridVertical.InvalidateRow(gridProtectionOilTypeRow);

                //GridVertical.EndUpdate();
            }

            //ShowHideParameters();
        }

        private void cbEquipmentClass_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (!m_bDataLoad)
            {
                long EquipmentKindID = m_dictClassIDToKindID[Convert.ToInt64(e.NewValue)];
                m_OldEquipmentKindID = m_dictClassIDToKindID[Convert.ToInt64(e.OldValue)];
                if (EquipmentKindID != m_OldEquipmentKindID)
                {
                    DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
                    GridVertical.BeginUpdate();
                    drv.Row["EquipmentKindID"] = EquipmentKindID;
                    // скидываем все значения, зависящие от EquipmentKindID в Null;
                    drv.Row["EquipmentTypeID"] = DBNull.Value;
                    drv.Row["RPNTypeID"] = DBNull.Value;
                    foreach (KeyValuePair<string, List<string>> p in m_dictInputs)
                    {
                        drv.Row["InputTypeID" + p.Key] = DBNull.Value;
                    }
                    GridVertical.EndUpdate();
                    ShowHideParameters(EquipmentKindID);
                }
            }
        }

        private void repSwitchDriveType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                /*DataRowView dgv = (DataRowView)(this.qEquipmentRecordBindingSource.Current);

                if (dgv.Row["EquipmentKindID"] == DBNull.Value)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо выбрать категорию оборудования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long EquipmentKindID = Convert.ToInt64(dgv.Row["EquipmentKindID"]);*/

                long EquipmentKindID = -1;
                if (cbEquipmentClass.EditValue != null && cbEquipmentClass.EditValue != DBNull.Value)
                {
                    DataRowView drv = (DataRowView)cbEquipmentClass.GetSelectedDataRow();
                    EquipmentKindID = Convert.ToInt64(drv.Row["EquipmentKindID"]);//Convert.ToInt64(cbEquipmentKind.EditValue);
                }

                if (EquipmentKindID <= 0)
                {
                    MyLocalizer.XtraMessageBoxShow("Необходимо выбрать категорию оборудования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SwitchDriveTypeForm f = new SwitchDriveTypeForm();
                f.m_bCanSelect = true;
                f.m_EquipmentKindID = EquipmentKindID;
                DialogResult res = f.ShowDialog(this);

                this.qSwitchDriveTypesByKindTableAdapter.Fill(this.dataSetQuery.QSwitchDriveTypesByKind, EquipmentKindID);
                if (this.dataSetQuery.QSwitchDriveTypesByKind.Rows.Count < 7)
                    this.repSwitchDriveType.DropDownRows = this.dataSetQuery.QSwitchDriveTypesByKind.Rows.Count;
                else
                    this.repSwitchDriveType.DropDownRows = 7;

                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    GridVertical.BeginUpdate();
                    DataRowView drv = (DataRowView)(qEquipmentRecordBindingSource.Current);
                    drv.Row["SwitchDriveTypeID"] = f.m_SelectID;
                    GridVertical.EndUpdate();
                    //GridVertical.Refresh();
                }
            }
        }

        private void peImage_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;
        }

        private void repPower_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                if (e.KeyChar == '.') e.KeyChar = ',';

            if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
                if (e.KeyChar == ',') e.KeyChar = '.';
        }        
    }
}