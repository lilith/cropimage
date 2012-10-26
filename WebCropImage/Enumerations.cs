using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Web.UI {
    /// <summary>
    /// jquery include method
    /// </summary>
    public enum JQueryIncludeMode {
        /// <summary>
        /// jQuery will not be included - you must include it manually on all pages that have the control
        /// </summary>
        None,
        /// <summary>
        /// First, the browser will attempt to load jquery from the Google CDN. If that fails, the embedded version will be used.
        /// </summary>
        Google,
        /// <summary>
        /// First, the browser will attempt to load jQuery from the folder specified in the ScriptsFolder property.  If that fails, the embedded version will be used.
        /// </summary>
        ScriptFolder,
        
        /// <summary>
        /// Only the embedded version will be attempted.
        /// </summary>
        Embedded
    }

    /// <summary>
    /// jquery.Jcrop.js include method
    /// </summary>
    public enum JCropIncludeMode {
        /// <summary>
        /// jquery.jcrop.cs will not be loaded. You must include jquery.Jcrop.js yourself.
        /// </summary>
        None,
        /// <summary>
        /// Jcrop (javascript) will be loaded from the folder specified in the ScriptFolder property, with fallback to the embedded javascript
        /// </summary>
        ScriptFolder,
        /// <summary>
        /// Only the embedded javascript for JCrop will be loaded.
        /// </summary>
        Embedded
    }
    /// <summary>
    /// webcropimage.js include method
    /// </summary>
    public enum WebCropImageIncludeMode {
        /// <summary>
        /// webcropimage.js will not be loaded. You must include webcropimage.js yourself.
        /// </summary>
        None,
        /// <summary>
        /// webcropimage.js will be loaded from the folder specified in the ScriptFolder property, with fallback to the embedded javascript
        /// </summary>
        ScriptFolder,
        /// <summary>
        /// Only the embedded webcropimage.js will be loaded.
        /// </summary>
        Embedded
    }

    /// <summary>
    /// jquery.jcrop.css include method
    /// </summary>
    public enum JCropCssIncludeMode {
        /// <summary>
        /// No css for JCrop will be loaded. You must include jquery.Jcrop.css yourself.
        /// </summary>
        None,
        /// <summary>
        /// jquery.jcrop.css will be loaded from the folder specified in the ScriptFolder property.
        /// </summary>
        ScriptFolder,
        /// <summary>
        /// Only the embedded css for JCrop will be loaded.
        /// </summary>
        Embedded
    }

}
