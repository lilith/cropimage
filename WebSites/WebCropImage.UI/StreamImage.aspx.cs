using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class StreamImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ImageToByteArray();

    }


    private void ImageToByteArray()
    {
        Response.Clear();
        Response.ContentType = "image/jpeg";
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("~/images/328.jpg")))
        {
            image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}