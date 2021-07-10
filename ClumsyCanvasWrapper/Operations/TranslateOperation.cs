using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class TranslateOperation : BaseOperation
    {
        private short x;
        private short y;

        public TranslateOperation(short x, short y)
        {
            this.x = x;
            this.y = y;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteShort(array, info, x);
            WriteShort(array, info, y);
        }

        public override int GetLength()
        {
            return 2 + 2;
        }
    }
}
