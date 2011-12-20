using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {        
         
    }          


    protected void btnCrop_Click(object sender, EventArgs e)
    {    

        /* thats the method to crop and save the image.*/
        wci1.Crop(Server.MapPath("images/cropped.jpg"));
        
        /*
         this part is just to show the image after its been cropped and saved! so focus on just the code above.
         */
        Image img = new Image();
        img.ImageUrl = "images/cropped.jpg?rnd=" + (new Random()).Next();  // added random for caching issue.
        this.Controls.Add(img);

        // test comment for patch by @cemsisman @20090520
    }

    

}
