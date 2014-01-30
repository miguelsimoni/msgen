using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace msgen.ui
{
    public partial class TextBoxPlus : TextBox
    {
        private string innerLabel;

        [CategoryAttribute("Plus"), DescriptionAttribute("Inner Label")]
        public string InnerLabel
        {
            get{ return innerLabel; }
            set { innerLabel = value; }
        }


        private InputTypes inputType;

        [CategoryAttribute("Plus"), DescriptionAttribute("Input Type")]
        public InputTypes InputType
        {
            get { return inputType; }
            set { inputType = value; }
        }

        public enum InputTypes
        {
            Numeric,
            Decimal
        }


        private string validationPattern;

        [CategoryAttribute("Plus"), DescriptionAttribute("Validation Pattern")]
        public string ValidationPattern
        {
            get { return validationPattern; }
            set { validationPattern = value; } 
        }

        
        public TextBoxPlus()
        {
            InitializeComponent();
        }

        private void TextBoxPlus_Load(object sender, EventArgs e)
        {
            clear();
        }

        private void TextBoxPlus_Enter(object sender, EventArgs e)
        {
            this.Text = string.Empty;
            this.ForeColor = Color.Black;
        }

        private void TextBoxPlus_Leave(object sender, EventArgs e)
        {
            if(this.Text == string.Empty)
            {
                clear();
            }
        }

        private void clear()
        {
            this.ForeColor = Color.Gray;
            this.Text = this.InnerLabel;
        }


        #region input type validation
        
        private void TextBoxPlus_KeyDown(object sender, KeyEventArgs e)
        {
            switch(this.inputType)
            {
                case InputTypes.Numeric:
                    if(!validKeyForNumber.Contains(e.KeyCode) && !validKeyForEdit.Contains(e.KeyCode))
                    {
                        e.SuppressKeyPress = true;
                    }
                    if(Control.ModifierKeys == Keys.Shift)
                    {
                        e.SuppressKeyPress = true;
                    }
                    break;
                case InputTypes.Decimal:
                    if(!validKeyForDecimal.Contains(e.KeyCode) && !validKeyForEdit.Contains(e.KeyCode))
                    {
                        e.SuppressKeyPress = true;
                    }
                    if(Control.ModifierKeys == Keys.Shift)
                    {
                        e.SuppressKeyPress = true;
                    }
                    break;
                default:
                    break;
            }
        }

        public static List<System.Windows.Forms.Keys> validKeyForEdit = new List<System.Windows.Forms.Keys>()
        {
            System.Windows.Forms.Keys.Enter,
            System.Windows.Forms.Keys.Tab,
            System.Windows.Forms.Keys.Home,
            System.Windows.Forms.Keys.End,
            System.Windows.Forms.Keys.Delete,
            System.Windows.Forms.Keys.Back,
            System.Windows.Forms.Keys.Up,
            System.Windows.Forms.Keys.Down,
            System.Windows.Forms.Keys.Left,
            System.Windows.Forms.Keys.Right
        };

        public static List<System.Windows.Forms.Keys> validKeyForNumber = new List<System.Windows.Forms.Keys>()
        {
            System.Windows.Forms.Keys.D0,
            System.Windows.Forms.Keys.D1,
            System.Windows.Forms.Keys.D2,
            System.Windows.Forms.Keys.D3,
            System.Windows.Forms.Keys.D4,
            System.Windows.Forms.Keys.D5,
            System.Windows.Forms.Keys.D5,
            System.Windows.Forms.Keys.D6,
            System.Windows.Forms.Keys.D7,
            System.Windows.Forms.Keys.D8,
            System.Windows.Forms.Keys.D9,
            System.Windows.Forms.Keys.NumPad0,
            System.Windows.Forms.Keys.NumPad1,
            System.Windows.Forms.Keys.NumPad2,
            System.Windows.Forms.Keys.NumPad3,
            System.Windows.Forms.Keys.NumPad4,
            System.Windows.Forms.Keys.NumPad5,
            System.Windows.Forms.Keys.NumPad6,
            System.Windows.Forms.Keys.NumPad7,
            System.Windows.Forms.Keys.NumPad8,
            System.Windows.Forms.Keys.NumPad9
        };

        public static List<System.Windows.Forms.Keys> validKeyForDecimal = new List<System.Windows.Forms.Keys>(validKeyForNumber)
        {
            //validKeyForNumber + 
            System.Windows.Forms.Keys.Decimal,
            System.Windows.Forms.Keys.Oemcomma
        };

        #endregion

    }
}
