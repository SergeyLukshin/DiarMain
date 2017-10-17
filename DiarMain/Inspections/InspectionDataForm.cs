 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SQLite;
using DevExpress.Utils;

namespace DiarMain
{
    public partial class InspectionDataForm : DevExpress.XtraEditors.XtraForm
    {
        public class InspectionSubType
        {
            public long m_ID;
            public string m_strName;

            public InspectionSubType(long id, string strName)
            {
                m_ID = id;
                m_strName = strName;
            }
        }

        public class Algorithm
        {
            public long m_ID;
            public string m_strName;

            public Algorithm(long id, string strName)
            {
                m_ID = id;
                m_strName = strName;
            }
        }

        public class Defect
        {
            public Defect(long DefectID, long? InspectionSubType, string strDefectName)
            {
                m_DefectID = DefectID;
                m_InspectionSubType = InspectionSubType;
                m_strDefectName = strDefectName;
            }

            public long m_DefectID;
            public long? m_InspectionSubType;
            public string m_strDefectName;
        }

        public class Parameter : ICloneable
        {
            public object Clone()
            {
                //return this.MemberwiseClone();
                Parameter p = new Parameter();
                p.m_ParameterID = m_ParameterID;
                p.m_strParameterDescript = m_strParameterDescript;
                p.m_strParameterName = m_strParameterName;
                p.m_ParameterType = m_ParameterType;

                p.m_valSelect = m_valSelect;
                p.m_valNumber = m_valNumber;
                p.m_valString = m_valString;
                p.m_valDate = m_valDate;
                p.m_valRange = m_valRange;
                p.m_InspectionSubType = m_InspectionSubType;

                p.m_minVal = m_minVal;
                p.m_maxVal = m_maxVal;
                p.m_notEqualVal = m_notEqualVal;
                p.m_defVal = m_defVal;
                p.m_bReadOnly = m_bReadOnly;
                p.m_DefectID = m_DefectID;
                //p.m_strDefectName = m_strDefectName;
                
                p.m_Row = null;

                p.m_CalcAlgorithm = null;
                if (m_CalcAlgorithm != null) p.m_CalcAlgorithm = (CalcAlgorithm)m_CalcAlgorithm.Clone();

                p.m_listLimits = null;
                if (m_listLimits != null)
                {
                    p.m_listLimits = new List<LimitRange>();
                    for (int i = 0; i < m_listLimits.Count; i++)
                    {
                        p.m_listLimits.Add((LimitRange)m_listLimits[i].Clone());
                    }
                }

                p.m_dicParameterSelect = null;
                if (m_dicParameterSelect != null)
                {
                    p.m_dicParameterSelect = new Dictionary<int, string>();
                    foreach (KeyValuePair<int, string> pair in m_dicParameterSelect)
                    {
                        p.m_dicParameterSelect[pair.Key] = pair.Value;
                    }
                }

                p.m_dictAlgLimits = null;
                if (m_dictAlgLimits != null)
                {
                    p.m_dictAlgLimits = new Dictionary<long, List<LimitAlg>>();
                    foreach (KeyValuePair<long, List<LimitAlg>> pair in m_dictAlgLimits)
                    {
                        p.m_dictAlgLimits[pair.Key] = new List<LimitAlg>();
                        for (int i = 0; i < pair.Value.Count; i++)
                        {
                            p.m_dictAlgLimits[pair.Key].Add((LimitAlg)pair.Value[i].Clone());
                        }
                    }
                }

                p.m_listDepends = null;
                if (m_listDepends != null)
                {
                    p.m_listDepends = new List<KeyValuePair<long, long> >();
                    for (int i = 0; i < m_listDepends.Count; i++)
                    {
                        p.m_listDepends.Add(m_listDepends[i]);
                    }
                }

                return p;
            }

            public class CalcAlgorithm : ICloneable
            {
                public object Clone()
                {
                    CalcAlgorithm ca = new CalcAlgorithm();

                    ca.m_algType = m_algType;
                    ca.m_AlgorithmName = m_AlgorithmName;
                    ca.m_AlgorithmExpression = m_AlgorithmExpression;

                    ca.m_arrParameterIDs = null;
                    if (m_arrParameterIDs != null)
                    {
                        ca.m_arrParameterIDs = new List<long>();
                        //ca.m_arrParameterIDs.InsertRange(0, m_arrParameterIDs);
                        for (int i = 0; i < m_arrParameterIDs.Count; i++)
                        {
                            ca.m_arrParameterIDs.Add(m_arrParameterIDs[i]);
                        }
                    }

                    return ca;
                }

                public Inspection.CalcType m_algType;
                public string m_AlgorithmName;
                public List<long> m_arrParameterIDs;
                public string m_AlgorithmExpression;
            }

            public class LimitAlg : ICloneable
            {
                public object Clone()
                {
                    return this.MemberwiseClone();
                }

                public LimitAlg(long? VoltageMin, long? VoltageMax, long? YearsMin, long? YearsMax, long? ProtectionOilType, double ValueMax)
                {
                    maxVal = ValueMax;
                    m_VoltageMin = VoltageMin;
                    m_VoltageMax = VoltageMax;
                    m_YearsMin = YearsMin;
                    m_YearsMax = YearsMax;
                    m_ProtectionOilType = ProtectionOilType;
                }

                public double maxVal;
                public long? m_VoltageMin;
                public long? m_VoltageMax;
                public long? m_YearsMin;
                public long? m_YearsMax;
                public long? m_ProtectionOilType;
            }

            public class LimitRange : ICloneable
            {
                public object Clone()
                {
                    return this.MemberwiseClone();
                }

                public LimitRange(long? VoltageMin, long? VoltageMax,
                    long? ProtectionOilType, long? InputVoltageType,
                    long? YearsMin, long? YearsMax,
                    double? ValueMin, double? ValueMax,
                    long? RangeValue)
                {
                    minVal = ValueMin;
                    maxVal = ValueMax;

                    m_VoltageMin = VoltageMin;
                    m_VoltageMax = VoltageMax;
                    m_ProtectionOilType = ProtectionOilType;
                    m_InputVoltageType = InputVoltageType;

                    m_YearsMin = YearsMin;
                    m_YearsMax = YearsMax;
                    m_RangeValue = RangeValue;
                }

                public double? minVal;
                public double? maxVal;

                public long? m_VoltageMin;
                public long? m_VoltageMax;
                public long? m_ProtectionOilType;
                public long? m_InputVoltageType;
                public long? m_YearsMin;
                public long? m_YearsMax;

                public long? m_RangeValue;
            }

            public Parameter()
            {
            }

            public Parameter(InspectionDataForm parent, long SystemItemID, long ParameterID, string strParameterDescript, string strParameterName, Inspection.ParameterType iParameterType, 
                string strParameterSelect1, string strParameterSelect2, string strParameterSelect3, string strParameterSelect4,
                string strParameterAlgorithm, long? InspectionSubType, double? minVal, double? maxVal, double? notEqualVal, double? defVal, long? DefectID, bool bNotEmpty)
            {
                m_ParameterID = ParameterID;
                m_strParameterDescript = strParameterDescript;
                m_strParameterName = strParameterName;
                m_ParameterType = iParameterType;
                m_dicParameterSelect[0] = strParameterSelect1;
                m_dicParameterSelect[1] = strParameterSelect2;
                m_dicParameterSelect[2] = strParameterSelect3;
                m_dicParameterSelect[3] = strParameterSelect4;

                m_valSelect = null;
                m_valNumber = null;
                m_valString = null;
                m_valDate = null;
                m_valRange = Inspection.RangeResult.None;
                m_Row = null;

                m_DefectID = DefectID;
                m_bNotEmpty = bNotEmpty;

                //if (m_ParameterType == Inspection.ParameterType.Select && m_DefectID == null) m_strDefectName = m_strParameterDescript;
                //else m_strDefectName = strDefectName;

                m_bReadOnly = false;

                string[] arr = strParameterAlgorithm.Split('_');
                if (arr.Length >= 2)
                {
                    if (arr[0].IndexOf("calc") >= 0)
                    {
                        m_CalcAlgorithm = new CalcAlgorithm();
                        if (arr[0] == "calc")
                        {
                            m_CalcAlgorithm.m_algType = Inspection.CalcType.Calc;
                            m_bReadOnly = true;
                        }
                        if (arr[0] == "calcifall")
                        {
                            m_CalcAlgorithm.m_algType = Inspection.CalcType.Calc_if_all;
                        }
                        if (arr[0] == "calcifempty")
                        {
                            m_CalcAlgorithm.m_algType = Inspection.CalcType.Calc_if_empty;
                        }
                        m_CalcAlgorithm.m_AlgorithmName = arr[1];

                        m_CalcAlgorithm.m_arrParameterIDs = new List<long>();

                        if (m_CalcAlgorithm.m_AlgorithmName != "exp")
                        {
                            for (int i = 2; i < arr.Length; i++)
                            {
                                m_CalcAlgorithm.m_arrParameterIDs.Add(Convert.ToInt32(arr[i]));
                            }
                        }
                        else
                        {
                            if (arr.Length >= 3)
                            {
                                m_CalcAlgorithm.m_AlgorithmExpression = arr[2];
                                string strTmp = arr[2];
                                string[] delim = { "id", "/", "+", "-", "^", "*", "(", ")" };
                                string[] arr_ = strTmp.Split(delim, 100, StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < arr_.Length; i++)
                                {
                                    m_CalcAlgorithm.m_arrParameterIDs.Add(Convert.ToInt32(arr_[i]));
                                }
                            }
                        }
                    }
                    if (arr[0].IndexOf("depends") >= 0)
                    {
                        // depends_sidfrom_sidpidfor1_sidpidfor2_...
                        // sidpidfor = sid,pid
                        if (Convert.ToInt64(arr[1]) == SystemItemID)
                        {
                            m_listDepends = new List<KeyValuePair<long, long> >();

                            for (int i = 2; i < arr.GetLength(0); i++)
                            {
                                string strTmp = arr[i];
                                string[] arr_ = strTmp.Split(',');
                                if (arr_.GetLength(0) == 2)
                                {
                                    long sid = Convert.ToInt64(arr_[0]);
                                    long pid = Convert.ToInt64(arr_[1]);
                                    m_listDepends.Add(new KeyValuePair<long, long>(sid, pid));
                                    if (!parent.dictDependsParameters.ContainsKey(sid)) parent.dictDependsParameters[sid] = new List<long>();
                                    parent.dictDependsParameters[sid].Add(pid);
                                }
                            }
                        }
                    }
                }

                m_InspectionSubType = InspectionSubType;

                m_minVal = minVal;
                m_maxVal = maxVal;
                m_notEqualVal = notEqualVal;
                m_defVal = defVal;
            }

            public double GetMaxValue(long? VoltageValue, long? YearsValue, long? ProtectionOilType, Inspection.AlgorithmType alg_type)
            {
                if (!m_dictAlgLimits.ContainsKey((long)alg_type)) return double.MaxValue;

                for (int i = 0; i < m_dictAlgLimits[(long)alg_type].Count; i++)
                {
                    if (m_dictAlgLimits[(long)alg_type][i].m_VoltageMax == null && m_dictAlgLimits[(long)alg_type][i].m_VoltageMin == null ||
                        m_dictAlgLimits[(long)alg_type][i].m_VoltageMax >= VoltageValue && m_dictAlgLimits[(long)alg_type][i].m_VoltageMin <= VoltageValue)
                    {
                        if (m_dictAlgLimits[(long)alg_type][i].m_YearsMax == null && m_dictAlgLimits[(long)alg_type][i].m_YearsMin == null ||
                            m_dictAlgLimits[(long)alg_type][i].m_YearsMax >= YearsValue && m_dictAlgLimits[(long)alg_type][i].m_YearsMin <= YearsValue)
                        {
                            if (m_dictAlgLimits[(long)alg_type][i].m_ProtectionOilType == null ||
                                m_dictAlgLimits[(long)alg_type][i].m_ProtectionOilType == ProtectionOilType)
                            {
                                return m_dictAlgLimits[(long)alg_type][i].maxVal;
                            }
                        }
                    }
                }
                return double.MaxValue;
            }

            public Inspection.RangeResult CalcResultRange(long? VoltageValue, long? ProtectionOilType, long? InputVoltageType, long? YearsValue)
            {
                if (m_valNumber == null) return Inspection.RangeResult.None;

                for (int i = 0; i < m_listLimits.Count; i++)
                {
                    if (m_listLimits[i].minVal == null && m_listLimits[i].maxVal == null || m_listLimits[i].m_RangeValue == null)
                        continue;

                    if (m_listLimits[i].m_VoltageMax == null && m_listLimits[i].m_VoltageMin == null ||
                        m_listLimits[i].m_VoltageMax >= VoltageValue && m_listLimits[i].m_VoltageMin <= VoltageValue)
                    {
                        if (m_listLimits[i].m_YearsMax == null && m_listLimits[i].m_YearsMin == null ||
                            m_listLimits[i].m_YearsMax >= YearsValue && m_listLimits[i].m_YearsMin <= YearsValue)
                        {
                            if (m_listLimits[i].m_ProtectionOilType == null || m_listLimits[i].m_ProtectionOilType == ProtectionOilType)
                            {
                                if (m_listLimits[i].m_InputVoltageType == null || m_listLimits[i].m_InputVoltageType == InputVoltageType)
                                {
                                    if ((m_listLimits[i].minVal == null || m_valNumber >= m_listLimits[i].minVal)
                                        && (m_listLimits[i].maxVal == null || m_valNumber < m_listLimits[i].maxVal))
                                    {
                                        return (Inspection.RangeResult)m_listLimits[i].m_RangeValue;
                                    }
                                }
                            }
                        }
                    }
                }

                return Inspection.RangeResult.None;
            }

            public long m_ParameterID;
            public string m_strParameterDescript;
            public string m_strParameterName;
            public Inspection.ParameterType m_ParameterType;
            public Dictionary<int, string> m_dicParameterSelect = new Dictionary<int, string>();

            public Inspection.RangeResult? m_valSelect;
            public double? m_valNumber;
            public string m_valString;
            public DateTime? m_valDate;
            public Inspection.RangeResult m_valRange; // значение рассчитывается на основании данных

            public CalcAlgorithm m_CalcAlgorithm = null;
            //public Inspection.CalculatedFieldType m_CalculatedField;

            public long? m_InspectionSubType;

            public DevExpress.XtraVerticalGrid.Rows.MultiEditorRow m_Row;
            public List<LimitRange> m_listLimits = new List<LimitRange>();
            public Dictionary<long, List<LimitAlg> > m_dictAlgLimits = new Dictionary<long, List<LimitAlg> >();
            public List<KeyValuePair<long, long> > m_listDepends = null;

            public double? m_minVal;
            public double? m_maxVal;
            public double? m_notEqualVal;
            public double? m_defVal;

            //public string m_strDefectName;
            public long? m_DefectID;

            public bool m_bReadOnly;
            public bool m_bNotEmpty;
        }

        public class SystemItem
        {
            public class DefectAlgorithm
            {
                public string m_AlgorithmName;
                //public List<long> m_arrParameterIDs;
                public List<long> m_arrSystemItemIDs;
            }

            public SystemItem(InspectionDataForm parent, long SystemItemID, string strSystemItemName, long? InputVoltageIndex, string strSystemItemAlgorithm, long VoltageType)
            {
                m_SystemItemID = SystemItemID;
                m_strSystemItemName = strSystemItemName;
                m_InputVoltageIndex = InputVoltageIndex;

                switch (VoltageType)
                {
                    case 0:
                        bIsVoltageVN = true;
                        bIsVoltageSN = false;
                        bIsVoltageRPN = false;
                        bIsVoltageNeitral = false;
                        break;
                    case 1:
                        bIsVoltageVN = false;
                        bIsVoltageSN = true;
                        bIsVoltageRPN = false;
                        bIsVoltageNeitral = false;
                        break;
                    case 2:
                        bIsVoltageVN = false;
                        bIsVoltageSN = false;
                        bIsVoltageRPN = true;
                        bIsVoltageNeitral = false;
                        break;
                    case 3:
                        bIsVoltageVN = false;
                        bIsVoltageSN = false;
                        bIsVoltageRPN = false;
                        bIsVoltageNeitral = true;
                        break;
                }

                m_dictValRange[0] = new Inspection.InspectionResult(Inspection.RangeResult.None);
                for (int i = 0; i < parent.m_list_sub_types.Count; i++)
                {
                    m_dictValRange[parent.m_list_sub_types[i].m_ID] = new Inspection.InspectionResult(Inspection.RangeResult.None);
                }
                //m_dictValRange[Inspection.InspectionSubType.Main] = Inspection.RangeResult.None;
                //m_dictValRange[Inspection.InspectionSubType.Extra] = Inspection.RangeResult.None;
                //m_dictCalcType[Inspection.InspectionSubType.Main] = Inspection.CalcType.Selector;
                //m_dictCalcType[Inspection.InspectionSubType.Extra] = Inspection.CalcType.Selector;

                string[] arr = strSystemItemAlgorithm.Split('_');
                if (arr.Length >= 2)
                {
                    m_DefectAlgorithm = new DefectAlgorithm();
                    m_DefectAlgorithm.m_AlgorithmName = arr[1];

                    //m_DefectAlgorithm.m_arrParameterIDs = new List<long>();
                    m_DefectAlgorithm.m_arrSystemItemIDs = new List<long>();

                    for (int i = 2; i < arr.Length; i++)
                    {
                        m_DefectAlgorithm.m_arrSystemItemIDs.Add(Convert.ToInt32(arr[i]));

                        //if (arr[i].IndexOf("s") == 0)
                        //{
                            //int sid = Convert.ToInt32(arr[i].Replace("s", ""));
                        /*}
                        else
                        {
                            m_DefectAlgorithm.m_arrParameterIDs.Add(Convert.ToInt32(arr[i]));
                        }*/
                    }
                }
            }

            public void CalcFieldValue(InspectionDataForm parent_form, int ParamIndex)
            {
                List<KeyValuePair<long, Parameter>> arrValues = new List<KeyValuePair<long, Parameter>>();
                // собираем значения нужных параметров

                Parameter.CalcAlgorithm ca = m_listParams[ParamIndex].m_CalcAlgorithm;

                if (ca.m_algType == Inspection.CalcType.Calc || ca.m_algType == Inspection.CalcType.Calc_if_all)
                {
                    for (int i = 0; i < ca.m_arrParameterIDs.Count; i++)
                    {
                        for (int j = 0; j < m_listParams.Count; j++)
                        {
                            if (m_listParams[j].m_InspectionSubType != null &&
                                m_listParams[j].m_InspectionSubType != parent_form.m_sub_type_id)
                            {
                                continue;
                            }

                            if (m_listParams[j].m_ParameterID == ca.m_arrParameterIDs[i])
                            {
                                arrValues.Add(new KeyValuePair<long, Parameter>(ca.m_arrParameterIDs[i], m_listParams[j]));
                            }
                        }
                    }

                    if (ca.m_AlgorithmName == "votn")
                    {
                        long FunctionalSystemID = parent_form.dictSystemItemsFunctionalSystems[this.m_SystemItemID];
                        int FunctionalSystemPos = parent_form.dictFunctionalSystemPos[FunctionalSystemID];

                        string strParameterName = "";
                        double? val = CalcAlgorithms.Calc_Votn(parent_form, parent_form.m_list[FunctionalSystemPos], this, ref strParameterName);

                        if (m_listParams[ParamIndex].m_valNumber == null && val != null ||
                            m_listParams[ParamIndex].m_valNumber != null && val == null ||
                            m_listParams[ParamIndex].m_valNumber != null && val != null && m_listParams[ParamIndex].m_valNumber != val)
                            parent_form.m_bChangeData = true;

                        m_listParams[ParamIndex].m_valNumber = val;
                        m_listParams[ParamIndex].m_Row.Properties.Value = val;

                        // ищем столбец с названием "Votn_gas" и заносим данные по ключевому газу
                        for (int i = ParamIndex + 1; i < this.m_listParams.Count; i++)
                        {
                            if (m_listParams[i].m_strParameterName == "Votn_gas")
                            {
                                if (strParameterName == "")
                                {
                                    m_listParams[i].m_valString = null;
                                    m_listParams[i].m_Row.Properties.Value = null;
                                }
                                else
                                {
                                    m_listParams[i].m_valString = strParameterName;
                                    m_listParams[i].m_Row.Properties.Value = strParameterName;
                                }
                                break;
                            }
                        }

                        //m_listParams[ParamIndex].m_Row.PropertiesCollection[1].Caption = "ключевой газ";
                        //m_listParams[ParamIndex].m_Row.PropertiesCollection[1].Value = strParameterName;
                        /*
                        if (strParameterName != "")
                            m_listParams[ParamIndex].m_Row.Properties.Caption = m_listParams[ParamIndex].m_strParameterDescript + " (расчет производится по газу " + strParameterName + ")";
                        else
                            m_listParams[ParamIndex].m_Row.Properties.Caption = m_listParams[ParamIndex].m_strParameterDescript;
                        */ 
                    }

                    if (ca.m_AlgorithmName == "exp")
                    {
                        if (ca.m_algType == Inspection.CalcType.Calc_if_all) // если хотя бы одно значение не заполнено, то поле доступно для редактирования
                        {
                            bool bAllExists = true;
                            for (int i = 0; i < arrValues.Count; i++)
                            {
                                if (arrValues[i].Value == null || arrValues[i].Value.m_valNumber == null)
                                {
                                    bAllExists = false;
                                    break;
                                }
                            }
                            if (bAllExists)
                            {
                                m_listParams[ParamIndex].m_bReadOnly = true;
                                m_listParams[ParamIndex].m_Row.Appearance.BackColor = Color.FromArgb(240, 240, 240);
                                m_listParams[ParamIndex].m_Row.Appearance.Options.UseBackColor = true;
                            }
                            else
                            {
                                m_listParams[ParamIndex].m_bReadOnly = false;
                                m_listParams[ParamIndex].m_Row.Appearance.Options.UseBackColor = false;
                                return;
                            }
                        }

                        Lexems lex = new Lexems();
                        string postfix = lex.StandartToRPN(ca.m_AlgorithmExpression);
                        Lexems.RPN rpn = lex.CreateRPN(postfix);
                        for (int i = 0; i < arrValues.Count; i++)
                        {
                            if (arrValues[i].Value != null && arrValues[i].Value.m_valNumber != null)
                                rpn.SetVariable("id" + arrValues[i].Key.ToString(), (double)arrValues[i].Value.m_valNumber);
                            else
                                rpn.SetVariable("id" + arrValues[i].Key.ToString(), double.NaN);
                        }
                        rpn.Execute();
                        if (double.IsNaN(rpn.Result))
                        {
                            if (m_listParams[ParamIndex].m_valNumber != null)
                                parent_form.m_bChangeData = true;

                            m_listParams[ParamIndex].m_valNumber = null;
                            m_listParams[ParamIndex].m_Row.Properties.Value = null;
                        }
                        else
                        {
                            double val = Round(rpn.Result, 6);

                            if (m_listParams[ParamIndex].m_valNumber == null ||
                                m_listParams[ParamIndex].m_valNumber != null && m_listParams[ParamIndex].m_valNumber != val)
                                parent_form.m_bChangeData = true;

                            m_listParams[ParamIndex].m_valNumber = val;
                            m_listParams[ParamIndex].m_Row.Properties.Value = val;
                        }
                    }

                    if (ca.m_algType == Inspection.CalcType.Calc_if_empty)
                    {
                        bool bFind = false;
                        for (int i = 0; i < arrValues.Count; i++)
                        {
                            if (arrValues[i].Value != null && (arrValues[i].Value.m_valNumber != null 
                                || arrValues[i].Value.m_valSelect != null 
                                || arrValues[i].Value.m_valString != null
                                || arrValues[i].Value.m_valDate != null))
                            {
                                if (m_listParams[ParamIndex].m_valNumber != null
                                    || m_listParams[ParamIndex].m_valSelect != null
                                    || m_listParams[ParamIndex].m_valString != null
                                    || m_listParams[ParamIndex].m_valDate != null)
                                    parent_form.m_bChangeData = true;

                                m_listParams[ParamIndex].m_valNumber = null;
                                m_listParams[ParamIndex].m_valSelect = null;
                                m_listParams[ParamIndex].m_valString = null;
                                m_listParams[ParamIndex].m_valDate = null;
                                m_listParams[ParamIndex].m_Row.Properties.Value = null;

                                m_listParams[ParamIndex].m_bReadOnly = true;
                                m_listParams[ParamIndex].m_Row.Appearance.BackColor = Color.FromArgb(240, 240, 240);
                                m_listParams[ParamIndex].m_Row.Appearance.Options.UseBackColor = true;

                                bFind = true;
                                break;
                            }
                        }
                        if (!bFind)
                        {
                            m_listParams[ParamIndex].m_bReadOnly = false;
                            m_listParams[ParamIndex].m_Row.Appearance.Options.UseBackColor = false;
                        }
                    }

                    // проверка
                    /*if (m_listParams[ParamIndex].m_valNumber != null)
                    {
                        if (m_listParams[ParamIndex].m_maxVal != null && m_listParams[ParamIndex].m_valNumber > m_listParams[ParamIndex].m_maxVal ||
                            m_listParams[ParamIndex].m_minVal != null && m_listParams[ParamIndex].m_valNumber < m_listParams[ParamIndex].m_minVal)
                        {
                            if (m_listParams[ParamIndex].m_defVal != null)
                            {
                                m_listParams[ParamIndex].m_valNumber = m_listParams[ParamIndex].m_defVal;
                                m_listParams[ParamIndex].m_Row.Properties.Value = m_listParams[ParamIndex].m_defVal;

                                if (this.m_Row.ParentRow.Visible) // для скрытых строк не надо показывать сообщение (при объединении узлов)
                                    MyLocalizer.XtraMessageBoxShow("Значение параметра \"" + m_listParams[ParamIndex].m_strParameterDescript + "\" выходит за пределы допустимых значений.\nПараметру будет присвоено значение " + m_listParams[ParamIndex].m_defVal.ToString() + ".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                m_listParams[ParamIndex].m_valNumber = null;
                                m_listParams[ParamIndex].m_Row.Properties.Value = null;

                                if (this.m_Row.ParentRow.Visible) // для скрытых строк не надо показывать сообщение (при объединении узлов)
                                    MyLocalizer.XtraMessageBoxShow("Значение параметра \"" + m_listParams[ParamIndex].m_strParameterDescript + "\" выходит за пределы допустимых значений.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        if (m_listParams[ParamIndex].m_notEqualVal != null && Math.Abs((double)m_listParams[ParamIndex].m_notEqualVal - (double)m_listParams[ParamIndex].m_valNumber) <= 0.0000009
                            || (double.IsNaN((double)m_listParams[ParamIndex].m_valNumber) || double.IsInfinity((double)m_listParams[ParamIndex].m_valNumber)))
                        {
                            if (m_listParams[ParamIndex].m_defVal != null)
                            {
                                m_listParams[ParamIndex].m_valNumber = m_listParams[ParamIndex].m_defVal;
                                m_listParams[ParamIndex].m_Row.Properties.Value = m_listParams[ParamIndex].m_defVal;

                                if (this.m_Row.ParentRow.Visible) // для скрытых строк не надо показывать сообщение (при объединении узлов)
                                    MyLocalizer.XtraMessageBoxShow("Недопустимое значение параметра \"" + m_listParams[ParamIndex].m_strParameterDescript + "\".\nПараметру будет присвоено значение " + m_listParams[ParamIndex].m_defVal.ToString() + ".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                m_listParams[ParamIndex].m_valNumber = null;
                                m_listParams[ParamIndex].m_Row.Properties.Value = null;

                                if (this.m_Row.ParentRow.Visible) // для скрытых строк не надо показывать сообщение (при объединении узлов)
                                    MyLocalizer.XtraMessageBoxShow("Недопустимое значение параметра \"" + m_listParams[ParamIndex].m_strParameterDescript + "\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }*/
                }
            }

            public List<Inspection.DefectInfo> CalcResultRange(InspectionDataForm parent_form)
            {
                if (m_DefectAlgorithm == null) return new List<Inspection.DefectInfo>();

                /*List<KeyValuePair<long, Parameter>> arrValues = new List<KeyValuePair<long, Parameter>>();
                // собираем значения нужных параметров
                for (int i = 0; i < m_DefectAlgorithm.m_arrParameterIDs.Count; i++)
                {
                    for (int j = 0; j < m_listParams.Count; j++)
                    {
                        if (m_listParams[j].m_InspectionSubType != null &&
                            m_listParams[j].m_InspectionSubType != parent_form.m_sub_type_id)
                        {
                            continue;
                        }

                        if (m_listParams[j].m_ParameterID == m_DefectAlgorithm.m_arrParameterIDs[i])
                        {
                            arrValues.Add(new KeyValuePair<long, Parameter>(m_DefectAlgorithm.m_arrParameterIDs[i], m_listParams[j]));
                        }
                    }
                }*/

                if (parent_form.m_type == Inspection.InspectionType.HARG)
                {
                    long FunctionalSystemID = parent_form.dictSystemItemsFunctionalSystems[this.m_SystemItemID];
                    int FunctionalSystemPos = parent_form.dictFunctionalSystemPos[FunctionalSystemID];

                    if (m_DefectAlgorithm.m_AlgorithmName == "harg1") // высоковольные вводы
                    {
                        return DefectAlgorithms.Defect_HARG_1(parent_form, parent_form.m_list[FunctionalSystemPos], this); // всегда считать по РД
                    }

                    if (m_DefectAlgorithm.m_AlgorithmName == "harg2") // обмотки
                    {
                        return DefectAlgorithms.Defect_HARG_2(parent_form, parent_form.m_list[FunctionalSystemPos], this);
                    }

                    if (m_DefectAlgorithm.m_AlgorithmName == "harg3") // магнитопровод
                    {
                        return DefectAlgorithms.Defect_HARG_3(parent_form, parent_form.m_list[FunctionalSystemPos], this);
                    }

                    /*if (m_DefectAlgorithm.m_AlgorithmName == "harg3") // магнитопровод
                    {
                        List<long> listAlgorithmIDs = new List<long>();
                        for (int i = 0; i < parent_form.cbAlgorithm.Properties.Items.Count; i++)
                        {
                            if (parent_form.cbAlgorithm.Properties.Items[i].CheckState == CheckState.Checked) listAlgorithmIDs.Add(parent_form.m_list_algorithms[i].m_ID);

                        }
                        bool bNeedDisableAlgorithm = false;
                        List<Inspection.InspectionResult> results = null;
                        Inspection.RangeResult res = DefectAlgorithms.Defect_HARG_3(parent_form, parent_form.m_list[FunctionalSystemPos], this, listAlgorithmIDs, out bNeedDisableAlgorithm, out results); // зависит от выбранных алгоритмов

                        parent_form.cbAlgorithm.ToolTip =  "";
                        if (results != null)
                        {
                            for (int i = 0; i < results.Count; i++)
                            {
                                string strAlgorithmName = "";
                                for (int j = 0; j < parent_form.m_list_algorithms.Count; j++)
                                {
                                    if (parent_form.m_list_algorithms[i].m_ID == results[i].m_AlgorithmID)
                                    {
                                        strAlgorithmName = parent_form.m_list_algorithms[i].m_strName;
                                        break;
                                    }
                                }
                                parent_form.cbAlgorithm.ToolTip += "<b>" + strAlgorithmName + ":</b> " + results[i].m_strResult + "\n";
                            }
                        }

                        if (this.m_Row.ParentRow.Visible)
                        {
                            if (bNeedDisableAlgorithm)
                            {
                                if (!parent_form.cbAlgorithm.Properties.ReadOnly)
                                {
                                    parent_form.cbAlgorithm.Properties.ReadOnly = true;
                                    parent_form.m_bDataLoad = true;
                                    for (int i = 0; i < parent_form.cbAlgorithm.Properties.Items.Count; i++)
                                    {
                                        parent_form.cbAlgorithm.Properties.Items[i].CheckState = CheckState.Unchecked;
                                    }
                                    parent_form.cbAlgorithm.EditValue = "";
                                    parent_form.m_bDataLoad = false;
                                    parent_form.UpdateResults();
                                }
                            }
                            else
                            {
                                if (parent_form.cbAlgorithm.Properties.ReadOnly)
                                {
                                    parent_form.cbAlgorithm.Properties.ReadOnly = false;
                                    parent_form.m_bDataLoad = true;
                                    if (parent_form.m_list_algorithms.Count > 0 && parent_form.cbAlgorithm.Properties.Items.GetCheckedValues().Count == 0)
                                    {
                                        parent_form.cbAlgorithm.Properties.Items[0].CheckState = CheckState.Checked;
                                    }
                                    parent_form.m_bDataLoad = false;
                                    parent_form.UpdateResults();
                                }
                            }
                        }

                        return res;
                    }*/
                }

                return new List<Inspection.DefectInfo>();
            }

            public long m_SystemItemID;
            public string m_strSystemItemName;
            public long? m_InputVoltageIndex;
            public Dictionary<long, Inspection.InspectionResult> m_dictValRange = new Dictionary<long, Inspection.InspectionResult>(); // значение рассчитывается на основании данных
            //public Dictionary<Inspection.InspectionSubType, Inspection.CalcType> m_dictCalcType = new Dictionary<Inspection.InspectionSubType,Inspection.CalcType>();

            public List<Parameter> m_listParams = new List<Parameter>();
            public List<Defect> m_listDefects = new List<Defect>();
            public DefectAlgorithm m_DefectAlgorithm = null;

            public DevExpress.XtraVerticalGrid.Rows.CategoryRow m_Row;

            public bool bIsVoltageVN = false;
            public bool bIsVoltageSN = false;
            public bool bIsVoltageNeitral = false;
            public bool bIsVoltageRPN = false;
        }

        public class FunctionalSystem
        {
            public FunctionalSystem(InspectionDataForm parent, long FunctionalSystemID, string strFunctionalSystemName)
            {
                m_FunctionalSystemID = FunctionalSystemID;
                m_strFunctionalSystemName = strFunctionalSystemName;
                
                m_dictValRange[0] = Inspection.RangeResult.None;
                for (int i = 0; i < parent.m_list_sub_types.Count; i++)
                {
                    m_dictValRange[parent.m_list_sub_types[i].m_ID] = Inspection.RangeResult.None;
                }

                //if (strFunctionalSystemName.IndexOf("РПН") >= 0) m_bIsRPN = true;
                //else m_bIsRPN = false;
            }

            public long m_FunctionalSystemID;
            public string m_strFunctionalSystemName;
            //public bool m_bIsRPN = false;
            public Dictionary<long, Inspection.RangeResult> m_dictValRange = new Dictionary<long, Inspection.RangeResult>(); // значение рассчитывается на основании данных

            public List<SystemItem> m_listSystemItems = new List<SystemItem>(); // список подузлов
            public Dictionary<long, long> m_dictUnionMainID = new Dictionary<long, long>(); // указатель на основной объединяющий узел
            public Dictionary<long, List<long>> m_dictUnionExtraID = new Dictionary<long, List<long>>(); // список подчиненный узлов для объединения

            public DevExpress.XtraVerticalGrid.Rows.CategoryRow m_Row;
        }

        public Inspection.InspectionType m_type;
        //public Inspection.InspectionSubType m_sub_type;
        public long m_sub_type_id;
        public long m_InspectionID = -1;
        long m_EquipmentID = -1;
        public long m_EquipmentKindID = -1;
        public long? m_VoltageValue_VN = null;
        public long? m_VoltageValue_SN = null;
        public long? m_VoltageValue_Neitral = null;
        public long? m_VoltageValue_RPN = null;
        public long? m_ProtectionOilType = null;
        public long m_CoolingSystemTypeID = -1;
        public long m_RPNKind = -1;
        public long? m_YearsValue = null;
        //public long? m_InputVoltageType = null;
        bool m_bDataLoad = true;
        bool m_bDataLoadEnd = false;
        bool m_bChangeData = false;
        Graphics m_Graphics = null;
        bool m_bExistTypeSelect = false;

        public bool m_bContinueNext = false;
        public bool m_bContinuePrev = false;
        public bool m_bShowContinueMsg = false;
        public bool m_bShowVotnMsg = true;
        //public DateTime? m_dateForFind = null;
        public long m_CheckID = -1;

        public List<InspectionSubType> m_list_sub_types = new List<InspectionSubType>();
        public List<Algorithm> m_list_algorithms = new List<Algorithm>();
        public List<FunctionalSystem> m_list = new List<FunctionalSystem>();
        public Dictionary<long, int> dictFunctionalSystemPos = new Dictionary<long, int>();
        public Dictionary<long, int> dictSystemItemPos = new Dictionary<long, int>();
        public Dictionary<long, long> dictSystemItemsFunctionalSystems = new Dictionary<long, long>();

        List<RepositoryItemMyLookUpEdit/*DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit*/> listItems = new List<RepositoryItemMyLookUpEdit/*DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit*/>();
        List<BindingList<DataSourceString>> listBindings = new List<BindingList<DataSourceString>>();

        public Dictionary<long, List<ParameterLite>> dictLastValuesForHARG = new Dictionary<long, List<ParameterLite>>();
        public Dictionary<long, Dictionary<int, List<int>>> dictDependSystemCalc = new Dictionary<long, Dictionary<int, List<int>>>(); // исп при зависимости расчетов узлов между собой
        public Dictionary<long, List<long>> dictDependsParameters = new Dictionary<long, List<long>>(); // зависимые от других параметры, нужно делать их только для чтения

        public static ToolTipController ttc;

        public Dictionary<long, long> dictInputVoltageIndexes = new Dictionary<long,long>();
        public long m_RPNCount = 0;

        Dictionary<long, string> m_dictParamName = new Dictionary<long,string>();
        Dictionary<long, string> m_dictDefectName = new Dictionary<long,string>();

        Dictionary<long, long> m_dictSystemItemExclude = new Dictionary<long, long>();
        Dictionary<long, long> m_dictParameterExclude = new Dictionary<long, long>();

        DateTime? m_CheckDateBegin = null;
        DateTime? m_CheckDateEnd = null;

        Brush m_BrushGray = new SolidBrush(Color.Gray);
        Brush m_BrushBlack = new SolidBrush(Color.Black);
        Brush m_BrushRed = new SolidBrush(Color.Red);

        public InspectionDataForm(long equipmentID, Inspection.InspectionType type, long inspectionID, long checkID, DateTime? dateCheckBegin = null, DateTime? dateCheckEnd = null)
        {
            m_EquipmentID = equipmentID;
            m_InspectionID = inspectionID;
            m_type = type;
            m_sub_type_id = 0;
            m_CheckID = checkID;

            m_CheckDateBegin = dateCheckBegin;
            m_CheckDateEnd = dateCheckEnd;

            InitializeComponent();

            LoadData();
        }

        SQLiteConnection m_con = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);

        static public double Round(double value, int digits)
        {
            double scale = Math.Pow(10.0, digits);
            double round = Math.Floor(Math.Abs(value) * scale + 0.5);
            return (Math.Sign(value) * round / scale);
        }

        private void AddParam(SQLiteCommand com, string name, DbType type, object value)
        {
            SQLiteParameter param = new SQLiteParameter(name, type);
            param.Value = value;
            com.Parameters.Add(param);
        }

        private Inspection.InspectionType GetNextType(long EquipmentKindID, Inspection.InspectionType cur_type, out bool bEnd)
        {
            Inspection.InspectionType next_type = Inspection.InspectionType.Visual;
            bEnd = false;
            int pos = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID].IndexOf((Inspection.InspectionType)cur_type);
            if (pos >= 0)
            {
                if (Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID].Count > pos + 1)
                    next_type = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID][pos + 1];
                else
                    bEnd = true;
            }
            return next_type;
        }

        private Inspection.InspectionType GetPrevType(long EquipmentKindID, Inspection.InspectionType cur_type, out bool bPassport)
        {
            Inspection.InspectionType prev_type = Inspection.InspectionType.Visual;
            bPassport = false;
            int pos = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID].IndexOf((Inspection.InspectionType)cur_type);
            if (pos >= 0)
            {
                if (pos > 0)
                    prev_type = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)EquipmentKindID][pos - 1];
                else
                    bPassport = true;
            }
            return prev_type;
        }

        private void LoadData()
        {
            try
            {
                m_con.Open();
                SQLiteCommand com = new SQLiteCommand(m_con);
                com.CommandType = CommandType.Text;

                /*com.CommandText = "Select EquipmentName, EquipmentNumber, ProtectionOilTypeID, UseBeginYear, " + 
                    "NominalVoltages.NominalVoltageValue FROM Equipments " +
                    "LEFT JOIN NominalVoltages ON Equipments.NominalVoltageHighID = NominalVoltages.NominalVoltageID " +
                    "WHERE EquipmentID = @id";*/
                com.CommandText = "Select e.EquipmentName, e.EquipmentNumber, e.NominalVoltageHigh, " +
                    "ihA.InputKind as InputKindHighA, ihB.InputKind as InputKindHighB, ihC.InputKind as InputKindHighC, " +
                    "imA.InputKind as InputKindMiddleA, imB.InputKind as InputKindMiddleB, imC.InputKind as InputKindMiddleC, iNN.InputKind as InputKindNeutral, " +
                    "e.RPNCnt, e.RPNVoltage, e.ProtectionOilTypeID, e.CreateYear, e.UseBeginYear, e.NominalVoltageMiddle, e.NominalVoltageNeitral, e.CoolingSystemTypeID, " + 
                    "e.RPNKind, e.EquipmentKindID " +
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

                //long? UseBeginYear = null;
                m_YearsValue = 1000;

                long? iCreateYear = null;
                long? iBeginUseYear = null;

                if (drEquip.HasRows)
                {
                    while (drEquip.Read())
                    {
                        teEquipmentName.Text = drEquip["EquipmentName"].ToString();
                        teEquipmentNumber.Text = drEquip["EquipmentNumber"].ToString();
                        if (drEquip["NominalVoltageHigh"] != DBNull.Value)
                            m_VoltageValue_VN = Convert.ToInt64(drEquip["NominalVoltageHigh"]);

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

                        /*if (m_type == Inspection.InspectionType.HARG)
                        {
                            if (drEquip["InputKindHighA"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighA"]) == 1) // не твердая изоляция и не масляная негерметичная
                            {
                                dictInputVoltageIndexes[1] = Convert.ToInt64(drEquip["InputKindHighA"]);
                            }
                            if (drEquip["InputKindHighB"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighB"]) == 1)
                            {
                                dictInputVoltageIndexes[2] = Convert.ToInt64(drEquip["InputKindHighB"]);
                            }
                            if (drEquip["InputKindHighC"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighC"]) == 1)
                            {
                                dictInputVoltageIndexes[3] = Convert.ToInt64(drEquip["InputKindHighC"]);
                            }
                            if (drEquip["InputKindMiddleA"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleA"]) == 1)
                            {
                                dictInputVoltageIndexes[4] = Convert.ToInt64(drEquip["InputKindMiddleA"]);
                            }
                            if (drEquip["InputKindMiddleB"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleB"]) == 1)
                            {
                                dictInputVoltageIndexes[5] = Convert.ToInt64(drEquip["InputKindMiddleB"]);
                            }
                            if (drEquip["InputKindMiddleC"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleC"]) == 1)
                            {
                                dictInputVoltageIndexes[6] = Convert.ToInt64(drEquip["InputKindMiddleC"]);
                            }
                            if (drEquip["InputKindNeutral"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindNeutral"]) == 1)
                            {
                                dictInputVoltageIndexes[7] = Convert.ToInt64(drEquip["InputKindNeutral"]);
                            }
                        }
                        if (m_type == Inspection.InspectionType.FHA)
                        {
                            if (drEquip["InputKindHighA"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighA"]) != 3) // не твердая изоляция
                            {
                                dictInputVoltageIndexes[1] = Convert.ToInt64(drEquip["InputKindHighA"]);
                            }
                            if (drEquip["InputKindHighB"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighB"]) != 3)
                            {
                                dictInputVoltageIndexes[2] = Convert.ToInt64(drEquip["InputKindHighB"]);
                            }
                            if (drEquip["InputKindHighC"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighC"]) != 3)
                            {
                                dictInputVoltageIndexes[3] = Convert.ToInt64(drEquip["InputKindHighC"]);
                            }
                            if (drEquip["InputKindMiddleA"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleA"]) != 3)
                            {
                                dictInputVoltageIndexes[4] = Convert.ToInt64(drEquip["InputKindMiddleA"]);
                            }
                            if (drEquip["InputKindMiddleB"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleB"]) != 3)
                            {
                                dictInputVoltageIndexes[5] = Convert.ToInt64(drEquip["InputKindMiddleB"]);
                            }
                            if (drEquip["InputKindMiddleC"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleC"]) != 3)
                            {
                                dictInputVoltageIndexes[6] = Convert.ToInt64(drEquip["InputKindMiddleC"]);
                            }
                            if (drEquip["InputKindNeutral"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindNeutral"]) != 3)
                            {
                                dictInputVoltageIndexes[7] = Convert.ToInt64(drEquip["InputKindNeutral"]);
                            }
                        }*/
                        if (drEquip["RPNCnt"] != DBNull.Value && Convert.ToInt64(drEquip["RPNCnt"]) != 0)
                            m_RPNCount = Convert.ToInt64(drEquip["RPNCnt"]);

                        if (drEquip["RPNVoltage"] != DBNull.Value)
                            m_VoltageValue_RPN = Convert.ToInt64(drEquip["RPNVoltage"]);

                        if (drEquip["ProtectionOilTypeID"] != DBNull.Value)
                            m_ProtectionOilType = Convert.ToInt64(drEquip["ProtectionOilTypeID"]);

                        if (drEquip["CoolingSystemTypeID"] != DBNull.Value)
                            m_CoolingSystemTypeID = Convert.ToInt64(drEquip["CoolingSystemTypeID"]);

                        if (drEquip["RPNKind"] != DBNull.Value)
                            m_RPNKind = Convert.ToInt64(drEquip["RPNKind"]);

                        if (drEquip["CreateYear"] != DBNull.Value)
                            iCreateYear = Convert.ToInt64(drEquip["CreateYear"]);

                        if (drEquip["UseBeginYear"] != DBNull.Value)
                            iBeginUseYear = Convert.ToInt64(drEquip["UseBeginYear"]);

                        if (drEquip["NominalVoltageMiddle"] != DBNull.Value)
                            m_VoltageValue_SN = Convert.ToInt64(drEquip["NominalVoltageMiddle"]);

                        if (drEquip["NominalVoltageNeitral"] != DBNull.Value)
                            m_VoltageValue_Neitral = Convert.ToInt64(drEquip["NominalVoltageNeitral"]);

                        break;
                    }
                }
                drEquip.Close();

                /*if (UseBeginYear != null)
                {
                    m_UseYears = DateTime.Now.Year - UseBeginYear;
                }*/

                if (m_type == Inspection.InspectionType.FHA)
                {
                    if (m_VoltageValue_VN == null)
                    {
                        //MyLocalizer.XtraMessageBoxShow("Для корректного расчета ФХА в паспортных данных оборудования должно быть указано значение \"Класс напряжения ВН\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        m_VoltageValue_VN = 0;
                    }

                    if (m_VoltageValue_SN == null)
                    {
                        //MyLocalizer.XtraMessageBoxShow("Для корректного расчета ФХА в паспортных данных оборудования должно быть указано значение \"Класс напряжения СН\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        m_VoltageValue_SN = 0;
                    }

                    if (m_VoltageValue_Neitral == null)
                    {
                        //MyLocalizer.XtraMessageBoxShow("Для корректного расчета ФХА в паспортных данных оборудования должно быть указано значение \"Класс напряжения нейтрали\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        m_VoltageValue_Neitral = 0;
                    }

                    if (m_RPNCount > 0 && m_VoltageValue_RPN == null)
                    {
                        m_VoltageValue_RPN = 0;
                        //MyLocalizer.XtraMessageBoxShow("Для корректного расчета ФХА в паспортных данных оборудования должно быть указано значение \"Класс напряжения РПН\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    /*if (m_ProtectionOilType == null)
                    {
                        MyLocalizer.XtraMessageBoxShow("Для корректного расчета ФХА в паспортных данных оборудования должно быть указано значение \"Тип защиты масла\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }*/
                }
                if (m_type == Inspection.InspectionType.HARG)
                {
                    if (m_VoltageValue_VN == null)
                    {
                        //MyLocalizer.XtraMessageBoxShow("Для корректного расчета ХАРГ в паспортных данных оборудования должно быть указано значение \"Класс напряжения ВН\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        m_VoltageValue_VN = 0;
                    }

                    if (m_VoltageValue_SN == null)
                    {
                        //MyLocalizer.XtraMessageBoxShow("Для корректного расчета ХАРГ в паспортных данных оборудования должно быть указано значение \"Класс напряжения СН\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        m_VoltageValue_SN = 0;
                    }

                    if (m_VoltageValue_Neitral == null)
                    {
                        //MyLocalizer.XtraMessageBoxShow("Для корректного расчета ХАРГ в паспортных данных оборудования должно быть указано значение \"Класс напряжения нейтрали\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        m_VoltageValue_Neitral = 0;
                    }

                    /*if (m_RPNCount > 0 && m_VoltageRPNValue == null)
                    {
                        MyLocalizer.XtraMessageBoxShow("Для корректного расчета ХАРГ в паспортных данных оборудования должно быть указано значение \"Класс напряжения РПН\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }*/
                    /*if (m_VoltageValue == null || m_UseYears == null || m_ProtectionOilTypeID == null)
                    {
                        MyLocalizer.XtraMessageBoxShow("Для корректного расчета ХАРГ в паспортных данных оборудования должны быть указаны значения: \"Номинальное напряжение ВН\", \"Тип защиты масла\" и \"Год ввода в эксплуатацию\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }*/
                }

                // ---------------------

                com.CommandText = "Select InspectionSubTypeID, InspectionSubTypeName FROM InspectionSubTypes WHERE InspectionType = @type AND EquipmentKindID = @eq_kind ORDER BY InspectionSubTypeID";

                com.Parameters.Clear();
                AddParam(com, "@type", DbType.Int64, (long)m_type);
                AddParam(com, "@eq_kind", DbType.Int64, (long)m_EquipmentKindID);

                SQLiteDataReader drInspectionSubType = com.ExecuteReader();
                if (drInspectionSubType.HasRows)
                {
                    while (drInspectionSubType.Read())
                    {
                        long InspectionSubTypeID = Convert.ToInt64(drInspectionSubType["InspectionSubTypeID"]);
                        string strInspectionSubTypeName = drInspectionSubType["InspectionSubTypeName"].ToString();

                        m_list_sub_types.Add(new InspectionSubType(InspectionSubTypeID, strInspectionSubTypeName));
                        cbSubType.Properties.Items.Add(strInspectionSubTypeName);
                    }
                }
                drInspectionSubType.Close();

                // ---------------------

                com.CommandText = "Select AlgorithmID, AlgorithmName FROM AlgorithmTypes WHERE InspectionType = @type AND EquipmentKindID = @eq_kind ORDER BY AlgorithmID";

                com.Parameters.Clear();
                AddParam(com, "@type", DbType.Int64, (long)m_type);
                AddParam(com, "@eq_kind", DbType.Int64, (long)m_EquipmentKindID);

                SQLiteDataReader drAlgorithmType = com.ExecuteReader();
                if (drAlgorithmType.HasRows)
                {
                    while (drAlgorithmType.Read())
                    {
                        long AlgorithmID = Convert.ToInt64(drAlgorithmType["AlgorithmID"]);
                        string strAlgorithmName = drAlgorithmType["AlgorithmName"].ToString();

                        m_list_algorithms.Add(new Algorithm(AlgorithmID, strAlgorithmName));
                        cbAlgorithm.Properties.Items.Add(strAlgorithmName, false);
                    }
                }
                drAlgorithmType.Close();

                // ---------------------
                // Данные об исключении подсистем или параметров в списке
                // ExcludeType: 1 – исп. для объекта “Тип системы охлаждения”, 2 – исп. для объекта Тип РПН, 3 – исп. для объекта “Тип защиты масла”, (4 - 10) – исп. для объекта “Вид ввода”
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
                        /*long ExcludeType = -1;
                        if (drParameterExclude["ExcludeType"] != DBNull.Value)
                            ExcludeType = Convert.ToInt64(drParameterExclude["ExcludeType"]);
                        long ExcludeObjectID = -1;
                        if (drParameterExclude["ExcludeObjectID"] != DBNull.Value)
                            ExcludeObjectID = Convert.ToInt64(drParameterExclude["ExcludeObjectID"]);*/

                        /*if (ExcludeType != 4) // для вводов используется отдельная проверка
                        {
                            ExcludeObjectID = 0;
                            ExcludeType = 0;
                        }*/

                        if (SystemItemID > 0) m_dictSystemItemExclude[SystemItemID] = 1;
                        if (ParameterID > 0) m_dictParameterExclude[ParameterID] = 1;
                    }
                }
                drParameterExclude.Close();

                // ---------------------

                com.CommandText = "Select FunctionalSystemID, FunctionalSystemName " +
                    " FROM FunctionalSystems WHERE EXISTS (SELECT * FROM SystemItems " +
                    "WHERE SystemItems.FunctionalSystemID = FunctionalSystems.FunctionalSystemID " +
                    "AND (UseInVisual <> 0 AND @type = 0 OR UseInHARG <> 0 AND @type = 1 OR UseInFHA <> 0 AND @type = 2 " +
                    " OR UseInWarm <> 0 AND @type = 3  OR UseInVibro <> 0 AND @type = 4  OR UseInParameter <> 0 AND @type = 5 " +
                    " OR UseInElectrical <> 0 AND @type = 6) AND EquipmentKindID = @eq_kind) AND EquipmentKindID = @eq_kind ORDER BY Sort";

                com.Parameters.Clear();
                AddParam(com, "@type", DbType.Int64, (long)m_type);
                AddParam(com, "@eq_kind", DbType.Int64, (long)m_EquipmentKindID);

                SQLiteDataReader drFuncSystem = com.ExecuteReader();
                if (drFuncSystem.HasRows)
                {
                    while (drFuncSystem.Read())
                    {
                        long FunctionalSystemID = Convert.ToInt64(drFuncSystem["FunctionalSystemID"]);
                        string strFunctionalSystemName = drFuncSystem["FunctionalSystemName"].ToString();

                        m_list.Add(new FunctionalSystem(this, FunctionalSystemID, strFunctionalSystemName));
                        dictFunctionalSystemPos[FunctionalSystemID] = m_list.Count - 1;
                    }
                }
                drFuncSystem.Close();

                // ---------------------

                com.CommandText = "Select InspectionSubType, MainFunctionalSystemID, ExtraFunctionalSystemID " +
                    " FROM UnionFunctionalSystems WHERE InspectionType = @type AND EquipmentKindID = @eq_kind ORDER BY UnionFunctionalSystemID";
                SQLiteDataReader drUnion = com.ExecuteReader();
                if (drUnion.HasRows)
                {
                    while (drUnion.Read())
                    {
                        long sub_type_id = 0;
                        if (drUnion["InspectionSubType"] != DBNull.Value)
                            sub_type_id = Convert.ToInt64(drUnion["InspectionSubType"]);
                        long MainFunctionalSystemID = Convert.ToInt64(drUnion["MainFunctionalSystemID"]);
                        long ExtraFunctionalSystemID = Convert.ToInt64(drUnion["ExtraFunctionalSystemID"]);

                        if (dictFunctionalSystemPos.ContainsKey(MainFunctionalSystemID) && dictFunctionalSystemPos.ContainsKey(ExtraFunctionalSystemID))
                        {
                            int ExtraFunctionalSystemPos = dictFunctionalSystemPos[ExtraFunctionalSystemID];
                            int MainFunctionalSystemPos = dictFunctionalSystemPos[MainFunctionalSystemID];

                            if (sub_type_id != 0)
                            {
                                m_list[ExtraFunctionalSystemPos].m_dictUnionMainID[sub_type_id] = MainFunctionalSystemID;

                                if (!m_list[MainFunctionalSystemPos].m_dictUnionExtraID.ContainsKey(sub_type_id))
                                    m_list[MainFunctionalSystemPos].m_dictUnionExtraID[sub_type_id] = new List<long>();
                                m_list[MainFunctionalSystemPos].m_dictUnionExtraID[sub_type_id].Add(ExtraFunctionalSystemID);
                            }
                            else
                            {
                                // заносим объединения для все подтипов
                                m_list[ExtraFunctionalSystemPos].m_dictUnionMainID[0] = MainFunctionalSystemID;

                                if (!m_list[MainFunctionalSystemPos].m_dictUnionExtraID.ContainsKey(0))
                                    m_list[MainFunctionalSystemPos].m_dictUnionExtraID[0] = new List<long>();
                                m_list[MainFunctionalSystemPos].m_dictUnionExtraID[0].Add(ExtraFunctionalSystemID);

                                for (int i = 0; i < m_list_sub_types.Count; i++)
                                {
                                    m_list[ExtraFunctionalSystemPos].m_dictUnionMainID[m_list_sub_types[i].m_ID] = MainFunctionalSystemID;

                                    if (!m_list[MainFunctionalSystemPos].m_dictUnionExtraID.ContainsKey(m_list_sub_types[i].m_ID))
                                        m_list[MainFunctionalSystemPos].m_dictUnionExtraID[m_list_sub_types[i].m_ID] = new List<long>();
                                    m_list[MainFunctionalSystemPos].m_dictUnionExtraID[m_list_sub_types[i].m_ID].Add(ExtraFunctionalSystemID);
                                }
                            }
                        }
                    }
                }
                drUnion.Close();

                // ---------------------

                com.CommandText = "Select SystemItemID, FunctionalSystemID, SystemItemName, " +
                    " CASE @type WHEN 0 THEN AlgorithmVisual WHEN 1 THEN AlgorithmHARG " + 
                    " WHEN 2 THEN AlgorithmFHA " + 
                    " WHEN 3 THEN AlgorithmWarm " +
                    " WHEN 4 THEN AlgorithmVibro " + 
                    " WHEN 5 THEN AlgorithmParameter " + 
                    " WHEN 6 THEN AlgorithmElectrical END AS Algorithm, " +
                    " MinRPNCnt, InputVoltageIndex, VoltageType " + 
                    " FROM SystemItems WHERE (UseInVisual <> 0 AND @type = 0 OR UseInHARG <> 0 AND @type = 1 OR UseInFHA <> 0 AND @type = 2 " +
                    " OR UseInWarm <> 0 AND @type = 3  OR UseInVibro <> 0 AND @type = 4 OR UseInParameter <> 0 AND @type = 5 OR UseInElectrical <> 0 AND @type = 6)  AND EquipmentKindID = @eq_kind ORDER BY SystemItemName";

                SQLiteDataReader drSystemItem = com.ExecuteReader();
                if (drSystemItem.HasRows)
                {
                    while (drSystemItem.Read())
                    {
                        long SystemItemID = Convert.ToInt64(drSystemItem["SystemItemID"]);
                        long FunctionalSystemID = Convert.ToInt64(drSystemItem["FunctionalSystemID"]);
                        string strSystemItemName = drSystemItem["SystemItemName"].ToString();
                        string strAlgorithm = drSystemItem["Algorithm"].ToString();
                        long? MinRPNCount = null;
                        if (drSystemItem["MinRPNCnt"] != DBNull.Value)
                            MinRPNCount = Convert.ToInt64(drSystemItem["MinRPNCnt"]);
                        long? InputVoltageIndex = null;
                        if (drSystemItem["InputVoltageIndex"] != DBNull.Value)
                            InputVoltageIndex = Convert.ToInt64(drSystemItem["InputVoltageIndex"]);
                        long? VoltageType = 0;
                        if (drSystemItem["VoltageType"] != DBNull.Value)
                            VoltageType = Convert.ToInt64(drSystemItem["VoltageType"]);

                        int FunctionalSystemPos = dictFunctionalSystemPos[FunctionalSystemID];

                        // ------------------------
                        // ограничения
                        // ------------------------
                        if (MinRPNCount != null && MinRPNCount > m_RPNCount) continue;
                        if (InputVoltageIndex != null && dictInputVoltageIndexes[(long)InputVoltageIndex] <= 0) continue;
                        if (m_dictSystemItemExclude.ContainsKey(SystemItemID)) continue;
                        /*{
                            if (m_dictSystemItemExclude[SystemItemID].Count == 0) continue;
                            
                            if (InputVoltageIndex != null && dictInputVoltageIndexes.ContainsKey((long)InputVoltageIndex))
                            {
                                KeyValuePair<long, long> val = new KeyValuePair<long, long>(4, dictInputVoltageIndexes[(long)InputVoltageIndex]);
                                if (m_dictSystemItemExclude[SystemItemID].ContainsKey(val)) continue;
                            }
                        }*/
                        // ------------------------

                        m_list[FunctionalSystemPos].m_listSystemItems.Add(new SystemItem(this, SystemItemID, strSystemItemName, InputVoltageIndex, strAlgorithm, (long)VoltageType));
                        dictSystemItemPos[SystemItemID] = m_list[FunctionalSystemPos].m_listSystemItems.Count - 1;
                        dictSystemItemsFunctionalSystems[SystemItemID] = FunctionalSystemID;

                        if (m_list[FunctionalSystemPos].m_listSystemItems[dictSystemItemPos[SystemItemID]].m_DefectAlgorithm != null)
                        {
                            SystemItem.DefectAlgorithm da = m_list[FunctionalSystemPos].m_listSystemItems[dictSystemItemPos[SystemItemID]].m_DefectAlgorithm;
                            for (int i = 0; i < da.m_arrSystemItemIDs.Count; i++)
                            {
                                if (!dictDependSystemCalc.ContainsKey(da.m_arrSystemItemIDs[i]))
                                {
                                    dictDependSystemCalc.Add(da.m_arrSystemItemIDs[i], new Dictionary<int, List<int>>());
                                    dictDependSystemCalc[da.m_arrSystemItemIDs[i]][FunctionalSystemPos] = new List<int>();
                                }
                                dictDependSystemCalc[da.m_arrSystemItemIDs[i]][FunctionalSystemPos].Add(dictSystemItemPos[SystemItemID]);
                            }
                        }
                    }
                }
                drSystemItem.Close();

                // ---------------------

                com.CommandText = "Select * FROM Parameters AS p WHERE p.InspectionType = @type AND p.EquipmentKindID = @eq_kind ORDER BY Sort";

                SQLiteDataReader drParam = com.ExecuteReader();
                if (drParam.HasRows)
                {
                    while (drParam.Read())
                    {
                        long ParameterID = Convert.ToInt64(drParam["ParameterID"]);
                        long SystemItemID = -1;
                        if (drParam["SystemItemID"] != DBNull.Value)
                            SystemItemID = Convert.ToInt64(drParam["SystemItemID"]);
                        string strParameterDescript = drParam["ParameterDescript"].ToString();
                        long iParameterType = Convert.ToInt64(drParam["ParameterType"]);
                        string strParameterSelect1 = drParam["ParameterSelect1"].ToString();
                        string strParameterSelect2 = drParam["ParameterSelect2"].ToString();
                        string strParameterSelect3 = drParam["ParameterSelect3"].ToString();
                        string strParameterSelect4 = drParam["ParameterSelect4"].ToString();
                        string strParameterAlgorithm = drParam["ParameterAlgorithm"].ToString();
                        /*long ExcludeSystemItemID = -1;
                        if (drParam.GetValue(9) != DBNull.Value)
                            ExcludeSystemItemID = Convert.ToInt64(drParam.GetValue(9));*/
                        long? InspectionSubType = null;
                        if (drParam["InspectionSubType"] != DBNull.Value)
                            InspectionSubType = Convert.ToInt64(drParam["InspectionSubType"]);
                        
                        double? minVal = null;
                        double? maxVal = null;
                        double? notEqualVal = null;
                        double? defVal = null;

                        if (drParam["MinValue"] != DBNull.Value)
                            minVal = Convert.ToDouble(drParam["MinValue"]);
                        if (drParam["MaxValue"] != DBNull.Value)
                            maxVal = Convert.ToDouble(drParam["MaxValue"]);
                        if (drParam["NotEqualValue"] != DBNull.Value)
                            notEqualVal = Convert.ToDouble(drParam["NotEqualValue"]);
                        if (drParam["DefaultValue"] != DBNull.Value)
                            defVal = Convert.ToDouble(drParam["DefaultValue"]);

                        string strParameterName = drParam["ParameterName"].ToString();
                        long? DefectID = null;
                        if (drParam["DefectID"] != DBNull.Value)
                            DefectID = Convert.ToInt64(drParam["DefectID"]);

                        bool bNotEmpty = false;
                        if (drParam["NotEmpty"] != DBNull.Value && Convert.ToInt64(drParam["NotEmpty"]) == 1)
                            bNotEmpty = true;

                        //string strDefectName = "";
                        //if (drParam["DefectName"] != DBNull.Value)
                        //    strDefectName = drParam["DefectName"].ToString();

                        // ------------------------
                        // ограничения
                        // ------------------------
                        if (m_dictParameterExclude.ContainsKey(ParameterID)) continue;// && m_dictParameterExclude[ParameterID].Count == 0) continue;
                        // ------------------------

                        m_dictParamName[ParameterID] = strParameterDescript;

                        if (SystemItemID > 0)
                        {
                            if (dictSystemItemsFunctionalSystems.ContainsKey(SystemItemID) && dictSystemItemPos.ContainsKey(SystemItemID))
                            {
                                long FunctionalSystemID = dictSystemItemsFunctionalSystems[SystemItemID];
                                int FunctionalSystemPos = dictFunctionalSystemPos[FunctionalSystemID];
                                int SystemItemPos = dictSystemItemPos[SystemItemID];

                                /*if (m_dictParameterExclude.ContainsKey(ParameterID) && m_dictParameterExclude[ParameterID].Count > 0) // исключения по вводам обрабатываются отдельно
                                {
                                    SystemItem si = m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos];

                                    if (si.m_InputVoltageIndex != null && dictInputVoltageIndexes.ContainsKey((long)si.m_InputVoltageIndex))
                                    {
                                        KeyValuePair<long, long> val = new KeyValuePair<long, long>(4, dictInputVoltageIndexes[(long)si.m_InputVoltageIndex]);
                                        if (m_dictParameterExclude[ParameterID].ContainsKey(val)) continue;
                                    }
                                }*/

                                m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_listParams.Add(new Parameter(this, SystemItemID, ParameterID,
                                    strParameterDescript, strParameterName, (Inspection.ParameterType)iParameterType, strParameterSelect1, strParameterSelect2, strParameterSelect3, strParameterSelect4,
                                    strParameterAlgorithm, InspectionSubType, minVal, maxVal, notEqualVal, defVal, DefectID, bNotEmpty));
                            }

                            /*if ((Inspection.ParameterType)iParameterType == Inspection.ParameterType.Number)
                            {
                                if (InspectionSubType == null)
                                {
                                    m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_dictCalcType[Inspection.InspectionSubType.Main] = Inspection.CalcType.Algorithm;
                                    m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_dictCalcType[Inspection.InspectionSubType.Extra] = Inspection.CalcType.Algorithm;
                                }
                                else
                                    m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_dictCalcType[(Inspection.InspectionSubType)InspectionSubType] = Inspection.CalcType.Algorithm;
                            }*/
                        }
                        else
                        {
                            for (int i = 0; i < m_list.Count; i++)
                            {
                                for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                                {
                                    /*if (m_dictParameterExclude.ContainsKey(ParameterID) && m_dictParameterExclude[ParameterID].Count > 0) // исключения по вводам обрабатываются отдельно
                                    {
                                        SystemItem si = m_list[i].m_listSystemItems[j];

                                        if (si.m_InputVoltageIndex != null && dictInputVoltageIndexes.ContainsKey((long)si.m_InputVoltageIndex))
                                        {
                                            KeyValuePair<long, long> val = new KeyValuePair<long, long>(4, dictInputVoltageIndexes[(long)si.m_InputVoltageIndex]);
                                            if (m_dictParameterExclude[ParameterID].ContainsKey(val)) continue;
                                        }
                                    }*/


                                    //if (m_list[i].m_listSystemItems[j].m_SystemItemID != ExcludeSystemItemID)
                                    {
                                        m_list[i].m_listSystemItems[j].m_listParams.Add(new Parameter(this, m_list[i].m_listSystemItems[j].m_SystemItemID, ParameterID,
                                            strParameterDescript, strParameterName, (Inspection.ParameterType)iParameterType, strParameterSelect1, strParameterSelect2, strParameterSelect3,
                                            strParameterSelect4, strParameterAlgorithm, InspectionSubType, minVal, maxVal, notEqualVal, defVal, DefectID, bNotEmpty));
                                    }
                                }
                            }
                        }
                    }
                }
                drParam.Close();

                // ---------------------

                // удаляем подсистемы, где нет ни одного дефекта
                for (int i = m_list.Count - 1; i >= 0; i--)
                {
                    for (int j = m_list[i].m_listSystemItems.Count - 1; j >= 0; j--)
                    {
                        if (m_list[i].m_listSystemItems[j].m_listParams.Count == 0)
                        {
                            m_list[i].m_listSystemItems.RemoveAt(j);
                        }
                    }
                }
                // ---------------------

                com.CommandText = "Select d.* FROM Defects AS d WHERE d.InspectionType = @type AND d.EquipmentKindID = @eq_kind";

                SQLiteDataReader drDefect = com.ExecuteReader();
                if (drDefect.HasRows)
                {
                    while (drDefect.Read())
                    {
                        long SystemItemID = Convert.ToInt64(drDefect["SystemItemID"]);
                        long? InspectionSubType = null;
                        if (drDefect["InspectionSubType"] != DBNull.Value)
                            InspectionSubType = Convert.ToInt64(drDefect["InspectionSubType"]);

                        long DefectID = Convert.ToInt64(drDefect["DefectID"]);
                        string strDefectName = drDefect["DefectName"].ToString();
                        m_dictDefectName[DefectID] = strDefectName;

                        if (dictSystemItemsFunctionalSystems.ContainsKey(SystemItemID) && dictSystemItemPos.ContainsKey(SystemItemID))
                        {
                            long FunctionalSystemID = dictSystemItemsFunctionalSystems[SystemItemID];
                            int FunctionalSystemPos = dictFunctionalSystemPos[FunctionalSystemID];
                            int SystemItemPos = dictSystemItemPos[SystemItemID];
                            m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_listDefects.Add(new Defect(DefectID, InspectionSubType, strDefectName));
                        }

                    }
                }
                drDefect.Close();

                // ---------------------

                /*com.CommandText = "Select ParameterLimits.ParameterID, VoltageMin, VoltageMax, ProtectionOilTypeID, UseYearFrom, " +
                    "UseYearTo, ValueMin, ValueMax, RangeValue " +
                    "FROM ParameterLimits INNER JOIN Parameters ON Parameters.ParameterID = ParameterLimits.ParameterID " +
                    "WHERE InspectionType = @type AND (VoltageMin IS NULL AND VoltageMax IS NULL OR VoltageMin <= COALESCE(@voltage, -1) AND VoltageMax >= COALESCE(@voltage, -1)) " +
                    "AND (ProtectionOilTypeID IS NULL OR ProtectionOilTypeID = COALESCE(@protection_type_id, -1)) AND " +
                    "(UseYearFrom IS NULL AND UseYearTo IS NULL OR UseYearFrom <= COALESCE(@use_years, -1) AND UseYearTo >= COALESCE(@use_years, -1)) " +
                    "ORDER BY ParameterLimits.ParameterID, RangeValue";

                AddParam(com, "@voltage", DbType.Int64, m_VoltageValue);
                AddParam(com, "@protection_type_id", DbType.Int64, m_ProtectionOilTypeID);
                AddParam(com, "@use_years", DbType.Int64, m_UseYears);*/

                /*com.CommandText = "Select ParameterLimits.ParameterID, ParameterLimits.ValueMin, ParameterLimits.ValueMax, ParameterLimits.RangeValue, ParameterLimits.AlgorithmID " +
                    "FROM ParameterLimits INNER JOIN Parameters ON Parameters.ParameterID = ParameterLimits.ParameterID " +
                    "WHERE InspectionType = @type " + 
                    "AND (VoltageMin IS NULL AND VoltageMax IS NULL OR VoltageMin <= COALESCE(@voltage, -1) AND VoltageMax >= COALESCE(@voltage, -1)) " +
                    "AND (VoltageRPNMin IS NULL AND VoltageRPNMax IS NULL OR VoltageRPNMin <= COALESCE(@voltageRPN, -1) AND VoltageRPNMax >= COALESCE(@voltageRPN, -1)) " +
                    "AND (ProtectionOilType IS NULL OR ProtectionOilType == COALESCE(@ProtectionOilType, -1)) " +
                    "ORDER BY ParameterLimits.ParameterID, ParameterLimits.RangeValue";*/

                com.CommandText = "Select ParameterLimits.ParameterID, ParameterLimits.ValueMin, ParameterLimits.ValueMax, ParameterLimits.RangeValue, ParameterLimits.AlgorithmID, " +
                    "VoltageMin, VoltageMax, ProtectionOilType, InputVoltageType, YearsMin, YearsMax, ParameterLimits.SystemItemID " +
                    "FROM ParameterLimits INNER JOIN Parameters ON Parameters.ParameterID = ParameterLimits.ParameterID " +
                    "WHERE InspectionType = @type AND Parameters.EquipmentKindID = @eq_kind " +
                    "ORDER BY ParameterLimits.ParameterID, ParameterLimits.RangeValue";

                //AddParam(com, "@voltage", DbType.Int64, m_VoltageValue);
                //AddParam(com, "@voltageRPN", DbType.Int64, m_VoltageRPNValue);
                //AddParam(com, "@ProtectionOilType", DbType.Int64, m_ProtectionOilType);

                SQLiteDataReader drParamLimit = com.ExecuteReader();
                if (drParamLimit.HasRows)
                {
                    while (drParamLimit.Read())
                    {
                        long ParameterID = Convert.ToInt64(drParamLimit["ParameterID"]);

                        double? ValueMin = null;
                        if (drParamLimit["ValueMin"] != DBNull.Value)
                            ValueMin = Convert.ToDouble(drParamLimit["ValueMin"]);
                        double? ValueMax = null;
                        if (drParamLimit["ValueMax"] != DBNull.Value)
                            ValueMax = Convert.ToDouble(drParamLimit["ValueMax"]);
                        long? RangeValue = null;
                        if (drParamLimit["RangeValue"] != DBNull.Value)
                            RangeValue = Convert.ToInt64(drParamLimit["RangeValue"]);
                        long? AlgorithmID = null;
                        if (drParamLimit["AlgorithmID"] != DBNull.Value)
                            AlgorithmID = Convert.ToInt64(drParamLimit["AlgorithmID"]);

                        long? VoltageMin = null;
                        if (drParamLimit["VoltageMin"] != DBNull.Value)
                            VoltageMin = Convert.ToInt64(drParamLimit["VoltageMin"]);

                        long? VoltageMax = null;
                        if (drParamLimit["VoltageMax"] != DBNull.Value)
                            VoltageMax = Convert.ToInt64(drParamLimit["VoltageMax"]);

                        /*long? VoltageRPNMin = null;
                        if (drParamLimit.GetValue(7) != DBNull.Value)
                            VoltageRPNMin = Convert.ToInt64(drParamLimit.GetValue(7));

                        long? VoltageRPNMax = null;
                        if (drParamLimit.GetValue(8) != DBNull.Value)
                            VoltageRPNMax = Convert.ToInt64(drParamLimit.GetValue(8));*/

                        long? ProtectionOilType = null;
                        if (drParamLimit["ProtectionOilType"] != DBNull.Value)
                            ProtectionOilType = Convert.ToInt64(drParamLimit["ProtectionOilType"]);

                        long? InputVoltageType = null;
                        if (drParamLimit["InputVoltageType"] != DBNull.Value)
                            InputVoltageType = Convert.ToInt64(drParamLimit["InputVoltageType"]);

                        long? YearsMin = null;
                        if (drParamLimit["YearsMin"] != DBNull.Value)
                            YearsMin = Convert.ToInt64(drParamLimit["YearsMin"]);

                        long? YearsMax = null;
                        if (drParamLimit["YearsMax"] != DBNull.Value)
                            YearsMax = Convert.ToInt64(drParamLimit["YearsMax"]);

                        long? SystemItemID = null;
                        if (drParamLimit["SystemItemID"] != DBNull.Value)
                            SystemItemID = Convert.ToInt64(drParamLimit["SystemItemID"]);

                        for (int i = 0; i < m_list.Count; i++)
                        {
                            for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                            {
                                for (int k = 0; k < m_list[i].m_listSystemItems[j].m_listParams.Count; k++)
                                {
                                    if (m_list[i].m_listSystemItems[j].m_listParams[k].m_ParameterID == ParameterID && (SystemItemID == null ||
                                        (long)SystemItemID == m_list[i].m_listSystemItems[j].m_SystemItemID))
                                    {
                                        if (AlgorithmID == null)
                                        {
                                            m_list[i].m_listSystemItems[j].m_listParams[k].m_listLimits.Add(
                                                new Parameter.LimitRange(VoltageMin, VoltageMax, ProtectionOilType, InputVoltageType, YearsMin, YearsMax, ValueMin, ValueMax, RangeValue));
                                        }
                                        else
                                        {
                                            if (!m_list[i].m_listSystemItems[j].m_listParams[k].m_dictAlgLimits.ContainsKey((long)AlgorithmID))
                                                m_list[i].m_listSystemItems[j].m_listParams[k].m_dictAlgLimits[(long)AlgorithmID] = new List<Parameter.LimitAlg>();
                                            m_list[i].m_listSystemItems[j].m_listParams[k].m_dictAlgLimits[(long)AlgorithmID].Add(new Parameter.LimitAlg(VoltageMin, VoltageMax, YearsMin, YearsMax, ProtectionOilType, (double)ValueMin));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                drParamLimit.Close();

                // ----------------------

                if (m_InspectionID > 0 || m_CheckID > 0) // ищем по ID, либо по проверке, оборудованию и типу (для перемещений по стрелочкам)
                {
                    bool bReadCheckDate = false;
                    if (m_InspectionID > 0)
                    {
                        com.CommandText = "Select i.InspectionID, i.InspectionType, i.InspectionSubType, i.InspectionDate, i.CreateDate, i.Executor, i.Delegator, i.AlgorithmIDs, i.CheckID, " +
                            "c.CheckDateBegin, c.CheckDateEnd " + 
                            "FROM Inspections AS i " +
                            "LEFT JOIN Checks AS c ON c.CheckID = i.CheckID " +
                            "WHERE i.InspectionID = @id";
                        com.Parameters.Clear();

                        AddParam(com, "@id", DbType.Int64, m_InspectionID);

                        bReadCheckDate = true;
                    }
                    else
                    {
                        com.CommandText = "Select InspectionID, InspectionType, InspectionSubType, InspectionDate, CreateDate, Executor, Delegator, AlgorithmIDs, CheckID " +
                            "FROM Inspections " +
                            "WHERE @check_id = CheckID and InspectionType = @type AND EquipmentID = @eq_id  LIMIT 1";
                        com.Parameters.Clear();

                        AddParam(com, "@check_id", DbType.Int64, m_CheckID);
                        AddParam(com, "@type", DbType.Int64, (long)m_type);
                        AddParam(com, "@eq_id", DbType.Int64, m_EquipmentID);
                    }

                    SQLiteDataReader drInsp = com.ExecuteReader();
                    string strAlgorithmIDs = "";

                    if (drInsp.HasRows)
                    {
                        while (drInsp.Read())
                        {
                            if (m_InspectionID <= 0) m_InspectionID = Convert.ToInt64(drInsp["InspectionID"]);
                            m_type = (Inspection.InspectionType)Convert.ToInt64(drInsp["InspectionType"]);
                            if (drInsp["InspectionSubType"] != DBNull.Value)
                                m_sub_type_id = Convert.ToInt64(drInsp["InspectionSubType"]);
                            else
                                m_sub_type_id = 0;
                            dtInspectionDate.DateTime = Convert.ToDateTime(drInsp["InspectionDate"]);
                            dtCreateDate.DateTime = Convert.ToDateTime(drInsp["CreateDate"]);
                            teExecutor.Text = drInsp["Executor"].ToString();
                            teDelegator.Text = drInsp["Delegator"].ToString();
                            strAlgorithmIDs = drInsp["AlgorithmIDs"].ToString();

                            if (drInsp["CheckID"] != DBNull.Value)
                                m_CheckID = Convert.ToInt64(drInsp["CheckID"]);
                            else
                                m_CheckID = -1;

                            if (bReadCheckDate)
                            {
                                if (drInsp["CheckDateBegin"] != DBNull.Value)
                                    m_CheckDateBegin = Convert.ToDateTime(drInsp["CheckDateBegin"]);
                                else
                                    m_CheckDateBegin = null;

                                if (drInsp["CheckDateEnd"] != DBNull.Value)
                                    m_CheckDateEnd = Convert.ToDateTime(drInsp["CheckDateEnd"]);
                                else
                                    m_CheckDateEnd = null;
                            }

                            break;
                        }
                    }
                    drInsp.Close();

                    if (m_InspectionID > 0)
                    {
                        com.Parameters.Clear();
                        AddParam(com, "@id", DbType.Int64, m_InspectionID);

                        for (int i = 0; i < m_list_sub_types.Count; i++)
                        {
                            if (m_list_sub_types[i].m_ID == m_sub_type_id)
                            {
                                cbSubType.SelectedIndex = i;
                                break;
                            }
                        }

                        if (strAlgorithmIDs != "")
                        {
                            string[] arrIDs = strAlgorithmIDs.Split(',');
                            for (int i = 0; i < arrIDs.Length; i++)
                            {
                                for (int j = 0; j < m_list_algorithms.Count; j++)
                                {
                                    if (m_list_algorithms[j].m_ID == Int64.Parse(arrIDs[i]))
                                    {
                                        cbAlgorithm.Properties.Items[j].CheckState = CheckState.Checked;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            cbAlgorithm.Properties.ReadOnly = true;
                        }
                        /*if (m_list_algorithms.Count > 0 && cbAlgorithm.Properties.Items.GetCheckedValues().Count == 0)
                        {
                            cbAlgorithm.SetEditValue(cbAlgorithm.Properties.Items[0].Value);
                            cbAlgorithm.Properties.Items[0].CheckState = CheckState.Checked;
                        }*/

                        // загрузка данных
                        com.CommandText = "Select InspectionDatas.*, Parameters.DefectID AS ParameterDefectID FROM InspectionDatas " +
                        "LEFT JOIN Parameters ON Parameters.ParameterID = InspectionDatas.ParameterID WHERE InspectionID = @id";

                        SQLiteDataReader drInspData = com.ExecuteReader();
                        while (drInspData.Read())
                        {
                            long SystemItemID = Convert.ToInt64(drInspData["SystemItemID"]);
                            long? ParameterID = null;
                            if (drInspData["ParameterID"] != DBNull.Value)
                                ParameterID = Convert.ToInt64(drInspData["ParameterID"]);
                            long? DefectID = null;
                            if (drInspData["DefectID"] != DBNull.Value)
                                DefectID = Convert.ToInt64(drInspData["DefectID"]);
                            double? ValNumber = null;
                            if (drInspData["ValNumber"] != DBNull.Value)
                                ValNumber = Convert.ToDouble(drInspData["ValNumber"]);
                            Inspection.RangeResult? ValSelect = null;
                            if (drInspData["ValSelect"] != DBNull.Value)
                                ValSelect = (Inspection.RangeResult)Convert.ToInt64(drInspData["ValSelect"]);
                            string ValString = null;
                            if (drInspData["ValString"] != DBNull.Value)
                                ValString = Convert.ToString(drInspData["ValString"]);

                            DateTime? ValDate = null;
                            if (drInspData["ValDate"] != DBNull.Value)
                                ValDate = Convert.ToDateTime(drInspData["ValDate"]);

                            Inspection.RangeResult Result = (Inspection.RangeResult)Convert.ToInt64(drInspData["Result"]);
                            long? ParameterDefectID = null;
                            if (drInspData["ParameterDefectID"] != DBNull.Value)
                                ParameterDefectID = Convert.ToInt64(drInspData["ParameterDefectID"]);

                            int SystemItemPos = -1;
                            if (dictSystemItemPos.ContainsKey(SystemItemID))
                                SystemItemPos = dictSystemItemPos[SystemItemID];
                            else
                                continue;

                            long FunctionalSystemID = -1;
                            if (dictSystemItemsFunctionalSystems.ContainsKey(SystemItemID))
                                FunctionalSystemID = dictSystemItemsFunctionalSystems[SystemItemID];
                            else
                                continue;

                            int FunctionalSystemPos = -1;
                            if (dictFunctionalSystemPos.ContainsKey(FunctionalSystemID))
                                FunctionalSystemPos = dictFunctionalSystemPos[FunctionalSystemID];
                            else
                                continue;

                            if (ParameterID != null)
                            {
                                for (int i = 0; i < m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_listParams.Count; i++)
                                {
                                    if (ParameterID == m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_listParams[i].m_ParameterID)
                                    {
                                        m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_listParams[i].m_valNumber = ValNumber;
                                        m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_listParams[i].m_valSelect = ValSelect;
                                        m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_listParams[i].m_valString = ValString;
                                        m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_listParams[i].m_valDate = ValDate;
                                        m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_listParams[i].m_valRange = Result;
                                        break;
                                    }
                                }
                            }

                            if (DefectID != null)
                            {
                                m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_dictValRange[m_sub_type_id].Add(null, new Inspection.DefectInfo((long)DefectID, Result));
                            }
                            else
                            {
                                // если параметр связан с дефектом, то не заносим его
                                if (ParameterDefectID != null)
                                    m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_dictValRange[m_sub_type_id].Add(null, new Inspection.DefectInfo((long)ParameterDefectID, Result));
                                else
                                    m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_dictValRange[m_sub_type_id].Add(new Inspection.DefectInfo((long)ParameterID, Result), null);
                            }
                        }
                        drInspData.Close();

                        // -------------------
                        com.CommandText = "Select FunctionalSystemID, Result FROM FunctionalSystemResults WHERE InspectionID = @id";

                        SQLiteDataReader drFuncResult = com.ExecuteReader();
                        while (drFuncResult.Read())
                        {
                            long FunctionalSystemID = Convert.ToInt64(drFuncResult["FunctionalSystemID"]);
                            long Result = Convert.ToInt64(drFuncResult["Result"]);

                            m_list[dictFunctionalSystemPos[FunctionalSystemID]].m_dictValRange[m_sub_type_id] = (Inspection.RangeResult)Result;
                        }
                        drFuncResult.Close();

                        // -------------------
                        com.CommandText = "Select SystemItemID, Result FROM SystemItemResults WHERE InspectionID = @id";

                        SQLiteDataReader drSystResult = com.ExecuteReader();
                        while (drSystResult.Read())
                        {
                            long SystemItemID = Convert.ToInt64(drSystResult["SystemItemID"]);
                            long Result = Convert.ToInt64(drSystResult["Result"]);
                            //string strResult = drSystResult["DefectName"].ToString();

                            int SystemItemPos = -1;
                            if (dictSystemItemPos.ContainsKey(SystemItemID))
                                SystemItemPos = dictSystemItemPos[SystemItemID];
                            else
                                continue;

                            long FunctionalSystemID = -1;
                            if (dictSystemItemsFunctionalSystems.ContainsKey(SystemItemID))
                                FunctionalSystemID = dictSystemItemsFunctionalSystems[SystemItemID];
                            else
                                continue;

                            int FunctionalSystemPos = -1;
                            if (dictFunctionalSystemPos.ContainsKey(FunctionalSystemID))
                                FunctionalSystemPos = dictFunctionalSystemPos[FunctionalSystemID];
                            else
                                continue;

                            m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_dictValRange[m_sub_type_id].m_RangeResult = (Inspection.RangeResult)Result;
                        }
                        drSystResult.Close();
                    }
                    else
                    {
                        dtInspectionDate.DateTime = DateTime.Now;
                        dtCreateDate.DateTime = DateTime.Now;
                        cbSubType.SelectedIndex = 0;
                        if (m_list_sub_types.Count > 0) m_sub_type_id = m_list_sub_types[0].m_ID;

                        cbAlgorithm.Properties.ReadOnly = true;
                    }
                }
                else
                {
                    dtInspectionDate.DateTime = DateTime.Now;
                    dtCreateDate.DateTime = DateTime.Now;
                    cbSubType.SelectedIndex = 0;
                    if (m_list_sub_types.Count > 0) m_sub_type_id = m_list_sub_types[0].m_ID;

                    cbAlgorithm.Properties.ReadOnly = true;
                    /*if (m_list_algorithms.Count > 0)
                    {
                        cbAlgorithm.SetEditValue(cbAlgorithm.Properties.Items[0].Value);
                        cbAlgorithm.Properties.Items[0].CheckState = CheckState.Checked;
                    }*/
                }

                if (iBeginUseYear != null)
                {
                    m_YearsValue = dtInspectionDate.DateTime.Year - iBeginUseYear;
                }
                else
                {
                    if (iCreateYear != null)
                    {
                        m_YearsValue = dtInspectionDate.DateTime.Year - iCreateYear;
                    }
                }
                if (m_YearsValue < 0) m_YearsValue = 0;
                if (m_YearsValue > 1000) m_YearsValue = 1000;

                m_con.Close();
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
        }

        private void SetGridLastValuesForHARG()
        {
            GridVertical.BeginUpdate();

            for (int i = 0; i < m_list.Count; i++)
            {
                for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                {
                    SystemItem si = m_list[i].m_listSystemItems[j];
                    for (int k = 0; k < si.m_listParams.Count; k++)
                    {
                        DevExpress.XtraVerticalGrid.Rows.MultiEditorRow row_ = si.m_listParams[k].m_Row;
                        if (row_.PropertiesCollection.Count <= 1) continue;

                        if (dictLastValuesForHARG.ContainsKey(si.m_SystemItemID))
                        {
                            bool bFind = false;
                            for (int z = 0; z < dictLastValuesForHARG[si.m_SystemItemID].Count; z++)
                            {
                                ParameterLite pl = dictLastValuesForHARG[si.m_SystemItemID][z];
                                if (pl.m_ParameterID == si.m_listParams[k].m_ParameterID)
                                {
                                    switch (si.m_listParams[k].m_ParameterType)
                                    {
                                        case Inspection.ParameterType.Date:
                                            row_.PropertiesCollection[1].Value = pl.m_valDate;
                                            break;
                                        case Inspection.ParameterType.Integer:
                                            row_.PropertiesCollection[1].Value = pl.m_valNumber;
                                            break;
                                        case Inspection.ParameterType.Number:
                                            row_.PropertiesCollection[1].Value = pl.m_valNumber;
                                            break;
                                        case Inspection.ParameterType.Select:
                                            {
                                                if (pl.m_valSelect != null)
                                                    row_.PropertiesCollection[1].Value = pl.m_valString;
                                                else
                                                    row_.PropertiesCollection[1].Value = null;
                                            }
                                            break;
                                        case Inspection.ParameterType.String:
                                            row_.PropertiesCollection[1].Value = pl.m_valString;
                                            break;
                                    }

                                    bFind = true;
                                    break;
                                }
                            }

                            if (!bFind)
                                row_.PropertiesCollection[1].Value = null;
                        }
                        else
                            row_.PropertiesCollection[1].Value = null;
                    }
                }
            }

            GridVertical.EndUpdate();
        }

        private void CreateGrid()
        {
            GridVertical.BeginUpdate();

            m_bExistTypeSelect = false;
            // необходимо проверить параметры calcifall
            for (int i = 0; i < m_list.Count; i++)
            {
                for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                {
                    SystemItem si = m_list[i].m_listSystemItems[j];
                    for (int k = 0; k < si.m_listParams.Count; k++)
                    {
                        Parameter p = si.m_listParams[k];
                        if (p.m_InspectionSubType != null &&
                            p.m_InspectionSubType != m_sub_type_id)
                        {
                            continue;
                        }

                        if (p.m_CalcAlgorithm != null)
                        {
                            if (p.m_CalcAlgorithm.m_algType == Inspection.CalcType.Calc_if_all)
                            {
                                bool bfind = false;
                                p.m_bReadOnly = true;
                                // проверяем значения, участвующие в расчете поля на null
                                for (int z = 0; z < p.m_CalcAlgorithm.m_arrParameterIDs.Count; z++)
                                {
                                    long ParameterID = p.m_CalcAlgorithm.m_arrParameterIDs[z];

                                    for (int y = 0; y < si.m_listParams.Count; y++)
                                    {
                                        Parameter p2 = si.m_listParams[y];
                                        if (p2.m_ParameterID == ParameterID)
                                        {
                                            if (p2.m_InspectionSubType != null &&
                                                p2.m_InspectionSubType != m_sub_type_id)
                                            {
                                                continue;
                                            }

                                            if (p2.m_valNumber == null)
                                            {
                                                p.m_bReadOnly = false;
                                                bfind = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (bfind) break;
                                }
                            }

                            if (p.m_CalcAlgorithm.m_algType == Inspection.CalcType.Calc_if_empty)
                            {
                                bool bfind = false;
                                p.m_bReadOnly = false;
                                
                                for (int z = 0; z < p.m_CalcAlgorithm.m_arrParameterIDs.Count; z++)
                                {
                                    long ParameterID = p.m_CalcAlgorithm.m_arrParameterIDs[z];

                                    for (int y = 0; y < si.m_listParams.Count; y++)
                                    {
                                        Parameter p2 = si.m_listParams[y];
                                        if (p2.m_ParameterID == ParameterID)
                                        {
                                            if (p2.m_InspectionSubType != null &&
                                                p2.m_InspectionSubType != m_sub_type_id)
                                            {
                                                continue;
                                            }

                                            if (p2.m_valNumber != null || p2.m_valSelect != null || p2.m_valString != null || p2.m_valDate != null)
                                            {
                                                p.m_bReadOnly = true;
                                                bfind = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (bfind) break;
                                }
                            }
                        }

                        if (dictDependsParameters.ContainsKey(si.m_SystemItemID) &&
                            dictDependsParameters[si.m_SystemItemID].IndexOf(p.m_ParameterID) >= 0)
                        {
                            p.m_bReadOnly = true;
                        }
                    }
                }
            }

            GridVertical.Rows.Clear();
            listItems.Clear();
            listBindings.Clear();

            for (int i = 0; i < m_list.Count; i++)
            {
                if (m_list[i].m_listSystemItems.Count == 0) continue;

                DevExpress.XtraVerticalGrid.Rows.CategoryRow catRow = new DevExpress.XtraVerticalGrid.Rows.CategoryRow(m_list[i].m_strFunctionalSystemName);
                catRow.OptionsRow.AllowMove = false;
                catRow.OptionsRow.AllowSize = false;
                catRow.MaxCaptionLineCount = 0;
                //catRow.Appearance.BackColor = Color.FromArgb(255, 190, 125);

                if (m_list[i].m_dictUnionMainID.Count > 0 && m_list[i].m_dictUnionMainID.ContainsKey(m_sub_type_id))
                {
                    // удаляем параметры из зависимых подузлов
                    for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                    {
                        for (int k = m_list[i].m_listSystemItems[j].m_listParams.Count - 1; k >= 0; k--)
                        {
                            if (m_list[i].m_listSystemItems[j].m_listParams[k].m_InspectionSubType == m_sub_type_id || m_list[i].m_listSystemItems[j].m_listParams[k].m_InspectionSubType == null)
                                m_list[i].m_listSystemItems[j].m_listParams.RemoveAt(k);
                        }
                    }
                    catRow.Visible = false;
                }
                else
                {
                    for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                    {
                        string strCaption = m_list[i].m_listSystemItems[j].m_strSystemItemName;
                        //if (strCaption == m_list[i].m_strFunctionalSystemName) strCaption = "";
                        DevExpress.XtraVerticalGrid.Rows.CategoryRow catRow_ = new DevExpress.XtraVerticalGrid.Rows.CategoryRow(strCaption);
                        catRow_.OptionsRow.AllowMove = false;
                        catRow_.OptionsRow.AllowSize = false;
                        catRow_.MaxCaptionLineCount = 0;

                        catRow_.Appearance.Options.UseTextOptions = true;
                        catRow_.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                        catRow_.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        catRow_.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Word;
                        catRow_.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                        catRow.ChildRows.Add(catRow_);

                        // добавляем строку с данными Текущие измерения и Предыдущие измерения
                        if (m_type == Inspection.InspectionType.HARG)
                        {
                            DevExpress.XtraVerticalGrid.Rows.MultiEditorRow row_ = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRow();
                            row_.Name = "#";
                            row_.OptionsRow.AllowMove = false;
                            row_.OptionsRow.AllowSize = false;
                            row_.MaxCaptionLineCount = 0;

                            DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowItem1 = row_.PropertiesCollection.Add();
                            rowItem1.Caption = "";
                            rowItem1.FieldName = "#";

                            DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowItem2 = row_.PropertiesCollection.Add();
                            rowItem2.Caption = "";// p.m_strParameterDescript + " пред.";
                            rowItem2.FieldName = "#";
                            rowItem2.Width = 1;
                            rowItem1.Width = 1000000;

                            row_.Properties.Value = "Текущие измерения";
                            row_.PropertiesCollection[1].Value = "Предыдущие измерения";
                            row_.OptionsRow.AllowFocus = false;

                            row_.Appearance.BackColor = Color.FromArgb(240, 240, 240);
                            row_.Appearance.Options.UseBackColor = true;
                            row_.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                            row_.Appearance.Options.UseTextOptions = true;

                            catRow_.ChildRows.Add(row_);
                        }

                        SystemItem si = m_list[i].m_listSystemItems[j];
                        for (int k = 0; k < si.m_listParams.Count; k++)
                        {
                            Parameter p = si.m_listParams[k];
                            if (p.m_InspectionSubType != null &&
                                p.m_InspectionSubType != m_sub_type_id)
                            {
                                p.m_Row = null;
                                continue;
                            }

                            string strFieldName = si.m_SystemItemID.ToString() + "_" + p.m_ParameterID.ToString();
                            //DevExpress.XtraVerticalGrid.Rows.EditorRow row_ = new DevExpress.XtraVerticalGrid.Rows.EditorRow(strFieldName);
                            DevExpress.XtraVerticalGrid.Rows.MultiEditorRow row_ = new DevExpress.XtraVerticalGrid.Rows.MultiEditorRow();
                            //row_.SeparatorKind = DevExpress.XtraVerticalGrid.Rows.SeparatorKind.String;
                            row_.Name = strFieldName;
                            //row_.Properties.Caption = p.m_strParameterDescript;
                            row_.OptionsRow.AllowMove = false;
                            row_.OptionsRow.AllowSize = false;
                            row_.MaxCaptionLineCount = 0;

                            DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowItem1 = row_.PropertiesCollection.Add();
                            rowItem1.Caption = p.m_strParameterDescript;
                            rowItem1.FieldName = strFieldName;

                            /*DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowItem2 = row_.PropertiesCollection.Add();
                            rowItem2.Caption = "MPG City 2";
                            rowItem2.FieldName = strFieldName;
                            rowItem2.ReadOnly = true;*/
                            //rowItem2.Appearance.BackColor = Color.FromArgb(240, 240, 240);
                            //rowItem2.Appearance.Options.UseBackColor = true;

                            if (p.m_ParameterType == Inspection.ParameterType.Number)
                            {
                                row_.Properties.RowEdit = this.repositoryItemTextEdit1;
                                row_.Appearance.Options.UseTextOptions = true;
                                row_.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                                row_.Properties.Value = p.m_valNumber;
                                row_.Properties.ImageIndex = (int)p.m_valRange;
                                if (p.m_listLimits.Count > 0 || p.m_dictAlgLimits.Count > 0)
                                {
                                    row_.Properties.RowEdit.KeyDown += new KeyEventHandler(RowEdit_KeyDown);
                                    //row_.Properties.RowEdit.ContextMenuStrip = (popupParameter. as ContextMenuStrip);
                                }

                                if (m_type == Inspection.InspectionType.HARG /*&& p.m_CalcAlgorithm == null*/)
                                {
                                    DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowItem2 = row_.PropertiesCollection.Add();
                                    rowItem2.Caption = "";// p.m_strParameterDescript + " пред.";
                                    rowItem2.FieldName = "#";
                                    rowItem2.Width = 1;
                                    rowItem1.Width = 1000000;
                                }
                            }
                            if (p.m_ParameterType == Inspection.ParameterType.Integer)
                            {
                                row_.Properties.RowEdit = this.repositoryItemTextEdit3;
                                row_.Appearance.Options.UseTextOptions = true;
                                row_.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                                row_.Properties.Value = p.m_valNumber;
                                row_.Properties.ImageIndex = (int)p.m_valRange;
                                if (p.m_listLimits.Count > 0 || p.m_dictAlgLimits.Count > 0)
                                {
                                    row_.Properties.RowEdit.KeyDown += new KeyEventHandler(RowEdit_KeyDown);
                                    //row_.Properties.RowEdit.ContextMenuStrip = (popupParameter. as ContextMenuStrip);
                                }
                            }
                            if (p.m_ParameterType == Inspection.ParameterType.String)
                            {
                                row_.Properties.RowEdit = this.repositoryItemMemoEdit1;
                                //row_.Appearance.Options.UseTextOptions = true;
                                //row_.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
                                row_.Properties.Value = p.m_valString;
                                row_.Properties.ImageIndex = (int)p.m_valRange;
                                if (p.m_listLimits.Count > 0 || p.m_dictAlgLimits.Count > 0)
                                {
                                    row_.Properties.RowEdit.KeyDown += new KeyEventHandler(RowEdit_KeyDown);
                                }

                                if (m_type == Inspection.InspectionType.HARG /*&& p.m_CalcAlgorithm == null*/)
                                {
                                    DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowItem2 = row_.PropertiesCollection.Add();
                                    rowItem2.Caption = "";// p.m_strParameterDescript + " пред.";
                                    rowItem2.FieldName = "#";
                                    rowItem2.Width = 1;
                                    rowItem1.Width = 1000000;
                                }
                            }
                            if (p.m_ParameterType == Inspection.ParameterType.Date)
                            {
                                row_.Properties.RowEdit = this.repositoryItemDateEdit1;
                                row_.Properties.Value = p.m_valDate;
                                row_.Properties.ImageIndex = (int)p.m_valRange;
                                if (p.m_listLimits.Count > 0 || p.m_dictAlgLimits.Count > 0)
                                {
                                    row_.Properties.RowEdit.KeyDown += new KeyEventHandler(RowEdit_KeyDown);
                                }

                                if (m_type == Inspection.InspectionType.HARG /*&& p.m_CalcAlgorithm == null*/)
                                {
                                    DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowItem2 = row_.PropertiesCollection.Add();
                                    rowItem2.Caption = "";// p.m_strParameterDescript + " пред.";
                                    rowItem2.FieldName = "#";
                                    rowItem2.Width = 1;
                                    rowItem1.Width = 1000000;
                                }
                            }
                            if (p.m_ParameterType == Inspection.ParameterType.Select)
                            {
                                m_bExistTypeSelect = true;

                                BindingList<DataSourceString> list = new BindingList<DataSourceString>();

                                if (p.m_dicParameterSelect[0] != "") list.Add(new DataSourceString(0, p.m_dicParameterSelect[0]));
                                if (p.m_dicParameterSelect[1] != "") list.Add(new DataSourceString(1, p.m_dicParameterSelect[1]));
                                if (p.m_dicParameterSelect[2] != "") list.Add(new DataSourceString(2, p.m_dicParameterSelect[2]));
                                if (p.m_dicParameterSelect[3] != "") list.Add(new DataSourceString(3, p.m_dicParameterSelect[3]));

                                listBindings.Add(list);

                                RepositoryItemMyLookUpEdit/*DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit*/ ile = new RepositoryItemMyLookUpEdit();// new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                                ((System.ComponentModel.ISupportInitialize)(ile)).BeginInit();
                                this.SuspendLayout();
                                ile.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                                ile.AppearanceDropDown.Options.UseFont = true;
                                ile.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
                                ile.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] { new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VAL", "---") });
                                ile.DisplayMember = "VAL";
                                ile.Name = "repositoryItemLookUpEdit" + listItems.Count.ToString();
                                ile.NullText = "";
                                ile.ShowHeader = false;
                                //ile.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                                ile.ValidateOnEnterKey = true;
                                ile.ValueMember = "KEY";
                                this.ResumeLayout(false);
                                ((System.ComponentModel.ISupportInitialize)(ile)).EndInit();
                                ile.DataSource = list;
                                ile.DropDownRows = list.Count;
                                ile.ShowFooter = true;
                                //ile.AutoHeight = true;
                                ile.Appearance.Options.UseTextOptions = true;
                                ile.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                                ile.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                                ile.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Word;
                                ile.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                                ile.CreatePainter();
                                ile.KeyDown += new KeyEventHandler(ile_KeyDown);
                                //ile.Properties.BeforeShowingTooltip += new EventHandler(lookUpEditHints1_BeforeShowingTooltip);
                                //ile.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(ile_CustomDisplayText);

                                listItems.Add(ile);

                                row_.Properties.RowEdit = ile;
                                row_.Properties.Value = (long?)p.m_valSelect;
                                if (p.m_valSelect == null && p.m_defVal != null)
                                {
                                    p.m_valSelect = (Inspection.RangeResult?)Convert.ToInt64(p.m_defVal);
                                    row_.Properties.Value = Convert.ToInt64(p.m_defVal); // если есть значения по умолчанию и нет данных, то ставим его
                                }
                                if (p.m_valSelect != null)
                                {
                                    if (si.m_DefectAlgorithm == null)
                                        row_.Properties.ImageIndex = (int)p.m_valSelect;
                                }

                                if (m_type == Inspection.InspectionType.HARG /*&& p.m_CalcAlgorithm == null*/)
                                {
                                    DevExpress.XtraVerticalGrid.Rows.MultiEditorRowProperties rowItem2 = row_.PropertiesCollection.Add();
                                    rowItem2.Caption = "";// p.m_strParameterDescript + " пред.";
                                    rowItem2.FieldName = "#";
                                    rowItem2.Width = 1;
                                    rowItem1.Width = 1000000;
                                }

                                RecalcRowHeight(row_);
                            }

                            if (p.m_bReadOnly || m_list[i].m_dictUnionMainID.Count > 0 && m_list[i].m_dictUnionMainID.ContainsKey(m_sub_type_id))
                            {
                                row_.Appearance.BackColor = Color.FromArgb(240, 240, 240);
                                row_.Appearance.Options.UseBackColor = true;
                            }

                            row_.Tag = "данные отсутствуют";
                            if (p.m_strParameterDescript == "Примечание") row_.Tag = "заполнить при необходимости";
                            if (p.m_CalcAlgorithm != null) row_.Tag = "данные для расчета отсутствуют";
                            p.m_Row = row_;

                            catRow_.ChildRows.Add(row_);
                            if (cbTableView.SelectedIndex != 0) catRow_.Expanded = false;
                        }
                        si.m_Row = catRow_;
                        catRow_.Properties.ImageIndex = (int)si.m_dictValRange[m_sub_type_id].m_RangeResult;
                    }
                }
                catRow.Properties.ImageIndex = (int)m_list[i].m_dictValRange[m_sub_type_id];

                /*if (m_list[i].m_dictUnionMainID.Count > 0 && m_list[i].m_dictUnionMainID.ContainsKey(m_sub_type_id))
                {
                    catRow.Visible = false;
                }*/

                if (m_list[i].m_dictUnionExtraID.Count > 0 && m_list[i].m_dictUnionExtraID.ContainsKey(m_sub_type_id))
                {
                    string strName = m_list[i].m_strFunctionalSystemName;
                    for (int j = 0; j < m_list[i].m_dictUnionExtraID[m_sub_type_id].Count; j++)
                    {
                        long ExtraFunctionalSystemID = m_list[i].m_dictUnionExtraID[m_sub_type_id][j];
                        if (dictFunctionalSystemPos.ContainsKey(ExtraFunctionalSystemID))
                        {
                            strName = strName + ", " + m_list[dictFunctionalSystemPos[ExtraFunctionalSystemID]].m_strFunctionalSystemName;
                        }                        
                    }
                    catRow.Properties.Caption = strName;
                    if (catRow.ChildRows.Count == 1) catRow.ChildRows[0].Properties.Caption = strName;
                }

                m_list[i].m_Row = catRow;

                int ind = GridVertical.Rows.Add(catRow);
                if (cbTableView.SelectedIndex == 2) catRow.Expanded = false;
                GridVertical.Rows[ind].Appearance.BackColor = Color.FromArgb(255, 190, 125);
                GridVertical.Rows[ind].Appearance.ForeColor = Color.Black;
            }

            GridVertical.EndUpdate();

            //GridVertical.LayoutStyle = DevExpress.XtraVerticalGrid.LayoutViewStyle.MultiRecordView;
        }

        void RowEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.I && !e.Handled)
            {
                string row_name = GridVertical.FocusedRow.Properties.FieldName;
                if (row_name == "#") return;
                long iSystemItemID = -1;
                long iParameterID = -1;

                int pos = row_name.IndexOf("_");
                iSystemItemID = Convert.ToInt64(row_name.Substring(0, pos));
                iParameterID = Convert.ToInt64(row_name.Substring(pos + 1));

                int SystemItemPos = dictSystemItemPos[iSystemItemID];
                int FunctionalSystemPos = dictFunctionalSystemPos[dictSystemItemsFunctionalSystems[iSystemItemID]];

               // Inspection.ParameterType ParamType = Inspection.ParameterType.Number;
                int param_index = -1;

                SystemItem si = m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos];

                for (int i = 0; i < si.m_listParams.Count; i++)
                {
                    Parameter p = si.m_listParams[i];
                    if (p.m_ParameterID == iParameterID)
                    {
                        //ParamType = p.m_ParameterType;
                        param_index = i;
                        break;
                    }
                }

                if (param_index < 0) return;

                Parameter p_ = si.m_listParams[param_index];

                if (p_.m_listLimits.Count > 0)
                {
                    ParameterLimitForm f = new ParameterLimitForm();
                    f.m_listLimits = p_.m_listLimits;
                    f.ShowDialog(this);
                }
                if (p_.m_dictAlgLimits.Count > 0)
                {
                    ParameterLimitAlgForm f = new ParameterLimitAlgForm();
                    f.m_dictAlgLimits = p_.m_dictAlgLimits;
                    f.m_type = m_type;
                    f.ShowDialog(this);
                }
                e.Handled = true;
            }
            //throw new NotImplementedException();
        }

        void RowEdit_MouseDown(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            /*if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                popupParameter.ShowPopup(new Point(e.X, e.Y));
            }*/
        }

        void ile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DevExpress.XtraVerticalGrid.Rows.BaseRow row = GridVertical.FocusedRow;
                GridVertical.BeginUpdate();
                GridVertical.SetCellValue(row, 0, DBNull.Value);
                GridVertical.EndUpdate();
                //GridVertical.Refresh();
            }
        }

        /*void ile_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            //throw new NotImplementedException();
        }*/

        private void InspectionDataForm_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;

            //string strSeparator = System.Globalization.NumberFormatInfo.InvariantInfo.NumberDecimalSeparator;
            string strSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (strSeparator == ".") strSeparator = "\\.";
            this.repositoryItemTextEdit1.Mask.EditMask = "(\\d+|\\d+" + strSeparator + "\\d|\\d+" + strSeparator + "\\d\\d|\\d+" + strSeparator
                + "\\d\\d\\d|\\d+" + strSeparator + "\\d\\d\\d\\d|\\d+" + strSeparator + "\\d\\d\\d\\d\\d|\\d+" + strSeparator + "\\d\\d\\d\\d\\d\\d)";
            this.repositoryItemTextEdit1.KeyPress += new KeyPressEventHandler(repositoryItemTextEdit1_KeyPress);
            this.repositoryItemTextEdit1.MouseWheel += new MouseEventHandler(repositoryItemTextEdit1_MouseWheel);

            ttc = toolTip;

            cbTableView.SelectedIndex = 0;

            panelSubType.Visible = false;
            panelAlgorithm.Visible = false;

            switch (m_type)
            {
                case Inspection.InspectionType.Visual:
                    gcInspectionTime.Text = "Визуальное обследование";
                    break;
                case Inspection.InspectionType.HARG:
                    if ((Equipment.EquipmentKind)m_EquipmentKindID == Equipment.EquipmentKind.Transformer)
                        gcInspectionTime.Text = "ХАРГ";
                    else
                        gcInspectionTime.Text = "ХАРГ";
                    
                    //panelSubType.Visible = true;
                    //cbSubType.Properties.Items.Add("РД Методические указания по диагностике");
                    //cbSubType.Properties.Items.Add("%");

                    break;
                case Inspection.InspectionType.FHA:
                    if ((Equipment.EquipmentKind)m_EquipmentKindID == Equipment.EquipmentKind.Transformer)
                        gcInspectionTime.Text = "ФХА";
                    else
                        gcInspectionTime.Text = "ФХА";
                    break;
                case Inspection.InspectionType.Warm:
                    gcInspectionTime.Text = "Тепловизионный контроль";
                    break;
                case Inspection.InspectionType.Vibro:
                    gcInspectionTime.Text = "Вибрационное обследование";
                    
                    //panelSubType.Visible = true;
                    //cbSubType.Properties.Items.Add("программа Веста");
                    //cbSubType.Properties.Items.Add("вручную");
                    
                    break;
                case Inspection.InspectionType.Parameter:
                    gcInspectionTime.Text = "Определение характеристик выключателя";
                    break;
                case Inspection.InspectionType.Electrical:
                    gcInspectionTime.Text = "Электрические измерения выключателя";
                    break;
                default:
                    break;
            }

            m_Graphics = GridVertical.CreateGraphics();

            //LoadData();
            CreateGrid();

            if (m_InspectionID <= 0)
                this.Text = "Добавить обследование";
            else
                this.Text = "Изменить обследование";

            if (m_list_sub_types.Count > 0) panelSubType.Visible = true;
            //if (m_list_algorithms.Count > 0) panelAlgorithm.Visible = true;

            m_bDataLoad = false;
            //GetLastValuesForHARG();
            dtInspectionDate_EditValueChanged(dtInspectionDate, null);

            m_bDataLoadEnd = true;

            if (m_type == Inspection.InspectionType.HARG ||
                m_type == Inspection.InspectionType.FHA)
            {
                cbShowDefect.Checked = true;
            }


            /*if (m_bShowContinueMsg && m_type != Inspection.InspectionType.Vibro)
            {
                bNext.Visible = true;
            }*/

            if (m_bShowContinueMsg)
            {
                bool bEnd = false;
                bool bPassport = false;
                Inspection.InspectionType prev_type = GetPrevType(m_EquipmentKindID, m_type, out bPassport);
                Inspection.InspectionType next_type = GetNextType(m_EquipmentKindID, m_type, out bEnd);

                bPrev.Visible = true;
                bNext.Visible = true;

                if (bPassport)
                {
                    bPrev.Text = "Паспортные данные";
                }
                else
                {
                    switch (prev_type)
                    {
                        case Inspection.InspectionType.Visual:
                            bPrev.Text = "Визуальное обследование";
                            break;
                        case Inspection.InspectionType.FHA:
                            if ((Equipment.EquipmentKind)m_EquipmentKindID == Equipment.EquipmentKind.Transformer)
                                bPrev.Text = "ФХА";
                            else
                                bPrev.Text = "ФХА";
                            break;
                        case Inspection.InspectionType.HARG:
                            if ((Equipment.EquipmentKind)m_EquipmentKindID == Equipment.EquipmentKind.Transformer)
                                bPrev.Text = "ХАРГ";
                            else
                                bPrev.Text = "ХАРГ";
                            break;
                        case Inspection.InspectionType.Warm:
                            bPrev.Text = "Тепловизионный контроль";
                            break;
                        case Inspection.InspectionType.Vibro:
                            bPrev.Text = "Вибрационное обследование";
                            break;
                        case Inspection.InspectionType.Parameter:
                            bPrev.Text = "Определение характеристик выключателя";
                            break;
                        case Inspection.InspectionType.Electrical:
                            bPrev.Text = "Электрические измерения выключателя";
                            break;
                    }
                }

                if (bEnd)
                {
                    bNext.Text = "Завершить";
                }
                else
                {
                    switch (next_type)
                    {
                        case Inspection.InspectionType.Visual:
                            bNext.Text = "Визуальное обследование";
                            break;
                        case Inspection.InspectionType.FHA:
                            if ((Equipment.EquipmentKind)m_EquipmentKindID == Equipment.EquipmentKind.Transformer)
                                bNext.Text = "ФХА";
                            else
                                bNext.Text = "ФХА";
                            break;
                        case Inspection.InspectionType.HARG:
                            if ((Equipment.EquipmentKind)m_EquipmentKindID == Equipment.EquipmentKind.Transformer)
                                bNext.Text = "ХАРГ";
                            else
                                bNext.Text = "ХАРГ";
                            break;
                        case Inspection.InspectionType.Warm:
                            bNext.Text = "Тепловизионный контроль";
                            break;
                        case Inspection.InspectionType.Vibro:
                            bNext.Text = "Вибрационное обследование";
                            break;
                        case Inspection.InspectionType.Parameter:
                            bNext.Text = "Определение характеристик выключателя";
                            break;
                        case Inspection.InspectionType.Electrical:
                            bNext.Text = "Электрические измерения выключателя";
                            break;
                    }
                }                
            }

            if (m_type == Inspection.InspectionType.HARG || m_type == Inspection.InspectionType.FHA)
            {
                for (int i = 0; i < m_list.Count; i++)
                {
                    for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                    {
                        for (int k = 0; k < m_list[i].m_listSystemItems[j].m_listParams.Count; k++)
                        {
                            if (m_list[i].m_listSystemItems[j].m_listParams[k].m_strParameterName == "Unit")
                            {
                                VerifyParameterForHARG_FHA(i, j, k, true);
                                break;
                            }
                        }
                    }
                }                
            }
        }

        void repositoryItemTextEdit1_MouseWheel(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void repositoryItemTextEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                if (e.KeyChar == '.') e.KeyChar = ',';

            if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
                if (e.KeyChar == ',') e.KeyChar = '.';
        }

        private void CalcParameter(int FunctionalSystemPos, int SystemItemPos, int EditableParamIndex)
        {
            GridVertical.BeginUpdate();

            try
            {
                SystemItem si = m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos];
                // расчет вычисляемых полей
                for (int i = 0; i < si.m_listParams.Count; i++)
                {
                    Parameter p = si.m_listParams[i];
                    if (p.m_InspectionSubType != null &&
                        p.m_InspectionSubType != m_sub_type_id)
                    {
                        continue;
                    }

                    Parameter.CalcAlgorithm ca = p.m_CalcAlgorithm;

                    if (ca != null && (ca.m_algType == Inspection.CalcType.Calc || ca.m_algType == Inspection.CalcType.Calc_if_all)) // используем только те, у которых указаны пределы или алгоритм для вычисления
                    {
                        bool bfind = false;
                        for (int j = 0; j < ca.m_arrParameterIDs.Count; j++)
                        {
                            if (ca.m_arrParameterIDs[j] == si.m_listParams[EditableParamIndex].m_ParameterID)
                            {
                                bfind = true;
                                break;
                            }
                        }    
                        if (bfind)
                            si.CalcFieldValue(this, i);
                    }
                }
            }
            catch (Exception)
            {
                //MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            GridVertical.EndUpdate();
        }

        private void CalcRangeSystemItem(int FunctionalSystemPos, int SystemItemPos)
        {
            GridVertical.BeginUpdate();

            try
            {
                // расчет оценки
                //Inspection.InspectionResult max_val = new Inspection.InspectionResult(Inspection.RangeResult.None);

                SystemItem si = m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos];
                
                si.m_dictValRange[m_sub_type_id].m_dictDefectIDs.Clear();
                si.m_dictValRange[m_sub_type_id].m_dictParameterIDs.Clear();
                si.m_dictValRange[m_sub_type_id].m_RangeResult = Inspection.RangeResult.None;

                if (si.m_DefectAlgorithm != null)
                {
                    List<Inspection.DefectInfo> listDefects = si.CalcResultRange(this);
                    for (int i = 0; i < listDefects.Count; i++)
                    {
                        si.m_dictValRange[m_sub_type_id].Add(null, new Inspection.DefectInfo((long)listDefects[i].m_ID, listDefects[i].m_Result));                        
                    }
                }
                else
                {
                    for (int i = 0; i < si.m_listParams.Count; i++)
                    {
                        Parameter p = si.m_listParams[i];
                        if (p.m_InspectionSubType != null &&
                            p.m_InspectionSubType != m_sub_type_id)
                        {
                            continue;
                        }

                        Inspection.InspectionResult val = new Inspection.InspectionResult(Inspection.RangeResult.None);
                        if (p.m_ParameterType == Inspection.ParameterType.Select)
                        {
                            if (p.m_valSelect != null)
                            {
                                //val = new Inspection.InspectionResult((Inspection.RangeResult)p.m_valSelect, p.m_strDefectName, p.m_DefectID);
                                p.m_valRange = (Inspection.RangeResult)p.m_valSelect;
                                
                                if (p.m_DefectID != null)
                                    si.m_dictValRange[m_sub_type_id].Add(null, new Inspection.DefectInfo((long)p.m_DefectID, p.m_valRange));
                                else
                                    si.m_dictValRange[m_sub_type_id].Add(new Inspection.DefectInfo((long)p.m_ParameterID, p.m_valRange), null);
                            }
                            else
                                p.m_valRange = Inspection.RangeResult.None;

                            GridVertical.BeginUpdate();
                            p.m_Row.Properties.ImageIndex = (int)p.m_valRange;
                            GridVertical.EndUpdate();
                        }
                        else
                        {
                            //val = new Inspection.InspectionResult(Inspection.RangeResult.None);

                            if (p.m_listLimits.Count > 0) // используем только те, у которых указаны пределы
                            {
                                long? InputVoltageType = null;
                                if (si.m_InputVoltageIndex != null)
                                {
                                    if (dictInputVoltageIndexes.ContainsKey((long)si.m_InputVoltageIndex))
                                        InputVoltageType = dictInputVoltageIndexes[(long)si.m_InputVoltageIndex];
                                }

                                long? VoltageValue = m_VoltageValue_VN;
                                if (si.bIsVoltageRPN) VoltageValue = m_VoltageValue_RPN;
                                if (si.bIsVoltageVN) VoltageValue = m_VoltageValue_VN;
                                if (si.bIsVoltageSN) VoltageValue = m_VoltageValue_SN;
                                if (si.bIsVoltageNeitral) VoltageValue = m_VoltageValue_Neitral;

                                Inspection.RangeResult result = p.CalcResultRange(VoltageValue, m_ProtectionOilType, InputVoltageType, m_YearsValue);
                                p.m_valRange = result;// (Inspection.RangeResult)val.m_RangeResult;
                                
                                GridVertical.BeginUpdate();
                                p.m_Row.Properties.ImageIndex = (int)p.m_valRange;
                                GridVertical.EndUpdate();

                                if (result != Inspection.RangeResult.None)
                                {
                                    if (p.m_DefectID != null)
                                        si.m_dictValRange[m_sub_type_id].Add(null, new Inspection.DefectInfo((long)p.m_DefectID, result));
                                    else
                                        si.m_dictValRange[m_sub_type_id].Add(new Inspection.DefectInfo((long)p.m_ParameterID, result), null);
                                }
                            }
                        }
                        /*if (val.m_RangeResult > max_val.m_RangeResult)
                        {
                            max_val = val;
                        }
                        else
                        {
                            // объединяем описания дефектов
                            if (val.m_RangeResult == max_val.m_RangeResult)
                            {
                                max_val.AddResult(val);
                            }
                        }*/
                    }
                }

                si.m_dictValRange[m_sub_type_id].CalcMaxRange();

                /*if (max_val.m_RangeResult == Inspection.RangeResult.None 
                    || max_val.m_RangeResult == Inspection.RangeResult.Green) max_val.m_strResult = "";

                si.m_dictValRange[m_sub_type_id] = max_val;*/

                GridVertical.BeginUpdate();
                si.m_Row.Properties.ImageIndex = (int)si.m_dictValRange[m_sub_type_id].m_RangeResult;
                if (cbShowDefect.Checked)
                {
                    si.m_Row.Properties.Caption = si.m_strSystemItemName + si.m_dictValRange[m_sub_type_id].GetDefectName(m_dictParamName, m_dictDefectName);
                }
                GridVertical.EndUpdate();

                // если от данного узла зависит расчет другого узла, то вызываем эту функцию для него
                if (dictDependSystemCalc.ContainsKey(si.m_SystemItemID))
                {
                    foreach (KeyValuePair<int, List<int>> pr in dictDependSystemCalc[si.m_SystemItemID])
                    {
                        for (int i = 0; i < pr.Value.Count; i++)
                        {
                            CalcRangeSystemItem(pr.Key, pr.Value[i]);
                        }
                        CalcRangeFunctionalSystem(pr.Key);
                    }
                }
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            GridVertical.EndUpdate();
        }

        private void CalcRangeFunctionalSystem(int FunctionalSystemPos)
        {
            GridVertical.BeginUpdate();

            try
            {
                // по всем узлам
                Inspection.RangeResult max_val = Inspection.RangeResult.None;
                for (int i = 0; i < m_list[FunctionalSystemPos].m_listSystemItems.Count; i++)
                {
                    Inspection.RangeResult val = m_list[FunctionalSystemPos].m_listSystemItems[i].m_dictValRange[m_sub_type_id].m_RangeResult;
                    if (val > max_val) max_val = val;
                }
                m_list[FunctionalSystemPos].m_dictValRange[m_sub_type_id] = max_val;
                m_list[FunctionalSystemPos].m_Row.Properties.ImageIndex = (int)max_val;
            }
            catch (Exception)
            {
            }

            GridVertical.EndUpdate();
        }

        private void RecalcRowHeight(DevExpress.XtraVerticalGrid.Rows.BaseRow row)
        {
            //GridVertical.BeginUpdate();

            string strVal = GridVertical.GetCellDisplayText(row, 0);
            if (strVal != "")
            {
                string strHeader = row.Properties.Caption;
                int width = GridVertical.Width;
                int record_width = (int)(GridVertical.RecordWidth * width / 200.0) - 6;
                int header_width = (int)(GridVertical.RecordWidth * width / 200.0) - 6;

                if (row.Properties.ImageIndex >= 0) header_width -= (GridVertical.ImageList as ImageList).ImageSize.Width;

                Size record_size = GridVertical.Appearance.RecordValue.CalcTextSize(m_Graphics, strVal, record_width).ToSize();
                Size header_size = GridVertical.Appearance.RecordValue.CalcTextSize(m_Graphics, strHeader, header_width).ToSize();

                if (record_size.Height > header_size.Height)
                    row.Height = record_size.Height + 2;
                else
                    row.Height = -1;
            }
            else
                row.Height = -1;

            //GridVertical.EndUpdate();
        }

        private void VerifyParameter(SystemItem si, int ind, double mult)
        {
            if (si.m_listParams[ind].m_strParameterName == "H2" ||
                si.m_listParams[ind].m_strParameterName == "CH4" ||
                si.m_listParams[ind].m_strParameterName == "C2H2" ||
                si.m_listParams[ind].m_strParameterName == "C2H4" ||
                si.m_listParams[ind].m_strParameterName == "C2H6" ||
                si.m_listParams[ind].m_strParameterName == "CO" ||
                si.m_listParams[ind].m_strParameterName == "CO2")
            {
                if (mult < 0)
                {
                    mult = 1;
                    // ищем параметр Unit
                    for (int i = 0; i < si.m_listParams.Count; i++)
                    {
                        if (si.m_listParams[i].m_strParameterName == "Unit")
                        {
                            if (si.m_listParams[i].m_valSelect != null && si.m_listParams[i].m_valSelect != (Inspection.RangeResult?)0) mult = 10000; // для % умножаем значения на 10000
                            break;
                        }
                    }
                }
            
                long? VoltageValue = m_VoltageValue_VN;
                if (si.bIsVoltageRPN) VoltageValue = m_VoltageValue_RPN;
                if (si.bIsVoltageVN) VoltageValue = m_VoltageValue_VN;
                if (si.bIsVoltageSN) VoltageValue = m_VoltageValue_SN;
                if (si.bIsVoltageNeitral) VoltageValue = m_VoltageValue_Neitral;
            
                long? YearsValue = m_YearsValue;
                long? ProtectionOilType = m_ProtectionOilType;
            
                double max_val = si.m_listParams[ind].GetMaxValue(VoltageValue, YearsValue, ProtectionOilType, Inspection.AlgorithmType.AT_RD);
            
                if (si.m_listParams[ind].m_valNumber != null &&
                    si.m_listParams[ind].m_valNumber * mult > /*1.5 **/ max_val)
                {
                    si.m_listParams[ind].m_Row.Appearance.ForeColor = Color.Red;//.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204))); ;
                }
                else
                {
                    si.m_listParams[ind].m_Row.Appearance.ForeColor = Color.Black;//.ForeColor = Color.Red;//.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204))); ;
                }
            }
        }

        private void VerifyParameterForHARG_FHA(int FunctionalSystemPos, int SystemItemPos, int EditableParamIndex, bool bNeedUpdateGrid)
        {
            if (bNeedUpdateGrid)
                GridVertical.BeginUpdate();

            SystemItem si = m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos];

            if (si.m_listParams[EditableParamIndex].m_strParameterName == "Unit")
            {
                double mult = 1;
                if (si.m_listParams[EditableParamIndex].m_valSelect != null && si.m_listParams[EditableParamIndex].m_valSelect != (Inspection.RangeResult?)0) mult = 10000;

                for (int ii = 0; ii < si.m_listParams.Count; ii++)
                {
                    VerifyParameter(si, ii, mult);
                }
            }
            else
            {
                VerifyParameter(si, EditableParamIndex, -1);
            }

            if (bNeedUpdateGrid)
                GridVertical.EndUpdate();
        }

        private Parameter CellValueChanged(object eValue, long iSystemItemID, long iParameterID, bool bShowMessage)
        {
            int SystemItemPos = dictSystemItemPos[iSystemItemID];
            int FunctionalSystemPos = dictFunctionalSystemPos[dictSystemItemsFunctionalSystems[iSystemItemID]];

            Inspection.ParameterType ParamType = Inspection.ParameterType.Number;
            int param_index = -1;

            SystemItem si = m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos];

            for (int i = 0; i < si.m_listParams.Count; i++)
            {
                Parameter p = si.m_listParams[i];
                if (p.m_ParameterID == iParameterID)
                {
                    ParamType = p.m_ParameterType;
                    param_index = i;
                    break;
                }
            }

            if (param_index < 0) return null;

            Parameter p_ = si.m_listParams[param_index];
            if (ParamType == Inspection.ParameterType.Select)
            {
                p_.m_valSelect = null;
                if (eValue != null && eValue.ToString() != "") p_.m_valSelect = (Inspection.RangeResult)Convert.ToInt64(eValue);
                else
                {
                    if (p_.m_defVal != null)
                    {
                        p_.m_valSelect = (Inspection.RangeResult?)Convert.ToInt64(p_.m_defVal);
                        GridVertical.BeginUpdate();
                        /*e.Row*/p_.m_Row.Properties.Value = Convert.ToInt64(p_.m_defVal); // если есть значения по умолчанию и нет данных, то ставим его
                        GridVertical.EndUpdate();
                    }
                }

                // пересчитываем высоту ячейки
                GridVertical.BeginUpdate();
                RecalcRowHeight(/*e.Row*/p_.m_Row);
                GridVertical.EndUpdate();
            }
            if (ParamType == Inspection.ParameterType.String)
            {
                p_.m_valString = null;
                if (eValue != null && eValue.ToString() != "") p_.m_valString = Convert.ToString(eValue);

                // пересчитываем высоту ячейки
                GridVertical.BeginUpdate();
                RecalcRowHeight(/*e.Row*/p_.m_Row);
                GridVertical.EndUpdate();
            }
            if (ParamType == Inspection.ParameterType.Date)
            {
                p_.m_valDate = null;
                if (eValue != null && eValue.ToString() != "") p_.m_valDate = Convert.ToDateTime(eValue);
                else
                {
                    GridVertical.BeginUpdate();
                    p_.m_Row.Properties.Value = null;
                    GridVertical.EndUpdate();
                }

                // пересчитываем высоту ячейки
                //GridVertical.BeginUpdate();
                //RecalcRowHeight(/*e.Row*/p_.m_Row);
                //GridVertical.EndUpdate();
            }
            if (ParamType == Inspection.ParameterType.Number || ParamType == Inspection.ParameterType.Integer)
            {
                double? valNumber = null;
                if (eValue != null && eValue.ToString() != "") valNumber = Convert.ToDouble(eValue);

                /*if (p_.m_maxVal != null && valNumber != null && valNumber > p_.m_maxVal ||
                    p_.m_minVal != null && valNumber != null && valNumber < p_.m_minVal)
                {
                    if (p_.m_defVal != null)
                    {
                        //MyLocalizer.XtraMessageBoxShow("Значение параметра \"" + p_.m_strParameterDescript + "\" выходит за пределы допустимых значений.\nПараметру будет присвоено значение " + p_.m_defVal.ToString() + ".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        valNumber = p_.m_defVal;
                        if (bShowMessage)
                        {
                            string strMessage = "Значение параметра \"" + p_.m_strParameterDescript + "\" выходит за пределы допустимых значений.\nПараметру будет присвоено значение " + p_.m_defVal.ToString() + ".";
                            backgroundMessage.RunWorkerAsync(strMessage);
                        }
                    }
                    else
                    {
                        //MyLocalizer.XtraMessageBoxShow("Значение параметра \"" + p_.m_strParameterDescript + "\" выходит за пределы допустимых значений.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        valNumber = p_.m_valNumber;
                        if (bShowMessage)
                        {
                            string strMessage = "Значение параметра \"" + p_.m_strParameterDescript + "\" выходит за пределы допустимых значений.";
                            backgroundMessage.RunWorkerAsync(strMessage);
                        }

                    }
                    GridVertical.BeginUpdate();
                    p_.m_Row.Properties.Value = valNumber;
                    GridVertical.EndUpdate();
                }

                if (p_.m_notEqualVal != null && valNumber != null && Math.Abs((double)p_.m_notEqualVal - (double)valNumber) <= 0.0000009)
                {
                    if (p_.m_defVal != null)
                    {
                        //MyLocalizer.XtraMessageBoxShow("Недопустимое значение параметра \"" + p_.m_strParameterDescript + "\".\nПараметру будет присвоено значение " + p_.m_defVal.ToString() + ".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        valNumber = p_.m_defVal;
                        if (bShowMessage)
                        {
                            string strMessage = "Недопустимое значение параметра \"" + p_.m_strParameterDescript + "\".\nПараметру будет присвоено значение " + p_.m_defVal.ToString() + ".";
                            backgroundMessage.RunWorkerAsync(strMessage);
                        }
                    }
                    else
                    {
                        //MyLocalizer.XtraMessageBoxShow("Недопустимое значение параметра \"" + p_.m_strParameterDescript + "\".", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        valNumber = p_.m_valNumber;
                        if (bShowMessage)
                        {
                            string strMessage = "Недопустимое значение параметра \"" + p_.m_strParameterDescript + "\".";
                            backgroundMessage.RunWorkerAsync(strMessage);
                        }
                    }
                    GridVertical.BeginUpdate();
                    p_.m_Row.Properties.Value = valNumber;
                    GridVertical.EndUpdate();
                }*/

                p_.m_valNumber = valNumber;
            }

            if (m_type == Inspection.InspectionType.HARG /*|| m_type == Inspection.InspectionType.FHA*/)
            {
                VerifyParameterForHARG_FHA(FunctionalSystemPos, SystemItemPos, param_index, true);
            }

            // пересчитываем данные по узлу
            CalcParameter(FunctionalSystemPos, SystemItemPos, param_index);
            CalcRangeSystemItem(FunctionalSystemPos, SystemItemPos);
            CalcRangeFunctionalSystem(FunctionalSystemPos);

            // объединенным подузлам заносим данные с основных подузлов
            if (m_list[FunctionalSystemPos].m_dictUnionExtraID.Count > 0 && m_list[FunctionalSystemPos].m_dictUnionExtraID.ContainsKey(m_sub_type_id))
            {
                for (int k = 0; k < m_list[FunctionalSystemPos].m_dictUnionExtraID[m_sub_type_id].Count; k++)
                {
                    long ExtraFunctionalSystemID = m_list[FunctionalSystemPos].m_dictUnionExtraID[m_sub_type_id][k];
                    if (dictFunctionalSystemPos.ContainsKey(ExtraFunctionalSystemID))
                    {
                        int ExtraFunctionalSystemPos = dictFunctionalSystemPos[ExtraFunctionalSystemID];
                        m_list[ExtraFunctionalSystemPos].m_dictValRange[m_sub_type_id] = m_list[FunctionalSystemPos].m_dictValRange[m_sub_type_id];

                        if (m_list[ExtraFunctionalSystemPos].m_listSystemItems.Count >= SystemItemPos)
                        {
                            m_list[ExtraFunctionalSystemPos].m_listSystemItems[SystemItemPos].m_dictValRange[m_sub_type_id] = m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos].m_dictValRange[m_sub_type_id];
                        }

                        /*for (int i = 0; i < m_list[ExtraFunctionalSystemPos].m_listSystemItems.Count; i++)
                        {
                            SystemItem si2 = m_list[ExtraFunctionalSystemPos].m_listSystemItems[i];
                            for (int j = 0; j < si2.m_listParams.Count; j++)
                            {
                                Parameter p2 = si2.m_listParams[j];

                                if (p2.m_InspectionSubType != null &&
                                    p2.m_InspectionSubType != m_sub_type_id)
                                {
                                    continue;
                                }

                                //if (m_list[ExtraFunctionalSystemPos].m_listSystemItems[i].m_listParams[j].m_ParameterID == ParameterID)
                                if (p2.m_strParameterDescript == p_.m_strParameterDescript)
                                {
                                    p2.m_valNumber = p_.m_valNumber;
                                    p2.m_valSelect = p_.m_valSelect;

                                    if (p2.m_ParameterType == Inspection.ParameterType.Number)
                                    {
                                        p2.m_Row.Properties.Value = p2.m_valNumber;
                                    }
                                    else
                                    {
                                        p2.m_Row.Properties.Value = (long?)p2.m_valSelect;
                                    }
                                    CalcParameter(ExtraFunctionalSystemPos, i, j);
                                }
                            }
                            CalcRangeSystemItem(ExtraFunctionalSystemPos, i);
                        }
                        CalcRangeFunctionalSystem(ExtraFunctionalSystemPos);*/
                    }
                }
            }

            return p_;
        }

        private void GridVertical_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;

            string row_name = e.Row.Properties.FieldName;
            if (row_name == "#") return;
            long iSystemItemID = -1;
            long iParameterID = -1;

            int pos = row_name.IndexOf("_");
            iSystemItemID = Convert.ToInt64(row_name.Substring(0, pos));
            iParameterID = Convert.ToInt64(row_name.Substring(pos + 1));

            Parameter p = CellValueChanged(e.Value, iSystemItemID, iParameterID, true);

            if (p != null && p.m_listDepends != null)
            {
                foreach (KeyValuePair<long, long> pair in p.m_listDepends)
                {
                    object value = null;
                    if (p.m_ParameterType == Inspection.ParameterType.Select) value = p.m_valSelect;
                    if (p.m_ParameterType == Inspection.ParameterType.Number || p.m_ParameterType == Inspection.ParameterType.Integer) value = p.m_valNumber;
                    if (p.m_ParameterType == Inspection.ParameterType.String) value = p.m_valString;
                    if (p.m_ParameterType == Inspection.ParameterType.Date) value = p.m_valDate;

                    Parameter p_ = CellValueChanged(value, pair.Key, pair.Value, false);
                    GridVertical.BeginUpdate();
                    if (p_.m_ParameterType == Inspection.ParameterType.Select) p_.m_Row.Properties.Value = p.m_valSelect;
                    if (p_.m_ParameterType == Inspection.ParameterType.Number || p_.m_ParameterType == Inspection.ParameterType.Integer) p_.m_Row.Properties.Value = p.m_valNumber;
                    if (p_.m_ParameterType == Inspection.ParameterType.String) p_.m_Row.Properties.Value = p.m_valString;
                    if (p_.m_ParameterType == Inspection.ParameterType.Date) p_.m_Row.Properties.Value = p.m_valDate;
                    GridVertical.EndUpdate();
                }
            }
        }

        private void cbTableView_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridVertical.BeginUpdate();
            if (cbTableView.SelectedIndex == 0)
            {
                for (int i = 0; i < GridVertical.Rows.Count; i++)
                {
                    GridVertical.Rows[i].Expanded = true;
                    for (int j = 0; j < GridVertical.Rows[i].ChildRows.Count; j++)
                    {
                        GridVertical.Rows[i].ChildRows[j].Expanded = true;
                    }                    
                }
            }

            if (cbTableView.SelectedIndex == 1)
            {
                for (int i = 0; i < GridVertical.Rows.Count; i++)
                {
                    GridVertical.Rows[i].Expanded = true;
                    for (int j = 0; j < GridVertical.Rows[i].ChildRows.Count; j++)
                    {
                        GridVertical.Rows[i].ChildRows[j].Expanded = false;
                    }
                }
            }

            if (cbTableView.SelectedIndex == 2)
            {
                for (int i = 0; i < GridVertical.Rows.Count; i++)
                {
                    GridVertical.Rows[i].Expanded = false;
                }
            }
            GridVertical.EndUpdate();
        }

        private bool VerifyDataForHARG_FHA()
        {
            bool bShowVotnMsg = m_bShowVotnMsg;

            for (int i = 0; i < m_list.Count; i++)
            {
                long FunctionalSystemID = m_list[i].m_FunctionalSystemID;
                for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                {
                    Parameter pProtocolDate = null;
                    Parameter pProbeDate = null;
                    Parameter pProtocolNumber = null;
                    Parameter pVotn = null;
                    Parameter pUnit = null;
                    List<Parameter> listValues = new List<Parameter>();
                    bool bDataExists = false;

                    SystemItem si = m_list[i].m_listSystemItems[j];
                    long SystemItemID = si.m_SystemItemID;
                    for (int k = 0; k < si.m_listParams.Count; k++)
                    {
                        Parameter p = si.m_listParams[k];
                        if (p.m_InspectionSubType != null &&
                            p.m_InspectionSubType != m_sub_type_id)
                        {
                            continue;
                        }

                        if (si.m_listParams[k].m_ParameterType == Inspection.ParameterType.Number || 
                            si.m_listParams[k].m_ParameterType == Inspection.ParameterType.Integer)
                        {
                            if (si.m_listParams[k].m_valNumber != null)
                            {
                                bDataExists = true;
                            }
                        }

                        if (si.m_listParams[k].m_strParameterName == "ProtocolDate")
                        {
                            pProtocolDate = si.m_listParams[k];
                        }

                        if (si.m_listParams[k].m_strParameterName == "ProtocolNumber")
                        {
                            pProtocolNumber = si.m_listParams[k];
                        }

                        if (m_type == Inspection.InspectionType.HARG)
                        {
                            if (si.m_listParams[k].m_strParameterName == "Unit")
                            {
                                pUnit = si.m_listParams[k];
                            }

                            if ((si.m_listParams[k].m_ParameterType == Inspection.ParameterType.Number ||
                                si.m_listParams[k].m_ParameterType == Inspection.ParameterType.Integer) &&
                                si.m_listParams[k].m_CalcAlgorithm == null)
                            {
                                listValues.Add(si.m_listParams[k]);
                            }

                            if (si.m_listParams[k].m_strParameterName == "ProbeDate")
                            {
                                pProbeDate = si.m_listParams[k];

                                if (pProbeDate.m_valDate != null)
                                {
                                    if (dictLastValuesForHARG.ContainsKey(si.m_SystemItemID))
                                    {
                                        for (int ii = 0; ii < dictLastValuesForHARG[si.m_SystemItemID].Count; ii++)
                                        {
                                            ParameterLite pl = dictLastValuesForHARG[si.m_SystemItemID][ii];
                                            if (pl.m_strParameterName == si.m_listParams[k].m_strParameterName)
                                            {
                                                if (pl.m_valDate != null)
                                                {
                                                    TimeSpan duration = ((DateTime)pProbeDate.m_valDate).Date - ((DateTime)pl.m_valDate).Date;

                                                    if (si.m_InputVoltageIndex == null /*"Обмотки" || "Магнитопровод"*/)
                                                    {
                                                        if (duration.TotalDays / 30 > 6)
                                                        {
                                                            GridVertical.FocusedRow = si.m_listParams[k].m_Row;
                                                            GridVertical.MakeRowVisible(si.m_listParams[k].m_Row);
                                                            if (MyLocalizer.XtraMessageBoxShow("\"" + m_list[i].m_strFunctionalSystemName + "/" + m_list[i].m_listSystemItems[j].m_strSystemItemName + "\"" + (char)10 + "данные обследования масла из бака не должны быть старше 6 месяцев. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                                                            {
                                                                GridVertical.ShowEditor();
                                                                return false;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        long? VoltageValue = m_VoltageValue_VN;
                                                        if (si.bIsVoltageRPN) VoltageValue = m_VoltageValue_RPN;
                                                        if (si.bIsVoltageVN) VoltageValue = m_VoltageValue_VN;
                                                        if (si.bIsVoltageSN) VoltageValue = m_VoltageValue_SN;
                                                        if (si.bIsVoltageNeitral) VoltageValue = m_VoltageValue_Neitral;

                                                        if ((VoltageValue >= 110 && VoltageValue <= 220) && duration.TotalDays / 365 >= 4)
                                                        {
                                                            GridVertical.FocusedRow = si.m_listParams[k].m_Row;
                                                            GridVertical.MakeRowVisible(si.m_listParams[k].m_Row);
                                                            if (MyLocalizer.XtraMessageBoxShow("\"" + m_list[i].m_strFunctionalSystemName + "/" + m_list[i].m_listSystemItems[j].m_strSystemItemName + "\"" + (char)10 + "данные обследования высоковольтных вводов не должны быть старше 4 лет. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                                                            {
                                                                GridVertical.ShowEditor();
                                                                return false;
                                                            }
                                                        }
                                                        if (VoltageValue > 220 && duration.TotalDays / 365 >= 2)
                                                        {
                                                            GridVertical.FocusedRow = si.m_listParams[k].m_Row;
                                                            GridVertical.MakeRowVisible(si.m_listParams[k].m_Row);
                                                            if (MyLocalizer.XtraMessageBoxShow("\"" + m_list[i].m_strFunctionalSystemName + "/" + m_list[i].m_listSystemItems[j].m_strSystemItemName + "\"" + (char)10 + "данные обследования высоковольтных вводов не должны быть старше 2 лет. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                                                            {
                                                                GridVertical.ShowEditor();
                                                                return false;
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            if (si.m_listParams[k].m_strParameterName == "Votn")
                            {
                                pVotn = si.m_listParams[k];
                            }
                        }
                    }

                    // если значения в %, то они не должны превышать 100
                    if (m_type == Inspection.InspectionType.HARG)
                    {
                        if (pUnit.m_valSelect != null && pUnit.m_valSelect != (Inspection.RangeResult?)0)
                        {
                            for (int k = 0; k < listValues.Count; k++)
                            {
                                if (listValues[k].m_valNumber != null && listValues[k].m_valNumber > 100)
                                {
                                    GridVertical.FocusedRow = listValues[k].m_Row;
                                    GridVertical.MakeRowVisible(listValues[k].m_Row);
                                    MyLocalizer.XtraMessageBoxShow("\"" + m_list[i].m_strFunctionalSystemName + "/" + m_list[i].m_listSystemItems[j].m_strSystemItemName + "\"" + (char)10 + "значение параметра \"" + listValues[k].m_strParameterDescript + "\" не должно превышать 100%.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    GridVertical.ShowEditor();
                                    return false;
                                }
                            }
                        }
                    }

                    // если задан хотя бы один из главных показателей по газам, то даты должны присутствовать обязательно
                    if (bDataExists && pProbeDate != null && pProtocolDate != null && pProtocolNumber != null)
                    {
                        if (pProtocolNumber.m_valString == null || pProtocolNumber.m_valString == "")
                        {
                            GridVertical.FocusedRow = pProtocolNumber.m_Row;
                            GridVertical.MakeRowVisible(pProtocolNumber.m_Row);
                            MyLocalizer.XtraMessageBoxShow("\"" + m_list[i].m_strFunctionalSystemName + "/" + m_list[i].m_listSystemItems[j].m_strSystemItemName + "\"" + (char)10 + "необходимо указать № протокола.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            GridVertical.ShowEditor();
                            return false;
                        }
                        if (pProbeDate.m_valDate == null)
                        {
                            GridVertical.FocusedRow = pProbeDate.m_Row;
                            GridVertical.MakeRowVisible(pProbeDate.m_Row);
                            MyLocalizer.XtraMessageBoxShow("\"" + m_list[i].m_strFunctionalSystemName + "/" + m_list[i].m_listSystemItems[j].m_strSystemItemName + "\"" + (char)10 + "необходимо указать дату отбора пробы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            GridVertical.ShowEditor();
                            return false;
                        }
                        if (pProtocolDate.m_valDate == null)
                        {
                            GridVertical.FocusedRow = pProtocolDate.m_Row;
                            GridVertical.MakeRowVisible(pProtocolDate.m_Row);
                            MyLocalizer.XtraMessageBoxShow("\"" + m_list[i].m_strFunctionalSystemName + "/" + m_list[i].m_listSystemItems[j].m_strSystemItemName + "\"" + (char)10 + "необходимо указать дату протокола.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            GridVertical.ShowEditor();
                            return false;
                        }
                    }

                    // если заданы даты протокола и даты отбора, то дата отбора не должна превышать дату протокола
                    if (pProbeDate != null && pProtocolDate != null && pProbeDate.m_valDate != null && pProtocolDate.m_valDate != null)
                    {
                        if (((DateTime)pProtocolDate.m_valDate).Date < ((DateTime)pProbeDate.m_valDate).Date)
                        {
                            GridVertical.FocusedRow = pProbeDate.m_Row;
                            GridVertical.MakeRowVisible(pProbeDate.m_Row);
                            MyLocalizer.XtraMessageBoxShow("\"" + m_list[i].m_strFunctionalSystemName + "/" + m_list[i].m_listSystemItems[j].m_strSystemItemName + "\"" + (char)10 + "дата отбора пробы не должны превышать дату протокола.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            GridVertical.ShowEditor();
                            return false;
                        }
                    }

                    if (m_type == Inspection.InspectionType.HARG)
                    {
                        if (bDataExists && pVotn != null && pVotn.m_valNumber == null && bShowVotnMsg)
                        {
                            GridVertical.FocusedRow = pVotn.m_Row;
                            GridVertical.MakeRowVisible(pVotn.m_Row);

                            if (MyLocalizer.XtraMessageBoxShow("\"" + m_list[i].m_strFunctionalSystemName + "/" + m_list[i].m_listSystemItems[j].m_strSystemItemName + "\"" + (char)10 + "для расчета \"Vотн, %/мес\" необходимо ввести данные предыдущего ХАРГ", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, "Ввести данные", "Отмена") == DialogResult.Yes)
                            {
                                // вызываем окно добавления ХАРГ, где отключаем возможность перехода по другим обследованиям
                                // если был выход из окна Отмена, то делаем return
                                // если положительный - пересчитываем Vотн
                                //this.ShowInTaskbar = false;

                                InspectionDataForm form = new InspectionDataForm(m_EquipmentID, Inspection.InspectionType.HARG, 0, -1);
                                form.m_bShowContinueMsg = false;
                                form.m_bShowVotnMsg = false;
                                DialogResult dr = form.ShowDialog(this);

                                //this.ShowInTaskbar = true;

                                if (dr == System.Windows.Forms.DialogResult.OK)
                                {
                                    dtInspectionDate_EditValueChanged(dtInspectionDate, null);
                                }

                                return false;
                            };

                            bShowVotnMsg = false;
                        }
                    }
                }
            }

            return true;
        }

        private bool SaveData()
        {
            DateTime dateInspection = dtInspectionDate.DateTime;
            dateInspection = dateInspection.Date;
            DateTime dateCreate = dtCreateDate.DateTime;

            string strExecutor = teExecutor.Text.Trim();
            string strDelegator = teDelegator.Text.Trim();
            string strAlgorithmIDs = "";
            for (int i = 0; i < m_list_algorithms.Count; i++)
            {
                if (cbAlgorithm.Properties.Items[i].CheckState == CheckState.Checked)
                {
                    if (strAlgorithmIDs == "")
                        strAlgorithmIDs = m_list_algorithms[i].m_ID.ToString();
                    else
                        strAlgorithmIDs = strAlgorithmIDs + "," + m_list_algorithms[i].m_ID.ToString();
                }
            }

            /*if (strExecutor == "")
            {
                teExecutor.Focus();
                MyLocalizer.XtraMessageBoxShow("Необходимо указать исполнителя обследования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }*/

            if (dateInspection.Date > DateTime.Now.Date)
            {
                dtInspectionDate.Focus();
                MyLocalizer.XtraMessageBoxShow("Дата обследования не должна превышать текущую дату.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dateCreate.Date > DateTime.Now.Date)
            {
                dtCreateDate.Focus();
                MyLocalizer.XtraMessageBoxShow("Дата формирования отчета не должна превышать текущую дату.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (m_CheckDateBegin != null && m_CheckDateEnd != null)
            {
                if (dateInspection.Date < ((DateTime)m_CheckDateBegin).Date ||
                    dateInspection.Date > ((DateTime)m_CheckDateEnd).Date)
                {
                    dtInspectionDate.Focus();
                    MyLocalizer.XtraMessageBoxShow("Дата обследования не должна выходить за срок проверки (с " + ((DateTime)m_CheckDateBegin).Date.ToShortDateString()
                        + " по " + ((DateTime)m_CheckDateEnd).Date.ToShortDateString() + ").", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            bool bExistData = false;
            for (int i = 0; i < m_list.Count; i++)
            {
                long FunctionalSystemID = m_list[i].m_FunctionalSystemID;
                for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                {
                    SystemItem si = m_list[i].m_listSystemItems[j];
                    long SystemItemID = si.m_SystemItemID;
                    for (int k = 0; k < si.m_listParams.Count; k++)
                    {
                        Parameter p = si.m_listParams[k];
                        if (p.m_InspectionSubType != null &&
                            p.m_InspectionSubType != m_sub_type_id)
                        {
                            continue;
                        }

                        Inspection.RangeResult? ValSelect = p.m_valSelect;
                        double? ValNumber = p.m_valNumber;
                        string ValString = p.m_valString;
                        DateTime? ValDate = p.m_valDate;

                        if ((ValSelect != null || ValNumber != null || ValString != null || ValDate != null) && p.m_defVal == null) bExistData = true;
                        else
                        {
                            if (si.m_listParams[k].m_bNotEmpty)
                            {
                                GridVertical.FocusedRow = si.m_listParams[k].m_Row;
                                GridVertical.MakeRowVisible(si.m_listParams[k].m_Row);
                                MyLocalizer.XtraMessageBoxShow("Необходимо заполнить значение показателя \"" + si.m_listParams[k].m_strParameterDescript + "\"\nв подсистеме \"" + si.m_strSystemItemName + "\".", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }                            
                        }
                    }
                }
            }

            if (!bExistData)
            {
                MyLocalizer.XtraMessageBoxShow("Необходимо указать значение хотя бы одного показателя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // -----------------------------------
            // для ХАРГ проверяем даты протоколов и Vотн
            // -----------------------------------

            if (m_type == Inspection.InspectionType.HARG || m_type == Inspection.InspectionType.FHA)
            {
                if (!VerifyDataForHARG_FHA())
                    return false;                
            }
            // ------------------------------------------------

            long InspectionID = 0;
            SQLiteTransaction tran = null;
            try
            {
                m_con.Open();

                SQLiteCommand com = new SQLiteCommand(m_con);
                com.CommandType = CommandType.Text;

                com.CommandText = "Select * FROM Inspections WHERE InspectionID <> @id AND InspectionDate = @date AND EquipmentID = @eq_id AND InspectionType = @type";

                AddParam(com, "@id", DbType.Int64, m_InspectionID);
                AddParam(com, "@date", DbType.DateTime, dateInspection);
                AddParam(com, "@eq_id", DbType.Int64, m_EquipmentID);
                AddParam(com, "@type", DbType.Int64, (long)m_type);

                SQLiteDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dtInspectionDate.Focus();
                    MyLocalizer.XtraMessageBoxShow("Для указанного оборудования уже существует обследование данного вида в выбранную дату.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dr.Close();
                    m_con.Close();
                    return false;
                }
                dr.Close();

                tran = m_con.BeginTransaction();

                com.Parameters.Clear();

                AddParam(com, "@date", DbType.DateTime, dateInspection);
                AddParam(com, "@eq_id", DbType.Int64, m_EquipmentID);
                AddParam(com, "@type", DbType.Int64, (long)m_type);
                if (m_sub_type_id == 0)
                    AddParam(com, "@sub_type", DbType.Int64, null);
                else
                    AddParam(com, "@sub_type", DbType.Int64, m_sub_type_id);
                AddParam(com, "@date_create", DbType.DateTime, dateInspection);
                AddParam(com, "@executor", DbType.String, strExecutor);
                AddParam(com, "@delegator", DbType.String, strDelegator);
                AddParam(com, "@algorithmids", DbType.String, strAlgorithmIDs);
                if (m_CheckID <= 0)
                    AddParam(com, "@check_id", DbType.Int64, null);
                else
                    AddParam(com, "@check_id", DbType.Int64, m_CheckID);

                if (m_InspectionID <= 0)
                {
                    com.CommandText = "INSERT INTO Inspections (EquipmentID, InspectionType, InspectionSubType, InspectionDate, CreateDate, Executor, Delegator, AlgorithmIDs, CheckID) " +
                        "VALUES (@eq_id, @type, @sub_type, @date, @date_create, @executor, @delegator, @algorithmids, @check_id); SELECT InspectionID FROM Inspections WHERE InspectionID = last_insert_rowid();";

                    SQLiteDataReader dr2 = com.ExecuteReader();

                    if (dr2.HasRows)
                    {
                        InspectionID = Convert.ToInt64(dr2["InspectionID"]);
                    }
                    dr2.Close();
                }
                else
                {
                    InspectionID = m_InspectionID;

                    com.CommandText = "UPDATE Inspections SET InspectionSubType = @sub_type, InspectionDate = @date, CreateDate = @date_create, Executor = @executor, Delegator = @delegator, " +
                        "AlgorithmIDs = @algorithmids, CheckID = @check_id " +
                        "WHERE InspectionID = @insp_id";

                    AddParam(com, "@insp_id", DbType.Int64, InspectionID);

                    com.ExecuteNonQuery();

                    /*SQLiteDataReader dr2 = com.ExecuteReader();

                    if (dr2.HasRows)
                    {
                        InspectionID = Convert.ToInt64(dr2.GetValue(0));
                    }
                    dr2.Close();*/

                    com.CommandText = "DELETE FROM InspectionDatas WHERE InspectionID = @insp_id";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM FunctionalSystemResults WHERE InspectionID = @insp_id";
                    com.ExecuteNonQuery();

                    com.CommandText = "DELETE FROM SystemItemResults WHERE InspectionID = @insp_id";
                    com.ExecuteNonQuery();
                }

                for (int i = 0; i < m_list.Count; i++)
                {
                    long FunctionalSystemID = m_list[i].m_FunctionalSystemID;
                    for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                    {
                        SystemItem si = m_list[i].m_listSystemItems[j];
                        long SystemItemID = si.m_SystemItemID;
                        for (int k = 0; k < si.m_listParams.Count; k++)
                        {
                            Parameter p = si.m_listParams[k];
                            if (p.m_InspectionSubType != m_sub_type_id &&
                                p.m_InspectionSubType != null)
                            {
                                continue;
                            }

                            long ParamID = p.m_ParameterID;
                            Inspection.RangeResult? ValSelect = p.m_valSelect;
                            double? ValNumber = p.m_valNumber;
                            string ValString = p.m_valString;
                            DateTime? ValDate = p.m_valDate;
                            long Result = (long)p.m_valRange;

                            if (ValSelect != null || ValNumber != null || ValString != null || ValDate != null)
                            {
                                com.CommandText = "INSERT INTO InspectionDatas (InspectionID, SystemItemID, ParameterID, ValNumber, ValSelect, ValString, ValDate, Result) " +
                                    "VALUES (@insp_id, @syst_item_id, @param_id, @val_number, @val_select, @val_string, @val_date, @result)";

                                com.Parameters.Clear();

                                AddParam(com, "@insp_id", DbType.Int64, InspectionID);
                                AddParam(com, "@syst_item_id", DbType.Int64, SystemItemID);
                                AddParam(com, "@param_id", DbType.Int64, ParamID);
                                AddParam(com, "@val_number", DbType.Decimal, ValNumber);
                                AddParam(com, "@val_select", DbType.Int64, (long?)ValSelect);
                                AddParam(com, "@val_string", DbType.String, ValString);
                                AddParam(com, "@val_date", DbType.DateTime, ValDate);
                                AddParam(com, "@result", DbType.Int64, Result);

                                com.ExecuteNonQuery();
                            }
                            /*else
                            {
                                if (si.m_listParams[k].m_bNotEmpty)
                                {
                                    if (tran != null) tran.Rollback();
                                    m_con.Close();
                                    GridVertical.FocusedRow = si.m_listParams[k].m_Row;
                                    GridVertical.MakeRowVisible(si.m_listParams[k].m_Row);
                                    MyLocalizer.XtraMessageBoxShow("Необходимо заполнить значение показателя \"" + si.m_listParams[k].m_strParameterDescript + "\"\nв подсистеме \"" + si.m_strSystemItemName + "\".", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }*/
                        }

                        if (si.m_dictValRange[m_sub_type_id].m_RangeResult >= 0)
                        {
                            com.CommandText = "INSERT INTO SystemItemResults (InspectionID, SystemItemID, Result) " +
                                        "VALUES (@insp_id, @syst_item_id, @result)";

                            com.Parameters.Clear();

                            AddParam(com, "@insp_id", DbType.Int64, InspectionID);
                            AddParam(com, "@syst_item_id", DbType.Int64, SystemItemID);
                            AddParam(com, "@result", DbType.Int64, si.m_dictValRange[m_sub_type_id].m_RangeResult);
                            //AddParam(com, "@defect_name", DbType.String, si.m_dictValRange[m_sub_type_id].m_strResult);
                            com.ExecuteNonQuery();
                        }

                        // заносим дефекты
                        foreach (KeyValuePair<long, Inspection.RangeResult> defect in si.m_dictValRange[m_sub_type_id].m_dictDefectIDs)
                        {
                            com.CommandText = "INSERT INTO InspectionDatas (InspectionID, SystemItemID, DefectID, Result) " +
                                    "VALUES (@insp_id, @syst_item_id, @defect_id, @result)";

                            com.Parameters.Clear();

                            long DefectID = defect.Key;
                            Inspection.RangeResult Result = defect.Value;

                            AddParam(com, "@insp_id", DbType.Int64, InspectionID);
                            AddParam(com, "@syst_item_id", DbType.Int64, SystemItemID);
                            AddParam(com, "@defect_id", DbType.Int64, DefectID);
                            AddParam(com, "@result", DbType.Int64, Result);

                            com.ExecuteNonQuery();
                        }
                    }

                    if (m_list[i].m_dictValRange[m_sub_type_id] >= 0)
                    {
                        com.CommandText = "INSERT INTO FunctionalSystemResults (InspectionID, FunctionalSystemID, Result) " +
                                        "VALUES (@insp_id, @funct_id, @result)";

                        com.Parameters.Clear();

                        AddParam(com, "@insp_id", DbType.Int64, InspectionID);
                        AddParam(com, "@funct_id", DbType.Int64, FunctionalSystemID);
                        AddParam(com, "@result", DbType.Int64, m_list[i].m_dictValRange[m_sub_type_id]);

                        com.ExecuteNonQuery();
                    }
                }

                if (tran != null) tran.Commit();
                tran = null;
                m_con.Close();

            }
            catch (SQLiteException ex)
            {
                if (tran != null) tran.Rollback();
                m_con.Close();
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (DBConcurrencyException ex)
            {
                if (tran != null) tran.Rollback();
                m_con.Close();
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                if (tran != null) tran.Rollback();
                m_con.Close();
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            m_InspectionID = InspectionID;
            m_bChangeData = false;
            return true;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if (!SaveData()) return;

            if (m_bShowContinueMsg)
            {
                if (m_type != Inspection.InspectionType.Vibro)
                {
                    if (MyLocalizer.XtraMessageBoxShow("Перейти к заполнению данных по следующему обследованию?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        m_bContinueNext = true;
                    }
                    else return;
                }
                else
                {
                    if (MyLocalizer.XtraMessageBoxShow("Завершить ввод данных?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        m_bContinueNext = true;
                    }
                    else return;
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        void GridVertical_Resize(object sender, System.EventArgs e)
        {
            /*GridVertical.BeginUpdate();

            int width = GridVertical.RecordWidth + GridVertical.RowHeaderWidth;
            int new_width = GridVertical.Width - 40;

            GridVertical.RecordWidth = (int)(GridVertical.RecordWidth * new_width / width);
            GridVertical.RowHeaderWidth = (int)(GridVertical.RowHeaderWidth * new_width / width);

            GridVertical.EndUpdate();*/
        }

        private void GridVertical_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (GridVertical.FocusedRecordCellIndex > 0)
            {
                GridVertical.FocusedRecordCellIndex = 0;
                e.Cancel = true;
                return;
            }

            string row_name = GridVertical.FocusedRow.Properties.FieldName;
            if (row_name == "#")
            {
                GridVertical.FocusedRecordCellIndex = 0;
                e.Cancel = true;
                return;
            }

            long iSystemItemID = -1;
            long iParameterID = -1;

            int pos = row_name.IndexOf("_");
            iSystemItemID = Convert.ToInt64(row_name.Substring(0, pos));
            iParameterID = Convert.ToInt64(row_name.Substring(pos + 1));

            int SystemItemPos = dictSystemItemPos[iSystemItemID];
            int FunctionalSystemPos = dictFunctionalSystemPos[dictSystemItemsFunctionalSystems[iSystemItemID]];

            SystemItem si = m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos];
            for (int i = 0; i < si.m_listParams.Count; i++)
            {
                Parameter p = si.m_listParams[i];
                if (p.m_ParameterID == iParameterID)
                {
                    if (p.m_bReadOnly || m_list[FunctionalSystemPos].m_dictUnionMainID.Count > 0 && m_list[FunctionalSystemPos].m_dictUnionMainID.ContainsKey(m_sub_type_id))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void GridVertical_CustomDrawRowHeaderCell(object sender, DevExpress.XtraVerticalGrid.Events.CustomDrawRowHeaderCellEventArgs e)
        {
            /*if (e.CellIndex > 0 && e.Row.Properties.FieldName != "#")
            {
                e.Appearance.ForeColor = Color.Gray;
            }*/
        }

        private void UpdateResults()
        {
            GridVertical.BeginUpdate();

            for (int i = 0; i < m_list.Count; i++)
            {
                for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                {
                    SystemItem si = m_list[i].m_listSystemItems[j];
                    for (int k = 0; k < si.m_listParams.Count; k++)
                    {
                        Parameter p = si.m_listParams[k];
                        if (p.m_InspectionSubType != null &&
                            p.m_InspectionSubType != m_sub_type_id)
                        {
                            continue;
                        }

                        CalcParameter(i, j, k);
                        CalcRangeSystemItem(i, j);
                        CalcRangeFunctionalSystem(i);
                    }
                }
            }

            GridVertical.EndUpdate();
        }

        private void cbSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;

            if (m_bDataLoad) return;

            m_sub_type_id = m_list_sub_types[cbSubType.SelectedIndex].m_ID;
            CreateGrid();
            //dtInspectionDate_EditValueChanged(dtInspectionDate, null);
            UpdateResults();
        }

        private void GridVertical_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            DevExpress.XtraVerticalGrid.Rows.BaseRow row = GridVertical.FocusedRow;

            string row_name = row.Properties.FieldName;
            if (row_name == "#") return;

            long iSystemItemID = -1;
            long iParameterID = -1;

            int pos = row_name.IndexOf("_");
            iSystemItemID = Convert.ToInt64(row_name.Substring(0, pos));
            iParameterID = Convert.ToInt64(row_name.Substring(pos + 1));

            int ParamIndex = -1;

            int SystemItemPos = dictSystemItemPos[iSystemItemID];
            int FunctionalSystemPos = dictFunctionalSystemPos[dictSystemItemsFunctionalSystems[iSystemItemID]];

            SystemItem si = m_list[FunctionalSystemPos].m_listSystemItems[SystemItemPos];

            for (int i = 0; i < si.m_listParams.Count; i++)
            {
                Parameter p = si.m_listParams[i];
                if (p.m_ParameterID == iParameterID)
                {
                    //ParamType = p.m_ParameterType;
                    ParamIndex = i;
                    break;
                }
            }

            if (ParamIndex < 0) return;

            if (si.m_listParams[ParamIndex].m_ParameterType == Inspection.ParameterType.Number ||
                si.m_listParams[ParamIndex].m_ParameterType == Inspection.ParameterType.Integer)
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    if (si.m_listParams[ParamIndex].m_maxVal != null && Convert.ToDouble(e.Value) > si.m_listParams[ParamIndex].m_maxVal ||
                        si.m_listParams[ParamIndex].m_minVal != null && Convert.ToDouble(e.Value) < si.m_listParams[ParamIndex].m_minVal)
                    {
                        if (row.ParentRow.Visible) // для скрытых строк не надо показывать сообщение (при объединении узлов)
                        {
                            e.Valid = false;
                            if (si.m_listParams[ParamIndex].m_maxVal != null && si.m_listParams[ParamIndex].m_minVal != null)
                                e.ErrorText = "Значение параметра выходит за пределы допустимых значений (от " + ((double)si.m_listParams[ParamIndex].m_minVal).ToString("0") + " до " + ((double)si.m_listParams[ParamIndex].m_maxVal).ToString("0") + ").";
                            if (si.m_listParams[ParamIndex].m_maxVal != null && si.m_listParams[ParamIndex].m_minVal == null)
                                e.ErrorText = "Значение параметра выходит за пределы допустимых значений (до " + ((double)si.m_listParams[ParamIndex].m_maxVal).ToString("0") + ").";
                            if (si.m_listParams[ParamIndex].m_maxVal == null && si.m_listParams[ParamIndex].m_minVal != null)
                                e.ErrorText = "Значение параметра выходит за пределы допустимых значений (от " + ((double)si.m_listParams[ParamIndex].m_minVal).ToString("0") + ").";
                            return;
                        }
                    }

                    if (si.m_listParams[ParamIndex].m_notEqualVal != null && Math.Abs((double)si.m_listParams[ParamIndex].m_notEqualVal - Convert.ToDouble(e.Value)) <= 0.0000009
                        || (double.IsNaN(Convert.ToDouble(e.Value)) || double.IsInfinity(Convert.ToDouble(e.Value))))
                    {
                        if (row.ParentRow.Visible) // для скрытых строк не надо показывать сообщение (при объединении узлов)
                        {
                            e.Valid = false;
                            e.ErrorText = "Недопустимое значение параметра.";
                            return;
                        }
                    }
                }
            }
        }

        private void GetLastValuesForHARG()
        {
            if (m_type == Inspection.InspectionType.HARG)
            {
                // SELECT ParameterID, SystemItemID, ValNumber FROM InspectionDatas INNER JOIN Inspections ON
                // Inspections.InspectionID = InspectionDatas.InspectionID WHERE InspectionType = ? AND InspectionDate < ? ORDER BY InspectionDate DESC LIMIT 1
                try
                {
                    dictLastValuesForHARG.Clear();

                    m_con.Open();

                    SQLiteCommand com = new SQLiteCommand(m_con);
                    com.CommandType = CommandType.Text;

                    com.CommandText = "SELECT InspectionDatas.*, " +
                        " Inspections.InspectionDate, Inspections.InspectionSubType, Parameters.ParameterName, Parameters.ParameterType, " +
                        " Parameters.ParameterSelect1, Parameters.ParameterSelect2, Parameters.ParameterSelect3, Parameters.ParameterSelect4 FROM InspectionDatas " + 
                        " INNER JOIN Inspections ON Inspections.InspectionID = InspectionDatas.InspectionID " +
                        " INNER JOIN Parameters ON Parameters.ParameterID = InspectionDatas.ParameterID " +
                        " WHERE Inspections.InspectionID = (SELECT InspectionID FROM Inspections WHERE " +
                        " EquipmentID = @eq_id AND InspectionType = @type AND InspectionDate < @date AND InspectionID <> @insp_id ORDER BY InspectionDate DESC LIMIT 1)";

                    DateTime dateInspection = dtInspectionDate.DateTime;
                    dateInspection = dateInspection.Date;

                    AddParam(com, "@eq_id", DbType.Int64, m_EquipmentID);
                    AddParam(com, "@type", DbType.Int64, (long)m_type);
                    AddParam(com, "@date", DbType.DateTime, dateInspection);
                    AddParam(com, "@insp_id", DbType.Int64, m_InspectionID);

                    SQLiteDataReader drLastData = com.ExecuteReader();
                    if (drLastData.HasRows)
                    {
                        while (drLastData.Read())
                        {
                            long ParameterID = Convert.ToInt64(drLastData["ParameterID"]);
                            long SystemItemID = Convert.ToInt64(drLastData["SystemItemID"]);
                            double? ValNumber = null;
                            if (drLastData["ValNumber"] != DBNull.Value)
                                ValNumber = Convert.ToDouble(drLastData["ValNumber"]);
                            
                            Inspection.RangeResult? ValSelect = null;
                            if (drLastData["ValSelect"] != DBNull.Value)
                                ValSelect = (Inspection.RangeResult)Convert.ToInt64(drLastData["ValSelect"]);

                            string ValString = null;
                            if (drLastData["ValString"] != DBNull.Value)
                                ValString = Convert.ToString(drLastData["ValString"]);

                            DateTime? ValDate = null;
                            if (drLastData["ValDate"] != DBNull.Value)
                                ValDate = Convert.ToDateTime(drLastData["ValDate"]);
                            
                            DateTime date = Convert.ToDateTime(drLastData["InspectionDate"]);
                            long? sub_type_id = null;
                            if (drLastData["InspectionSubType"] != DBNull.Value)
                                sub_type_id = Convert.ToInt64(drLastData["InspectionSubType"]);
                            string strParameterName = drLastData["ParameterName"].ToString();

                            long iParameterType = Convert.ToInt64(drLastData["ParameterType"]);
                            if (iParameterType == (long)Inspection.ParameterType.Select)
                            {
                                if (ValSelect == Inspection.RangeResult.Green) ValString = drLastData["ParameterSelect1"].ToString();
                                if (ValSelect == Inspection.RangeResult.Yellow) ValString = drLastData["ParameterSelect2"].ToString();
                                if (ValSelect == Inspection.RangeResult.Orange) ValString = drLastData["ParameterSelect3"].ToString();
                                if (ValSelect == Inspection.RangeResult.Red) ValString = drLastData["ParameterSelect4"].ToString();
                            }

                            if (!dictLastValuesForHARG.ContainsKey(SystemItemID)) dictLastValuesForHARG[SystemItemID] = new List<ParameterLite>();
                            dictLastValuesForHARG[SystemItemID].Add(new ParameterLite(ParameterID, strParameterName, ValNumber, ValSelect, ValString, ValDate, date, sub_type_id));
                        }
                    }
                    drLastData.Close();

                    m_con.Close();

                    SetGridLastValuesForHARG();
                }
                catch (SQLiteException ex)
                {
                    m_con.Close();
                    MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (DBConcurrencyException ex)
                {
                    m_con.Close();
                    MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    m_con.Close();
                    MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void dtInspectionDate_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;

            if (m_bDataLoad) return;

            if (m_type == Inspection.InspectionType.HARG)
            {
                // SELECT ParameterID, SystemItemID, ValNumber FROM InspectionDatas INNER JOIN Inspections ON
                // Inspections.InspectionID = InspectionDatas.InspectionID WHERE InspectionType = ? AND InspectionDate < ? ORDER BY InspectionDate DESC LIMIT 1
                try
                {
                    GetLastValuesForHARG();

                    // ищем параметр V отн
                    for (int i = 0; i < m_list.Count; i++)
                    {
                        for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                        {
                            SystemItem si = m_list[i].m_listSystemItems[j];
                            for (int k = 0; k < si.m_listParams.Count; k++)
                            {
                                Parameter p = si.m_listParams[k];
                                if (p.m_InspectionSubType != null &&
                                    p.m_InspectionSubType != m_sub_type_id)
                                {
                                    continue;
                                }

                                if (p.m_CalcAlgorithm != null && p.m_CalcAlgorithm.m_AlgorithmName == "votn" && p.m_CalcAlgorithm.m_algType == Inspection.CalcType.Calc)
                                {
                                    si.CalcFieldValue(this, k);
                                    //CalcParameter(i, j, k);
                                    CalcRangeSystemItem(i, j);
                                    CalcRangeFunctionalSystem(i);
                                    
                                    break;
                                }
                            }
                        }                        
                    }
                }
                catch (SQLiteException ex)
                {
                    m_con.Close();
                    MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (DBConcurrencyException ex)
                {
                    m_con.Close();
                    MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    m_con.Close();
                    MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void GridVertical_CustomDrawRowValueCell(object sender, DevExpress.XtraVerticalGrid.Events.CustomDrawRowValueCellEventArgs e)
        {
            string str = e.CellText;
            Rectangle rect = e.Bounds;
            rect.X += 3;
            rect.Width -= 6;

            if (e.Row.Appearance.Options.UseBackColor && !e.Row.HasChildren || e.CellIndex > 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.White) /*.FromArgb(180, 180, 180))*/, e.Bounds);
            }

            if (str == "")
            {
                if (e.CellIndex > 0)
                    e.Graphics.DrawString("данные отсутствуют", e.Appearance.Font, m_BrushGray, rect, e.Appearance.GetStringFormat());
                else
                    e.Graphics.DrawString(e.Row.Tag.ToString(), e.Appearance.Font, m_BrushGray, rect, e.Appearance.GetStringFormat());
                /*
                if (e.Row.Properties.Caption == "Примечание")
                    e.Graphics.DrawString("заполнить при необходимости", e.Appearance.Font, new SolidBrush(Color.Gray), rect, e.Appearance.GetStringFormat());
                else
                {
                    if (e.Row.Properties.Caption == "Vотн, %/мес")
                        e.Graphics.DrawString("данные для расчета отсутствуют", e.Appearance.Font, new SolidBrush(Color.Gray), rect, e.Appearance.GetStringFormat());
                    else
                        e.Graphics.DrawString("данные отсутствуют", e.Appearance.Font, new SolidBrush(Color.Gray), rect, e.Appearance.GetStringFormat());
                }*/
            }
            else
            {
                if (e.CellIndex > 0)
                {
                    if (e.Row.Properties.FieldName != "#")
                    {
                        e.Graphics.DrawString(str, e.Appearance.Font, m_BrushGray, rect, e.Appearance.GetStringFormat());
                    }
                    else
                    {
                        //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        e.Graphics.DrawString(str, e.Appearance.Font, m_BrushBlack, rect, e.Appearance.GetStringFormat());
                    }
                }
                else
                {
                    if (e.Row.Properties.FieldName != "#")
                    {
                        if (e.Appearance.ForeColor != Color.Red)
                            e.Graphics.DrawString(str, e.Appearance.Font, m_BrushBlack, rect, e.Appearance.GetStringFormat());
                        else
                            e.Graphics.DrawString(str, e.Appearance.Font, m_BrushRed, rect, e.Appearance.GetStringFormat());
                    }
                    else
                    {
                        //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        e.Graphics.DrawString(str, e.Appearance.Font, m_BrushBlack, rect, e.Appearance.GetStringFormat());
                    }
                }
            }

            e.Handled = true;
        }

        private void GridVertical_SizeChanged(object sender, EventArgs e)
        {
            if (!m_bExistTypeSelect) return;

            GridVertical.BeginUpdate();
            for (int i = 0; i < GridVertical.Rows.Count; i++)
            {
                for (int j = 0; j < GridVertical.Rows[i].ChildRows.Count; j++)
                {
                    for (int k = 0; k < GridVertical.Rows[i].ChildRows[j].ChildRows.Count; k++)
                    {
                        RecalcRowHeight(GridVertical.Rows[i].ChildRows[j].ChildRows[k]);
                    }
                }
            }
            GridVertical.EndUpdate();
        }

        private void GridVertical_RowHeaderWidthChanged(object sender, EventArgs e)
        {
            if (!m_bExistTypeSelect) return;

            GridVertical.BeginUpdate();
            for (int i = 0; i < GridVertical.Rows.Count; i++)
            {
                for (int j = 0; j < GridVertical.Rows[i].ChildRows.Count; j++)
                {
                    for (int k = 0; k < GridVertical.Rows[i].ChildRows[j].ChildRows.Count; k++)
                    {
                        RecalcRowHeight(GridVertical.Rows[i].ChildRows[j].ChildRows[k]);
                    }
                }
            }
            GridVertical.EndUpdate();
        }

        private void cbAlgorithm_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;

            if (m_bDataLoad) return;

            UpdateResults();
        }

        private void cbAlgorithm_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (m_bDataLoad) return;

            if (!cbAlgorithm.Properties.ReadOnly)
            {
                if (cbAlgorithm.Properties.Items.GetCheckedValues().Count == 0)
                {
                    e.Cancel = true;
                }
            }
        }

        private void menuShowLimits_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void backgroundMessage_DoWork(object sender, DoWorkEventArgs e)
        {
            //MyLocalizer.XtraMessageBoxShow(m_strMessageError, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MyLocalizer.XtraMessageBoxShow(e.Argument.ToString(), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void cbShowDefect_CheckedChanged(object sender, EventArgs e)
        {
            GridVertical.BeginUpdate();
            for (int i = 0; i < m_list.Count; i++)
            {
                for (int j = 0; j < m_list[i].m_listSystemItems.Count; j++)
                {
                    SystemItem si = m_list[i].m_listSystemItems[j];
                    if (cbShowDefect.Checked)
                    {
                        si.m_Row.Properties.Caption = si.m_strSystemItemName + si.m_dictValRange[m_sub_type_id].GetDefectName(m_dictParamName, m_dictDefectName);
                    }
                    else
                        si.m_Row.Properties.Caption = si.m_strSystemItemName;
                }
            }
            GridVertical.EndUpdate();
        }

        private void teEquipmentName_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;
        }

        private void teEquipmentNumber_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;
        }

        private void dtCreateDate_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;
        }

        private void teExecutor_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;
        }

        private void teDelegator_EditValueChanged(object sender, EventArgs e)
        {
            if (m_bDataLoadEnd) m_bChangeData = true;
        }

        private void InspectionDataForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void bNext_Click(object sender, EventArgs e)
        {
            if (m_bShowContinueMsg)
            {
                /*NextPrevForm form = new NextPrevForm();
                if (m_type == Inspection.InspectionType.Visual)
                {
                    form.m_strPrev = "Паспортные данные";
                    form.m_strNext = "Данные ФХА трансформаторного масла";
                }
                if (m_type == Inspection.InspectionType.FHA)
                {
                    form.m_strPrev = "Данные визуального обследования";
                    form.m_strNext = "Данные ХАРГ трансформаторного масла";
                }
                if (m_type == Inspection.InspectionType.HARG)
                {
                    form.m_strPrev = "Данные ФХА трансформаторного масла";
                    form.m_strNext = "Данные тепловизионного контроля";
                }
                if (m_type == Inspection.InspectionType.Warm)
                {
                    form.m_strPrev = "Данные ХАРГ трансформаторного масла";
                    form.m_strNext = "Данные вибрационного обследования";
                }
                if (m_type == Inspection.InspectionType.Vibro)
                {
                    form.m_strPrev = "Данные тепловизионного контроля";
                    form.m_strNext = "";
                }
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (form.m_bNext) m_bContinueNext = true;
                    else m_bContinuePrev = true;
                }*/

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

                m_bContinueNext = true;                

                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }

            /*m_bChangeData = false;

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();*/
        }

        private void bPrev_Click(object sender, EventArgs e)
        {
            if (m_bShowContinueMsg)
            {
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

                m_bContinuePrev = true;

                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void InspectionDataForm_ResizeEnd(object sender, EventArgs e)
        {
        }

        private void InspectionDataForm_SizeChanged(object sender, EventArgs e)
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

        private void InspectionDataForm_Resize(object sender, EventArgs e)
        {
        }

        private void InspectionDataForm_Shown(object sender, EventArgs e)
        {
        }
    }
}