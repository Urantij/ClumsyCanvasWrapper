using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class FillOperation : BaseOperation
    {
        public enum FillOperationEnum
        {
            nonzero,
            evenodd
        }

        private static List<FillOperationEnum> enumArray = new List<FillOperationEnum>()
        {
            FillOperationEnum.nonzero,
            FillOperationEnum.evenodd,
        };

        private FillOperationEnum value;

        public FillOperation(FillOperationEnum value)
        {
            this.value = value;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            byte index = (byte)enumArray.IndexOf(value);

            WriteByte(array, info, index);
        }

        public override int GetLength()
        {
            return 1;
        }
    }
}
