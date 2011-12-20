using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;

namespace CS.Web.UI
{
    public class CropImageDesigner : CompositeControlDesigner
    {

        /// <summary>
        /// 
        /// </summary>
        public override bool AllowResize
        {
            get
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetDesignTimeHtml()
        {
            return base.GetDesignTimeHtml();
        }
    }
}
