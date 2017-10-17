using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress;
using DevExpress.Utils;
using System.Text;
using System.Management;
using System.Drawing;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Reflection;

namespace DiarMain
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static Decimal m_strVersion = new Decimal(0);

        static void RegisterAssembly(string strDir)
        {
            Assembly SkinFHA = Assembly.LoadFile(Environment.CurrentDirectory + strDir +"/SkinFHA.dll");
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(SkinFHA);

            Assembly SkinHARG = Assembly.LoadFile(Environment.CurrentDirectory + strDir + "/SkinHARG.dll");
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(SkinHARG);

            Assembly SkinVibro = Assembly.LoadFile(Environment.CurrentDirectory + strDir + "/SkinVibro.dll");
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(SkinVibro);

            Assembly SkinVisual = Assembly.LoadFile(Environment.CurrentDirectory + strDir + "/SkinVisual.dll");
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(SkinVisual);

            Assembly SkinParameter = Assembly.LoadFile(Environment.CurrentDirectory + strDir + "/SkinParameter.dll");
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(SkinParameter);

            Assembly SkinWarm = Assembly.LoadFile(Environment.CurrentDirectory + strDir + "/SkinWarm.dll");
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(SkinWarm);

            Assembly SkinPassport = Assembly.LoadFile(Environment.CurrentDirectory + strDir + "/SkinPassport.dll");
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(SkinPassport);

            Assembly SkinReport = Assembly.LoadFile(Environment.CurrentDirectory + strDir + "/SkinReport.dll");
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(SkinReport);

            Assembly SkinAdd = Assembly.LoadFile(Environment.CurrentDirectory + strDir + "/SkinAdd.dll");
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(SkinAdd);

            Assembly SkinAdd2 = Assembly.LoadFile(Environment.CurrentDirectory + strDir + "/SkinAdd2.dll");
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(SkinAdd2);

            Assembly SkinElectrical = Assembly.LoadFile(Environment.CurrentDirectory + strDir + "/SkinElectrical.dll");
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(SkinElectrical);
        }

        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory);

                AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
                {
                    // В вашем случае в args.Name будет лежать строка
                    // Oracle.ManagedDataAccess, Version=4.121.2.0, Culture=neutral, PublicKeyToken=89b483f429c47342
                    // Также сюда вам необходимо каким-то образом передать каталог, где лежит COM-компонент
                    // и зависимая сборка (например, считать через реестр: HKEY_CLASSES_ROOT\CLSID\{guid_вашего_компонента}\InprocServer32)
                    // Пусть comPath - каталог, где лежит ваша зависимая сборка

                    string depAssemblyFileName = new AssemblyName(args.Name).Name + ".dll";
                    string depAssemblyPath = Path.Combine("", depAssemblyFileName);
                    return Assembly.Load(depAssemblyPath);
                };

                //DevExpress.UserSkins.SkinFHA fha = new DevExpress.UserSkins.SkinFHA();
                //string str = fha.SkinInfo1[0];


                //string str = typeof(DevExpress.UserSkins.SkinFHA).ToString();
                try
                {
                    RegisterAssembly("/DLL");                    
                }
                catch(Exception ex)
                {
                    RegisterAssembly("");
                }

                /*DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.SkinHARG).Assembly); //Register!
                DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.SkinVibro).Assembly); //Register!
                DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.SkinVisual).Assembly); //Register!
                DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.SkinParameter).Assembly); //Register!
                DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.SkinWarm).Assembly); //Register!
                DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.SkinPassport).Assembly); //Register!
                DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.SkinReport).Assembly); //Register!
                DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.SkinAdd).Assembly); //Register!
                DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.SkinAdd2).Assembly); //Register!
                DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.SkinElectrical).Assembly); //Register!*/

                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.UserSkins.OfficeSkins.Register();

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "DB\\diar.sqlite"))
                {
                    Properties.Settings.Default["diarConnectionString"] = "data source=" + AppDomain.CurrentDomain.BaseDirectory + "DB\\diar.sqlite";
                }
                else
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "diar.sqlite"))
                    {
                        Properties.Settings.Default["diarConnectionString"] = "data source=" + AppDomain.CurrentDomain.BaseDirectory + "diar.sqlite";
                    }
                    else
                    {
                        MyLocalizer.XtraMessageBoxShow("Не удалось подключиться к базе данных. Описание: отсутствует файл diar.sqlite.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                AppearanceObject.DefaultFont = new Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

                // -----------------
                // проверка лицензии
                // -----------------

                string testString = GetUniqueHardwaeId();

                byte[] key = ASCIIEncoding.ASCII.GetBytes("DIAR");

                RC4 encoder = new RC4(key);
                byte[] testBytes = ASCIIEncoding.ASCII.GetBytes(testString);
                byte[] result = encoder.Encode(testBytes, testBytes.Length);
                string encryptedString = encoder.GetByteString(result);// ASCIIEncoding.ASCII.GetString(result);

                SQLiteConnection con = new SQLiteConnection(global::DiarMain.Properties.Settings.Default.diarConnectionString);
                bool bNoLicense = true;
                long bAddPassportMessage = 0;
                string strLicenseCode = "";
                try
                {
                    con.Open();

                    SQLiteCommand com = new SQLiteCommand(con);
                    com.CommandType = CommandType.Text;

                    com.CommandText = "Select Code, AddPassportMessage FROM Licenses WHERE Code = '" + encryptedString + "'";

                    SQLiteDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        bNoLicense = false;
                        strLicenseCode = dr["Code"].ToString();
                        bAddPassportMessage = Convert.ToInt64(dr["AddPassportMessage"]);
                    }
                    dr.Close();

                    con.Close();
                }
                catch (SQLiteException ex)
                {
                    MyLocalizer.XtraMessageBoxShow("Не удалось подключиться к базе данных. Описание: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                /*MyLocalizer.XtraMessageBoxShow(encryptedString, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                byte[] key2 = ASCIIEncoding.ASCII.GetBytes("DIAR");

                RC4 decoder = new RC4(key2);
                result = decoder.SetByteString(encryptedString);
                byte[] decryptedBytes = decoder.Decode(result, result.Length);
                string decryptedString = ASCIIEncoding.ASCII.GetString(decryptedBytes);

                MyLocalizer.XtraMessageBoxShow(decryptedString, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);*/

                // -----------------

                if (bNoLicense)
                {
                    LicenseForm f = new LicenseForm();
                    f.m_strCode = testString;
                    Application.Run(f);
                }
                else
                {
                    try
                    {
                        con.Open();
                        SQLiteCommand com = new SQLiteCommand(con);
                        com.CommandText = "Select CurVersion from Version";
                        com.CommandType = CommandType.Text;
                        SQLiteDataReader dr = com.ExecuteReader();

                        //Decimal curVersion = new decimal(0);
                        while (dr.Read())
                        {
                            m_strVersion = Convert.ToDecimal(dr.GetValue(0));
                        }
                        dr.Close();
                        con.Close();

                        ServicePackForm sp = new ServicePackForm();
                        if (m_strVersion < sp.m_CurVersion)
                        {
                            sp.m_DBVersion = m_strVersion;
                            if (sp.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            {
                                return;
                            };
                        }

                        // считываем доступные типы
                        con.Open();
                        SQLiteCommand comEquipmentKinds = new SQLiteCommand(con);
                        comEquipmentKinds.CommandText = "Select EquipmentKindID, EquipmentKindName FROM EquipmentKinds WHERE Disable = 0";
                        comEquipmentKinds.CommandType = CommandType.Text;
                        SQLiteDataReader drEquipmentKinds = comEquipmentKinds.ExecuteReader();
                        if (drEquipmentKinds.HasRows)
                        {
                            while (drEquipmentKinds.Read())
                            {
                                Inspection.m_dictActualEquipmentKinds[(Equipment.EquipmentKind)Convert.ToInt64(drEquipmentKinds["EquipmentKindID"])] = drEquipmentKinds["EquipmentKindName"].ToString();
                            }
                        }
                        drEquipmentKinds.Close();

                        con.Close();
                    }
                    catch (SQLiteException ex)
                    {
                        MyLocalizer.XtraMessageBoxShow("Не удалось подключиться к базе данных. Описание: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.Transformer] = new List<Inspection.InspectionType>();
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.Transformer].Add(Inspection.InspectionType.Visual);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.Transformer].Add(Inspection.InspectionType.FHA);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.Transformer].Add(Inspection.InspectionType.HARG);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.Transformer].Add(Inspection.InspectionType.Warm);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.Transformer].Add(Inspection.InspectionType.Vibro);

                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.AirSwitch] = new List<Inspection.InspectionType>();
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.AirSwitch].Add(Inspection.InspectionType.Visual);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.AirSwitch].Add(Inspection.InspectionType.Warm);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.AirSwitch].Add(Inspection.InspectionType.Parameter);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.AirSwitch].Add(Inspection.InspectionType.Electrical);

                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilLessSwitch] = new List<Inspection.InspectionType>();
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilLessSwitch].Add(Inspection.InspectionType.Visual);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilLessSwitch].Add(Inspection.InspectionType.Warm);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilLessSwitch].Add(Inspection.InspectionType.Parameter);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilLessSwitch].Add(Inspection.InspectionType.Electrical);

                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilTankSwitch] = new List<Inspection.InspectionType>();
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilTankSwitch].Add(Inspection.InspectionType.Visual);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilTankSwitch].Add(Inspection.InspectionType.FHA);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilTankSwitch].Add(Inspection.InspectionType.HARG);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilTankSwitch].Add(Inspection.InspectionType.Warm);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilTankSwitch].Add(Inspection.InspectionType.Parameter);
                    Inspection.m_listEquipmentInspections[Equipment.EquipmentKind.OilTankSwitch].Add(Inspection.InspectionType.Electrical);

                    Inspection.m_dictInspections[Inspection.InspectionType.Visual] = new KeyValuePair<string, string>("Визуальное обследование", "Visual");
                    Inspection.m_dictInspections[Inspection.InspectionType.FHA] = new KeyValuePair<string, string>("ФХА", "FHA");
                    Inspection.m_dictInspections[Inspection.InspectionType.HARG] = new KeyValuePair<string, string>("ХАРГ", "HARG");
                    Inspection.m_dictInspections[Inspection.InspectionType.Warm] = new KeyValuePair<string, string>("Тепловизионный контроль", "Warm");
                    Inspection.m_dictInspections[Inspection.InspectionType.Vibro] = new KeyValuePair<string, string>("Вибрационное обследование", "Vibro");
                    Inspection.m_dictInspections[Inspection.InspectionType.Parameter] = new KeyValuePair<string, string>("Определение характеристик", "Parameter");
                    Inspection.m_dictInspections[Inspection.InspectionType.Electrical] = new KeyValuePair<string, string>("Электрические измерения", "Electrical");

                    MainCheckForm f = new MainCheckForm();
                    /*f.m_bAddPassportMessage = false;
                    if (bAddPassportMessage != 0) f.m_bAddPassportMessage = true;
                    f.m_strLicenseCode = strLicenseCode;*/
                    //f.ShowDialog();
                    Application.Run(f);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "(" + ex.StackTrace + ")", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GetUniqueHardwaeId()
        {
            /*Dictionary<string, string> ids =
            new Dictionary<string, string>();

            ManagementObjectSearcher searcher;

            //процессор
            searcher = new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT * FROM Win32_Processor");
            foreach (ManagementObject queryObj in searcher.Get())
                ids.Add("ProcessorId", queryObj["ProcessorId"].ToString());

            //мать
            searcher = new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT * FROM CIM_Card");
            foreach (ManagementObject queryObj in searcher.Get())
                ids.Add("CardID", queryObj["SerialNumber"].ToString());

            //клавиатура
            searcher = new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT * FROM CIM_KeyBoard");
            foreach (ManagementObject queryObj in searcher.Get())
                ids.Add("KeyBoardID", queryObj["DeviceId"].ToString());

            //ОС
            searcher = new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT * FROM CIM_OperatingSystem");
            foreach (ManagementObject queryObj in searcher.Get())
                ids.Add("OSSerialNumber", queryObj["SerialNumber"].ToString());

            //мышь
            searcher = new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT * FROM Win32_PointingDevice");
            foreach (ManagementObject queryObj in searcher.Get())
                ids.Add("MouseID", queryObj["DeviceID"].ToString());

            //звук
            searcher = new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT * FROM Win32_SoundDevice");
            foreach (ManagementObject queryObj in searcher.Get())
                ids.Add("SoundCardID", queryObj["DeviceID"].ToString());

            //CD-ROM
            searcher = new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT * FROM Win32_CDROMDrive");
            foreach (ManagementObject queryObj in searcher.Get())
                ids.Add("CDROMID", queryObj["DeviceID"].ToString());

            //UUID
            searcher = new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT UUID FROM Win32_ComputerSystemProduct");
            foreach (ManagementObject queryObj in searcher.Get())
                ids.Add("UUID", queryObj["UUID"].ToString());

            foreach (var x in ids)
                richTextBox1.Text += x.Key + ": " + x.Value + "\r\n";*/

            /*StringBuilder sb = new StringBuilder();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                  "SELECT * FROM Win32_Processor");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                sb.Append(queryObj["NumberOfCores"]);
                sb.Append(queryObj["ProcessorId"]);
                sb.Append(queryObj["Name"]);
                sb.Append(queryObj["SocketDesignation"]);

                //Console.WriteLine(queryObj["ProcessorId"]);
                //Console.WriteLine(queryObj["Name"]);
                //Console.WriteLine(queryObj["SocketDesignation"]);
            }

            searcher = new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_BIOS");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                sb.Append(queryObj["Manufacturer"]);
                sb.Append(queryObj["Name"]);
                sb.Append(queryObj["Version"]);

                //Console.WriteLine(queryObj["Manufacturer"]);
                //Console.WriteLine(queryObj["Name"]);
                //Console.WriteLine(queryObj["Version"]);
            }

            searcher = new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT * FROM Win32_BaseBoard");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                sb.Append(queryObj["Product"]);
                //Console.WriteLine(queryObj["Product"]);
            }

            var bytes = Encoding.ASCII.GetBytes(sb.ToString());
            SHA256Managed sha = new SHA256Managed();

            byte[] hash = sha.ComputeHash(bytes);

            return BitConverter.ToString(hash);*/

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT UUID FROM Win32_ComputerSystemProduct");

            foreach (ManagementObject queryObj in searcher.Get())
                return queryObj["UUID"].ToString();

            return "";
        }
    }
}
