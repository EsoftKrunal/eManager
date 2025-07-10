<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMSReview.aspx.cs" Inherits="SMS_SMSReview" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/LPSQE/Procedures/SMSADMIN/SMSManualMenu.ascx" tagname="SMSManualMenu" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link rel="Stylesheet" href="../../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" src="../../js/Common.js"></script>
</head>
<body style="font-size:12px;font-family:Arial;">
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <asp:UpdatePanel runat="server" id="UpdatePanel2">
    <ContentTemplate>
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1" ></asp:ToolkitScriptManager>
        <uc3:SMSManualMenu ID="ManualMenu2" runat="server" />
    <div style="padding:5px 5px 5px 5px; ">       
       <div style="">
         <table style="width :100%; border-collapse:collapse; " cellpadding="2" cellspacing="2"  width="100%">
         <tr>
             <td style="text-align:right; font-weight:bold; ">Vessel :</td>
             <td style="text-align:left; "><asp:DropDownList runat="server" ID="ddlVessel"></asp:DropDownList></td>
             <td style="text-align:right; font-weight:bold; ">Office :</td>
             <td style="text-align:left; "><asp:DropDownList runat="server" ID="ddlOffice"></asp:DropDownList></td>
             <td style="text-align:right; font-weight:bold; ">Duration :</td>
             <td style="text-align:left">
                <asp:TextBox runat="server" id="txtFromDate"  MaxLength="15" CssClass="input_box" Width="80px" ></asp:TextBox>
                -
                <asp:TextBox runat="server" id="txtToDate"  MaxLength="15" CssClass="input_box" Width="80px"  ></asp:TextBox>
                <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"  PopupPosition="TopRight" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
                <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"  PopupPosition="TopRight" TargetControlID="txtToDate"></ajaxToolkit:CalendarExtender>
            </td>
            <td style="text-align:center; ">
            <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" CssClass="btn" runat="server"/>
            </td>
            </tr>
            <tr>
            <td style="text-align:right; font-weight:bold; ">Status :</td>
             <td style="text-align:left; "><asp:DropDownList runat="server" Width="90px" ID="ddlStatus">
                  <asp:ListItem Text="< All >" Value=""></asp:ListItem>
                  <asp:ListItem Text="Open" Value="O" Selected="True"></asp:ListItem>
                  <asp:ListItem Text="Closed" Value="C"></asp:ListItem>
                 </asp:DropDownList>
             </td>
             <td style="text-align:right; font-weight:bold; ">Stage :</td>
             <td style="text-align:left; "><asp:DropDownList runat="server" Width="100px" ID="ddlStage">
                  <asp:ListItem Text="< All >" Value=""></asp:ListItem>
                  <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
                  <asp:ListItem Text="UnApproved" Value="N"></asp:ListItem>
                  <asp:ListItem Text="Pending" Value="P"></asp:ListItem>
                  <asp:ListItem Text="On Hold" Value="H"></asp:ListItem>
                 </asp:DropDownList>
             </td>
              <td style="text-align:right; font-weight:bold;">Change Requsted :</td>
             <td style="text-align:left; "><asp:DropDownList runat="server" Width="100px" ID="ddlChReq">
                  <asp:ListItem Text="< All >" Value=""></asp:ListItem>
                  <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                  <asp:ListItem Text="No" Value="N"></asp:ListItem>
                 </asp:DropDownList>&nbsp;&nbsp;
                 <asp:Label runat="server" ID="lblNOR"></asp:Label>
             </td>
             <td style="text-align:center; ">
                <asp:Button ID="btnExport" Text="Export" OnClick="btnExport_Click" CssClass="btn" runat="server" />
             </td>
         </tr>
         </table>        
       </div>
       <div style="width:100%; background-color:#FFFFCC; font-weight:bold; height:20px; vertical-align:middle;">&nbsp;&nbsp;&nbsp;<img src='../../../HRD/Images/wrench.png'> : SMS Change Request</div>
       <div style="background-color:#FFFFFF;">
            <div style="width: 100%; overflow-y: scroll; overflow-x: hidden; height: 25px;">
                    <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width: 100%;border-collapse: collapse; height: 25px;" bordercolor="white">
                         <colgroup> 
                            <col style="text-align: left" width="120px" /> 
                            <col style="text-align: left" width="250px" /> 
                            <col style="text-align: left" width="250px" />                            
                            <col style="text-align: left"  />
                            <col style="text-align: center" width="80px" />
                            <col style="text-align: center" width="60px" />
                            <col style="text-align: center" width="80px" />
                            <col width="25px" />
                            <col width="25px" />
                            <%--<col style="text-align: left" width="70px" />--%>
                            <col width="25px" />
                        </colgroup>
                        <tr style="text-align :center;	" class= "headerstylegrid" >
                            <td>&nbsp;Location</td>
                            <td>&nbsp;Reviewed By</td>
                            <td>&nbsp;Manual</td>
                            <td>&nbsp;Section</td>
                            <td>Recd. Dt.</td>
                            <td>Status</td>
                            <td>SMS Change</td>
                            <td><img src="../../../HRD/Images/comments.png" title='Comments' /></td>
                            <td><img src="../../../HRD/Images/paperclip.gif" title='Manual Section' /></td>
                            <%--<td>&nbsp;Action</td>--%>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    </div>
                <div id="dvscroll_Supply" style="width: 100%; overflow-y: scroll; overflow-x: hidden; height: 450px;" class="scrollbox" onscroll="SetScrollPos(this)">
                    <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width: 100%;border-collapse: collapse;">
                        <colgroup> 
                            <col style="text-align: left" width="120px" /> 
                            <col style="text-align: left" width="250px" /> 
                            <col style="text-align: left" width="250px" />                            
                            <col style="text-align: left"  />
                            <col style="text-align: center" width="80px" />
                            <col style="text-align: center" width="60px" />
                            <col style="text-align: center" width="80px" />
                            <col width="25px" />
                            <col width="25px" />
                            <%--<col style="text-align: left" width="70px" />--%>
                            <col width="25px" />
                        </colgroup>
                        <asp:Repeater ID="rptComments" runat="server">
                            <ItemTemplate>
                                <tr onmouseover="style.backgroundColor='Yellow'" onmouseout="style.backgroundColor=''" >
                                    <td>&nbsp;<%#Eval("Location")%><%#(Eval("ChangeRequested").ToString().Trim()=="Y")?"<img src='../Images/wrench.png'>":""%></td>
                                    <td align="left">&nbsp;<%#Eval("CommentBy")%><span style='color:blue; font-style:italic;'>( <%#Eval("PositionName")%> )</span></td>
                                    <td>&nbsp;<%#Eval("ManualName")%></td>
                                    <td>&nbsp;<b><%#Eval("SectionId")%></b>: <%#Eval("heading")%></td>
                                    <td>&nbsp;<%# Common.ToDateString(Eval("CommentOn"))%></td>
                                    <td>&nbsp;<%#Eval("StatusText")%></td>
                                    <td>&nbsp;<%#Eval("StageText")%></td> 
                                    <td align="center"><asp:ImageButton ID="btnShowComments" OnClick="btnShowComments_Click" CommandArgument='<%#(Eval("MODE").ToString() + ":" + Eval("Location").ToString() + ":" + Eval("CommentId").ToString()) %>' runat="server" ImageUrl="~/Modules/LPSQE/Procedures/Images/icon_comment.gif" ToolTip="View Comment" /> </td>
                                    <td align="center"><asp:ImageButton ID="ImageButton1" OnClick="btnShowSection_Click" CommandArgument='<%#(Eval("ManualId").ToString() + ":" + Eval("SectionId").ToString())%>' runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.png" ToolTip="Open Manual Section" /> </td>
                                   <%--<td><asp:ImageButton ID="btnAction" CommandArgument='<%#(Eval("MODE").ToString() + ":" + Eval("Location").ToString() + ":" + Eval("CommentId").ToString()) %>' runat="server" ImageUrl="~/Images/HourGlass.png"  /></td> --%>
                                    <td>&nbsp;</td>                                                                
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
       
       </div>
    
    </div>
    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;font-family:Arial;font-size:12px;" id="dvComments" runat="server" visible="false">
    <center>
        <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
        <div style="position: relative; width: 1000px; height: 440px; padding:0px; text-align: center;background: white; z-index: 150; top: 70px; border: solid 3px #0066CC;">
        <div style="vertical-align:middle; text-align:center; padding-top:5px; height:20px; position:relative; box-sizing:border-box;" class="text headerband">Comments :</div>
            <div style='text-align:left; padding:3px; ' ><b>Manual Name : </b>[ <asp:Label runat="server" ID="lblMname"></asp:Label> ] </div>
            <div style='text-align:left; padding:3px; ' ><b>Section Name : </b>[ <asp:Label runat="server" ID="lblSName"></asp:Label> ] </div>
            <div style='text-align:left; padding:3px; ' ><b>Change Request : </b>[ <asp:Label runat="server" ID="lblByOn"></asp:Label> ] </div>
            <div style='text-align:left;' >
               <asp:TextBox runat="server" ID="advComments" Width="99%" Height="150px" style="border:solid 1px white" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
            </div>
            <div style='text-align:left; padding:3px; ' ><b>Response :</b>[ <asp:Label runat="server" ID="lblOfficeByOn"></asp:Label> ] </div>
            <div style='text-align:left;' >
               <asp:TextBox runat="server" ID="txtComments" Width="99%" Height="140px" style="border:solid 1px white; border-bottom:solid 1px grey;" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div style="vertical-align:middle;box-sizing:border-box; padding-top:5px; text-align:center; position:relative;">                
                <%--<div id="dv_SMSChangeReq" runat="server" visible="false" style="width:100%;" >--%>
                <span id="sp_SMSChangeReq" runat="server" visible="false" style="font-weight:bold;" >SMS Change Request : &nbsp;</span>
                    <asp:Button  ID="btnApprove" Text="Approve" OnClick="btnAction_Click" CommandArgument="A" style="padding:3px 10px 3px 10px;" runat="server" CssClass="btn" />
                    <asp:Button  ID="btnReject" Text="Reject" OnClick="btnAction_Click" CommandArgument="N" style=" padding:3px 10px 3px 10px; " runat="server" CssClass="btn" />
                    <asp:Button  ID="btnOnHold" Text="OnHold" OnClick="btnAction_Click" CommandArgument="H" style=" padding:3px 10px 3px 10px; " runat="server" CssClass="btn" />
                <%--</div>--%>
                <asp:Button  ID="btnSave" Text="Save" OnClick="btnAction_Click" CommandArgument="S" style=" padding:3px 10px 3px 10px;" runat="server" CssClass="btn" />
                <asp:Button  ID="btnCloseComment" Text="Close" OnClick="btnCloseComment_Click" style="padding:3px 10px 3px 10px; " runat="server" CssClass="btn" />
             </div>
        </div>
        </center>
        </div>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>