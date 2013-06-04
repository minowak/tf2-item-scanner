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
using System.Reflection;

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

            openFileDialog1.Filter = "Scan Result files (.ns) |*.ns|All files (*.*)|*.*";
            saveFileDialog1.Filter = openFileDialog1.Filter;

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

            _title = Text;

            LoadWorkspace(_workspace, true);
        }

        private void LoadSchema()
        {
            SchemaParser parser = new SchemaParser(Utils.SchemaFilename);
            schemaList = parser.Parse();
            leftList = schemaList;
        }

        long longTmp, profilesCount, itemsCount;
        double value, doubleTmp, playtime, online;
        string startingId;

        #region Button clicks

        // start btn
        private void StartBtn_Click(object sender, EventArgs e)
        {

            // API
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

            // If status scanning
            if (statusRadio.Checked)
            {
                scanFromStatus = Utils.GetUsersFromStatus(statusTextBox.Text.Trim());
            }
            else // or depth
            {
                bool success = true;

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
                if (!double.TryParse(playtimeTextBox.Text, out doubleTmp))
                {
                    // handle
                    errorProvider.SetError(playtimeTextBox, "Enter an integer");
                    success = false;
                }
                else
                {
                    errorProvider.Dispose();
                    playtime = doubleTmp;
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
                if (!double.TryParse(onlineTextBox.Text, out doubleTmp))
                {
                    errorProvider.SetError(onlineTextBox, "Enter an integer");
                    success = false;
                }
                else
                {
                    errorProvider.Dispose();
                    online = doubleTmp;
                }
                if (!long.TryParse(itemsCountTextBox.Text, out longTmp))
                {
                    errorProvider.SetError(itemsCountTextBox, "Enter an integer");
                    success = false;
                }
                else
                {
                    errorProvider.Dispose();
                    itemsCount = longTmp;
                }

                if (!success)
                {
                    return; // bad input
                }
            }


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

            profilesFound.Clear();

            StatusLabel.Text = "Scanning";
            tabControl1.SelectedTab = tabControl1.TabPages[1];
            if (statusRadio.Checked)
            {
                if (!statusScanWorker.IsBusy)
                {
                    statusScanWorker.RunWorkerAsync();
                }
            }
            else
            {
                startingId = Utils.GetId(startingIdTextBox.Text);
                if (!scanningWorker.IsBusy)
                {
                    scanningWorker.RunWorkerAsync();
                }
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
        private List<string> scanned = new List<string>();
        private List<string> scanFromStatus = new List<string>();

        private void statusScanWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            long max = scanFromStatus.Count;
            long curr = max;
            foreach (string id in scanFromStatus)
            {
                if (worker.CancellationPending == true)
                {
                    worker.ReportProgress(-2, id);
                    e.Cancel = true;
                    return;
                }

                SteamUser user = new SteamUser(Utils.GetId(id));
                if (!user.Init())
                {
                    continue;
                }

                double d1 = (double)(max - curr--);
                double d2 = d1 / (double)max;
                int progress = (int)(d2 * 100.0);

                worker.ReportProgress(progress);

                TF2Item itemFound = null;
                bool found = false;

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

                if (found)
                {
                    SteamProfile sp = new SteamProfile(user.Name, user.Id, !user.IsPremium(), user.Value, itemFound, user.State, user.PlayTime);
                    worker.ReportProgress(-1, sp);
                    found = false;
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            // searching
            long max = profilesCount;
            string currId = startingId;
            List<string> toScan = new List<string>();

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
                        return;
                    }

                    TF2Item itemFound = null;
                    bool found = false;
                    double bpValue = user.Value;
                    // checking time played
                    if (playtime == 0 || user.PlayTime < playtime * 60)
                    {
                        DateTime dateOnline, dateDesired;
                        dateDesired = DateTime.Now;

                        dateDesired = dateDesired.AddDays(-online);

                        dateOnline = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        dateOnline = dateOnline.AddSeconds(user.LastOnline);

                        if (DateTime.Compare(dateDesired, dateOnline) <= 0 &&
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
                        SteamProfile sp = new SteamProfile(user.Name, user.Id, !user.IsPremium(), user.Value, itemFound, user.State, user.PlayTime);
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

            // save scan to history
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dirPath = Path.Combine(appPath, _workspace, DateTime.Now.ToShortDateString().Replace('/','-'));
            Directory.CreateDirectory(dirPath);
            Utils.SaveScan(Path.Combine(dirPath, DateTime.Now.ToLongTimeString().Replace(':', '_') + ".ns"), rightList, profilesFound, scanned);

            LoadWorkspace(_workspace, true);
        }

        private List<SteamProfile> profilesFound = new List<SteamProfile>();

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -1) // steam profile
            {
                // add row
                SteamProfile sp = e.UserState as SteamProfile;
                AddResultsRow(sp);
                resultsDataGrid.Refresh();

                profilesFound.Add(sp);
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
            Properties.Settings.Default.Online = double.Parse(onlineTextBox.Text);
            Properties.Settings.Default.Playtime = double.Parse(playtimeTextBox.Text);
            Properties.Settings.Default.ProfilesCount = long.Parse(profilesCountTextBox.Text);
            Properties.Settings.Default.Value = double.Parse(valueTextBox.Text);
            Properties.Settings.Default.ItemsCount = long.Parse(itemsCountTextBox.Text);
            Properties.Settings.Default.Save();
        }

        #region Cell clicks

        private void cellContent_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                var cell = (sender as DataGridView).Rows[e.RowIndex].Cells[5] as DataGridViewLinkCell;
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

        private void saveScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StatusLabel.Text = "Saving";
                toolStripProgressBar1.Value = 50;
                Utils.SaveScan(saveFileDialog1.FileName, rightList, profilesFound, scanned);
                StatusLabel.Text = "Saved";
                toolStripProgressBar1.Value = 100;
            }
        }

        private void loadScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StatusLabel.Text = "Loading";
                toolStripProgressBar1.Value = 50;
                List<TF2Item> itemList = new List<TF2Item>();
                List<SteamProfile> profiles = new List<SteamProfile>();
                List<string> sc = new List<string>();
                Utils.LoadScan(openFileDialog1.FileName, out itemList, out profiles, out sc);

                FinishLoading(sc, profiles, itemList);
            }
        }

        private void FinishLoading(List<string> sc, List<SteamProfile> profiles, List<TF2Item> itemList)
        {
            scanned = sc;
            if(sc.Count > 0)
                startingIdTextBox.Text = sc.Last();
            // hard part now
            // results
            resultsDataGrid.Rows.Clear();
            foundCount = 0;
            foreach (SteamProfile sp in profiles)
            {
                AddResultsRow(sp);
            }
            resultsDataGrid.Refresh();
            // items
            selectedDataGrid.Rows.Clear();
            foreach (TF2Item item in itemList)
            {
                bool added = false;
                // check if item already exist with different qualities
                foreach(DataGridViewRow row in selectedDataGrid.Rows)
                {
                    if((string)row.Cells["nameColumn"].Value == item.Name)
                    {
                        switch(item.Quality)
                        {
                            case ItemQuality.Normal:
                                row.Cells["normalColumn"].Value = true;
                                break;
                            case ItemQuality.Unique:
                                row.Cells["uniqueColumn"].Value = true;
                                break;
                            case ItemQuality.Strange:
                                row.Cells["strangeColumn"].Value = true;
                                break;
                            case ItemQuality.Vintage:
                                row.Cells["vintageColumn"].Value = true;
                                break;
                            case ItemQuality.Genuine:
                                row.Cells["genuineColumn"].Value = true;
                                break;
                            case ItemQuality.Unusual:
                                row.Cells["unusualColumn"].Value = true;
                                break;
                            case ItemQuality.Haunted:
                                row.Cells["hauntedColumn"].Value = true;
                                break;
                        }
                        added = true;
                        break;
                    }
                }
                if (added) continue;

                int index = selectedDataGrid.Rows.Add();
                selectedDataGrid.Rows[index].Cells["stockColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Normal));
                selectedDataGrid.Rows[index].Cells["strangeColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Strange));
                selectedDataGrid.Rows[index].Cells["vintageColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Vintage));
                selectedDataGrid.Rows[index].Cells["uniqueColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Unique));
                selectedDataGrid.Rows[index].Cells["genuineColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Genuine));
                selectedDataGrid.Rows[index].Cells["unusualColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Unusual));
                selectedDataGrid.Rows[index].Cells["hauntedColumn"].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)ItemQuality.Haunted));

                DataGridViewRow addedRow = selectedDataGrid.Rows[index];
                addedRow.Cells["nameColumn"].Value = item.Name;
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
                addedRow.Cells["imageColumn"].Value = (Image)(new Bitmap(img, new Size(50, 50)));
                switch (item.Quality)
                {
                    case ItemQuality.Normal:
                        addedRow.Cells["normalColumn"].Value = true;
                        break;
                    case ItemQuality.Unique:
                        addedRow.Cells["uniqueColumn"].Value = true;
                        break;
                    case ItemQuality.Strange:
                        addedRow.Cells["strangeColumn"].Value = true;
                        break;
                    case ItemQuality.Vintage:
                        addedRow.Cells["vintageColumn"].Value = true;
                        break;
                    case ItemQuality.Genuine:
                        addedRow.Cells["genuineColumn"].Value = true;
                        break;
                    case ItemQuality.Unusual:
                        addedRow.Cells["unusualColumn"].Value = true;
                        break;
                    case ItemQuality.Haunted:
                        addedRow.Cells["hauntedColumn"].Value = true;
                        break;
                }
                rightList.Add(item);
            }
            StatusLabel.Text = "Loaded";
            toolStripProgressBar1.Value = 100;
        }

        private void AddResultsRow(SteamProfile sp)
        {
            var itemDraft = sp.ItemFound;
            // we nedd to gather more info from schema
            var item = schemaList.Where(x => x.DefinitionIndex == itemDraft.DefinitionIndex).First();
            item.Quality = itemDraft.Quality;

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

            LinkLabel.Link linkData = new LinkLabel.Link();

            LinkLabel link = new LinkLabel();
            linkData.LinkData = "http://backpack.tf/id/" + sp.Id;
            link.Links.Add(linkData);

            int index = resultsDataGrid.Rows.Add((Image)(new Bitmap(img, new Size(50, 50))), sp.Name, (Math.Truncate(sp.Value * 100) / 100) + "$",
                (sp.Played / 60) + "h", sp.State, link);
            resultsDataGrid.Rows[index].Cells[0].Selected = false;
            if (sp.IsF2P())
            {
                resultsDataGrid.Rows[index].Cells[1].Style.BackColor = Color.Gray;
            }
            resultsDataGrid.Rows[index].Cells[0].Style.BackColor = Color.FromArgb(255, Color.FromArgb((int)item.Quality));
            switch (sp.State)
            {
                default:
                case "Online": resultsDataGrid.Rows[index].Cells[4].Style.ForeColor = Color.Green; break;
                case "Offline": resultsDataGrid.Rows[index].Cells[4].Style.ForeColor = Color.Red; break;
                case "Away":
                case "Snooze": resultsDataGrid.Rows[index].Cells[4].Style.ForeColor = Color.Blue; break;
            }
            resultsDataGrid.Rows[index].Cells[5].Value = "http://backpack.tf/id/" + sp.Id;
            foundCount++;
            tabControl1.TabPages[1].Text = "Results (" + foundCount + ")";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Written by News. Version " + Application.ProductVersion.ToString());
        }

        #region Tree View

        private string _workspace = "workspace";
        private string _title;

        private void selectWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                _workspace = folderBrowserDialog1.SelectedPath;
                Text = _title + " " + _workspace;
                LoadWorkspace(_workspace, true);
            }
        }

        private void LoadWorkspace(string path, bool clear)
        {
            if (clear)
            {
                treeView1.Nodes.Clear();
            }

            try
            {

                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    List<TreeNode> array = new List<TreeNode>();

                    string[] files = Directory.GetFiles(dir);
                    foreach (string file in files)
                    {
                        string fName = file.Split('\\').Last();
                        if (fName.EndsWith(".ns"))
                        {
                            TreeNode subNode = new TreeNode(fName.Substring(0, fName.Length-3));
                            subNode.ImageIndex = 1;
                            subNode.SelectedImageIndex = 1;
                            array.Add(subNode);
                        }
                    }

                    TreeNode treeNode = new TreeNode(dir.Split('\\').Last(), array.ToArray());
                    treeView1.Nodes.Add(treeNode);
                }
            }
            catch (Exception e)
            {
            }
        }

        #endregion

        #region Menu Clicks

        private void newProjectToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        #endregion

        // depth
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            statusRadio.Checked = !depthRadio.Checked;
            depthGroup.Enabled = true;
            statusGroup.Enabled = false;
        }


        // status
        private void statusRadio_CheckedChanged(object sender, EventArgs e)
        {
            depthRadio.Checked = !statusRadio.Checked;
            depthGroup.Enabled = false;
            statusGroup.Enabled = true;
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Level == 1)
            {
                StatusLabel.Text = "Loading";
                toolStripProgressBar1.Value = 50;
                List<TF2Item> itemList = new List<TF2Item>();
                List<SteamProfile> profiles = new List<SteamProfile>();
                List<string> sc = new List<string>();

                var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                Utils.LoadScan(Path.Combine(appPath, _workspace, treeView1.SelectedNode.Parent.Text , 
                    treeView1.SelectedNode.Text + ".ns"), out itemList, out profiles, out sc);

                FinishLoading(sc, profiles, itemList);
            }
        }

        private void historyBtn_Click(object sender, EventArgs e)
        {
            Directory.Delete(_workspace, true);
            Directory.CreateDirectory(_workspace);

            LoadWorkspace(_workspace, true);
        }
    }
}
