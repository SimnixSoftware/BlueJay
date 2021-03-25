/////////////////////////////////////////////////////////////////////////////
// Main.cs
// 
// Program Tytle:  BlueJay
// Author:  Simon Nixon
// Copyright: 2013 ©
// Description: Binary, Decimal and Hexidecimal Converter/Calculator. now
//  has binary arithmetic tab and decimal to roman tabs.
//
// IDE: Microsoft Visual Studio 2019 Community edition
// Language: C# 7.0
//
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;


namespace BlueJay
{
    public partial class Main : Form
    {
        #region Local Variables...

        // these need to be declared as 64 bit variables (32 bit plus and 32 bit negative)
        // DO NOT CHANGE THE VARIABLE TYPES  
        private Int64 i64BufferHex = 0;
        private Int64 i64BinaryLimit = 128;
        private Int64 i64buffer = 0;
        private Int64 i64bAfter2sC;
        private Int64 i64NegativeBinaryLimit = -256;
        private Int64 i64BinaryLength = 8;
        private bool nonNumberEntered;
        private const int BLACK = 0;
        private const int ERROR = 1;
        private const int WARNING = 2;
        private const int MESSAGE = 3;

        enum StatusBarTextColour { NORMAL, ERROR, WARNING, MESSAGE};


        #endregion
      
        // create new classes
        Calculate Calc = new Calculate();
        GlobalData Global = new GlobalData();

public Main()
        {
            InitializeComponent();
            txtRoman_Input.CharacterCasing = CharacterCasing.Upper;
            txtHex.CharacterCasing = CharacterCasing.Upper;

            Global.DevelopmentWarning = " (DEBUG ONLY - Not for public use)";  // development message
#if DEBUG
            Global.DevelopmentModeSwitch = true;   // turns development warning on
#else
            Global.DevelopmentModeSwitch = false;   // turns development warning off
#endif
            lblErrorStatus.Text = "";
        }

        #region Application's Control Functions...

        /// <summary>
        /// Property for statusbar text
        /// </summary>
        /// <param name="text">String to appear in the statusbar</param>
        /// <param name="colour">Statusbar text colour seting</param>
        private void SetStatusbarText(string text, int colour)
        {
            switch (colour)
            {
                case 0:
                    lblErrorStatus.ForeColor = Color.Black;
                    break;
                case 1:
                    lblErrorStatus.ForeColor = Color.Red;
                    break;
                case 2:
                    lblErrorStatus.ForeColor = Color.OrangeRed;
                    break;
                case 3:
                    lblErrorStatus.ForeColor = Color.DarkGreen;
                    break;
            }

            lblErrorStatus.Text = text;
        }
        
        /// <summary>
        /// Gets the number typed and converts them to binary and hexidecimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDec_KeyUp(object sender, KeyEventArgs e)
        {
            // check if textbox is empty
            if (txtDec.Text == "")
            {
                txtBin.Text = "";
                txtHex.Text = "";
                txt2sCompliment.Text = "";
            }
            else
            {
                try
                {
                    if (Int64.Parse(txtDec.Text) < (i64BinaryLimit * 2))
                    {
                        // 2's compliment convertion
                        i64buffer = Int64.Parse(txtDec.Text);
                        if (i64buffer <= (i64BinaryLimit - 1))
                        {
                            txt2sCompliment.Text = txtDec.Text;
                        }
                        else
                        {
                            i64bAfter2sC = ~i64buffer + 1;
                            i64bAfter2sC -= i64NegativeBinaryLimit;
                            i64bAfter2sC *= -1;
                            txt2sCompliment.Text = i64bAfter2sC.ToString();
                        }


                        // display hex
                        i64BufferHex = Int64.Parse(txtDec.Text);
                        txtHex.Text = i64BufferHex.ToString("X");
                        // display binary
                        txtBin.Text = Calc.ToBinary(Convert.ToInt64(txtDec.Text));
                    }
                    else
                    {
                        //txtBin.Text = "";
                        //txtHex.Text = "";
                        //txt2sCompliment.Text = "";
                    }

                }
                catch (Exception)
                {
                    txtBin.Text = "";
                    txtHex.Text = "";
                    txt2sCompliment.Text = "";
                    SetStatusbarText("Please enter 0 to 9 only", (int)StatusBarTextColour.ERROR);
                }
            }

            if ((e.KeyValue < '0' || e.KeyValue > '9')) e.Handled = true;
        }

        /// <summary>
        /// Gets the number typed and converts them to decimal and hexidecimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBin_KeyUp(object sender, KeyEventArgs e)
        {
            Int64 buffer = 0;
            char[] szArrayLength;

            // check to see if the textbox is empty
            if (txtBin.Text == "")
            {
                txtDec.Text = "";
                txtHex.Text = "";
                txt2sCompliment.Text = "";
            }
            else
            {
                try
                {
                    szArrayLength = txtBin.Text.ToCharArray();
                    if(szArrayLength.Length <= i64BinaryLength)
                    {
                        // display decimal
                        txtDec.Text = Convert.ToInt64(Convert.ToInt64(txtBin.Text, 2)).ToString();

                        // 2's compliment convertion
                        i64buffer = Int64.Parse(txtDec.Text);   
                        if (i64buffer <= (i64BinaryLimit - 1))
                        {
                            txt2sCompliment.Text = txtDec.Text;
                        }
                        else
                        {
                            i64bAfter2sC = ~i64buffer + 1;
                            i64bAfter2sC -= i64NegativeBinaryLimit;
                            i64bAfter2sC *= -1;
                            txt2sCompliment.Text = i64bAfter2sC.ToString();
                        }

                        // display hex
                        buffer = Int64.Parse(txtDec.Text);
                        txtHex.Text = buffer.ToString("X"); 
                    }
                    else
                    {
                        //txtDec.Text = "";
                        //txtHex.Text = "";
                        //txt2sCompliment.Text = "";
                    }
                }
                catch (Exception)
                {
                    txtDec.Text = "";
                    txtHex.Text = "";
                    txt2sCompliment.Text = "";
                    SetStatusbarText("Please enter 0's or 1's only", (int)StatusBarTextColour.ERROR);
                }
            }
        }

        /// <summary>
        /// Gets the number typed and converts them to decimal and binary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHex_KeyUp(object sender, KeyEventArgs e)
        {
            // Check to see if textbox is empty
            if (txtHex.Text == "")
            {
                txtDec.Text = "";
                txtBin.Text = "";
                txt2sCompliment.Text = "";
            }
            else
            {
                try
                {
                    // convert to decimal
                    Int64 i64decAgain = 
                        Int64.Parse(txtHex.Text, System.Globalization.NumberStyles.HexNumber);
                    // display decimal
                    txtDec.Text = i64decAgain.ToString();

                    if (i64decAgain < (i64BinaryLimit * 2))
                    {
                        // 2's compliment convertion
                        i64buffer = Int64.Parse(txtDec.Text);
                        if (i64buffer <= (i64BinaryLimit - 1))
                        {
                            txt2sCompliment.Text = txtDec.Text;
                        }
                        else
                        {
                            i64bAfter2sC = ~i64buffer + 1;
                            i64bAfter2sC -= i64NegativeBinaryLimit;
                            i64bAfter2sC *= -1;
                            txt2sCompliment.Text = i64bAfter2sC.ToString();
                        }

                        // display binary
                        txtBin.Text = Calc.ToBinary(Convert.ToInt64(txtDec.Text)); 
                    }
                    else
                    {
                        //txtDec.Text = "";
                        //txtBin.Text = "";
                        //txt2sCompliment.Text = "";
                    }
                }
                catch (Exception)
                {
                    txtDec.Text = "";
                    txtBin.Text = "";
                    txt2sCompliment.Text = "";
                    SetStatusbarText("Please enter 0 to 9 & A to F only", (int)StatusBarTextColour.ERROR);
                }
            }
        }

        /// <summary>
        /// Clears all numeric textbox's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDec_Enter(object sender, EventArgs e)
        {
            //txtDec.Text = "";
            //txtBin.Text = "";
            //txtHex.Text = "";
            //txt2sCompliment.Text = "";
        }

        /// <summary>
        /// Clears all numeric textbox's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBin_Enter(object sender, EventArgs e)
        {
            //txtDec.Text = "";
            //txtBin.Text = "";
            //txtHex.Text = "";
            //txt2sCompliment.Text = "";
        }

        /// <summary>
        /// Clears all numeric textbox's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHex_Enter(object sender, EventArgs e)
        {
            //txtDec.Text = "";
            //txtBin.Text = "";
            //txtHex.Text = "";
            //txt2sCompliment.Text = "";
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }    

        /// <summary>
        /// Shows the About box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 About = new AboutBox1();
            About.Height = (tabControl1.Height + 72);
            About.Width = (tabControl1.Width + 24);
            About.Top = (this.Top + 25);
            About.Left = (this.Left + 3);
            About.ShowDialog();
        }

        /// <summary>
        /// Shows the help file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void f1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo myProc = new System.Diagnostics.ProcessStartInfo();
            myProc.FileName = ".\\help.chm";
            myProc.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
            Process.Start(myProc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            if (e.KeyCode == Keys.F1)
            {
                ProcessStartInfo myProc = new System.Diagnostics.ProcessStartInfo();
                myProc.FileName = ".\\help.chm";
                myProc.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                Process.Start(myProc);
                nonNumberEntered = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName;

            // displays development message title bar if development switch is 'on'
            if (Global.DevelopmentModeSwitch == true)
			{
                this.Text = Application.ProductName + Global.DevelopmentWarning;
			}

            i64BinaryLength = 32;
            i64BinaryLimit = 2147483648;
            i64NegativeBinaryLimit = -4294967296;

            // tab 1
            txtDec.MaxLength = 10;
            txtBin.MaxLength = 32;
            txtHex.MaxLength = 8;
            // tab 2
            txtBinSource1.MaxLength = 32;
            txtBinSource2.MaxLength = 32;
            BinaryLimit_32bit_ToolStripMenuItem.Checked = true;
            lblBinaryLimit.Text = "Binary Limit: 32 bit";

            // hide the bit shift controls
            cboBitShiftNumber.Hide();
            cboBitShift_LR.Hide();
            lblBitShift.Hide();
            //btnCalculate.Hide();

            if (!txtDec.Focused)
            {
                txtDec.Focus();
            }
        }

        /// <summary>
        /// calculates the answer for two binery numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBinaryCalculate_Click(object sender, EventArgs e)
        {
            Int64 BinaryBuffer1_32;
            Int64 BinaryBuffer2_32;
            //direction indicators
            byte Right = 0;
            byte Left = 1;

            if (cbxBitShiftEnabled.Checked)
            {
                // clear textbox
                txtBinaryAnswer.Text = "";

                if (cboBitShift_LR.SelectedIndex == Right)
                {
                    try
                    {
                        BinaryBuffer1_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource1.Text, 2));
                        txtBinSource2.Text = 
                            Calc.ToBinary(BinaryBuffer1_32 >> Int32.Parse(cboBitShiftNumber.Text));
                        BinaryBuffer2_32 = Int64.Parse(txtBinSource2.Text);
                    }
                    catch (Exception)
                    {
                        SetStatusbarText("Binary 1 cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                    }
                }
                else if (cboBitShift_LR.SelectedIndex == Left)
                {
                    try
                    {
                        BinaryBuffer1_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource1.Text, 2));
                        txtBinSource2.Text = Calc.ToBinary(BinaryBuffer1_32 << Int32.Parse(cboBitShiftNumber.Text));
                        BinaryBuffer2_32 = Int64.Parse(txtBinSource2.Text);
                    }
                    catch (Exception)
                    {
                        SetStatusbarText("Binary 1 cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                    }
                }
            }

            if ((cbxAdd.Checked == true))
            {
                try
                {
                    BinaryBuffer1_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource1.Text, 2));
                    BinaryBuffer2_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource2.Text, 2));


                    txtBinaryAnswer.Text = Calc.ToBinary(BinaryBuffer1_32 + BinaryBuffer2_32);
                }
                catch (Exception)
                {
                    SetStatusbarText("Binary 1 and Binary 2 boxes cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                    if (txtBinSource1.TextLength == 0)
                    {
                        SetStatusbarText("Binary 1 box cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                        txtBinSource1.BackColor = Color.Red;
                        txtBinSource1.ForeColor = Color.White;
                    }
                    
                    if (txtBinSource2.TextLength == 0)
                    {
                        SetStatusbarText("Binary 2 box cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                        txtBinSource2.BackColor = Color.Red;
                        txtBinSource2.ForeColor = Color.White;
                    }
                }
            }

            
            if ((cbxSubtract.Checked == true) && (txtBinSource1.TextLength > 0))
            {
                try
                {
                    BinaryBuffer1_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource1.Text, 2));
                    BinaryBuffer2_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource2.Text, 2));
                    
                    txtBinaryAnswer.Text = Calc.ToBinary(BinaryBuffer1_32 - BinaryBuffer2_32);
                }
                catch (Exception)
                {
                    SetStatusbarText("Binary 1 and Binary 2 boxes cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                    if (txtBinSource1.TextLength == 0)
                    {
                        txtBinSource1.BackColor = Color.Red;
                        txtBinSource1.ForeColor = Color.White;
                    }

                    if (txtBinSource2.TextLength == 0)
                    {
                        txtBinSource2.BackColor = Color.Red;
                        txtBinSource2.ForeColor = Color.White;
                    }
                }
            }

            if ((cbxDivide.Checked == true) && (txtBinSource1.TextLength > 0))
            {
                try
                {
                    BinaryBuffer1_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource1.Text, 2));
                    BinaryBuffer2_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource2.Text, 2));
                    
                    txtBinaryAnswer.Text = Calc.ToBinary(BinaryBuffer1_32 / BinaryBuffer2_32);
                }
                catch (Exception)
                {
                    SetStatusbarText("Binary 1 and Binary 2 boxes cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                    if (txtBinSource1.TextLength == 0)
                    {
                        txtBinSource1.BackColor = Color.Red;
                        txtBinSource1.ForeColor = Color.White;
                    }

                    if (txtBinSource2.TextLength == 0)
                    {
                        txtBinSource2.BackColor = Color.Red;
                        txtBinSource2.ForeColor = Color.White;
                    }
                }  
            }

            if ((cbxMultiply.Checked == true) && (txtBinSource1.TextLength > 0))
            {
                try
                {
                    BinaryBuffer1_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource1.Text, 2));
                    BinaryBuffer2_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource2.Text, 2));
                    
                    txtBinaryAnswer.Text = Calc.ToBinary(BinaryBuffer1_32 * BinaryBuffer2_32);
                }
                catch (Exception)
                {
                    SetStatusbarText("Binary 1 and Binary 2 boxes cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                    if (txtBinSource1.TextLength == 0)
                    {
                        txtBinSource1.BackColor = Color.Red;
                        txtBinSource1.ForeColor = Color.White;
                    }

                    if (txtBinSource2.TextLength == 0)
                    {
                        txtBinSource2.BackColor = Color.Red;
                        txtBinSource2.ForeColor = Color.White;
                    }
                }
            }
            
            if (cbxXOR.Checked == true)
            {
                try
                {
                    BinaryBuffer1_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource1.Text, 2));
                    BinaryBuffer2_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource2.Text, 2));
                    
                    txtBinaryAnswer.Text = 
                        Calc.ToBinary(BinaryBuffer1_32 ^ BinaryBuffer2_32).ToString();
                }
                catch (Exception)
                {
                    SetStatusbarText("Binary 1 and Binary 2 boxes cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                    if (txtBinSource1.TextLength == 0)
                    {
                        txtBinSource1.BackColor = Color.Red;
                        txtBinSource1.ForeColor = Color.White;
                    }

                    if (txtBinSource2.TextLength == 0)
                    {
                        txtBinSource2.BackColor = Color.Red;
                        txtBinSource2.ForeColor = Color.White;
                    }
                } 
            }
            
            if (cbxAND.Checked == true)
            {
                try
                {
                    BinaryBuffer1_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource1.Text, 2));
                    BinaryBuffer2_32 = Convert.ToInt32(Convert.ToInt32(txtBinSource2.Text, 2));
                    
                    if((Calc.ToBinary(BinaryBuffer1_32 & BinaryBuffer2_32).ToString() == "" ) || 
                        (Calc.ToBinary(BinaryBuffer1_32 & BinaryBuffer2_32).ToString() == "0"))
                    {
                        txtBinaryAnswer.Text = "0";
                    }
                    else
                    {
                        txtBinaryAnswer.Text = 
                            Calc.ToBinary(BinaryBuffer1_32 & BinaryBuffer2_32).ToString();
                    }
                }
                catch (Exception)
                {
                    SetStatusbarText("Binary 1 and Binary 2 boxes cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                    if (txtBinSource1.TextLength == 0)
                    {
                        txtBinSource1.BackColor = Color.Red;
                        txtBinSource1.ForeColor = Color.White;
                    }

                    if (txtBinSource2.TextLength == 0)
                    {
                        txtBinSource2.BackColor = Color.Red;
                        txtBinSource2.ForeColor = Color.White;
                    }
                } 
            }

            if (cbxOR.Checked == true)
            {
                try
                {
                    BinaryBuffer1_32 = 
                        Convert.ToInt32(Convert.ToInt32(txtBinSource1.Text, 2));
                    BinaryBuffer2_32 = 
                        Convert.ToInt32(Convert.ToInt32(txtBinSource2.Text, 2));
                    
                    txtBinaryAnswer.Text = 
                        Calc.ToBinary(BinaryBuffer1_32 | BinaryBuffer2_32).ToString();
                }
                catch (Exception)
                {
                    SetStatusbarText("Binary 1 and Binary 2 boxes cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                    if (txtBinSource1.TextLength == 0)
                    {
                        txtBinSource1.BackColor = Color.Red;
                        txtBinSource1.ForeColor = Color.White;
                    }

                    if (txtBinSource2.TextLength == 0)
                    {
                        txtBinSource2.BackColor = Color.Red;
                        txtBinSource2.ForeColor = Color.White;
                    }
                }
            }

            try
            {
                txtdec2bin1_tab2.Text = 
                    Convert.ToInt64(Convert.ToInt64(txtBinSource1.Text, 2)).ToString();
                txtdec2bin2_tab2.Text = 
                    Convert.ToInt64(Convert.ToInt64(txtBinSource2.Text, 2)).ToString();
                txtAnswerBinary2Decimal.Text = 
                    Convert.ToInt64(Convert.ToInt64(txtBinaryAnswer.Text, 2)).ToString();
            }
            catch (Exception)
            {
                
            }

            if (cbxJOIN.Checked == true)
            {
                string sBinary1 = "", sBinary2 = "", sBinaryJoined = "";

                try
                {
                    sBinary1 = txtBinSource1.Text;
                    sBinary2 = txtBinSource2.Text;
                    sBinaryJoined = sBinary1 + sBinary2;
                    txtBinaryAnswer.Text = sBinaryJoined;
                }
                catch (Exception)
                {
                    SetStatusbarText("Binary 1 and Binary 2 boxes cannot be EMPTY", (int)StatusBarTextColour.ERROR);
                    if (txtBinSource1.TextLength == 0)
                    {
                        txtBinSource1.BackColor = Color.Red;
                        txtBinSource1.ForeColor = Color.White;
                    }

                    if (txtBinSource2.TextLength == 0)
                    {
                        txtBinSource2.BackColor = Color.Red;
                        txtBinSource2.ForeColor = Color.White;
                    }
                }
            }

            try
            {
                txtdec2bin1_tab2.Text =
                    Convert.ToInt64(Convert.ToInt64(txtBinSource1.Text, 2)).ToString();
                txtdec2bin2_tab2.Text =
                    Convert.ToInt64(Convert.ToInt64(txtBinSource2.Text, 2)).ToString();
                txtAnswerBinary2Decimal.Text =
                    Convert.ToInt64(Convert.ToInt64(txtBinaryAnswer.Text, 2)).ToString();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Allows only '0' and '1' to be entered into the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBinSource1_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = 
                System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if ((e.KeyChar == '1') || (e.KeyChar == '0'))
            {
                // Digits are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        /// <summary>
        /// Allows only '0' and '1' to be entered into the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBinSource2_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = 
                System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if ((e.KeyChar == '1') || (e.KeyChar == '0'))
            {
                // Digits are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        /// <summary>
        /// Add's two binary numbers together
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxAdd_Click(object sender, EventArgs e)
        {
            if (cbxAdd.Checked == false)
            {
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxAND.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = false;
            }
            else if (cbxAdd.Checked == true)
            {
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxAND.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = true;
            }
        }

        /// <summary>
        /// Subtract's two binary numbers together
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxSubtract_Click(object sender, EventArgs e)
        {
            if (cbxSubtract.Checked == false)
            {
                cbxAdd.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxAND.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = false;
            }
            else if (cbxSubtract.Checked == true) 
            {
                cbxAdd.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxAND.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = true;
            }
        }

        /// <summary>
        /// Divide's two binary numbers together
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDivide_Click(object sender, EventArgs e)
        {
            if (cbxDivide.Checked == false)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxMultiply.Checked = false;
                cbxAND.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = false;
            }
            else if (cbxDivide.Checked == true)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxMultiply.Checked = false;
                cbxAND.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = true;
            }
            
        }
        
        /// <summary>
        /// Multiply's two binary numbers together
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxMultiply_Click(object sender, EventArgs e)
        {
            if (cbxMultiply.Checked == false)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxAND.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = false;
            }
            else if (cbxMultiply.Checked == true)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxAND.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = true;
            }
        }

        /// <summary>
        /// Exclusive OR's two binary numbers together
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxXOR_Click(object sender, EventArgs e)
        {
            if (cbxXOR.Checked == false)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxOR.Checked = false;
                cbxAND.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = false;
            }
            else if (cbxXOR.Checked == true)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxOR.Checked = false;
                cbxAND.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = true;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxOR_Click(object sender, EventArgs e)
        {
            if (cbxOR.Checked == false)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxXOR.Checked = false;
                cbxAND.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = false;
            }
            else if (cbxOR.Checked == true)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxXOR.Checked = false;
                cbxAND.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = true;
            }
            
        }

        /// <summary>
        /// And's two binary numbers together
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxAND_Click(object sender, EventArgs e)
        {
            if (cbxAND.Checked == false)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = false;
            }
            else if (cbxAND.Checked == true)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxJOIN.Checked = false;
                btnBinary_Calculate.Enabled = true;
            }
           
        }

        private void cbxJOIN_Click(object sender, EventArgs e)
        {
            if (cbxJOIN.Checked == false)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxAND.Checked = false;
                btnBinary_Calculate.Enabled = false;
            }
            else if (cbxJOIN.Checked == true)
            {
                cbxAdd.Checked = false;
                cbxSubtract.Checked = false;
                cbxDivide.Checked = false;
                cbxMultiply.Checked = false;
                cbxXOR.Checked = false;
                cbxOR.Checked = false;
                cbxAND.Checked = false;
                btnBinary_Calculate.Enabled = true;
            }
        }
        /// <summary>
        /// Allow's only numbers to be entered into the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDec_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            
            NumberFormatInfo numberFormatInfo = 
                System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
            
        }
        
        /// <summary>
        /// Allow's only '0' and '1' to be entered into the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBin_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = 
                System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            //string keyInput = e.KeyChar.ToString();

            if ((e.KeyChar == '1') || (e.KeyChar == '0'))
            {
                // Digits are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        /// <summary>
        /// Allow's only numbers and A to F to be entered into the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHex_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = 
                System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            //string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if ((e.KeyChar == 'a') || (e.KeyChar == 'b'))
            {
                // Charactors are OK
            }
            else if ((e.KeyChar == 'c') || (e.KeyChar == 'd'))
            {
                // Charactors are OK
            }
            else if ((e.KeyChar == 'e') || (e.KeyChar == 'f'))
            {
                // Charactors are OK
            }
            else if ((e.KeyChar == 'A') || (e.KeyChar == 'B'))
            {
                // Charactors are OK
            }
            else if ((e.KeyChar == 'C') || (e.KeyChar == 'D'))
            {
                // Charactors are OK
            }
            else if ((e.KeyChar == 'E') || (e.KeyChar == 'F'))
            {
                // Charactors are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        /// <summary>
        /// Show's or Hide's the BIT shifting controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxBitShiftEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxBitShiftEnabled.Checked == true)
            {
                cboBitShiftNumber.Show();
                cboBitShift_LR.Show();
                lblBitShift.Show();
                //btnBinary_Calculate.Show();
            }
            else
            {
                cboBitShiftNumber.Hide();
                cboBitShift_LR.Hide();
                lblBitShift.Hide();
                //btnBinary_Calculate.Hide();
            }
        }

        /// <summary>
        /// Set's all control's to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtBinSource1.Text = "";
            txtBinSource2.Text = "";
            txtBinaryAnswer.Text = "";
            txtdec2bin1_tab2.Text = "";
            txtdec2bin2_tab2.Text = "";
            txtAnswerBinary2Decimal.Text = "";

            
            cbxAdd.Checked = false;
            cbxSubtract.Checked = false;
            cbxDivide.Checked = false;
            cbxMultiply.Checked = false;
            cbxXOR.Checked = false;
            cbxOR.Checked = false;
            cbxAND.Checked = false;
            cbxJOIN.Checked = false;
            cbxBitShiftEnabled.Checked = false;

            cboBitShiftNumber.Hide();
            cboBitShift_LR.Hide();
            lblBitShift.Hide();

            txtBinSource1.BackColor = Color.FromArgb(224, 224, 224);
            txtBinSource1.ForeColor = Color.Navy;
            txtBinSource2.BackColor = Color.FromArgb(224, 224, 224);
            txtBinSource2.ForeColor = Color.Navy;

            lblErrorStatus.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BinaryLimit_8bit_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           i64BinaryLength = 8;
           i64BinaryLimit = 128;
           i64NegativeBinaryLimit = -256;

           txtDec.Text = "";
           txtBin.Text = "";
           txtHex.Text = "";
           txt2sCompliment.Text = "";

           // tab 1
           txtDec.MaxLength = 3;
           txtBin.MaxLength = 8;
           txtHex.MaxLength = 2;
           // tab 2
           txtBinSource1.MaxLength = 8;
           txtBinSource2.MaxLength = 8;
            

           if (i64BinaryLimit == 128)
           {
               lblBinaryLimit.Text = "Binary Limit: 8 bit";
               BinaryLimit_8bit_ToolStripMenuItem.Checked = true;
               BinaryLimit_16bit_ToolStripMenuItem.Checked = false;
               BinaryLimit_32bit_ToolStripMenuItem.Checked = false;
           }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BinaryLimit_16bit_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           i64BinaryLength = 16;
           i64BinaryLimit = 32768;
           i64NegativeBinaryLimit = -65536;

           txtDec.Text = "";
           txtBin.Text = "";
           txtHex.Text = "";
           txt2sCompliment.Text = "";

           // tab 1
           txtDec.MaxLength = 5;
           txtBin.MaxLength = 16;
           txtHex.MaxLength = 4;
           // tab 2
           txtBinSource1.MaxLength = 16;
           txtBinSource2.MaxLength = 16;

           if (i64BinaryLimit == 32768)
           {
              lblBinaryLimit.Text = "Binary Limit: 16 bit";
               BinaryLimit_8bit_ToolStripMenuItem.Checked = false;
               BinaryLimit_16bit_ToolStripMenuItem.Checked = true;
               BinaryLimit_32bit_ToolStripMenuItem.Checked = false;
           }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BinaryLimit_32bit_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        	i64BinaryLength = 32;
            i64BinaryLimit = 2147483648;
            i64NegativeBinaryLimit = -4294967296;

            txtDec.Text = "";
            txtBin.Text = "";
            txtHex.Text = "";
            txt2sCompliment.Text = "";

            // tab 1
            txtDec.MaxLength = 10;
            txtBin.MaxLength = 32;
            txtHex.MaxLength = 8;
            // tab 2
            txtBinSource1.MaxLength = 32;
            txtBinSource2.MaxLength = 32;

            if (i64BinaryLimit == 2147483648)
            {
                lblBinaryLimit.Text = "Binary Limit: 32 bit";
                BinaryLimit_8bit_ToolStripMenuItem.Checked = false;
                BinaryLimit_16bit_ToolStripMenuItem.Checked = false;
                BinaryLimit_32bit_ToolStripMenuItem.Checked = true;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtdec2bin1_tab2_KeyUp(object sender, KeyEventArgs e)
        {
            // check if textbox is empty
            if (txtdec2bin1_tab2.Text == "")
            {
               
            }
            else
            {
                try
                {
                    if (Int64.Parse(txtdec2bin1_tab2.Text) < (i64BinaryLimit * 2))
                    {
                        // display binary
                        txtBinSource1.Text = Calc.ToBinary(Convert.ToInt64(txtdec2bin1_tab2.Text));
                        
                    }
                    else
                    {
                        txtBinSource1.Text = "";
                    }

                }
                catch (Exception)
                {
                    txtBinSource1.Text = "";
                    SetStatusbarText("Please enter 0 to 9 only", (int)StatusBarTextColour.ERROR);
                }
            }

            if ((e.KeyValue < '0' || e.KeyValue > '9')) e.Handled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtdec2bin1_tab2_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = 
                System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtdec2bin2_tab2_KeyUp(object sender, KeyEventArgs e)
        {
            // check if textbox is empty
            if (txtdec2bin2_tab2.Text == "")
            {

            }
            else
            {
                try
                {
                    if (Int64.Parse(txtdec2bin2_tab2.Text) < (i64BinaryLimit * 2))
                    {
                        // display binary
                        txtBinSource2.Text = Calc.ToBinary(Convert.ToInt64(txtdec2bin2_tab2.Text));

                    }
                    else
                    {
                        txtBinSource2.Text = "";
                    }

                }
                catch (Exception)
                {
                    txtBinSource1.Text = "";
                    SetStatusbarText("Please enter 0 to 9 only", (int)StatusBarTextColour.ERROR);
                }
            }

            if ((e.KeyValue < '0' || e.KeyValue > '9')) e.Handled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtdec2bin2_tab2_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = 
                System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetValues_Click(object sender, EventArgs e)
        {
            txtBinSource1.Text = "";
            txtBinSource2.Text = "";
            txtdec2bin1_tab2.Text = "";
            txtdec2bin2_tab2.Text = "";
            txtBinaryAnswer.Text = "";
            txtAnswerBinary2Decimal.Text = "";

            txtBinSource1.BackColor = Color.FromArgb(224, 224, 224);
            txtBinSource1.ForeColor = Color.Navy;
            txtBinSource2.BackColor = Color.FromArgb(224, 224, 224);
            txtBinSource2.ForeColor = Color.Navy;

            lblErrorStatus.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRoman_Calculate_Click(object sender, EventArgs e)
        {
            bool containsLetter = false;    // flag for letter
            string Number = txtRoman_Input.Text.Trim();

            for (int i = 0; i < Number.Length; i++)
            {
                if (!char.IsNumber(Number[i]))
                {
                    containsLetter = true;
                }
            }

            string szBuffer;
            // check if input textbox contains roman numerals or integer
            if (containsLetter)
            {
                try
                {
                    // contains roman numerals 
                    szBuffer = Calc.RomanToNumber(txtRoman_Input.Text).ToString();
                    if (szBuffer == "-1")
                    {
                        txtRoman_Input.Text = "";
                        txtRoman_Result.Text = "";
                    }
                    else
                    {
                        txtRoman_Result.Text = szBuffer;
                        SetStatusbarText("", (int)StatusBarTextColour.NORMAL);
                    }
                    txtRoman_Input.Focus();
                }
                catch (Exception)
                {
                    SetStatusbarText("INPUT box cannot be empty", (int)StatusBarTextColour.ERROR);
                } 
            }
            else
            {
                try
                {
                    // contains an interger number
                    szBuffer = Calc.NumberToRoman(int.Parse(txtRoman_Input.Text));
                    if (szBuffer == "-1")
                    {
                        txtRoman_Input.Text = "";
                        txtRoman_Result.Text = "";
                    }
                    else
                    {
                        txtRoman_Result.Text = szBuffer;
                        SetStatusbarText("", (int)StatusBarTextColour.NORMAL);
                    }
                    txtRoman_Input.Focus();
                }
                catch (Exception)
                {
                    SetStatusbarText("INPUT box cannot be empty", (int)StatusBarTextColour.ERROR);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRoman_Clear_Click(object sender, EventArgs e)
        {
            txtRoman_Input.Text = "";
            txtRoman_Result.Text = "";
            txtRoman_Input.Focus();
            SetStatusbarText("", (int)StatusBarTextColour.NORMAL);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRoman_Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo =
                System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            //string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if ((e.KeyChar == 'I') || (e.KeyChar == 'i'))
            {
                // Charactors are OK
            }
            else if ((e.KeyChar == 'V') || (e.KeyChar == 'v'))
            {
                // Charactors are OK
            }
            else if ((e.KeyChar == 'X') || (e.KeyChar == 'x'))
            {
                // Charactors are OK
            }
            else if ((e.KeyChar == 'L') || (e.KeyChar == 'l'))
            {
                // Charactors are OK
            }
            else if ((e.KeyChar == 'C') || (e.KeyChar == 'c'))
            {
                // Charactors are OK
            }
            else if ((e.KeyChar == 'D') || (e.KeyChar == 'd'))
            {
                // Charactors are OK
            }
            else if ((e.KeyChar == 'M') || (e.KeyChar == 'm'))
            {
                // Charactors are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabBaseConversion)
            {
                txtDec.Focus();
            }

            if (tabControl1.SelectedTab == tabBinaryArithmetic)
            {
                txtBinSource1.Focus();
            }

            if (tabControl1.SelectedTab == tabRoman)
            {
                txtRoman_Input.Focus();
            }
        }

        /// <summary>
        /// Checks if the value entered is more than or equal to the binary limit,
        ///  if the value entered is grater than the binary limit, then it does not accept</summary>
        ///  the charactor.<param name="sender"></param>
        /// <param name="e"></param>
        private void txtDec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Int64.Parse(txtDec.Text) >= (i64BinaryLimit * 2))
                {
                    //removes the first number from the testbox
                    txtDec.Text = i64buffer.ToString().Remove((i64buffer.ToString().Length - 1), 0);
                    //moves the currser to the beginning of the textbox
                    txtDec.Select(txtDec.Text.Length, 0);
                    //status message
                    SetStatusbarText("INPUT cannot be grater than the binary limit", (int)StatusBarTextColour.ERROR);
                }
            }
            catch (Exception)
            {
                
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyToClipboard_Decimal_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();

            if (txtDec.Text == "")
            {
                SetStatusbarText("ERROR: the 'Decimal' textbox cannot be empty", (int)StatusBarTextColour.ERROR);
                txtDec.Focus();
            }
            else
            { 
                Clipboard.SetText(txtDec.Text);
                if (Clipboard.ContainsText(TextDataFormat.Text))
                {
                   SetStatusbarText("Copied to clipboard: successful", (int)StatusBarTextColour.MESSAGE);
                   txtDec.Focus();
                }
                else
                {
                    SetStatusbarText("Copied to clipboard: not successful", (int)StatusBarTextColour.ERROR);
                   txtDec.Focus();
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyToClipboard_Binary_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();

            if (txtBin.Text == "")
            {
                SetStatusbarText("ERROR: the 'Binary' textbox cannot be empty", (int)StatusBarTextColour.ERROR);
                txtBin.Focus();
            }
            else
            {
                Clipboard.SetText(txtBin.Text);
                if (Clipboard.ContainsText(TextDataFormat.Text))
                {
                    SetStatusbarText("Copied to clipboard: successful", (int)StatusBarTextColour.MESSAGE);
                    txtBin.Focus();
                    txtBin.Select(txtBin.Text.Length, 0);
                }
                else
                {
                    SetStatusbarText("Copied to clipboard: not successful", (int)StatusBarTextColour.ERROR);
                    txtBin.Focus();
                    txtBin.Select(txtBin.Text.Length, 0);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyToClipboard_Hex_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();

            if (txtHex.Text == "")
            {
                SetStatusbarText("ERROR: the 'Hexadecimal' textbox cannot be empty", (int)StatusBarTextColour.ERROR);
                txtHex.Focus();
            }
            else
            {
                Clipboard.SetText(txtHex.Text);
                if (Clipboard.ContainsText(TextDataFormat.Text))
                {
                    SetStatusbarText("Copied to clipboard: successful", (int)StatusBarTextColour.MESSAGE);
                    txtHex.Focus();
                }
                else
                {
                    SetStatusbarText("Copied to clipboard: not successful", (int)StatusBarTextColour.ERROR);
                    txtHex.Focus();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyToClipboard_2sCompliment_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();

            if (txt2sCompliment.Text == "")
            {
                SetStatusbarText("ERROR: the '2's Compliment' textbox cannot be empty", (int)StatusBarTextColour.ERROR);
                txtDec.Focus();
            }
            else
            {
                Clipboard.SetText(txt2sCompliment.Text);
                if (Clipboard.ContainsText(TextDataFormat.Text))
                {
                    SetStatusbarText("Copied to clipboard: successful", (int)StatusBarTextColour.MESSAGE);
                    txtDec.Focus();
                }
                else
                {
                    SetStatusbarText("Copied to clipboard: not successful", (int)StatusBarTextColour.ERROR);
                    txtDec.Focus();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaseConversionClear_Click(object sender, EventArgs e)
        {
            txtDec.Text = "";
            txtBin.Text = "";
            txtHex.Text = "";
            txt2sCompliment.Text = "";

            SetStatusbarText("", (int)StatusBarTextColour.NORMAL);
            txtDec.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRoman_Input_TextChanged(object sender, EventArgs e)
        {
            if (txtRoman_Input.Text == "")
            {
                btnRoman_Calculate.Enabled = false;
            }
            else
            {
                btnRoman_Calculate.Enabled = true;
            }
        }

        #endregion

        #region Mass

        private void btnMassCalculate_Click(object sender, EventArgs e)
        {   
            int iFrom = cboMassFrom.SelectedIndex;
            int iTo = cboMassTo.SelectedIndex;
            int iRound = 8;
            int iThousand = 1000;
            int iMillion = 1000000;
            int iThouMill = 1000000000;


            switch (iFrom)
            {
                case 0: //from milligrams
                    switch (iTo)
                    {
                        case 0: //to milligrams
                            txtMassResults.Text = txtMassInput.Text;
                            break;
                        case 1: //to grams
                            txtMassResults.Text = (Math.Round(float.Parse(txtMassInput.Text) / iThousand, iRound)).ToString();
                            break;
                        case 2: //to kilograms
                            txtMassResults.Text = (Math.Round(float.Parse(txtMassInput.Text) / iMillion, iRound)).ToString();
                            break;
                        case 3: //to metric ton (tonne)
                            txtMassResults.Text = (Math.Round(float.Parse(txtMassInput.Text) / iThouMill, iRound)).ToString();
                            break;
                        case 4: //to ounce
                            txtMassResults.Text = (Math.Round(double.Parse(txtMassInput.Text) * 0.00003527, iRound)).ToString();
                            break;
                    }
                        break;
                case 1: //from grams

                    break;
            }
            
        }

        private void btnMassClear_Click(object sender, EventArgs e)
        {
            cboMassFrom.Text = "Select from";
            txtMassInput.Text = "";
            cboMassTo.Text = "Select To";
            txtMassResults.Text = "";
        }


        #endregion
    }
}