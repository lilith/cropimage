/*This file contains two parts, jquery.jcrop.preview, and webcropimage() */


/* A tiny jQuery plugin to provide live previews for Jcrop
Init by calling on an empty div which you'd like to contain the preview.
You can specify a width and height using the defaultWidth and defaultHeight settings, although CSS width/height specified on the div take precedence.
Ex:
$("div.preview").JcropPreview({ jcropImg: $("img.primaryImage") });
 
After initializing the preview, then you can start up Jcrop. If you don't have any special onSelect event handling to do, call $("div.preview").JcropPreviewUpdateFn() to create a handler for the event:

Ex. 

$("img.primaryImage").Jcrop({
onChange: $("div.preview").JcropPreviewUpdateFn(),
onSelect: $("div.preview").JcropPreviewUpdateFn()});

//If you have a custom function, just call the update function directly.
    
$("div.preview").JcropPreviewUpdate(coords); 

This software is MIT licensed.

Copyright (c) 2012 Nathanael Jones

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

(function ($) {
    $.fn.JcropPreview = function (options) {

        var defaults = {
            jcropImg: null,
            defaultWidth: 100,
            defaultHeight: 100
        };

        var options = $.extend(defaults, options);
        options.jcropImg = $(options.jcropImg);

        return this.each(function () {
            var obj = $(this);


            //Clear all previous contents
            obj.empty();

            //Allow the div to override the default width and height in the style attribute
            var previewMaxWidth = (obj.attr('style') != null && obj.attr('style').indexOf('width') > -1) ? obj.width() : options.defaultWidth;
            var previewMaxHeight = (obj.attr('style') != null && obj.attr('style').indexOf('height') > -1) ? obj.height() : options.defaultHeight;
            //Set the values explicitly.
            obj.css({
                width: previewMaxWidth + 'px',
                height: previewMaxHeight + 'px',
                overflow: 'hidden'
            });

            //Create another child div and style it to form a 'clipping rectangle' for the preview div.
            var innerPreview = $('<div />').css({
                overflow: 'hidden'
            }).addClass('innerPreview').appendTo(obj);


            //Create a copy of the image inside the inner preview div(s)
            var innerImg = $('<img />').attr('src', options.jcropImg.attr('src')).appendTo(innerPreview);


            var update = function (coords) {
                //Require valid width and height to do anything
                if (parseInt(coords.w) <= 0 || parseInt(coords.h) <= 0) return; //Require valid width and height
                //Resolve JCrop image target to jCrop API reference.
                if (options.jcropRef == null) options.jcropRef = $(options.jcropImg).data('Jcrop');

                var imgSize = options.jcropRef.getWidgetSize();
                var scale = options.jcropRef.getScaleFactor();
                imgSize[0] *= scale[0];
                imgSize[1] *= scale[1];

                var jopts = options.jcropRef.getOptions;

                //The aspect ratio of the cropping rectangle.
                var cropRatio = coords.w / coords.h;
                // Used forced ratio if present, as it is more precise and fixes jitter
                if (jopts != null && jopts().aspectRatio) cropRatio = jopts().aspectRatio;

                //When the selection aspect ratio changes, the preview clipping area has to also.
                //Calculate the width and height.
                var innerWidth = cropRatio >= (previewMaxWidth / previewMaxHeight) ? previewMaxWidth : previewMaxHeight * cropRatio;
                var innerHeight = cropRatio < (previewMaxWidth / previewMaxHeight) ? previewMaxHeight : previewMaxWidth / cropRatio;

                innerPreview.css({
                    width: Math.ceil(innerWidth) + 'px',
                    height: Math.ceil(innerHeight) + 'px',
                    marginTop: (previewMaxHeight - innerHeight) / 2 + 'px',
                    marginLeft: (previewMaxWidth - innerWidth) / 2 + 'px',
                    overflow: 'hidden'
                });
                //Set the outer div's padding so it stays centered
                obj.css({

                });

                //Calculate how much we are shrinking the image inside the preview window
                var scalex = innerWidth / coords.w;
                var scaley = innerHeight / coords.h;

                //Set the width and height of the image so the right areas appear at the right scale appear.
                innerImg.css({
                    width: Math.round(scalex * imgSize[0]) + 'px',
                    height: Math.round(scaley * imgSize[1]) + 'px',
                    marginLeft: '-' + Math.round(scalex * coords.x) + 'px',
                    marginTop: '-' + Math.round(scaley * coords.y) + 'px'
                });
            };

            obj.data('updateFunc', update);
        });
    };

    $.fn.JcropPreviewUpdate = function (coords) {
        return this.each(function () {
            $(this).data('updateFunc')(coords);
        });
    };
    $.fn.JcropPreviewUpdateFn = function () {
        var t = $(this);
        return function (coords) {
            t.data('updateFunc')(coords);
        };
    };

})(jQuery);



/*//image - '<img />' tag ID, DOM, or jquery reference
//settings - object

/* 
settings = {keepAspectRatio:false,
keepAspectRatioCheckbox: '#id' or reference,
aspectRatio: null or value,
minSize: [w,h],
maxSize: [w,h],
previewDiv: '#id' or reference or null,
previewWidth: (previewDiv original width, or 100),
previewHeight: (previewDiv original height, or 100),
boxWidth: null or value,
boxHeight: null or value,
setSelect: [x1,y1,x2,y2],
updateLinks: "#id1, #id2" or null,
updateFields: "#id1, #id2" or null,
updateImages: "#id1, #id2" or null
*/


function webcropimage(image, settings) {
    image = $(image);
    
    //root closure object
    var cl = {};

    //Find the original aspect ratio of the image
    cl.ow = image.width();
    cl.oh = image.height();
    cl.originalRatio = cl.ow / cl.oh;

    //Find the checkbox (if present) and the default lock aspect ratio value
	cl.checkbox = settings.keepAspectRatioCheckbox ? $(settings.keepAspectRatioCheckbox) : null;
	cl.keepRatio = cl.checkbox ? cl.checkbox.is(':checked') : (settings.keepAspectRatio == true);

	cl.aspectRatio = settings.aspectRatio > 0 ? settings.aspectRatio : cl.originalRatio;

	//Find the URL of the original image minus the querystring.
	cl.origPath = image.attr('src');
	var path = cl.origPath;
	if (path.indexOf('?') > 0) path = path.substr(0, path.indexOf('?'));
	if (path.indexOf(';') > 0) path = path.substr(0, path.indexOf(';')); //For parsing Amazon-cloudfront compatible querystrings

	cl.path = path;
	cl.cloudFront = image.attr('src').indexOf(';') > -1; //To use CloudFront-friendly URLs.



	//Create a function to update the link, hidden input, and preview pane
	var links = settings.updateLinks ? $(settings.updateLinks) : null;
	var fields = settings.updateFields ? $(settings.updateFields) : null;
	var images = settings.updateImages ? $(settings.updateImages) : null;
	var update = function (coords) {
	    if (parseInt(coords.w) <= 0 || parseInt(coords.h) <= 0) return; //Require valid width and height

	    //Update the preview
	    if (cl.preview) cl.preview.JcropPreviewUpdate(coords);

	    //Calculate the querystring
	    var query = '?crop=' + coords.x + ',' + coords.y + ',' + coords.x2 + ',' + coords.y2 + '&cropxunits=' + cl.ow + '&cropyunits=' + cl.oh;
	    //Replace ? and & with ; if using Amazon Cloudfront
	    if (cl.cloudFront) query = query.replace(/\?\&/g, ';');

	    //Now, update the links and input values.
	    if (links) links.attr('href', path + query);
	    if (fields) fields.attr('value', path + query);
	    if (images) images.attr('value', path + query);
	};

	//Start up preview (yes, this can happen first)
	if (settings.previewDiv) {
	    cl.preview = $(settings.previewDiv);
	    cl.preview.JcropPreview({ jcropImg: image, defaultWidth: settings.previewWidth, defaultHeight: settings.previewHeight });
	}
    

	//Start up jCrop
    image.Jcrop({
	    onChange: update,
	    onSelect: update,
	    aspectRatio: cl.keepRatio ? cl.aspectRatio : null,
	    minSize: settings.minSize,
	    maxSize: settings.maxSize,
	    boxWidth: settings.boxWidth,
	    boxHeight: settings.boxHeight,
        setSelect: settings.setSelect,
	    bgColor: 'black',
	    bgOpacity: 0.6
	}, function () {
        //Called when loaded
	    cl.jcrop = this;

        //Init preview to entire image
	    if (cl.preview) cl.preview.JcropPreviewUpdate({ x: 0, y: 0, x2: cl.ow, y2: cl.oh, width: cl.ow, height: cl.oh });
	    //if (settings.setSelect != null) this.setSelect(settings.setSelect);


	    //Link up checkbox
	    if (cl.checkbox) {
	        //Handle the 'lock ratio' checkbox change vent
	        cl.checkbox.change(function (e) {
	            //Update cl.keepRatio value
	            cl.keepRatio = this.checked;

	            //Update the jcrop settings
	            cl.jcrop.setOptions({ aspectRatio: cl.keepRatio ? cl.aspectRatio : null });
	            //cl.jcrop.focus();
	        });
	    }

	});


}
