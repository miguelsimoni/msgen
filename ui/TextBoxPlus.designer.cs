namespace msgen.ui
{
    partial class TextBoxPlus
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TextBoxPlus
            // 
            this.Name = "TextBoxPlus";
            this.Size = new System.Drawing.Size(100, 20);
            this.ForeColor = System.Drawing.Color.Gray;
            this.Leave += new System.EventHandler(this.TextBoxPlus_Leave);
            this.Enter += new System.EventHandler(this.TextBoxPlus_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxPlus_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}
