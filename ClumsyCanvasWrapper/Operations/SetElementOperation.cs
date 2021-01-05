using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class SetElementOperation : BaseOperation
    {
        private short index;
        private string elementId;

        public SetElementOperation(short index, string elementId)
        {
            this.index = index;
            this.elementId = elementId;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteShort(array, info, index);
            WriteString(array, info, elementId);
        }

        public override int GetLength()
        {
            return 2 + GetStringLength(elementId);
        }
    }
}
