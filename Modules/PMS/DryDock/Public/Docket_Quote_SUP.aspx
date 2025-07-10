<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Docket_Quote_SUP.aspx.cs" Inherits="Docket_Quote_SUP" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <title>Planned Maintenance System </title>
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/JQScript.js" type="text/javascript"></script>
    <style type="text/css">
    .number
    {
        text-align:right;
        width:95%;
        margin:0px;
        border:none;
    }
    </style>
    <script type="text/javascript" language="javascript">
        function fncInputNumericValuesOnly(evnt) {            
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="background-color:#1947A3;color:White; text-align:center; padding:3px; font-size:15px; ">
        <b>RFQ - 
        <asp:Label runat="server" ID="lblRFQNo"></asp:Label>
        </b></div>
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="1">
        <tr><td>
        <div style="border:none; background-color : #ADD6FF; padding:5px; font-size:13px; ">
            <table style="width :100%;">
            <tr>
            <td style="text-align:right; width:">DD # :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblDocketNo"></asp:Label></td>
            <td>
                <span style="color:Red;">Download in PDF</span>&nbsp;<asp:ImageButton id="btnDocketView" runat="server" ImageUrl="~/Images/paperclipx12.png" OnClick="btnDocketView_Click" />
            </td>
            <td style="text-align:right">Vessel :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblVessel"></asp:Label></td>
            <td style="text-align:right">Type :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblType"></asp:Label></td>
            <td style="text-align:right">Plan Duration :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblPlanDuration"></asp:Label></td>
            </tr>
            </table>
        </div>
        </td></tr>
        <tr>        
        <td style=" text-align :left; vertical-align : top;" > 
         <table style="width:100%" cellpadding="0" cellspacing="0">
         <tr>
         <td>
          
         </td>
         </tr>
         </table>
         <table style="width:100%" cellpadding="0" cellspacing="0">
         <tr style=" background-color:#99CCFF; font-size:15px;">
         <td style="padding:3px;"><b>Job Category :</b></td>
         <td style="padding:3px;">
         <b>Job Details :</b>
         </td>
         </tr>
         <tr>
         <td style="width:300px">
         <div style="border:solid 1px gray; text-align:left;font-size :11px;padding:3px; height:521px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="d266">
               <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:15px;" />
                                    <col style="width:40px;" />
                                    <col />
                                    <col style="width:17px;" />
                                </colgroup>
                                <asp:Repeater ID="rptJobCats" runat="server">
                                    <ItemTemplate>
                                            <tr style='<%# (Common.CastAsInt32(Eval("CatId"))== JobCatId)? "background-color:#1589FF;color:white;" : "" %> '>
                                            <td style="text-align:center"><asp:ImageButton runat="server" ID="btnSelectCat" OnClick="btnSelectCat_Click" CommandArgument='<%#Eval("CatId")%>' ImageUrl="~/Images/icon-view-blue_12.png" style="float:right" ToolTip="Select Docking Category" /></td>
                                            <td style="text-align:center"><%#Eval("CatCode")%></td>
                                            <td align="left"><%#Eval("CatName")%></td>   
                                            <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style='<%# (Common.CastAsInt32(Eval("CatId"))== JobCatId)? "background-color:#1589FF;color:white;" : "background-color:#FFF5E6" %>'>
                                            <td style="text-align:center"><asp:ImageButton runat="server" ID="btnSelectCat" OnClick="btnSelectCat_Click" CommandArgument='<%#Eval("CatId")%>' ImageUrl="~/Images/icon-view-blue_12.png" style="float:right" ToolTip="Select Docking Category" /></td>
                                            <td style="text-align:center"><%#Eval("CatCode")%></td>
                                            <td align="left"><%#Eval("CatName")%></td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>  
          </div>
         </td>
         <td >
         <div style="border:solid 1px gray; text-align:left;font-size :11px;height:25px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="Div1">
         <table cellspacing="0" rules="all" border="0" cellpadding="2" style="width:100%;border-collapse:collapse; height:25px; vertical-align:middle; ">
                    <tr style='background-color:#FFC266;'>
                        <td style="text-align:left;width:100px; vertical-align:middle;">SR#</td>
                        <td align="left" style=' vertical-align:middle;'>Job Description</td>
                        <td style="text-align:center;width:30px; vertical-align:middle;">SOR</td>
                        <td style="text-align:center;width:60px; vertical-align:middle;">Bid Qty</td>
                        <td style="text-align:center;width:60px; vertical-align:middle;">Quote Qty</td>
                        <td style="text-align:center;width:100px; vertical-align:middle;">Unit</td>
                        <td style="text-align:center;width:100px; vertical-align:middle;">Unit Rate<asp:Label ID="lblCurr1" ForeColor="Blue" runat="server"></asp:Label></td>
                        <td style="text-align:center;width:50px; vertical-align:middle;">Disc(%)</td>
                        <td style="text-align:center;width:100px; vertical-align:middle;">Net Amt.<asp:Label ID="lblCurr2" ForeColor="Blue" runat="server"></asp:Label></td>
                        <td style="text-align:center;width:20px; vertical-align:middle;">&nbsp;</td>
                        <td style="text-align:left;width:20px; vertical-align:middle;">&nbsp;</td>
                    </tr>
         </table>
         </div>
         <div style="border:solid 1px gray; text-align:left;font-size :11px;height:500px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="dv26">
                <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:60px;" />
                                    <col/>
                                    <col style="width:17px;" />
                                </colgroup>
                                <asp:Repeater ID="rptJobs" runat="server">
                                    <ItemTemplate>
                                           <tr style="background-color:#E0F5FF;">
                                           <td style="text-align:center; padding:4px;"><b><%#Eval("JobCode")%></b></td>
                                           <td align="left" style="padding:5px;"><div style="height:14px"><b><%#Eval("JobName")%></b></div></td>
                                           <td>&nbsp;</td>
                                           </tr>
                                           <tr style=''>
                                           <td colspan="3" style="background-color:#E0F5FF;border-bottom:solid 1px white;">
                                                    <table cellspacing="0" rules="all" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;">
                                                    <asp:Repeater ID="rptSubJobs" runat="server" DataSource='<%#BindSubJobs(Eval("DocketJobId"))%>'>
                                                       <ItemTemplate>
                                                            <tr style='background-color:white;'>
                                                            <td style="text-align:left;width:110px;"><b><%#Eval("SubJobCode")%></b>
                                                            <div id="Div3" style="color:Red; clear:both;" runat="server" visible='<%#(Eval("VendorRemarks").ToString().Trim()!="")%>'>
                                                                Vendor Remarks :
                                                            </div>
                                                            </td>
                                                            <td align="left" >
                                                                <div>
                                                                    <div style="float:left;white-space: nowrap; width: 290px; overflow: hidden;text-overflow: ellipsis;"><%#Eval("SubJobName")%></div>
                                                                    <div style=" float:right; padding-right:10px;">
                                                                        <a href="#" style="font-size:10px; color:blue; text-decoration:italc;" onclick="View_More(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' title='View Job Details..' >More..</a>
                                                                    </div>
                                                                </div>
                                                                <div style="color:Red; clear:both;" runat="server" visible='<%#(Eval("VendorRemarks").ToString().Trim()!="")%>'>
                                                                <%#Eval("VendorRemarks")%>
                                                                </div>
                                                            </td>
                                                            <td style="text-align:center;width:30px;"><asp:ImageButton runat="server" Visible='<%#(Eval("AttachmentName").ToString().Trim()!="")%>' ID="imgDownload" ImageUrl="~/Images/paperclipx12.png" CommandArgument='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' OnClick="imgDownload_Click" /></td>
                                                            <td style="text-align:right;width:60px;"><%#Eval("BidQty")%></td>
                                                            <td style="text-align:right;width:60px;"><asp:TextBox runat="server" ID="txtQty" ReadOnly='<%#!CanEdit%>' CssClass="number" onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("SUPQty")%>' MaxLength="8" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>'></asp:TextBox></td>
                                                            <td style="text-align:left;width:100px;"><%#Eval("Unit")%></td>
                                                            <td style="text-align:right;width:100px;"><asp:TextBox runat="server" ID="txtRate" ReadOnly='<%#!CanEdit%>' CssClass="number" onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("SUPUnitPrice_USD")%>' MaxLength="10" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' ></asp:TextBox></td>
                                                            <td style="text-align:right;width:50px;"><asp:TextBox runat="server" ID="txtDisc" ReadOnly='<%#!CanEdit%>' CssClass="number" onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("SUPDiscountPer")%>' MaxLength="3" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' ></asp:TextBox></td>
                                                            <td style="text-align:right;width:100px;"><asp:Label runat="server" ID="lblNetAmt" Text='<%#Eval("SUPNetAmount_USD")%>' style="text-align:right" ></asp:Label></td>
                                                            <td style="text-align:center;width:20px;"><span id="Span1" runat="server" visible='<%#CanEdit%>'><img title="Remarks" style="cursor:pointer;" onclick="View_Remarks(this);" src="../Images/AddPencil.gif" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' /></span></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            </tr>
                                                    </ItemTemplate>       
                                                       <AlternatingItemTemplate>
                                                            <tr style='background-color:#FFF5E6'>
                                                           <td style="text-align:left;width:110px;"><b><%#Eval("SubJobCode")%></b>
                                                            <div id="Div3" style="color:Red; clear:both;" runat="server" visible='<%#(Eval("VendorRemarks").ToString().Trim()!="")%>'>
                                                                Vendor Remarks :
                                                            </div>
                                                            </td>
                                                            <td align="left" >
                                                                <div>
                                                                    <div style="float:left;white-space: nowrap; width: 290px; overflow: hidden;text-overflow: ellipsis;"><%#Eval("SubJobName")%></div>
                                                                    <div style=" float:right; padding-right:10px;">
                                                                        <a href="#" style="font-size:10px; color:blue; text-decoration:italc;" onclick="View_More(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' title='View Job Details..' >More..</a>
                                                                    </div>
                                                                </div>
                                                                <div id="Div2" style="color:Red; clear:both;" runat="server" visible='<%#(Eval("VendorRemarks").ToString().Trim()!="")%>'>
                                                                <%#Eval("VendorRemarks")%>
                                                                </div>
                                                            </td>
                                                            <td style="text-align:center;width:20px;"><asp:ImageButton runat="server" Visible='<%#(Eval("AttachmentName").ToString().Trim()!="")%>' ID="imgDownload" ImageUrl="~/Images/paperclipx12.png" CommandArgument='<%#Eval("DocketSubJobId")%>' OnClick="imgDownload_Click" /></td>
                                                            <td style="text-align:right;width:60px;"><%#Eval("BidQty")%></td>
                                                            <td style="text-align:right;width:60px;"><asp:TextBox runat="server" ID="txtQty" CssClass="number" ReadOnly='<%#!CanEdit%>' onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("SUPQty")%>' MaxLength="8" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>'></asp:TextBox></td>
                                                            <td style="text-align:left;width:100px;"><%#Eval("Unit")%></td>
                                                            <td style="text-align:right;width:100px;"><asp:TextBox runat="server" ID="txtRate" CssClass="number" ReadOnly='<%#!CanEdit%>' onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("SUPUnitPrice_USD")%>' MaxLength="10" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' ></asp:TextBox></td>
                                                            <td style="text-align:right;width:50px;"><asp:TextBox runat="server" ID="txtDisc" CssClass="number" ReadOnly='<%#!CanEdit%>'  onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("SUPDiscountPer")%>' MaxLength="3" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' ></asp:TextBox></td>
                                                            <td style="text-align:right;width:100px;"><asp:Label runat="server" ID="lblNetAmt" Text='<%#Eval("SUPNetAmount_USD")%>' style="text-align:right" ></asp:Label></td>
                                                            <td style="text-align:center;width:20px;"><span runat="server" visible='<%#CanEdit%>'><img title="Remarks" style="cursor:pointer;" onclick="View_Remarks(this);" src="../Images/AddPencil.gif" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' /></span></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            </tr>
                                                    </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                    </table>
                                           </td>
                                           </tr>
                                    </ItemTemplate>       
                                </asp:Repeater>
                            </table>
                <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                <colgroup>
                    <col style="width:150px;" />
                    <col/>
                    <col style="width:20px;" /> 
                    <col style="width:17px;" />
                </colgroup>
            </table>
         </div>
         </td>
         </tr>
         </table>
         <div style="text-align:right; padding:3px; background-color:#ADD6FF; font-weight:bold;">
          <table cellspacing="0" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">                    
          <tr>
            <td style="width:130px; padding-top:3px">Quote Currency : </td>
            <td style="text-align:left;width:120px; ">&nbsp;<asp:DropDownList runat="server" ID="ddlCurrency" Width="100px" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_OnSelectedIndexChanged"></asp:DropDownList></td>
            <td style="width:100px; padding-top:3px">Exch. Rate : </td>
            <td style="padding-top:3px; text-align:left;width:150px; ">&nbsp;
              <asp:Label ID="lblExchRate" ForeColor="Red" runat="server"></asp:Label>
            </td>
            <td style="padding-top:3px; text-align:left;">&nbsp;
              <asp:Label ID="lblMsgMain" ForeColor="Red" runat="server"></asp:Label>
            </td>
            <td style="width:200px; padding-top:3px; text-align:right">
            Total Amount <asp:Label ID="lblCurr" ForeColor="Red" runat="server"></asp:Label> :
            </td>
            <td style="width:100px; padding-top:3px; text-align:left;">
                &nbsp;<asp:Label runat="server" ID="lblTotalAmount"></asp:Label>
            </td>
            <td style="width:60px; padding-top:3px; text-align:left;">
            &nbsp;
            </td>
          </tr>     
          </table>
         </div>
         </td> 
    </tr>
    </table>
    </div>
    <div style="padding:3px; text-align:center">
    <asp:Button runat="server" ID="btnSubmitQuote" OnClientClick="return confirm('Are you sure to submit Quotes? You can not change records once submitted.');" Text="Submit Quote" OnClick="btnSubmitQuote_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:100px;"  />
         </div>               

     <%-- View More --%>
     <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; display:none;" id="dv_ViewMore" runat="server" >
            <center>
            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
            <div style="position: relative; width: 800px; height: 500px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
            <asp:UpdatePanel runat="server" id="UpdatePanel1">
            <ContentTemplate>
            <div style="padding:3px; background-color:orange" >
               <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px; font-weight:bold; font-size:16px;">
               <tr>
                   <td style="text-align:center;"> Job Description </td>
               </tr>
               </table>
                  
            </div>
            <div style="border-bottom:none;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                   <tr> 
                        <td colspan="2"><b>Short Description</b></td>
                    </tr>                    
                   <tr>
                        <td colspan="2" style="text-align:left;">
                         <asp:TextBox ID="txtShortDescr" TextMode="MultiLine" Width="99%" Height="125px" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr> 
                        <td colspan="2"><b>Long Description</b></td>
                    </tr> 
                    <tr>
                        <td colspan="2" style="text-align:left;">
                         <asp:TextBox ID="txtLongDescr" TextMode="MultiLine" Width="99%" Height="175px" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; width:120px; font-weight:bold;">Cost Category :</td>
                        <td style="text-align:left;"><asp:Label ID="lblCostCat" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align:right; width:120px; font-weight:bold;">Outside Repair :</td>
                        <td style="text-align:left;"><asp:Label ID="lblOutsideRepair" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                         <td colspan="2">
                            <div style="text-align:center; padding:5px;">
                                    <div style="text-align:center;float:right">
                                        <asp:Button runat="server" ID="btnClose_Descr" Text="Close" OnClientClick="return Close_Descr();" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                    </div>
                            </div>
                         </td>
                    </tr>
                </table>
             </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            </center>
            </div>
     <%-- Update Remarks --%>
     <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; display:none;" id="dv_Remarks" runat="server" >
            <center>
            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
            <div style="position: relative; width: 800px; height: 500px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
            <asp:UpdatePanel runat="server" id="UpdatePanel2">
            <ContentTemplate>
            <div style="padding:3px; background-color:orange" >
               <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px; font-weight:bold; font-size:16px;">
               <tr>
                   <td style="text-align:center;"> Remarks </td>
               </tr>
               </table>
                <asp:HiddenField ID="hfdRFQId" runat="server" />
                <asp:HiddenField ID="hfdDocketId" runat="server" />
                <asp:HiddenField ID="hfdDocketJobId" runat="server" />
                <asp:HiddenField ID="hfdDocketSubJobId" runat="server" /> 
            </div>
            <div style="border-bottom:none;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                  <tr><td><b>Supdt Remarks</b></td></tr> 
                  <tr>
                        <td style="text-align:left;">
                         <asp:TextBox ID="txtSUPRemarks" TextMode="MultiLine" Width="99%" Height="195px" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                   <tr><td><b>Vendor Remarks</b></td></tr>                    
                   <tr>
                        <td style="text-align:left;">
                         <asp:TextBox ID="txtVendorRemarks" TextMode="MultiLine" Width="99%" Height="160px" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td>
                            <div style="text-align:center; padding:5px;">
                                   <div style="text-align:left;float:left; padding-top:5px;">
                                        <asp:Label ID="lbl_MsgRemarks" ForeColor="Red" runat="server"></asp:Label>
                                   </div>
                                    <div style="text-align:center;float:right">
                                        <asp:Button runat="server" ID="btn_UpdateRemarks" Text="Save" OnClick="btn_UpdateRemarks_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;"  />
                                        <asp:Button runat="server" ID="Button1" Text="Close" OnClientClick="return Close_Remarks();" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                    </div>
                            </div>
                         </td>
                    </tr>
                </table>
             </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_UpdateRemarks" />
            </Triggers>
            </asp:UpdatePanel>
            </div>
            </center>
            </div>


    <script type="text/javascript">
        
        function Update_Values(txt) {            
            var RFQId=$(txt).attr("RFQId");
            var DocketId=$(txt).attr("DocketId");
            var DocketJobId=$(txt).attr("DocketJobId");
            var DocketSubJobId=$(txt).attr("DocketSubJobId");            

            var QuoteQty = $(txt).parent().parent().find("[id$='txtQty']").val();
            var Disc = $(txt).parent().parent().find("[id$='txtDisc']").val();
            var Rate = $(txt).parent().parent().find("[id$='txtRate']").val();
            var lblNetAmt = $(txt).parent().parent().find("[id$='lblNetAmt']");

            $.post('UpdateQuote.ashx', {
                Type: "SUPQuoteQty",
                RFQId: RFQId,
                DocketId: DocketId,
                DocketJobId: DocketJobId,
                DocketSubJobId: DocketSubJobId,
                QuoteQty: QuoteQty,
                Disc: Disc,
                Rate: Rate
            },             
            function (data, status) {
                var arr = data.split(',');
                $(lblNetAmt).html(arr[0]);
                $("#lblTotalAmount").html(arr[1]);
            }
            );
        }            

    </script>

    <script type="text/javascript">

        function View_More(txt) {            
            var RFQId = $(txt).attr("RFQId");
            var DocketId = $(txt).attr("DocketId");
            var DocketJobId = $(txt).attr("DocketJobId");
            var DocketSubJobId = $(txt).attr("DocketSubJobId");

            $.post('UpdateQuote.ashx', {
                Type: "ViewJobDescr",
                RFQId: RFQId,
                DocketId: DocketId,
                DocketJobId: DocketJobId,
                DocketSubJobId: DocketSubJobId
            },
            function (data, status) {
                var sd = $.parseJSON(data).sd;
                sd = sd.split("#NEWLINE#").join("\r\n");

                var ld = $.parseJSON(data).ld;
                ld = ld.split("#NEWLINE#").join("\r\n");               
                    
                $("#txtShortDescr").val(sd);
                $("#txtLongDescr").val(ld);
                $("#lblCostCat").html($.parseJSON(data).cc);
                $("#lblOutsideRepair").html($.parseJSON(data).osr);

                $("#dv_ViewMore").show();
            }
            );

        }

        function Close_Descr() {
            $("#dv_ViewMore").hide();
            return false;
        }
        </script>  

        <script type="text/javascript">

            function View_Remarks(txt) {
                var RFQId = $(txt).attr("RFQId");
                var DocketId = $(txt).attr("DocketId");
                var DocketJobId = $(txt).attr("DocketJobId");
                var DocketSubJobId = $(txt).attr("DocketSubJobId");

                $("#hfdRFQId").val(RFQId);
                $("#hfdDocketId").val(DocketId);
                $("#hfdDocketJobId").val(DocketJobId);
                $("#hfdDocketSubJobId").val(DocketSubJobId);

                $.post('UpdateQuote.ashx', {
                    Type: "SUPRemarks",
                    RFQId: RFQId,
                    DocketId: DocketId,
                    DocketJobId: DocketJobId,
                    DocketSubJobId: DocketSubJobId
                },
            function (data, status) {
                var arr = data.split('^');
                $("#txtSUPRemarks").val(arr[0]);
                $("#txtVendorRemarks").val(arr[1]);
                $("#dv_Remarks").show();
            }
            );

            }

            function Close_Remarks() {     
                $("#dv_Remarks").hide();
                return false;
            }
        </script>  
    </form>
        
</body>
</html>
