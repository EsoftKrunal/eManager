<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CircularCategory.aspx.cs" Inherits="FormReporting_CircularCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Circular Category</title>
</head>
<body >
    <form id="form1" runat="server" >
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3">
                    
                </td>
            </tr>
            <tr>
                <td>Circular Category</td>
                <td> 
                    <asp:TextBox ID="txtCat" runat="server" ></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAddCat" runat="server" OnClick="OnClick_btnAddCat" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="grdCat" runat="server" >
                        <Columns>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Circular Category">
                                <ItemTemplate>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Images/edit.png" OnClick="imgDel_OnClick" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    
                </td>
                <td >
                </td>
            </tr>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
