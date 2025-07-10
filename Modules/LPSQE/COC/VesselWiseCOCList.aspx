<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselWiseCOCList.aspx.cs" Inherits="FormReporting_VesselWiseCOCList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
    
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
</head>
<script language="javascript" type="text/javascript">
      function CheckOnOff(rdoId,gridName)
      {
        var rdo = document.getElementById(rdoId);
        var str1=rdo.id;  
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
    function OpenModifyCOCWindow(COCid)
    {
        var COCId=COCid;
        if(!(parseInt(COCId)==0 || COCId==""))
        {
            window.open('..\\COC\\ModifyCOCPopUp.aspx?COC_Id='+COCId,'MCOC','title=no,toolbars=no,scrollbars=yes,width=840,height=500,left=240,top=100,addressbar=no');        
        }
        else
        {
            alert("Please select a COC.");
        }
    }
    function OpenCOCClosureWindow(COCid)
    {
        var COCId=COCid;
        if(!(parseInt(COCId)==0 || COCId==""))
        {
            window.open('..\\COC\\COCClosurePopUp.aspx?COC_Id='+COCId,'CCOC','title=no,toolbars=no,scrollbars=yes,width=840,height=320,left=240,top=100,addressbar=no');        
        }
        else
        {
            alert("Please select a COC.");
        }
    }
    function OpenCOCTrackerReport(vesselid)
    {
        var VesselId=vesselid;
        var NFromDate=document.getElementById("HiddenField_FrmDate").value;
        var NToDate=document.getElementById("HiddenField_ToDate").value;
        var NDueInDays=document.getElementById("HiddenField_DueInDays").value;
	    var NStatus=document.getElementById("HiddenField_Status").value;
	    var COCitical=document.getElementById("HiddenField_Critical").value;
	    var NResponsibility=document.getElementById("HiddenField_Responsibility").value; 
	    var NOverDue=document.getElementById("HiddenField_OverDue").value; 
	    alert("Pending");
    }
</script>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div align="center" style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:right; background-color:#f9f9f9">
            <tr>
                <td class="text headerband" colspan="9" style="height: 23px;  text-align: center">
                    COC Details</td>
                    <asp:TextBox Style="display: none" ID="hid" runat="server" /></tr>
            <tr>
                <td colspan="9" style="text-align: center">
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
                        ID="btn_Reset" runat="server" CssClass="btn" Text="Reset"
                        Width="59px" OnClick="btn_Reset_Click" /></td>
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
            </tr>
            <tr>
                <td style="padding-right: 3px; text-align: right">
                    Vessel Name:</td>
                <td colspan="7" style="text-align: left">
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
            </tr>
            <tr>
                <td colspan="9" style="padding-right: 5px; padding-left: 5px; text-align: left">
                <asp:UpdatePanel ID="updlnk" runat="server">
                   <ContentTemplate>
                    <%--<asp:LinkButton id="lnk" runat="server" Text="" OnClick="lnk_Click" ></asp:LinkButton>--%>
                    <asp:GridView ID="Grd_VslCOCDetail" runat="server" AllowPaging="True" AutoGenerateColumns="False" GridLines="Horizontal" HeaderStyle-Wrap="false" PageSize="9" Style="text-align: center" Width="100%" OnPageIndexChanging="Grd_VslCOCDetail_PageIndexChanging" AllowSorting="true" OnSorted="Grd_VslCOCDetail_Sorted" OnSorting="Grd_VslCOCDetail_Sorting" OnPreRender="Grd_VslCOCDetail_PreRender" OnRowDeleting="Grd_VslCOCDetail_RowDeleting">
                        <RowStyle CssClass="rowstyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rd_select" OnClick="javascript:CheckOnOff(this.id,'Grd_VslCOCDetail');" GroupName="asdf" runat="server" Enabled='<%# (Eval("COCID").ToString().Trim()=="")?false:true %>' />
                                    <asp:Label ID="lblid" runat="server" Style="display: none" Text='<%# Eval("COCID") %>'></asp:Label>
                                    <asp:HiddenField ID="hfd_COCId" runat="server" Value='<%# Eval("COCID") %>' />
                                    <asp:HiddenField ID="hfd_Closed" runat="server" Value='<%# Eval("Closed") %>' />                                        
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" Wrap="False" VerticalAlign="Top"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View">
                                <ItemTemplate>
                                    <a target="_blank" href='<%# GetPath(Eval("UPFILENAME").ToString()).ToString() %>' style='display: <%# (Eval("UPFILENAME").ToString().Trim()=="")?"None":"Block" %>; cursor: hand'><img src="../../HRD/Images/paperclipx12.png" border="0" /></a>
                                </ItemTemplate>
                                <ItemStyle Width="35px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issued From" SortExpression="AA,BB">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Source" runat="server" Text='<%# Eval("IssuedFrom") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Ref #">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_RefNo" runat="server" Text='<%# Eval("COCNO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date" SortExpression="TGCLOSEDT">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_DueDate" runat="server" Text='<%# Eval("TargetCloseDate") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="110px" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Completion Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_CompDate" runat="server" Text='<%# Eval("CompletionDate") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" ToolTip="Delete" />
                                </ItemTemplate>
                                <ItemStyle Width="45px"></ItemStyle>
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
                <td style="height: 18px; padding-right: 5px; text-align: right;" colspan="2">
                    <asp:Button ID="btn_View" runat="server" CssClass="btn" Text="Open/Modify" Width="85px" OnClientClick='return OpenModifyCOCWindow(document.getElementById("hid").value);' />
                    <asp:Button ID="btn_Closure" runat="server" CssClass="btn" Text="Closure" Width="59px" OnClientClick='return OpenCOCClosureWindow(document.getElementById("hid").value);' />
                    <asp:Button ID="btn_Print" runat="server" CssClass="btn" Text="Print" Width="59px" OnClientClick='return OpenCOCTrackerReport(document.getElementById("txt_VesselId").value);' /></td>
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
                <td style="padding-right: 8px; height: 18px; text-align: right">
                </td>
            </tr>
            <tr>
                <td colspan="9">
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txt_DueDays">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:HiddenField ID="HiddenField_FrmDate" runat="server" />
                    <asp:HiddenField ID="HiddenField_ToDate" runat="server" />
                    <asp:HiddenField ID="HiddenField_DueInDays" runat="server" />
                    <asp:HiddenField ID="HiddenField_Status" runat="server" />
                    <asp:HiddenField ID="HiddenField_Critical" runat="server" />
                    <asp:HiddenField ID="HiddenField_Responsibility" runat="server" />
                    <asp:HiddenField ID="HiddenField_OverDue" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
