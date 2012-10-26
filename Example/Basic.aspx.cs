using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Basic : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
    }
    protected void btnCrop_Click(object sender, EventArgs e) {

        //If ImageResizing.net is installed, we can just use CroppedUrl from now on
        Result.Visible = true;
        Result.ImageUrl = CropImage1.CroppedUrl;

        //If not, we can save out a copy
        CropImage1.Crop(MapPath("~/images/basic-cropped"), true);

        //We can also access the coordinates
        this.Title = CropImage1.X + "," + CropImage1.Y + "," + CropImage1.X2 + "," + CropImage1.Y2;

    }

}