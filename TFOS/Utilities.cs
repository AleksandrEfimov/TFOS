using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFOS_utilities
{
    public static class Utilities
    {
        static StringBuilder sb;

        ////public string intArToStr(this Array arr)
        public static string ToString(this List<int> arr)
        {
            //string res = "[";
            sb = new StringBuilder();
            sb.Append("[");
            foreach (var V in arr)
                sb.Append(V.ToString() + ",");
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }
    }

    class SpecList<T> : List<T>
    {
        static StringBuilder sb;

        public override string ToString()
        {
            sb = new StringBuilder();
            sb.Append("[");
            foreach (var V in this)
                sb.Append(V.ToString() + ",");
            if(sb.Length>1)
                sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }


    }




}
