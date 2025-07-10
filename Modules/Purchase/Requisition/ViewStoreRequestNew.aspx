<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewStoreRequestNew.aspx.cs" Inherits="MenuPlanner_ViewStoreRequestNew" %>
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
    td{
        vertical-align:middle;
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
            <td class="text headerband" style="font-size:17px;padding:5px;">&nbsp;Store Requisition <asp:Label ID="lblReqNo" runat="server"></asp:Label></td>
        </tr>
        </table>

        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <col width="400px" />
            <col />
            <tr>
                <td style="vertical-align:top;">
                    <table border="0" cellpadding="2" cellspacing="0" width="100%">                
            <tr>
            <td style="background-color:#66A3E0;font-size:13px;padding:5px; text-align:center; color:White;">&nbsp;Requisition Details</td>
            </tr>
            </table>
                    <table border="0" cellpadding="2" cellspacing="2" width="100%" style="text-align:left;">   
                    <tr>
                        <td >Requisition # :</td>
                        <td >
                            <asp:Label ID="txtReqNo" Enabled="false" Font-Bold="true"  MaxLength="20" runat="server"></asp:Label>
                        </td>
                    </tr>     
                        <tr>
                             <td >Account Code :</td>
                            <td >  
                                <asp:Label ID="lblAccountCode" runat="server"></asp:Label>
                            </td>
                        </tr>    
                        <tr>
                            <td >Port of Supply :</td>
                            <td >
                                <asp:TextBox ID="txtPort" MaxLength="50" runat="server" BackColor="#FFFFCC"  ValidationGroup="vv"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtPort" ValidationGroup="vv" ErrorMessage="*"></asp:RequiredFieldValidator>
                             </td>
                        </tr>  
                        <tr>
                            <td >ETA to Port :</td>
                            <td ><asp:TextBox ID="txtETA" MaxLength="11" BackColor="#FFFFCC" runat="server" Width="90px" ValidationGroup="vv"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtETA" ValidationGroup="vv" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                
                  <tr>
                    <td colspan="2" >
                        Remarks : <br />
                        <asp:TextBox ID="txtRemarks" MaxLength="5000" runat="server" Width="99%" Height="54px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>  
                <tr>
                    <td >Updated By / On :</td>
                    <td >
                    <asp:TextBox ID="txtUpdatedBy" BackColor="#FFFFCC" MaxLength="50" runat="server" ValidationGroup="vv"></asp:TextBox> / <asp:Label ID="lblUpdatedOn"  runat="server"></asp:Label>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtUpdatedBy" ValidationGroup="vv" ErrorMessage="*"></asp:RequiredFieldValidator>

                    <asp:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtETA" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td >Verified By / On :</td>
                    <td >
                        <asp:Label ID="lblVerifiedBy" runat="server" ></asp:Label> / <asp:Label ID="lblVerifiedOn"  runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>  
                    
                    <td style="text-align:right;">Transfered By / On :</td>
                    <td style="text-align:left;">
                       <asp:Label ID="lblTransferedBy" runat="server" ></asp:Label> / <asp:Label ID="lblTransferedOn"  runat="server"></asp:Label>
                    </td>
                </tr> 
              

                </table>
                </td>
                <td>
                    <table border="0" cellpadding="3" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">        
                        <tr style="height:20px; color:#ffffff; font-size:14px;">
                            <td style="background-color:#66A3E0;">Items List</td>
                        </tr>
                        <tr>
                           <td >
                                <div id="Div2" style="width: 100%; overflow-y: scroll;overflow-x: hidden; background-color:Orange;" class="scrollbox" >
                                <table cellspacing="0" rules="all" border="1" cellpadding="5" style="width: 100%;border-collapse: collapse; height: 22px;font-size:12px;" bordercolor="wheat">
                                <colgroup>
                                        <%--<col style="text-align: center" width="45px" />--%>
                                        <col style="text-align: left" />
                                        <col style="text-align: left" width="100px" />
                                        <col style="text-align: left" width="100px" />
                                        <col style="text-align: left" width="100px" />
                                        <col style="text-align: center" width="100px" />
                                        <col style="text-align: center" width="50px" />
                                        <col width="20px" />
                                </colgroup>
                                <tr class= "headerstylegrid">
                                    <%--<td>
                                    <asp:ImageButton runat="server" id="imgbtnAddStore" OnClick="imgbtnAddStore_Click" ImageUrl="~/Images/add.png" ToolTip="Add Item(s)"/>
                                    </td>--%>
                                    <%--<td>&nbsp;</td>--%>
                                    <td>&nbsp;Item Description</td>
                                    <td>Req.Qty</td>
                                    <td>Office Qty</td>
                                    <td>UOM</td>
                                    <td>&nbsp;IMPA</td>
                                    <td>ROB</td>
                                    <td>&nbsp;</td>
                                </tr>
                                </table>
                                </div>
                                <div id="dvScrollSparesList" style="width:100%; height:340px;overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset">
                                        <table cellpadding="5" cellspacing="0" border="1" bordercolor="#E6E6E6" width="100%" style="border-collapse:collapse; background-color:#e2e2e2;">
                                        <colgroup>
                                       <%-- <col style="text-align: center" width="45px" />--%>
                                        <col style="text-align: left" />
                                        <col style="text-align: left" width="100px" />
                                        <col style="text-align: left" width="100px" />
                                        <col style="text-align: left" width="100px" />
                                        <col style="text-align: center" width="100px" />
                                        <col style="text-align: center" width="50px" />
                                        <col width="20px" />
                                         </colgroup>
                                        <asp:Repeater ID="rptStores" runat="server" >
                                                        <ItemTemplate>
                                                            <tr style=" background-color:White"  onmouseover="this.style.backgroundColor='#e2e2e2';" onmouseout="this.style.backgroundColor='white';">                                                        
                                                               <%-- <td style="text-align:center">
                                                                    <asp:ImageButton runat="server" ID="btnDeleteItem" OnClick="btnDeleteItem_OnClick" ImageUrl="~/Images/close.gif" pid='<%#Eval("pid")%>' Visible='<%#btnAddNew.Visible%>'  OnClientClick="return window.confirm('Are you sure to remove this item ?');" />
                                                                </td>--%>
                                                                <td align="left">
                                                                   <%--<asp:LinkButton runat="server" ID="lnkDescription" OnClick="btnEditStore_Click" Text='<%#Eval("ItemName")%>' ItemId='<%#Eval("ItemId")%>' ></asp:LinkButton>--%>
                                                                   <b><%#Eval("PName")%></b>
                                                                   <%--<div style="border-top:dotted 1px blue; margin-top:3px;">
                                                                   <%#Eval("Description")%>
                                                                   </div>--%>
                                                                </td>
                                                                <td><asp:Label ID="txtReqQty" Text='<%#Eval("Qty")%>' onkeypress="fncInputNumericValuesOnly(event)" style="text-align:center" Width="40px" MaxLength="6" runat="server"></asp:Label></td>                                                        
                                                                <td>
                                                                    <asp:TextBox ID="txtOffQty" vesslcode='<%#Eval("VesselCode")%>' Text='<%#Eval("OfficeQty")%>' onkeypress="fncInputNumericValuesOnly(event)" style="text-align:center" Width="40px" MaxLength="6" runat="server"></asp:TextBox>                                                    
                                                                    <asp:HiddenField ID="hfdPid" runat="server" Value='<%#Eval("PID")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblUnit" Text='<%#Eval("UnitName")%>' style="text-align:center" Width="95%" MaxLength="10" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                <%#Eval("impano")%>
                                                                </td>                                                        
                                                                <td>
                                                                    <asp:Label ID="lblROBQty" Text='<%#Eval("ROB")%>' style="text-align:center" Width="95%" MaxLength="10" runat="server"></asp:Label>
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
                    <%--<div style="padding:5px">
                        <asp:Button runat="server" ID="btnAddNew" Text="+ Add new Item " OnClick="btnAddNew_Click" style="background-color:orange;border:none;padding:5px; color:white;" />
                    </div>--%>
                </td>
            </tr>
        </table>

            <%-------------------------------------------------------------------------------------------------------------%>
                <table cellpadding="5" cellspacing="0" border="2" bordercolor="white" width="100%" style="border-collapse:collapse; background-color:#e2e2e2;">
            <tr>
            <td style='color:Black'>
            <%--<asp:UpdatePanel runat="server" ID="up11">
            <ContentTemplate>--%>
                <div style='color:Black; font-size:13px; text-decoration:none; width:100%;'><b>Extra Items</b>&nbsp;
                </div>
                <div id="Div3" style="width: 100%; overflow-y: scroll;overflow-x: hidden;" class="scrollbox" >
                    <table cellspacing="0" border="1" cellpadding="5" style="width: 100%; border-collapse: collapse;background-color: #e2e2e2;" bordercolor="wheat" >
                            <colgroup>
                                <%--<col style="text-align: center" width="45px" />--%>
                                <col style="text-align: left" />
                                <col style="text-align: left" width="100px" />
                                <col style="text-align: left" width="100px" />
                                <col style="text-align: left" width="130px" />
                                <col style="text-align: left" width="100px" />
                                <col width="20px" />
                            </colgroup>
                            <tr class= "headerstylegrid">
                                <%--<td>
                                    <asp:ImageButton ID="imgbtnAddOthreItems" ToolTip="Add items" ImageAlign="Middle" ImageUrl="~/Images/add.png" runat="server"  />                                                                    
                                </td>--%>
                                <td>&nbsp;Product Name</td>
                                <td>&nbsp;Req. Qty</td>
                                <td>&nbsp;ROB Qty</td>
                                <td>&nbsp;Unit</td>
                                <td>&nbsp;IMPA</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                </div>
                <div id="dvOtherItems" class='dvSlide1' style="width:100%; height:120px;overflow-y: scroll; overflow-x: hidden; border:solid 1px #e2e2e2;background-color:#FBFBFB; margin-top:5px;" class="scrollbox" onscroll="SetScrollPos(this)">
                    <table cellspacing="0"  border="1" cellpadding="5" style="width: 100%; border-collapse: collapse;" bordercolor="wheat">
                    <colgroup>
                        <%--<col style="text-align: center" width="45px" />--%>
                        <col style="text-align: left" />
                        <col style="text-align: left" width="100px" />                                                
                        <col style="text-align: left" width="100px" />
                        <col style="text-align: left" width="130px" />
                        <col style="text-align: left" width="100px" />
                        <col width="20px" />
                    </colgroup>
                    <asp:Repeater ID="rptOtherItems" runat="server">
                        <ItemTemplate>
                            <tr>        
                                <%--<td style="text-align:center">
                                    <asp:ImageButton runat="server" ID="btnDelteExtraItem" OnClick="btnDeleteExtraItem_OnClick" Visible='<%#btnAddNew.Visible%>' ImageUrl="~/Images/close.gif"  ToolTip="Delete" OnClientClick="return confirm('are you sure to remove this item ?');" />
                                    <asp:HiddenField ID="hfdStoreReqId" runat="server" Value='<%#Eval("StoreReqId")%>' />
                                    <asp:HiddenField ID="hfdSno" runat="server" Value='<%#Eval("Sno")%>' />
                                </td> --%>                                               
                                <td align="left">
                                    &nbsp;<asp:Label ID="txtProdName" Text='<%#Eval("PName")%>' Width="310px" MaxLength="50" runat="server"></asp:Label>                                    
                                </td>
                                <td align="center">
                                    &nbsp;<asp:Label ID="txtReqdQty" CssClass="reqQty" Text='<%#Eval("Qty")%>' onkeypress="fncInputNumericValuesOnly(event)" Width="40px" MaxLength="6" runat="server"></asp:Label></td>                                                        
                                <td align="center">
                                    &nbsp;<asp:Label ID="txtROBQty" Text='<%#Eval("ROB")%>' onkeypress="fncInputNumericValuesOnly(event)" Width="40px" MaxLength="6" runat="server"></asp:Label></td>                                
                                <td align="center">                                    
                                    &nbsp;<asp:Label ID="txtUOM" Text='<%#Eval("UOM")%>'  Width="40px"  runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    &nbsp;<asp:Label ID="txtIsaaImpa"  Width="80px" MaxLength="50" runat="server" Text='<%#Eval("ISSAIMPA")%>'></asp:Label>                                    
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            </td>
            </tr>
        </table>
            <%-------------------------------------------------------------------------------------------------------------%>
        </td>
        </tr>        
        
        <tr>
           <td style="text-align:right;border: solid 2px #66A3E0; padding:2px;">
              <asp:Label ID="lblMsg" Text="Message" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>&nbsp;
              <%--<asp:Button ID="btnApprove" Text="Approve" runat="server" onclick="btnApprove_Click" ValidationGroup="vv" class="btn_Approve"></asp:Button>--%>
              <asp:Button ID="btnSave" Text="Save" runat="server" onclick="btnSave_Click" ValidationGroup="vv" class="btn"></asp:Button>
              <asp:Button ID="btnTransfer" Text="Send for RFQ" runat="server" onclick="btnTransfer_Click" ValidationGroup="vv" class="btn" OnClientClick="return window.confirm('Are you sure to Send for RFQ.');" Visible="false" ></asp:Button>
              <asp:Button ID="btnInactive" Text="Make Inactive" runat="server" onclick="btnInactive_Click" ValidationGroup="vv" class="btn"  OnClientClick="return window.confirm('Are you sure to Make Inactive.');" Visible="false" ></asp:Button>
              <asp:Button ID="btnPrint" Text="Print" runat="server" onclick="btnPrint_Click" class="btn" ></asp:Button>
           </td>
        </tr>
        </table>
        <%-- Products Section --%>
        <div style="position: absolute; top: 0px; left: 0px; height: 520px; width: 100%;" id="dvStore" runat="server" visible="false">
        <center>
            <div style="position: fixed; top: 0px; left: 0px; min-height:100%;width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
            <div style="position: relative; width: 1200px; height: 560px; padding: 3px; text-align: left;background: white; z-index: 150; top: 100px; border: solid 10px gray">
            <asp:TabContainer runat="server" ID="tab">
            <asp:TabPanel HeaderText="Add / Modify Item" runat="server" ID="t1">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                <table cellpadding="4" cellspacing="0" width="100%" border="1" bordercolor="#66A3E0" style="border-collapse:collapse">
                <tr>
                    <td style="text-align: center; background-color:#66A3E0; font-size:14px; color:white;" >Add / Modify Store Item</td>
                </tr> 
                <tr>
                <td>
                    <table cellpadding="4" cellspacing="2" width="100%" border="0">
                    <tr>
                        <td width="150px">Item Name :</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtItemName" Width="90%" BackColor="#FFFFCC" MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtItemName" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="150px">Item Description :</td>
                        <td>
                        <asp:TextBox runat="server" ID="txtDescription" Width="90%" TextMode="MultiLine" Height="50px" ></asp:TextBox>
                                       
                        </td>
                    </tr>
                    <tr>
                        <td>IMPA Code :</td>
                        <td>
                        <asp:TextBox runat="server" ID="txtAddISSA" Width="200px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>UOM : </td>
                        <td>
                        <asp:DropDownList runat="server" ID="ddlUnit" BackColor="#FFFFCC" ></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtQty" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>ROB Qty :</td>
                        <td>
                        <asp:TextBox runat="server" ID="txtROB" Width="50px" BackColor="#FFFFCC" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtROB" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Req. Qty : </td>
                        <td>
                        <asp:TextBox runat="server" ID="txtQty" Width="50px" BackColor="#FFFFCC" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtQty" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Account Code : </td>
                        <td>
                        <asp:TextBox runat="server" ID="txtAccountCode" Width="50px" BackColor="#FFFFCC" MaxLength="4"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtAccountCode" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    </table>
                </td>
                </tr>
                <tr>
                    <td style="text-align:right;"> 
                        <asp:Label ID="lblMessage2" runat="server" ForeColor="Red" >&nbsp;</asp:Label>
                        <asp:Button ID="btnAddStoreItem" runat="server" Width="100px" CssClass="btn" Text=" Save " style="background-color:Green; color:White; border:solid 1px grey;" OnClick="btnAddStoreItem_Click" />                                           
                        <asp:Button ID="btnCancel1" runat="server" CausesValidation="false" Width="100px" CssClass="btn" Text=" Close " style="background-color:red; color:White; border:solid 1px grey;" OnClick="btnCancel_Click" />
                    </td>
                </tr>
                </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger  ControlID="btnCancel1" />
                    <asp:PostBackTrigger  ControlID="btnAddStoreItem" />
                </Triggers>
                </asp:UpdatePanel>

            </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel HeaderText="Select from Existing" runat="server" ID="t2">
            <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <table cellpadding="4" cellspacing="2" width="100%" border="0">
            <tr>
                <td style="text-align: center; background-color:#66A3E0; font-size:14px; color:white;" >Select Existing Items</td>
            </tr>                     
            <tr>
                <td style="text-align: center; background-color:#E0F0FF; font-size:14px; color:Blue;" >
                    <table cellpadding="1" cellspacing="2" width="100%" border="0">
                        <tr>
                        <td style="text-align:right">Search By Description / IMPA :</td>
                        <td style="text-align:left">
                            <asp:TextBox runat="server" ID="txt_F_ItemDesc" MaxLength="100" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                        <asp:Label runat="server" ID="lblRecCount"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btnSearchStores" runat="server" Width="100px" Height="20px" Text=" Search " ValidationGroup="sch" style="padding:0px; color:black; font-size:10px; background-color:Orange; border:solid 1px grey;" OnClick="btnSearchStores_Click"  />
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
                                        <col style="text-align: left" />
                                        <col style="text-align: left" width="100px" />
                                        <col style="text-align: left" width="100px" />
                                        <col style="text-align: left" width="100px" />
                                        <col width="20px" />
                                    </colgroup>
                                    <tr class="blueheader">
                                        <td><input type="checkbox" id="chkselall" /></td>
                                        <td>&nbsp;Item Description</td>
                                        <td>&nbsp;IMPA #</td>
                                        <td>ROB</td>
                                        <td>Acct. Code</td>
                                        <td>&nbsp;</td>
                                    </tr>
                        </table>
                    </div>
                    <div id="divScroll_Products" style="overflow-y: scroll; overflow-x: hidden;height: 382px;" class="scrollbox" onscroll="SetScrollPos(this)">
                        <table cellspacing="0"  border="1" cellpadding="5" style="width: 100%;border-collapse: collapse;" bordercolor="wheat">
                                <colgroup>
                                        <col style="text-align: center" width="40px" />
                                        <col style="text-align: left" />
                                        <col style="text-align: left" width="100px" />
                                        <col style="text-align: left" width="100px" />
                                        <col style="text-align: left" width="100px" />
                                        <col width="20px" />
                            </colgroup>
                            <asp:Repeater ID="rptPopStores" runat="server">
                                <ItemTemplate>
                                    <tr onmouseover="this.style.backgroundColor='#e2e2e2';" onmouseout="this.style.backgroundColor='white';">                                                        
                                        <td style="text-align:center"><input type="checkbox" runat="server" id="chkSelect" class="selall1" ItemId='<%#Eval("ItemId")%>'/></td>
                                        <td align="left">
                                        <%--OnClick="btnEditStore_Click"--%>
                                        <asp:LinkButton runat="server" ID="lnkEditStore" Font-Bold="true" Font-Underline="false" Text='<%#Eval("ItemName")%>' vesslcode='<%#Eval("VesselCode")%>' ItemId='<%#Eval("ItemId")%>'></asp:LinkButton>
                                                <div style="border-top:dotted 1px blue; margin-top:3px;">
                                                <%#Eval("Description")%>
                                            </div>
                                        </td>
                                        <td align="left"><%#Eval("ISSA_IMPA")%></td>
                                        <td><asp:Label ID="lblROBQty" Text='<%#Eval("ROBQty")%>' style="text-align:center" runat="server"></asp:Label></td>
                                        <td align="left"><%#Eval("AccountCode")%></td>
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
                            <asp:Button ID="btnAddToList" runat="server" Width="100px" CssClass="btn" CausesValidation="false" Text=" Save " style="background-color:Green; color:White; border:solid 1px grey;" OnClick="btnAddToList_Click" />                                           

                            <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Width="100px" CssClass="btn" Text=" Close " style="background-color:red; color:White; border:solid 1px grey;" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger  ControlID="btnCancel" />
                </Triggers>
            </asp:UpdatePanel>
            </ContentTemplate>
            </asp:TabPanel>
            </asp:TabContainer>
            </div>
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
                          <asp:Button ID="btnEnter" Text="Save" runat="server" onclick="btnInactiveSave_Click" ValidationGroup="ec" style="background-color:Orange; border:solid 1px grey; color:White;" width="100px"></asp:Button>
                          <asp:Button ID="btnCloseComm" Text="Close" runat="server" onclick="btnCloseComm_Click" CausesValidation="false" style="background-color:Orange; border:solid 1px grey; color:White;" width="100px"></asp:Button>
                    </td>
                </tr>
                </table>
            </div>
        </center>
     </div>

        <%--Link Product ----------------------------------------------------------------------------------%>
        <div style="position: absolute; top: 0px; left: 0px; height: 520px; width: 100%;" id="divForwardProduct" runat="server" visible="false">
        <center>
            <div style="position: fixed; top: 0px; left: 0px; min-height:100%;width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
            <div style="position: relative; width: 80%; padding: 0px; text-align: left;background: white; z-index: 150; top: 30px; border: solid 10px gray">
                <div style="text-align: center; font-size:15px;font-weight:bold;padding:5px;" class="text headerband" > Add New Product to Requst </div>
                <table width="100%" style="background-color:#e2e2e2;font-weight:bold; margin:0 auto;">
                    <tr>
                        <td style="text-align:center"> 
                            <asp:RadioButtonList runat="server" ID="radtype" RepeatDirection="Horizontal" OnSelectedIndexChanged="radtype_OnSelectedIndexChanged" AutoPostBack="true" style="margin:0 auto;">
                            <asp:ListItem Text="Select Existing Item" Value="S" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Create New" Value="C"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:Panel runat="server" ID="pnlSelect" Visible="true">
                  
                    <div>
     <table width="100%" cellpadding="5" cellspacing="0">
         <tr style="background-color:#4371a5;color:white;">
             <td>Product Category</td>
             <td>Items List</td>
         </tr>
         <tr>
             <td style="width:300px;border-right:solid 1px Gray;padding:0px; vertical-align:top;">
                 <div style="height:428px;overflow-x:hidden;overflow-y:scroll;">
                        <asp:TreeView ID="tvCategories" runat="server" onselectednodechanged="tvCategories_SelectedNodeChanged" OnTreeNodePopulate="tvCategories_TreeNodePopulate" ShowLines="true">
                            <LevelStyles>
                                <asp:TreeNodeStyle Font-Underline="False" ForeColor="Purple" />
                                <asp:TreeNodeStyle Font-Underline="False" ForeColor="DarkGreen" />
                                <asp:TreeNodeStyle Font-Underline="False" />
                                <asp:TreeNodeStyle Font-Underline="False" ForeColor="Black" />
                            </LevelStyles>
                            <HoverNodeStyle CssClass="treehovernode" />
                            <SelectedNodeStyle CssClass="treeselectednode" />
                        </asp:TreeView>
                        <asp:HiddenField ID="hfSelectedNodeID" runat="server" />
                        <asp:HiddenField ID="hfSelectedNodeCode" runat="server" />
                        <%--<asp:Button ID="btnSearchedCode" style="display:none" runat="server" onclick="btnSearchedCode_Click" />               --%>
                 </div>
             </td>
             <td style="padding:0px;vertical-align:top;">
                   <div style="text-align:center;padding:5px;">
                        <asp:TextBox runat="server" ID="txtSearch" style="height:18px; width:50%;padding-left:5px;" placeholder="Enter text to search."></asp:TextBox>
                        <asp:Button runat="server" ID="btnFind" Text="Search" OnClick="btnFind_Click" CssClass="btn" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="btn" />
                    </div>
                 <asp:HiddenField runat="server" ID="hfdSelProdId" />
                 <div style="height:30px;overflow-x:hidden;overflow-y:scroll;">
                 <table border="0" cellpadding="2" cellspacing="0" class="bordered" style=" width: 100%;;border-collapse:collapse;">
                                               <col width="40px" />
                                               <col width="120px" />
                                               <col />
                                               <col width="100px" />
                                               <col width="100px" />
                                               <col width="70px" />
                                               <tr style="text-align:left;height:30px;" class= "headerstylegrid">
                                                   <td></td>
                                                   <td>Product Code</td>
                                                   <td>Product Name</td>
                                                   <td>Unit</td>
                                                   <td>IMPA#</td>
                                                   <td>Status</td>
                                               </tr>
                                           </table>
                  </div>
                 <div style="height:369px;overflow-x:hidden;overflow-y:scroll;">
                 <table border="0" cellpadding="2" cellspacing="0" class="bordered" style=" width: 100%;border-collapse:collapse;">
                                               <col width="40px" />
                                               <col width="120px" />
                                               <col />
                                               <col width="100px" />
                                               <col width="100px" />
                                               <col width="70px" />
                                               <asp:Repeater ID="rptProductLL" runat="server">
                                                   <ItemTemplate>
                                                       <tr>
                                                           <td style="text-align:center">
                                                               <input type="radio" name="productLL" onclick='SetProductIDLL(<%#Eval("PID")%>)' />
                                                           </td>
                                                           <td align="left"><%# Eval("PCode") %></td>
                                                           <td align="left"><%# Eval("Pname") %></td>
                                                           <td align="left"><%#Eval("UnitName") %></td>
                                                           <td align="left"><%#Eval("impano") %></td>
                                                           <td align="left"><%#Eval("StatusName") %></td>
                                                       </tr>
                                                   </ItemTemplate>
                                               </asp:Repeater>
                                           </table>
                 </div>
             </td>
         </tr>
     </table>
                                           
                                       
                                       
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlCreate" Visible="false">
                    <table cellpadding="2" cellspacing="3" border="0" width="100%">
                    <tr>
                        <td>Category</td>
                        <td>
                            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Sub Category</td>
                        <td>
                            <asp:DropDownList ID="ddlSubcategory" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Product Name</td>
                        <td>
                            <asp:TextBox ID="txtProductNameFor" runat="server" width='500px'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Unit</td>
                        <td>
                            <asp:DropDownList ID="ddlUnitFor" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>IMPA</td>
                        <td>
                            <asp:TextBox ID="txtImpaFor" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Status</td>
                        <td>
                            <asp:DropDownList ID="ddlStatusFor" runat="server">
                                <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                    
                </asp:Panel> 
                <table cellpadding="5" cellspacing="0" width="100%" style="text-align:center;border-color:#c2c2c2;" border="0" >
                    <tr>
                        <td style="text-align:right;width:80px;">Qty :</td>
                        <td style="text-align:left;width:200px;">
                            <asp:TextBox ID="txtQtyFor" runat="server"></asp:TextBox>
                        </td>
                        <td style="text-align:right;width:80px;">ROB :</td>
                        <td  style="text-align:left;">
                            <asp:TextBox ID="txtRobFor" runat="server"></asp:TextBox>                            
                        </td>
                        <td style="text-align:right;">
                            <asp:Button ID="btnSaveForward" runat="server" Text="Save"  OnClick="btnSaveForward_OnClick" CssClass="btn"  />
                            <asp:Button ID="btnCloseForwardProductPopup" runat="server" Text="Close" CssClass="btn" OnClick="btnCloseForwardProductPopup_OnClick"  />
                        </td>
                    </tr>
                </table>
               <div style="padding:5px;background-color:#f5f158;text-align:center;">
                   <asp:Label ID="lblMsgForward" runat="server" CssClass="error_msg" Font-Bold="true"></asp:Label>
               </div>               
            </div>
        </center>
        <script type="text/javascript">
            function SetProductIDLL(PID)
            {
                $("#hfdSelProdId").val(PID);
            }
        </script>
        </div>
        <%-------------------------------------------------------------------------------------------------%>
        </div>
    </form>
</body>
</html>
