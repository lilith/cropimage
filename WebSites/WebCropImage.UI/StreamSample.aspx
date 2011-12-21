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
        <asp:Image ID="Image1" runat="server" ImageUrl="StreamImage.aspx" />                
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
    
    
    
    
    
    
    
    
    

</body>
</html>
