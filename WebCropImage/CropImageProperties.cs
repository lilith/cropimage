using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using drawing = System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using System.Net;



namespace CS.Web.UI {
    public partial class CropImage : CompositeControl, INamingContainer {

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


        string cropButton;

        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [Category("CS")]
        [IDReferenceProperty(typeof(Button))]
        public string CropButton {
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
            get { return cropButtonID; }
            set { cropButtonID = value; }
        }


        private bool serverSizeResize = false;
        /// <summary>
        /// If true, server-side resizing will be used instead of client-size resizing when display the image to be cropped. 
        /// Only relevant when CanvasWidth and/or CanvasHeight are set.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(false)]
        [Category("CS")]
        public bool ServerSizeResize {
            get { return serverSizeResize; }
            set { serverSizeResize = value; }
        }


        private int jpegQuality = 90;
        /// <summary>
        /// Adjust the default jpeg save quality. The default is 90, a very good balance with no visible artifacts.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(90)]
        [Category("CS")]
        public int JpegQuality {
            get { return jpegQuality; }
            set { jpegQuality = value; }
        }

        private string forcedImageFormat = null;
        /// <summary>
        /// If set, forces all images to be converted to the specified format instead of retaining their original format. 
        /// Example values: "jpg", "png", "gif", null (default)
        ///
        /// </summary>
        [Bindable(true)]
        [DefaultValue(null)]
        [Category("CS")]
        public string ForcedImageFormat {
            get { return forcedImageFormat; }
            set { forcedImageFormat = value; }
        }




        /// <summary>
        /// In V5+, uses human-readable javascript files when enabled. Previous versions do nothing.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(false)]
        [Category("CS")]
        public bool DebugMode {
            get { return ViewState["DM"] == null ? false : Convert.ToBoolean(ViewState["DM"]); }
            set { ViewState["DM"] = value; }
        }

        /// <summary>
        /// The maximum display width for the uncropped image. If larger, the image will be scaled down (for display), maintaining aspect ratio.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int CanvasWidth {
            get { return ViewState["CW"] == null ? 0 : Convert.ToInt32(ViewState["CW"]); }
            set { ViewState["CW"] = value; }
        }

        /// <summary>
        /// The maximum display height for the uncropped image. If larger, the image will be scaled down (for display), maintaining aspect ratio.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int CanvasHeight {
            get { return ViewState["CH"] == null ? 0 : Convert.ToInt32(ViewState["CH"]); }
            set { ViewState["CH"] = value; }
        }


        /// <summary>
        /// When true, enables the preview window. You must also set the PreviewImageID value to the ID of a div or panel control.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(false)]
        [Category("CS")]
        public bool EnablePreview {
            get { return ViewState["EP"] == null ? false : Convert.ToBoolean(ViewState["EP"]); }
            set { ViewState["EP"] = value; }
        }

        /// <summary>
        /// The width of the preview div.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int PreviewWidth {
            get { return ViewState["EPH"] == null ? 0 : Convert.ToInt32(ViewState["EPH"]); }
            set { ViewState["EPH"] = value; }
        }

        /// <summary>
        /// The height of the preview div
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int PreviewHeight {
            get { return ViewState["EPH"] == null ? 0 : Convert.ToInt32(ViewState["EPH"]); }
            set { ViewState["EPH"] = value; }
        }

        /// <summary>
        /// The ID of the preview div or panel (NOT img tag). The preview div doesn't need to have any content.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(false)]
        [Category("CS")]
        public string PreviewImageID {
            get { return ViewState["EPIMAGEID"] == null ? null : Convert.ToString(ViewState["EPIMAGEID"]); }
            set { ViewState["EPIMAGEID"] = value; }
        }

   


        private string imageId;
        /// <summary>
        /// ID of the asp.net Image control to be cropped.
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Category("CS")
            , Description("ID of the asp.net Image control to be cropped.")]
        public string Image {
            get {
                return imageId;
            }
            set {
                imageId = value;
            }
        }


        private string maxSize;
        /// <summary>
        /// Maximum permitted crop size, in "w,h" format. See CanvasWidth and CanvasHeight for the maximum display size of the image being cropped.
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Category("CS")
            , Description("[w,h]")]
        public string MaxSize {
            get {

                return maxSize;
            }
            set {

                maxSize = value;
            }
        }

        private string minSize;
        /// <summary>
        /// Minimum permitted crop size in "w,h" format 
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Category("CS")
            , Description("[w,h]")]
        public string MinSize {
            get {

                return minSize;
            }
            set {

                minSize = value;
            }
        }


 

        /// <summary>
        /// Gets or sets the X value for the cropping rectangle.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int X {
            get {
                return ViewState["WCI_X"] == null ? 0 : Convert.ToInt32(ViewState["WCI_X"]);
            }
            set { ViewState["WCI_X"] = value; }
        }

        /// <summary>
        /// Gets or sets the Y value for the cropping rectangle.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int Y {
            get { return ViewState["WCI_Y"] == null ? 0 : Convert.ToInt32(ViewState["WCI_Y"]); }
            set { ViewState["WCI_Y"] = value; }
        }


        /// <summary>
        /// (Readonly) Gets the X2 value for the cropping rectangle.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int X2 {
            get {
                return X + W;
            }
        }

        /// <summary>
        /// (Readonly) Gets the Y2 value for the cropping rectangle.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int Y2 {
            get { return Y + H; }
        }

        /// <summary>
        /// Gets or sets the width of the crop rectangle
        /// </summary>
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("CS")]
        public int W {
            get {
                return ViewState["WCI_W"] == null ? 0 : Convert.ToInt32(ViewState["WCI_W"]);
            }
            set { ViewState["WCI_W"] = value; }
        }

        /// <summary>
        /// Gets or sets the height of the crop rectangle
        /// </summary>
        [Bindable(true)]
        [Category("CS")]
        [DefaultValue(0)]
        public int H {
            get {
                return ViewState["WCI_H"] == null ? 0 : Convert.ToInt32(ViewState["WCI_H"]);
            }
            set { ViewState["WCI_H"] = value; }
        }

        /// <summary>
        /// We're hiding the Width field, it's pointless, since this control doesn't render, just affects the rendering of other controls.
        /// </summary>
        private new Unit Width {
            get {
                return base.Width;
            }
            set {
                base.Width = value;
            }
        }
        /// <summary>
        /// We're hiding the Height field, it's pointless, since this control doesn't render, just affects the rendering of other controls.
        /// </summary>
        private new Unit Height {
            get {
                return base.Height;
            }
            set {
                base.Height = value;
            }
        }

        private string scriptPath;
        /// <summary>
        /// The path to the folder containing jquery, jcrop, and the jcrop css file. Defaults to ~/scripts/
        /// </summary>
        [Bindable(true)]
        [DefaultValue("~/scripts/")]
        [Category("CS")]
        public string ScriptPath {
            get { return scriptPath; }
            set { scriptPath = value; }
        }


        string ratio;
        /// <summary>
        /// The aspect ratio to enforce on the cropping rectangle. Defaults to the original aspect ratio, but only enforeced if FixedAspectRatio=true. 
        /// May be a fraction like 16/9 or a decimal like 1.3
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Category("CS")]
        public string Ratio {
            get { return ratio; }
            set { ratio = value; }
        }


        /// <summary>
        /// If true, the cropping rectangle will be forced to the original aspect ratio of the image (or 'Ratio', if set). Defaults to false.
        /// Not consulted if FixedAspectRatioCheckboxID is specified; the checkbox's value will be used instead.
        /// </summary>
        [Bindable(true)]
        [DefaultValue(false)]
        [Category("CS")]
        public bool FixedAspectRatio {
            get { return ViewState["FAR"] == null ? false : Convert.ToBoolean(ViewState["FAR"]); }
            set { ViewState["FAR"] = value; }
        }

        private string fixedAspectRatioCheckboxID = null;
        /// <summary>
        /// The ID of a checkbox to change the FixedAspectRatio setting. If set, FixedAspectRatio is ignored; the checkbox's default value is used instead.
        /// </summary>
        public string FixedAspectRatioCheckboxID {
            get { return fixedAspectRatioCheckboxID; }
            set { fixedAspectRatioCheckboxID = value; }
        }



        bool isInUpdatePanel;
        /// <summary>
        /// Set this to true if the control is within an update panel, so the scripts can be registered properly.
        /// </summary>
        [Bindable(true)]
        [DefaultValue("false")]
        [Category("CS")]
        public bool IsInUpdatePanel {
            get { return isInUpdatePanel; }
            set { isInUpdatePanel = value; }
        }



        private JQueryIncludeMode jQueryInclude = JQueryIncludeMode.Google;
        /// <summary>
        /// Control how (or if) JQuery is automatically refereced.
        /// Defaults to Google CDN-hosted version, falling back to to embedded WebResource.axd method when internet connection or firewall problems interfere.
        /// </summary>
        public JQueryIncludeMode JQueryInclude {
            get { return jQueryInclude; }
            set { jQueryInclude = value; }
        }

        private JCropIncludeMode jCropInclude = JCropIncludeMode.Embedded;
        /// <summary>
        /// Control how (or if) JCrop javascript is automatically refereced. Defaults to embedded WebResource.axd method.
        /// </summary>
        public JCropIncludeMode JCropInclude {
            get { return jCropInclude; }
            set { jCropInclude = value; }
        }

        private JCropCssIncludeMode jCropCssInclude = JCropCssIncludeMode.Embedded;
        /// <summary>
        /// Control how (or if) JCrop css is automatically refereced. Defaults to embedded WebResource.axd method.
        /// </summary>
        public JCropCssIncludeMode JCropCssInclude {
            get { return jCropCssInclude; }
            set { jCropCssInclude = value; }
        }


        private WebCropImageIncludeMode webCropInclude = WebCropImageIncludeMode.Embedded;
        /// <summary>
        /// Control how (or if) webcropimage.js is automatically refereced. Defaults to embedded WebResource.axd method.
        /// </summary>
        public WebCropImageIncludeMode WebCropInclude {
            get { return webCropInclude; }
            set { webCropInclude = value; }
        }

       
    }
}
