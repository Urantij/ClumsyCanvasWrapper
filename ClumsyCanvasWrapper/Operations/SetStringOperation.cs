using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class SetStringOperation : BaseOperation
    {
        private short index;
        private string value;

        public SetStringOperation(short index, string value)
        {
            this.index = index;
            this.value = value;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteShort(array, info, index);
            WriteString(array, info, value);
        }

        public override int GetLength()
        {
            return 2 + GetStringLength(value);
        }
    }
}
