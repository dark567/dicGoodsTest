using FirebirdSql.Data.FirebirdClient;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class DicGoods : Form
    {
        public int i = 0;

        List<int> colX = new List<int>();
        public DicGoods()
        {
            InitializeComponent();

            // DriveTreeInit();
            //SimpleAdd();
            //Load += new EventHandler(Form1_Load);

            //treeView1.BeginUpdate();

            Load += new EventHandler(Form1_Load);

            // GridAdd();
        }

        private void GridAdd()
        {

            //создадим таблицу вывода товаров с колонками 
            //Название, Цена, Остаток

            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Код"; //текст в шапке
            column1.Width = 250; //ширина колонки
            column1.ReadOnly = true; //значение в этой колонке нельзя править
            column1.Name = "code"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
            column1.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
            column1.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки

            var column2 = new DataGridViewColumn();
            column2.HeaderText = "Наименование";
            column2.Name = "name";
            column2.CellTemplate = new DataGridViewTextBoxCell();

            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Цена";
            column3.Name = "price";
            column3.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);

            dataGridView1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки

            for (int i = 0; i < 100; ++i)
            {
                //Добавляем строку, указывая значения колонок поочереди слева направо
                dataGridView1.Rows.Add("Пример 1, Товар " + i, i * 1000, i);
            }

            for (int i = 0; i < 5; ++i)
            {
                //Добавляем строку, указывая значения каждой ячейки по имени (можно использовать индекс 0, 1, 2 вместо имен)
                dataGridView1.Rows.Add();
                dataGridView1["name", dataGridView1.Rows.Count - 1].Value = "Пример 2, Товар " + i;
                dataGridView1["price", dataGridView1.Rows.Count - 1].Value = i * 1000;
                dataGridView1["count", dataGridView1.Rows.Count - 1].Value = i;
            }

            //А теперь простой пройдемся циклом по всем ячейкам
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; ++j)
                {
                    //Значения ячеек хряняться в типе object
                    //это позволяет хранить любые данные в таблице
                    object o = dataGridView1[j, i].Value;
                }
            }

            int index = 0;
            object header;
            string indexStr = (index + 1).ToString();
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                header = this.dataGridView1.Rows[index].HeaderCell.Value;
                if (header == null || !header.Equals(indexStr))
                    this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;
                // index++;
                indexStr = (index++).ToString();
            }


        }

        void Form1_Load(object sender, EventArgs e)
        {
            TreeNode rootNode = new TreeNode() { Name = "0", Text = "Номенклатура", ImageIndex = 0 };
            treeView1.Nodes.Add(rootNode);

            TreeNode autoNode00 = treeView1.Nodes["0"];
            autoNode00.ImageIndex = 0;

            //создаём и добавляем дочерний узел
            autoNode00.Nodes.Add("29513", getNomNameFromID("29513"));

            TreeNode autoNode01 = rootNode.Nodes["29513"];
            autoNode01.ImageIndex = 0;

            TreeNode autoNode02;

            TreeNode autoNode03;


            //autoNode01.Nodes.Add("29530", getNomNameFromID("29530"));
            foreach (string s in getNomID_dic_goods_grp("29513"))
            {
                autoNode01.Nodes.Add(s, getNomNameFromID(s));
                autoNode02 = autoNode01.Nodes[s];
                autoNode02.ImageIndex = 0;
                foreach (string s02 in getNomID_dic_goods_grp(s))
                {
                    autoNode02.Nodes.Add(s02, getNomNameFromID(s02));
                    autoNode03 = autoNode02.Nodes[s02];
                    autoNode03.ImageIndex = 0;
                    foreach (string s03 in getNomID_dic_goods_grp(s02))
                    {
                        autoNode03.Nodes.Add(s03, getNomNameFromID(s03));
                    }
                }
                // autoNode01.Nodes.Add("29513", getNomNameFromID("29513"));
                // testDirectoryModel.AddDicGoods(new testDirectoryModel(s, "0", getNomNameFromID(s), i));
                //testDirectoryModel.AddDicGoods(new testDirectoryModel("1", "1", "test", i));
            }
















            // RegistryKey key = Registry.CurrentUser;

            ////заполняем первый раз коллекцию
            //foreach (string s in getNomID("0"))
            //{
            //    testDirectoryModel.AddDicGoods(new testDirectoryModel(s, "0", getNomNameFromID(s), i));
            //    //testDirectoryModel.AddDicGoods(new testDirectoryModel("1", "1", "test", i));
            //}


            //foreach (testDirectoryModel s in testDirectoryModel.GetDicGoods.Where(n => n.NumDepth == i).Where(n => n.ParentID == "0"/* || n.ParentID == "1"*/))
            //{
            //    //TreeNode drive = new TreeNode($"({s.ParentID}):" + s.FullName + $"({s.ID}):{s.NumDepth}") { Name = s.ParentID };
            //    //treeView1.Nodes.Add(drive);

            //    TreeNode tn = new TreeNode($"({s.ParentID}:{s.ParentID}):" + s.FullName + $"({s.ID}):{s.NumDepth}") { Name = s.ParentID };

            //    BuildChildNodes(tn);
            //    treeView1.Nodes.Add(tn);
            //    //GetGoods(drive, s);
            //}

        }

        private void BuildChildNodes(TreeNode tn)
        {

            string keyTest = (string)tn.Name;
            // MessageBox.Show(keyTest.ToString());


            /* foreach (testDirectoryModel item in testDirectoryModel.GetDicGoods.Where(n => n.NumDepth == i).Where(n => n.ParentID == keyTest || n.ParentID == ""))*/
            //заполнение массива
            foreach (testDirectoryModel item in testDirectoryModel.GetDicGoods.Where(n => n.NumDepth == i && n.ParentID == keyTest))
            {
                foreach (string s in getNomID_dic_goods_grp(item.ID))
                {
                    testDirectoryModel.AddDicGoods(new testDirectoryModel(s, item.ID, getNomNameFromID(s), i + 1));
                }
            }

            i++;


            foreach (testDirectoryModel s in testDirectoryModel.GetDicGoods.Where(n => (n.NumDepth == i)))
            {
                TreeNode child = new TreeNode($"({keyTest}:{s.ParentID}:{s.ID}):" + s.FullName + $":{s.NumDepth}") { Name = s.ParentID };
                tn.Nodes.Add(child);
            }
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //treeView1.BeginUpdate();

            //foreach (TreeNode node in e.Node.Nodes)
            //{
            //    GetGoods(node);
            //}

            //treeView1.EndUpdate();

            foreach (var childNode in e.Node.Nodes.Cast<TreeNode>())
            {
                if (childNode.Nodes.Count == 0)
                {
                    BuildChildNodes(childNode);
                }
            }
        }

        private void SimpleAdd()
        {
            ////
            //// This is the first node in the view.
            ////
            //TreeNode treeNode = new TreeNode("Windows");
            //treeView1.Nodes.Add(treeNode);
            ////
            //// Another node following the first node.
            ////
            //treeNode = new TreeNode("Linux");
            //treeView1.Nodes.Add(treeNode);
            ////
            //// Create two child nodes and put them in an array.
            //// ... Add the third node, and specify these as its children.
            ////
            //TreeNode node2 = new TreeNode("C#");
            //TreeNode node3 = new TreeNode("VB.NET");
            //TreeNode[] array = new TreeNode[] { node2, node3 };
            ////
            //// Final node.
            ////
            //treeNode = new TreeNode("Dot Net Perls", array);
            //treeView1.Nodes.Add(treeNode);

            //  TreeNode treeNode;

            foreach (string s in getNomID_dic_goods_grp("0"))
            {
                testDirectoryModel.AddDicGoods(new testDirectoryModel(s, "0", getNomNameFromID(s), i));
            }

            //BuildTree();
            //BuildTree();
            //BuildTree();

            //i = 0;



            foreach (testDirectoryModel s in testDirectoryModel.GetDicGoods.Where(n => n.NumDepth == i))
            {
                TreeNode drive = new TreeNode(s.FullName + $"({s.ID}):{s.NumDepth}:{s.ParentID}", 0, 0);
                treeView1.Nodes.Add(drive);


                GetGoods(drive, s);


            }
        }

        public void GetGoods(TreeNode node, testDirectoryModel Parent)
        {

            //node.Nodes.Clear();



            foreach (testDirectoryModel item in testDirectoryModel.GetDicGoods.Where(n => n.NumDepth == i).Where(n => n.ParentID == Parent.ParentID))
            {
                foreach (string s in getNomID_dic_goods_grp(item.ID))
                {
                    testDirectoryModel.AddDicGoods(new testDirectoryModel(s, item.ID, getNomNameFromID(s), i + 1));
                }
            }

            i++;

            foreach (testDirectoryModel s in testDirectoryModel.GetDicGoods.Where(n => n.NumDepth == i)/*.Where(n => n.ParentID == Parent.ParentID)*/)
            {

                TreeNode drive = new TreeNode($"({s.ParentID}):" + s.FullName + $"({s.ID}):{s.NumDepth}", 0, 0);
                node.Nodes.Add(drive);

            }

        }

        public void BuildTree()
        {
            i++;
            foreach (testDirectoryModel item in testDirectoryModel.GetDicGoods.Where(n => n.NumDepth == i - 1)/*.Where((n => n.NumDepth == i - 1)*/)
            {
                foreach (string s in getNomID_dic_goods_grp(item.ID))
                {
                    testDirectoryModel.AddDicGoods(new testDirectoryModel(s, item.ID, getNomNameFromID(s), i));
                }
            }

        }







        //#region
        ///// <summary>
        ///// Инициализация окна древовидного списка дисковых устройств
        ///// </summary>
        //public void DriveTreeInit()
        //{
        //    string[] drivesArray = Directory.GetLogicalDrives();

        //    foreach (string s in drivesArray)
        //    {
        //        testDirectoryModel.AddDicGoods(new testDirectoryModel(s, s + "_", 0));
        //    }

        //    treeView1.BeginUpdate();
        //    treeView1.Nodes.Clear();

        //    foreach (testDirectoryModel s in testDirectoryModel.GetDicGoods)
        //    {
        //        TreeNode drive = new TreeNode(s.ID, 0, 0);
        //        treeView1.Nodes.Add(drive);

        //        GetDirs(drive);
        //    }

        //    //foreach (string s in drivesArray)
        //    //{
        //    //    TreeNode drive = new TreeNode(s + "_", 0, 0);
        //    //    treeView1.Nodes.Add(drive);

        //    //    GetDirs(drive);
        //    //}

        //    treeView1.EndUpdate();
        //}

        ///// <summary>
        ///// Получение списка каталогов
        ///// </summary>
        //public void GetDirs(TreeNode node)
        //{
        //    DirectoryInfo[] diArray;

        //    node.Nodes.Clear();

        //    string fullPath = node.FullPath;
        //    DirectoryInfo di = new DirectoryInfo(fullPath);

        //    try
        //    {
        //        diArray = di.GetDirectories();
        //    }
        //    catch
        //    {
        //        return;
        //    }

        //    foreach (DirectoryInfo dirinfo in diArray)
        //    {
        //        testDirectoryModel.AddDicGoods(new testDirectoryModel(dirinfo.Name, dirinfo.Name + "_", 0));
        //    }

        //    foreach (testDirectoryModel item in testDirectoryModel.GetDicGoods)
        //    {
        //        TreeNode dir = new TreeNode(item.ID, 0, 0);
        //        node.Nodes.Add(dir);
        //    }

        //    //foreach (DirectoryInfo dirinfo in diArray)
        //    //{
        //    //    TreeNode dir = new TreeNode(dirinfo.Name, 0, 0);
        //    //    node.Nodes.Add(dir);
        //    //}
        //}



        //#endregion


        public static ArrayList getNomID_dic_goods_grp(string ID)
        {
            ArrayList Nom = new ArrayList(); ;

            // Описание: ExecuteScalar — получение единственного значения. Firebird, InterBase .Net провайдер (c#)
            string connString = "User=SYSDBA;" +
                                "Password=masterkey;" +
                                "Charset = UTF8;" +
                                "Database=127.0.0.1:terra;" +
                                "DataSource=localhost;" +
                                "Port=3050;";
            FbConnection fb = new FbConnection(connString);

            fb.Open();
            FbCommand SelectSQL = new FbCommand("SELECT id FROM dic_goods_grp where PARENT_ID = @cust_no and id <> '0' ORDER BY name", fb);
            //add one IN parameter                     
            FbParameter nameParam = new FbParameter("@cust_no", ID);
            // добавляем параметр к команде
            SelectSQL.Parameters.Add(nameParam);

            FbTransaction fbt = fb.BeginTransaction();
            SelectSQL.Transaction = fbt;
            FbDataReader reader = SelectSQL.ExecuteReader();

            try
            {

                while (reader.Read())
                {
                    Nom.Add(reader.GetString(0));
                }
            }
            finally
            {
                fbt.Commit();
                reader.Close();
                SelectSQL.Dispose();
                fb.Close();
            }

            return Nom;

        }


        public static string getNomNameFromID(string ID)
        {
            string Nom = "n/a";

            // Описание: ExecuteScalar — получение единственного значения. Firebird, InterBase .Net провайдер (c#)
            string connString = "User=SYSDBA;" +
                                "Password=masterkey;" +
                                "Charset = UTF8;" +
                                "Database=127.0.0.1:terra;" +
                                "DataSource=localhost;" +
                                "Port=3050;";
            FbConnection fb = new FbConnection(connString);

            fb.Open();
            FbCommand SelectSQL = new FbCommand("SELECT Name FROM dic_goods_grp where ID = @cust_no ORDER BY name", fb);
            //add one IN parameter                     
            FbParameter nameParam = new FbParameter("@cust_no", ID);
            // добавляем параметр к команде
            SelectSQL.Parameters.Add(nameParam);


            FbTransaction fbt = fb.BeginTransaction();
            SelectSQL.Transaction = fbt;
            FbDataReader reader = SelectSQL.ExecuteReader();
            // SelectSQL.Parameters["cust_no"].Value = reader["0"];
            try
            {

                while (reader.Read()) { Nom = reader.GetString(0); }

            }
            finally
            {
                fbt.Commit();
                reader.Close();
                SelectSQL.Dispose();
                fb.Close();
            }

            return Nom;

        }

        /// <summary>
        /// 3D button paint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, button1.ClientRectangle,
        SystemColors.ControlLightLight, 1, ButtonBorderStyle.Outset,
        SystemColors.ControlLightLight, 1, ButtonBorderStyle.Outset,
        SystemColors.ControlLightLight, 3, ButtonBorderStyle.Outset,
        SystemColors.ControlLightLight, 3, ButtonBorderStyle.Outset);
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            dataGridView1.Rows.Clear();
            // dataGridView1.Columns.Clear();

            dataGridView1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки
            dataGridView1.AllowUserToResizeColumns = true;

            TreeNode selectedNode = e.Node;
            DicGoodsModel.ClearDicGoodsModel();
            getNomDicGoodsID_(selectedNode.Name);
            //MessageBox.Show(selectedNode.Name);

            BindingList<SampleRow> data = new BindingList<SampleRow>(); //Специальный список List с вызовом события обновления внутреннего состояния, необходимого для автообновления datagridview

            foreach (DicGoodsModel s in DicGoodsModel.GetDicGoodsModel)
            {
                data.Add(new SampleRow(code: s.ID, name: s.Name, _isService: s.IsService, price: s.Price.ToString("F2"), _isSale: s.IsSale));
            }

            dataGridView1.DataSource = data;

            //// set autosize mode
            // dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            // dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // //dataGridView1.Columns[0].Width = 250;
            // //dataGridView1.Columns[1].Width = 250;
            // //dataGridView1.Columns[2].Width = 250;

            // //datagrid has calculated it widths so we can store them
            // for (int i = 0; i <= dataGridView1.Columns.Count - 1; i++)
            // {
            //     //store autosized widths
            //     int colw = dataGridView1.Columns[i].Width;
            //     //remove autosizing
            //     dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //     //set width to calculated by autosize
            //     dataGridView1.Columns[i].Width = colw;
            // }

            //dataGridView1.AutoGenerateColumns = false;

            ////create the column programatically
            //DataGridViewCell cell = new DataGridViewTextBoxCell();
            //DataGridViewTextBoxColumn colFileName = new DataGridViewTextBoxColumn()
            //{
            //    CellTemplate = cell,
            //    Name = "code",
            //    HeaderText = "File Name",
            //    DataPropertyName = "Value" // Tell the column which property of FileName it should use
            //};

            //dataGridView1.Columns.Add(colFileName);


            /*
            TreeNode selectedNode = e.Node;

            DicGoodsModel.ClearDicGoodsModel();
            getNomDicGoodsID_(selectedNode.Name);

            foreach (DicGoodsModel s in DicGoodsModel.GetDicGoodsModel)
            {
                dataGridView1.Rows.Add();
                dataGridView1["code", dataGridView1.Rows.Count - 1].Value = s.ID;
                dataGridView1["name", dataGridView1.Rows.Count - 1].Value = s.Name;
                dataGridView1["price", dataGridView1.Rows.Count - 1].Value = s.Price.ToString("F2");
            }
            */
            //for (int i = 0; i < 100; ++i)
            //{
            //    //Добавляем строку, указывая значения колонок поочереди слева направо
            //    dataGridView1.Rows.Add("Пример 1, Товар " + i, i * 1000, i);
            //}

            //for (int i = 0; i < 5; ++i)
            //{
            //   // Добавляем строку, указывая значения каждой ячейки по имени(можно использовать индекс 0, 1, 2 вместо имен)
            //    dataGridView1.Rows.Add();
            //    dataGridView1["code", dataGridView1.Rows.Count - 1].Value = "Пример 2, Товар xxx" + i;
            //    dataGridView1["name", dataGridView1.Rows.Count - 1].Value = i * 1000;
            //    dataGridView1["price", dataGridView1.Rows.Count - 1].Value = i;
            //}

            ////А теперь простой пройдемся циклом по всем ячейкам
            //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            //{
            //    for (int j = 0; j < dataGridView1.Columns.Count; ++j)
            //    {
            //        //Значения ячеек хрянятся в типе object
            //        //это позволяет хранить любые данные в таблице
            //        object o = dataGridView1[j, i].Value;
            //    }
            //}

            //нумерация
            int index = 0;
            object header;
            string indexStr = (index + 1).ToString();
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                header = this.dataGridView1.Rows[index].HeaderCell.Value;
                if (header == null || !header.Equals(indexStr))
                    this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;
                indexStr = (index++).ToString();
            }

        }

        public static void getNomDicGoodsID_(string ID)
        {
            //ArrayList Code = new ArrayList();
            //ArrayList Name = new ArrayList();
            //ArrayList PriceOut = new ArrayList();

            // Описание: ExecuteScalar — получение единственного значения. Firebird, InterBase .Net провайдер (c#)
            string connString = "User=SYSDBA;" +
                                "Password=masterkey;" +
                                "Charset = UTF8;" +
                                "Database=127.0.0.1:terra;" +
                                "DataSource=localhost;" +
                                "Port=3050;";
            FbConnection fb = new FbConnection(connString);

            fb.Open();
            FbCommand SelectSQL = new FbCommand("SELECT code, name, IS_SERVICE, PRICE_OUT, IS_ACTIVE FROM dic_goods where GRP_ID = @cust_no ORDER BY name", fb);
            //add one IN parameter                     
            FbParameter nameParam = new FbParameter("@cust_no", ID);
            // добавляем параметр к команде
            SelectSQL.Parameters.Add(nameParam);

            FbTransaction fbt = fb.BeginTransaction();
            SelectSQL.Transaction = fbt;
            FbDataReader reader = SelectSQL.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    DicGoodsModel.AddDicGoodsModel(new DicGoodsModel(id: reader?.GetString(0), name: reader?.GetString(1), isService: reader?.GetString(2) == "1" ? true : false, price: reader.GetDouble(3),isSale: reader?.GetString(4) == "1" ? true : false));
                }
            }
            catch (Exception) //selection=="+"? (x+y) : (x-y);
            {
                //dataGridView1.Rows.Clear();
            }
            finally
            {
                fbt.Commit();
                reader.Close();
                SelectSQL.Dispose();
                fb.Close();
            }
            // return dgm;
        }

        public static ArrayList getNomDicGoodsID(string ID)
        {
            ArrayList Code = new ArrayList();
            ArrayList Name = new ArrayList();
            ArrayList PriceOut = new ArrayList();

            // Описание: ExecuteScalar — получение единственного значения. Firebird, InterBase .Net провайдер (c#)
            string connString = "User=SYSDBA;" +
                                "Password=masterkey;" +
                                "Charset = UTF8;" +
                                "Database=127.0.0.1:terra;" +
                                "DataSource=localhost;" +
                                "Port=3050;";
            FbConnection fb = new FbConnection(connString);

            fb.Open();
            FbCommand SelectSQL = new FbCommand("SELECT id, name, price_out FROM dic_goods where GRP_ID = @cust_no ORDER BY name", fb);
            //add one IN parameter                     
            FbParameter nameParam = new FbParameter("@cust_no", ID);
            // добавляем параметр к команде
            SelectSQL.Parameters.Add(nameParam);

            FbTransaction fbt = fb.BeginTransaction();
            SelectSQL.Transaction = fbt;
            FbDataReader reader = SelectSQL.ExecuteReader();

            try
            {

                while (reader.Read())
                {
                    Code.Add(reader.GetString(0));
                    Name.Add(reader.GetString(1));
                    PriceOut.Add(reader.GetString(2));
                }
            }
            finally
            {
                fbt.Commit();
                reader.Close();
                SelectSQL.Dispose();
                fb.Close();
            }

            return Name;

        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //listView1.Focus();
            //this.listView1.Items[0].Focused = true;
            //this.listView1.Items[0].Selected = true;
        }

        /// <summary>
        /// нумерация строк
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;
        }

        /// <summary>
        /// обработка аттрибутов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            // Get the property object based on the DataPropertyName of the column
            var property = typeof(SampleRow).GetProperty(e.Column.DataPropertyName);
            // Get the ColumnWeight attribute from the property if it exists
            var weightAttribute = (ColumnWeight)property?.GetCustomAttribute(typeof(ColumnWeight));
            if (weightAttribute != null)
            {
                // Finally, set the FillWeight of the column to our defined weight in the attribute
                e.Column.FillWeight = weightAttribute.Weight;
            }

            var weightAttributeDisp = (DisplayNameAttribute)property?.GetCustomAttribute(typeof(DisplayNameAttribute));
            if (weightAttributeDisp != null)
            {
                // Finally, set the FillWeight of the column to our defined weight in the attribute
                e.Column.HeaderText = weightAttributeDisp.DisplayName;
            }

            var autoSize = (AutoSizeMode)property?.GetCustomAttribute(typeof(AutoSizeMode));
            if (autoSize != null)
            {
                // Finally, set the FillWeight of the column to our defined weight in the attribute
                e.Column.AutoSizeMode = (DataGridViewAutoSizeColumnMode)autoSize.SizeMode;
            }

            //var TypeIsService = (TypesIServiceAttribute)property?.GetCustomAttribute(typeof(TypesIServiceAttribute));
            //if (TypeIsService != null)
            //{
            //    // Finally, set the FillWeight of the column to our defined weight in the attribute
            //    if (TypeIsService.typesIServiceAttribute)
            //    {
            //        AddOutOfOfficeColumn();
            //    }
            //}
            // base.DataGridView1_ColumnAdded(sender, e);



        }

        private void AddOutOfOfficeColumn()
        {
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            {
                //column.HeaderText = ColumnName.OutOfOffice.ToString();
                //column.Name = ColumnName.OutOfOffice.ToString();
                column.AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.FlatStyle = FlatStyle.Standard;
                column.ThreeState = true;
                column.CellTemplate = new DataGridViewCheckBoxCell();
                column.CellTemplate.Style.BackColor = Color.Beige;
            }

            this.dataGridView1.Columns.Insert(0, column);
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
