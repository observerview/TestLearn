using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WirelessCommon;
using WirelessCommon.Visa;
using NLog;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;

namespace xwx.TDP.Common.Instruments
{
    public class MessageInstruments : MessageBaseInstrument
    {
        private static  Logger nlogger = LogManager.GetLogger("Visa");

        private bool _isOutputDisable = false;
        private bool _isOutputVisaCommandToConsole = false;
        private bool _isOutputVisaCommandToNLogger = true;
        private bool _isTraceShowInstrResourceName = false;

        public override bool Initialize(string resource)
        {
            return Initialize(resource, "");
        }
        public override bool Initialize(string resource, string remoteServer)
        {
            if (!_isOutputDisable)
            {
                MessageReadWrite += new MessageReadWriteEventHandler(MessageInstruments_MessageReadWrite);
            }
            return base.Initialize(resource, remoteServer);
        }

        private void MessageInstruments_MessageReadWrite(object sender, MessageReadWriteEventArgs args)
        {
            string instrumentDisplayName = this.GetType().Name;
            object[] attrs = this.GetType().GetCustomAttributes(typeof(DisplayNameAttribute), false);
            if (attrs != null && attrs.Length > 0)
            {
                instrumentDisplayName = (attrs[0] as DisplayNameAttribute).DisplayName;
            }
            string message = string.Format("{0}{1} {2} {3}",
                                           instrumentDisplayName,
                                           this._isTraceShowInstrResourceName ? string.Format("({0})", this.ResourceName) : string.Empty,
                                           args.Direction == MessageReadWriteDirection.Read ? "==>" : "<==",
                                           args.DataType == MessageReadWriteDataType.String ? args.Text : string.Format("Byte Array, Length: {0}", args.Data.Length.ToString()));
            if (_isOutputVisaCommandToConsole)
            {
                Console.WriteLine(message);
            }
            if (_isOutputVisaCommandToNLogger)
            {
                nlogger.Trace(message);
            }
        }

        public string InstrumentDisplayName
        {
            get
            {
                string displayName = GetType().Name;
                object[] atts = GetType().GetCustomAttributes(typeof(DisplayNameAttribute), true);
                foreach (object att in atts)
                {
                    if (att is DisplayNameAttribute)
                    {
                        displayName = (att as DisplayNameAttribute).DisplayName;
                    }
                }
                return displayName;
            }
        }
        public string InstrumentFirmwareVersion
        {
            get
            {
                string firmwareVersion = string.Empty;
                object[] atts = GetType().GetCustomAttributes(typeof(FirmwareVersionAttribute), true);
                foreach (object att in atts)
                {
                    if (att is FirmwareVersionAttribute)
                    {
                        firmwareVersion = (att as FirmwareVersionAttribute).FirmwareVersion;
                    }
                }
                return firmwareVersion;
            }
        }
        public string InstrumentDescription
        {
            get
            {
                string description = "N/A";
                object[] atts = this.GetType().GetCustomAttributes(typeof(DescriptionAttribute), true);
                foreach (object att in atts)
                {
                    if (att is DescriptionAttribute)
                    {
                        description = (att as DescriptionAttribute).Description;
                    }
                }
                return description;
            }
        }


        public class InstrumentFactory
        {
            public static void GetSupportedInstruments<T>(out List<InstrumentInfomation<T>> instrs) where T : MessageInstruments
            {
                instrs = new List<InstrumentInfomation<T>>();
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                Type[] exportedTypes = currentAssembly.GetExportedTypes();
                foreach (Type exportedType in exportedTypes)
                {
                    if (exportedType.IsSubclassOf(typeof(T)) && !exportedType.IsAbstract)
                    {
                        object[] atts = null;
                        InstrumentInfomation<T> ii = new InstrumentInfomation<T>(exportedType.Name);
                        ii.FullName = exportedType.FullName;


                        // Display Name
                        atts = null;
                        ii.DisplayName = exportedType.Name;
                        atts = exportedType.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                        if (atts != null && atts.Length != 0)
                        {
                            ii.DisplayName = (atts[0] as DisplayNameAttribute).DisplayName;
                        }

                        // FirmwareVersion Name
                        atts = null;
                        atts = exportedType.GetCustomAttributes(typeof(FirmwareVersionAttribute), false);
                        if (atts != null && atts.Length != 0)
                        {
                            ii.FirmwareVersion = (atts[0] as FirmwareVersionAttribute).FirmwareVersion;
                        }

                        // FirmwareVersion Name
                        atts = null;
                        atts = exportedType.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (atts != null && atts.Length != 0)
                        {
                            ii.Description = (atts[0] as DescriptionAttribute).Description;
                        }

                        instrs.Add(ii);
                    }
                }
            }
            private static T CreateInstanceFromFullName<T>(string fullName) where T : MessageInstruments
            {
                T instr = null;
                //string typeFullName = string.Format("{0}", fullName);

                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                object objTemp = null;
                try
                {
                    objTemp = currentAssembly.CreateInstance(fullName, true);
                }
                catch (Exception)
                {
                    objTemp = null;
                }

                if (objTemp != null && objTemp is T)
                {
                    instr = objTemp as T;
                }

                return instr;
            }
           
            public static T CreateInstance<T>(string instrInternalName) where T : MessageInstruments
            {
                T instr = null;
                List<InstrumentInfomation<T>> instrs = new List<InstrumentInfomation<T>>();
                InstrumentFactory.GetSupportedInstruments<T>(out instrs);
                foreach (InstrumentInfomation<T> ii in instrs)
                {
                    if (string.Compare(ii.InternalName, instrInternalName, false) == 0)
                    {
                        instr = InstrumentFactory.CreateInstanceFromFullName<T>(ii.FullName);
                        break;
                    }
                }
                return instr;
            }
            
            public static T CreateInstanceFromDisplayName<T>(string instrDisplayName) where T : MessageInstruments
            {
                T instr = null;
                List<InstrumentInfomation<T>> instrs = new List<InstrumentInfomation<T>>();
                InstrumentFactory.GetSupportedInstruments<T>(out instrs);
                foreach (InstrumentInfomation<T> ii in instrs)
                {
                    if (string.Compare(ii.DisplayName, instrDisplayName, false) == 0)
                    {
                        instr = InstrumentFactory.CreateInstanceFromFullName<T>(ii.FullName);
                        break;
                    }
                }
                return instr;
            }
        }

        public class InstrumentInfomation<T> where T : MessageInstruments
        {
            private string _internalName = string.Empty;
            private string _displayName = string.Empty;
            private string _firmwareVersion = string.Empty;
            private string _description = string.Empty;
            private string _fullName = string.Empty;

            public InstrumentInfomation(string internalName)
            {
                this._internalName = internalName;
            }

            public string InternalName
            {
                get { return this._internalName; }
                set { this._internalName = value; }
            }

            public string DisplayName
            {
                get { return this._displayName; }
                set { this._displayName = value; }
            }
            public string FirmwareVersion
            {
                get { return this._firmwareVersion; }
                set { this._firmwareVersion = value; }
            }
            public string Description
            {
                get { return this._description; }
                set { this._description = value; }
            }

            public string FullName
            {
                get { return _fullName; }
                set { _fullName = value; }
            }

            public override string ToString()
            {
                return this.DisplayName;
            }
        }

        public class InstrumentInfomationConverter<T> : TypeConverter
       where T : MessageInstruments
        {
            private object _oldValue;

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return (destinationType == typeof(string)) || base.CanConvertTo(context, destinationType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (!(value is string))
                {
                    return base.ConvertFrom(context, culture, value);
                }

                string displayName = value as string;
                List<InstrumentInfomation<T>> instrs = new List<InstrumentInfomation<T>>();
                InstrumentFactory.GetSupportedInstruments<T>(out instrs);
                foreach (InstrumentInfomation<T> instr in instrs)
                {
                    if (string.Compare(displayName, instr.DisplayName, false) == 0)
                    {
                        return instr;
                    }
                }
                return this._oldValue as InstrumentInfomation<T>;
            }
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                this._oldValue = value;
                if (destinationType == typeof(string))
                {
                    return value.ToString();
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }


        public class FirmwareVersionAttribute : Attribute
        {

            public static readonly FirmwareVersionAttribute Default = new FirmwareVersionAttribute();
            private string _firmwareVersion;

            public FirmwareVersionAttribute() : this(string.Empty)
            {
            }

            public FirmwareVersionAttribute(string firmwareVersion)
            {
                this._firmwareVersion = firmwareVersion;
            }

            public virtual string FirmwareVersion
            {
                get { return this.FirmwareVersionValue; }
            }


            protected string FirmwareVersionValue
            {
                get { return this._firmwareVersion; }
                set { this._firmwareVersion = value; }
            }

            public override bool Equals(object obj)
            {
                if (obj == this)
                {
                    return true;
                }
                FirmwareVersionAttribute attribute = obj as FirmwareVersionAttribute;
                return (attribute != null) && (attribute.FirmwareVersion == this.FirmwareVersion);
            }

            public override int GetHashCode()
            {
                return this.FirmwareVersion.GetHashCode();
            }

            public override bool IsDefaultAttribute()
            {
                return this.Equals(Default);
            }
        }
    }
}
