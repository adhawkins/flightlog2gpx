namespace flightlog2gpx
{
    partial class SettingsForm
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGPXPath = new System.Windows.Forms.TextBox();
            this.txtFlightlogPath = new System.Windows.Forms.TextBox();
            this.btnFlightlogBrowse = new System.Windows.Forms.Button();
            this.btnGPXBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(146, 65);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(266, 65);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Flightlog path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "GPX path";
            // 
            // txtGPXPath
            // 
            this.txtGPXPath.Location = new System.Drawing.Point(90, 39);
            this.txtGPXPath.Name = "txtGPXPath";
            this.txtGPXPath.ReadOnly = true;
            this.txtGPXPath.Size = new System.Drawing.Size(303, 20);
            this.txtGPXPath.TabIndex = 5;
            // 
            // txtFlightlogPath
            // 
            this.txtFlightlogPath.Location = new System.Drawing.Point(90, 13);
            this.txtFlightlogPath.Name = "txtFlightlogPath";
            this.txtFlightlogPath.ReadOnly = true;
            this.txtFlightlogPath.Size = new System.Drawing.Size(303, 20);
            this.txtFlightlogPath.TabIndex = 3;
            // 
            // btnFlightlogBrowse
            // 
            this.btnFlightlogBrowse.Location = new System.Drawing.Point(399, 9);
            this.btnFlightlogBrowse.Name = "btnFlightlogBrowse";
            this.btnFlightlogBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnFlightlogBrowse.TabIndex = 6;
            this.btnFlightlogBrowse.Text = "Browse...";
            this.btnFlightlogBrowse.UseVisualStyleBackColor = true;
            this.btnFlightlogBrowse.Click += new System.EventHandler(this.btnFlightlogBrowse_Click);
            // 
            // btnGPXBrowse
            // 
            this.btnGPXBrowse.Location = new System.Drawing.Point(399, 35);
            this.btnGPXBrowse.Name = "btnGPXBrowse";
            this.btnGPXBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnGPXBrowse.TabIndex = 7;
            this.btnGPXBrowse.Text = "Browse...";
            this.btnGPXBrowse.UseVisualStyleBackColor = true;
            this.btnGPXBrowse.Click += new System.EventHandler(this.btnGPXBrowse_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 99);
            this.Controls.Add(this.btnGPXBrowse);
            this.Controls.Add(this.btnFlightlogBrowse);
            this.Controls.Add(this.txtGPXPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFlightlogPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGPXPath;
        private System.Windows.Forms.TextBox txtFlightlogPath;
        private System.Windows.Forms.Button btnFlightlogBrowse;
        private System.Windows.Forms.Button btnGPXBrowse;
    }
}