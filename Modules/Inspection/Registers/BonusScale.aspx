<%@ Page Language="C#" MasterPageFile="~/Modules/Inspection/Registers/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="BonusScale.aspx.cs" Inherits="Registers_BonusScale" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="padding:5px; text-align:right">
<asp:Button runat="server" ID="btnAdd" Text="Add Bonus Scale" CssClass="btn" OnClick="btnAddScale_Click" />
</div>
<table cellpadding="2" cellspacing="0" width="100%" style=" background-color:#E0E0FF; border-collapse:collapse;" border="1">
<tr>
    <td style="width:50px; text-align:center">View</td>
    <td style="width:50px; text-align:center">Edit</td>
    <td style="width:50px; text-align:center">Sr#</td>
    <td style="text-align:left">Effective Date</td>
    <td style="width:300px; text-align:center">CreatedBy/On</td>
    <td style="width:300px; text-align:center">ModifiedBy/On</td>
    <td style="width:80px; text-align:center">Suptd(%)</td>
    <td style="width:70px; text-align:center">Delete</td>
    <td style="width:20px; text-align:center">&nbsp;</td>
</tr>
</table>
<div style=" height:350px; overflow:hidden; overflow-y:scroll; border:solid 1px gray;">   
    <table cellpadding="2" cellspacing="0" width="100%" style="border-collapse:collapse;" border="1">
    <asp:Repeater runat="server" ID="rptBS">
    <ItemTemplate>
    <tr>
        <td style="width:50px; text-align:center">
            <asp:LinkButton runat="server" ID="btnView" Text="View" ForeColor="Green" OnClick="btnViewClick" CommandArgument='<%#Eval("BonusId")%>'></asp:LinkButton>
        </td>
        <td style="width:50px; text-align:center">
            <asp:LinkButton runat="server" ID="btnEdit" Text="Edit" ForeColor="Blue" OnClick="btnEditClick" CommandArgument='<%#Eval("BonusId")%>'></asp:LinkButton>
        </td>
        
        <td style="width:50px; text-align:center"><%#Eval("SNo")%></td>
        <td style="text-align:left"><%#Common.ToDateString(Eval("EffDate"))%></td>
        <td style="width:300px; text-align:center"><%#Eval("CreatedByName")%>/<%#Common.ToDateString(Eval("CreatedOn"))%></td>
        <td style="width:300px; text-align:center"><%#Eval("ModifiedByName")%>/<%#Common.ToDateString(Eval("ModifiedOn"))%></td>
        <td style="width:80px; text-align:center"><%#Eval("SuptdPer")%></td>
        <td style="width:70px; text-align:center">
            <asp:LinkButton runat="server" ID="btnDelete" Text="[X] Delete" ForeColor="Red" OnClick="btnDeleteClick" CommandArgument='<%#Eval("BonusId")%>' OnClientClick="return window.confirm('Are you sure to remove this?');"></asp:LinkButton>
        </td>
        <td style="width:20px; text-align:center">&nbsp;</td>
    </tr>    
    </ItemTemplate>
    </asp:Repeater>
    </table>
</div>

 <!-- Section to Add / Edit Bonus Scale -->    
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvAddEditBS" runat="server" visible="false">
    <center>
        <div style="position:absolute;top:0px;left:0px; height :520px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
         <div style="position :relative;width:600px; height:400px;padding :3px; text-align :center;background : white; z-index:150;top:50px;">
         <center >
         <div style="height:370px">
            <div style="padding:4px;background-color:Wheat; font-size:11px;">Add/Edit Bonus Scale</div>
            <div style="height:325px; border:solid 1px Wheat; padding:5px; text-align:left">
            <div style="padding:2px;"> 
                <asp:HiddenField runat="server" ID="hfdbonusid" />
                &nbsp; Effective From : <asp:TextBox runat="server" id="txtEffectveFrom" CssClass="required_box" Width="90px" ></asp:TextBox>
                <ajaxToolkit:CalendarExtender runat="server" ID="d1" TargetControlID="txtEffectveFrom" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                &nbsp;Suptd. (%) <asp:TextBox runat="server"  ID="txtper" CssClass="input_box" MaxLength="3" Width="40px" ></asp:TextBox>
            </div>
            <div style="height:300px; overflow-x:hidden;overflow-y:scroll; border:solid 1px Wheat;">
            <table cellpadding="0" cellspacing="0" width="100%">
            <asp:Repeater runat="server" ID="rpt_RankList">
                <ItemTemplate>
                    <tr>
                        <td style="width:30px; text-align:center;"><%#Eval("Sno")%>
                        <asp:HiddenField runat="server" ID="hfdRank" Value='<%#Eval("RankId")%>' />
                        </td>
                        <td style="width:75px; text-align:left;"><%#Eval("RankCode")%></td>
                        <td style="text-align:left;"><asp:TextBox Runat="server" id="txtAmount" Text='<%#Eval("Amount")%>' CssClass="input_box" style='text-align:right'></asp:TextBox> </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
            </div>
         </div>
            <asp:Button runat="server" id="btnSave" OnClick="btnSave_Click" Text="Save" CssClass="btn" style="padding:4px; width:120px; background-color:Green; color:White"/>
            <asp:Button runat="server" id="btnClose" OnClick="btnClose_Click" Text="Close" CssClass="btn" style="padding:4px; width:120px; background-color:Red; color:Black"/>
         </center>
         </div>
    </center>
    </div> 
</asp:Content>

