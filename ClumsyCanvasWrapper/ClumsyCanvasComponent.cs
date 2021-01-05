using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper
{
    public class ClumsyCanvasComponent : ComponentBase
    {
        protected ElementReference canvasRef;

        [Parameter]
        public long Height { get; set; }

        [Parameter]
        public long Width { get; set; }

        /// <summary>
        /// Link this canvas with MyReader on js side
        /// </summary>
        /// <param name="js"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task Attach(MyReaderInterop myReader, short index)
        {
            await myReader.AttachCanvas(canvasRef, index);
        }
    }
}
