﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSASDiag.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SSASDiag.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] ASProfilerTraceImporterCmd {
            get {
                object obj = ResourceManager.GetObject("ASProfilerTraceImporterCmd", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;Backup xmlns=&quot;http://schemas.microsoft.com/analysisservices/2003/engine&quot;&gt;
        ///  &lt;Object&gt;
        ///    &lt;DatabaseID/&gt;
        ///  &lt;/Object&gt;
        ///  &lt;File/&gt;
        ///  &lt;AllowOverwrite/&gt;
        ///&lt;/Backup&gt;.
        /// </summary>
        internal static string BackupDbXMLA {
            get {
                return ResourceManager.GetString("BackupDbXMLA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;configuration&gt;
        ///&lt;configSections&gt;
        ///    &lt;sectionGroup name=&quot;userSettings&quot; type=&quot;System.Configuration.UserSettingsGroup, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089&quot; &gt;
        ///        &lt;section name=&quot;SSASDiag.Properties.Settings&quot; type=&quot;System.Configuration.ClientSettingsSection, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089&quot; allowExeDefinition=&quot;MachineToLocalUser&quot; requirePermission=&quot;false&quot; /&gt;
        ///    &lt;/sectionGroup&gt;
        ///&lt;/conf [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Config {
            get {
                return ResourceManager.GetString("Config", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF  NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N&apos;&lt;dbname/&gt;&apos;)
        ///CREATE DATABASE [&lt;dbname/&gt;]
        /// CONTAINMENT = NONE
        /// ON  PRIMARY 
        ///( NAME = N&apos;test&apos;, FILENAME = N&apos;&lt;mdfpath/&gt;&apos; , SIZE = 4096KB , FILEGROWTH = 1024KB )
        /// LOG ON 
        ///( NAME = N&apos;test_log&apos;, FILENAME = N&apos;&lt;ldfpath/&gt;&apos; , SIZE = 1024KB , FILEGROWTH = 10%)
        ///.
        /// </summary>
        internal static string CreateDBSQLScript {
            get {
                return ResourceManager.GetString("CreateDBSQLScript", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;Batch xmlns=&quot;http://schemas.microsoft.com/analysisservices/2003/engine&quot; xmlns:soap=&quot;http://schemas.xmlsoap.org/soap/envelope/&quot;&gt;
        ///	&lt;Create xmlns=&quot;http://schemas.microsoft.com/analysisservices/2003/engine&quot;&gt;
        ///		&lt;ObjectDefinition&gt;
        ///			&lt;Trace&gt;
        ///				&lt;LogFileName/&gt;
        ///				&lt;ID/&gt;
        ///				&lt;LogFileSize/&gt;
        ///				&lt;LogFileRollover/&gt;
        ///				&lt;Name/&gt;
        ///				&lt;AutoRestart/&gt;
        ///				&lt;StartTime/&gt;
        ///				&lt;StopTime/&gt;
        ///				&lt;LogFileAppend&gt;false&lt;/LogFileAppend&gt;
        ///				&lt;Events&gt;
        ///					&lt;Event&gt;
        ///						&lt;EventID&gt;15&lt;/EventID&gt;
        ///						&lt;Columns&gt;
        ///							&lt;Colu [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DbsCapturedTraceStartXMLA {
            get {
                return ResourceManager.GetString("DbsCapturedTraceStartXMLA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select 
        ///RowNumber, b.Duration, b.EventClass, EventClassName, EventSubClass, EventSubClassName, b.StartTime, CurrentTime, convert(nvarchar(4000), TextData) TextData, 
        ///b.ConnectionID, DatabaseName, NTUserName, NTDomainName, SessionID, NTCanonicalUserName, SPID, ServerName, ActivityID, RequestID, CPUTime, IntegerData, Error, ClientProcessID, 
        ///ApplicationName, SessionType, ObjectID, ObjectType, ObjectName, ObjectPath, ObjectReference,
        ///convert(nvarchar(4000), RequestParameters) RequestParameters, convert(nva [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DrillThroughQueryAllRowsForQueryOrCommand {
            get {
                return ResourceManager.GetString("DrillThroughQueryAllRowsForQueryOrCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Dump {
            get {
                object obj = ResourceManager.GetObject("Dump", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N&apos;[dbo].[ProfilerEventClass]&apos;) AND type in (N&apos;U&apos;)) DROP TABLE [dbo].[ProfilerEventClass]; CREATE TABLE [dbo].[ProfilerEventClass]([EventClassID] [int] NOT NULL, [Name] [nvarchar](50) NULL, [Description] [nvarchar](500) NULL, CONSTRAINT [PK_ProfilerEventClass] PRIMARY KEY CLUSTERED ([EventClassID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMAR [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string EventClassSubClassTablesScript {
            get {
                return ResourceManager.GetString("EventClassSubClassTablesScript", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap heart {
            get {
                object obj = ResourceManager.GetObject("heart", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap helpimg {
            get {
                object obj = ResourceManager.GetObject("helpimg", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;sqlpp_request&gt;&lt;clientid&gt;dpriver-9094-8133-2031&lt;/clientid&gt;&lt;dbvendor&gt;mdx&lt;/dbvendor&gt;&lt;outputfmt&gt;html2&lt;/outputfmt&gt;&lt;inputsql&gt;&lt;/inputsql&gt;&lt;formatoptions&gt;&lt;keywordcs&gt;Uppercase&lt;/keywordcs&gt;&lt;tablenamecs&gt;Lowercase&lt;/tablenamecs&gt;&lt;columnnamecs&gt;Lowercase&lt;/columnnamecs&gt;&lt;functioncs&gt;InitCap&lt;/functioncs&gt;&lt;datatypecs&gt;Uppercase&lt;/datatypecs&gt;&lt;variablecs&gt;Unchanged&lt;/variablecs&gt;&lt;aliascs&gt;Unchanged&lt;/aliascs&gt;&lt;quotedidentifiercs&gt;Unchanged&lt;/quotedidentifiercs&gt;&lt;identifiercs&gt;Lowercase&lt;/identifiercs&gt;&lt;lnbrwithcomma&gt;after&lt;/lnbrwithcomma&gt;&lt;liststy [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MDXConversionPostData {
            get {
                return ResourceManager.GetString("MDXConversionPostData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon Microsoft_107 {
            get {
                object obj = ResourceManager.GetObject("Microsoft_107", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon ntshrui_3029 {
            get {
                object obj = ResourceManager.GetObject("ntshrui_3029", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap package_new {
            get {
                object obj = ResourceManager.GetObject("package_new", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap play {
            get {
                object obj = ResourceManager.GetObject("play", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap play_half_lit {
            get {
                object obj = ResourceManager.GetObject("play_half_lit", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap play_lit {
            get {
                object obj = ResourceManager.GetObject("play_lit", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;Batch xmlns=&quot;http://schemas.microsoft.com/analysisservices/2003/engine&quot; xmlns:soap=&quot;http://schemas.xmlsoap.org/soap/envelope/&quot;&gt;
        ///	&lt;Create xmlns=&quot;http://schemas.microsoft.com/analysisservices/2003/engine&quot;&gt;
        ///		&lt;ObjectDefinition&gt;
        ///			&lt;Trace&gt;
        ///				&lt;LogFileName/&gt;
        ///				&lt;ID/&gt;
        ///				&lt;LogFileSize/&gt;&lt;LogFileRollover/&gt;
        ///				&lt;Name/&gt;
        ///				&lt;AutoRestart/&gt;&lt;StartTime/&gt;&lt;StopTime/&gt;&lt;LogFileAppend&gt;true&lt;/LogFileAppend&gt;		&lt;Events&gt;&lt;Event&gt;&lt;EventID&gt;15&lt;/EventID&gt;&lt;Columns&gt;&lt;ColumnID&gt;32&lt;/ColumnID&gt;&lt;ColumnID&gt;1&lt;/ColumnID&gt;&lt;ColumnID&gt;25&lt;/ColumnI [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ProfilerTraceStartWithQuerySubcubeEventsXMLA {
            get {
                return ResourceManager.GetString("ProfilerTraceStartWithQuerySubcubeEventsXMLA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;Batch xmlns=&quot;http://schemas.microsoft.com/analysisservices/2003/engine&quot; xmlns:soap=&quot;http://schemas.xmlsoap.org/soap/envelope/&quot;&gt;
        ///	&lt;Create xmlns=&quot;http://schemas.microsoft.com/analysisservices/2003/engine&quot;&gt;
        ///		&lt;ObjectDefinition&gt;
        ///			&lt;Trace&gt;
        ///				&lt;LogFileName/&gt;
        ///				&lt;ID/&gt;
        ///				&lt;LogFileSize/&gt;&lt;LogFileRollover/&gt;
        ///				&lt;Name/&gt;
        ///				&lt;AutoRestart/&gt;&lt;StartTime/&gt;&lt;StopTime/&gt;&lt;LogFileAppend&gt;true&lt;/LogFileAppend&gt;
        ///				&lt;Events&gt;
        ///					&lt;Event&gt;
        ///						&lt;EventID&gt;15&lt;/EventID&gt;
        ///						&lt;Columns&gt;
        ///							&lt;ColumnID&gt;32&lt;/ColumnID&gt;
        ///					 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ProfilerTraceStartXMLA {
            get {
                return ResourceManager.GetString("ProfilerTraceStartXMLA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;Delete xmlns=&quot;http://schemas.microsoft.com/analysisservices/2003/engine&quot; xmlns:soap=&quot;http://schemas.xmlsoap.org/soap/envelope/&quot;&gt;
        ///	&lt;Object&gt;
        ///		&lt;TraceID/&gt;
        ///	&lt;/Object&gt;
        ///&lt;/Delete&gt;.
        /// </summary>
        internal static string ProfilerTraceStopXMLA {
            get {
                return ResourceManager.GetString("ProfilerTraceStopXMLA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Progress {
            get {
                object obj = ResourceManager.GetObject("Progress", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select * from
        ///(select min(CurrentTime) as [Trace Start Time], max(CurrentTime) as [Trace End Time] from [Table]) aa,
        ///(select count(*)[Queries Started] from [Table] where EventClass = 9) a,
        ///(select count(*)[Queries Completed] from [Table] where EventClass = 10) b,
        ///(select count(*)[Incomplete Queries] from [Table_QueriesAndCommandsIncludingIncomplete] where EventClass = -10) bb,
        ///(select sum(Duration)[Total Query Duration], avg(Duration)[Average Query Duration] from [Table] where EventClass = 10) e,
        ///(sel [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryBasicTraceSummary {
            get {
                return ResourceManager.GetString("QueryBasicTraceSummary", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select top 100 a.StartRow, a.EndRow, a.Duration, round(b.[FE], 2) [Formula Engine %], round(b.[SE], 2) [Storage Engine %], convert(nvarchar(4000), a.StartTime, 21) StartTime, convert(nvarchar(4000), c.CurrentTime, 21) EndTime, DatabaseName, convert(nvarchar(4000), c.TextData) TextData, a.ConnectionID, c.NTUserName, c.NTDomainName, c.ApplicationName, c.ClientProcessID, c.SPID, convert(nvarchar(4000), c.RequestParameters) RequestParameters, convert(nvarchar(4000), c.RequestProperties) RequestProperties
        ///from  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryFESEStats {
            get {
                return ResourceManager.GetString("QueryFESEStats", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select top 100 a.StartRow, case when a.EndRow is null then &apos;Command Incomplete&apos; else convert(nvarchar(4000), a.EndRow) end EndRow,
        ///a.Duration, convert(nvarchar(4000), a.StartTime, 21) StartTime, 
        ///case when a.RequestCompleted = 0 then &apos;Command Incomplete&apos; else convert(nvarchar(4000), b.CurrentTime, 21) end EndTime,
        ///b.DatabaseName, convert(nvarchar(4000), TextData) TextData, a.ConnectionID, b.NTUserName, b.NTDomainName, b.ApplicationName, b.ClientProcessID, b.SPID, convert(nvarchar(4000), RequestParameters [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryLongestRunningCommands {
            get {
                return ResourceManager.GetString("QueryLongestRunningCommands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select top 100 a.StartRow, case when a.EndRow is null then &apos;Query Incomplete&apos; else convert(nvarchar(4000), a.EndRow) end EndRow, 
        ///a.Duration, convert(nvarchar(4000), a.StartTime, 21) StartTime, 
        ///case when a.RequestCompleted = 0 then &apos;Query Incomplete&apos; else convert(nvarchar(4000), b.CurrentTime, 21) end EndTime,
        ///b.DatabaseName, convert(nvarchar(4000), TextData) TextData, a.ConnectionID, b.NTUserName, b.NTDomainName, b.ApplicationName, b.ClientProcessID, b.SPID, convert(nvarchar(4000), RequestParameters) R [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryLongestRunningQueries {
            get {
                return ResourceManager.GetString("QueryLongestRunningQueries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select top 100 max(b.RowNumber) LastRow, count(*) as [Execution Count], sum(a.Duration) [Total Duration], convert(nvarchar(4000), b.TextData) TextData, sum(RequestCompleted) [Requests Completed]
        ///from [Table_QueriesAndCommandsIncludingIncomplete] a, 
        ///[Table_v] b
        ///where a.EventClass in (16, -15) and (b.RowNumber = a.EndRow or (b.RowNumber = a.StartRow and a.EndRow is null)) and b.StartTime = a.StartTime and b.ConnectionID = a.ConnectionID
        ///group by convert(nvarchar(4000), b.TextData)
        ///having sum(a.Duration) [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryMostCollectivelyExpensiveCommands {
            get {
                return ResourceManager.GetString("QueryMostCollectivelyExpensiveCommands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select top 100 count(*) as ExecutionCount, sum(Duration) TotalDuration, EventClass, EventClassName, EventSubclass, EventSubclassName, TextData
        ///from [Table_v]
        ///where EventClass not in (39, 42, 10, 16) -- skip ExistingSession and Notification durations, query/command end events
        ///group by TextData, EventClass, EventClassName, EventSubclass, EventSubclassName
        ///having sum(Duration) &gt; 0
        ///order by sum(Duration) desc.
        /// </summary>
        internal static string QueryMostCollectivelyExpensiveEvents {
            get {
                return ResourceManager.GetString("QueryMostCollectivelyExpensiveEvents", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select top 100 max(b.RowNumber) LastRow, count(*) as [Execution Count], sum(a.Duration) [Total Duration], convert(nvarchar(4000), b.TextData) TextData, sum(RequestCompleted) [Requests Completed], max(b.RowNumber) LastRow
        ///from [Table_QueriesAndCommandsIncludingIncomplete] a, 
        ///[Table_v] b
        ///where a.EventClass in (10, -9) and (b.RowNumber = a.EndRow or (b.RowNumber = a.StartRow and a.EndRow is null)) and b.StartTime = a.StartTime and b.ConnectionID = a.ConnectionID
        ///group by convert(nvarchar(4000), b.TextData [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryMostCollectivelyExpensiveQueries {
            get {
                return ResourceManager.GetString("QueryMostCollectivelyExpensiveQueries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -- Returns a ranking of all queries and commands completed within the trace by the number of other queries and commands that overlap the original query or command in question.
        ///-- The highest ranked among these may be likely offending queries or commands that affected other queries or commands on the server.
        ///-- Using the QueriesAndCommandIncludingIncomplete view exposed in the underlying db, it includes requests that didn&apos;t complete even without their end events, calculating duration
        ///select top 100 b.[Ove [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryMostImpactfulQueriesCommands {
            get {
                return ResourceManager.GetString("QueryMostImpactfulQueriesCommands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -- These sessions were active when the trace started and never showed any begin or end events.
        ///-- This can indicate the sessions were already active and remained active without ever completing.
        ///-- If heavy load isn&apos;t otherwise detectable in a trace, check with users of possible runaway sessions.
        ///select &apos;Existing Session&apos; as EventClassName, convert(nvarchar(4000), StartTime, 21) StartTime, Duration as [Session Duration at TraceStart], ConnectionID, NTUserName, NTDomainName, DatabaseName, SPID, TextData as [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryPossibleRunawaySessions {
            get {
                return ResourceManager.GetString("QueryPossibleRunawaySessions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select  StartRow, &apos;Request Incomplete&apos; EndRow, a.Duration, a.StartTime, &apos;Request Incomplete&apos; EndTime, DatabaseName, convert(nvarchar(4000), TextData) TextData, a.ConnectionID, NTUserName, NTDomainName, ApplicationName, ClientProcessID, SPID, convert(nvarchar(4000), RequestParameters) RequestParameters, convert(nvarchar(4000), RequestProperties) RequestProperties
        ///from [Table_QueriesAndCommandsIncludingIncomplete] a,
        ///[Table] b
        ///where EndRow is null and b.RowNumber = a.StartRow.
        /// </summary>
        internal static string QueryQueriesCommandsNotCompleted {
            get {
                return ResourceManager.GetString("QueryQueriesCommandsNotCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select a.RowNumber, EventClass, EventClassName, a.ConnectionID, Duration, convert(nvarchar(4000), a.StartTime, 21) StartTime, convert(nvarchar(4000), a.CurrentTime, 21) CurrentTime, upper(sys.fn_varbintohexstr(convert(varbinary(8), Error))) [Error Code], DatabaseName, convert(nvarchar(4000), TextData) TextData, NTUserName, NTDomainName, ApplicationName, ClientProcessID, SPID, convert(nvarchar(4000), RequestParameters) RequestParameters, convert(nvarchar(4000), RequestProperties) RequestProperties
        ///from [Tab [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string QueryQueriesCommandsWithErrors {
            get {
                return ResourceManager.GetString("QueryQueriesCommandsWithErrors", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] ResourcesZip {
            get {
                object obj = ResourceManager.GetObject("ResourcesZip", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap RuleCountersUnavailable {
            get {
                object obj = ResourceManager.GetObject("RuleCountersUnavailable", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap RuleFail {
            get {
                object obj = ResourceManager.GetObject("RuleFail", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap RuleNotRun {
            get {
                object obj = ResourceManager.GetObject("RuleNotRun", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap RuleOther {
            get {
                object obj = ResourceManager.GetObject("RuleOther", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap RulePass {
            get {
                object obj = ResourceManager.GetObject("RulePass", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap RuleWarn {
            get {
                object obj = ResourceManager.GetObject("RuleWarn", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Settings {
            get {
                object obj = ResourceManager.GetObject("Settings", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap stop_button_half_lit {
            get {
                object obj = ResourceManager.GetObject("stop_button_half_lit", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap stop_button_lit {
            get {
                object obj = ResourceManager.GetObject("stop_button_lit", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap stop_button_th {
            get {
                object obj = ResourceManager.GetObject("stop_button_th", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
