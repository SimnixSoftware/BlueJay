/////////////////////////////////////////////////////////////////////////////
// Calc.cs
// 
// Program Tytle:  BlueJay
// Author:  Simon Nixon
// Copyright: 2013 ©
// Description: Binary, Decimal and Hexidecimal Converter
//
// IDE: Microsoft Visual Studio 2012 Professional
// Language: C# 4.0
//
// $Id: Calculate.cs 959 2013-08-23 11:39:26Z Simon $
// $URL: svn://sys2k/svnrepos/Software_Development/Projects/Visual_Studio/BlueJay/trunk/Source/Calculate.cs $
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BlueJay
{
    public class Calculate
    {
        /// <summary>
        /// 
        /// </summary>
        private enum RomanDigit
        {
            I = 1,
            V = 5,
            X = 10,
            L = 50,
            C = 100,
            D = 500,
            M = 1000
        }

        /// <summary>
        /// converts a decimal numbers to binary
        /// </summary>
        /// <param name="Decimal">takes in a decimal number</param>
        /// <returns>returns a string of 1's and 0's</returns>
        public string ToBinary(Int64 Decimal)
        {
            // Declare a few variables we're going to need
            Int64 BinaryHolder;
            char[] BinaryArray;
            string BinaryResult = "";

            //this converts the decimal number to binary
            while (Decimal > 0)
            {
                BinaryHolder = Decimal % 2;
                BinaryResult += BinaryHolder;
                Decimal /= 2;
            }

            // The algoritm gives us the binary number in reverse order (mirrored)
            // We store it in an array so that we can reverse it back to normal
            BinaryArray = BinaryResult.ToCharArray();
            Array.Reverse(BinaryArray);
            BinaryResult = new string(BinaryArray);
            
            //returning answer
            return BinaryResult;
        }

        /// <summary>
        /// Converts an integer value into Roman numerals
        /// </summary>
        /// <param name="number">Variable used to hold an integer value</param>
        /// <returns>Roman Numerals</returns>
        public string NumberToRoman(int number)
        {
            // Validate
            if (number < 0 || number > 3999)
            {
                MessageBox.Show("Value must be in the range 0 - 3,999.");
                return "Error";
            }

            if (number == 0) return "N";

            // Set up key numerals and numeral pairs
            int[] values = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 
                                        9, 5, 4, 1 };
            string[] numerals = new string[] { "M", "CM", "D", "CD", "C", "XC", 
                                            "L", "XL", "X", "IX", "V", "IV", "I" };

            // Initialise the string builder
            StringBuilder result = new StringBuilder();

            // Loop through each of the values to diminish the number
            for (int i = 0; i < 13; i++)
            {
                // If the number being converted is less than the test value, append
                // the corresponding numeral or numeral pair to the resultant string
                while (number >= values[i])
                {
                    number -= values[i];
                    result.Append(numerals[i]);
                }
            }

            // Done
            return result.ToString();
        }

        /// <summary>
        /// Converts a Roman numerals value into an integer
        /// </summary>
        /// <param name="roman">string used to hold roman numerals</param>
        /// <returns>Integer value of the roman numerals</returns>
        public int RomanToNumber(string roman)
        {
            // Rule 7
            roman = roman.ToUpper().Trim();
            if (roman == "N") return 0;

            // Rule 4
            if (roman.Split('V').Length > 2 ||
                roman.Split('L').Length > 2 ||
                roman.Split('D').Length > 2)
            {
                MessageBox.Show("Invalid duplicate numeral!");
                return -1;
            }

            // Rule 1
            int count = 1;
            char last = 'Z';
            foreach (char numeral in roman)
            {
                // Valid character?
                if ("IVXLCDM".IndexOf(numeral) == -1)
                {
                    MessageBox.Show("Invalid numeral!");
                    return -1;
                    //break;
                }

                // Duplicate?
                if (numeral == last)
                {
                    count++;
                    if (count == 4)
                    {
                        MessageBox.Show("Repetition of roman numeral, >3");
                        return -1;
                        //break;
                    }
                }
                else
                {
                    count = 1;
                    last = numeral;
                }
            }

            // Create an ArrayList containing the values
            int ptr = 0;
            ArrayList values = new ArrayList();
            int maxDigit = 1000;
            while (ptr < roman.Length)
            {
                // Base value of digit
                char numeral = roman[ptr];
                int digit = (int)Enum.Parse(typeof(RomanDigit), numeral.ToString());

                if (digit > maxDigit)
                {
                    MessageBox.Show("Invalid subtractive combination!");
                    return -1;
                    //break;
                }

                // Next digit
                int nextDigit = 0;
                if (ptr < roman.Length - 1)
                {
                    char nextNumeral = roman[ptr + 1];
                    nextDigit = (int)Enum.Parse(typeof(RomanDigit), nextNumeral.ToString());

                    if (nextDigit > digit)
                    {
                        if ("IXC".IndexOf(numeral) == -1 ||
                            nextDigit > (digit * 10) ||
                            roman.Split(numeral).Length > 3)
                        {
                            MessageBox.Show("Invalid subtractive combination!");
                            return -1;
                            //break;
                        }

                        maxDigit = digit - 1;
                        digit = nextDigit - digit;
                        ptr++;
                    }
                }

                values.Add(digit);

                // Next digit
                ptr++;
            }

            for (int i = 0; i < values.Count - 1; i++)
                if ((int)values[i] < (int)values[i + 1])
                {
                    MessageBox.Show("Invalid reducing value!");
                    return -1;
                    //break;
                }

            int total = 0;
            foreach (int digit in values)
                total += digit;

            return total;
        }
    }
}
