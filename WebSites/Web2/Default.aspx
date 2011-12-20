<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    
    <link href="css/jquery.jcrop.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
     
        <cs:CropImage ID="CropImage1" runat="server"
            Image="Image1" CropButton="test" 
            MaxSize=100,100 MinSize=100,100
            W=100 H=100 X=10 Y=10
            IncludeJQuery="true" ScriptPath="~/scripts/" />
            
        <asp:Image runat="server" ImageUrl="~/images/cem01.jpg" id="Image1" />
            
        <asp:Button runat="server" ID="test" Text="Crop" onclick="test_Click" />
    
    
    
    
    </div>
    </form>
</body>
</html>
