<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewTraining.master" AutoEventWireup="true" CodeFile="Classification.aspx.cs" Inherits="HSSQE_KST_Classification" %>
<%@ Register src="~/Modules/HRD/CrewOperation/KSTMenu.ascx" tagname="kstMenu" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <div>
        <uc2:kstMenu ID="kstMenu2" runat="server" />
    </div>
    <div style="padding:5px; text-align:right;">
        <asp:Button ID="btnAdd" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_add_Click" Text="Add" Width="80px" />
    </div>
            <div style="height:26px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1;">
        <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
        <thead>
        <tr style='color:White; height:25px;'>
               <td style="width:50px;color:White;text-align:center;"><b>Edit</b></td>      
                <td style="width:50px;color:White;text-align:center;"><b>Delete</b></td>       
                <td style="color:White;text-align:left;"><b>Classification</b></td>
                <td style="width:30px;"><b>&nbsp;</b></td>
        </tr>
        </thead>
        </table>
</div>
<div style="height:390px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll; " class='ScrollAutoReset' id='dv_SCM_List'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:White; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptGrid">
<ItemTemplate>
<tr onmouseover="">
    <td style="width:50px;color:White;text-align:center;">
                    <b>
                        <asp:ImageButton runat="server" ID="btnEdit" CommandArgument='<%#Eval("ClassificationId")%>' OnClick="btnEdit_Click" ImageUrl="~/Modules/HRD/Images/editX12.jpg" />
                    </b>
                </td>      
                <td style="width:50px;color:White;text-align:center;">
                    <b>
                        <asp:ImageButton runat="server" ID="btndelete" CommandArgument='<%#Eval("ClassificationId")%>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure to delete?');" ImageUrl="~/Modules/HRD/Images/delete_12.gif" />
                    </b>
                </td>
        <td style="text-align:left;"><%#Eval("Classificationname")%></td><td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>

<div id="divPanel" style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" visible="false" >
<center>
    <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position :relative; width:600px; padding :10px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:50px;opacity:1;filter:alpha(opacity=100)">
        <div>
            <h2>Add/Edit Classification</h2>
            <table width="100%">
                <tr>
                    <td style="width:100px">Classification :</td>
                    <td><asp:TextBox ID="txtClassification" runat="server" CssClass="btn" Width="95%" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtClassification" ErrorMessage="*" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <asp:Button ID="btn_Training_save" runat="server" CssClass="btn" OnClick="btn_save_Click" Text="Save" Width="80px" />
            <asp:Button ID="btn_Training_Cancel" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Cancel_Click" Text="Close" Width="80px"  />
        </div>
    </div>                            
</center>
</div>
<div style="padding:5px; background-color:#FFFFCC; text-align:left;">&nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></div>

    
</asp:Content>