<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/RestHour.master" AutoEventWireup="true" CodeFile="RestHourOldNew.aspx.cs" Inherits="RestHourOldNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" runat="Server">

    <style type="text/css">
        .selbtn {
            background-color: #669900;
            color: White;
            border: none;
            padding:5px 10px 5px 10px;

            
        }

        .btn1 {
            background-color: #c2c2c2;
            border: solid 1px gray;
            border: none;
            padding:5px 10px 5px 10px;
        }
    </style>

    <table cellpadding="0" cellspacing="0" style="background-color: #f9f9f9; border-collapse: collapse; border-bottom: solid 5px #4371a5" border="0" width="100%">
        <colgroup>
        <col width="70px" />
        <col width="70px" />
        <col />
            </colgroup>
        <tr>
            <td>
                <asp:Button ID="btnRestHourNew" runat="server" Text="Rest Hour" OnClick="menu_OnClick" CssClass="selbtn" CommandArgument="1" />
            </td>
            <td style="display:none">
                <asp:Button ID="btnRestHourOld" runat="server" Text="Rest Hour - Old" OnClick="menu_OnClick" CssClass="btn1" CommandArgument="2" Visible="false"/>
            </td>
            <td style="text-align: left;">
                <asp:Button ID="btnRestHourStatus" runat="server" Text="Rest Hour Status" OnClick="menu_OnClick" CssClass="btn1" CommandArgument="3" Visible="false" />
            </td>
        </tr>
    </table>
    <iframe id="iframe" runat="server" src="CrewList_N.aspx" frameborder="0" style="width: 100%; height: 529px;"></iframe>

</asp:Content>
