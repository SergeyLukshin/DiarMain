using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MSWord = Microsoft.Office.Interop.Word;

namespace DiarMain
{
    public partial class WaitingFrom : DevExpress.XtraEditors.XtraForm
    {
        //public long m_ID;
        private bool m_bEnd = false;
        public Inspection.ReportType m_reportType;
        public long m_CheckID = -1;

        public List<ReportInfo.Equipment> m_listEquipments = new List<ReportInfo.Equipment>();
        Dictionary<Inspection.InspectionType, List<ReportInfo.InspectionSubType_>> m_list_sub_types = new Dictionary<Inspection.InspectionType, List<ReportInfo.InspectionSubType_>>();
        Dictionary<Inspection.InspectionType, List<long?>> m_dictCommonSubTypes = new Dictionary<Inspection.InspectionType, List<long?>>();

        public WaitingFrom(Inspection.ReportType type)
        {
            m_reportType = type;
            InitializeComponent();
        }

        public WaitingFrom()
        {
            InitializeComponent();
        }

        private void PrintProtocolTransformer()
        {
            worker.ReportProgress(5);

            Word word = new Word();
            try
            {
                List<KeyValuePair<string, string>> bookmarks = new List<KeyValuePair<string, string>>();

                if (!ReportInfo.GetData(m_CheckID, m_listEquipments, m_dictCommonSubTypes, m_list_sub_types, 0))
                    return;

                bookmarks.Add(new KeyValuePair<string, string>("Трансформатор1", m_listEquipments[0].m_dictFields["EquipmentName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Трансформатор2", m_listEquipments[0].m_dictFields["EquipmentName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Год_ввода_в_эксплуатацию", m_listEquipments[0].m_dictFields["UseBeginYear"]));
                bookmarks.Add(new KeyValuePair<string, string>("Год_изготовления", m_listEquipments[0].m_dictFields["CreateYear"]));
                bookmarks.Add(new KeyValuePair<string, string>("Год_изготовления2", m_listEquipments[0].m_dictFields["CreateYear"]));
                bookmarks.Add(new KeyValuePair<string, string>("Год_оценки", DateTime.Now.Year.ToString()));
                bookmarks.Add(new KeyValuePair<string, string>("Диспетчерское_наименование", m_listEquipments[0].m_dictFields["EquipmentName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Диспетчерское_наименование2", m_listEquipments[0].m_dictFields["EquipmentName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Диспетчерское_наименование3", m_listEquipments[0].m_dictFields["EquipmentName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Заводской_номер", m_listEquipments[0].m_dictFields["EquipmentNumber"]));
                bookmarks.Add(new KeyValuePair<string, string>("Заводской_номер2", m_listEquipments[0].m_dictFields["EquipmentNumber"]));
                bookmarks.Add(new KeyValuePair<string, string>("Напряжение_ВН", m_listEquipments[0].m_dictFields["NominalVoltageHigh"]));
                bookmarks.Add(new KeyValuePair<string, string>("Напряжение_НН", m_listEquipments[0].m_dictFields["NominalVoltageMiddle"]));
                bookmarks.Add(new KeyValuePair<string, string>("Напряжение_СН", m_listEquipments[0].m_dictFields["NominalVoltageLow"]));
                bookmarks.Add(new KeyValuePair<string, string>("Напряжение_нейтрали", m_listEquipments[0].m_dictFields["NominalVoltageNeitral"]));
                bookmarks.Add(new KeyValuePair<string, string>("Подстанция", m_listEquipments[0].m_dictFields["SubstationName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Филиал", m_listEquipments[0].m_dictFields["BranchName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Субъект", m_listEquipments[0].m_dictFields["SubjectName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Тип_трансформатора", m_listEquipments[0].m_dictFields["EquipmentTypeName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Тип_трансформатора2", m_listEquipments[0].m_dictFields["EquipmentTypeName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Тип_трансформатора3", m_listEquipments[0].m_dictFields["EquipmentTypeName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Завод_изготовитель", m_listEquipments[0].m_dictFields["ManufacturerName"]));
                bookmarks.Add(new KeyValuePair<string, string>("Номинальная_мощность", m_listEquipments[0].m_dictFields["NominalPower"]));
                bookmarks.Add(new KeyValuePair<string, string>("Тип_защиты_масла", m_listEquipments[0].m_dictFields["ProtectionOilTypeID"]));
                bookmarks.Add(new KeyValuePair<string, string>("Число_фаз", m_listEquipments[0].m_dictFields["ConstructionType"]));
                bookmarks.Add(new KeyValuePair<string, string>("Система_охлаждения", m_listEquipments[0].m_dictFields["CoolingSystemTypeID"]));
                bookmarks.Add(new KeyValuePair<string, string>("Исполнение_ВН_фазы_А", m_listEquipments[0].m_dictFields["InputKindHighA"]));
                bookmarks.Add(new KeyValuePair<string, string>("Исполнение_ВН_фазы_В", m_listEquipments[0].m_dictFields["InputKindHighB"]));
                bookmarks.Add(new KeyValuePair<string, string>("Исполнение_ВН_фазы_С", m_listEquipments[0].m_dictFields["InputKindHighC"]));
                bookmarks.Add(new KeyValuePair<string, string>("Исполнение_СН_фазы_А", m_listEquipments[0].m_dictFields["InputKindMiddleA"]));
                bookmarks.Add(new KeyValuePair<string, string>("Исполнение_СН_фазы_В", m_listEquipments[0].m_dictFields["InputKindMiddleB"]));
                bookmarks.Add(new KeyValuePair<string, string>("Исполнение_СН_фазы_С", m_listEquipments[0].m_dictFields["InputKindMiddleC"]));
                bookmarks.Add(new KeyValuePair<string, string>("Исполнение_нейтрали", m_listEquipments[0].m_dictFields["InputKindNeutral"]));

                worker.ReportProgress(15);

                Dictionary<KeyValuePair<int, int>, MSWord.WdColor> dictColors = new Dictionary<KeyValuePair<int, int>, MSWord.WdColor>();
                List<KeyValuePair<int, int>> listMergeCells = new List<KeyValuePair<int, int>>();
                List<KeyValuePair<int, int>> listMergeCellsSystemItem = new List<KeyValuePair<int, int>>();

                if (!word.Start(AppDomain.CurrentDomain.BaseDirectory + "/Reports/Протокол.dot")) return;
                word.SetBookmarkText(bookmarks);
                if (m_listEquipments[0].m_image != null)
                {
                    m_listEquipments[0].m_image.Save(AppDomain.CurrentDomain.BaseDirectory + "/Reports/tmp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    word.InsertPicture(AppDomain.CurrentDomain.BaseDirectory + "/Reports/tmp.jpg", Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter, "Рисунок");
                }

                char chTab = (char)9;
                char chN = (char)13;

                string strTable = "";

                int tableIndex = 0;
                int indexParams = 1;
                int first_row = -1;

                KeyValuePair<Inspection.RangeResult, List<string>> resultList = new KeyValuePair<Inspection.RangeResult, List<string>>(Inspection.RangeResult.None, new List<string>());

                worker.ReportProgress(20);

                //int AllCnt = /*Enum.GetValues(typeof(Inspection.InspectionType)).GetLength(0)*/Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[0].m_EquipmentKindID].Count * m_listEquipments[0].m_listFunctionalSystem.Count;
                int AllCnt = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[0].m_EquipmentKindID].Count * m_listEquipments[0].m_listFunctionalSystem.Count;
                int gl_index = 0;

                //foreach (Inspection.InspectionType m_type in Enum.GetValues(typeof(Inspection.InspectionType)))
                for (int ii = 0; ii < Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[0].m_EquipmentKindID].Count; ii++)
                {
                    Inspection.InspectionType m_type = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[0].m_EquipmentKindID][ii];
                    //if (m_type != Inspection.InspectionType.Vibro) continue;

                    dictColors.Clear();
                    listMergeCells.Clear();
                    listMergeCellsSystemItem.Clear();

                    strTable = "№ пп" + chTab + "Функциональная система (узел) трансформатора" + chTab + "Состояние узла" + chTab + "Материал, сборочный узел, деталь" + chTab + "Дефект, повреждение, нарушение" + chTab + chTab + chTab + chTab + chN;
                    int index = 1;
                    indexParams = 1;
                    for (int i = 0; i < m_listEquipments[0].m_listFunctionalSystem.Count; i++)
                    {
                        gl_index++;

                        string strFunctionalSystemName = m_listEquipments[0].m_listFunctionalSystem[i].m_strFunctionalSystemName;

                        long m_sub_type_id = m_listEquipments[0].m_listFunctionalSystem[i].m_dictSubTypes[m_type];

                        if (m_listEquipments[0].m_listFunctionalSystem[i].m_dictUnionMainID[m_type].ContainsKey(m_sub_type_id)) continue;

                        bool bUnion = false;
                        if (m_listEquipments[0].m_listFunctionalSystem[i].m_dictUnionExtraID[m_type].ContainsKey(m_sub_type_id))
                        {
                            strFunctionalSystemName = m_listEquipments[0].m_listFunctionalSystem[i].m_strFunctionalSystemName;
                            for (int j = 0; j < m_listEquipments[0].m_listFunctionalSystem[i].m_dictUnionExtraID[m_type][m_sub_type_id].Count; j++)
                            {
                                long ExtraFunctionalSystemID = m_listEquipments[0].m_listFunctionalSystem[i].m_dictUnionExtraID[m_type][m_sub_type_id][j];
                                if (m_listEquipments[0].m_dictFunctionalSystemPos.ContainsKey(ExtraFunctionalSystemID))
                                {
                                    strFunctionalSystemName = strFunctionalSystemName + ", " + m_listEquipments[0].m_listFunctionalSystem[m_listEquipments[0].m_dictFunctionalSystemPos[ExtraFunctionalSystemID]].m_strFunctionalSystemName;
                                    bUnion = true;
                                }
                            }
                        }

                        bool bFind = false;
                        bool bFirstFuncSystem = true;
                        Inspection.RangeResult maxResult = Inspection.RangeResult.None;
                        first_row = -1;

                        if (m_sub_type_id == 0 && m_dictCommonSubTypes[m_type][0] != null) m_sub_type_id = (long)m_dictCommonSubTypes[m_type][0]; // на всякий случай, если нет данных, то берется первое подобследование

                        foreach (KeyValuePair<string, ReportInfo.SystemItem> si in m_listEquipments[0].m_listFunctionalSystem[i].m_dictSystemItems[m_type])
                        {
                            bool bFirstSystemItem = true;
                            string strSystemItemName = si.Key;
                            if (bUnion && m_listEquipments[0].m_listFunctionalSystem[i].m_dictSystemItems[m_type].Count == 1)
                                strSystemItemName = strFunctionalSystemName;

                            if (si.Value.m_listDefectResults.Count > 0)
                            {
                                for (int j = 0; j < si.Value.m_listDefectResults.Count; j++)
                                {
                                    // если нет результатов обследования по подразделу, то m_sub_type_id = 0
                                    
                                    if (si.Value.m_listDefectResults[j].m_InspectionSubType == null
                                        || si.Value.m_listDefectResults[j].m_InspectionSubType == m_sub_type_id)
                                    {
                                        string strDefectName = si.Value.m_listDefectResults[j].m_strDefectName;

                                        if (si.Value.m_listDefectResults[j].m_Result != null)
                                        {
                                            // считаем дефекты, влияющие на результат
                                            if (resultList.Key < (Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result)
                                            {
                                                resultList.Value.Clear();
                                                resultList = new KeyValuePair<Inspection.RangeResult, List<string>>((Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result, new List<string>());
                                                resultList.Value.Add(strFunctionalSystemName + " (" + strSystemItemName + "): " + strDefectName);
                                            }
                                            else
                                            {
                                                if (resultList.Key == (Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result)
                                                    resultList.Value.Add(strFunctionalSystemName + " (" + strSystemItemName + "): " + strDefectName);
                                            }
                                            // ------------------

                                            switch ((Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result)
                                            {
                                                case Inspection.RangeResult.Green:
                                                    if (maxResult < Inspection.RangeResult.Green) maxResult = Inspection.RangeResult.Green;
                                                    dictColors[new KeyValuePair<int, int>(6, indexParams + 1)] = (MSWord.WdColor)52224;
                                                    break;
                                                case Inspection.RangeResult.Yellow:
                                                    if (maxResult < Inspection.RangeResult.Yellow) maxResult = Inspection.RangeResult.Yellow;
                                                    dictColors[new KeyValuePair<int, int>(7, indexParams + 1)] = MSWord.WdColor.wdColorYellow;
                                                    break;
                                                case Inspection.RangeResult.Orange:
                                                    if (maxResult < Inspection.RangeResult.Orange) maxResult = Inspection.RangeResult.Orange;
                                                    dictColors[new KeyValuePair<int, int>(8, indexParams + 1)] = MSWord.WdColor.wdColorLightOrange;
                                                    break;
                                                case Inspection.RangeResult.Red:
                                                    if (maxResult < Inspection.RangeResult.Red) maxResult = Inspection.RangeResult.Red;
                                                    dictColors[new KeyValuePair<int, int>(9, indexParams + 1)] = MSWord.WdColor.wdColorRed;
                                                    break;
                                            }
                                        }

                                        if (bFirstFuncSystem)
                                        {
                                            strTable += index.ToString() + chTab + strFunctionalSystemName + chTab + chTab + strSystemItemName + chTab + strDefectName + chTab + chTab + chTab + chTab + chN;
                                        }
                                        else
                                        {
                                            if (bFirstSystemItem)
                                            {
                                                strTable += "" + chTab + chTab + chTab + strSystemItemName + chTab + strDefectName + chTab + chTab + chTab + chTab + chN;
                                            }
                                            else
                                            {
                                                strTable += "" + chTab + chTab + chTab + chTab + strDefectName + chTab + chTab + chTab + chTab + chN;
                                            }
                                        }

                                        bFirstFuncSystem = false;
                                        bFirstSystemItem = false;
                                        if (first_row < 0) first_row = indexParams;
                                        indexParams++;
                                        bFind = true;
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < si.Value.m_listParameterResults.Count; j++)
                                {
                                    if (si.Value.m_listParameterResults[j].m_InspectionSubType == null
                                        || si.Value.m_listParameterResults[j].m_InspectionSubType == m_sub_type_id)
                                    {
                                        string strDefectName = "";// si.Value.m_listParameterResults[j].m_strDefectName;
                                        if (si.Value.m_listParameterResults[j].m_ParameterType == Inspection.ParameterType.Select /*&& si.Value.m_listParameterResults[j].m_strDefectName == ""*/)
                                            strDefectName = si.Value.m_listParameterResults[j].m_strParameterDescript;

                                        string strGreen = "";
                                        string strYellow = "";
                                        string strOrange = "";
                                        string strRed = "";
                                        string strDefect = "";

                                        switch (si.Value.m_listParameterResults[j].m_valSelect)
                                        {
                                            case 0:
                                                strDefect = si.Value.m_listParameterResults[j].m_strParameterSelect1;
                                                break;
                                            case 1:
                                                strDefect = si.Value.m_listParameterResults[j].m_strParameterSelect2;
                                                break;
                                            case 2:
                                                strDefect = si.Value.m_listParameterResults[j].m_strParameterSelect3;
                                                break;
                                            case 3:
                                                strDefect = si.Value.m_listParameterResults[j].m_strParameterSelect4;
                                                break;
                                        }

                                        if (si.Value.m_listParameterResults[j].m_Result != null)
                                        {
                                            // считаем дефекты, влияющие на результат
                                            if (resultList.Key < (Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result)
                                            {
                                                resultList.Value.Clear();
                                                resultList = new KeyValuePair<Inspection.RangeResult, List<string>>((Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result, new List<string>());
                                                resultList.Value.Add(strFunctionalSystemName + " (" + strSystemItemName + "): " + strDefect);
                                            }
                                            else
                                            {
                                                if (resultList.Key == (Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result)
                                                    resultList.Value.Add(strFunctionalSystemName + " (" + strSystemItemName + "): " + strDefect);
                                            }
                                            // ------------------

                                            switch ((Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result)
                                            {
                                                case Inspection.RangeResult.Green:
                                                    if (maxResult < Inspection.RangeResult.Green) maxResult = Inspection.RangeResult.Green;
                                                    strGreen = strDefect;
                                                    dictColors[new KeyValuePair<int, int>(6, indexParams + 1)] = (MSWord.WdColor)52224;
                                                    break;
                                                case Inspection.RangeResult.Yellow:
                                                    if (maxResult < Inspection.RangeResult.Yellow) maxResult = Inspection.RangeResult.Yellow;
                                                    strYellow = strDefect;
                                                    dictColors[new KeyValuePair<int, int>(7, indexParams + 1)] = MSWord.WdColor.wdColorYellow;
                                                    break;
                                                case Inspection.RangeResult.Orange:
                                                    if (maxResult < Inspection.RangeResult.Orange) maxResult = Inspection.RangeResult.Orange;
                                                    strOrange = strDefect;
                                                    dictColors[new KeyValuePair<int, int>(8, indexParams + 1)] = MSWord.WdColor.wdColorLightOrange;
                                                    break;
                                                case Inspection.RangeResult.Red:
                                                    if (maxResult < Inspection.RangeResult.Red) maxResult = Inspection.RangeResult.Red;
                                                    strRed = strDefect;
                                                    dictColors[new KeyValuePair<int, int>(9, indexParams + 1)] = MSWord.WdColor.wdColorRed;
                                                    break;
                                            }
                                        }

                                        if (bFirstFuncSystem)
                                        {
                                            strTable += index.ToString() + chTab + strFunctionalSystemName + chTab + chTab + strSystemItemName + chTab + strDefectName + chTab + strGreen + chTab + strYellow + chTab + strOrange + chTab + strRed + chN;
                                        }
                                        else
                                        {
                                            if (bFirstSystemItem)
                                            {
                                                strTable += "" + chTab + chTab + chTab + strSystemItemName + chTab + strDefectName + chTab + strGreen + chTab + strYellow + chTab + strOrange + chTab + strRed + chN;
                                            }
                                            else
                                            {
                                                strTable += "" + chTab + chTab + chTab + chTab + strDefectName + chTab + strGreen + chTab + strYellow + chTab + strOrange + chTab + strRed + chN;
                                            }
                                        }

                                        bFirstFuncSystem = false;
                                        bFirstSystemItem = false;
                                        bFind = true;
                                        if (first_row < 0) first_row = indexParams;
                                        indexParams++;
                                    }
                                }
                            }

                            if (listMergeCellsSystemItem.Count == 0)
                            {
                                if (first_row >= 0)
                                    listMergeCellsSystemItem.Add(new KeyValuePair<int, int>(first_row + 1, indexParams));
                            }
                            else
                                listMergeCellsSystemItem.Add(new KeyValuePair<int, int>(listMergeCellsSystemItem[listMergeCellsSystemItem.Count - 1].Value + 1, indexParams));
                        }

                        switch (maxResult)
                        {
                            case Inspection.RangeResult.Green:
                                dictColors[new KeyValuePair<int, int>(3, first_row + 1)] = (MSWord.WdColor)52224;
                                break;
                            case Inspection.RangeResult.Yellow:
                                dictColors[new KeyValuePair<int, int>(3, first_row + 1)] = MSWord.WdColor.wdColorYellow;
                                break;
                            case Inspection.RangeResult.Orange:
                                dictColors[new KeyValuePair<int, int>(3, first_row + 1)] = MSWord.WdColor.wdColorLightOrange;
                                break;
                            case Inspection.RangeResult.Red:
                                dictColors[new KeyValuePair<int, int>(3, first_row + 1)] = MSWord.WdColor.wdColorRed;
                                break;
                        }

                        m_listEquipments[0].m_listFunctionalSystem[i].m_dictResults[m_type] = maxResult;

                        if (first_row >= 0)
                            listMergeCells.Add(new KeyValuePair<int, int>(first_row + 1, indexParams));

                        if (bFind) index++;
                    }

                    tableIndex = 0;
                    switch (m_type)
                    {
                        case Inspection.InspectionType.FHA:
                            tableIndex = 7;
                            word.ConvertToTable(strTable, index, 9, true, "Карта_ФХА");
                            break;
                        case Inspection.InspectionType.HARG:
                            tableIndex = 6;
                            word.ConvertToTable(strTable, index, 9, true, "Карта_ХАРГ");
                            break;
                        case Inspection.InspectionType.Vibro:
                            tableIndex = 9;
                            word.ConvertToTable(strTable, index, 9, true, "Карта_вибрационное");
                            break;
                        case Inspection.InspectionType.Visual:
                            tableIndex = 5;
                            word.ConvertToTable(strTable, index, 9, true, "Карта_визуальное");
                            break;
                        case Inspection.InspectionType.Warm:
                            tableIndex = 8;
                            word.ConvertToTable(strTable, index, 9, true, "Карта_тепловизионное");
                            break;
                    }

                    foreach (KeyValuePair<KeyValuePair<int, int>, MSWord.WdColor> col in dictColors)
                    {
                        word.SetCellColor(col.Key.Key, col.Key.Value, col.Value, MSWord.WdColor.wdColorAutomatic, tableIndex);
                    }

                    word.SetCellColor(6, 1, (MSWord.WdColor)52224, MSWord.WdColor.wdColorAutomatic, tableIndex);
                    word.SetCellColor(7, 1, MSWord.WdColor.wdColorYellow, MSWord.WdColor.wdColorAutomatic, tableIndex);
                    word.SetCellColor(8, 1, MSWord.WdColor.wdColorLightOrange, MSWord.WdColor.wdColorAutomatic, tableIndex);
                    word.SetCellColor(9, 1, MSWord.WdColor.wdColorRed, MSWord.WdColor.wdColorAutomatic, tableIndex);

                    word.SetColumnWidth(1, 9.4f, tableIndex);
                    word.SetColumnWidth(2, 32.5f, tableIndex);
                    word.SetColumnWidth(3, 20f, tableIndex);
                    word.SetColumnWidth(4, 32.5f, tableIndex);
                    word.SetColumnWidth(5, 67.5f, tableIndex);
                    word.SetColumnWidth(6, 25f, tableIndex);
                    word.SetColumnWidth(7, 25f, tableIndex);
                    word.SetColumnWidth(8, 25f, tableIndex);
                    word.SetColumnWidth(9, 25f, tableIndex);

                    word.SetCellsFont(1, 9, 1, 1, "", 1, -1, tableIndex);
                    word.SetTableAlignment(MSWord.WdParagraphAlignment.wdAlignParagraphCenter, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, tableIndex);

                    for (int i = listMergeCellsSystemItem.Count - 1; i >= 0; i--)
                    {
                        if (listMergeCellsSystemItem[i].Key < listMergeCellsSystemItem[i].Value)
                        {
                            word.SetCellsMerge(4, 4, listMergeCellsSystemItem[i].Key, listMergeCellsSystemItem[i].Value, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, tableIndex);
                        }
                    }

                    for (int i = listMergeCells.Count - 1; i >= 0; i--)
                    {
                        if (listMergeCells[i].Key < listMergeCells[i].Value)
                        {
                            word.SetCellsMerge(3, 3, listMergeCells[i].Key, listMergeCells[i].Value, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, tableIndex);
                            word.SetCellsMerge(2, 2, listMergeCells[i].Key, listMergeCells[i].Value, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, tableIndex);
                            word.SetCellsMerge(1, 1, listMergeCells[i].Key, listMergeCells[i].Value, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, tableIndex);
                        }
                    }

                    worker.ReportProgress(20 + (int)(70 * gl_index / (float)AllCnt));
                }

                worker.ReportProgress(90);

                // формируем общую таблицу
                string strTableCommon = "№ пп" + chTab + "Функциональная система (узел) трансформатора" + chTab + "Материал, сборочный узел, деталь" +
                    chTab + "Визуальный осмотр" + chTab + "ХАРГ" + chTab + "ФХА" + chTab + "Тепловизионное обследование" + chTab + "Вибрационное обследование" + chTab + "Техническое состояние" + chN;

                indexParams = 1;
                first_row = -1;
                dictColors.Clear();
                listMergeCells.Clear();

                //Inspection.RangeResult common_max_result = Inspection.RangeResult.None;


                for (int i = 0; i < m_listEquipments[0].m_listFunctionalSystem.Count; i++)
                {
                    Dictionary<string, Dictionary<Inspection.InspectionType, long?>> dictSystemItems = new Dictionary<string, Dictionary<Inspection.InspectionType, long?>>();
                    //foreach (Inspection.InspectionType m_type in Enum.GetValues(typeof(Inspection.InspectionType)))
                    for (int ii = 0; ii < Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[0].m_EquipmentKindID].Count; ii++)
                    {
                        Inspection.InspectionType m_type = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[0].m_EquipmentKindID][ii];
                        long m_sub_type_id = m_listEquipments[0].m_listFunctionalSystem[i].m_dictSubTypes[m_type];

                        if (m_listEquipments[0].m_listFunctionalSystem[i].m_dictUnionMainID[m_type].ContainsKey(m_sub_type_id))
                        {
                            long MainFunctionalSystemID = m_listEquipments[0].m_listFunctionalSystem[i].m_dictUnionMainID[m_type][m_sub_type_id];

                            if (m_listEquipments[0].m_dictFunctionalSystemPos.ContainsKey(MainFunctionalSystemID))
                            {
                                foreach (KeyValuePair<string, ReportInfo.SystemItem> si in m_listEquipments[0].m_listFunctionalSystem[m_listEquipments[0].m_dictFunctionalSystemPos[MainFunctionalSystemID]].m_dictSystemItems[m_type])
                                {
                                    // название подузла заменяем названием узла
                                    if (!dictSystemItems.ContainsKey(m_listEquipments[0].m_listFunctionalSystem[i].m_strFunctionalSystemName)) dictSystemItems[m_listEquipments[0].m_listFunctionalSystem[i].m_strFunctionalSystemName] = new Dictionary<Inspection.InspectionType, long?>();
                                    dictSystemItems[m_listEquipments[0].m_listFunctionalSystem[i].m_strFunctionalSystemName][m_type] = si.Value.m_Result;
                                }
                            }
                        }
                        else
                        {
                            foreach (KeyValuePair<string, ReportInfo.SystemItem> si in m_listEquipments[0].m_listFunctionalSystem[i].m_dictSystemItems[m_type])
                            {
                                if (!dictSystemItems.ContainsKey(si.Key)) dictSystemItems[si.Key] = new Dictionary<Inspection.InspectionType, long?>();
                                dictSystemItems[si.Key][m_type] = si.Value.m_Result;
                            }
                        }
                    }

                    bool bFirstSystemItem = true;
                    first_row = -1;

                    foreach (KeyValuePair<string, Dictionary<Inspection.InspectionType, long?>> si in dictSystemItems)
                    {
                        Inspection.RangeResult max_result = Inspection.RangeResult.None;

                        //foreach (Inspection.InspectionType m_type in Enum.GetValues(typeof(Inspection.InspectionType)))
                        for (int ii = 0; ii < Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[0].m_EquipmentKindID].Count; ii++)
                        {
                            Inspection.InspectionType m_type = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[0].m_EquipmentKindID][ii];
                            int col_index = 0;
                            switch (m_type)
                            {
                                case Inspection.InspectionType.Visual:
                                    col_index = 4;
                                    break;
                                case Inspection.InspectionType.HARG:
                                    col_index = 5;
                                    break;
                                case Inspection.InspectionType.FHA:
                                    col_index = 6;
                                    break;
                                case Inspection.InspectionType.Warm:
                                    col_index = 7;
                                    break;
                                case Inspection.InspectionType.Vibro:
                                    col_index = 8;
                                    break;
                            }

                            if (si.Value.ContainsKey(m_type) && si.Value[m_type] != null)
                            {
                                if (max_result < (Inspection.RangeResult)si.Value[m_type])
                                    max_result = (Inspection.RangeResult)si.Value[m_type];

                                //if (common_max_result < (Inspection.RangeResult)si.Value[m_type])
                                //    common_max_result = (Inspection.RangeResult)si.Value[m_type];                                

                                switch ((Inspection.RangeResult)si.Value[m_type])
                                {
                                    case Inspection.RangeResult.Green:
                                        dictColors[new KeyValuePair<int, int>(col_index, indexParams + 1)] = (MSWord.WdColor)52224;
                                        break;
                                    case Inspection.RangeResult.Yellow:
                                        dictColors[new KeyValuePair<int, int>(col_index, indexParams + 1)] = MSWord.WdColor.wdColorYellow;
                                        break;
                                    case Inspection.RangeResult.Orange:
                                        dictColors[new KeyValuePair<int, int>(col_index, indexParams + 1)] = MSWord.WdColor.wdColorLightOrange;
                                        break;
                                    case Inspection.RangeResult.Red:
                                        dictColors[new KeyValuePair<int, int>(col_index, indexParams + 1)] = MSWord.WdColor.wdColorRed;
                                        break;
                                }
                            }
                        }

                        if (max_result > Inspection.RangeResult.None)
                        {
                            int col_index = 9;

                            switch (max_result)
                            {
                                case Inspection.RangeResult.Green:
                                    dictColors[new KeyValuePair<int, int>(col_index, indexParams + 1)] = (MSWord.WdColor)52224;
                                    break;
                                case Inspection.RangeResult.Yellow:
                                    dictColors[new KeyValuePair<int, int>(col_index, indexParams + 1)] = MSWord.WdColor.wdColorYellow;
                                    break;
                                case Inspection.RangeResult.Orange:
                                    dictColors[new KeyValuePair<int, int>(col_index, indexParams + 1)] = MSWord.WdColor.wdColorLightOrange;
                                    break;
                                case Inspection.RangeResult.Red:
                                    dictColors[new KeyValuePair<int, int>(col_index, indexParams + 1)] = MSWord.WdColor.wdColorRed;
                                    break;
                            }
                        }

                        if (bFirstSystemItem)
                            strTableCommon += (i + 1).ToString() + chTab + m_listEquipments[0].m_listFunctionalSystem[i].m_strFunctionalSystemName + chTab + si.Key + chTab + chTab + chTab + chTab + chTab + chTab + chN;
                        else
                            strTableCommon += "" + chTab + chTab + si.Key + chTab + chTab + chTab + chTab + chTab + chTab + chN;

                        if (first_row < 0) first_row = indexParams;

                        bFirstSystemItem = false;
                        indexParams++;
                    }

                    if (first_row >= 0)
                        listMergeCells.Add(new KeyValuePair<int, int>(first_row + 1, indexParams));
                }

                word.ConvertToTable(strTableCommon, indexParams, 9, true, "Карта_общая");

                tableIndex = 10;

                word.SetColumnWidth(1, 9.4f, tableIndex);
                word.SetColumnWidth(2, 42.5f, tableIndex);
                word.SetColumnWidth(3, 35f, tableIndex);
                word.SetColumnWidth(4, 29f, tableIndex);
                word.SetColumnWidth(5, 29f, tableIndex);
                word.SetColumnWidth(6, 29f, tableIndex);
                word.SetColumnWidth(7, 29f, tableIndex);
                word.SetColumnWidth(8, 29f, tableIndex);
                word.SetColumnWidth(9, 29f, tableIndex);

                word.SetCellsFont(1, 9, 1, 1, "", 1, -1, tableIndex);
                word.SetTableAlignment(MSWord.WdParagraphAlignment.wdAlignParagraphCenter, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, tableIndex);

                foreach (KeyValuePair<KeyValuePair<int, int>, MSWord.WdColor> col in dictColors)
                {
                    word.SetCellColor(col.Key.Key, col.Key.Value, col.Value, MSWord.WdColor.wdColorAutomatic, tableIndex);
                }

                for (int i = listMergeCells.Count - 1; i >= 0; i--)
                {
                    if (listMergeCells[i].Key < listMergeCells[i].Value)
                    {
                        word.SetCellsMerge(2, 2, listMergeCells[i].Key, listMergeCells[i].Value, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, tableIndex);
                        word.SetCellsMerge(1, 1, listMergeCells[i].Key, listMergeCells[i].Value, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, tableIndex);
                    }
                }


                bookmarks.Clear();
                switch (resultList.Key)
                {
                    case Inspection.RangeResult.Green:
                        bookmarks.Add(new KeyValuePair<string, string>("Заключение", "По результатам проведенной экспресс-оценки, техническое состояние силового трансформатора " 
                            + m_listEquipments[0].m_dictFields["EquipmentName"] + " характеризуется как работоспособное, исправное состояние, при котором все характеристики соответствуют требованиям нормативной документации"));
                        word.SetCellColor(3, 2, (MSWord.WdColor)52224, MSWord.WdColor.wdColorAutomatic, 11);
                        break;
                    case Inspection.RangeResult.Yellow:
                        bookmarks.Add(new KeyValuePair<string, string>("Заключение", "По результатам проведенной экспресс-оценки, техническое состояние силового трансформатора "
                            + m_listEquipments[0].m_dictFields["EquipmentName"] + " характеризуется как работоспособное, неисправное состояние, при котором эксплуатация оборудования возможна без ограничений режима эксплуатации"));
                        word.SetCellColor(3, 2, MSWord.WdColor.wdColorYellow, MSWord.WdColor.wdColorAutomatic, 11);
                        break;
                    case Inspection.RangeResult.Orange:
                        bookmarks.Add(new KeyValuePair<string, string>("Заключение", "По результатам проведенной экспресс-оценки, техническое состояние силового трансформатора "
                            + m_listEquipments[0].m_dictFields["EquipmentName"] + " характеризуется как работоспособное, неисправное состояние, при котором эксплуатация возможна с учащенным контролем оборудования до устранения дефектов"));
                        word.SetCellColor(3, 2, MSWord.WdColor.wdColorLightOrange, MSWord.WdColor.wdColorAutomatic, 11);
                        break;
                    case Inspection.RangeResult.Red:
                        bookmarks.Add(new KeyValuePair<string, string>("Заключение", "По результатам проведенной экспресс-оценки, техническое состояние силового трансформатора "
                            + m_listEquipments[0].m_dictFields["EquipmentName"] + " характеризуется как предаварийное"));
                        word.SetCellColor(3, 2, MSWord.WdColor.wdColorRed, MSWord.WdColor.wdColorAutomatic, 11);
                        break;
                }

                for (int i = 0; i < resultList.Value.Count; i++)
                {
                    if (i == 0)
                        word.SetTextInCell(4, 2, resultList.Value[i], 11);
                    else
                        word.SetTextInCell(4, 2, chN + resultList.Value[i], 11);
                }

                if (bookmarks.Count > 0)
                {
                    word.SetBookmarkText(bookmarks);
                }

                worker.ReportProgress(100);

                word.SetVisible(true);
                word.DestroyWord();
            }
            catch (Exception ex)
            {
                word.DestroyWord();
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        class DefectInfo
        {
            public DefectInfo(string strDefectName, Inspection.RangeResult? result, string strDefectDescript, bool bEnable)
            {
                m_strDefectName = strDefectName;
                m_result = result;
                m_strDefectDescript = strDefectDescript;
                m_bEnable = bEnable;
            }

            public string m_strDefectName;
            public Inspection.RangeResult? m_result;
            public string m_strDefectDescript;
            public bool m_bEnable;
        }

        class SystemItemShortInfo
        {
            public SystemItemShortInfo(string strName, bool bEnable)
            {
                m_strName = strName;
                m_bEnable = bEnable;
                m_defects = new List<DefectInfo>();
                m_strNote = "";
            }
            public string m_strName;
            public bool m_bEnable;
            public List<DefectInfo> m_defects;
            public string m_strNote;
        }

        class FunctionalSystemShortInfo
        {
            public FunctionalSystemShortInfo(string strName)
            {
                m_strName = strName;
                m_systemitems = new List<SystemItemShortInfo>();
            }
            public string m_strName;
            public List<SystemItemShortInfo> m_systemitems;
        }

        class ColorInfo
        {
            public ColorInfo(MSWord.WdColor textColor, MSWord.WdColor bkColor)
            {
                m_textColor = textColor;
                m_bkColor = bkColor;
            }

            public MSWord.WdColor m_textColor;
            public MSWord.WdColor m_bkColor;
        }

        class EquipmentResultsDefectInfo
        {
            public class DefectInfo
            {
                public DefectInfo(Inspection.RangeResult result, string strDefectInfo)
                {
                    m_Result = result;
                    m_strDefectInfo = strDefectInfo;
                }
                public Inspection.RangeResult m_Result;
                public string m_strDefectInfo;
            };
            
            public Inspection.RangeResult  m_maxResult = Inspection.RangeResult.None;
            public List<DefectInfo>        m_listDefects = new List<DefectInfo>();
        }

        private void PrintReport(Inspection.ReportType reportType)
        {
            worker.ReportProgress(5);

            Word word = new Word();

            //try
            {
                List<KeyValuePair<string, string>> bookmarks = new List<KeyValuePair<string, string>>();

                for (int i = 0; i < m_listEquipments.Count; i++)
                {
                    if (!ReportInfo.GetData(m_CheckID, m_listEquipments, m_dictCommonSubTypes, m_list_sub_types, i, true))
                        return;

                    worker.ReportProgress(5 + (int)(10 * (i + 1) / (float)m_listEquipments.Count));
                }

                worker.ReportProgress(15);

                if (!word.Start(AppDomain.CurrentDomain.BaseDirectory + "/Reports/Отчет по техническому состоянию.dot")) return;

                List<Dictionary<Inspection.InspectionType, Inspection.RangeResult?>> listInspectionResults = new List<Dictionary<Inspection.InspectionType, Inspection.RangeResult?>>();
                List<EquipmentResultsDefectInfo> listEquipmentResultsDefects = new List<EquipmentResultsDefectInfo>();
                Dictionary<Inspection.InspectionType, List<Dictionary<long, List<FunctionalSystemShortInfo>>>> listData = new Dictionary<Inspection.InspectionType, List<Dictionary<long, List<FunctionalSystemShortInfo>>>>();

                // для listInspectionResults и listEquipmentResultsDefects не делаем зависимость от m_sub_type_id, т.к. два результата от разных подобследований обдновременно невозможны

                // получаем по каждому трансформатору список критичных дефектов
                for (int k = 0; k < m_listEquipments.Count; k++)
                {
                    listEquipmentResultsDefects.Add(new EquipmentResultsDefectInfo());
                    //listEquipmentResultsDefects[k] = new KeyValuePair<Inspection.RangeResult, List<KeyValuePair<Inspection.RangeResult, string>>>(Inspection.RangeResult.None, new List<KeyValuePair<Inspection.RangeResult, string>>());

                    listInspectionResults.Add(new Dictionary<Inspection.InspectionType, Inspection.RangeResult?>());

                    //foreach (Inspection.InspectionType m_type in Enum.GetValues(typeof(Inspection.InspectionType)))
                    for (int ii = 0; ii < Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[k].m_EquipmentKindID].Count; ii++)
                    {
                        Inspection.InspectionType m_type = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[k].m_EquipmentKindID][ii];
                        // для хранения данных
                        if (!listData.ContainsKey(m_type))
                            listData[m_type] = new List<Dictionary<long, List<FunctionalSystemShortInfo>>>();
                        listData[m_type].Add(new Dictionary<long, List<FunctionalSystemShortInfo>>());

                        listInspectionResults[k][m_type] = Inspection.RangeResult.None;

                        for (int z = 0; z < m_dictCommonSubTypes[m_type].Count; z++)
                        {
                            long m_sub_type_id = 0;

                            if (m_dictCommonSubTypes[m_type][z] != null)
                                m_sub_type_id = (long)m_dictCommonSubTypes[m_type][z];

                            listData[m_type][k][m_sub_type_id] = new List<FunctionalSystemShortInfo>();

                            for (int i = 0; i < m_listEquipments[k].m_listFunctionalSystem.Count; i++)
                            {
                                string strFunctionalSystemName = m_listEquipments[k].m_listFunctionalSystem[i].m_strFunctionalSystemName;

                                long m_sub_type_id_ = m_listEquipments[k].m_listFunctionalSystem[i].m_dictSubTypes[m_type];

                                if (m_listEquipments[k].m_listFunctionalSystem[i].m_dictUnionMainID[m_type].ContainsKey(m_sub_type_id)) continue;

                                bool bUnion = false;
                                if (m_listEquipments[k].m_listFunctionalSystem[i].m_dictUnionExtraID[m_type].ContainsKey(m_sub_type_id))
                                {
                                    strFunctionalSystemName = m_listEquipments[k].m_listFunctionalSystem[i].m_strFunctionalSystemName;
                                    for (int j = 0; j < m_listEquipments[k].m_listFunctionalSystem[i].m_dictUnionExtraID[m_type][m_sub_type_id].Count; j++)
                                    {
                                        long ExtraFunctionalSystemID = m_listEquipments[k].m_listFunctionalSystem[i].m_dictUnionExtraID[m_type][m_sub_type_id][j];
                                        if (m_listEquipments[k].m_dictFunctionalSystemPos.ContainsKey(ExtraFunctionalSystemID))
                                        {
                                            strFunctionalSystemName = strFunctionalSystemName + ", " + m_listEquipments[k].m_listFunctionalSystem[m_listEquipments[k].m_dictFunctionalSystemPos[ExtraFunctionalSystemID]].m_strFunctionalSystemName;
                                            bUnion = true;
                                        }
                                    }
                                }

                                //if (m_sub_type_id == 0 && m_dictCommonSubTypes[m_type][0] != null) m_sub_type_id = (long)m_dictCommonSubTypes[m_type][0]; // на всякий случай, если нет данных, то берется первое подобследование

                                // для хранения данных
                                listData[m_type][k][m_sub_type_id].Add(new FunctionalSystemShortInfo(strFunctionalSystemName));

                                Inspection.RangeResult maxResult = Inspection.RangeResult.None;

                                foreach (KeyValuePair<string, ReportInfo.SystemItem> si in m_listEquipments[k].m_listFunctionalSystem[i].m_dictSystemItems[m_type])
                                {
                                    string strSystemItemName = si.Key;
                                    if (bUnion && m_listEquipments[k].m_listFunctionalSystem[i].m_dictSystemItems[m_type].Count == 1)
                                        strSystemItemName = strFunctionalSystemName;

                                    int iFuncIndex = listData[m_type][k][m_sub_type_id].Count - 1;

                                    listData[m_type][k][m_sub_type_id][iFuncIndex].m_systemitems.Add(new SystemItemShortInfo(strSystemItemName, si.Value.m_bEnable));

                                    int iSystemItemIndex = listData[m_type][k][m_sub_type_id][iFuncIndex].m_systemitems.Count - 1;

                                    // переносим примечание
                                    listData[m_type][k][m_sub_type_id][iFuncIndex].m_systemitems[iSystemItemIndex].m_strNote = si.Value.m_strNote;

                                    // если в подузле помимо дефектов, если еще и параметры типа Список и у подузла нет алгоритма расчета, то эти параметры надо включаться в дефекты
                                    if (si.Value.m_listDefectResults.Count > 0 && si.Value.m_listParameterResults.Count > 0 && si.Value.m_strAlg == "")
                                    {
                                        for (int kk = si.Value.m_listParameterResults.Count - 1; kk >= 0; kk--)
                                        {
                                            if (si.Value.m_listParameterResults[kk].m_ParameterType == Inspection.ParameterType.Select)
                                            {
                                                string strDefect = "";

                                                switch (si.Value.m_listParameterResults[kk].m_valSelect)
                                                {
                                                    case 1:
                                                        strDefect = si.Value.m_listParameterResults[kk].m_strParameterSelect2;//m_strParameterSelect2;
                                                        break;
                                                    case 2:
                                                        strDefect = si.Value.m_listParameterResults[kk].m_strParameterSelect3;//m_strParameterSelect3;
                                                        break;
                                                    case 3:
                                                        strDefect = si.Value.m_listParameterResults[kk].m_strParameterSelect4;//m_strParameterSelect4;
                                                        break;
                                                }

                                                // если уже есть дефект с таким наименованием, то ставится макс цвет
                                                bool bFind = false;
                                                for (int kkk = 0; kkk < si.Value.m_listDefectResults.Count; kkk++)
                                                {
                                                    if (si.Value.m_listDefectResults[kkk].m_strDefectName == si.Value.m_listParameterResults[kk].m_strParameterDescript)
                                                    {
                                                        long? res1 = si.Value.m_listDefectResults[kkk].m_Result;
                                                        long? res2 = si.Value.m_listParameterResults[kk].m_Result;
                                                        if (res1 == null || res1 != null && res2 != null && res1 < res2)
                                                        {
                                                            si.Value.m_listDefectResults[kkk].m_Result = res2;
                                                            //si.Value.m_listDefectResults[kkk].m_strDefectDescript = strDefect;
                                                        }
                                                        bFind = true;
                                                        break;
                                                    }
                                                }

                                                if (!bFind)
                                                {
                                                    ReportInfo.DefectResult df = new ReportInfo.DefectResult(-1, si.Value.m_listParameterResults[kk].m_strParameterDescript, si.Value.m_listParameterResults[kk].m_InspectionSubType);
                                                    df.m_Result = si.Value.m_listParameterResults[kk].m_Result;
                                                    df.m_strDefectDescript = strDefect;
                                                    si.Value.m_listDefectResults.Add(df);
                                                }

                                                si.Value.m_listParameterResults.RemoveAt(kk);
                                            }
                                        }
                                    }
                                    //---------------

                                    if (si.Value.m_listDefectResults.Count > 0)
                                    {
                                        for (int j = 0; j < si.Value.m_listDefectResults.Count; j++)
                                        {
                                            bool bEnable = true;
                                            // выключаем узлы, в которых нет результата
                                            /*if (m_sub_type_id_ != m_sub_type_id)
                                            {
                                                bEnable = false;
                                            }*/

                                            if (si.Value.m_listDefectResults[j].m_InspectionSubType == null
                                                || si.Value.m_listDefectResults[j].m_InspectionSubType == m_sub_type_id)
                                            {
                                                string strDefectName = si.Value.m_listDefectResults[j].m_strDefectName;

                                                if (si.Value.m_listDefectResults[j].m_Result != null)
                                                {
                                                    // считаем дефекты, влияющие на результат
                                                    /*if (listEquipmentResultsDefects[k].m_maxResult < (Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result)
                                                    {
                                                        listEquipmentResultsDefects[k].Value.Clear();
                                                        listEquipmentResultsDefects[k] = new KeyValuePair<Inspection.RangeResult, List<KeyValuePair<Inspection.RangeResult, string>>>((Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result, new List<KeyValuePair<Inspection.RangeResult, string>>());
                                                        listEquipmentResultsDefects[k].Value.Add(new KeyValuePair<Inspection.RangeResult, string>((Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result, strFunctionalSystemName + " (" + strSystemItemName + "): " + strDefectName));
                                                    }
                                                    else
                                                    {
                                                        if (listEquipmentResultsDefects[k].Key == (Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result)
                                                            listEquipmentResultsDefects[k].Value.Add(new KeyValuePair<Inspection.RangeResult, string>((Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result, strFunctionalSystemName + " (" + strSystemItemName + "): " + strDefectName));
                                                    }*/

                                                    if ((Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result > Inspection.RangeResult.Green)
                                                    {
                                                        listEquipmentResultsDefects[k].m_listDefects.Add(new EquipmentResultsDefectInfo.DefectInfo((Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result, strFunctionalSystemName + " (" + strSystemItemName + "): " + strDefectName));
                                                        if (listEquipmentResultsDefects[k].m_maxResult < (Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result)
                                                            listEquipmentResultsDefects[k].m_maxResult = (Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result;
                                                    }

                                                    switch ((Inspection.RangeResult)si.Value.m_listDefectResults[j].m_Result)
                                                    {
                                                        case Inspection.RangeResult.Green:
                                                            if (maxResult < Inspection.RangeResult.Green) maxResult = Inspection.RangeResult.Green;
                                                            break;
                                                        case Inspection.RangeResult.Yellow:
                                                            if (maxResult < Inspection.RangeResult.Yellow) maxResult = Inspection.RangeResult.Yellow;
                                                            break;
                                                        case Inspection.RangeResult.Orange:
                                                            if (maxResult < Inspection.RangeResult.Orange) maxResult = Inspection.RangeResult.Orange;
                                                            break;
                                                        case Inspection.RangeResult.Red:
                                                            if (maxResult < Inspection.RangeResult.Red) maxResult = Inspection.RangeResult.Red;
                                                            break;
                                                    }
                                                }

                                                //int iFuncIndex = listData[m_type][k][m_sub_type_id].Count - 1;
                                                //int iSystemItemIndex = listData[m_type][k][m_sub_type_id][iFuncIndex].m_systemitems.Count - 1;
                                                listData[m_type][k][m_sub_type_id][iFuncIndex].m_systemitems[iSystemItemIndex].m_defects.Add(
                                                    new DefectInfo(strDefectName, (Inspection.RangeResult?)si.Value.m_listDefectResults[j].m_Result, si.Value.m_listDefectResults[j].m_strDefectDescript, bEnable));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < si.Value.m_listParameterResults.Count; j++)
                                        {
                                            bool bEnable = si.Value.m_listParameterResults[j].m_bEnable;
                                            // выключаем узлы, в которых нет результата
                                            /*if (m_sub_type_id_ != m_sub_type_id)
                                            {
                                                bEnable = false;
                                            }*/

                                            if (si.Value.m_listParameterResults[j].m_InspectionSubType == null
                                                || si.Value.m_listParameterResults[j].m_InspectionSubType == m_sub_type_id)
                                            {
                                                string strDefectName = si.Value.m_listParameterResults[j].m_strParameterDescript;
                                                string strDefect = "";

                                                switch (si.Value.m_listParameterResults[j].m_valSelect)
                                                {
                                                    //case 0:
                                                    //    strDefect = si.Value.m_listParameterResults[j].m_strParameterDescript;//.m_strParameterSelect1;
                                                    //    break;
                                                    case 1:
                                                        strDefect = si.Value.m_listParameterResults[j].m_strParameterSelect2;//m_strParameterSelect2;
                                                        break;
                                                    case 2:
                                                        strDefect = si.Value.m_listParameterResults[j].m_strParameterSelect3;//m_strParameterSelect3;
                                                        break;
                                                    case 3:
                                                        strDefect = si.Value.m_listParameterResults[j].m_strParameterSelect4;//m_strParameterSelect4;
                                                        break;
                                                }

                                                if (si.Value.m_listParameterResults[j].m_Result != null)
                                                {
                                                    // считаем дефекты, влияющие на результат
                                                    /*if (listEquipmentResultsDefects[k].Key < (Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result)
                                                    {
                                                        listEquipmentResultsDefects[k].Value.Clear();
                                                        listEquipmentResultsDefects[k] = new KeyValuePair<Inspection.RangeResult, List<KeyValuePair<Inspection.RangeResult, string>>>((Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result, new List<KeyValuePair<Inspection.RangeResult, string>>());
                                                        listEquipmentResultsDefects[k].Value.Add(new KeyValuePair<Inspection.RangeResult, string>((Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result, strFunctionalSystemName + " (" + strSystemItemName + "): " + strDefect));
                                                    }
                                                    else
                                                    {
                                                        if (listEquipmentResultsDefects[k].Key == (Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result)
                                                            listEquipmentResultsDefects[k].Value.Add(new KeyValuePair<Inspection.RangeResult, string>((Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result, strFunctionalSystemName + " (" + strSystemItemName + "): " + strDefect));
                                                    }*/

                                                    if ((Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result > Inspection.RangeResult.Green)
                                                    {
                                                        listEquipmentResultsDefects[k].m_listDefects.Add(new EquipmentResultsDefectInfo.DefectInfo((Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result, strFunctionalSystemName + " (" + strSystemItemName + "): " + strDefect));
                                                        if (listEquipmentResultsDefects[k].m_maxResult < (Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result)
                                                            listEquipmentResultsDefects[k].m_maxResult = (Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result;
                                                    }
                                                    // ------------------

                                                    switch ((Inspection.RangeResult)si.Value.m_listParameterResults[j].m_Result)
                                                    {
                                                        case Inspection.RangeResult.Green:
                                                            if (maxResult < Inspection.RangeResult.Green) maxResult = Inspection.RangeResult.Green;
                                                            //strGreen = strDefect;
                                                            break;
                                                        case Inspection.RangeResult.Yellow:
                                                            if (maxResult < Inspection.RangeResult.Yellow) maxResult = Inspection.RangeResult.Yellow;
                                                            //strYellow = strDefect;
                                                            break;
                                                        case Inspection.RangeResult.Orange:
                                                            if (maxResult < Inspection.RangeResult.Orange) maxResult = Inspection.RangeResult.Orange;
                                                            //strOrange = strDefect;
                                                            break;
                                                        case Inspection.RangeResult.Red:
                                                            if (maxResult < Inspection.RangeResult.Red) maxResult = Inspection.RangeResult.Red;
                                                            //strRed = strDefect;
                                                            break;
                                                    }
                                                }

                                                //int iFuncIndex = listData[m_type][k][m_sub_type_id].Count - 1;
                                                //int iSystemItemIndex = listData[m_type][k][m_sub_type_id][iFuncIndex].m_systemitems.Count - 1;
                                                listData[m_type][k][m_sub_type_id][iFuncIndex].m_systemitems[iSystemItemIndex].m_defects.Add(
                                                    new DefectInfo(strDefectName, (Inspection.RangeResult?)si.Value.m_listParameterResults[j].m_Result, strDefect, bEnable));

                                            }
                                        }
                                    }

                                    // удаляем пустые узлы
                                    int iFuncIndex_ = listData[m_type][k][m_sub_type_id].Count - 1;
                                    int iSystemItemIndex_ = listData[m_type][k][m_sub_type_id][iFuncIndex_].m_systemitems.Count - 1;
                                    if (listData[m_type][k][m_sub_type_id][iFuncIndex_].m_systemitems[iSystemItemIndex_].m_defects.Count == 0)
                                    {
                                        listData[m_type][k][m_sub_type_id][iFuncIndex_].m_systemitems.RemoveAt(iSystemItemIndex_);
                                    }
                                }

                                // удаляем пустые узлы
                                int iFuncIndex__ = listData[m_type][k][m_sub_type_id].Count - 1;
                                if (listData[m_type][k][m_sub_type_id][iFuncIndex__].m_systemitems.Count == 0)
                                {
                                    listData[m_type][k][m_sub_type_id].RemoveAt(iFuncIndex__);
                                }

                                m_listEquipments[k].m_listFunctionalSystem[i].m_dictResults[m_type] = maxResult;

                                if (listInspectionResults[k][m_type] < maxResult)
                                    listInspectionResults[k][m_type] = maxResult;
                            }
                        }
                    }

                    worker.ReportProgress(15 + (int)(5 * (k + 1) / (float)m_listEquipments.Count));
                }

                worker.ReportProgress(20);

                // ---------------------------------------------------------------------------
                // ------------ Оценка технического состояния 
                // ---------------------------------------------------------------------------
                int iCntRows = m_listEquipments.Count;
                for (int i = 0; i < m_listEquipments.Count; i++)
                {
                    if (listEquipmentResultsDefects[i].m_listDefects.Count > 0)
                        iCntRows += listEquipmentResultsDefects[i].m_listDefects.Count - 1;
                }

                word.InsertRowsInTable(2, iCntRows);

                word.SetCellsColor(1, 5, 2, 1 + iCntRows, MSWord.WdColor.wdColorAutomatic, MSWord.WdColor.wdColorAutomatic, 2);
                word.SetCellsAlignment(5, 5, 2, 1 + iCntRows, MSWord.WdParagraphAlignment.wdAlignParagraphLeft, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, 2);
                word.SetCellsFont(1, 5, 2, 1 + iCntRows, "", 0, -1, 2);

                char chTab = (char)9;
                char chN = (char)13;

                //string strTransfDefects = "";
                int row = 2;
                Dictionary<string, List<string>> dictEquipDefects = new Dictionary<string, List<string>>();
                for (int i = 0; i < m_listEquipments.Count; i++)
                {
                    word.SetTextInCell(1, row, (i + 1).ToString(), 2);
                    word.SetTextInCell(2, row, m_listEquipments[i].m_dictFields["EquipmentName"], 2);
                    word.SetTextInCell(3, row, m_listEquipments[i].m_dictFields["EquipmentTypeName"], 2);

                    string strInfo = "";
                    switch (listEquipmentResultsDefects[i].m_maxResult)
                    {
                        case Inspection.RangeResult.Green:
                            word.SetCellColor(4, row, (MSWord.WdColor)52224, MSWord.WdColor.wdColorAutomatic, 2);

                            //if (strTransfDefects != "") strTransfDefects += ";" + chN;
                            //strTransfDefects += "- № " + m_listEquipments[i].m_dictFields["EquipmentName"] + " характеризуется как работоспособное, исправное состояние, при котором все характеристики соответствуют требованиям нормативной документации";
                            strInfo = " оценивается как исправное";
                            break;
                        case Inspection.RangeResult.Yellow:
                            word.SetCellColor(4, row, MSWord.WdColor.wdColorYellow, MSWord.WdColor.wdColorAutomatic, 2);

                            //if (strTransfDefects != "") strTransfDefects += ";" + chN;
                            //strTransfDefects += "- № " + m_listEquipments[i].m_dictFields["EquipmentName"] + " характеризуется как работоспособное, неисправное состояние, при котором эксплуатация оборудования возможна без ограничений режима эксплуатации";
                            strInfo = " оценивается как работоспособное";
                            break;
                        case Inspection.RangeResult.Orange:
                            word.SetCellColor(4, row, MSWord.WdColor.wdColorLightOrange, MSWord.WdColor.wdColorAutomatic, 2);

                            //if (strTransfDefects != "") strTransfDefects += ";" + chN;
                            //strTransfDefects += "- № " + m_listEquipments[i].m_dictFields["EquipmentName"] + " характеризуется как работоспособное, неисправное состояние, при котором эксплуатация возможна с учащенным контролем оборудования до устранения дефектов";
                            strInfo = " оценивается как частично неработоспособное";
                            break;
                        case Inspection.RangeResult.Red:
                            word.SetCellColor(4, row, MSWord.WdColor.wdColorRed, MSWord.WdColor.wdColorAutomatic, 2);

                            //if (strTransfDefects != "") strTransfDefects += ";" + chN;
                            //strTransfDefects += "- № " + m_listEquipments[i].m_dictFields["EquipmentName"] + " характеризуется как предаварийное";
                            strInfo = " оценивается как предельное";
                            break;
                    }

                    if (!dictEquipDefects.ContainsKey(strInfo)) dictEquipDefects[strInfo] = new List<string>();
                    dictEquipDefects[strInfo].Add(m_listEquipments[i].m_dictFields["EquipmentName"]);


                    /*if (listEquipmentResultsDefects[i].Key != Inspection.RangeResult.None && listEquipmentResultsDefects[i].Key != Inspection.RangeResult.Green)
                    {
                        string strDefects = "";
                        for (int j = 0; j < listEquipmentResultsDefects[i].Value.Count; j++)
                        {
                            if (j == 0)
                                strDefects += listEquipmentResultsDefects[i].Value[j].Value;
                            else
                                strDefects += chN + listEquipmentResultsDefects[i].Value[j].Value;
                        }

                        word.SetTextInCell(5, i + 2, strDefects, 2);
                        word.SetCellList(5, i + 2, 2);
                    }*/
                    for (int ii = 0; ii < listEquipmentResultsDefects[i].m_listDefects.Count; ii++)
                    {
                        word.SetTextInCell(5, row, listEquipmentResultsDefects[i].m_listDefects[ii].m_strDefectInfo, 2);
                        word.SetCellList(5, row, 2);

                        switch (listEquipmentResultsDefects[i].m_listDefects[ii].m_Result)
                        {
                            case Inspection.RangeResult.Yellow:
                                word.SetCellColor(6, row, MSWord.WdColor.wdColorYellow, MSWord.WdColor.wdColorAutomatic, 2);
                                break;
                            case Inspection.RangeResult.Orange:
                                word.SetCellColor(6, row, MSWord.WdColor.wdColorLightOrange, MSWord.WdColor.wdColorAutomatic, 2);
                                break;
                            case Inspection.RangeResult.Red:
                                word.SetCellColor(6, row, MSWord.WdColor.wdColorRed, MSWord.WdColor.wdColorAutomatic, 2);
                                break;
                        }

                        row++;
                    }

                    if (listEquipmentResultsDefects[i].m_maxResult == Inspection.RangeResult.Green)
                    {
                        word.SetTextInCell(5, row, "Дефектов не выявлено", 2);
                        word.SetCellList(5, row, 2);

                        row++;
                    }

                    worker.ReportProgress(20 + (int)(10 * (i + 1) / (float)m_listEquipments.Count));
                }

                List<Word.BorderInfo> listBorders = new List<Word.BorderInfo>();
                listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderTop, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth075pt, (MSWord.WdColor)10243597));
                word.SetCellsBorders(1, 5, 2, 2, listBorders, 2);

                listBorders.Clear();
                listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderHorizontal, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth075pt, (MSWord.WdColor)10243597));
                word.SetCellsBorders(1, 1, 1, 1 + iCntRows, listBorders, 2);

                row = 2;
                for (int i = 0; i < m_listEquipments.Count; i++)
                {
                    if (listEquipmentResultsDefects[i].m_listDefects.Count > 1)
                    {
                        word.SetCellsMerge(1, 1, row, row + listEquipmentResultsDefects[i].m_listDefects.Count - 1, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, 2);
                        word.SetCellsMerge(2, 2, row, row + listEquipmentResultsDefects[i].m_listDefects.Count - 1, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, 2);
                        word.SetCellsMerge(3, 3, row, row + listEquipmentResultsDefects[i].m_listDefects.Count - 1, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, 2);
                        word.SetCellsMerge(4, 4, row, row + listEquipmentResultsDefects[i].m_listDefects.Count - 1, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter, 2);
                    }

                    if (listEquipmentResultsDefects[i].m_listDefects.Count == 0) row++;
                    else row += listEquipmentResultsDefects[i].m_listDefects.Count;
                }

                worker.ReportProgress(30);

                // ---------------------------------------------------------------------------
                // ---------------------------------------------------------------------------

                if (m_listEquipments[0].m_dictFields["SubstationType"] != "1")
                    bookmarks.Add(new KeyValuePair<string, string>("Подстанция", "подстанции " + m_listEquipments[0].m_dictFields["SubstationName"]));
                else
                    bookmarks.Add(new KeyValuePair<string, string>("Подстанция", "станции " + m_listEquipments[0].m_dictFields["SubstationName"]));

                if (reportType == Inspection.ReportType.ReportTransformer)
                {
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения", "5"));
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения2", "5"));
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения3", "5"));

                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования1", "силовых маслонаполненных трансформаторов"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования2", "силовых маслонаполненных трансформаторов"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования3", "силовых трансформаторов"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования4", "силовых трансформаторов"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования5", "силовых трансформаторов"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования6", "силовых трансформаторов"));
                }
                if (reportType == Inspection.ReportType.ReportOilLessSwitch)
                {
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения", "6"));
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения2", "6"));
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения3", "6"));

                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования1", "высоковольтных масляных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования2", "высоковольтных масляных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования3", "высоковольтных масляных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования4", "высоковольтных масляных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования5", "высоковольтных масляных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования6", "высоковольтных масляных выключателей"));
                }
                if (reportType == Inspection.ReportType.ReportOilTankSwitch)
                {
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения", "7"));
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения2", "7"));
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения3", "7"));

                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования1", "силовых маслонаполненных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования2", "силовых маслонаполненных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования3", "силовых маслонаполненных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования4", "силовых маслонаполненных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования5", "силовых маслонаполненных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования6", "силовых маслонаполненных выключателей"));
                }
                if (reportType == Inspection.ReportType.ReportAirSwitch)
                {
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения", "8"));
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения2", "8"));
                    bookmarks.Add(new KeyValuePair<string, string>("Номер_приложения3", "8"));

                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования1", "высоковольтных воздушных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования2", "высоковольтных воздушных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования3", "высоковольтных воздушных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования4", "высоковольтных воздушных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования5", "высоковольтных воздушных выключателей"));
                    bookmarks.Add(new KeyValuePair<string, string>("Вид_оборудования6", "высоковольтных воздушных выключателей"));
                }

                string strTransfDefects = "";
                foreach (KeyValuePair<string, List<string>> pair in dictEquipDefects)
                {
                    if (strTransfDefects != "") strTransfDefects += ";" + chN;
                    strTransfDefects += "- ";

                    for (int i = 0; i < pair.Value.Count; i++)
                    {
                        if (i == 0)
                            strTransfDefects += pair.Value[i];
                        else
                            strTransfDefects += ", " + pair.Value[i];
                    }
                    strTransfDefects += pair.Key;
                }
                strTransfDefects += ".";

                bookmarks.Add(new KeyValuePair<string, string>("Дефекты", strTransfDefects));

                word.SetBookmarkText(bookmarks);

                // ---------------------------------------------------------------------------
                // ------------ Карта технического состояния
                // ---------------------------------------------------------------------------
                word.GotoBookmark("Начало");
                if (reportType == Inspection.ReportType.ReportTransformer)
                    word.SetText("Карта технического состояния силовых трансформаторов\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter);
                if (reportType == Inspection.ReportType.ReportAirSwitch)
                    word.SetText("Карта технического состояния высоковольных воздушных выключателей\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter);
                if (reportType == Inspection.ReportType.ReportOilLessSwitch)
                    word.SetText("Карта технического состояния высоковольных масляных выключателей\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter);
                if (reportType == Inspection.ReportType.ReportOilTankSwitch)
                    word.SetText("Карта технического состояния силовых маслонаполненных выключателей\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter);

                int TableNumber = 1;

                int iEquipmentRemain = m_listEquipments.Count;
                int ind_begin = 0;
                while (true)
                {
                    word.SetText("Таблица " + TableNumber.ToString() + "\n", MSWord.WdParagraphAlignment.wdAlignParagraphRight, new Word.FontParams("", 13, true));
                    TableNumber++;

                    int iMaxEquipmentCntInTable = 10;
                    int iEquipmentCntInTable = iEquipmentRemain > iMaxEquipmentCntInTable ? iMaxEquipmentCntInTable : iEquipmentRemain;
                    int iRowsCnt = 0;

                    if (reportType == Inspection.ReportType.ReportTransformer)
                    {
                        iRowsCnt = 10;
                        word.InitVirtualTable(2 + iEquipmentCntInTable, iRowsCnt);

                        word.SetTextInVirtualCell(1, 1, "№ п/п");
                        word.SetTextInVirtualCell(2, 1, "Вид обследования");
                        word.SetTextInVirtualCell(3, 1, "Силовые трансформаторы");
                        for (int j = 0; j < iEquipmentCntInTable; j++)
                        {
                            word.SetTextInVirtualCell(3 + j, 2, (ind_begin + j + 1).ToString());
                        }
                        word.SetTextInVirtualCell(1, 4, "1");
                        word.SetTextInVirtualCell(2, 4, "Визуальный осмотр");
                        word.SetTextInVirtualCell(1, 5, "2");
                        word.SetTextInVirtualCell(2, 5, "ХАРГ");
                        word.SetTextInVirtualCell(1, 6, "3");
                        word.SetTextInVirtualCell(2, 6, "ФХА");
                        word.SetTextInVirtualCell(1, 7, "4");
                        word.SetTextInVirtualCell(2, 7, "Тепловизионное обследование");
                        word.SetTextInVirtualCell(1, 8, "5");
                        word.SetTextInVirtualCell(2, 8, "Вибрационное обследование");
                        word.SetTextInVirtualCell(1, 9, "6");
                        word.SetTextInVirtualCell(2, 9, "Электрические измерения выключателя");
                        word.SetTextInVirtualCell(1, 10, "Техническое состояние");
                    }
                    if (reportType == Inspection.ReportType.ReportAirSwitch)
                    {
                        iRowsCnt = 8;
                        word.InitVirtualTable(2 + iEquipmentCntInTable, iRowsCnt);

                        word.SetTextInVirtualCell(1, 1, "№ п/п");
                        word.SetTextInVirtualCell(2, 1, "Вид обследования");
                        word.SetTextInVirtualCell(3, 1, "Высоковольные выключатели");
                        for (int j = 0; j < iEquipmentCntInTable; j++)
                        {
                            word.SetTextInVirtualCell(3 + j, 2, (ind_begin + j + 1).ToString());
                        }
                        word.SetTextInVirtualCell(1, 4, "1");
                        word.SetTextInVirtualCell(2, 4, "Визуальное обследование");
                        word.SetTextInVirtualCell(1, 5, "2");
                        word.SetTextInVirtualCell(2, 5, "Тепловизионное обследование");
                        word.SetTextInVirtualCell(1, 6, "3");
                        word.SetTextInVirtualCell(2, 6, "Определение характеристик выключателя");
                        word.SetTextInVirtualCell(1, 7, "4");
                        word.SetTextInVirtualCell(2, 7, "Электрические измерения выключателя");
                        word.SetTextInVirtualCell(1, 8, "Техническое состояние");
                    }
                    if (reportType == Inspection.ReportType.ReportOilLessSwitch)
                    {
                        iRowsCnt = 8;
                        word.InitVirtualTable(2 + iEquipmentCntInTable, iRowsCnt);

                        word.SetTextInVirtualCell(1, 1, "№ п/п");
                        word.SetTextInVirtualCell(2, 1, "Вид обследования");
                        word.SetTextInVirtualCell(3, 1, "Высоковольные выключатели");
                        for (int j = 0; j < iEquipmentCntInTable; j++)
                        {
                            word.SetTextInVirtualCell(3 + j, 2, (ind_begin + j + 1).ToString());
                        }
                        word.SetTextInVirtualCell(1, 4, "1");
                        word.SetTextInVirtualCell(2, 4, "Визуальное обследование");
                        word.SetTextInVirtualCell(1, 5, "2");
                        word.SetTextInVirtualCell(2, 5, "Тепловизионное обследование");
                        word.SetTextInVirtualCell(1, 6, "3");
                        word.SetTextInVirtualCell(2, 6, "Определение характеристик выключателя");
                        word.SetTextInVirtualCell(1, 7, "4");
                        word.SetTextInVirtualCell(2, 7, "Электрические измерения выключателя");
                        word.SetTextInVirtualCell(1, 8, "Техническое состояние");
                    }
                    if (reportType == Inspection.ReportType.ReportOilTankSwitch)
                    {
                        iRowsCnt = 10;
                        word.InitVirtualTable(2 + iEquipmentCntInTable, iRowsCnt);

                        word.SetTextInVirtualCell(1, 1, "№ п/п");
                        word.SetTextInVirtualCell(2, 1, "Вид обследования");
                        word.SetTextInVirtualCell(3, 1, "Высоковольные выключатели");
                        for (int j = 0; j < iEquipmentCntInTable; j++)
                        {
                            word.SetTextInVirtualCell(3 + j, 2, (ind_begin + j + 1).ToString());
                        }
                        word.SetTextInVirtualCell(1, 4, "1");
                        word.SetTextInVirtualCell(2, 4, "Визуальное обследование");
                        word.SetTextInVirtualCell(1, 5, "2");
                        word.SetTextInVirtualCell(2, 5, "ХАРГ");
                        word.SetTextInVirtualCell(1, 6, "3");
                        word.SetTextInVirtualCell(2, 6, "ФХА");
                        word.SetTextInVirtualCell(1, 7, "4");
                        word.SetTextInVirtualCell(2, 7, "Тепловизионное обследование");
                        word.SetTextInVirtualCell(1, 8, "5");
                        word.SetTextInVirtualCell(2, 8, "Определение характеристик выключателя");
                        word.SetTextInVirtualCell(1, 9, "6");
                        word.SetTextInVirtualCell(2, 9, "Электрические измерения выключателя");
                        word.SetTextInVirtualCell(1, 10, "Техническое состояние");
                    }

                    for (int i = ind_begin; i < ind_begin + iEquipmentCntInTable; i++)
                    {
                        word.SetTextInVirtualCell(i - ind_begin + 3, 3, m_listEquipments[i].m_dictFields["EquipmentName"]);

                        Inspection.RangeResult? maxResult = Inspection.RangeResult.None;

                        //foreach (Inspection.InspectionType m_type in Enum.GetValues(typeof(Inspection.InspectionType)))
                        for (int ii = 0; ii < Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[i].m_EquipmentKindID].Count; ii++)
                        {
                            Inspection.InspectionType m_type = Inspection.m_listEquipmentInspections[(Equipment.EquipmentKind)m_listEquipments[i].m_EquipmentKindID][ii];
                            int ind_beg = 0;

                            if (reportType == Inspection.ReportType.ReportTransformer)
                            {
                                switch (m_type)
                                {
                                    case Inspection.InspectionType.Visual:
                                        ind_beg = 4;
                                        break;
                                    case Inspection.InspectionType.HARG:
                                        ind_beg = 5;
                                        break;
                                    case Inspection.InspectionType.FHA:
                                        ind_beg = 6;
                                        break;
                                    case Inspection.InspectionType.Warm:
                                        ind_beg = 7;
                                        break;
                                    case Inspection.InspectionType.Vibro:
                                        ind_beg = 8;
                                        break;
                                }
                            }
                            if (reportType == Inspection.ReportType.ReportAirSwitch)
                            {
                                switch (m_type)
                                {
                                    case Inspection.InspectionType.Visual:
                                        ind_beg = 4;
                                        break;
                                    case Inspection.InspectionType.Warm:
                                        ind_beg = 5;
                                        break;
                                    case Inspection.InspectionType.Parameter:
                                        ind_beg = 6;
                                        break;
                                    case Inspection.InspectionType.Electrical:
                                        ind_beg = 7;
                                        break;
                                }
                            }
                            if (reportType == Inspection.ReportType.ReportOilLessSwitch)
                            {
                                switch (m_type)
                                {
                                    case Inspection.InspectionType.Visual:
                                        ind_beg = 4;
                                        break;
                                    case Inspection.InspectionType.Warm:
                                        ind_beg = 5;
                                        break;
                                    case Inspection.InspectionType.Parameter:
                                        ind_beg = 6;
                                        break;
                                    case Inspection.InspectionType.Electrical:
                                        ind_beg = 7;
                                        break;
                                }
                            }
                            if (reportType == Inspection.ReportType.ReportOilTankSwitch)
                            {
                                switch (m_type)
                                {
                                    case Inspection.InspectionType.Visual:
                                        ind_beg = 4;
                                        break;
                                    case Inspection.InspectionType.HARG:
                                        ind_beg = 5;
                                        break;
                                    case Inspection.InspectionType.FHA:
                                        ind_beg = 6;
                                        break;
                                    case Inspection.InspectionType.Warm:
                                        ind_beg = 7;
                                        break;
                                    case Inspection.InspectionType.Parameter:
                                        ind_beg = 8;
                                        break;
                                    case Inspection.InspectionType.Electrical:
                                        ind_beg = 9;
                                        break;
                                }
                            }

                            switch (listInspectionResults[i][m_type])
                            {
                                case null:
                                case Inspection.RangeResult.None:
                                    word.SetTextInVirtualCell(i - ind_begin + 3, ind_beg, "н/д");
                                    break;
                                case Inspection.RangeResult.Green:
                                    word.SetVirtualCellColor(i - ind_begin + 3, ind_beg, (MSWord.WdColor)52224);
                                    break;
                                case Inspection.RangeResult.Yellow:
                                    word.SetVirtualCellColor(i - ind_begin + 3, ind_beg, MSWord.WdColor.wdColorYellow);
                                    break;
                                case Inspection.RangeResult.Orange:
                                    word.SetVirtualCellColor(i - ind_begin + 3, ind_beg, MSWord.WdColor.wdColorLightOrange);
                                    break;
                                case Inspection.RangeResult.Red:
                                    word.SetVirtualCellColor(i - ind_begin + 3, ind_beg, MSWord.WdColor.wdColorRed);
                                    break;
                            }

                            if (maxResult < listInspectionResults[i][m_type])
                                maxResult = listInspectionResults[i][m_type];
                        }

                        switch (maxResult)
                        {
                            case null:
                            case Inspection.RangeResult.None:
                                word.SetTextInVirtualCell(i - ind_begin + 3, iRowsCnt, "н/д");
                                break;
                            case Inspection.RangeResult.Green:
                                word.SetVirtualCellColor(i - ind_begin + 3, iRowsCnt, (MSWord.WdColor)52224);
                                break;
                            case Inspection.RangeResult.Yellow:
                                word.SetVirtualCellColor(i - ind_begin + 3, iRowsCnt, MSWord.WdColor.wdColorYellow);
                                break;
                            case Inspection.RangeResult.Orange:
                                word.SetVirtualCellColor(i - ind_begin + 3, iRowsCnt, MSWord.WdColor.wdColorLightOrange);
                                break;
                            case Inspection.RangeResult.Red:
                                word.SetVirtualCellColor(i - ind_begin + 3, iRowsCnt, MSWord.WdColor.wdColorRed);
                                break;
                        }

                        worker.ReportProgress(30 + (int)(10 * (i + 1) / (float)m_listEquipments.Count));
                    }

                    word.DisplayVirtualTable();
                    word.SetTableHeadingRows(3);
                    word.SetCellsColor(1, 2 + iEquipmentCntInTable, 1, 3, (MSWord.WdColor)15395562);
                    word.SetTableFont(new Word.FontParams("Arial Narrow", 11, false, false, false, "Обычный")); // ТИ_табл_название
                    word.SetCellsFont(1, 2 + iEquipmentCntInTable, 1, 3, new Word.FontParams("", -1, true));
                    word.SetCellsFont(1, 2, 4, iRowsCnt, new Word.FontParams("", -1, true));

                    word.SetTableAlignment(MSWord.WdParagraphAlignment.wdAlignParagraphCenter, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                    word.SetCellsAlignment(2, 2, 4, iRowsCnt - 1, MSWord.WdParagraphAlignment.wdAlignParagraphLeft, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);

                    word.SetColumnWidth(1, 10.7);
                    word.SetColumnWidth(2, 36.2);

                    double col_width = 209.5 / iEquipmentCntInTable;
                    if (m_listEquipments.Count <= 5) col_width = 209.5 / 5;
                    if (m_listEquipments.Count <= 3) col_width = 209.5 / 3;

                    for (int j = 0; j < iEquipmentCntInTable; j++)
                    {
                        word.SetColumnWidth(3 + j, col_width);
                    }

                    word.SetTableBordersColor(10243597);

                    listBorders.Clear();
                    listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderTop, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth225pt, (MSWord.WdColor)10243597));
                    word.SetCellsBorders(1, 2 + iEquipmentCntInTable, 1, 1, listBorders);
                    listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderBottom, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth225pt, (MSWord.WdColor)10243597));
                    word.SetCellsBorders(1, 2 + iEquipmentCntInTable, iRowsCnt, iRowsCnt, listBorders);

                    word.SetCellsMerge(1, 1, 1, 3, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                    word.SetCellsMerge(2, 2, 1, 3, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                    word.SetCellsMerge(3, 2 + iEquipmentCntInTable, 1, 1, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                    word.SetCellsMerge(1, 2, iRowsCnt, iRowsCnt, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);

                    ind_begin = ind_begin + iEquipmentCntInTable;
                    iEquipmentRemain -= iEquipmentCntInTable;
                    if (ind_begin >= m_listEquipments.Count) break;
                }

                worker.ReportProgress(40);

                // ---------------------------------------------------------------------------
                // ---------------------------------------------------------------------------

                // ------------------
                // Паспортные данные
                // ------------------
                word.SetText("", MSWord.WdParagraphAlignment.wdAlignParagraphLeft);
                word.SetPageBreak();

                if (reportType == Inspection.ReportType.ReportTransformer)
                    word.SetText("Паспортные данные силовых трансформаторов\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                if (reportType == Inspection.ReportType.ReportAirSwitch)
                    word.SetText("Паспортные данные высоковольтных воздушных выключателей\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                if (reportType == Inspection.ReportType.ReportOilLessSwitch)
                    word.SetText("Паспортные данные высоковольтных масляных выключателей\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                if (reportType == Inspection.ReportType.ReportOilTankSwitch)
                    word.SetText("Паспортные данные силовых маслонаполненных выключателей\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));

                iEquipmentRemain = m_listEquipments.Count;
                ind_begin = 0;
                while (true)
                {
                    int iMaxEquipmentCntInTable = 4;
                    word.SetText("Таблица " + TableNumber.ToString() + "\n", MSWord.WdParagraphAlignment.wdAlignParagraphRight, new Word.FontParams("", 13, true));
                    TableNumber++;
                    int iEquipmentCntInTable = iEquipmentRemain > iMaxEquipmentCntInTable ? iMaxEquipmentCntInTable : iEquipmentRemain;

                    int iRowsCnt = 0;

                    if (reportType == Inspection.ReportType.ReportTransformer)
                    {
                        iRowsCnt = 42;
                        word.InitVirtualTable(2 + iEquipmentCntInTable, iRowsCnt);

                        word.SetTextInVirtualCell(1, 1, "1. Паспортные данные");
                        word.SetTextInVirtualCell(1, 2, "№ п/п");
                        word.SetTextInVirtualCell(2, 2, "Наименование показателя");
                        for (int j = 0; j < iEquipmentCntInTable; j++)
                        {
                            word.SetTextInVirtualCell(3 + j, 2, (ind_begin + j + 1).ToString());
                        }
                        word.SetTextInVirtualCell(1, 3, "1.1. Трансформатор");
                        word.SetTextInVirtualCell(1, 4, "1");
                        word.SetTextInVirtualCell(2, 4, "Объектный номер\n(дисп. наименование)");
                        word.SetTextInVirtualCell(1, 5, "2");
                        word.SetTextInVirtualCell(2, 5, "Тип (марка) трансформатора");
                        word.SetTextInVirtualCell(1, 6, "3");
                        word.SetTextInVirtualCell(2, 6, "Тип системы охлаждения");
                        word.SetTextInVirtualCell(1, 7, "4");
                        word.SetTextInVirtualCell(2, 7, "Тип защиты масла\nот увлажнения");
                        word.SetTextInVirtualCell(1, 8, "5");
                        word.SetTextInVirtualCell(2, 8, "Завод-изготовитель");
                        word.SetTextInVirtualCell(1, 9, "6");
                        word.SetTextInVirtualCell(2, 9, "Заводской номер");
                        word.SetTextInVirtualCell(1, 10, "7");
                        word.SetTextInVirtualCell(2, 10, "Год изготовления");
                        word.SetTextInVirtualCell(1, 11, "8");
                        word.SetTextInVirtualCell(2, 11, "Год ввода в эксплуатацию");
                        word.SetTextInVirtualCell(1, 12, "9");
                        word.SetTextInVirtualCell(2, 12, "Срок службы (фактический)");
                        word.SetTextInVirtualCell(1, 13, "10");
                        word.SetTextInVirtualCell(2, 13, "Номинальная мощность (полная), кВА");
                        word.SetTextInVirtualCell(1, 14, "11");
                        word.SetTextInVirtualCell(2, 14, "Номинальное напряжение ВН, кВ");
                        word.SetTextInVirtualCell(1, 15, "12");
                        word.SetTextInVirtualCell(2, 15, "Номинальное напряжение СН, кВ");
                        word.SetTextInVirtualCell(1, 16, "13");
                        word.SetTextInVirtualCell(2, 16, "Номинальное напряжение НН, кВ");
                        word.SetTextInVirtualCell(1, 17, "1.2. Высоковольтные вводы");
                        word.SetTextInVirtualCell(1, 18, "1.2.1. ВН");
                        word.SetTextInVirtualCell(1, 19, "14");
                        word.SetTextInVirtualCell(2, 19, "Фаза");
                        word.SetTextInVirtualCell(1, 20, "15");
                        word.SetTextInVirtualCell(2, 20, "Тип");
                        word.SetTextInVirtualCell(1, 21, "16");
                        word.SetTextInVirtualCell(2, 21, "Завод-изготовитель");
                        word.SetTextInVirtualCell(1, 22, "17");
                        word.SetTextInVirtualCell(2, 22, "Заводской номер");
                        word.SetTextInVirtualCell(1, 23, "18");
                        word.SetTextInVirtualCell(2, 23, "Год изготовления");
                        word.SetTextInVirtualCell(1, 24, "19");
                        word.SetTextInVirtualCell(2, 24, "Год ввода в эксплуатацию");
                        word.SetTextInVirtualCell(1, 25, "1.2.2. СН (110 кВ и выше)");
                        word.SetTextInVirtualCell(1, 26, "20");
                        word.SetTextInVirtualCell(2, 26, "Фаза");
                        word.SetTextInVirtualCell(1, 27, "21");
                        word.SetTextInVirtualCell(2, 27, "Тип");
                        word.SetTextInVirtualCell(1, 28, "22");
                        word.SetTextInVirtualCell(2, 28, "Завод-изготовитель");
                        word.SetTextInVirtualCell(1, 29, "23");
                        word.SetTextInVirtualCell(2, 29, "Заводской номер");
                        word.SetTextInVirtualCell(1, 30, "24");
                        word.SetTextInVirtualCell(2, 30, "Год изготовления");
                        word.SetTextInVirtualCell(1, 31, "25");
                        word.SetTextInVirtualCell(2, 31, "Год ввода в эксплуатацию");
                        word.SetTextInVirtualCell(1, 32, "1.2.3. Нейтраль (110 кВ и выше)");
                        word.SetTextInVirtualCell(1, 33, "26");
                        word.SetTextInVirtualCell(2, 33, "Тип");
                        word.SetTextInVirtualCell(1, 34, "27");
                        word.SetTextInVirtualCell(2, 34, "Завод-изготовитель");
                        word.SetTextInVirtualCell(1, 35, "28");
                        word.SetTextInVirtualCell(2, 35, "Заводской номер");
                        word.SetTextInVirtualCell(1, 36, "29");
                        word.SetTextInVirtualCell(2, 36, "Год изготовления");
                        word.SetTextInVirtualCell(1, 37, "30");
                        word.SetTextInVirtualCell(2, 37, "Год ввода в эксплуатацию");
                        word.SetTextInVirtualCell(1, 38, "1.3. РПН");
                        word.SetTextInVirtualCell(1, 39, "31");
                        word.SetTextInVirtualCell(2, 39, "Тип");
                        word.SetTextInVirtualCell(1, 40, "32");
                        word.SetTextInVirtualCell(2, 40, "Завод-изготовитель");
                        word.SetTextInVirtualCell(1, 41, "33");
                        word.SetTextInVirtualCell(2, 41, "Заводской номер (фаза В)");
                        word.SetTextInVirtualCell(1, 42, "34");
                        word.SetTextInVirtualCell(2, 42, "Заводской номер (фаза С)");

                        for (int i = ind_begin; i < ind_begin + iEquipmentCntInTable; i++)
                        {
                            int col_index = i - ind_begin + 3;

                            word.SetTextInVirtualCell(col_index, 4, m_listEquipments[i].m_dictFields["EquipmentName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 5, m_listEquipments[i].m_dictFields["EquipmentTypeName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 6, m_listEquipments[i].m_dictFields["CoolingSystemTypeID"], "н/д");
                            word.SetTextInVirtualCell(col_index, 7, m_listEquipments[i].m_dictFields["ProtectionOilTypeID"], "н/д");
                            word.SetTextInVirtualCell(col_index, 8, m_listEquipments[i].m_dictFields["ManufacturerName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 9, m_listEquipments[i].m_dictFields["EquipmentNumber"], "н/д");
                            word.SetTextInVirtualCell(col_index, 10, m_listEquipments[i].m_dictFields["CreateYear"], "н/д");
                            word.SetTextInVirtualCell(col_index, 11, m_listEquipments[i].m_dictFields["UseBeginYear"], "н/д");
                            // 
                            int iBeginYear = -1;
                            if (m_listEquipments[i].m_dictFields["CreateYear"] != "")
                                iBeginYear = Convert.ToInt32(m_listEquipments[i].m_dictFields["CreateYear"]);
                            if (m_listEquipments[i].m_dictFields["UseBeginYear"] != "")
                                iBeginYear = Convert.ToInt32(m_listEquipments[i].m_dictFields["UseBeginYear"]);
                            if (iBeginYear >= 0)
                            {
                                word.SetTextInVirtualCell(col_index, 12, (DateTime.Now.Year - iBeginYear + 1).ToString(), "н/д");
                            }
                            // 
                            word.SetTextInVirtualCell(col_index, 13, m_listEquipments[i].m_dictFields["NominalPower"], "н/д");
                            word.SetTextInVirtualCell(col_index, 14, m_listEquipments[i].m_dictFields["NominalVoltageHigh"], "н/д");
                            word.SetTextInVirtualCell(col_index, 15, m_listEquipments[i].m_dictFields["NominalVoltageMiddle"], "н/д");
                            word.SetTextInVirtualCell(col_index, 16, m_listEquipments[i].m_dictFields["NominalVoltageLow"], "н/д");

                            word.SetTextInVirtualCell(col_index, 33, m_listEquipments[i].m_dictFields["InputTypeNeutral"], "н/д");
                            word.SetTextInVirtualCell(col_index, 34, m_listEquipments[i].m_dictFields["InputManufacturerNameNeutral"], "н/д");
                            word.SetTextInVirtualCell(col_index, 35, m_listEquipments[i].m_dictFields["InputNumberNeutral"], "н/д");
                            word.SetTextInVirtualCell(col_index, 36, m_listEquipments[i].m_dictFields["InputCreateYearNeutral"], "н/д");
                            word.SetTextInVirtualCell(col_index, 37, m_listEquipments[i].m_dictFields["InputUseBeginYearNeutral"], "н/д");

                            word.SetTextInVirtualCell(col_index, 39, m_listEquipments[i].m_dictFields["RPNTypeName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 40, m_listEquipments[i].m_dictFields["RPNNumber"], "н/д");
                            word.SetTextInVirtualCell(col_index, 41, m_listEquipments[i].m_dictFields["RPNNumber2"], "н/д");
                            word.SetTextInVirtualCell(col_index, 42, m_listEquipments[i].m_dictFields["RPNNumber3"], "н/д");

                            worker.ReportProgress(40 + (int)(10 * (i + 1) / (float)m_listEquipments.Count));
                        }
                    }

                    if (reportType == Inspection.ReportType.ReportAirSwitch)
                    {
                        iRowsCnt = 13;
                        word.InitVirtualTable(2 + iEquipmentCntInTable, iRowsCnt);

                        word.SetTextInVirtualCell(1, 1, "1. Паспортные данные");
                        word.SetTextInVirtualCell(1, 2, "№ п/п");
                        word.SetTextInVirtualCell(2, 2, "Наименование показателя");
                        for (int j = 0; j < iEquipmentCntInTable; j++)
                        {
                            word.SetTextInVirtualCell(3 + j, 2, (ind_begin + j + 1).ToString());
                        }
                        word.SetTextInVirtualCell(1, 3, "1.1. Выключатель");
                        word.SetTextInVirtualCell(1, 4, "1");
                        word.SetTextInVirtualCell(2, 4, "Объектный номер\n(дисп. наименование)");
                        word.SetTextInVirtualCell(1, 5, "2");
                        word.SetTextInVirtualCell(2, 5, "Тип (марка) выключателя");
                        word.SetTextInVirtualCell(1, 6, "3");
                        word.SetTextInVirtualCell(2, 6, "Тип привода");
                        word.SetTextInVirtualCell(1, 7, "4");
                        word.SetTextInVirtualCell(2, 7, "Завод-изготовитель");
                        word.SetTextInVirtualCell(1, 8, "5");
                        word.SetTextInVirtualCell(2, 8, "Заводской номер");
                        word.SetTextInVirtualCell(1, 9, "6");
                        word.SetTextInVirtualCell(2, 9, "Год изготовления");
                        word.SetTextInVirtualCell(1, 10, "7");
                        word.SetTextInVirtualCell(2, 10, "Год ввода в эксплуатацию");
                        word.SetTextInVirtualCell(1, 11, "8");
                        word.SetTextInVirtualCell(2, 11, "Срок службы (фактический)");
                        word.SetTextInVirtualCell(1, 12, "9");
                        word.SetTextInVirtualCell(2, 12, "Номинальный ток, А");
                        word.SetTextInVirtualCell(1, 13, "10");
                        word.SetTextInVirtualCell(2, 13, "Номинальное напряжение, кВ");

                        for (int i = ind_begin; i < ind_begin + iEquipmentCntInTable; i++)
                        {
                            int col_index = i - ind_begin + 3;

                            word.SetTextInVirtualCell(col_index, 4, m_listEquipments[i].m_dictFields["EquipmentName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 5, m_listEquipments[i].m_dictFields["EquipmentTypeName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 6, m_listEquipments[i].m_dictFields["SwitchDriveTypeName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 7, m_listEquipments[i].m_dictFields["ManufacturerName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 8, m_listEquipments[i].m_dictFields["EquipmentNumber"], "н/д");
                            word.SetTextInVirtualCell(col_index, 9, m_listEquipments[i].m_dictFields["CreateYear"], "н/д");
                            word.SetTextInVirtualCell(col_index, 10, m_listEquipments[i].m_dictFields["UseBeginYear"], "н/д");
                            // 
                            int iBeginYear = -1;
                            if (m_listEquipments[i].m_dictFields["CreateYear"] != "")
                                iBeginYear = Convert.ToInt32(m_listEquipments[i].m_dictFields["CreateYear"]);
                            if (m_listEquipments[i].m_dictFields["UseBeginYear"] != "")
                                iBeginYear = Convert.ToInt32(m_listEquipments[i].m_dictFields["UseBeginYear"]);
                            if (iBeginYear >= 0)
                            {
                                word.SetTextInVirtualCell(col_index, 11, (DateTime.Now.Year - iBeginYear + 1).ToString(), "н/д");
                            }
                            // 
                            word.SetTextInVirtualCell(col_index, 12, m_listEquipments[i].m_dictFields["NominalCurrent"], "н/д");
                            word.SetTextInVirtualCell(col_index, 13, m_listEquipments[i].m_dictFields["NominalVoltageHigh"], "н/д");

                            worker.ReportProgress(40 + (int)(10 * (i + 1) / (float)m_listEquipments.Count));
                        }
                    }

                    if (reportType == Inspection.ReportType.ReportOilLessSwitch)
                    {
                        iRowsCnt = 13;
                        word.InitVirtualTable(2 + iEquipmentCntInTable, iRowsCnt);

                        word.SetTextInVirtualCell(1, 1, "1. Паспортные данные");
                        word.SetTextInVirtualCell(1, 2, "№ п/п");
                        word.SetTextInVirtualCell(2, 2, "Наименование показателя");
                        for (int j = 0; j < iEquipmentCntInTable; j++)
                        {
                            word.SetTextInVirtualCell(3 + j, 2, (ind_begin + j + 1).ToString());
                        }
                        word.SetTextInVirtualCell(1, 3, "1.1. Выключатель");
                        word.SetTextInVirtualCell(1, 4, "1");
                        word.SetTextInVirtualCell(2, 4, "Объектный номер\n(дисп. наименование)");
                        word.SetTextInVirtualCell(1, 5, "2");
                        word.SetTextInVirtualCell(2, 5, "Тип (марка) выключателя");
                        word.SetTextInVirtualCell(1, 6, "3");
                        word.SetTextInVirtualCell(2, 6, "Тип привода");
                        word.SetTextInVirtualCell(1, 7, "4");
                        word.SetTextInVirtualCell(2, 7, "Завод-изготовитель");
                        word.SetTextInVirtualCell(1, 8, "5");
                        word.SetTextInVirtualCell(2, 8, "Заводской номер");
                        word.SetTextInVirtualCell(1, 9, "6");
                        word.SetTextInVirtualCell(2, 9, "Год изготовления");
                        word.SetTextInVirtualCell(1, 10, "7");
                        word.SetTextInVirtualCell(2, 10, "Год ввода в эксплуатацию");
                        word.SetTextInVirtualCell(1, 11, "8");
                        word.SetTextInVirtualCell(2, 11, "Срок службы (фактический)");
                        word.SetTextInVirtualCell(1, 12, "9");
                        word.SetTextInVirtualCell(2, 12, "Номинальный ток, А");
                        word.SetTextInVirtualCell(1, 13, "10");
                        word.SetTextInVirtualCell(2, 13, "Номинальное напряжение, кВ");

                        for (int i = ind_begin; i < ind_begin + iEquipmentCntInTable; i++)
                        {
                            int col_index = i - ind_begin + 3;

                            word.SetTextInVirtualCell(col_index, 4, m_listEquipments[i].m_dictFields["EquipmentName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 5, m_listEquipments[i].m_dictFields["EquipmentTypeName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 6, m_listEquipments[i].m_dictFields["SwitchDriveTypeName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 7, m_listEquipments[i].m_dictFields["ManufacturerName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 8, m_listEquipments[i].m_dictFields["EquipmentNumber"], "н/д");
                            word.SetTextInVirtualCell(col_index, 9, m_listEquipments[i].m_dictFields["CreateYear"], "н/д");
                            word.SetTextInVirtualCell(col_index, 10, m_listEquipments[i].m_dictFields["UseBeginYear"], "н/д");
                            // 
                            int iBeginYear = -1;
                            if (m_listEquipments[i].m_dictFields["CreateYear"] != "")
                                iBeginYear = Convert.ToInt32(m_listEquipments[i].m_dictFields["CreateYear"]);
                            if (m_listEquipments[i].m_dictFields["UseBeginYear"] != "")
                                iBeginYear = Convert.ToInt32(m_listEquipments[i].m_dictFields["UseBeginYear"]);
                            if (iBeginYear >= 0)
                            {
                                word.SetTextInVirtualCell(col_index, 11, (DateTime.Now.Year - iBeginYear + 1).ToString(), "н/д");
                            }
                            // 
                            word.SetTextInVirtualCell(col_index, 12, m_listEquipments[i].m_dictFields["NominalCurrent"], "н/д");
                            word.SetTextInVirtualCell(col_index, 13, m_listEquipments[i].m_dictFields["NominalVoltageHigh"], "н/д");

                            worker.ReportProgress(40 + (int)(10 * (i + 1) / (float)m_listEquipments.Count));
                        }
                    }

                    if (reportType == Inspection.ReportType.ReportOilTankSwitch)
                    {
                        iRowsCnt = 35;
                        word.InitVirtualTable(2 + iEquipmentCntInTable, iRowsCnt);

                        word.SetTextInVirtualCell(1, 1, "1. Паспортные данные");
                        word.SetTextInVirtualCell(1, 2, "№ п/п");
                        word.SetTextInVirtualCell(2, 2, "Наименование показателя");
                        for (int j = 0; j < iEquipmentCntInTable; j++)
                        {
                            word.SetTextInVirtualCell(3 + j, 2, (ind_begin + j + 1).ToString());
                        }
                        word.SetTextInVirtualCell(1, 3, "1.1. Выключатель");
                        word.SetTextInVirtualCell(1, 4, "1");
                        word.SetTextInVirtualCell(2, 4, "Объектный номер\n(дисп. наименование)");
                        word.SetTextInVirtualCell(1, 5, "2");
                        word.SetTextInVirtualCell(2, 5, "Тип (марка) выключателя");
                        word.SetTextInVirtualCell(1, 6, "3");
                        word.SetTextInVirtualCell(2, 6, "Тип привода");
                        word.SetTextInVirtualCell(1, 7, "4");
                        word.SetTextInVirtualCell(2, 7, "Завод-изготовитель");
                        word.SetTextInVirtualCell(1, 8, "5");
                        word.SetTextInVirtualCell(2, 8, "Заводской номер");
                        word.SetTextInVirtualCell(1, 9, "6");
                        word.SetTextInVirtualCell(2, 9, "Год изготовления");
                        word.SetTextInVirtualCell(1, 10, "7");
                        word.SetTextInVirtualCell(2, 10, "Год ввода в эксплуатацию");
                        word.SetTextInVirtualCell(1, 11, "8");
                        word.SetTextInVirtualCell(2, 11, "Срок службы (фактический)");
                        word.SetTextInVirtualCell(1, 12, "9");
                        word.SetTextInVirtualCell(2, 12, "Номинальный ток, А");
                        word.SetTextInVirtualCell(1, 13, "10");
                        word.SetTextInVirtualCell(2, 13, "Номинальное напряжение, кВ");

                        word.SetTextInVirtualCell(1, 14, "1.2. Высоковольтные вводы");
                        word.SetTextInVirtualCell(1, 15, "1.2.1. Полюс А");
                        word.SetTextInVirtualCell(1, 16, "11");
                        word.SetTextInVirtualCell(2, 16, "Сторона");
                        word.SetTextInVirtualCell(1, 17, "12");
                        word.SetTextInVirtualCell(2, 17, "Тип");
                        word.SetTextInVirtualCell(1, 18, "13");
                        word.SetTextInVirtualCell(2, 18, "Завод-изготовитель");
                        word.SetTextInVirtualCell(1, 19, "14");
                        word.SetTextInVirtualCell(2, 19, "Заводской номер");
                        word.SetTextInVirtualCell(1, 20, "15");
                        word.SetTextInVirtualCell(2, 20, "Год изготовления");
                        word.SetTextInVirtualCell(1, 21, "16");
                        word.SetTextInVirtualCell(2, 21, "Год ввода в эксплуатацию");

                        word.SetTextInVirtualCell(1, 22, "1.2.2. Полюс В");
                        word.SetTextInVirtualCell(1, 23, "17");
                        word.SetTextInVirtualCell(2, 23, "Сторона");
                        word.SetTextInVirtualCell(1, 24, "18");
                        word.SetTextInVirtualCell(2, 24, "Тип");
                        word.SetTextInVirtualCell(1, 25, "19");
                        word.SetTextInVirtualCell(2, 25, "Завод-изготовитель");
                        word.SetTextInVirtualCell(1, 26, "20");
                        word.SetTextInVirtualCell(2, 26, "Заводской номер");
                        word.SetTextInVirtualCell(1, 27, "21");
                        word.SetTextInVirtualCell(2, 27, "Год изготовления");
                        word.SetTextInVirtualCell(1, 28, "22");
                        word.SetTextInVirtualCell(2, 28, "Год ввода в эксплуатацию");

                        word.SetTextInVirtualCell(1, 29, "1.2.3. Полюс C");
                        word.SetTextInVirtualCell(1, 30, "17");
                        word.SetTextInVirtualCell(2, 30, "Сторона");
                        word.SetTextInVirtualCell(1, 31, "18");
                        word.SetTextInVirtualCell(2, 31, "Тип");
                        word.SetTextInVirtualCell(1, 32, "19");
                        word.SetTextInVirtualCell(2, 32, "Завод-изготовитель");
                        word.SetTextInVirtualCell(1, 33, "20");
                        word.SetTextInVirtualCell(2, 33, "Заводской номер");
                        word.SetTextInVirtualCell(1, 34, "21");
                        word.SetTextInVirtualCell(2, 34, "Год изготовления");
                        word.SetTextInVirtualCell(1, 35, "22");
                        word.SetTextInVirtualCell(2, 35, "Год ввода в эксплуатацию");

                        for (int i = ind_begin; i < ind_begin + iEquipmentCntInTable; i++)
                        {
                            int col_index = i - ind_begin + 3;

                            word.SetTextInVirtualCell(col_index, 4, m_listEquipments[i].m_dictFields["EquipmentName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 5, m_listEquipments[i].m_dictFields["EquipmentTypeName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 6, m_listEquipments[i].m_dictFields["SwitchDriveTypeName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 7, m_listEquipments[i].m_dictFields["ManufacturerName"], "н/д");
                            word.SetTextInVirtualCell(col_index, 8, m_listEquipments[i].m_dictFields["EquipmentNumber"], "н/д");
                            word.SetTextInVirtualCell(col_index, 9, m_listEquipments[i].m_dictFields["CreateYear"], "н/д");
                            word.SetTextInVirtualCell(col_index, 10, m_listEquipments[i].m_dictFields["UseBeginYear"], "н/д");
                            // 
                            int iBeginYear = -1;
                            if (m_listEquipments[i].m_dictFields["CreateYear"] != "")
                                iBeginYear = Convert.ToInt32(m_listEquipments[i].m_dictFields["CreateYear"]);
                            if (m_listEquipments[i].m_dictFields["UseBeginYear"] != "")
                                iBeginYear = Convert.ToInt32(m_listEquipments[i].m_dictFields["UseBeginYear"]);
                            if (iBeginYear >= 0)
                            {
                                word.SetTextInVirtualCell(col_index, 11, (DateTime.Now.Year - iBeginYear + 1).ToString(), "н/д");
                            }
                            // 
                            word.SetTextInVirtualCell(col_index, 12, m_listEquipments[i].m_dictFields["NominalCurrent"], "н/д");
                            word.SetTextInVirtualCell(col_index, 13, m_listEquipments[i].m_dictFields["NominalVoltageHigh"], "н/д");

                            worker.ReportProgress(40 + (int)(10 * (i + 1) / (float)m_listEquipments.Count));
                        }
                    }

                    word.DisplayVirtualTable();
                    word.SetTableHeadingRows(2);
                    word.SetCellsColor(1, 2 + iEquipmentCntInTable, 1, 2, (MSWord.WdColor)15395562);
                    word.SetTableFont(new Word.FontParams("Arial Narrow", 11, false, false, false, "Обычный")); // ТИ_табл_название
                    word.SetCellsFont(1, 2 + iEquipmentCntInTable, 1, 2, new Word.FontParams("", -1, true));
                    word.SetCellsFont(1, 2, 3, iRowsCnt, new Word.FontParams("", -1, true));

                    if (reportType == Inspection.ReportType.ReportTransformer)
                    {
                        word.SetCellsColor(1, 2 + iEquipmentCntInTable, 3, 3, (MSWord.WdColor)16768701, (MSWord.WdColor)10243597);
                        word.SetCellsColor(1, 2 + iEquipmentCntInTable, 17, 17, (MSWord.WdColor)16768701, (MSWord.WdColor)10243597);
                        word.SetCellsColor(1, 2 + iEquipmentCntInTable, 38, 38, (MSWord.WdColor)16768701, (MSWord.WdColor)10243597);
                    }
                    if (reportType == Inspection.ReportType.ReportAirSwitch)
                    {
                        word.SetCellsColor(1, 2 + iEquipmentCntInTable, 3, 3, (MSWord.WdColor)16768701, (MSWord.WdColor)10243597);
                    }
                    if (reportType == Inspection.ReportType.ReportOilLessSwitch)
                    {
                        word.SetCellsColor(1, 2 + iEquipmentCntInTable, 3, 3, (MSWord.WdColor)16768701, (MSWord.WdColor)10243597);
                    }
                    if (reportType == Inspection.ReportType.ReportOilTankSwitch)
                    {
                        word.SetCellsColor(1, 2 + iEquipmentCntInTable, 3, 3, (MSWord.WdColor)16768701, (MSWord.WdColor)10243597);
                        word.SetCellsColor(1, 2 + iEquipmentCntInTable, 14, 14, (MSWord.WdColor)16768701, (MSWord.WdColor)10243597);
                    }

                    word.SetTableAlignment(MSWord.WdParagraphAlignment.wdAlignParagraphCenter, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                    word.SetCellsAlignment(2, 2, 3, iRowsCnt, MSWord.WdParagraphAlignment.wdAlignParagraphLeft, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);

                    word.SetColumnWidth(1, 10.7);
                    word.SetColumnWidth(2, 55.1);
                    for (int j = 0; j < iEquipmentCntInTable; j++)
                    {
                        word.SetColumnWidth(3 + j, 190.4 / iMaxEquipmentCntInTable);
                    }

                    listBorders.Clear();
                    listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderTop, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth225pt, (MSWord.WdColor)10243597));
                    listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderBottom, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth225pt, (MSWord.WdColor)10243597));
                    word.SetCellsBorders(1, 2 + iEquipmentCntInTable, 1, iRowsCnt, listBorders);

                    if (reportType == Inspection.ReportType.ReportTransformer)
                    {
                        // разделяем ячейки для вводов
                        for (int i = ind_begin + iEquipmentCntInTable - 1; i >= ind_begin; i--)
                        {
                            int col_index = i - ind_begin + 3;
                            word.SplitCell(col_index, 19, 24, 3, 1);
                            word.SplitCell(col_index, 26, 31, 3, 1);
                        }

                        // информация по вводам
                        for (int i = ind_begin; i < ind_begin + iEquipmentCntInTable; i++)
                        {
                            int col_index = i - ind_begin + 3;
                            int col_input_index = (col_index - 3) * 3 + 3;

                            word.SetTextInCell(col_input_index, 19, "A");
                            word.SetTextInCell(col_input_index + 1, 19, "B");
                            word.SetTextInCell(col_input_index + 2, 19, "C");

                            word.SetTextInCell(col_input_index, 26, "A");
                            word.SetTextInCell(col_input_index + 1, 26, "B");
                            word.SetTextInCell(col_input_index + 2, 26, "C");

                            word.SetTextInCell(col_input_index, 20, m_listEquipments[i].m_dictFields["InputTypeHighA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 21, m_listEquipments[i].m_dictFields["InputManufacturerNameHighA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 22, m_listEquipments[i].m_dictFields["InputNumberHighA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 23, m_listEquipments[i].m_dictFields["InputCreateYearHighA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 24, m_listEquipments[i].m_dictFields["InputUseBeginYearHighA"], -1, "н/д");

                            word.SetTextInCell(col_input_index + 1, 20, m_listEquipments[i].m_dictFields["InputTypeHighB"]);
                            word.SetTextInCell(col_input_index + 1, 21, m_listEquipments[i].m_dictFields["InputManufacturerNameHighB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 22, m_listEquipments[i].m_dictFields["InputNumberHighB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 23, m_listEquipments[i].m_dictFields["InputCreateYearHighB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 24, m_listEquipments[i].m_dictFields["InputUseBeginYearHighB"], -1, "н/д");

                            word.SetTextInCell(col_input_index + 2, 20, m_listEquipments[i].m_dictFields["InputTypeHighC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 2, 21, m_listEquipments[i].m_dictFields["InputManufacturerNameHighC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 2, 22, m_listEquipments[i].m_dictFields["InputNumberHighC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 2, 23, m_listEquipments[i].m_dictFields["InputCreateYearHighC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 2, 24, m_listEquipments[i].m_dictFields["InputUseBeginYearHighC"], -1, "н/д");

                            word.SetTextInCell(col_input_index, 27, m_listEquipments[i].m_dictFields["InputTypeMiddleA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 28, m_listEquipments[i].m_dictFields["InputManufacturerNameMiddleA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 29, m_listEquipments[i].m_dictFields["InputNumberMiddleA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 30, m_listEquipments[i].m_dictFields["InputCreateYearMiddleA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 31, m_listEquipments[i].m_dictFields["InputUseBeginYearMiddleA"], -1, "н/д");

                            word.SetTextInCell(col_input_index + 1, 27, m_listEquipments[i].m_dictFields["InputTypeMiddleB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 28, m_listEquipments[i].m_dictFields["InputManufacturerNameMiddleB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 29, m_listEquipments[i].m_dictFields["InputNumberMiddleB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 30, m_listEquipments[i].m_dictFields["InputCreateYearMiddleB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 31, m_listEquipments[i].m_dictFields["InputUseBeginYearMiddleB"], -1, "н/д");

                            word.SetTextInCell(col_input_index + 2, 27, m_listEquipments[i].m_dictFields["InputTypeMiddleC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 2, 28, m_listEquipments[i].m_dictFields["InputManufacturerNameMiddleC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 2, 29, m_listEquipments[i].m_dictFields["InputNumberMiddleC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 2, 30, m_listEquipments[i].m_dictFields["InputCreateYearMiddleC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 2, 31, m_listEquipments[i].m_dictFields["InputUseBeginYearMiddleC"], -1, "н/д");
                        }

                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 1, 1, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 3, 3, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 17, 17, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 18, 18, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 25, 25, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 32, 32, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 38, 38, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                    }

                    if (reportType == Inspection.ReportType.ReportAirSwitch)
                    {
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 1, 1, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 3, 3, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                    }

                    if (reportType == Inspection.ReportType.ReportOilLessSwitch)
                    {
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 1, 1, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 3, 3, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                    }

                    if (reportType == Inspection.ReportType.ReportOilTankSwitch)
                    {
                        // разделяем ячейки для вводов
                        for (int i = ind_begin + iEquipmentCntInTable - 1; i >= ind_begin; i--)
                        {
                            int col_index = i - ind_begin + 3;
                            word.SplitCell(col_index, 16, 21, 2, 1);
                            word.SplitCell(col_index, 23, 28, 2, 1);
                            word.SplitCell(col_index, 30, 35, 2, 1);
                        }

                        // информация по вводам
                        for (int i = ind_begin; i < ind_begin + iEquipmentCntInTable; i++)
                        {
                            int col_index = i - ind_begin + 3;
                            int col_input_index = (col_index - 3) * 2 + 3;

                            word.SetTextInCell(col_input_index, 16, m_listEquipments[i].m_dictFields["InputNameHighA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 17, m_listEquipments[i].m_dictFields["InputTypeHighA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 18, m_listEquipments[i].m_dictFields["InputManufacturerNameHighA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 19, m_listEquipments[i].m_dictFields["InputNumberHighA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 20, m_listEquipments[i].m_dictFields["InputCreateYearHighA"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 21, m_listEquipments[i].m_dictFields["InputUseBeginYearHighA"], -1, "н/д");

                            word.SetTextInCell(col_input_index + 1, 16, m_listEquipments[i].m_dictFields["InputNameHighB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 17, m_listEquipments[i].m_dictFields["InputTypeHighB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 18, m_listEquipments[i].m_dictFields["InputManufacturerNameHighB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 19, m_listEquipments[i].m_dictFields["InputNumberHighB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 20, m_listEquipments[i].m_dictFields["InputCreateYearHighB"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 21, m_listEquipments[i].m_dictFields["InputUseBeginYearHighB"], -1, "н/д");

                            // ------------------------

                            word.SetTextInCell(col_input_index, 23, m_listEquipments[i].m_dictFields["InputNameHighC"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 24, m_listEquipments[i].m_dictFields["InputTypeHighC"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 25, m_listEquipments[i].m_dictFields["InputManufacturerNameHighC"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 26, m_listEquipments[i].m_dictFields["InputNumberHighC"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 27, m_listEquipments[i].m_dictFields["InputCreateYearHighC"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 28, m_listEquipments[i].m_dictFields["InputUseBeginYearHighC"], -1, "н/д");

                            word.SetTextInCell(col_input_index + 1, 23, m_listEquipments[i].m_dictFields["InputNameMiddleA"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 24, m_listEquipments[i].m_dictFields["InputTypeMiddleA"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 25, m_listEquipments[i].m_dictFields["InputManufacturerNameMiddleA"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 26, m_listEquipments[i].m_dictFields["InputNumberMiddleA"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 27, m_listEquipments[i].m_dictFields["InputCreateYearMiddleA"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 28, m_listEquipments[i].m_dictFields["InputUseBeginYearMiddleA"], -1, "н/д");

                            // ------------------------

                            word.SetTextInCell(col_input_index, 30, m_listEquipments[i].m_dictFields["InputNameMiddleB"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 31, m_listEquipments[i].m_dictFields["InputTypeMiddleB"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 32, m_listEquipments[i].m_dictFields["InputManufacturerNameMiddleB"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 33, m_listEquipments[i].m_dictFields["InputNumberMiddleB"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 34, m_listEquipments[i].m_dictFields["InputCreateYearMiddleB"], -1, "н/д");
                            word.SetTextInCell(col_input_index, 35, m_listEquipments[i].m_dictFields["InputUseBeginYearMiddleB"], -1, "н/д");

                            word.SetTextInCell(col_input_index + 1, 30, m_listEquipments[i].m_dictFields["InputNameMiddleC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 31, m_listEquipments[i].m_dictFields["InputTypeMiddleC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 32, m_listEquipments[i].m_dictFields["InputManufacturerNameMiddleC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 33, m_listEquipments[i].m_dictFields["InputNumberMiddleC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 34, m_listEquipments[i].m_dictFields["InputCreateYearMiddleC"], -1, "н/д");
                            word.SetTextInCell(col_input_index + 1, 35, m_listEquipments[i].m_dictFields["InputUseBeginYearMiddleC"], -1, "н/д");
                        }

                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 1, 1, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 3, 3, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 14, 14, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 15, 15, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 22, 22, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 29, 29, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                    }

                    ind_begin = ind_begin + iEquipmentCntInTable;
                    iEquipmentRemain -= iEquipmentCntInTable;
                    if (ind_begin >= m_listEquipments.Count) break;
                }

                worker.ReportProgress(50);
                // ---------------------------------------------------------------------------
                // ---------------------------------------------------------------------------

                // ------------------
                // Карта дефектов функциональных узлов оборудования
                // ------------------

                List<KeyValuePair<int, ColorInfo>> listMergedRows = new List<KeyValuePair<int, ColorInfo>>();
                List<KeyValuePair<int, int>> listNoteCell = new List<KeyValuePair<int, int>>();

                List<Inspection.InspectionType> listTypes = new List<Inspection.InspectionType>();
                if (reportType == Inspection.ReportType.ReportTransformer)
                {
                    listTypes.Add(Inspection.InspectionType.Visual);
                    listTypes.Add(Inspection.InspectionType.HARG);
                    listTypes.Add(Inspection.InspectionType.FHA);
                    listTypes.Add(Inspection.InspectionType.Warm);
                    listTypes.Add(Inspection.InspectionType.Vibro);
                }
                if (reportType == Inspection.ReportType.ReportAirSwitch)
                {
                    listTypes.Add(Inspection.InspectionType.Visual);
                    listTypes.Add(Inspection.InspectionType.Warm);
                    listTypes.Add(Inspection.InspectionType.Parameter);
                    listTypes.Add(Inspection.InspectionType.Electrical);
                }
                if (reportType == Inspection.ReportType.ReportOilLessSwitch)
                {
                    listTypes.Add(Inspection.InspectionType.Visual);
                    listTypes.Add(Inspection.InspectionType.Warm);
                    listTypes.Add(Inspection.InspectionType.Parameter);
                    listTypes.Add(Inspection.InspectionType.Electrical);
                }
                if (reportType == Inspection.ReportType.ReportOilTankSwitch)
                {
                    listTypes.Add(Inspection.InspectionType.Visual);
                    listTypes.Add(Inspection.InspectionType.HARG);
                    listTypes.Add(Inspection.InspectionType.FHA);
                    listTypes.Add(Inspection.InspectionType.Warm);
                    listTypes.Add(Inspection.InspectionType.Parameter);
                    listTypes.Add(Inspection.InspectionType.Electrical);
                }

                word.SetText("", MSWord.WdParagraphAlignment.wdAlignParagraphLeft);
                word.SetPageBreak();

                word.SetText("\n", MSWord.WdParagraphAlignment.wdAlignParagraphLeft, new Word.FontParams("", 6));

                int MinTableIndex = TableNumber;
                int MaxTableIndex = TableNumber + listTypes.Count * (m_listEquipments.Count / 4 + ((m_listEquipments.Count % 4) == 0 ? 0 : 1)) - 1;

                word.ConvertToTable(chTab + "- в таблицах " + MinTableIndex.ToString() + "-" + MaxTableIndex.ToString() + " ячейки не заполняются\nиз-за конструктивных особенностей" + chN, 1, 2, false);
                word.SetTableFont(new Word.FontParams("Arial Narrow", 12, true, false, false, "ТИ_табл_название"));
                word.SetColumnWidth(1, 31.9);
                word.SetColumnWidth(2, 85);
                word.SetCellColor(1, 1, MSWord.WdColor.wdColorGray25);
                word.SetCellsAlignment(2, 2, 1, 1, MSWord.WdParagraphAlignment.wdAlignParagraphLeft, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                listBorders.Clear();
                listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderTop, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth225pt, (MSWord.WdColor)10243597));
                listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderBottom, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth225pt, (MSWord.WdColor)10243597));
                listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderLeft, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth075pt, (MSWord.WdColor)10243597));
                listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderRight, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth075pt, (MSWord.WdColor)10243597));
                word.SetCellsBorders(1, 1, 1, 1, listBorders);

                int tmp_cnt = 0;
                for (int ind = 0; ind < listTypes.Count; ind++)
                {
                    if (ind != 0)
                    {
                        word.SetText("", MSWord.WdParagraphAlignment.wdAlignParagraphLeft);
                        word.SetPageBreak();
                    }

                    if (reportType == Inspection.ReportType.ReportTransformer)
                    {
                        switch (ind)
                        {
                            case 0:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода визуального обследования\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 1:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода ХАРГ\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 2:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода ФХА\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 3:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода тепловизионного обследования\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 4:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода вибрационного обследования\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                        }
                    }
                    if (reportType == Inspection.ReportType.ReportAirSwitch)
                    {
                        switch (ind)
                        {
                            case 0:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода визуального обследования\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 1:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода тепловизионного обследования\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 2:
                                word.SetText("Карта дефектов функциональных узлов оборудования при определении характеристик выключателя\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 3:
                                word.SetText("Карта дефектов функциональных узлов оборудования при электрических измерениях выключателя\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                        }
                    }
                    if (reportType == Inspection.ReportType.ReportOilLessSwitch)
                    {
                        switch (ind)
                        {
                            case 0:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода визуального обследования\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 1:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода тепловизионного обследования\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 2:
                                word.SetText("Карта дефектов функциональных узлов оборудования при определении характеристик выключателя\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 3:
                                word.SetText("Карта дефектов функциональных узлов оборудования при электрических измерениях выключателя\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                        }
                    }
                    if (reportType == Inspection.ReportType.ReportOilTankSwitch)
                    {
                        switch (ind)
                        {
                            case 0:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода визуального обследования\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 1:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода ХАРГ\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 2:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода ФХА\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 3:
                                word.SetText("Карта дефектов функциональных узлов оборудования для метода тепловизионного обследования\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 4:
                                word.SetText("Карта дефектов функциональных узлов оборудования при определении характеристик выключателя\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                            case 5:
                                word.SetText("Карта дефектов функциональных узлов оборудования при электрических измерениях выключателя\n\n", MSWord.WdParagraphAlignment.wdAlignParagraphCenter, new Word.FontParams("Arial Narrow", 14, true, false, false, "ТИ_табл_название"));
                                break;
                        }
                    }

                    iEquipmentRemain = m_listEquipments.Count;
                    ind_begin = 0;

                    while (true)
                    {
                        listNoteCell.Clear();

                        word.SetText("Таблица " + TableNumber.ToString() + "\n", MSWord.WdParagraphAlignment.wdAlignParagraphRight, new Word.FontParams("", 13, true));
                        TableNumber++;
                        int iMaxEquipmentCntInTable = 4;
                        int iEquipmentCntInTable = iEquipmentRemain > iMaxEquipmentCntInTable ? iMaxEquipmentCntInTable : iEquipmentRemain;

                        int iRowsCnt = 3;
                        word.InitVirtualTable(2 + iEquipmentCntInTable, iRowsCnt);

                        if (reportType == Inspection.ReportType.ReportTransformer)
                        {
                            switch (ind)
                            {
                                case 0:
                                    word.SetTextInVirtualCell(1, 1, "2. Визуальное обследование");
                                    break;
                                case 1:
                                    word.SetTextInVirtualCell(1, 1, "3. ХАРГ в трансформаторном масле");
                                    break;
                                case 2:
                                    word.SetTextInVirtualCell(1, 1, "4. Сокращенный ФХА трансформаторного масла");
                                    break;
                                case 3:
                                    word.SetTextInVirtualCell(1, 1, "5. Тепловизионный контроль");
                                    break;
                                case 4:
                                    word.SetTextInVirtualCell(1, 1, "6. Вибрационное обследование");
                                    break;
                            }
                        }
                        if (reportType == Inspection.ReportType.ReportAirSwitch)
                        {
                            switch (ind)
                            {
                                case 0:
                                    word.SetTextInVirtualCell(1, 1, "2. Визуальное обследование");
                                    break;
                                case 1:
                                    word.SetTextInVirtualCell(1, 1, "3. Тепловизионное обследование");
                                    break;
                                case 2:
                                    word.SetTextInVirtualCell(1, 1, "4. Определение характеристик выключателя");
                                    break;
                                case 3:
                                    word.SetTextInVirtualCell(1, 1, "5. Электрические измерения");
                                    break;
                            }
                        }
                        if (reportType == Inspection.ReportType.ReportOilLessSwitch)
                        {
                            switch (ind)
                            {
                                case 0:
                                    word.SetTextInVirtualCell(1, 1, "2. Визуальное обследование");
                                    break;
                                case 1:
                                    word.SetTextInVirtualCell(1, 1, "3. Тепловизионное обследование");
                                    break;
                                case 2:
                                    word.SetTextInVirtualCell(1, 1, "4. Определение характеристик выключателя");
                                    break;
                                case 3:
                                    word.SetTextInVirtualCell(1, 1, "5. Электрические измерения");
                                    break;
                            }
                        }
                        if (reportType == Inspection.ReportType.ReportOilTankSwitch)
                        {
                            switch (ind)
                            {
                                case 0:
                                    word.SetTextInVirtualCell(1, 1, "2. Визуальное обследование");
                                    break;
                                case 1:
                                    word.SetTextInVirtualCell(1, 1, "3. ХАРГ");
                                    break;
                                case 2:
                                    word.SetTextInVirtualCell(1, 1, "4. ФХА");
                                    break;
                                case 3:
                                    word.SetTextInVirtualCell(1, 1, "5. Тепловизионный контроль");
                                    break;
                                case 4:
                                    word.SetTextInVirtualCell(1, 1, "6. Определение характеристик выключателя");
                                    break;
                                case 5:
                                    word.SetTextInVirtualCell(1, 1, "7. Электрические измерения");
                                    break;
                            }
                        }
                        word.SetTextInVirtualCell(1, 2, "№\nп/п");
                        word.SetTextInVirtualCell(2, 2, "Дефект, повреждение, нарушение");

                        for (int j = 0; j < iEquipmentCntInTable; j++)
                        {
                            word.SetTextInVirtualCell(3 + j, 2, (ind_begin + j + 1).ToString());
                        }

                        listMergedRows.Clear();

                        for (int i = ind_begin; i < ind_begin + iEquipmentCntInTable; i++)
                        {
                            int col_index = i - ind_begin + 3;

                            word.SetTextInVirtualCell(col_index, 3, m_listEquipments[i].m_dictFields["EquipmentName"]);

                            int iCnt = 0;

                            // функциональные узлы
                            int iRowIndex = 4;
                            int iRootIndex = 0;

                            foreach (KeyValuePair<long, List<FunctionalSystemShortInfo>> sub_type in listData[listTypes[ind]][i])
                            {
                                // заносим данные о подтипах, если их больше, чем 1
                                if (sub_type.Key > 0)
                                {
                                    for (int j = 0; j < m_list_sub_types[listTypes[ind]].Count; j++)
                                    {
                                        if (m_list_sub_types[listTypes[ind]][j].m_ID == sub_type.Key)
                                        {
                                            if (i == ind_begin)
                                            {
                                                word.InsertRowsInVirtualTable(1);
                                                iRowsCnt++;
                                                word.SetTextInVirtualCell(1, iRowIndex, m_list_sub_types[listTypes[ind]][j].m_strName);
                                                listMergedRows.Add(new KeyValuePair<int, ColorInfo>(iRowIndex, new ColorInfo(MSWord.WdColor.wdColorLightOrange, MSWord.WdColor.wdColorGray10)));
                                            }
                                            iRowIndex++;
                                            break;
                                        }
                                    }
                                }

                                for (int j = 0; j < sub_type.Value.Count; j++)
                                {
                                    if (i == ind_begin)
                                    {
                                        word.InsertRowsInVirtualTable(1);
                                        iRowsCnt++;
                                        word.SetTextInVirtualCell(1, iRowIndex, (ind + 2).ToString() + "." + (iRootIndex + 1).ToString() + ". " + sub_type.Value[j].m_strName);
                                        listMergedRows.Add(new KeyValuePair<int, ColorInfo>(iRowIndex, new ColorInfo((MSWord.WdColor)10243597, (MSWord.WdColor)16768701)));
                                    }
                                    iRowIndex++;

                                    for (int k = 0; k < sub_type.Value[j].m_systemitems.Count; k++)
                                    {
                                        if (sub_type.Value[j].m_systemitems[k].m_strName != sub_type.Value[j].m_strName)
                                        {
                                            if (i == ind_begin)
                                            {
                                                word.InsertRowsInVirtualTable(1);
                                                iRowsCnt++;
                                                word.SetTextInVirtualCell(1, iRowIndex, (ind + 2).ToString() + "." + (iRootIndex + 1).ToString() + "." + (k + 1).ToString() + ". " + sub_type.Value[j].m_systemitems[k].m_strName);
                                                listMergedRows.Add(new KeyValuePair<int, ColorInfo>(iRowIndex, null));
                                            }
                                            iRowIndex++;
                                        }

                                        for (int z = 0; z < sub_type.Value[j].m_systemitems[k].m_defects.Count; z++)
                                        {
                                            if (i == ind_begin)
                                            {
                                                word.InsertRowsInVirtualTable(1);
                                                iRowsCnt++;
                                                word.SetTextInVirtualCell(1, iRowIndex, (iCnt + 1).ToString());
                                                word.SetTextInVirtualCell(2, iRowIndex, sub_type.Value[j].m_systemitems[k].m_defects[z].m_strDefectName);

                                                //word.SetVirtualCellColor(1, 2 + iEquipmentCntInTable, iRowIndex, iRowIndex, MSWord.WdColor.wdColorAutomatic, MSWord.WdColor.wdColorAutomatic);
                                            }

                                            if (sub_type.Value[j].m_systemitems[k].m_bEnable && sub_type.Value[j].m_systemitems[k].m_defects[z].m_bEnable)
                                            {
                                                switch (sub_type.Value[j].m_systemitems[k].m_defects[z].m_result)
                                                {
                                                    case null:
                                                    case Inspection.RangeResult.None:
                                                        word.SetTextInVirtualCell(col_index, iRowIndex, "н/д");
                                                        break;
                                                    case Inspection.RangeResult.Green:
                                                        word.SetVirtualCellColor(col_index, iRowIndex, (MSWord.WdColor)52224);
                                                        break;
                                                    case Inspection.RangeResult.Yellow:
                                                        word.SetVirtualCellColor(col_index, iRowIndex, MSWord.WdColor.wdColorYellow);
                                                        word.SetTextInVirtualCell(col_index, iRowIndex, sub_type.Value[j].m_systemitems[k].m_defects[z].m_strDefectDescript, "");
                                                        break;
                                                    case Inspection.RangeResult.Orange:
                                                        word.SetVirtualCellColor(col_index, iRowIndex, MSWord.WdColor.wdColorLightOrange);
                                                        word.SetTextInVirtualCell(col_index, iRowIndex, sub_type.Value[j].m_systemitems[k].m_defects[z].m_strDefectDescript, "");
                                                        break;
                                                    case Inspection.RangeResult.Red:
                                                        word.SetVirtualCellColor(col_index, iRowIndex, MSWord.WdColor.wdColorRed);
                                                        word.SetTextInVirtualCell(col_index, iRowIndex, sub_type.Value[j].m_systemitems[k].m_defects[z].m_strDefectDescript, "");
                                                        break;
                                                }
                                            }
                                            else
                                                word.SetVirtualCellColor(col_index, iRowIndex, MSWord.WdColor.wdColorGray25);

                                            iRowIndex++;
                                            iCnt++;
                                        }

                                        // выводим примечание
                                        if (i == ind_begin)
                                        {
                                            word.InsertRowsInVirtualTable(1);
                                            iRowsCnt++;
                                            word.SetTextInVirtualCell(2, iRowIndex, "Примечание");
                                            listNoteCell.Add(new KeyValuePair<int, int>(2, iRowIndex));
                                            //word.SetVirtualCellColor(1, 2 + iEquipmentCntInTable, iRowIndex, iRowIndex, MSWord.WdColor.wdColorAutomatic, MSWord.WdColor.wdColorAutomatic);
                                        }
                                        word.SetTextInVirtualCell(col_index, iRowIndex, sub_type.Value[j].m_systemitems[k].m_strNote, "-");
                                        iRowIndex++;
                                        //iCnt++;
                                    }
                                    iRootIndex++;
                                }
                            }

                            worker.ReportProgress(50 + (int)(45 * (tmp_cnt + 1) / (float)(m_listEquipments.Count * listTypes.Count)));

                            tmp_cnt++;
                        }

                        word.DisplayVirtualTable();

                        word.SetTableHeadingRows(3);
                        word.SetCellsColor(1, 2 + iEquipmentCntInTable, 1, 3, (MSWord.WdColor)15395562);
                        word.SetTableFont(new Word.FontParams("Arial Narrow", 11, false, false, false, "Обычный")); // ТИ_табл_название
                        word.SetCellsFont(1, 2 + iEquipmentCntInTable, 1, 3, new Word.FontParams("", -1, true));
                        word.SetCellsFont(1, 1, 4, iRowsCnt, new Word.FontParams("", -1, true));

                        word.SetTableAlignment(MSWord.WdParagraphAlignment.wdAlignParagraphCenter, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsAlignment(2, 2, 4, iRowsCnt, MSWord.WdParagraphAlignment.wdAlignParagraphLeft, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);

                        word.SetColumnWidth(1, 11.8);
                        word.SetColumnWidth(2, 72);
                        for (int j = 0; j < iEquipmentCntInTable; j++)
                        {
                            word.SetColumnWidth(3 + j, 172 / iMaxEquipmentCntInTable);
                        }

                        listBorders.Clear();
                        listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderTop, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth225pt, (MSWord.WdColor)10243597));
                        listBorders.Add(new Word.BorderInfo(MSWord.WdBorderType.wdBorderBottom, MSWord.WdLineStyle.wdLineStyleSingle, MSWord.WdLineWidth.wdLineWidth225pt, (MSWord.WdColor)10243597));
                        word.SetCellsBorders(1, 2 + iEquipmentCntInTable, 1, iRowsCnt, listBorders);
                        //listBorders.Clear();
                        //word.SetCellsBorders(1, 2 + iEquipmentCntInTable, iRowsCnt, iRowsCnt, listBorders);

                        word.SetCellsMerge(1, 2 + iEquipmentCntInTable, 1, 1, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(1, 1, 2, 3, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        word.SetCellsMerge(2, 2, 2, 3, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);

                        for (int j = 0; j < listMergedRows.Count; j++)
                        {
                            if (listMergedRows[j].Value != null)
                            {
                                word.SetCellColor(1, listMergedRows[j].Key, listMergedRows[j].Value.m_bkColor, listMergedRows[j].Value.m_textColor);
                            }
                            word.SetCellsMerge(1, 2 + iEquipmentCntInTable, listMergedRows[j].Key, listMergedRows[j].Key, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        }

                        for (int j = listNoteCell.Count - 1; j >= 0; j--)
                        {
                            string str = word.GetCellsText(3, 2 + iEquipmentCntInTable, listNoteCell[j].Value, listNoteCell[j].Value);
                            str = str.Replace("\r", "").Replace("\a", "").Replace("-", "");
                            if (str == "")
                                word.DeleteRowEx(listNoteCell[j].Value);
                            else
                                word.SetCellsAlignment(2, 2, listNoteCell[j].Value, listNoteCell[j].Value, MSWord.WdParagraphAlignment.wdAlignParagraphRight, MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter);
                        }
                        listNoteCell.Clear();

                        ind_begin = ind_begin + iEquipmentCntInTable;
                        iEquipmentRemain -= iEquipmentCntInTable;
                        if (ind_begin >= m_listEquipments.Count) break;
                    }
                }

                // ---------------------------------------------------------------------------
                // ---------------------------------------------------------------------------

                worker.ReportProgress(100);

                word.SetVisible(true);
                word.DestroyWord();
            }
            /*catch (Exception ex)
            {
                word.DestroyWord();
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (m_reportType == Inspection.ReportType.ProtocolTransformer)
                PrintProtocolTransformer();

            if (m_reportType == Inspection.ReportType.ReportTransformer
                || m_reportType == Inspection.ReportType.ReportAirSwitch
                || m_reportType == Inspection.ReportType.ReportOilLessSwitch
                || m_reportType == Inspection.ReportType.ReportOilTankSwitch)
                PrintReport(m_reportType);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Position = e.ProgressPercentage;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progress.Position = 100;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            m_bEnd = true;
            Close();
        }

        private void WaitingFrom_Load(object sender, EventArgs e)
        {
            if (m_reportType == Inspection.ReportType.ProtocolTransformer) 
                this.Text = "Вывод протокола";
            if (m_reportType == Inspection.ReportType.ReportTransformer
                || m_reportType == Inspection.ReportType.ReportAirSwitch
                || m_reportType == Inspection.ReportType.ReportOilLessSwitch
                || m_reportType == Inspection.ReportType.ReportOilTankSwitch) 
                this.Text = "Вывод отчета по комплексному обследованию";

            worker.RunWorkerAsync();
        }

        private void WaitingFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_bEnd) e.Cancel = true;
        }
    }
}