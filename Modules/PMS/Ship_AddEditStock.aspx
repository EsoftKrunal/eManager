<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ship_AddEditStock.aspx.cs" Inherits="Ship_AddEditStock" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />
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

        function OpenPageForSpareConsumption(Typ, ID) {
            if (Typ == "DD")
                {
                //Popup_BreakDown.aspx?DN=LIG/16/2
                //window.open('Popup_BreakDown.aspx?DN=' + ID  , '', 'status=1,scrollbars=0,toolbar=0,menubar=0');
                window.open('Popup_BreakDown.aspx?DN=' + ID, '_blank', '');
            }
            if (Typ == "JE") {
                //JobCard.aspx?VC=LIG&&HID=38&&RP=R 
                window.open('JobCard.aspx?DN=' + ID, '_blank', '');
            }

        }
    </script>
    <style type="text/css">
        .control
        {
            padding:5px;
        }
        .bordered tr td
        {
            border:solid 1px #e6e3e3;
            
        }
        td{
            vertical-align:middle;
        }
    </style>
     <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body style="font-size:13px;font-family:Arial;font-size:12px;">
    <form id="form1" runat="server">
    <div style="text-align: center;font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%;" cellpadding="0" cellspacing="0" >
        <tr>        
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center" class="text headerband" >
                    Spare Stock Management</td>
            </tr>
            </table>
        <div id="trCritical" runat="server" align="center" style="height: 23px; text-align :center; padding:3px;color:#ff1a1a;font-weight:bold;font-size:15px;" visible="false" >
                    Can not modify. Marked as critical.
            </div>
        </td>
        </tr>
        </table>
     </div>
        <div style="padding:8px; background-color:#bad3f6;color:#333; font-size:15px;">
            <table width="100%" border="0" cellpadding="2">
                <col width="150px" />
                <col width="400px" />
                <col width="60px" />
                <col />
                <tr>
                    <td style="width:150px;text-align:right"> Component Name :</td>
                    <td><asp:Label ID="lblComponent" Font-Bold="true" runat="server" ></asp:Label></td>
                    <td> Part# :</td>
                    <td><asp:Label ID="lblPartNo" Font-Bold="true" runat="server" ></asp:Label></td>
                </tr>
                <tr>
                    <td style="text-align:right"> Spare Name :</td>
                    <td> <asp:Label ID="txtName" runat="server" Font-Bold="true" ></asp:Label></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <col width="50%" />
            <tr style="border-bottom:solid 1px #cac5c5;">
                
                <td style="border:solid 0px red;vertical-align:top;border-right:solid 1px #cac5c5;">
                    <div style="padding:10px;background-color:#e6e3e3;color:#393232;font-size:16px;">  <b> Recieved Spare</b> </div>
                    <div style="padding:6px;">  
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="margin:0px;border:solid 0px red;">               
                <tr id="trStockAdd" runat="server"  >
                    <td style="text-align:right">Date : </td>
                    <td>
                        <asp:TextBox ID="txtRecdDt" CssClass="control" MaxLength="11"  runat="server" Width="90px" ></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" PopupPosition="TopLeft" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtRecdDt" TargetControlID="txtRecdDt"></asp:CalendarExtender>
                    </td>
                    <td style="text-align:right">Qty : </td>
                    <td>
                        <asp:TextBox ID="txtRecdQty" CssClass="control" MaxLength="5" onkeypress="fncInputIntegerValuesOnly(event)" runat="server" Width="40px" ></asp:TextBox>
                    </td>
                    <td style="text-align:right">Stock Location : </td>
                    <td>
                        <%--<asp:TextBox ID="txtStockLocation" CssClass="control" MaxLength="25" Width="100px" runat="server" ></asp:TextBox>--%>
                         <asp:DropdownList ID="ddlStockLocation" runat="server" Width="200px"></asp:DropdownList>
                    </td>
                    <td>
                        <asp:Button ID="btnAdd" Text="Add" CssClass="btn" runat="server" onclick="btnAdd_Click" />
                        <asp:Button ID="btnClear" Text="Clear" CssClass="btn" runat="server" onclick="btnClear_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="7">&nbsp;</td>
                </tr>
                <tr >
                    <td colspan="7">
                        <div id="dvSpares" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: hidden; overflow-x: hidden; text-align: center;">
                        <table class="bordered" cellpadding="3" cellspacing="0"  style="width: 100%;border-collapse: collapse; ">
                            <colgroup>
                            <%if (Session["UserType"].ToString().Trim() == "S")
                                {%>
                                <col style="width: 40px;" />
                                <% }%>
                                <col style="width: 100px" />
                                <col style="width: 80px;" />
                                <col />                                
                            </colgroup>
                            <tr align="left" class= "headerstylegrid">
                            <%if (Session["UserType"].ToString().Trim() == "S")
                                {%>
                                    <td class="style1"></td>
                                    <% }%>
                                    <td class="style1">
                                        Date 
                                    </td>
                                    <td class="style1">
                                        Qty 
                                    </td>
                                    <td class="style1" style="text-align:left;">
                                        Stock Location
                                    </td>
                                    
                                                                                      
                                </tr>
                        </table>
                        </div>
                        <div id="dvSpares" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; height: 370px; text-align: center;">
                            <table class="bordered" cellpadding="3" cellspacing="0" style="width: 100%; border-collapse: collapse;">
                                <colgroup>
                                <%if (Session["UserType"].ToString().Trim() == "S")
                                {%>
                                    <col style="width: 40px;" />
                                    <% }%>
                                    <col style="width: 100px" />
                                    <col style="width: 80px;" />
                                    <col />
                                </colgroup>
                                <asp:Repeater ID="rptStock" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                        <%if (Session["UserType"].ToString().Trim() == "S")
                                            {%>
                                            <td>
                                                <asp:ImageButton ID="btnEditStock"  CommandArgument='<%#Eval("PKID") %>' Visible='<%#(Eval("RecordType").ToString()=="I")%>' ToolTip="Edit Stock" ImageUrl="~/Images/editX12.jpg" OnClick="btnEditStock_Click" runat="server" />
                                            </td>
                                            <% }%>
                                            <td>
                                                <%#Eval("RecdDate")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblQty" Text='<%#Eval("QtyRecd")%>' runat="server"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <%#Eval("StockLocation")%>
                                            </td>                                                                                         
                                            
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="7" style="display:none;">
                        <uc1:MessageBox ID="MessageBox2" runat="server" />
                    </td>
                </tr>
            </table>
                    </div>
                 </td>
                <td style="border:solid 0px red;vertical-align:top;">
                    <div style="padding:10px;background-color:#e6e3e3;color:#393232;font-size:16px;">  <b> Consumed Spare</b> </div>
                    <div style="padding:6px;">  
                    <table cellpadding="2" cellspacing="0" width="100%" >               
                        <tr>
                            <td>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="dvConsumedStock" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: hidden; overflow-x: hidden;width:100%;text-align: center;">
                                <table class="bordered" cellpadding="3" cellspacing="0"  style="width: 100%;border-collapse: collapse; ">
                            <colgroup>                            
                                <col style="width: 100px" />
                                <col style="width: 80px;" />
                                <col style="width: 80px;" />
                                <col />
                                <col style="width: 107px;" />
                                
                            </colgroup>
                            <tr align="left" class= "headerstylegrid">
                            
                                    <td class="style1">
                                        Date 
                                    </td>
                                    <td class="style1">
                                        Qty
                                    </td>
                                    <td class="style1" style="text-align:left;">
                                        Job Type
                                    </td>
                                    <td class="style1" style="text-align:left;">
                                        Job Details
                                    </td>
                                    <td class="style1" style="text-align:left;">
                                        Reference
                                    </td>                                             
                                </tr>
                        </table>
                                </div>
                        <div id="dvConsumedStock" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 370px; text-align: center;">
                            <table class="bordered" cellpadding="3" cellspacing="0" style="width: 100%; border-collapse: collapse;">
                                <colgroup>
                                    <col style="width: 100px" />
                                    <col style="width: 80px;" />
                                    <col style="width: 80px;" />
                                    <col />
                                    <col style="width: 90px;" />
                                </colgroup>
                                <asp:Repeater ID="rptItemConsumed" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Common.ToDateString(Eval("DoneDate"))%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblQty" Text='<%#Eval("Qty")%>' runat="server"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="Label1" Text='<%#Eval("ConsumedIn_Type_Name").ToString().ToUpper() %>' runat="server"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <%#Eval("JobDetails")%>
                                            </td>
                                            <td align="left">
                                                <%--<span style="cursor:pointer;text-decoration:underline;color:#4371a5;" onclick='OpenPageForSpareConsumption("<%#Eval("ConsumedIn_Type_Id")%>","<%#Eval("Reference")%>")'>
                                                 <%#Eval("Reference")%>
                                                </span>--%>
                                                <%--onclick='OpenPageForSpareConsumption("<%#Eval("ConsumedIn_Type_Id")%>","<%#Eval("ReferenceID")%>")'--%>

                                                <a  href='Popup_BreakDown.aspx?DN=<%#Eval("ReferenceID")%>&&M=Y&&ModifySpare=Y' target="_blank"  style='display:<%#((Eval("ConsumedIn_Type_Id").ToString()=="DD")?"block":"none") %>'>
                                                 <%#Eval("ReferenceText")%>
                                                </a>

                                                <%--JobCard.aspx?VC=LIG&&HID=38&&RP=R --%> 
                                                <a  href='JobCard.aspx?VC=<%# (VesselCode) %>&&HID=<%#Eval("HistoryId")%>&&RP=R&&ModifySpare=Y' target="_blank"  style='display:<%#((Eval("ConsumedIn_Type_Id").ToString()=="JE")?"block":"none") %>'>
                                                 <%#Eval("ReferenceText")%>
                                                </a>

                                                <%--Popup_AddUnPlanJob.aspx?VSL=LIG&UPId=1--%>
                                                <a  href='Popup_AddUnPlanJob.aspx?VSL=<%# (VesselCode) %>&&UPId=<%#Eval("UPId")%>&&ModifySpare=Y' target="_blank"  style='display:<%#((Eval("ConsumedIn_Type_Id").ToString()=="UP")?"block":"none") %>'>
                                                 <%#Eval("ReferenceText")%>
                                                </a>
                                            </td>                                                                                         
                                            
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                            </td>
                        </tr>
                    </table>
                     </div>
                </td>
            </tr>
        </table>
        <div style="border-top:solid 1px #e6e3e3;"></div>
        </div>
        <asp:HiddenField ID="hfCompCode" runat="server" />
        <asp:HiddenField ID="hfOffice_Ship" runat="server" />
                                         
          <div style="position:fixed;width:100%;bottom:0px;left:0px; text-align:center; background-color:#f7f1ab">
              <table cellpadding="6" style="float: left" cellspacing="0" width="100%">
                  <tr style="font-size:15px; font-weight:bold; ">
                      <td>Total Qty Recd</td>
                      <td>Total Consumed</td>
                      <td>Total ROB ( Calculated )</td>
                  </tr>
                          
                   <tr>
                      <td><asp:Label ID="lblTQtyRecd" runat="server" Font-Size="14px"></asp:Label></td>
                      <td><asp:Label ID="lblTQtyCons" runat="server" Font-Size="14px"></asp:Label></td>
                      <td><asp:Label ID="lblTROB" runat="server" Font-Size="14px"></asp:Label></td>
                  </tr>
              </table>
          </div>                                        
    </form>
</body>
</html>
