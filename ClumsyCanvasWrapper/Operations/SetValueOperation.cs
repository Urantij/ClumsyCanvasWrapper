using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class SetValueOperation : BaseOperation
    {
        public enum SetValueOperationEnum
        {
            globalCompositeOperation,
            fillStyle,
            filter,
        }

        private static List<SetValueOperationEnum> enumArray = new List<SetValueOperationEnum>()
        {
            SetValueOperationEnum.globalCompositeOperation,
            SetValueOperationEnum.fillStyle,
            SetValueOperationEnum.filter,
        };

        private byte index;
        private string value;

        public SetValueOperation(SetValueOperationEnum key, string value)
        {
            index = (byte)enumArray.IndexOf(key);
            this.value = value;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteByte(array, info, index);
            WriteString(array, info, value);
        }

        public override int GetLength()
        {
            return 1 + GetStringLength(value);
        }
    }
}
