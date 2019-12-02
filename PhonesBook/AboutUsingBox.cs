using System;
using System.Windows.Forms;


namespace PhonesBook
{
    partial class AboutUsingBox : Form
    {
        private bool boolButtonOk = false;

        public AboutUsingBox()
        {
            InitializeComponent();

            System.Diagnostics.FileVersionInfo myFileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

            this.Text = String.Format("{0,-14}{1}", "О программе:", myFileVersionInfo.ProductName);
            this.labelProductName.Text = "Название ПО:";
            this.labelCopyright.Text = "Разработчик:";
            this.labelProductNameData.Text = myFileVersionInfo.Comments;
            this.labelCopyrightData.Text = myFileVersionInfo.LegalCopyright;

            this.textBox1.Text="1.Первый раз, перед получением данных с сервера T - Factura, необходимо:" + Environment.NewLine + @"    A) Ввести адрес сервера в виде - SERVER.DOMAIN.SUBDOMAIN" + Environment.NewLine +
                    @"    B) Ввести авторизационные данные" + Environment.NewLine+
                    @"    С) Нажать кнопку " + "\"Сохранить\"." + Environment.NewLine +
                    "2. Корректный адрес сервера, имя и пароль пользователя T-Factura, можно получить в ИТ - отделе." + Environment.NewLine +
                    "3. Программа умеет работать с локальными текстовыми списками." + Environment.NewLine +
                    "Для корректного импорта списка с информацией о персонале в программу используйте" +
                    " текстовый файл с шаблонным набором полей \"MobilePhones.txt\". Данный файл можно сгенерировать из меню программы. Он хранится в папке, вместе с программой.";
            toolTip1.SetToolTip(textBox1, "Информация об использовании программы");
            toolTip1.SetToolTip(labelProductName, myFileVersionInfo.Comments);
            toolTip1.SetToolTip(labelCopyright, myFileVersionInfo.LegalCopyright);
            toolTip1.SetToolTip(labelProductNameData, labelProductName.Text);
            toolTip1.SetToolTip(labelCopyrightData, labelCopyright.Text);
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
