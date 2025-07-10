<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSpareRequest.aspx.cs" Inherits="MenuPlanner_ViewSpareRequest" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />    
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/JQScript.js" type="text/javascript"></script>
    <script type="text/javascript"> 
        function fncInputNumericValuesOnly(evnt) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function fncInputIntegerValuesOnly(evnt) {
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function RefreshParent() {
            window.opener.Refresh();
        }

        function PrintReport(PrId) {
            if (typeof (winref) == 'undefined' || winref.closed) {
                winref = window.open('../Reports/ProvisionReport.aspx?PrId=' + PrId, '', '');
            }
            else {
                winref.focus();
            }
        }

        
        function SetOnLoad1() {
            $(".ancSlide").click(function () {
                var vis = $(".dvSlide:visible");
                $(".dvSlide:visible").slideToggle(500);
                $(this).next().slideToggle(500);
            });
            $(".ancSlide1").click(function () {
                $(this).next().slideToggle(500);
            });
        }
        function SetOnLoad2() {
            $("#chkselall").click(function () {
                var res = $("#chkselall").prop("checked");
                $(".selall1").each(function (i, o) {
                    //$(o).prop("checked", true);
                    $(o).attr("checked", res);
                });
            });
        }
        function Page_CallAfterRefresh() {
            SetOnLoad1();
            SetOnLoad2();
        }
        $(document).ready(function () {
            SetOnLoad1();
            SetOnLoad2();
        });

        function reloadunits() {
            __doPostBack('lnkRefresh', '');
        }
    </script>
    <style type="text/css">
    td
    {
        text-overflow:ellipsis;
        word-break:break-all;
    }
    .style1
    {
        width: 154px;
    }
    input:focus
    {
        background-color:Orange;
    }
    .btn_Save
    {
        background-color:#009933; 
        color:White; 
        border:solid 1px grey; 
        background-image:url('../Images/save.png'); 
        background-repeat:no-repeat; 
        background-position-x:3px;
        background-position-y:2px;
        text-align:left;
        padding-left:25px;
        width:80px;
        
    }
    .btn_Approve
    {
        background-color:Orange; 
        color:Black; 
        border:solid 1px grey; 
        background-image:url('../Images/Approved.png'); 
        background-repeat:no-repeat; 
        background-position-x:3px;
        background-position-y:2px;
        text-align:left;
        padding-left:25px;
        width:80px;
    }
    .btn_Cancel
    {
       background-color:#66C2C2; 
       color:Black; 
       border:solid 1px grey;
       background-image:url('../Images/close.gif'); 
       background-repeat:no-repeat; 
       background-position-x:3px;
       background-position-y:2px;
       text-align:left;
       padding-left:25px;
      width:80px;
    }
    .btn_Print
    {
       background-color:White; 
       color:Black; 
       border:solid 1px grey;
       background-image:url('../Images/printer16x16.png'); 
       background-repeat:no-repeat; 
       background-position-x:3px;
       background-position-y:2px;
       text-align:left;
       padding-left:25px;
       width:80px;
    }
        .auto-style1 {
            height: 62px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;font-family:Arial;font-size:12px;">
        <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_Click" CausesValidation="false"></asp:LinkButton>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>        
        <td style="vertical-align: top; position :relative;">
     
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">        
        <tr>
            <td class="text headerband" style="font-size:17px;padding:5px;">&nbsp;Spare Requisition <asp:Label ID="lblReqNo" runat="server"></asp:Label></td>
        </tr>
        </table>
        <table border="0" cellpadding="3" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">        
        <tr style="height:20px; color:#ffffff; font-size:14px;">
            <td style="background-color:#66A3E0;color:white;" >Spares List</td>
        </tr>
        <tr>
           <td style="border: solid 2px #66A3E0;">
                <div id="Div2" style="width: 100%; overflow-y: scroll;overflow-x: hidden; background-color:Orange;" class="scrollbox" >
                <table cellspacing="0" rules="all" border="1" cellpadding="5" style="width: 100%;border-collapse: collapse; height: 22px;font-size:12px;" bordercolor="wheat">
                <colgroup>
                    <%--<col style="text-align: center" width="45px" />--%>
                    <col style="text-align: left" width="80px" />
                    <col style="text-align: left" />
                    <col style="text-align: left" width="100px" />
                    <col style="text-align: left" width="100px" />
                    <col style="text-align: center" width="50px" />
                    <col style="text-align: center" width="100px" />
                    <col style="text-align: center" width="50px" />
                    <col style="text-align: center" width="100px" />
                    <col style="text-align: center" width="50px" />
                    
                    <col width="20px" />
                </colgroup>
                <tr class= "headerstylegrid">
                    <%--<td>
                        <asp:ImageButton runat="server" id="imgbtnAddSpares" OnClick="imgbtnAddSpares_Click" ImageUrl="~/Images/add.png" ToolTip="Add Spares"/>
                    </td>--%>
                    <td>&nbsp;Comp.Code</td>
                    <td>&nbsp;Spare Details</td>
                    <td>&nbsp;Part #</td>
                    <td>&nbsp;Drawing #</td>
                    <td>Req.Qty</td>
                    <td>UOM</td>
                    <td>Qty(O)</td>
                    <td>UOM(O)</td>
                    <td>ROB</td>
                    <td>&nbsp;</td>
                </tr>
                </table>
                </div>
                <div id="dvScrollSparesList" style="width:100%; height:400px;overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset">
                        <table cellpadding="5" cellspacing="0" border="1" bordercolor="#E6E6E6" width="100%" style="border-collapse:collapse; background-color:#e2e2e2;">
                        <colgroup>
                                <%--<col style="text-align: center" width="45px" />--%>
                                <col style="text-align: left" width="80px" />
                                <col style="text-align: left" />
                                <col style="text-align: left" width="100px" />
                                <col style="text-align: left" width="100px" />
                                <col style="text-align: center" width="50px" />
                                <col style="text-align: center" width="100px" />
                                <col style="text-align: center" width="50px" />
                                <col style="text-align: center" width="100px" />
                                <col style="text-align: center" width="50px" />
                    
                                <col width="20px" />
                            </colgroup>
                        <asp:Repeater ID="rptSpares" runat="server" >
                                        <ItemTemplate>
                                            <tr style=" background-color:White"  onmouseover="this.style.backgroundColor='#e2e2e2';" onmouseout="this.style.backgroundColor='white';">                                                        
                                                <%--<td style="text-align:center">
                                                    <asp:ImageButton runat="server" ID="btnDelSpare" OnClick="btnDelSpare_Click" ImageUrl="~/Images/cancel.png" vesslcode='<%#Eval("VesselCode")%>' componentid='<%#Eval("ComponentId")%>' officeship='<%#Eval("Office_Ship")%>' spareId='<%#Eval("SpareId")%>' OnClientClick="return window.confirm('Are you sure to remove this?');" />
                                                </td>--%>
                                                <td align="left"><%#Eval("ComponentCode")%></td>
                                                <td align="left">
                                                    <b>
                                                        <%--<asp:LinkButton runat="server" ID="lnkEditSpare" OnClick="btnEditSpare_Click" Text='<%#Eval("SpareName")%>' vesslcode='<%#Eval("VesselCode")%>' componentcode='<%#Eval("ComponentCode")%>' officeship='<%#Eval("Office_Ship")%>' spareId='<%#Eval("SpareId")%>'></asp:LinkButton>--%>
                                                        <%#Eval("SpareName")%>
                                                    </b>
                                                    <div style="border-top:dotted 1px purple; margin-top:2px;" >
                                                    <span style="color:Purple">Maker : <%#Eval("Maker")%> <span style="color:blue">( <%#Eval("MakerType")%> ) </span> </span>
                                                    </div>
                                                    <div style="border-top:dotted 1px green; margin-top:2px; color:Green;" >
                                                    <%#Eval("Specification")%>
                                                    </div>
                                                </td>
                                                <td align="left"><%#Eval("PartNo")%></td>
                                                <td align="left"><%#Eval("DrawingNo")%></td>
                                                <td align="left"><%#Eval("Qty")%></td>
                                                <td align="left"><%#Eval("UOM")%></td>
                                                <td><asp:TextBox ID="txtReqQty" vesslcode='<%#Eval("VesselCode")%>' componentid='<%#Eval("ComponentId")%>' officeship='<%#Eval("Office_Ship")%>' spareId='<%#Eval("SpareId")%>' Text='<%#Eval("OfficeQty")%>' onkeypress="fncInputNumericValuesOnly(event)" style="text-align:center" Width="40px" MaxLength="6" runat="server"></asp:TextBox></td>                                                        
                                                <td>
                                                <asp:DropDownList runat="server" ID="ddlUnit" DataSource='<%#LoadUnits()%>' Width="95px" SelectedValue='<%#Eval("OfficeUOM")%>' ></asp:DropDownList>
                                                </td>
                                                <td>
                                                <asp:Label ID="lblROBQty" Text='<%#Eval("ROB")%>' style="text-align:center" runat="server"></asp:Label>
                                                </td>
                                                
                                                    <td>&nbsp;</td>
                                            </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                        </table>
                </div>
                 </td>
        </tr>
        </table>
        </td>
        </tr>        
        <tr>
           <td style="text-align:right;border: solid 2px #66A3E0; padding:2px;">
           <table border="0" cellpadding="2" cellspacing="2" width="100%">                
            <tr>
            <td style="background-color:#66A3E0;font-size:13px;padding:5px; text-align:center; color:White;">&nbsp;Requisition Details</td>
            </tr>
            </table>
                <table border="0" cellpadding="2" cellspacing="2" width="100%">                
                <tr>
                    <td style="text-align:right; " class="style1">Requisition # :</td>
                    <td style="text-align:left;">
                        <asp:Label ID="txtReqNo" Enabled="false" Font-Bold="true"  MaxLength="20" runat="server"></asp:Label>
                    </td>
                    <td style="text-align:right;">
                        Account Code :</td>
                    <td style="text-align:left;">
                        <%--<asp:DropDownList runat="server" ID="ddlAccCode" Width="80px" BackColor="#FFFFCC"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="ddlAccCode" ValidationGroup="vv" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                        <asp:Label ID="lblAccountCode" runat="server"></asp:Label>
                    </td>
                    <td style="text-align:right; width:150px;">Port of Supply :</td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="txtPort" MaxLength="50" runat="server" BackColor="#FFFFCC"  ValidationGroup="vv"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtPort" ValidationGroup="vv" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    <td style="text-align:right;  width:150px;">ETA to Port :</td>
                    <td style="text-align:left;"><asp:TextBox ID="txtETA" MaxLength="11" BackColor="#FFFFCC" runat="server" Width="90px" ValidationGroup="vv"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtETA" ValidationGroup="vv" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                  <tr>
                    <td style="text-align:right;" class="style1">Remarks :</td>
                    <td colspan="7" style="text-align:left;"><asp:TextBox ID="txtRemarks" 
                            MaxLength="5000" runat="server" 
                            Width="99%" Height="54px" TextMode="MultiLine"></asp:TextBox></td>
                </tr> 
                <tr>                   
                    <td style="text-align:right; " class="style1">Updated By / On :</td>
                    <td style="text-align:left;">
                    <asp:TextBox ID="txtUpdatedBy" BackColor="#FFFFCC" MaxLength="50" runat="server" ValidationGroup="vv"></asp:TextBox> / <asp:Label ID="lblUpdatedOn"  runat="server"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtUpdatedBy" ValidationGroup="vv" ErrorMessage="*"></asp:RequiredFieldValidator>

                    <asp:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtETA" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                    </td>
                    <td style="text-align:left;">&nbsp;</td>
                    <td style="text-align:left;">&nbsp;</td>                    
                    <td style="text-align:right;">Verified By / On :</td>
                    <td style="text-align:left;">
                        <asp:Label ID="lblVerifiedBy" runat="server" ></asp:Label> / <asp:Label ID="lblVerifiedOn"  runat="server"></asp:Label>
                    </td>
                    <td style="text-align:right;">Transfered By / On :</td>
                    <td style="text-align:left;">
                       <asp:Label ID="lblTransferedBy" runat="server" ></asp:Label> / <asp:Label ID="lblTransferedOn"  runat="server"></asp:Label>
                    </td>
                </tr> 
              

                </table>
           </td>
        </tr>
        <tr>
           <td style="text-align:right;border: solid 2px #66A3E0; padding:2px;">
              <asp:Label ID="lblMsg" Text="Message" ForeColor="Red" runat="server"></asp:Label>&nbsp;
              <%--<asp:Button ID="btnApprove" Text="Approve" runat="server" onclick="btnApprove_Click" ValidationGroup="vv" class="btn_Approve"></asp:Button>--%>
              <asp:Button ID="btnSave" Text="Save" runat="server" onclick="btnSave_Click" ValidationGroup="vv" CssClass="btn"></asp:Button>

              <asp:Button ID="btnTransfer" Text="Send for RFQ" runat="server" onclick="btnTransfer_Click" ValidationGroup="vv" CssClass="btn" OnClientClick="return window.confirm('Are you sure to send for RFQ.');" Visible="false" ></asp:Button>
              <asp:Button ID="btnInactive" Text="Make Inactive" runat="server" onclick="btnInactive_Click" ValidationGroup="vv" CssClass="btn"  OnClientClick="return window.confirm('Are you sure to Make Inactive.');" Visible="false" ></asp:Button>
              <asp:Button ID="btnPrint" Text="Print" runat="server" onclick="btnPrint_Click" CssClass="btn" ></asp:Button>
           </td>
        </tr>
        </table>
        <%-- Products Section --%>
         <div style="position: absolute; top: 0px; left: 0px; height: 520px; width: 100%;" id="dvSpares" runat="server" visible="false">
                        <center>

                            <div style="position: fixed; top: 0px; left: 0px; min-height:100%;width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            <div style="position: relative; width: 1200px; height: 560px; padding: 3px; text-align: center;background: white; z-index: 150; top: 100px; border: solid 10px gray">
                            <table cellpadding="4" cellspacing="2" width="100%" border="0">
                            <tr>
                                <td style="text-align: center;  font-size:14px; " class="text headerband" >Select Spares</td>
                            </tr>                     
                            <tr>
                                <td style="text-align: center; background-color:#E0F0FF; font-size:14px; color:Blue;" >
                                    <table cellpadding="1" cellspacing="2" width="100%" border="0">
                                        <tr>
                                        <td style="text-align:right">Search By Component Code :</td>
                                        <td style="text-align:left"><asp:TextBox runat="server" ID="txt_F_CompCode" MaxLength="15" BackColor="#FFFFCC"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" Text="*" ControlToValidate="txt_F_CompCode" ValidationGroup="sch"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align:right">Account Code :</td>
                                        <td style="text-align:left"><asp:TextBox runat="server" ID="txt_F_AccCode" MaxLength="4" BackColor="#FFFFCC"></asp:TextBox>
                                        </td>
                                        <td>
                                        <asp:Label runat="server" ID="lblRecCount"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearchSpares" runat="server" Width="100px" Height="20px" Text=" Search " ValidationGroup="sch" CssClass="btn" OnClick="btnSearchSpares_Click"  />
                                        </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                                    
                            <tr>
                                <td>
                                    <div style="overflow-y: scroll; overflow-x: hidden; height: 25px;">
                                    <table cellspacing="0" rules="all" border="1" cellpadding="5" style="width: 100%;border-collapse: collapse; height: 22px;font-size:12px;" bordercolor="wheat">
                                                    <colgroup>
                                                        <col style="text-align: center" width="40px" />
                                                        <col style="text-align: left" width="150px" />
                                                        <col style="text-align: left" />
                                                        <col style="text-align: left" width="100px" />
                                                        <col style="text-align: left" width="100px" />
                                                        <col style="text-align: center" width="50px" />
                                                        <col width="20px" />
                                                    </colgroup>
                                                    <tr class= "headerstylegrid">
                                                        <td><input type="checkbox" id="chkselall" /></td>
                                                        <td>&nbsp;Comp.Code / Name </td>
                                                        <td>&nbsp;Spare Details</td>
                                                        <td>&nbsp;Part #</td>
                                                        <td>&nbsp;Drawing #</td>
                                                        <td>ROB</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                        </table>
                                    </div>
                                    <div id="divScroll_Products" style="overflow-y: scroll; overflow-x: hidden;height: 420px;" class="scrollbox" onscroll="SetScrollPos(this)">
                                        <table cellspacing="0"  border="1" cellpadding="5" style="width: 100%;border-collapse: collapse;" bordercolor="wheat">
                                            <colgroup>
                                                        <col style="text-align: left" width="40px" />
                                                        <col style="text-align: left" width="150px" />
                                                        <col style="text-align: left" />
                                                        <col style="text-align: left" width="100px" />
                                                        <col style="text-align: center" width="100px" />
                                                        <col style="text-align: center" width="50px" />
                                                        <col width="20px" />
                                            </colgroup>
                                            <asp:Repeater ID="rptPopSpares" runat="server">
                                                <ItemTemplate>
                                                    <tr onmouseover="this.style.backgroundColor='#e2e2e2';" onmouseout="this.style.backgroundColor='white';">                                                        
                                                        <td style="text-align:center">
                                                            <input type="checkbox" runat="server" id="chkSelect" class="selall1" vesslcode='<%#Eval("VesselCode")%>' componentid='<%#Eval("ComponentId")%>' officeship='<%#Eval("Office_Ship")%>' spareId='<%#Eval("SpareId")%>' uom='<%#Eval("UOM")%>'/>
                                                        </td>
                                                        <td align="left"><%#Eval("ComponentCode")%>
                                                        <div style="border-top:dotted 1px purple; margin-top:2px; color: Blue; " >
                                                        <%#Eval("ComponentName")%>
                                                        </div>
                                                        </td>
                                                        <td align="left">
                                                            <b><asp:LinkButton runat="server" ID="lnkEditSpare" OnClick="btnEditSpare_Click" Text='<%#Eval("SpareName")%>' vesslcode='<%#Eval("VesselCode")%>' componentcode='<%#Eval("ComponentCode")%>' officeship='<%#Eval("Office_Ship")%>' spareId='<%#Eval("SpareId")%>'></asp:LinkButton></b>
                                                            <div style="border-top:dotted 1px purple; margin-top:2px;" >
                                                            <span style="color:Purple">Maker : <%#Eval("Maker")%> <span style="color:blue">( <%#Eval("MakerType")%> ) </span> </span>
                                                            </div>
                                                            <div style="border-top:dotted 1px green; margin-top:2px; color:Green;" >
                                                            <%#Eval("Specification")%>
                                                            </div>
                                                        </td>
                                                        <td align="left"><%#Eval("PartNo")%></td>
                                                        <td align="left"><%#Eval("DrawingNo")%></td>
                                                        <td>
                                                        <asp:Label ID="lblROBQty" Text='<%#Eval("ROBQty")%>' style="text-align:center" runat="server"></asp:Label></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </td>
                            
                            </tr>
                                    <tr>
                                        <td style="text-align:right;"> 
                                            <asp:Label ID="lblerrorMsg" runat="server" ForeColor="Red" >&nbsp;</asp:Label>
                                            <asp:Button ID="btnAddToList" runat="server" Width="100px" CssClass="btn" Text=" Save "  OnClick="btnAddToList_Click" />                                           
                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Width="100px" CssClass="btn" Text=" Close "  OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger  ControlID="btnCancel" />
                            </Triggers>
                            </asp:UpdatePanel>
         
                        </center>
         </div>
    <%-- InActive Section --%>
        <div style="position: absolute; top: 0px; left: 0px; height: 520px; width: 100%;" id="dvInactive" runat="server" visible="false">
        <center>
            <div style="position: fixed; top: 0px; left: 0px; min-height:100%;width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
            <div style="position: relative; width: 600px; height: 150px; padding: 3px; text-align: left;background: white; z-index: 150; top: 200px; border: solid 10px gray">
                <table cellpadding="0" cellspacing="0" width="100%" border="0"> 
                <tr>
                    <td style="text-align:center; padding:3px;"><b>Please Enter comments to make Inactive</b></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox runat="server" Width="99%" TextMode="MultiLine" Height="80px" ID="txtOFCComments" ValidationGroup="ec"></asp:TextBox> 
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        <asp:RequiredFieldValidator runat="server" id="r11" ControlToValidate="txtOFCComments" ValidationGroup="ec" ErrorMessage="Comments are required." ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center;">
                          <asp:Button ID="btnEnter" Text="Save" runat="server" onclick="btnInactiveSave_Click" ValidationGroup="ec"  width="100px" CssClass="btn"></asp:Button>
                          <asp:Button ID="btnCloseComm" Text="Close" runat="server" onclick="btnCloseComm_Click" CausesValidation="false" CssClass="btn" width="100px"></asp:Button>
                    </td>
                </tr>
                </table>
            </div>
        </center>
     </div>
     </div>

    </form>
</body>
</html>
