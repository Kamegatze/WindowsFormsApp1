using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities.Encoders;

namespace WindowsFormsApp1
{
    public partial class UsersForm : Form
    {
        List<UserInPolicy> listofUser = new List<UserInPolicy>();//хранит пользователей из xml файла
        private bool check = false;// проверка пользователей загруженный ли они
        public enum right
        {
            None,
            Guest,
            User,
            Moderator,
            Administrator
        }
        public struct accessRights
        {
            public right fileServer;
            public right databaseServer;
            public right documentServer;
            public right webServer;
        }
        public struct user
        {
            public string name;
            public accessRights rights;
            public string password;
        }
        public int usersCount;
        public user[] listofUsers;
        Random r = new Random();

        public UsersForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MaskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;
            usersCount = Convert.ToInt32(textBox1.Text);
            listBox2.Items.Clear();
            listofUsers = new user[usersCount + 1];
            for (int i = 0; i < usersCount; i++)
            {
                listofUsers[i].name = "Пользователей №" + (i + 1).ToString();
                listofUsers[i].rights.fileServer = (right)r.Next(0, 5);
                listofUsers[i].rights.databaseServer = (right)r.Next(0, 5);
                listofUsers[i].rights.documentServer = (right)r.Next(0, 5);
                listofUsers[i].rights.webServer = (right)r.Next(0, 5);
                listofUsers[i].password = "добавьте пароль"; 
            }
            listofUsers[usersCount].name = "Администратор";
            listofUsers[usersCount].rights.fileServer = right.Administrator;
            listofUsers[usersCount].rights.databaseServer = right.Administrator;
            listofUsers[usersCount].rights.documentServer = right.Administrator;
            listofUsers[usersCount].rights.webServer = right.Administrator;
            listofUsers[usersCount].password = "admin";

            for (int i = 0; i < usersCount + 1; i++)
            {
                listBox2.Items.Add(listofUsers[i].name);
            }


        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (listBox2.SelectedIndex == -1)
                return;
            // проверяет згруженный ли файл
            if(check == false)
            {
                dataGridView1.Rows.Add("Файловый сервер", listofUsers[listBox2.SelectedIndex].rights.fileServer.ToString());
                dataGridView1.Rows.Add("Сервер баз данных", listofUsers[listBox2.SelectedIndex].rights.databaseServer.ToString());
                dataGridView1.Rows.Add("Сервер документов", listofUsers[listBox2.SelectedIndex].rights.documentServer.ToString());
                dataGridView1.Rows.Add("Web - сервер", listofUsers[listBox2.SelectedIndex].rights.webServer.ToString());
                dataGridView1.Rows.Add("Пороль", listofUsers[listBox2.SelectedIndex].password.ToString());
                
                var sha256 = SHA256.Create();
                var hashMicrosoftSha256 = sha256.ComputeHash(Encoding.UTF8.GetBytes(listofUsers[listBox2.SelectedIndex].password));

                var md5 = MD5.Create();
                var hashMicrosoftMD5 = md5.ComputeHash(Encoding.UTF8.GetBytes(listofUsers[listBox2.SelectedIndex].password));

                dataGridView1.Rows.Add("Sha256", Hex.ToHexString(hashMicrosoftSha256));
                dataGridView1.Rows.Add("MD5", Hex.ToHexString(hashMicrosoftMD5));
            }
            else
            {
                dataGridView1.Rows.Add("Файловый сервер", (right)listofUser[listBox2.SelectedIndex].RightFileServer);
                dataGridView1.Rows.Add("Сервер баз данных", (right)listofUser[listBox2.SelectedIndex].RightDatabaseServer);
                dataGridView1.Rows.Add("Сервер документов", (right)listofUser[listBox2.SelectedIndex].RightDocumentServer);
                dataGridView1.Rows.Add("Web - сервер", (right)listofUser[listBox2.SelectedIndex].RightWebServer);
                dataGridView1.Rows.Add("Password", listofUser[listBox2.SelectedIndex].Password);
                dataGridView1.Rows.Add("Sha256", listofUser[listBox2.SelectedIndex].PasswordSha256);
                dataGridView1.Rows.Add("MD5", listofUser[listBox2.SelectedIndex].PasswordMD5);
            }
            // Проверяем админ ли?
            if (listBox2.SelectedIndex == usersCount)
            {
                listBox1.Items.Clear();
                for(int i = 0; i < usersCount; i++)
                {
                    listBox1.Items.Add(listofUsers[i].name);
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdministorFormsInput administorForms = new AdministorFormsInput();
            administorForms.Show(); 
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();// добавление в listbox3 сервисов
            listBox3.Items.Add("Фаловый сервер");
            listBox3.Items.Add("Сервер баз данных");
            listBox3.Items.Add("Сервер документов");
            listBox3.Items.Add("Web - сервер");
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            for(int j = 0; j < usersCount; j++) // индекс пользователя
            {
                for (int i = 0; i < 5; i++) // индекс уровня доступа
                {
                    if (listBox3.Items.IndexOf("Фаловый сервер") == listBox3.SelectedIndex && listBox1.Items.IndexOf(listofUsers[j].name) == listBox1.SelectedIndex)
                    {
                        if (listBox4.SelectedIndex == i)
                        {
                            listofUsers[j].rights.fileServer = (right)i;
                        }
                    }
                    if (listBox3.Items.IndexOf("Сервер баз данных") == listBox3.SelectedIndex && listBox1.Items.IndexOf(listofUsers[j].name) == listBox1.SelectedIndex)
                    {
                        if (listBox4.SelectedIndex == i)
                        {
                            listofUsers[j].rights.databaseServer = (right)i;
                        }
                    }
                    if (listBox3.Items.IndexOf("Сервер документов") == listBox3.SelectedIndex && listBox1.Items.IndexOf(listofUsers[j].name) == listBox1.SelectedIndex)
                    {
                        if (listBox4.SelectedIndex == i)
                        {
                            listofUsers[j].rights.documentServer = (right)i;
                        }
                    }
                    if (listBox3.Items.IndexOf("Web - сервер") == listBox3.SelectedIndex && listBox1.Items.IndexOf(listofUsers[j].name) == listBox1.SelectedIndex)
                    {
                        if (listBox4.SelectedIndex == i)
                        {
                            listofUsers[j].rights.webServer = (right)i;
                        }
                    }
                }
           }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox4.Items.Clear();// добавление в listbox4 уровни доступа
            for (int i = 0; i < 5; i++)
            {
                listBox4.Items.Add((right)i);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void bSaveToFile_Click(object sender, EventArgs e)
        {
            XmlDocument xDoc = new XmlDocument(); // создание новый xml document
            var xmlDeclaration = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);// создание xml заголовка
            // Добавление заголовка перед корневым элементом
            xDoc.AppendChild(xmlDeclaration);
            XmlElement xRoot = xDoc.CreateElement("securitypolicy");
            //цикл по записям
            for(int i = 0; i < usersCount; i++)
            {
                // создание нового элемента, характеризующий пользователя
                XmlElement userElem = xDoc.CreateElement("user");
                // создадим атрибут name
                XmlAttribute nameAttr  = xDoc.CreateAttribute("name");
                // создадим элементы для хранения прав доступа
                XmlElement fileServerElem = xDoc.CreateElement("fileServer");
                XmlElement databaseServerElem = xDoc.CreateElement("databaseServer");
                XmlElement documentServerElem = xDoc.CreateElement("documentServer");
                XmlElement webServerElem = xDoc.CreateElement("webServer");
                XmlElement HexPasswordElem = xDoc.CreateElement("hexPassword");
                XmlElement HexSha256Elem = xDoc.CreateElement("hexSha256");
                XmlElement HexMd5Elem = xDoc.CreateElement("hexMd5");
                // Зададим текстовые значения для элементов и атрибута
                XmlText nameText = xDoc.CreateTextNode(listofUsers[i].name);
                XmlText fileServerText = xDoc.CreateTextNode(((int)listofUsers[i].rights.fileServer).ToString());
                XmlText databaseServerText = xDoc.CreateTextNode(((int)listofUsers[i].rights.databaseServer).ToString());
                XmlText documentServerText = xDoc.CreateTextNode(((int)listofUsers[i].rights.documentServer).ToString());
                XmlText webServerText = xDoc.CreateTextNode(((int)listofUsers[i].rights.webServer).ToString());
                XmlText HexPasswordText = xDoc.CreateTextNode(listofUsers[i].password);

                var sha256 = SHA256.Create();
                var hashMicrosoftSha256 = sha256.ComputeHash(Encoding.UTF8.GetBytes(listofUsers[i].password));

                var md5 = MD5.Create();
                var hashMicrosoftMD5 = md5.ComputeHash(Encoding.UTF8.GetBytes(listofUsers[i].password));

                XmlText HexSha256Text = xDoc.CreateTextNode(Hex.ToHexString(hashMicrosoftSha256));
                XmlText HexMd5Text = xDoc.CreateTextNode(Hex.ToHexString(hashMicrosoftMD5));
                // добавление узлов
                nameAttr.AppendChild(nameText);
                fileServerElem.AppendChild(fileServerText);
                databaseServerElem.AppendChild(databaseServerText);
                documentServerElem.AppendChild(documentServerText);
                webServerElem.AppendChild(webServerText);
                HexPasswordElem.AppendChild(HexPasswordText);
                HexSha256Elem.AppendChild(HexSha256Text);
                HexMd5Elem.AppendChild(HexMd5Text);

                userElem.Attributes.Append(nameAttr);
                userElem.AppendChild(fileServerElem);
                userElem.AppendChild(databaseServerElem);
                userElem.AppendChild(documentServerElem);
                userElem.AppendChild(webServerElem);
                userElem.AppendChild(HexPasswordElem);
                userElem.AppendChild(HexSha256Elem);
                userElem.AppendChild(HexMd5Elem);
                //добавление нового корневого элемента в документ
                xRoot.AppendChild(userElem);
                xDoc.AppendChild(xRoot);

                //XmlElement Hex = xDoc.CreateElement("hex");
                //XmlAttribute nameAttr1 = xDoc.CreateAttribute("password");

                /*XmlElement HexPasswordElem = xDoc.CreateElement("hexPassword");
                XmlElement HexSha256Elem = xDoc.CreateElement("hexSha256");
                XmlElement HexMd5Elem = xDoc.CreateElement("hexMd5");*/

                /*XmlText HexName = xDoc.CreateTextNode("password");
                XmlText HexPasswordText = xDoc.CreateTextNode(textPassword);
                XmlText HexSha256Text = xDoc.CreateTextNode(sHA256);
                XmlText HexMd5Text = xDoc.CreateTextNode(mD5);*/

               /* nameAttr1.AppendChild(HexName);
                HexPasswordElem.AppendChild(HexPasswordText);
                HexSha256Elem.AppendChild(HexSha256Text);
                HexMd5Elem.AppendChild(HexMd5Text);

                Hex.Attributes.Append(nameAttr1);
                Hex.AppendChild(HexPasswordElem);
                Hex.AppendChild(HexSha256Elem);
                Hex.AppendChild(HexMd5Elem);

                xRoot.AppendChild(Hex);
                xDoc.AppendChild(xRoot);*/
            }
            

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML файлы(*.xml)|*.xml";
            //вызываем диалог сохранения файла
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog.FileName;
            xDoc.Save(filename);
        }

        private void bLoadFromFile_Click(object sender, EventArgs e)
        {
            // Добавляем фильтр к компоненту openFileDialog, чтобы он позволял выбрать только файлы типа xml
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML файлы(*.xml)|*.xml";
            // запускаем openFileDialog с использованием функции  ShowDialog
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog.FileName;
            // открываем xml документ
            XmlDocument doc = new XmlDocument();    
            doc.Load(filename);

            // цикл для каждого элемента из документа
            usersCount = 1;

            foreach (XmlNode node in doc.DocumentElement)
            {
                // считываем данный из файла
                string name = node.Attributes["name"].Value;
                int rightFileServer = int.Parse(node["fileServer"].InnerText);
                int rightDatabaseServer = int.Parse(node["databaseServer"].InnerText);
                int rightDocumentServer = int.Parse(node["documentServer"].InnerText);
                int rightWebServer = int.Parse(node["webServer"].InnerText);
                string password = node["hexPassword"].InnerText;
                string passwordSha256 = node["hexSha256"].InnerText;
                string passwordMD5 = node["hexMd5"].InnerText;

                UserInPolicy currentUser = new UserInPolicy(name, rightFileServer, rightDatabaseServer, rightDocumentServer, rightWebServer, password, passwordSha256, passwordMD5);
                listofUser.Add(currentUser);
                usersCount++;
            }

            var sha256 = SHA256.Create();
            var hashMicrosoftSha256 = sha256.ComputeHash(Encoding.UTF8.GetBytes("admin"));

            var md5 = MD5.Create();
            var hashMicrosoftMD5 = md5.ComputeHash(Encoding.UTF8.GetBytes("admin"));

            UserInPolicy currentUserAdministrator = new UserInPolicy("Administrator", 4, 4, 4, 4, "admin", Hex.ToHexString(hashMicrosoftSha256), Hex.ToHexString(hashMicrosoftMD5));
            listofUser.Add(currentUserAdministrator);
            check = true;
            listBox2.Items.Clear();
            for(int i = 0; i < usersCount; i++)
            {
                listBox2.Items.Add(listofUser[i].Name);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            string text = Convert.ToString(textBox2.Text);

            listofUsers[listBox2.SelectedIndex].password = text;
            /*var sha256 = SHA256.Create();
            var hashMicrosoftSha256 = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

            var md5 = MD5.Create();
            var hashMicrosoftMD5 = md5.ComputeHash(Encoding.UTF8.GetBytes(text));

            dataGridView2.Rows.Add(text, Hex.ToHexString(hashMicrosoftSha256), Hex.ToHexString(hashMicrosoftMD5));
            
            textPassword = text;
            sHA256 = Hex.ToHexString(hashMicrosoftSha256);
            mD5 = Hex.ToHexString(hashMicrosoftMD5);*/
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
