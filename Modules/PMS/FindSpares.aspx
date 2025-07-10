<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FindSpares.aspx.cs" Inherits="FindSpares" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <title>eMANAGER</title>
   <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
     <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function selectspare(ctl) {
            var key = ctl.getAttribute('key');
            if (window.opener != null) {
                var x = window.opener.document.getElementById("ddlSparesList");
                for (var i = 0; i < x.length; i++) {
                    if (x.options[i].value == key) {
                        x.options[i].selected = "true";
                        window.close();
                    }
                }
            }
        }

        function openaddsparewindow(CompCode, VC, SpareId, Office_Ship) {

            window.open('Ship_AddEditSpares.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SPID=' + SpareId + '&&OffShip=' + Office_Ship, '', 'status=1,scrollbars=0,toolbar=0,menubar=0');

        }
        function reloadunits() {
            document.getElementById('ctl00_ContentMainMaster_btnRefresh').click();
            //__doPostBack('btnRefresh', '');
        }
    </script>
    <style type="text/css">
       input[type=checkbox] + label{
            width:50px;
            }

    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:Button ID="btnRefresh" runat="server" onclick="btnRefresh_Click" style="display:none" />
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center"  class="text headerband">Find Spares</td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>                       
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-top:5px;"></td>
                        </tr>
                        <tr>
                          <td>
                              <div style="padding:5px;">
                                  Component : <asp:Label ID="lblComponent" Font-Bold="true" runat="server"></asp:Label>
                              </div>
                             <div style="padding:5px;">
                                Find Spare : <asp:TextBox runat="server" ID="txtfilter" Width="250px"></asp:TextBox>
                                 <asp:Button runat="server" ID="btnsearch" Text="Search Spare" OnClick="btnsearch_click" />
                                 <asp:Button runat="server" ID="btnUpdateSpareToSession" Text="Update Spare" OnClick="btnUpdateSpareToSession_click" />
                                 <asp:Button runat="server" ID="btnAddSpare" Text="Add Spare" OnClick="btnAddSpare_click" />
                             </div>
                              
                              <div>
                                    <table cellpadding="10" cellspacing="" border="1" width="100%" style=" background-color:#c2c2c2; border-collapse:collapse" >
                                    <tr>
                                        <td style="width:30px; text-align:center">Sr#</td>
                                        <td>Spare Name</td>
                                        <td style="width:200px;">Maker</td>
                                        <td style="width:200px;">Maker Type</td>
                                        <td style="width:200px;">Part#</td>
                                        <td style="width:200px;">Drawing#</td>
                                        <td style="width:70px;">Qty(Cons)</td>
                                        <td style="width:70px;">Qty(ROB)</td>
                                        <td style="width:12px;">&nbsp;</td>
                                    </tr>
                                    </table>
                                    <div style="width:100%;height:550px;overflow-x:hidden;overflow-y:scroll;border:solid 1px gray;">
                                    <table cellpadding="0" cellspacing="" border="1" width="100%" style="border-collapse:collapse" >
                                        <asp:Repeater ID="rptspares" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width:30px; text-align:center">
                                                        <asp:CheckBox ID="chkSelectSpare" runat="server" key='<%#Eval("pkid")%>' />

                                                        <%--<img src="Images/add1.png" style="cursor:pointer" onclick="selectspare(this);" key='<%#Eval("pkid")%>' />--%>
                                                        <asp:HiddenField ID="hfdVesselCode" runat="server" Value='<%#Eval("VesselCode")%>' />
                                                        <asp:HiddenField ID="hfdComponentID" runat="server" Value='<%#Eval("ComponentId")%>' />
                                                        <asp:HiddenField ID="hfdOfficeShip" runat="server"  Value='<%#Eval("Office_Ship")%>'/>
                                                        <asp:HiddenField ID="hfdSpareID" runat="server"  Value='<%#Eval("SpareId")%>'/>
                                                    </td>
                                                    <td style="text-align:left;">&nbsp; <asp:Label ID="lblsparename" runat="server"  Text='<%#Eval("sparename")%>' ></asp:Label> </td>
                                                    <td style="text-align:left;width:200px;">&nbsp;<asp:Label ID="lblMaker" runat="server"  Text='<%#Eval("Maker")%>'></asp:Label></td>
                                                    <td style="text-align:left;width:200px;">&nbsp;<asp:Label ID="lblMakerType" runat="server"  Text='<%#Eval("MakerType")%>'></asp:Label></td>
                                                    <td style="text-align:left;width:200px;">&nbsp;<asp:Label ID="lblpartNo" runat="server"  Text='<%#Eval("partNo")%>'></asp:Label></td>
                                                    <td style="text-align:left;width:200px;">&nbsp;<asp:Label ID="lbldrawingno" runat="server"  Text='<%#Eval("drawingno")%>'></asp:Label></td>
                                                    <td style="text-align:left;width:70px;">&nbsp; <asp:TextBox ID="txtQtyCons" runat="server" style="width:60px;"></asp:TextBox> </td>
                                                    <td style="text-align:left;width:70px;">&nbsp; <asp:TextBox ID="txtQtyRob" runat="server" style="width:60px;"></asp:TextBox> </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    </div>
                              </div>
                          </td>
                        </tr>
                        </table>                       
                        </ContentTemplate>
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
  </asp:Content>
