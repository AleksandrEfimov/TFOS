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

        public struct Properties
        {
            public string Name { get; set; }
            public string Link { get; set; }
            
            public float RegularPrice { get; set; }
            public int[] RegularColorRGB;
            public string RegularFontSize;
            public bool RegularStriked;

            
            
            public float ActionPrice { get; set ; }
            public int[] ActionColorRGB;
            public string ActionFontSize;
        }
        public Products() { }

        public int[] ParsingRGB(string str)
        {
            Color = 
            int[] Arr = new int[3];
            string[] stAr = str.Split(',');            
            foreach i in stAr


            return Arr;
        }

        }
    }
