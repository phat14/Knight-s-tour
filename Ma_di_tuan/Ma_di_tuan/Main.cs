using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Ma_di_tuan
{
    public partial class Main : Form
    {
        public int[,] vt = new int[3, 2501];
        private Label[,] labelSo;
        private PictureBox[,] banCo;
        private GroupInit _group;
        public int kich_thuoc;//Kích thước bàn cờ
        public int toa_do_X,_X;
        public int toa_do_Y,_Y;
        private int bd = 1;//Số bước đi của mã
        private bool tuyChon = false;//Tùy chọn Start hay Stop
        private int do_rong = 30;
        public Main()
        {
            InitializeComponent();
        }
        public void khoiTao()
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            this.Controls.Clear();
            MaDiTuan mdt = new MaDiTuan();
            labelSo = new Label[2, kich_thuoc + 1];
            for (int i = 1; i <= kich_thuoc; i++)
            {
                //Ngang
                labelSo[1, i] = new Label();
                labelSo[1, i].AutoSize = true;
                labelSo[1, i].Location = new System.Drawing.Point(do_rong * i - 5, 9);
                labelSo[1, i].Name = i.ToString();
                labelSo[1, i].Size = new System.Drawing.Size(19, 13);                
                labelSo[1, i].TabIndex = 1;
                labelSo[1, i].Text = i.ToString();
                labelSo[1, i].ForeColor = Color.White;
                this.Controls.Add(labelSo[1, i]);
                //Doc
                labelSo[0, i] = new Label();
                labelSo[0, i].AutoSize = true;
                labelSo[0, i].Location = new System.Drawing.Point(5, do_rong * i - 5);
                labelSo[0, i].Name = i.ToString();
                labelSo[0, i].Size = new System.Drawing.Size(19, 13);
                labelSo[0, i].TabIndex = 1;
                labelSo[0, i].Text = i.ToString();
                labelSo[0, i].ForeColor = Color.White;
                this.Controls.Add(labelSo[0, i]);
            }
            //Khởi tạo mảng hai chiều picture hiển thị bàn cờ
            banCo = new PictureBox[kich_thuoc + 1, kich_thuoc + 1];
            for (int i = 1; i <= kich_thuoc; i++)
            {
                for (int j = 1; j <= kich_thuoc; j++)
                {
                    banCo[i, j] = new PictureBox();
                    if (i % 2 != 0)
                    {
                        if (j % 2 != 0) banCo[i, j].BackgroundImage = global::Ma_di_tuan.Properties.Resources.Nen_trang;
                        else banCo[i, j].BackgroundImage = global::Ma_di_tuan.Properties.Resources.Nen_do;
                    }
                    else if (j % 2 != 0) banCo[i, j].BackgroundImage = global::Ma_di_tuan.Properties.Resources.Nen_do;
                    else banCo[i, j].BackgroundImage = global::Ma_di_tuan.Properties.Resources.Nen_trang;


                    banCo[i, j].Location = new System.Drawing.Point(25 + (j - 1) * do_rong, 25 + (i - 1) * do_rong);
                    banCo[i, j].Name = "pictureBox1";
                    banCo[i, j].Size = new System.Drawing.Size(do_rong, do_rong);
                    banCo[i, j].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    banCo[i, j].TabIndex = 0;
                    banCo[i, j].TabStop = false;
                    this.Controls.Add(banCo[i, j]);
                }
            }
            banCo[toa_do_X, toa_do_Y].Image = global::Ma_di_tuan.Properties.Resources.Ngua_do;
            //Khởi tạo control điều khiển chương trình chính
            _group = new GroupInit();
            _group.Top = 12;
            _group.Left = 25 + 12 + do_rong * kich_thuoc;
            _group.labKT.Text = "Size of chess board: " + kich_thuoc.ToString() + "x" + kich_thuoc.ToString();
            _group.labX.Text = "Coordinate X: " + toa_do_X.ToString();
            _group.labY.Text = "Coordinate Y: " + toa_do_Y.ToString();
            mdt.Set(kich_thuoc, toa_do_X, toa_do_Y);
            mdt._vt = vt;
            if (mdt.TimDuong() == true)
            {
                _group.labKQ.Text = "Status: found";
            }
            else
            {
                _group.labKQ.Text = "Status: can not found";
                _group.grpKQ.Enabled = false;
                _group.grpLuu.Enabled = false;
            }
            _X = toa_do_X;
            _Y = toa_do_Y;
            _group.btnVT_BatDau.Click += new EventHandler(btnVT_BatDau_Click);
            _group.btnVT_Init.Click += new EventHandler(btnVT_Init_Click);
            _group.btnVT_Luu.Click += new EventHandler(btnVT_Luu_Click);
            _group.cboTG_Cho.SelectedIndexChanged += new EventHandler(cboTG_Cho_SelectedIndexChanged);
            this.Controls.Add(_group);
            //Khởi tạo tùy chọn
            bd = 1;
            tuyChon = false;
            //Điều chỉnh độ rộng thích hợp cho form
            Width = 50 + do_rong * kich_thuoc + _group.Width;
            this.WindowState = FormWindowState.Normal;
            if (kich_thuoc <= 14)
            {
                AutoScroll = false;
                FormBorderStyle = FormBorderStyle.FixedSingle;
                MaximizeBox = false;
                _group.Height = 30 + do_rong * 14;
                Height = 25 + 12 + do_rong * 14 + 34;//34 là chiều cao của thanh tiêu đề
            }
            else
            {
                AutoScroll = true;
                FormBorderStyle = FormBorderStyle.Sizable;
                MaximizeBox = true;                
                _group.Height = 30 + do_rong * kich_thuoc;
                Width += 20;//20 độ rộng của thanh trượt
                Height = 25 + 12 + do_rong * kich_thuoc + 34 + 20;//34 là chiều cao của thanh tiêu đề, 20 là chiều cao than trượt
            }
        }

        void cboTG_Cho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_group.cboTG_Cho.Text == "0.5")
            {
                timer1.Interval = 500;
            }
            else
            {
                timer1.Interval = int.Parse(_group.cboTG_Cho.Text) * 1000;
                if (timer1.Interval >= 2000)
                {
                    timer2.Enabled = true;
                }
                else timer2.Enabled = false;
            }
        }

        void btnVT_Luu_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "txt files (*.txt)|*.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                FileStream fileStream = new FileStream(save.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("Kich thuoc ban co: " + kich_thuoc.ToString() + "x" + kich_thuoc.ToString());
                streamWriter.WriteLine("Toa do xuat phat: ( " + toa_do_X.ToString() + " , " + toa_do_Y.ToString() + " )");
                streamWriter.WriteLine("Toa do cac diem di lan luot la:");
                for (int i = 1; i < kich_thuoc * kich_thuoc; i++)
                    streamWriter.WriteLine("( " + vt[1, i].ToString() + " , " + vt[2, i].ToString() + " )");
                streamWriter.WriteLine("Ket thuc...");
                streamWriter.Close();
                fileStream.Close();
            }
        }

        void btnVT_Init_Click(object sender, EventArgs e)
        {
            Init form = new Init();
            form.form = this;
            form.Show();
            form.Focus();
        }

        void btnVT_BatDau_Click(object sender, EventArgs e)
        {
            if (tuyChon == false)//bắt đầu chạy tự động
            {
                tuyChon = true;
                _group.btnVT_BatDau.ButtonText = "Ngưng";
                timer1.Enabled = true;
                if (_group.cboTG_Cho.Text == "0.5")
                {
                    timer1.Interval = 500;
                }
                else
                {
                    timer1.Interval = int.Parse(_group.cboTG_Cho.Text) * 1000;
                    if (timer1.Interval >= 2000)
                    {
                        timer2.Enabled = true;
                    }
                    else
                    {                        
                        timer2.Enabled = false;
                        //banCo[vt[1, bd], vt[2, bd]].Image = null;
                    }
                }
            }
            else
            {
                tuyChon = false;
                _group.btnVT_BatDau.ButtonText = "Tiếp theo";
                timer1.Enabled = false;
                timer2.Enabled = false;
            }
        }

        void btnLuu_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "txt files (*.txt)|*.txt";  
            if (save.ShowDialog() == DialogResult.OK)
            {
                FileStream fileStream = new FileStream(save.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("Size of chess board: "+kich_thuoc.ToString()+"x"+kich_thuoc.ToString());
                streamWriter.WriteLine("Toa do xuat phat: ( " + toa_do_X.ToString() + " , " + toa_do_Y.ToString() + " )");
                streamWriter.WriteLine("Toa do cac diem di lan luot la:");
                for (int i = 1; i < kich_thuoc * kich_thuoc; i++)
                    streamWriter.WriteLine("( " + vt[1, i].ToString() + " , " + vt[2, i].ToString() + " )");
                streamWriter.WriteLine("Ket thuc..."); 
                streamWriter.Close();
                fileStream.Close();
            }
        }

        void btnInit_Click(object sender, EventArgs e)
        {
            Init form = new Init();
            form.form = this;
            form.Show();
            form.Focus();
        }
        public void xoaNgua()
        {
            for (int i = 1; i <= kich_thuoc; i++)
            {
                for (int j = 1; j <= kich_thuoc; j++)
                {
                    banCo[i, j].Image = null;
                }
            }
        }
        
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            //vt[1,i]là tọa độ x, vt[2,i] là tọa độ y
            if (bd == 0)
            {
                bd++;
                xoaNgua();
                banCo[toa_do_X, toa_do_Y].Image = global::Ma_di_tuan.Properties.Resources.Ngua_do;
            }
            else
            {
                banCo[_X, _Y].Image = global::Ma_di_tuan.Properties.Resources.Ngua_trang;
                _X = vt[1, bd];
                _Y = vt[2, bd];
                banCo[_X, _Y].Image = global::Ma_di_tuan.Properties.Resources.Ngua_do;
                bd++;
                if (bd == kich_thuoc * kich_thuoc)//đi hết bàn cờ
                {
                    _X = toa_do_X;
                    _Y = toa_do_Y;
                    bd = 0;
                    tuyChon = false;
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    _group.btnVT_BatDau.ButtonText = "Bắt đầu";
                }
            }
        }

        private void Main_Scroll(object sender, ScrollEventArgs e)
        {
            _group.Top = 12;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        bool doi1 = false;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (bd != 0)
            {
                    if (doi1)
                    {
                        doi1 = false;
                        banCo[vt[1, bd], vt[2, bd]].Image = global::Ma_di_tuan.Properties.Resources.Ngua_do;
                    }
                    else
                    {
                        doi1 = true;
                        banCo[vt[1, bd], vt[2, bd]].Image = null;
                    }
            }
        }
    }
}