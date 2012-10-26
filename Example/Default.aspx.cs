using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Imazen.Crop;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnCrop_Click(object sender, EventArgs e) {
        //We're assuming the default names are used here - Button1, Image1, CropImage1, Button2, Image2, CropImage2, etc.
        //Find the cropimage instance near the clicked button
        CropImage ci = this.FindControl(((Button)sender).ID.Replace("Button", "CropImage")) as CropImage;
 

        //Show the image using the CroppedUrl property
        result.ImageUrl = ci.CroppedUrl;
        result.Visible = true;

        //Save an unneccesary copy of the file out with Crop(), just to show we can
        ci.Crop(MapPath("~/images/last-cropped"), true);

        //Tell user about it
        message.Text = "Crop successful. (copy saved to /images/last-cropped.jpg/png)";
        coords.Text = "Final Coordinates:" + ci.X + "," + ci.Y + "," + ci.X2 + "," + ci.Y2 + "  XUnits:" + ci.CropXUnits + ", YUnits:" + ci.CropYUnits;
        cropped.NavigateUrl = ci.CroppedUrl;
        cropped.Text = "Dynamic URL: " + ci.CroppedUrl;


    }
    
}