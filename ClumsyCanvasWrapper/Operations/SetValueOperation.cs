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

        private byte option;
        private byte dictIndex;
        private string value;
        private short objIndex;

        public SetValueOperation(SetValueOperationEnum key, string value)
        {
            option = 0;
            dictIndex = (byte)enumArray.IndexOf(key);
            this.value = value;
        }

        public SetValueOperation(SetValueOperationEnum key, short objIndex)
        {
            option = 1;
            dictIndex = (byte)enumArray.IndexOf(key);
            this.objIndex = objIndex;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteByte(array, info, option);
            WriteByte(array, info, dictIndex);

            if (option == 0)
            {
                WriteString(array, info, value);
            }
            else
            {
                WriteShort(array, info, objIndex);
            }
        }

        public override int GetLength()
        {
            if (option == 0)
                return 2 + GetStringLength(value);
            else
                return 4;
        }
    }
}
