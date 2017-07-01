﻿using System.IO.MemoryMappedFiles;
using Microsoft.AnalysisServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO.Pipes;
using NamedPipeWrapper;

namespace SSASDiag
{
    public partial class frmSSASDiag : Form
    {
        string svcOutputPath = "";
        NamedPipeClient<string> npClient;

        [DllImport("advapi32.dll")]
        public static extern int ImpersonateNamedPipeClient(int hNamedPipe);

        private void btnCapture_Click(object sender, EventArgs e)
        {
            // worker we use to launch either start or stop blocking operations to the CDiastnosticsCollector asynchronously
            BackgroundWorker bg = new BackgroundWorker();

            if (dc == null || dc.bRunning || !(btnCapture.Image.Tag as string).Contains("Half Lit"))
            {
                if (btnCapture.Image.Tag as string == "Play" || btnCapture.Image.Tag as string == "Play Lit")
                {
                    new Thread(new ThreadStart(() => DettachProfilerTraceDB())).Start();  // Dettach any existing data from analysis because we're capturing new data now.

                    // Adjust UI to startup.
                    btnCapture.Click -= btnCapture_Click;
                    btnCapture.Image = imgPlayHalfLit;
                    tbAnalysis.ForeColor = SystemColors.ControlDark;
                    tcCollectionAnalysisTabs.Refresh();
                    txtSaveLocation.Enabled = btnSaveLocation.Enabled = tbAnalysis.Enabled = chkZip.Enabled = chkDeleteRaw.Enabled = grpDiagsToCapture.Enabled = dtStopTime.Enabled = chkStopTime.Enabled = chkAutoRestart.Enabled = dtStartTime.Enabled = chkRollover.Enabled = chkStartTime.Enabled = udRollover.Enabled = udInterval.Enabled = cbInstances.Enabled = lblInterval.Enabled = lblInterval2.Enabled = false;
                    ComboBoxServiceDetailsItem cbsdi = cbInstances.SelectedItem as ComboBoxServiceDetailsItem;
                    string TracePrefix = Environment.MachineName + (cbsdi == null ? "" : "_"
                        + (cbInstances.SelectedIndex == 0 ? "" : cbsdi.Text + "_"));
                    
                    // Unhook the status text area from selection while we are actively using it.
                    // I do allow selection after but it was problematic to scroll correctly while allowing user selection during active collection.
                    // This is functionally good, allows them to copy paths or file names after completion but also gives nice behavior during collection.
                    txtStatus.Cursor = Cursors.Arrow;
                    txtStatus.GotFocus += txtStatus_GotFocusWhileRunning;
                    txtStatus.Enter += txtStatus_EnterWhileRunning;

                    if (!Environment.UserInteractive)
                    {
                        dc = new CDiagnosticsCollector(TracePrefix, cbInstances.SelectedIndex == 0 || cbsdi == null ? "" : cbsdi.Text, m_instanceVersion, m_instanceType, m_instanceEdition, m_ConfigDir, m_LogDir, (cbsdi == null ? null : cbsdi.ServiceAccount),
                            txtStatus,
                            (int)udInterval.Value, chkAutoRestart.Checked, chkZip.Checked, chkDeleteRaw.Checked, chkProfilerPerfDetails.Checked, chkXMLA.Checked, chkABF.Checked, chkBAK.Checked, (int)udRollover.Value, chkRollover.Checked, dtStartTime.Value, chkStartTime.Checked, dtStopTime.Value, chkStopTime.Checked,
                            chkGetConfigDetails.Checked, chkGetProfiler.Checked, chkGetPerfMon.Checked, chkGetNetwork.Checked);
                        while (!dc.npServer._connections.Exists(c=>c.IsConnected))
                            Thread.Sleep(100);
                        ImpersonateNamedPipeClient(dc.npServer._connections[0].Id);
                        
                        LogFeatureUse("Collection", "InstanceVersion=" + m_instanceVersion + ",InstanceType=" + m_instanceType + ",InstanceEdition=" + m_instanceEdition + ",PerfMonInterval=" + udInterval.Value + ",AutoRestartProfiler=" + chkAutoRestart.Checked +
                                                    ",UseZip=" + chkZip.Checked + ",DeleteRawDataAfterZip=" + chkDeleteRaw.Checked + ",IncludeProfilerVerbosePerfDetails=" + chkProfilerPerfDetails.Checked +
                                                    ",IncludeXMLA=" + chkXMLA.Checked + ",IncludeABF=" + chkABF.Checked + ",IncludeBAK=" + chkBAK.Checked + (chkRollover.Checked ? ",RolloverMB=" + udRollover.Value : "") +
                                                    (chkStartTime.Checked ? ",StartTime=" + dtStartTime.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss") : "") +
                                                    (chkStopTime.Checked ? ",StopTime=" + dtStopTime.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss") : "")
                                                    + ",ConfigDetails=" + chkGetConfigDetails.Checked + ",Profiler=" + chkGetProfiler.Checked + ",PerfMon=" + chkGetPerfMon.Checked + ",NetworkTrace=" + chkGetNetwork.Checked + ",RunningAsService=" + !Environment.UserInteractive);
                        dc.CompletionCallback = callback_StartDiagnosticsComplete;
                        new Thread(new ThreadStart(() => dc.StartDiagnostics())).Start();
                    }
                    else
                    {
                        // Install the service for this instance
                        string sInstanceServiceConfig = Program.TempPath + "SSASDiagService_" + (cbInstances.SelectedIndex == 0 ? "MSSQLSERVER" : cbInstances.Text) + ".exe";
                        File.Copy(Program.TempPath + "SSASDiagService.exe", sInstanceServiceConfig, true);
                        File.Copy(Program.TempPath + "SSASDiagService.ini", sInstanceServiceConfig.Replace(".exe", ".ini"), true);
                        List<string> svcconfig = new List<string>(File.ReadAllLines(sInstanceServiceConfig.Replace(".exe", ".ini")));
                        svcconfig[svcconfig.FindIndex(s => s.StartsWith("ServiceName="))] = "ServiceName=SSASDiag_" + (cbInstances.SelectedIndex == 0 ? "MSSQLSERVER" : cbInstances.Text);
                        svcconfig[svcconfig.FindIndex(s => s.StartsWith("ServiceLongName="))] = "ServiceLongName=SQL Server Analysis Services Diagnostic Collection Service (" + (cbInstances.SelectedIndex == 0 ? "MSSQLSERVER" : cbInstances.Text) + ")";
                        svcconfig[svcconfig.FindIndex(s => s.StartsWith("ServiceDesc="))] = "ServiceDesc=Launch SSASDiag.exe to administer data collection.  SSASDiag provides automated diagnostic collection for SQL Server Analysis Services.";
                        svcconfig[svcconfig.FindIndex(s => s.StartsWith("WorkingDir="))] = "WorkingDir=" + (AppDomain.CurrentDomain.GetData("originalbinlocation") as string);
                        File.WriteAllLines(sInstanceServiceConfig.Replace(".exe", ".ini"), svcconfig.ToArray());
                        ProcessStartInfo p = new ProcessStartInfo(sInstanceServiceConfig);
                        p.CreateNoWindow = true;
                        p.UseShellExecute = false;
                        p.WindowStyle = ProcessWindowStyle.Hidden;
                        p.Arguments = "-i";
                        Process proc = Process.Start(p);
                        proc.WaitForExit();

                        // Setup the service startup parameters according to user selections
                        svcconfig[svcconfig.FindIndex(s => s.StartsWith("CommandLine="))] 
                            = 
                            "CommandLine=" + (AppDomain.CurrentDomain.GetData("originalbinlocation") as string) + "\\SSASDiag.exe" +
                            " /workingdir \"" + txtSaveLocation.Text + "\"" +
                            (chkZip.Checked ? " /zip" : "") +
                            " /instance " + (cbInstances.SelectedIndex == 0 ? "MSSQLSERVER" : cbInstances.Text) +
                            (chkDeleteRaw.Checked ? " /deleteraw" : "") +
                            (chkRollover.Checked ? " /rollover" + udRollover.Value : "") +
                            (chkStartTime.Checked ? " /starttime \"" + dtStartTime.Value.ToString("MM/dd/yyyy HH:mm:ss") + "\"" : "") +
                            (chkStopTime.Checked ? " /stoptime \"" + dtStopTime.Value.ToString("MM/dd/yyyy HH:mm:ss") + "\"" : "") +
                            (chkAutoRestart.Checked ? " /autorestartprofiler" : "") +
                            " /perfmoninterval " + udInterval.Value +
                            (chkGetConfigDetails.Checked ? " /config" : "") +
                            (chkGetPerfMon.Checked ? " /perfmon" : "") +
                            (chkGetProfiler.Checked ? " /profiler" : "") +
                            (chkProfilerPerfDetails.Checked ? " /verbose" : "") +
                            (chkABF.Checked ? " /abf" : "") +
                            (chkBAK.Checked ? " /bak" : "") +
                            (chkXMLA.Checked ? " /xmla" : "") +
                            (chkGetNetwork.Checked ? " /network" : "") +
                            (Args.ContainsKey("debug") ? " /debug" : "") +
                            (chkAllowUsageStatsCollection.Checked  ? " /reportusage" : "") +
                            " /start";
                        File.WriteAllLines(sInstanceServiceConfig.Replace(".exe", ".ini"), svcconfig.ToArray());
                        svcOutputPath = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\SSASDiag_" + (cbInstances.SelectedIndex == 0 ? "MSSQLSERVER" : cbInstances.Text)).GetValue("ImagePath") as string;
                        svcOutputPath = svcOutputPath.Substring(0, svcOutputPath.IndexOf(".exe")) + ".output.log";
                        File.CreateText(svcOutputPath).Close();
                        string sMsg = "Initializing SSAS diagnostics collection at " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss UTCzzz") + ".";
                        File.WriteAllText(svcOutputPath, sMsg);
                        txtStatus.Text = sMsg;
                        ServiceController InstanceCollectionService = new ServiceController("SSASDiag_" + (cbInstances.SelectedIndex == 0 ? "MSSQLSERVER" : cbInstances.Text));
                        new Thread(new ThreadStart(() => InstanceCollectionService.Start())).Start();
                        npClient = new NamedPipeClient<string>("SSASDiag_" + (cbInstances.SelectedIndex == 0 ? "MSSQLSERVER" : cbInstances.Text));
                        npClient.ServerMessage += NpClient_ServerMessage;
                        npClient.Start();
                        npClient.PushMessage("User=" + System.DirectoryServices.AccountManagement.UserPrincipal.Current.UserPrincipalName);
                    }
                }
                else if (btnCapture.Image.Tag as string == "Stop" || btnCapture.Image.Tag as string == "Stop Lit")
                {
                    btnCapture.Click -= btnCapture_Click;
                    btnCapture.Image = imgStopHalfLit;
                    if (!Environment.UserInteractive)
                        new Thread(new ThreadStart(() => dc.StopAndFinalizeAllDiagnostics())).Start();
                    else
                    {
                        string sInstance = (cbInstances.SelectedIndex == 0 ? "MSSQLSERVER" : cbInstances.Text);
                        new Thread(new ThreadStart(() =>
                            {
                                ServiceController sc = new ServiceController("SSASDiag_" + sInstance);
                                try { sc.Stop(); } catch { }
                                while (sc.Status != ServiceControllerStatus.Stopped)
                                    Thread.Sleep(100);
                                Invoke(new System.Action(() =>
                                {
                                    callback_StopAndFinalizeAllDiagnosticsComplete();
                                    npClient.ServerMessage -= NpClient_ServerMessage;
                                }));
                            })).Start();
                    }
                }
            }
        }

        private void NpClient_ServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            string LastStatusLine = "";
            txtStatus.Invoke(new System.Action(() => LastStatusLine = txtStatus.Lines.Last()));
            if ((message.StartsWith("\r\nDiagnostics captured for ") && LastStatusLine.StartsWith("Diagnostics captured for ")) ||
                (message.StartsWith("\r\nTime remaining until collection starts: ") && LastStatusLine.StartsWith("Time remaining until collection starts: ")))
            {
                btnCapture.Invoke(new System.Action(() =>
                {
                    if (btnCapture.Image.Tag as string == "Play Half Lit")
                    {
                        btnCapture.Image = imgStop;
                        btnCapture.Click += btnCapture_Click;
                    }
                }));
                txtStatus.Invoke(new System.Action(() => txtStatus.Text = txtStatus.Text.Replace(LastStatusLine, message.Replace("\r\n", ""))));
            }
            else if (message.StartsWith("\r\nWaiting for client interaction:\r\n"))
            {
                string uiMsg = message.Replace("\r\nWaiting for client interaction:\r\n", "");
                if (uiMsg.StartsWith("Windows Administrator required for remote server:\r\n"))
                {
                    frmPasswordPrompt pp = new frmPasswordPrompt();
                    pp.UserMessage = uiMsg;
                    Invoke(new System.Action(() =>
                    {
                        pp.Top = Top + Height / 2 - pp.Height / 2;
                        pp.Left = Left + Width / 2 - pp.Width / 2;
                    }));
                    Invoke(new System.Action(() => Enabled = false));
                    pp.ShowDialog();
                    Invoke(new System.Action(() => Enabled = true));
                    if (pp.DialogResult == DialogResult.OK)
                        npClient.PushMessage("Administrator=" + pp.User.Trim() + ";Domain=" + pp.Domain.Trim() + ";Password=" + pp.Password.Trim());
                    else
                        npClient.PushMessage("Cancelled by client");
                }
            }
            else
                txtStatus.Invoke(new System.Action(() => txtStatus.AppendText(message)));
        }
                
        private string[] WriteSafeReadAllLines(String path)
        {
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                List<string> file = new List<string>();
                while (!sr.EndOfStream)
                    file.Add(sr.ReadLine());
                return file.ToArray();
            }
            else
                return new string[] { "" };
        }

        #region BlockingUIComponentsBesidesCapture

        class ComboBoxServiceDetailsItem
        {
            public string Text { get; set; }
            public string ConfigPath { get; set; }
            public string ServiceAccount { get; set; }
        }
        private void cbInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCapture.Enabled = false;
            BackgroundWorker bgPopulateInstanceDetails = new BackgroundWorker();
            bgPopulateInstanceDetails.DoWork += BgPopulateInstanceDetails_DoWork;
            bgPopulateInstanceDetails.RunWorkerCompleted += BgPopulateInstanceDetails_RunWorkerCompleted;
            bgPopulateInstanceDetails.RunWorkerAsync();  
        }

        private void BgPopulateInstanceDetails_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Server srv = new Server();
                ComboBoxServiceDetailsItem SelItem = cbInstances.Invoke(new Func<ComboBoxServiceDetailsItem>(() => { return (cbInstances.SelectedItem as ComboBoxServiceDetailsItem); })) as ComboBoxServiceDetailsItem;
                if (SelItem != null)
                {
                    srv.Connect("Data source=" + Environment.MachineName + (SelItem.Text == "Default instance (MSSQLServer)" ? "" : "\\" + SelItem.Text) + ";Timeout=0;Integrated Security=SSPI;SSPI=NTLM;", true);
                    System.Diagnostics.Trace.WriteLine("Connected to server with connection string: " + srv.ConnectionString);
                    lblInstanceDetails.Invoke(new System.Action(() => lblInstanceDetails.Text = "Instance Details:\r\n" + srv.Version + " (" + srv.ProductLevel + "), " + srv.ServerMode + ", " + srv.Edition));
                    m_instanceType = srv.ServerMode.ToString();
                    m_instanceVersion = srv.Version + " - " + srv.ProductLevel;
                    m_instanceEdition = srv.Edition.ToString();
                    m_LogDir = srv.ServerProperties["LogDir"].Value;
                    m_ConfigDir = SelItem.ConfigPath;
                    srv.Disconnect();
                    btnCapture.Invoke(new System.Action(() => btnCapture.Enabled = true));
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                if (!lblInstanceDetails.IsDisposed) lblInstanceDetails.Invoke(new System.Action(() => lblInstanceDetails.Text = "Instance details could not be obtained due to failure connecting:\r\n" + ex.Message));
            }
        }

        private void BgPopulateInstanceDetails_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (btnCapture.Enabled && Args.ContainsKey("start") && cbInstances.Items.Count > 0)
                    btnCapture_Click(sender, e);
            if (cbInstances.SelectedIndex >= 0)
            {
                chkRunAsService_CheckedChanged("uninstall", e);
                chkRunAsService_CheckedChanged(null, e);
            }
        }

        private void PopulateInstanceDropdown()
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += bgPopulateInstanceDropdown;
            bg.RunWorkerCompleted += bgPopulateInstanceDropdownComplete;
            bg.RunWorkerAsync();
        }
        private void bgPopulateInstanceDropdown(object sender, DoWorkEventArgs e)
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices();
                foreach (ServiceController s in services.OrderBy(ob => ob.DisplayName))
                    if (s.DisplayName.Contains("Analysis Services") && !s.DisplayName.Contains("SQL Server Analysis Services CEIP ("))
                    {
                        SelectQuery sQuery = new SelectQuery("select name, startname, pathname from Win32_Service where name = \"" + s.ServiceName + "\"");
                        ManagementObjectSearcher mgmtSearcher = new ManagementObjectSearcher(sQuery);
                        string sSvcUser = "";
                        foreach (ManagementObject svc in mgmtSearcher.Get())
                            sSvcUser = svc["startname"] as string;
                        if (sSvcUser.Contains(".")) sSvcUser = sSvcUser.Replace(".", Environment.UserDomainName);
                        if (sSvcUser == "LocalSystem") sSvcUser = "NT AUTHORITY\\SYSTEM";

                        string ConfigPath = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Services\\" + s.ServiceName, false).GetValue("ImagePath") as string;
                        System.Diagnostics.Trace.WriteLine("Found AS instance: " + ConfigPath);
                        ConfigPath = ConfigPath.Substring(ConfigPath.IndexOf("-s \"") + "-s \"".Length).TrimEnd('\"');
                        if (s.DisplayName.Replace("SQL Server Analysis Services (", "").Replace(")", "").ToUpper() == "MSSQLSERVER")
                            LocalInstances.Insert(0, new ComboBoxServiceDetailsItem() { Text = "Default instance (MSSQLServer)", ConfigPath = ConfigPath, ServiceAccount = sSvcUser });
                        else
                            LocalInstances.Add(new ComboBoxServiceDetailsItem() { Text = s.DisplayName.Replace("SQL Server Analysis Services (", "").Replace(")", ""), ConfigPath = ConfigPath, ServiceAccount = sSvcUser });
                    }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("Failure during instance enumeration - could be because no instances were there.  Move on quietly then.");
                System.Diagnostics.Trace.WriteLine(ex);
            }
            if (LocalInstances.Count == 0)
                cbInstances.Invoke(new System.Action(() => cbInstances.Enabled = false));
        }
        private void bgPopulateInstanceDropdownComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            cbInstances.DataSource = LocalInstances;
            cbInstances.DisplayMember = "Text";
            cbInstances.Refresh();
            if (cbInstances.Items.Count > 0) cbInstances.SelectedIndex = 0;
            if (LocalInstances.Count == 0)
                lblInstanceDetails.Text = "There were no Analysis Services instances found on this server.\r\nPlease run on a server with a SQL 2008 or later SSAS instance.";
            else
            {
                if (Args.ContainsKey("instance"))
                {
                    int i = LocalInstances.FindIndex(c => c.Text.ToLower() == Args["instance"].ToLower().TrimEnd().TrimStart());
                    if (i > 0)
                        cbInstances.SelectedIndex = i;
                }
            }
        }

        #endregion BlockingUIComponentsBesidesCapture

        #region VariousNonBlockingUIElements

        private void btnSaveLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.Description = "Select save location for capture.";
            fbd.SelectedPath = txtSaveLocation.Text;
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                Properties.Settings.Default["SaveLocation"] = Environment.CurrentDirectory = txtSaveLocation.Text = fbd.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }
        private void txtStatus_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string sOut = "";
                foreach (string s in txtStatus.Lines)
                    sOut += s + "\r\n";
                Clipboard.SetData(DataFormats.StringFormat, sOut);
                ttStatus.Show("Output window text copied to clipboard.", txtStatus, 2500);
                new Thread(new ThreadStart(new System.Action(() =>
                {
                    Thread.Sleep(2500);
                    txtStatus.Invoke(new System.Action(() => ttStatus.SetToolTip(txtStatus, "")));
                }))).Start();
            }
        }
        private void splitCollectionUI_Panel1_Resize(object sender, EventArgs e)
        {
            grpDiagsToCapture.Height = splitCollectionUI.Panel1.Height - grpDiagsToCapture.Top;
            grpDiagsToCapture.Width = splitCollectionUI.Panel1.Width - grpDiagsToCapture.Left;
            rtbProblemDescription.Width = tabGuided.Width - rtbProblemDescription.Left;
            rtbProblemDescription.Height = tabGuided.Height - rtbProblemDescription.Top;
        }
        private void btnCapture_MouseEnter(object sender, EventArgs e)
        {
            if (btnCapture.Image.Tag as string == "Play")
                btnCapture.Image = imgPlayLit;
            else if (btnCapture.Image.Tag as string == "Stop")
                btnCapture.Image = imgStopLit;
        }
        private void btnCapture_MouseLeave(object sender, EventArgs e)
        {
            if (btnCapture.Image.Tag as string == "Play Lit")
                btnCapture.Image = imgPlay;
            else if (btnCapture.Image.Tag as string == "Stop Lit")
                btnCapture.Image = imgStop;
        }
        private void tcCollectionAnalysisTabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tcCollectionAnalysisTabs.TabPages[e.Index];
            e.Graphics.FillRectangle(new SolidBrush(page.BackColor), e.Bounds);
            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            TextRenderer.DrawText(e.Graphics, page.Text, Font, paddedBounds, page.ForeColor);
        }

        #endregion VariousNonBlockingUIElements

        #region CaptureDetailsUI

        private void chkRollover_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRollover.Checked) udRollover.Enabled = true; else udRollover.Enabled = false;
            if (chkGetNetwork.Checked && chkRollover.Checked)
                ttStatus.Show("NOTE: Network traces rollover circularly,\n"
                            + "always deleting older data automatically.", chkRollover, 3500);
        }
        private void chkStopTime_CheckedChanged(object sender, EventArgs e)
        {
            dtStopTime.Enabled = chkStopTime.Checked;
            if (!chkStopTime.Checked)
            {
                if (chkAutoRestart.Checked) ttStatus.Show("AutoRestart disabled for your protection without stop time.", chkAutoRestart, 1750);
                chkAutoRestart.Checked = false;
            }
            else
                dtStopTime.Value = DateTime.Now.AddHours(1);
        }
        private void chkStartTime_CheckedChanged(object sender, EventArgs e)
        {
            dtStartTime.Enabled = chkStartTime.Checked;
            if (chkStartTime.Checked) dtStartTime.Value = DateTime.Now.AddHours(0);
        }
        private void chkAutoRestart_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRestart.Checked && !chkStopTime.Checked)
            {
                ttStatus.Show("Stop time required for your protection with AutoRestart=true.", dtStopTime, 1750);
                chkStopTime.Checked = true;
            }
        }
        private void chkZip_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkZip.Checked)
                chkDeleteRaw.Checked = false;
        }
        private void chkDeleteRaw_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeleteRaw.Checked)
            {
                ttStatus.Show("After zip, keep raw data for analysis.\r\nThis saves the zip decompression step later.", chkDeleteRaw, 4000);
                chkZip.Checked = true;
            }
        }
        private void SetRolloverAndStartStopEnabledStates()
        {
            chkRollover.Enabled = chkStartTime.Enabled = chkStopTime.Enabled = dtStartTime.Enabled = dtStopTime.Enabled
                = chkGetPerfMon.Checked | chkGetProfiler.Checked | chkGetNetwork.Checked;
            udRollover.Enabled = chkRollover.Enabled & chkRollover.Checked;
            dtStartTime.Enabled = chkStartTime.Enabled & chkStartTime.Checked;
            dtStopTime.Enabled = chkStopTime.Enabled & chkStopTime.Checked;
        }
        private void chkRunAsService_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        #endregion CaptureDetailsUI
    }
}
