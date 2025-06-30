using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WirelessCommon.Visa
{
    public class MessageReadWriteEventArgs : EventArgs
    {
        private byte[] _data;
        private MessageReadWriteDataType _dataType;
        private MessageReadWriteDirection _direction;
        private string _text;

        public MessageReadWriteEventArgs(MessageReadWriteDirection direction, string text)
        {
            _text = string.Empty;
            _direction = MessageReadWriteDirection.Write;
            _data = new byte[0];
            _direction = direction;
            _text = text;
            _dataType = MessageReadWriteDataType.String;
        }

        public MessageReadWriteEventArgs(MessageReadWriteDirection direction, byte[] data)
        {
            _text = string.Empty;
            _direction = MessageReadWriteDirection.Write;
            _data = new byte[0];
            _direction = direction;
            _data = new byte[data.Length];
            Array.Copy(data, _data, data.Length);
            _dataType = MessageReadWriteDataType.ByteArray;
        }
        public byte[] Data
        {
            get
            {
                return _data;
            }
        }

        public MessageReadWriteDataType DataType
        {
            get
            {
                return _dataType;
            }
        }

        public MessageReadWriteDirection Direction
        {
            get
            {
                return _direction;
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
        }
    }
}
