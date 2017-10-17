using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DiarMain
{
    public partial class ParameterLimitForm : DevExpress.XtraEditors.XtraForm
    {
        class DataSourceLimits
        {
            public DataSourceLimits(InspectionDataForm.Parameter.LimitRange lr)
            {
                minVal = lr.minVal;
                maxVal = lr.maxVal;

                m_VoltageMin = lr.m_VoltageMin;
                m_VoltageMax = lr.m_VoltageMax;
                //m_VoltageRPNMin = lr.m_VoltageRPNMin;
                //m_VoltageRPNMax = lr.m_VoltageRPNMax;
                m_ProtectionOilType = "";
                if (lr.m_ProtectionOilType == 1) m_ProtectionOilType = "пленочная защита";
                if (lr.m_ProtectionOilType == 2) m_ProtectionOilType = "азотная защита";
                if (lr.m_ProtectionOilType == 3) m_ProtectionOilType = "свободное дыхание (воздухоосушитель)";
                m_InputVoltageType = "";
                if (lr.m_InputVoltageType == 1) m_InputVoltageType = "масляный герметичный";
                if (lr.m_InputVoltageType == 2) m_InputVoltageType = "масляный негерметичный";
                if (lr.m_InputVoltageType == 3) m_InputVoltageType = "твердая изоляция";

                m_RangeValue = lr.m_RangeValue;
            }

            public double? minVal;
            public double? maxVal;

            public long? m_VoltageMin;
            public long? m_VoltageMax;
            public long? m_VoltageRPNMin;
            public long? m_VoltageRPNMax;
            public string m_ProtectionOilType;
            public string m_InputVoltageType;

            //public long? m_UseYearFrom;
            //public long? m_UseYearTo;
            public long? m_RangeValue;

            public double? MIN_VAL
            {
                get { return minVal; }
                set { minVal = value; }
            }

            public double? MAX_VAL
            {
                get { return maxVal; }
                set { maxVal = value; }
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

            public long? MIN_VOLTAGE_RPN_VAL
            {
                get { return m_VoltageRPNMin; }
                set { m_VoltageRPNMin = value; }
            }

            public long? MAX_VOLTAGE_RPN_VAL
            {
                get { return m_VoltageRPNMax; }
                set { m_VoltageRPNMax = value; }
            }

            public string PROTECTION_OIL_TYPE
            {
                get { return m_ProtectionOilType; }
                set { m_ProtectionOilType = value; }
            }

            public string INPUT_VOLTAGE_TYPE
            {
                get { return m_InputVoltageType; }
                set { m_InputVoltageType = value; }
            }

            public long? RANGE
            {
                get { return m_RangeValue; }
                set { m_RangeValue = value; }
            }
        };

        public List<InspectionDataForm.Parameter.LimitRange> m_listLimits;
        BindingList<DataSourceLimits> listData = new BindingList<DataSourceLimits>();

        public ParameterLimitForm()
        {
            InitializeComponent();
        }

        private void ParameterLimitForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < m_listLimits.Count; i++)
            {
                listData.Add(new DataSourceLimits(m_listLimits[i]));
            }

            gridControl1.DataSource = listData;
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