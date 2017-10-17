using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;

namespace DiarMain
{
    public class ImportData
    {
        // АЛГОРИТМ загрузки данных из другой базы
        // 1. получаем все данные по всем оборудованиям из загружаемой базы
        // 2. бегаем по списку полученного оборудования
        // 3. если в основной базе есть оборудование с таким же ключевыми полями, то шаг 5, иначе шаг 4
        // 4. проверям наличие основных полей оборудования, если чего то не хватает - добавляем в таблицу, добавляем оборудование, переходим на шаг 6
        // 5. проверяем, есть ли поля, которые поменялись, если ничего не менялось, переходим к шагу 6, иначе проверям наличие основных полей оборудования, если чего то не хватает - добавляем в таблицу, изменяем оборудование
        // 6. проверки: если оборудование существует в базе, но у него поменялись поля, которые не должны меняться, выводится ошибка
        // 7. бегаем по списку обследований
        // 8. если у обследования нет объекта проверка, тогда переходим на шаг 10, иначе ищем проверку по ключевым полям
        // 9. если проверка не найдена, то проверяется пересечение с существующими проверками по датам в той же подстанции, если такие проверки найдены - то это ошибка и такое обследование не заносится в базу
        // 10. если все нормально, и нет обследования по ключевым полям, то оно добавляется, иначе все данные обследования заменяются на данные из загружаемой базы
        public enum MsgState
        {
            Error = 0,
            Success = 1,
            Info = 2,
            Warning = 3
        }

        public class CheckInfo
        {
            public long m_iCheckID;
            public DateTime m_DateBegin;
            public DateTime m_DateEnd;
            public string m_strSubstation;
            public string m_strBranch;
            public string m_strSubject;
        }


        public class EquipmentInfo
        {
            public EquipmentInfo()
            {
                m_listInspections = new Dictionary<long, InspectionInfo>();
                m_Input = new Dictionary<int, InputInfo>();
            }

            public class InspectionInfo
            {
                public InspectionInfo()
                {
                    m_listDatas = new List<InspectionDataInfo>();
                    m_listFunctionalSystemResults = new List<FunctionalSystemResultInfo>();
                    m_listSystemItemResults = new List<SystemItemResultInfo>();
                }

                public class CheckInfo
                {
                    public DateTime m_dtBegin; // ключевое поле
                    public DateTime m_dtEnd; // ключевое поле
                }

                public class InspectionDataInfo
                {
                    public long m_iSystemItemID; // ключевое поле
                    public long? m_iParameterID; // ключевое поле
                    public long? m_iDefectID; // ключевое поле
                    public Decimal? m_fValNumber;
                    public long? m_iValSelect;
                    public string m_strValString;
                    public DateTime? m_dtValDate;
                    public long m_iResult;
                }

                public class FunctionalSystemResultInfo
                {
                    public long m_iFunctionalSystemID;
                    public long m_iResult;
                }

                public class SystemItemResultInfo
                {
                    public long m_iSystemItemID;
                    public long m_iResult;
                }

                public long m_iType; // ключевое поле
                public long? m_iSubType;
                public DateTime m_dtDate; // ключевое поле
                public DateTime m_dtCreateDate;
                public string m_strExecutor;
                public string m_strDelegator;
                public CheckInfo m_Check; // ключевое поле
                public List<InspectionDataInfo> m_listDatas;
                public List<FunctionalSystemResultInfo> m_listFunctionalSystemResults;
                public List<SystemItemResultInfo> m_listSystemItemResults;
            }

            public class RPNInfo
            {
                public long? m_iRPNCnt;
                public long? m_iRPNKind;
                public long? m_iRPNVoltage;
                public string m_strRPNTypeName;
                public string m_strRPNNumber;
                public string m_strRPNNumber2;
                public string m_strRPNNumber3;
            }

            public class InputInfo
            {
                public long? m_iInputID;
                public string m_strName;
                public string m_strType;
                public string m_strNumber;
                public long? m_iKind;
                public string m_strManufacturerName;
                public long? m_iCreateYear;
                public long? m_iUseBeginYear;
            }

            public long? m_iEquipmentIDForImport = null; // поле, нужное для импортируемых данных, чтобы связать их с данными в текущей базе
            public string   m_strEquipmentName; // ключевое поле
            public string m_strEquipmentNumber; // ключевое поле
            public long     m_iEquipmentClassID; // должно совпадать
            public long m_iEquipmentKindID; // должно совпадать
            public string m_strSubstation; // должно совпадать
            public string m_strBranch; // должно совпадать
            public string m_strSubject; // должно совпадать
            public string   m_strEquipmentTypeName;
            public string   m_strManufacturerName;
            public long?    m_iConstructionType;
            public long?    m_iCoolingSystemID;
            public long?    m_iProtectionOilTypeID;
            public long?    m_iCreateYear;
            public long?    m_iUseBeginYear;
            public long?    m_iNominalVoltageLow;
            public long?    m_iNominalVoltageMiddle;
            public long?    m_iNominalVoltageHigh;
            public long?    m_iNominalVoltageNeitral;
            public long?    m_iNominalCurrent;
            public Decimal?    m_iNominalPower;
            public Dictionary<int, InputInfo> m_Input;
            public RPNInfo  m_RPN;
            public string   m_strSwitchDriveTypeName;
            public object   m_Image;

            public Dictionary<long, InspectionInfo> m_listInspections;
        }

        //public Dictionary<long, EquipmentInfo> m_list = new Dictionary<long,EquipmentInfo>();

        public static KeyValuePair<MsgState, string> LoadData(string strDBConnectionString, ref Dictionary<long, EquipmentInfo> m_list, 
            ref List<ImportData.CheckInfo> m_listChecks, ref Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefs)
        {
            SQLiteConnection con = new SQLiteConnection(strDBConnectionString);
            m_list.Clear();
            try
            {
                con.Open();

                SQLiteCommand com = new SQLiteCommand(con);
                com.CommandType = CommandType.Text;

                com.CommandText = "Select CurVersion from Version";
                SQLiteDataReader drVersion = com.ExecuteReader();

                Decimal curVersion = new decimal(0);
                while (drVersion.Read())
                {
                    curVersion = Convert.ToDecimal(drVersion.GetValue(0));
                }
                drVersion.Close();

                if (curVersion != Program.m_strVersion)
                {
                    //MyLocalizer.XtraMessageBoxShow("Версия загружаемой базы данных не совпадает с версией текущей базы данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new KeyValuePair<MsgState, string>(MsgState.Error, "Версия загружаемой базы данных не совпадает с версией текущей базы данных.");
                }

                //m_list = new Dictionary<long, EquipmentInfo>();

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
                    "LEFT JOIN InputVoltageTypes as input_t_N ON input_t_N.InputVoltageTypeID = iNN.InputTypeID ";

                SQLiteDataReader drEquipment = com.ExecuteReader();
                if (drEquipment.HasRows)
                {
                    while (drEquipment.Read())
                    {
                        EquipmentInfo ei = new EquipmentInfo();
                        long iID = Convert.ToInt64(drEquipment["EquipmentID"]);
                        ei.m_strEquipmentName = drEquipment["EquipmentName"].ToString();
                        ei.m_strEquipmentNumber = drEquipment["EquipmentNumber"].ToString();
                        ei.m_iEquipmentClassID = Convert.ToInt64(drEquipment["EquipmentClassID"]);
                        ei.m_iEquipmentKindID = Convert.ToInt64(drEquipment["EquipmentKindID"]);
                        ei.m_strSubstation = drEquipment["SubstationName"].ToString();
                        ei.m_strBranch = drEquipment["BranchName"].ToString();
                        ei.m_strSubject = drEquipment["SubjectName"].ToString();
                        if (drEquipment["EquipmentTypeName"] != DBNull.Value) ei.m_strEquipmentTypeName = drEquipment["EquipmentTypeName"].ToString();
                        else ei.m_strEquipmentTypeName = null;
                        if (drEquipment["ManufacturerName"] != DBNull.Value) ei.m_strManufacturerName = drEquipment["ManufacturerName"].ToString();
                        else ei.m_strManufacturerName = null;
                        if (drEquipment["SwitchDriveTypeName"] != DBNull.Value) ei.m_strSwitchDriveTypeName = drEquipment["SwitchDriveTypeName"].ToString();
                        else ei.m_strSwitchDriveTypeName = null;
                        if (drEquipment["ConstructionType"] != DBNull.Value) ei.m_iConstructionType = Convert.ToInt64(drEquipment["ConstructionType"]);
                        else ei.m_iConstructionType = null;
                        if (drEquipment["CoolingSystemTypeID"] != DBNull.Value) ei.m_iCoolingSystemID = Convert.ToInt64(drEquipment["CoolingSystemTypeID"]);
                        else ei.m_iCoolingSystemID = null;
                        if (drEquipment["ProtectionOilTypeID"] != DBNull.Value) ei.m_iProtectionOilTypeID = Convert.ToInt64(drEquipment["ProtectionOilTypeID"]);
                        else ei.m_iProtectionOilTypeID = null;
                        if (drEquipment["CreateYear"] != DBNull.Value) ei.m_iCreateYear = Convert.ToInt64(drEquipment["CreateYear"]);
                        else ei.m_iCreateYear = null;
                        if (drEquipment["UseBeginYear"] != DBNull.Value) ei.m_iUseBeginYear = Convert.ToInt64(drEquipment["UseBeginYear"]);
                        else ei.m_iUseBeginYear = null;
                        if (drEquipment["NominalVoltageLow"] != DBNull.Value) ei.m_iNominalVoltageLow = Convert.ToInt64(drEquipment["NominalVoltageLow"]);
                        else ei.m_iNominalVoltageLow = null;
                        if (drEquipment["NominalVoltageMiddle"] != DBNull.Value) ei.m_iNominalVoltageMiddle = Convert.ToInt64(drEquipment["NominalVoltageMiddle"]);
                        else ei.m_iNominalVoltageMiddle = null;
                        if (drEquipment["NominalVoltageHigh"] != DBNull.Value) ei.m_iNominalVoltageHigh = Convert.ToInt64(drEquipment["NominalVoltageHigh"]);
                        else ei.m_iNominalVoltageHigh = null;
                        if (drEquipment["NominalVoltageNeitral"] != DBNull.Value) ei.m_iNominalVoltageNeitral = Convert.ToInt64(drEquipment["NominalVoltageNeitral"]);
                        else ei.m_iNominalVoltageNeitral = null;
                        if (drEquipment["NominalCurrent"] != DBNull.Value) ei.m_iNominalCurrent = Convert.ToInt64(drEquipment["NominalCurrent"]);
                        else ei.m_iNominalCurrent = null;
                        if (drEquipment["NominalPower"] != DBNull.Value) ei.m_iNominalPower = Convert.ToInt64(drEquipment["NominalPower"]);
                        else ei.m_iNominalPower = null;

                        if (drEquipment["Image"] != DBNull.Value) ei.m_Image = drEquipment["Image"];
                        else ei.m_Image = null;

                        EquipmentInfo.RPNInfo rpn = new EquipmentInfo.RPNInfo();
                        if (drEquipment["RPNKind"] != DBNull.Value) rpn.m_iRPNKind = Convert.ToInt64(drEquipment["RPNKind"]);
                        else rpn.m_iRPNKind = null;
                        if (drEquipment["RPNCnt"] != DBNull.Value) rpn.m_iRPNCnt = Convert.ToInt64(drEquipment["RPNCnt"]);
                        else rpn.m_iRPNCnt = null;
                        if (drEquipment["RPNVoltage"] != DBNull.Value) rpn.m_iRPNVoltage = Convert.ToInt64(drEquipment["RPNVoltage"]);
                        else rpn.m_iRPNVoltage = null;
                        if (drEquipment["RPNTypeName"] != DBNull.Value) rpn.m_strRPNTypeName = drEquipment["RPNTypeName"].ToString();
                        else rpn.m_strRPNTypeName = null;
                        if (drEquipment["RPNNumber"] != DBNull.Value) rpn.m_strRPNNumber = drEquipment["RPNNumber"].ToString();
                        else rpn.m_strRPNNumber = null;
                        if (drEquipment["RPNNumber2"] != DBNull.Value) rpn.m_strRPNNumber2 = drEquipment["RPNNumber2"].ToString();
                        else rpn.m_strRPNNumber2 = null;
                        if (drEquipment["RPNNumber3"] != DBNull.Value) rpn.m_strRPNNumber3 = drEquipment["RPNNumber3"].ToString();
                        else rpn.m_strRPNNumber3 = null;

                        ei.m_RPN = rpn;

                        for (int i = 0; i < 7; i++)
                        {
                            string strName = "";
                            switch (i)
                            {
                                case 0:
                                    strName = "HighA";
                                    break;
                                case 1:
                                    strName = "HighB";
                                    break;
                                case 2:
                                    strName = "HighC";
                                    break;
                                case 3:
                                    strName = "MiddleA";
                                    break;
                                case 4:
                                    strName = "MiddleB";
                                    break;
                                case 5:
                                    strName = "MiddleC";
                                    break;
                                case 6:
                                    strName = "Neutral";
                                    break;
                            }

                            EquipmentInfo.InputInfo ii = new EquipmentInfo.InputInfo();
                            if (drEquipment["InputKind" + strName] != DBNull.Value) ii.m_iKind = Convert.ToInt64(drEquipment["InputKind" + strName]);
                            else ii.m_iKind = null;
                            if (ii.m_iKind != null)
                            {
                                if (drEquipment["InputID" + strName] != DBNull.Value) ii.m_iInputID = Convert.ToInt64(drEquipment["InputID" + strName]);
                                else ii.m_iInputID = null;
                                if (drEquipment["InputType" + strName] != DBNull.Value) ii.m_strType = drEquipment["InputType" + strName].ToString();
                                else ii.m_strType = null;
                                if (drEquipment["InputName" + strName] != DBNull.Value) ii.m_strName = drEquipment["InputName" + strName].ToString();
                                else ii.m_strName = null;
                                if (drEquipment["InputNumber" + strName] != DBNull.Value) ii.m_strNumber = drEquipment["InputNumber" + strName].ToString();
                                else ii.m_strNumber = null;
                                if (drEquipment["InputManufacturerName" + strName] != DBNull.Value) ii.m_strManufacturerName = drEquipment["InputManufacturerName" + strName].ToString();
                                else ii.m_strManufacturerName = null;
                                if (drEquipment["InputCreateYear" + strName] != DBNull.Value) ii.m_iCreateYear = Convert.ToInt64(drEquipment["InputCreateYear" + strName]);
                                else ii.m_iCreateYear = null;
                                if (drEquipment["InputUseBeginYear" + strName] != DBNull.Value) ii.m_iUseBeginYear = Convert.ToInt64(drEquipment["InputUseBeginYear" + strName]);
                                else ii.m_iUseBeginYear = null;

                                ei.m_Input[i] = ii;
                            }
                            else ei.m_Input[i] = null;
                        }

                        m_list[iID] = ei;
                    }
                }
                drEquipment.Close();

                com.CommandText = "SELECT i.EquipmentID, i.InspectionID, i.InspectionType, i.InspectionSubType, i.InspectionDate, i.CreateDate, i.Executor, i.Delegator, c.CheckID, c.CheckDateBegin, c.CheckDateEnd " +
                    "FROM Inspections AS i " +
                    "LEFT JOIN Checks AS c ON c.CheckID = i.CheckID";

                SQLiteDataReader drInspection = com.ExecuteReader();
                if (drInspection.HasRows)
                {
                    while (drInspection.Read())
                    {
                        long EquipmentID = Convert.ToInt64(drInspection["EquipmentID"]);
                        if (m_list.ContainsKey(EquipmentID))
                        {
                            EquipmentInfo.InspectionInfo ii = new EquipmentInfo.InspectionInfo();

                            long iID = Convert.ToInt64(drInspection["InspectionID"]);
                            ii.m_iType = Convert.ToInt64(drInspection["InspectionType"]);
                            if (drInspection["InspectionSubType"] != DBNull.Value) ii.m_iSubType = Convert.ToInt64(drInspection["InspectionSubType"]);
                            else ii.m_iSubType = null;
                            ii.m_dtDate = Convert.ToDateTime(drInspection["InspectionDate"]);
                            ii.m_dtCreateDate = Convert.ToDateTime(drInspection["CreateDate"]);
                            ii.m_strDelegator = drInspection["Delegator"].ToString();
                            ii.m_strExecutor = drInspection["Executor"].ToString();

                            if (drInspection["CheckID"] != DBNull.Value)
                            {
                                EquipmentInfo.InspectionInfo.CheckInfo ci = new EquipmentInfo.InspectionInfo.CheckInfo();
                                ci.m_dtBegin = Convert.ToDateTime(drInspection["CheckDateBegin"]);
                                ci.m_dtEnd = Convert.ToDateTime(drInspection["CheckDateEnd"]);
                                ii.m_Check = ci;
                            }
                            else ii.m_Check = null;


                            m_list[EquipmentID].m_listInspections[iID] = ii;
                        }
                    }
                }
                drInspection.Close();

                com.CommandText = "SELECT i.EquipmentID, id.InspectionID, id.SystemItemID, id.ParameterID, id.DefectID, id.ValNumber, id.ValSelect, id.ValString, id.ValDate, id.Result " +
                    "FROM InspectionDatas AS id " +
                    "INNER JOIN Inspections AS i ON id.InspectionID = i.InspectionID ";

                SQLiteDataReader drData = com.ExecuteReader();
                if (drData.HasRows)
                {
                    while (drData.Read())
                    {
                        long EquipmentID = Convert.ToInt64(drData["EquipmentID"]);
                        if (m_list.ContainsKey(EquipmentID))
                        {
                            long InspectionID = Convert.ToInt64(drData["InspectionID"]);

                            if (m_list[EquipmentID].m_listInspections.ContainsKey(InspectionID))
                            {
                                EquipmentInfo.InspectionInfo.InspectionDataInfo id = new EquipmentInfo.InspectionInfo.InspectionDataInfo();

                                id.m_iSystemItemID = Convert.ToInt64(drData["SystemItemID"]);
                                if (drData["ParameterID"] != DBNull.Value) id.m_iParameterID = Convert.ToInt64(drData["ParameterID"]);
                                else id.m_iParameterID = null;
                                if (drData["DefectID"] != DBNull.Value) id.m_iDefectID = Convert.ToInt64(drData["DefectID"]);
                                else id.m_iDefectID = null;
                                if (drData["ValNumber"] != DBNull.Value) id.m_fValNumber = Convert.ToDecimal(drData["ValNumber"]);
                                else id.m_fValNumber = null;
                                if (drData["ValSelect"] != DBNull.Value) id.m_iValSelect = Convert.ToInt64(drData["ValSelect"]);
                                else id.m_iValSelect = null;
                                if (drData["ValString"] != DBNull.Value) id.m_strValString = Convert.ToString(drData["ValString"]);
                                else id.m_strValString = null;
                                if (drData["ValDate"] != DBNull.Value) id.m_dtValDate = Convert.ToDateTime(drData["ValDate"]);
                                else id.m_dtValDate = null;
                                id.m_iResult = Convert.ToInt64(drData["Result"]);

                                m_list[EquipmentID].m_listInspections[InspectionID].m_listDatas.Add(id);
                            }
                        }
                    }
                }
                drData.Close();

                com.CommandText = "SELECT fr.InspectionID, i.EquipmentID, fr.FunctionalSystemID, fr.Result " +
                    "FROM FunctionalSystemResults AS fr " + 
                    "INNER JOIN Inspections AS i ON fr.InspectionID = i.InspectionID ";
                
                SQLiteDataReader drFR = com.ExecuteReader();
                if (drFR.HasRows)
                {
                    while (drFR.Read())
                    {
                        long EquipmentID = Convert.ToInt64(drFR["EquipmentID"]);
                        if (m_list.ContainsKey(EquipmentID))
                        {
                            long InspectionID = Convert.ToInt64(drFR["InspectionID"]);

                            if (m_list[EquipmentID].m_listInspections.ContainsKey(InspectionID))
                            {
                                EquipmentInfo.InspectionInfo.FunctionalSystemResultInfo fr = new EquipmentInfo.InspectionInfo.FunctionalSystemResultInfo();
                                fr.m_iFunctionalSystemID = Convert.ToInt64(drFR["FunctionalSystemID"]);
                                fr.m_iResult = Convert.ToInt64(drFR["Result"]);

                                m_list[EquipmentID].m_listInspections[InspectionID].m_listFunctionalSystemResults.Add(fr);
                            }
                        }
                    }
                }
                drFR.Close();

                com.CommandText = "SELECT sr.InspectionID, i.EquipmentID, sr.SystemItemID, sr.Result " +
                    "FROM SystemItemResults AS sr " +
                    "INNER JOIN Inspections AS i ON sr.InspectionID = i.InspectionID ";

                SQLiteDataReader drSR = com.ExecuteReader();
                if (drSR.HasRows)
                {
                    while (drSR.Read())
                    {
                        long EquipmentID = Convert.ToInt64(drSR["EquipmentID"]);
                        if (m_list.ContainsKey(EquipmentID))
                        {
                            long InspectionID = Convert.ToInt64(drSR["InspectionID"]);

                            if (m_list[EquipmentID].m_listInspections.ContainsKey(InspectionID))
                            {
                                EquipmentInfo.InspectionInfo.SystemItemResultInfo sr = new EquipmentInfo.InspectionInfo.SystemItemResultInfo();
                                sr.m_iSystemItemID = Convert.ToInt64(drSR["SystemItemID"]);
                                sr.m_iResult = Convert.ToInt64(drSR["Result"]);

                                m_list[EquipmentID].m_listInspections[InspectionID].m_listSystemItemResults.Add(sr);
                            }
                        }
                    }
                }
                drSR.Close();

                if (m_listChecks != null)
                {
                    com.CommandText = "SELECT c.*, Substations.SubstationName, Branches.BranchName, Subjects.SubjectName FROM Checks AS c " +
                        "LEFT JOIN Substations ON Substations.SubstationID = c.SubstationID " +
                        "LEFT JOIN Branches ON Branches.BranchID = Substations.BranchID " +
                        "LEFT JOIN Subjects ON Subjects.SubjectID = Branches.SubjectID ";
                    SQLiteDataReader drCheck = com.ExecuteReader();
                    if (drCheck.HasRows)
                    {
                        while (drCheck.Read())
                        {
                            ImportData.CheckInfo ci = new CheckInfo();
                            ci.m_iCheckID = Convert.ToInt64(drCheck["CheckID"]);
                            ci.m_strBranch = drCheck["BranchName"].ToString();
                            ci.m_strSubject = drCheck["SubjectName"].ToString();
                            ci.m_strSubstation = drCheck["SubstationName"].ToString();
                            ci.m_DateBegin = Convert.ToDateTime(drCheck["CheckDateBegin"]);
                            ci.m_DateEnd = Convert.ToDateTime(drCheck["CheckDateEnd"]);

                            m_listChecks.Add(ci);
                        }
                    }
                    drCheck.Close();
                }

                if (m_dictRefs != null)
                {
                    // загружаем справочники Subjects, Branches, Substations, EquipmentTypes, InputVoltageTypes, Manufacturers, ManufacturersInputs, RPNTypes, SwitchDriveTypes

                    com.CommandText = "SELECT * FROM EquipmentTypes";
                    SQLiteDataReader drRef1 = com.ExecuteReader();
                    m_dictRefs["EquipmentTypes"] = new Dictionary<KeyValuePair<long, string>, long>();
                    if (drRef1.HasRows)
                    {
                        while (drRef1.Read())
                        {
                            m_dictRefs["EquipmentTypes"][new KeyValuePair<long, string>(Convert.ToInt64(drRef1["EquipmentKindID"]), drRef1["EquipmentTypeName"].ToString())] = Convert.ToInt64(drRef1["EquipmentTypeID"]);
                        }
                    }
                    drRef1.Close();

                    com.CommandText = "SELECT * FROM InputVoltageTypes";
                    SQLiteDataReader drRef2 = com.ExecuteReader();
                    m_dictRefs["InputVoltageTypes"] = new Dictionary<KeyValuePair<long, string>, long>();
                    if (drRef2.HasRows)
                    {
                        while (drRef2.Read())
                        {
                            m_dictRefs["InputVoltageTypes"][new KeyValuePair<long, string>(Convert.ToInt64(drRef2["EquipmentKindID"]), drRef2["InputVoltageTypeName"].ToString())] = Convert.ToInt64(drRef2["InputVoltageTypeID"]);
                        }
                    }
                    drRef2.Close();

                    com.CommandText = "SELECT * FROM Manufacturers";
                    SQLiteDataReader drRef3 = com.ExecuteReader();
                    m_dictRefs["Manufacturers"] = new Dictionary<KeyValuePair<long, string>, long>();
                    if (drRef3.HasRows)
                    {
                        while (drRef3.Read())
                        {
                            m_dictRefs["Manufacturers"][new KeyValuePair<long, string>(Convert.ToInt64(drRef3["EquipmentKindID"]), drRef3["ManufacturerName"].ToString())] = Convert.ToInt64(drRef3["ManufacturerID"]);
                        }
                    }
                    drRef3.Close();

                    com.CommandText = "SELECT * FROM ManufacturersInputs";
                    SQLiteDataReader drRef4 = com.ExecuteReader();
                    m_dictRefs["ManufacturersInputs"] = new Dictionary<KeyValuePair<long, string>, long>();
                    if (drRef4.HasRows)
                    {
                        while (drRef4.Read())
                        {
                            m_dictRefs["ManufacturersInputs"][new KeyValuePair<long, string>(Convert.ToInt64(drRef4["EquipmentKindID"]), drRef4["ManufacturerInputName"].ToString())] = Convert.ToInt64(drRef4["ManufacturerInputID"]);
                        }
                    }
                    drRef4.Close();

                    com.CommandText = "SELECT * FROM RPNTypes";
                    SQLiteDataReader drRef5 = com.ExecuteReader();
                    m_dictRefs["RPNTypes"] = new Dictionary<KeyValuePair<long, string>, long>();
                    if (drRef5.HasRows)
                    {
                        while (drRef5.Read())
                        {
                            m_dictRefs["RPNTypes"][new KeyValuePair<long, string>(Convert.ToInt64(drRef5["EquipmentKindID"]), drRef5["RPNTypeName"].ToString())] = Convert.ToInt64(drRef5["RPNTypeID"]);
                        }
                    }
                    drRef5.Close();

                    com.CommandText = "SELECT * FROM SwitchDriveTypes";
                    SQLiteDataReader drRef6 = com.ExecuteReader();
                    m_dictRefs["SwitchDriveTypes"] = new Dictionary<KeyValuePair<long, string>, long>();
                    if (drRef6.HasRows)
                    {
                        while (drRef6.Read())
                        {
                            m_dictRefs["SwitchDriveTypes"][new KeyValuePair<long, string>(Convert.ToInt64(drRef6["EquipmentKindID"]), drRef6["SwitchDriveTypeName"].ToString())] = Convert.ToInt64(drRef6["SwitchDriveTypeID"]);
                        }
                    }
                    drRef6.Close();

                    com.CommandText = "SELECT * FROM Subjects";
                    SQLiteDataReader drRef7 = com.ExecuteReader();
                    m_dictRefs["Subjects"] = new Dictionary<KeyValuePair<long, string>, long>();
                    if (drRef7.HasRows)
                    {
                        while (drRef7.Read())
                        {
                            m_dictRefs["Subjects"][new KeyValuePair<long, string>(0, drRef7["SubjectName"].ToString())] = Convert.ToInt64(drRef7["SubjectID"]);
                        }
                    }
                    drRef7.Close();

                    com.CommandText = "SELECT * FROM Branches";
                    SQLiteDataReader drRef8 = com.ExecuteReader();
                    m_dictRefs["Branches"] = new Dictionary<KeyValuePair<long, string>, long>();
                    if (drRef8.HasRows)
                    {
                        while (drRef8.Read())
                        {
                            m_dictRefs["Branches"][new KeyValuePair<long, string>(Convert.ToInt64(drRef8["SubjectID"]), drRef8["BranchName"].ToString())] = Convert.ToInt64(drRef8["BranchID"]);
                        }
                    }
                    drRef8.Close();

                    com.CommandText = "SELECT * FROM Substations";
                    SQLiteDataReader drRef9 = com.ExecuteReader();
                    m_dictRefs["Substations"] = new Dictionary<KeyValuePair<long, string>, long>();
                    if (drRef9.HasRows)
                    {
                        while (drRef9.Read())
                        {
                            m_dictRefs["Substations"][new KeyValuePair<long, string>(Convert.ToInt64(drRef9["BranchID"]), drRef9["SubstationName"].ToString())] = Convert.ToInt64(drRef9["SubstationID"]);
                        }
                    }
                    drRef9.Close();
                }

                con.Close();
            }
            catch (SQLiteException ex)
            {
                //MyLocalizer.XtraMessageBoxShow("Не удалось подключиться к базе данных. Описание: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                return new KeyValuePair<MsgState, string>(MsgState.Error, "Ошибка при работе с базой данных: " + ex.Message);
            }
            catch (Exception ex)
            {
                //MyLocalizer.XtraMessageBoxShow("Не удалось подключиться к базе данных. Описание: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                return new KeyValuePair<MsgState, string>(MsgState.Error, "Ошибка: " + ex.Message);
            }

            return new KeyValuePair<MsgState, string>(MsgState.Success, "OK");
        }

        public static void CompareEquipments(EquipmentInfo eq1, EquipmentInfo eq2, ref bool bDiffParam, ref bool bDiffInputParam)
        {
            bDiffParam = false;
            bDiffInputParam = false;

            if (eq1.m_iConstructionType != eq2.m_iConstructionType) bDiffParam = true;
            if (eq1.m_iCoolingSystemID != eq2.m_iCoolingSystemID) bDiffParam = true;
            if (eq1.m_iCreateYear != eq2.m_iCreateYear) bDiffParam = true;
            if (eq1.m_iNominalCurrent != eq2.m_iNominalCurrent) bDiffParam = true;
            if (eq1.m_iNominalPower != eq2.m_iNominalPower) bDiffParam = true;
            if (eq1.m_iNominalVoltageHigh != eq2.m_iNominalVoltageHigh) bDiffParam = true;
            if (eq1.m_iNominalVoltageLow != eq2.m_iNominalVoltageLow) bDiffParam = true;
            if (eq1.m_iNominalVoltageMiddle != eq2.m_iNominalVoltageMiddle) bDiffParam = true;
            if (eq1.m_iNominalVoltageNeitral != eq2.m_iNominalVoltageNeitral) bDiffParam = true;
            if (eq1.m_iProtectionOilTypeID != eq2.m_iProtectionOilTypeID) bDiffParam = true;
            if (eq1.m_iUseBeginYear != eq2.m_iUseBeginYear) bDiffParam = true;
            if (eq1.m_RPN.m_iRPNCnt != eq2.m_RPN.m_iRPNCnt) bDiffParam = true;
            if (eq1.m_RPN.m_iRPNKind != eq2.m_RPN.m_iRPNKind) bDiffParam = true;
            if (eq1.m_RPN.m_iRPNVoltage != eq2.m_RPN.m_iRPNVoltage) bDiffParam = true;
            if (eq1.m_RPN.m_strRPNNumber != eq2.m_RPN.m_strRPNNumber) bDiffParam = true;
            if (eq1.m_RPN.m_strRPNNumber2 != eq2.m_RPN.m_strRPNNumber2) bDiffParam = true;
            if (eq1.m_RPN.m_strRPNNumber3 != eq2.m_RPN.m_strRPNNumber3) bDiffParam = true;
            if (eq1.m_RPN.m_strRPNTypeName != eq2.m_RPN.m_strRPNTypeName) bDiffParam = true;
            if (eq1.m_strEquipmentTypeName != eq2.m_strEquipmentTypeName) bDiffParam = true;
            if (eq1.m_iEquipmentClassID != eq2.m_iEquipmentClassID) bDiffParam = true;
            if (eq1.m_strManufacturerName != eq2.m_strManufacturerName) bDiffParam = true;
            if (eq1.m_strSwitchDriveTypeName != eq2.m_strSwitchDriveTypeName) bDiffParam = true;
            if (eq1.m_Image == null && eq2.m_Image != null || eq1.m_Image != null && eq2.m_Image == null) bDiffParam = true;
            if (eq1.m_Image != null && eq2.m_Image != null)
            {
                Byte[] bytes1 = (Byte[])eq1.m_Image;
                Byte[] bytes2 = (Byte[])eq2.m_Image;
                if (bytes1.Length != bytes2.Length) bDiffParam = true;
            }
            for (int i = 0; i < 7; i++)
            {
                if (eq1.m_Input[i] == null && eq2.m_Input[i] != null) bDiffInputParam = true;
                if (eq1.m_Input[i] != null && eq2.m_Input[i] == null) bDiffInputParam = true;
                if (eq1.m_Input[i] != null && eq2.m_Input[i] != null)
                {
                    if (eq1.m_Input[i].m_iCreateYear != eq2.m_Input[i].m_iCreateYear) bDiffInputParam = true;
                    if (eq1.m_Input[i].m_iKind != eq2.m_Input[i].m_iKind) bDiffInputParam = true;
                    if (eq1.m_Input[i].m_iUseBeginYear != eq2.m_Input[i].m_iUseBeginYear) bDiffInputParam = true;
                    if (eq1.m_Input[i].m_strManufacturerName != eq2.m_Input[i].m_strManufacturerName) bDiffInputParam = true;
                    if (eq1.m_Input[i].m_strName != eq2.m_Input[i].m_strName) bDiffInputParam = true;
                    if (eq1.m_Input[i].m_strNumber != eq2.m_Input[i].m_strNumber) bDiffInputParam = true;
                    if (eq1.m_Input[i].m_strType != eq2.m_Input[i].m_strType) bDiffInputParam = true;
                }
            }
        }

        private static void AddParam(SQLiteCommand com, string name, DbType type, object value)
        {
            SQLiteParameter param = new SQLiteParameter(name, type);
            param.Value = value;
            com.Parameters.Add(param);
        }

        public static long? InsertSubject(SQLiteConnection connection, String strSubject, ref Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefs)
        {
            SQLiteCommand com = new SQLiteCommand(connection);
            com.CommandText = "INSERT INTO Subjects (SubjectName, ReadOnly) VALUES (@name, 0)";
            AddParam(com, "@name", DbType.String, strSubject);
            com.ExecuteNonQuery();

            com.CommandText = "select seq from sqlite_sequence where name = 'Subjects'";
            com.CommandType = CommandType.Text;
            SQLiteDataReader dr = com.ExecuteReader();

            long id = 0;
            while (dr.Read())
            {
                id = Convert.ToInt64(dr["seq"]);
            }
            dr.Close();

            m_dictRefs["Subjects"][new KeyValuePair<long, string>(0, strSubject)] = id;
            return id;
        }

        public static long? InsertBranch(SQLiteConnection connection, long SubjectID, String strBranch, ref Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefs)
        {
            SQLiteCommand com = new SQLiteCommand(connection);
            com.CommandText = "INSERT INTO Branches (BranchName, SubjectID, ReadOnly) VALUES (@name, @subjectID, 0)";
            AddParam(com, "@name", DbType.String, strBranch);
            AddParam(com, "@subjectID", DbType.Int64, SubjectID);
            com.ExecuteNonQuery();

            com.CommandText = "select seq from sqlite_sequence where name = 'Branches'";
            com.CommandType = CommandType.Text;
            SQLiteDataReader dr = com.ExecuteReader();

            long id = 0;
            while (dr.Read())
            {
                id = Convert.ToInt64(dr["seq"]);
            }
            dr.Close();

            m_dictRefs["Branches"][new KeyValuePair<long, string>(SubjectID, strBranch)] = id;
            return id;
        }

        public static long? InsertSubstation(SQLiteConnection connection, long BranchID, String strSubstation, ref Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefs)
        {
            SQLiteCommand com = new SQLiteCommand(connection);
            com.CommandText = "INSERT INTO Substations (SubstationName, BranchID, ReadOnly) VALUES (@name, @branchID, 0)";
            AddParam(com, "@name", DbType.String, strSubstation);
            AddParam(com, "@branchID", DbType.Int64, BranchID);
            com.ExecuteNonQuery();

            com.CommandText = "select seq from sqlite_sequence where name = 'Substations'";
            com.CommandType = CommandType.Text;
            SQLiteDataReader dr = com.ExecuteReader();

            long id = 0;
            while (dr.Read())
            {
                id = Convert.ToInt64(dr["seq"]);
            }
            dr.Close();

            m_dictRefs["Substations"][new KeyValuePair<long, string>(BranchID, strSubstation)] = id;
            return id;
        }

        public static long? GetSubstationID(SQLiteConnection connection, String strSubstation, String strBranch, String strSubject, ref Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefs, bool bCanAdd = true)
        {
            if (strSubstation == null) return null;

            // ищем субъект по наименованию
            long? SubjectID = null;
            long? BranchID = null;
            foreach (KeyValuePair<KeyValuePair<long, string>, long> pairSubject in m_dictRefs["Subjects"])
            {
                if (pairSubject.Key.Value == strSubject)
                {
                    SubjectID = pairSubject.Value;
                    break;
                }
            }

            if (SubjectID == null)
            {
                if (!bCanAdd) throw (new Exception("не удалось найти субъект."));
                // добавляем субъект, филиал и подстанцию
                // и возвращаем id подстанции
                SubjectID = InsertSubject(connection, strSubject, ref m_dictRefs);
                BranchID = InsertBranch(connection, (long)SubjectID, strBranch, ref m_dictRefs);
                return InsertSubstation(connection, (long)BranchID, strSubstation, ref m_dictRefs);
            }
            else
            {
                // ищем филиал в субъекте MainSubjectID
                foreach (KeyValuePair<KeyValuePair<long, string>, long> pairBranch in m_dictRefs["Branches"])
                {
                    if (pairBranch.Key.Key == SubjectID && pairBranch.Key.Value == strBranch)
                    {
                        BranchID = pairBranch.Value;
                        break;
                    }
                }

                if (BranchID == null)
                {
                    if (!bCanAdd) throw (new Exception("не удалось найти филиал."));
                    // добавляем филиал и подстанцию
                    // и возвращаем id подстанции
                    BranchID = InsertBranch(connection, (long)SubjectID, strBranch, ref m_dictRefs);
                    return InsertSubstation(connection, (long)BranchID, strSubstation, ref m_dictRefs);
                }
                else
                {
                    // ищем подстанцию в филиале
                    foreach (KeyValuePair<KeyValuePair<long, string>, long> pairSubstation in m_dictRefs["Substations"])
                    {
                        if (pairSubstation.Key.Key == BranchID && pairSubstation.Key.Value == strSubstation)
                        {
                            return pairSubstation.Value;
                        }
                    }

                    // добавляем подстанцию и возвращаем id
                    if (!bCanAdd) throw (new Exception("не удалось найти подстанцию."));
                    return InsertSubstation(connection, (long)BranchID, strSubstation, ref m_dictRefs);
                }
            }
        }

        public static long? GetRefID(SQLiteConnection connection, string strDictName, string strFieldName, long EquipmentKindID, string strValue, ref Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefs)
        {
            if (strValue == null) return null;

            if (m_dictRefs.ContainsKey(strDictName))
            {
                KeyValuePair<long, string> pair = new KeyValuePair<long, string>(EquipmentKindID, strValue);
                if (m_dictRefs[strDictName].ContainsKey(pair))
                {
                    return m_dictRefs[strDictName][pair];
                }
                else
                {
                    // добавляем значение в справочник
                    SQLiteCommand upd_com = new SQLiteCommand(connection);
                    if (EquipmentKindID > 0)
                    {
                        upd_com.CommandText = "INSERT INTO " + strDictName + " (EquipmentKindID, " + strFieldName + ", ReadOnly) VALUES (@kindid, @name, 0)";
                        AddParam(upd_com, "@kindid", DbType.Int64, EquipmentKindID);
                    }
                    else
                    {
                        upd_com.CommandText = "INSERT INTO " + strDictName + " (" + strFieldName + ", ReadOnly) VALUES (@name, 0)";
                    }
                    AddParam(upd_com, "@name", DbType.String, strValue);
                    upd_com.ExecuteNonQuery();

                    upd_com.CommandText = "select seq from sqlite_sequence where name = '" + strDictName + "'";
                    upd_com.CommandType = CommandType.Text;
                    SQLiteDataReader dr = upd_com.ExecuteReader();

                    long id = 0;
                    while (dr.Read())
                    {
                        id = Convert.ToInt64(dr["seq"]);
                    }
                    dr.Close();
                    
                    m_dictRefs[strDictName][new KeyValuePair<long, string>(EquipmentKindID, strValue)] = id;
                    return id;
                }
            }
            else
                throw (new Exception("не удалось найти справочника \"" + strDictName + "\""));
        }

        public static KeyValuePair<MsgState, string> UpdateEquipment(SQLiteConnection connection, KeyValuePair<long, EquipmentInfo> eq, EquipmentInfo eqImport, 
            bool bDiffParam, bool bDiffInputParam, ref Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefs)
        {
            List<long?> listInputID = new List<long?>();

            SQLiteTransaction tran = null;
            Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefsClone = new Dictionary<string, Dictionary<KeyValuePair<long, string>, long>>();
            foreach (KeyValuePair<string, Dictionary<KeyValuePair<long, string>, long>> pair in m_dictRefs)
            {
                m_dictRefsClone[pair.Key] = new Dictionary<KeyValuePair<long,string>,long>();
                foreach(KeyValuePair<KeyValuePair<long, string>, long> pair2 in pair.Value)
                {
                    m_dictRefsClone[pair.Key][pair2.Key] = pair2.Value;
                }
            }

            try
            {
                tran = connection.BeginTransaction();

                if (bDiffInputParam)
                {
                    string strIDs = "";
                    if (eq.Value.m_Input[0] != null && eq.Value.m_Input[0].m_iInputID != null)
                    {
                        if (strIDs == "") strIDs = eq.Value.m_Input[0].m_iInputID.ToString();
                        else strIDs = strIDs + "," + eq.Value.m_Input[0].m_iInputID.ToString();
                    }
                    if (eq.Value.m_Input[1] != null && eq.Value.m_Input[1].m_iInputID != null)
                    {
                        if (strIDs == "") strIDs = eq.Value.m_Input[1].m_iInputID.ToString();
                        else strIDs = strIDs + "," + eq.Value.m_Input[1].m_iInputID.ToString();
                    }
                    if (eq.Value.m_Input[2] != null && eq.Value.m_Input[2].m_iInputID != null)
                    {
                        if (strIDs == "") strIDs = eq.Value.m_Input[2].m_iInputID.ToString();
                        else strIDs = strIDs + "," + eq.Value.m_Input[2].m_iInputID.ToString();
                    }
                    if (eq.Value.m_Input[3] != null && eq.Value.m_Input[3].m_iInputID != null)
                    {
                        if (strIDs == "") strIDs = eq.Value.m_Input[3].m_iInputID.ToString();
                        else strIDs = strIDs + "," + eq.Value.m_Input[3].m_iInputID.ToString();
                    }
                    if (eq.Value.m_Input[4] != null && eq.Value.m_Input[4].m_iInputID != null)
                    {
                        if (strIDs == "") strIDs = eq.Value.m_Input[4].m_iInputID.ToString();
                        else strIDs = strIDs + "," + eq.Value.m_Input[4].m_iInputID.ToString();
                    }
                    if (eq.Value.m_Input[5] != null && eq.Value.m_Input[5].m_iInputID != null)
                    {
                        if (strIDs == "") strIDs = eq.Value.m_Input[5].m_iInputID.ToString();
                        else strIDs = strIDs + "," + eq.Value.m_Input[5].m_iInputID.ToString();
                    }
                    if (eq.Value.m_Input[6] != null && eq.Value.m_Input[6].m_iInputID != null)
                    {
                        if (strIDs == "") strIDs = eq.Value.m_Input[6].m_iInputID.ToString();
                        else strIDs = strIDs + "," + eq.Value.m_Input[6].m_iInputID.ToString();
                    }

                    // удаляем существующие вводы
                    SQLiteCommand com = new SQLiteCommand(connection);
                    com.CommandText = "DELETE FROM Inputs WHERE InputID IN (" + strIDs + ")";
                    com.CommandType = CommandType.Text;
                    com.ExecuteNonQuery();

                    // добавляем новые вводы
                    for (int i = 0; i < 7; i++)
                    {
                        if (eqImport.m_Input[i] != null && eqImport.m_Input[i].m_iKind != null)
                        {
                            long? ManufacturerInputID = GetRefID(connection, "ManufacturersInputs", "ManufacturerInputName", eq.Value.m_iEquipmentKindID, eqImport.m_Input[i].m_strManufacturerName, ref m_dictRefsClone);
                            long? InputTypeID = GetRefID(connection, "InputVoltageTypes", "InputVoltageTypeName", eq.Value.m_iEquipmentKindID, eqImport.m_Input[i].m_strType, ref m_dictRefsClone);

                            com.Parameters.Clear();
                            AddParam(com, "@kind", DbType.Int64, eqImport.m_Input[i].m_iKind);
                            AddParam(com, "@type_id", DbType.Int64, InputTypeID);
                            AddParam(com, "@manufacturerID", DbType.Int64, ManufacturerInputID);
                            AddParam(com, "@number", DbType.String, eqImport.m_Input[i].m_strNumber);
                            AddParam(com, "@create_year", DbType.Int64, eqImport.m_Input[i].m_iCreateYear);
                            AddParam(com, "@use_begin_year", DbType.Int64, eqImport.m_Input[i].m_iUseBeginYear);
                            AddParam(com, "@name", DbType.String, eqImport.m_Input[i].m_strName);

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

                            listInputID.Add(id);
                        }
                        else
                            listInputID.Add(null);
                    }
                }

                if (bDiffParam || bDiffInputParam)
                {
                    // EquipmentTypeID, ManufacturerID, RPNTypeID, SwitchDriveTypeID
                    long? EquipmentTypeID = GetRefID(connection, "EquipmentTypes", "EquipmentTypeName", eq.Value.m_iEquipmentKindID, eqImport.m_strEquipmentTypeName, ref m_dictRefsClone);
                    long? ManufacturerID = GetRefID(connection, "Manufacturers", "ManufacturerName", eq.Value.m_iEquipmentKindID, eqImport.m_strManufacturerName, ref m_dictRefsClone);
                    long? RPNTypeID = GetRefID(connection, "RPNTypes", "RPNTypeName", eq.Value.m_iEquipmentKindID, eqImport.m_RPN.m_strRPNTypeName, ref m_dictRefsClone);
                    long? SwitchDriveTypeID = GetRefID(connection, "SwitchDriveTypes", "SwitchDriveTypeName", eq.Value.m_iEquipmentKindID, eqImport.m_strSwitchDriveTypeName, ref m_dictRefsClone);

                    SQLiteCommand upd_com = new SQLiteCommand(connection);
                    upd_com.CommandText = "UPDATE Equipments SET ";

                    if (bDiffParam)
                    {
                        upd_com.CommandText += "EquipmentTypeID = @EquipmentTypeID, " +
                            "ManufacturerID = @ManufacturerID, " +
                            "ConstructionType = @ConstructionType, " +
                            "CoolingSystemTypeID = @CoolingSystemTypeID, " +
                            "EquipmentClassID = @EquipmentClassID, " +
                            "CreateYear = @CreateYear, " +
                            "UseBeginYear = @UseBeginYear, " +
                            "NominalVoltageLow = @NominalVoltageLow, " +
                            "NominalVoltageMiddle = @NominalVoltageMiddle, " +
                            "NominalVoltageHigh = @NominalVoltageHigh, " +
                            "NominalPower = @NominalPower, " +
                            "ProtectionOilTypeID = @ProtectionOilTypeID, " +
                            "RPNCnt = @RPNCnt, " +
                            "RPNVoltage = @RPNVoltage, " +
                            "Image = @Image, " +
                            "NominalVoltageNeitral = @NominalVoltageNeitral, " +
                            "RPNTypeID = @RPNTypeID, " +
                            "RPNKind = @RPNKind, " +
                            "RPNNumber = @RPNNumber, " +
                            "RPNNumber2 = @RPNNumber2, " +
                            "RPNNumber3 = @RPNNumber3, " +
                            "NominalCurrent = @NominalCurrent, " +
                            "SwitchDriveTypeID = @SwitchDriveTypeID ";
                    }
                    
                    if (bDiffInputParam)
                    {
                        if (bDiffParam) upd_com.CommandText += ", ";
                        upd_com.CommandText += "InputIDHighA = @InputIDHighA, " +
                            "InputIDHighB = @InputIDHighB, " +
                            "InputIDHighC = @InputIDHighC, " +
                            "InputIDMiddleA = @InputIDMiddleA, " +
                            "InputIDMiddleB = @InputIDMiddleB, " +
                            "InputIDMiddleC = @InputIDMiddleC, " +
                            "InputIDNeutral = @InputIDNeutral ";
                    }
                    
                    upd_com.CommandText += "WHERE EquipmentID = @EquipmentID";

                    upd_com.Parameters.Clear();
                    if (bDiffParam)
                    {
                        AddParam(upd_com, "@EquipmentTypeID", DbType.Int64, EquipmentTypeID);
                        AddParam(upd_com, "@ManufacturerID", DbType.Int64, ManufacturerID);
                        AddParam(upd_com, "@ConstructionType", DbType.Int64, eqImport.m_iConstructionType);
                        AddParam(upd_com, "@CoolingSystemTypeID", DbType.Int64, eqImport.m_iCoolingSystemID);
                        AddParam(upd_com, "@EquipmentClassID", DbType.Int64, eqImport.m_iEquipmentClassID);
                        AddParam(upd_com, "@CreateYear", DbType.Int64, eqImport.m_iCreateYear);
                        AddParam(upd_com, "@UseBeginYear", DbType.Int64, eqImport.m_iUseBeginYear);
                        AddParam(upd_com, "@NominalVoltageLow", DbType.Int64, eqImport.m_iNominalVoltageLow);
                        AddParam(upd_com, "@NominalVoltageMiddle", DbType.Int64, eqImport.m_iNominalVoltageMiddle);
                        AddParam(upd_com, "@NominalVoltageHigh", DbType.Int64, eqImport.m_iNominalVoltageHigh);
                        AddParam(upd_com, "@NominalPower", DbType.Decimal, eqImport.m_iNominalPower);
                        AddParam(upd_com, "@ProtectionOilTypeID", DbType.Int64, eqImport.m_iProtectionOilTypeID);
                        AddParam(upd_com, "@RPNCnt", DbType.Int64, eqImport.m_RPN.m_iRPNCnt);
                        AddParam(upd_com, "@RPNVoltage", DbType.Int64, eqImport.m_RPN.m_iRPNVoltage);
                        AddParam(upd_com, "@Image", DbType.Object, eqImport.m_Image);
                        AddParam(upd_com, "@NominalVoltageNeitral", DbType.Int64, eqImport.m_iNominalVoltageNeitral);
                        AddParam(upd_com, "@RPNTypeID", DbType.Int64, RPNTypeID);
                        AddParam(upd_com, "@RPNKind", DbType.Int64, eqImport.m_RPN.m_iRPNKind);
                        AddParam(upd_com, "@RPNNumber", DbType.String, eqImport.m_RPN.m_strRPNNumber);
                        AddParam(upd_com, "@RPNNumber2", DbType.String, eqImport.m_RPN.m_strRPNNumber2);
                        AddParam(upd_com, "@RPNNumber3", DbType.String, eqImport.m_RPN.m_strRPNNumber3);
                        AddParam(upd_com, "@NominalCurrent", DbType.Int64, eqImport.m_iNominalCurrent);
                        AddParam(upd_com, "@SwitchDriveTypeID", DbType.Int64, SwitchDriveTypeID);
                    }

                    if (bDiffInputParam)
                    {
                        AddParam(upd_com, "@InputIDHighA", DbType.Int64, listInputID[0]);
                        AddParam(upd_com, "@InputIDHighB", DbType.Int64, listInputID[1]);
                        AddParam(upd_com, "@InputIDHighC", DbType.Int64, listInputID[2]);
                        AddParam(upd_com, "@InputIDMiddleA", DbType.Int64, listInputID[3]);
                        AddParam(upd_com, "@InputIDMiddleB", DbType.Int64, listInputID[4]);
                        AddParam(upd_com, "@InputIDMiddleC", DbType.Int64, listInputID[5]);
                        AddParam(upd_com, "@InputIDNeutral", DbType.Int64, listInputID[6]);
                    }

                    AddParam(upd_com, "@EquipmentID", DbType.Int64, eq.Key);

                    upd_com.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (tran != null) tran.Rollback();
                return new KeyValuePair<MsgState, string>(MsgState.Error, "Ошибка при изменении оборудования " + eq.Value.m_strEquipmentName + " (" + eq.Value.m_strEquipmentNumber + "): " + ex.Message);
            }

            // заносим в справочник элементы, которые были добавлены
            foreach (KeyValuePair<string, Dictionary<KeyValuePair<long, string>, long>> pair in m_dictRefsClone)
            {
                foreach (KeyValuePair<KeyValuePair<long, string>, long> pair2 in pair.Value)
                {
                    if (!m_dictRefs[pair.Key].ContainsKey(pair2.Key))
                        m_dictRefs[pair.Key][pair2.Key] = pair2.Value;
                }
            }

            if (tran != null) tran.Commit();
            return new KeyValuePair<MsgState, string>(MsgState.Success, "Оборудование " + eq.Value.m_strEquipmentName + " (" + eq.Value.m_strEquipmentNumber + ") успешно изменено.");
        }

        public static KeyValuePair<MsgState, string> InsertEquipment(SQLiteConnection connection, EquipmentInfo eqImport, ref Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefs, out long EquipmentID)
        {
            EquipmentID = 0;

            SQLiteTransaction tran = null;

            try
            {
                tran = connection.BeginTransaction();

                List<long?> listInputID = new List<long?>();

                SQLiteCommand com = new SQLiteCommand(connection);
                com.CommandType = CommandType.Text;

                // добавляем новые вводы
                for (int i = 0; i < 7; i++)
                {
                    if (eqImport.m_Input[i] != null && eqImport.m_Input[i].m_iKind != null)
                    {
                        long? ManufacturerInputID = GetRefID(connection, "ManufacturersInputs", "ManufacturerName", eqImport.m_iEquipmentKindID, eqImport.m_Input[i].m_strManufacturerName, ref m_dictRefs);
                        long? InputTypeID = GetRefID(connection, "InputVoltageTypes", "InputVoltageTypeName", eqImport.m_iEquipmentKindID, eqImport.m_Input[i].m_strType, ref m_dictRefs);

                        com.Parameters.Clear();
                        AddParam(com, "@kind", DbType.Int64, eqImport.m_Input[i].m_iKind);
                        AddParam(com, "@type_id", DbType.Int64, InputTypeID);
                        AddParam(com, "@manufacturerID", DbType.Int64, ManufacturerInputID);
                        AddParam(com, "@number", DbType.String, eqImport.m_Input[i].m_strNumber);
                        AddParam(com, "@create_year", DbType.Int64, eqImport.m_Input[i].m_iCreateYear);
                        AddParam(com, "@use_begin_year", DbType.Int64, eqImport.m_Input[i].m_iUseBeginYear);
                        AddParam(com, "@name", DbType.String, eqImport.m_Input[i].m_strName);

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

                        listInputID.Add(id);
                    }
                    else
                        listInputID.Add(null);
                }

                // добавляем оборудование
                // EquipmentTypeID, ManufacturerID, RPNTypeID, SwitchDriveTypeID
                long? EquipmentTypeID = GetRefID(connection, "EquipmentTypes", "EquipmentTypeName", eqImport.m_iEquipmentKindID, eqImport.m_strEquipmentTypeName, ref m_dictRefs);
                long? ManufacturerID = GetRefID(connection, "Manufacturers", "ManufacturerName", eqImport.m_iEquipmentKindID, eqImport.m_strManufacturerName, ref m_dictRefs);
                long? RPNTypeID = GetRefID(connection, "RPNTypes", "RPNTypeName", eqImport.m_iEquipmentKindID, eqImport.m_RPN.m_strRPNTypeName, ref m_dictRefs);
                long? SwitchDriveTypeID = GetRefID(connection, "SwitchDriveTypes", "SwitchDriveTypeName", eqImport.m_iEquipmentKindID, eqImport.m_strSwitchDriveTypeName, ref m_dictRefs);
                long? SubstationID = GetSubstationID(connection, eqImport.m_strSubstation, eqImport.m_strBranch, eqImport.m_strSubject, ref m_dictRefs);

                SQLiteCommand ins_com = new SQLiteCommand(connection);
                ins_com.CommandText = "INSERT INTO Equipments (" +
                    "EquipmentName, " +
                    "EquipmentNumber, " +
                    "EquipmentKindID, " +
                    "SubstationID, " +
                    "EquipmentTypeID, " +
                    "ManufacturerID, " +
                    "ConstructionType, " +
                    "CoolingSystemTypeID, " +
                    "EquipmentClassID, " +
                    "CreateYear, " +
                    "UseBeginYear, " +
                    "NominalVoltageLow, " +
                    "NominalVoltageMiddle, " +
                    "NominalVoltageHigh, " +
                    "NominalPower, " +
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
                    "NominalCurrent, " +
                    "SwitchDriveTypeID " + 
                    ") VALUES ( " +
                    "@EquipmentName, " +
                    "@EquipmentNumber, " +
                    "@EquipmentKindID, " +
                    "@SubstationID, " +
                    "@EquipmentTypeID, " +
                    "@ManufacturerID, " +
                    "@ConstructionType, " +
                    "@CoolingSystemTypeID, " +
                    "@EquipmentClassID, " +
                    "@CreateYear, " +
                    "@UseBeginYear, " +
                    "@NominalVoltageLow, " +
                    "@NominalVoltageMiddle, " +
                    "@NominalVoltageHigh, " +
                    "@NominalPower, " +
                    "@ProtectionOilTypeID, " +
                    "@InputIDHighA, " +
                    "@InputIDHighB, " +
                    "@InputIDHighC, " +
                    "@InputIDMiddleA, " +
                    "@InputIDMiddleB, " +
                    "@InputIDMiddleC, " +
                    "@InputIDNeutral, " +
                    "@RPNCnt, " +
                    "@RPNVoltage, " +
                    "@Image, " +
                    "@NominalVoltageNeitral, " +
                    "@RPNTypeID, " +
                    "@RPNKind, " +
                    "@RPNNumber, " +
                    "@RPNNumber2, " +
                    "@RPNNumber3, " +
                    "@NominalCurrent, " +
                    "@SwitchDriveTypeID " +
                    ")";

                ins_com.Parameters.Clear();
                AddParam(ins_com, "@EquipmentName", DbType.String, eqImport.m_strEquipmentName);
                AddParam(ins_com, "@EquipmentNumber", DbType.String, eqImport.m_strEquipmentNumber);
                AddParam(ins_com, "@EquipmentKindID", DbType.Int64, eqImport.m_iEquipmentKindID);
                AddParam(ins_com, "@SubstationID", DbType.Int64, SubstationID);
                AddParam(ins_com, "@EquipmentTypeID", DbType.Int64, EquipmentTypeID);
                AddParam(ins_com, "@ManufacturerID", DbType.Int64, ManufacturerID);
                AddParam(ins_com, "@ConstructionType", DbType.Int64, eqImport.m_iConstructionType);
                AddParam(ins_com, "@CoolingSystemTypeID", DbType.Int64, eqImport.m_iCoolingSystemID);
                AddParam(ins_com, "@EquipmentClassID", DbType.Int64, eqImport.m_iEquipmentClassID);
                AddParam(ins_com, "@CreateYear", DbType.Int64, eqImport.m_iCreateYear);
                AddParam(ins_com, "@UseBeginYear", DbType.Int64, eqImport.m_iUseBeginYear);
                AddParam(ins_com, "@NominalVoltageLow", DbType.Int64, eqImport.m_iNominalVoltageLow);
                AddParam(ins_com, "@NominalVoltageMiddle", DbType.Int64, eqImport.m_iNominalVoltageMiddle);
                AddParam(ins_com, "@NominalVoltageHigh", DbType.Int64, eqImport.m_iNominalVoltageHigh);
                AddParam(ins_com, "@NominalPower", DbType.Decimal, eqImport.m_iNominalPower);
                AddParam(ins_com, "@ProtectionOilTypeID", DbType.Int64, eqImport.m_iProtectionOilTypeID);

                AddParam(ins_com, "@InputIDHighA", DbType.Int64, listInputID[0]);
                AddParam(ins_com, "@InputIDHighB", DbType.Int64, listInputID[1]);
                AddParam(ins_com, "@InputIDHighC", DbType.Int64, listInputID[2]);
                AddParam(ins_com, "@InputIDMiddleA", DbType.Int64, listInputID[3]);
                AddParam(ins_com, "@InputIDMiddleB", DbType.Int64, listInputID[4]);
                AddParam(ins_com, "@InputIDMiddleC", DbType.Int64, listInputID[5]);
                AddParam(ins_com, "@InputIDNeutral", DbType.Int64, listInputID[6]);

                AddParam(ins_com, "@RPNCnt", DbType.Int64, eqImport.m_RPN.m_iRPNCnt);
                AddParam(ins_com, "@RPNVoltage", DbType.Int64, eqImport.m_RPN.m_iRPNVoltage);
                AddParam(ins_com, "@Image", DbType.Object, eqImport.m_Image);
                AddParam(ins_com, "@NominalVoltageNeitral", DbType.Int64, eqImport.m_iNominalVoltageNeitral);
                AddParam(ins_com, "@RPNTypeID", DbType.Int64, RPNTypeID);
                AddParam(ins_com, "@RPNKind", DbType.Int64, eqImport.m_RPN.m_iRPNKind);
                AddParam(ins_com, "@RPNNumber", DbType.String, eqImport.m_RPN.m_strRPNNumber);
                AddParam(ins_com, "@RPNNumber2", DbType.String, eqImport.m_RPN.m_strRPNNumber2);
                AddParam(ins_com, "@RPNNumber3", DbType.String, eqImport.m_RPN.m_strRPNNumber3);
                AddParam(ins_com, "@NominalCurrent", DbType.Int64, eqImport.m_iNominalCurrent);
                AddParam(ins_com, "@SwitchDriveTypeID", DbType.Int64, SwitchDriveTypeID);

                ins_com.ExecuteNonQuery();

                ins_com.CommandText = "select seq from sqlite_sequence where name = 'Equipments'";
                ins_com.Parameters.Clear();
                SQLiteDataReader drEquip = ins_com.ExecuteReader();
                while (drEquip.Read())
                {
                    EquipmentID = Convert.ToInt64(drEquip["seq"]);
                }
                drEquip.Close();
            }
            catch (SQLiteException ex)
            {
                if (tran != null) tran.Rollback();
                return new KeyValuePair<MsgState, string>(MsgState.Error, "Ошибка при добавлении оборудования " + eqImport.m_strEquipmentName + " (" + eqImport.m_strEquipmentNumber + "): " + ex.Message);
            }
            catch (Exception ex)
            {
                if (tran != null) tran.Rollback();
                return new KeyValuePair<MsgState, string>(MsgState.Error, "Ошибка при добавлении оборудования " + eqImport.m_strEquipmentName + " (" + eqImport.m_strEquipmentNumber + "): " + ex.Message);
            }

            if (tran != null) tran.Commit();
            return new KeyValuePair<MsgState, string>(MsgState.Success, "Оборудование " + eqImport.m_strEquipmentName + " (" + eqImport.m_strEquipmentNumber + ") успешно добавлено.");
        }

        public static KeyValuePair<MsgState, string> InsertCheck(SQLiteConnection connection, EquipmentInfo.InspectionInfo.CheckInfo check, EquipmentInfo eqImport, ref Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefs, out long? CheckID)
        {
            CheckID = null;

            try
            {
                // добавляем проверку
                long? SubstationID = GetSubstationID(connection, eqImport.m_strSubstation, eqImport.m_strBranch, eqImport.m_strSubject, ref m_dictRefs, false);

                SQLiteCommand ins_com = new SQLiteCommand(connection);
                ins_com.CommandText = "INSERT INTO Checks (" +
                    "CheckDateBegin, " +
                    "CheckDateEnd, " +
                    "SubstationID " +
                    ") VALUES ( " +
                    "@CheckDateBegin, " +
                    "@CheckDateEnd, " +
                    "@SubstationID " +
                    ")";

                ins_com.Parameters.Clear();
                AddParam(ins_com, "@CheckDateBegin", DbType.String, check.m_dtBegin);
                AddParam(ins_com, "@CheckDateEnd", DbType.String, check.m_dtEnd);
                AddParam(ins_com, "@SubstationID", DbType.Int64, SubstationID);

                ins_com.ExecuteNonQuery();

                ins_com.CommandText = "select seq from sqlite_sequence where name = 'Equipments'";
                ins_com.Parameters.Clear();
                SQLiteDataReader drCheck = ins_com.ExecuteReader();
                while (drCheck.Read())
                {
                    CheckID = Convert.ToInt64(drCheck["seq"]);
                }
                drCheck.Close();       
            }
            catch (SQLiteException ex)
            {
                return new KeyValuePair<MsgState, string>(MsgState.Error, "Ошибка при добавлении проверки c " + check.m_dtBegin.ToShortDateString() + " по " + check.m_dtEnd.ToShortDateString()
                                            + " на объекте \"" + eqImport.m_strSubject + ", " + eqImport.m_strBranch + ", " + eqImport.m_strSubstation + ": " + ex.Message);
            }

            return new KeyValuePair<MsgState, string>(MsgState.Success, "Проверка c " + check.m_dtBegin.ToShortDateString() + " по " + check.m_dtEnd.ToShortDateString()
                                            + " на объекте \"" + eqImport.m_strSubject + ", " + eqImport.m_strBranch + ", " + eqImport.m_strSubstation + ") успешно добавлена.");
        }

        public static void CompareInspections(EquipmentInfo.InspectionInfo inspImport, EquipmentInfo.InspectionInfo insp, ref bool bDiffParam)
        {
            bDiffParam = false;

            if (inspImport.m_dtCreateDate != insp.m_dtCreateDate ||
                inspImport.m_iSubType != insp.m_iSubType ||
                inspImport.m_strExecutor != insp.m_strExecutor ||
                inspImport.m_strDelegator != insp.m_strDelegator)
            {
                bDiffParam = true;
                return;
            }

            if (inspImport.m_Check != null && insp.m_Check != null)
            {
                if (inspImport.m_Check.m_dtBegin != insp.m_Check.m_dtBegin ||
                    inspImport.m_Check.m_dtEnd != insp.m_Check.m_dtEnd)
                {
                    bDiffParam = true;
                    return;
                }
            }

            for (int i = 0; i < inspImport.m_listDatas.Count; i++)
            {
                long SystemItemID = inspImport.m_listDatas[i].m_iSystemItemID;
                long? ParameterID = inspImport.m_listDatas[i].m_iParameterID;
                long? DefectID = inspImport.m_listDatas[i].m_iDefectID;

                if (inspImport.m_listDatas.Count != insp.m_listDatas.Count)
                {
                    bDiffParam = true;
                    return;
                }

                for (int j = 0; j < insp.m_listDatas.Count; j++)
                {
                    long SystemItemID_ = insp.m_listDatas[j].m_iSystemItemID;
                    long? ParameterID_ = insp.m_listDatas[j].m_iParameterID;
                    long? DefectID_ = insp.m_listDatas[j].m_iDefectID;

                    if (SystemItemID == SystemItemID_ &&
                        ParameterID == ParameterID_ &&
                        DefectID == DefectID_)
                    {
                        if (inspImport.m_listDatas[i].m_dtValDate != insp.m_listDatas[j].m_dtValDate ||
                            inspImport.m_listDatas[i].m_fValNumber != insp.m_listDatas[j].m_fValNumber ||
                            inspImport.m_listDatas[i].m_iResult != insp.m_listDatas[j].m_iResult ||
                            inspImport.m_listDatas[i].m_iValSelect != insp.m_listDatas[j].m_iValSelect ||
                            inspImport.m_listDatas[i].m_strValString != insp.m_listDatas[j].m_strValString)
                        {
                            bDiffParam = true;
                            return;
                        }

                        break;
                    }
                }
            }
        }

        public static KeyValuePair<MsgState, string> InsertInspection(SQLiteConnection connection, EquipmentInfo.InspectionInfo inspection, long? DeleteInspectionID,
            long EquipmentID, string strEquipmentName, string strEquipmentNumber, long? CheckID, ref Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefs)
        {
            SQLiteTransaction tran = null;

            try
            {
                SQLiteCommand ins_com = new SQLiteCommand(connection);

                if (CheckID != null)
                {
                    ins_com.CommandText = "SELECT COUNT(*) AS Cnt FROM Inspections WHERE CheckID = @CheckID AND EquipmentID = @EquipmentID AND InspectionType = @InspectionType AND InspectionID <> COALESCE(@DeleteInspectionID, 0)";
                    ins_com.Parameters.Clear();
                    AddParam(ins_com, "@CheckID", DbType.Int64, CheckID);
                    AddParam(ins_com, "@EquipmentID", DbType.Int64, EquipmentID);
                    AddParam(ins_com, "@InspectionType", DbType.Int64, inspection.m_iType);
                    AddParam(ins_com, "@DeleteInspectionID", DbType.Int64, DeleteInspectionID);

                    object res = ins_com.ExecuteScalar();
                    // если в проверке уже есть обследование на данное оборудование данного типа, то запрещено добавлять это обследование
                    string strType = "";
                    if (Convert.ToInt64(res) > 0)
                    {
                        switch ((Inspection.InspectionType)inspection.m_iType)
                        {
                            case Inspection.InspectionType.HARG:
                                strType = "ХАРГ";
                                break;
                            case Inspection.InspectionType.FHA:
                                strType = "ФХА";
                                break;
                            case Inspection.InspectionType.Electrical:
                                strType = "Электрические измерения";
                                break;
                            case Inspection.InspectionType.Parameter:
                                strType = "Определение характеристик";
                                break;
                            case Inspection.InspectionType.Vibro:
                                strType = "Вибрационное обследование";
                                break;
                            case Inspection.InspectionType.Visual:
                                strType = "Визуальное обследование";
                                break;
                            case Inspection.InspectionType.Warm:
                                strType = "Тепловизионный контроль";
                                break;

                        }
                        return new KeyValuePair<MsgState, string>(MsgState.Error, "Ошибка: в проверке c " + inspection.m_Check.m_dtBegin.ToShortDateString() + " по " + inspection.m_Check.m_dtEnd.ToShortDateString() + 
                            " уже есть обследование \"" + strType + "\" для оборудования " + strEquipmentName + " (" + strEquipmentNumber + ")");
                    }
                }

                tran = connection.BeginTransaction();

                if (DeleteInspectionID != null)
                {
                    ins_com.Parameters.Clear();
                    AddParam(ins_com, "@insp_id", DbType.Int64, DeleteInspectionID);

                    ins_com.CommandText = "DELETE FROM InspectionDatas WHERE InspectionID = @insp_id";
                    ins_com.ExecuteNonQuery();

                    ins_com.CommandText = "DELETE FROM FunctionalSystemResults WHERE InspectionID = @insp_id";
                    ins_com.ExecuteNonQuery();

                    ins_com.CommandText = "DELETE FROM SystemItemResults WHERE InspectionID = @insp_id";
                    ins_com.ExecuteNonQuery();

                    ins_com.CommandText = "DELETE FROM Inspections WHERE InspectionID = @insp_id";
                    ins_com.ExecuteNonQuery();
                }

                ins_com.CommandText = "INSERT INTO Inspections (" +
                    "EquipmentID, " +
                    "InspectionType, " +
                    "InspectionSubType, " +
                    "InspectionDate, " +
                    "CreateDate, " +
                    "Executor, " +
                    "Delegator, " +
                    "CheckID " +
                    ") VALUES ( " +
                    "@EquipmentID, " +
                    "@InspectionType, " +
                    "@InspectionSubType, " +
                    "@InspectionDate, " +
                    "@CreateDate, " +
                    "@Executor, " +
                    "@Delegator, " +
                    "@CheckID " +
                    ")";

                ins_com.Parameters.Clear();
                AddParam(ins_com, "@EquipmentID", DbType.Int64, EquipmentID);
                AddParam(ins_com, "@InspectionType", DbType.Int64, inspection.m_iType);
                AddParam(ins_com, "@InspectionSubType", DbType.Int64, inspection.m_iSubType);
                AddParam(ins_com, "@InspectionDate", DbType.DateTime, inspection.m_dtDate);
                AddParam(ins_com, "@CreateDate", DbType.DateTime, inspection.m_dtCreateDate);
                AddParam(ins_com, "@Executor", DbType.String, inspection.m_strExecutor);
                AddParam(ins_com, "@Delegator", DbType.String, inspection.m_strDelegator);
                AddParam(ins_com, "@CheckID", DbType.Int64, CheckID);

                ins_com.ExecuteNonQuery();

                long InspectionID = 0;
                ins_com.CommandText = "select seq from sqlite_sequence where name = 'Inspections'";
                ins_com.Parameters.Clear();
                SQLiteDataReader drInsp = ins_com.ExecuteReader();
                while (drInsp.Read())
                {
                    InspectionID = Convert.ToInt64(drInsp["seq"]);
                }
                drInsp.Close();

                // вносим данные по обследованию
                ins_com.CommandText = "INSERT INTO InspectionDatas (" +
                    "InspectionID, " +
                    "SystemItemID, " +
                    "ParameterID, " +
                    "DefectID, " +
                    "ValNumber, " +
                    "ValSelect, " +
                    "ValString, " +
                    "ValDate, " +
                    "Result " +
                    ") VALUES ( " +
                    "@InspectionID, " +
                    "@SystemItemID, " +
                    "@ParameterID, " +
                    "@DefectID, " +
                    "@ValNumber, " +
                    "@ValSelect, " +
                    "@ValString, " +
                    "@ValDate, " +
                    "@Result " +
                    ")";
                for (int i = 0; i < inspection.m_listDatas.Count; i++)
                {
                    ins_com.Parameters.Clear();
                    AddParam(ins_com, "@InspectionID", DbType.Int64, InspectionID);
                    AddParam(ins_com, "@SystemItemID", DbType.Int64, inspection.m_listDatas[i].m_iSystemItemID);
                    AddParam(ins_com, "@ParameterID", DbType.Int64, inspection.m_listDatas[i].m_iParameterID);
                    AddParam(ins_com, "@DefectID", DbType.Int64, inspection.m_listDatas[i].m_iDefectID);
                    AddParam(ins_com, "@ValNumber", DbType.Decimal, inspection.m_listDatas[i].m_fValNumber);
                    AddParam(ins_com, "@ValSelect", DbType.Int64, inspection.m_listDatas[i].m_iValSelect);
                    AddParam(ins_com, "@ValString", DbType.String, inspection.m_listDatas[i].m_strValString);
                    AddParam(ins_com, "@ValDate", DbType.DateTime, inspection.m_listDatas[i].m_dtValDate);
                    AddParam(ins_com, "@Result", DbType.Int64, inspection.m_listDatas[i].m_iResult);

                    ins_com.ExecuteNonQuery();
                }

                ins_com.CommandText = "INSERT INTO FunctionalSystemResults (" +
                    "InspectionID, " +
                    "FunctionalSystemID, " +
                    "Result " +
                    ") VALUES ( " +
                    "@InspectionID, " +
                    "@FunctionalSystemID, " +
                    "@Result " +
                    ")";
                for (int i = 0; i < inspection.m_listFunctionalSystemResults.Count; i++)
                {
                    ins_com.Parameters.Clear();
                    AddParam(ins_com, "@InspectionID", DbType.Int64, InspectionID);
                    AddParam(ins_com, "@FunctionalSystemID", DbType.Int64, inspection.m_listFunctionalSystemResults[i].m_iFunctionalSystemID);
                    AddParam(ins_com, "@Result", DbType.Int64, inspection.m_listFunctionalSystemResults[i].m_iResult);

                    ins_com.ExecuteNonQuery();
                }

                ins_com.CommandText = "INSERT INTO SystemItemResults (" +
                    "InspectionID, " +
                    "SystemItemID, " +
                    "Result " +
                    ") VALUES ( " +
                    "@InspectionID, " +
                    "@SystemItemID, " +
                    "@Result " +
                    ")";
                for (int i = 0; i < inspection.m_listSystemItemResults.Count; i++)
                {
                    ins_com.Parameters.Clear();
                    AddParam(ins_com, "@InspectionID", DbType.Int64, InspectionID);
                    AddParam(ins_com, "@SystemItemID", DbType.Int64, inspection.m_listSystemItemResults[i].m_iSystemItemID);
                    AddParam(ins_com, "@Result", DbType.Int64, inspection.m_listSystemItemResults[i].m_iResult);

                    ins_com.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                if (tran != null) tran.Rollback();
                return new KeyValuePair<MsgState, string>(MsgState.Error, "Ошибка при добавлении обследования: " + ex.Message);
            }
            catch (Exception ex)
            {
                if (tran != null) tran.Rollback();
                return new KeyValuePair<MsgState, string>(MsgState.Error, "Ошибка при добавлении обследования: " + ex.Message);
            }

            if (tran != null) tran.Commit();
            return new KeyValuePair<MsgState, string>(MsgState.Success, "Обследование успешно добавлено.");
        }
    }
}
