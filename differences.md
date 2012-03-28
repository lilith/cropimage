---
layout: default
---

# Differences between CropImage.NET and WebCropImage

V5 fixes over 25 bugs (including memory leaks and wasteful RAM & IO usage) and introduces support for large images.

## New feature highlights

* High-resolution images can now be cropped without sending the whole image to the browser; instead, a scaled, compressed version is used. This improves loading times by 2-20x and should make tablet/mobile support better. To enable, set  ServerSideResizing="true" and make [2 changes in web.config](http://imageresizing.net/).

* Image previews are much smoother, and now work even without a fixed aspect ratio, but you must set PreviewImageId to the ID of  a server-side &lt;div runat="server"/> or &lt;asp:Panel runat="server" />. It cannot be the ID of an &lt;img/> or &lt;asp:Image /> control. 

* You can now force conversion to a specific image type with ForceImageFormat="jpg/png/gif". Tiff images are now automatically converted to Jpegs.

* Uses jQuery 1.7.1 instead of 1.3.2, and the latest version of JCrop. All javascript and css includes are now configurable, and can be individually replaced or hosted as files.

* You can now specify a checkbox to enable fixed-aspect-ratio mode with the FixedAspectRatioCheckboxID="id" property.

## Breaking changes

* The properties X2 and Y2 are now readonly. Set W and H instead. This change prevents inconsistent X/X2/W and Y/Y2/H values, which was previously a common problem.

* Users must now set FixedAspectRatio="true" when setting 'Ratio'. This breaking change was required to support FixedAspectRatioCheckboxID. 
PreviewImageId must be the server id of a Panel or Div, not an image.

* Removed Cropped and Cropping events; they are now redundant as X,Y, W,H, X2, Y2 are properly updated on postback and can be modified before calling Crop().

* IncludeJQuery property has been replaced by the JQueryInclude, JCropInclude, WebCropInclude, and JCropCssInclude properties and their associated enumerations. The default behavior for JQuery inclusion has changed to first attempt to use the Google CDN version, falling back to the embedded file. Other files default to WebResource.axd method. All behavior can be modified through the enumerations. You can even turn all (or individual) file inclusion off, and manually include the files for maximum control.

## Fixed bugs

* Will not leak massive amounts of memory when unhanded exceptions occur during cropping or saving of images to disk.
* Will not read original image into memory twice per postback; GetRawSize() has been removed. Server-side 'original size' 
processing has been replaced by usage of JCrop boxWidth/boxHeight feature (or, if enabled, server-side resizing). This makes it MUCH faster.
* 

## Stuff that finally got implemented

* .DebugMode - Now makes human-readable javascript files be used instead of minified versions.
Will use JQuery 1.7.1 instead of 1.3.2
Will not default to PNG or BMP when mime type is unknown, will default to jpeg.
Will not save images in .bmp or .tiff format, will default to jpeg.
.JPG, .PNG, and .GIF images will be saved as .JPG, .PNG, and .GIF images respectively.




webcropimage.js has been introduced - correct preview functionality requires several pages of code, so it has been placed into a cachable javascript resource file.
Removed GetMimeType - unreliable method of determining photo format, due to GDI bugs. Also will report PNG 
incorrectly on windows 7. New code will first examine file extension, followed by GDI format. 
Will also support PSD, RAW, and 24+ other formats if the FreeImageDecoder or WicDecoder plugins are installed.
The properties X2 and Y2 have been made readonly to prevent conflicting or inconsistent X/X2/W and Y/Y2/H values. 
No default cropping rectangle is required, and the default (10,10,20,20) has been removed.
Default Jpeg save quality is now 90, not 100 (much better file size, indistinguishable visual quality). This can be adjusted with the new JpegQuality property.
Added ForceImageFormat property if users want to force conversion to a specific image format. 
Added FixedAspectRatio and FixedAspectRatioCheckboxID so that aspect ratio locking can either be enabled via a property or dynamically, client-side, via a linked checkbox.
Users must now set FixedAspectRatio="true" when setting 'Ratio'. This breaking change was required to support FixedAspectRatioCheckboxID. 
Users must now set PreviewImageId to the ID of a server-side <div runat="server"/> or <asp:Panel runat="server" />. It cannot be the ID of an <img/> or <asp:Image /> control. 
The div/panel may have a width/height (which will be changed if PreviewWidth/PreviewHeight is set explicitly) , but doesn't need to contain anything. 
Users may now enable ServerSideResizing="true" to dynamically resize images before cropping them to (greatly!) improve page load times, reduce browser memory usage, increase speed, and stop crashing mobile devices. 
JCrop vNext will support touchscreen devices.
Removed Cropped and Cropping events; they are now redundant as X,Y, W,H, X2, Y2 are updated on postback and can be modified before calling Crop(). 
Removed many unused and unreferenced properties and members.