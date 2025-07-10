<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrainingMatrix.aspx.cs" Inherits="TrainingMatrix"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
     
     <style type="text/css">
.hd
{
	background-color : #c2c2c2;
	cursor :pointer;
}
a img
{
border:none;	
}
</style>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <script type="text/ecmascript">
    var ScreenW=window.screen.availWidth;
    var ScreenH=window.screen.availHeight; 
    var seltd;
    function ShowAdd()
    {
        dvMat.style.left =event.clientX+20+'px';
        dvMat.style.top =(event.clientY+20)+'px';
        $("#dvMat").slideToggle();
        document.getElementById("txtMatName").focus();
    }
    function hideAdd()
    {
        if(event.keyCode==27 )
            $("#dvMat").slideUp();
    }
</script>
    </head>
<body style="background-color: #f9f9f9; margin :5px;">
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
        <table align="center" width="100%" border="1" cellpadding="2" cellspacing="0" style="border-collapse :collapse;font-family:Arial;font-size:12px;">
            <tr>
                <td style=" text-align:left; padding :5px;">
                     Select Training Matrix : 
                    
                </td>
                <td>
                    <asp:DropDownList ID="ddlTraining" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTraining_OnSelectedIndexChanged" Width="250px" CssClass="input_box" ></asp:DropDownList>
                </td>
                <td>
                     <asp:Button runat="server" ID="btnAddTraining" CssClass="btn" Text="Create New Matrix" style=" width:140px; height :24px;" onclick="btnSaveVessel_Click" OnClientClick ="ShowAdd();return false;"/>
                     <asp:Button runat="server" ID="btnUpdTraining" CssClass="btn" Text="Edit Vessel List" style=" width:115px; height :24px; " onclick="btnEditVessel_Click" />
                      <asp:Label runat="server" ID="lblMessMain" Font-Bold="true" Font-Names="Verdana" style="padding-top :7px; padding-left :10px;"></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="trVlist" visible="false">
                <td style=" text-align:left" >
                <div style=" background-color :#c2c2c2; font-weight:bold; padding :5px; text-align:center">Applicable Vessel List</div>
                    <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 300px">
                     <asp:CheckBoxList runat="server" ID="chklstVessel" RepeatColumns="1" RepeatDirection="Vertical"></asp:CheckBoxList>
                     </div>
                     <asp:Button runat="server" ID="btnCancel" CssClass="btn" Text="Cancel" style="width:60px; height :24px; background-position:3px; text-align:center; margin :2px; float:right" onclick="btnCancel_Click" />
                     <asp:Button runat="server" ID="btnSaveVessel" CssClass="btn" Text="Save Vessel List" style="  width:125px; height :24px; background-position:3px; text-align:right; margin :2px; float:right" onclick="btnSaveVessel_Click" />
                     <asp:Label runat="server" ID="lblMessVessel" Font-Bold="true" Font-Names="Verdana" style="float:right; padding-top :7px; padding-right :10px;"></asp:Label>
                </td>
            </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        <!-- DIV FOR SHOW ADD NEW TRAINING MARTIX -->
        <div id="dvMat" onkeydown='hideAdd();' style="border :solid 1px gray; border-bottom:solid 4px gray;border-right:solid 4px gray; width :300px; height :60px; padding :10px; position :absolute; top:400px; left:200px; background-color:#FFE4B5; display :none; font-family:Arial;font-size:12px;">
        Enter Training Matrix Name  
             <asp:TextBox runat="server" ID="txtMatName" MaxLength="100" Width="250px" CssClass="input_box" style="text-align:left; margin :3px;"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate ="txtMatName" ErrorMessage="*" ValidationGroup="add"></asp:RequiredFieldValidator> 
             <asp:Button runat="server" ID="btnSaveTMat" CssClass="btn" Text="Add" style="width:80px; height :24px; margin :2px;" onclick="btnSaveTMat_Click" ValidationGroup="add"/>
        </div>
        </form>
        </body>
        </html>
    
