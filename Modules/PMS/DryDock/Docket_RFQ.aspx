<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Docket_RFQ.aspx.cs" Inherits="Docket_RFQ" Async="true" ValidateRequest="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <title>Planned Maintenance System </title>
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/JQScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CopyData() {
            $("#txtMailText").val($("#dv_mailcontent").html());
        }

    </script>
    <link href="../../../css/app_style.css" rel="stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/style.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style=" text-align:center;font-size:14px;vertical-align:middle;  " class="text headerband">
        <b>Docket RFQ List</b></div>
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="1"> 
        <tr><td>
        <div style="border:none;  padding:5px; font-size:13px; ">
            <table style="width :100%;">
            <tr>
            <td style="text-align:right; width:">DD # :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblDocketNo"></asp:Label></td>
            <td>
                <span style="color:Red;">Download Specification</span>&nbsp;<asp:ImageButton id="btnDocketView" runat="server" ImageUrl="~/Modules/PMS/Images/paperclipx12.png" OnClick="btnDocketView_Click" />
            </td>
            <td>
                <span style="color:Red;">Download SOR</span>&nbsp;<asp:ImageButton id="btnDownloadSOR" runat="server" ImageUrl="~/Modules/PMS/Images/paperclipx12.png" OnClick="btnDownloadSOR_Click" />
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
         <tr style="  font-size:15px;">
         <td style="padding:3px;">
         <div style="float:left" >
         <b>RFQ List :</b>
         </div>
         <asp:ImageButton runat="server" ID="btn_AddRFQ" OnClick="btn_AddRFQ_Click" ImageUrl="~/Modules/PMS/Images/add.png" style="float:left; padding-left:5px" ToolTip="Add New RFQ"/>
         <asp:ImageButton runat="server" ID="btn_EditRFQ" OnClick="btn_EditRFQ_Click" ImageUrl="~/Modules/PMS/Images/editX16.png" style="float:left; padding-left:5px" ToolTip="Edit RFQ" Visible="false" />
         <asp:ImageButton runat="server" ID="btn_Resubmit" OnClick="btn_Resubmit_Click" ImageUrl="~/Modules/PMS/Images/resubmit.png" OnClientClick="return confirm('Are you sure to allow vendor to Re-Submit Quotes?');" style="float:left; padding-left:5px" ToolTip="Allow Vendor to Re-Sumbit RFQ" Visible="false" />
         </td>
         </tr>
         <tr>
         <td>
          <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 25px ; text-align:center; border-bottom:none; background-color:#ADD6FF; font-weight:bold;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px">
                <colgroup>
                            <col style="width:4%;" />
                            <col style="width:30%;" />
                            <col style="width:44%;"/>
                            <col style="width:10%;" />
                            <col style="width:10%;" />
                            <col style="width:2%;" />
                </colgroup>
                <tr class= "headerstylegrid">
                    <td style="text-align:center;width:4%;">Select</td>
                    <td align="left" style="width:30%;">RFQ# </td>
                    <td align="left" style="width:44%;">Yard Name</td>
                    <td align="left" style="width:10%;">Status</td>
                    <td align="center" style="width:10%;">Activity</td>
                    <td style="width:2%;">&nbsp;</td>
                    </tr>
                </table>
             </div>
         <div style="border:solid 1px gray; text-align:left;font-size :11px;padding:0px; height:500px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="dv2">
                <table cellspacing="0" rules="rows" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
               <colgroup>
                    <col style="width:4%;" />
                            <col style="width:30%;" />
                            <col style="width:44%;"/>
                            <col style="width:10%;" />
                            <col style="width:10%;" />
                            <col style="width:2%;" />
                </colgroup>
                <asp:Repeater ID="rptRFQ" runat="server">
                    <ItemTemplate>
                            <tr style='<%# (Common.CastAsInt32(Eval("RFQId"))== RFQId)? "background-color:#4DB8FF;color:white;" : "" %> '>
                            <td style="text-align:center;width:4%;"><%--<asp:ImageButton runat="server" OnClick="btnSelectRFQ_Click" CommandArgument='<%#Eval("RFQId")%>' ID="btnSelectRFQ" ImageUrl="~/Modules/PMS/Images/icon-view-blue_12.png" ToolTip="Edit Job" />--%> <asp:RadioButton ID="rdoSelect" OnCheckedChanged="btnSelectRFQ_Click" Checked='<%# (Common.CastAsInt32(Eval("RFQId"))== RFQId) %>'  GroupName="dd" RFQId='<%#Eval("RFQId")%>' AutoPostBack="true"  runat="server" /></td>
                            <td style="text-align:left;width:30%;"><b><%#Eval("RFQNO")%></b></td>
                            <td align="left" style="width:44%;"><%#Eval("YardName")%></td>
                            <td align="left" style="width:10%;"><%#Eval("StatusName")%></td>
                             <td align="left" style="width:10%;">
                                                <asp:ImageButton runat="server" ID="btnMailSend" OnClick="btnMailSend_Click" CommandArgument='<%#Eval("RFQId")%>' Visible='<%# (!OrderCreated && ( Eval("Status").ToString()!="I" && Eval("Status").ToString()!="Q" )) %>' ImageUrl="~/Modules/PMS/Images/email.png" ToolTip="Send Mail to Yard"/>
                                                <asp:ImageButton runat="server" ID="btnPrintRFQ" OnClick="btnPrintRFQ_Click" CommandArgument='<%#Eval("RFQId")%>' ImageUrl="~/Modules/PMS/Images/printer16x16.png" ToolTip="Print RFQ"/>
                                                <asp:ImageButton runat="server" ID="btnCancel" OnClick="btnCancel_Click" CommandArgument='<%#Eval("RFQId")%>' ImageUrl="~/Modules/PMS/Images/cancel.png" ToolTip="Cancel RFQ" Visible='<%#(Eval("Executed").ToString()=="" && Eval("Status").ToString()!="I" && !OrderCreated) %>' />
                                                <asp:ImageButton runat="server" ID="btnUndoCancel" OnClick="btnUndoCancel_Click" CommandArgument='<%#Eval("RFQId")%>' ImageUrl="~/Modules/PMS/Images/reset.png" ToolTip="Undo Cancel RFQ" Visible='<%#(Eval("Status").ToString()=="I" && UserId == 1 && !OrderCreated ) %>' />
                                                <asp:ImageButton runat="server" ID="btnCreatePassword" OnClick="btnCreatePassword_Click" CommandArgument='<%#Eval("RFQId")%>' Visible='<%#(UserId==1) && (!OrderCreated) %>' ImageUrl="~/Modules/PMS/Images/password.png" ToolTip="Create Password" />
                                                <asp:ImageButton runat="server" ID="btnSendMailToSelf" OnClick="btnSendMailToSelf_Click" CommandArgument='<%#Eval("RFQId")%>' Visible='<%#(POId==Common.CastAsInt32(Eval("RFQId")))%>' ImageUrl="~/Modules/PMS/Images/email.png" ToolTip="Get Link to access from internet"/>
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
         
         <div style="text-align:right; padding:3px;">
            <div style="text-align:left;float:left; padding-top:5px;">
                 <asp:Label ID="lblMsgMain" ForeColor="Red" runat="server"></asp:Label>
            </div>
            <div style="text-align:left;float:right; padding:3px;">
                <asp:Button runat="server" ID="btnQA" Text="Quote Comparison" OnClick="btnQA_Click" OnClientClick="this.value='Loading..';" style=" padding:3px; border:none; color:White; background-color:Red; width:120px;" Visible="false"  />
                <asp:Button runat="server" ID="Button1" Text="Quote Analysis" OnClick="btnQAnalysis_Click" OnClientClick="this.value='Loading..';" style=" padding:3px; border:none; color:White; background-color:Red; width:120px;"  />
                <asp:Button runat="server" ID="btnCreatePO" OnClientClick="return confirm('Are you sure to confirm ?');" Text="Yard Confirmation" OnClick="btnCreatePO_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:150px;"  />
            </div>
         </div>
        </td> 
    </tr>
    </table>
            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_Yards" runat="server" visible="false">
            <center>
            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
            <div style="position: relative; width: 800px; height: 500px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
            <asp:UpdatePanel runat="server" id="UpdatePanel413">
            <ContentTemplate>
            <div style="padding:3px; background-color:orange" >
                <table>
                    <tr>
                        <td>
                            Filter Yard Name : 
                        </td>
                        <td>
                             <asp:TextBox runat="server" ID="txtYard" Width="300px" MaxLength="100" OnTextChanged="LoadYards" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
                <br />
            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 25px ; text-align:center; border-bottom:none; background-color:#ADD6FF; font-weight:bold;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px">
                    <colgroup>
                          <col style="width:50px;" />
                            <col style="width:300px;"/>
                            <col style="width:400px;" />
                            <col style="width:17px;" />
                
                <tr class= "headerstylegrid">
                    <td style="text-align:center;width:50px;">Select</td>
                    <td align="left" style="width:300px;">Yard Name</td>
                    <td align="left" style="width:400px;">Email Address </td>
                    
                    <td style="width:17px;">&nbsp;</td>
                    </tr>
                </table>
                </colgroup>
             </div>
            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 400px ; text-align:center;" class="scrollbox">
                        <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                            <col style="width:50px;" />
                            <col style="width:300px;"/>
                            <col style="width:400px;" />
                            <col style="width:17px;" />
                                
                                <asp:Repeater ID="rptYards" runat="server">
                                    <ItemTemplate>
                                            <tr>
                                            <td style="text-align:center;width:50px;">
                                             <asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("YardId")%>' /></td>
                                            <td align="left" style="width:300px;" ><div style="width:280px;"><%#Eval("YardName")%></div></td>
                                            <td align="left" style="width:400px;"><div style="width:375px;"><%#Eval("Email")%></div></td>
                                            <td style="width:17px;">&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style='background-color:#FFF5E6'>
                                            <td style="text-align:center;width:50px;"><asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("YardId")%>' /></td>
                                            <td align="left" style="width:300px;"><div style="width:280px;"><%#Eval("YardName")%></div></td>
                                            <td align="left" style="width:400px;"><div style="width:375px;"><%#Eval("Email")%></div></td>
                                            <td style="width:17px;">&nbsp;</td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                                    </colgroup>
                            </table>
           </div>

            <div style="text-align:center; padding:5px;">
                    <div style="text-align:left;float:left; padding-top:5px;">
                        <asp:Label ID="lbl_MsgYards" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                    <div style="text-align:left;float:right">
                        <asp:Button runat="server" ID="btn_CreateRFQ" Text="Create RFQ" OnClick="btn_CreateRFQ_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                        <asp:Button runat="server" ID="btn_CloseYard" Text="Close" OnClick="btn_CloseYard_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                    </div>
            </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_CreateRFQ" />
                <asp:PostBackTrigger ControlID="btn_CloseYard" />
            </Triggers>
            </asp:UpdatePanel>
            </div>
            </center>
            </div>

            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_Mail" runat="server" visible="false">
            <center>
            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
            <div style="position: relative; width: 800px; height: 500px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
            <asp:UpdatePanel runat="server" id="UpdatePanel1">
            <ContentTemplate>
            <div style="padding:3px; background-color:orange" >
               <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px; font-weight:bold;">
               <tr>
                   <td style="text-align:right;">RFQ# : </td>
                   <td style="text-align:left;"><asp:Label runat="server" ID="lbl_Mail_RFQNo"  ></asp:Label></td>
                   <td style="text-align:right;">Yard Name : </td>
                   <td style="text-align:left;"><asp:Label runat="server" ID="lbl_Mail_YardName"  ></asp:Label></td>
               </tr>
               </table>
                  
            </div>
            <div style="border-bottom:none; background-color:#ADD6FF;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">                    
                   <tr>
                        <td style="text-align:right; width:7%; ">To : </td>
                        <td style="text-align:left"><asp:TextBox ID="txtMailTo" runat="server" ReadOnly="true" Width="98%" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align:right; ">CC : </td>
                        <td style="text-align:left"><asp:TextBox ID="txtMailCC" runat="server" ReadOnly="true" Width="98%" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align:right; ">Subject : </td>
                        <td style="text-align:left"><asp:TextBox ID="txtMailSubject" runat="server" Width="98%" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2" >
                        <asp:TextBox ID="txtMailText" runat="server" Width="98%" Height="330px" TextMode="MultiLine" style="display:none"></asp:TextBox>
                        <div runat="server" id="dv_mailcontent" contenteditable="true" style="height:330px;width:98%; background-color:White; overflow-x:hidden; overflow-y:scroll; text-align:left; padding-left:3px;">

                        </div>
                        </td>
                    </tr>
                </table>
             </div>            

            <div style="text-align:center; padding:5px;">
                    <div style="text-align:left;float:left; padding-top:5px;">
                        <asp:Label ID="lblMsg_Mail" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                    <div style="text-align:left;float:right">
                        <asp:Button runat="server" ID="btnCreateMail" Text="Create Mail" OnClientClick="CopyData();DisableMe(this);" OnClick="btnCreateMail_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />                        
                        <asp:Button runat="server" ID="btnClose_Mail" Text="Close" OnClick="btnClose_Mail_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                    </div>
            </div>

            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_SendMail" runat="server" visible="false">
            <center>
            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
            <div style="position: relative; width: 400px; height: 100px; padding: 3px; text-align: center;background: white; z-index: 180; top: 150px; border: solid 10px gray;">
               
               <div style="text-align:center; padding:5px;">
                    <div style="text-align:left;float:left; padding-top:5px;">
                        <asp:Label ID="lblMsgSendMail" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                    <br /><br /><br /><br />
                    <div style="text-align:center; width:100%;">
                        <asp:Button runat="server" ID="btnSendMail" Text="Send Mail" OnClick="btnSendMail_Click" OnClientClick="CopyData();DisableMe(this);" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                        <asp:Button runat="server" ID="btnCloseSendMail" Text="Close" OnClick="btnCloseSendMail_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                    </div>
            </div>

            </div>
            </center>
            </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSendMail" />
                <asp:PostBackTrigger ControlID="btnClose_Mail" />
            </Triggers>
            </asp:UpdatePanel>
            </div>
            </center>
            </div>

            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_PasswordUpdation" runat="server" visible="false">
            <center>
            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
            <div style="position: relative; width: 450px; height: 120px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
            <asp:UpdatePanel runat="server" id="UpdatePanel2">
            <ContentTemplate>
            <div style=" text-align:center; " class="text headerband" >Create/ update password </div>
            <div style="border-bottom:none;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">                    
                   <tr>
                        <td style="text-align:right; width:150px; ">Password : </td>
                        <td style="text-align:left">
                         <asp:TextBox runat="server" ID="Password" MaxLength ="15" ></asp:TextBox>
                         <asp:RegularExpressionValidator ID="Regex1" runat="server" ControlToValidate="Password" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" ErrorMessage="Password must be of minimum 8 characters and alphanumeric." ForeColor="Red" ValidationGroup="pw" Display="Dynamic" />
                         <asp:RequiredFieldValidator ID="rfvpw" runat="server" ControlToValidate="Password" SetFocusOnError="true" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="pw" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; ">Expiry Date : </td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txtExpiryDate" runat="server" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExpiryDate" SetFocusOnError="true" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="pw" />
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtExpiryDate" Format="dd-MMM-yyyy" ></asp:CalendarExtender>
                        </td>
                    </tr>
                </table>
             </div>           

            <div style="text-align:center; padding:5px;">
                    <div style="text-align:left;float:left; padding-top:5px;">
                        <asp:Label ID="lblMsg_Pwd" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                    <div style="text-align:left;float:right">
                        <asp:Button runat="server" ID="btnSavePassword" Text="Save" OnClick="btnSavePassword_Click" ValidationGroup="pw" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                        <asp:Button runat="server" ID="btnClosePassword" CausesValidation="false" Text="Close" OnClick="btnClosePassword_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                    </div>
            </div>

            </ContentTemplate>
            <Triggers >
            <asp:PostBackTrigger ControlID="btnClosePassword" />
            </Triggers>
            </asp:UpdatePanel>
            </div>
            </center>
            </div>

   
    </div>
    </form>
</body>
</html>
