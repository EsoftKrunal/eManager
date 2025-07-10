<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImagesViewer.aspx.cs" Inherits="Transactions_ImagesViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View More Images</title>
</head>
<script language="javascript">
    function imageChng(imgObj)
    {
        var px=1;
        var imgSrc=document.getElementById(imgObj).src;
        var imgAlt=document.getElementById(imgObj).alt;
        document.getElementById("GridView1_ctl02_img1").src=imgSrc;
        document.getElementById("lb_desc").innerText=imgAlt;
        var ob1=document.getElementById(imgObj).style;
        ob1.borderStyle="solid";
        ob1.borderWidth=px+"px";
        ob1.borderColor="black";
    }
    function imageChng1(imgObj)
    {
        var px=1;
        var imgSrc=document.getElementById(imgObj).src;
        var imgAlt=document.getElementById(imgObj).alt;
        document.getElementById("GridView1_ctl02_img1").src=imgSrc;
        document.getElementById("lb_desc").innerText=imgAlt;
        var ob1=document.getElementById(imgObj).style;
        ob1.borderStyle="solid";
        ob1.borderWidth=px+"px";
        ob1.borderColor="lightgrey";
    } 
</script>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" PageSize="1" AllowPaging="True" Width="100%" BorderStyle="None" BorderWidth="0px" HorizontalAlign="Center" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Image ID="img1" Width="230" Height="230" ImageUrl ='<%# Eval("FPath") %>' runat="server" BorderWidth="1" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="White" BorderColor="White" BorderStyle="None" />
            <FooterStyle BackColor="#C0FFC0" BorderStyle="None" />
            <PagerSettings Visible="False" />
        </asp:GridView>
        <asp:Label ID="lb_desc" runat="server"></asp:Label><br />
        <asp:DataList ID="GridView2" runat="server" RepeatDirection="horizontal" RepeatColumns="8" OnItemDataBound="GridView2_ItemDataBound">
            <ItemTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Image ID="img2" Width="50" Height="50" ImageUrl='<%# Eval("FPath") %>' runat="server" AlternateText='<%# Eval("PicCaption") %>' />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList><asp:Label ID="lb_tot" runat="server" Width="150px" Font-Bold="True" Font-Size="12pt" ForeColor="Black" Font-Underline="True"></asp:Label></div>
    </form>
</body>
</html>
