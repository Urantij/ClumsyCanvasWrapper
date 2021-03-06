using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClumsyCanvasWrapper
{
    public class OperationInfo
    {
        public int index;
    }

    public abstract class BaseOperation
    {
        protected void WritePath(byte[] array, OperationInfo info, IEnumerable<string> path)
        {
            string result = string.Join('.', path);
            WriteString(array, info, result);
        }

        protected void WriteShort(byte[] array, OperationInfo info, short shrt)
        {
            byte[] shrtArray = BitConverter.GetBytes(shrt);

            array[info.index] = shrtArray[0];
            array[info.index + 1] = shrtArray[1];

            info.index += 2;
        }

        protected void WriteString(byte[] array, OperationInfo info, string str)
        {
            if (str == null)
            {
                WriteShort(array, info, -1);
            }
            else if (str.Length > 0)
            {
                byte[] strArray = Encoding.UTF8.GetBytes(str);

                WriteShort(array, info, (short)strArray.Length);

                Array.Copy(strArray, 0, array, info.index, strArray.Length);

                info.index += strArray.Length;
            }
            else
            {
                WriteShort(array, info, 0);
            }
        }

        protected void WriteByte(byte[] array, OperationInfo info, byte bt)
        {
            array[info.index] = bt;
            info.index++;
        }

        protected int GetPathLength(IEnumerable<string> path)
        {
            int length = 0;

            foreach (string part in path)
            {
                //Путь же не может содержать такие символы
                //length += Encoding.UTF8.GetBytes(part).Length;
                length += part.Length;
                length++;
            }
            length--;

            return 2 + length;
        }

        /// <summary>
        /// Строки, чьи символы могут занимать больше 1 байта
        /// Типа кириллицы
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected int GetUTFStringLength(string str)
        {
            return 2 + str == null ? 0 : Encoding.UTF8.GetBytes(str).Length;
        }

        protected int GetStringLength(string str)
        {
            return 2 + str == null ? 0 : str.Length;
        }

        /// <summary>
        /// Запись информации в массив
        /// </summary>
        /// <param name="array"></param>
        /// <param name="info"></param>
        public abstract void Process(byte[] array, OperationInfo info);

        /// <summary>
        /// Сколько байт эта операция запишет в массив
        /// </summary>
        /// <returns></returns>
        public abstract int GetLength();
    }
}
