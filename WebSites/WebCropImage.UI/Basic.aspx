<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Basic.aspx.cs" Inherits="Basic" %>
<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Single basic example</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>Basic example</h3>
        <asp:Image ID="Image1" runat="server" ImageUrl="images/328.jpg" /> <br />
    
        <cs:CropImage ID="CropImage1" runat="server"  Image="Image1" CanvasWidth="300" />   <br />
        
        <asp:Button ID="Button1" runat="server" Text="Crop" onclick="btnCrop_Click" />

        <h3>The result</h3>

        <asp:Image ID="Result" runat="server" Visible=false/> 
    </div>
    </form>
</body>
</html>
