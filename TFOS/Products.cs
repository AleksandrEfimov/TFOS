using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        }
    }
