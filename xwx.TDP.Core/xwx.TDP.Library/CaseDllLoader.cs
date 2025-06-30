using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestManager.Extern;
using TestManager.Utility;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Library
{
    public class CaseDllLoader
    {
        private string baseDir;

        private Dictionary<string, List<CaseAssembly>> _cases = new Dictionary<string, List<CaseAssembly>>();
        public Dictionary<string, List<CaseAssembly>> Cases 
        {
            get 
            {
                return _cases; 
            } 
        }
        public string BaseDir
        {
            get
            {
                return baseDir;
            }
        }

        public CaseDllLoader()
        {
            baseDir = Assembly.GetEntryAssembly().Location;
            baseDir = baseDir.Substring(0, baseDir.LastIndexOf('\\'));
            baseDir += "\\applications\\";
        }

        public void Load()
        {
            DirectoryInfo dir = new DirectoryInfo(baseDir);
            ReadCasesRecursive(dir);
        }

        private void ReadCasesRecursive(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files) 
            {
                if(file.Extension.ToLower() != ".dll" || file.Name.StartsWith("xwx.TDP.TestCases"))
                {
                    continue;
                }

                try
                {
                    Assembly assembly = Assembly.LoadFile(file.FullName);
                    Type[] exportedTypes = assembly.GetExportedTypes();
                    foreach (Type type in exportedTypes)
                    {
                        if(!type.IsInterface && !type.IsAbstract && type.IsSubclassOf(typeof(CoreCase)))
                        {
                            CaseAssembly caseAssembly = new CaseAssembly();
                            caseAssembly.asmLib = assembly;
                            caseAssembly.FullName = type.FullName;
                            object[] customAttributes = type.GetCustomAttributes(typeof(CategoryAttribute), false);
                            if (customAttributes.Length > 0)
                            {
                                CategoryAttribute categoryAttribute = (CategoryAttribute)customAttributes[0];
                                caseAssembly.Category = categoryAttribute.Category;
                            }
                            customAttributes = type.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                            if (customAttributes.Length > 0)
                            {
                                DisplayNameAttribute displayNameAttribute = (DisplayNameAttribute)customAttributes[0];
                                caseAssembly.DisplayName = displayNameAttribute.DisplayName;
                            }
                            customAttributes = type.GetCustomAttributes(typeof(DisplayColorAttribute), false);
                            if (customAttributes.Length > 0)
                            {
                                DisplayColorAttribute displayColorAttribute = (DisplayColorAttribute)customAttributes[0];
                                caseAssembly.Color = displayColorAttribute.DisplayColor;
                            }
                            customAttributes = type.GetCustomAttributes(typeof(DescriptionAttribute), false);
                            if (customAttributes.Length > 0)
                            {
                                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)customAttributes[0];
                                caseAssembly.Description = descriptionAttribute.Description;
                            }
                            if (_cases.ContainsKey(file.FullName))
                            {
                                _cases[file.FullName].Add(caseAssembly);
                                continue;
                            }
                            List<CaseAssembly> list = new List<CaseAssembly>();
                            list.Add(caseAssembly);
                            _cases.Add(file.FullName, list);
                        }
                    }
                }
                catch (Exception ex) 
                {
                    Debug.WriteLine(ex);
                }
            
            }
            DirectoryInfo[] directories = dir.GetDirectories();
            foreach (DirectoryInfo d in directories)
            {
                ReadCasesRecursive(d);
            }
        }
    }
}
