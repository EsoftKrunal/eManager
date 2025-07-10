<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Docket_Quote.aspx.cs" Inherits="Docket_Quote" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="../../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <title>EMANAGER </title>
    <script src="../../JS/JQuery.js" type="text/javascript"></script>
    <script src="../../JS/Common.js" type="text/javascript"></script>
    <script src="../../JS/JQScript.js" type="text/javascript"></script>
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
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <asp:Panel ID="pnl_Password" runat="server" >
       
       <div style="width :100%; height :300px; text-align :center; padding-top :150px; font-family:Calibri; font-size:14px;">
        <br /><br />
        <center>

        <div style="font-size:20px; padding:3px;"></div>
        <div style="width:400px; border:solid 1px #999999">
        <div style=" color : Black; padding:10px; background-color:#999999; color:White; text-align:center; font-size:16px;" >Enter your password to access the Quote</div>
        <div style="width:350px;text-align :center; padding-top :20px; ">
        <div>
        <div style=""><asp:TextBox runat="server" ID="Password" TextMode="Password" MaxLength ="15" style="padding:5px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvpw" runat="server" ControlToValidate="Password" SetFocusOnError="true"  ErrorMessage="*" ForeColor="Red" ValidationGroup="pw"  Display="Dynamic"/>
        </div>
        </div> 
        <br />
        <div>
            <asp:Button runat="server" ID="btnSubmit" Text="Submit" style=" padding:3px; border:none; color:White; background-color:#999999; width:100px;"  onclick="btnSubmit_Click" ValidationGroup="pw" />
        </div>
        <div style="padding:8px"     >
              <asp:Label runat="server" id="Message" ForeColor="Red" ></asp:Label>
  
        </div>
        
        </div> 
        </div>
        <div style="color:Red; padding:10px;">
             Note: If you have not received your password please send request to it.singapore@mtmsm.com with your full contact details and ship name.      
        </div>
        </center>
        </div> 
    </asp:Panel>
    <asp:Panel ID="pnl_Main" runat="server" Visible="false">
    <div>
    <div style=" text-align:center; padding:3px; font-size:15px; " class="text headerband">
        <b>RFQ - 
        <asp:Label runat="server" ID="lblRFQNo"></asp:Label>
        </b></div>
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="1">
        <tr><td>
        <div style="border:none; padding:5px; font-size:13px; ">
            <table style="width :100%;">
            <tr>
            <td style="text-align:right; ">DD # :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblDocketNo"></asp:Label></td>
            <%--<td>
                <span style="color:Red;">Download in PDF</span>&nbsp;<asp:ImageButton id="btnDocketView" runat="server" ImageUrl="~/Images/paperclipx12.png" OnClick="btnDocketView_Click" />
            </td>
            <td>
                <span style="color:Red;">Download SOR</span>&nbsp;<asp:ImageButton id="btnDownloadSOR" runat="server" ImageUrl="~/Images/paperclipx12.png" OnClick="btnDownloadSOR_Click" />
            </td>--%>
            <td style="text-align:right">Vessel :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblVessel"></asp:Label></td>
            <td style="text-align:right">Type :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblType"></asp:Label></td>
            <td style="text-align:right">Plan Duration :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblPlanDuration"></asp:Label></td>
            </tr>
            <tr>
            <td style="text-align:right">Docking Category :</td>
            <td style="text-align:left" colspan="9">
            <div style="text-align:left"> 
                <asp:DropDownList runat="server" ID="ddlCat" style="padding:3px;" width="500px" AutoPostBack="true" OnSelectedIndexChanged="ddlCat_OnSelectedIndexChanged"></asp:DropDownList>
                <span style='color:Red'>&nbsp;Click here to change the category</span>
            </div>
            </td>
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
         <td style="padding:3px;">
         <b>Job Details : <span style='color:Red'>&nbsp;Please submit quote for all the items and to enter remarks click on the pencil icon in the right side of the screen.</span></b>
         </td>
         </tr>
         <tr>
         <td >
         <div style="border:solid 1px gray; text-align:left;font-size :11px;height:25px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="Div1">
         <table cellspacing="0" rules="all" border="0" cellpadding="2" style="width:100%;border-collapse:collapse; height:25px; vertical-align:middle; ">
                    <tr class= "headerstylegrid">
                        <td style="text-align:left;width:130px; vertical-align:middle;">SR#</td>
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
                                           <td align="left" style="padding:5px;"><div style="height:14px"><b><%#Eval("JobName")%></b>&nbsp;&nbsp;&nbsp;
                                           <a href="#" style="font-size:10px; color:blue; text-decoration:italc;" onclick="View_JobDesc(this);" RFQId='<%#Eval("RFQId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' title='View Job Description..' runat="server" visible='<%#(Eval("JobDesc").ToString().Trim()!="")%>' >More..</a></div></td>
                                           <td>&nbsp;</td>
                                           </tr>
                                           <tr style=''>
                                           <td colspan="3" style="background-color:#E0F5FF;border-bottom:solid 1px white;">
                                                    <table cellspacing="0" rules="all" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;">
                                                    <asp:Repeater ID="rptSubJobs" runat="server" DataSource='<%#BindSubJobs(Eval("DocketJobId"))%>'>
                                                       <ItemTemplate>
                                                            <tr style='background-color:white;'>
                                                            <td style="text-align:left;width:130px;"><b><%#Eval("SubJobCode")%></b>
                                                            <div id="Div3" style="color:Red; clear:both;" runat="server" visible='<%#(Eval("VendorRemarks").ToString().Trim()!="")%>'>
                                                                Vendor Remarks :
                                                            </div>
                                                            </td>
                                                            <td align="left" >
                                                                <div>
                                                                    <div style="float:left;white-space: nowrap; width: 290px; overflow: hidden;text-overflow: ellipsis;"><%#Eval("SubJobName")%></div>
                                                                    <div style=" float:left; padding-right:10px;">
                                                                        <a href="#" style="font-size:10px; color:blue; text-decoration:italc;" onclick="View_More(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' title='View Job Details..' runat="server" visible='<%#(Eval("LongDescr").ToString().Trim()!="")%>'>More..</a>
                                                                    </div>
                                                                </div>
                                                                <div style="color:Red; clear:both;" runat="server" visible='<%#(Eval("VendorRemarks").ToString().Trim()!="")%>'>
                                                                <%#Eval("VendorRemarks")%>
                                                                </div>
                                                            </td>
                                                            <td style="text-align:center;width:30px;"><asp:ImageButton runat="server" Visible='<%#(Eval("AttachmentName").ToString().Trim()!="")%>' ID="imgDownload" ImageUrl="~/Modules/PMS/Images/paperclipx12.png" CommandArgument='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' OnClick="imgDownload_Click" /></td>
                                                            <td style="text-align:right;width:60px;"><%#Eval("BidQty")%></td>
                                                            <td style="text-align:right;width:60px;"><asp:TextBox runat="server" ID="txtQty" ReadOnly='<%#!CanEdit%>' CssClass="number" onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("QuoteQty")%>' MaxLength="8" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>'></asp:TextBox></td>
                                                            <td style="text-align:left;width:100px;"><%#Eval("Unit")%></td>
                                                            <td style="text-align:right;width:100px;"><asp:TextBox runat="server" ID="txtRate" ReadOnly='<%#!CanEdit%>' CssClass="number" onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("UnitPrice")%>' MaxLength="10" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' ></asp:TextBox></td>
                                                            <td style="text-align:right;width:50px;"><asp:TextBox runat="server" ID="txtDisc" ReadOnly='<%#!CanEdit%>' CssClass="number" onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("DiscountPer")%>' MaxLength="3" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' ></asp:TextBox></td>
                                                            <td style="text-align:right;width:100px;"><asp:Label runat="server" ID="lblNetAmt" Text='<%#Eval("NetAmount")%>' style="text-align:right" ></asp:Label></td>
                                                            <td style="text-align:center;width:20px;"><span id="Span1" runat="server" visible='<%#CanEdit%>'><img title="Remarks" style="cursor:pointer;" onclick="View_Remarks(this);" src="../../Images/AddPencil.gif" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' /></span></td>
                                                            <td style="width:20px;">&nbsp;</td>
                                                            </tr>
                                                    </ItemTemplate>       
                                                       <AlternatingItemTemplate>
                                                            <tr style='background-color:#FFF5E6'>
                                                           <td style="text-align:left;width:130px;"><b><%#Eval("SubJobCode")%></b>
                                                            <div id="Div3" style="color:Red; clear:both;" runat="server" visible='<%#(Eval("VendorRemarks").ToString().Trim()!="")%>'>
                                                                Vendor Remarks :
                                                            </div>
                                                            </td>
                                                            <td align="left" >
                                                                <div>
                                                                    <div style="float:left;white-space: nowrap; width: 290px; overflow: hidden;text-overflow: ellipsis;"><%#Eval("SubJobName")%></div>
                                                                    <div style=" float:left; padding-right:10px;">
                                                                        <a id="A1" href="#" style="font-size:10px; color:blue; text-decoration:italc;" onclick="View_More(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' title='View Job Details..' runat="server" visible='<%#(Eval("LongDescr").ToString().Trim()!="")%>'>More..</a>
                                                                    </div>
                                                                </div>
                                                                <div id="Div2" style="color:Red; clear:both;" runat="server" visible='<%#(Eval("VendorRemarks").ToString().Trim()!="")%>'>
                                                                <%#Eval("VendorRemarks")%>
                                                                </div>
                                                            </td>
                                                            <td style="text-align:center;width:30px;"><asp:ImageButton runat="server" Visible='<%#(Eval("AttachmentName").ToString().Trim()!="")%>' ID="imgDownload" ImageUrl="~/Modules/PMS/Images/paperclipx12.png" CommandArgument='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' OnClick="imgDownload_Click" /></td>
                                                            <td style="text-align:right;width:60px;"><%#Eval("BidQty")%></td>
                                                            <td style="text-align:right;width:60px;"><asp:TextBox runat="server" ID="txtQty" CssClass="number" ReadOnly='<%#!CanEdit%>' onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("QuoteQty")%>' MaxLength="8" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>'></asp:TextBox></td>
                                                            <td style="text-align:left;width:100px;"><%#Eval("Unit")%></td>
                                                            <td style="text-align:right;width:100px;"><asp:TextBox runat="server" ID="txtRate" CssClass="number" ReadOnly='<%#!CanEdit%>' onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("UnitPrice")%>' MaxLength="10" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' ></asp:TextBox></td>
                                                            <td style="text-align:right;width:50px;"><asp:TextBox runat="server" ID="txtDisc" CssClass="number" ReadOnly='<%#!CanEdit%>'  onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("DiscountPer")%>' MaxLength="3" onchange="Update_Values(this);" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' ></asp:TextBox></td>
                                                            <td style="text-align:right;width:100px;"><asp:Label runat="server" ID="lblNetAmt" Text='<%#Eval("NetAmount")%>' style="text-align:right" ></asp:Label></td>
                                                            <td style="text-align:center;width:20px;"><span runat="server" visible='<%#CanEdit%>'><img title="Remarks" style="cursor:pointer;" onclick="View_Remarks(this);" src="../../Images/AddPencil.gif" RFQId='<%#Eval("RFQId")%>' DocketSubJobId='<%#Eval("DocketSubJobId")%>' DocketJobId='<%#Eval("DocketJobId")%>' DocketId='<%#Eval("DocketId")%>' /></span></td>
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
            <td style="width:110px; padding-top:3px">Quote Currency : </td>
            <td style="text-align:left;width:70px; ">&nbsp;<asp:DropDownList runat="server" ID="ddlCurrency" Width="60px" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_OnSelectedIndexChanged"></asp:DropDownList></td>
            <td style="width:80px; padding-top:3px">Exch. Rate : </td>
            <td style="padding-top:3px; text-align:left;width:100px; ">&nbsp;
              <asp:Label ID="lblExchRate" ForeColor="Red" runat="server"></asp:Label>
            </td>
            <td style="padding-top:3px; text-align:left;">&nbsp;
              <asp:Label ID="lblMsgMain" ForeColor="Red" runat="server"></asp:Label>
            </td>

            <td style="width:170px; padding-top:3px; text-align:right">
                &nbsp;</td>
            <td style="width:100px; padding-top:3px; text-align:left;">
                &nbsp;</td>

            <td style="width:190px; padding-top:3px; text-align:right">
                &nbsp;</td>
            <td style="width:100px; padding-top:3px; text-align:left;">
                &nbsp;</td>


            <td style="width:150px; padding-top:3px; text-align:right">
            Total Amount <asp:Label ID="lblCurr" ForeColor="Red" runat="server"></asp:Label> :
            </td>
            <td style="width:100px; padding-top:3px; text-align:left;">
                <asp:LinkButton runat="server" OnClick="TotalYardCostPrint" ID="lblTotalYardCost" style="text-decoration:none;"></asp:LinkButton>
            </td>
            <td style="width:20px; padding-top:3px; text-align:left;">
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
    <asp:Button runat="server" ID="btnSubmitQuote" OnClientClick="return confirm('Are you sure to submit Quotes? You can not change records once submitted.');" Text="Submit Quote" OnClick="btnSubmitQuote_Click" style=" padding:3px; border:none;  width:100px;" CssClass="btn"  />
    <br />
     <span style='color:Red'>&nbsp; ( Complete the quotation for all items before submitting the quote.  )</span>
         </div>               
    </asp:Panel>

     <%-- View More --%>
     <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; display:none;" id="dv_ViewMore" runat="server" >
            <center>
            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
            <div style="position: relative; width: 800px; height: 500px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
            <asp:UpdatePanel runat="server" id="UpdatePanel1">
            <ContentTemplate>
            <div style="padding:3px; " class="text headerband" >
               <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px; font-weight:bold; font-size:16px;">
               <tr>
                   <td style="text-align:center;"> Job Description </td>
               </tr>
               </table>
                  
            </div>
            <div style="border-bottom:none;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                   <tr> 
                        <td><b>Short Description</b></td>
                    </tr>                    
                   <tr>
                        <td style="text-align:left;">
                         <asp:TextBox ID="txtShortDescr" TextMode="MultiLine" Width="99%" Height="135px" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr> 
                        <td><b>Long Description</b></td>
                    </tr> 
                    <tr>
                        <td style="text-align:left;">
                         <asp:TextBox ID="txtLongDescr" TextMode="MultiLine" Width="99%" Height="190px" runat="server" ReadOnly="true" ></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                    <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <tr>
                        <td style="text-align:right; width:120px; font-weight:bold; ">Cost Category :</td>
                        <td style="text-align:left; width:200px; " >
                            <asp:RadioButton ID="rdoYardCost" Text="Yard Cost" runat="server" GroupName="CC" Checked="true" />
                            <asp:RadioButton ID="rdoNonYardCost" Text="Non Yard Cost" runat="server" GroupName="CC" />
                        </td>
                        <td style="text-align:right; width:130px; font-weight:bold;">Outside Repair :</td>
                        <td style="text-align:left; width:50px; ">
                            <asp:CheckBox ID="chkOutsideRepair" runat="server"  />                                                       
                        </td>
                        <td style="text-align:right; width:200px; font-weight:bold; ">Required For Job Tracking :</td>
                        <td style="text-align:left; ">
                            <asp:Label ID="lblReqJT" runat="server"  />                                                       
                        </td>
                    </tr>
                    <tr>
                         <td colspan="4">
                            <div style="text-align:center; padding:5px;">
                                    <div style="text-align:center;float:right">
                                        <asp:Button runat="server" ID="btn_UpdateSubJob" Text="Save" OnClick="btn_UpdateSubJob_Click" style=" padding:3px; border:none;  width:80px;"  CssClass="btn" />
                                        <asp:Button runat="server" ID="btnClose_Descr" Text="Close" OnClientClick="return Close_Descr();" style=" padding:3px; border:none;  width:80px;" CssClass="btn"   />
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
            <div style="padding:3px; " class="text headerband" >
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
                   <tr>
                        <td style="text-align:left;">
                         <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="99%" Height="410px" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td>
                            <div style="text-align:center; padding:5px;">
                                   <div style="text-align:left;float:left; padding-top:5px;">
                                        <asp:Label ID="lbl_MsgRemarks" ForeColor="Red" runat="server"></asp:Label>
                                   </div>
                                    <div style="text-align:center;float:right">
                                        <asp:Button runat="server" ID="btn_UpdateRemarks" Text="Save" OnClick="btn_UpdateRemarks_Click" style=" padding:3px; border:none;  width:80px;" CssClass="btn" />
                                        <asp:Button runat="server" ID="Button1" Text="Close" OnClientClick="return Close_Remarks();" style=" padding:3px; border:none;  width:80px;" CssClass="btn"  />
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
     <%-- View Job Descr --%>
     <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; display:none;" id="dv_ViewJobDesc" runat="server" >
            <center>
            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
            <div style="position: relative; width: 800px; height: 500px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
            <asp:UpdatePanel runat="server" id="UpdatePanel3">
            <ContentTemplate>
            <div style="padding:3px; " class="text headerband" >
               <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px; font-weight:bold; font-size:16px;">
               <tr>
                   <td style="text-align:center;"> Job Description </td>
               </tr>
               </table>
            </div>
            <div style="border-bottom:none;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">                    
                   <tr>
                        <td style="text-align:left;">
                         <asp:TextBox ID="txtJobDesc" TextMode="MultiLine" Width="99%" Height="410px" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         <td>
                            <div style="text-align:center; padding:5px;">                                   
                                    <div style="text-align:center;float:right">
                                        <asp:Button runat="server" ID="btnClose_ViewJobDesc" Text="Close" OnClientClick="return Close_ViewDesc();" style=" padding:3px; border:none;  width:80px;" CssClass="btn" />
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
                Type: "QuoteQty",
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
                                        $("#lblTotalYardCost").html(arr[2]);
                                        $("#lblTotalNonYardCost").html(arr[3]);
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

            $("#hfdRFQId").val(RFQId);
            $("#hfdDocketId").val(DocketId);
            $("#hfdDocketJobId").val(DocketJobId);
            $("#hfdDocketSubJobId").val(DocketSubJobId);

            $.post('UpdateQuote.ashx', {
                Type: "ViewSubJobDescr",
                RFQId: RFQId,
                DocketId: DocketId,
                DocketJobId: DocketJobId,
                DocketSubJobId: DocketSubJobId
            },
            function (data, status) {
                var sd = $.parseJSON(data).sd;
                sd = sd.split("#NEWLINE#").join("\r\n");

                var JT = $.parseJSON(data).JT;
                JT = JT.split("#NEWLINE#").join("\r\n");
                
                var ld = $.parseJSON(data).ld;
                ld = ld.split("#NEWLINE#").join("\r\n");

                $("#txtShortDescr").val(sd);
                $("#txtLongDescr").val(ld);

                if ($.parseJSON(data).cc == "Yard Cost")
                    $("#rdoYardCost").prop("checked", true);
                else
                    $("#rdoNonYardCost").prop("checked", true);

                if ($.parseJSON(data).osr == "Yes")
                    $("#chkOutsideRepair").prop("checked", true);
                else
                    $("#chkOutsideRepair").prop("checked", false);

                $("#lblReqJT").text(JT);

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
                    Type: "ViewRemarks",
                    RFQId: RFQId,
                    DocketId: DocketId,
                    DocketJobId: DocketJobId,
                    DocketSubJobId: DocketSubJobId
                },
            function (data, status) {
                $("#txtRemarks").val(data);
                $("#dv_Remarks").show();
            }
            );

            }

            function Close_Remarks() {     
                $("#dv_Remarks").hide();
                return false;
            }
        </script>
        
          

    <script type="text/javascript">

        function View_JobDesc(txt) {
            var RFQId = $(txt).attr("RFQId");
            var DocketId = $(txt).attr("DocketId");
            var DocketJobId = $(txt).attr("DocketJobId");
            //var DocketSubJobId = $(txt).attr("DocketSubJobId");            

            $.post('UpdateQuote.ashx', {
                Type: "ViewJobDescr",
                RFQId: RFQId,
                DocketId: DocketId,
                DocketJobId: DocketJobId
            },
            function (data, status) {
                $("#txtJobDesc").val(data);
                $("#dv_ViewJobDesc").show();
            }
            );
        }

        function Close_ViewDesc() {
            $("#dv_ViewJobDesc").hide();
            return false;
        }
        </script>  

    </form>
        
</body>
</html>
