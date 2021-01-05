using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class FillStyleOperation : BaseOperation
    {
        private byte option;

        private string color;

        private short index;

        public FillStyleOperation(string color)
        {
            this.color = color;
            this.option = 0;
        }

        public FillStyleOperation(short index)
        {
            this.index = index;

            this.option = 2;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteByte(array, info, option);

            switch (option)
            {
                case 0:

                    WriteString(array, info, color);
                    return;
                case 1:
                    throw new Exception("FillStyleOperation DontCare");
                case 2:

                    WriteShort(array, info, index);
                    return;

                default:
                    throw new Exception("FillStyleOperation Clown");
            }
        }

        public override int GetLength()
        {
            switch (option)
            {
                case 0:
                    return 1 + GetStringLength(color);
                case 1:
                    throw new Exception("FillStyleOperation DontCare");
                case 2:
                    return 1 + 2;

                default:
                    throw new Exception("FillStyleOperation Clown");
            }
        }
    }
}
