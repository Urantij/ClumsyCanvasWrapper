using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class DrawImageOperation : BaseOperation
    {
        private byte option;
        private short index;

        private short dx;
        private short dy;
        private short dWidth;
        private short dHeight;

        private short sx;
        private short sy;
        private short sWidth;
        private short sHeight;

        public DrawImageOperation(short index, short dx, short dy)
        {
            this.index = index;

            this.dx = dx;
            this.dy = dy;

            this.option = 0;
        }

        public DrawImageOperation(short index, short dx, short dy, short dWidth, short dHeight)
        {
            this.index = index;

            this.dx = dx;
            this.dy = dy;
            this.dWidth = dWidth;
            this.dHeight = dHeight;

            this.option = 1;
        }

        public DrawImageOperation(short index, short dx, short dy, short dWidth, short dHeight, short sx, short sy, short sWidth, short sHeight)
        {
            this.index = index;

            this.dx = dx;
            this.dy = dy;
            this.dWidth = dWidth;
            this.dHeight = dHeight;

            this.sx = sx;
            this.sy = sy;
            this.sWidth = sWidth;
            this.sHeight = sHeight;

            this.option = 2;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteByte(array, info, option);
            WriteShort(array, info, index);

            switch (option)
            {
                case 0:
                    WriteShort(array, info, dx);
                    WriteShort(array, info, dy);
                    return;
                case 1:
                    WriteShort(array, info, dx);
                    WriteShort(array, info, dy);

                    WriteShort(array, info, dWidth);
                    WriteShort(array, info, dHeight);
                    return;
                case 2:
                    WriteShort(array, info, sx);
                    WriteShort(array, info, sy);

                    WriteShort(array, info, sWidth);
                    WriteShort(array, info, sHeight);

                    WriteShort(array, info, dx);
                    WriteShort(array, info, dy);

                    WriteShort(array, info, dWidth);
                    WriteShort(array, info, dHeight);
                    return;

                default:
                    throw new Exception("DrawImageOperation Clown");
            }
        }

        public override int GetLength()
        {
            switch (option)
            {
                case 0:
                    return 1 + 2 + 2 * 2;
                case 1:
                    return 1 + 2 + 2 * 4;
                case 2:
                    return 1 + 2 + 2 * 8;

                default:
                    throw new Exception("DrawImageOperation Clown");
            }
        }
    }
}
