using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;
using TFOS_utilities;


namespace TFOS
{
    class Products
    {
             
        public Products() { }

        private Properties prop;

        public Properties Prop { get => prop; set => prop = value; }
        
        

        public struct Properties
        {
            public string strOfGetValue { get; set; }
           
            public string Name { get; set; }
            public SpecList<int> intArray { get; set; }
            public string Link { get; set; }

            public float RegularPrice { get; set; }
            public SpecList<int> RegularColorRGB { get; set; }
            public string RegularFontSize { get; set; }
            public string RegularStriked { get; set; }



            public float ActionPrice { get; set; }
            public SpecList<int> ActionColorRGB { get; set; }
            public string ActionFontSize { get; set; }


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


        public SpecList<int> ParsingRGB(string str)
        {
            SpecList<int> Arr = new SpecList<int>();
            Regex regex = new Regex(@"\d{3}");
            MatchCollection match = regex.Matches(str);
            for (int i = 0; i < match.Count; i++)
                Arr.Add(int.Parse(match[i].Groups[0].Value));
            return Arr;
        }


    }
}
