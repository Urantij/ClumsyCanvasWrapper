﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class FillRectOperation : BaseOperation
    {
        private short x;
        private short y;
        private short width;
        private short height;

        public FillRectOperation(short x, short y, short width, short height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteShort(array, info, x);
            WriteShort(array, info, y);
            WriteShort(array, info, width);
            WriteShort(array, info, height);
        }

        public override int GetLength()
        {
            return 2 * 4;
        }
    }
}
