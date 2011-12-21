# WebCropImage

Allows easy image cropping. Add the control to a page that already contains the image you'd like to crop, and set the Image property to the ID of the Image control to crop.





## How to use server-side resizing?

You'll need to make a web.config change - follow the instructions at http://imageresizing.net/

Then, set `ServerSizeResize`="true" on the control. 

This will make large images load MUCH faster when cropping, and use much less browser memory. Large images will crash mobile devices unless you are using server-side resizing.

## How to force the cropping rectangle to preserve the aspect ratio of the original image?

Set `FixedAspectRatio`="true". If you want to allow the user to control the setting, set `FixedAspectRatioCheckboxID` to the ID of a Checkbox control.

If you want to set a specific aspect ratio instead of using the original, set `Ratio="16/9"`