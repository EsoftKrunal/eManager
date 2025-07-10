<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ship_AddEditSpares.aspx.cs" Inherits="Ship_AddEditSpares" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
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
    <style type="text/css">
        .style1
        {
            height: 21px;
        }
        .auto-style1 {
            height: 548px;
        }
        .auto-style2 {
            width: 60%;
            height: 552px;
        }
        .auto-style3 {
            width: 40%;
            height: 552px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>        
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center" style="text-align :center; " class="text headerband" >
                   
                    Add/Edit Spares&nbsp;</td>
            </tr>
            </table>
            
            <div id="trCritical" runat="server" align="center" style="height: 23px; text-align :center; padding:3px;color:#ff1a1a;font-weight:bold;font-size:15px;" visible="false" >
                    Can not modify. Marked as critical / OR.
            </div>

            <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 0px solid; text-align:center" width="100%">
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 10px; padding-left: 10px;">
                        <asp:UpdatePanel runat="server" id="up1">
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
    <div style="padding:8px; background-color:#bad3f6;color:#333; font-size:15px;">
        <table width="100%" border="0" cellpadding="2">
            <tr>
                <td style="width:150px;text-align:right"> Component Name :</td>
                <td >
                     <asp:Label ID="lblComponent" Font-Bold="true" runat="server"></asp:Label></td>
            </tr>
        </table>
    </div>
                        <div style="border-left: 1px solid #8fafdb; border-right: 1px solid #8fafdb; border-bottom: 1px solid #8fafdb; background-color:#f9f9f9; border-top:#8fafdb 0px solid; height:656px; width:100%;font-family:Arial;font-size:12px;" >
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td align="left" style="padding:10px;width:60%">
                             <table cellpadding="0" cellspacing="0" width="100%" >                                                                         
                                     <tr>                                         
                                        <td style="padding-left:10px;padding-right:5px;width:60%;" >                                           
                                           <table cellpadding="2" style="float:left" cellspacing="0" width="100%">
                                           
                                            <tr><td colspan="6" style="height:10px"><asp:HiddenField ID="hfCompCode" 
                                                    runat="server" />
                                                <asp:HiddenField ID="hfOffice_Ship" runat="server" />
                                                </td>
                                               </tr>
                                               <tr>
                                                   <td style="width:100px;">
                                                       Spare Name :</td>
                                                   <td colspan="3">
                                                       <asp:TextBox ID="txtName" required='yes' runat="server" MaxLength="50" 
                                                           Width="310px"></asp:TextBox>
                                                   </td>
                                                   <td>
                                                       &nbsp;</td>
                                                   <td>
                                                       &nbsp;</td>
                                               </tr>                                                                                         
                                               <tr>
                                                   <td style="width:100px;">
                                                       Maker :</td>
                                                   <td colspan="3">
                                                       <asp:TextBox ID="txtMaker" required='yes' runat="server" MaxLength="50" 
                                                           Width="310px"></asp:TextBox>
                                                   </td>
                                                   <td>
                                                       &nbsp;</td>
                                                   <td>
                                                       &nbsp;</td>
                                               </tr>
                                               <tr>
                                                   <td style="width:100px;">
                                                       Model/Type :</td>
                                                   <td colspan="3">
                                                       <asp:TextBox ID="txtMakerType" required='yes' runat="server" MaxLength="50" 
                                                           Width="310px"></asp:TextBox>
                                                   </td>
                                                   <td>
                                                       &nbsp;</td>
                                                   <td>
                                                       &nbsp;</td>
                                               </tr>
                                            <tr>
                                            <td style="width:100px;">Part# :</td>
                                            <td style="width:225px;"><asp:TextBox ID="txtPart" MaxLength="50" runat="server" Width="200px"></asp:TextBox></td>
                                            <td style="width:100px;">Alt Part# :</td>
                                            <td style="width:100px;">
                                                <asp:TextBox ID="txtAltPart" runat="server" MaxLength="50" Width="80px"></asp:TextBox>
                                                </td>
                                            <td style="width:100px;">Drawing No. :</td>
                                            <td style="width:200px;">
                                                <asp:TextBox ID="txtDrawingNo" runat="server" MaxLength="50" Width="180px" ></asp:TextBox>
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
                                                   <td style="width:100px;">
                                                       Min. Qty :</td>
                                                   <td>
                                                       <asp:TextBox ID="txtMinQty" runat="server" MaxLength="5" 
                                                           onkeypress="fncInputIntegerValuesOnly(event)" Width="80px"></asp:TextBox>
                                                   </td>
                                                   <td>
                                                       Max. Qty :</td>
                                                   <td>
                                                       <asp:TextBox ID="txtMaxQty" runat="server" MaxLength="5" 
                                                           onkeypress="fncInputIntegerValuesOnly(event)" Width="80px"></asp:TextBox>
                                                   </td>
                                                   <td>
                                                       Statutory Qty :</td>
                                                   <td>
                                                       <asp:TextBox ID="txtStatutory" runat="server" MaxLength="5" 
                                                           onkeypress="fncInputIntegerValuesOnly(event)" Width="80px" ></asp:TextBox>
                                                   </td>
                                               </tr>
                                            <tr>
                                            <td style="width:100px;">Weight (Kg) :</td>
                                            <td><asp:TextBox ID="txtWeight" MaxLength="6" 
                                                    onkeypress="fncInputNumericValuesOnly(event)" runat="server" Width="80px"></asp:TextBox></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <%--  <tr>
                                                <td style="width:100px;">
                                                   Stock Location :
                                                </td>
                                                <td colspan="6">
                                                        <asp:TextBox ID="txtLocation" runat="server" Width="350px" ></asp:TextBox>
                                                    <asp:DropdownList ID="ddlStockLocation" runat="server" Width="310px"></asp:DropdownList>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                            <td style="width:100px;">Specification :</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtSpecs" TextMode="MultiLine" MaxLength="500" Width="369px" 
                                                    Height="79px" runat="server"></asp:TextBox></td>
                                                <td valign="baseline" colspan="2">
                                                 <a runat="server" ID="ancFile"  href="" target="_blank" Visible="False" 
                                                        title="Click to large view" >
                                                    <asp:Image ID="Image1" Visible="false" runat="server" Width="15px" ImageUrl="~/Modules/HRD/Images/paperclip.png" Height="16px" />
                                                    </a></td>
                                            </tr>
                                            <tr>
                                              <td style="width:100px;">AttachMent :</td>
                                              <td colspan="3">
                                                <asp:FileUpload ID="flAttachDocs" runat="server" />
                                              </td>
                                              <td valign="baseline">
                                              </td>
                                              <td>
                                                  &nbsp;</td>
                                            </tr>
                                            <tr>
                                            <td></td>
                                            <td colspan="5">
                                                <asp:Button ID="btnSave" runat="server" 
                                                    CssClass="btn" onclick="btnSave_Click" Text="Save" /> &nbsp;
                                                <asp:Button ID="btnActive" runat="server" CssClass="btn" 
                                                    OnClientClick="javascript:return confirm('Are you sure to active this spare?');" 
                                                    onclick="btnActive_Click" Text="Active" Visible="false" />  &nbsp;
                                                <asp:Button ID="btnInactive" runat="server" 
                                                    CssClass="btn" 
                                                    OnClientClick="javascript:return confirm('Are you sure to inactive this spare?');" 
                                                    onclick="btnInactive_Click" Text="Inactive" Visible="false" />&nbsp;
                                            <uc1:MessageBox ID="MessageBox1" runat="server" />
                                                
                                            </td>
                                            </tr>
                                            <tr id="trStockUpdate" runat="server" >
                                              <td colspan="6">
                                                  
                                              </td>
                                            </tr>                                            
                                           </table>
                                         </td>
                                     </tr>
                               </table>
                               </td>  
                            <td style="padding:10px;width:40%">
                                <div style="padding:10px;border:solid 1px grey;">
                                    <asp:Image ID="Image2" Visible="false" runat="server" Width="95%"  ImageUrl="~/Modules/HRD/Images/paperclip.png" Height="400px" />
                                </div>
                            </td>
                             </tr>
                        </table>
                        </div>                         
    </form>
</body>
</html>
