using System;
using System.Collections.Generic;
using System.Text;

namespace DiarMain
{
    class CalcAlgorithms
    {
        static public double? Calc_Votn(InspectionDataForm parent_form, InspectionDataForm.FunctionalSystem fs, InspectionDataForm.SystemItem si, ref string strParameterName)
        {
            InspectionDataForm.Parameter Unit = null;
            InspectionDataForm.Parameter H2 = null;
            InspectionDataForm.Parameter CH4 = null;
            InspectionDataForm.Parameter C2H2 = null;
            InspectionDataForm.Parameter C2H4 = null;
            InspectionDataForm.Parameter C2H6 = null;
            InspectionDataForm.Parameter ProbeDate = null;

            ParameterLite Unit_Last = null;
            ParameterLite H2_Last = null;
            ParameterLite CH4_Last = null;
            ParameterLite C2H2_Last = null;
            ParameterLite C2H4_Last = null;
            ParameterLite C2H6_Last = null;
            ParameterLite ProbeDate_Last = null;

            for (int i = 0; i < si.m_listParams.Count; i++)
            {
                if (si.m_listParams[i].m_strParameterName == "Unit") Unit = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "H2") H2 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "CH4") CH4 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H2") C2H2 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H4") C2H4 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H6") C2H6 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "ProbeDate") ProbeDate = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
            }

            if (Unit == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"Единица измерения\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            if (H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            if (CH4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CH4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            if (C2H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            if (C2H4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            if (C2H6 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H6\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            if (ProbeDate == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"Дата отбора пробы\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }

            if (H2.m_valNumber == null) H2.m_valNumber = 0;
            if (CH4.m_valNumber == null) CH4.m_valNumber = 0;
            if (C2H2.m_valNumber == null) C2H2.m_valNumber = 0;
            if (C2H4.m_valNumber == null) C2H4.m_valNumber = 0;
            if (C2H6.m_valNumber == null) C2H6.m_valNumber = 0;

            double mult = 1;
            if (Unit.m_valSelect != null && Unit.m_valSelect != (Inspection.RangeResult?)0) mult = 10000; // для % умножаем значения на 10000

            if (parent_form.dictLastValuesForHARG.ContainsKey(si.m_SystemItemID))
            {
                for (int i = 0; i < parent_form.dictLastValuesForHARG[si.m_SystemItemID].Count; i++)
                {
                    ParameterLite pl = parent_form.dictLastValuesForHARG[si.m_SystemItemID][i];
                    if (pl.m_strParameterName == "Unit") Unit_Last = pl;
                    if (pl.m_strParameterName == "H2") H2_Last = pl;
                    if (pl.m_strParameterName == "CH4") CH4_Last = pl;
                    if (pl.m_strParameterName == "C2H2") C2H2_Last = pl;
                    if (pl.m_strParameterName == "C2H4") C2H4_Last = pl;
                    if (pl.m_strParameterName == "C2H6") C2H6_Last = pl;
                    if (pl.m_strParameterName == "ProbeDate") ProbeDate_Last = pl;
                }
            }
            else 
                return null;

            if (Unit_Last == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"Единица измерения\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }

            if (ProbeDate_Last.m_valDate != null && ProbeDate.m_valDate != null)
            {
                double mult_last = 1;
                if (Unit_Last.m_valSelect != null && Unit_Last.m_valSelect != (Inspection.RangeResult?)0) mult_last = 10000; // для % умножаем значения на 10000

                DateTime dt = (DateTime)ProbeDate.m_valDate;
                dt = dt.Date;
                DateTime dtLast = (DateTime)ProbeDate_Last.m_valDate;
                dtLast = dtLast.Date;
                TimeSpan span = dt - dtLast;
                double months = span.TotalDays / 30;

                long? VoltageValue = parent_form.m_VoltageValue_VN;
                if (si.bIsVoltageRPN) VoltageValue = parent_form.m_VoltageValue_RPN;
                if (si.bIsVoltageVN) VoltageValue = parent_form.m_VoltageValue_VN;
                if (si.bIsVoltageSN) VoltageValue = parent_form.m_VoltageValue_SN;
                if (si.bIsVoltageNeitral) VoltageValue = parent_form.m_VoltageValue_Neitral;

                long? YearsValue = parent_form.m_YearsValue;
                long? ProtectionOilType = parent_form.m_ProtectionOilType;

                List<InspectionDataForm.Parameter> listParameters = new List<InspectionDataForm.Parameter>();
                List<ParameterLite> listParametersLast = new List<ParameterLite>();

                listParametersLast.Add(H2_Last);
                listParametersLast.Add(CH4_Last);
                listParametersLast.Add(C2H2_Last);
                listParametersLast.Add(C2H4_Last);
                listParametersLast.Add(C2H6_Last);


                if (H2.m_valNumber != null && H2.m_valNumber * mult > /*1.5 **/ H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD)) listParameters.Add(H2);
                if (CH4.m_valNumber != null && CH4.m_valNumber * mult > /*1.5 **/ CH4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD)) listParameters.Add(CH4);
                if (C2H2.m_valNumber != null && C2H2.m_valNumber * mult > /*1.5 **/ C2H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD)) listParameters.Add(C2H2);
                if (C2H4.m_valNumber != null && C2H4.m_valNumber * mult > /*1.5 **/ C2H4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD)) listParameters.Add(C2H4);
                if (C2H6.m_valNumber != null && C2H6.m_valNumber * mult > /*1.5 **/ C2H6.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD)) listParameters.Add(C2H6);

                double? val = null;
                strParameterName = "";
                for (int i = 0; i < listParameters.Count; i++)
                {
                    for (int j = 0; j < listParametersLast.Count; j++)
                    {
                        if (listParametersLast[j] != null && listParameters[i].m_strParameterName == listParametersLast[j].m_strParameterName)
                        {
                            if (listParameters[i].m_valNumber != null && listParametersLast[j].m_valNumber != null
                                && Math.Abs((double)listParametersLast[j].m_valNumber) > 0.0000009 && Math.Abs(months) > 0.0000009)
                            {
                                double v_abs = (double)((double)listParameters[i].m_valNumber * mult - listParametersLast[j].m_valNumber * mult_last) / months;
                                double v_otn = InspectionDataForm.Round(v_abs * 100 / ((double)listParametersLast[j].m_valNumber * mult_last), 6);

                                if (val < v_otn || val == null)
                                {
                                    val = v_otn;
                                    strParameterName = listParameters[i].m_strParameterName;
                                }
                            }
                            break;
                        }
                    }
                }

                if (val != null) val = Math.Round((double)val, 1);
                return val;
            }

            return null;
        }
    }
}
