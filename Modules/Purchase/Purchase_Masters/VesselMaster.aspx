<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselMaster.aspx.cs" Inherits="Registers" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Modules/Purchase/UserControls/Registers.ascx" TagName="Registers" TagPrefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>EMANAGER</title>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
   
     <script type="text/javascript" src="../JS/Common.js"></script>
    <script type="text/javascript" >
        var lastSel=null;
        function Selectrow(trSel, prid) 
        {
            if(lastSel==null)
            {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel=trSel;
                document.getElementById('hfPRID').value = prid;
            }
            else
            {
                if(lastSel.getAttribute("Id")==trSel.getAttribute("Id")) // clicking on same row
                {   
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel=trSel;
                    document.getElementById('hfPRID').value = prid;
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel=trSel;
                    document.getElementById('hfPRID').value = prid;
                }
            }
        }
        function fncInputNumericValuesOnly(evnt) {
                
                if (!(event.keyCode == 13 || event.keyCode == 45 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {

                    event.returnValue = false;

                }

            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div style="font-family:Arial;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="border:2px solid #4371a5;" >
    <table cellSpacing="0" cellPadding="0" width="100%" border="0" >
    <tr>
    <td>
       <asp:HiddenField ID="hfPRID" runat="server" Value="0" />
       <uc2:Registers runat="server" ID="Registers1" />  
       <center>
       <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="radNWC" AutoPostBack="true" OnSelectedIndexChanged="radNWC_OnSelectIndexChanged" Font-Bold="true" Visible="false" >
       <asp:ListItem Text="Other - Vessels" Value="A" Selected="True"></asp:ListItem>
       <asp:ListItem Text="NWC - Vessels" Value="N" ></asp:ListItem>
       </asp:RadioButtonList>
       </center>
       <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
            <colgroup>
                <col style="width:30px;" />
                <col style="width:60px;" />
                <col style="width:60px;" />
                <col style="width:220px;" />
                <col style="width:250px;" />
                <col style="width:50px;" />
                <col style="width:100px;" />
                <col style="width:320px;" />
                <col style="width:100px;" />
                <col />
                <col />
                <tr align="left" class= "headerstylegrid">
                    <td>Edit</td>
                    <td>Active</td>
                    <td>ShipID</td>
                    <td>Ship Name</td>
                    <td>Owner</td>
                    <td>Ship#</td>
                    <td>NWC-Code</td>
                    <td>Email</td>
                    <td>Ship FAX</td>
                    <td style="text-align:center;" colspan="3">Inmarsat</td>
                        
                    <td></td>
                </tr>
            </colgroup>
        </table>
       <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 320px ; text-align:center; ">
       <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
            <colgroup>
                <col style="width:30px;" />
                <col style="width:60px;" />
                <col style="width:60px;" />
                <col style="width:220px;" />
                <col style="width:250px;" />
                <col style="width:50px;" />
                <col style="width:100px;" />
                <col style="width:320px;" />
                <col style="width:100px;" />
                <col />
                <col />
            </colgroup>
            <asp:Repeater ID="RptVessel" runat="server">
                <ItemTemplate>
                        <tr id='tr<%#Eval("VesselNo")%>' class='<%#(Convert.ToInt32(Eval("VesselNo"))!=SelectedVessId)?"":"selectedrow"%>' onclick='Selectrow(this,<%#Eval("VesselNo")%>);' >
                        <%--<tr class="row">--%>
                        <td>
                            <asp:ImageButton ID="imgUpdate" runat="server" ImageUrl="~/Modules/HRD/Images/edit.png" OnClick="imgUpdate_OnClick" />
                        </td>
                        <td>
                            <asp:Label ID="lblActive" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblShipID" runat="server" Text='<%# Eval("ShipID") %>'></asp:Label>
                         </td>
                        <td style="text-align:left;"><%# Eval("ShipName")%>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblPRNumber" runat="server" Text='<%# Eval("Company Name") %>'></asp:Label>
                        </td>
                        <td style="text-align:center;">
                            <asp:Label ID="lblPRType" runat="server" Text='<%# Eval("VesselNo") %>'></asp:Label>
                        </td>
                        <td style="text-align:center;">
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("NWCShipId") %>'></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblCreated" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                        </td>
                        <td style="text-align :left;">
                            <%#Eval("ShipFax")%>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblCommentForVessel" runat="server" Text='<%# Eval("Inmarsat") %>'></asp:Label>
                        </td>
                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr id='tr<%#Eval("VesselNo")%>' class='<%#(Convert.ToInt32(Eval("VesselNo"))!=SelectedVessId)?"alternaterow":"selectedrow"%>' onclick='Selectrow(this,<%#Eval("VesselNo")%>);' lastclass='alternaterow'>
                       <td>
                            <asp:ImageButton ID="imgUpdate" runat="server" ImageUrl="~/Modules/HRD/Images/edit.png" OnClick="imgUpdate_OnClick" />
                        </td>
                       <td>
                            <asp:Label ID="lblShip" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblShipID" runat="server" Text='<%# Eval("ShipID") %>'></asp:Label>
                         </td>
                         
                        <td style="text-align:left;"><%# Eval("ShipName")%>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblPRNumber" runat="server" Text='<%# Eval("Company Name") %>'></asp:Label>
                        </td>
                        <td style="text-align:center;">
                            <asp:Label ID="lblPRType" runat="server" Text='<%# Eval("VesselNo") %>'></asp:Label>
                        </td>
                        <td style="text-align:center;">
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("NWCShipId") %>'></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblCreated" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                        </td>
                        <td style="text-align :left;">
                            <%#Eval("ShipFax")%>
                        </td>
                        <td style="text-align:left;">
                           <asp:Label ID="lblCommentForVessel" runat="server" Text='<%# Eval("Inmarsat") %>'></asp:Label>
                        </td>
                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </table>
        
       </div>
       <asp:button ID="btnAdd" runat="server" OnClick="btnAdd_OnClick" Text="ADD" style="float:right; margin:5px;" CssClass="btn" />
    </td>
    </tr>
    </table>
    </div>
</div> 
<div style="position:absolute;top:0px;left:0px; height :550px; width:100%;z-index:100;" runat="server" id="dvAddVessel" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :550px; width:100%;background-color:Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:1200px; height:450px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">
                    
            <%---------------------------------------------------%>
                        
            <div id="DivUpdate"  runat="server"  style="width:100%;"  >
             <fieldset>
         <legend style="font-weight:bold;color:#4371a5; font-family:Arial;" >Add Vessel </legend>
         <table cellspacing="0" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;margin:15px;">
            <tr align="center" >
                <td align="right">Ship ID</td>
                <td style="text-align:left;">
                    <asp:TextBox ID="txtShipID" runat="server" Width="30px" MaxLength="3" ></asp:TextBox>
                 </td>
                 <td align="right">Ship Name</td>
                <td align="left">
                    <asp:TextBox ID="txtShipName" runat="server" MaxLength="100" Width="200px" ></asp:TextBox>
                </td>
                <td align="right">Owner </td>
                <td align="left">
                    <asp:DropDownList ID="ddlOwner" runat="server" ></asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td align="right">Ship#</td>
                <td align="left">
                    <asp:TextBox ID="txtShipNo" runat="server" onkeypress='fncInputNumericValuesOnly(event)'></asp:TextBox>
                </td>
                <td align="right">Email</td>
                <td align="left">
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="150" Width="200px" ></asp:TextBox>
                </td>
                <td align="right">Ship Fax</td>
                <td align="left">
                    <asp:TextBox ID="txtShipFax" runat="server" MaxLength="150" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">Inmarsat</td>
                <td align="left">
                    <asp:TextBox ID="txtInmarsat" runat="server" MaxLength="150" ></asp:TextBox>
                </td>
                <td align="right">Active</td>
                <td align="left">
                    <asp:CheckBox ID="chkActive" runat="server"  />
                </td>
                <td align="right"><!--NWC ShipId--></td>
                 <td align="left"><asp:TextBox ID="txtNWC" runat="server" MaxLength="3" Visible="false" ></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="6" style="font-weight:bold;text-align:left;">
                    Vessel Details
                </td>
            </tr>
            <tr>
                <td align="right">Built</td>
                <td align="left">
                    <asp:TextBox ID="txtBuilt" runat="server" MaxLength="50" ></asp:TextBox>
                </td>
                <td align="right">DWT</td>
                <td align="left">
                    <asp:TextBox ID="txtDWT" runat="server"  MaxLength="50"></asp:TextBox>
                </td>
                <td align="right">GRT</td>
                <td align="left">
                    <asp:TextBox ID="txtGRT" runat="server"  MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">NRT</td>
                <td align="left">
                    <asp:TextBox ID="txtNRT" runat="server"  MaxLength="50"></asp:TextBox>
                </td>
                <td align="right">Registery</td>
                <td align="left">
                    <asp:TextBox ID="txtRegistery2" runat="server"  MaxLength="50"></asp:TextBox>
                </td>
                <td align="right">Year Built</td>
                <td align="left">
                    <asp:TextBox ID="txtYearBuilt" runat="server"  ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">Hull No</td>
                <td align="left">
                    <asp:TextBox ID="txtHullNo" runat="server"  ></asp:TextBox>
                </td>
                <td align="right">H & M</td>
                <td align="left">
                    <asp:TextBox ID="txtHAndM" runat="server"  ></asp:TextBox>
                </td>
                <td align="right">P & I</td>
                <td align="left">
                    <asp:TextBox ID="txtPAndI" runat="server"  ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">Registery</td>
                <td align="left">
                    <asp:TextBox ID="txtRegistery" runat="server"  ></asp:TextBox>
                </td>
                <td align="right">Official No</td>
                <td align="left">
                    <asp:TextBox ID="txtOfficialNo" runat="server"  ></asp:TextBox>
                </td>
                <td align="right">Call Sign</td>
                <td align="left">
                    <asp:TextBox ID="txtCallSign" runat="server"  ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">Main Engine</td>
                <td align="left">
                    <asp:TextBox ID="txtMainEngine" runat="server"  ></asp:TextBox>
                </td>
                <td align="right">Main Engine No</td>
                <td align="left">
                    <asp:TextBox ID="txtMainEngineNo" runat="server"  ></asp:TextBox>
                </td>
                <td align="right">AUX Engine</td>
                <td align="left">
                    <asp:TextBox ID="txtAUXEngine" runat="server"  ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">EX No</td>
                <td align="left">
                    <asp:TextBox ID="txtEXNo" runat="server"  ></asp:TextBox>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="lblmsg" runat="server" style="float:right;color:Red;"  ></asp:Label>
                </td>
                <td colspan="1" align="right" style="padding-right:20px;">
                        
                      <asp:button ID="imgSave" runat="server" Text="Save" OnClick="imgSave_OnClick" CssClass="btn" /> &nbsp;
                      <asp:button ID="imgCalcel" runat="server" Text="Cancel"  OnClick="imgCalcel_OnClick" CssClass="btn"  /> &nbsp;
                      <asp:HiddenField ID="hfAccID" runat="server" Value="" />
                
                </td>
            </tr>
    </table>
    </fieldset>
            </div> 
                <asp:Label ID="Label1" runat="server" CssClass="PageError" style="float:left;" ></asp:Label>
            
            <%-----------------------------------------------------------%>
            
        <br /><br />
        <br /><br />
    </div> 
    </center>
    </div>
    <%--<script type="text/javascript" >
var Id='tr' + document.getElementById('hfPRID').value;
lastSel=document.getElementById(Id);
</script>--%>
</asp:Content>
 



