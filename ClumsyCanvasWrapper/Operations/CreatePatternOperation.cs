using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class CreatePatternOperation : BaseOperation
    {
        public enum RepeatEnum
        {
            repeat,
            repeat_x,
            repeat_y,
            no_repeat,
        }

        private static List<RepeatEnum> enumArray = new List<RepeatEnum>()
        {
            RepeatEnum.repeat,
            RepeatEnum.repeat_x,
            RepeatEnum.repeat_y,
            RepeatEnum.no_repeat,
        };

        private short fromObjectIndex;
        private short toObjectIndex;
        private RepeatEnum repeat;

        public CreatePatternOperation(short fromObjectIndex, short toObjectIndex, RepeatEnum repeat)
        {
            this.fromObjectIndex = fromObjectIndex;
            this.toObjectIndex = toObjectIndex;
            this.repeat = repeat;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteShort(array, info, fromObjectIndex);
            WriteShort(array, info, toObjectIndex);

            byte repeatIndex = (byte)enumArray.IndexOf(repeat);

            WriteByte(array, info, repeatIndex);
        }

        public override int GetLength()
        {
            return 2 * 2 + 1;
        }
    }
}
