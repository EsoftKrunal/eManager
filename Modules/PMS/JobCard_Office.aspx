<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobCard_Office.aspx.cs" Inherits="JobCard_Office" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    *
    {
        font-family:Calibri;
        font-size:13px;
    }
    .pageheader
    {
         text-align :center; 
         padding:8px; 
         background-color:#4da6ff; 
         color:White; 
         font-size:20px;
    }
     .myheading1
    {
        color:black; 
        text-align :left; 
        font-size :16px; 
        padding:4px;
        border-bottom:solid 1px #ffcc80;
        background-color:#fff7e6;
        font-weight:bold;
    }
    .gridheader
    {
        background-color:#5CAEFF;
    }
    .border_table td
    {
        border: solid 1px #e5e5e5;
    }
     
        .btn1
        {
            color:white;
            border:none;
            background-color:#5CAEFF;
            line-height:normal;
            padding:10px ! important;            
            height:auto;
        }
    input,select
    {
        line-height: 30px;
        height: 20px;
        font-size: 13px;
    }
    </style>
    <script type="text/javascript">
        function closewin()
        {
            if (window.opener != null)
                window.opener.document.getElementById("btnTemp").click();
            window.close();
        }
    </script>
</head>
<body style="font-family: Calibri; font-size: 14px;">
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="text headerband">PMS - JOB CARD [ HISTORY ID : <%= Request.QueryString["HID"].ToString() %> ]</div>
    <table border="1" cellpadding="0" cellspacing="0" style="text-align:center" width="100%">
    <tr>
    <td>
    <asp:Panel ID="plUpdateJobs" runat="server">
    <table border="0" cellpadding="4" cellspacing="0" style="width:100%;border-collapse:collapse;">
                                                        <tr>
                                                            <td style="text-align:right;width:150px;  font-weight:bold">Vessel :</td>
                                                            <td style="text-align:left;">
                                                                <asp:Label ID="lblVesselCode" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                            <td style="text-align:right;width:150px; font-weight:bold">&nbsp;</td>
                                                            <td style="text-align:left;width:50px;">&nbsp;</td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;width:150px;  font-weight:bold">
                                                                Component :&nbsp;</td>
                                                            <td style="text-align:left;">
                                                                <asp:Label ID="lblUpdateComponent" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width:5px;">
                                                                &nbsp;</td>
                                                            <td style="text-align:right;width:150px; font-weight:bold">
                                                                &nbsp;</td>
                                                            <td style="text-align:left;width:50px;">
                                                                &nbsp;</td>
                                                            <td style="width:5px;">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;width:150px;  font-weight:bold">
                                                                Interval :&nbsp;</td>
                                                            <td style="text-align:left;">
                                                                <asp:Label ID="lblUpdateInterval" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width:5px;">
                                                                &nbsp;</td>
                                                            <td style="text-align:right;width:100px; font-weight:bold">
                                                                &nbsp;</td>
                                                            <td style="text-align:left;width:50px;">
                                                                &nbsp;</td>
                                                            <td style="width:5px;">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align:right; width:150px; font-weight:bold">Job :&nbsp;</td>
                                                           <td colspan="4" style="text-align:left; "><asp:Label ID="lblUpdateJob" runat="server"></asp:Label> </td>
                                                           <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right; width:150px; font-weight:bold">
                                                                <span lang="en-us">Job Description :</span></td>
                                                            <td colspan="4" style="text-align:left; ">
                                                                <asp:Label ID="lblJobDescr" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                 </table>
    <div class="myheading1">Maintenance Details</div>
    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;">
    <tr>
    <td>
    <table border="0" cellpadding="4" cellspacing="0" style="width:100%;border-collapse:collapse;">
                                            <tr>
                                               <td style="text-align:left; font-weight:bold;">Emp. No &nbsp;/&nbsp;Emp. Name :</td>
                                               <td style="text-align:left;"><asp:Label ID="lblEmpNo" runat="server" ></asp:Label>
                                                           &nbsp;/&nbsp;<asp:Label ID="lblEmpName" runat="server" ></asp:Label>
                                                       </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left; font-weight:bold;">Rank :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblRank" runat="server" ></asp:Label>
                                               </td>
                                               
                                            </tr>
                                            <tr>
                                               <td style="text-align:left; font-weight:bold;">Maintenance Reason :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblRemarks" runat="server" ></asp:Label>
                                                  <br />
                                                  <asp:RadioButtonList ID="rdoBreakdownReason" RepeatDirection="Horizontal" runat="server" Visible="false">
                                                   <asp:ListItem Text="Equipment Working" Value="1"></asp:ListItem>
                                                   <asp:ListItem Text="Equipment Not Working" Value="2"></asp:ListItem>
                                                  </asp:RadioButtonList>     
                                               </td>
                                            </tr>
                                            <tr id="trSpecify" runat="server" visible="false">
                                                <td style="text-align:left; font-weight:bold;">Specify :&nbsp;</td>
                                                <td style="text-align:left"><asp:Label ID="lblSpecify" runat="server" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr runat="server" visible="false">
                                               <td style="text-align:left; font-weight:bold;">Last Done Date :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblLastDoneDt" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr runat="server" visible="false">
                                               <td style="text-align:left; font-weight:bold;">Interval :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblInterval" runat="server"></asp:Label></td>
                                            </tr>
                                            
                                        </table>
    <div id="trHr" runat="server" visible="false">

                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                                                <ContentTemplate>
                                                 <table border="0" cellpadding="5" cellspacing="0" width="100%";border-collapse:collapse;">
                                                     <tr style="background-color:#fff7e6">
                                                        <td style="text-align:left; font-weight:bold;">Last Hr. Done </td>
                                                        <td style="text-align:left;font-weight:bold;"> Current Hr. Done</td>
                                                        <td></td>
                                                      </tr>
                                                      <tr style="padding-top:5px;">
                                                         <td style="text-align:left;" ><asp:Label ID="lblLastHour" runat="server" ></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;"><asp:Label ID="lblDoneHour" runat="server" ></asp:Label>
                                                         </td>
                                                         <td id="Td2" runat="server" visible="false" style="text-align:left;"><asp:Label ID="lblNextHour" runat="server" ></asp:Label>
                                                         </td>
                                                      </tr>
                                                  </table>
                                                   </ContentTemplate>
                                                </asp:UpdatePanel>
                                        </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>
    <table border="0" cellpadding="5" cellspacing="0" width="100%";border-collapse:collapse;">
    <tr style="background-color:#fff7e6">
    <td style="text-align:left; font-weight:bold;">Due Date</td>
    <td style="text-align:left; font-weight:bold;">Done Date</td>
    <td style="text-align:left; font-weight:bold;">Next Due Date</td>
    </tr> 
    <tr>  
    <td style="text-align:left"><asp:Label ID="lblDuedt" runat="server"></asp:Label>
    </td>
    <td style="text-align:left;"><asp:Label ID="lblDoneDate" runat="server" ></asp:Label>
                                           
    </td>
    <td id="Td1" style="text-align:left;"><asp:Label ID="lblNextDueDt" runat="server"></asp:Label>
    </td>
    </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </td>
    <td style="border-left:solid 1px #c2c2c2">
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%";border-collapse:collapse;">
                                              <tr>
                                               <td style="text-align:left; font-weight:bold;">Service Report :&nbsp;</td>
                                               <td style="text-align:left">                                                    
                                                    <asp:Label ID="lblServiceReport" TextMode="MultiLine" runat="server" ></asp:Label>
                                                    </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left; font-weight:bold;">Condition Before :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblCondBefore" runat="server"></asp:Label>
                                               <asp:HiddenField ID="hfIntervalId_H" runat="server" />
                                               <asp:HiddenField ID="hfInterval_H" runat="server" />
                                               </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left; font-weight:bold;">Condition After :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblCondAfter" runat="server"></asp:Label>
                                               </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left; font-weight:bold;">Updated By&nbsp;/&nbsp;On : </td>
                                               <td style="text-align:left"><asp:Label ID="lblUpdatedByOn" runat="server"></asp:Label></td>
                                            </tr>
                                            </table>
                                        </td>
    </tr>
    </table>
    <div>
    <asp:Label ID="lblSaveMsg" style="color:Red;" runat="server"></asp:Label>
    <asp:Button ID="btnSave" Text="Update" OnClick="btnSave_Click" CssClass="btnorange" runat="server" style="width:120px; height:20px; background-color:orange;" />
</div>
    <div class="myheading1" id="trRatingsHeader" runat="server" visible="false">Ratings</div>        
    <div id="trRating" runat="server" visible="false">
                                          <table cellpadding="2" cellspacing="0" width="100%">
                                             <tr>
                                                 <td style="text-align:right; font-weight:bold;">Coating :&nbsp;</td>
                                                 <td style="text-align:left">
                                                      <asp:Label ID="lblCoating" runat="server"></asp:Label>
                                                 </td>
                                                 <td style="text-align:right; font-weight:bold;">Corrosion :&nbsp;</td>
                                                 <td style="text-align:left">
                                                     <asp:Label ID="lblCorrosion" runat="server"></asp:Label>
                                                 </td>
                                                 <td style="text-align:right; font-weight:bold;">Deformation :&nbsp;</td>
                                                 <td style="text-align:left">
                                                      <asp:Label ID="lblDeformation" runat="server"></asp:Label>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td style="text-align:right; font-weight:bold;">Cracks :&nbsp;</td>
                                                 <td style="text-align:left">
                                                     <asp:Label ID="lblCracks" runat="server"></asp:Label>
                                                 </td>
                                                 <td style="text-align:right; font-weight:bold;">Overall Rating :&nbsp;</td>
                                                 <td style="text-align:left">
                                                     <asp:Label ID="lblOAllRating" runat="server"></asp:Label>
                                                 </td>
                                                 <td></td>
                                                 <td></td>
                                             </tr>
                                          </table>
                                        
                                    </div>                         
    <div class="myheading1" id="trSpareHeader" runat="server" visible="false">Consumed Spares</div>
    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;">

                                   <tr id="trSpare" runat="server" visible="false">
                                        <td colspan="2" style="text-align:left;">
                                          <table border="0" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;" class="border_table">
                                                <colgroup>
                                                        <col  />
                                                        <col style="width: 150px"/>
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 17px;" />
                                                    <tr align="left" class= "headerstylegrid">
                                                        <td style="text-align:left">Spare Details</td>
                                                        <td>Part#</td>
                                                        <td>Qty(Cons.)</td>
                                                        <td>Qty(ROB)</td>
                                                        <td></td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                            <%--<div id="divAttachment" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 130px; text-align: center;">--%>
                                                <table border="1" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;" class="border_table">
                                                    <colgroup>
                                                        <col  />
                                                        <col style="width: 150px"/>
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 17px;" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptSpares" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="left">
                                                                    <%#Eval("SpareName")%>
                                                                    <br />
                                                                    <span style="font-size:9px;color:blue;"><i><%#Eval("Maker")%></i></span>
                                                                </td>
                                                                
                                                                <td align="left">
                                                                    <%#Eval("PartNo")%>
                                                                </td>
                                                                <td align="center">
                                                                   <asp:Label ID="lblQtyCons" Text='<%#Eval("QtyCons")%>' runat="server"></asp:Label>
                                                                <td align="center">
                                                                   <asp:Label ID="lblQtyRob" Text='<%#Eval("QtyRob")%>' runat="server"></asp:Label>
                                                                </td>
                                                                <td></td>
                                                                <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            <%--</div>--%>
                                        </td>
                                    </tr>
                                    <tr id="trAddAttachments" runat="server">
                                       <td colspan="2">
                                              <table cellpadding="2" cellspacing="0" width="100%">
                                                 <tr>
                                                      <td style="text-align:right; vertical-align:middle;">Description :&nbsp;</td>
                                                      <td style="text-align:left; vertical-align:middle;">
                                                          <asp:TextBox ID="txtAttachmentText" runat="server" MaxLength="500" 
                                                              Width="470px" ></asp:TextBox></td>
                                                      <td style="text-align:right; vertical-align:middle;">Attachment :&nbsp;</td>
                                                      <td style="text-align:left; vertical-align:middle;">
                                                      <asp:FileUpload ID="flAttachDocs" runat="server" />
                                                      </td>
                                                      <td style="vertical-align:middle;">
                                                          <asp:Button ID="btnSaveAttachment" Text="Upload" 
                                                               runat="server" cssClass="btn1"
                                                              style="width:120px; height:20px; background-color:orange; float:right;" 
                                                              onclick="btnSaveAttachment_Click" /></td>
                                                 </tr>
                                             </table>
                                       </td>
                                    </tr>
                                  </table>
    <div class="myheading1" id="trAttachmentHeader" runat="server" visible="false">Attachments</div>
    <div id="trAttachment" runat="server" visible="false">
<table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
    <colgroup>
            <col  />
            <col style="width: 30px;" />
            <col style="width: 30px;" />
            <tr align="left" class= "headerstylegrid">
                <td style="text-align:left">Description</td>
                <td></td>
                <td></td>
            </tr>
    </colgroup>
</table>
<table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
<colgroup>
<col  />
<col style="width: 30px;" />
<col style="width: 30px;" />
</colgroup>
<asp:Repeater ID="rptAttachment" runat="server">
<ItemTemplate>
<tr >
<td align="left"><%#Eval("AttachmentText")%></td>
<td>
    <a runat="server" ID="ancFile"  href='<%# ProjectCommon.getLinkFolder(DateTime.Parse(lblDoneDate.Text.Trim())) + Eval("FileName").ToString()  %>' target="_blank" Visible='<%# Eval("FileName").ToString() != "" %>' title="Show Attachment" >
        <img src="Images/paperclip.gif" style="border:none"  />
    </a>
</td>
<td>
<asp:ImageButton runat="server" ID="btnDelAttachment" ImageUrl="~/Modules/PMS/Images/delete.png" Height="12px" OnClick="DeleteAttachment_OnClick" title='Delete Attachment' CssClass='<%#Eval("VesselCode")%>' CommandArgument='<%#Eval("TableId")%>' OnClientClick="javascript:confirm('Are you sure to remove this attachment?');" Visible='<%#(Session["UserType"].ToString() == "S" && (!IsVerified)) %>' />
</td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>
    <div class="myheading1" >Ship Verification</div>
    <table border="0" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;">
<tr>
<td style="text-align:right; width:200px; font-weight:bold;">Verified By/On :&nbsp;</td>
<td style="text-align:left"><asp:Label ID="lblVerified" runat="server"></asp:Label></td> 
</tr>
</table>
    <div class="myheading1" id="trOffVerifyLabel" runat="server">Office Verification</div>
    <div id="trOffVerify" runat="server">
<table border="0" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;">
<tr>
<td style="text-align:right; width:200px;font-weight:bold;">Remark :&nbsp;</td>
<td style="text-align:left"><asp:Label ID="lblRemark" runat="server"></asp:Label></td> 
</tr>
<tr>
<td style="text-align:right; width:200px;font-weight:bold;">Verified By/On :&nbsp;</td>
<td style="text-align:left"><asp:Label ID="lblOfficeVerified" runat="server"></asp:Label></td> 
</tr>
    <tr>
        <td style="text-align:right; width:200px;font-weight:bold;">
            Follow Up this Job with Ship :</td>
        <td style="text-align:left">
            <asp:Label ID="lblFUPShip" runat="server"></asp:Label>
        </td>
    </tr>
<tr>
    <td style="text-align:right; width:200px;font-weight:bold;"> Next Visit Verified :</td>
    <td style="text-align:left">
        <asp:Label ID="lblNextVisitVerified" runat="server" ></asp:Label>
    </td>
</tr>
<tr>
    <td style="text-align:right; width:200px;font-weight:bold;"> Rating :</td>
    <td style="text-align:left">
        <asp:Label ID="lblRating" runat="server" ></asp:Label>
    </td>
</tr>
<tr>
    <td style="text-align:right; width:200px;font-weight:bold;">Recieved On :</td>
    <td style="text-align:left">
        <asp:Label ID="lblRecievedOn" runat="server" ></asp:Label>
    </td>
</tr>
</table>
</div>
    <div class="myheading1" id="trOffCommLabel" runat="server">Office Verification</div>
    <div id="trOffComm" runat="server">
<table border="0" cellpadding="2" cellspacing="0" style="width: 100%;border-collapse: collapse;">

    <tr>
        <td style="text-align:right; width:350px; font-weight:bold;font-size:14px; vertical-align:middle;">Comments :&nbsp;</td>
        <td style="text-align:left">
            <asp:TextBox ID="txtComments" runat="server" style="height:45px; width:80%" TextMode="MultiLine" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="text-align:right; width:350px; font-weight:bold;">
            <span style="font-weight:bold; font-size:14px;">Do you want to follow up this 
            job with the ship ?</span>
            <%--<asp:CheckBox runat="server" ID="chkflpNeeded" Font-Bold="true" ForeColor="Red" Font-Size="22px" />&nbsp;--%>

            :</td>
        <td style="text-align:left">
            <asp:DropDownList ID="ddlflpNeeded" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlflpNeeded_OnSelectedIndexChanged" Width="100px">
                <asp:ListItem Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="text-align:right; width:350px; font-weight:bold;">
            <span style="font-weight:bold; font-size:14px;">Do you want to verify this job 
            during your next visit ?</span>
            :</td>
        <td style="text-align:left">
            <asp:DropDownList ID="ddlNextVisit" runat="server" Enabled="false"  Width="100px"> 
            <asp:ListItem Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                <asp:ListItem Text="No" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="text-align:right; width:350px; font-weight:bold;">
            <span style="font-weight:bold; font-size:14px;">How do you rate the condition of 
            this equipment ?</span>
            :</td>
        <td style="text-align:left">
            <asp:RadioButton ID="radA" runat="server" Enabled="false" Font-Bold="true" 
                Font-Size="22px" ForeColor="Red" GroupName="AFD" Text="A" />
            &nbsp;
            <asp:RadioButton ID="radB" runat="server" Enabled="false" Font-Bold="true" 
                Font-Size="22px" ForeColor="Red" GroupName="AFD" Text="B" />
            &nbsp;
            <asp:RadioButton ID="radC" runat="server" Enabled="false" Font-Bold="true" 
                Font-Size="22px" ForeColor="Red" GroupName="AFD" Text="C" />
            &nbsp;
            <asp:RadioButton ID="radD" runat="server" Enabled="false" Font-Bold="true" 
                Font-Size="22px" ForeColor="Red" GroupName="AFD" Text="D" />
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="text-align:right; width:350px; font-weight:bold;">
            &nbsp;</td>
        <td style="text-align:left">
            <asp:Button ID="btnVerify" runat="server" CompName='<%#Eval("ComponentName")%>' 
                historyid='<%#Eval("HistoryId")%>' OnClick="btnVerify_OnClick" 
                Text="Save" vsl='<%#Eval("VesselCode")%>' Width="83px" Height="25px" cssClass="btn" />
        </td>
    </tr>
</table>
</div>
    </asp:Panel>
         </td>            
         <td style="width:200px;">
            <div style="padding:10px;background-color:#c2c2c2">Photos Uploaded</div>
            <div style="height:500px;overflow-y:scroll;overflow-x:hidden">
                <asp:Repeater runat="server" ID="rptiamges">
                    <ItemTemplate>
                        <img src='<%#Eval("filename")%>' style="width:175px" onclick="window.open(this.src,'');"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </td>
        </tr>
        </table>

    </div>
    <div style="padding:5px; text-align:center;height:30px;">
        <asp:Button runat="server" ID="btnRejectJob" onClick="btnRejectJob_Click" cssClass="btn" Text="Send for Rejection" Height="25px"/>
        <asp:Button runat="server" ID="btnClose1"  cssClass="btn" OnClientClick="return closewin();" Text="Close" Height="25px" />
    </div>  
    
    <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="divCorrection" visible="false" >
        <center>
        <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:435px; height:235px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:50px; opacity:1;filter:alpha(opacity=100)">
            <b>Job Rejection</b>
            <div>
                    <table cellpadding="4" cellspacing="3" style="margin-top:10px;" width="100%">
                            <tr>
                                <td style="text-align:left;">
                                    <b>Remarks :</b><br />
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="99%" TextMode="MultiLine" Height="70px"></asp:TextBox>
                                </td>
                            </tr>
                        <tr>
                            
                    </table> 

                    <asp:Button runat="server" ID="btnSaveRejection" onClick="btnSaveRejection_Click" cssClass="btn" Text="Save" />
                    <asp:Button runat="server" ID="btnCancelRejection" onClick="btnCancelRejection_Click" cssClass="btn" Text="Cancel"  />
            </div>
        </div>        
        </center>
    </div>

    </form>
</body>
</html>
