using System;
using System.ComponentModel;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Library.Common;
using NationalInstruments.Visa;
using System.Threading;
using WirelessCommon.Visa;
using System.Data.SqlClient;
using System.Diagnostics;

namespace xwx.TDP.Library.DTC.Cases
{
    [Category("FPGA控制"),
     DisplayName("执行tcl命令"),
     Description("执行tcl脚本来实现跟fpga各种功能交互，这个case为配置命令执行。"),
    ]
    public class RunTCLCommands : CoreCaseCommon
    {
        #region Private Fields
        private MessageBasedSession mbSession;
        #endregion

        #region Parameter Setting
        private class ParameterSetting : ConfigBase
        {
            private string _vivadoPath = @"C:\Xilinx\Vivado\2018.3\bin\vivado.bat";
            [Category("A.参数设置组"),
             DisplayName("A01.Vivado安装路径(vivado.bat) "),
             Description("Vivado安装路径,需要包含vivado.bat"),
             DefaultValue(typeof(string), @"C:\Xilinx\Vivado\2018.3\bin\vivado.bat"),
            ]
            public string VivadoPath
            {
                get { return _vivadoPath; }
                set { _vivadoPath = value; }
            }

            
            private string[] _tclCmdStrings = { "puts \"Hello, World!\"", "" };
            [Category("A.参数设置组"),
             DisplayName("A02.TCL命令"),
             Description("TCL命令"),
             DefaultValue(typeof(string[]), "puts \"Hello, World!\""),
            ]
            public string[] TclCmdStrings
            {
                get { return _tclCmdStrings; }
                set { _tclCmdStrings = value; }
            }

            private int _cmdTimeSpan = 500;
            [Category("A.参数设置组"),
             DisplayName("A03.TCL命令发送间隔"),
             Description("VISA命令发送间隔，单位毫秒ms"),
             DefaultValue(typeof(int), "500"),
            ]
            public int CmdTimeSpan
            {
                get { return _cmdTimeSpan; }
                set { _cmdTimeSpan = value; }
            }

            //
            private string _displayName = "执行tcl命令";

            [DisplayName("Z01. 显示名称")]
            [Category("Z. 测试case通用配置")]
            [DefaultValue("执行tcl命令")]
            [Description("执行tcl命令")]
            public string DisplayName
            {
                get
                {
                    return _displayName;
                }
                set
                {
                    _displayName = (string.IsNullOrEmpty(value) ? "执行tcl命令" : value);
                }
            }
        }
        #endregion

        #region Limit Setting
        private class LimitSetting : ConfigBase
        {
            // 限制值示例
            //private ValueLimit _limitExample = new ValueLimit("[0,100] | double");
            //[Category("A.限制值组"),
            // DisplayName("A01.限制值名称"),
            // Description("限制值描述"),
            // DefaultValue(typeof(ValueLimit), "[0,100] | double"),
            //]
            //public ValueLimit LimitExample
            //{
            //    get { return _limitExample; }
            //    set { _limitExample = value; }
            //}
        }
        #endregion

        #region Parameter & Limit Getter and Setter
        private ParameterSetting _parameterSetting = new ParameterSetting();
        public override ConfigBase CaseParameterSetting
        {
            get { return _parameterSetting; }
            set
            {
                if (value != null && value is ParameterSetting)
                {
                    _parameterSetting = value as ParameterSetting;
                }
            }
        }

        private LimitSetting _limitSetting = new LimitSetting();
        public override ConfigBase CaseLimitSetting
        {
            get { return _limitSetting; }
            set
            {
                if (value != null && value is LimitSetting)
                {
                    _limitSetting = value as LimitSetting;
                }
            }
        }
        #endregion

        public string DisplayName
        {
            get
            {
                return _parameterSetting.DisplayName;
            }
            set
            {
                _parameterSetting.DisplayName = value;
            }
        }

        public RunTCLCommands() : this("执行tcl命令")
        {
        }

        public RunTCLCommands(string displayName)
        {
            DisplayName = displayName;
        }

        #region Test Case Implementation
        public override ExecOkError PreExec()
        {
            // 在此处添加测试前的准备工作
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            string vivadoPath = _parameterSetting.VivadoPath;

            // 创建进程启动信息
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe", // 启动cmd
                RedirectStandardInput = true, // 重定向标准输入
                RedirectStandardOutput = true, // 重定向标准输出
                RedirectStandardError = true, // 重定向错误输出
                UseShellExecute = false, // 禁用Shell执行
                CreateNoWindow = true // 不显示命令行窗口
            };

            // 启动进程
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;

                // 启动进程
                process.Start();

                // 异步读取标准输出和错误输出
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        LogInfo(LogInfoType.Notify, $"命令返回值: {e.Data}");
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        LogInfo(LogInfoType.Log, $"错误: {e.Data}");
                    }
                };
                // 开始异步读取
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // 向cmd发送命令
                using (var streamWriter = process.StandardInput)
                {
                    streamWriter.WriteLine($"\"{vivadoPath}\" -mode tcl");
                    streamWriter.WriteLine("puts {Start test.}");
                    Thread.Sleep(3000);
                    foreach (var command in _parameterSetting.TclCmdStrings)
                    {
                        // 发送vivado.bat命令并执行TCL指令
                        //streamWriter.WriteLine($"\"{vivadoPath}\" -mode tcl -source \"{command}\"");
                        streamWriter.WriteLine($"{command}");
                        LogInfo(LogInfoType.Notify, $"命令: {command}");
                        Thread.Sleep(_parameterSetting.CmdTimeSpan);

                    }
                }

                // 等待进程结束
                process.WaitForExit();
                // 输出结果
                if (process.ExitCode == 0)
                {
                    LogInfo(LogInfoType.Log,"所有TCL指令执行成功！");
                }
                else
                {
                    LogInfo(LogInfoType.Log, $"TCL指令执行失败，退出代码: {process.ExitCode}");
                    return ExecOkError.Error;
                }
            }

            return ExecOkError.OK;
        }

        public override ExecOkError PostExec()
        {
            // 在此处添加测试后的清理工作
            return ExecOkError.OK;
        }
        #endregion
    }
}