/////////////////////////////////////////////////////////////////////////////
// AboutBox.cs
// 
// Program Tytle:  BlueJay
// Author:  Simon Nixon
// Copyright: 2013 ©
// Description: Binary, Decimal and Hexidecimal Converter
//
// IDE: Microsoft Visual Studio 2012 Professional
// Language: C# 4.0
//
// $Id: AboutBox1.cs 959 2013-08-23 11:39:26Z Simon $
// $URL: svn://sys2k/svnrepos/Software_Development/Projects/Visual_Studio/BlueJay/trunk/Source/AboutBox1.cs $
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Resources;


namespace BlueJay
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
            
            GlobalData Global = new GlobalData();
            Global.GetVersion();
             
            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            
            this.Text = String.Format("About {0}", Application.ProductName); 
            this.labelProductName.Text = AssemblyProduct;   //always displays the project Name
            this.labelVersion.Text = Global.VersionInfo;    // < from global
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Display the appropriate link based on the value of the 
            // LinkData property of the Link object.
            string target = "http://www.simnix.com";
            Process.Start(target);
        }

        private void okButton_MouseEnter(object sender, EventArgs e)
        {
            okButton.BackColor = Color.FromArgb(192, 255, 255);
            okButton.ForeColor = Color.Black;
        }

        private void okButton_MouseLeave(object sender, EventArgs e)
        {
            okButton.BackColor = Color.Black;
            okButton.ForeColor = Color.FromArgb(192, 255, 255);
        }

        private void btnCredits_Click(object sender, EventArgs e)
        {
            CreditsList Credits = new CreditsList();
            Credits.Top = this.Top;
            Credits.Left = this.Left;
            Credits.Height = this.Height;
            Credits.Width = this.Width;

            //Credits.

            Credits.ShowDialog();
        }

        private void btnCredits_MouseEnter(object sender, EventArgs e)
        {
            btnCredits.BackColor = Color.FromArgb(192, 255, 255);
            btnCredits.ForeColor = Color.Black;
        }

        private void btnCredits_MouseLeave(object sender, EventArgs e)
        {
            btnCredits.BackColor = Color.Black;
            btnCredits.ForeColor = Color.FromArgb(192, 255, 255); ;
        }

        private void AboutBox1_Load(object sender, EventArgs e)
        {

        }
    }
}
