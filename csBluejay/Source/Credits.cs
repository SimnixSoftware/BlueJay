/////////////////////////////////////////////////////////////////////////////
// Credits.cs
// 
// Program Tytle:  BlueJay
// Author:  Simon Nixon
// Copyright: 2013 ©
// Description: Binary, Decimal and Hexidecimal Converter
//
// IDE: Microsoft Visual Studio 2012 Professional
// Language: C# 4.0
//
// $Id: Credits.cs 959 2013-08-23 11:39:26Z Simon $
// $URL: svn://sys2k/svnrepos/Software_Development/Projects/Visual_Studio/BlueJay/trunk/Source/Credits.cs $
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlueJay
{
    public partial class CreditsList : Form
    {
        public CreditsList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.FromArgb(192, 255, 255);
            btnClose.ForeColor = Color.FromArgb(0, 0, 64);
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Black;
            btnClose.ForeColor = Color.FromArgb(192, 255, 255);
        }
    }
}