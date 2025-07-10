<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="CheckList.aspx.cs" Inherits="CrewApproval_CheckList" Title="EMANAGER"  %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

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

   </head>
<body style="margin: 5 0 5 0; background-color:White;" >
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div style="text-align: center;font-family:Arial;font-size:12px;">
    <div class="text headerband">
        Checklist Master
    </div>

    <asp:Panel runat="server" ID="pnlCheckList" Visible="true">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
    <ContentTemplate>
    <div style="text-align: center;">
        <table cellpadding="5" cellspacing="5">
            <tr>
                <td>
                    <b>CheckList Name :</b>  
                </td>
                <td>
                    <asp:TextBox ID="txtCheckListName" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAddCheckListName" runat="server" Text="Add Checklist" CssClass="btn" OnClick="btnAddCheckListName_OnClick"  />
                </td>
                <td>
                    <asp:Label ID="lblMSgChecklist" runat="server" style="color:red;" ></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="overflow-y:scroll;overflow-x:hidden;width:95%;  border:solid 0px gray;" >
    <table width='100%' cellpadding='4' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:30px; background-color:#FFE6B2'>
    <colgroup>
        <col width='40px' />    
        <col width='60px' />        
        <col />
        <col width='140px' />  
        <col width='140px' />  
        <col width='140px' />    
    </colgroup>
    <tr class="headerstylegrid">
        <td></td>
        <td>Sr#</td>
        <td style="text-align:left;">Checklist</td>        
        <td>Link Questions</td>
        <td>Link Ranks</td>
        <td>Delete</td>
    </tr>
    </table>
    </div>
    <div style="overflow-y:scroll;overflow-x:hidden;height:444px; width:95%;  border:solid 0px gray;" >
    <table width='100%' cellpadding='0' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:25px;'>
    <colgroup>
        <col width='40px' />    
        <col width='60px' />        
        <col />
        <col width='140px' />
        <col width='140px' />  
        <col width='140px' />    
    </colgroup>
    <asp:Repeater runat="server" ID="rptCheckList">
    <ItemTemplate>
    <tr style="height:25px;">
        <td>
            <asp:ImageButton ID="btnEditCheckListName" runat="server" ImageUrl="~/Modules/HRD/Images/editX12.jpg" OnClick="btnEditCheckListName_OnClick" CommandArgument='<%#Eval("id")%>' ToolTip="Edit" Visible='<%#Eval("Locked").ToString()!="Y"%>' />
        </td>
        <td><%#Eval("Sr")%></td>
        <td style="text-align:left">&nbsp;            
            <asp:Label ID="lblCheckListname" runat="server" Text='<%#Eval("CheckListname")%>' ></asp:Label>
        </td>
        <td>
            <asp:ImageButton ID="btnAddChecklistItempPopup" runat="server" ImageUrl="~/Modules/HRD/Images/tools_32.png" OnClick="btnAddChecklistItempPopup_OnClick" CommandArgument='<%#Eval("id")%>' style="width:12px;"/>
        </td>
         <td>
            <asp:ImageButton ID="btnLinkRanks" runat="server" ImageUrl="~/Modules/HRD/Images/tools_32.png" OnClick="btnLinkRanks_OnClick" CommandArgument='<%#Eval("id")%>' style="width:12px;" Visible='<%# (Common.CastAsInt32(Eval("id"))!=3)%>'  />
        </td>
        <td>
            <asp:ImageButton ID="btnDeleteChecklist" runat="server" ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="btnDeleteChecklist_OnClick" CommandArgument='<%#Eval("id")%>' OnClientClick="return confirm('Are you sure to delete this record.');"  ToolTip="Delete" Visible='<%#Eval("Locked").ToString()!="Y"%>'/>
        </td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
    </table>
    </div>
    

    <div style="position:absolute;top:50px;left:50px; height :455px; width:95%; " id="divAddChecklistItems" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:50px;left:50px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:80%;padding :5px; text-align :center;background : white; z-index:150;top:20px; border:solid 0px black;">
            <center >
             <div class="text headerband" style="padding:6px;  font-size:12px; "><strong>Add Checklist Items</strong></div>
                <table cellpadding="5" cellspacing="5">
                    <tr>
                        <td>
                            <b>CheckList Item Name :</b> 
                        </td>
                        <td>
                             <asp:TextBox ID="txtCheckListItemName" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                            <asp:Button ID="btnAddChecklistItem" runat="server" Text="Add Checklist Item" CssClass="btn" OnClick="btnAddChecklistItem_OnClick"  />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;">
                            <asp:Label ID="lblMsgChecklistItems" runat="server" style="color:red;" ></asp:Label>

                        </td>
                    </tr>
                </table>

                    <div style="overflow-y:scroll;overflow-x:hidden;width:100%;  border:solid 0px gray;" >
    <table width='100%' cellpadding='4' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:30px; background-color:#FFE6B2'>
    <colgroup>
        <col width='40px' />    
        <col width='60px' />        
        <col />
        <col width='40px' />               
    </colgroup>
    <tr class="headerstylegrid">
        <td></td>
        <td>Sr#</td>
        <td style="text-align:left;">Checklist</td>        
        <td></td>        
    </tr>
    </table>
    </div>
                    <div style="overflow-y:scroll;overflow-x:hidden;height:250px; width:100%;  border:solid 0px gray;" >
    <table width='100%' cellpadding='0' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:25px;'>
    <colgroup>
        <col width='40px' />    
        <col width='60px' />        
        <col />
        <col width='40px' />            
    </colgroup>
    <asp:Repeater runat="server" ID="rptCheckListItems">
    <ItemTemplate>
    <tr style="height:25px;">
        <td style="text-align:center;">
            <asp:ImageButton ID="btnEditCheckListItem" runat="server" ImageUrl="~/Modules/HRD/Images/editX12.jpg" OnClick="btnEditCheckListItem_OnClick" CommandArgument='<%#Eval("id")%>' ToolTip="Edit" />
        </td>
        <td> &nbsp;<%#Eval("Sr")%></td>
        <td style="text-align:left">&nbsp;            
            <asp:Label ID="lblCheckListname" runat="server" Text='<%#Eval("CheckListItemName")%>' ></asp:Label>
        </td>        
        <td style="text-align:center;">
            <asp:ImageButton ID="btnDeleteChecklistItem" runat="server" ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="btnDeleteChecklistItem_OnClick" CommandArgument='<%#Eval("id")%>' OnClientClick="return confirm('Are you sure to delete this record.');"  ToolTip="Delete" />
        </td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
    </table>
    </div>
                
                <div style="width:100%; text-align:center;">                                  
                    <asp:Button ID="btn_Close" runat="server" Text="Close" Width="80px" OnClick="btn_CloseChecklistItem_Click" CausesValidation="false" style="  border:none; padding:4px;" CssClass="btn"/>                    
                </div>
             </center>
        </div>
    </center>
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
    </form>
</body>
</html>
   