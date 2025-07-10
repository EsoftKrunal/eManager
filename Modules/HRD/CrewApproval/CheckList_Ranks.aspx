<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="CheckList_Ranks.aspx.cs" Inherits="CrewApproval_CheckList_Ranks" Title="Crew Checkkist" %>
<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
       <link rel="stylesheet" href="../../../css/app_style.css" />

    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
   <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <style type="text/css">
        .bordered tr td
        {
            border :solid 1px #e5e5e5;
        }
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
</head>
<body style="margin: 5 0 5 0; background-color:White;" >
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div style="text-align: center;font-family:Arial;font-size:12px;">
    <div style="text-align:center;padding:8px; " class="text headerband">
        <asp:Label runat="server" ID="lblCheckListName" Font-Size="Larger" Font-Bold="true" ForeColor="White"></asp:Label>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>

        <div style="text-align: center;">
            <table cellpadding="4" cellspacing="3">
                <tr>
                    <td>
                        <b> Category : </b> 
                           
                    </td>
                    <td style="width:120px;">
                         <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_RankGroup_OnSelectedIndexChanged" Width="100px">
                                <%--<asp:ListItem Value="A">All</asp:ListItem>--%>
                                    <asp:ListItem Value="O">Officer</asp:ListItem>
                                    <asp:ListItem Value="R">Rating</asp:ListItem>
                            </asp:DropDownList>
                    </td>
                    <td>
                        <b> Rank Groups : </b> 
                    </td>
                    <td>
                        <asp:CheckBoxList ID="chklistRankGroups" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="chklistRankGroups_OnSelectedIndexChanged"></asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            

            
        </div>
        <div id="divChecklistRank" runat="server" style="margin-top:10px;">
            <div style="overflow-x:hidden;overflow-y:scroll;">
                <table cellpadding="3" cellspacing="0" width="50%" border="0" style="border-collapse: collapse;height: 25px; background-color: Orange; color: White;margin:0px auto;" class="bordered">
                 <col width="25px" />
                <col width="250px" />
                <col  />
                <tr class= "headerstylegrid" >
                    <td></td>
                    <td>Rank Code</td>
                    <td></td>
                </tr>
            </table>
            </div>
            <div style="overflow-x:hidden;overflow-y:scroll;height:300px;">
                <table cellpadding="3" cellspacing="0" width="50%" border="0" style="border-collapse: collapse;margin:0px auto;" class="bordered">
                <col width="25px" />
                <col width="250px" />
                <col  />
                <asp:Repeater ID="rptRanks" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td></td>
                            <td style="text-align:left;margin-left:10px;">
                                <%#Eval("RankCode") %>
                                <asp:HiddenField ID="hfdRankID" runat="server" Value='<%#Eval("RankID") %>'  />
                                
                            </td>
                            <td style="text-align:left;margin-left:10px;">
                                <asp:CheckBox ID="chkApplyRanks" runat="server"
                                    Checked=<%#getChecklistRankMapingResult(Common.CastAsInt32(Eval("RankID"))) %> />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            </div>
            <div style="padding:5px;">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_OnClick" />
            </div>
            <div style="padding:5px;">
                <asp:Label ID="lblMsg" runat="server" style="color:red;" ></asp:Label>
            </div>
        </div>
        
        
    </ContentTemplate>
    </asp:UpdatePanel>    
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

</form>
</body>
</html>
