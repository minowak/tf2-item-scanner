using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using tf2_item_scanner.schema;
using tf2_item_scanner.utils;
using tf2_item_scanner.engine;
using System.Net;
using System.IO;

namespace tf2_item_scanner
{
    public partial class MainWindow : Form
    {
        private List<TF2Item> leftList;
        private List<TF2Item> rightList;

        private List<TF2Item> schemaList;

        public MainWindow()
        {
            InitializeComponent();

            // get schema
            LoadSchema();
            leftListBox.DataSource = leftList;
            rightList = new List<TF2Item>();

            profilesCountTextBox.Text = Properties.Settings.Default.ProfilesCount.ToString();
            onlineTextBox.Text = Properties.Settings.Default.Online.ToString();
            valueTextBox.Text = Properties.Settings.Default.Value.ToString();
            playtimeTextBox.Text = Properties.Settings.Default.Playtime.ToString();
            itemsCountTextBox.Text = Properties.Settings.Default.ItemsCount.ToString();

            selectedDataGrid.Refresh();
        }

        private void LoadSchema()
        {
            SchemaParser parser = new SchemaParser(Utils.SchemaFilename);
            schemaList = parser.Parse();
            leftList = schemaList;
        }

        long longTmp, playtime, profilesCount, online, itemsCount;
        double value, doubleTmp;
        string startingId;

        #region Button clicks

        // start btn
        private void StartBtn_Click(object sender, EventArgs e)
        {
            bool success = true;

            if (apiTextBox.Text == String.Empty)
            {
                errorProvider.SetError(apiTextBox, "Enter API key");
                return;
            }
            else
            {
                if (!IsRegistered())
                {
                    errorProvider.SetError(apiTextBox, "Not registered API key");
                    return;
                }
            }

            Utils.ApiKey = apiTextBox.Text;

            errorProvider.Clear();

            if (!long.TryParse(profilesCountTextBox.Text, out longTmp))
            {
                // handle
                errorProvider.SetError(profilesCountTextBox, "Enter an integer");
                success = false;
            }
            else
            {
                errorProvider.Dispose();
                profilesCount = longTmp;
            }
            if (!long.TryParse(playtimeTextBox.Text, out longTmp))
            {
                // handle
                errorProvider.SetError(playtimeTextBox, "Enter an integer");
                success = false;
            }
            else
            {
                errorProvider.Dispose();
                playtime = longTmp;
            }
            if (!double.TryParse(valueTextBox.Text, out doubleTmp))
            {
                // handle
                errorProvider.SetError(valueTextBox, "Enter correct value");
                success = false;
            }
            else
            {
                errorProvider.Dispose();
                value = doubleTmp;
            }
            if (!long.TryParse(onlineTextBox.Text, out longTmp))
            {
                errorProvider.SetError(onlineTextBox, "Enter an integer");
                success = false;
            }
            else
            {
                errorProvider.Dispose();
                online = longTmp;
            }
            if(!long.TryParse(itemsCountTextBox.Text, out longTmp))
            {
                errorProvider.SetError(itemsCountTextBox, "Enter an integer");
                success = false;
            }
            else
            {
                errorProvider.Dispose();
                itemsCount = longTmp;
            }

            if (success)
            {
                saveSettings();
                errorProvider.Dispose();

                // fill list of items to scan for

                List <TF2Item> tmpList = new List<TF2Item>();
                foreach (DataGridViewRow row in selectedDataGrid.Rows)
                {
                    TF2Item rootItem = rightList.Where(x => x.Name == (string)row.Cells["nameColumn"].Value).First();

                    if (((DataGridViewCheckBoxCell)row.Cells["stockColumn"]).Value != null && (bool)((DataGridViewCheckBoxCell)row.Cells["stockColumn"]).Value)
                    {
                        TF2Item qualityItem = new TF2Item(rootItem);
                        qualityItem.Quality = ItemQuality.Normal;
                        tmpList.Add(qualityItem);
                    }
                    if (((DataGridViewCheckBoxCell)row.Cells["strangeColumn"]).Value != null && (bool)((DataGridViewCheckBoxCell)row.Cells["strangeColumn"]).Value)
                    {
                        TF2Item qualityItem = new TF2Item(rootItem);
                        qualityItem.Quality = ItemQuality.Strange;
                        tmpList.Add(qualityItem);
                    }
                    if (((DataGridViewCheckBoxCell)row.Cells["vintageColumn"]).Value != null && (bool)((DataGridViewCheckBoxCell)row.Cells["vintageColumn"]).Value)
                    {
                        TF2Item qualityItem = new TF2Item(rootItem);
                        qualityItem.Quality = ItemQuality.Vintage;
                        tmpList.Add(qualityItem);
                    }
                    if (((DataGridViewCheckBoxCell)row.Cells["genuineColumn"]).Value != null && (bool)((DataGridViewCheckBoxCell)row.Cells["genuineColumn"]).Value)
                    {
                        TF2Item qualityItem = new TF2Item(rootItem);
                        qualityItem.Quality = ItemQuality.Genuine;
                        tmpList.Add(qualityItem);
                    }
                    if (((DataGridViewCheckBoxCell)row.Cells["unusualColumn"]).Value != null && (bool)((DataGridViewCheckBoxCell)row.Cells["unusualColumn"]).Value)
                    {
                        TF2Item qualityItem = new TF2Item(rootItem);
                        qualityItem.Quality = ItemQuality.Genuine;
                        tmpList.Add(qualityItem);
                    }
                    if (((DataGridViewCheckBoxCell)row.Cells["hauntedColumn"]).Value != null && (bool)((DataGridViewCheckBoxCell)row.Cells["hauntedColumn"]).Value)
                    {
                        TF2Item qualityItem = new TF2Item(rootItem);
                        qualityItem.Quality = ItemQuality.Haunted;
                        tmpList.Add(qualityItem);
                    }
                    if (((DataGridViewCheckBoxCell)row.Cells["uniqueColumn"]).Value != null && (bool)((DataGridViewCheckBoxCell)row.Cells["uniqueColumn"]).Value)
                    {
                        TF2Item qualityItem = new TF2Item(rootItem);
                        qualityItem.Quality = ItemQuality.Unique;
                        tmpList.Add(qualityItem);
                    }
                }

                rightList = tmpList;

                StatusLabel.Text = "Scanning";
                tabControl1.SelectedTab = tabControl1.TabPages[1];
                startingId = Utils.GetId(startingIdTextBox.Text);
                scanningWorker.RunWorkerAsync();
            }
        }

        // stop btn
        private void StopBtn_Click(object sender, EventArgs e)
        {
            scanningWorker.CancelAsync();
            StatusLabel.Text = "Stopping";
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            selectedDataGrid.Rows.Clear();
            deleteButton.Enabled = false;
        }

        private void clearResultsBtn_Click(object sender, EventArgs e)
        {
            resultsDataGrid.Rows.Clear();
            tabControl1.TabPages[1].Text = "Results";
        }

        #endregion

        private void Search_TextChanged(object sender, EventArgs e)
        {
            string filter = searchTextBox.Text;
            if (filter == String.Empty)
            {
                leftList = schemaList;
            }
            else
            {
                leftList = schemaList.Where(x => x.Name.ToLower().Contains(filter.ToLower())).ToList();
            }

            leftListBox.DataSource = leftList;
            leftListBox.Refresh();
        }

        private void leftListBox_DoubleClick(object sender, EventArgs e)
        {
            if (leftListBox.SelectedItem != null)
            {
                var item = (TF2Item)leftListBox.SelectedItem;
                foreach (DataGridViewRow row in selectedDataGrid.Rows)
                {
                    if ((string)row.Cells["nameColumn"].Value == item.Name)
                    {
                        return;
                    }
                }

                System.Drawing.Image img = System.Drawing.Image.FromFile("qmark.jpg");
                try
                {
                    WebClient wc = new WebClient();
                    byte[] bytes = wc.DownloadData(item.Image);
                    MemoryStream ms = new MemoryStream(bytes);
                    img = System.Drawing.Image.FromStream(ms);
                }
                catch (Exception ex)
                {
                    // TODO log
                }

                int index = selectedDataGrid.Rows.Add();
                selectedDataGrid.Rows[index].Cells["selectedColumn"].Value = false;
                selectedDataGrid.Rows[index].Cells["nameColumn"].Value = item.Name;
                selectedDataGrid.Rows[index].Cells["imageColumn"].Value = (Image)(new Bitmap(img, new Size(50, 50)));
               
                selectedDataGrid.Rows[index].Cells["stockColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Normal));
                selectedDataGrid.Rows[index].Cells["strangeColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Strange));
                selectedDataGrid.Rows[index].Cells["vintageColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Vintage));
                selectedDataGrid.Rows[index].Cells["uniqueColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Unique));
                selectedDataGrid.Rows[index].Cells["genuineColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Genuine));
                selectedDataGrid.Rows[index].Cells["unusualColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Unusual));
                selectedDataGrid.Rows[index].Cells["hauntedColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Haunted));
                selectedDataGrid.Refresh();

                rightList.Add(item);
            }
        }

        private bool IsRegistered()
        {
            string keys = Utils.GetJson("http://student.agh.edu.pl/~minowak/keys");
            return keys.Contains(apiTextBox.Text.Trim());
        }

        #region Updating Schema Worker

        // update schema
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SchemaUpdater.UpdateSchema();
        }

        private void updateSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "Updating schema";
            toolStripProgressBar1.Value = 50;
            schemaUpdaterWorker.RunWorkerAsync();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar1.Value = 100;
            StatusLabel.Text = "Updated schema";
        }

        #endregion

        #region Scanning worker

        private long foundCount = 0;

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            // searching
            long max = profilesCount;
            string currId = startingId;
            List<string> toScan = new List<string>();
            List<string> scanned = new List<string>();
            SteamUser user;

            toScan.Add(currId);

            foundCount = 0;

                while (profilesCount > 0)
                {
                    if (worker.CancellationPending == true || toScan.Count == 0)
                    {
                        worker.ReportProgress(-2, currId);
                        e.Cancel = true;
                        return;
                    }

                    double d1 = (double)(max - profilesCount);
                    double d2 = d1 / (double)max;

                    int progress = (int)(d2 * 100.0);

                    profilesCount--;
                    bool initialized = false;

                    user = new SteamUser(currId);

                    if (initialized = user.Init())
                    {
                        // accessible profile
                        foreach (string friend in user.FriendsIds)
                        {
                            if (!scanned.Contains(friend) && !toScan.Contains(friend))
                            {
                                toScan.Add(friend);
                            }
                        }
                    }

                    // finally from java
                    worker.ReportProgress(progress);
                    bool newFound = false;
                    while (toScan.Count > 0)
                    {
                        currId = toScan[0];
                        toScan.Remove(currId);
                        if (scanned.Contains(currId))
                        {
                            continue;
                        }
                        scanned.Add(currId);
                        newFound = true;
                        break;
                    }
                    if (!initialized)
                    {
                        continue;
                    }
                    if (!newFound && (toScan.Count == 0))
                    {
                        // friends tree ended early
                        return;
                    }

                    TF2Item itemFound = null;
                    bool found = false;
                    double bpValue = user.Value;
                    // checking time played
                    if (playtime == 0 || user.PlayTime < playtime * 60)
                    {
                        DateTime dateOnline, dateDesired;
                        dateDesired = new DateTime(online * 86400000);
                        dateOnline = new DateTime(user.LastOnline);

                        // TODO add items count
                        if (DateTime.Compare(dateOnline, dateDesired) <= 0 &&
                           (value == 0 || bpValue <= value) &&
                           (itemsCount == 0 || itemsCount >= user.ItemsCount))
                        {
                            // checking item
                            if ((itemFound = user.HasUnusual()) != null)
                            {
                                found = true;
                            }
                            else
                            {
                                foreach (TF2Item item in rightList)
                                {
                                    if (user.HasItem(item))
                                    {
                                        found = true;
                                        itemFound = item;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (found)
                    {
                        // TODO  rethink: wypierdolic visited ? Tak
                        SteamProfile sp = new SteamProfile(user.Name, user.Id, user.IsPremium(), user.Value, itemFound, user.State);
                        // TODO add found profile to results (update tab with (n) ? )
                        worker.ReportProgress(-1, sp);
                        found = false;
                    }

                    while (toScan.Count > 0)
                    {
                        currId = toScan[0];
                        toScan.Remove(currId);
                        if (scanned.Contains(currId))
                        {
                            continue;
                        }
                        scanned.Add(currId);
                        newFound = true;
                        break;
                    }
                }

            
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel.Text = "Scanning finished.";
            toolStripProgressBar1.Value = toolStripProgressBar1.Maximum;
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -1) // steam profile
            {
                foundCount++;
                tabControl1.TabPages[1].Text = "Results (" + foundCount + ")";
                // add row
                SteamProfile sp = e.UserState as SteamProfile;
                var itemDraft = sp.ItemFound;
                // we nedd to gather more info from schema
                var item = schemaList.Where(x => x.DefinitionIndex == itemDraft.DefinitionIndex).First();
                item.Quality = itemDraft.Quality;
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(item.Image);
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                LinkLabel.Link linkData = new LinkLabel.Link();
                
                LinkLabel link = new LinkLabel();
                linkData.LinkData = "http://backpack.tf/id/" + sp.Id;
                link.Links.Add(linkData);

                int index = resultsDataGrid.Rows.Add((Image)(new Bitmap(img, new Size(50, 50))), sp.Name, (Math.Truncate(sp.Value * 100) / 100) + "$", 
                    sp.State, link);
                resultsDataGrid.Rows[index].Cells[0].Selected = false;
                resultsDataGrid.Rows[index].Cells[0].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)item.Quality));
                switch(sp.State)
                {
                    default:
                    case "Online": resultsDataGrid.Rows[index].Cells[3].Style.ForeColor = Color.Green; break;
                    case "Offline": resultsDataGrid.Rows[index].Cells[3].Style.ForeColor = Color.Red; break;
                    case "Away":
                    case "Snooze": resultsDataGrid.Rows[index].Cells[3].Style.ForeColor = Color.Blue; break;
                }
                resultsDataGrid.Rows[index].Cells[4].Value = "http://backpack.tf/id/" + sp.Id;
                resultsDataGrid.Refresh();
            }
            else if (e.ProgressPercentage == -2)
            {
                string id = e.UserState as string;
                startingIdTextBox.Text = id;
            }
            else
            {
                toolStripProgressBar1.Value = e.ProgressPercentage;
            }
        }

        #endregion

        private void tabColtrol1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in selectedDataGrid.Rows)
            {
                foreach(DataGridViewCell cell in row.Cells)
                {
                    cell.Selected = false;
                }
            }
            foreach (DataGridViewRow row in resultsDataGrid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Selected = false;
                }
            }
        }

        private void saveSettings()
        {
            Properties.Settings.Default.StartingId = startingIdTextBox.Text;
            Properties.Settings.Default.ApiKey = apiTextBox.Text;
            Properties.Settings.Default.Online = long.Parse(onlineTextBox.Text);
            Properties.Settings.Default.Playtime = long.Parse(playtimeTextBox.Text);
            Properties.Settings.Default.ProfilesCount = long.Parse(profilesCountTextBox.Text);
            Properties.Settings.Default.Value = double.Parse(valueTextBox.Text);
            Properties.Settings.Default.ItemsCount = long.Parse(itemsCountTextBox.Text);
            Properties.Settings.Default.Save();
        }

        #region Cell clicks

        private void cellContent_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var cell = (sender as DataGridView).Rows[e.RowIndex].Cells[3] as DataGridViewLinkCell;
                System.Diagnostics.Process.Start(cell.Value.ToString());
            }
        }

        private void selectedCell_ContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                var cell = selectedDataGrid.Rows[e.RowIndex].Cells["selectedColumn"] as DataGridViewCheckBoxCell;
                cell.Value = cell.Value == cell.TrueValue ? cell.FalseValue : cell.TrueValue;
                if (cell.Value != null && cell.Value == cell.TrueValue)
                {
                    deleteButton.Enabled = true;
                }
                else
                {
                    bool anySelected = false;
                    for (int i = 0; i < selectedDataGrid.Rows.Count; i++)
                    {
                        cell = selectedDataGrid.Rows[i].Cells["selectedColumn"] as DataGridViewCheckBoxCell;
                        if (cell.Value != null && cell.Value == cell.TrueValue)
                        {
                            anySelected = true;
                            break;
                        }
                    }

                    deleteButton.Enabled = anySelected;
                }
            }
        }

        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://steamcommunity.com/dev/apikey/");
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in selectedDataGrid.Rows)
            {
                if (row.Cells[0].Value != null && (string)row.Cells[0].Value == "true")
                {
                    selectedDataGrid.Rows.Remove(row);
                }
            }
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            if (scanningWorker.IsBusy)
            {
                DialogResult dialog = dialog = MessageBox.Show("Scanning in progress. Are you sure?", "Quit", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    scanningWorker.CancelAsync();
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
