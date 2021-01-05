using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper.Operations
{
    public class SetObjectOperation : BaseOperation
    {
        private short index;
        private IEnumerable<string> path;

        public SetObjectOperation(short index, IEnumerable<string> path)
        {
            this.index = index;
            this.path = path;
        }

        public override void Process(byte[] array, OperationInfo info)
        {
            WriteShort(array, info, index);
            WritePath(array, info, path);
        }

        public override int GetLength()
        {
            return 2 + GetPathLength(path);
        }
    }
}
