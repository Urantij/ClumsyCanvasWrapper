using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class GlobalCompositeOperation : BaseOperation
    {
        public enum GlobalCompositeOperationEnum
        {
            source_over,
            source_in,
            source_out,
            source_atop,
            destination_over,
            destination_in,
            destination_out,
            destination_atop,
            lighter,
            copy,
            xor,
            multiply,
            screen,
            overlay,
            darken,
            lighten,
            color_dodge,
            color_burn,
            hard_light,
            soft_light,
            difference,
            exclusion,
            hue,
            saturation,
            color,
            luminosity
        }

        private static List<GlobalCompositeOperationEnum> enumArray = new List<GlobalCompositeOperationEnum>
        {
            GlobalCompositeOperationEnum.source_over,
            GlobalCompositeOperationEnum.source_in,
            GlobalCompositeOperationEnum.source_out,
            GlobalCompositeOperationEnum.source_atop,
            GlobalCompositeOperationEnum.destination_over,
            GlobalCompositeOperationEnum.destination_in,
            GlobalCompositeOperationEnum.destination_out,
            GlobalCompositeOperationEnum.destination_atop,
            GlobalCompositeOperationEnum.lighter,
            GlobalCompositeOperationEnum.copy,
            GlobalCompositeOperationEnum.xor,
            GlobalCompositeOperationEnum.multiply,
            GlobalCompositeOperationEnum.screen,
            GlobalCompositeOperationEnum.overlay,
            GlobalCompositeOperationEnum.darken,
            GlobalCompositeOperationEnum.lighten,
            GlobalCompositeOperationEnum.color_dodge,
            GlobalCompositeOperationEnum.color_burn,
            GlobalCompositeOperationEnum.hard_light,
            GlobalCompositeOperationEnum.soft_light,
            GlobalCompositeOperationEnum.difference,
            GlobalCompositeOperationEnum.exclusion,
            GlobalCompositeOperationEnum.hue,
            GlobalCompositeOperationEnum.saturation,
            GlobalCompositeOperationEnum.color,
            GlobalCompositeOperationEnum.luminosity
        };

        private byte index;

        public GlobalCompositeOperation(GlobalCompositeOperationEnum key)
        {
            this.index = (byte)enumArray.IndexOf(key);
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteByte(array, info, index);
        }

        public override int GetLength()
        {
            return 1;
        }
    }
}
