
namespace SoftEtherConfigurationForm
{
    partial class SoftEtherConfigurationForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoftEtherConfigurationForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pgFile = new System.Windows.Forms.TabPage();
            this.btSave = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.btFile = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.pgSawmill = new System.Windows.Forms.TabPage();
            this.btDeleteSawmill = new System.Windows.Forms.Button();
            this.ClearSelectSawmill = new System.Windows.Forms.Button();
            this.txtNameSawmill = new System.Windows.Forms.TextBox();
            this.listAccess = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvSawmill = new System.Windows.Forms.DataGridView();
            this.btSubSawmill = new System.Windows.Forms.Button();
            this.pgUsers = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIdUser = new System.Windows.Forms.TextBox();
            this.listAccessUser = new System.Windows.Forms.CheckedListBox();
            this.btClearUser = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNameUser = new System.Windows.Forms.TextBox();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.BtCert = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.pgFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pgSawmill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSawmill)).BeginInit();
            this.pgUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pgFile);
            this.tabControl1.Controls.Add(this.pgSawmill);
            this.tabControl1.Controls.Add(this.pgUsers);
            this.tabControl1.ItemSize = new System.Drawing.Size(48, 20);
            this.tabControl1.Location = new System.Drawing.Point(4, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(407, 431);
            this.tabControl1.TabIndex = 0;
            // 
            // pgFile
            // 
            this.pgFile.Controls.Add(this.btSave);
            this.pgFile.Controls.Add(this.pictureBox1);
            this.pgFile.Controls.Add(this.textBox6);
            this.pgFile.Controls.Add(this.btFile);
            this.pgFile.Controls.Add(this.label9);
            this.pgFile.Location = new System.Drawing.Point(4, 24);
            this.pgFile.Name = "pgFile";
            this.pgFile.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.pgFile.Size = new System.Drawing.Size(399, 403);
            this.pgFile.TabIndex = 0;
            this.pgFile.Text = "File";
            this.pgFile.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            this.btSave.Enabled = false;
            this.btSave.ForeColor = System.Drawing.Color.Green;
            this.btSave.Location = new System.Drawing.Point(318, 374);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 4;
            this.btSave.Text = "Save...";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SoftEtherConfigurationForm.Properties.Resources.logo_lbl;
            this.pictureBox1.Location = new System.Drawing.Point(347, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(6, 51);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(387, 319);
            this.textBox6.TabIndex = 2;
            // 
            // btFile
            // 
            this.btFile.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btFile.Location = new System.Drawing.Point(6, 22);
            this.btFile.Name = "btFile";
            this.btFile.Size = new System.Drawing.Size(100, 23);
            this.btFile.TabIndex = 0;
            this.btFile.Text = "Choose File...";
            this.btFile.UseVisualStyleBackColor = true;
            this.btFile.Click += new System.EventHandler(this.btFile_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Download file (.config) :";
            // 
            // pgSawmill
            // 
            this.pgSawmill.Controls.Add(this.btDeleteSawmill);
            this.pgSawmill.Controls.Add(this.ClearSelectSawmill);
            this.pgSawmill.Controls.Add(this.txtNameSawmill);
            this.pgSawmill.Controls.Add(this.listAccess);
            this.pgSawmill.Controls.Add(this.label3);
            this.pgSawmill.Controls.Add(this.label2);
            this.pgSawmill.Controls.Add(this.label1);
            this.pgSawmill.Controls.Add(this.dgvSawmill);
            this.pgSawmill.Controls.Add(this.btSubSawmill);
            this.pgSawmill.Location = new System.Drawing.Point(4, 24);
            this.pgSawmill.Name = "pgSawmill";
            this.pgSawmill.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.pgSawmill.Size = new System.Drawing.Size(399, 403);
            this.pgSawmill.TabIndex = 1;
            this.pgSawmill.Text = "Sawmills";
            this.pgSawmill.UseVisualStyleBackColor = true;
            // 
            // btDeleteSawmill
            // 
            this.btDeleteSawmill.BackColor = System.Drawing.Color.Transparent;
            this.btDeleteSawmill.Enabled = false;
            this.btDeleteSawmill.ForeColor = System.Drawing.Color.Crimson;
            this.btDeleteSawmill.Location = new System.Drawing.Point(318, 3);
            this.btDeleteSawmill.Name = "btDeleteSawmill";
            this.btDeleteSawmill.Size = new System.Drawing.Size(75, 23);
            this.btDeleteSawmill.TabIndex = 8;
            this.btDeleteSawmill.Text = "Delete";
            this.btDeleteSawmill.UseVisualStyleBackColor = false;
            this.btDeleteSawmill.Click += new System.EventHandler(this.btDeleteSawmill_Click);
            // 
            // ClearSelectSawmill
            // 
            this.ClearSelectSawmill.Location = new System.Drawing.Point(196, 377);
            this.ClearSelectSawmill.Name = "ClearSelectSawmill";
            this.ClearSelectSawmill.Size = new System.Drawing.Size(197, 23);
            this.ClearSelectSawmill.TabIndex = 7;
            this.ClearSelectSawmill.Text = "Clear selection";
            this.ClearSelectSawmill.UseVisualStyleBackColor = true;
            this.ClearSelectSawmill.Click += new System.EventHandler(this.btClearSawmill_Click);
            // 
            // txtNameSawmill
            // 
            this.txtNameSawmill.BackColor = System.Drawing.SystemColors.Window;
            this.txtNameSawmill.Location = new System.Drawing.Point(6, 27);
            this.txtNameSawmill.Name = "txtNameSawmill";
            this.txtNameSawmill.Size = new System.Drawing.Size(184, 20);
            this.txtNameSawmill.TabIndex = 1;
            // 
            // listAccess
            // 
            this.listAccess.CheckOnClick = true;
            this.listAccess.Enabled = false;
            this.listAccess.FormattingEnabled = true;
            this.listAccess.Location = new System.Drawing.Point(6, 99);
            this.listAccess.Name = "listAccess";
            this.listAccess.Size = new System.Drawing.Size(184, 274);
            this.listAccess.TabIndex = 2;
            this.listAccess.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listAccess_ItemCheck);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "List of clients :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Access :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name :";
            // 
            // dgvSawmill
            // 
            this.dgvSawmill.AllowUserToAddRows = false;
            this.dgvSawmill.AllowUserToDeleteRows = false;
            this.dgvSawmill.AllowUserToResizeColumns = false;
            this.dgvSawmill.AllowUserToResizeRows = false;
            this.dgvSawmill.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSawmill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSawmill.ColumnHeadersVisible = false;
            this.dgvSawmill.Location = new System.Drawing.Point(196, 27);
            this.dgvSawmill.MultiSelect = false;
            this.dgvSawmill.Name = "dgvSawmill";
            this.dgvSawmill.ReadOnly = true;
            this.dgvSawmill.RowHeadersVisible = false;
            this.dgvSawmill.RowHeadersWidth = 51;
            this.dgvSawmill.RowTemplate.Height = 25;
            this.dgvSawmill.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSawmill.Size = new System.Drawing.Size(197, 344);
            this.dgvSawmill.TabIndex = 4;
            this.dgvSawmill.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSawmill_CellClick);
            // 
            // btSubSawmill
            // 
            this.btSubSawmill.Enabled = false;
            this.btSubSawmill.Location = new System.Drawing.Point(6, 53);
            this.btSubSawmill.Name = "btSubSawmill";
            this.btSubSawmill.Size = new System.Drawing.Size(184, 23);
            this.btSubSawmill.TabIndex = 6;
            this.btSubSawmill.Text = "Add";
            this.btSubSawmill.UseVisualStyleBackColor = true;
            this.btSubSawmill.Click += new System.EventHandler(this.btAddSawmill_Click);
            // 
            // pgUsers
            // 
            this.pgUsers.Controls.Add(this.label6);
            this.pgUsers.Controls.Add(this.label5);
            this.pgUsers.Controls.Add(this.txtIdUser);
            this.pgUsers.Controls.Add(this.listAccessUser);
            this.pgUsers.Controls.Add(this.btClearUser);
            this.pgUsers.Controls.Add(this.label4);
            this.pgUsers.Controls.Add(this.txtNameUser);
            this.pgUsers.Controls.Add(this.dgvUser);
            this.pgUsers.Controls.Add(this.BtCert);
            this.pgUsers.Controls.Add(this.flowLayoutPanel1);
            this.pgUsers.Controls.Add(this.pictureBox2);
            this.pgUsers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pgUsers.Location = new System.Drawing.Point(4, 24);
            this.pgUsers.Name = "pgUsers";
            this.pgUsers.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.pgUsers.Size = new System.Drawing.Size(399, 403);
            this.pgUsers.TabIndex = 2;
            this.pgUsers.Text = "Users";
            this.pgUsers.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "ID :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Access :";
            // 
            // txtIdUser
            // 
            this.txtIdUser.Location = new System.Drawing.Point(24, 26);
            this.txtIdUser.Name = "txtIdUser";
            this.txtIdUser.Size = new System.Drawing.Size(119, 20);
            this.txtIdUser.TabIndex = 5;
            this.txtIdUser.TextChanged += new System.EventHandler(this.txtIdUser_TextChanged);
            // 
            // listAccessUser
            // 
            this.listAccessUser.CheckOnClick = true;
            this.listAccessUser.FormattingEnabled = true;
            this.listAccessUser.Location = new System.Drawing.Point(6, 144);
            this.listAccessUser.Name = "listAccessUser";
            this.listAccessUser.Size = new System.Drawing.Size(387, 79);
            this.listAccessUser.TabIndex = 14;
            this.listAccessUser.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listAccessUser_ItemCheck);
            // 
            // btClearUser
            // 
            this.btClearUser.Location = new System.Drawing.Point(6, 378);
            this.btClearUser.Name = "btClearUser";
            this.btClearUser.Size = new System.Drawing.Size(387, 23);
            this.btClearUser.TabIndex = 12;
            this.btClearUser.Text = "Clear selection";
            this.btClearUser.UseVisualStyleBackColor = true;
            this.btClearUser.Click += new System.EventHandler(this.btClearUser_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Name :";
            // 
            // txtNameUser
            // 
            this.txtNameUser.Location = new System.Drawing.Point(24, 65);
            this.txtNameUser.Name = "txtNameUser";
            this.txtNameUser.Size = new System.Drawing.Size(119, 20);
            this.txtNameUser.TabIndex = 6;
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            this.dgvUser.AllowUserToResizeColumns = false;
            this.dgvUser.AllowUserToResizeRows = false;
            this.dgvUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.ColumnHeadersVisible = false;
            this.dgvUser.Location = new System.Drawing.Point(6, 244);
            this.dgvUser.MultiSelect = false;
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.ReadOnly = true;
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.RowHeadersWidth = 51;
            this.dgvUser.RowTemplate.Height = 25;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(387, 130);
            this.dgvUser.TabIndex = 10;
            this.dgvUser.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUser_CellClick);
            // 
            // BtCert
            // 
            this.BtCert.Enabled = false;
            this.BtCert.Location = new System.Drawing.Point(24, 91);
            this.BtCert.Name = "BtCert";
            this.BtCert.Size = new System.Drawing.Size(119, 23);
            this.BtCert.TabIndex = 7;
            this.BtCert.Text = "Add";
            this.BtCert.UseVisualStyleBackColor = true;
            this.BtCert.Click += new System.EventHandler(this.btAddUser_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(148, 121);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::SoftEtherConfigurationForm.Properties.Resources.logo_lbl;
            this.pictureBox2.Location = new System.Drawing.Point(347, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(45, 45);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // SoftEtherConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 446);
            this.Controls.Add(this.tabControl1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SoftEtherConfigurationForm";
            this.Text = "SoftEtherConfiguration";
            this.tabControl1.ResumeLayout(false);
            this.pgFile.ResumeLayout(false);
            this.pgFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pgSawmill.ResumeLayout(false);
            this.pgSawmill.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSawmill)).EndInit();
            this.pgUsers.ResumeLayout(false);
            this.pgUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pgSawmill;
        private System.Windows.Forms.TabPage pgUsers;
        private System.Windows.Forms.TabPage pgFile;
        private System.Windows.Forms.Button btSubSawmill;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox listAccess;
        private System.Windows.Forms.TextBox txtNameSawmill;
        private System.Windows.Forms.Button BtCert;
        private System.Windows.Forms.TextBox txtNameUser;
        private System.Windows.Forms.TextBox txtIdUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btFile;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvSawmill;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.Button ClearSelectSawmill;
        private System.Windows.Forms.Button btClearUser;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox listAccessUser;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btDeleteSawmill;
    }
}

