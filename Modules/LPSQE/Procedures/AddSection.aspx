<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSection.aspx.cs" Inherits="AddSection" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link rel="Stylesheet" href="CSS/style.css" />
     <link rel="Stylesheet" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/KPIScript.js"></script>
    <style type="text/css">
        .Tab {
            padding: 4px 8px 4px 8px;
            color: #312d2d;
            background-color: #8e8989;
            font-weight: bold;
        }
        .SelTab {
             padding: 4px 8px 4px 8px;
            color: white;
            background-color: black;
            font-weight: bold;
        }
    </style>
</head>
<body style="margin-top:1px;font-family:Arial;font-size:12px;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width:100%; text-align:center;  height:20px; border-bottom:solid 1px black; padding:3px;" class="text headerband"><b>Add New / Edit Heading</b></div>
    <table width="100%"  cellpadding="2" border="0">
    <tr>
        <td>Parent Section :</td>
        <td><asp:DropDownList runat="server" ID="ddlParent" Width="85%"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Heading :</td>
        <td><asp:TextBox runat="server" ID="txtHeading" Width="85%" ></asp:TextBox></td>
    </tr>
    <tr>
        <td>Search Tags :</td>
        <td>
            <asp:UpdatePanel ID="upst" runat="server">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td >
                                <asp:TextBox runat="server" ID="txtSearchTags" Width="85%" Enabled="false" ></asp:TextBox>
                                <asp:HiddenField ID="hfSearchTag" runat="server" />
                                <asp:ImageButton ID="btnEditST" runat="server" ImageUrl="~/Modules/HRD/Images/edit.png" OnClick="btnEditST_OnClick" />&nbsp;
                                <asp:ImageButton ID="btnUpdateST" runat="server" ImageUrl="~/Modules/HRD/Images/check.png" Visible="false"  OnClick="btnUpdateST_OnClick" />&nbsp;
                                <asp:ImageButton ID="btnCancelST" runat="server" ImageUrl="~/Modules/HRD/Images/delete1.gif" Visible="false" OnClick="btnCancelST_OnClick"  />&nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>Revision No :</td>
        <td><asp:TextBox runat="server" ID="txtRevision" Width="85%" Enabled="false" ></asp:TextBox></td>
    </tr>
    <tr>
        <td>Content File :</td>
        <td><asp:FileUpload runat="server" ID="flpContent" />&nbsp;<asp:Button runat="server" ID="btnClearAttachment" Text="Clear Attachment" OnClientClick="return confirm('Are you sure to clear attachment?');" onclick="btnClearAttachment_Click" /></td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button runat="server" ID="btnSubmit" Text="Save" CssClass="btn"   onclick="btnSubmit_Click" />
            <asp:Button runat="server" ID="btnBack" Text="Go Back" CssClass="btn"  onclick="btnBack_Click" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Label runat="server" id="lblMessage" ForeColor="Red" Font-Size="Larger"></asp:Label>
        </td>
    </tr>
    </table> 
    <hr />
    <div style="padding:0px">
    <div style="">
        <div style="border-bottom:solid 3px black;margin-bottom:5px;">
            <asp:Button ID="btnForms" runat="server" Text="Forms" OnClick="btnForms_OnClick" CssClass="SelTab" />
            <asp:Button ID="btnRanks" runat="server" Text="Rank" OnClick="btnRanks_OnClick" CssClass="Tab"/>
        </div>
        <div id="divForms" runat="server">
            <table cellpadding="3" cellspacing="0" width="100%" border="1" style="text-align:center; border-collapse:collapse;">
        <col width="50%" />
        <col width="50%" />
        <tr class="heading" style="background-color:#e2e2e2">
            <td>Available Forms</td>
            <td>Linked Forms</td>
        </tr>
        <tr>
            <td valign="top">
                <div style="overflow-x:hidden;overflow-y:scroll;height:150px;">
                <asp:Repeater runat="server" ID="rptAvailableForms">
                <ItemTemplate>
                <div style="height:10px;">
                    <asp:ImageButton ID="btnFileDownload" runat="server" OnClick="btnAddFormsToHeading" ImageUrl="~/Modules/HRD/Images/w_add2.png" CommandArgument='<%#Eval("FormId")%>' style="float:left; clear:left; padding-right:5px;" ToolTip="Add this from to Section." />
                    <asp:LinkButton ID="lnkLink" runat="server" OnClick="btnFileDownload_OnClick" CommandArgument='<%#Eval("FormId")%>' style="float:left;" Text='<%#Eval("FormNo") +" ( "+Eval("VersionNo") +" )"%>'></asp:LinkButton>  
                    <asp:HiddenField ID="hfFileName" runat="server" Value='<%#Eval("FileName")%>'/>
                    <span style="float:right"><%# Common.ToDateString(Eval("CreatedOn"))%></span>
                </div><br />
                </ItemTemplate>
                </asp:Repeater>
                </div>
            </td>
            <td valign="top">
                <div style="overflow-x:hidden;overflow-y:scroll;height:150px;">
                 <asp:Repeater runat="server" ID="rptLinkedForm">
                <ItemTemplate>
                <div style="height:10px;">
                    <asp:ImageButton ID="btnFileDelete" runat="server" OnClick="btnRemoveFormsToHeading" ImageUrl="~/Modules/HRD/Images/delete1.gif" CommandArgument='<%#Eval("FormId")%>' style="float:left; clear:left; padding-right:5px;" ToolTip="Remove this from from Section." OnClientClick="return confirm('Are you sure to remove this form?');"/>
                    <asp:LinkButton ID="lnkLink" runat="server" CommandArgument='<%#Eval("FormId")%>' style="float:left;" Text='<%# Eval("FormNo")+ " ( "+ Eval("VersionNo") +" )"%>'  OnClick="btnDownLoadFile" ></asp:LinkButton>  
                    <asp:HiddenField ID="hfFileName" runat="server" Value='<%#Eval("FileName")%>'/>
                    <span style="float:right"><%# Common.ToDateString(Eval("CreatedOn"))%></span>
                </div><br />
                </ItemTemplate>
                </asp:Repeater>
                </div>
            </td>
        </tr>
    </table>
        </div>
        <div id="divRanks" runat="server" visible="false">
            <table cellpadding="3" cellspacing="0" width="100%" border="1" style="text-align:center; border-collapse:collapse;">
        <col width="50%" />
        <col width="50%" />
        <tr class="heading" style="background-color:#e2e2e2">
            <td>Available Ranks</td>
            <td>Linked Ranks</td>
        </tr>
        <tr>
            <td valign="top">
                <div style="overflow-x:hidden;overflow-y:scroll;height:150px;" class="ScrollAutoReset" id="divAR">
                <asp:Repeater runat="server" ID="rptAvailableRanks">
                <ItemTemplate>
                <div style="height:10px;">
                    <asp:ImageButton ID="btnFileDownload" runat="server" OnClick="btnAddRanksToHeading" ImageUrl="~/Modules/HRD/Images/w_add2.png" CommandArgument='<%#Eval("RankID")%>' style="float:left; clear:left; padding-right:5px;" ToolTip="Add this rank to Section." />
                    <asp:LinkButton ID="lnkLink" runat="server" OnClick="btnFileDownload_OnClick" CommandArgument='<%#Eval("RankID")%>' style="float:left;" Text='<%#Eval("RankCode")%>'></asp:LinkButton>  
                    <asp:HiddenField ID="hfRankCode" runat="server" Value='<%#Eval("RankCode")%>'/>
                    <%--<span style="float:right"><%# Common.ToDateString(Eval("CreatedOn"))%></span>--%>
                </div><br />
                </ItemTemplate>
                </asp:Repeater>
                </div>
            </td>
            <td valign="top">
                <div style="overflow-x:hidden;overflow-y:scroll;height:150px;"  class="ScrollAutoReset" id="divLR">
                 <asp:Repeater runat="server" ID="rptLinkedRanks">
                <ItemTemplate>
                <div style="height:10px;">
                    <asp:ImageButton ID="btnFileDelete" runat="server" OnClick="btnRemoveRanksToHeading" ImageUrl="~/Modules/HRD/Images/delete1.gif" CommandArgument='<%#Eval("RankID")%>' style="float:left; clear:left; padding-right:5px;" ToolTip="Remove this rank from Section." OnClientClick="return confirm('Are you sure to remove this rank?');"/>
                    <asp:LinkButton ID="lnkLink" runat="server" CommandArgument='<%#Eval("RankID")%>' style="float:left;" Text='<%# Eval("RankCode")%>'  OnClick="btnDownLoadFile" ></asp:LinkButton>  
                    <%--<asp:HiddenField ID="hfFileName" runat="server" Value='<%#Eval("FileName")%>'/>--%>
                    <%--<span style="float:right"><%# Common.ToDateString(Eval("CreatedOn"))%></span>--%>
                </div><br />
                </ItemTemplate>
                </asp:Repeater>
                </div>
            </td>
        </tr>
    </table>
        </div>
    </div>
    
    </div>
    </form>
</body>
</html>
