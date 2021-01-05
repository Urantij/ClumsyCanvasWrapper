using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class SetCustomValueOperation : BaseOperation
    {
        private string key;
        private string value;

        public SetCustomValueOperation(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteString(array, info, key);
            WriteString(array, info, value);
        }

        public override int GetLength()
        {
            return GetStringLength(key) + GetStringLength(value);
        }
    }
}
