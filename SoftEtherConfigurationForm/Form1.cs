using NLog;
using SoftEtherConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SoftEtherConfigurationForm
{
    public partial class SoftEtherConfigurationForm : Form
    {
        #region Head
        /// <summary>
        /// Logger de la classe SoftEtherConfigutaionForm
        /// </summary>
        private static readonly Logger appLog = LogManager.GetLogger("FormLogger");

        #region Membres
        Configuration configuration { get; set; }

        string fileName { get; set; }
        string filePath { get; set; }

        bool canChangeUser = true;
        bool canChangeSawmill = true;
        bool clearData = true;
        #endregion
        #endregion

        /// <summary>
        /// Connexion au configuration
        /// </summary>
        public SoftEtherConfigurationForm()
        {
            InitializeComponent();
        }

        #region Boutons
        #region Boutons Fichier
        /// <summary>
        /// Import du fichier .config
        /// </summary>
        private void btFile_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = "Fichier SoftEther Config|*.config" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
                filePath = dialog.SafeFileName;
                configuration = new Configuration(dialog.FileName);

                textBox6.Text = dialog.FileName;
                btFile.Enabled = false;
                btSave.Enabled = BtCert.Enabled = btSubSawmill.Enabled = true;

                UpdateDisplay();
            }
        }

        /// <summary>
        /// Bouton d'écriture et de sauvegarde du fichier
        /// </summary>
        private void btSave_Click(object sender, EventArgs e)
        {
            var path = fileName.Replace(filePath, "vpn_server.config");
            string[] text = (string[])configuration.WriteText();
            File.WriteAllLines(path, text);
            textBox6.Text += "\r\n\r\nFile saved in : " + path;
            btSave.Enabled = false;
            textBox6.Text += "\r\n\r\nTo make others changes, please restart the software.";
        }
        #endregion

        #region Boutons Utilisateurs
        /// <summary>
        /// Bouton ajout utilisateur
        /// </summary>
        private void btAddUser_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = "Fichier Cer|*.cer" };
            if (String.IsNullOrEmpty(txtIdUser.Text) || String.IsNullOrEmpty(txtNameUser.Text))
                MessageBox.Show("Please complete the fields", "ERROR");
            else
            {
                if (listAccessUser.CheckedItems.Count != 0)
                {
                    var currentIDName = txtIdUser.Text;
                    var currentSawMill = configuration.SawMills;
                    User user = null;

                    if (!configuration.Users.ContainsKey(currentIDName))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {

                            if (!configuration.Users.ContainsKey(currentIDName))
                            {
                                user = configuration.AddUser(txtIdUser.Text, dialog.FileName, txtNameUser.Text);
                            }
                            List<object> checkedItems = new List<object>();

                            foreach (var check in listAccessUser.CheckedItems)
                                checkedItems.Add(check);


                            foreach (var check in checkedItems)
                            {
                                //foreach (var sawmill in currentSawMill)
                                //{
                                //    var currentName = sawmill.Value.SawMillName;
                                //    if (currentName == check.ToString())
                                //    {
                                //        configuration.AddTextUser(check.ToString(), user);
                                //        configuration.SawMills[check.ToString()].Users[currentIDName] = user;

                                //        txtIdUser.ResetText();
                                //        txtNameUser.ResetText();
                                //    }
                                //}


                                if(currentSawMill.ContainsKey(check.ToString()))
                                {
                                    configuration.AddTextUser(check.ToString(), user);
                                    configuration.SawMills[check.ToString()].Users[currentIDName] = user;
                                }
                            }
                            txtIdUser.ResetText();
                            txtNameUser.ResetText();
                        }
                    }
                    else
                    {
                        MessageBox.Show("The user already exists", "ERROR");
                    }
                }
                else
                    MessageBox.Show("Please select a sawmill", "ERROR");
            }
            UpdateDisplay();
        }

        /// <summary>
        /// Bouton clear selection utilisateur
        /// </summary>
        private void btClearUser_Click(object sender, EventArgs e)
        {
            canChangeUser = false;

            dgvUser.ClearSelection();

            txtIdUser.ResetText();
            txtIdUser.Enabled = true;
            txtIdUser.BackColor = Color.White;

            txtNameUser.ResetText();
            txtNameUser.Enabled = true;
            txtNameUser.BackColor = Color.White;

            foreach (int checkedItemIndex in listAccessUser.CheckedIndices)
            {
                listAccessUser.SetItemChecked(checkedItemIndex, false);
            }
        }
        #endregion

        #region Boutons Scieries
        /// <summary>
        /// Bouton ajout scierie
        /// </summary>
        private void btAddSawmill_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNameSawmill.Text))
                MessageBox.Show("Please complete the fields", "ERROR");
            else
            {
                var currentSawmillName = txtNameSawmill.Text;
                User currentUser = configuration.Users["client1"];

                SawMill newSawmill = new SawMill();
                newSawmill.SawMillName = currentSawmillName;

                if (!configuration.SawMills.ContainsKey(currentSawmillName))
                {
                    configuration.AddTextSawmill(currentSawmillName);
                    configuration.SawMills.Add(currentSawmillName, newSawmill);

                    configuration.AddTextUser(currentSawmillName, currentUser);
                    configuration.SawMills[currentSawmillName].Users.Add(currentUser.UserName, currentUser);

                    txtNameSawmill.Text = "";
                }
                else
                    MessageBox.Show("The sawmill already exists", "ERROR");
            }
            UpdateDisplay();
        }

        /// <summary>
        /// Bouton suppression scierie
        /// </summary>
        private void btDeleteSawmill_Click(object sender, EventArgs e)
        {
            var currentSawmillName = Convert.ToString(dgvSawmill.SelectedCells[0].Value);

            configuration.RemoveTextSawmill(currentSawmillName);
            configuration.SawMills.Remove(currentSawmillName);

            UpdateDisplay();
        }

        /// <summary>
        /// Bouton clear selection scierie
        /// </summary>
        private void btClearSawmill_Click(object sender, EventArgs e)
        {
            canChangeSawmill = false;

            btDeleteSawmill.Enabled = listAccess.Enabled = false;

            dgvSawmill.ClearSelection();
            txtNameSawmill.ResetText();
            txtNameSawmill.Enabled = true;
            txtNameSawmill.BackColor = Color.White;

            foreach (int checkedItemIndex in listAccess.CheckedIndices)
            {
                listAccess.SetItemChecked(checkedItemIndex, false);
            }
        }
        #endregion
        #endregion

        #region Update()
        /// <summary>
        /// Update des éléments
        /// </summary>
        private void UpdateDisplay()
        {
            UpdateDisplayUser();
            UpdateDisplaySawmill();
            UpdateDisplayList();
            UpdateDisplayListUser();
        }

        #region Sous-updates
        /// <summary>
        /// Update de la datagridview utilisateur
        /// </summary>
        private void UpdateDisplayUser()
        {
            dgvUser.Rows.Clear();

            var properties = typeof(User).GetProperties().OrderBy(p => p.MetadataToken);

            int y = 0;

            dgvUser.ColumnCount = properties.Count();
            foreach (var property in properties)
            {
                dgvUser.Columns[y].Name = property.Name;
                y++;
            }

            foreach (var n in configuration.Users)
            {
                string[] row = new string[] { n.Value.UserName, n.Value.Key, n.Value.Name };
                dgvUser.Rows.Add(row);
            }
        }

        /// <summary>
        /// Update de la datagridview scierie
        /// </summary>
        private void UpdateDisplaySawmill()
        {
            dgvSawmill.Rows.Clear();

            var properties = typeof(SawMill).GetProperties().OrderBy(p => p.MetadataToken);

            int y = 0;

            dgvSawmill.ColumnCount = 1;
            foreach (var property in properties)
            {
                if (property.Name != "Users")
                {
                    dgvSawmill.Columns[y].Name = property.Name;
                    y++;
                }
            }

            foreach (var n in configuration.SawMills)
            {
                string row = n.Value.SawMillName;
                dgvSawmill.Rows.Add(row);
            }
        }

        /// <summary>
        /// Update de la checkedlistbox page scieries
        /// </summary>
        private void UpdateDisplayList()
        {
            listAccess.Items.Clear();

            foreach (var n in configuration.Users)
            {
                string row = n.Value.UserName;
                listAccess.Items.Add(row);
            }
        }

        /// <summary>
        /// Update de la checkedlistbox page utilisateurs
        /// </summary>
        private void UpdateDisplayListUser()
        {
            listAccessUser.Items.Clear();

            foreach (var n in configuration.SawMills)
            {
                string row = n.Value.SawMillName;
                listAccessUser.Items.Add(row);
            }
        }

        private void ClearDataGrid()
        {
            if(clearData)
                dgvUser.ClearSelection();
        }
        #endregion

        #endregion

        #region Selection lignes DataGridView
        /// <summary>
        /// Événement de selection d'une ligne scierie
        /// </summary>
        private void dgvSawmill_CellClick(object sender, EventArgs e)
        {
            canChangeSawmill = false;

            btDeleteSawmill.Enabled = listAccess.Enabled = true;

            foreach (int checkedItemIndex in listAccess.CheckedIndices)
            {
                listAccess.SetItemChecked(checkedItemIndex, false);
            }

            txtNameSawmill.Text = Convert.ToString(dgvSawmill.SelectedCells[0].Value);
            txtNameSawmill.Enabled = false;
            txtNameSawmill.BackColor = Color.LightGray;

            var currentSawMillName = txtNameSawmill.Text;
            var currentSawMill = configuration.SawMills[currentSawMillName];

            foreach (var user in currentSawMill.Users)
            {
                var currentUserName = user.Value.UserName;

                for (int i = 0; i < listAccess.Items.Count; i++)
                {
                    if (currentUserName == listAccess.Items[i].ToString())
                        listAccess.SetItemChecked(i, true);
                }
            }
            canChangeSawmill = true;
        }

        /// <summary>
        /// Événement de selection d'une ligne utilisateur
        /// </summary>
        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            canChangeUser = false;
            clearData = false;

            foreach (int checkedItemIndex in listAccessUser.CheckedIndices)
            {
                listAccessUser.SetItemChecked(checkedItemIndex, false);
            }

            if (dgvUser.SelectedCells.Count != 0)
            {

                txtIdUser.Text = Convert.ToString(dgvUser.SelectedCells[0].Value);
                txtIdUser.Enabled = false;
                txtIdUser.BackColor = Color.LightGray;

                txtNameUser.Text = Convert.ToString(dgvUser.SelectedCells[2].Value);
                txtNameUser.Enabled = false;
                txtNameUser.BackColor = Color.LightGray;
            }

            var currentIDName = Convert.ToString(dgvUser.SelectedCells[0].Value);
            var currentSawMill = configuration.SawMills;

            foreach (var sawmill in currentSawMill)
            {
                foreach (var user in sawmill.Value.Users)
                {
                    var currentName = sawmill.Value.SawMillName;

                    for (int i = 0; i < listAccessUser.Items.Count; i++)
                    {
                        if (user.Value.UserName == currentIDName)
                        {
                            if (currentName == listAccessUser.Items[i].ToString())
                                listAccessUser.SetItemChecked(i, true);
                        }
                    }
                }
            }

            canChangeUser = true;
        }
        #endregion

        #region Contrôle check/uncheck CheckedListBox
        /// <summary>
        /// Événement de check et uncheck de la checkedlistbox page utilisateurs
        /// </summary>
        private void listAccessUser_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (canChangeUser)
            {
                var currentIDName = txtIdUser.Text;
                if (listAccessUser.CheckedItems.Count != 0)
                {
                    var currentSawMill = configuration.SawMills;
                    foreach (var sawmill in currentSawMill)
                    {
                        var currentName = sawmill.Value.SawMillName;
                        if (dgvUser.SelectedCells.Count > 0)
                        {
                            var id = Convert.ToString(dgvUser.SelectedCells[0].Value);
                            if (e.NewValue == CheckState.Checked)
                            {
                                if (currentName == listAccessUser.Items[e.Index].ToString())
                                {
                                    User user = configuration.Users[id];
                                    configuration.AddTextUser(currentName, user);
                                    configuration.SawMills[currentName].Users[currentIDName] = user;
                                }
                            }
                            else
                            {
                                if (currentName == listAccessUser.Items[e.Index].ToString())
                                {
                                    User user = configuration.Users[id];
                                    configuration.RemoveTextUser(currentName, user.UserName);
                                    if (configuration.SawMills[currentName].Users.ContainsKey(currentIDName))
                                    {
                                        var x = configuration.SawMills[currentName];
                                        x.Users.Remove(currentIDName);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Événement de check et uncheck de la checkedlistbox page scieries
        /// </summary>
        private void listAccess_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (canChangeSawmill)
            {
                var currentSawmillName = txtNameSawmill.Text;
                var currentUsers = configuration.Users;
                var listUser = listAccess.Items[e.Index];

                foreach (var user in currentUsers)
                {
                    var currentName = user.Value.UserName;
                    User currentUser = user.Value;
                    if (e.NewValue == CheckState.Checked)
                    {
                        if (currentName == listUser.ToString())
                        {
                            if (listAccess.CheckedItems.Count != 0)
                            {
                                configuration.AddTextUser(currentSawmillName, currentUser);
                                configuration.SawMills[currentSawmillName].Users.Add(currentName, currentUser);
                            }

                            txtIdUser.ResetText();
                            txtNameUser.ResetText();
                        }
                    }
                    else
                    {
                        if (currentName == listUser.ToString())
                        {
                            configuration.RemoveTextUser(currentSawmillName, currentName);
                            if (configuration.Users.ContainsKey(currentName))
                            {
                                var x = configuration.SawMills[currentSawmillName];
                                x.Users.Remove(currentName);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        private void txtIdUser_TextChanged(object sender, EventArgs e)
        {
            ClearDataGrid();
        }
    }
}