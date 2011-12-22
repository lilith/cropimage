using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RequiredFieldValidator req = new RequiredFieldValidator();
        req.ControlToValidate = "test";

        //CropImage1.Cropped +=new CS.Web.UI.CropImage.CropImageEventHandler(CropImage1_Cropped);
    }

    protected void CropImage1_Cropped(object sender, CS.Web.UI.CropImageEventArgs e)
    {
        //Response.Write("done!");
        
    }
    protected void test_Click(object sender, EventArgs e)
    {
        CropImage1.Crop(Server.MapPath("images/test.jpg"));
    }
}
