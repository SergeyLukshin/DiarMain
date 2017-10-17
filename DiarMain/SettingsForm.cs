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
    public partial class SettingsForm : DevExpress.XtraEditors.XtraForm
    {
        class DataSourceMsg
        {
            public DataSourceMsg(Image img, string strTime, string strMsg)
            {
                m_Image = img;
                m_strTime = strTime;
                m_strMsg = strMsg;
            }

            private Image m_Image;
            private string m_strMsg;
            private string m_strTime;

            public Image IMAGE
            {
                get { return m_Image; }
                set { m_Image = value; }
            }

            public string MSG
            {
                get { return m_strMsg; }
                set { m_strMsg = value; }
            }

            public string TIME
            {
                get { return m_strTime; }
                set { m_strTime = value; }
            }
        };

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
        BindingList<DataSourceMsg> listMsg = new BindingList<DataSourceMsg>();
        bool m_bEnd = true;

        public Dictionary<long, ImportData.EquipmentInfo> m_listImportData = new Dictionary<long, ImportData.EquipmentInfo>();
        public Dictionary<long, ImportData.EquipmentInfo> m_listData = new Dictionary<long, ImportData.EquipmentInfo>();
        public List<ImportData.CheckInfo> m_listChecks = null;
        Dictionary<string, Dictionary<KeyValuePair<long, string>, long>> m_dictRefs = null;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            //listModules.Add(new DataSourceModule(false, "12121"));
            //listModules.Add(new DataSourceModule(true, "45454"));

            try
            {
                SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                connection.Open();
                SQLiteCommand com = new SQLiteCommand(connection);
                com.CommandText = "SELECT EquipmentKindID, EquipmentKindName, Disable FROM EquipmentKinds ORDER BY EquipmentKindID";
                com.CommandType = CommandType.Text;
                SQLiteDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    long iEquipmentKindID = Convert.ToInt64(dr["EquipmentKindID"]);
                    long iDisable = Convert.ToInt64(dr["Disable"]);
                    string strEquipmentKindName = Convert.ToString(dr["EquipmentKindName"]);

                    listModules.Add(new DataSourceModule(iEquipmentKindID, (iDisable == 0) ? true : false, strEquipmentKindName));
                }
                dr.Close();

                com.CommandText = "SELECT MainImage FROM Settings";

                SQLiteDataReader drImage = com.ExecuteReader();
                if (drImage.HasRows)
                {
                    while (drImage.Read())
                    {
                        peImage.EditValue = drImage["MainImage"];
                        break;
                    }
                }
                drImage.Close();

                connection.Close();
            }
            catch (SQLiteException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DBConcurrencyException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            GridGC.DataSource = listModules;

            GridMsg.DataSource = listMsg;
        }

        private void bActivation_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabSettings.SelectedTabPageIndex == 2)
                {
                    if (tePath.Text == "")
                    {
                        MyLocalizer.XtraMessageBoxShow("Необходимо указать путь к импортируемой базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if ("data source=" + tePath.Text == global::DiarMain.Properties.Settings.Default.diarConnectionString)
                    {
                        MyLocalizer.XtraMessageBoxShow("Необходимо выбрать другую базу данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    listMsg.Clear();
                    GridMsg.Invalidate();

                    bActivation.Enabled = false;
                    m_bEnd = false;

                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    worker.RunWorkerAsync();
                }
                else
                {
                    bool bExistModule = false;
                    for (int i = 0; i < listModules.Count; i++)
                    {
                        if (listModules[i].CHECK)
                        {
                            bExistModule = true;
                            break;
                        }
                    }

                    if (!bExistModule)
                    {
                        MyLocalizer.XtraMessageBoxShow("Необходимо включить хотя бы один модуль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                    connection.Open();
                    SQLiteCommand com = new SQLiteCommand(connection);
                    com.CommandText = "UPDATE EquipmentKinds SET Disable = @disable WHERE EquipmentKindID = @id";
                    com.CommandType = CommandType.Text;
                    SQLiteParameter param = new SQLiteParameter("@disable", DbType.Int64);
                    SQLiteParameter param2 = new SQLiteParameter("@id", DbType.Int64);

                    for (int i = 0; i < listModules.Count; i++)
                    {
                        com.Parameters.Clear();

                        param.Value = listModules[i].CHECK ? 0 : 1;
                        param2.Value = listModules[i].ID;

                        com.Parameters.Add(param);
                        com.Parameters.Add(param2);
                        com.ExecuteNonQuery();
                    }

                    com.CommandText = "UPDATE Settings SET MainImage = @image";
                    com.Parameters.Clear();
                    SQLiteParameter param3 = new SQLiteParameter("@image", DbType.Object);
                    param3.Value = peImage.EditValue;
                    com.Parameters.Add(param3);
                    com.ExecuteNonQuery();

                    connection.Close();

                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
            }
            catch (SQLiteException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DBConcurrencyException ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MyLocalizer.XtraMessageBoxShow(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnChangePicture_Click(object sender, EventArgs e)
        {

        }

        private void peImage_DoubleClick(object sender, EventArgs e)
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
                peImage.EditValue = f.m_img;
            }
        }

        private void btnChangePicture_Click_1(object sender, EventArgs e)
        {
            ChangePicture();
        }

        private void btnClearPicture_Click(object sender, EventArgs e)
        {
            peImage.EditValue = null;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tePath.Text = openFileDlg.FileName;
            }
        }

        private void tabSettings_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (tabSettings.SelectedTabPageIndex == 0 || tabSettings.SelectedTabPageIndex == 1)
            {
                bActivation.Text = "Сохранить";
                bCancel.Text = "Отменить";
            }
            if (tabSettings.SelectedTabPageIndex == 2)
            {
                bActivation.Text = "Импорт";
                bCancel.Text = "Закрыть";
            }
        }

        delegate void AddMsgDelegate(KeyValuePair<ImportData.MsgState, string> msg);

        public void AddMsg(KeyValuePair<ImportData.MsgState, string> msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new AddMsgDelegate(AddMsg), new object[] { msg });
                return;
            }
            else
            {
                listMsg.Add(new DataSourceMsg(imageListMsg.Images[(int)msg.Key], DateTime.Now.ToLongTimeString(), msg.Value));
                GridMsg.Invalidate();
            }
        }

        public void AnalizeData()
        {
            // АЛГОРИТМ загрузки данных из другой базы
            // 1. получаем все данные по всем оборудованиям из загружаемой базы
            // 2. бегаем по списку полученного оборудования
            // 3. если в основной базе есть оборудование с таким же ключевыми полями, то шаг 5, иначе шаг 4
            // 4. проверям наличие основных полей оборудования, если чего то не хватает - добавляем в таблицу, добавляем оборудование, переходим на шаг 6
            // 5. проверяем, есть ли поля, которые поменялись, если ничего не менялось, переходим к шагу 6, иначе проверям наличие основных полей оборудования, если чего то не хватает - добавляем в таблицу, изменяем оборудование
            // 6. проверки: если оборудование существует в базе, но у него поменялись поля, которые не должны меняться, выводится ошибка

            AddMsg(new KeyValuePair<ImportData.MsgState, string>(ImportData.MsgState.Info, "Анализ данных по оборудованию..."));

            SQLiteConnection connection = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
            connection.Open();

            KeyValuePair<ImportData.MsgState, string> Msg = new KeyValuePair<ImportData.MsgState, string>();

            int iCntInsert = 0;
            int iCntUpdate = 0;
            int iCntError = 0;
            foreach (KeyValuePair<long, ImportData.EquipmentInfo> pairImport in m_listImportData)
            {
                long EquipmentID = 0;

                bool bFind = false;
                bool bNext = false;
                foreach (KeyValuePair<long, ImportData.EquipmentInfo> pair in m_listData)
                {
                    if (pairImport.Value.m_strEquipmentName == pair.Value.m_strEquipmentName &&
                        pairImport.Value.m_strEquipmentNumber == pair.Value.m_strEquipmentNumber)
                    {
                        if (pairImport.Value.m_iEquipmentKindID != pair.Value.m_iEquipmentKindID)
                        {
                            AddMsg(new KeyValuePair<ImportData.MsgState, string>(ImportData.MsgState.Warning, "Оборудование " + pairImport.Value.m_strEquipmentName + " (" + pairImport.Value.m_strEquipmentNumber + ") в исходной базе имеет другое значение поля \"Вид оборудования\"."));
                            iCntError++;
                            bNext = true;
                            break;
                        }
                        if (pairImport.Value.m_strSubstation != pair.Value.m_strSubstation ||
                            pairImport.Value.m_strBranch != pair.Value.m_strBranch ||
                            pairImport.Value.m_strSubject != pair.Value.m_strSubject)
                        {
                            AddMsg(new KeyValuePair<ImportData.MsgState, string>(ImportData.MsgState.Warning, "Оборудование " + pairImport.Value.m_strEquipmentName + " (" + pairImport.Value.m_strEquipmentNumber + ") в исходной базе имеет другое значение полей \"Субъект\", \"Филиал\", \"Подстанция\"."));
                            iCntError++;
                            bNext = true;
                            break;
                        }

                        bool bDiffParam = false;
                        bool bDiffInputParam = false;
                        ImportData.CompareEquipments(pairImport.Value, pair.Value, ref bDiffParam, ref bDiffInputParam);

                        if (bDiffParam || bDiffInputParam)
                        {
                            Msg = ImportData.UpdateEquipment(connection, pair, pairImport.Value, bDiffParam, bDiffInputParam, ref m_dictRefs);
                            if (Msg.Key == ImportData.MsgState.Success)
                            {
                                pairImport.Value.m_iEquipmentIDForImport = pair.Key;
                                iCntUpdate++;
                            }
                            else
                            {
                                iCntError++;
                                AddMsg(Msg);
                            }
                        }
                        else
                            pairImport.Value.m_iEquipmentIDForImport = pair.Key;

                        bFind = true;
                        break;
                    }
                }
                if (bNext) continue;

                if (!bFind)
                {
                    Msg = ImportData.InsertEquipment(connection, pairImport.Value, ref m_dictRefs, out EquipmentID);
                    if (Msg.Key == ImportData.MsgState.Success)
                    {
                        pairImport.Value.m_iEquipmentIDForImport = EquipmentID;
                        iCntInsert++;
                    }
                    else
                    {
                        iCntError++;
                        AddMsg(Msg);
                    }
                }
            }

            AddMsg(new KeyValuePair<ImportData.MsgState, string>(ImportData.MsgState.Success, "Анализ данных по оборудованию завершен: изменено " + iCntUpdate.ToString() + " записей, добавлено " + iCntInsert.ToString() + " записей, ошибочных " + iCntError.ToString() + " записей"));

            // 7. бегаем по списку обследований
            // 8. если у обследования нет объекта проверка, тогда переходим на шаг 10, иначе ищем проверку по ключевым полям
            // 9. если проверка не найдена, то проверяется пересечение с существующими проверками по датам в той же подстанции, если такие проверки найдены - то это ошибка и такое обследование не заносится в базу
            // 10. если все нормально, и нет обследования по ключевым полям, то оно добавляется, иначе все данные обследования заменяются на данные из загружаемой базы

            AddMsg(new KeyValuePair<ImportData.MsgState, string>(ImportData.MsgState.Info, "Анализ данных по обследованиям..."));
            int iCntCheckInsert = 0;
            int iCntInspInsert = 0;
            int iCntCheckError = 0;
            int iCntInspError = 0;

            long? DeleteInspectionID = null;
            foreach (KeyValuePair<long, ImportData.EquipmentInfo> pairImport in m_listImportData)
            {
                if (pairImport.Value.m_iEquipmentIDForImport != null)
                {
                    if (m_listData.ContainsKey((long)pairImport.Value.m_iEquipmentIDForImport))
                    {
                        foreach (KeyValuePair<long, ImportData.EquipmentInfo.InspectionInfo> pairInspImport in pairImport.Value.m_listInspections)
                        {
                            bool bFind = false;
                            foreach (KeyValuePair<long, ImportData.EquipmentInfo.InspectionInfo> pairInsp in m_listData[(long)pairImport.Value.m_iEquipmentIDForImport].m_listInspections)
                            {
                                if (pairInspImport.Value.m_iType == pairInsp.Value.m_iType &&
                                    pairInspImport.Value.m_dtDate == pairInsp.Value.m_dtDate)
                                {
                                    // проверяем, изменились ли данные по обследованию
                                    bool bDiffParam = false;
                                    ImportData.CompareInspections(pairInspImport.Value, pairInsp.Value, ref bDiffParam);
                                    bFind = true;
                                    if (bDiffParam)
                                    {
                                        // все данные по инспекции
                                        DeleteInspectionID = pairInsp.Key;
                                        bFind = false; //  чтобы потом добавить новые данные
                                    }
                                    break;
                                }
                            }
                            if (!bFind)
                            {
                                long? CheckID = null;
                                if (pairInspImport.Value.m_Check != null)
                                {
                                    // ищем такую, же проверку
                                    bool bNext = false;
                                    for (int i = 0; i < m_listChecks.Count; i++)
                                    {
                                        if (m_listChecks[i].m_strBranch == pairImport.Value.m_strBranch &&
                                            m_listChecks[i].m_strSubject == pairImport.Value.m_strSubject &&
                                            m_listChecks[i].m_strSubstation == pairImport.Value.m_strSubstation &&
                                            m_listChecks[i].m_DateBegin == pairInspImport.Value.m_Check.m_dtBegin &&
                                            m_listChecks[i].m_DateEnd == pairInspImport.Value.m_Check.m_dtEnd)
                                        {
                                            CheckID = m_listChecks[i].m_iCheckID;
                                            break;
                                        }

                                        // ищем пересечение
                                        if (m_listChecks[i].m_strBranch == pairImport.Value.m_strBranch &&
                                            m_listChecks[i].m_strSubject == pairImport.Value.m_strSubject &&
                                            m_listChecks[i].m_strSubstation == pairImport.Value.m_strSubstation &&
                                            m_listChecks[i].m_DateBegin <= pairInspImport.Value.m_Check.m_dtEnd &&
                                            m_listChecks[i].m_DateEnd >= pairInspImport.Value.m_Check.m_dtBegin)
                                        {
                                            AddMsg(new KeyValuePair<ImportData.MsgState, string>(ImportData.MsgState.Warning, "Проверка c " + pairInspImport.Value.m_Check.m_dtBegin.ToShortDateString() + " по " + pairInspImport.Value.m_Check.m_dtEnd.ToShortDateString()
                                                + " на объекте \"" + pairImport.Value.m_strSubject + ", " + pairImport.Value.m_strBranch
                                                + ", " + pairImport.Value.m_strSubstation + "\" пересекается с существующей проверкой c " + m_listChecks[i].m_DateBegin.ToShortDateString() + " по " + m_listChecks[i].m_DateEnd.ToShortDateString()
                                                + "."));
                                            iCntCheckError++;
                                            bNext = true;
                                            break;
                                        }
                                    }
                                    if (bNext) continue;

                                    if (CheckID == null)
                                    {
                                        // добавляем проверку
                                        Msg = ImportData.InsertCheck(connection, pairInspImport.Value.m_Check, pairImport.Value, ref m_dictRefs, out CheckID);
                                        if (Msg.Key == ImportData.MsgState.Success)
                                        {
                                            ImportData.CheckInfo ci = new ImportData.CheckInfo();
                                            ci.m_iCheckID = (long)CheckID;
                                            ci.m_DateBegin = pairInspImport.Value.m_Check.m_dtBegin;
                                            ci.m_DateEnd = pairInspImport.Value.m_Check.m_dtEnd;
                                            ci.m_strBranch = pairImport.Value.m_strBranch;
                                            ci.m_strSubject = pairImport.Value.m_strSubject;
                                            ci.m_strSubstation = pairImport.Value.m_strSubstation;
                                            m_listChecks.Add(ci);

                                            iCntCheckInsert++;
                                        }
                                        else
                                        {
                                            iCntCheckError++;
                                            AddMsg(Msg);
                                        }
                                    }
                                }

                                // вносим все данные по этому обследованию
                                Msg = ImportData.InsertInspection(connection, pairInspImport.Value, DeleteInspectionID, (long)pairImport.Value.m_iEquipmentIDForImport, pairImport.Value.m_strEquipmentName, pairImport.Value.m_strEquipmentNumber, CheckID, ref m_dictRefs);
                                if (Msg.Key == ImportData.MsgState.Success) iCntInspInsert++;
                                else
                                {
                                    iCntInspError++;
                                    AddMsg(Msg);
                                }
                            }
                        }
                    }
                }
            }
            AddMsg(new KeyValuePair<ImportData.MsgState, string>(ImportData.MsgState.Success, "Анализ данных по обследованиям завершен: добавлено: " + iCntCheckInsert.ToString() + " проверок, " + iCntInspInsert.ToString() + " обследований, ошибочных: " + iCntCheckError.ToString() + " проверок, " + iCntInspError.ToString() + " обследований"));

            connection.Close();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string strConnectionString = "data source=" + tePath.Text;

                AddMsg(new KeyValuePair<ImportData.MsgState, string>(ImportData.MsgState.Info, "Получение данных для дальнейшей обработки из импортируемой базы..."));

                m_dictRefs = null;
                m_listChecks = null;
                KeyValuePair<ImportData.MsgState, string> Msg = ImportData.LoadData(strConnectionString, ref m_listImportData, ref m_listChecks, ref m_dictRefs);
                AddMsg(Msg);
                if (Msg.Key == 0) return;

                AddMsg(new KeyValuePair<ImportData.MsgState, string>(ImportData.MsgState.Info, "Получение данных для дальнейшей обработки из текущей базы..."));
                m_dictRefs = new Dictionary<string, Dictionary<KeyValuePair<long, string>, long>>();
                m_listChecks = new List<ImportData.CheckInfo>();
                Msg = ImportData.LoadData(global::DiarMain.Properties.Settings.Default.diarConnectionString, ref m_listData, ref m_listChecks, ref m_dictRefs);
                AddMsg(Msg);
                if (Msg.Key == 0) return;

                AnalizeData();

                AddMsg(new KeyValuePair<ImportData.MsgState, string>(ImportData.MsgState.Info, "Загрузка данных завершена"));
            }
            catch (Exception ex)
            {
                AddMsg(new KeyValuePair<ImportData.MsgState, string>(ImportData.MsgState.Error, ex.Message));
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_bEnd) e.Cancel = true;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_bEnd = true;
            this.Cursor = System.Windows.Forms.Cursors.Default;
            bActivation.Enabled = true;
        }
    }
}