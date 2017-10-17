using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace DiarMain
{
    public partial class ParameterLimitAlgForm : DevExpress.XtraEditors.XtraForm
    {
        class DataSourceLimits
        {
            public DataSourceLimits(string strAlgorithm, double limitVal, long? VoltageMin, long? VoltageMax,
                long? YearsMin, long? YearsMax, long? ProtectionOilType)
            {
                m_limitVal = limitVal;
                m_strAlgorithm = strAlgorithm;
                m_VoltageMin = VoltageMin;
                m_VoltageMax = VoltageMax;
                m_YearsMin = YearsMin;
                m_YearsMax = YearsMax;

                m_ProtectionOilType = "";
                if (ProtectionOilType == 1) m_ProtectionOilType = "пленочная защита";
                if (ProtectionOilType == 2) m_ProtectionOilType = "азотная защита";
                if (ProtectionOilType == 3) m_ProtectionOilType = "свободное дыхание (воздухоосушитель)";
            }

            public double? m_limitVal;
            public string m_strAlgorithm;
            public long? m_VoltageMin;
            public long? m_VoltageMax;
            public long? m_YearsMin;
            public long? m_YearsMax;
            public string m_ProtectionOilType;

            public double? LIMIT_VAL
            {
                get { return m_limitVal; }
                set { m_limitVal = value; }
            }

            public string ALGORITHM
            {
                get { return m_strAlgorithm; }
                set { m_strAlgorithm = value; }
            }

            public long? MIN_VOLTAGE_VAL
            {
                get { return m_VoltageMin; }
                set { m_VoltageMin = value; }
            }

            public long? MAX_VOLTAGE_VAL
            {
                get { return m_VoltageMax; }
                set { m_VoltageMax = value; }
            }

            public long? MIN_YEARS_VAL
            {
                get { return m_YearsMin; }
                set { m_YearsMin = value; }
            }

            public long? MAX_YEARS_VAL
            {
                get { return m_YearsMax; }
                set { m_YearsMax = value; }
            }

            public string PROTECTION_OIL_TYPE_VAL
            {
                get { return m_ProtectionOilType; }
                set { m_ProtectionOilType = value; }
            }
        };

        public Dictionary<long, List<InspectionDataForm.Parameter.LimitAlg>> m_dictAlgLimits;
        public Dictionary<long, string> m_dictAlgNames = new Dictionary<long,string>();
        public Inspection.InspectionType m_type;
        BindingList<DataSourceLimits> listAlgLimits = new BindingList<DataSourceLimits>();

        public ParameterLimitAlgForm()
        {
            InitializeComponent();
        }

        private void AddParam(SQLiteCommand com, string name, DbType type, object value)
        {
            SQLiteParameter param = new SQLiteParameter(name, type);
            param.Value = value;
            com.Parameters.Add(param);
        }

        private void ParameterLimitAlgForm_Load(object sender, EventArgs e)
        {
            /*for (int i = 0; i < m_listLimits.Count; i++)
            {
                listData.Add(new DataSourceLimits(m_listLimits[i]));
            }*/

            try
            {
                SQLiteConnection m_con = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);

                m_con.Open();
                SQLiteCommand com = new SQLiteCommand(m_con);
                com.CommandType = CommandType.Text;

                com.CommandText = "Select AlgorithmID, AlgorithmName FROM AlgorithmTypes WHERE InspectionType = @type ORDER BY AlgorithmID";

                com.Parameters.Clear();
                AddParam(com, "@type", DbType.Int64, (long)m_type);

                SQLiteDataReader drAlgorithmType = com.ExecuteReader();
                if (drAlgorithmType.HasRows)
                {
                    while (drAlgorithmType.Read())
                    {
                        long AlgorithmID = Convert.ToInt64(drAlgorithmType["AlgorithmID"]);
                        string strAlgorithmName = drAlgorithmType["AlgorithmName"].ToString();

                        m_dictAlgNames[AlgorithmID] = strAlgorithmName;
                    }
                }
                drAlgorithmType.Close();
            }
            catch (SQLiteException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            catch (DBConcurrencyException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            foreach (KeyValuePair<long, List<InspectionDataForm.Parameter.LimitAlg>> key in m_dictAlgLimits)
            {
                for (int i = 0; i < key.Value.Count; i++)
                {
                    listAlgLimits.Add(new DataSourceLimits(m_dictAlgNames[key.Key], key.Value[i].maxVal, key.Value[i].m_VoltageMin, key.Value[i].m_VoltageMax,
                        key.Value[i].m_YearsMin, key.Value[i].m_YearsMax, key.Value[i].m_ProtectionOilType));
                }
            }

            gridControl1.DataSource = listAlgLimits;
            gridLimits.ExpandAllGroups();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}