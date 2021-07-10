using ClumsyCanvasWrapper.Operations;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper
{
    public static class OpList
    {
        public static List<Type> ops = new List<Type>
        {
            typeof(CreateCanvasOperation),
            typeof(SetObjectOperation),
            typeof(SetElementOperation),
            typeof(SetStringOperation),
            typeof(RemoveObjectOperation),
            typeof(FillOperation),
            typeof(GlobalCompositeOperation),
            typeof(FillRectOperation),
            typeof(ResizeCanvasOperation),
            typeof(DrawImageOperation),
            typeof(SetValueOperation),
            typeof(SetCustomValueOperation),
            typeof(SetCanvasOperation),
            typeof(ClearRectOperation),
            typeof(CreatePatternOperation),
            typeof(FillStyleOperation),
            typeof(RectOperation),
            typeof(SaveOperation),
            typeof(RestoreOperation),
            typeof(TranslateOperation),
        };
    }

    public class MyReaderInterop
    {
        private IJSRuntime js;
        private IJSUnmarshalledRuntime jsUnmarsh;

        public MyReaderInterop(IJSRuntime js)
        {
            this.js = js;
            jsUnmarsh = (IJSUnmarshalledRuntime)js;
        }

        public async Task InitReader()
        {
            await js.InvokeVoidAsync("initDrawReader");
        }

        public async Task<string> GetSingleImageData(int x, int y)
        {
            return await js.InvokeAsync<string>("getSingleImageData", x, y);
        }

        public async Task AttachCanvas(ElementReference reference, short index)
        {
            await js.InvokeVoidAsync("attachCanvas", reference, index);
        }

        public async Task CreateCanvas(short index)
        {
            await js.InvokeVoidAsync("createCanvas", index);
        }

        public void ProcessList(List<BaseOperation> list)
        {
            int length = list.Count;

            foreach (BaseOperation operation in list)
            {
                length += operation.GetLength();
            }

            byte[] array = new byte[length];
            OperationInfo info = new OperationInfo();
            info.index = 0;

            try
            {
                foreach (BaseOperation operation in list)
                {
                    int opIndex = OpList.ops.IndexOf(operation.GetType());

                    array[info.index] = (byte)opIndex;
                    info.index++;

                    operation.Process(array, info);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception {e}");
            }

            //Console.WriteLine($"c# process {array.Length}");

            jsUnmarsh.InvokeUnmarshalled<byte[], object>("processDrawArray", array);
        }
    }
}
