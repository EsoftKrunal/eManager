<%@ Page MasterPageFile="~/Modules/LPSQE/PositionReport/VesselPositionReporting.master" Language="C#" AutoEventWireup="true" CodeFile="VPR_EEOI.aspx.cs" Inherits="VPR_EEOI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
     .pink
    {
    	background-color:#FFCCE0;
    	color:White;
    }
    .btn1
    {
        border:none;
        border-bottom:none;
        background-color:#B2D1FF;
        padding:5px;
    }

    .tblHeading
    {
        text-align:center;
        background-color:#c2c2c2;
        color:White;
        font-size:10px;
        
    }
     .tblClass
    {
        
    }
     .total
    {
       background-color:#F7F2E0; 
       font-weight:bold;
    }
    .total:hover
    {
       background-color:#c2c2c2;
    }

    .Row
    {
        
    }
    .Row:hover
    {
       background-color:#c2c2c2; 
    }
    .fieldname
    {
        width:170px;
        border:solid 1px green;
        padding:3px;
        background-color:#CCFFFF;
        float:left;
        clear:both;
    }
    .operator
    {
        width:150px;
        border:solid 1px green;
        padding:0px;
        background-color:#CCFFFF;
        float:left;
    }
    .values
    {
        width:330px;
        border:solid 1px green;
        padding:0px;
        background-color:#CCFFFF;
        float:left;
        height:21px;
    }
    
    </style>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
    <ProgressTemplate>
    <center>
    <div style='width:100%; height:150px; position:absolute;'>
        <img src="../../HRD/Images/loading1.gif" alt="Loading.." style="top:0px; " />
    </div>
    </center>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <div style=" overflow:auto;">
    <div style="float:left;display:none;">
        <asp:Button ID="btnVDA" runat="server" Text="Voyage Data Analysis" OnClick="btnVDA_OnClick" CssClass="btn1" />
        <asp:Button ID="btnEEOI" runat="server" Text="EEOI" OnClick="btnEEOI_OnClick" CssClass="btn1" style="background-color:#3385FF;color:white; border-bottom:none" />
    </div>
    
    </div>
    <%--<div style=" background-color:#3385FF; height:5px"></div>--%>
<div runat="server" id="trVessel" class="text headerband">
       <span style="font-size:20px; font-weight:bold;">
          Port to Port Voyage Basis EEOI Report ( <%=Page.Request.QueryString["VessleName"].ToString() %> )</span> 
</div>
<%--<div style="padding:0px; background-color:gray; color:Black; font-size:14px; text-align:left; padding-top:5px;padding-left:1px;">
<asp:Button runat="server" ID="btnPortToPort" Text="Port To Port Voyage Basis" style='border:solid 1px #99EB99; background-color:#99EB99; font-size:12px;' />
</div>
<div style=' height:10px; background-color:#99EB99'>

</div>--%>
<asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<div runat="server" id="dvFilter" style='border-left:solid 1px gray;' >
<table cellpadding="0" cellspacing="0" width="100%" border="0" style='border-collapse:collapse'>
<tr>
<td style="vertical-align:top">

<div id="dvscroll_D" style="overflow-x:hidden;overflow-y:scroll; height:500px" onscroll="SetScrollPos(this)" runat="server">
    <table cellpadding="1" cellspacing="0" border="1" style=" border-collapse:collapse;" bordercolor='#e2e2e2'>
    <tr style=' height:25px;' class= "headerstylegrid">
        <td style='width:80px; text-align:center'>Voy#</td>
        <td style='width:150px; text-align:center'>Departure Port</td>
        <td style='width:150px; text-align:center'>Next Port</td>
        <td style='width:90px; text-align:center'>Dep Date</td>
        <td style='width:90px; text-align:center'>Next Date</td>
        <td style='width:80px; text-align:center'>Total Dist.</td>
        <td style='width:80px; text-align:center'>HFO</td>
        <td style='width:80px; text-align:center'>LSFO</td>
        <td style='width:80px; text-align:center'>MGO1.0</td>
        <td style='width:80px; text-align:center'>MGO0.5</td>
        <td style='width:80px; text-align:center'>MDO</td>
        <td style='width:80px; text-align:center'>CARGO</td>
        <td style='width:120px; text-align:center'>Heavy Weather</td>
        <td style='width:100px; text-align:center'>Total Co2</td>
        <td style='width:100px; text-align:center'>EEOI</td>
        <td style='width:100px; text-align:center'>EEOI (Roll)</td>
    </tr>  
    <asp:Repeater runat="server" ID="rpt_Data">
    <ItemTemplate>
    <tr style=' height:18px;'>
        <td style='width:80px;'><%#Eval("VOYAGENO")%></td>
        <td style='width:150px;'><%#Eval("DEPPORT")%></td>
        <td style='width:150px;'><%#Eval("NEXTDEPPORT")%></td>
        <td style='width:90px; text-align:center'><%#Common.ToDateString(Eval("DEPDATE"))%></td>
        <td style='width:90px; text-align:center'><%#Common.ToDateString(Eval("NEXTDEPDATE"))%></td>
        <td style='width:80px; text-align:right'><%#Eval("TOTALDISTANCE")%></td>
        <td style='width:80px; text-align:right'><%#Eval("HFO")%></td>
        <td style='width:80px; text-align:right'><%#Eval("LFO")%></td>
        <td style='width:80px; text-align:right'><%#Eval("MGO1_0")%></td>
        <td style='width:80px; text-align:right'><%#Eval("MGO0_5")%></td>
        <td style='width:80px; text-align:right'><%#Eval("MDO")%></td>
        <td style='width:80px; text-align:right'><%#Eval("CARGO")%></td>
        <td style='width:120px; text-align:center'><%#Eval("WEATHER")%></td>
        <td style='width:100px; text-align:right'><%#Eval("CO2")%></td>
        <td style='width:100px; text-align:right'><%#String.Format("{0:0.00}",Eval("EEOI"))%></td>
        <td style='width:100px; text-align:right'><%#String.Format("{0:0.00}",Eval("EEOI_ROLL"))%></td>
    </tr>    
    </ItemTemplate>
    </asp:Repeater>
    </table>
</div>
<div id="dvscroll_I" style="overflow-y:hidden;overflow-x:hidden; height:402px; background-color:White;" onscroll="SetScrollPos(this)" runat="server" visible="false">
<div style="float:left; width:200px;  border-right:solid 1px gray; overflow:hidden;">
    <asp:Image runat="server" ID="imgChart" />
</div>
<div style="float:left;overflow-x:scroll;overflow-y:hidden;height:400px;width:1000px;">
    <asp:Image runat="server" ID="imgChart1" />
</div>
</div>
</td>
</tr>
</table>
</div>
<div style="padding:5px; background-color:#99EB99; color:Black; font-size:14px;">
Filters .....
</div>
<div style='border:solid 1px gray; height:70px;'>
    <div style='width:175px; padding:10px; font-size:11px; float:left; display:none;'>
    Select Vessel 
    <hr />
    <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" Width="170px"></asp:DropDownList>
    </div>
    <div style='width:190px; padding:10px; font-size:11px; float:left; margin-left:0px;'>
    Select Period
    <hr />
    <asp:TextBox runat="server" ID="txtFromDate" CssClass="input_box" Width="80px" MaxLength="15" ></asp:TextBox> -
    <asp:TextBox runat="server" ID="txtToDate" CssClass="input_box" Width="80px" MaxLength="15"></asp:TextBox>
    </div>
    <div style='width:80px; padding:10px; font-size:11px; float:left; margin-left:0px;'>
    Voy Condition
    <hr />
    <asp:DropDownList ID="ddlVoyCon" runat="server" CssClass="input_box" Width="80px" >
        <asp:ListItem Text="Any" Value=""></asp:ListItem>
        <asp:ListItem Text="Laden" Value="1"></asp:ListItem>
        <asp:ListItem Text="Ballast" Value="0"></asp:ListItem>
    </asp:DropDownList>
    </div>
    <div style='width:80px; padding:10px; font-size:11px; float:left; margin-left:0px;'>
    Position
    <hr />
    <asp:DropDownList ID="ddlPos" runat="server" CssClass="input_box" Width="80px">
            <asp:ListItem Text="Any" Value=""></asp:ListItem>
            <asp:ListItem Text="In Port" Value="('A','D','PD','PB','PA')"></asp:ListItem>
            <asp:ListItem Text="At Sea" Value="('N','A','D')"></asp:ListItem>
    </asp:DropDownList>
    </div>
    <div style='width:190px; padding:10px; font-size:11px; float:left; margin-left:0px;'>
    Rolling Average ( No of Voyage )
    <hr />
    <asp:TextBox runat="server" ID="txtPPY" Width='50px' style='text-align:center' Text='6' CssClass='input_box' ></asp:TextBox> <span style="font-size:12px"><b>x</b></span> Port To Port Voyage
    </div>
    <div style='width:200px; padding:10px; font-size:11px; float:left; margin-left:0px;'>
    Display Items
    <hr />
    <asp:CheckBox id="chkCo2" runat="server" Text="Co2" ForeColor="Purple" Checked="true" />
    <asp:CheckBox ID="chkEEOI" runat="server" Text="EEOI" ForeColor="Red" Checked="true"/>
    <asp:CheckBox ID="chkEEOI_R" runat="server" Text="EEOI Rolling" ForeColor="Blue" Checked="true"/>
    </div>
    <div style='width:120px; padding:10px; font-size:11px; float:left; margin-left:0px;'>
    Report Type
    <hr />
    <asp:RadioButton id="radChart" runat="server" Text="Chart" ForeColor="Purple" GroupName="dd" />
    <asp:RadioButton id="radTable" runat="server" Text="Table" ForeColor="Purple" Checked="true" GroupName="dd" />    
    </div>
    <div style='width:50px; padding:10px; font-size:11px; float:left; margin-left:0px;'>
    &nbsp;
    <hr />
    <asp:Button ID="btnShow" runat="server" Text="Show" onclick="btnShow_Click" CssClass="btn" />
    </div>
</div>
<div style="padding:4px;">
        <ajaxToolkit:CalendarExtender runat="server" ID="afsd" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender> 
        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender> 
</div>
</ContentTemplate>
<%--<Triggers></Triggers>--%>
</asp:UpdatePanel>
</asp:Content>