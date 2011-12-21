<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="RatioSample.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web Crop Image - Ratio Sample</title>
    
    
    
</head>
<body>


    <form id="form1" runat="server">
    
    <div>                
        
        <cs:CropImage ID="wci1" runat="server" 
            Image="Image1"            
            Ratio="1/1"
            FixedAspectRatio="true"
            X="150"           
            Y="150"           
             />                
        
        <asp:Image ID="Image1" runat="server" ImageUrl="images/328.jpg" />        
        
     </div> 
         
    <br />    
    <asp:Button ID="btnCrop" runat="server" Text="Crop" onclick="btnCrop_Click" />
    <br />
    
    <br /><br />
    <a href="http://webcropimage.codeplex.com">Go to the project website on CodePlex</a>
    <br /><br />
    </form>
    
    
    
    
    
    
    
    
</body>
</html>
