<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSpares.aspx.cs" Inherits="AddSpares" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>eMANAGER</title>
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
     <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" >
        function fncInputNumericValuesOnly(evnt) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
    </script>
    <script type="text/javascript" >
        function fncInputIntegerValuesOnly(evnt) {
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function refresh() {
            window.opener.reloadunits();
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-size:12px;font-family:Arial;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>        
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center"  class="text headerband" >
                  
                    Add/Edit Spares&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 10px; padding-left: 10px;">
                        <asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>   
                        <div style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid; height:450px; width:100% " >
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td align="left">
                             <table cellpadding="0" cellspacing="0" width="100%">                                                                         
                                     <tr>                                         
                                        <td style="padding-left:10px;padding-right:5px;width:790px;" >                                           
                                           <table border="1" bordercolor="#c2c2c2" rules="rows" cellpadding="3" cellspacing="1" style="float:left;border-collapse: collapse;" width="99%">
                                           <tr>
                                           <td colspan="3"><asp:Label ID="lblComponent" Font-Bold="true" runat="server"></asp:Label></td>
                                           <td colspan="3"><asp:Label ID="lblMessage" runat="server" CssClass="error_msg"></asp:Label></td>
                                           
                                           </tr>
                                            <tr><td colspan="6" style="height:10px"><asp:HiddenField ID="hfCompCode" runat="server" />
                                                </td>
                                               </tr>
                                               <tr>
                                                   <td>
                                                       Spare Name :</td>
                                                   <td colspan="3">
                                                       <asp:TextBox ID="txtName" required='yes' runat="server" MaxLength="50" Width="310px"></asp:TextBox>
                                                   </td>
                                                   <td>
                                                       &nbsp;</td>
                                                   <td>
                                                       <asp:HiddenField ID="hfOffice_Ship" runat="server" />
                                                   </td>
                                               </tr>                                                                                         
                                               <tr>
                                                   <td>
                                                       Maker :</td>
                                                   <td colspan="3">
                                                       <asp:TextBox ID="txtMaker" required='yes' runat="server" MaxLength="50" Width="310px"></asp:TextBox>
                                                   </td>
                                                   <td>
                                                       &nbsp;</td>
                                                   <td>
                                                       &nbsp;</td>
                                               </tr>
                                               <tr>
                                                   <td>
                                                       Model/Type :</td>
                                                   <td colspan="3">
                                                       <asp:TextBox ID="txtMakerType" required='yes' runat="server" MaxLength="50" Width="310px"></asp:TextBox>
                                                   </td>
                                                   <td>
                                                       &nbsp;</td>
                                                   <td>
                                                       &nbsp;</td>
                                               </tr>
                                            <tr>
                                            <td>Part# :</td>
                                            <td><asp:TextBox ID="txtPart" MaxLength="50" runat="server"></asp:TextBox></td>
                                            <td>Alt Part# :</td>
                                            <td>
                                                <asp:TextBox ID="txtAltPart" runat="server" MaxLength="50" Width="80px"></asp:TextBox>
                                                </td>
                                            <td>Drawing No. :</td>
                                            <td>
                                                <asp:TextBox ID="txtDrawingNo" runat="server" MaxLength="50"></asp:TextBox>
                                                </td>
                                             </tr>
                                            <%--<tr>
                                            <td>Location :</td>
                                            <td><asp:TextBox ID="txtLocation" MaxLength="50" runat="server"></asp:TextBox></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>--%>                                             
                                               <tr>
                                                   <td>
                                                       Min. Qty :</td>
                                                   <td>
                                                       <asp:TextBox ID="txtMinQty" runat="server" MaxLength="5" onkeypress="fncInputIntegerValuesOnly(event)" Width="80px"></asp:TextBox>
                                                   </td>
                                                   <td>
                                                       Max. Qty :</td>
                                                   <td>
                                                       <asp:TextBox ID="txtMaxQty" runat="server" MaxLength="5" onkeypress="fncInputIntegerValuesOnly(event)" Width="80px"></asp:TextBox>
                                                   </td>
                                                   <td>
                                                       Statutory Qty :</td>
                                                   <td>
                                                       <asp:TextBox ID="txtStatutory" runat="server" MaxLength="5" onkeypress="fncInputIntegerValuesOnly(event)" Width="80px"></asp:TextBox>
                                                   </td>
                                               </tr>
                                            <tr>
                                            <td>Weight (Kg) :</td>
                                            <td><asp:TextBox ID="txtWeight" MaxLength="6" onkeypress="fncInputNumericValuesOnly(event)" runat="server" Width="80px"></asp:TextBox></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                          
                                            <tr>
                                            <td>Specification :</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtSpecs" TextMode="MultiLine" MaxLength="500" Width="369px" 
                                                    Height="79px" runat="server"></asp:TextBox></td>
                                                <td valign="baseline" colspan="2">
                                                 <a runat="server" ID="ancFile"  href="" target="_blank" Visible="False" title="Click to large view" >
                                                    <asp:Image ID="Image1"  runat="server" Height="93px" Width="101px" Visible="False" />
                                                 </a>
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>AttachMent :</td>
                                              <td colspan="3">
                                                <asp:FileUpload ID="flAttachDocs" runat="server" />
                                              </td>
                                            </tr>
                                            <tr>
                                            <td></td>
                                            <td colspan="5"><asp:Button ID="btnSave" runat="server" CssClass="btn" onclick="btnSave_Click" Text="Save" /></td>
                                            </tr>                                            
                                           </table>
                                         </td>
                                     </tr>
                               </table>
                               </td>  
                             </tr>
                        </table>
                        </div>                         
                        </ContentTemplate>
                        <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                        </Triggers>
                        </asp:UpdatePanel>
                        </td>
            </tr>
        </table>
        </td> 
        </tr>
        </table>
        </td>
        </tr>
        </table>
     </div>
    </form>
</body>
</html>
