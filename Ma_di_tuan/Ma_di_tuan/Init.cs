using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ma_di_tuan
{
    public partial class Init : Form
    {
        public Main form = new Main();
        public Init()
        {
            InitializeComponent();
        }
        public void KiemTraKT()
        {
            //Kiểm tra dữ liệu kích thước
            if (int.Parse(txtKT.Text) <= 7 || int.Parse(txtKT.Text) >50)
            {
                MessageBox.Show("Vui lòng nhập kích thước lớn hơn hoặc bằng 8 và nhỏ hơn hoặc bằng 50","Thông báo");
                txtKT.Focus();
            }
            else if (int.Parse(txtKT.Text) % 2 !=0)
            {
                MessageBox.Show("Vui lòng nhập kích thước là số chẵn", "Thông báo");
                txtKT.Focus();
            }
            else if (int.Parse(txtX.Text) <= 0 || int.Parse(txtX.Text) > int.Parse(txtKT.Text))
            {
                MessageBox.Show("Vui lòng nhập tọa độ X lớn hơn hoặc bằng 1 và nhỏ hơn hoặc bằng kích thước", "Thông báo");
                txtX.Focus();
            }
            else if (int.Parse(txtY.Text) <= 0 || int.Parse(txtY.Text) > int.Parse(txtKT.Text))
            {
                MessageBox.Show("Vui lòng nhập tọa độ Y lớn hơn hoặc bằng 1 và nhỏ hơn hoặc bằng kích thước", "Thông báo");
                txtY.Focus();
            }
            else
            {
                //Nếu không có lỗi từ dữ liệu nhập thì khởi tạo main
                form.kich_thuoc = int.Parse(txtKT.Text);
                form.toa_do_X = int.Parse(txtX.Text);
                form.toa_do_Y = int.Parse(txtY.Text);
                form.Show();
                form.khoiTao();
                Hide();
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            KiemTraKT();
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) KiemTraKT();
        }

        private void vistaButton1_Click(object sender, EventArgs e)
        {
            KiemTraKT();
        }

        private void vistaButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtKT_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }
}