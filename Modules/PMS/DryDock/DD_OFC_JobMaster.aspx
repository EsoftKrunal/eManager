<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DD_OFC_JobMaster.aspx.cs" Inherits="DD_OFC_JobMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
 <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
 <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
 <title>eMANAGER</title>
  <script src="../JS/JQuery.js" type="text/javascript"></script>
<script src="../JS/Common.js" type="text/javascript"></script>
<script src="../JS/JQScript.js" type="text/javascript"></script>
      <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/style.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

        <div class="box_withpad" style="min-height:450px">
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>     
        <td style=" text-align :left; vertical-align : top;"> 
            <table style="width :100%" cellpadding="2" cellspacing="" border="0">
            <tr>
              <td style="vertical-align:middle; width:130px;">&nbsp;&nbsp;Docking Category :&nbsp;</td>
              <td colspan="0" style="padding:0px;vertical-align:middle">
                 <asp:DropDownList ID="ddlDDCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlDDCategory_OnSelectedIndexChanged" runat="server" Width="400px"></asp:DropDownList>
              </td>
              <td>
                 <asp:Button runat="server" ID="btnPrint" Text="Print" OnClick="btnPrint_Click" style="color:White; background-color:Red;" />           
              </td>
            </tr>
            </table>
            <table style="width :100%" cellpadding="1" cellspacing="1" border="0">
                <tr>
            <td style="width:40%;">    
                    
            <asp:UpdatePanel runat="server" id="up22" UpdateMode="Conditional">
            <ContentTemplate>
            <div class="dvScrollheader">  
            <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
            <tr>
            <td>
            <asp:ImageButton runat="server" ID="btnAddJob" OnClick="btnAddJob_Click" ImageUrl="~/Modules/PMS/Images/add.png" style="float:left"  ToolTip="Add New Job" Visible="false" />
            <asp:ImageButton runat="server" ID="btnEditJob" OnClick="btnEditJob_Click" Visible="false" ImageUrl="~/Modules/PMS/Images/editx16.png" style="float:left; padding-left:5px;" ToolTip="Edit Job" />
            <asp:ImageButton runat="server" ID="btnDeleteJob" OnClick="btnDeleteJob_Click" Visible="false" ImageUrl="~/Modules/PMS/Images/Delete.png" style="float:left; padding-left:5px;" ToolTip="Delete Job" />
            &nbsp;&nbsp;Job List
            </td>
            </tr>
            </table>
            </div>
            <div style="HEIGHT: 400px;" class="ScrollAutoReset dvScrolldata" id="df236" >
                        <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:44%;" />
                                    <col style="width:10%;" />
                                    <col style="width:44%;"/>
                                    <col style="width:2%;" />
                                </colgroup>
                                <asp:Repeater ID="rptJobs" runat="server">
                                    <ItemTemplate>
                                            <tr class='<%# (Common.CastAsInt32(Eval("JobId"))== JobId)? "activetr1" : "tr1" %> '>
                                            <td style="text-align:left;display:flex;margin-top:2px;"> 
                                                <asp:RadioButton ID="rdoSelect" OnCheckedChanged="btnSelectJob_Click" Checked='<%# (Common.CastAsInt32(Eval("JobId"))== JobId) %>'  GroupName="dd" JobId='<%#Eval("JobId")%>' AutoPostBack="true"  runat="server" /> <asp:Label ID="lblJobName" runat="server" Text='<%#Eval("JobName")%>' style="text-overflow:ellipsis; overflow:hidden; word-wrap:break-word;"></asp:Label>
                                                <%--<div style="height:14px; text-overflow:ellipsis; overflow:hidden; word-wrap:break-word;"></div>--%>

                                            </td>

                                            <td style="text-align:left;"><%#Eval("JobCode")%>&nbsp;</td>

                                            <td align="left" ><div style="height:14px; text-overflow:ellipsis; overflow:hidden;  width:350px;word-wrap:break-word;"><%#Eval("JobName")%></div><%--<asp:ImageButton runat="server" OnClick="btnSelectJob_Click" CommandArgument='<%#Eval("JobId")%>' ID="btnSelectJob" ImageUrl="~/Modules/PMS/Images/search_magnifier_12.png" style="float:right" ToolTip="Select Job" />--%> </td>
                                            
                                           </tr>
                                    </ItemTemplate>       
                                </asp:Repeater>
                            </table>

                        
                                 
            </div>
            <%-- Jobs  Add/ Edit Section --%>
                        <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_Jobs" runat="server" visible="false">
                        <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)"></div>
                            <div style="position: relative; width: 600px; height: 395px; padding: 0px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px orange;">
                               <asp:UpdatePanel runat="server" id="UpdatePanel1">
                                <ContentTemplate>
                                <div>
                                      <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                      <tr>
                                        <td colspan="2" class="text headerband" >
                                            Add/ Edit Job 
                                        </td>
                                    </tr>

                                       <tr >
                                            <td style="text-align:right; width:23%;">Job Code :</td>
                                            <td style="text-align:left;"><asp:Label runat="server" ID="txtJobCode"  ></asp:Label></td>
                            
                                        </tr> 
                                        <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right;">Job Name :Job Description :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtJobName"  Width="99%" ></asp:TextBox></td>                  
                                         </tr>
                                         <tr >
                                            <td style="text-align:right;">Job Description :Job Name :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" TextMode="MultiLine" Height="210px" ID="txtJobDesc"  Width="98%"></asp:TextBox></td>
                            
                                        </tr>
                                        <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right;">Component Code :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtComponentCode"  Width="99%" MaxLength="12"></asp:TextBox></td>                  
                                         </tr>
                                        </table>
                                        <div style="text-align:center; padding:2px;">
                                            <uc1:MessageBox runat="server" ID="lblMsgJob" />
                                        </div>
                                        <div style="text-align:center">
                                            <asp:Button runat="server" ID="btnSaveJob" Text="Save" OnClick="btnSaveJob_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                            <asp:Button runat="server" ID="btnCloseJob" Text="Close" OnClick="btnCloseJob_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                        </div>
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
            </ContentTemplate>
            </asp:UpdatePanel>


            </td>
            <td style="width:60%;">
            <asp:UpdatePanel runat="server" id="UpdatePanel2">
            <ContentTemplate>

                                <div class="dvScrollheader">  
                                <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                <tr>
                                <td>
                                    <asp:ImageButton runat="server" ID="imgAddSubJob" OnClick="btnAddSubJob_Click" Visible="false" ImageUrl="~/Modules/PMS/Images/add.png" style="float:left" ToolTip="Add Sub Job" />                                    
                                    &nbsp;&nbsp;Sub Job List
                                </td>
                                </tr>
                                </table>
                                </div>
                                <div>
                                      <div style="HEIGHT: 400px;" class="ScrollAutoReset, dvScrolldata" id="d1232">
                                        <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                                                    <colgroup>
                                                                        <col style="width:15px;" />                                                                        
                                                                        <col style="width:80px;" />
                                                                        <col/>
                                                                        <col style="width:17px;" />
                                                                    </colgroup>
                                                                    <asp:Repeater ID="rptSubJobs" runat="server">
                                                                        <ItemTemplate>
                                                                                <tr class='<%# (Common.CastAsInt32(Eval("SubJobId"))== SubJobId)? "activetr1" : "tr1" %> '>
                                                                                <td style="text-align:center"><asp:ImageButton runat="server" OnClick="btnSelectSubJob_Click" JobId='<%#Eval("JobId")%>' CommandArgument='<%#Eval("SubJobId")%>' ID="btnSelectSubJob" ImageUrl="~/Modules/PMS/Images/icon-view-blue_12.png" style="float:right" ToolTip="Edit Job" /></td>
                                                                                <td style="text-align:left; "><%#Eval("SubJobCode")%></td>
                                                                                <td align="left"><div style="height:14px; text-overflow:ellipsis; overflow:hidden; white-space: nowrap; width:480px;"><%#Eval("SubJobName")%></div></td>                                                                                
                                                                                <td>&nbsp;</td>
                                                                               </tr>
                                                                        </ItemTemplate>       
                                                                    </asp:Repeater>
                                                                </table>
                                      </div>
                                </div>
                                 <%-- Sub Jobs  Add/ Edit Section --%>
                                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvAddEditSubJob" runat="server" visible="false">
                                <center>
                                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)"></div>
                                <div style="position: relative; width: 800px; height: 440px; padding: 0px; text-align: center;background: white; z-index: 150; top: 20px; border: solid 10px orange;">
                                <asp:UpdatePanel runat="server" id="up123">
                                <ContentTemplate>
                                    <center>

                                        <table width="100%" cellpadding="2" cellspacing="0" border="1" class="formborder" style="border-collapse:collapse;">
                                        <tr>
                                        <td  colspan="2" class="text headerband" >
                                            Add/ Edit Sub Job 
                                        </td>
                                        </tr>
                                        <tr >
                                            <td style="text-align:right; "><b>Job Code :</b></td>
                                            <td style="text-align:left;"><asp:Label runat="server" ID="txtSubJobCode" ></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right;"><b>Short Descr :</b></td>
                                            <td style="text-align:left;">
                                            <asp:TextBox runat="server" ID="txtSubJobName" TextMode="MultiLine" Width="99%" Height="110px" ></asp:TextBox>
                                            </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;"><b>Long Descr :</b></td>
                                            <td style="text-align:left;">
                                            <asp:TextBox runat="server" ID="txtLongDescr" TextMode="MultiLine" Width="99%" Height="110px" ></asp:TextBox>
                                            </td>
                                                            
                                            </tr>
                                            <tr>
                                            <td style="text-align:right;"><b>Unit :</b></td>
                                            <td style="text-align:left;">
                                            <asp:TextBox runat="server" ID="txtSubjobUnit"  Width="99%" MaxLength="50" ></asp:TextBox>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="text-align:right;"><b>Cost Category :</b></td>
                                            <td style="text-align:left;">
                                                <asp:RadioButton ID="rdoYardCost" Text="Shipyard Supply Costs" runat="server" GroupName="CC" Checked="true" />
                                                <asp:RadioButton ID="rdoNonYardCost" Text="Owner’s Supply Shipyard Costs" runat="server" GroupName="CC" />
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="text-align:right;"><b>Outside Repair :</b></td>
                                            <td style="text-align:left;">
                                                <asp:CheckBox ID="chkOutsideRepair" runat="server"  />                                                       
                                            </td>
                                            </tr>
                                            </tr>
                                            <tr>
                                                <td style="text-align:right;"><b>Required For Job Tracking :</b></td>
                                                <td style="text-align:left;">
                                                    <asp:CheckBox ID="chkReqJT" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="text-align:center; padding:2px;">
                                            <uc1:MessageBox runat="server" ID="lblMsg_SubJob" />
                                        </div>
                                        <div style="text-align:center; padding:3px;">
                                        <asp:Button runat="server" ID="btnEditSubJob" Text="Edit" OnClick="btnEditSubJob_Click" style=" padding:3px; border:none;  width:80px; color:White; background-color:Red;"   />
                                        <asp:Button runat="server" ID="btnSaveSubJob" Text="Save" Visible="false" OnClick="btnSaveSubJob_Click" style=" padding:3px; border:none;  width:80px; color:White; background-color:Red;"   />
                                        <asp:Button runat="server" ID="btnDeleteSubJob" Text="Delete" OnClick="btnDeleteSubJob_Click" style=" padding:3px;  width:80px; color:White; background-color:Red;"   />
                                        <asp:Button runat="server" ID="btnCloseSubJob" Text="Close" OnClick="btnCloseSubJob_Click" style=" padding:3px; border:none; width:80px; color:White; background-color:Red;"   />
                                        </div>
                                    </div>
            </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveSubJob" />
            <asp:PostBackTrigger ControlID="btnCloseSubJob" />
            </Triggers>
            </asp:UpdatePanel>
            </div>
            </center>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            </table>

 </div>       
    </div>
    </form>
</body>
</html>
