<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobTrackingRemarkEntry.aspx.cs" Inherits="DryDock_JobTrackingRemarkEntry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <%--<link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />--%>
    <title>DryDock : Job Tracking </title>
    <script src="JS/JQuery.js" type="text/javascript"></script>
    <script src="JS/Common.js" type="text/javascript"></script>
    <script src="JS/JQScript.js" type="text/javascript"></script>

    <%--<link rel="stylesheet" href="http://code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css">
	<script src="http://code.jquery.com/jquery-1.10.2.js"></script>
	<script src="http://code.jquery.com/ui/1.11.2/jquery-ui.js"></script>--%>
    
    <style type="text/css">
    .hover_highlight_cat
     {
         vertical-align:middle;
         color:Red;
         height:17px;
         cursor:pointer;
         border-bottom:solid 1px #e2e2e2;
     }
    .hover_highlight_cat_active
    {
        background-color:Orange;
        color:Black;
    }
    
    
    .hover_highlight_job
     {
         vertical-align:middle;
         color:blue;
         height:17px;
         cursor:pointer;
         border-bottom:solid 1px #e2e2e2;
     }
     
  .hover_highlight_subjob
     {
         vertical-align:middle;
         color:Black;
         height:17px;
         cursor:pointer;
         border-bottom:solid 1px #e2e2e2;
         
       
     }
     
   .hover_highlight_subjob_active
    {
        background-color:Orange;
    }
    
    .columnHead
    {
        background-color:#ADD6FF;
        height:50px;
        color:Black;
    }
    .nav
    {
        margin:0px;
        padding:0px;
        width:100%;
    }
    .inactive_tab
    {
        padding:4px;
        background-color:White;
        border:solid 1px #4D70DB;
        border-bottom:solid 1px white;
        display:block;
        position:relative;
        float:left;
        margin-right:4px;
        color:Black;
    }
     .inactive_tab a
    {
        color:Grey;
    }
    .active_tab
    {
        padding:4px;
        background-color:#4D70DB;
        border:solid 1px #4D70DB;
        display:block;
        position:relative;
        float:left;
        margin-right:4px;
        border-bottom:solid 1px #4D70DB;
    }
     .active_tab a
    {
        color:White;
    }
    
    .ui-selecting { background: #FFFF75; }
	.ui-selected { background: #FFFF66; color: Blue; }
	.ui-selectedg { background: #96CD96; color: Blue; }
	.ui-selectednone { background:white; color: Blue; }
	.plancss
	{
	    background-color:Yellow;
	}
	
	ol { list-style-type: none; margin: 0; padding: 0; width: 60%; }
	li { width:50px; float:left; text-align:center; height:15px;font-size:9px;border:solid 1px #e2e2e2; border-left:none;border-top:none; border-bottom:none; margin:0px ; padding:0px;}
	</style>
</head>
<body>
<div id="dvMain"  style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40); display:none; ">
<div style="font-size:25px; font-weight:bold; text-align:center; vertical-align:middle; color:White; top:400px; padding:200px 0px;">Loading.......</div></div> 
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="background-color:#1947A3;color:White; text-align:center; padding:3px; font-size:15px; "><b>Job Progress : <asp:Label ID="txtDate" runat="server" MaxLength="17" Text="" ></asp:Label></b></div>
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="1">
        <tr>
        <td>
            <div style="border:none; background-color : #ADD6FF; padding:5px; font-size:13px; ">
                <table style="width :100%;">
                <tr>
                    <td style="text-align:right; width:120px;">Docket # :</td>
                    <td style="text-align:left"><asp:Label runat="server" ID="lblDocketNo"></asp:Label></td>
                    <td style="text-align:right">Vessel :</td>
                    <td style="text-align:left"><asp:Label runat="server" ID="lblVessel"></asp:Label></td>
                    <td style="text-align:right">Type :</td>
                    <td style="text-align:left"><asp:Label runat="server" ID="lblType"></asp:Label></td>
                    <td style="text-align:right">DD Duration :</td>
                    <td style="text-align:left"><asp:Label runat="server" ID="lblPlanDuration"></asp:Label>&nbsp;</td>
                </tr>
                </table>
            </div>
        </td>
        </tr>
        <tr>        
        <td style=" text-align :left; vertical-align : top;" > 
         <table cellpadding="0" cellspacing="0" style="width:100%;" >
         <tr>
            <td >
                <div style="background-color:#ADD6FF;color:Black; height:35px;" >
                    <div style="width:100%; padding-top:5px;">
                    &nbsp;&nbsp;<b>Job Category : </b><asp:Label runat="server" ID="lblJObCat" ></asp:Label>
                    
                    <%--<asp:ImageButton runat="server" ID="img_Refresh" OnClick="Refresh_Click" OnClientClick="this.src='../Images/spinner_16.gif';" ImageUrl="~/Images/refresh_16x16.png" />--%>
                    </div>
                    
                </div>

                <div class="dvScrollheader">  
                    <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px;" bordercolor="white">                           
                         <colgroup>
                            <col style="width:450px;" />
                            <col style="width:110px;" />
                            <col style="width:125px;" />
                            <col />
                            <col style="width:70px;" />
                            <col style="width:20px;" />
                        </colgroup>

                            <tr>
                            <td style="text-align:left;"><b>Job Name </b></td>
                            <td style="text-align:left;"><b>Commenced Dt.</b></td>
                            <td style="text-align:left;"><b>Est. Completion Dt.</b></td>
                            <td style="text-align:left;"><b>Remarks</b></td>
                            <td style="text-align:center;"><b>Completed</b></td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>

                <div  class="ScrollAutoReset, dvScrolldata" style="HEIGHT: 420px ; text-align:center; overflow-y:scroll;overflow-x:hidden;">
                    <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                            <col style="width:450px;" />
                            <col style="width:110px;" />
                            <col style="width:125px;" />
                            <col />
                            <col style="width:70px;" />
                            <col style="width:20px;" />
                        </colgroup>
                        <asp:Repeater ID="rptRFQJobs" runat="server">
                            <ItemTemplate>
                                    <tr>                                                                              
                                        <td style="text-align:left"><%#Eval("JobCode")%> : <%#Eval("JobName")%> <asp:HiddenField ID="hfDocketjobId" Value='<%#Eval("DocketJobId")%>' runat="server" /></td>
                                        <td align="center"><asp:TextBox ID="txtCommencedDate" Text='<%# Common.ToDateString(Eval("PlanFrom"))%>' MaxLength="12" Width="100px" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCommencedDate" Format="dd-MMM-yyyy" ></asp:CalendarExtender>   
                                        </td>
                                        <td align="center"><asp:TextBox ID="txtEstCompDate" Text='<%# Common.ToDateString(Eval("PlanTo"))%>' MaxLength="12" Width="100px" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEstCompDate" Format="dd-MMM-yyyy" ></asp:CalendarExtender>   
                                        </td>                                        
                                        <td align="left"><asp:TextBox ID="txtRemarks" Text='<%#Eval("Remark")%>' TextMode="MultiLine" Height="25px" Width="98%" runat="server"></asp:TextBox></td>
                                        <td align="center"><asp:CheckBox ID="chk_Select" Checked='<%#Common.CastAsInt32(Eval("ExecPer")) == 100%>' runat="server"></asp:CheckBox> </td>
                                        <td>&nbsp;</td>
                                    </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                     <tr style="background-color:#FFF0B2;">                                                                              
                                        <td style="text-align:left"><%#Eval("JobCode")%> : <%#Eval("JobName")%> <asp:HiddenField ID="hfDocketjobId" Value='<%#Eval("DocketJobId")%>' runat="server" /></td>
                                        <td align="center"><asp:TextBox ID="txtCommencedDate" Text='<%# Common.ToDateString(Eval("PlanFrom"))%>' MaxLength="12" Width="100px" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCommencedDate" Format="dd-MMM-yyyy" ></asp:CalendarExtender>   
                                        </td>
                                        <td align="center"><asp:TextBox ID="txtEstCompDate" Text='<%# Common.ToDateString(Eval("PlanTo"))%>' MaxLength="12" Width="100px" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEstCompDate" Format="dd-MMM-yyyy" ></asp:CalendarExtender>   
                                        </td>                                        
                                        <td align="left"><asp:TextBox ID="txtRemarks" Text='<%#Eval("Remark")%>' TextMode="MultiLine" Height="25px" Width="98%" runat="server"></asp:TextBox></td>
                                        <td align="center"><asp:CheckBox ID="chk_Select" Checked='<%#Common.CastAsInt32(Eval("ExecPer")) == 100%>' runat="server"></asp:CheckBox> </td>
                                        <td>&nbsp;</td>
                                    </tr>
                            </AlternatingItemTemplate>       
                        </asp:Repeater>
                    </table>
                </div>
                
            </td>
         </tr>
         </table>
         
         
            <div style="text-align:center; padding:5px;">
                 <asp:Label ID="lblMsgMain" ForeColor="Red" runat="server"></asp:Label>
            </div>
        </td> 
    </tr>
    </table>
    </div>
    <div style="padding:3px; text-align:center">
            
            <asp:Button runat="server" ID="btn_Save" Text=" Save " OnClientClick="this.value='Processing..';$('#dvMain').show();" OnClick="btn_Save_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:150px;"  />
            <asp:Button runat="server" ID="btn_Cancel" Text=" Close " style=" padding:3px; border:none; color:White; background-color:red; width:150px;" OnClientClick="window.close();return false;" />
    </div>  


     
    
    </form>
</body>
</html>
