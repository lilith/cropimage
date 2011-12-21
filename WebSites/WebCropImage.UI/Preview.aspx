<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Preview.aspx.cs" Inherits="Preview" %>

<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web Crop Image - Preview Sample</title>
</head>
<body>



    <form id="form1" runat="server">
    
    <div>                
        <asp:Image ID="Image1" runat="server" ImageUrl="images/328.jpg" />                
    </div>
    
    <br />
    
    <asp:Button ID="btnCrop" runat="server" Text="Crop" onclick="btnCrop_Click" />
    
    <p> 
        
        <cs:CropImage ID="wci1" runat="server" 
            Image="Image1"
            X="10"
            Y="10"
            X2="150"
            Y2="150"            
            EnablePreview="true"
            PreviewImageID="preview"
            PreviewWidth ="100"
            PreviewHeight="100"
             />   
        
    </p>
    
    <asp:Panel ID="preview" runat="server">
    </asp:Panel>


    
    </form>
    
    

</body>
</html>
