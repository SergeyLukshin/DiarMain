using System;
using System.Collections.Generic;
using System.Text;

namespace DiarMain
{
    class DefectAlgorithms
    {
        static public List<Inspection.DefectInfo> Defect_HARG_1(InspectionDataForm parent_form, InspectionDataForm.FunctionalSystem fs, InspectionDataForm.SystemItem si)
        {
            List<Inspection.DefectInfo> result = new List<Inspection.DefectInfo>();

            InspectionDataForm.Parameter Unit = null;
            //InspectionDataForm.Parameter H2 = null;
            InspectionDataForm.Parameter CH4 = null;
            InspectionDataForm.Parameter C2H2 = null;
            InspectionDataForm.Parameter C2H4 = null;
            InspectionDataForm.Parameter C2H6 = null;
            //InspectionDataForm.Parameter CO = null;
            //InspectionDataForm.Parameter CO2 = null;
            //InspectionDataForm.Parameter SumAll = null;

            for (int i = 0; i < si.m_listParams.Count; i++)
            {
                if (si.m_listParams[i].m_strParameterName == "Unit") Unit = si.m_listParams[i];
                //if (si.m_listParams[i].m_strParameterName == "H2") H2 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "CH4") CH4 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H2") C2H2 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H4") C2H4 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H6") C2H6 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                //if (si.m_listParams[i].m_strParameterName == "CO") CO = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                //if (si.m_listParams[i].m_strParameterName == "CO2") CO2 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                //if (si.m_listParams[i].m_strParameterName == "SumAll") SumAll = si.m_listParams[i];
            }

            if (Unit == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"Единица измерения\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            /*if (H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }*/
            if (CH4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CH4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (C2H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (C2H4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (C2H6 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H6\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            /*if (CO == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CO\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (CO2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CO2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }*/
            /*if (SumAll == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"Газосодержание общее\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }*/

            double mult = 1;
            if (Unit.m_valSelect != null && Unit.m_valSelect != (Inspection.RangeResult?)0) mult = 10000;

            long? VoltageValue = parent_form.m_VoltageValue_VN;
            if (si.bIsVoltageSN) VoltageValue = parent_form.m_VoltageValue_SN;
            if (si.bIsVoltageRPN) VoltageValue = parent_form.m_VoltageValue_RPN;
            if (si.bIsVoltageVN) VoltageValue = parent_form.m_VoltageValue_VN;
            if (si.bIsVoltageNeitral) VoltageValue = parent_form.m_VoltageValue_Neitral;

            long? YearsValue = parent_form.m_YearsValue;
            long? ProtectionOilType = parent_form.m_ProtectionOilType;

            if (/*H2.m_valNumber == null &&*/
                CH4.m_valNumber == null &&
                C2H2.m_valNumber == null &&
                C2H4.m_valNumber == null &&
                C2H6.m_valNumber == null /*&&
                CO.m_valNumber == null &&
                CO2.m_valNumber == null*/)
                return result;

            //if (H2.m_valNumber == null) H2.m_valNumber = 0;
            if (CH4.m_valNumber == null) CH4.m_valNumber = 0;
            if (C2H2.m_valNumber == null) C2H2.m_valNumber = 0;
            if (C2H4.m_valNumber == null) C2H4.m_valNumber = 0;
            if (C2H6.m_valNumber == null) C2H6.m_valNumber = 0;
            //if (CO.m_valNumber == null) CO.m_valNumber = 0;
            //if (CO2.m_valNumber == null) CO2.m_valNumber = 0;

            long defectID = 0;
            for (int i = 0; i < si.m_listDefects.Count; i++)
            {
                if (si.m_listDefects[i].m_InspectionSubType == null ||
                    si.m_listDefects[i].m_InspectionSubType == parent_form.m_sub_type_id)
                {
                    defectID = si.m_listDefects[i].m_DefectID;
                    break;
                }
            }

            /*if (H2.m_valNumber * mult <= 1.5 * H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) && 
               CH4.m_valNumber * mult <= 1.5 * CH4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
               C2H2.m_valNumber * mult <= 1.5 * C2H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
               C2H4.m_valNumber * mult <= 1.5 * C2H4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
               C2H6.m_valNumber * mult <= 1.5 * C2H6.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
               CO.m_valNumber * mult <= 1.5 * CO.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
               CO2.m_valNumber * mult <= 1.5 * CO2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD))
            {
                result.Add(new Inspection.DefectInfo(defectID, Inspection.RangeResult.Green));
                return result;
            }*/

            double fSumCH = (double)(/*H2.m_valNumber +*/ CH4.m_valNumber + C2H2.m_valNumber + C2H4.m_valNumber + C2H6.m_valNumber);

            
            if (C2H2.m_valNumber * mult >= 5
                || fSumCH * mult >= 300 && (VoltageValue >= 110 && VoltageValue <= 220)
                || fSumCH * mult >= 150 && (VoltageValue >= 330 && VoltageValue <= 750))
            {
                result.Add(new Inspection.DefectInfo(defectID, Inspection.RangeResult.Red));
                return result;
            }

            result.Add(new Inspection.DefectInfo(defectID, Inspection.RangeResult.Green));
            return result;            
        }

        static public List<Inspection.DefectInfo> Defect_HARG_2(InspectionDataForm parent_form, InspectionDataForm.FunctionalSystem fs, InspectionDataForm.SystemItem si)
        {
            List<Inspection.DefectInfo> result = new List<Inspection.DefectInfo>();

            InspectionDataForm.Parameter Unit = null;
            InspectionDataForm.Parameter H2 = null;
            InspectionDataForm.Parameter CH4 = null;
            InspectionDataForm.Parameter C2H2 = null;
            InspectionDataForm.Parameter C2H4 = null;
            InspectionDataForm.Parameter C2H6 = null;
            InspectionDataForm.Parameter CO = null;
            InspectionDataForm.Parameter CO2 = null;
            InspectionDataForm.Parameter Votn = null;

            long defectID1 = 0;
            long defectID2 = 0;
            long defectID3 = 0;
            for (int i = 0; i < si.m_listDefects.Count; i++)
            {
                if (si.m_listDefects[i].m_InspectionSubType == null ||
                    si.m_listDefects[i].m_InspectionSubType == parent_form.m_sub_type_id)
                {
                    if (si.m_listDefects[i].m_strDefectName == "Дефекты термического и/или электрического характера")
                        defectID1 = si.m_listDefects[i].m_DefectID;
                    if (si.m_listDefects[i].m_strDefectName == "Ускоренное старение и/или увлажнение твердой изоляции")
                        defectID2 = si.m_listDefects[i].m_DefectID;
                    if (si.m_listDefects[i].m_strDefectName == "Перегрев твердой изоляции")
                        defectID3 = si.m_listDefects[i].m_DefectID;
                }
            }

            for (int i = 0; i < si.m_listParams.Count; i++)
            {
                if (si.m_listParams[i].m_strParameterName == "Unit") Unit = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "H2") H2 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "CH4") CH4 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H2") C2H2 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H4") C2H4 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H6") C2H6 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "CO") CO = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "CO2") CO2 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "Votn") Votn = si.m_listParams[i];
            }

            if (Unit == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"Единица измерения\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (CH4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CH4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (C2H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (C2H4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (C2H6 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H6\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (CO == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CO\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (CO2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CO2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }

            double mult = 1;
            if (Unit.m_valSelect != null && Unit.m_valSelect != (Inspection.RangeResult?)0) mult = 10000;

            //long? VoltageValue = parent_form.m_VoltageValue_VN;
            long? VoltageValue = parent_form.m_VoltageValue_VN;
            if (si.bIsVoltageRPN) VoltageValue = parent_form.m_VoltageValue_RPN;
            if (si.bIsVoltageVN) VoltageValue = parent_form.m_VoltageValue_VN;
            if (si.bIsVoltageSN) VoltageValue = parent_form.m_VoltageValue_SN;
            if (si.bIsVoltageNeitral) VoltageValue = parent_form.m_VoltageValue_Neitral;

            long? YearsValue = parent_form.m_YearsValue;
            long? ProtectionOilType = parent_form.m_ProtectionOilType;

            if (H2.m_valNumber == null &&
                CH4.m_valNumber == null &&
                C2H2.m_valNumber == null &&
                C2H4.m_valNumber == null &&
                C2H6.m_valNumber == null &&
                CO.m_valNumber == null &&
                CO2.m_valNumber == null)
                return result;

            if (H2.m_valNumber == null) H2.m_valNumber = 0;
            if (CH4.m_valNumber == null) CH4.m_valNumber = 0;
            if (C2H2.m_valNumber == null) C2H2.m_valNumber = 0;
            if (C2H4.m_valNumber == null) C2H4.m_valNumber = 0;
            if (C2H6.m_valNumber == null) C2H6.m_valNumber = 0;
            if (CO.m_valNumber == null) CO.m_valNumber = 0;
            if (CO2.m_valNumber == null) CO2.m_valNumber = 0;

            bool bMoreLimitValues = true;

            if (H2.m_valNumber * mult <= /*1.5 **/ H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) && 
                CH4.m_valNumber * mult <= /*1.5 **/ CH4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
               C2H2.m_valNumber * mult <= /*1.5 **/ C2H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
               C2H4.m_valNumber * mult <= /*1.5 **/ C2H4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
               C2H6.m_valNumber * mult <= /*1.5 **/ C2H6.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD))
            {
                bMoreLimitValues = false;
                //return result;
            }

            if (Votn != null && Votn.m_valNumber >= 10 && bMoreLimitValues)
            {
                result.Add(new Inspection.DefectInfo(defectID1, Inspection.RangeResult.Red));
            }
            else
            {
                if (Votn != null && Votn.m_valNumber > 5 && Votn.m_valNumber < 10 && bMoreLimitValues)
                {
                    result.Add(new Inspection.DefectInfo(defectID1, Inspection.RangeResult.Orange));
                }
                else
                {
                    if (bMoreLimitValues)
                        result.Add(new Inspection.DefectInfo(defectID1, Inspection.RangeResult.Yellow));
                    else
                        result.Add(new Inspection.DefectInfo(defectID1, Inspection.RangeResult.Green));
                }
            }

            //Inspection.RangeResult result = Inspection.RangeResult.Green;
            //string strResult = "";

            if (CO2.m_valNumber * mult > /*1.5 **/ CO2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD))
            {
                result.Add(new Inspection.DefectInfo(defectID2, Inspection.RangeResult.Yellow));
                result.Add(new Inspection.DefectInfo(defectID3, Inspection.RangeResult.Yellow));
                //strResult = strResult + "Ускоренное старение и/или увлажнение твердой изоляции; Перегрев твердой изоляции";
            }
            else
            {
                result.Add(new Inspection.DefectInfo(defectID3, Inspection.RangeResult.Green));

                if (CO.m_valNumber * mult > /*1.5 **/ CO.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD))
                    result.Add(new Inspection.DefectInfo(defectID2, Inspection.RangeResult.Yellow));
                else
                    result.Add(new Inspection.DefectInfo(defectID2, Inspection.RangeResult.Green));
            }

            return result;//, strResult);
        }

        static public List<Inspection.DefectInfo> Defect_HARG_3(InspectionDataForm parent_form, InspectionDataForm.FunctionalSystem fs, InspectionDataForm.SystemItem si)
        {
            List<Inspection.DefectInfo> result = new List<Inspection.DefectInfo>();

            InspectionDataForm.Parameter Unit = null;
            InspectionDataForm.Parameter H2 = null;
            InspectionDataForm.Parameter CH4 = null;
            InspectionDataForm.Parameter C2H2 = null;
            InspectionDataForm.Parameter C2H4 = null;
            InspectionDataForm.Parameter C2H6 = null;
            InspectionDataForm.Parameter Votn = null;

            long defectID = 0;
            for (int i = 0; i < si.m_listDefects.Count; i++)
            {
                if (si.m_listDefects[i].m_InspectionSubType == null ||
                    si.m_listDefects[i].m_InspectionSubType == parent_form.m_sub_type_id)
                {
                    defectID = si.m_listDefects[i].m_DefectID;
                    break;
                }
            }

            for (int i = 0; i < si.m_listParams.Count; i++)
            {
                if (si.m_listParams[i].m_strParameterName == "Unit") Unit = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "H2") H2 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "CH4") CH4 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H2") C2H2 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H4") C2H4 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "C2H6") C2H6 = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
                if (si.m_listParams[i].m_strParameterName == "Votn") Votn = (InspectionDataForm.Parameter)si.m_listParams[i].Clone();
            }

            if (Unit == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"Единица измерения\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (CH4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CH4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (C2H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (C2H4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }
            if (C2H6 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H6\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return result;
            }

            double mult = 1;
            if (Unit.m_valSelect != null && Unit.m_valSelect != (Inspection.RangeResult?)0) mult = 10000;

            //long? VoltageValue = parent_form.m_VoltageValue_VN;
            long? VoltageValue = parent_form.m_VoltageValue_VN;
            if (si.bIsVoltageRPN) VoltageValue = parent_form.m_VoltageValue_RPN;
            if (si.bIsVoltageVN) VoltageValue = parent_form.m_VoltageValue_VN;
            if (si.bIsVoltageSN) VoltageValue = parent_form.m_VoltageValue_SN;
            if (si.bIsVoltageNeitral) VoltageValue = parent_form.m_VoltageValue_Neitral;

            long? YearsValue = parent_form.m_YearsValue;
            long? ProtectionOilType = parent_form.m_ProtectionOilType;

            if (H2.m_valNumber == null &&
                CH4.m_valNumber == null &&
                C2H2.m_valNumber == null &&
                C2H4.m_valNumber == null &&
                C2H6.m_valNumber == null)
                return result;

            if (H2.m_valNumber == null) H2.m_valNumber = 0;
            if (CH4.m_valNumber == null) CH4.m_valNumber = 0;
            if (C2H2.m_valNumber == null) C2H2.m_valNumber = 0;
            if (C2H4.m_valNumber == null) C2H4.m_valNumber = 0;
            if (C2H6.m_valNumber == null) C2H6.m_valNumber = 0;

            bool bMoreLimitValues = true;

            if (H2.m_valNumber * mult <= /*1.5 **/ H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                CH4.m_valNumber * mult <= /*1.5 **/ CH4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                C2H2.m_valNumber * mult <= /*1.5 **/ C2H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                C2H4.m_valNumber * mult <= /*1.5 **/ C2H4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                C2H6.m_valNumber * mult <= /*1.5 **/ C2H6.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD))
            {
                bMoreLimitValues = false;
            }

            if (Votn != null && Votn.m_valNumber >= 10 && bMoreLimitValues)
            {
                result.Add(new Inspection.DefectInfo(defectID, Inspection.RangeResult.Red));
                return result;
            }

            if (Votn != null && Votn.m_valNumber > 5 && Votn.m_valNumber < 10 && bMoreLimitValues)
            {
                result.Add(new Inspection.DefectInfo(defectID, Inspection.RangeResult.Orange));
                return result;
            }

            if (bMoreLimitValues)
            {
                result.Add(new Inspection.DefectInfo(defectID, Inspection.RangeResult.Yellow));
                return result;
            }

            result.Add(new Inspection.DefectInfo(defectID, Inspection.RangeResult.Green));
            return result;
        }

        /*static public Inspection.RangeResult Defect_HARG_2(InspectionDataForm parent_form, InspectionDataForm.FunctionalSystem fs, InspectionDataForm.SystemItem si)
        {
            InspectionDataForm.Parameter Unit = null;
            InspectionDataForm.Parameter H2 = null;
            InspectionDataForm.Parameter CH4 = null;
            InspectionDataForm.Parameter C2H2 = null;
            InspectionDataForm.Parameter C2H4 = null;
            InspectionDataForm.Parameter C2H6 = null;

            for (int i = 0; i < si.m_listParams.Count; i++)
            {
                if (si.m_listParams[i].m_strParameterName == "Unit") Unit = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "H2") H2 = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "CH4") CH4 = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "C2H2") C2H2 = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "C2H4") C2H4 = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "C2H6") C2H6 = si.m_listParams[i];
            }

            if (Unit == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"Единица измерения\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (CH4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CH4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (C2H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (C2H4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (C2H6 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H6\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }

            double mult = 1;
            if (Unit.m_valSelect != null && Unit.m_valSelect != (Inspection.RangeResult?)0) mult = 10000;

            if (H2.m_valNumber == null
                || CH4.m_valNumber == null
                || C2H2.m_valNumber == null
                || C2H4.m_valNumber == null
                || C2H6.m_valNumber == null) return Inspection.RangeResult.None;

            // находим значения из бака трансформатора

            InspectionDataForm.Parameter Unit_Boiler = null;
            InspectionDataForm.Parameter H2_Boiler = null;
            InspectionDataForm.Parameter CH4_Boiler = null;
            InspectionDataForm.Parameter C2H2_Boiler = null;
            InspectionDataForm.Parameter C2H4_Boiler = null;
            InspectionDataForm.Parameter C2H6_Boiler = null;
            
            bool bfind = false;
            for (int i = 0; i < parent_form.m_list.Count; i++)
            {
                for (int j = 0; j < parent_form.m_list[i].m_listSystemItems.Count; j++)
                {
                    InspectionDataForm.SystemItem si_ = parent_form.m_list[i].m_listSystemItems[j];
                    if (si_.m_SystemItemID == si.m_DefectAlgorithm.m_arrSystemItemIDs[0])
                    {
                        for (int k = 0; k < si_.m_listParams.Count; k++)
                        {
                            if (si_.m_listParams[k].m_strParameterName == "Unit") Unit_Boiler = si_.m_listParams[k];
                            if (si_.m_listParams[k].m_strParameterName == "H2") H2_Boiler = si_.m_listParams[k];
                            if (si_.m_listParams[k].m_strParameterName == "CH4") CH4_Boiler = si_.m_listParams[k];
                            if (si_.m_listParams[k].m_strParameterName == "C2H2") C2H2_Boiler = si_.m_listParams[k];
                            if (si_.m_listParams[k].m_strParameterName == "C2H4") C2H4_Boiler = si_.m_listParams[k];
                            if (si_.m_listParams[k].m_strParameterName == "C2H6") C2H6_Boiler = si_.m_listParams[k];
                        }

                        if (Unit_Boiler == null)
                        {
                            MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"Единица измерения\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return Inspection.RangeResult.None;
                        }
                        if (H2_Boiler == null)
                        {
                            MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return Inspection.RangeResult.None;
                        }
                        if (CH4_Boiler == null)
                        {
                            MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CH4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return Inspection.RangeResult.None;
                        }
                        if (C2H2_Boiler == null)
                        {
                            MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return Inspection.RangeResult.None;
                        }
                        if (C2H4_Boiler == null)
                        {
                            MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return Inspection.RangeResult.None;
                        }
                        if (C2H6_Boiler == null)
                        {
                            MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H6\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return Inspection.RangeResult.None;
                        }

                        bfind = true;
                        break;
                    }
                }
                if (bfind) break;
            }
            if (!bfind) return Inspection.RangeResult.None;

            double mult_Boiler = 1;
            if (Unit_Boiler.m_valSelect != null && Unit_Boiler.m_valSelect != (Inspection.RangeResult?)0) mult_Boiler = 10000;

            if (H2_Boiler.m_valNumber == null
                || CH4_Boiler.m_valNumber == null
                || C2H2_Boiler.m_valNumber == null
                || C2H4_Boiler.m_valNumber == null
                || C2H6_Boiler.m_valNumber == null) return Inspection.RangeResult.None;

            if (!H2_Boiler.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_RD)
                || !CH4_Boiler.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_RD)
                || !C2H2_Boiler.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_RD)
                || !C2H4_Boiler.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_RD)
                || !C2H6_Boiler.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_RD))
            {
                return Inspection.RangeResult.None;
            }

            long? VoltageValue = parent_form.m_VoltageValue;
            long? YearsValue = parent_form.m_YearsValue;
            long? ProtectionOilType = parent_form.m_ProtectionOilType;

            if (H2_Boiler.m_valNumber * mult_Boiler <= H2_Boiler.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                CH4_Boiler.m_valNumber * mult_Boiler <= CH4_Boiler.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                    C2H2_Boiler.m_valNumber * mult_Boiler <= C2H2_Boiler.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                    C2H4_Boiler.m_valNumber * mult_Boiler <= C2H4_Boiler.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                    C2H6_Boiler.m_valNumber * mult_Boiler <= C2H6_Boiler.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD))
            {
                if (H2_Boiler.m_valNumber * mult_Boiler * 0.8 > H2.m_valNumber * mult
                    || CH4_Boiler.m_valNumber * mult_Boiler * 0.8 > CH4.m_valNumber * mult
                    || C2H2_Boiler.m_valNumber * mult_Boiler * 0.8 > C2H2.m_valNumber * mult
                    || C2H4_Boiler.m_valNumber * mult_Boiler * 0.8 > C2H4.m_valNumber * mult
                    || C2H6_Boiler.m_valNumber * mult_Boiler * 0.8 > C2H6.m_valNumber * mult) // в пределах 20%
                    return Inspection.RangeResult.Green;

                if (H2_Boiler.m_valNumber * mult_Boiler * 1.2 >= H2.m_valNumber * mult && H2_Boiler.m_valNumber * mult_Boiler * 0.8 <= H2.m_valNumber * mult
                    || CH4_Boiler.m_valNumber * mult_Boiler * 1.2 >= CH4.m_valNumber * mult && CH4_Boiler.m_valNumber * mult_Boiler * 0.8 <= CH4.m_valNumber * mult
                    || C2H2_Boiler.m_valNumber * mult_Boiler * 1.2 >= C2H2.m_valNumber * mult && C2H2_Boiler.m_valNumber * mult_Boiler * 0.8 <= C2H2.m_valNumber * mult
                    || C2H4_Boiler.m_valNumber * mult_Boiler * 1.2 >= C2H4.m_valNumber * mult && C2H4_Boiler.m_valNumber * mult_Boiler * 0.8 <= C2H4.m_valNumber * mult
                    || C2H6_Boiler.m_valNumber * mult_Boiler * 1.2 >= C2H6.m_valNumber * mult && C2H6_Boiler.m_valNumber * mult_Boiler * 0.8 <= C2H6.m_valNumber * mult)
                    return Inspection.RangeResult.Yellow; // в пределах +- 20 %

                return Inspection.RangeResult.None;
            }
            else
            {
                if (H2_Boiler.m_valNumber * mult_Boiler * 1.2 >= H2.m_valNumber * mult && H2_Boiler.m_valNumber * mult_Boiler * 0.8 <= H2.m_valNumber * mult
                    || CH4_Boiler.m_valNumber * mult_Boiler * 1.2 >= CH4.m_valNumber * mult && CH4_Boiler.m_valNumber * mult_Boiler * 0.8 <= CH4.m_valNumber * mult
                    || C2H2_Boiler.m_valNumber * mult_Boiler * 1.2 >= C2H2.m_valNumber * mult && C2H2_Boiler.m_valNumber * mult_Boiler * 0.8 <= C2H2.m_valNumber * mult
                    || C2H4_Boiler.m_valNumber * mult_Boiler * 1.2 >= C2H4.m_valNumber * mult && C2H4_Boiler.m_valNumber * mult_Boiler * 0.8 <= C2H4.m_valNumber * mult
                    || C2H6_Boiler.m_valNumber * mult_Boiler * 1.2 >= C2H6.m_valNumber * mult && C2H6_Boiler.m_valNumber * mult_Boiler * 0.8 <= C2H6.m_valNumber * mult)
                    return Inspection.RangeResult.Orange; // в пределах +- 20 %

                return Inspection.RangeResult.None;
            }
        }*/

        /*tatic public double MaxVal(params double[] vals)
        {
            double max = double.MinValue;
            for (int i = 0; i < vals.Length; i++)
            {
                if (max < vals[i]) max = vals[i];
            }
            return max;
        }

        static public double CalcAVG(List<double> listGases)
        {
            double sum = 0;
            for (int i = 0; i < listGases.Count; i++)
            {
                sum += listGases[i];
            }
            return sum / listGases.Count;
        }

        static public double CalcSigma(List<double> listGases)
        {
            double sigma = 0;
            double avg = CalcAVG(listGases);
            for (int i = 0; i < listGases.Count; i++)
            {
                sigma += (listGases[i] - avg) * (listGases[i] - avg);
            }
            return Math.Sqrt(sigma);
        }

        static public double CalcKoef(List<double> listGases1, List<double> listGases2)
        {
            double koef = 0;
            double avg1 = CalcAVG(listGases1);
            double avg2 = CalcAVG(listGases2);

            for (int i = 0; i < listGases1.Count; i++)
            {
                koef += (listGases1[i] - avg1) * (listGases2[i] - avg2);
            }

            koef = koef / (CalcSigma(listGases1) * CalcSigma(listGases2));

            return koef;
        }

        class NormogrammaResultInfo
        {
            public NormogrammaResultInfo(Inspection.InspectionResult result)
            {
                listGases = new List<double>();
                m_result = result;
            }

            public Inspection.InspectionResult m_result;
            public List<double> listGases; // H2, CH4, C2H6, C2H4, C2H2 
            public double koef = 0;
        }

        static public Inspection.InspectionResult Defect_HARG_Alg(Inspection.AlgorithmType alg_type, InspectionDataForm parent_form, InspectionDataForm.Parameter Unit, 
            InspectionDataForm.Parameter H2, InspectionDataForm.Parameter CH4, InspectionDataForm.Parameter C2H2, 
            InspectionDataForm.Parameter C2H4, InspectionDataForm.Parameter C2H6, InspectionDataForm.Parameter CO, 
            InspectionDataForm.Parameter CO2)
        {
            double mult = 1;
            if (Unit.m_valSelect != null && Unit.m_valSelect != (Inspection.RangeResult?)0) mult = 10000;

            long? VoltageValue = fs.m_bIsRPN ? parent_form.m_VoltageRPNValue : parent_form.m_VoltageValue;
            long? YearsValue = parent_form.m_YearsValue;
            long? ProtectionOilType = parent_form.m_ProtectionOilType;

            if (alg_type == Inspection.AlgorithmType.AT_RD)
            {
                if (CO.m_valNumber == null || CO2.m_valNumber == null) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");

                if (H2.m_valNumber * mult <= H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) * 1.5 &&
                    CH4.m_valNumber * mult <= CH4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) * 1.5 &&
                    C2H2.m_valNumber * mult <= C2H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) * 1.5 &&
                    C2H4.m_valNumber * mult <= C2H4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) * 1.5 &&
                    C2H6.m_valNumber * mult <= C2H6.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) * 1.5)
                {
                    return new Inspection.InspectionResult(Inspection.RangeResult.Green);
                }

                double C2H2_C2H4 = double.MaxValue;
                double CH4_H2 = double.MaxValue;
                double C2H4_C2H6 = double.MaxValue;
                double CO2_CO = double.MaxValue;

                if (Math.Abs((double)C2H4.m_valNumber) > 0.0000009) C2H2_C2H4 = (double)(C2H2.m_valNumber / C2H4.m_valNumber);
                if (Math.Abs((double)H2.m_valNumber) > 0.0000009) CH4_H2 = (double)(CH4.m_valNumber / H2.m_valNumber);
                if (Math.Abs((double)C2H6.m_valNumber) > 0.0000009) C2H4_C2H6 = (double)(C2H4.m_valNumber / C2H6.m_valNumber);
                if (Math.Abs((double)CO.m_valNumber) > 0.0000009) CO2_CO = (double)(CO2.m_valNumber / CO.m_valNumber);

                // проверяем V, если есть
                // проверяем концентрации газов
                // если концентрации не выходят за макс предел и 5<=CO2/CO<=13 - тогда Зеленый
                // если концентрации не выходят за макс предел и (CO2/CO<5 или CO2/CO>13) - тогда Оранж
                // если какой нибудь один выходит, проверям по очереди условия для Красного, потом для Оранжевого, потом для Желтого
                // если во время проверки выполняется хотя бы одно условие, тогда ставим цвет узла текущим проверяемым цветом

                // Red
                if ( C2H2_C2H4 < 0.1 && CH4_H2 >= 1 && C2H4_C2H6 >= 3)
                    return new Inspection.InspectionResult(Inspection.RangeResult.Red, "Термический дефект высокой температуры (>700°С).");

                if (C2H2_C2H4 >= 0.1 && C2H2_C2H4 <= 3 && CH4_H2 >= 0.1 && CH4_H2 <= 1 && C2H4_C2H6 >= 3)
                    return new Inspection.InspectionResult(Inspection.RangeResult.Red, "Дуговые разряды; искрение; пробой масла между обмотками или катушками или между катушками на землю.");

                // Orange
                //if (T.m_valNumber >= 300 && T.m_valNumber <= 700)
                //    return Inspection.RangeResult.Orange;

                if (C2H2_C2H4 >= 0.1 && C2H2_C2H4 <= 3 && CH4_H2 < 0.1 && C2H4_C2H6 <= 1)
                    return new Inspection.InspectionResult(Inspection.RangeResult.Orange, "Разряды в заполненных газом полостях, образовавшихся вследствие не полной пропитки или влажности изоляции, приводящие к оставлению следа или пробою твердой изоляции.");

                if (CO2_CO < 5 || CO2_CO > 13)
                    return new Inspection.InspectionResult(Inspection.RangeResult.Orange, "Твердая изоляция затронута повреждением.");

                // Yellow
                if ( C2H2_C2H4 < 0.1 && CH4_H2 >= 0.1 && CH4_H2 <= 1 && C2H4_C2H6 >= 1 && C2H4_C2H6 <= 3)
                    return new Inspection.InspectionResult(Inspection.RangeResult.Yellow, "Термический дефект низкой температуры (<150°С).");

                if (C2H2_C2H4 < 0.1 && CH4_H2 > 1 && C2H4_C2H6 < 1)
                    return new Inspection.InspectionResult(Inspection.RangeResult.Yellow, "Термический дефект в диапазоне низких температур (150-300°С).");

                if (C2H2_C2H4 < 0.1 && CH4_H2 < 0.1 && C2H4_C2H6 <= 1)
                    return new Inspection.InspectionResult(Inspection.RangeResult.Yellow, "Разряды в заполненных газом полостях, образовавшихся вследствие не полной пропитки или влажности изоляции.");

                if (C2H2_C2H4 > 0.1 && CH4_H2 >= 0.1 && CH4_H2 <= 1 && C2H4_C2H6 >= 1 && C2H4_C2H6 <= 3)
                    return new Inspection.InspectionResult(Inspection.RangeResult.Yellow, "Непрерывное искрение в масле между соединениями различных потенциалов или плавающего потенциала. Пробой масла между твердыми материалами.");

                return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");
            }

            if (alg_type == Inspection.AlgorithmType.AT_Dornenburg)
            {
                if (CO.m_valNumber == null) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");

                if (!H2.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_Dornenburg)
                    || !CH4.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_Dornenburg)
                    || !C2H2.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_Dornenburg)
                    || !C2H4.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_Dornenburg)
                    || !C2H6.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_Dornenburg)
                    || !CO.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_Dornenburg))
                {
                    return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");
                }

                double R1 = double.MaxValue;
                double R2 = double.MaxValue;
                double R3 = double.MaxValue;
                double R4 = double.MaxValue;

                if (Math.Abs((double)H2.m_valNumber) > 0.0000009) R1 = (double)(CH4.m_valNumber / H2.m_valNumber);
                if (Math.Abs((double)C2H4.m_valNumber) > 0.0000009) R2 = (double)(C2H2.m_valNumber / C2H4.m_valNumber);
                if (Math.Abs((double)CH4.m_valNumber) > 0.0000009) R3 = (double)(C2H2.m_valNumber / CH4.m_valNumber);
                if (Math.Abs((double)C2H2.m_valNumber) > 0.0000009) R4 = (double)(C2H6.m_valNumber / C2H2.m_valNumber);

                if (H2.m_valNumber * mult < H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_Dornenburg) * 2 &&
                    CH4.m_valNumber * mult < CH4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_Dornenburg) * 2 &&
                    C2H2.m_valNumber * mult < C2H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_Dornenburg) * 2 &&
                    C2H4.m_valNumber * mult < C2H4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_Dornenburg) * 2)
                {
                    return new Inspection.InspectionResult(Inspection.RangeResult.Green);
                }

                if (C2H6.m_valNumber * mult < C2H6.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_Dornenburg) &&
                    CO.m_valNumber * mult < CO.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_Dornenburg))
                {
                    return new Inspection.InspectionResult(Inspection.RangeResult.Green);
                }

                if (R1 < 0.1)
                {
                    if (R3 < 0.3 && R4 > 0.4)
                        return new Inspection.InspectionResult(Inspection.RangeResult.None, "Частичный разряд или корона."); // Частичный разряд или корона.
                    else
                        return new Inspection.InspectionResult(Inspection.RangeResult.None, "Повреждение не идентифицировано. Новый отбор пробы."); // Повреждение не идентифицировано. Новый отбор пробы.
                }
                else
                {
                    if (R1 >= 0.1 && R1 <= 1.0)
                    {
                        if (R2 > 0.75 && R3 > 0.3 && R4 < 0.4)
                            return new Inspection.InspectionResult(Inspection.RangeResult.None, "Разряды, дуга."); // Разряды, дуга.
                        else
                            return new Inspection.InspectionResult(Inspection.RangeResult.None, "Повреждение не идентифицировано. Новый отбор пробы."); // Повреждение не идентифицировано. Новый отбор пробы.
                    }
                    else
                    {
                        if (R2 < 0.75 && R3 < 0.3 && R4 > 0.4)
                            return new Inspection.InspectionResult(Inspection.RangeResult.None, "Термическое повреждение."); // Термическое повреждение.
                        else
                            return new Inspection.InspectionResult(Inspection.RangeResult.None, "Повреждение не идентифицировано. Новый отбор пробы."); // Повреждение не идентифицировано. Новый отбор пробы.
                    }
                }
            }

            if (alg_type == Inspection.AlgorithmType.AT_Rodgers)
            {
                if (CO.m_valNumber == null || CO2.m_valNumber == null) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");

                double R1 = double.MaxValue;
                double R2 = double.MaxValue;
                double R5 = double.MaxValue;
                double R6 = double.MaxValue;

                if (Math.Abs((double)H2.m_valNumber) > 0.0000009) R1 = (double)(CH4.m_valNumber / H2.m_valNumber);
                if (Math.Abs((double)C2H4.m_valNumber) > 0.0000009) R2 = (double)(C2H2.m_valNumber / C2H4.m_valNumber);
                if (Math.Abs((double)C2H6.m_valNumber) > 0.0000009) R6 = (double)(C2H4.m_valNumber / C2H6.m_valNumber);
                if (Math.Abs((double)CH4.m_valNumber) > 0.0000009) R5 = (double)(C2H6.m_valNumber / CH4.m_valNumber);

                int R1_code = 0;
                int R2_code = 0;
                int R5_code = 0;
                int R6_code = 0;

                if (R1 <= 0.1) R1_code = 5;
                if (R1 > 0.1 && R1 <= 1.0) R1_code = 0;
                if (R1 >= 1.0 && R1 < 3.0) R1_code = 1;
                if (R1 >= 3.0) R1_code = 2;

                if (R2 < 0.5) R2_code = 0;
                if (R2 >= 0.5 && R2 < 3.0) R2_code = 1;
                if (R2 >= 3.0) R2_code = 2;

                if (R5 < 1) R5_code = 0;
                else R5_code = 1;

                if (R6 < 0.5) R6_code = 0;
                if (R6 >= 0.5 && R6 < 3.0) R6_code = 1;
                if (R6 >= 3.0) R6_code = 2;

                if (R1_code == 0 && R5_code == 0 && R6_code == 0 && R2_code == 0)
                    return new Inspection.InspectionResult(Inspection.RangeResult.Green);
                if (R1_code == 5 && R5_code == 0 && R6_code == 0 && R2_code == 0)
                    return new Inspection.InspectionResult(Inspection.RangeResult.None, "Частичные разряды.");
                if ((R1_code == 1 || R1_code == 2) && R5_code == 0 && R6_code == 0 && R2_code == 0)
                    return new Inspection.InspectionResult(Inspection.RangeResult.None, "Незначительный перегрев (<150°C).");
                if (R1_code == 0 && R5_code == 1 && R6_code == 0 && R2_code == 0)
                    return new Inspection.InspectionResult(Inspection.RangeResult.None, "Незначительный перегрев в диапазоне (200-300)°С.");
                if (R1_code == 0 && R5_code == 0 && R6_code == 1 && R2_code == 0)
                    return new Inspection.InspectionResult(Inspection.RangeResult.None, "Перегрев проводников.");
                if (R1_code == 1 && R5_code == 0 && R6_code == 1 && R2_code == 0)
                    return new Inspection.InspectionResult(Inspection.RangeResult.None, "Циркуляционные токи в обмотках.");
                if (R1_code == 1 && R5_code == 0 && R6_code == 2 && R2_code == 0)
                    return new Inspection.InspectionResult(Inspection.RangeResult.None, "Циркуляционные токи в магнитопроводе и баке, \"горячая\" точка.");
                if (R1_code == 0 && R5_code == 0 && R6_code == 0 && R2_code == 1)
                    return new Inspection.InspectionResult(Inspection.RangeResult.None, "Поверхностные разряды.");
                if (R1_code == 0 && R5_code == 0 && (R6_code == 1 || R6_code == 2) && (R2_code == 1 || R2_code == 2))
                    return new Inspection.InspectionResult(Inspection.RangeResult.None, "Дуговые процессы.");
                if (R1_code == 0 && R5_code == 0 && R6_code == 2 && R2_code == 2)
                    return new Inspection.InspectionResult(Inspection.RangeResult.None, "Продолжительное искрение, \"плавающий\" потенциал.");
                if (R1_code == 5 && R5_code == 0 && R6_code == 0 && (R2_code == 1 || R2_code == 2))
                    return new Inspection.InspectionResult(Inspection.RangeResult.None, "\"Ползущий\" разряд.");

                //if (CO2_CO > 11)
                //    return new Inspection.InspectionResult(Inspection.RangeResult.None, "Повышенная температура в твердой изоляции.");

                return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");
            }

            if (alg_type == Inspection.AlgorithmType.AT_StandardMEK)
            {
                double R1 = double.MaxValue;
                double R2 = double.MaxValue;
                double R6 = double.MaxValue;

                if (Math.Abs((double)H2.m_valNumber) > 0.0000009) R1 = (double)(CH4.m_valNumber / H2.m_valNumber);
                if (Math.Abs((double)C2H4.m_valNumber) > 0.0000009) R2 = (double)(C2H2.m_valNumber / C2H4.m_valNumber);
                if (Math.Abs((double)C2H6.m_valNumber) > 0.0000009) R6 = (double)(C2H4.m_valNumber / C2H6.m_valNumber);

                if (R1 < 0.1 && R6 < 0.2) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Частичные разряды.");
                if (R2 > 1 && R1 >= 0.1 && R1 <= 0.5 && R6 > 1) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Разряды низкой энергии.");
                if (R2 >= 0.6 && R2 <= 2.5 && R1 >= 0.1 && R1 <= 1.0 && R6 > 2) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Разряды высокой энергии.");
                if (R1 > 1 && R6 < 1) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Перегрев до 300°С.");
                if (R2 < 0.1 && R1 > 1 && R6 >= 1.0 && R6 <= 4.0) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Перегрев при 300-700°С.");
                if (R2 < 0.2 && R1 > 1.0 && R6 > 4.0) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Перегрев >700°С.");

                return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");
            }

            if (alg_type == Inspection.AlgorithmType.AT_MainGas)
            {
                if (CO.m_valNumber == null || CO2.m_valNumber == null) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");

                double fSum = (double)(H2.m_valNumber + CH4.m_valNumber + C2H2.m_valNumber + C2H4.m_valNumber + C2H6.m_valNumber + CO.m_valNumber + CO2.m_valNumber);

                if (Math.Abs(fSum) <= 0.0000009) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");

                double H2_proc = 100 * (double)H2.m_valNumber / fSum;
                double CH4_proc = 100 * (double)CH4.m_valNumber / fSum;
                double C2H2_proc = 100 * (double)C2H2.m_valNumber / fSum;
                double C2H4_proc = 100 * (double)C2H4.m_valNumber / fSum;
                double C2H6_proc = 100 * (double)C2H6.m_valNumber / fSum;
                double CO_proc = 100 * (double)CO.m_valNumber / fSum;

                if (H2_proc >= 60 && C2H2_proc >= 30) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Дуга.");
                if (H2_proc >= 85 && CH4_proc >= 15) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Частичные разряды.");
                if (C2H4_proc >= 63 && C2H6_proc >= 20) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Перегрев масла.");
                if (CO_proc >= 92) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Перегрев бумажной изоляции.");

                return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");
            }

            if (alg_type == Inspection.AlgorithmType.AT_Normogramma)
            {
                List<NormogrammaResultInfo> listNormograms = new List<NormogrammaResultInfo>();
                for (int i = 0; i < 13; i++)
                {
                    switch (i)
                    {
                        case 0:
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты, вызванные разрядами, ЧР с низкой плотностью энергии.")));
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0.06);
                            listNormograms[i].listGases.Add(0.04);
                            listNormograms[i].listGases.Add(0.02);
                            listNormograms[i].listGases.Add(0);
                            break;
                        case 1:
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты, вызванные разрядами, разряды малой мощности.")));
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0.4);
                            listNormograms[i].listGases.Add(0.1);
                            listNormograms[i].listGases.Add(0.15);
                            listNormograms[i].listGases.Add(0.2);
                            break;
                        case 2:
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты, вызванные разрядами, дефекты, вызванные искрением.")));
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0.5);
                            listNormograms[i].listGases.Add(0.1);
                            listNormograms[i].listGases.Add(0.7);
                            listNormograms[i].listGases.Add(0.4);
                            break;
                        case 3:
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты, вызванные разрядами, дефекты, вызванные дугой.")));
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0.25);
                            listNormograms[i].listGases.Add(0);
                            listNormograms[i].listGases.Add(0.4);
                            listNormograms[i].listGases.Add(0.75);
                            break;
                        case 4:
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты, вызванные разрядами, дефекты, вызванные дугой.")));
                            listNormograms[i].listGases.Add(0.8);
                            listNormograms[i].listGases.Add(0.15);
                            listNormograms[i].listGases.Add(0.05);
                            listNormograms[i].listGases.Add(0.1);
                            listNormograms[i].listGases.Add(1);
                            break;
                        case 5: 
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты термического характера в диапазоне средних температур, дефекты термического характера, переходящие в ЧР.")));
                            listNormograms[i].listGases.Add(0.5);
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0.3);
                            listNormograms[i].listGases.Add(0.5);
                            listNormograms[i].listGases.Add(0);
                            break;
                        case 6:
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты термического характера в диапазоне средних температур, дефекты термического характера, переходящие в ЧР.")));
                            listNormograms[i].listGases.Add(0.1);
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0.15);
                            listNormograms[i].listGases.Add(0.5);
                            listNormograms[i].listGases.Add(0);
                            break;
                        case 7:
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты термического характера, нагрев, переходящий в дугу.")));
                            listNormograms[i].listGases.Add(0.8);
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0.1);
                            listNormograms[i].listGases.Add(0.8);
                            listNormograms[i].listGases.Add(0.5);
                            break;
                        case 8: 
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты термического характера в диапазоне средних температур, дефекты термического характера, переходящие в ЧР.")));
                            listNormograms[i].listGases.Add(0.55);
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0.2);
                            listNormograms[i].listGases.Add(0.1);
                            listNormograms[i].listGases.Add(0);
                            break;
                        case 9:
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты термического характера в диапазоне высоких температур, высокотемпературный нагрев, переходящий в дугу.")));
                            listNormograms[i].listGases.Add(0.8);
                            listNormograms[i].listGases.Add(0.9);
                            listNormograms[i].listGases.Add(0.1);
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0.4);
                            break;
                        case 10: 
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты термического характера в диапазоне высоких температур.")));
                            listNormograms[i].listGases.Add(0.04);
                            listNormograms[i].listGases.Add(0.2);
                            listNormograms[i].listGases.Add(0);
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0.1);
                            break;
                        case 11: 
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты термического характера в диапазоне высоких температур.")));
                            listNormograms[i].listGases.Add(0);
                            listNormograms[i].listGases.Add(0.25);
                            listNormograms[i].listGases.Add(0.5);
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0);
                            break;
                        case 12:
                            listNormograms.Add(new NormogrammaResultInfo(new Inspection.InspectionResult(Inspection.RangeResult.None, "Дефекты термического характера, нагрев переходящий в ЧР.")));
                            listNormograms[i].listGases.Add(0.9);
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add(0.2);
                            listNormograms[i].listGases.Add(1);
                            listNormograms[i].listGases.Add( 0.3);
                            break;
                    }
                }

                List<double> listGases = new List<double>();

                double max_val = MaxVal((double)H2.m_valNumber, (double)CH4.m_valNumber, (double)C2H6.m_valNumber, (double)C2H4.m_valNumber, (double)C2H2.m_valNumber);

                if (Math.Abs(max_val) <= 0.0000009) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");

                listGases.Add((double)H2.m_valNumber / max_val);
                listGases.Add((double)CH4.m_valNumber / max_val);
                listGases.Add((double)C2H6.m_valNumber / max_val);
                listGases.Add((double)C2H4.m_valNumber / max_val);
                listGases.Add((double)C2H2.m_valNumber / max_val);

                double maxVal = double.MinValue;
                int index = -1;
                for (int i = 0; i < listNormograms.Count; i++)
                {
                    listNormograms[i].koef = CalcKoef(listGases, listNormograms[i].listGases);
                    if (!double.IsInfinity(listNormograms[i].koef) && !double.IsNaN(listNormograms[i].koef))
                    {
                        if (maxVal < listNormograms[i].koef)
                        {
                            maxVal = listNormograms[i].koef;
                            index = i;
                        }
                    }
                }

                if (index >= 0 && index < listNormograms.Count) return listNormograms[index].m_result;
                else return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");
            }

            if (alg_type == Inspection.AlgorithmType.AT_Duval)
            {
                double fSum = (double)CH4.m_valNumber + (double)C2H4.m_valNumber + (double)C2H2.m_valNumber;

                if (Math.Abs(fSum) <= 0.0000009) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Диагноз не определен.");

                double CH4_proc = 100 * (double)CH4.m_valNumber / fSum;
                double C2H4_proc = 100 * (double)C2H4.m_valNumber / fSum;
                double C2H2_proc = 100 * (double)C2H2.m_valNumber / fSum;

                if (CH4_proc >= 98) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Частичный разряд."); // PD
                else
                {
                    if (C2H2_proc <= 4 && C2H4_proc <= 10) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Перегрев, T<300°C."); // T1
                    else
                    {
                        if (C2H2_proc <= 4 && C2H4_proc <= 50) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Перегрев, 300°C<T<700°C."); // T2
                        else
                        {
                            if (C2H2_proc <= 15 && C2H4_proc > 50) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Перегрев, T>700°C."); // T3
                            else
                            {
                                if (C2H2_proc >= 13 && C2H4_proc <= 23) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Разряды низкой энергии."); // D1
                                else
                                {
                                    if (C2H2_proc >= 29 && C2H4_proc > 23 || C2H2_proc >= 13 && C2H2_proc <= 29 && C2H4_proc > 23 && C2H4_proc <= 38) return new Inspection.InspectionResult(Inspection.RangeResult.None, "Разряды высокой энергии."); // D2
                                    else
                                    {
                                        return new Inspection.InspectionResult(Inspection.RangeResult.None, "Комбинация электрических и термических дефектов."); // D+T
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return new Inspection.InspectionResult(Inspection.RangeResult.None, "Неизвестный алгоритм.");
        }

        static public Inspection.RangeResult Defect_HARG_3(InspectionDataForm parent_form, InspectionDataForm.FunctionalSystem fs, InspectionDataForm.SystemItem si, List<long> listAlgorithmIDs,
            out bool bNeedDisableAlgorithm, out List<Inspection.InspectionResult> results)
        {
            bNeedDisableAlgorithm = false;
            results = null;

            InspectionDataForm.Parameter Unit = null;
            InspectionDataForm.Parameter H2 = null;
            InspectionDataForm.Parameter CH4 = null;
            InspectionDataForm.Parameter C2H2 = null;
            InspectionDataForm.Parameter C2H4 = null;
            InspectionDataForm.Parameter C2H6 = null;
            InspectionDataForm.Parameter CO = null;
            InspectionDataForm.Parameter CO2 = null;
            InspectionDataForm.Parameter Votn = null;

            for (int i = 0; i < si.m_listParams.Count; i++)
            {
                if (si.m_listParams[i].m_strParameterName == "Unit") Unit = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "H2") H2 = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "CH4") CH4 = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "C2H2") C2H2 = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "C2H4") C2H4 = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "C2H6") C2H6 = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "CO") CO = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "CO2") CO2 = si.m_listParams[i];
                if (si.m_listParams[i].m_strParameterName == "Votn") Votn = si.m_listParams[i];
            }

            if (Unit == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"Единица измерения\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (CH4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CH4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (C2H2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (C2H4 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H4\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (C2H6 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"C2H6\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (CO == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CO\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (CO2 == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"CO2\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }
            if (Votn == null)
            {
                MyLocalizer.XtraMessageBoxShow("Не удалось найти параметр \"V отн\"", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return Inspection.RangeResult.None;
            }

            double mult = 1;
            if (Unit.m_valSelect != null && Unit.m_valSelect != (Inspection.RangeResult?)0) mult = 10000;

            if (Votn.m_valNumber != null && Votn.m_valNumber > 10)
            {
                bNeedDisableAlgorithm = true;
                return Inspection.RangeResult.Red;
            }

            if (H2.m_valNumber == null
                || CH4.m_valNumber == null
                || C2H2.m_valNumber == null
                || C2H4.m_valNumber == null
                || C2H6.m_valNumber == null)
            {
                bNeedDisableAlgorithm = true;
                return Inspection.RangeResult.None;
            }

            if (!H2.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_RD)
                || !CH4.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_RD)
                || !C2H2.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_RD)
                || !C2H4.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_RD)
                || !C2H6.m_dictAlgLimits.ContainsKey((long)Inspection.AlgorithmType.AT_RD))
            {
                bNeedDisableAlgorithm = true;
                return Inspection.RangeResult.None;
            }

            long? VoltageValue = parent_form.m_VoltageValue;
            long? YearsValue = parent_form.m_YearsValue;
            long? ProtectionOilType = parent_form.m_ProtectionOilType;

            if (H2.m_valNumber * mult <= H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                CH4.m_valNumber * mult <= CH4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                C2H2.m_valNumber * mult <= C2H2.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                C2H4.m_valNumber * mult <= C2H4.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD) &&
                C2H6.m_valNumber * mult <= C2H6.GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD))
            {
                bNeedDisableAlgorithm = true;
                return Inspection.RangeResult.Green;
            }

            if (listAlgorithmIDs.Count == 0) listAlgorithmIDs.Add(1); // по умолчанию применяем РД

            Inspection.RangeResult common_result = Inspection.RangeResult.None;
            results = new List<Inspection.InspectionResult>();
            for (int alg_ind = 0; alg_ind < listAlgorithmIDs.Count; alg_ind++)
            {
                Inspection.InspectionResult result = new Inspection.InspectionResult(Inspection.RangeResult.None);
                result = Defect_HARG_Alg((Inspection.AlgorithmType)listAlgorithmIDs[alg_ind], parent_form, Unit, H2, CH4, C2H2, C2H4, C2H6, CO, CO2);
                result.m_AlgorithmID = listAlgorithmIDs[alg_ind];
                results.Add(result);
            }

            // подсчитываем итоговый результат, по кол-ву
            Dictionary<Inspection.RangeResult, int> dictResults = new Dictionary<Inspection.RangeResult,int>();
            foreach (Inspection.RangeResult rr in Enum.GetValues(typeof(Inspection.RangeResult)))
                dictResults[rr] = 0;

            int max_cnt = 0;
            for (int i = 0; i < results.Count; i++)
            {
                dictResults[results[i].m_RangeResult]++;
                if (max_cnt < dictResults[results[i].m_RangeResult] && results[i].m_RangeResult != Inspection.RangeResult.None)
                {
                    max_cnt = dictResults[results[i].m_RangeResult];
                    common_result = results[i].m_RangeResult;
                }
            }

            return common_result;
        }
        */
    }
}
