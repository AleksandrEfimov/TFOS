using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support;
using System.Text.RegularExpressions;


namespace TFOS
{
    class Products
    {
        public Products() { }

        private Properties prop;

        public Properties Prop { get => prop; set => prop = value; }

        public struct Properties
        {
            public string Name { get; set; }
            public string Link { get; set; }

            public float RegularPrice { get; set; }
            public int[] RegularColorRGB;
            public string RegularFontSize;
            public string RegularStriked;



            public float ActionPrice { get; set; }
            public int[] ActionColorRGB;
            public string ActionFontSize;
        }


        public int[] ParsingRGB(string str)
        {
            int[] Arr = new int[3];
            Regex regex = new Regex(@"\d{3}");
            MatchCollection match = regex.Matches(str);
            for (int i = 0; i < match.Count - 1; i++)
                Arr[i] = int.Parse(match[i].Groups[0].Value);
            return Arr;
        }


    }
}
