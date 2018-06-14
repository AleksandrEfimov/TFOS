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
            /*
            public string strOfGetValue;

            public string Name;
            public SpecList<int> intArray;
            public string Link;

            public float RegularPrice;
            public SpecList<int> RegularColorRGB;
            public string RegularFontSize;
            public string RegularStriked;



            public float CampaignPrice;
            public SpecList<int> CampaignColorRGB;
            public string CampaignFontSize;
            */
            //public string strOfGetValue { get; set; }
            //public SpecList<int> intArray { get; set; }

            public string Name { get; set; }
            public string Link { get; set; }
            public string RegularPrice { get; set; }
            public SpecList<int> RegularColorRGB { get; set; }
            public float RegularFontSize { get; set; }
            public string RegularStriked { get; set; }

            public string CampaignPrice { get; set; }
            public SpecList<int> CampaignColorRGB { get; set; }
            public float CampaignFontSize { get; set; }
            public string CampaignFontStrong { get; set; }
            

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
            Regex regex = new Regex(@"\d{1,3}");
            MatchCollection match = regex.Matches(str);

            for (int i = 0; i < match.Count; i++)
                Arr.Add(int.Parse(match[i].Groups[0].Value));
            return Arr;
        }

        public float ParsingFontSize(string str)
        {
            Regex regex = new Regex(@"\d+\.*\d*");
            Regex regex_point = new Regex(@"\.");
             MatchCollection match = regex.Matches(str);
            
            var S = regex_point.Replace( match[0].ToString(), ",");
            float size = float.Parse(S);
            return size;
        }

    }
}
