using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhonesBook
{
    public partial class MainForm : Form
    {
        private System.Diagnostics.FileVersionInfo myFileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
        private ContextMenu contextMenu1;
        private bool buttonAboutForm;
        private bool buttonAboutUsingForm;

        private string UserWindowsAuthorization = ";Persist Security Info=True";

        /// <summary>
        /// access to MS SQL server. form URL - 'server.domain'
        /// </summary>
        private string ServerName;

        /// <summary>
        /// access to MS SQL server. form - 'login'
        /// </summary>
        private string UserLogin;

        /// <summary>
        /// access to MS SQL server. form - 'password'
        /// </summary>
        private string UserPassword;

        /// <summary>
        /// string connection to sql server
        /// </summary>
        private string strConnection;

        /// <summary>
        /// folder with stored people' photos
        /// </summary>
        private string sFolderPhotos = @".\Photos\";

        /// <summary>
        /// keys for encryption-decryption
        /// </summary>
        private readonly byte[] btsMess1 = Convert.FromBase64String(@"OCvesunvXXsxtt381jr7vp3+UCwDbE4ebdiL1uinGi0="); //Key Encrypt
        private readonly byte[] btsMess2 = Convert.FromBase64String(@"NO6GC6Zjl934Eh8MAJWuKQ=="); //Key Decrypt

        /// <summary>
        /// Registry path to app
        /// </summary>
        private string myRegKey = @"SOFTWARE\RYIK\PhonesBook";
       
        private int iRowRecords = 0;
        private int iRowFIO = 0;
        private HashSet<string> lData = new HashSet<string>();
        private HashSet<string> hFullData = new HashSet<string>();
        private string[] sCellRow;
        private string sLastGotData = "";

        private DataTable dtPeriod = new DataTable("PeriodListData");
        private DataColumn[] dcPeriod ={
                                  new DataColumn("№ п/п",typeof(double)),
                                  new DataColumn("Номер телефона",typeof(string)),
                                  new DataColumn("ФИО ответственного",typeof(string)),
                                  new DataColumn("NAV",typeof(string)),
                                  new DataColumn("Подразделение",typeof(string)),
                                  new DataColumn("Используется как",typeof(string)),
                                  new DataColumn("Тариф действует с",typeof(string)),
                                  new DataColumn("Тариф действовал по",typeof(string)),
                              };
        DataColumn[] dcPeriodkeys = new DataColumn[2];
        private DataTable dtPeriodShow = new DataTable("PeriodListData");
        private DataColumn[] dcPeriodShow ={
                                  new DataColumn("№ п/п",typeof(double)),
                                  new DataColumn("Номер телефона",typeof(string)),
                                  new DataColumn("ФИО ответственного",typeof(string)),
                                  new DataColumn("NAV",typeof(string)),
                                  new DataColumn("Подразделение",typeof(string)),
                                  new DataColumn("Используется как",typeof(string)),
                                  new DataColumn("Тариф действует с",typeof(string)),
                                  new DataColumn("Тариф действовал по",typeof(string)),
                              };
        DataColumn[] dcPeriodShowkeys = new DataColumn[1];

        private List<string> lTarifData = new List<string>();

        private DataTable dtTarif = new DataTable("TarifListData");
        private DataColumn[] dcTarif ={
                                  new DataColumn("№ п/п",typeof(double)),
                                  new DataColumn("Номер телефона",typeof(string)),
                                  new DataColumn("ФИО ответственного",typeof(string)),
                                  new DataColumn("NAV",typeof(string)),
                                  new DataColumn("Подразделение",typeof(string)),
                                  new DataColumn("Используется как",typeof(string)),
                                  new DataColumn("Тариф действует с",typeof(string)),
                                  new DataColumn("Модель компенсации",typeof(string)),
                                  new DataColumn("Тарифный пакет",typeof(string)),
                              };

        private DataTable dtTarifShow = new DataTable("ListData");
        private DataColumn[] dcTarifShow ={
                                  new DataColumn("№ п/п",typeof(double)),
                                  new DataColumn("Номер телефона",typeof(string)),
                                  new DataColumn("ФИО ответственного",typeof(string)),
                                  new DataColumn("NAV",typeof(string)),
                                  new DataColumn("Подразделение",typeof(string)),
                                  new DataColumn("Используется как",typeof(string)),
                                  new DataColumn("Тариф действует с",typeof(string)),
                                  new DataColumn("Модель компенсации",typeof(string)),
                                  new DataColumn("Тарифный пакет",typeof(string)),
                              };

        private List<string> lFullData = new List<string>();

        private DataTable dtFull = new DataTable("FullListData");
        private DataColumn[] dcFull ={
                                  new DataColumn("№ п/п",typeof(double)),
                                  new DataColumn("ФИО ответственного",typeof(string)),
                                  new DataColumn("Номер телефона",typeof(string)),
                                  new DataColumn("Подразделение",typeof(string)),
                                  new DataColumn("Должность",typeof(string)),
                                  new DataColumn("Адрес",typeof(string)),
                                  new DataColumn("Дата приема",typeof(string)),
                                  new DataColumn("Дата увольнения",typeof(string)),
                                  new DataColumn("NAV",typeof(string)),
                              };
        private DataTable dtFullShow = new DataTable("FullListData");
        private DataColumn[] dcFullShow ={
                                  new DataColumn("№ п/п",typeof(double)),
                                  new DataColumn("NAV",typeof(string)),
                                  new DataColumn("ФИО ответственного",typeof(string)),
                                  new DataColumn("Должность",typeof(string)),
                                  new DataColumn("Подразделение",typeof(string)),
                                  new DataColumn("Адрес",typeof(string)),
                                  new DataColumn("Номер телефона",typeof(string)),
                                  new DataColumn("Дата приема",typeof(string)),
                                  new DataColumn("Дата увольнения",typeof(string)),
                              };

        private string[] getEmployeePhones =
     {
            "Код карточки",     //0
            "Ф.И.О.",
            "Должность",        //2
            "ТСП",
            "Город",            //4
            "Адрес",
            "Рабочий телефон",  //6
            "Домашний телефон",
            "Телефон мобильный",//8
            "Дата Приема",
            "Дата Увольнения"   //10
        };
        private int[] getEmployeePhonesCollumns =
            {
            0,//"Код карточки",     //0
            0,//"Ф.И.О.",
            0,//"Должность",        //2
            0,// "ТСП",
            0,//"Город",            //4
            0,//"Адрес",
            0,//"Рабочий телефон",  //6
            0,// "Домашний телефон",
            0,// "Телефон мобильный",//8
            0,// "Дата Приема",
            0// "Дата Увольнения"   //10
        };

        private string[] getTAS =
            {
            "Код карточки",     //0
            "ФИО сотрудника",
            "Должность",        //2
            "Подразделение",
            "Город прописки",   //4
            "Адрес 1",
            "Рабочий телефон - нет",  //6
            "Домашний телефон - нет",
            "Телефон мобильный - нет",//8
            "Дата приема",
            "Дата увольнения"   //10
        };


        private Label labelServer;
        private TextBox textBoxServer;
        private Label labelServerUserName;
        private TextBox textBoxServerUserName;
        private Label labelServerUserPassword;
        private TextBox textBoxServerUserPassword;
        private Label labelFolderPhotos;
        private TextBox textFolderPhotos;
        private ToolTip toolTip1 = new ToolTip();
        private Bitmap bmp;
        private string sSelectedNAV;
        private string sSelectedFIO;
        private string sSelectedPhone;
        private string sSelected4;
        private string sSelected5;
        private string sSelected6;
        private Label lString1;
        private Label lString2;
        private Label lString3;
        private Label lString4;
        private Label lString5;
        private Label lString6;

        //current period
        private DateTime nowDay = DateTime.Now;
        private DateTime firstDay = DateTime.Now;
        private DateTime lastDay = DateTime.Now;
        private string dayStartSearch;
        private string dayEndSearch;


        public MainForm()
        { InitializeComponent(); }

        private void FormPhonesBook1_Load(object sender, EventArgs e)
        { LoadForm(); }

        private void LoadForm()
        {
            //  dateTime1.Format = DateTimePickerFormat.Time;
            dateTime1.CustomFormat = "yyyy-MMM-dd";
            //search first and last day within current month
            firstDay = new DateTime(nowDay.Year, nowDay.Month, 1);
            lastDay = firstDay.AddMonths(1).AddDays(-1);
            //convert dates into custom format - '2018-05-01'
            dayStartSearch = string.Format("{0:yyyy-MM-dd}", firstDay); //'2018-05-01'
            dayEndSearch = string.Format("{0:yyyy-MM-dd}", lastDay); //'2018-05-31'

            buttonShowAll.Enabled = false;
            panelView.Visible = false;
            dataGridView1.Visible = true;
            dataGridView1.BringToFront();
            ListTarifItem.Visible = false;
            ListModelItem.Visible = false;

            bmp = new Bitmap(Properties.Resources.LogoRYIK, logoPictureBox.Width, logoPictureBox.Height);
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoPictureBox.BorderStyle = BorderStyle.None;
            RefreshPictureBox(logoPictureBox, bmp);

            panelViewResize();
            CheckRegistrySavedData();

            LoadFromServerWithModelItem.ToolTipText = "Загрузить список ФИО/телефонов с сервера T-factura с тарифными пакетами и моделями оплат сотрудниками";

            toolTip1.SetToolTip(textBoxData, "Поле ввода данных для поиска.\nЭто может быть ФИО полностью или его часть\n" +
            "Это может быть весь номер или же часть его.\nЭто может быть NAV-код или часть наименования организации");
            LoadFromServerItem.ToolTipText = "Загрузить список ФИО/телефонов с сервера T-factura";
            LoadFromFileItem.ToolTipText = "Загрузить список ФИО/телефонов с текстового файла";
            HelpMenu.ToolTipText = "Сгенерировать текстовый файл с шаблонным набором полей, пригодный для импорта в данную программу через пункт меню\n\"Загрузить список из файла\"";
            SetUpItem.ToolTipText = "Внести настройки в программу,\nнеобходимые для выполнения корректного подключения и авторизации на сервере Т-Factura.\n" +
                "Все настройки хранятся в профиле пользователя в зашифрованном виде.";
            SelectListMenu.ToolTipText = "Построить список для быстрого набора по соответствующему критерию поиска - телефоны/ФИО/NAV и т.д.";

            toolTip1.SetToolTip(checkBoxActiveNumbers, "Загрузить только активные на данный момент номера");

            FindOwnersItem.Enabled = false;
            SelectListMenu.Enabled = false;

            dtTarif.Columns.AddRange(dcTarif);
            //   dtTarif.PrimaryKey = dcTarif;
            dtTarifShow.Columns.AddRange(dcTarifShow);
            //  dtTarifShow.PrimaryKey = dcTarifShow;

            dtFull.Columns.AddRange(dcFull);
            // dtFull.PrimaryKey = dcFull;
            dtFullShow.Columns.AddRange(dcFullShow);
            //    dtFullShow.PrimaryKey = dcFullShow;

            dtPeriod.Columns.AddRange(dcPeriod);
            dcPeriodkeys[0] = dcPeriod[1];
            dcPeriodkeys[1] = dcPeriod[2];
            //    dtPeriod.PrimaryKey = dcPeriodkeys;
            dtPeriodShow.Columns.AddRange(dcPeriodShow);
            dcPeriodShowkeys[0] = dcPeriodShow[0];
            // dcPeriodShowkeys[1] = dcPeriodShow[2];
            //    dtPeriodShow.PrimaryKey = dcPeriodShowkeys;

            StatusLabel2.Text = "";
            StatusLabel3.Text = "";

            myFileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            StatusLabel1.Text = myFileVersionInfo.ProductName + " ver." + myFileVersionInfo.FileVersion + " " + myFileVersionInfo.LegalCopyright;
            StatusLabel1.Alignment = ToolStripItemAlignment.Right;

            contextMenu1 = new ContextMenu();  //Context Menu on notify Icon
            notifyIcon1.ContextMenu = contextMenu1;
            contextMenu1.MenuItems.Add("About", AboutSoft);
            contextMenu1.MenuItems.Add("Exit", ApplicationExit);
            notifyIcon1.Text = myFileVersionInfo.ProductName + "\nv." + myFileVersionInfo.FileVersion + "\n" + myFileVersionInfo.CompanyName;
            this.Text = myFileVersionInfo.Comments;
            toolTip1.SetToolTip(logoPictureBox, "Разработчик: " + myFileVersionInfo.LegalCopyright);
        }

        private void ApplicationExit(object sender, EventArgs e) //for the Context Menu on notify Icon
        { ApplicationExit(); }

        private void ApplicationExit()
        { Application.Exit(); }

        private void AboutSoft(object sender, EventArgs e) //for the Context Menu on notify Icon
        { AboutSoft(); }

        private void AboutSoft()
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
            buttonAboutForm = aboutBox.OKButtonClicked;
        }

        private void usingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUsingBox aboutUsingBox = new AboutUsingBox();
            aboutUsingBox.Show();
            buttonAboutUsingForm = aboutUsingBox.OKButtonClicked;
        }

        private void CheckRegistrySavedData()
        {
            string server, user, password;
            try
            {
                using (Microsoft.Win32.RegistryKey EvUserKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(myRegKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey))
                {
                    server = DecryptBase64ToString(EvUserKey.GetValue("ServerName").ToString(), btsMess1, btsMess2);
                    user = DecryptBase64ToString(EvUserKey.GetValue("UserLogin").ToString(), btsMess1, btsMess2);
                    password = DecryptBase64ToString(EvUserKey.GetValue("UserPassword").ToString(), btsMess1, btsMess2);
                    sFolderPhotos = EvUserKey?.GetValue("FolderPhotos").ToString();
                }

                CheckSqlServer(server, user, password);
            }
            catch { }
        }
        private bool serverDied = false;

        public void CheckSqlServer(string serverName, string userName, string userPassword)
        {
            if (string.IsNullOrEmpty(serverName))
                throw new ArgumentNullException($"{nameof(serverName)} не должен быть пустым");

            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException($"{nameof(userName)} не должен быть пустым");

            if (string.IsNullOrEmpty(userPassword))
                throw new ArgumentNullException($"{nameof(userPassword)} не должен быть пустым");

            ToolStripStatusLabelAddText(StatusLabel3, "Проверяю доступность сервера и баз " + serverName);
            ToolStripStatusLabelBackColor(StatusLabel3, SystemColors.Control);

            string stringConnection = 
                $"Data Source={serverName}; Initial Catalog=EBP; Type System Version=SQL Server 2005{UserWindowsAuthorization};" +
                $"User ID={userName}; Password={userPassword}; Connect Timeout=5";

            string sqlQuery = @"SELECT database_id FROM sys.databases WHERE Name ='EBP'";
            try
            {

                using (var sqlConnection = new System.Data.SqlClient.SqlConnection(stringConnection))
                {
                    sqlConnection.Open();

                    using (var sqlCommand = new System.Data.SqlClient.SqlCommand(sqlQuery, sqlConnection))
                    { sqlCommand.ExecuteScalar(); }
                    sqlConnection.Close();

                    ServerName = serverName;
                    UserLogin = userName;
                    UserPassword = userPassword;
                    strConnection = stringConnection;
                }
                ToolStripStatusLabelAddText(StatusLabel3, "");
            }
            catch
            {
                serverDied = true;
                ToolStripStatusLabelAddText(StatusLabel3, "Не доступно: " + serverName + " ");
                ToolStripStatusLabelBackColor(StatusLabel3, Color.DarkOrange);
            }
        }

        private void ToolStripStatusLabelAddText(ToolStripStatusLabel statusLabel, string s) //add string into  from other threads
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(delegate { statusLabel.Text = s; }));
            else
                statusLabel.Text = s;
        }

        private void ToolStripStatusLabelForeColor(ToolStripStatusLabel statusLabel, Color s)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(delegate { statusLabel.ForeColor = s; }));
            else
                statusLabel.ForeColor = s;
        }

        private void ToolStripStatusLabelBackColor(ToolStripStatusLabel statusLabel, Color s)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(delegate { statusLabel.BackColor = s; }));
            else
                statusLabel.ForeColor = s;
        }

        private void ListPhonesItem_Click(object sender, EventArgs e)
        { MakeListData("phone_no"); }

        private void ListFioItem_Click(object sender, EventArgs e)
        { MakeListData("emp_name"); }

        private void ListNavItem_Click(object sender, EventArgs e)
        { MakeListData("NAV"); }

        private void ListOrgItem_Click(object sender, EventArgs e)
        { MakeListData("org_unit_name"); }

        private void ListModelItem_Click(object sender, EventArgs e)
        { MakeListData("Модель компенсации"); }

        private void ListTarifItem_Click(object sender, EventArgs e)
        { MakeListData("Тарифный пакет"); }

        private void LoadFromServerItem_Click(object sender, EventArgs e) //use
        { LoadFromServer(); }

        private List<string> LoadFromServerActiveNumbers()
        {
            string sSqlQuery;

            List<string> phones = new List<string>();

            using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(strConnection))
            {
                sqlConnection.Open();
                sSqlQuery =
                   " SELECT end_dt, phone_no" +
                   " FROM v_dp_contract_bill" +
                   " WHERE end_dt = " +
                   "(SELECT max(end_dt) FROM v_dp_account_bill)";


                using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(sSqlQuery, sqlConnection))
                {
                    using (System.Data.SqlClient.SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        foreach (System.Data.Common.DbDataRecord record in sqlReader)
                        {
                            if (record["phone_no"]?.ToString()?.Length > 0)
                            {
                                phones.Add(MakeCommonViewPhone(record["phone_no"]?.ToString()));
                            }
                        }
                    }
                }
            }

            return phones;
        }

        private void GetDateTime1()
        {
            //search first and last day within current month
            firstDay = new DateTime(dateTime1.Value.Year, dateTime1.Value.Month, 1);
            lastDay = firstDay.AddMonths(1).AddDays(-1);
            //convert dates into custom format - '2018-05-01'
            dayStartSearch = string.Format("{0:yyyy-MM-dd}", firstDay); //'2018-05-01'
            dayEndSearch = string.Format("{0:yyyy-MM-dd}", lastDay); //'2018-05-31'

        }

        private async void LoadFromServer()
        {
            bool loadActiveNumbers = !checkBoxActiveNumbers.Checked; //get only active numbers

            GetDateTime1();

            buttonShowAll.Enabled = false;
            ListTarifItem.Visible = false;
            ListModelItem.Visible = false;
            sLastGotData = "LoadFromServer";

            //Check alive server
            ToolStripStatusLabelBackColor(StatusLabel3, SystemColors.Control);
            serverDied = false;
            StatusLabel3.Text = "Проверяю доступность " + ServerName;
            StatusLabel3.BackColor = SystemColors.Control;
            LoadFromServerItem.Enabled = false;
            SettingsMenu.Enabled = false;

            await System.Threading.Tasks.Task.Run(() =>
            CheckSqlServer(ServerName, UserLogin, UserPassword));

            if (!serverDied)
            {
                GetData(loadActiveNumbers);
                if (dtPeriod.Rows.Count > 1)
                {
                    StatusLabel3.Text = "Обрабатываю полученные данные...";
                    StatusLabel3.ForeColor = Color.Black;
                    FindOwnersItem.Enabled = true;
                    SelectListMenu.Enabled = true;
                    MakeListData("emp_name");
                    MakeTable();
                    StatusLabel2.Text = "Всего ФИО - " + iRowFIO.ToString() + " | Всего номеров - " + dtPeriod.Rows.Count.ToString();
                    StatusLabel3.Text = "Готово!";
                    dataGridView1.Columns[7].Visible = false;
                }
                else
                {
                    StatusLabel3.Text = "Данные с выбранного сервера не получены!";
                    StatusLabel3.ForeColor = Color.DarkRed;
                }
            }
            LoadFromServerItem.Enabled = true;
            SettingsMenu.Enabled = true;
        }

        private void GetData(bool looadAllNumbers)
        {
            List<string> phonesActive = LoadFromServerActiveNumbers();
            string number = ""; bool numberAlive;
            string lostNumbers = null;
            string sSqlQuery;

            try
            {
                using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(strConnection))
                {
                    sqlConnection.Open();
                    sSqlQuery = "SELECT t1.phone_no AS phone_no, t1.emp_name AS emp_name, t1.org_unit_name AS org_unit_name, t1.till_dt AS till_dt, t1.from_dt as from_dt, t1.descr AS main, os_emp.emp_cd AS NAV " +
                        "FROM v_rs_contract_detail t1 INNER JOIN os_emp ON os_emp.emp_id=t1.emp_id ";
                    //    if (dayStartSearch.Length > 0)
                    //    {
                    sSqlQuery +=
                    "WHERE emp_name is not null " +
                   //search info in current period
                   " AND (t1.till_dt IS null OR t1.till_dt > '" +
                   dayStartSearch +
                   "') AND t1.from_dt < '" +
                   dayEndSearch +
                   "' AND state LIKE '%A%'"; //only an active contract
                                             //     }
                    sSqlQuery += " ORDER by emp_name, phone_no";

                    dtPeriod.Rows.Clear();
                    iRowRecords = 0;

                    using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(sSqlQuery, sqlConnection))
                    {
                        using (System.Data.SqlClient.SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                        {
                            foreach (System.Data.Common.DbDataRecord record in sqlReader)
                            {
                                number = record["phone_no"]?.ToString();
                                numberAlive = phonesActive.Any(x => x == MakeCommonViewPhone(number));
                                if (number?.Length > 0 && record["emp_name"]?.ToString()?.Length > 0)
                                {
                                    if (numberAlive || looadAllNumbers)
                                    {
                                        DataRow row = dtPeriod.NewRow();
                                        row["№ п/п"] = ++iRowRecords;
                                        row["Номер телефона"] = MakeCommonViewPhone(number);
                                        row["ФИО ответственного"] = record["emp_name"].ToString().Trim();
                                        row["NAV"] = record["NAV"]?.ToString()?.Trim();
                                        row["Подразделение"] = record["org_unit_name"]?.ToString()?.Trim();
                                        row["Используется как"] = DefineMainPhone(record["main"]?.ToString());
                                        row["Тариф действует с"] = MakeDateCorrectFormat(record["from_dt"]?.ToString()?.Trim()?.Split(' ')[0]);
                                        dtPeriod.Rows.Add(row);
                                    }

                                    if (!numberAlive)
                                    {
                                        lostNumbers += MakeCommonViewPhone(number) + "\n";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception expt) { MessageBox.Show(expt.ToString()); }

            if (lostNumbers?.Length > 8)
            {
                Task.Run(() =>
                MessageBox.Show("В последнем счете потеряны номера:\n" + lostNumbers)
                );
            }

            List<string> phonesTables = dtPeriod.Rows.OfType<DataRow>()
           .Select(dr => dr.Field<string>("Номер телефона")).Distinct().ToList(); //select list numbers in the table

            List<string> phoness = new List<string>();
            foreach (string str in phonesTables)
            { phoness.Add(MakeCommonViewPhone(str)); }

            string res = null;
            foreach (string str in phonesActive.Except(phoness).ToList())
            { res += str + "\n"; }

            if (res?.Length > 8)
            {
                Task.Run(() =>
                MessageBox.Show("В последнем счете добавлены новые номера по отношению с выбранным периодом:\n" + res) //show lost numbers
                );
            }
        }

        private void MakeListData(string sFindData)
        {
            lData = new HashSet<string>();
            List<string> l = new List<string>();
            string sCellFound = "";

            switch (sFindData.ToLower())
            {
                case ("phone_no"):
                    sCellFound = "Номер телефона";
                    StatusLabel3.Text = "Сгенерирован список телефонов";
                    break;
                case ("номер телефона"):
                    sCellFound = "Номер телефона";
                    StatusLabel3.Text = "Сгенерирован список телефонов";
                    break;
                case ("emp_name"):
                    sCellFound = "ФИО ответственного";
                    StatusLabel3.Text = "Сгенерирован список ФИО";
                    break;
                case ("фио"):
                    sCellFound = "ФИО ответственного";
                    StatusLabel3.Text = "Сгенерирован список ФИО";
                    break;
                case ("nav"):
                    sCellFound = "NAV";
                    StatusLabel3.Text = "Сгенерирован список NAV-кодов";
                    break;
                case ("org_unit_name"):
                    sCellFound = "Подразделение";
                    StatusLabel3.Text = "Сгенерирован список подразделений";
                    break;
                case ("подразделение"):
                    sCellFound = "Подразделение";
                    StatusLabel3.Text = "Сгенерирован список подразделений";
                    break;
                case ("модель компенсации"):
                    sCellFound = "Модель компенсации";
                    StatusLabel3.Text = "Сгенерирован список моделей";
                    break;
                case ("тарифный пакет"):
                    sCellFound = "Тарифный пакет";
                    StatusLabel3.Text = "Сгенерирован список тарифных пакетов";
                    break;
            }

            if (sLastGotData == "LoadFromServer")
            { TableSearchToLdata(dtPeriod, lData, sCellFound); }
            else if (sLastGotData == "LoadFromFile")
            { TableSearchToLdata(dtFull, lData, sCellFound); }
            else if (sLastGotData == "GetDataWithModel")
            { TableSearchToLdata(dtTarif, lData, sCellFound); }

            if (lData.Count > 0)
            {
                iRowFIO = lData.Count;
                try
                {
                    if (this.InvokeRequired)
                        BeginInvoke(new MethodInvoker(delegate
                        {
                            comboBoxData.Items.Clear();
                            comboBoxData.Sorted = true;
                            AutoCompleteStringCollection sourceList = new AutoCompleteStringCollection();
                            sourceList.AddRange(lData.ToArray());
                            comboBoxData.Items.AddRange(lData.ToArray());
                            comboBoxData.AutoCompleteCustomSource = sourceList;
                            comboBoxData.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            comboBoxData.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        }));
                    else
                    {
                        comboBoxData.Items.Clear();
                        comboBoxData.Sorted = true;
                        AutoCompleteStringCollection sourceList = new AutoCompleteStringCollection();
                        sourceList.AddRange(lData.ToArray());
                        comboBoxData.Items.AddRange(lData.ToArray());
                        comboBoxData.AutoCompleteCustomSource = sourceList;
                        comboBoxData.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        comboBoxData.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
                catch { }
            }
            else { iRowFIO = 0; }

            StatusLabel2.Text = "Всего записей: " + iRowFIO.ToString();
        }

        private void SetUpItem_Click(object sender, EventArgs e)
        { ShowSettingsData(); }

        private void ShowSettingsData() //Perform of Controls Settings Data
        {
            dataGridView1.Visible = false;
            logoPictureBox.Visible = false;
            comboBoxData.Visible = false;
            textBoxData.Visible = false;
            buttonShowAll.Visible = false;
            GetDataItem.Visible = false;

            panelView.BorderStyle = BorderStyle.FixedSingle;
            panelView.Visible = true;
            panelViewResize();

            labelServer = new Label
            {
                Text = "Server",
                BackColor = Color.PaleGreen,
                Location = new Point(20, 60),
                Size = new Size(590, 22),
                BorderStyle = BorderStyle.None,
                TextAlign = ContentAlignment.MiddleLeft,
                Parent = panelView
            };
            textBoxServer = new TextBox
            {
                Text = "",
                PasswordChar = '*',
                Location = new Point(90, 61),
                Size = new Size(90, 20),
                BorderStyle = BorderStyle.FixedSingle,
                Parent = panelView
            };
            toolTip1.SetToolTip(textBoxServer, "Имя сервера с базой T-factura в виде:\nNameOfServer.Domain.Subdomain");
            textBoxServer.Click += new System.EventHandler(ClearTextBox);

            labelServerUserName = new Label
            {
                Text = "UserName",
                BackColor = Color.PaleGreen,
                Location = new Point(220, 61),
                Size = new Size(70, 20),
                BorderStyle = BorderStyle.None,
                TextAlign = ContentAlignment.MiddleLeft,
                Parent = panelView
            };
            textBoxServerUserName = new TextBox
            {
                Text = "",
                PasswordChar = '*',
                Location = new Point(300, 61),
                Size = new Size(90, 20),
                BorderStyle = BorderStyle.FixedSingle,
                Parent = panelView
            };
            toolTip1.SetToolTip(textBoxServerUserName, "Имя пользователя SQL-сервера T-factura");
            textBoxServerUserName.Click += new System.EventHandler(ClearTextBox);

            labelServerUserPassword = new Label
            {
                Text = "Password",
                BackColor = Color.PaleGreen,
                Location = new Point(420, 61),
                Size = new Size(70, 20),
                BorderStyle = BorderStyle.None,
                TextAlign = ContentAlignment.MiddleLeft,
                Parent = panelView
            };
            textBoxServerUserPassword = new TextBox
            {
                Text = "",
                PasswordChar = '*',
                Location = new Point(500, 61),
                Size = new Size(90, 20),
                BorderStyle = BorderStyle.FixedSingle,
                Parent = panelView
            };
            toolTip1.SetToolTip(textBoxServerUserPassword, "Пароль администратора SQL-сервера T-factura");
            textBoxServerUserPassword.Click += new System.EventHandler(ClearTextBox);

            labelFolderPhotos = new Label
            {
                Text = "Photos",
                BackColor = Color.PaleGreen,
                Location = new Point(20, 90),
                Size = new Size(70, 20),
                BorderStyle = BorderStyle.None,
                TextAlign = ContentAlignment.MiddleLeft,
                Parent = panelView
            };
            textFolderPhotos = new TextBox
            {
                Text = sFolderPhotos,
                Location = new Point(90, 91),
                Size = new Size(90, 20),
                BorderStyle = BorderStyle.FixedSingle,
                Parent = panelView
            };
            textFolderPhotos.Click += new System.EventHandler(ClearTextBox);
            toolTip1.SetToolTip(textFolderPhotos, "Путь к папке с фотографиями.\nМожно указать относительным к папке с программой в виде:\n.\\Photos\\");

            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonSave.FlatAppearance.MouseOverBackColor = Color.PaleGreen; //Change color of button if mouse over the button
            buttonCancel.FlatAppearance.MouseOverBackColor = Color.PaleGreen;

            labelFolderPhotos.BringToFront();
            labelServerUserName.BringToFront();
            labelServerUserPassword.BringToFront();
            textBoxServer.BringToFront();
            textBoxServerUserName.BringToFront();
            textBoxServerUserPassword.BringToFront();
            textFolderPhotos.BringToFront();

            if (UserLogin?.Length > 0 && UserPassword?.Length > 0 && ServerName?.Length > 0)
            {
                textBoxServer.Text = ServerName;
                textBoxServerUserName.Text = UserLogin;
                textBoxServerUserPassword.Text = UserPassword;
            }
        }

        private void ClearTextBox(object sender, EventArgs e)
        { (sender as TextBox).Clear(); }

        private void buttonCancel_Click(object sender, EventArgs e) //Use Cancel()
        { Cancel(); }

        private void Cancel() //Cancel inputed Data
        {
            panelView.Visible = false;
            DisposeControlsOnPanelView();

            dataGridView1.Visible = true;
            logoPictureBox.Visible = true;
            comboBoxData.Visible = true;
            textBoxData.Visible = true;
            buttonShowAll.Visible = true;
            GetDataItem.Visible = true;
        }

        private void buttonSave_Click(object sender, EventArgs e) // Use Save()
        { Save(); }

        private void Save() //Save inputed Credintials and Parameters into Registry and variables
        {
            string server, user, password;
            server = textBoxServer?.Text;
            user = textBoxServerUserName?.Text;
            password = textBoxServerUserPassword?.Text;
            sFolderPhotos = textFolderPhotos?.Text;

            CheckSqlServer(server, user, password);

            if (UserLogin?.Length > 0 && UserPassword?.Length > 0 && ServerName?.Length > 0)
            {
                try
                {
                    using (Microsoft.Win32.RegistryKey EvUserKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(myRegKey))
                    {
                        EvUserKey.SetValue("ServerName", EncryptStringToBase64Text(ServerName, btsMess1, btsMess2), Microsoft.Win32.RegistryValueKind.String);
                        EvUserKey.SetValue("UserLogin", EncryptStringToBase64Text(UserLogin, btsMess1, btsMess2), Microsoft.Win32.RegistryValueKind.String);
                        EvUserKey.SetValue("UserPassword", EncryptStringToBase64Text(UserPassword, btsMess1, btsMess2), Microsoft.Win32.RegistryValueKind.String);
                        EvUserKey.SetValue("FolderPhotos", sFolderPhotos, Microsoft.Win32.RegistryValueKind.String);
                    }
                }
                catch { }
            }

            panelView.Visible = false;
            DisposeControlsOnPanelView();

            dataGridView1.Visible = true;
            logoPictureBox.Visible = true;
            comboBoxData.Visible = true;
            textBoxData.Visible = true;
            buttonShowAll.Visible = true;
            GetDataItem.Visible = true;
        }

        private void DisposeControlsOnPanelView()
        {
            var labelsToRemove = this.panelView.Controls.OfType<Label>().ToList();
            DisposeAllLabels(labelsToRemove);

            var textboxesToRemove = this.panelView.Controls.OfType<TextBox>().ToList();
            DisposeAllTextBoxes(textboxesToRemove);
        }

        private void DisposeAllLabels(List<Label> list)
        {
            foreach (Control control in list)
            {
                Controls?.Remove(control);
                control?.Dispose();
            }
        }

        private void DisposeAllTextBoxes(List<TextBox> list)
        {
            foreach (Control control in list)
            {
                Controls?.Remove(control);
                control?.Dispose();
            }
        }

        private void panelView_SizeChanged(object sender, EventArgs e)
        { panelViewResize(); }

        private void panelViewResize() //Change PanelView
        {
            panelView.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            panelView.Height = panelView.Parent.Height - 120;
            panelView.AutoScroll = true;
            panelView.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelView.ResumeLayout();
        }

        private void RefreshPictureBox(PictureBox picBox, Bitmap picImage) // не работает
        {
            picBox.Image = RefreshBitmap(picImage, panelView.Width - 2, panelView.Height - 2); //сжатая картина
            picBox.Refresh();
        }

        private Bitmap RefreshBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
            { g.DrawImage(b, 0, 0, nWidth, nHeight); }
            return result;
        }

        private void buttonShowAll_Click(object sender, EventArgs e) //Use ShowAll()
        { ShowAll(); }

        private void ShowAll() //Show all data from lFullData()
        {
            FindOwnersItem.Enabled = true;
            SelectListMenu.Enabled = true;
            buttonShowAll.Enabled = false;
            MakeTable();

            textBoxData.Clear();
            comboBoxData.SelectedText = null;

            StatusLabel3.Text = "Фильтр отключен";
            StatusLabel2.Text = "Всего записей - " + iRowRecords.ToString();
        }

        private void MakeTable()
        {
            iRowRecords = 0;
            if (sLastGotData == "GetDataWithModel")
            {
                ShowDataTableAtDatagrid(dtTarif);
            }
            else if (sLastGotData == "LoadFromServer")
            {
                ShowDataTableAtDatagrid(dtPeriod);
                dataGridView1.Columns[7].Visible = false;
            }
            else if (sLastGotData == "LoadFromFile")
            {
                ShowDataTableAtDatagrid(dtFull);
                DatagridCollumnsFullTableToHide(dataGridView1, getEmployeePhonesCollumns);
            }
        }

        private void DatagridCollumnsFullTableToHide(DataGridView dgv, int[] showFullTableCollumns)
        {
            dgv.Columns[8].Visible = showFullTableCollumns[0] > 0 ? true : false;
            dgv.Columns[7].Visible = showFullTableCollumns[10] > 0 ? true : false;
            dgv.Columns[6].Visible = showFullTableCollumns[9] > 0 ? true : false;
            dgv.Columns[2].Visible = showFullTableCollumns[8] > 0 ? true : false;
            dgv.Columns[5].Visible = showFullTableCollumns[5] > 0 ? true : false;
            dgv.Columns[4].Visible = showFullTableCollumns[2] > 0 ? true : false;
        }

        private static string EncryptStringToBase64Text(string plainText, byte[] Key, byte[] IV) //Encrypt variables PlainText Data. Use EncryptStringToBytes()
        {
            string sBase64Test;
            sBase64Test = Convert.ToBase64String(EncryptStringToBytes(plainText, Key, IV));
            return sBase64Test;
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV) //Encrypt variables PlainText Data
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            using (RijndaelManaged rijAlg = new RijndaelManaged())        // Create an RijndaelManaged object with the specified key and IV. 
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);    // Create a decrytor to perform the stream transform.

                using (System.IO.MemoryStream msEncrypt = new System.IO.MemoryStream())   // Create the streams used for encryption. 
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (System.IO.StreamWriter swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);   //Write all data to the stream.
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;    // Return the encrypted bytes from the memory stream. 
        }

        private static string DecryptBase64ToString(string sBase64Text, byte[] Key, byte[] IV) //Encrypt variables PlainText Data. Use DecryptStringFromBytes()
        {
            byte[] bBase64Test;
            bBase64Test = Convert.FromBase64String(sBase64Text);
            return DecryptStringFromBytes(bBase64Test, Key, IV);
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV) //Decrypt PlainText Data to variables
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;   // Declare the string used to hold the decrypted text.

            using (RijndaelManaged rijAlg = new RijndaelManaged())  // Create an RijndaelManaged object  with the specified key and IV.
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);  // Create a decrytor to perform the stream transform.                              

                using (System.IO.MemoryStream msDecrypt = new System.IO.MemoryStream(cipherText))  // Create the streams used for decryption. 
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (System.IO.StreamReader srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();  // Read the decrypted bytes from the decrypting stream and place them in a string. 
                        }
                    }
                }
            }
            return plaintext;
        }

        private void FormPhonesBook1_FormClosed(object sender, FormClosedEventArgs e)
        { ApplicationExit(); }


        private void LoadFromFileItem_Click(object sender, EventArgs e)
        { LoadFromFile(); }

        private async void LoadFromFile() //Load data from a text file
        {
            buttonShowAll.Enabled = false;
            ListTarifItem.Visible = false;
            ListModelItem.Visible = false;

            StatusLabel3.Text = "Загружаю данные с файла...";
            sLastGotData = "LoadFromFile";

            openFileDialog1.FileName = @"*.xls*";
            openFileDialog1.Filter = "Excel файлы (*.xls*)|*.xls*| Текстовые файлы (*.txt)|*.txt| CSV файлы(*.csv)|*.csv| All files (*.*)|*.*";
            openFileDialog1.ShowDialog();
            string sFileName = openFileDialog1.FileName;
            if (sFileName == null || sFileName.Trim().Length < 7)
            { MessageBox.Show("Файл не выбран"); }
            else
            {
                StatusLabel3.Text = "Импортирую данные из файла...";
                if (sFileName.ToLower().Contains("xls"))
                { await Task.Run(() => ImportExcelFile(sFileName)); }
                else
                { await Task.Run(() => ImportTextFile(sFileName)); }

                if (dtFull.Rows.Count > 0)
                {
                    FindOwnersItem.Enabled = true;
                    SelectListMenu.Enabled = true;
                    StatusLabel3.ForeColor = Color.Black;
                    StatusLabel3.Text = "Генерирую список ФИО...";

                    ShowAll();
                    if (sFileName.ToLower().Contains("xls"))
                    { await Task.Run(() => MakeListData("ФИО ответственного")); }
                    else
                    { await Task.Run(() => MakeListData("emp_name")); }

                    StatusLabel3.Text = "Файл " + sFileName + " загружен!";
                    StatusLabel2.Text = "Всего ФИО - " + (iRowFIO - 1).ToString() + " | Всего номеров - " + (iRowRecords - 1).ToString();
                }
                else
                {
                    StatusLabel3.Text = "Неверный формат полей или данные в файле отсутствуют!";
                    StatusLabel3.ForeColor = Color.DarkRed;
                }
            }
        }

        private void ImportTextFile(string sFileName)
        {
            dtFull.Rows.Clear();
            getEmployeePhonesCollumns = new int[]
            {
            0,//"Код карточки",     //0
            0,//"Ф.И.О.",
            0,//"Должность",        //2
            0,// "ТСП",
            0,//"Город",            //4
            0,//"Адрес",
            0,//"Рабочий телефон",  //6
            0,// "Домашний телефон",
            0,// "Телефон мобильный",//8
            0,// "Дата Приема",
            0// "Дата Увольнения"   //10
            };

            lFullData = new List<string>();
            string s;
            bool bFileCorrect = false;
            string[] sStrings = new string[1]; ;
            int iPhone_no = -1, iEmp_name = -1, iNav = -1, iOrg_unit_name = -1, iEmp_address = -1, iEmp_dismiss = -1; //Needed Cells
            string sPhone_no = "", sEmp_name = "", sNav = "", sOrg_unit_name = "", sEmp_address = "", sEmp_dismiss = ""; //Needed Cells

            iRowFIO = 1;
            iRowRecords = 1;

            if (sFileName == null) return;
            else
            {
                var Coder = Encoding.GetEncoding(1251);
                using (System.IO.StreamReader Reader = new System.IO.StreamReader(sFileName, Coder))
                {
                    while (!Reader.EndOfStream)
                    {
                        s = Reader.ReadLine();

                        if (s.ToLower().Contains("emp_name") || s.ToLower().Contains("фио"))
                        {
                            bFileCorrect = true;
                            sStrings = Regex.Split(s.ToLower(), "[|]"); //splitter of cell is "|"
                            for (int iCell = 0; iCell < sStrings.Length; iCell++) //Define infex of needed cells
                            {
                                if (sStrings[iCell].Contains("phone_no") || sStrings[iCell].Contains("Номер телефона"))
                                { iPhone_no = iCell; getEmployeePhonesCollumns[8] = iCell; }
                                else if (sStrings[iCell].Contains("emp_name") || sStrings[iCell].Contains("ФИО ответственного"))
                                { iEmp_name = iCell; getEmployeePhonesCollumns[1] = iCell; }
                                else if (sStrings[iCell].Contains("nav") || sStrings[iCell].Contains("NAV"))
                                { iNav = iCell; getEmployeePhonesCollumns[0] = iCell; }
                                else if (sStrings[iCell].Contains("org_unit_name") || sStrings[iCell].Contains("Подразделение"))
                                { iOrg_unit_name = iCell; getEmployeePhonesCollumns[3] = iCell; }
                                else if (sStrings[iCell].Contains("emp_address") || sStrings[iCell].Contains("Адрес"))
                                { iEmp_address = iCell; getEmployeePhonesCollumns[5] = iCell; }
                                else if (sStrings[iCell].Contains("emp_dismiss") || sStrings[iCell].Contains("Дата увольнения"))
                                { iEmp_dismiss = iCell; getEmployeePhonesCollumns[10] = iCell; }
                            }
                            if (iPhone_no > -1 || iEmp_name > -1 || iNav > -1)
                            { bFileCorrect = true; }
                            else
                            { bFileCorrect = false; }
                        }
                        else if (bFileCorrect)
                        {
                            sStrings = Regex.Split(s, "[|]"); //splitter of cell is "|"

                            if (iPhone_no > -1) { sPhone_no = MakeCommonViewPhone(sStrings[iPhone_no]); if (sPhone_no.Length > 4) { iRowRecords++; } } else { sPhone_no = ""; }
                            if (iEmp_name > -1) { sEmp_name = sStrings[iEmp_name].Trim(); } else { sEmp_name = ""; }
                            if (iNav > -1) { sNav = sStrings[iNav].Trim(); } else { sNav = ""; }
                            if (iOrg_unit_name > -1) { sOrg_unit_name = sStrings[iOrg_unit_name].Trim(); } else { sOrg_unit_name = ""; }
                            if (iEmp_address > -1) { sEmp_address = sStrings[iEmp_address].Trim(); } else { sEmp_address = ""; }
                            if (iEmp_dismiss > -1) { sEmp_dismiss = sStrings[iEmp_dismiss].Trim(); } else { sEmp_dismiss = ""; }

                            if (iEmp_name > -1)
                            {
                                DataRow row = dtFull.NewRow();
                                row["№ п/п"] = Convert.ToDouble(iRowFIO);
                                row["ФИО ответственного"] = sEmp_name;
                                row["Номер телефона"] = sPhone_no;
                                row["Подразделение"] = sOrg_unit_name;
                                row["Должность"] = "";
                                row["Адрес"] = sEmp_address;
                                row["Дата приема"] = "";
                                row["Дата увольнения"] = sEmp_dismiss;
                                row["NAV"] = sNav;
                                try { dtFull.Rows.Add(row); iRowFIO++; } catch { }
                            }
                        }
                    }
                }
            }
        }

        private void ImportExcelFile(string sFileName)
        {
            getEmployeePhonesCollumns = new int[]
             {
            0,//"Код карточки",     //0
            0,//"Ф.И.О.",
            0,//"Должность",        //2
            0,// "ТСП",
            0,//"Город",            //4
            0,//"Адрес",
            0,//"Рабочий телефон",  //6
            0,// "Домашний телефон",
            0,// "Телефон мобильный",//8
            0,// "Дата Приема",
            0// "Дата Увольнения"   //10
             };

            sLastGotData = "LoadFromFile";
            int offsetRowfileExcelWillImport = 0;
            string checkData = "", tempCell = "";
            bool fileExcelCorrect = false;
            int collumnsEmployeePhones = 0;
            int collumnsTAS = 0;
            int totalRows = 0, totalColumns = 0;

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(sFileName);
            object misValue = System.Reflection.Missing.Value;

            try
            {
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets[1]; //get ONly first WorkSheet from the Excel's file
                totalRows = xlWorkSheet.UsedRange.Rows.Count;
                totalColumns = 0;

                //Get names of collumns
                for (int findoffset = 1; findoffset <= 10; findoffset++)
                {
                    for (int xlWorkSheetCollumn = 1; xlWorkSheetCollumn <= xlWorkSheet.UsedRange.Columns.Count; xlWorkSheetCollumn++)  //get the first row with names of collumns and get totals collumns with data
                    {
                        try
                        {
                            checkData = xlWorkSheet.Cells[findoffset, xlWorkSheetCollumn].Value.ToString().Trim();
                            if (checkData.Length > 0)
                            {
                                for (int i = 0; i < getEmployeePhones.Length; i++)
                                {
                                    if (getEmployeePhones[i].ToLower().Equals(checkData.ToLower()))
                                    {
                                        getEmployeePhonesCollumns[i] = xlWorkSheetCollumn;
                                        collumnsEmployeePhones += 1;
                                        break;
                                    }
                                }

                                for (int i = 0; i < getTAS.Length; i++)
                                {
                                    if (getTAS[i].ToLower().Equals(checkData.ToLower()))
                                    {
                                        getEmployeePhonesCollumns[i] = xlWorkSheetCollumn;
                                        collumnsTAS += 1;
                                        break;
                                    }
                                }
                            }
                            checkData = "";
                        }
                        catch { }
                        offsetRowfileExcelWillImport = findoffset;
                    }
                    if (collumnsTAS > 1 || collumnsEmployeePhones > 1)
                    { fileExcelCorrect = true; break; }
                }
                //Get data and fill the table
                if (fileExcelCorrect)
                {
                    totalColumns = getEmployeePhonesCollumns.Max();

                    dtFull.Rows.Clear();
                    for (int findoffset = offsetRowfileExcelWillImport + 1; findoffset <= totalRows; findoffset++)
                    {
                        DataRow row = dtFull.NewRow();
                        try
                        {
                            iRowRecords++;
                            row["№ п/п"] = Convert.ToDouble(iRowRecords);
                            try { tempCell = xlWorkSheet.Cells[findoffset, getEmployeePhonesCollumns[1]].Value.ToString().Trim(); } catch { tempCell = ""; }
                            row["ФИО ответственного"] = tempCell;

                            try { tempCell = MakeCommonViewPhone(xlWorkSheet.Cells[findoffset, getEmployeePhonesCollumns[8]].Value.ToString().Trim()); } catch { tempCell = ""; }
                            row["Номер телефона"] = tempCell;
                            try { tempCell = xlWorkSheet.Cells[findoffset, getEmployeePhonesCollumns[3]].Value.ToString().Trim(); } catch { tempCell = ""; }
                            row["Подразделение"] = tempCell;
                            try { tempCell = xlWorkSheet.Cells[findoffset, getEmployeePhonesCollumns[2]].Value.ToString().Trim(); } catch { tempCell = ""; }
                            row["Должность"] = tempCell;
                            tempCell = "";
                            try { tempCell = xlWorkSheet.Cells[findoffset, getEmployeePhonesCollumns[4]].Value.ToString().Trim() + ", " + xlWorkSheet.Cells[findoffset, getEmployeePhonesCollumns[5]].Value.ToString().Trim(); } catch { tempCell = xlWorkSheet.Cells[findoffset, getEmployeePhonesCollumns[5]].Value.ToString().Trim(); }
                            row["Адрес"] = tempCell;
                            try { tempCell = xlWorkSheet.Cells[findoffset, getEmployeePhonesCollumns[9]].Value.ToString().Trim(); } catch { tempCell = ""; }
                            row["Дата приема"] = tempCell;
                            try { tempCell = xlWorkSheet.Cells[findoffset, getEmployeePhonesCollumns[10]].Value.ToString().Trim(); } catch { tempCell = ""; }
                            row["Дата увольнения"] = tempCell;
                            try { tempCell = xlWorkSheet.Cells[findoffset, getEmployeePhonesCollumns[0]].Value.ToString().Trim(); } catch { tempCell = ""; }
                            row["NAV"] = tempCell;
                        }
                        catch { }

                        if (row["ФИО ответственного"].ToString().Length > 1) dtFull.Rows.Add(row);
                    }
                }

                // releaseObject(xlWorkSheet);
            }
            catch { MessageBox.Show("Error\nMicrosoft.Office.Interop.Excel.Worksheet"); }

            xlWorkBook.Close(false, misValue, misValue);
            xlApp.Quit();
            // releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void releaseObject(object obj) //for close to excel
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                // GC.Collect();
            }
        }
        private string MakeCommonViewPhone(string sPrimaryPhone) //Normalize Phone to +380504197443
        {
            string sPhone = sPrimaryPhone.Trim();
            string sTemp1 = "", sTemp2 = "";
            sTemp1 = sPhone.Replace(" ", "");
            sTemp2 = sTemp1.Replace("-", "");
            sTemp1 = sTemp2.Replace(")", "");
            sTemp2 = sTemp1.Replace("(", "");
            sTemp1 = sTemp2.Replace("/", "");
            sTemp2 = sTemp1.Replace("_", "");

            if (sTemp2.StartsWith("+") && sTemp2.Length == 13) sPhone = sTemp2;
            else if (sTemp2.StartsWith("380") && sTemp2.Length == 12) sPhone = "+" + sTemp2;
            else if (sTemp2.StartsWith("80") && sTemp2.Length == 11) sPhone = "+3" + sTemp2;
            else if (sTemp2.StartsWith("0") && sTemp2.Length == 10) sPhone = "+38" + sTemp2;
            else if (sTemp2.Length == 9) sPhone = "+380" + sTemp2;
            else sPhone = sTemp2;

            sTemp1 = ""; sTemp2 = "";
            return sPhone;
        }

        private string DefineMainPhone(string sDescription)
        {
            if (sDescription.Trim() == "!")
            { return "Основной"; }
            else { return sDescription.Replace("\n", " "); }
        }

        private void StatusLabel2_TextChanged(object sender, EventArgs e)
        {
            if (StatusLabel2.Text.Length > 0)
            { SplitButton1.Visible = true; }
            else
            { SplitButton1.Visible = false; }
        }

        private void StatusLabel3_TextChanged(object sender, EventArgs e)
        {
            if (StatusLabel3.Text.Length > 0)
            { SplitButton2.Visible = true; }
            else
            { SplitButton2.Visible = false; }
        }

        private void textBoxData_KeyUp(object sender, KeyEventArgs e) //Action after pressed the button "Enter"
        {
            if (e.KeyCode == Keys.Enter)
            {
                string sSelected = textBoxData.Text.Trim();
                DataSearch(sSelected);
                StatusLabel3.Text = "Фильтр включен";
            }
            StatusLabel2.Text = "Найдено записей - " + iRowRecords;
        }

        private void comboBoxData_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSelected = comboBoxData.SelectedItem.ToString();
            DataSearch(sSelected);
        }

        private void DataSearch(string sSelected)
        {
            SearchSelectedData(sSelected);
            ShowData(sSelectedPhone, sSelectedFIO, sSelectedNAV, sSelected4, sSelected5, sSelected6);
            buttonShowAll.Enabled = true;
        }

        private void SearchSelectedData(string sSelected)
        {
            iRowRecords = 0;
            sSelectedPhone = "";
            sSelectedFIO = "";
            sSelectedNAV = "";
            sSelected4 = "";
            sSelected5 = "";
            sSelected6 = "";

            lString1?.Dispose();
            lString2?.Dispose();
            lString3?.Dispose();
            lString4?.Dispose();
            lString5?.Dispose();
            lString6?.Dispose();

            if (sLastGotData == "GetDataWithModel")
            { TableSearchToTableshow(dtTarif, dtTarifShow, sSelected); }
            else if (sLastGotData == "LoadFromServer")
            { TableSearchToTableshow(dtPeriod, dtPeriodShow, sSelected); }
            else if (sLastGotData == "LoadFromFile")
            { TableSearchToTableshow(dtFull, dtFullShow, sSelected); }
        }

        private void wc_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        { progressBar1.Value = e.ProgressPercentage; }

        private void ShowData(string s1 = "", string s2 = "", string s3 = "", string s4 = "", string s5 = "", string s6 = "") //Show the Loaded Picture in The PictureBox1
        {
            string photoPath = "";
            System.IO.FileInfo photoFileInfo = new System.IO.FileInfo("new.jpg");
            toolTip1.SetToolTip(logoPictureBox, "Разработчик: " + myFileVersionInfo.LegalCopyright);

            try
            {
                progressBar1.Value = 0;
                logoPictureBox.BorderStyle = BorderStyle.FixedSingle;
                photoPath = sFolderPhotos + sSelectedNAV + @".jpg";

                photoFileInfo = new System.IO.FileInfo(photoPath);
                if (photoFileInfo.Exists && photoFileInfo.Length < 100)
                { System.IO.File.Delete(photoPath); }

                logoPictureBox.Load(photoPath);
            }
            catch
            {
                string sTempNav = sSelectedNAV;
                if (sSelectedNAV.Contains("С")) //russkaya С                       
                {
                    sTempNav = sSelectedNAV.Replace("С", "S");
                    try
                    {
                        logoPictureBox.BorderStyle = BorderStyle.FixedSingle;
                        photoPath = sFolderPhotos + sTempNav + @".jpg";

                        photoFileInfo = new System.IO.FileInfo(photoPath);
                        if (photoFileInfo.Exists && photoFileInfo.Length < 100)
                        { System.IO.File.Delete(photoPath); }

                        logoPictureBox.Load(photoPath);
                    }
                    catch
                    {
                        try
                        {
                            sTempNav = sSelectedNAV.Replace("С", "C");//english С
                            logoPictureBox.BorderStyle = BorderStyle.FixedSingle;
                            photoPath = sFolderPhotos + sTempNav + @".jpg";

                            photoFileInfo = new System.IO.FileInfo(photoPath);
                            if (photoFileInfo.Exists && photoFileInfo.Length < 100)
                            { System.IO.File.Delete(photoPath); }

                            logoPictureBox.Load(photoPath);
                        }
                        catch
                        {
                            try
                            {
                                try { System.IO.Directory.CreateDirectory(sFolderPhotos); } catch { }
                                progressBar1.Value = 0;
                                using (System.Net.WebClient wc = new System.Net.WebClient())
                                {
                                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                                    string url = "http://www.ais/usersimage/Fotos/" + sSelectedNAV + @".jpg";
                                    string save_path = sFolderPhotos + sSelectedNAV + @".jpg";
                                    wc.DownloadFileAsync(new Uri(url), save_path);
                                    logoPictureBox.BorderStyle = BorderStyle.FixedSingle;
                                    photoPath = sFolderPhotos + sSelectedNAV + @".jpg";

                                    photoFileInfo = new System.IO.FileInfo(photoPath);
                                    if (photoFileInfo.Exists && photoFileInfo.Length < 100)
                                    { System.IO.File.Delete(photoPath); }

                                    logoPictureBox.Load(photoPath);
                                }
                            }
                            catch
                            {
                                bmp = new Bitmap(Properties.Resources.LogoRYIK, logoPictureBox.Width, logoPictureBox.Height);
                                logoPictureBox.BorderStyle = BorderStyle.None;
                                RefreshPictureBox(logoPictureBox, bmp);
                            }
                        }
                    }
                }
                else if (sSelectedNAV.Contains("C")) //english С                       
                {
                    sTempNav = sSelectedNAV.Replace("C", "S");
                    try
                    {
                        logoPictureBox.BorderStyle = BorderStyle.FixedSingle;
                        photoPath = sFolderPhotos + sTempNav + @".jpg";

                        photoFileInfo = new System.IO.FileInfo(photoPath);
                        if (photoFileInfo.Exists && photoFileInfo.Length < 100)
                        { System.IO.File.Delete(photoPath); }

                        logoPictureBox.Load(photoPath);
                    }
                    catch
                    {
                        try
                        {
                            try { System.IO.Directory.CreateDirectory(sFolderPhotos); } catch { }
                            progressBar1.Value = 0;
                            using (System.Net.WebClient wc = new System.Net.WebClient())
                            {
                                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                                string url = "http://www.ais/usersimage/Fotos/" + sSelectedNAV + @".jpg";
                                string save_path = sFolderPhotos + sSelectedNAV + @".jpg";
                                wc.DownloadFileAsync(new Uri(url), save_path);
                                logoPictureBox.BorderStyle = BorderStyle.FixedSingle;
                                photoPath = sFolderPhotos + sSelectedNAV + @".jpg";

                                photoFileInfo = new System.IO.FileInfo(photoPath);
                                if (photoFileInfo.Exists && photoFileInfo.Length < 100)
                                { System.IO.File.Delete(photoPath); }

                                logoPictureBox.Load(photoPath);
                            }
                        }
                        catch
                        {
                            bmp = new Bitmap(Properties.Resources.LogoRYIK, logoPictureBox.Width, logoPictureBox.Height);
                            logoPictureBox.BorderStyle = BorderStyle.None;
                            RefreshPictureBox(logoPictureBox, bmp);
                        }
                    }
                }
                else
                {
                    try
                    {
                        try { System.IO.Directory.CreateDirectory(sFolderPhotos); } catch { }
                        progressBar1.Value = 0;
                        using (System.Net.WebClient wc = new System.Net.WebClient())
                        {
                            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                            string url = "http://www.ais/usersimage/Fotos/" + sSelectedNAV + @".jpg";
                            string save_path = sFolderPhotos + sSelectedNAV + @".jpg";
                            wc.DownloadFileAsync(new Uri(url), save_path);
                            logoPictureBox.BorderStyle = BorderStyle.FixedSingle;
                            photoPath = sFolderPhotos + sSelectedNAV + @".jpg";

                            photoFileInfo = new System.IO.FileInfo(photoPath);
                            if (photoFileInfo.Exists && photoFileInfo.Length < 100)
                            { System.IO.File.Delete(photoPath); }

                            logoPictureBox.Load(photoPath);
                        }
                    }
                    catch
                    {
                        bmp = new Bitmap(Properties.Resources.LogoRYIK, logoPictureBox.Width, logoPictureBox.Height);
                        logoPictureBox.BorderStyle = BorderStyle.None;
                        RefreshPictureBox(logoPictureBox, bmp);
                    }
                }
            }
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            try
            {
                if (s1 != null && s1.Length > 1)
                {
                    lString1 = new Label
                    {
                        Text = s1,
                        BackColor = Color.PaleGreen,
                        Location = new Point(this.Width - 245, 290),
                        Size = new Size(220, 20),
                        BorderStyle = BorderStyle.None,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Anchor = (AnchorStyles.Right | AnchorStyles.Top),
                        Parent = this
                    };
                    toolTip1.SetToolTip(lString1, "Номер владельца");
                }

                if (s2 != null && s2.Length > 1)
                {
                    lString2 = new Label
                    {
                        Text = s2,
                        BackColor = Color.PaleGreen,
                        Location = new Point(this.Width - 245, 320),
                        Size = new Size(220, 20),
                        BorderStyle = BorderStyle.None,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Anchor = (AnchorStyles.Right | AnchorStyles.Top),
                        Parent = this
                    };
                    toolTip1.SetToolTip(lString2, "ФИО владельца");
                    toolTip1.SetToolTip(logoPictureBox, lString2.Text);
                }

                if (s3 != null && s3.Length > 1)
                {
                    lString3 = new Label
                    {
                        Text = s3,
                        BackColor = Color.PaleGreen,
                        Location = new Point(this.Width - 245, 350),
                        Size = new Size(220, 20),
                        BorderStyle = BorderStyle.None,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Anchor = (AnchorStyles.Right | AnchorStyles.Top),
                        Parent = this
                    };
                    toolTip1.SetToolTip(lString3, "Персональный код владельца");
                }

                if (s4 != null && s4.Length > 1)
                {
                    lString4 = new Label
                    {
                        Text = s4,
                        BackColor = Color.PaleGreen,
                        Location = new Point(this.Width - 245, 380),
                        Size = new Size(220, 20),
                        BorderStyle = BorderStyle.None,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Anchor = (AnchorStyles.Right | AnchorStyles.Top),
                        Parent = this
                    };
                    toolTip1.SetToolTip(lString4, "Организация, в которой работает владелец");
                }

                if (s5 != null && s5.Length > 1)
                {
                    lString5 = new Label
                    {
                        Text = s5,
                        BackColor = Color.PaleGreen,
                        Location = new Point(this.Width - 245, 410),
                        Size = new Size(220, 20),
                        BorderStyle = BorderStyle.None,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Anchor = (AnchorStyles.Right | AnchorStyles.Top),
                        Parent = this
                    };
                    toolTip1.SetToolTip(lString5, "Домашний адрес владельца");
                }

                if (s6 != null && s6.Length > 1)
                {
                    lString6 = new Label
                    {
                        Text = s6,
                        BackColor = Color.PaleGreen,
                        Location = new Point(this.Width - 245, 440),
                        Size = new Size(220, 20),
                        BorderStyle = BorderStyle.None,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Anchor = (AnchorStyles.Right | AnchorStyles.Top),
                        Parent = this
                    };
                    toolTip1.SetToolTip(lString6, "Дата увольнения");
                }
            }
            catch (Exception expt) { MessageBox.Show(expt.ToString()); }

            try
            {
                photoFileInfo = new System.IO.FileInfo(photoPath);
                if (photoFileInfo.Exists && photoFileInfo.Length < 100)
                { System.IO.File.Delete(photoPath); }
            }
            catch { }
            photoFileInfo = null;
        }

        private void dataGridView1_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DefineSelectedDataInDatagrid();
            ShowData(sSelectedPhone, sSelectedFIO, sSelectedNAV, sSelected4, sSelected5, sSelected6);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            FindOwners();
        }

        private void DefineSelectedDataInDatagrid() //Define Selected Data in Datagrid
        {
            sSelectedPhone = "";
            sSelectedFIO = "";
            sSelectedNAV = "";
            sSelected4 = "";
            sSelected5 = "";
            sSelected6 = "";

            lString1?.Dispose();
            lString2?.Dispose();
            lString3?.Dispose();
            lString4?.Dispose();
            lString5?.Dispose();
            lString6?.Dispose();
            if (dataGridView1.ColumnCount > 0)
            {
                string NameCollum = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText.ToString(); //имя колонки выбранной ячейки

                int iIndexColumn1 = -1;   //dataGridView1.ColumnCount - всего колонок в датагрид отображается в данный момент     
                int iIndexColumn2 = -1;
                int iIndexColumn3 = -1;
                int iIndexColumn4 = -1;
                int iIndexColumn5 = -1;
                int iIndexColumn6 = -1;

                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (dataGridView1.Columns[i].HeaderText.ToString() == "Номер телефона")
                    { iIndexColumn1 = i; }
                    if (dataGridView1.Columns[i].HeaderText.ToString() == "ФИО ответственного")
                    { iIndexColumn2 = i; }
                    if (dataGridView1.Columns[i].HeaderText.ToString() == "NAV")
                    { iIndexColumn3 = i; }
                    if (dataGridView1.Columns[i].HeaderText.ToString() == "Подразделение")
                    { iIndexColumn4 = i; }
                    if (dataGridView1.Columns[i].HeaderText.ToString() == "Адрес")
                    { iIndexColumn5 = i; }
                    if (dataGridView1.Columns[i].HeaderText.ToString() == "Дата увольнения")
                    { iIndexColumn6 = i; }
                }

                int selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
                int IndexCurrentRow = dataGridView1.CurrentRow.Index;
                sSelectedPhone = dataGridView1.Rows[IndexCurrentRow].Cells[iIndexColumn1].Value.ToString();
                sSelectedFIO = dataGridView1.Rows[IndexCurrentRow].Cells[iIndexColumn2].Value.ToString();
                sSelectedNAV = dataGridView1.Rows[IndexCurrentRow].Cells[iIndexColumn3].Value.ToString();
                if (iIndexColumn4 > -1) sSelected4 = dataGridView1.Rows[IndexCurrentRow].Cells[iIndexColumn4].Value.ToString();
                if (iIndexColumn5 > -1) sSelected5 = dataGridView1.Rows[IndexCurrentRow].Cells[iIndexColumn5].Value.ToString();
                if (iIndexColumn6 > -1) sSelected6 = dataGridView1.Rows[IndexCurrentRow].Cells[iIndexColumn6].Value.ToString();
            }
        }

        private void FolderItem_Click(object sender, EventArgs e)
        { System.Diagnostics.Process.Start("explorer", Environment.CurrentDirectory); }


        private void TemplateItem_Click(object sender, EventArgs e)
        { MakeTemplate(); }

        private void MakeTemplate() //Make file with tamplate of text for import
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@";Формат заголовков столбцов и тип разделителей для корректного импорта списка в телефонную книгу");
            sb.AppendLine(@";При отсутствии заголовков столбцов данные НЕ БУДУТ проимпортированы в программу!");
            sb.AppendLine(@";минимальный набор столбцов должен содержать phone_no, emp_name, NAV");
            sb.AppendLine(@";номер телефона|ФИО|Уникальный персональный код|подразделение|адрес|уволен|");
            sb.AppendLine(@"");
            sb.AppendLine(@"phone_no|emp_name|NAV|org_unit_name|emp_address|emp_dismiss|");
            System.IO.FileInfo fFileInfo = new System.IO.FileInfo(Environment.CurrentDirectory + "\\MobilePhones.txt");
            var Coder = Encoding.GetEncoding(1251);
            if (fFileInfo.Exists)
            { System.IO.File.WriteAllText(fFileInfo.FullName + ".txt", sb.ToString(), Coder); }
            else
            { System.IO.File.WriteAllText(fFileInfo.FullName, sb.ToString(), Coder); }
            System.Diagnostics.Process.Start("explorer", Environment.CurrentDirectory);
        }

        private void textBoxData_Click(object sender, EventArgs e)
        { textBoxData.Clear(); }

        private void FindOwnersItem_Click(object sender, EventArgs e)
        { FindOwners(); }

        private async void FindOwners()
        {
            //Check alive server
            ToolStripStatusLabelBackColor(StatusLabel3, SystemColors.Control);
            serverDied = false;
            StatusLabel3.Text = "Проверяю доступность " + ServerName;
            StatusLabel3.BackColor = SystemColors.Control;
            LoadFromServerItem.Enabled = false;
            SettingsMenu.Enabled = false;
            await System.Threading.Tasks.Task.Run(() =>
            CheckSqlServer(ServerName, UserLogin, UserPassword));

            if (!serverDied)
            {

                string sSelected = "";
                if (comboBoxData.SelectedIndex > -1)
                { try { sSelected = comboBoxData.SelectedItem.ToString(); } catch { sSelected = textBoxData.Text.Trim(); } }
                else
                { sSelected = textBoxData.Text.Trim(); }

                double iNum = 0;
                bool isNum = double.TryParse(sSelected, out iNum);
                if (isNum && iNum > 0)
                { GetPeriodData(sSelected); }
                else
                {
                    DefineSelectedDataInDatagrid();
                    iNum = 0;
                    isNum = double.TryParse(sSelectedPhone.Replace("+", ""), out iNum);
                    if (isNum && iNum > 0)
                    { GetPeriodData(sSelectedPhone); }
                    else
                    { MessageBox.Show("Чтобы что-то найти, Вам нужно\nили выбрать номер из списка\nили ввести номер или его часть вручную в поле для ввода данных"); }
                }
                buttonShowAll.Enabled = true;
            }
            LoadFromServerItem.Enabled = true;
            SettingsMenu.Enabled = true;
        }

        private void GetPeriodData(string sWhereQuery) //Get All Owners of the selected number
        {
            SelectListMenu.Enabled = false;
            FindOwnersItem.Enabled = false;
            iRowRecords = 0;
            dtPeriodShow.Rows.Clear();

            string sTemp1 = sWhereQuery, sSqlQuery;

            //Transform all numbers to 501112233
            if (sTemp1.StartsWith("+380") && sTemp1.Length == 13) sWhereQuery = sTemp1.Replace("+380", "");
            else if (sTemp1.StartsWith("380") && sTemp1.Length == 12) sWhereQuery = sTemp1.Replace("380", "");
            else if (sTemp1.StartsWith("80") && sTemp1.Length == 11) sWhereQuery = sTemp1.Replace("80", "");
            else if (sTemp1.StartsWith("0") && sTemp1.Length == 10) sWhereQuery = sTemp1.Replace("0", "");
            else if (sTemp1.Length == 9) sWhereQuery = sTemp1;
            else sWhereQuery = sTemp1;

            using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(strConnection))
            {
                sqlConnection.Open();
                sSqlQuery = "SELECT v_rs_contract_detail.phone_no AS phone_no, v_rs_contract_detail.emp_name AS emp_name, v_rs_contract_detail.org_unit_name AS org_unit_name, v_rs_contract_detail.from_dt AS from_dt, v_rs_contract_detail.till_dt AS till_dt, " +
                    " os_emp.emp_cd AS NAV " +
                    " FROM v_rs_contract_detail INNER JOIN os_emp ON(v_rs_contract_detail.emp_id = os_emp.emp_id) " +
                    " Where emp_name is not null AND phone_no LIKE '%" + sWhereQuery + "' ORDER by phone_no, from_dt";

                using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(sSqlQuery, sqlConnection))
                {
                    using (System.Data.SqlClient.SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        foreach (System.Data.Common.DbDataRecord record in sqlReader)
                        {
                            if (record != null && record.ToString().Length > 0 && record["phone_no"].ToString().Length > 0)
                            {
                                DataRow row = dtPeriodShow.NewRow();
                                row["№ п/п"] = ++iRowRecords;
                                row["Номер телефона"] = MakeCommonViewPhone(record["phone_no"].ToString());
                                row["Тариф действует с"] = MakeDateCorrectFormat(record["from_dt"].ToString().Trim().Split(' ')[0]);
                                row["Тариф действовал по"] = MakeDateCorrectFormat(record["till_dt"].ToString().Trim().Split(' ')[0]);
                                row["ФИО ответственного"] = record["emp_name"].ToString().Trim();
                                row["NAV"] = record["NAV"].ToString().Trim();
                                row["Подразделение"] = record["org_unit_name"].ToString().Trim();
                                dtPeriodShow.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            ShowDataTableAtDatagrid(dtPeriodShow);
        }

        private string MakeDateCorrectFormat(string dateOtherFormat) //Change format from "dd.MM.yyyy" to  "yyyy.MM.dd"
        {
            string datestr = dateOtherFormat;
            string formatsrc = "dd.MM.yyyy";
            string formatdst = "yyyy.MM.dd";
            DateTime dtCorrectFormat = DateTime.Now;
            string result = "";
            try
            {
                dtCorrectFormat = DateTime.ParseExact(datestr, formatsrc, System.Globalization.CultureInfo.InvariantCulture);
                result = dtCorrectFormat.ToString(formatdst);
            }
            catch { result = dateOtherFormat; }
            return result;
        }

        private void FindhDataWithModelItem_Click(object sender, EventArgs e)
        { FindhDataWithModel(); }

        private async void FindhDataWithModel()
        {
            //Check alive server
            ToolStripStatusLabelBackColor(StatusLabel3, SystemColors.Control);
            serverDied = false;
            StatusLabel3.Text = "Проверяю доступность " + ServerName;
            StatusLabel3.BackColor = SystemColors.Control;
            LoadFromServerItem.Enabled = false;
            SettingsMenu.Enabled = false;

            await System.Threading.Tasks.Task.Run(() =>
            CheckSqlServer(ServerName, UserLogin, UserPassword));

            if (!serverDied)
            {
                sLastGotData = "GetDataWithModel";
                buttonShowAll.Enabled = false;
                StatusLabel2.Text = "Запрашиваю данные с сервера...";
                StatusLabel3.ForeColor = Color.DarkRed;
                StatusLabel3.Text = "Ждите...";
                await Task.Run(() => GetDataWithModel());
                ShowDataTableAtDatagrid(dtTarif);

                MakeListData("Номер телефона");

                FindOwnersItem.Enabled = false;
                SelectListMenu.Enabled = false;
                StatusLabel2.Text = "Всего номеров - " + dtTarif.Rows.Count.ToString();
                StatusLabel3.ForeColor = Color.Black;
                StatusLabel3.Text = "Готово!";

                FindOwnersItem.Enabled = true;
                SelectListMenu.Enabled = true;
                ListTarifItem.Visible = true;
                ListModelItem.Visible = true;
            }
            LoadFromServerItem.Enabled = true;
            SettingsMenu.Enabled = true;
        }

        private void GetDataWithModel() //Get Info with NAV-cod and Model Payment
        {
            List<string> lTarif = new List<string>();
            List<string> lTemp = new List<string>();

            string sTempRaw, sTarif = "", sSqlQuery;
            try
            {
                using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(strConnection))
                {
                    sqlConnection.Open();

                    // Take last contract_bill_id for every contract_id
                    sSqlQuery = "SELECT t.contract_id AS contractmain, max(t.contract_bill_id) AS contractlast FROM v_dp_contract_bill_detail_ex t group by t.contract_id ORDER by t.contract_id desc;";
                    using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(sSqlQuery, sqlConnection))
                    {
                        using (System.Data.SqlClient.SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                        {
                            foreach (System.Data.Common.DbDataRecord record in sqlReader)
                            {
                                if (record != null && record.ToString().Length > 0)
                                {
                                    sTempRaw = record["contractmain"].ToString().Replace("|", "_") + "|";
                                    sTempRaw += record["contractlast"].ToString().Replace("|", "_") + "|";
                                    lTemp.Add(sTempRaw);
                                }
                            }
                        }
                    }
                    sTempRaw = null;

                    foreach (string sTemp in lTemp.ToArray()) // for every selected contract_bill_id Takes Tarif
                    {
                        sCellRow = Regex.Split(sTemp, "[|]");

                        sSqlQuery = "SELECT tariff_package_name AS tarif FROM v_dp_contract_bill_detail_ex where contract_bill_id LIKE '" + sCellRow[1] + "';";
                        using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(sSqlQuery, sqlConnection))
                        {
                            using (System.Data.SqlClient.SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                            {
                                foreach (System.Data.Common.DbDataRecord record in sqlReader)
                                {
                                    if (record != null && record.ToString().Length > 0)
                                    {
                                        sTarif = record["tarif"].ToString().Replace("|", "_") + "|";
                                    }
                                }
                            }
                        }
                        lTarif.Add(sCellRow[0] + "|" + sTarif + "|");
                    }

                    sSqlQuery = "SELECT t1.*, t1.descr  AS main, " +
                                " t2.emp_cd AS NAV, t2.emp_id AS t2emp_id, " +
                                " t3.contract_id as t3contract_id, t3.pay_model_id, t3.till_dt as t3till_dt, " +
                                " t4.name AS model_name " +
                                //  ", t5.tariff_package_name AS tariff, t5.end_dt AS last_data " +
                                " FROM v_rs_contract_detail t1 " +
                                " INNER JOIN os_emp t2 ON t1.emp_id = t2.emp_id " +
                                " LEFT JOIN (SELECT * FROM os_contract_link WHERE till_dt IS NULL)  t3 ON t1.contract_id = t3.contract_id " +
                                " LEFT JOIN rs_pay_model t4 ON t3.pay_model_id = t4.pay_model_id " +
                                // " right JOIN (SELECT contract_id,tariff_package_name,end_dt, contract_bill_id FROM v_dp_contract_bill_detail_ex) t5 ON t1.contract_id = t5.contract_id " +
                                " WHERE t1.emp_id IS NOT NULL AND(" +
                                " (t1.till_dt IS null" +
                                " OR" +
                                " t1.till_dt > '" + dayStartSearch + "')" +   //search info only in current period
                                " AND t3.till_dt IS NULL " +
                                ") " +
                                " ORDER by t1.emp_name, t1.phone_no " +
                                ";";

                    dtTarif.Rows.Clear();
                    DataRow row = dtTarif.NewRow();
                    iRowRecords = 0;
                    using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(sSqlQuery, sqlConnection))
                    {
                        using (System.Data.SqlClient.SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                        {
                            foreach (System.Data.Common.DbDataRecord record in sqlReader)
                            {
                                if (record != null && record.ToString().Length > 0 && record["phone_no"].ToString().Length > 0)
                                {
                                    row = dtTarif.NewRow();
                                    sTarif = "";
                                    foreach (string sTemp in lTarif.ToArray()) // for every selected contract_bill_id Takes Tarif
                                    {
                                        sCellRow = Regex.Split(sTemp, "[|]");
                                        if (sCellRow[0] == record["contract_id"].ToString().Trim().Replace("|", "_"))
                                        { sTarif = sCellRow[1]; break; }
                                    }
                                    row["№ п/п"] = ++iRowRecords;
                                    row["Номер телефона"] = MakeCommonViewPhone(record["phone_no"].ToString());
                                    row["ФИО ответственного"] = record["emp_name"].ToString().Trim();
                                    row["NAV"] = record["NAV"].ToString().Trim();
                                    row["Подразделение"] = record["org_unit_name"].ToString().Trim();
                                    row["Используется как"] = DefineMainPhone(record["main"].ToString());
                                    row["Тариф действует с"] = MakeDateCorrectFormat(record["from_dt"].ToString().Trim().Split(' ')[0]);
                                    row["Модель компенсации"] = record["model_name"].ToString().Trim();
                                    row["Тарифный пакет"] = sTarif;
                                    try { dtTarif.Rows.Add(row); } catch { }
                                }
                            }
                        }
                    }
                    row = null;
                }
            }
            catch (Exception expt) { MessageBox.Show(expt.ToString()); }

            //clear var
            lTarif.Clear(); lTemp.Clear();
        }

        private void TableSearchToTableshow(DataTable dt, DataTable dtShow, string searchData)
        {
            dtShow.Rows.Clear();
            foreach (DataColumn column in dt.Columns)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[column].ToString().ToLower().Contains(searchData.ToLower()))
                    {
                        try
                        { dtShow.ImportRow(row); }
                        catch { }
                    }
                }
            }
            iRowRecords = dtShow.Rows.Count;
            ShowDataTableAtDatagrid(dtShow);
        }

        private void ShowDataTableAtDatagrid(DataTable dt) //Access into Datagrid from other threads
        {
            try
            {
                if (this.InvokeRequired)
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoResizeColumns();
                    }));
                else
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoResizeColumns();
                }
            }
            catch { }
        }

        private void TableSearchToLdata(DataTable dt, HashSet<string> hsData, string searchData)
        {
            iRowFIO = 0;
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ColumnName.Equals(searchData))
                    {
                        hsData.Add(row[column].ToString());
                        iRowFIO++;
                    }
                }
            }
        }

    }
}
