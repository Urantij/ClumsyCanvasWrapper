using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class ResizeCanvasOperation : BaseOperation
    {
        private short width;
        private short height;

        public ResizeCanvasOperation(short width, short height)
        {
            this.width = width;
            this.height = height;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteShort(array, info, width);
            WriteShort(array, info, height);
        }

        public override int GetLength()
        {
            return 2 * 2;
        }
    }
}
