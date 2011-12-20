<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StreamSample.aspx.cs" Inherits="StreamSample" %>
<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web Crop Image - Stream Sample</title>
</head>
<body>


    <form id="form1" runat="server">
 
       
       
        
    <div>                
        <asp:Image ID="Image1" runat="server" ImageUrl="http://samples.cemsisman.com/webcropimage/StreamImage.aspx" />                
    </div>
    
    <br />
    
    <asp:Button ID="btnCrop" runat="server" Text="Crop" onclick="btnCrop_Click" />
    
    <p> 
        
        <cs:CropImage ID="wci1" runat="server" 
            Image="Image1"
            X="10"
            Y="10"
            X2="50"
            Y2="50" 
             />   
        
    </p>
    
    
    <br /><br />
    <a href="http://webcropimage.codeplex.com">Go to the project website on CodePlex</a>
   <br /><br />

   
    
    </form>
    
    
    
    
    
    
    
    
    
    
    
    
    
    <script type="text/javascript">
        var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
        document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
    try {
        var pageTracker = _gat._getTracker("UA-6577322-1");
        pageTracker._setDomainName("samples.cemsisman.com");
        pageTracker._trackPageview();
    } catch (err) { }</script>





</body>
</html>
