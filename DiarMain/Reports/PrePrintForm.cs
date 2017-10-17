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
    public partial class PrePrintForm : DevExpress.XtraEditors.XtraForm
    {
        public long m_CheckID;
        public Dictionary<long, List<ReportInfo.Equipment>> m_dictEquipments = new Dictionary<long, List<ReportInfo.Equipment>>();

        class DataSourceModule
        {
            public DataSourceModule(long id, bool bCheck, string strName)
            {
                m_ID = id;
                m_bCheck = bCheck;
                m_strName = strName;
            }

            private long m_ID;
            private bool m_bCheck;
            private string m_strName;

            public long ID
            {
                get { return m_ID; }
                set { m_ID = value; }
            }

            public bool CHECK
            {
                get { return m_bCheck; }
                set { m_bCheck = value; }
            }

            public string NAME
            {
                get { return m_strName; }
                set { m_strName = value; }
            }
        };

        BindingList<DataSourceModule> listModules = new BindingList<DataSourceModule>();

        public PrePrintForm()
        {
            InitializeComponent();
        }

        private void PrePrintForm_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<long, List<ReportInfo.Equipment>> pair in m_dictEquipments)
            {
                long iEquipmentKindID = pair.Key;
                string strEquipmentKindName = "";
                switch ((Equipment.EquipmentKind)pair.Key)
                {
                    case Equipment.EquipmentKind.Transformer:
                        strEquipmentKindName = "Трансформаторы";
                        break;
                    case Equipment.EquipmentKind.AirSwitch:
                        strEquipmentKindName = "Выключатели воздушные";
                        break;
                    case Equipment.EquipmentKind.OilLessSwitch:
                        strEquipmentKindName = "Выключатели маломасляные";
                        break;
                    case Equipment.EquipmentKind.OilTankSwitch:
                        strEquipmentKindName = "Выключатели масляные баковые";
                        break;
                }

                listModules.Add(new DataSourceModule(iEquipmentKindID, true, strEquipmentKindName));
            }

            GridGC.DataSource = listModules;
        }

        private void bActivation_Click(object sender, EventArgs e)
        {
            bool bPrint = false;
            List<Word> listWord = new List<Word>();

            for (int i = 0; i < listModules.Count; i++)
            {
                if (listModules[i].CHECK)
                {
                    WaitingForm wf = new WaitingForm();

                    switch ((Equipment.EquipmentKind)listModules[i].ID)
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

                    wf.m_listEquipments = m_dictEquipments[listModules[i].ID];
                    wf.m_CheckID = m_CheckID;
                    wf.ShowDialog(this);

                    if (wf.m_Word != null) listWord.Add(wf.m_Word);

                    bPrint = true;
                }
            }
            if (!bPrint)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо выбрать хотя бы один вид отчета.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < listWord.Count; i++)
            {
                listWord[i].SetVisible(true);
                listWord[i].DestroyWord();
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
        }
    }
}