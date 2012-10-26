# CropImage.NET (originally WebCropImage)

Simple, flexible, and configurable image cropping for ASP.NET.

This control does not directly generate markup (although it creates javascript references). 

Add this control to a page that already contains an Image control, then set the CropImage.ImageID property to link them.



## Basic settings

* ImageID="Image1" - (required) ID of the asp.net Image control to be cropped.
* CanvasWidth=integer - (required) The maximum display width for the uncropped image. If larger, the image will be scaled down (for display), maintaining aspect ratio.
* CanvasHeight=integer - (required) The maximum display height for the uncropped image. If larger, the image will be scaled down (for display), maintaining aspect ratio.
* ServerSideResize=True|False (Defaults to False)- Enables server-side resizing of uncropped images. Greatly increases client-side performance.
* JpegQuality=0..100 - Adjust the jpeg save quality. The default is 90, a very good balance with no visible artifacts.
* ForcedImageFormat=jpg|png|gif -  If set, forces all images to be converted to the specified format instead of retaining their original format. 

## Getting the result

There are several ways of getting the result. 

1. Use the 'CroppedUrl' property. This property will return a URL to the dynamically cropped image. This is the preferred method, as it is non-destructive - however, http://imageresizing.net must be installed in Web.config.
2. Use the 'Crop(saveAsPath)' method. This will save the cropped image to the given physical path.
3. Access the X, Y, W, and H properties. 


## Crop rectangle values

* X, Y, W,H, X2(readonly), Y2(readonly), - Gets or sets the crop coordinates for the image. Note that if ServerSideResize=True, these may be relative to CropXUnits and CropYUnits instead of the original image size. You CAN set X,Y,W, and H if you want to set the default selection.
* (readonly) CropXUnits - The width of the image used when cropping, when ServerSideResize=True.
* (readonly) CropYUnits - The height of the image used when cropping.
* (readonly) CroppedUrl - If provides a URL to the dynamically cropped image (requires http://imageresizing.net/ to be installed)


## External Preview Settings

* EnablePreview=True/False - When true, enables the preview window. You must also set the PreviewImageID value to the ID of a div or panel control.
* PreviewImageId="PanelOrDivID" -  The ID of the preview div or panel (NOT img tag). The preview div doesn't need to have any content.
* PreviewWidth=integer - The width of the preview
* PreviewHeight=int - The Height of the preview


## Constraining the crop rectangle

* MaxSize=w,h - Maximum permitted crop size, in "w,h" format. See CanvasWidth and CanvasHeight for the maximum display size of the image being croppe
* MinSize=w,h - Minimum permitted crop size in "w,h" format.
* Ratio="16/9" or "1.3" - The aspect ratio to enforce on the cropping rectangle. Defaults to the original aspect ratio, but only enforced if FixedAspectRatio=true. May be a fraction like 16/9 or a decimal like 1.3
* FixedAspectRatio=True|False - If true, the cropping rectangle will be forced to the original aspect ratio of the image (or 'Ratio', if set). Defaults to false. Not consulted if FixedAspectRatioCheckboxID is specified; the checkbox's value will be used instead.
* FixedAspectRatioCheckboxID="Checkbox1" - The ID of a checkbox to change the FixedAspectRatio setting. If set, FixedAspectRatio is ignored; the checkbox's default value is used instead.


## Customizing the javascript includes

* ScriptPath="~/scripts" - The path to the folder containing jquery, jcrop, and the jcrop css file. Defaults to ~/scripts/
* DebugMode=True|False - When set, uses human-readable javascript instead of minified versions
* IsInUpdatePanel=True|False - Set this to true if the control is within an update panel, so the scripts can be registered properly. Defaults to false.
* JQueryInclude=None|Google|ScriptFolder|Embedded - (Defaults to Google CDN-hosted version, falling back to to embedded WebResource.axd method when internet connection or firewall problems interfere.)
* JCropInclude=None|ScriptFolder|Embedded - Control how (or if) JCrop javascript is automatically refereced. Defaults to embedded WebResource.axd method.
* JCropCssInclude=None|ScriptFolder|Embedded - Control how (or if) JCrop css is automatically refereced. Defaults to embedded WebResource.axd method.
* WebCropInclude=None|ScriptFolder|Embedded - Control how (or if) webcropimage.js is automatically refereced. Defaults to embedded WebResource.axd method. 

       
## How to use server-side resizing?

You'll need to make a web.config change - follow the instructions at http://imageresizing.net/

Then, set `ServerSizeResize`="true" on the control. 

This will make large images load MUCH faster when cropping, and use much less browser memory. Large images will crash mobile devices unless you are using server-side resizing.

## How to force the cropping rectangle to preserve the aspect ratio of the original image?

Set `FixedAspectRatio`="true". If you want to allow the user to control the setting, set `FixedAspectRatioCheckboxID` to the ID of a Checkbox control.

If you want to set a specific aspect ratio instead of using the original, set `Ratio="16/9"`
