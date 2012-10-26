<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CropImage.NET Examples</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>CropImage.NET Examples</h1>

    See readme.md in the root of the download for the most comprehensive documentation. Also, check out <a href="Basic.aspx" runat="server">the basic example if this page is too difficult to dissect</a>.


    <h3>Last crop results:</h3>
    <div>
    <asp:Label ID="message" runat="server" /><br />
    <asp:Label ID="coords" runat="server" /><br />
    <asp:Hyperlink ID="cropped" runat="server" /><br />
    <asp:Image ID="result" runat="server" Visible = false />
    </div>

    <h3>Basic example</h3>

    <pre> &lt;cs:CropImage ID="CropImage1" runat="server"  Image="Image1" CanvasWidth="300" />   </pre>
                    
    <asp:Image ID="Image1" runat="server" ImageUrl="images/328.jpg" /> <br />
    
    <cs:CropImage ID="CropImage1" runat="server"  Image="Image1" CanvasWidth="300" />   
        
    <asp:Button ID="Button1" runat="server" Text="Crop" onclick="btnCrop_Click" />

    <h3>Keep aspect ratio</h3>

    <pre> &lt;cs:CropImage ID="CropImage2" runat="server"  Image="Image2" CanvasWidth="300"  FixedAspectRatio="true"  />   </pre>
                    
    <asp:Image ID="Image2" runat="server" ImageUrl="images/328.jpg" /> <br />
    <cs:CropImage ID="CropImage2" runat="server"  Image="Image2" CanvasWidth="300" FixedAspectRatio="true" />   
    <asp:Button ID="Button2" runat="server" Text="Crop" onclick="btnCrop_Click" />

    <h3>Custom aspect ratio</h3>

    <pre> &lt;cs:CropImage ID="CropImage3" runat="server"  Image="Image3" CanvasWidth="300"  FixedAspectRatio="true" Ratio="16/9"  />   </pre>
                    
    <asp:Image ID="Image3" runat="server" ImageUrl="images/328.jpg" /> <br />
    <cs:CropImage ID="CropImage3" runat="server"  Image="Image3" CanvasWidth="300" FixedAspectRatio="true" Ratio="16/9" />   
    <asp:Button ID="Button3" runat="server" Text="Crop" onclick="btnCrop_Click" />


    <h3>Crop with live preview</h3>

      
    <pre> &lt;cs:CropImage ID="CropImage5" runat="server"  Image="Image5" CanvasWidth="300"  PreviewImageID="Preview5" PreviewHeight="150" PreviewWidth="150"/>   </pre>
    <asp:Panel ID="Preview5" runat="server"></asp:Panel>

    <asp:Image ID="Image5" runat="server" ImageUrl="images/328.jpg" /> <br />
    <cs:CropImage ID="CropImage5" runat="server"  Image="Image5" CanvasWidth="300" PreviewImageID="Preview5" PreviewHeight="150" PreviewWidth="150" />   
    <asp:Button ID="Button5" runat="server" Text="Crop" onclick="btnCrop_Click" />

    <h3>Crop with server-side source file resizing</h3>

      <pre> &lt;cs:CropImage ID="CropImage6" runat="server"  Image="Image6" CanvasWidth="300"  ServerSizeResize="true" />   </pre>
    
    <asp:Image ID="Image6" runat="server" ImageUrl="images/328.jpg" /> <br />
    <cs:CropImage ID="CropImage6" runat="server"  Image="Image6" CanvasWidth="300"  ServerSizeResize="true" />   
    <asp:Button ID="Button6" runat="server" Text="Crop" onclick="btnCrop_Click" />

        <h3>Crop with min/max size of 100,100 and 150,1500</h3>

    <pre> &lt;cs:CropImage ID="CropImage7" runat="server"  Image="Image7" MinSize="100,100" MaxSize="150,150" />   </pre>
                    
    <asp:Image ID="Image7" runat="server" ImageUrl="images/328.jpg" /> <br />
    <cs:CropImage ID="CropImage7" runat="server"  Image="Image7" CanvasWidth="300" MinSize="100,100" MaxSize="150,150" />   
    <asp:Button ID="Button7" runat="server" Text="Crop" onclick="btnCrop_Click" />


            <h3>Provide a default selection rectangle</h3>

    <pre> &lt;cs:CropImage ID="CropImage8" runat="server"  Image="Image8"  X="100" Y="100" W="100" H="100" />   </pre>
                    
    <asp:Image ID="Image8" runat="server" ImageUrl="images/328.jpg" /> <br />
    <cs:CropImage ID="CropImage8" runat="server"  Image="Image8" CanvasWidth="300" X="100" Y="100" W="100" H="100" />   
    <asp:Button ID="Button8" runat="server" Text="Crop" onclick="btnCrop_Click" />


        <h3>Fixed aspect ratio checkbox</h3>

    <asp:CheckBox ID="fixedRatio" runat=server />
    <pre> &lt;cs:CropImage ID="CropImage9" runat="server"  Image="Image9" CanvasWidth="300"  Ratio="16/9"  FixedAspectRatioCheckboxID="fixedRatio" />   </pre>
                    
    <asp:Image ID="Image4" runat="server" ImageUrl="images/328.jpg" /> <br />
    <cs:CropImage ID="CropImage4" runat="server"  Image="Image4" CanvasWidth="300" Ratio="16/9" FixedAspectRatioCheckboxID="fixedRatio" />   
    <asp:Button ID="Button4" runat="server" Text="Crop" onclick="btnCrop_Click" />


    </div>
    </form>
</body>
</html>
