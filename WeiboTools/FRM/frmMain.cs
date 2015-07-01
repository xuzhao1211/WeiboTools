using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Spring.Context.Support;
using Spring.Data.NHibernate.Generic;
using NetDimension.Weibo;
using System.Threading;
using NHibernate;
using System.Data.SQLite;
using WeiboTools.Xml;
using System.Collections;

namespace WeiboTools.frm
{
    public partial class frmMain : Form
    {
        private ContextMenuStrip gridRightMenu = new ContextMenuStrip();
        private HibernateTemplate hibernateTemplate;

        public frmMain()
        {
            hibernateTemplate = ContextRegistry.GetContext().GetObject("HibernateTemplate") as HibernateTemplate;
            InitializeComponent();
            InitData();
            BindEvent(); 
            InitRigthMenu();
        }

        #region 表格左键菜单
        public void InitRigthMenu()
        {
            gridRightMenu.Items.Add("删除", null, new EventHandler(delegate(object sender, EventArgs e)
            {
                DataGridView d = gridRightMenu.Tag as DataGridView;
                List<DataGridViewRow> rlist = new List<DataGridViewRow>();
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    if (d.Rows[i].Selected &&
                        !d.Rows[i].IsNewRow)
                    {
                        rlist.Add(d.Rows[i]);
                    }
                }
                foreach (DataGridViewRow row in rlist)
                {
                    if (d.Tag == typeof(Xml.SinaUser))
                    {
                        hibernateTemplate.Delete(
                        hibernateTemplate.Get<Xml.SinaUser>(row.Cells[0].Value));
                    }
                    if (d.Tag == typeof(Xml.API))
                    {
                        hibernateTemplate.Delete(
                        hibernateTemplate.Get<Xml.API>(row.Cells[0].Value));
                    }
                }
                if (d.Tag == typeof(Xml.SinaUser))
                {
                    this.Init_dataGridView1();
                }
                if (d.Tag == typeof(Xml.API))
                {
                    this.Init_dataGridView2();
                }
            }));
        } 
        #endregion

        public void InitData()
        {
            Init_dataGridView1();
            Init_dataGridView2();
            Init_dataGridView3();
            Init_dataGridView4();
            Init_comboBox1();
            Init_comboBox2();
            Init_comboBox3();
            Init_comboBox4();
            Init_comboBox5();
            Init_comboBox6();
            Init_comboBox7();
            Init_comboBox8();
        }

        public void BindEvent()
        {
            this.dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(OnCellMouseDown);
            this.dataGridView2.CellMouseDown += new DataGridViewCellMouseEventHandler(OnCellMouseDown);
            this.dataGridView3.CellMouseDown += new DataGridViewCellMouseEventHandler(OnCellMouseDown);
            this.dataGridView4.CellMouseDown += new DataGridViewCellMouseEventHandler(OnCellMouseDown);            
        }

        #region 载入表格数据
        void Init_dataGridView1()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.SinaUser>(hibernateTemplate, "", "", 0);
            this.dataGridView1.DataSource = d;
            this.dataGridView1.Tag = typeof(Xml.SinaUser);
        }

        void Init_dataGridView2()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.API>(hibernateTemplate, "", "", 0);
            this.dataGridView2.DataSource = d;
            this.dataGridView2.Tag = typeof(Xml.API);
        }

        void Init_dataGridView3()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.FollowersUser>(hibernateTemplate, "", "", 0);
            this.dataGridView3.DataSource = d;
            this.dataGridView3.Tag = typeof(Xml.API);
        }

        void Init_dataGridView4()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.FriendsUser>(hibernateTemplate, "", "", 0);
            this.dataGridView4.DataSource = d;
            this.dataGridView4.Tag = typeof(Xml.API);
        }

        void Init_comboBox1()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.SinaUser>(hibernateTemplate, "", "", 0);
            this.comboBox1.DisplayMember = "UserName";
            this.comboBox1.DataSource = d;
        }

        void Init_comboBox2()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.API>(hibernateTemplate, "", "", 0);
            this.comboBox2.DisplayMember = "Name";
            this.comboBox2.DataSource = d;
        }

        void Init_comboBox3()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.SinaUser>(hibernateTemplate, "", "", 0);
            this.comboBox3.DisplayMember = "UserName";
            this.comboBox3.DataSource = d;
        }

        void Init_comboBox4()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.API>(hibernateTemplate, "", "", 0);
            this.comboBox4.DisplayMember = "Name";
            this.comboBox4.DataSource = d;
        }

        void Init_comboBox5()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.SinaUser>(hibernateTemplate, "", "", 0);
            this.comboBox5.DisplayMember = "UserName";
            this.comboBox5.DataSource = d;
        }

        void Init_comboBox6()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.API>(hibernateTemplate, "", "", 0);
            this.comboBox6.DisplayMember = "Name";
            this.comboBox6.DataSource = d;
        }

        void Init_comboBox7()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.SinaUser>(hibernateTemplate, "", "", 0);
            this.comboBox7.DisplayMember = "UserName";
            this.comboBox7.DataSource = d;
        }

        void Init_comboBox8()
        {
            var d = WeiboTools.Tools.GetTopList<Xml.API>(hibernateTemplate, "", "", 0);
            this.comboBox8.DisplayMember = "Name";
            this.comboBox8.DataSource = d;
        }
        #endregion

        #region 控件事件

        private void OnCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView d = sender as DataGridView;
                
                if (d != null && !d.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
                {
                    if (e.RowIndex >= 0)
                    {
                        //若行已是选中状态就不再进行设置
                        if (d.Rows[e.RowIndex].Selected == false)
                        {
                            d.ClearSelection();
                            d.Rows[e.RowIndex].Selected = true;
                        }
                        //只选中一行时设置活动单元格
                        if (d.SelectedRows.Count == 1)
                        {
                            //d.CurrentCell = d.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        }

                        gridRightMenu.Tag = d;
                        gridRightMenu.Show(MousePosition.X, MousePosition.Y);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Xml.SinaUser m = new WeiboTools.Xml.SinaUser();
            m.UserName = this.textBox1.Text;
            m.Password = this.textBox2.Text;
            hibernateTemplate.Save(m);
            Init_dataGridView1();
            this.textBox1.Text = string.Empty;
            this.textBox2.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Xml.API m = new WeiboTools.Xml.API();
            m.Name = this.textBox4.Text;
            m.AppKey = this.textBox3.Text;
            m.AppSecret = this.textBox10.Text;
            hibernateTemplate.Save(m);
            Init_dataGridView2();
            this.textBox4.Text = string.Empty;
            this.textBox3.Text = string.Empty;
            this.textBox10.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var u = this.comboBox1.SelectedItem as Xml.SinaUser;
                var a = this.comboBox2.SelectedItem as Xml.API;
                var s = int.Parse(textBox11.Text);
                var p = int.Parse(textBox12.Text);
                var pn = int.Parse(textBox13.Text);

                this.textBox5.AppendText("开始:同步粉丝\r\n");
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object o)
                {
                    try
                    {
                        Client client;
                        string uid = Tools.CreateClient(out client, a.AppKey, a.AppSecret, u.UserName, u.Password);
                        Tools.UpdateFollowers(hibernateTemplate, textBox5, client, uid, s, p, pn);
                    }
                    catch (Exception ex)
                    {
                        textBox5.Invoke(new EventHandler(delegate(object so, EventArgs se)
                        {
                            textBox5.AppendText("出错:" + ex.Message + "\r\n");
                        }));
                    }
                }));
            }
            catch (Exception ex)
            {
                this.textBox5.AppendText("出错:" + ex.Message + "\r\n");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var u = this.comboBox3.SelectedItem as Xml.SinaUser;
                var a = this.comboBox4.SelectedItem as Xml.API;
                var s = int.Parse(textBox16.Text);
                var p = int.Parse(textBox15.Text);
                var pn = int.Parse(textBox14.Text);

                this.textBox6.AppendText("开始:同步朋友\r\n");
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object o)
                {
                    try
                    {
                        Client client;
                        string uid = Tools.CreateClient(out client, a.AppKey, a.AppSecret, u.UserName, u.Password);
                        Tools.UpdateFriends(hibernateTemplate, textBox6, client, uid, s, p, pn);
                    }
                    catch (Exception ex)
                    {
                        textBox6.Invoke(new EventHandler(delegate(object so, EventArgs se)
                        {
                            textBox6.AppendText("出错:" + ex.Message + "\r\n");
                        }));
                    }
                }));
            }
            catch (Exception ex)
            {
                this.textBox6.AppendText("出错:" + ex.Message + "\r\n");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var u = this.comboBox5.SelectedItem as Xml.SinaUser;
                var a = this.comboBox6.SelectedItem as Xml.API;

                var du = this.comboBox7.SelectedItem as Xml.SinaUser;
                var da = this.comboBox8.SelectedItem as Xml.API;

                var t = int.Parse(textBox9.Text);


                ArrayList oids = new ArrayList();
                List<NetDimension.Weibo.Entities.user.Entity> arr = checkedListBox1.Tag as List<NetDimension.Weibo.Entities.user.Entity>;

                foreach (var i in checkedListBox1.CheckedItems)
                {
                    foreach(var ar in arr)
                    {
                        if(ar.Name == i.ToString())
                        {
                            oids.Add(ar.IDStr);
                        }
                    }
                }

                this.textBox6.AppendText("开始:转发\r\n");
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object o)
                {
                    while (true)
                    {
                        try
                        {
                            Client s_client;
                            string s_uid = Tools.CreateClient(out s_client, a.AppKey, a.AppSecret, u.UserName, u.Password);

                            Client d_client;
                            string d_uid = Tools.CreateClient(out d_client, da.AppKey, da.AppSecret, du.UserName, du.Password);

                            NetDimension.Weibo.Entities.status.Collection coll = s_client.API.Statuses.FriendsTimeline("0", "0", 50, 1, false, 1);
                            if (coll != null && coll.Statuses != null)
                            {
                                foreach (NetDimension.Weibo.Entities.status.Entity item in coll.Statuses)
                                {
                                    if (oids.Contains(item.User.IDStr))
                                    {

                                        byte[] pic = new System.Net.WebClient().DownloadData(item.OriginalPictureUrl);

                                        if (Tools.GetTopList<StatusesUploadLog>(hibernateTemplate, "StatusesID = '" + item.ID + "'", "", 1).Count == 0)
                                        {
                                            d_client.API.Statuses.Upload(item.Text, pic, 0.0f, 0.0f, "");

                                            textBox7.Invoke(new EventHandler(delegate(object so, EventArgs se)
                                            {
                                                textBox7.AppendText("复制微博" + item.Text + "\r\n");
                                            }));

                                            hibernateTemplate.Save(new StatusesUploadLog(item.ID, du.UserName, DateTime.Now));

                                            break;
                                        }
                                        else
                                        {
                                            textBox7.Invoke(new EventHandler(delegate(object so, EventArgs se)
                                            {
                                                textBox7.AppendText("已存微博" + item.Text + "\r\n");
                                            }));
                                        }
                                    }
                                    else
                                    {
                                        textBox7.Invoke(new EventHandler(delegate(object so, EventArgs se)
                                        {
                                            textBox7.AppendText("不存在目标用户的微博\r\n");
                                        }));
                                    }
                                }
                            }
                            else
                            {
                                textBox7.Invoke(new EventHandler(delegate(object so, EventArgs se)
                                {
                                    textBox7.AppendText("没有微博\r\n");
                                }));
                            }
                        }
                        catch (Exception ex)
                        {
                            textBox7.Invoke(new EventHandler(delegate(object so, EventArgs se)
                            {
                                textBox7.AppendText("出错:" + ex.Message + "\r\n");
                            }));
                        }
                        Thread.Sleep(t * 1000);
                    }
                }));
            }
            catch (Exception ex)
            {
                this.textBox6.AppendText("出错:" + ex.Message + "\r\n");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = hibernateTemplate.SessionFactory.OpenSession();
                SQLiteCommand cmd = s.Connection.CreateCommand() as SQLiteCommand;
                cmd.CommandText = this.textBox19.Text;
                SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView5.DataSource = dt;
                s.Close();
            }
            catch (Exception ex)
            {
                this.textBox17.AppendText(ex.Message + "\r\n");
            }
        }
        #endregion

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            InitData();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var u = this.comboBox5.SelectedItem as Xml.SinaUser;
            var a = this.comboBox6.SelectedItem as Xml.API;

            Client s_client;
            string s_uid = Tools.CreateClient(out s_client, a.AppKey, a.AppSecret, u.UserName, u.Password);

            NetDimension.Weibo.Entities.user.Collection coll = s_client.API.Friendships.Friends(s_uid, "", 50, 0, true);
            List<NetDimension.Weibo.Entities.user.Entity> arr = new List<NetDimension.Weibo.Entities.user.Entity>(coll.Users);

            checkedListBox1.Items.Clear();

            foreach (var ou in arr)
            {
                checkedListBox1.Items.Add(ou.Name);
            }

            checkedListBox1.Tag = arr;
        }
    }
}
