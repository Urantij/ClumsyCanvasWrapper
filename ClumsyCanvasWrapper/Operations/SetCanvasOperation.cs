using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class SetCanvasOperation : BaseOperation
    {
        private short index;

        public SetCanvasOperation(short index)
        {
            this.index = index;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteShort(array, info, index);
        }

        public override int GetLength()
        {
            return 2;
        }
    }
}
