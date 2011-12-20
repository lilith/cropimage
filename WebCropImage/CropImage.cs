using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using drawing=System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using System.Net;




[assembly: WebResource("CS.Web.UI.jquery.jcrop.css", "text/css")]
[assembly: WebResource("CS.Web.UI.jquery.jcrop.js", "text/javascript")]
[assembly: WebResource("CS.Web.UI.jquery.js", "text/javascript")]
namespace CS.Web.UI
{
    /// <summary>
    /// 
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxBitmap(typeof(CropImage), "CropImage")]
    [Designer(typeof(CropImageDesigner))]
    [ToolboxData("<{0}:CropImage runat=server></{0}:CropImage>")]
    public class CropImage : CompositeControl, INamingContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public delegate void CropImageEventHandler(Object sender, CropImageEventArgs e);
        
        
        /// <summary>
        /// 
        /// </summary>
        public event CropImageEventHandler Cropped;


        /// <summary>
        /// 
        /// </summary>
        public event CropImageEventHandler Cropping;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnCropped(Object sender, CropImageEventArgs e)
        {
            //EnsureChildControls();
            if (Cropped != null)
            {

                Cropped(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnCropping(Object sender, CropImageEventArgs e)
        {
            if (Cropping != null)
            {
                Cropping(this, e);
            }
        }


        

        /// <summary>
        /// Call this function when button clicked.
        /// </summary>
        /// <param name="path"></param>
        public void Crop(string path) {

            //EnsureChildControls();


            CropImageEventArgs args = new CropImageEventArgs();
            args.X = Convert.ToInt32(Page.Request["_x"]);
            args.Y = Convert.ToInt32(Page.Request["_y"]);            
            args.Width= Convert.ToInt32(Page.Request["_w"]);
            args.Height = Convert.ToInt32(Page.Request["_h"]);



            this.X = args.X;
            this.Y = args.Y;
            this.W = args.Width;
            this.H = args.Height;
            this.X2 = args.Width + args.X;
            this.Y2 = args.Height + args.Y;

            //repair size because of canvas fitting
            double fact = CanvasRatio;
            if (fact > 0)
            {
                args.X = Convert.ToInt32(args.X / fact);
                args.Y = Convert.ToInt32(args.Y / fact);                
                args.Width = Convert.ToInt32(args.Width / fact);
                args.Height = Convert.ToInt32(args.Height / fact);
            }
            // maintain details after postback!
            
           

            
            //HttpContext.Current.Response.Write("w:" + this.W);

            OnCropping(this, args);

            // create property called save as and save this file to there.
            System.Drawing.Graphics g = null;
            System.Drawing.Bitmap cropedImage = null;
            drawing.Image image = null;
            try
            {
                image = drawing.Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(this.ImagePath))));
            }
            catch (Exception ex)
            {
                Stream stream = null;
                try
                {
                    
                    WebRequest req = WebRequest.Create(this.ImagePath);
                    WebResponse response = req.GetResponse();
                    stream= response.GetResponseStream();

                    
                    image = drawing.Image.FromStream(stream);
                    stream.Close();
                    
                }
                catch (Exception ex2)
                {
                    stream.Close();
                    throw ex2;
                }
            }

            if (EnablePreview)
            {
                cropedImage = new System.Drawing.Bitmap(
                this.PreviewWidth,
                this.PreviewHeight
                , image.PixelFormat);
            }
            else
            {
                cropedImage = new System.Drawing.Bitmap(
                args.Width
                , args.Height
                , image.PixelFormat);
            }
            
            
            g = System.Drawing.Graphics.FromImage(cropedImage);

            drawing.Rectangle _rec;
            drawing.Rectangle _rec2;

            if (EnablePreview)
            {
                _rec = new drawing.Rectangle(
                            0,0,
                            this.PreviewWidth,
                            this.PreviewHeight);

                _rec2 = new drawing.Rectangle(
                            args.X,
                            args.Y,
                            args.Width,
                            args.Height);
            }
            else
            {
                _rec = new drawing.Rectangle(0, 0,
                                args.Width,
                                args.Height);

                _rec2 = new drawing.Rectangle(
                            args.X,
                            args.Y,
                            args.Width,
                            args.Height);
            }

            


            g.DrawImage(image, _rec, _rec2.X, _rec2.Y
                            , _rec2.Width, _rec2.Height
                            , drawing.GraphicsUnit.Pixel);
            
            

            string mimeType = GetMimeType(image);
            if (mimeType == "image/unknown")
            {
                cropedImage.Save(path);
            }
            else if (mimeType == "image/jpeg")
            {
                cropedImage.Save(path,ImageFormat.Jpeg);
            }
            else if (mimeType == "image/gif")
            {
                cropedImage.Save(path, ImageFormat.Gif);
            }
            else if (mimeType == "image/x-png")
            {
                cropedImage.Save(path, ImageFormat.Png);
            }
            else if (mimeType == "image/x-ms-bmp")
            {
                cropedImage.Save(path, ImageFormat.Bmp);
            }

            image.Dispose();

            g.Dispose();

            cropedImage.Dispose();

            OnCropped(this, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetMimeType(drawing.Image i)
        {
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == i.RawFormat.Guid)
                    return codec.MimeType;
            }

            return "image/unknown";
        }

        
        /// <summary>
        /// 
        /// </summary>
        private string ImagePath { get { return ViewState["ImageUrl"].ToString(); } }

        private double CanvasRatio { get { return Convert.ToDouble(ViewState["canvasRatio"]); } }


        /// <summary>
        /// 
        /// </summary>
        protected override void CreateChildControls()
        {
            bool isInUpdatePanel = this.IsInUpdatePanel;


            //Page p = (Page)System.Web.HttpContext.Current.Handler;
            //ScriptManager m = ScriptManager.GetCurrent(p);

            
            
            ClientScriptManager cs = Page.ClientScript;
            
            if (this.IncludeJQuery)
            {
                if (string.IsNullOrEmpty(this.ScriptPath))
                {
                    this.ScriptPath = "/scripts/";
                }
                
                if (isInUpdatePanel)
                {
                    if (!cs.IsClientScriptIncludeRegistered(this.GetType(), "jquery"))
                        ScriptManager.RegisterClientScriptInclude(this,this.GetType()
                            , "jquery", ResolveClientUrl(this.ScriptPath + "jquery.js"));

                    if (!cs.IsClientScriptIncludeRegistered(this.GetType(), "jqueryCrop"))
                        ScriptManager.RegisterClientScriptInclude(this,this.GetType()
                            , "jqueryCrop", ResolveClientUrl(this.ScriptPath + "jquery.jcrop.js"));
                }
                else
                {
                    if (!cs.IsClientScriptIncludeRegistered(this.GetType(), "jquery"))
                        cs.RegisterClientScriptInclude(this.GetType()
                            , "jquery", ResolveClientUrl(this.ScriptPath + "jquery.js"));

                    if (!cs.IsClientScriptIncludeRegistered(this.GetType(), "jqueryCrop"))
                        cs.RegisterClientScriptInclude(this.GetType()
                            , "jqueryCrop", ResolveClientUrl(this.ScriptPath + "jquery.jcrop.js"));
                }
                

            }
            else
            {
                // load it from google ajax library. Faster way and less annoying!
                /*if(!cs.IsClientScriptIncludeRegistered("jquery"))
                    cs.RegisterClientScriptInclude("jquery"
                        , "http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js");
                */

                string jquery = "\r\n<script src=\"" + cs.GetWebResourceUrl(this.GetType(),
                        "CS.Web.UI.jquery.js") + "\" type=\"text/javascript\"></script>\r\n";

                string cropJS = "\r\n<script src=\"" + cs.GetWebResourceUrl(this.GetType(),
                        "CS.Web.UI.jquery.jcrop.js") + "\" type=\"text/javascript\"></script>\r\n";


                if (isInUpdatePanel)
                {
                    if (!cs.IsClientScriptBlockRegistered("jquery"))
                        cs.RegisterClientScriptBlock(this.GetType(), "jquery", jquery, false);

                    if (!cs.IsClientScriptBlockRegistered("cropJS"))
                        cs.RegisterClientScriptBlock(this.GetType(), "cropJS", cropJS, false);
                }
                else
                {
                    if (!cs.IsClientScriptBlockRegistered("jquery"))
                        ScriptManager.RegisterClientScriptBlock(this,this.GetType(), "jquery", jquery, false);

                    if (!cs.IsClientScriptBlockRegistered("cropJS"))
                        ScriptManager.RegisterClientScriptBlock(this,this.GetType(), "cropJS", cropJS, false);
                }
            }


            
            
            string cropCss = "<link href=\"" + cs.GetWebResourceUrl(this.GetType(),
                        "CS.Web.UI.jquery.jcrop.css") + "\" type=\"text/css\" rel=\"stylesheet\" />\r\n";

            //if (!Page.Header.Controls.Add(new LiteralControl(("cropCss"))
            //    cs.RegisterStartupScript(this.GetType(), "cropCss", cropCss, false);

            if (!cs.IsClientScriptBlockRegistered("cropJS"))
                Page.Header.Controls.Add(new LiteralControl(cropCss));
            
            //bool cropInitScriptAdded = cs.IsClientScriptBlockRegistered("cropInit");
                        
            if (true)
            {
                System.Web.UI.WebControls.Image image = 
                    (System.Web.UI.WebControls.Image)Parent.FindControl(cropimage);
                ViewState["ImageUrl"] = image.ImageUrl;


                //System.Drawing.Image img = 
                //    System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath( image.ImageUrl));


                //check if we set canvas size
                if (this.CanvasHeight > 0 | this.CanvasWidth > 0)
                {
                    string folder = HttpContext.Current.Server.MapPath("~/");
                    GetRawSize(folder + image.ImageUrl);

                    double rratio = (double)rawHeight / rawWidth; //raw image ratio
                    double cratio; //canvas ratio
                    if (this.CanvasHeight == 0)
                    {
                        cratio = rratio;
                        this.CanvasHeight = Convert.ToInt32(this.CanvasWidth * rratio);
                    }
                    else if (this.CanvasWidth == 0)
                    {
                        cratio = rratio;
                        this.CanvasWidth = Convert.ToInt32(this.CanvasHeight / rratio);
                    }
                    else
                    {
                        cratio = (double)CanvasHeight / CanvasWidth;
                    }
                    if (rratio > cratio)
                    {//we have to fit height  
                        if (rawHeight > CanvasHeight)
                        {
                            resizefact = (double)CanvasHeight / rawHeight;
                            image.Height = CanvasHeight;
                            image.Width = Convert.ToInt32(rawWidth * resizefact);
                        }
                    }
                    else
                    { //we have to fit width
                        if (rawWidth > CanvasWidth)
                        {
                            resizefact = (double)CanvasWidth / rawWidth;
                            image.Width = CanvasWidth;
                            image.Height = Convert.ToInt32(rawHeight * resizefact);
                        }
                    }
                    ViewState["canvasRatio"] = resizefact;
                }
                
                StringBuilder sb = new StringBuilder();
                
                sb.Append("<script>$(function(){ ");
                
                string showPreviewScript = "";
                
                showPreviewScript = ", onChange: __showPreview" +
                    ", onSelect: __showPreview";
                               

                sb.Append("$('#" + image.ClientID + "').Jcrop({" +
                    "setSelect: [" + X.ToString() + "," + 
                    Y.ToString() + "," + 
                    X2.ToString() + ", " + 
                    Y2.ToString() + "]" + 
                    showPreviewScript);


                if (!string.IsNullOrEmpty(this.Ratio))
                    sb.Append(", aspectRatio: " + this.Ratio);

                if (!string.IsNullOrEmpty(this.MaxSize))
                    sb.Append(", maxSize: [" + this.MaxSize + "]");

                if (!string.IsNullOrEmpty(this.MinSize))
                    sb.Append(", minSize: [" + this.MinSize + "]");
                
                sb.Append("});");
                sb.Append("});");

                
                string script = @"function __showPreview(coords){                        

                            try{
                                    
                                    $('#_x').val(coords.x); 
                                    $('#_y').val(coords.y); 
                                    $('#_w').val(coords.w);
                                    $('#_h').val(coords.h); 

                                    var rx = {1} / coords.w;
	                                var ry = {2} / coords.h;

	                                $('#{0}').css({
		                                width: Math.round(rx * {3}) + 'px',
		                                height: Math.round(ry * {4}) + 'px',
		                                marginLeft: '-' + Math.round(rx * coords.x) + 'px',
		                                marginTop: '-' + Math.round(ry * coords.y) + 'px'
	                                });

                            }
                            catch(e){} 
                                                                                      
                        }
                            
                        ";


                GetRawSize(image.ImageUrl);


                script = script.Replace("{0}", this.PreviewImageID);
                script = script.Replace("{1}", rawWidth.ToString());
                script = script.Replace("{2}", rawHeight.ToString());
                script = script.Replace("{3}", rawWidth.ToString());
                script = script.Replace("{4}", rawHeight.ToString());

                sb.Append(script);

                    //sb.Append(string.Format(script.ToString(),this.PreviewImageID));
                
                

                //if (EnablePreview)
                //{
                //    sb.Append("\r\n function __showPreview22(coords){ \r\n" +
                //                "var rx = 100 / coords.w; \r\n" +
                //                "var ry = 100 / coords.h; \r\n" +                                     
                //                "$('#preview').css({" +
                //                "    width: Math.round(rx * " + img.Width + ") + 'px', \r\n" +
                //                "    height: Math.round(ry * " + img.Height + ") + 'px', \r\n" +
                //                "    marginLeft: '-' + Math.round(rx * coords.x) + 'px', \r\n" +
                //                "    marginTop: '-' + Math.round(ry * coords.y) + 'px' \r\n" +
                //                "});  try {__showDebug(coords); } catch(e) {}}\r\n\r\n");
                //}               

               
                /*sb.Append(@"function __showDebug(coords){
                            try{
                                $('#wci_xt').val(coords.x); 
                                $('#wci_yt').val(coords.y); 
                                $('#wci_wt').val(coords.w);
                                $('#wci_ht').val(coords.h);                                  
                            }
                            catch(e){}
                        }");

                */

                sb.Append(@"</script>");


                if (isInUpdatePanel)
                {
                    ScriptManager.RegisterClientScriptBlock(this,this.GetType()
                        , "cropInit"
                        , sb.ToString(), false);


                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertbabam", "alert('test');", true);

                    ScriptManager.RegisterHiddenField(this, "_x", X.ToString());
                    ScriptManager.RegisterHiddenField(this, "_y", Y.ToString());
                    ScriptManager.RegisterHiddenField(this, "_w", W.ToString());
                    ScriptManager.RegisterHiddenField(this, "_h", H.ToString());
                }
                else
                {
                    cs.RegisterClientScriptBlock(this.GetType()
                        , "cropInit"
                        , sb.ToString(), false);


                    cs.RegisterHiddenField("_x", X.ToString());
                    cs.RegisterHiddenField("_y", Y.ToString());
                    cs.RegisterHiddenField("_w", W.ToString());
                    cs.RegisterHiddenField("_h", H.ToString());
                }

                

                image.Dispose();
                //img.Dispose();
            }

            base.CreateChildControls();

            //HttpContext.Current.Response.Write("ssss");

        }


        private double resizefact;
        private int canvasw;


        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(false)]
        [Category("CS")]
        public bool DebugMode
        {
            get { return ViewState["DM"] == null ? false : Convert.ToBoolean(ViewState["DM"]); }
            set { ViewState["DM"] = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]       
        public int CanvasHeight
        {
            get { return ViewState["CH"] == null ? 0 : Convert.ToInt32(ViewState["CH"]); }
            set { ViewState["CH"] = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(false)]
        [Category("CS")]
        public bool EnablePreview
        {
            get { return ViewState["EP"] == null ? false : Convert.ToBoolean(ViewState["EP"]); }
            set { ViewState["EP"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int PreviewWidth
        {
            get { return ViewState["EPH"] == null ? 0 : Convert.ToInt32(ViewState["EPH"]); }
            set { ViewState["EPH"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int PreviewHeight
        {
            get { return ViewState["EPH"] == null ? 0 : Convert.ToInt32(ViewState["EPH"]); }
            set { ViewState["EPH"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(false)]
        [Category("CS")]
        public string PreviewImageID
        {
            get { return ViewState["EPIMAGEID"] == null ? null : Convert.ToString(ViewState["EPIMAGEID"]); }
            set { ViewState["EPIMAGEID"] = value; }
        }

        private int canvash;
        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int CanvasWidth
        {
            get { return ViewState["CW"] == null ? 0 : Convert.ToInt32(ViewState["CW"]); }
            set { ViewState["CW"] = value; }
        }

        private bool GetRawSize(string rawimage)
        {
            drawing.Image image = null;
            try
            {
                image = drawing.Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(this.ImagePath))));

                rawWidth = image.Width;
                rawHeight = image.Height;
                image.Dispose();
            }
            catch (Exception ex)
            {
                Stream stream = null;
                try
                {
                    WebRequest req = WebRequest.Create(this.ImagePath);
                    WebResponse response = req.GetResponse();
                    stream = response.GetResponseStream();
                    
                    image = drawing.Image.FromStream(stream);

                    rawWidth = image.Width;
                    rawHeight = image.Height;
                    image.Dispose();
                    stream.Close();
                }
                catch (Exception ex2)
                {
                    image.Dispose();
                    stream.Close();
                    throw ex2;
                }
            }

            return true;
        }

       

        private int rawWidth;
        private int rawHeight;

        /* 
         * must for all composite controls!           
         * http://msdn.microsoft.com/en-us/library/aa478969.aspx
        
        /// <summary>
        /// 
        /// </summary>
        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }
*/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            //base.RenderEndTag(writer);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            //base.RenderBeginTag(writer);
        }

        

        #region "Properties"


        private string cropimage;
        /// <summary>
        /// ID of the asp.net Image control to be cropped.
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Category("CS")
            , Description("ID of the asp.net Image control to be cropped.")]
        public string Image
        {
            get
            {
                return cropimage;
            }
            set
            {
                cropimage = value;
            }
        }


        private string maxSize;
        /// <summary>
        /// MaxSize of the asp.net Image control to be cropped.
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Category("CS")
            , Description("[w,h]")]
        public string MaxSize
        {
            get
            {

                return maxSize;
            }
            set
            {

                maxSize = value;
            }
        }

        private string minSize;
        /// <summary>
        /// MinSize of the asp.net Image control to be cropped.
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Category("CS")
            , Description("[w,h]")]
        public string MinSize
        {
            get
            {

                return minSize;
            }
            set
            {

                minSize = value;
            }
        }


        string cropButton;

        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [Category("CS")]
        [IDReferenceProperty(typeof(Button))]
        public string CropButton
        {
            get { return cropButton; }
            set { cropButton = value; }
        }


        string cropButtonID;
        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Category("CS")]
        public string CropButtonID {
            get {  return cropButtonID; }
            set {  cropButtonID = value; } 
        }

        private int x;
        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(10)]
        [Category("CS")]
        public int X {
            get {
                return ViewState["WCI_X"] == null ? 10 : Convert.ToInt32(ViewState["WCI_X"]);
            }
            set { ViewState["WCI_X"] = value; }
        }

        private int y;
        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(10)]
        [Category("CS")]
        public int Y {
            get { return ViewState["WCI_Y"] == null ? 10 : Convert.ToInt32(ViewState["WCI_Y"]); }
            set { ViewState["WCI_Y"] = value; }
        }








        private int x2;
        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(20)]
        [Category("CS")]
        public int X2
        {
            get
            {
                return ViewState["WCI_X2"] == null ? 20 : Convert.ToInt32(ViewState["WCI_X2"]);
            }
            set { ViewState["WCI_X2"] = value; }
        }

        private int y2;
        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(20)]
        [Category("CS")]
        public int Y2
        {
            get { return ViewState["WCI_Y2"] == null ? 20 : Convert.ToInt32(ViewState["WCI_Y2"]); }
            set { ViewState["WCI_Y2"] = value; }
        }











        private int w;
        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]        
        [Category("CS")]
        public int W {
            get { return ViewState["W"] == null ? 10 : Convert.ToInt32(ViewState["W"]); }
            set { ViewState["W"] = value; }
        }

        private int h;
        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]        
        [Category("CS")]
        public int H {
            get { return ViewState["H"] == null ? 10 : Convert.ToInt32(ViewState["H"]); ; }
            set { ViewState["H"] = value; }
        }


        private Unit Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }

        private Unit Height
        {
            get
            {
                return base.Height;
            }
            set
            {
                base.Height = value;
            }
        }

        private string scriptPath;
        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue("~/scripts/")]
        [Category("CS")]
        public string ScriptPath {
            get {  return scriptPath; }
            set {  scriptPath = value; }
        }


        private bool includeJQuery;
        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue(false)]
        [Category("CS")]
        public bool IncludeJQuery {
            get {  return includeJQuery; }
            set {  includeJQuery = value; }
        }


        string ratio;
        /// <summary>
        /// If you set ratio no need to set both W and H properties.
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Category("CS")]
        public string Ratio {
            get { return ratio; }
            set { ratio = value; }
        }

        bool isInUpdatePanel;
        /// <summary>
        /// If you set ratio no need to set both W and H properties.
        /// </summary>
        [Bindable(true)]
        [DefaultValue("false")]
        [Category("CS")]
        public bool IsInUpdatePanel
        {
            get { return isInUpdatePanel; }
            set { isInUpdatePanel = value; }
        }



        #endregion


    }
}
