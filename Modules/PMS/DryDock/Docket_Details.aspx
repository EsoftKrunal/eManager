<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Docket_Details.aspx.cs" Inherits="Docket_Details" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="../../../css/app_style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <title>eMANAGER-PMS </title>
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/JQScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        function opendefectdetails(DN) {
            window.open('../Popup_BreakDown.aspx?DN=' + DN + '&FM=1', '', '');
        }
    </script>
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/style.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="text-align:center; padding:3px; font-size:15px; " class="text headerband">
        <b>Docking Specification</b>
     </div>
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="1">
        <tr><td>
        <div style="border:none; background-color : #ADD6FF; padding:5px; font-size:13px; ">
            <table width="100%">
            <tr>
            <td style="text-align:right; width:">DD # :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblDocketNo"></asp:Label></td>
            <td style="text-align:right">Vessel :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblVessel"></asp:Label></td>
            <td style="text-align:right">Type :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblType"></asp:Label></td>
            <td style="text-align:right">Plan Duration :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblPlanDuration"></asp:Label></td>
            </tr>
            <tr>
            <td style="text-align:right">Docking Category :</td>
            <td style="text-align:left" colspan="7">
            <div style="text-align:left"> 
                <asp:DropDownList runat="server" ID="ddlCat" style="padding:3px;" width="500px" AutoPostBack="true" OnSelectedIndexChanged="ddlCat_OnSelectedIndexChanged"></asp:DropDownList>
            </div>
            </td>
            </tr>
            </table>
        </div>
        </td></tr>
        <tr>        
        <td style=" text-align :left; vertical-align : top;" > 
         <table style="width:100%" cellpadding="0" cellspacing="0">
         <tr style=" background-color:#99CCFF; font-size:15px;">
         <td style="padding:3px;">
             <div style="float:left" >
                <b>Job Category :</b> 
             </div>
             <asp:ImageButton runat="server" ID="btn_ImportJobCategory" OnClick="btn_ImportJobCategory_Click" ImageUrl="~/Modules/PMS/Images/add.png" style="float:left; padding-left:5px" ToolTip="Import Job Category" />
         </td>
         <td style="padding:3px;">
         <div style="float:left" >
         <b>Job Details :</b>
         </div>
         <asp:ImageButton runat="server" ID="btn_A_AddJob" OnClick="btn_A_AddJob_Click" ImageUrl="~/Modules/PMS/Images/add.png" style="float:left; padding-left:5px" ToolTip="Add New Job" Visible="false"/>
         </td>
         </tr>
         <tr>
         <td >
          <div style="border:solid 1px gray; text-align:left;font-size :11px;height:25px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="Div4">
         <table cellspacing="0" rules="all" border="0" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px; vertical-align:middle; ">
                 <colgroup>
                    <col style="width:2.5%;" />
                    <col style="width:2.5%;" />
                    <col style="width:2.5%;" />
                    <col style="width:8%;" />
                    <col style="width:82.5%;"/>
                    <col style="width:2%;" />
                </colgroup>
                    <tr class= "headerstylegrid">
                        <td style="width:2.5%;">&nbsp;</td>
                        <td style="width:2.5%;">&nbsp;</td>
                        <td style="width:2.5%;">&nbsp;</td>
                        <td style="font-weight:bold;width:8%;" >Job Code</td>
                        <td style="font-weight:bold;width:82.5%;">Job Descr</td>
                        <td style="width:2%;">&nbsp;</td>
                    </tr>
         </table>
         </div> 
          <div style="border:solid 1px gray; text-align:left;font-size :11px;padding:3px; height:375px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="dv1">
                <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:2.5%;" />
                                    <col style="width:2.5%;" />
                                    <col style="width:2.5%;" />
                                    <col style="width:8%;" />
                                    <col style="width:82.5%;"/>
                                    <col style="width:2%;" />
                                </colgroup>
                                <asp:Repeater ID="rptJobs" runat="server">
                                    <ItemTemplate>
                                            <tr style='<%# (Common.CastAsInt32(Eval("DocketJobId"))== DocketJobId)? "background-color:#4DB8FF;color:white;" : "" %> '>
                                            <td style="text-align:center;width:2.5%;"><%--<asp:ImageButton runat="server" CssClass='<%#Eval("JobCode")%>' OnClick="btnSelectJob_Click" JobId='<%#Eval("JobId")%>' CommandArgument='<%#Eval("DocketJobId")%>' ID="btnSelectJob" ImageUrl="~/Modules/PMS/Images/icon-view-blue_12.png"  ToolTip="Select Job" />--%> <asp:RadioButton ID="rdoSelect" OnCheckedChanged="btnSelectJob_Click" CssClass='<%#Eval("JobCode")%>' JobId='<%#Eval("JobId")%>' Checked='<%# (Common.CastAsInt32(Eval("DocketJobId"))== DocketJobId) %>'  GroupName="DJ" DocketJobId='<%#Eval("DocketJobId")%>' AutoPostBack="true"  runat="server" /> </td>
                                            <td style="text-align:center;width:2.5%;"><asp:ImageButton runat="server" CssClass='<%#Eval("JobCode")%>' OnClick="btnEditJob_Click" JobId='<%#Eval("JobId")%>' CommandArgument='<%#Eval("DocketJobId")%>' ID="btnEditJob" ImageUrl="~/Modules/PMS/Images/editx16.png"  ToolTip="Edit Job" /></td>
                                            <td style="text-align:center;width:2.5%;"><asp:ImageButton runat="server" OnClientClick="return confirm('Are you sure to delete this Job Category?');" OnClick="btnDelete_Click" CommandArgument='<%#Eval("DocketJobId")%>' ID="btnDelete" ImageUrl="~/Modules/PMS/Images/Delete.png"  ToolTip="Delete Job" Visible='<%#Eval("DeleteVisible").ToString() == "true"%>' /></td>
                                            <td style="text-align:left;width:8%;"><b><%#Eval("JobCode")%></b></td>
                                            <td align="left" style="width:82.5%;"><div style="height:14px"><%#Eval("JobName")%></div></td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style='<%# (Common.CastAsInt32(Eval("DocketJobId"))== DocketJobId)? "background-color:#4DB8FF;color:white;" : "background-color:#FFF5E6" %>'>
                                            <td style="text-align:center;width:2.5%;"><%--<asp:ImageButton runat="server" CssClass='<%#Eval("JobCode")%>' OnClick="btnSelectJob_Click" JobId='<%#Eval("JobId")%>' CommandArgument='<%#Eval("DocketJobId")%>' ID="btnSelectJob" ImageUrl="~/Modules/PMS/Images/icon-view-blue_12.png"  ToolTip="Select Job" />--%> <asp:RadioButton ID="rdoSelect" OnCheckedChanged="btnSelectJob_Click" CssClass='<%#Eval("JobCode")%>' JobId='<%#Eval("JobId")%>' Checked='<%# (Common.CastAsInt32(Eval("DocketJobId"))== DocketJobId) %>'  GroupName="DJ" DocketJobId='<%#Eval("DocketJobId")%>' AutoPostBack="true"  runat="server" /> </td>
                                            <td style="text-align:center;width:2.5%;"><asp:ImageButton runat="server" CssClass='<%#Eval("JobCode")%>' OnClick="btnEditJob_Click" JobId='<%#Eval("JobId")%>' CommandArgument='<%#Eval("DocketJobId")%>' ID="btnEditJob" ImageUrl="~/Modules/PMS/Images/editx16.png"  ToolTip="Edit Job" /></td>
                                            <td style="text-align:center;width:2.5%;"><asp:ImageButton runat="server" OnClientClick="return confirm('Are you sure to delete this Job Category?');" OnClick="btnDelete_Click" CommandArgument='<%#Eval("DocketJobId")%>' ID="btnDelete" ImageUrl="~/Modules/PMS/Images/Delete.png"  ToolTip="Delete Job" Visible='<%#Eval("DeleteVisible").ToString() == "true"%>' /></td>
                                            <td style="text-align:left;width:8%;"><b><%#Eval("JobCode")%></b></td>
                                            <td align="left" style="width:82.5%;"><div style="height:14px"><%#Eval("JobName")%></div></td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
          </div>
         </td>
         <td style="width:800px">
         <div style="border:solid 1px gray; text-align:left;font-size :11px;height:25px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="Div5">
         <table cellspacing="0" rules="all" border="0" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px; vertical-align:middle; ">
                 <colgroup>
                    <col style="width:3%;" />                                                                        
                    <col style="width:15%;" />
                    <col style="width:66%;"/>
                    <col style="width:6%" />
                    <col style="width:6%;" />
                    <col style="width:2%;" /> 
                    <col style="width:2%;" />
                </colgroup>
                    <tr class= "headerstylegrid">
                        <td style="width:3%;">&nbsp;</td>
                        <td style="font-weight:bold;width:15%;" >Job Code</td>
                        <td style="font-weight:bold;width:66%; ">Short Descr</td>
                        <td style="font-weight:bold;width:6%; " >BidQty</td> 
                        <td style="font-weight:bold;width:6%; " >Unit</td>
                        <td style="width:2%;"><img src="../Images/paperclipx12.png" alt="" title="Attachment" /></td>
                        <td style="width:2%;">&nbsp;</td>
                    </tr>
         </table>
         </div>
         <div style="border:solid 1px gray; text-align:left;font-size :11px;padding:3px; height:375px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="dv2">
                <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                <colgroup>
                   <col style="width:3%;" />                                                                        
                    <col style="width:15%;" />
                    <col style="width:66%;"/>
                    <col style="width:6%;" />
                    <col style="width:6%;" />
                    <col style="width:2%;" /> 
                    <col style="width:2%;" />
                </colgroup>
                <asp:Repeater ID="rptSubJobs" runat="server">
                    <ItemTemplate>
                            <tr style='<%# (Common.CastAsInt32(Eval("DocketSubJobId"))== DocketSubJobId)? "background-color:#4DB8FF;color:white;" : "" %> '>
                            <td style="text-align:left;width:3%;"><asp:ImageButton runat="server" OnClick="btnSelectSubJob_Click" CommandArgument='<%#Eval("DocketSubJobId")%>' ID="btnSelectSubJob" ImageUrl="~/Modules/PMS/Images/icon-view-blue_12.png" style="float:right" ToolTip="Edit Job" /></td>
                            <td style="text-align:left;width:15%;"><b><%#Eval("SubJobCode")%></b></td>
                            <td align="left" style="width:66%;"><div style=" white-space: nowrap; width: 375px; overflow: hidden;text-overflow: ellipsis; "><%#Eval("SubJobName")%></div></td>
                            <td style=" text-align:right;width:6%;"><%#Eval("BidQty")%></td>
                            <td style="width:6%;"><%#Eval("Unit")%></td>
                            <td style="width:2%;"><asp:ImageButton runat="server" Visible='<%#(Eval("AttachmentName").ToString().Trim()!="")%>' ID="imgDownload" ImageUrl="~/Modules/PMS/Images/paperclipx12.png" CommandArgument='<%#Eval("DocketSubJobId")%>' OnClick="imgDownload_Click" /></td>
                            <td>&nbsp;</td>
                            </tr>
                    </ItemTemplate>       
                    <AlternatingItemTemplate>
                            <tr style='<%# (Common.CastAsInt32(Eval("DocketSubJobId"))== DocketSubJobId)? "background-color:#4DB8FF;color:white;" : "background-color:#FFF5E6" %>'>
                            <td style="text-align:center;width:3%;"><asp:ImageButton runat="server" OnClick="btnSelectSubJob_Click" CommandArgument='<%#Eval("DocketSubJobId")%>' ID="btnSelectSubJob" ImageUrl="~/Modules/PMS/Images/icon-view-blue_12.png" style="float:right" ToolTip="Edit Job" /></td>
                            <td style="text-align:left;width:15%;"><b><%#Eval("SubJobCode")%></b></td>
                            <td align="left" style="width:66%;"><div style=" white-space: nowrap; width: 375px; overflow: hidden;text-overflow: ellipsis;"><%#Eval("SubJobName")%></div></td>
                            <td style=" text-align:right;width:6%;"><%#Eval("BidQty")%></td>
                            <td style="width:6%;"><%#Eval("Unit")%></td>
                            <td style="width:2%;"><asp:ImageButton runat="server" Visible='<%#(Eval("AttachmentName").ToString().Trim()!="")%>' ID="imgDownload" ImageUrl="~/Modules/PMS/Images/paperclipx12.png" CommandArgument='<%#Eval("DocketSubJobId")%>' OnClick="imgDownload_Click" /></td>
                            <td>&nbsp;</td>
                            </tr>
                    </AlternatingItemTemplate>
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
                <asp:Button runat="server" ID="btnImportDefect" Text="Import Defect List from PMS" OnClick="btnImportDefect_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:180px;"  />
                <asp:Button runat="server" ID="btnDownloadSOR" Text="Download SOR" OnClick="btnDownloadSOR_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:130px;"  />
                <asp:Button runat="server" ID="btnPrint" Text="Print (Owner's Supply)" OnClick="btnPrintO_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:150px;"  />
                <asp:Button runat="server" ID="Button1" Text="Print (Shipyard Supply)" OnClick="btnPrintY_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:150px;"  />
                <asp:Button runat="server" ID="btnPublish" Text="Publish" OnClick="btnPublish_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
            </div>
         </div>
         <table style="width:100%" cellpadding="0" cellspacing="0">
         <tr style=" background-color:#99CCFF; font-size:15px;">
            <td style="padding:3px;"><b>Publishing History :</b></td>
         </tr>
         </table>
         <div style="border:solid 1px gray; text-align:left;font-size :11px;padding:3px; height:22px; overflow-y: scroll; overflow-x: hidden; background-color:#FFB84D; font-weight:bold;" class="ScrollAutoReset" id="Div2">
                <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;height:22px;border-collapse:collapse;">
                    <colgroup>
                        <col style="width:2%;" />
                        <col style="width:64%;"/>
                        <col style="width:20%;" />
                        <col style="width:10%;" />
                        <col style="width:2%;" />
                        <col style="width:2%;" />
                    </colgroup>
                    <tr class= "headerstylegrid">
                        <td style="width:2%;">Sr#</td>
                        <td style="width:64%;">DD #</td>
                        <td style="width:20%;">Published By</td>
                        <td style="width:10%;">Published On</td>
                        <td style="width:2%;"><img src="../Images/paperclipx12.png" /></td>
                        <td style="width:2%;">&nbsp;</td>
                    </tr>
                    </table>
          </div>
          <div style="border:solid 1px gray; text-align:left;font-size :11px;padding:3px; height:100px; overflow-y: scroll; overflow-x: hidden;border-bottom:none;" class="ScrollAutoReset" id="dv255">
                <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                 <colgroup>
                                    <col style="width:2%;" />
                        <col style="width:64%;"/>
                        <col style="width:20%;" />
                        <col style="width:10%;" />
                        <col style="width:2%;" />
                        <col style="width:2%;" />
                                </colgroup>
                                <asp:Repeater ID="rprPublishHistory" runat="server">
                                    <ItemTemplate>
                                            <tr>
                                            <td style="text-align:center;width:2%;"><%#Eval("SNO")%></td>
                                            <td style="text-align:left;width:64%;"><%#Eval("DocketNo")%>-<%#Eval("VersionNo")%></td>
                                            <td style="text-align:left;width:20%;"><%#Eval("PublishedBy")%></td>
                                            <td style="text-align:center;width:10%;"><%#Common.ToDateString(Eval("publishedOn"))%></td>
                                            <td style="width:2%;"><asp:ImageButton runat="server" ID="btn_Download_PublishedFile" CommandArgument='<%#Eval("TableId")%>' OnClick="btn_Download_PublishedFile_Click" ImageUrl="~/Modules/PMS/Images/paperclipx12.png" style="float:left; padding-left:5px" ToolTip="Add New Job"/></td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style='background-color:#FFF5E6;'>
                                            <td style="text-align:center;width:2%;"><%#Eval("SNO")%></td>
                                            <td style="text-align:left;width:64%;"><%#Eval("DocketNo")%>-<%#Eval("VersionNo")%></td>
                                            <td style="text-align:left;width:20%;"><%#Eval("PublishedBy")%></td>
                                            <td style="text-align:center;width:10%;"><%#Common.ToDateString(Eval("publishedOn"))%></td>
                                            <td style="width:2%;"><asp:ImageButton runat="server" ID="btn_Download_PublishedFile" CommandArgument='<%#Eval("TableId")%>' OnClick="btn_Download_PublishedFile_Click" ImageUrl="~/Modules/PMS/Images/paperclipx12.png" style="float:left; padding-left:5px" ToolTip="Add New Job"/></td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
               </table>
          </div>
          <div style="text-align:right ;padding:2px; ">
                    <span style="float:left;color:Red; font-size:14px;"><strong>Send</strong> <b>DD specs to Technical Manager for Approval.Click Notify.</b></span>
                    <asp:Button runat="server" ID="btnNotifyToGM" Text="Notify  Tech Manager" OnClick="btnNotifyGM_Click"  Width="130px" OnClientClick="return ConfirmNotify(this);" CommandArgument="4" CssClass='btn'/>
                    <asp:Button runat="server" ID="btnGMApproval" Text="Approval" OnClick="btnGMApproval_Click"  Width="130px" OnClientClick="return ConfirmApprove(this);"  CommandArgument="4" CssClass='btn'/>
          </div>
        </td> 
    </tr>
    </table>

    <%-- Add Sub Jobs --%>
    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_A_SubJobs" runat="server" visible="false">
    <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 800px; height: 540px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
                               <asp:UpdatePanel runat="server" id="UpdatePanel411">
                                <ContentTemplate>
                                 <div style="float:left; padding:4px; background-color:#99CCFF; width:99%;">
                                    <asp:RadioButton runat="server" ID="radNew" Checked="true" Text="Create New Job" GroupName="a" Font-Bold="true" AutoPostBack="true" Font-Size="13px" OnCheckedChanged="radMasterNew_OnCheckedChanged"/>
                                    <asp:RadioButton runat="server" ID="radMaster"  Text="Import from Master" GroupName="a" Font-Bold="true" AutoPostBack="true" Font-Size="13px" OnCheckedChanged="radMasterNew_OnCheckedChanged" />
                                 </div>   
                                 
                                 <asp:Panel runat="server" id="pnlNew">
                                   <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                   <tr>
                                        <td colspan="2" class="text headerband" >
                                            Create New Job
                                        </td>
                                   </tr>
                                   <tr>
                                        <td style="text-align:right; width:120px;">Short Descr :</td>
                                        <td style="text-align:left;"><asp:TextBox runat="server" ID="txt_A_SubJobName"  Width="99%" TextMode="MultiLine" Height="100px" ></asp:TextBox></td>                  
                                   </tr>
                                   <tr style="background-color:#DDF2FF;">
                                        <td style="text-align:right;">Long Descr :</td>
                                        <td style="text-align:left;">
                                            <asp:TextBox runat="server" ID="txt_A_LongDescr" TextMode="MultiLine" Width="99%" Height="150px" ></asp:TextBox>
                                        </td>
                                   </tr>
                                   <tr >
                                            <td style="text-align:right;">Bid Qty :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txt_A_SubJobBidQty"  Width="99%" MaxLength="50"  ></asp:TextBox></td>                  
                                   </tr>
                                   <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right;">Unit :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txt_A_SubJobunit"  Width="99%" MaxLength="50"  ></asp:TextBox></td>                  
                                   </tr>
                                   <tr>
                                        <td style="text-align:right;">Cost Category :</td>
                                        <td style="text-align:left;">
                                            <asp:RadioButton ID="rdo_A_YardCost" Text="Yard Cost" runat="server" GroupName="CC" Checked="true" />
                                            <asp:RadioButton ID="rdo_A_NonYardCost" Text="Owner Cost" runat="server" GroupName="CC" />
                                        </td>
                                   </tr>
                                   <tr style="background-color:#DDF2FF;">
                                        <td style="text-align:right;">Outside Repair :</td>
                                        <td style="text-align:left;">
                                            <asp:CheckBox ID="chk_A_OutsideRepair" runat="server"  />                                                       
                                        </td>
                                   </tr>
                                   <tr >
                                        <td style="text-align:right;">Required for Job Tracking :</td>
                                        <td style="text-align:left;"> 
                                            <asp:CheckBox ID="chk_A_ReqJT" runat="server" Checked="true"  />                                                       
                                        </td>
                                   </tr>
                                   <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right;">Attachment :</td>
                                            <td style="text-align:left;">
                                            <asp:FileUpload runat="server" id="ftp_A_Upload" />
                                            </td>                  
                                         </tr>
                                   </table>
                                 </asp:Panel>
                                 <asp:Panel runat="server" id="pnlMaster"  Visible="False">
                                 <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                 <tr>
                                        <td style="text-align: center;  font-size:14px; " class="text headerband" >
                                            List of Available Jobs - <asp:Label runat="server" ID="lblJobCode"></asp:Label>
                                        </td>
                                 </tr>
                                 <tr>
                                 <td>
                                 
                                  <div style="border:solid 1px gray; text-align:left;font-size :11px;padding:3px; height:410px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="Div1">
                                    <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                        <col style="width:2%;" />                                                                        
                                        <col style="width:10%;" />
                                        <col style="width:86%;"/>
                                        <col style="width:2%;" />
                                    </colgroup>
                                    <asp:Repeater ID="rpt_MasterSubJobs" runat="server">
                                        <ItemTemplate>
                                                <tr style=''>
                                                <td style="text-align:center;width:2%;"><asp:CheckBox runat="server" ID="chkSelect" CssClass=<%#Eval("SubJobId")%> /></td>
                                                <td style="text-align:center;width:10%;"><b><%#Eval("SubJobCode")%></b></td>
                                                <td align="left" style="width:86%;"><div style=" white-space: nowrap; width: 630px; overflow: hidden;text-overflow: ellipsis;"><%#Eval("SubJobName")%></div></td>
                                                <td>&nbsp;</td>
                                                </tr>
                                        </ItemTemplate>       
                                        <AlternatingItemTemplate>
                                                <tr style='background-color:#FFF5E6;'>
                                                <td style="text-align:center;width:2%;"><asp:CheckBox runat="server" ID="chkSelect" CssClass=<%#Eval("SubJobId")%> /></td>
                                                <td style="text-align:center;width:10%;"><b><%#Eval("SubJobCode")%></b></td>
                                                <td align="left" style="width:86%;"><div style=" white-space: nowrap; width: 630px; overflow: hidden;text-overflow: ellipsis;"><%#Eval("SubJobName")%></div></td>
                                                <td>&nbsp;</td>
                                                </tr>
                                        </AlternatingItemTemplate>
                                    </asp:Repeater>
                                </table>
                             </div>

                                 </td>
                                 </tr>
                                 </table>
                                 </asp:Panel>
                                 <div style="text-align:center; padding:5px;">
                                            <div style="text-align:left;float:left; padding-top:5px;">
                                                <asp:Label ID="lbl_A_MsgSubJob" ForeColor="Red" runat="server"></asp:Label>
                                            </div>
                                            <div style="text-align:left;float:right">
                                                <asp:Button runat="server" ID="btn_A_SaveJob" Text="Save" OnClick="btn_A_SaveJob_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                                <asp:Button runat="server" ID="btn_A_CloseJob" Text="Close" OnClick="btn_A_CloseJob_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                            </div>
                                </div>

                                </ContentTemplate>
                                <Triggers>
                                  <asp:PostBackTrigger ControlID="btn_A_CloseJob" />
                                  <asp:PostBackTrigger ControlID="btn_A_SaveJob" />
                                </Triggers>
                                </asp:UpdatePanel>
                            
                            </div>
                        </center>
    </div>
    <%-- View / Edit Sub Jobs --%>
    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_U_SubJobs" runat="server" visible="false">
    <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 900px; height: 550px; padding: 3px; text-align: center;background: white; z-index: 150; top: 30px; border: solid 10px gray;">
                               <asp:UpdatePanel runat="server" id="UpdatePanel1">
                                <ContentTemplate>
                                <div>
                                      <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                      <tr>
                                        <td  colspan="2" style="text-align: center;  font-size:14px;" class="text headerband" >
                                            Job Details 
                                        </td>
                                    </tr>

                                       <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; width:170px;">Job Code :</td>
                                            <td style="text-align:left;"><asp:Label runat="server" ID="lblSubJobCode"  ></asp:Label></td>
                            
                                        </tr>                                        
                                        <tr >
                                            <td style="text-align:right;">Short Descr :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtSubJobName"  Width="99%" TextMode="MultiLine" Height="100px" ></asp:TextBox></td>                  
                                         </tr>
                                         <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right;">Long Descr :</td>
                                            <td style="text-align:left;">
                                                <asp:TextBox runat="server" ID="txtLongDescr" TextMode="MultiLine" Width="99%" Height="170px" ></asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr >
                                            <td style="text-align:right;">Bid Qty :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtSubJobBidQty"  Width="99%" MaxLength="50"  ></asp:TextBox></td>                  
                                         </tr>
                                         <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right;">Unit :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtSubJobUnit"  Width="99%" MaxLength="50"  ></asp:TextBox></td>                  
                                         </tr>
                                         <tr>
                                            <td style="text-align:right;">Cost Category :</td>
                                            <td style="text-align:left;">
                                                <asp:RadioButton ID="rdoYardCost" Text="Shipyard Supply Costs" runat="server" GroupName="CC" Checked="true" />
                                                <asp:RadioButton ID="rdoNonYardCost" Text="Owner’s Supply Shipyard Costs" runat="server" GroupName="CC" />
                                            </td>
                                        </tr>
                                        <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right;">Outside Repair :</td>
                                            <td style="text-align:left;">
                                                <asp:CheckBox ID="chkOutsideRepair" runat="server"  />                                                       
                                            </td>
                                        </tr>
                                        <tr >
                                            <td style="text-align:right;">Required For Job Tracking :</td>
                                            <td style="text-align:left;">
                                                <asp:CheckBox ID="chkReqJT" runat="server"  />                                                       
                                            </td>
                                        </tr>
                                         <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right;">SOR :</td>
                                            <td style="text-align:left;">
                                            <asp:FileUpload runat="server" id="flp1" />
                                             <asp:Button runat="server" ID="btn_U_RemoveAttachment" Text="Remove Attachment" OnClick="btn_U_RemoveAttachment_Click" style=" padding:1px; border:none; color:White; background-color:Red; width:150px;" OnClientClick="return window.confirm('Are you sure to remove attachment ?.');"  />
                                            </td>                  
                                         </tr>
                                        </table>
                                        <div style="text-align:center; padding:5px;">
                                            <div style="text-align:left;float:left; padding-top:5px;">
                                                <asp:Label ID="lblMsgSubJob" ForeColor="Red" runat="server"></asp:Label>
                                            </div>
                                            <div style="text-align:left;float:right">
                                                <asp:Button runat="server" ID="btn_U_EditJob" Text="Edit" OnClick="btn_U_EditJob_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                                <asp:Button runat="server" ID="btn_U_SaveJob" Text="Save" OnClick="btn_U_SaveJob_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                                <asp:Button runat="server" ID="btn_U_DeleteJob" Text="Delete" OnClick="btn_U_DeleteJob_Click" OnClientClick="return window.confirm('Are you sure to delete this job ?');" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                                <asp:Button runat="server" ID="btn_U_CloseJob" Text="Close" OnClick="btn_U_CloseJob_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                            </div>
                                        </div>
                                </div>
                                </ContentTemplate>
                                <Triggers>
                                  <asp:PostBackTrigger ControlID="btn_U_CloseJob" />
                                  <asp:PostBackTrigger ControlID="btn_U_SaveJob" />
                                </Triggers>
                                </asp:UpdatePanel>
                            
                            </div>
                        </center>
    </div>

    <%-- Import Defects Section --%>
    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvImportPMS" runat="server" visible="false">
    <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 1000px; height: 510px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
                               <asp:UpdatePanel runat="server" id="UpdatePanel81">
                                <ContentTemplate>
                                <div>
                                      <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                      <tr>
                                        <td  style="text-align: center;  font-size:14px;" class="text headerband" >
                                            Open Defects List
                                        </td>
                                        </tr>
                                        <tr>
                                        <td >

                                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                               <col style="width:2%;" />
                                               <col style="width:5%;" />
                                               <col style="width:12%;" />
                                               <col style="width:43%;"/>
                                               <col style="width:10%;" />
                                               <col style="width:9%;" />
                                               <col style="width:11%;" />
                                               <%--<col style="width:110px;" />
                                               <col style="width:85px;" />--%>
                                               <col style="width:6%;" />
                                              <%-- <col style="width:90px;" />--%>
                                               <col style="width:2%;" />
                                               <tr align="left" class= "headerstylegrid">
                                               <td style="width:2%;">&nbsp;</td>
                                               <td style="width:5%;">Vessel</td>
                                               <td style="width:12%;">Component Code</td>
                                               <td style="width:43%;">Component Name</td>
                                               <td style="width:10%;">Defect #</td>
                                               <td style="width:9%;">Report Dt.</td>
                                               <td style="width:11%;">Target Closure Dt.</td>
                                              <%-- <td>Component Status</td>
                                               <td>Comp. Dt</td>--%>
                                               <td style="width:6%;">Status</td>
                                               <%--<td>RQN Dt.</td>--%>
                                               <td style="width:2%;">&nbsp;</td>    
                                               </tr>
                                            </colgroup>
                                            </table>     
                                            <div style="border:solid 1px gray; text-align:left;font-size :11px; height:200px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="Div3">
                                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                       <colgroup>
                                            <col style="width:2%;" />
                                            <col style="width:5%;" />
                                            <col style="width:12%;" />
                                            <col style="width:43%;"/>
                                            <col style="width:10%;" />
                                            <col style="width:9%;" />
                                            <col style="width:11%;" />
                                            <%--<col style="width:110px;" />
                                            <col style="width:85px;" />--%>
                                            <col style="width:6%;" />
                                            <%--<col style="width:90px;" />--%>
                                            <col style="width:2%;" />
                                            </colgroup>
                                       <asp:Repeater ID="rptDefects" runat="server">
                                          <ItemTemplate>
                                              <tr  title="Click to view details." class='<%# (Eval("Status").ToString()=="OD")?"highlightrow":"row" %>' >
                                                   <td style="width:2%;"><asp:CheckBox ID="chkSelectDef" Text="" CssClass='<%#Eval("DefectNo")%>' runat="server" /> </td>
                                                   <td align="left" style="width:5%;"> <%#Eval("VesselCode")%></td>
                                                   <td align="left" style="width:12%;" onclick="opendefectdetails('<%#Eval("DefectNo")%>')"> <span style="color:Blue;"><%#Eval("ComponentCode")%></span></td>
                                                   <td align="left" style="width:43%;"><%#Eval("ComponentName")%>
                                                   <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                                                   </td>
                                                   <td align="left" style="width:10%;" ><%#Eval("DefectNo")%></td>
                                                   <td align="left" style="width:9%;"><%#Eval("ReportDt")%></td>
                                                   <td align="center" style="width:11%;"><%#Eval("TargetDt")%></td>
                                                   <%--<td align="left"><%#Eval("CompStatus")%></td>                           
                                                   <td align="left"><%#Eval("CompletionDt")%></td>--%>
                           
                                                   <td align="center" style="width:6%;"><%#Eval("DefectStatus")%></td>
                                                  <%-- <td align="center"><%#Eval("RqnDate")%></td>--%>
                                                   <td>&nbsp;</td>
                                               </tr>
                                           </ItemTemplate>
                                          </asp:Repeater>
                                      </table>
                                        </div>

                                        </td>
                                        </tr>
                                        <tr>
                                          <td>                                          
                                                <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                        <tr >
                                            <td style="text-align:right; width:150px; font-weight:bold;">Docking Category :</td>
                                            <td style="text-align:left; "><asp:DropDownList runat="server" ID="ddlCat_Def" style="padding:3px;" width="500px" AutoPostBack="true" OnSelectedIndexChanged="ddlCat_Def_OnSelectedIndexChanged"></asp:DropDownList></td>                  
                                         </tr>
                                         <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right;  font-weight:bold; ">Job :</td>
                                            <td style="text-align:left;"><asp:DropDownList runat="server" ID="ddlJobs_Def" style="padding:3px;" width="500px" ></asp:DropDownList></td>                  
                                         </tr>
                                        <tr >
                                            <td style="text-align:right;  font-weight:bold; ">Bid Qty :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtDefBidQty"  Width="495px" MaxLength="50"  ></asp:TextBox></td>                  
                                         </tr>
                                         <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right;  font-weight:bold;">Unit :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtDefSubjobUnit"  Width="495px" MaxLength="50"  ></asp:TextBox></td>                  
                                         </tr>
                                         <tr>
                                            <td style="text-align:right; font-weight:bold;">Cost Category :</td>
                                            <td style="text-align:left;">
                                                <asp:RadioButton ID="rdoDefYardCost" Text="Shipyard Supply Costs" runat="server" GroupName="CCDef" Checked="true" />
                                                <asp:RadioButton ID="rdoDefNonYardCost" Text="Owner’s Supply Shipyard Costs" runat="server" GroupName="CCDef" />
                                            </td>
                                        </tr>
                                        <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; font-weight:bold;">Outside Repair :</td>
                                            <td style="text-align:left;">
                                                <asp:CheckBox ID="chkDefOutsideRepair" runat="server"  />                                                       
                                            </td>
                                        </tr>
                                        <tr >
                                            <td style="text-align:right; font-weight:bold;">Required for Job Tracking :</td>
                                            <td style="text-align:left;">
                                                <asp:CheckBox ID="chkDefReqJT" runat="server"  />                                                       
                                            </td>
                                        </tr>
                                        </table>
                                          </td>
                                        </tr>
                                    </table>
                                    <div style="text-align:center; padding:5px;">
                                            <div style="text-align:left;float:left; padding-top:5px;">
                                                <asp:Label ID="lblDefMsg" ForeColor="Red" runat="server"></asp:Label>
                                            </div>
                                            <div style="text-align:left;float:right">
                                                <asp:Button runat="server" ID="btnSaveDefect" Text="Save" OnClick="btnSaveDefect_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                                <asp:Button runat="server" ID="btnImpCancel" Text="Close" OnClick="btnImpCancel_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                            </div>
                                        </div>
                                </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSaveDefect" />
                                    <asp:PostBackTrigger ControlID="btnImpCancel" />
                                </Triggers>
                                </asp:UpdatePanel>
                           </div>6
    </center>
    </div>

    <%-- Import Job Category --%>
    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvImportJobCategory" runat="server" visible="false">
    <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 800px; height: 520px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
                               <asp:UpdatePanel runat="server" id="UpdatePanel2">
                                <ContentTemplate>
                                <div style="float:left; padding:4px; background-color:#99CCFF; width:99%; font-size:13px; font-weight:bold;">
                                    Import Job Category
                                </div>
                                 <asp:Panel runat="server" id="Panel2" >
                                 <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                 <tr>
                                        <td style="text-align: center;  font-size:14px;" class="text headerband" >
                                            List of Available Job Categories
                                        </td>
                                 </tr>
                                 <tr>
                                 <td>
                                 
                                  <div style="border:solid 1px gray; text-align:left;font-size :11px;padding:3px; height:410px; overflow-y: scroll; overflow-x: hidden;" class="ScrollAutoReset" id="Div7">
                                    <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                        <col style="width:2%;" />                                                                        
                                        <col style="width:10%;" />
                                        <col style="width:86%;"/>
                                        <col style="width:2%;" />
                                    </colgroup>
                                    <asp:Repeater ID="rptJobcategory" runat="server">
                                        <ItemTemplate>
                                                <tr style=''>
                                                <td style="text-align:center;width:2%;"><asp:CheckBox runat="server" ID="chkSelect" CssClass=<%#Eval("JobId")%> /></td>
                                                <td style="text-align:center;width:10%;"><b><%#Eval("JobCode")%></b></td>
                                                <td align="left" style="width:86%;"><div style=" white-space: nowrap; width: 630px; overflow: hidden;text-overflow: ellipsis;"><%#Eval("JobName")%></div></td>
                                                <td>&nbsp;</td>
                                                </tr>
                                        </ItemTemplate>       
                                        <AlternatingItemTemplate>
                                                <tr style='background-color:#FFF5E6;'>
                                                <td style="text-align:center;width:2%;"><asp:CheckBox runat="server" ID="chkSelect" CssClass=<%#Eval("JobId")%> /></td>
                                                <td style="text-align:center;width:10%;"><b><%#Eval("JobCode")%></b></td>
                                                <td align="left" style="width:86%;"><div style=" white-space: nowrap; width: 630px; overflow: hidden;text-overflow: ellipsis;"><%#Eval("JobName")%></div></td>
                                                <td>&nbsp;</td>
                                                </tr>
                                        </AlternatingItemTemplate>
                                    </asp:Repeater>
                                </table>
                             </div>

                                 </td>
                                 </tr>
                                 </table>
                                 </asp:Panel>
                                 <div style="text-align:center; padding:5px;">
                                            <div style="text-align:left;float:left; padding-top:5px;">
                                                <asp:Label ID="lblMsgImportJC" ForeColor="Red" runat="server"></asp:Label>
                                            </div>
                                            <div style="text-align:left;float:right">
                                                <asp:Button runat="server" ID="btnSaveJobCat" Text="Save" OnClick="btnSaveJobCat_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                                <asp:Button runat="server" ID="btnCloseJobCat" Text="Close" OnClick="btnCloseJobCat_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                            </div>
                                </div>

                                </ContentTemplate>
                                <Triggers>
                                  <asp:PostBackTrigger ControlID="btnCloseJobCat" />
                                  <asp:PostBackTrigger ControlID="btnSaveJobCat" />
                                </Triggers>
                                </asp:UpdatePanel>
                            
                            </div>
                        </center>
    </div>
     <%-- Add Sub Jobs --%>
    <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_ModifyJOb" runat="server" visible="false">
    <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 800px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
                               <asp:UpdatePanel runat="server" id="UpdatePanel22411">
                                <ContentTemplate>
                                <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                <tr><td style="text-align: center; font-size:14px; " class="text headerband" >Modify Job Description <asp:Label Text="" runat="server" ID="lblJobCode1"></asp:Label> </td></tr>
                                <tr>
                                    <td>
                                    <asp:TextBox runat="server" ID="txtDescr"  Width="100%" MaxLength="50" TextMode="MultiLine" Rows="20" ></asp:TextBox>                              
                                    </td>
                                </tr>
                                </table>
                                <div style="text-align:center;">
                                    <asp:Button runat="server" ID="btnSaveJob" Text="Save" OnClick="btnSaveJob_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                    <asp:Button runat="server" ID="btnCloseJob" Text="Close" OnClick="btnCloseJob_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                </div>
                                </ContentTemplate>
                                <Triggers>
                                  <asp:PostBackTrigger ControlID="btnSaveJob" />
                                  <asp:PostBackTrigger ControlID="btnCloseJob" />
                                </Triggers>
                                </asp:UpdatePanel>
                            </div>
    </center>
    </div>

    <script type="text/javascript">
    function ConfirmNotify(ctl)
    {
        if (window.confirm('Are you sure to notify to Technical Manager ?')) {
            $(ctl).val('Processing..');
            return true;
        }
        else
            return false;
    }
    //----------------------------
    function ConfirmApprove(ctl)
    {
        if (window.confirm('Are you sure to approve ?')) {
            $(ctl).val('Processing..');
            return true;
        }
        else
            return false;
    }
    </script>
    </div>
    </form>
</body>
</html>
