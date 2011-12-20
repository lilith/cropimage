using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Web.UI
{

    /// <summary>
    /// 
    /// </summary>
    public class CropImageEventArgs : EventArgs
    {

        /// <summary>
        /// X coordinate of the selection
        /// </summary>
        public int X { get; set; }
        
        /// <summary>
        /// Y coordinate of the selection
        /// </summary>
        public int Y { get; set; }
                
        
        /// <summary>
        /// Width of the selection
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the selection
        /// </summary>
        public int Height { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public CropImageEventArgs() { 
             
        }

    }
}
