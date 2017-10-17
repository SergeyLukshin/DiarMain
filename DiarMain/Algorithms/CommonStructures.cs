using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace DiarMain
{
    class DataSourceString
    {
        public DataSourceString(long key, string strVal)
        {
            m_key = key;
            m_strVal = strVal;
        }

        private long m_key;
        private string m_strVal;

        public string VAL
        {
            get { return m_strVal; }
            set { m_strVal = value; }
        }

        public long KEY
        {
            get { return m_key; }
            set { m_key = value; }
        }
    };

    class DataSourceInt
    {
        public DataSourceInt(long key, long val)
        {
            m_key = key;
            m_val = val;
        }

        private long m_key;
        private long m_val;

        public long VAL
        {
            get { return m_val; }
            set { m_val = value; }
        }

        public long KEY
        {
            get { return m_key; }
            set { m_key = value; }
        }
    };

    /*class DataSelect
    {
        public DataSelect(int val, string strVal)
        {
            m_val = val;
            m_strVal = strVal;
        }

        private long m_val;
        private string m_strVal;

        public string STR_VAL
        {
            get { return m_strVal; }
            set { m_strVal = value; }
        }

        public long INT_VAL
        {
            get { return m_val; }
            set { m_val = value; }
        }
    };*/

    public partial class Equipment
    {
        public enum EquipmentKind
        {
            None = -1,
            Transformer = 1,
            AirSwitch = 2,
            OilLessSwitch = 3,
            OilTankSwitch = 4
        };
    }

    class InspectionTypeInfo
    {
        public InspectionTypeInfo(Inspection.InspectionType inspType, string strRusName, string strEngName)
        {
            m_inspType = inspType;
            m_strRusName = strRusName;
            m_strEngName = strEngName;
        }
        Inspection.InspectionType m_inspType;
        string m_strRusName;
        string m_strEngName;
    };

    public partial class Inspection
    {
        public static Dictionary<Equipment.EquipmentKind, List<Inspection.InspectionType>> m_listEquipmentInspections = new Dictionary<Equipment.EquipmentKind, List<Inspection.InspectionType>>();
        public static Dictionary<Equipment.EquipmentKind, string> m_dictActualEquipmentKinds = new Dictionary<Equipment.EquipmentKind, string>();
        public static Dictionary<Inspection.InspectionType, KeyValuePair<string, string>> m_dictInspections = new Dictionary<Inspection.InspectionType, KeyValuePair<string, string>>();

        public enum ParameterType
        {
            Number = 0,
            Select = 1,
            String = 2,
            Date = 3,
            Integer = 4
        };

        public enum InspectionType
        {
            Visual = 0,
            HARG = 1,
            FHA = 2,
            Warm = 3,
            Vibro = 4,
            Parameter = 5,
            Electrical = 6
        };

        public enum CalcType
        {
            Calc = 0,
            Calc_if_all = 1,
            Calc_if_empty = 1
        };

        public enum ReportType
        {
            ProtocolTransformer,
            ReportTransformer,
            ReportAirSwitch,
            ReportOilLessSwitch,
            ReportOilTankSwitch
        }

        public enum RangeResult
        {
            None = -1,
            Green = 0,
            Yellow = 1,
            Orange = 2,
            Red = 3
        };

        public enum ValueCombinationType
        {
            OR = 0,
            AND = 1
        }

        /*public enum CalculatedFieldType
        {
            NONE = 0,
            CALC = 1
        }*/

        public class DefectInfo
        {
            public DefectInfo(long ID, Inspection.RangeResult Result)
            {
                m_ID = ID;
                m_Result = Result;
            }
            public long m_ID;
            public Inspection.RangeResult m_Result;
        }

        public class InspectionResult
        {
            /*public void AddResult(InspectionResult val)
            {
                if (val.m_listDefectIDs.Count > 0)
                {
                    if (m_listDefectIDs.IndexOf(val.m_listDefectIDs[0]) >= 0) return;
                    m_listDefectIDs.Add(val.m_listDefectIDs[0]);
                    if (m_strResult != "" && val.m_strResult != "") m_strResult = m_strResult + "; " + val.m_strResult;
                    else
                    {
                        if (val.m_strResult != "") m_strResult = val.m_strResult;
                    }
                }
                else
                {
                    if (m_strResult != "" && val.m_strResult != "") m_strResult = m_strResult + "; " + val.m_strResult;
                    else
                    {
                        if (val.m_strResult != "") m_strResult = val.m_strResult;
                    }
                }
            }*/

            public InspectionResult(RangeResult rangeResult)
            {
                m_RangeResult = rangeResult;                
            }

            public void Add(DefectInfo ParameterID, DefectInfo DefectID)
            {
                if (DefectID != null)
                {
                    if (m_dictDefectIDs.ContainsKey(DefectID.m_ID) && m_dictDefectIDs[DefectID.m_ID] >= DefectID.m_Result)
                        return;

                    m_dictDefectIDs[DefectID.m_ID] = DefectID.m_Result;
                }
                else
                {
                    if (ParameterID != null)
                    {
                        if (m_dictParameterIDs.ContainsKey(ParameterID.m_ID) && m_dictParameterIDs[ParameterID.m_ID] >= ParameterID.m_Result)
                            return;
                            
                        m_dictParameterIDs[ParameterID.m_ID] = ParameterID.m_Result;
                    }
                }
            }

            public void CalcMaxRange()
            {
                Inspection.RangeResult max = RangeResult.None;

                foreach (KeyValuePair<long, Inspection.RangeResult> param in m_dictParameterIDs)
                {
                    if (param.Value > max) max = param.Value;
                }

                foreach (KeyValuePair<long, Inspection.RangeResult> defect in m_dictDefectIDs)
                {
                    if (defect.Value > max) max = defect.Value;
                }

                m_RangeResult = max;
            }

            public string GetDefectName(Dictionary<long, string> dictParamInfo, Dictionary<long, string> dictDefectInfo)
            {
                string strCaption = "";
                Dictionary<string, int> dictDefects = new Dictionary<string,int>();

                foreach (KeyValuePair<long, Inspection.RangeResult> param in m_dictParameterIDs)
                {
                    if (param.Value <= Inspection.RangeResult.Green) continue;

                    if (dictParamInfo.ContainsKey(param.Key))
                    {
                        if (!dictDefects.ContainsKey(dictParamInfo[param.Key]))
                        {
                            if (strCaption == "")
                                strCaption += "\t- " + dictParamInfo[param.Key];
                            else
                                strCaption += "\n\t- " + dictParamInfo[param.Key];

                            dictDefects[dictParamInfo[param.Key]] = 1;
                        }
                    }
                }

                foreach (KeyValuePair<long, Inspection.RangeResult> defect in m_dictDefectIDs)
                {
                    if (defect.Value <= Inspection.RangeResult.Green) continue;

                    if (dictDefectInfo.ContainsKey(defect.Key))
                    {
                        if (!dictDefects.ContainsKey(dictDefectInfo[defect.Key]))
                        {
                            if (strCaption == "")
                                strCaption += "\t- " + dictDefectInfo[defect.Key].Replace("\n", "\n\t- ");
                            else
                                strCaption += "\n\t- " + dictDefectInfo[defect.Key].Replace("\n", "\n\t- ");

                            dictDefects[dictDefectInfo[defect.Key]] = 1;
                        }
                    }
                }

                if (strCaption != "") strCaption = "\n" + strCaption;

                return strCaption;
            }

            public RangeResult  m_RangeResult;
            //public string       m_strResult;
            //public long         m_AlgorithmID;
            public Dictionary<long, Inspection.RangeResult> m_dictDefectIDs = new Dictionary<long, Inspection.RangeResult>();
            public Dictionary<long, Inspection.RangeResult> m_dictParameterIDs = new Dictionary<long, Inspection.RangeResult>();
        }

        public enum AlgorithmType
        {
            AT_RD = 1,
            AT_Dornenburg = 2,
            AT_Rodgers = 3,
            AT_StandardMEK = 4,
            AT_MainGas = 5,
            AT_Normogramma = 6,
            AT_Duval = 7
        }
    };

    public class ParameterLite // исп для хранения предыдущих значений по ХАРГ
    {
        public ParameterLite(long ParameterID, string strParameterName, double? ValNumber, Inspection.RangeResult? ValSelect, string ValString, DateTime? ValDate, DateTime date, long? sub_type_id)
        {
            m_ParameterID = ParameterID;
            m_valNumber = ValNumber;
            m_valSelect = ValSelect;
            m_valString = ValString;
            m_valDate = ValDate;
            m_Date = date;
            m_sub_type_id = sub_type_id;
            m_strParameterName = strParameterName;
        }

        public long m_ParameterID;
        public double? m_valNumber;
        public Inspection.RangeResult? m_valSelect;
        public string m_valString;
        public DateTime? m_valDate;
        public DateTime m_Date;
        public long? m_sub_type_id;
        public string m_strParameterName;
    }

    public class ReportInfo
    {
        public class ParameterResult
        {
            public ParameterResult(long ParameterID, long? DefectID, string strParameterDescript, long? InspectionSubType, Inspection.ParameterType ParameterType,
                string strParameterSelect1, string strParameterSelect2, string strParameterSelect3, string strParameterSelect4, string strParameterAlgorithm, bool bEnable/*, string strDefectName*/)
            {
                m_ParameterID = ParameterID;
                m_DefectID = DefectID;
                m_strParameterDescript = strParameterDescript;
                m_InspectionSubType = InspectionSubType;
                m_ParameterType = ParameterType;
                m_strParameterSelect1 = strParameterSelect1;
                m_strParameterSelect2 = strParameterSelect2;
                m_strParameterSelect3 = strParameterSelect3;
                m_strParameterSelect4 = strParameterSelect4;

                m_bEnable = bEnable;

                m_arrParameterAlgorithmIDs = new List<long>();
                string[] arr = strParameterAlgorithm.Split('_');
                if (arr.Length >= 2)
                {
                    if (arr[0].IndexOf("calc") >= 0)
                    {
                        if (arr[0] == "calcifempty")
                        {
                            List<long> arrParameterIDs = new List<long>();

                            for (int i = 2; i < arr.Length; i++)
                            {
                                m_arrParameterAlgorithmIDs.Add(Convert.ToInt32(arr[i]));
                            }
                        }
                    }

                    if (arr[0] == "calc" && (arr[1] == "votn" || arr[1] == "votn_gas"))
                    {
                        m_bEnable = false; // отключаем, чтобы не считалась статистика
                    }
                }

                //m_strDefectName = strDefectName;
            }

            public long m_ParameterID;
            public long? m_DefectID;
            public string m_strParameterDescript;
            public long? m_InspectionSubType;
            public Inspection.ParameterType m_ParameterType;
            public string m_strParameterSelect1;
            public string m_strParameterSelect2;
            public string m_strParameterSelect3;
            public string m_strParameterSelect4;
            //public string m_strDefectName;


            public bool m_bEnable;

            public long? m_Result;
            public long? m_valSelect;
            public double? m_valNumber;

            public List<long> m_arrParameterAlgorithmIDs;
        }

        public class InspectionSubType_
        {
            public long m_ID;
            public string m_strName;

            public InspectionSubType_(long id, string strName)
            {
                m_ID = id;
                m_strName = strName;
            }
        }

        public class DefectResult
        {
            public DefectResult(long DefectID, string strDefectName, long? InspectionSubType)
            {
                m_DefectID = DefectID;
                m_strDefectName = strDefectName;
                m_InspectionSubType = InspectionSubType;
                m_strDefectDescript = "";
            }

            public long m_DefectID;
            public string m_strDefectName;
            public long? m_InspectionSubType;
            public string m_strDefectDescript; // поле нужно только в случае переноса праметров в дефекты

            public long? m_Result;
        }

        public class SystemItem
        {
            public SystemItem(long SystemItemID, string strAlg, /*long? InputVoltageIndex,*/ bool bEnable)
            {
                m_SystemItemID = SystemItemID;
                m_strAlg = strAlg;
                m_Result = null;
                m_bEnable = bEnable;
                m_strNote = "";
                //m_InputVoltageIndex = InputVoltageIndex;
                //m_strDefectName = "";
                //m_InspectionSubType = null;
            }

            /*public SystemItem(long SystemItemID, long? Result, bool bEnable)
            {
                m_SystemItemID = SystemItemID;
                m_Result = Result;
                m_bEnable = bEnable;
                //m_strDefectName = strDefectName;
                //m_InspectionSubType = InspectionSubType;
            }*/
            public long m_SystemItemID;
            //public long? m_InputVoltageIndex;
            public long? m_Result;
            public bool m_bEnable;
            //public long? m_Defec;
            //public long? m_InspectionSubType;

            public List<ParameterResult> m_listParameterResults = new List<ParameterResult>();
            public List<DefectResult> m_listDefectResults = new List<DefectResult>();
            public string m_strAlg;
            public string m_strNote;
        }

        public class FunctionalSystem
        {
            public FunctionalSystem(long EquipmentKindID, long FunctionalSystemID, string strFunctionalSystemName/*,
                long UseInVisual, long UseInHARG, long UseInFHA, long UseInWarm, long UseInVibro*/)
            {
                m_FunctionalSystemID = FunctionalSystemID;
                m_strFunctionalSystemName = strFunctionalSystemName;

                /*m_dictTypes[Inspection.InspectionType.Visual] = UseInVisual;
                m_dictTypes[Inspection.InspectionType.HARG] = UseInHARG;
                m_dictTypes[Inspection.InspectionType.FHA] = UseInFHA;
                m_dictTypes[Inspection.InspectionType.Warm] = UseInWarm;
                m_dictTypes[Inspection.InspectionType.Vibro] = UseInVibro;*/

                //foreach (Inspection.InspectionType type in Enum.GetValues(typeof(Inspection.InspectionType)))
                for (int i = 0; i < Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)EquipmentKindID].Count; i++)
                {
                    Inspection.InspectionType type = Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)EquipmentKindID][i];

                    m_dictSystemItems[type] = new Dictionary<string, SystemItem>();
                    m_dictUnionMainID[type] = new Dictionary<long, long>();
                    m_dictUnionExtraID[type] = new Dictionary<long, List<long>>();
                    m_dictSubTypes[type] = 0;
                    m_dictResults[type] = Inspection.RangeResult.None;
                }
            }

            public long m_FunctionalSystemID;
            public string m_strFunctionalSystemName;
            //public Dictionary<Inspection.InspectionType, long> m_dictTypes = new Dictionary<Inspection.InspectionType, long>();
            public Dictionary<Inspection.InspectionType, Dictionary<string, SystemItem>> m_dictSystemItems = new Dictionary<Inspection.InspectionType, Dictionary<string, SystemItem>>();

            public Dictionary<Inspection.InspectionType, Dictionary<long, long>> m_dictUnionMainID = new Dictionary<Inspection.InspectionType, Dictionary<long, long>>();
            public Dictionary<Inspection.InspectionType, Dictionary<long, List<long>>> m_dictUnionExtraID = new Dictionary<Inspection.InspectionType, Dictionary<long, List<long>>>();

            public Dictionary<Inspection.InspectionType, long> m_dictSubTypes = new Dictionary<Inspection.InspectionType, long>();
            public Dictionary<Inspection.InspectionType, Inspection.RangeResult> m_dictResults = new Dictionary<Inspection.InspectionType, Inspection.RangeResult>();
        }

        public class Equipment
        {
            public Equipment(long id, long EquipmentKindID)
            {
                m_ID = id;
                m_EquipmentKindID = EquipmentKindID;
                m_image = null;

                m_listFunctionalSystem = new List<FunctionalSystem>();
                m_dictFunctionalSystemPos = new Dictionary<long, int>();
                m_dictSystemItemExclude = new Dictionary<long, long>();
                m_dictParameterExclude = new Dictionary<long, long>();
                m_dictFields = new Dictionary<string, string>();
            }

            public long m_ID;
            public long m_EquipmentKindID;
            public Image m_image;

            public List<FunctionalSystem> m_listFunctionalSystem;
            public Dictionary<long, int> m_dictFunctionalSystemPos;
            public Dictionary<long, long> m_dictSystemItemExclude;
            public Dictionary<long, long> m_dictParameterExclude;
            public Dictionary<string, string> m_dictFields;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private static void AddParam(SQLiteCommand com, string name, DbType type, object value)
        {
            SQLiteParameter param = new SQLiteParameter(name, type);
            param.Value = value;
            com.Parameters.Add(param);
        }

        public static double GetFillability(long EquipmentKindID, List<ReportInfo.Equipment> m_listEquipments, Dictionary<Inspection.InspectionType, List<long?>> dictCommonSubTypes, int index, Dictionary<Inspection.InspectionType, double> m_InspectionTypeFillability)
        {
            //foreach (Inspection.InspectionType m_type in Enum.GetValues(typeof(Inspection.InspectionType)))
            for (int i = 0; i < Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)EquipmentKindID].Count; i++)
            {
                m_InspectionTypeFillability[Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)EquipmentKindID][i]] = 0;
            }

            int iCntAllParameters = 0;
            double iCntAllNoEmptyParameters = 0;

            //foreach (Inspection.InspectionType m_type in Enum.GetValues(typeof(Inspection.InspectionType)))
            for (int ii = 0; ii < Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)EquipmentKindID].Count; ii++)
            {
                Inspection.InspectionType m_type = Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)EquipmentKindID][ii];

                int iCntParameters = 0;
                int iCntNoEmptyParameters = 0;

                for (int i = 0; i < m_listEquipments[index].m_listFunctionalSystem.Count; i++) // по функциональным системам
                {
                    string strFunctionalSystemName = m_listEquipments[index].m_listFunctionalSystem[i].m_strFunctionalSystemName;

                    long m_sub_type_id = m_listEquipments[index].m_listFunctionalSystem[i].m_dictSubTypes[m_type];

                    if (m_listEquipments[index].m_listFunctionalSystem[i].m_dictUnionMainID[m_type].ContainsKey(m_sub_type_id)) continue;

                    if (m_sub_type_id == 0 && dictCommonSubTypes[m_type][0] != null) m_sub_type_id = (long)dictCommonSubTypes[m_type][0]; // на всякий случай, если нет данных, то берется первое подобследование

                    foreach (KeyValuePair<string, ReportInfo.SystemItem> si in m_listEquipments[index].m_listFunctionalSystem[i].m_dictSystemItems[m_type]) // по подсистемам
                    {
                        for (int j = 0; j < si.Value.m_listParameterResults.Count; j++) // по параметрам
                        {
                            if (si.Value.m_listParameterResults[j].m_InspectionSubType == null
                                || si.Value.m_listParameterResults[j].m_InspectionSubType == m_sub_type_id)
                            {
                                if (si.Value.m_listParameterResults[j].m_ParameterType == Inspection.ParameterType.Select ||
                                    si.Value.m_listParameterResults[j].m_ParameterType == Inspection.ParameterType.Number ||
                                    si.Value.m_listParameterResults[j].m_ParameterType == Inspection.ParameterType.Integer)
                                {
                                    if (si.Value.m_listParameterResults[j].m_bEnable)
                                    {
                                        iCntParameters++;
                                        iCntAllParameters++;

                                        if (si.Value.m_listParameterResults[j].m_valNumber != null ||
                                            si.Value.m_listParameterResults[j].m_valSelect != null)
                                        {
                                            iCntAllNoEmptyParameters++;
                                            iCntNoEmptyParameters++;
                                        }
                                        else
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (iCntParameters > 0)
                    m_InspectionTypeFillability[m_type] = iCntNoEmptyParameters / (double)iCntParameters;
            }

            if (iCntAllParameters > 0)
                return iCntAllNoEmptyParameters / (double)iCntAllParameters;
            else return 0;
        }

        public static bool GetData(long checkID, List<ReportInfo.Equipment> m_listEquipments, Dictionary<Inspection.InspectionType, List<long?>> dictCommonSubTypes,
            Dictionary<Inspection.InspectionType, List<ReportInfo.InspectionSubType_>> list_sub_types,
            int index, bool bAllItems = false)
        {
            try
            {
                SQLiteConnection m_con = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                m_con.Open();
                SQLiteCommand com = new SQLiteCommand(m_con);
                com.CommandType = CommandType.Text;

                Dictionary<long, long> dictInputVoltageIndexes = new Dictionary<long, long>();
                //dictInputVoltageIndexes[Inspection.InspectionType.FHA] = new Dictionary<long, long>();
                //dictInputVoltageIndexes[Inspection.InspectionType.HARG] = new Dictionary<long, long>();

                long m_RPNCount = 0;
                long m_CoolingSystemTypeID = -1;
                long m_RPNKind = -1;
                long m_EquipmentKindID = -1;
                long m_ProtectionOilTypeID = -1;

                com.CommandText = "Select Equipments.*, Substations.SubstationName, Branches.BranchName, Subjects.SubjectName, Substations.SubstationType, EquipmentTypes.EquipmentTypeName, " +
                    "EquipmentClasses.EquipmentClassName, m.ManufacturerName, RPNTypes.RPNTypeName, SwitchDriveTypes.SwitchDriveTypeName, " +
                    "ihA.InputKind as InputKindHighA, ihB.InputKind as InputKindHighB, ihC.InputKind as InputKindHighC, " +
                    "imA.InputKind as InputKindMiddleA, imB.InputKind as InputKindMiddleB, imC.InputKind as InputKindMiddleC, iNN.InputKind as InputKindNeutral, " +
                    "input_t_hA.InputVoltageTypeName as InputTypeHighA, input_t_hB.InputVoltageTypeName as InputTypeHighB, input_t_hC.InputVoltageTypeName as InputTypeHighC, " +
                    "input_t_mA.InputVoltageTypeName as InputTypeMiddleA, input_t_mB.InputVoltageTypeName as InputTypeMiddleB, input_t_mC.InputVoltageTypeName as InputTypeMiddleC, input_t_N.InputVoltageTypeName as InputTypeNeutral, " +
                    "ihA.InputNumber as InputNumberHighA, ihB.InputNumber as InputNumberHighB, ihC.InputNumber as InputNumberHighC, " +
                    "imA.InputNumber as InputNumberMiddleA, imB.InputNumber as InputNumberMiddleB, imC.InputNumber as InputNumberMiddleC, iNN.InputNumber as InputNumberNeutral, " +
                    "ihA.InputCreateYear as InputCreateYearHighA, ihB.InputCreateYear as InputCreateYearHighB, ihC.InputCreateYear as InputCreateYearHighC, " +
                    "imA.InputCreateYear as InputCreateYearMiddleA, imB.InputCreateYear as InputCreateYearMiddleB, imC.InputCreateYear as InputCreateYearMiddleC, iNN.InputCreateYear as InputCreateYearNeutral, " +
                    "ihA.InputUseBeginYear as InputUseBeginYearHighA, ihB.InputUseBeginYear as InputUseBeginYearHighB, ihC.InputUseBeginYear as InputUseBeginYearHighC, " +
                    "imA.InputUseBeginYear as InputUseBeginYearMiddleA, imB.InputUseBeginYear as InputUseBeginYearMiddleB, imC.InputUseBeginYear as InputUseBeginYearMiddleC, iNN.InputUseBeginYear as InputUseBeginYearNeutral, " +
                    "ihA.InputName as InputNameHighA, ihB.InputName as InputNameHighB, ihC.InputName as InputNameHighC, " +
                    "imA.InputName as InputNameMiddleA, imB.InputName as InputNameMiddleB, imC.InputName as InputNameMiddleC, iNN.InputName as InputNameNeutral, " +
                    "input_m_hA.ManufacturerInputName as InputManufacturerNameHighA, input_m_hB.ManufacturerInputName as InputManufacturerNameHighB, input_m_hC.ManufacturerInputName as InputManufacturerNameHighC, " +
                    "input_m_mA.ManufacturerInputName as InputManufacturerNameMiddleA, input_m_mB.ManufacturerInputName as InputManufacturerNameMiddleB, input_m_mC.ManufacturerInputName as InputManufacturerNameMiddleC, " +
                    "input_m_N.ManufacturerInputName as InputManufacturerNameNeutral " +
                    "FROM Equipments " +
                    "LEFT JOIN Substations ON Substations.SubstationID = Equipments.SubstationID " +
                    "LEFT JOIN Branches ON Branches.BranchID = Substations.BranchID " +
                    "LEFT JOIN Subjects ON Subjects.SubjectID = Branches.SubjectID " +
                    "LEFT JOIN Manufacturers as m ON m.ManufacturerID = Equipments.ManufacturerID " +
                    "LEFT JOIN EquipmentTypes ON EquipmentTypes.EquipmentTypeID = Equipments.EquipmentTypeID AND EquipmentTypes.EquipmentKindID = Equipments.EquipmentKindID " +
                    "LEFT JOIN EquipmentClasses ON EquipmentClasses.EquipmentClassID = Equipments.EquipmentClassID AND EquipmentClasses.EquipmentKindID = Equipments.EquipmentKindID " +
                    "LEFT JOIN SwitchDriveTypes ON SwitchDriveTypes.SwitchDriveTypeID = Equipments.SwitchDriveTypeID AND SwitchDriveTypes.EquipmentKindID = Equipments.EquipmentKindID " +
                    "LEFT JOIN RPNTypes ON RPNTypes.RPNTypeID = Equipments.RPNTypeID " +
                    "LEFT JOIN Inputs as ihA ON ihA.InputID = Equipments.InputIDHighA " +
                    "LEFT JOIN Inputs as ihB ON ihB.InputID = Equipments.InputIDHighB " +
                    "LEFT JOIN Inputs as ihC ON ihC.InputID = Equipments.InputIDHighC " +
                    "LEFT JOIN Inputs as imA ON imA.InputID = Equipments.InputIDMiddleA " +
                    "LEFT JOIN Inputs as imB ON imB.InputID = Equipments.InputIDMiddleB " +
                    "LEFT JOIN Inputs as imC ON imC.InputID = Equipments.InputIDMiddleC " +
                    "LEFT JOIN Inputs as iNN ON iNN.InputID = Equipments.InputIDNeutral " +
                    "LEFT JOIN ManufacturersInputs as input_m_hA ON input_m_hA.ManufacturerInputID = ihA.InputManufacturerID " +
                    "LEFT JOIN ManufacturersInputs as input_m_hB ON input_m_hB.ManufacturerInputID = ihB.InputManufacturerID " +
                    "LEFT JOIN ManufacturersInputs as input_m_hC ON input_m_hC.ManufacturerInputID = ihC.InputManufacturerID " +
                    "LEFT JOIN ManufacturersInputs as input_m_mA ON input_m_mA.ManufacturerInputID = imA.InputManufacturerID " +
                    "LEFT JOIN ManufacturersInputs as input_m_mB ON input_m_mB.ManufacturerInputID = imB.InputManufacturerID " +
                    "LEFT JOIN ManufacturersInputs as input_m_mC ON input_m_mC.ManufacturerInputID = imC.InputManufacturerID " +
                    "LEFT JOIN ManufacturersInputs as input_m_N ON input_m_N.ManufacturerInputID = iNN.InputManufacturerID " +
                    "LEFT JOIN InputVoltageTypes as input_t_hA ON input_t_hA.InputVoltageTypeID = ihA.InputTypeID " +
                    "LEFT JOIN InputVoltageTypes as input_t_hB ON input_t_hB.InputVoltageTypeID = ihB.InputTypeID " +
                    "LEFT JOIN InputVoltageTypes as input_t_hC ON input_t_hC.InputVoltageTypeID = ihC.InputTypeID " +
                    "LEFT JOIN InputVoltageTypes as input_t_mA ON input_t_mA.InputVoltageTypeID = imA.InputTypeID " +
                    "LEFT JOIN InputVoltageTypes as input_t_mB ON input_t_mB.InputVoltageTypeID = imB.InputTypeID " +
                    "LEFT JOIN InputVoltageTypes as input_t_mC ON input_t_mC.InputVoltageTypeID = imC.InputTypeID " +
                    "LEFT JOIN InputVoltageTypes as input_t_N ON input_t_N.InputVoltageTypeID = iNN.InputTypeID " +
                    "WHERE EquipmentID = @id";
                AddParam(com, "@id", DbType.Int64, m_listEquipments[index].m_ID);

                SQLiteDataReader drEquip = com.ExecuteReader();
                if (drEquip.HasRows)
                {
                    while (drEquip.Read())
                    {
                        // *Год_ввода_в_эксплуатацию
                        // *Год_изготовления
                        // *Год_изготовления2
                        // *Год_оценки
                        // *Диспетчерское_наименование
                        // *Диспетчерское_наименование2
                        // *Завод_изготовитель
                        // *Заводской_номер
                        // *Заводской_номер2
                        // Марка_масла
                        // *Напряжение_ВН
                        // *Напряжение_нейтрали
                        // *Напряжение_НН
                        // *Напряжение_СН
                        // *Номинальная_мощность
                        // *Подстанция
                        // Рисунок
                        // *Система_охлаждения
                        // *Субъект
                        // *Тип_защиты_масла
                        // *Тип_трансформатора
                        // *Тип_трансформатора2
                        // *Трансформатор1
                        // *Трансформатор2
                        // *Трансформатор3
                        // *Филиал
                        // *Число_фаз

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

                        /*if (drEquip["InputKindHighA"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighA"]) == 1) // не твердая изоляция и не масляная негерметичная
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.HARG][1] = Convert.ToInt64(drEquip["InputKindHighA"]);
                        }
                        if (drEquip["InputKindHighB"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighB"]) == 1)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.HARG][2] = Convert.ToInt64(drEquip["InputKindHighB"]);
                        }
                        if (drEquip["InputKindHighC"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighC"]) == 1)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.HARG][3] = Convert.ToInt64(drEquip["InputKindHighC"]);
                        }
                        if (drEquip["InputKindMiddleA"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleA"]) == 1)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.HARG][4] = Convert.ToInt64(drEquip["InputKindMiddleA"]);
                        }
                        if (drEquip["InputKindMiddleB"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleB"]) == 1)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.HARG][5] = Convert.ToInt64(drEquip["InputKindMiddleB"]);
                        }
                        if (drEquip["InputKindMiddleC"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleC"]) == 1)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.HARG][6] = Convert.ToInt64(drEquip["InputKindMiddleC"]);
                        }
                        if (drEquip["InputKindNeutral"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindNeutral"]) == 1)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.HARG][7] = Convert.ToInt64(drEquip["InputKindNeutral"]);
                        }
                        if (drEquip["InputKindHighA"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighA"]) != 3) // не твердая изоляция
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.FHA][1] = Convert.ToInt64(drEquip["InputKindHighA"]);
                        }
                        if (drEquip["InputKindHighB"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighB"]) != 3)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.FHA][2] = Convert.ToInt64(drEquip["InputKindHighB"]);
                        }
                        if (drEquip["InputKindHighC"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindHighC"]) != 3)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.FHA][3] = Convert.ToInt64(drEquip["InputKindHighC"]);
                        }
                        if (drEquip["InputKindMiddleA"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleA"]) != 3)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.FHA][4] = Convert.ToInt64(drEquip["InputKindMiddleA"]);
                        }
                        if (drEquip["InputKindMiddleB"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleB"]) != 3)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.FHA][5] = Convert.ToInt64(drEquip["InputKindMiddleB"]);
                        }
                        if (drEquip["InputKindMiddleC"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindMiddleC"]) != 3)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.FHA][6] = Convert.ToInt64(drEquip["InputKindMiddleC"]);
                        }
                        if (drEquip["InputKindNeutral"] != DBNull.Value && Convert.ToInt64(drEquip["InputKindNeutral"]) != 3)
                        {
                            dictInputVoltageIndexes[Inspection.InspectionType.FHA][7] = Convert.ToInt64(drEquip["InputKindNeutral"]);
                        }*/

                        if (drEquip["CoolingSystemTypeID"] != DBNull.Value && Convert.ToInt64(drEquip["CoolingSystemTypeID"]) != 0)
                        {
                            m_CoolingSystemTypeID = Convert.ToInt64(drEquip["CoolingSystemTypeID"]);

                            switch (Convert.ToInt64(drEquip["CoolingSystemTypeID"]))
                            {
                                case 1:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "Д (Принудительная циркуляция воздуха и естественная циркуляция масла)";
                                    break;
                                case 2:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ДЦ (Принудительная циркуляция воздуха и масла с ненаправленным потоком масла)";
                                    break;
                                case 3:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "М (Естественная циркуляция воздуха и масла)";
                                    break;
                                case 4:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "МЦ (Естественная циркуляция воздуха и принудительная циркуляция масла с ненаправленным потоком масла)";
                                    break;
                                case 5:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "НМЦ (Естественная циркуляция воздуха и принудительная циркуляция масла с направленным потоком масла)";
                                    break;
                                case 6:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "НДЦ (Принудительная циркуляция воздуха и масла с направленным потоком масла)";
                                    break;
                                case 7:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "Ц (Принудительная циркуляция воды и масла с ненаправленным потоком масла)";
                                    break;
                                case 8:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "НЦ (Принудительная циркуляция воды и масла с направленным потоком масла)";
                                    break;
                                case 9:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ONAF (Принудительная циркуляция воздуха и естественная циркуляция масла)";
                                    break;
                                case 10:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "OFAF (Принудительная циркуляция воздуха и масла с ненаправленным потоком масла)";
                                    break;
                                case 11:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ONAN (Естественная циркуляция воздуха и масла)";
                                    break;
                                case 12:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "OFAN (Естественная циркуляция воздуха и принудительная циркуляция масла с ненаправленным потоком масла)";
                                    break;
                                case 13:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ODAN (Естественная циркуляция воздуха и принудительная циркуляция масла с направленным потоком масла)";
                                    break;
                                case 14:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ODAF (Принудительная циркуляция воздуха и масла с направленным потоком масла)";
                                    break;
                                case 15:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "OFWF (Принудительная циркуляция воды и масла с ненаправленным потоком масла)";
                                    break;
                                case 16:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ODWF (Принудительная циркуляция воды и масла с направленным потоком масла)";
                                    break;
                            }
                        }

                        if (drEquip["RPNCnt"] != DBNull.Value && Convert.ToInt64(drEquip["RPNCnt"]) != 0)
                        {
                            m_RPNCount = Convert.ToInt64(drEquip["RPNCnt"]);
                        }

                        if (drEquip["RPNKind"] != DBNull.Value && Convert.ToInt64(drEquip["RPNKind"]) != 0)
                        {
                            m_RPNKind = Convert.ToInt64(drEquip["RPNKind"]);
                        }

                        m_listEquipments[index].m_dictFields["EquipmentName"] = drEquip["EquipmentName"].ToString();
                        if (drEquip["UseBeginYear"] != DBNull.Value) m_listEquipments[index].m_dictFields["UseBeginYear"] = drEquip["UseBeginYear"].ToString();
                        else m_listEquipments[index].m_dictFields["UseBeginYear"] = "";
                        if (drEquip["CreateYear"] != DBNull.Value) m_listEquipments[index].m_dictFields["CreateYear"] = drEquip["CreateYear"].ToString();
                        else m_listEquipments[index].m_dictFields["CreateYear"] = "";
                        if (drEquip["EquipmentNumber"] != DBNull.Value) m_listEquipments[index].m_dictFields["EquipmentNumber"] = drEquip["EquipmentNumber"].ToString();
                        else m_listEquipments[index].m_dictFields["EquipmentNumber"] = "";
                        if (drEquip["NominalVoltageHigh"] != DBNull.Value) m_listEquipments[index].m_dictFields["NominalVoltageHigh"] = drEquip["NominalVoltageHigh"].ToString();
                        else m_listEquipments[index].m_dictFields["NominalVoltageHigh"] = "";
                        if (drEquip["NominalVoltageMiddle"] != DBNull.Value) m_listEquipments[index].m_dictFields["NominalVoltageMiddle"] = drEquip["NominalVoltageMiddle"].ToString();
                        else m_listEquipments[index].m_dictFields["NominalVoltageMiddle"] = "";
                        if (drEquip["NominalVoltageLow"] != DBNull.Value) m_listEquipments[index].m_dictFields["NominalVoltageLow"] = drEquip["NominalVoltageLow"].ToString();
                        else m_listEquipments[index].m_dictFields["NominalVoltageLow"] = "";
                        if (drEquip["NominalVoltageNeitral"] != DBNull.Value) m_listEquipments[index].m_dictFields["NominalVoltageNeitral"] = drEquip["NominalVoltageNeitral"].ToString();
                        else m_listEquipments[index].m_dictFields["NominalVoltageNeitral"] = "";
                        if (drEquip["SubstationName"] != DBNull.Value) m_listEquipments[index].m_dictFields["SubstationName"] = drEquip["SubstationName"].ToString();
                        else m_listEquipments[index].m_dictFields["SubstationName"] = "";
                        if (drEquip["SubstationType"] != DBNull.Value) m_listEquipments[index].m_dictFields["SubstationType"] = drEquip["SubstationType"].ToString();
                        else m_listEquipments[index].m_dictFields["SubstationType"] = "";
                        if (drEquip["BranchName"] != DBNull.Value) m_listEquipments[index].m_dictFields["BranchName"] = drEquip["BranchName"].ToString();
                        else m_listEquipments[index].m_dictFields["BranchName"] = "";
                        if (drEquip["SubjectName"] != DBNull.Value) m_listEquipments[index].m_dictFields["SubjectName"] = drEquip["SubjectName"].ToString();
                        else m_listEquipments[index].m_dictFields["SubjectName"] = "";
                        if (drEquip["EquipmentTypeName"] != DBNull.Value) m_listEquipments[index].m_dictFields["EquipmentTypeName"] = drEquip["EquipmentTypeName"].ToString();
                        else m_listEquipments[index].m_dictFields["EquipmentTypeName"] = "";
                        if (drEquip["EquipmentClassName"] != DBNull.Value) m_listEquipments[index].m_dictFields["EquipmentClassName"] = drEquip["EquipmentClassName"].ToString();
                        else m_listEquipments[index].m_dictFields["EquipmentClassName"] = "";
                        if (drEquip["ManufacturerName"] != DBNull.Value) m_listEquipments[index].m_dictFields["ManufacturerName"] = drEquip["ManufacturerName"].ToString();
                        else m_listEquipments[index].m_dictFields["ManufacturerName"] = "";
                        if (drEquip["NominalPower"] != DBNull.Value) m_listEquipments[index].m_dictFields["NominalPower"] = drEquip["NominalPower"].ToString();
                        else m_listEquipments[index].m_dictFields["NominalPower"] = "";
                        if (drEquip["NominalCurrent"] != DBNull.Value) m_listEquipments[index].m_dictFields["NominalCurrent"] = drEquip["NominalCurrent"].ToString();
                        else m_listEquipments[index].m_dictFields["NominalCurrent"] = "";
                        if (drEquip["SwitchDriveTypeName"] != DBNull.Value) m_listEquipments[index].m_dictFields["SwitchDriveTypeName"] = drEquip["SwitchDriveTypeName"].ToString();
                        else m_listEquipments[index].m_dictFields["SwitchDriveTypeName"] = "";
                        
                        if (drEquip["ProtectionOilTypeID"] != DBNull.Value)
                        {
                            m_ProtectionOilTypeID = Convert.ToInt64(drEquip["ProtectionOilTypeID"]);

                            switch (Convert.ToInt64(drEquip["ProtectionOilTypeID"]))
                            {
                                case 1:
                                    m_listEquipments[index].m_dictFields["ProtectionOilTypeID"] = "пленочная защита";
                                    break;
                                case 2:
                                    m_listEquipments[index].m_dictFields["ProtectionOilTypeID"] = "азотная защита";
                                    break;
                                case 3:
                                    m_listEquipments[index].m_dictFields["ProtectionOilTypeID"] = "свободное дыхание (воздухоосушитель)";
                                    break;
                            }
                        }
                        else m_listEquipments[index].m_dictFields["ProtectionOilTypeID"] = "";
                        if (drEquip["ConstructionType"] != DBNull.Value) m_listEquipments[index].m_dictFields["ConstructionType"] = drEquip["ConstructionType"].ToString();
                        else m_listEquipments[index].m_dictFields["ConstructionType"] = "";
                        if (drEquip["CoolingSystemTypeID"] != DBNull.Value)
                        {
                            switch (Convert.ToInt64(drEquip["CoolingSystemTypeID"]))
                            {
                                case 1:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "Д (Принудительная циркуляция воздуха и естественная циркуляция масла)";
                                    break;
                                case 2:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ДЦ (Принудительная циркуляция воздуха и масла с ненаправленным потоком масла)";
                                    break;
                                case 3:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "М (Естественная циркуляция воздуха и масла)";
                                    break;
                                case 4:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "МЦ (Естественная циркуляция воздуха и принудительная циркуляция масла с ненаправленным потоком масла)";
                                    break;
                                case 5:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "НМЦ (Естественная циркуляция воздуха и принудительная циркуляция масла с направленным потоком масла)";
                                    break;
                                case 6:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "НДЦ (Принудительная циркуляция воздуха и масла с направленным потоком масла)";
                                    break;
                                case 7:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "Ц (Принудительная циркуляция воды и масла с ненаправленным потоком масла)";
                                    break;
                                case 8:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "НЦ (Принудительная циркуляция воды и масла с направленным потоком масла)";
                                    break;
                                case 9:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ONAF (Принудительная циркуляция воздуха и естественная циркуляция масла)";
                                    break;
                                case 10:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "OFAF (Принудительная циркуляция воздуха и масла с ненаправленным потоком масла)";
                                    break;
                                case 11:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ONAN (Естественная циркуляция воздуха и масла)";
                                    break;
                                case 12:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "OFAN (Естественная циркуляция воздуха и принудительная циркуляция масла с ненаправленным потоком масла)";
                                    break;
                                case 13:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ODAN (Естественная циркуляция воздуха и принудительная циркуляция масла с направленным потоком масла)";
                                    break;
                                case 14:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ODAF (Принудительная циркуляция воздуха и масла с направленным потоком масла)";
                                    break;
                                case 15:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "OFWF (Принудительная циркуляция воды и масла с ненаправленным потоком масла)";
                                    break;
                                case 16:
                                    m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "ODWF (Принудительная циркуляция воды и масла с направленным потоком масла)";
                                    break;
                            }
                        }
                        else m_listEquipments[index].m_dictFields["CoolingSystemTypeID"] = "";

                        string strFieldKindName = "";
                        string strFieldManufacturerName = "";
                        string strFieldNumberName = "";
                        string strFieldTypeName = "";
                        string strFieldCreateYearName = "";
                        string strFieldUseBeginYearName = "";
                        string strFieldName = "";

                        for (int i = 0; i < 7; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    strFieldKindName = "InputKindHighA";
                                    strFieldTypeName = "InputTypeHighA";
                                    strFieldManufacturerName = "InputManufacturerNameHighA";
                                    strFieldNumberName = "InputNumberHighA";
                                    strFieldCreateYearName = "InputCreateYearHighA";
                                    strFieldUseBeginYearName = "InputUseBeginYearHighA";
                                    strFieldName = "InputNameHighA";
                                    break;
                                case 1:
                                    strFieldKindName = "InputKindHighB";
                                    strFieldTypeName = "InputTypeHighB";
                                    strFieldManufacturerName = "InputManufacturerNameHighB";
                                    strFieldNumberName = "InputNumberHighB";
                                    strFieldCreateYearName = "InputCreateYearHighB";
                                    strFieldUseBeginYearName = "InputUseBeginYearHighB";
                                    strFieldName = "InputNameHighB";
                                    break;
                                case 2:
                                    strFieldKindName = "InputKindHighC";
                                    strFieldTypeName = "InputTypeHighC";
                                    strFieldManufacturerName = "InputManufacturerNameHighC";
                                    strFieldNumberName = "InputNumberHighC";
                                    strFieldCreateYearName = "InputCreateYearHighC";
                                    strFieldUseBeginYearName = "InputUseBeginYearHighC";
                                    strFieldName = "InputNameHighC";
                                    break;
                                case 3:
                                    strFieldKindName = "InputKindMiddleA";
                                    strFieldTypeName = "InputTypeMiddleA";
                                    strFieldManufacturerName = "InputManufacturerNameMiddleA";
                                    strFieldNumberName = "InputNumberMiddleA";
                                    strFieldCreateYearName = "InputCreateYearMiddleA";
                                    strFieldUseBeginYearName = "InputUseBeginYearMiddleA";
                                    strFieldName = "InputNameMiddleA";
                                    break;
                                case 4:
                                    strFieldKindName = "InputKindMiddleB";
                                    strFieldTypeName = "InputTypeMiddleB";
                                    strFieldManufacturerName = "InputManufacturerNameMiddleB";
                                    strFieldNumberName = "InputNumberMiddleB";
                                    strFieldCreateYearName = "InputCreateYearMiddleB";
                                    strFieldUseBeginYearName = "InputUseBeginYearMiddleB";
                                    strFieldName = "InputNameMiddleB";
                                    break;
                                case 5:
                                    strFieldKindName = "InputKindMiddleC";
                                    strFieldTypeName = "InputTypeMiddleC";
                                    strFieldManufacturerName = "InputManufacturerNameMiddleC";
                                    strFieldNumberName = "InputNumberMiddleC";
                                    strFieldCreateYearName = "InputCreateYearMiddleC";
                                    strFieldUseBeginYearName = "InputUseBeginYearMiddleC";
                                    strFieldName = "InputNameMiddleC";
                                    break;
                                case 6:
                                    strFieldKindName = "InputKindNeutral";
                                    strFieldTypeName = "InputTypeNeutral";
                                    strFieldManufacturerName = "InputManufacturerNameNeutral";
                                    strFieldNumberName = "InputNumberNeutral";
                                    strFieldCreateYearName = "InputCreateYearNeutral";
                                    strFieldUseBeginYearName = "InputUseBeginYearNeutral";
                                    strFieldName = "InputNameNeutral";
                                    break;
                            }

                            if (drEquip[strFieldKindName] != DBNull.Value)
                            {
                                switch (Convert.ToInt64(drEquip[strFieldKindName]))
                                {
                                    case 1:
                                        m_listEquipments[index].m_dictFields[strFieldKindName] = "масляный герметичный";
                                        break;
                                    case 2:
                                        m_listEquipments[index].m_dictFields[strFieldKindName] = "масляный негерметичный";
                                        break;
                                    case 3:
                                        m_listEquipments[index].m_dictFields[strFieldKindName] = "твердая изоляция";
                                        break;
                                }
                            }
                            else m_listEquipments[index].m_dictFields[strFieldKindName] = "";

                            if (drEquip[strFieldManufacturerName] != DBNull.Value) m_listEquipments[index].m_dictFields[strFieldManufacturerName] = drEquip[strFieldManufacturerName].ToString();
                            else m_listEquipments[index].m_dictFields[strFieldManufacturerName] = "";

                            if (drEquip[strFieldNumberName] != DBNull.Value) m_listEquipments[index].m_dictFields[strFieldNumberName] = drEquip[strFieldNumberName].ToString();
                            else m_listEquipments[index].m_dictFields[strFieldNumberName] = "";

                            if (drEquip[strFieldTypeName] != DBNull.Value) m_listEquipments[index].m_dictFields[strFieldTypeName] = drEquip[strFieldTypeName].ToString();
                            else m_listEquipments[index].m_dictFields[strFieldTypeName] = "";

                            if (drEquip[strFieldCreateYearName] != DBNull.Value) m_listEquipments[index].m_dictFields[strFieldCreateYearName] = drEquip[strFieldCreateYearName].ToString();
                            else m_listEquipments[index].m_dictFields[strFieldCreateYearName] = "";

                            if (drEquip[strFieldUseBeginYearName] != DBNull.Value) m_listEquipments[index].m_dictFields[strFieldUseBeginYearName] = drEquip[strFieldUseBeginYearName].ToString();
                            else m_listEquipments[index].m_dictFields[strFieldUseBeginYearName] = "";

                            if (drEquip[strFieldName] != DBNull.Value) m_listEquipments[index].m_dictFields[strFieldName] = drEquip[strFieldName].ToString();
                            else m_listEquipments[index].m_dictFields[strFieldName] = "";
                        }

                        if (drEquip["RPNCnt"] != DBNull.Value)
                        {
                            switch (Convert.ToInt64(drEquip["RPNCnt"]))
                            {
                                case 0:
                                    m_listEquipments[index].m_dictFields["RPNCnt"] = "нет";
                                    break;
                                default:
                                    m_listEquipments[index].m_dictFields["RPNCnt"] = drEquip["RPNCnt"].ToString();
                                    break;
                            }
                        }
                        else m_listEquipments[index].m_dictFields["RPNCnt"] = "";

                        if (drEquip["RPNKind"] != DBNull.Value)
                        {
                            switch (Convert.ToInt64(drEquip["RPNKind"]))
                            {
                                case 0:
                                    m_listEquipments[index].m_dictFields["RPNKind"] = "погружной";
                                    break;
                                case 1:
                                    m_listEquipments[index].m_dictFields["RPNKind"] = "в навесном баке";
                                    break;
                            }
                        }
                        else m_listEquipments[index].m_dictFields["RPNKind"] = "";

                        //if (drEquip["RPNManufacturerName"] != DBNull.Value) m_listEquipments[index].m_dictFields["RPNManufacturerName"] = drEquip["RPNManufacturerName"].ToString();
                        //else m_listEquipments[index].m_dictFields["RPNManufacturerName"] = "";

                        if (drEquip["RPNVoltage"] != DBNull.Value) m_listEquipments[index].m_dictFields["RPNVoltage"] = drEquip["RPNVoltage"].ToString();
                        else m_listEquipments[index].m_dictFields["RPNVoltage"] = "";

                        if (drEquip["RPNNumber"] != DBNull.Value) m_listEquipments[index].m_dictFields["RPNNumber"] = drEquip["RPNNumber"].ToString();
                        else m_listEquipments[index].m_dictFields["RPNNumber"] = "";

                        if (drEquip["RPNNumber2"] != DBNull.Value) m_listEquipments[index].m_dictFields["RPNNumber2"] = drEquip["RPNNumber2"].ToString();
                        else m_listEquipments[index].m_dictFields["RPNNumber2"] = "";

                        if (drEquip["RPNNumber3"] != DBNull.Value) m_listEquipments[index].m_dictFields["RPNNumber3"] = drEquip["RPNNumber3"].ToString();
                        else m_listEquipments[index].m_dictFields["RPNNumber3"] = "";

                        if (drEquip["RPNTypeName"] != DBNull.Value) m_listEquipments[index].m_dictFields["RPNTypeName"] = drEquip["RPNTypeName"].ToString();
                        else m_listEquipments[index].m_dictFields["RPNTypeName"] = "";

                        //if (drEquip["RPNCreateYear"] != DBNull.Value) m_listEquipments[index].m_dictFields["RPNCreateYear"] = drEquip["RPNCreateYear"].ToString();
                        //else m_listEquipments[index].m_dictFields["RPNCreateYear"] = "";

                        //if (drEquip["RPNUseBeginYear"] != DBNull.Value) m_listEquipments[index].m_dictFields["RPNUseBeginYear"] = drEquip["RPNUseBeginYear"].ToString();
                        //else m_listEquipments[index].m_dictFields["RPNUseBeginYear"] = "";

                        if (drEquip["image"] != DBNull.Value)
                        {
                            m_listEquipments[index].m_image = new Bitmap(new MemoryStream((byte[])drEquip["image"]));
                            m_listEquipments[index].m_image = resizeImage(m_listEquipments[index].m_image, new System.Drawing.Size(550, m_listEquipments[index].m_image.Height * 550 / m_listEquipments[index].m_image.Width));
                        }
                    }
                }
                drEquip.Close();

                // ---------------------
                // Данные об исключении или присутствии подсистем или параметров в списке в зависимости от типа системы охлаждения
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
                AddParam(com, "@potid", DbType.Int64, m_ProtectionOilTypeID);
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

                        if (SystemItemID > 0) m_listEquipments[index].m_dictSystemItemExclude[SystemItemID] = 1;
                        if (ParameterID > 0) m_listEquipments[index].m_dictParameterExclude[ParameterID] = 1;

                        /*if (ExcludeType != 4) // для вводов используется отдельная проверка
                        {
                            ExcludeObjectID = 0;
                            ExcludeType = 0;
                        }

                        if (SystemItemID > 0)
                        {
                            if (!m_listEquipments[index].m_dictSystemItemExclude.ContainsKey(SystemItemID)) m_listEquipments[index].m_dictSystemItemExclude[SystemItemID] = new Dictionary<KeyValuePair<long, long>, long>();
                            if ((ExcludeObjectID + ExcludeType) > 0) m_listEquipments[index].m_dictSystemItemExclude[SystemItemID][new KeyValuePair<long, long>(ExcludeType, ExcludeObjectID)] = 1;
                        }
                        if (ParameterID > 0)
                        {
                            if (!m_listEquipments[index].m_dictParameterExclude.ContainsKey(ParameterID)) m_listEquipments[index].m_dictParameterExclude[ParameterID] = new Dictionary<KeyValuePair<long, long>, long>();
                            if ((ExcludeObjectID + ExcludeType) > 0) m_listEquipments[index].m_dictParameterExclude[ParameterID][new KeyValuePair<long, long>(ExcludeType, ExcludeObjectID)] = 1;
                        }*/
                    }
                }
                drParameterExclude.Close();

                // ---------------------
                com.Parameters.Clear();

                com.CommandText = "Select FunctionalSystemID, FunctionalSystemName FROM FunctionalSystems WHERE EquipmentKindID = @eq_kind ORDER BY Sort";
                AddParam(com, "@eq_kind", DbType.Int64, m_EquipmentKindID);

                SQLiteDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        long FunctionalSystemID = Convert.ToInt64(dr["FunctionalSystemID"]);
                        string strFunctionalSystemName = dr["FunctionalSystemName"].ToString();

                        m_listEquipments[index].m_listFunctionalSystem.Add(new ReportInfo.FunctionalSystem(m_EquipmentKindID, FunctionalSystemID, strFunctionalSystemName));
                        m_listEquipments[index].m_dictFunctionalSystemPos[FunctionalSystemID] = m_listEquipments[index].m_listFunctionalSystem.Count - 1;
                    }
                }
                dr.Close();

                // ---------------------

                list_sub_types.Clear();
                //foreach (Inspection.InspectionType m_type in Enum.GetValues(typeof(Inspection.InspectionType)))
                for (int ii = 0; ii < Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)m_EquipmentKindID].Count; ii++)
                {
                    list_sub_types[Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)m_EquipmentKindID][ii]] = new List<ReportInfo.InspectionSubType_>();
                }

                com.Parameters.Clear();
                com.CommandText = "SELECT InspectionSubTypeID, InspectionSubTypeName, InspectionType FROM InspectionSubTypes WHERE EquipmentKindID = @eq_kind ORDER BY InspectionSubTypeID";
                AddParam(com, "@eq_kind", DbType.Int64, m_EquipmentKindID);

                SQLiteDataReader drInspectionSubType = com.ExecuteReader();
                if (drInspectionSubType.HasRows)
                {
                    while (drInspectionSubType.Read())
                    {
                        long InspectionSubTypeID = Convert.ToInt64(drInspectionSubType["InspectionSubTypeID"]);
                        string strInspectionSubTypeName = drInspectionSubType["InspectionSubTypeName"].ToString();
                        Inspection.InspectionType InspectionType = (Inspection.InspectionType)Convert.ToInt64(drInspectionSubType["InspectionType"]);

                        list_sub_types[InspectionType].Add(new ReportInfo.InspectionSubType_(InspectionSubTypeID, strInspectionSubTypeName));
                    }
                }
                drInspectionSubType.Close();

                // ---------------------

                com.Parameters.Clear();
                com.CommandText = "Select InspectionSubType, MainFunctionalSystemID, ExtraFunctionalSystemID, InspectionType " +
                    " FROM UnionFunctionalSystems WHERE EquipmentKindID = @eq_kind ORDER BY UnionFunctionalSystemID";
                AddParam(com, "@eq_kind", DbType.Int64, m_EquipmentKindID);

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
                        Inspection.InspectionType InspectionType = (Inspection.InspectionType)Convert.ToInt64(drUnion["InspectionType"]);

                        if (m_listEquipments[index].m_dictFunctionalSystemPos.ContainsKey(MainFunctionalSystemID) && m_listEquipments[index].m_dictFunctionalSystemPos.ContainsKey(ExtraFunctionalSystemID))
                        {
                            int ExtraFunctionalSystemPos = m_listEquipments[index].m_dictFunctionalSystemPos[ExtraFunctionalSystemID];
                            int MainFunctionalSystemPos = m_listEquipments[index].m_dictFunctionalSystemPos[MainFunctionalSystemID];

                            if (sub_type_id != 0)
                            {
                                m_listEquipments[index].m_listFunctionalSystem[ExtraFunctionalSystemPos].m_dictUnionMainID[InspectionType][sub_type_id] = MainFunctionalSystemID;

                                if (!m_listEquipments[index].m_listFunctionalSystem[MainFunctionalSystemPos].m_dictUnionExtraID[InspectionType].ContainsKey(sub_type_id))
                                    m_listEquipments[index].m_listFunctionalSystem[MainFunctionalSystemPos].m_dictUnionExtraID[InspectionType][sub_type_id] = new List<long>();
                                m_listEquipments[index].m_listFunctionalSystem[MainFunctionalSystemPos].m_dictUnionExtraID[InspectionType][sub_type_id].Add(ExtraFunctionalSystemID);
                            }
                            else
                            {
                                // заносим объединения для все подтипов
                                m_listEquipments[index].m_listFunctionalSystem[ExtraFunctionalSystemPos].m_dictUnionMainID[InspectionType][0] = MainFunctionalSystemID;

                                if (!m_listEquipments[index].m_listFunctionalSystem[MainFunctionalSystemPos].m_dictUnionExtraID[InspectionType].ContainsKey(0))
                                    m_listEquipments[index].m_listFunctionalSystem[MainFunctionalSystemPos].m_dictUnionExtraID[InspectionType][0] = new List<long>();
                                m_listEquipments[index].m_listFunctionalSystem[MainFunctionalSystemPos].m_dictUnionExtraID[InspectionType][0].Add(ExtraFunctionalSystemID);

                                for (int i = 0; i < list_sub_types[InspectionType].Count; i++)
                                {
                                    m_listEquipments[index].m_listFunctionalSystem[ExtraFunctionalSystemPos].m_dictUnionMainID[InspectionType][list_sub_types[InspectionType][i].m_ID] = MainFunctionalSystemID;

                                    if (!m_listEquipments[index].m_listFunctionalSystem[MainFunctionalSystemPos].m_dictUnionExtraID[InspectionType].ContainsKey(list_sub_types[InspectionType][i].m_ID))
                                        m_listEquipments[index].m_listFunctionalSystem[MainFunctionalSystemPos].m_dictUnionExtraID[InspectionType][list_sub_types[InspectionType][i].m_ID] = new List<long>();
                                    m_listEquipments[index].m_listFunctionalSystem[MainFunctionalSystemPos].m_dictUnionExtraID[InspectionType][list_sub_types[InspectionType][i].m_ID].Add(ExtraFunctionalSystemID);
                                }
                            }
                        }
                    }
                }
                drUnion.Close();

                // ---------------------

                com.Parameters.Clear();
                com.CommandText = "SELECT SystemItemID, FunctionalSystemID, SystemItemName, UseInVisual, UseInHARG, UseInFHA, UseInWarm, UseInVibro, UseInParameter, UseInElectrical, MinRPNCnt, InputVoltageIndex, " +
                    " AlgorithmVisual, AlgorithmHARG, AlgorithmFHA, AlgorithmWarm, AlgorithmVibro, AlgorithmParameter, AlgorithmElectrical " +
                    " FROM SystemItems WHERE EquipmentKindID = @eq_kind ORDER BY SystemItemName";
                AddParam(com, "@eq_kind", DbType.Int64, m_EquipmentKindID);

                SQLiteDataReader drSystemItem = com.ExecuteReader();
                if (drSystemItem.HasRows)
                {
                    while (drSystemItem.Read())
                    {
                        long SystemItemID = Convert.ToInt64(drSystemItem["SystemItemID"]);
                        long FunctionalSystemID = Convert.ToInt64(drSystemItem["FunctionalSystemID"]);
                        string strSystemItemName = drSystemItem["SystemItemName"].ToString();

                        long UseInVisual = Convert.ToInt64(drSystemItem["UseInVisual"]);
                        long UseInHARG = Convert.ToInt64(drSystemItem["UseInHARG"]);
                        long UseInFHA = Convert.ToInt64(drSystemItem["UseInFHA"]);
                        long UseInWarm = Convert.ToInt64(drSystemItem["UseInWarm"]);
                        long UseInVibro = Convert.ToInt64(drSystemItem["UseInVibro"]);
                        long UseInParameter = Convert.ToInt64(drSystemItem["UseInParameter"]);
                        long UseInElectrical = Convert.ToInt64(drSystemItem["UseInElectrical"]);

                        string strAlgVisual = Convert.ToString(drSystemItem["AlgorithmVisual"]);
                        string strAlgHARG = Convert.ToString(drSystemItem["AlgorithmHARG"]);
                        string strAlgFHA = Convert.ToString(drSystemItem["AlgorithmFHA"]);
                        string strAlgWarm = Convert.ToString(drSystemItem["AlgorithmWarm"]);
                        string strAlgVibro = Convert.ToString(drSystemItem["AlgorithmVibro"]);
                        string strAlgParameter = Convert.ToString(drSystemItem["AlgorithmParameter"]);
                        string strAlgElectrical = Convert.ToString(drSystemItem["AlgorithmElectrical"]);

                        long? MinRPNCount = null;
                        if (drSystemItem["MinRPNCnt"] != DBNull.Value)
                            MinRPNCount = Convert.ToInt64(drSystemItem["MinRPNCnt"]);
                        long? InputVoltageIndex = null;
                        if (drSystemItem["InputVoltageIndex"] != DBNull.Value)
                            InputVoltageIndex = Convert.ToInt64(drSystemItem["InputVoltageIndex"]);

                        //foreach (Inspection.InspectionType m_type in Enum.GetValues(typeof(Inspection.InspectionType)))
                        for (int ii = 0; ii < Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)m_EquipmentKindID].Count; ii++)
                        {
                            Inspection.InspectionType m_type = Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)m_EquipmentKindID][ii];

                            if (m_type == Inspection.InspectionType.FHA && UseInFHA != 0 ||
                                m_type == Inspection.InspectionType.HARG && UseInHARG != 0 ||
                                m_type == Inspection.InspectionType.Visual && UseInVisual != 0 ||
                                m_type == Inspection.InspectionType.Warm && UseInWarm != 0 ||
                                m_type == Inspection.InspectionType.Vibro && UseInVibro != 0 ||
                                m_type == Inspection.InspectionType.Parameter && UseInParameter != 0 ||
                                m_type == Inspection.InspectionType.Electrical && UseInElectrical != 0)
                            {

                                string strAlg = "";
                                if (m_type == Inspection.InspectionType.FHA) strAlg = strAlgFHA;
                                if (m_type == Inspection.InspectionType.HARG) strAlg = strAlgHARG;
                                if (m_type == Inspection.InspectionType.Visual) strAlg = strAlgVisual;
                                if (m_type == Inspection.InspectionType.Warm) strAlg = strAlgWarm;
                                if (m_type == Inspection.InspectionType.Vibro) strAlg = strAlgVibro;
                                if (m_type == Inspection.InspectionType.Parameter) strAlg = strAlgParameter;
                                if (m_type == Inspection.InspectionType.Electrical) strAlg = strAlgElectrical;

                                // ------------------------
                                // ограничения
                                // ------------------------
                                bool bEnable = true;
                                if (!bAllItems)
                                {
                                    if (MinRPNCount != null && MinRPNCount > m_RPNCount) continue;
                                    if (InputVoltageIndex != null && dictInputVoltageIndexes[(long)InputVoltageIndex] <= 0) continue;
                                    if (m_listEquipments[index].m_dictSystemItemExclude.ContainsKey(SystemItemID)) continue;

                                    /*if (dictInputVoltageIndexes.ContainsKey(m_type))
                                    {
                                        if (InputVoltageIndex != null && !dictInputVoltageIndexes[m_type].ContainsKey((long)InputVoltageIndex)) continue;
                                    }
                                    if (m_listEquipments[index].m_dictSystemItemExclude.ContainsKey(SystemItemID))
                                    {
                                        if (m_listEquipments[index].m_dictSystemItemExclude[SystemItemID].Count == 0) continue;

                                        if (InputVoltageIndex != null && dictInputVoltageIndexes[m_type].ContainsKey((long)InputVoltageIndex))
                                        {
                                            KeyValuePair<long, long> val = new KeyValuePair<long,long>(4, dictInputVoltageIndexes[m_type][(long)InputVoltageIndex]);
                                            if (m_listEquipments[index].m_dictSystemItemExclude[SystemItemID].ContainsKey(val)) continue;
                                        }
                                    }*/
                                }
                                else
                                {
                                    if (MinRPNCount != null && MinRPNCount > m_RPNCount) bEnable = false;
                                    if (InputVoltageIndex != null && dictInputVoltageIndexes[(long)InputVoltageIndex] <= 0) bEnable = false;
                                    if (m_listEquipments[index].m_dictSystemItemExclude.ContainsKey(SystemItemID)) bEnable = false;

                                    /*if (dictInputVoltageIndexes.ContainsKey(m_type))
                                    {
                                        if (InputVoltageIndex != null && !dictInputVoltageIndexes[m_type].ContainsKey((long)InputVoltageIndex)) bEnable = false;
                                    }
                                    if (m_listEquipments[index].m_dictSystemItemExclude.ContainsKey(SystemItemID))
                                    {
                                        if (m_listEquipments[index].m_dictSystemItemExclude[SystemItemID].Count == 0) bEnable = false;

                                        if (InputVoltageIndex != null && dictInputVoltageIndexes[m_type].ContainsKey((long)InputVoltageIndex))
                                        {
                                            KeyValuePair<long, long> val = new KeyValuePair<long, long>(4, dictInputVoltageIndexes[m_type][(long)InputVoltageIndex]);
                                            if (m_listEquipments[index].m_dictSystemItemExclude[SystemItemID].ContainsKey(val)) bEnable = false;
                                        }
                                    }*/
                                    //if (m_listEquipments[index].m_dictSystemItemExclude.ContainsKey(SystemItemID)) bEnable = false;
                                }
                                // ------------------------

                                ReportInfo.FunctionalSystem fs = m_listEquipments[index].m_listFunctionalSystem[m_listEquipments[index].m_dictFunctionalSystemPos[FunctionalSystemID]];
                                if (!fs.m_dictSystemItems[m_type].ContainsKey(strSystemItemName))
                                {
                                    fs.m_dictSystemItems[m_type][strSystemItemName] = new ReportInfo.SystemItem(SystemItemID, strAlg/*, InputVoltageIndex*/, bEnable);
                                }
                            }
                        }
                    }
                }
                drSystemItem.Close();

                com.Parameters.Clear();
                com.CommandText = "SELECT si.FunctionalSystemID, si.SystemItemID, si.SystemItemName,  p.ParameterID, p.ParameterDescript, p.InspectionSubType, p.ParameterType, " +
                            "p.ParameterSelect1, p.ParameterSelect2, p.ParameterSelect3, p.ParameterSelect4, p.InspectionType, p.ParameterAlgorithm, p.DefectID " +
                            "FROM Parameters AS p " +
                            "LEFT JOIN SystemItems AS si ON si.SystemItemID = p.SystemItemID WHERE p.EquipmentKindID = @eq_kind " +
                            "ORDER BY si.SystemItemID, p.InspectionSubType, p.Sort";
                AddParam(com, "@eq_kind", DbType.Int64, m_EquipmentKindID);

                SQLiteDataReader drParam = com.ExecuteReader();
                if (drParam.HasRows)
                {
                    while (drParam.Read())
                    {
                        long ParameterID = Convert.ToInt64(drParam["ParameterID"]);

                        long? DefectID = null;
                        if (drParam["DefectID"] != DBNull.Value)
                            DefectID = Convert.ToInt64(drParam["DefectID"]);

                        string strParameterDescript = drParam["ParameterDescript"].ToString();
                        string strSystemItemName = "";
                        if (drParam["SystemItemName"] != DBNull.Value)
                            strSystemItemName = drParam["SystemItemName"].ToString();
                        long? FunctionalSystemID = null;
                        if (drParam["FunctionalSystemID"] != DBNull.Value)
                            FunctionalSystemID = Convert.ToInt64(drParam["FunctionalSystemID"]);
                        long? SystemItemID = null;
                        if (drParam["SystemItemID"] != DBNull.Value)
                            SystemItemID = Convert.ToInt64(drParam["SystemItemID"]);
                        long iParameterType = Convert.ToInt64(drParam["ParameterType"]);
                        string strParameterSelect1 = drParam["ParameterSelect1"].ToString();
                        string strParameterSelect2 = drParam["ParameterSelect2"].ToString();
                        string strParameterSelect3 = drParam["ParameterSelect3"].ToString();
                        string strParameterSelect4 = drParam["ParameterSelect4"].ToString();
                        long? iInspectionSubType = null;
                        if (drParam["InspectionSubType"] != DBNull.Value)
                            iInspectionSubType = Convert.ToInt64(drParam["InspectionSubType"]);

                        long iInspectionType = Convert.ToInt64(drParam["InspectionType"]);

                        string strParameterAlgorithm = drParam["ParameterAlgorithm"].ToString(); // если используется алгоритм calcifempty,то надо выводить в зависимости от зависимых полей

                        // запоминаем различные подтипы обследований, для дальнейшего вывода
                        if (!dictCommonSubTypes.ContainsKey((Inspection.InspectionType)iInspectionType))
                            dictCommonSubTypes[(Inspection.InspectionType)iInspectionType] = new List<long?>();
                        if (!dictCommonSubTypes[(Inspection.InspectionType)iInspectionType].Contains(iInspectionSubType))
                            dictCommonSubTypes[(Inspection.InspectionType)iInspectionType].Add(iInspectionSubType);

                        // ------------------------
                        // ограничения
                        // ------------------------
                        bool bEnable = true;
                        if (!bAllItems)
                        {
                            if (m_listEquipments[index].m_dictParameterExclude.ContainsKey(ParameterID)) continue;
                            //if (m_listEquipments[index].m_dictParameterExclude.ContainsKey(ParameterID) && m_listEquipments[index].m_dictParameterExclude[ParameterID].Count == 0) continue;
                        }
                        else
                        {
                            if (m_listEquipments[index].m_dictParameterExclude.ContainsKey(ParameterID)) bEnable = false;
                            //if (m_listEquipments[index].m_dictParameterExclude.ContainsKey(ParameterID) && m_listEquipments[index].m_dictParameterExclude[ParameterID].Count == 0) bEnable = false;
                        }

                        if (iParameterType != (long)Inspection.ParameterType.Number &&
                            iParameterType != (long)Inspection.ParameterType.Select &&
                            iParameterType != (long)Inspection.ParameterType.Integer) continue;
                        // ------------------------

                        //if (iParameterType == (long)Inspection.ParameterType.Number ||
                        //    iParameterType == (long)Inspection.ParameterType.Select)
                        {
                            if (FunctionalSystemID != null)
                            {
                                ReportInfo.FunctionalSystem fs = m_listEquipments[index].m_listFunctionalSystem[m_listEquipments[index].m_dictFunctionalSystemPos[(long)FunctionalSystemID]];
                                if (fs.m_dictSystemItems[(Inspection.InspectionType)iInspectionType].ContainsKey(strSystemItemName))
                                {
                                    /*if (m_listEquipments[index].m_dictParameterExclude.ContainsKey(ParameterID) && m_listEquipments[index].m_dictParameterExclude[ParameterID].Count > 0) // исключения по вводам обрабатываются отдельно
                                    {
                                        SystemItem si = fs.m_dictSystemItems[(Inspection.InspectionType)iInspectionType][strSystemItemName];

                                        if (si.m_InputVoltageIndex != null && dictInputVoltageIndexes[(Inspection.InspectionType)iInspectionType].ContainsKey((long)si.m_InputVoltageIndex))
                                        {
                                            KeyValuePair<long, long> val = new KeyValuePair<long, long>(4, dictInputVoltageIndexes[(Inspection.InspectionType)iInspectionType][(long)si.m_InputVoltageIndex]);

                                            if (m_listEquipments[index].m_dictParameterExclude[ParameterID].ContainsKey(val))
                                            {
                                                if (!bAllItems) continue;
                                                else bEnable = false;
                                            }
                                        }
                                    }*/

                                    fs.m_dictSystemItems[(Inspection.InspectionType)iInspectionType][strSystemItemName].m_listParameterResults.Add(new ReportInfo.ParameterResult(ParameterID, DefectID, strParameterDescript,
                                        iInspectionSubType, (Inspection.ParameterType)iParameterType, strParameterSelect1, strParameterSelect2, strParameterSelect3, strParameterSelect4, strParameterAlgorithm, bEnable));
                                }
                            }
                            else
                            {
                                for (int i = 0; i < m_listEquipments[index].m_listFunctionalSystem.Count; i++)
                                {
                                    foreach (KeyValuePair<string, ReportInfo.SystemItem> si in m_listEquipments[index].m_listFunctionalSystem[i].m_dictSystemItems[(Inspection.InspectionType)iInspectionType])
                                    {
                                        /*if (m_listEquipments[index].m_dictParameterExclude.ContainsKey(ParameterID) && m_listEquipments[index].m_dictParameterExclude[ParameterID].Count > 0) // исключения по вводам обрабатываются отдельно
                                        {
                                            if (si.Value.m_InputVoltageIndex != null && dictInputVoltageIndexes[(Inspection.InspectionType)iInspectionType].ContainsKey((long)si.Value.m_InputVoltageIndex))
                                            {
                                                KeyValuePair<long, long> val = new KeyValuePair<long, long>(4, dictInputVoltageIndexes[(Inspection.InspectionType)iInspectionType][(long)si.Value.m_InputVoltageIndex]);
                                                if (m_listEquipments[index].m_dictParameterExclude[ParameterID].ContainsKey(val))
                                                {
                                                    if (!bAllItems) continue;
                                                    else bEnable = false;
                                                }
                                            }
                                        }*/

                                        si.Value.m_listParameterResults.Add(new ReportInfo.ParameterResult(ParameterID, DefectID, strParameterDescript,
                                            iInspectionSubType, (Inspection.ParameterType)iParameterType, strParameterSelect1, strParameterSelect2, strParameterSelect3, strParameterSelect4, strParameterAlgorithm, bEnable));
                                    }
                                }
                            }
                        }
                    }
                }
                drParam.Close();

                // ------------

                com.Parameters.Clear();
                com.CommandText = "SELECT d.*, si.SystemItemName, si.FunctionalSystemID from Defects as d " +
                    " INNER JOIN SystemItems as si ON si.SystemItemID = d.SystemItemID WHERE d.EquipmentKindID = @eq_kind ORDER BY DefectName";
                AddParam(com, "@eq_kind", DbType.Int64, m_EquipmentKindID);
                
                SQLiteDataReader drDefect = com.ExecuteReader();
                if (drDefect.HasRows)
                {
                    while (drDefect.Read())
                    {
                        string strSystemItemName = "";
                        if (drDefect["SystemItemName"] != DBNull.Value)
                            strSystemItemName = drDefect["SystemItemName"].ToString();
                        long? FunctionalSystemID = null;
                        if (drDefect["FunctionalSystemID"] != DBNull.Value)
                            FunctionalSystemID = Convert.ToInt64(drDefect["FunctionalSystemID"]);

                        long SystemItemID = Convert.ToInt64(drDefect["SystemItemID"]);
                        long DefectID = Convert.ToInt64(drDefect["DefectID"]);
                        string strDefectName = drDefect["DefectName"].ToString();
                        long? iInspectionSubType = null;
                        if (drDefect["InspectionSubType"] != DBNull.Value)
                            iInspectionSubType = Convert.ToInt64(drDefect["InspectionSubType"]);
                        long iInspectionType = Convert.ToInt64(drDefect["InspectionType"]);

                        ReportInfo.FunctionalSystem fs = m_listEquipments[index].m_listFunctionalSystem[m_listEquipments[index].m_dictFunctionalSystemPos[(long)FunctionalSystemID]];
                        if (fs.m_dictSystemItems[(Inspection.InspectionType)iInspectionType].ContainsKey(strSystemItemName))
                        {
                            fs.m_dictSystemItems[(Inspection.InspectionType)iInspectionType][strSystemItemName].m_listDefectResults.Add(new ReportInfo.DefectResult(DefectID, strDefectName, iInspectionSubType));
                        }
                    }
                }
                drDefect.Close();

                // получение данных по обследованиям
                //foreach (Inspection.InspectionType m_type in Enum.GetValues(typeof(Inspection.InspectionType)))
                for (int ii = 0; ii < Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)m_EquipmentKindID].Count; ii++)
                {
                    Inspection.InspectionType m_type = Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)m_EquipmentKindID][ii];
                    //if (m_type != Inspection.InspectionType.Vibro) continue;

                    long? InspectionSubType = null;
                    long InspectionType = 0;

                    if (checkID <= 0) // это потом надо удалить, пока решается судьба протокола
                    {
                        com.CommandText = "SELECT InspectionID, InspectionType, InspectionSubType FROM Inspections WHERE InspectionType = @type AND " +
                            " EquipmentID = @eq_id AND InspectionDate <= current_timestamp ORDER BY InspectionDate DESC LIMIT 1";
                        com.Parameters.Clear();
                        AddParam(com, "@type", DbType.Int64, (long)m_type);
                        AddParam(com, "@eq_id", DbType.Int64, m_listEquipments[index].m_ID);
                    }
                    else
                    {
                        com.CommandText = "SELECT InspectionID, InspectionType, InspectionSubType FROM Inspections WHERE InspectionType = @type AND " +
                            " EquipmentID = @eq_id AND CheckID = @check_ID LIMIT 1";
                        com.Parameters.Clear();
                        AddParam(com, "@type", DbType.Int64, (long)m_type);
                        AddParam(com, "@eq_id", DbType.Int64, m_listEquipments[index].m_ID);
                        AddParam(com, "@check_ID", DbType.Int64, checkID);
                    }

                    SQLiteDataReader dr3 = com.ExecuteReader();
                    long InspectionID = 0;
                    if (dr3.HasRows)
                    {
                        while (dr3.Read())
                        {
                            InspectionID = Convert.ToInt64(dr3["InspectionID"]);
                            if (dr3["InspectionSubType"] != DBNull.Value)
                                InspectionSubType = Convert.ToInt64(dr3["InspectionSubType"]);
                            InspectionType = Convert.ToInt64(dr3["InspectionType"]);
                        }
                    }
                    dr3.Close();

                    if (InspectionID > 0)
                    {
                        for (int i = 0; i < m_listEquipments[index].m_listFunctionalSystem.Count; i++)
                        {
                            if (InspectionSubType != null)
                                m_listEquipments[index].m_listFunctionalSystem[i].m_dictSubTypes[m_type] = (long)InspectionSubType;

                            /*foreach (KeyValuePair<string, SystemItem> si in m_listFunctionalSystem[i].m_dictSystemItems[(Inspection.InspectionType)InspectionType])
                            {
                                si.Value.m_InspectionSubType = InspectionSubType;
                            }*/
                        }


                        com.CommandText = "SELECT SystemItemID, Result FROM SystemItemResults WHERE InspectionID = @id";

                        com.Parameters.Clear();
                        AddParam(com, "@id", DbType.Int64, InspectionID);

                        SQLiteDataReader dr4 = com.ExecuteReader();
                        if (dr4.HasRows)
                        {
                            while (dr4.Read())
                            {
                                long? iResult = null;
                                if (dr4["Result"] != DBNull.Value) iResult = Convert.ToInt64(dr4["Result"]);
                                long SystemItemID = Convert.ToInt64(dr4["SystemItemID"]);
                                //string strDefectName = dr4["DefectName"].ToString();

                                for (int i = 0; i < m_listEquipments[index].m_listFunctionalSystem.Count; i++)
                                {
                                    foreach (KeyValuePair<string, ReportInfo.SystemItem> si in m_listEquipments[index].m_listFunctionalSystem[i].m_dictSystemItems[(Inspection.InspectionType)InspectionType])
                                    {
                                        if (si.Value.m_SystemItemID == SystemItemID)
                                        {
                                            si.Value.m_Result = iResult;
                                        }
                                    }
                                }
                            }
                        }
                        dr4.Close();

                        com.CommandText = "SELECT InspectionDatas.Result, InspectionDatas.ValNumber, InspectionDatas.ValSelect, InspectionDatas.ValString, " +
                            "InspectionDatas.ParameterID, InspectionDatas.DefectID, InspectionDatas.SystemItemID, Parameters.ParameterDescript, Parameters.ParameterType " +
                            "FROM InspectionDatas LEFT JOIN Parameters ON InspectionDatas.ParameterID = Parameters.ParameterID WHERE InspectionDatas.InspectionID = @id ORDER BY InspectionDatas.SystemItemID";

                        com.Parameters.Clear();
                        AddParam(com, "@id", DbType.Int64, InspectionID);

                        SQLiteDataReader dr5 = com.ExecuteReader();
                        if (dr5.HasRows)
                        {
                            while (dr5.Read())
                            {
                                long? iResult = null;
                                if (dr5["Result"] != DBNull.Value) iResult = Convert.ToInt64(dr5["Result"]);
                                double? valNumber = null;
                                if (dr5["ValNumber"] != DBNull.Value) valNumber = Convert.ToInt64(dr5["ValNumber"]);
                                long? valSelect = null;
                                if (dr5["ValSelect"] != DBNull.Value) valSelect = Convert.ToInt64(dr5["ValSelect"]);
                                string valString = dr5["ValString"].ToString();
                                long SystemItemID = Convert.ToInt64(dr5["SystemItemID"]);
                                long? ParameterID = null;
                                if (dr5["ParameterID"] != DBNull.Value)
                                    ParameterID = Convert.ToInt64(dr5["ParameterID"]);
                                long? DefectID = null;
                                if (dr5["DefectID"] != DBNull.Value)
                                    DefectID = Convert.ToInt64(dr5["DefectID"]);

                                string strParameterDescript = "";
                                if (dr5["ParameterDescript"] != DBNull.Value)
                                    strParameterDescript = dr5["ParameterDescript"].ToString();
                                long iParameterType = 0;
                                if (dr5["ParameterType"] != DBNull.Value)
                                    iParameterType = Convert.ToInt64(dr5["ParameterType"]);

                                for (int i = 0; i < m_listEquipments[index].m_listFunctionalSystem.Count; i++)
                                {
                                    foreach (KeyValuePair<string, ReportInfo.SystemItem> si in m_listEquipments[index].m_listFunctionalSystem[i].m_dictSystemItems[(Inspection.InspectionType)InspectionType])
                                    {
                                        if (si.Value.m_SystemItemID == SystemItemID)
                                        {
                                            if (ParameterID != null)
                                            {
                                                for (int j = 0; j < si.Value.m_listParameterResults.Count; j++)
                                                {
                                                    if (si.Value.m_listParameterResults[j].m_ParameterID == ParameterID)
                                                    {
                                                        if (si.Value.m_listParameterResults[j].m_ParameterType == Inspection.ParameterType.Select ||
                                                            si.Value.m_listParameterResults[j].m_ParameterType == Inspection.ParameterType.Number ||
                                                            si.Value.m_listParameterResults[j].m_ParameterType == Inspection.ParameterType.Integer)
                                                        {
                                                            si.Value.m_listParameterResults[j].m_Result = iResult;
                                                            si.Value.m_listParameterResults[j].m_valNumber = valNumber;
                                                            si.Value.m_listParameterResults[j].m_valSelect = valSelect;
                                                        }
                                                    }
                                                }

                                                if (iParameterType == (long)Inspection.ParameterType.String &&
                                                   strParameterDescript == "Примечание")
                                                {
                                                    si.Value.m_strNote = valString;
                                                }
                                            }
                                            if (DefectID != null)
                                            {
                                                for (int j = 0; j < si.Value.m_listDefectResults.Count; j++)
                                                {
                                                    if (si.Value.m_listDefectResults[j].m_DefectID == DefectID)
                                                    {
                                                        si.Value.m_listDefectResults[j].m_Result = iResult;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        dr5.Close();
                    }
                }

                m_con.Close();

                // ---------------------
                // - исключаем из параметров зависимые по алгоритму параметры (если у зависимого параметра есть значение, то текущий параметр исключается)
                //foreach (Inspection.InspectionType m_type in Enum.GetValues(typeof(Inspection.InspectionType)))
                for (int ii = 0; ii < Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)m_EquipmentKindID].Count; ii++)
                {
                    Inspection.InspectionType m_type = Inspection.m_listEquipmentInspections[(DiarMain.Equipment.EquipmentKind)m_EquipmentKindID][ii];

                    for (int i = 0; i < m_listEquipments[index].m_listFunctionalSystem.Count; i++)
                    {
                        foreach (KeyValuePair<string, ReportInfo.SystemItem> si in m_listEquipments[index].m_listFunctionalSystem[i].m_dictSystemItems[m_type])
                        {
                            for (int j = 0; j < si.Value.m_listParameterResults.Count; j++)
                            {
                                if (si.Value.m_listParameterResults[j].m_arrParameterAlgorithmIDs.Count > 0)
                                {
                                    bool bFind = false;
                                    for (int k = 0; k < si.Value.m_listParameterResults[j].m_arrParameterAlgorithmIDs.Count; k++)
                                    {
                                        long ParameterID = si.Value.m_listParameterResults[j].m_arrParameterAlgorithmIDs[k];
                                        // ищем значение этого параметра в этой же подсистеме, но вообще надо искать везде
                                        for (int j2 = 0; j2 < si.Value.m_listParameterResults.Count; j2++)
                                        {
                                            if (ParameterID == si.Value.m_listParameterResults[j2].m_ParameterID
                                                && (si.Value.m_listParameterResults[j2].m_valNumber != null ||
                                                si.Value.m_listParameterResults[j2].m_valSelect != null))
                                            {
                                                si.Value.m_listParameterResults[j].m_bEnable = false;
                                                bFind = true;
                                                break;
                                            }
                                        }

                                        if (bFind) break;
                                    }
                                }
                            }
                        }
                    }
                }
                // ---------------------
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}

