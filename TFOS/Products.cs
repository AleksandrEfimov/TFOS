using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support;


namespace TFOS
{
    class Products
    {
        public Products() { }

        public override string ToString()
        {

            return base.ToString();
        }

        public struct Properties
        {
            public string Name { get; set; }
            public string Link { get; set; }
            
            public float RegularPrice { get; set; }
            public int[] RegularColorRGB;
            public string RegularFontSize;
            public string RegularStriked;

            
            
            public float ActionPrice { get; set ; }
            public int[] ActionColorRGB;
            public string ActionFontSize;
        }
        

        public int[] ParsingRGB(string str)
        {

            int[] Arr = new int[] { 0, 0, 0, 0 };
            string[] stAr = str.Split(',');
            for (int i = 0; i < stAr.Length; i++)
                Arr[i] = Convert.ToInt32(stAr[i]);


            return Arr;
        }

        }
    }
