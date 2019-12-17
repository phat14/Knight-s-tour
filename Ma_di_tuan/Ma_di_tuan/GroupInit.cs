using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Ma_di_tuan
{
    public partial class GroupInit : UserControl
    {
        
        public GroupInit()
        {
            InitializeComponent();
            
            cboTG_Cho.Items.Add("0.5");
            for (int i = 1; i <= 5; i++)
                cboTG_Cho.Items.Add(i);
            cboTG_Cho.Text = "1";
        }



        private void btnInit_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LabX_Click(object sender, EventArgs e)
        {

        }

        private void BtnVT_BatDau_Click(object sender, EventArgs e)
        {

        }

        private void BtnVT_Init_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void BtnVT_Luu_Click(object sender, EventArgs e)
        {

        }
    }
}
