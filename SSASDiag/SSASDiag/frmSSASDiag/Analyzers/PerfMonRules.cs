﻿using Microsoft.Win32;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Management;
using System.ServiceProcess;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Data.SqlClient;
using System.IO;
using SimpleMDXParser;
using FastColoredTextBoxNS;
using System.Windows.Forms.DataVisualization.Charting;
using Ionic.Zip;

namespace SSASDiag
{
    public partial class ucASPerfMonAnalyzer : UserControl
    {
        void DefineRules()
        {
            Rules.Clear();

            // Rule 0

            RuleCounter AvailableMB = new RuleCounter("Memory\\Available MBytes", true, false, Color.Blue);
            RuleCounter WorkingSet = new RuleCounter("Process\\Working Set\\_Total", false);
            Rule r = new Rule();
            r.Category = "Memory";
            r.Name = "Server Available Memory";
            r.Description = "Checks to ensure sufficient free memory.";
            r.Counters.Add(AvailableMB);
            r.Counters.Add(WorkingSet);
            
            r.RuleFunction = new Action(() =>
            {
                // Create custom series.
                r.RuleResult = RuleResultEnum.Pass;

                Series TotalMemory = new Series("Total Physical Memory MB");
                TotalMemory.ChartType = SeriesChartType.Line;
                TotalMemory.XValueType = ChartValueType.DateTime;
                TotalMemory.EmptyPointStyle.BorderWidth = 0;
                TotalMemory.BorderColor = TotalMemory.Color = Color.Black;

                double MaxUtilizationPct = .97;
                Series MaximumUtilization = new Series("97% Memory Utilization");
                MaximumUtilization.ChartType = SeriesChartType.Line;
                MaximumUtilization.XValueType = ChartValueType.DateTime;
                MaximumUtilization.EmptyPointStyle.BorderWidth = 0;
                MaximumUtilization.BorderColor = MaximumUtilization.Color = Color.Red;
                MaximumUtilization.BorderDashStyle = ChartDashStyle.Dot;

                DataPointCollection p1 = AvailableMB.ChartSeries.Points, p2 = WorkingSet.ChartSeries.Points;
                double totalMem = ((double)p1[0].Tag) + (((double)p2[0].Tag) / 1024.0 / 1024.0);
                for (int i = 0; i < p1.Count; i++)
                {
                    TotalMemory.Points.Add(new DataPoint(p1[i].XValue, totalMem));
                    MaximumUtilization.Points.Add(new DataPoint(p1[i].XValue, totalMem * MaxUtilizationPct));
                }

                r.CustomSeries.Add(TotalMemory);
                r.CustomSeries.Add(MaximumUtilization);

                // Call out data any points that fail the test.
                foreach (DataPoint p in AvailableMB.ChartSeries.Points)
                {
                    if ((double)p.Tag >= totalMem * MaxUtilizationPct)
                    {
                        p.Color = Color.Red;
                        p.BorderWidth = 2;
                        r.RuleResult = RuleResultEnum.Fail;
                    }
                }
            });

            Rules.Add(r);
        }

        private enum RuleResultEnum
        {
            NotRun, Fail, Warn, Pass, CountersUnavailable, Other
        }

        private class RuleCounter
        {
            public string Path;
            public bool ShowInChart;
            public bool HighlightInChart = true;
            public Series ChartSeries = null;
            public Color? CounterColor = null;
            public RuleCounter(string Path, bool ShowInChart = true, bool HighlightInChart = false, Color? CounterColor = null)
            {
                this.Path = Path;
                this.ShowInChart = ShowInChart;
                this.HighlightInChart = HighlightInChart;
                this.CounterColor = CounterColor;
            }
        }

        private class Rule : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string Category { get; set; } = "";
            public string ResultDescription { get; set; } = "";

            public List<RuleCounter> Counters = new List<RuleCounter>();
            public List<Series> CustomSeries = new List<Series>();

            private RuleResultEnum ruleResult = RuleResultEnum.NotRun;
            [Browsable(false)]
            public RuleResultEnum RuleResult { get { return ruleResult; } set { OnPropertyChanged("RuleResultImg"); ruleResult = value; } }

                        
            private Action ruleFunction = null;
            [Browsable(false)]
            public Action RuleFunction { get { return ruleFunction; } set { ruleFunction = value; } }

            public Image RuleResultImg
            {
                get
                {                   
                    switch (RuleResult)
                    {
                        case RuleResultEnum.CountersUnavailable:
                            return Properties.Resources.RuleCountersUnavailable;
                        case RuleResultEnum.Fail:
                            return Properties.Resources.RuleFail;
                        case RuleResultEnum.NotRun:
                            return Properties.Resources.RuleNotRun;
                        case RuleResultEnum.Other:
                            return Properties.Resources.RuleOther;
                        case RuleResultEnum.Pass:
                            return Properties.Resources.RulePass;
                        case RuleResultEnum.Warn:
                            return Properties.Resources.RuleWarn;
                    }
                    return null;
                }
            }

            protected void OnPropertyChanged(string name)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }
        }
    }
}
