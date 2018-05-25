using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;


namespace TFOS
{
    class Products
    {
             
        public Products() { }

        private Properties prop;

        public Properties Prop { get => prop; set => prop = value; }
        
        

        public struct Properties
        {
            public string strOfGetValue;
            public List<int> intArray;
            public string Name { get; set; }
            public string Link { get; set; }

            public float RegularPrice { get; set; }
            public List<int> RegularColorRGB;
            public string RegularFontSize;
            public string RegularStriked;



            public float ActionPrice { get; set; }
            public List<int> ActionColorRGB;
            public string ActionFontSize;
            

        }

        
        ////public string intArToStr(this Array arr)
        //public static string ToString(this int[] arr)
        //{
        //    //string res = "[";
            
        //    sb.Append("[");
        //    foreach (var V in arr)
        //        sb.Append( V.ToString()+",");
        //    sb.Remove(sb.Length-1,1);
        //    sb.Append("]");
        //    return sb.ToString();
        //}
        //public override string ToString()
        //{

        //    return base.ToString();
        //}


        public List<int> ParsingRGB(string str)
        {
            List<int> Arr = new List<int>();
            Regex regex = new Regex(@"\d{3}");
            MatchCollection match = regex.Matches(str);
            for (int i = 0; i < match.Count; i++)
                Arr.Add(int.Parse(match[i].Groups[0].Value));
            return Arr;
        }


    }
}
