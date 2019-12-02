using System;
using System.Windows.Forms;
using System.Linq;


namespace PhonesBook
{
    partial class AboutBox : Form
    {
        private bool boolButtonOk = false;

        public AboutBox()
        {
            InitializeComponent();

            System.Diagnostics.FileVersionInfo myFileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

            /*            foreach (Label label in tableLayoutPanel.Controls.OfType<Label>())
                        { label.Font = new System.Drawing.Font("Courier New", 8, System.Drawing.FontStyle.Regular); }

                        this.Text = String.Format("{0,-14}{1}", "О программе:", myFileVersionInfo.ProductName);
                        this.labelProductName.Text = String.Format("{0,-14}{1}", "Название ПО:", myFileVersionInfo.Comments);
                        this.labelVersion.Text = String.Format("{0,-14}{1}", "Версия:", myFileVersionInfo.FileVersion);
                        this.labelCopyright.Text = String.Format("{0,-14}{1}", "Разработчик:", myFileVersionInfo.LegalCopyright);
                        this.labelPath.Text = String.Format("{0,-14}{1}", "Полный путь:", Application.ExecutablePath);
                        this.labelFile.Text = String.Format("{0,-14}{1}", "Имя файла:", myFileVersionInfo.OriginalFilename);
                        this.labelPC.Text = String.Format("{0,-14}{1}", "Имя ПК:", Environment.MachineName);
                        this.labelOS.Text = String.Format("{0,-14}{1}", "Верися ОС:", Environment.OSVersion.ToString());
                        this.labelOcupiedRAM.Text = String.Format("{0,-14}{1}", "Память, MB:", (Environment.WorkingSet / 1024 / 1024).ToString());*/

            this.Text = "О программе:";
            labelProductName.Text = "Название ПО:";
            labelVersion.Text = "Версия:";
            labelCopyright.Text =  "Разработчик:";
            labelPath.Text = "Полный путь:";
            labelFile.Text = "Имя файла:";
            labelPC.Text = "Имя ПК:";
            labelOS.Text = "Верися ОС:";
            labelOcupiedRAM.Text = "Память, MB:";

            labelProductNameData.Text = myFileVersionInfo.Comments;
            labelVersionData.Text =  myFileVersionInfo.FileVersion;
            labelCopyrightData.Text = myFileVersionInfo.LegalCopyright;
            labelPathData.Text =  Application.ExecutablePath;
            labelFileData.Text =  myFileVersionInfo.OriginalFilename;
            labelPCData.Text = Environment.MachineName;
            labelOSData.Text =  Environment.OSVersion.ToString();
            labelOcupiedRAMData.Text = (Environment.WorkingSet / 1024 / 1024).ToString();

            toolTip1.SetToolTip(labelProductName, myFileVersionInfo.Comments);
            toolTip1.SetToolTip(labelVersion, myFileVersionInfo.FileVersion);
            toolTip1.SetToolTip(labelCopyright, myFileVersionInfo.LegalCopyright);
            toolTip1.SetToolTip(labelPath, Application.ExecutablePath);
            toolTip1.SetToolTip(labelFile, myFileVersionInfo.OriginalFilename);
            toolTip1.SetToolTip(labelPC, Environment.MachineName);
            toolTip1.SetToolTip(labelOS, Environment.OSVersion.ToString());
            //toolTip1.SetToolTip(labelOcupiedRAM, "Занятый приложением объем памяти в ОЗУ");
              toolTip1.SetToolTip(labelOcupiedRAM, (Environment.WorkingSet / 1024 / 1024).ToString());

            toolTip1.SetToolTip(labelProductNameData, labelProductName.Text);
            toolTip1.SetToolTip(labelVersionData, labelVersion.Text);
            toolTip1.SetToolTip(labelCopyrightData, labelCopyright.Text);
            toolTip1.SetToolTip(labelPathData, labelPath.Text);
            toolTip1.SetToolTip(labelFileData, labelFile.Text);
            toolTip1.SetToolTip(labelPCData, labelPC.Text);
            toolTip1.SetToolTip(labelOSData, labelOS.Text);
            toolTip1.SetToolTip(labelOcupiedRAMData, "Занятый приложением объем памяти в ОЗУ");
            toolTip1.SetToolTip(logoPictureBox, "Разработчик: " + myFileVersionInfo.LegalCopyright);
        }

        public bool OKButtonClicked
        {
            get { return boolButtonOk; }
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            boolButtonOk = true;
            this.Close();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            boolButtonOk = false;
            this.Close();
        }

    }
}
