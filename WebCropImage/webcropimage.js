//image - '<img />' tag ID, DOM, or jquery reference
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


	//Find the original aspect ratio of the image
	var originalRatio = image.width() / image.height()

    //Find the checkbox (if present) and the default lock aspect ratio value
	var checkbox = settings.keepAspectRatioCheckbox ? $(settings.keepAspectRatioCheckbox) : null;
	var keepRatio = checkbox ? checkbox.is(':checked') : (settings.keepAspectRatio == true);

	var aspectRatio = settings.aspectRatio > 0 ? settings.aspectRatio : originalRatio;

	//Find the URL of the original image minus the querystring.
	var path = image.attr('src');
	if (path.indexOf('?') > 0) path = path.substr(0, path.indexOf('?'));
	if (path.indexOf(';') > 0) path = path.substr(0, path.indexOf(';')); //For parsing Amazon-cloudfront compatible querystrings

	var cloudFront = image.attr('src').indexOf(';') > -1; //To use CloudFront-friendly URLs.

	//Find the preview div(s) (if they exist) and make sure the have a set height and width.
	var divPreview = settings.previewDiv ? $(settings.previewDiv) : null;
	if (divPreview) {
	    //What size to make the preview window (defaults to existing width/height if specified in 'style' attribute)
	    var previewFallbackWidth = 100;
	    var previewFallbackHeight = 100;

	    //Allow the div to override the default width and height in the style attribute
	    var previewMaxWidth = (divPreview.attr('style') != null && divPreview.attr('style').indexOf('width') > -1) ? divPreview.width() : previewFallbackWidth;
	    var previewMaxHeight = (divPreview.attr('style') != null && divPreview.attr('style').indexOf('height') > -1) ? divPreview.height() : previewFallbackHeight;
	    previewMaxWidth = settings.previewWidth > 0 ? settings.previewWidth : previewMaxWidth;
	    previewMaxHeight = settings.previewHeight > 0 ? settings.previewHeight : previewMaxHeight;

	    //Set the values explicitly.
	    divPreview.css({
	        width: previewMaxWidth + 'px',
	        height: previewMaxHeight + 'px',
	        overflow: 'hidden'
	    });

	    //clear the div contents for backwards compatibility.
	    divPreview.empty();

	    //Create another child div and style it to form a 'clipping rectangle' for the preview div.
	    var innerPreview = $('<div />').css({
	        overflow: 'hidden'
	    }).addClass('innerPreview').appendTo(divPreview);

	   //Create a copy of the image inside the inner preview div(s)
	    var innerPreviewImg = $('<img />').attr('src', image.attr('src')).appendTo(innerPreview);
	}

	var links = settings.updateLinks ? $(settings.updateLinks) : null;
	var fields = settings.updateFields ? $(settings.updateFields) : null;
	var images = settings.updateImages ? $(settings.updateImages) : null;


	//Create a function to update the link, hidden input, and preview pane
	var update = function (coords) {
	    if (parseInt(coords.w) <= 0 || parseInt(coords.h) <= 0) return; //Require valid width and height


	    if (divPreview) {
	        //The aspect ratio of the cropping rectangle. If 'keepRatio', use originalRatio since it's more precise.
	        var cropRatio = keepRatio ? aspectRatio : (coords.w / coords.h);


	        //When the selection aspect ratio changes, the preview clipping area has to also.
	        //Calculate the width and height.

	        var innerWidth = cropRatio >= (previewMaxWidth / previewMaxHeight) ? previewMaxWidth : previewMaxHeight * cropRatio;
	        var innerHeight = cropRatio < (previewMaxWidth / previewMaxHeight) ? previewMaxHeight : previewMaxWidth / cropRatio;

	        //Use the inner preview div to center the image within the outer preview div.
	        innerPreview.css({
	            width: Math.ceil(innerWidth) + 'px',
	            height: Math.ceil(innerHeight) + 'px',
	            marginTop: (previewMaxHeight - innerHeight) / 2 + 'px',
	            marginLeft: (previewMaxWidth - innerWidth) / 2 + 'px',
	            overflow: 'hidden'
	        });


	        //Calculate how much we are shrinking the image inside the preview window
	        var scalex = innerWidth / coords.w;
	        var scaley = innerHeight / coords.h;

	        //Set the width and height of the image so the right areas appear at the right scale appear.
	        innerPreviewImg.css({
	            width: Math.round(scalex * image.width()) + 'px',
	            height: Math.round(scaley * image.height()) + 'px',
	            marginLeft: '-' + Math.round(scalex * coords.x) + 'px',
	            marginTop: '-' + Math.round(scaley * coords.y) + 'px'
	        });

	    }

	    //Calculate the querystring
	    var query = '?';

	    //Add final size, if specified.
	   // var inputWidth = container.find('input.width');
	   // var inputHeight = container.find('input.height');
	   // if (inputWidth.size() > 0 && parseInt(inputWidth.val()) > 1) query += 'maxwidth=' + inputWidth.val() + '&';
	   // if (inputHeight.size() > 0 && parseInt(inputHeight.val()) > 1) query += 'maxheight=' + inputHeight.val() + '&';

	    //Add crop rectangle
	    query += '&crop=(' + coords.x + ',' + coords.y + ',' + coords.x2 + ',' + coords.y2 + ')&cropxunits=' + image.width() + '&cropyunits=' + image.height()
	    //Replace ? and & with ; if using Amazon Cloudfront
	    if (cloudFront) query = query.replace(/\?\&/g, ';');

	    //Now, update the links and input values.
	    if (links) links.attr('href', path + query);
	    if (fields) fields.attr('value', path + query);
	    if (images) images.attr('value', path + query);

	}

	//Start up jCrop
	var jcrop_reference = image.Jcrop({
	    onChange: update,
	    onSelect: update,
	    aspectRatio: keepRatio ? aspectRatio : null,
	    minSize: settings.minSize,
	    maxSize: settings.maxSize,
	    boxWidth: settings.boxWidth,
	    boxHeight: settings.boxHeight,
        setSelect: settings.setSelect,
	    bgColor: 'black',
	    bgOpacity: 0.6
	});

	//Call the function to init the preview windows
	update({ x: 0, y: 0, x2: image.width(), y2: image.height(), w: image.width(), h: image.height() });


	if (checkbox) {
	    //Handle the 'lock ratio' checkbox change vent
	    checkbox.change(function (e) {
	        //Update keepRatio value
	        keepRatio = this.checked;

	        //Update the jcrop settings
	        jcrop_reference.setOptions({ aspectRatio: keepRatio ? aspectRatio : null });
	        jcrop_reference.focus();
	    });
	}

}
