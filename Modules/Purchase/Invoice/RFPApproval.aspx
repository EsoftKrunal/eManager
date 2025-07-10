<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RFPApproval.aspx.cs" Inherits="Modules_Purchase_Invoice_RFPApproval" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
     <script src="JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="JS/AutoComplete/jquery-ui.css" />
     <script src="JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
    
  <script type="text/javascript" src="JS/KPIScript.js"></script>
    <style type="text/css">
    body
    {
        font-family:Verdana;
        font-size:12px;
        margin:0px;
    }
    
        .input_box
        {}
    
        .required_box
        {}
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div >
             <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
    <div id="log" style="display:none"></div>
    <div>
    <center>
    <div style="border:solid 1px #008AE6; ">
    <asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>
        <table cellpadding="6" cellspacing="0" width="100%">
         <tr>
         <td class="text headerband">
             <strong>RFP Approval</strong>

         </td>
         </tr>
           <tr>
               <td style=" background-color:#FFFFCC">
               &nbsp;
                 <asp:Label ID="lbl_inv_Message" runat="server" ForeColor="#C00000"></asp:Label>
               </td>
           </tr>
         <tr>
           <td>
              <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                    <tr>
                      <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                               Owner :</td>
                           <td style="text-align: left; ">
                                <asp:Label runat="server" ID="lblOwnerName" ></asp:Label>
                               <asp:Label runat="server" ID="lblOwnerCode" Visible="false"></asp:Label>
                           </td>
                           <td style="text-align: right;">
                               PV No#</td>
                           <td style="text-align: left;width:500px;">
                               <asp:Label runat="server" ID="lblpvno"></asp:Label></td>
                      </tr>
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Vendor:</td>
                          <td style="text-align: left; ">
                              <asp:Label ID="lblVendorName" runat="server" CssClass="input_box" ></asp:Label>
                              <asp:HiddenField runat="server" id="hfdSupplier" />
                          </td>
                          <td style="text-align: right;">
                              Currency:</td>
                          <td style="text-align: left;width:500px;">
                              <asp:Label ID="lblCurrency" runat="server" CssClass="input_box"></asp:Label>
                          </td>
                      </tr>
                   <tr>
                           <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                               Payment Mode :</td>
                           <td style="text-align: left; ">
                              <asp:Label ID="lblPaymentMode" runat="server"></asp:Label>
                           </td>
                           <td style="text-align: right;">
                               &nbsp;</td>
                           <td style="text-align: left;width:500px;">
                               &nbsp;</td>
                      </tr>
                  <tr style="background-color:#E6F3FC">
                      <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                          Submitted By :
                      </td>
                      <td style="text-align: left; ">
                          <asp:Label ID="lblApprovalSubmittedBy" runat="server"></asp:Label>
                         
                      </td>
                      <td style="text-align: right;">
                          Submitted On :
                      </td>
                      <td style="text-align: left;width:500px;">
                           <asp:Label ID="lblApprovalSubmittedOn" runat="server"></asp:Label>
                      </td>
                  </tr>
                       
                       <tr >
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Approval Comments :</td>
                          <td style="text-align: left;" colspan="3">
                              <asp:Label ID="lblApprovalSubmittedComments" runat="server"  Height="50px" 
                                   Width="80%"></asp:Label>
                             
                              </td>
                           
                      </tr>
                  
                     </table>
                     
                     <div class="table-responsive" style="HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:25px;"/>
                                                <col style=""/>
                                                <col style="width:200px;" />
                                                <col style="width:200px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <%--<col style="width:50px;text-align:center;" />--%>
                                                <col style="width:25px;"/>
                                                <tr class= "headerstylegrid" >
                                                    <td></td>
                                                    <td>Ref #</td>
                                                    <td>Inv #</td>
                                                    <td>Inv Dt.</td>
                                                    <td>Inv Amt</td>
                                                    <td>Approval Amt</td>
                                                    <%--<td></td>--%>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>
                    <div id="divPayment" runat="server" class="table-responsive" style="HEIGHT: 150px; text-align:center;">
                                            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                                                <colgroup>
                                                <col style="width:25px;"/>
                                                <col style=""/>
                                                <col style="width:200px;" />
                                                <col style="width:200px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <%--<col style="width:50px;text-align:center;" />--%>
                                                <col style="width:25px;"/>
                                                </colgroup>
                                                <asp:Repeater ID="RptInvoices" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td align="left"><a href="ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>" target="_blank" > <%#Eval("RefNo")%></a>
                                                                <asp:HiddenField ID="hfdInvId" runat="server" Value='<%#Eval("InvoiceId")%>' />
                                                            </td>                                                                                                                       
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="right"><%#Eval("ApprovalAmount")%></td>
                                                            <%-- <td>
                                                                  <asp:ImageButton runat="server" CommandArgument='<%#Eval("InvoiceId")%>' Visible='<%#(lblpvno.Text.Trim()=="")%>' OnClientClick="return window.confirm('Are you sure to delete this record?');" ID="btnDelRow" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnDeleteRow_Click" CausesValidation="false" />
                                                            </td> --%>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                       <tr style="background-color:#E6F3FC">
                                                            <td></td>
                                                            <td align="left"><a href="ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>" target="_blank" > <%#Eval("RefNo")%></a>
                                                                <asp:HiddenField ID="hfdInvId" runat="server" Value='<%#Eval("InvoiceId")%>' />
                                                            </td>                                                                                                                       
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="right"><%#Eval("ApprovalAmount")%></td>
                                                            <%--<td>
                                                                  <asp:ImageButton runat="server" CommandArgument='<%#Eval("InvoiceId")%>' Visible='<%#(lblpvno.Text.Trim()=="")%>' OnClientClick="return window.confirm('Are you sure to delete this record?');" ID="btnDelRow" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnDeleteRow_Click" CausesValidation="false" />
                                                            </td> --%>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:Repeater>
                                             </table>
                                        </div>
                                        <div class="table-responsive" style="HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:25px;"/>
                                                <col style=""/>
                                                <col style="width:200px;" />
                                                <col style="width:200px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:50px;text-align:center;" />
                                                <col style="width:25px;"/>
                                                <tr class= "headerstylegrid" >
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>Total:</td>
                                                    <td align="right"><asp:Label ID="lbltotal" runat="server"></asp:Label></td>
                                                    <td></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>
                <br />
              <table  cellpadding="0" cellspacing="0" width="100%">
                <tr>
                  <td style="">
                      <asp:Button ID="btnApprove" runat="server" Text="Approve"  style="  border:none; padding:4px;" CssClass="btn" OnClick="btnApprove_Click" /> &nbsp;
                     <asp:Button ID="btnBacktoStage1" runat="server" Text="Reject"  style="  border:none; padding:4px;" CssClass="btn" OnClick="btnBacktoStage1_Click" /> &nbsp;
                      <asp:Button ID="btnClose" runat="server" Text="Close" Width="80px" OnClientClick="RefreshParent();" style="  border:none; padding:4px;" CssClass="btn"  />
                     
                  </td>
                </tr>
              </table>
           </td>
         </tr>
       </table>

                   
    
       </ContentTemplate>
       <Triggers>
       <asp:PostBackTrigger ControlID="btnApprove"  />
       <asp:PostBackTrigger ControlID="btnBacktoStage1"  />
       

       </Triggers>
     </asp:UpdatePanel>

              <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="dvRFPApprove" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:650px;padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">
                <center>
                    <div style=" float :right " >
                        <asp:ImageButton runat="server" ID="btnApproveRFPRequest_ClosePopup" ImageUrl="~/Modules/HRD/Images/close.gif"  ToolTip="Close this Window." OnClick="btnApproveRFPRequest_ClosePopup_Click" /> </div>
                    <div style="font-size:15px;padding:6px;font-weight:bold;" class="text headerband">
                        RFP Approval [ PV No : <asp:Label ID="lblRFPPVNo" runat="server"></asp:Label>]
                       
                    </div>  
                    <table border="0" cellpadding="6" cellspacing="0" style="height: 100px; text-align: left; border-collapse:collapse; width:100%;">
                                <tr>
                                    <td>
                                        <b> Remarks :</b>
                                    </td>
                                </tr>
                                   <tr >
                                          <td style="text-align: left;">
                                              <asp:TextBox ID="txtRFPApprovalRemarks" runat="server"  Width="90%" TextMode="MultiLine" Height="120px" MaxLength="500"></asp:TextBox>
                                               &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                  ControlToValidate="txtRFPApprovalRemarks" Display="Dynamic" ErrorMessage="*" ValidationGroup="RFPA"></asp:RequiredFieldValidator>
                                          </td>
                                   </tr>
                                             
                                    <tr >
                                          <td align="right" style="text-align: right; padding-right:15px; width:100px;" >
                                              
                                            
                                        </td>
                                    </tr>
                                  </table>    
                    <%------------------------------------------------------------------------------%>
                    

                    <div style="text-align:center;padding:5px;">
                        <asp:Button ID="btnRFPApprove" runat="server" Text="Approve" Width="80px" ValidationGroup="RFPA" style="  border:none; padding:4px;" CssClass="btn" OnClick="btnRFPApprove_Click" /> &nbsp;
                        <asp:Button ID="btnCloseRFPApprove" runat="server" Text="Close" CssClass="btn" Width="80px" style="  border:none; padding:4px;" ToolTip="Close this Window." OnClick="btnCloseRFPApprove_Click" />
                    </div>
                    <div style="text-align:center;padding:5px;background-color:#e5e5e5">
                        <asp:Label ID="lblMsgRFPApprove" runat="server" CssClass="error"></asp:Label>
                    </div>
                    </center>    
                    </div>
                    </center>
                        </div>
              <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="dvSendBackToApproval" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:650px;padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)">
                <center>
                    <div style=" float :right " >
                        <asp:ImageButton runat="server" ID="btnSendBackRFP_ClosePopup_Click" ImageUrl="~/Modules/HRD/Images/close.gif"  ToolTip="Close this Window." OnClick="btnSendBackRFP_ClosePopup_Click_Click" /> </div>
                    <div style="font-size:15px;padding:6px;font-weight:bold;" class="text headerband">
                        RFP Request Reject [ PV No : <asp:Label ID="lblSendBackRFPPVNo" runat="server"></asp:Label>]
                        
                    </div>  
                    <table border="0" cellpadding="6" cellspacing="0" style="height: 100px; text-align: left; border-collapse:collapse; width:100%;">
                                <tr>
                                    <td>
                                        <b> Remarks :</b>
                                    </td>
                                </tr>
                                   <tr >
                                          <td style="text-align: left;">
                                              <asp:TextBox ID="txtRFPSendBackComments" runat="server"  Width="90%" TextMode="MultiLine" Height="120px" MaxLength="500"></asp:TextBox>
                                               &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                  ControlToValidate="txtRFPSendBackComments" Display="Dynamic" ErrorMessage="*" ValidationGroup="RFPSB"></asp:RequiredFieldValidator>
                                          </td>
                                   </tr>
                                             
                                    <tr >
                                          <td align="right" style="text-align: right; padding-right:15px; width:100px;" >
                                              
                                            
                                        </td>
                                    </tr>
                                  </table>    
                    <%------------------------------------------------------------------------------%>
                    

                    <div style="text-align:center;padding:5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="80px" ValidationGroup="RFPSB" style="  border:none; padding:4px;" CssClass="btn" OnClick="btnSave_Click"  /> &nbsp;
                        <asp:Button ID="btnSendBackClose" runat="server" Text="Close" CssClass="btn" Width="80px" style="  border:none; padding:4px;" ToolTip="Close this Window." OnClick="btnSendBackClose_Click" />
                    </div>
                    <div style="text-align:center;padding:5px;background-color:#e5e5e5">
                        <asp:Label ID="lblMsgSendBackRFP" runat="server" CssClass="error"></asp:Label>
                    </div>
                    </center>    
                    </div>
                    </center>
                        </div>

    </div>
    </center>
    </div>
        </div>
    </form>
</body>
    <script type="text/javascript">
    //    function Refresh_RFPRequestList() {

            
    //        alert('Hi');
    //        this.window.opener.re
    //       // window.opener.document.getElementById('btn_Search').click();
    //        this.window.opener.document.getElementById('btn_Search').click;
    //      //  window.opener.document.getElementById('ctl00_ContentMainMaster_btnSearch').click();
    //}
        function RefreshParent() {
            console.log("Windows ", window);
            window.opener.refreshParent();
            
            window.close();
        }
    </script>
</html>
