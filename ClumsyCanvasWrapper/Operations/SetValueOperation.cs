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

            imageSmoothingEnabled,
            imageSmoothingQuality,
        }

        private static List<SetValueOperationEnum> enumArray = new List<SetValueOperationEnum>()
        {
            SetValueOperationEnum.globalCompositeOperation,
            SetValueOperationEnum.fillStyle,
            SetValueOperationEnum.filter,

            SetValueOperationEnum.imageSmoothingEnabled,
            SetValueOperationEnum.imageSmoothingQuality,
        };

        private byte dictIndex;

        private byte option;
        private string value;
        private short objIndex;
        private bool @bool;

        public SetValueOperation(SetValueOperationEnum key, string value)
        {
            dictIndex = (byte)enumArray.IndexOf(key);
            option = 0;
            this.value = value;
        }

        public SetValueOperation(SetValueOperationEnum key, short objIndex)
        {
            dictIndex = (byte)enumArray.IndexOf(key);
            option = 1;
            this.objIndex = objIndex;
        }

        /// <summary>
        /// imageSmoothingEnabled
        /// </summary>
        /// <param name="bool"></param>
        public SetValueOperation(bool @bool)
        {
            dictIndex = (byte)enumArray.IndexOf(SetValueOperationEnum.imageSmoothingEnabled);
            this.@bool = @bool;
        }

        /// <summary>
        /// imageSmoothingQuality
        /// </summary>
        /// <param name="option">0 - low, 1 - medium, 2 - high</param>
        public SetValueOperation(byte option)
        {
            dictIndex = (byte)enumArray.IndexOf(SetValueOperationEnum.imageSmoothingQuality);
            this.option = option;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteByte(array, info, dictIndex);

            switch (dictIndex)
            {
                case 0:
                case 1:
                case 2:
                    {
                        WriteByte(array, info, option);

                        if (option == 0)
                        {
                            WriteString(array, info, value);
                        }
                        else
                        {
                            WriteShort(array, info, objIndex);
                        }

                        return;
                    }

                case 3:
                    {
                        WriteBool(array, info, @bool);
                        return;
                    }

                case 4:
                    {
                        WriteByte(array, info, option);
                        return;
                    }
            }
        }

        public override int GetLength()
        {
            switch (dictIndex)
            {
                case 0:
                case 1:
                case 2:
                    if (option == 0)
                        return 2 + GetStringLength(value);
                    else
                        return 4;

                case 3:
                case 4:
                    return 2;
            }

            throw new Exception($"SetValueOperation Process Dumbass");
        }
    }
}
