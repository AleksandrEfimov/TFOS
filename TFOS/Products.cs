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
            string Name { get; set; }
            
            struct RegularPrice
            {
                private float Price { get; set; }
                private int[] ColorRGB;
                private string FontSize;
                private bool striked;

            };

            struct CampainPrice
            {
                private float Price { get; set; }
                private int[] ColorRGB;
                private string FontSize;
            }
            


        }


        }
    }
