<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselWiseFollowUpList.aspx.cs" Inherits="FormReporting_VesselWiseFollowUpList" ValidateRequest="False" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Deficiencies for FollowUp</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <%--<link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />--%>
</head>
<script language="javascript" type="text/javascript">
      function CheckOnOff(rdoId,gridName)
      {
        var rdo = document.getElementById(rdoId);
        var str1=rdo.id;  
        //-------------------- 
        var str2=str1.replace(/rd_select/,"hfd_ObsvId");
        document.getElementById("hid1").value= document.getElementById(str2).value;
        //--------------------
        //-------------------- 
        var str3=str1.replace(/rd_select/,"hfd_TblName");
        document.getElementById("hid2").value= document.getElementById(str3).value;
        //--------------------
        var str=str1.replace(/rd_select/,"lblid");       
        document.getElementById("hid").value= document.getElementById(str).innerHTML; 
        /* Getting an array of all the "INPUT" controls on the form.*/
        var all = document.getElementsByTagName("input");
        for(i=0;i<all.length;i++)
        {  
            /*Checking if it is a radio button, and also checking if the
            id of that radio button is different than "rdoId" */
            if(all[i].type=="radio" && all[i].id != rdo.id)
            {
                var count=all[i].id.indexOf(gridName);
                if(count!=-1)
                {
                   all[i].checked=false;
                }
            }
         }
         rdo.checked=true;/* Finally making the clicked radio button CHECKED */
    }
    function OpenDeficiencyDetailWindow(inspdueid,obsid,tblnameflag)
    {
        var InspectionDueId=inspdueid;
        var ObservationId=obsid;
        var TblFlag=tblnameflag;
        if((!(parseInt(InspectionDueId)==0 || InspectionDueId=="")) || (!(parseInt(ObservationId)==0 || ObservationId=="")))
        {
            window.open('..\\Tracker\\DeficiencyDetailPopUp.aspx?INSPDId='+InspectionDueId+'&OBSVTNID='+ObservationId+'&TBLFLAG='+TblFlag,'uiop','title=no,toolbars=no,scrollbars=yes,width=840,height=510,left=240,top=100,addressbar=no');        
        }
        else
        {
            alert("Please select a Deficiency.");
        }
    }
    function OpenClosureWindow(inspdueid,obsid,tblnameflag)
    {
        var InspectionDueId=inspdueid;
        var ObservationId=obsid;
        var TblFlag=tblnameflag;
        if((!(parseInt(InspectionDueId)==0 || InspectionDueId=="")) || (!(parseInt(ObservationId)==0 || ObservationId=="")))
        {
            window.open('..\\Tracker\\ClosurePopUp.aspx?INSPDId='+InspectionDueId+'&OBSVTNID='+ObservationId+'&TBLFLAG='+TblFlag,'uiopa','title=no,toolbars=no,scrollbars=yes,width=840,height=320,left=240,top=100,addressbar=no');        
        }
        else
        {
            alert("Please select a Deficiency.");
        }
    }
    function OpenFollowUpTrackerReport(vesselid)
    {
        var VesselId=vesselid;
        var FollowUpCategoryId=document.getElementById("HiddenField_FollowUpCat").value;
        var FFromDate=document.getElementById("HiddenField_FrmDate").value;
        var FToDate=document.getElementById("HiddenField_ToDate").value;
        var DueInDays=document.getElementById("HiddenField_DueInDays").value;
	    var Status=document.getElementById("HiddenField_Status").value;
	    var Critical=document.getElementById("HiddenField_Critical").value;
	    var Responsibility=document.getElementById("HiddenField_Responsibility").value; 
	    var OverDue=document.getElementById("HiddenField_OverDue").value; 
        if(!(parseInt(VesselId)==0 || VesselId==""))
        {
            window.open('..\\Reports\\FollowupTracker_Report.aspx?FPVslId='+VesselId+'&FPCatgryID='+FollowUpCategoryId+'&FPFromDate='+FFromDate+'&FPTDate='+FToDate+'&FPDueDays='+DueInDays+'&FPStatus='+Status+'&FPCritical='+Critical+'&FPResponsibility='+Responsibility+'&FPOverDue='+OverDue,'uiopp','title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');        
        }
    }
</script>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div align="center" style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:right; background-color:#f9f9f9">
            <tr>
                <td class="text headerband" colspan="11" style="height: 23px;  text-align: center">
                    Deficiencies for FollowUp</td>
                    <asp:TextBox Style="display: none" ID="hid" runat="server" /><asp:TextBox Style="display: none" ID="hid1" runat="server" /><asp:TextBox Style="display: none" ID="hid2" runat="server" /></tr>
            <tr>
                <td colspan="11" style="text-align: center">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
                    <asp:TextBox ID="txt_VesselId" runat="server" style="display: none"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;</td>
                <td style="width: 122px">
                </td>
                <td style="text-align: left">
                    </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 3px; text-align: right">
                    OverDue:</td>
                <td style="padding-right: 3px; text-align: left">
                    <asp:CheckBox ID="chk_OverDue" runat="server" AutoPostBack="True" OnCheckedChanged="chk_OverDue_CheckedChanged" /></td>
                <td style="text-align: right; padding-right: 3px; width: 122px;">
                    Due in Days:</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_DueDays" runat="server" CssClass="input_box" Width="50px" MaxLength="5"></asp:TextBox></td>
                <td style="text-align: right; padding-right: 3px;">
                    Status:</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddl_Status" runat="server" CssClass="input_box" Width="74px">
                        <asp:ListItem Value="A">All</asp:ListItem>
                        <asp:ListItem Value="1">Closed</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">Open</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="text-align: right">
                    Critical:</td>
                <td style="text-align: left">
                    <asp:CheckBox ID="chk_Critical" runat="server" /></td>
                <td style="text-align: right; padding-right: 3px;">
                    Responsibility:</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddl_Resp" runat="server" CssClass="input_box" Width="74px">
                        <asp:ListItem Value="A">All</asp:ListItem>
                        <asp:ListItem>Office</asp:ListItem>
                        <asp:ListItem>Vessel</asp:ListItem>
                        <asp:ListItem Value="Vessel,Office">Both</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="text-align: right; padding-right: 8px;">
                    <asp:Button ID="btn_Show" runat="server" CssClass="btn" Text="Show" Width="59px" OnClick="btn_Show_Click" />&nbsp;<asp:Button
                        ID="btn_Reset" runat="server" CssClass="btn" OnClick="btn_Reset_Click" Text="Reset"
                        Width="59px" /></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td style="width: 122px">
                </td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 3px; text-align: right">
                    Vessel Name:</td>
                <td colspan="9" style="text-align: left">
                    <asp:Label ID="lbl_VesselName" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>&nbsp;
                </td>
                <td style="padding-right: 8px; text-align: right">
                    <asp:TextBox ID="TextBox1" runat="server" BackColor="#FFCCCC" CssClass="input_box"
                        Enabled="False" ReadOnly="True" Width="14px"></asp:TextBox>
                    <em>
                    OverDue Items</em>&nbsp;</td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td style="width: 122px">
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="11" style="padding-right: 5px; padding-left: 5px; text-align: left">
                <asp:UpdatePanel ID="updlnk" runat="server">
                   <ContentTemplate>
                    <%--<asp:LinkButton id="lnk" runat="server" Text="" OnClick="lnk_Click" ></asp:LinkButton>--%>
                    <asp:GridView ID="Grd_VslFollowUpDetail" runat="server" AllowPaging="True" AutoGenerateColumns="False" GridLines="Horizontal" HeaderStyle-Wrap="false" PageSize="9" Style="text-align: center" Width="100%" OnPageIndexChanging="Grd_VslFollowUpDetail_PageIndexChanging" AllowSorting="true" OnSorted="Grd_VslFollowUpDetail_Sorted" OnSorting="Grd_VslFollowUpDetail_Sorting" OnPreRender="Grd_VslFollowUpDetail_PreRender">
                        <RowStyle CssClass="rowstyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rd_select" OnClick="javascript:CheckOnOff(this.id,'Grd_VslFollowUpDetail');" GroupName="asdf" runat="server" Enabled='<%# (Eval("InspDueId").ToString().Trim()=="")?false:true %>' />
                                    <asp:Label ID="lblid" runat="server" Style="display: none" Text='<%# Eval("InspDueId") %>'></asp:Label>
                                    <asp:HiddenField ID="hfd_InspDueId" runat="server" Value='<%# Eval("InspDueId") %>' />
                                    <asp:HiddenField ID="hfd_ObsvId" runat="server" Value='<%# Eval("ObsvId") %>' />   
                                    <asp:HiddenField ID="hfd_TblName" runat="server" Value='<%# Eval("TblName") %>' />
                                    <asp:HiddenField ID="hfd_Closed" runat="server" Value='<%# Eval("Closed") %>' />                                        
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" Wrap="false" VerticalAlign="top"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deficiency">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Def" runat="server" Text='<%# Eval("Deficiency") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source" SortExpression="AA,BB">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Source" runat="server" Text='<%# Eval("Source") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date" SortExpression="TGCLOSEDT">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_DueDate" runat="server" Text='<%# Eval("TargetCloseDate") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="110px" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Responsibility">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Resp" runat="server" Text='<%# Eval("Responsibility") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Completion Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_CompDate" runat="server" Text='<%# Eval("CompletionDate") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="False" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="selectedtowstyle" />
                        <HeaderStyle CssClass="headerstylefixedheadergrid" ForeColor="#0E64A0" Wrap="False" />
                        <FooterStyle HorizontalAlign="Center" />
                    </asp:GridView>
                   </ContentTemplate>
                </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="1" style="padding-left: 5px; height: 18px; text-align: left">
                </td>
                <td colspan="1" style="padding-left: 5px; height: 18px; text-align: left">
                </td>
                <td colspan="3" style="padding-left: 5px; height: 18px; text-align: left">
                </td>
                <td style="height: 18px">
                    &nbsp;</td>
                <td style="height: 18px">
                </td>
                <td style="height: 18px">
                </td>
                <td style="height: 18px">
                </td>
                <td style="height: 18px">
                </td>
                <td style="padding-right: 8px; height: 18px; text-align: right">
                </td>
            </tr>
            <tr>
                <td colspan="5" style="padding-left: 5px; height: 18px; text-align: left">
                    <asp:Label ID="lbl_RecordCount" runat="server"></asp:Label></td>
                <td style="height: 18px">
                    &nbsp;</td>
                <td style="height: 18px">
                </td>
                <td style="height: 18px">
                </td>
                <td style="height: 18px">
                </td>
                <td style="height: 18px; padding-right: 5px; text-align: right;" colspan="2">
                    <asp:Button ID="btn_View" runat="server" CssClass="btn" Text="Open/Modify" Width="85px" OnClientClick='return OpenDeficiencyDetailWindow(document.getElementById("hid").value,document.getElementById("hid1").value,document.getElementById("hid2").value);' />
                    <asp:Button ID="btn_Closure" runat="server" CssClass="btn" Text="Closure" Width="59px" OnClientClick='return OpenClosureWindow(document.getElementById("hid").value,document.getElementById("hid1").value,document.getElementById("hid2").value);' />
                    <asp:Button ID="btn_Print" runat="server" CssClass="btn" Text="Print" Width="59px" OnClientClick='return OpenFollowUpTrackerReport(document.getElementById("txt_VesselId").value);' /></td>
            </tr>
            <tr>
                <td colspan="1" style="padding-left: 5px; height: 18px; text-align: left">
                </td>
                <td colspan="1" style="padding-left: 5px; height: 18px; text-align: left">
                </td>
                <td colspan="3" style="padding-left: 5px; height: 18px; text-align: left">
                </td>
                <td style="height: 18px">
                    &nbsp;</td>
                <td style="height: 18px">
                </td>
                <td style="height: 18px">
                </td>
                <td style="height: 18px">
                </td>
                <td style="height: 18px">
                </td>
                <td style="padding-right: 8px; height: 18px; text-align: right">
                </td>
            </tr>
            <tr>
                <td colspan="11">
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txt_DueDays">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:HiddenField ID="HiddenField_FollowUpCat" runat="server" />
                    <asp:HiddenField ID="HiddenField_FrmDate" runat="server" />
                    <asp:HiddenField ID="HiddenField_ToDate" runat="server" />
                    <asp:HiddenField ID="HiddenField_DueInDays" runat="server" />
                    <asp:HiddenField ID="HiddenField_Status" runat="server" />
                    <asp:HiddenField ID="HiddenField_Critical" runat="server" />
                    <asp:HiddenField ID="HiddenField_Responsibility" runat="server" />
                    <asp:HiddenField ID="HiddenField_OverDue" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
