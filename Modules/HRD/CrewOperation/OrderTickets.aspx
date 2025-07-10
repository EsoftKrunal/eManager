<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="OrderTickets.aspx.cs" Inherits="CrewOperation_OrderTickets" Title="CMS : Crew Operation > Crew Travel ( Ticket Mangement ) " %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>CMS : Crew Operation > Crew Travel ( Ticket Mangement ) </title>
<link href="../../../css/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
<link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<script type="text/javascript">

    function SelectAll(ctl) {
        var selfid = ctl.getAttribute("id");
        var ctls=document.getElementsByTagName("input");
        var c=0;
        for(c=0;c<ctls.length-1;c++)
        {
            if (ctls[c].getAttribute("type") == "checkbox" && ctls[c].getAttribute("id") != selfid) {
                ctls[c].setAttribute("checked", ctl.getAttribute("checked"));
            }
        }
    }

    var CurrentTxt;
    var CurrentList;

    function Hide_List(ctl) {
        ctl.parentNode.style.position = "static";
        ctl.nextSibling.innerHTML = '';
    }
    function OnKeyDown(e) {
        var event = window.event ? window.event : e;
        if (event.keyCode == 40) {
            CurrentTxt.nextSibling.childNodes[0].focus();
        }
        if (event.keyCode == 27) {
            Hide_List(CurrentTxt);
        }
    }
    function Show_List(ctl, type) {
        if (event.keyCode==27) {return;}
        CurrentTxt=ctl;
        var xmlhttp;
        if (window.XMLHttpRequest)
          {// code for IE7+, Firefox, Chrome, Opera, Safari
          xmlhttp=new XMLHttpRequest();
          }
        else
          {// code for IE6, IE5
          xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
          }
          xmlhttp.onreadystatechange = function () {

              if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                  //ctl.nextSibling.style.zIndex = 200;
                  ctl.parentNode.style.position = "relative";
                  ctl.nextSibling.style.position = "absolute";
                  ctl.nextSibling.style.top = 25;
                  ctl.nextSibling.style.left = 0;
                  ctl.nextSibling.innerHTML = xmlhttp.responseText;
                  CurrentList = ctl.nextSibling.childNodes[0];
              }
          }
          xmlhttp.open("GET", "../AzaxAutoList.aspx?Type=" + type + "&Key=" + ctl.value + "&Rnd=" + Math.random(), true);
          xmlhttp.send();
    }
        function SetValue_Back(ctl) 
        {
            CurrentTxt.value = ctl.options[ctl.selectedIndex].innerHTML;
            Hide_List(CurrentTxt);
        }

        function SetValue_Back_Key(e) {
            var ctl = CurrentList;
            var event = window.event ? window.event : e;
            if (event.keyCode == 13) 
            {
                CurrentTxt.value = ctl.options[ctl.selectedIndex].innerHTML;
                Hide_List(CurrentTxt);
            }
            if (event.keyCode == 27) {
                Hide_List(CurrentTxt);
            }
        }
        
        
</script>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%" border="1" bordercolor="#4371a5" style="font-family:Arial;">
    <tr>
        <td class="text headerband" colspan="2" >
            <strong> Crew Travel - Order Ticket </strong>
        </td>
    </tr>
    <tr>
    <td>
    <div style=" padding:5px; background-color:Wheat;text-align:center;  ">
        <asp:Label runat="server" ID="lblPortRefNO" Font-Size="14px"></asp:Label>
    </div>
    <div style=" padding:3px; ">
    <asp:GridView ID="gvsearch" runat="server" AutoGenerateColumns="False" DataKeyNames="CrewId" GridLines="Horizontal" Width="100%" OnPreRender="gvsearch_OnPreRender" CellPadding="1" >
    <Columns>
    
    <asp:TemplateField HeaderText="Crew #" SortExpression="CrewNumber" ItemStyle-Width="50px">
    <ItemTemplate>
       <asp:HiddenField ID="hfdEmpNo" runat="server" Value='<%#Eval("CrewNumber")%>' />
       <asp:LinkButton ID="btncrewnosignoff" runat="server" Text='<%#Eval("CrewNumber") %>'  Font-Underline="false" CommandName="Select"></asp:LinkButton>  
       <asp:HiddenField ID="HiddencrewIdsignoff" runat="server" Value='<%#Eval("CrewId")%>' />
       <asp:HiddenField ID="HfdCrewFlag" runat="server" Value='<%#Eval("CrewFlag")%>' />
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Crew Name" SortExpression="CrewName">
        <ItemStyle HorizontalAlign="Left" />
        <ItemTemplate>
            <asp:Label ID="lblCompanyNamesignoff" runat="server" Text='<%# Eval("CrewName") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="No(s) Tickets" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
    <ItemTemplate >
    <%# Eval("NO_TICS") %>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Rank" SortExpression="RankName" ItemStyle-Width="50px">
        <ItemStyle HorizontalAlign="Left" />
        <ItemTemplate>
            <asp:Label ID="lblranknamesignoff" runat="server" Text='<%# Eval("RankName") %>'></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="" ItemStyle-Width="20px">
    <HeaderTemplate>
        <asp:CheckBox runat="server" ID="chkAll" onclick="SelectAll(this);" />
    </HeaderTemplate>
    <ItemTemplate>
    <asp:CheckBox runat="server" ID="chkSelect" style=' margin:5px;'/>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Travel Agent" >
        <ItemStyle HorizontalAlign="Left" Width='255px' />
        <ItemTemplate>
            <asp:DropDownList runat="server" ID="ddl_TA" DataSource="<%#BindTAgents()%>" DataTextField="COMPANY" DataValueField="TRAVELAGENTID" CssClass='input_box' Width="250px"></asp:DropDownList>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Airline Name" >
        <ItemStyle HorizontalAlign="Left" Width="125px"/>
        <ItemTemplate>
            <div>   
                <asp:TextBox runat="server" ID="txt_Airline" CssClass='input_box' Text='' MaxLength="50" OnKeyDown="OnKeyDown(this);" onKeyUp='Show_List(this,1);' ></asp:TextBox><div style=""></div>
            </div>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Origin" >
        <ItemStyle HorizontalAlign="Left" Width="125px"/>
        <ItemTemplate>
            <div>   
                <asp:TextBox runat="server" ID="txt_FA" CssClass='input_box' MaxLength="50" OnKeyDown="OnKeyDown(this);" onKeyUp='Show_List(this,2);' ></asp:TextBox><div style=""></div>
            </div>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Destination" >
        <ItemStyle HorizontalAlign="Left" Width="125px"/>
        <ItemTemplate>
        
            <asp:TextBox runat="server" ID="txt_TA" CssClass='input_box' Text='' MaxLength="50"></asp:TextBox>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Dep. Dt." >
        <ItemStyle HorizontalAlign="Left" Width="55px" />
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txt_Date" CssClass='input_box' Text='' MaxLength="50" Width="70px"></asp:TextBox>
            <ajaxToolkit:CalendarExtender runat="server" ID="c1" TargetControlID="txt_Date" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
        </ItemTemplate>
    </asp:TemplateField>
    <%--<asp:TemplateField HeaderText="Class" >
        <ItemStyle HorizontalAlign="Left" Width="60px"/>
        <ItemTemplate>
            <asp:DropDownList runat="server" ID="ddl_Class" CssClass='input_box'>
                <asp:ListItem Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="Executive" Value="E"></asp:ListItem>
                <asp:ListItem Text="Business" Value="B"></asp:ListItem>
                <asp:ListItem Text="Economy" Value="C"></asp:ListItem>
            </asp:DropDownList>
        </ItemTemplate>
    </asp:TemplateField>--%>
    <asp:TemplateField HeaderText="PNR#"  >
        <ItemStyle HorizontalAlign="Left" Width="105px"/>
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txt_PNR" CssClass='input_box' Text='' MaxLength="50" Width="100px"></asp:TextBox>
        </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="Booking Dt." >
        <ItemStyle HorizontalAlign="Left" Width="55px" />
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txt_Bdate" Text='' Width='70px' CssClass='input_box'></asp:TextBox>
            <ajaxToolkit:CalendarExtender runat="server" ID="c2" TargetControlID="txt_Bdate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Currency" >
        <ItemStyle HorizontalAlign="Left" Width="60px" />
        <ItemTemplate>
            <asp:DropDownList runat="server" ID="ddl_Curr" DataSource="<%#BindCurrency()%>" DataTextField="FOR_CURR" DataValueField="FOR_CURR" CssClass='input_box'></asp:DropDownList>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Amount" >
        <ItemStyle HorizontalAlign="Left" Width='60px' />
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txt_Amount" CssClass='input_box' Text='' MaxLength="50" Width="60px" style='text-align:right'></asp:TextBox>
        </ItemTemplate>
    </asp:TemplateField>
    
    </Columns>
    <RowStyle CssClass="rowstyle" />
    <SelectedRowStyle Font-Bold="true" />
    <HeaderStyle CssClass="headerstylefixedheadergrid" />
</asp:GridView>

        </div>
        
        </td>
    </tr>
    </table>
    <br />
    <div>
    <asp:Button runat="server" ID="bthSave" Text="Save" style="float:right; padding:5px;" CssClass="btn" OnClick="btnSave_Click" />
    <asp:Label runat="server" ID="lblMsg" Text="" ForeColor="Red" style="float:right" Font-Size="15px" ></asp:Label>
    </div>
    </form>
</body>
</html>


