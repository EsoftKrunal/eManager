<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApprovalManagement.aspx.cs" Inherits="Modules_HRD_CrewApproval_ApprovalManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<%--<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm"  %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <title>EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
  <%-- <link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />

    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
   <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <style type="text/css">
        .newbtn
        {
            border:solid 0px #c2c2c2;
            background-color:rgba(13, 93, 140, 1);
            color:white;
            padding:8px 15px;        
            font-size:15px;
            margin-top:2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div style="text-align: center;font-family:Arial;font-size:12px;">
    <div class="text headerband">
        Approval Management
    </div>
    <div style="text-align: center;">
        <asp:RadioButtonList runat="server" ID="rbl_Type" RepeatDirection="Horizontal" Font-Bold="true" AutoPostBack="true" OnSelectedIndexChanged="rbl_Type_OnSelectedIndexChanged" Font-Name="Verdana" Font-Size="12px" CssClass="" > 
            
            <asp:ListItem Text="Deployment Approval" Selected="True"  Value="3"></asp:ListItem> 
            <asp:ListItem Text="Approval Authority" Value="4"></asp:ListItem> 
        </asp:RadioButtonList>
    </div>
  
    
    <asp:Panel runat="server" ID="pnlApprovalLevel" Visible="true">
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
        <div style="overflow-y:scroll;overflow-x:hidden;width:95%;  border:solid 0px gray;" >
            <%--Working here 111111--%>
        <table width='100%' cellpadding='4' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:30px; background-color:#FFE6B2'>
        <colgroup>
            <col width='60px' />        
            <%--<col width='250px' />   --%>     
            <col />
            <col width='100px' />        
            <col width='100px' />        
            <col width='100px' />        
            <col width='120px' />        
            <col width='100px' />        
        </colgroup>
        <tr class="headerstylegrid">
            
            <td>Sr#</td>
            <td style="text-align:left;">Rank</td>        
            <%--<td style="text-align:left;">No Of Approval Required</td>--%>
            <td>Crew</td>
            <td>Technical	</td>
            <td>Marine</td>
            <td>Fleet Manager</td>
            <td>Owner</td>
        </tr>
        </table>
        </div>
        <div style="overflow-y:scroll;overflow-x:hidden;height:455px; width:95%;  border-bottom:solid 1px gray;" >
        <table width='100%' cellpadding='0' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:25px;'>
        <colgroup>
            <col width='60px' />  
            <%--<col width='250px' />    --%>
            <col />   
            <col width='100px' />        
            <col width='100px' />        
            <col width='100px' />        
            <col width='120px' />        
            <col width='100px' />
        </colgroup>
        <asp:Repeater runat="server" ID="rptRankNoOfApproval">
        <ItemTemplate>
        <tr style="height:25px;">
            <td><%#Eval("Sr")%></td>
            <td style="text-align:left">&nbsp;            
                <%#Eval("RankName") %>
                <asp:HiddenField ID="hfdRankID" runat="server" Value='<%#Eval("RankID") %>' />
            </td>
            <%--<td style="text-align:left">
                <asp:DropDownList ID="ddlRankApprovalLevel" runat="server" SelectedValue='<%#Eval("NoOfApproval") %>' style="text-align:left;width:50px;">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>                   
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>                   
                </asp:DropDownList>
            </td>--%>
            <td><asp:CheckBox ID="chkCrew" runat="server" Checked='<%# Eval("ApprovalLevels").ToString().IndexOf("1")>=0 %>'/> </td>
            <td><asp:CheckBox ID="chkTechnical" runat="server" Checked='<%# Eval("ApprovalLevels").ToString().IndexOf("2")>=0 %>'/> </td>
            <td><asp:CheckBox ID="chkMarine" runat="server" Checked='<%# Eval("ApprovalLevels").ToString().IndexOf("3")>=0 %>'/> </td>
            <td><asp:CheckBox ID="chkFleetManager" runat="server" Checked='<%# Eval("ApprovalLevels").ToString().IndexOf("4")>=0 %>'/> </td>
            <td><asp:CheckBox ID="chkOR" runat="server" Checked='<%# Eval("ApprovalLevels").ToString().IndexOf("5")>=0 %>'/> </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        </table>
        </div>
        <div style="padding:5px; text-align:right;padding-right:10px; background-color:#f7f57d;width:95%;">
            <asp:Label ID="lblMsgApproval" runat="server" style="color:red; float:left" Font-Size="Larger"></asp:Label>
            <asp:Button ID="btnSaveNoOfApprovalRequired" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveNoOfApprovalRequired_OnClick" />
            
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>


    <asp:Panel runat="server" ID="pnlApprovalAuthority" Visible="false">
    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
        <ContentTemplate>
            <div style="overflow-y:scroll;overflow-x:hidden;height:480px; width:95%;  border-bottom:solid 1px gray;" >

                <table width='100%' cellpadding='0' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:30px; background-color:#FFE6B2'>
                <colgroup>
                    <col width='50px' />
                    <col  />
                    <col width='100px' />
                    <col width='100px' />
                    <col width='100px' />
                    <col width='100px' />
                    <col width='100px' />
                </colgroup>
                <tr class= "headerstylegrid">
                            <td><b>Sr.</b></td>
                            <td> <b>User Name</b></td>
                            <td> <b>Crew</b></td>
                            <td> <b>Technical</b></td>
                            <td> <b>Marine</b></td>                            
                            <td> <b>Fleet Manager</b></td>
                            <td> <b>Owner</b></td>
                        </t>
                </table>

        <table width='100%' cellpadding='0' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:25px;'>
        <colgroup>
            <col width='50px' />
                    <col  />
                    <col width='100px' />
                    <col width='100px' />
                    <col width='100px' />
                    <col width='100px' />
                    <col width='100px' />
        </colgroup>
        
        <asp:Repeater runat="server" ID="rptApprovalAuthority">
        <ItemTemplate>
        <tr style="height:25px;">
            <td>
                <%#Eval("Rowno")%>
                <asp:HiddenField ID="hfdLoginID" runat="server" Value='<%#Eval("LoginID") %>' />
            </td>
            <td style="text-align:left">&nbsp;<%#Eval("UserName") %></td>
            <td style="text-align:center">
                <asp:CheckBox ID="chlapp1" runat="server"  Checked='<%# Convert.ToBoolean( Eval("Approval1"))%>' />
            </td>
            <td style="text-align:center">
                <asp:CheckBox ID="chlapp2" runat="server"  Checked='<%# Convert.ToBoolean(Eval("Approval2"))%>' />
            </td>
            <td style="text-align:center">
                <asp:CheckBox ID="chlapp3" runat="server"  Checked='<%# Convert.ToBoolean(Eval("Approval3"))%>' />
            </td>
            <td style="text-align:center">
                <asp:CheckBox ID="chlapp4" runat="server" Checked='<%# Convert.ToBoolean(Eval("Approval4"))%>'  />
            </td>
            <td style="text-align:center">
                <asp:CheckBox ID="chlapp5" runat="server" Checked='<%# Convert.ToBoolean(Eval("Approval5"))%>'  />
            </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        </table>
        </div>
        <div style="padding:5px; text-align:right;padding-right:10px; background-color:#f7f57d;width:95%">
            <asp:Label ID="lblMsgAuthoity" runat="server" ForeColor="Red" style="float:left" Font-Size="Larger"></asp:Label>
            <asp:Button ID="btnSaveAuthoity" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveAuthoity_OnClick" />
        </div>
            <div style="padding:10px;">
                    
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
</div>
    <script type="text/javascript" src="../JS/jquery-1.3.2.min.js"></script>
    <script type="text/javascript"  src="../JS/KPIScript.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            
        })
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            // re-bind your jQuery events here
            $(".listRanks").click(function () {

                var Action = 'D'
                //if ($(this).prop('checked'))
                if( $(this).is(':checked'))                
                    Action = 'A'

                var Checklistid = $(this).attr("checklistid");
                var Rankid = $(this).attr("rankid");
                $.ajax({
                    url: "CheckList.aspx/save_checklist_rank_mapping",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ checklistid: Checklistid, rankid: Rankid, action: Action}),
                    type: "POST",
                    success: function (data) {
                        
                    }
                });

                
            })
        });

       
       
    </script>
    </asp:Content>


