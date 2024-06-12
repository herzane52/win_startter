using Microsoft.Win32;
using System.Windows.Forms;

namespace WinFormsApp1
{
    partial class WinPlayer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinPlayer));
            exit_button = new Button();
            label1 = new Label();
            label2 = new Label();
            lstUygulamalar = new ListBox();
            btnEkle = new Button();
            btnSil = new Button();
            linkLabel1 = new LinkLabel();
            label3 = new Label();
            SuspendLayout();
            // 
            // exit_button
            // 
            resources.ApplyResources(exit_button, "exit_button");
            exit_button.BackColor = Color.Transparent;
            exit_button.ForeColor = Color.Cyan;
            exit_button.Name = "exit_button";
            exit_button.UseVisualStyleBackColor = false;
            exit_button.Click += button1_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.Cyan;
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.BackColor = Color.Transparent;
            label2.ForeColor = Color.Cyan;
            label2.Name = "label2";
            // 
            // lstUygulamalar
            // 
            resources.ApplyResources(lstUygulamalar, "lstUygulamalar");
            lstUygulamalar.AccessibleRole = AccessibleRole.None;
            lstUygulamalar.AllowDrop = true;
            lstUygulamalar.BackColor = Color.Black;
            lstUygulamalar.BorderStyle = BorderStyle.None;
            lstUygulamalar.DrawMode = DrawMode.OwnerDrawVariable;
            lstUygulamalar.ForeColor = Color.Aqua;
            lstUygulamalar.FormattingEnabled = true;
            lstUygulamalar.Name = "lstUygulamalar";
            lstUygulamalar.SelectionMode = SelectionMode.None;
            lstUygulamalar.DrawItem += LstUygulamalar_DrawItem;
            // 
            // btnEkle
            // 
            resources.ApplyResources(btnEkle, "btnEkle");
            btnEkle.BackColor = Color.Transparent;
            btnEkle.ForeColor = Color.Cyan;
            btnEkle.Name = "btnEkle";
            btnEkle.UseVisualStyleBackColor = false;
            btnEkle.Click += btnEkle_Click;
            // 
            // btnSil
            // 
            resources.ApplyResources(btnSil, "btnSil");
            btnSil.BackColor = Color.Transparent;
            btnSil.ForeColor = Color.Cyan;
            btnSil.Name = "btnSil";
            btnSil.UseVisualStyleBackColor = false;
            btnSil.Click += btnSil_Click;
            // 
            // linkLabel1
            // 
            resources.ApplyResources(linkLabel1, "linkLabel1");
            linkLabel1.ActiveLinkColor = Color.RoyalBlue;
            linkLabel1.LinkColor = Color.White;
            linkLabel1.Name = "linkLabel1";
            linkLabel1.TabStop = true;
            linkLabel1.UseWaitCursor = true;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.BackColor = Color.Transparent;
            label3.FlatStyle = FlatStyle.Popup;
            label3.ForeColor = Color.Cyan;
            label3.Name = "label3";
            // 
            // WinPlayer
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            BackColor = Color.Black;
            Controls.Add(label3);
            Controls.Add(linkLabel1);
            Controls.Add(btnSil);
            Controls.Add(btnEkle);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(exit_button);
            Controls.Add(lstUygulamalar);
            DoubleBuffered = true;
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.None;
            Name = "WinPlayer";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button exit_button;
        private Label label1;
        private Label label2;
        private ListBox lstUygulamalar;
        private Button btnEkle;
        private Button btnSil;
        private LinkLabel linkLabel1;
        private Label label3;
    }
}