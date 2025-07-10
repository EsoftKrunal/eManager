<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeminarInvite.aspx.cs" Inherits="SeminarInvite" Title="Add Edit Seminar" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Circular Form</title>
    <script src="./eReports/JS/jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        .highlight
        {
            margin:3px 0px 3px 0px;
            background-color:#F0F5FF;
        }
        .highlight:hover
        {
           background-color:#FFB2D1;
        }
        
       *{
          font-family:calibri;  
          font-size:14px;
        }
        body
        {
            margin:0px;
        }
        .selectedrow
        {
            background-color : lightgray;
            color :White; 
            cursor:pointer;
        }
        .row
        {
            background-color : White;
            color :Black;
            cursor:pointer; 
        }
        hr
        {
            margin:0px;
            padding:2px;
        }
        .btn
        {
            background-color:#005CE6;
            color:White;
            border:solid 1px #005CE6;
            padding:4px;
        }
        .btnred
        {
            background-color:red;
            color:White;
            border:solid 1px red;
            padding:4px;
        }
        .mandate
        {
            background-color:#ffffcc !important;
            border:solid 1px grey;
            padding:2px;
        }
       .aquaScroll {
          scrollbar-base-color: bisque;
          scrollbar-arrow-color: #7094FF;
          border-color: orange;
          overflow-x:hidden; 
          overflow-y:scroll;
        }
        .changed
        {
            border:solid 1px red;
        }
        .saved
        {
            border:solid 1px green;
        }
    </style>
    <script type="text/javascript" src="./eReports/JS/KPIScript.js"></script>
    
   <link rel="stylesheet" type="text/css" href="eReports/CSS/jquery.datetimepicker.css"/>
</head>
<body >
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
            <table cellpadding="0" cellspacing="0" style="width: 100%;border-collapse:collapse; text-align: center;">
            <tr>
                <td style="background-color:#7094FF;text-align: center; padding:8px; font-size:14px; color:White;">
                    <b>MISS &amp; Seminar -&nbsp; Invite Peoples</b>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top">
                <div style="text-align: right; padding:2px; font-size:14px; color:White; margin-top:2px;">
                 
                <asp:Button runat="server" ID="btnAddAgenda" Text=" + Invite Crew Members" class="btn" onclick="btnInvite_Click"/>
                </div>
                   <div style="height:25px; overflow-x:hidden; overflow-y:scroll; text-align:left;">
                  
                     <table border="1" bordercolor="#e2e2e2" cellpadding="3" cellspacing="0" style="border-collapse:collapse" width="100%">
                         <colgroup>
                                <col width="40px" />
                                <col width="100px" />
                                <col />
                                <col width="140px" />
                                <col width="140px" />
                                <col width="140px" />
                                <col width="60px" />
                            </colgroup>
                        <tr style="background-color:#555; ">
                            <td style="color:White; text-align:center;">Sr#</td>
                            <td style="color:White; text-align:center">Crew#</td>
                            <td style="color:White;">Crew Name</td>
                            <td style="color:White;text-align:center">Reqested On</td>
                            <td style="color:White;text-align:center">Answer</td>
                            <td style="color:White;text-align:center">Replied On</td>
                            <td style="color:White;text-align:center">Action</td>
                        </tr>
                        </table>
                         </div>
                    <div style="height:260px; overflow-x:hidden; overflow-y:scroll;">

                        <table border="1" bordercolor="#e2e2e2" cellpadding="3" cellspacing="0" style="border-collapse:collapse" width="100%">
                            <colgroup>
                                <col width="40px" />
                                <col width="100px" />
                                <col />
                                <col width="140px" />
                                <col width="140px" />
                                <col width="140px" />
                                <col width="60px" />
                            </colgroup>
                            <asp:Repeater ID="rptInvite" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align:center;"><%#Eval("SNO")%></td>
                                        <td style="text-align:center;"><%#Eval("CREWNUMBER")%></td>
                                        <td style="text-align:left;"><%#Eval("CREWNAME")%></td>
                                        <td style="text-align:center;"><%#Common.ToDateString(Eval("RequstedOn"))%></td>
                                        <td style="text-align:center;"><%#Eval("ReplyStatus")%></td>
                                        <td style="text-align:center;"><%#Common.ToDateString(Eval("RepliedOn"))%></td>
                                        <td style="text-align:left">
                                            <asp:ImageButton ID="btnDeleteInvite" runat="server" CommandArgument='<%#(Eval("TableId").ToString())%>' ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="btnDeleteInvite_Click" OnClientClick="return confirm('Are you sure to delete?');" ToolTip="Delete Agenda" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
               
                </div>
           
                </td>
            </tr>
            </table>
            <div style="position:fixed;top:0px;left:0px; height :100%; width:100%;" id="dvFrame" runat="server" visible="false">
            <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
            <div style="position:relative;text-align :center;background : white; z-index:150;top:0px; border:solid 5px black; width:900px;" >
                <center >
                <asp:UpdatePanel runat="server" ID="fsdaf">
                <ContentTemplate>
                <div>
                <table border="0" bordercolor="#e2e2e2" cellpadding="2" cellspacing="0" style="border-collapse:collapse" width="100%">
                <colgroup>
                <col style="width:100px"/>
                <col style="width:80px"/>
                <col/>
                <col style="width:100px"/>
                <col style="width:200px"/>
                </colgroup>
                <tr style=" background:#888; color:white;">
                    <td>No Of Records</td>
                    <td>Crew#</td>
                    <td>Crew Name</td>
                    <td>Status</td>
                    <td>Recruiting Office</td>
                </tr>
                <tr style=" background:#888; color:white;" >
                    <td><asp:DropDownList runat="server" ID="ddlnor" AutoPostBack="true" OnTextChanged="LoadCrewList">
                        <asp:ListItem Text="50"></asp:ListItem>
                        <asp:ListItem Text="100"></asp:ListItem>
                        <asp:ListItem Text="200"></asp:ListItem>
                        <asp:ListItem Text="500"></asp:ListItem>
                        <asp:ListItem Text="ALL"></asp:ListItem>
                    </asp:DropDownList></td>
                    <td><asp:TextBox runat="server" ID="txtcrewn" Width="99%" MaxLength="6" AutoPostBack="true" OnTextChanged="LoadCrewList"></asp:TextBox></td>
                    <td><asp:TextBox runat="server" ID="txtCrewName" Width="99%" MaxLength="6" AutoPostBack="true" OnTextChanged="LoadCrewList"></asp:TextBox></td>
                    <td><asp:DropDownList runat="server" ID="ddlCrewStatus" Width="99%"  AutoPostBack="true" OnSelectedIndexChanged="LoadCrewList"></asp:DropDownList></td>
                    <td><asp:DropDownList runat="server" ID="ddlRecuitingOffice" Width="99%" AutoPostBack="true" OnSelectedIndexChanged="LoadCrewList" ></asp:DropDownList></td>
                </tr>
                </table>
                <div style="height:300px; overflow-x:hidden; overflow-y:scroll;">
                <table border="1" bordercolor="#e2e2e2" cellpadding="2" cellspacing="0" style="border-collapse:collapse" width="100%">
                <colgroup>
                <col style="width:100px"/>
                <col style="width:80px"/>
                <col/>
                <col style="width:100px"/>
                <col style="width:200px"/>
                </colgroup>
                <asp:Repeater runat="server" ID="rptCrewList">
                <ItemTemplate>
                <tr>
                    <td><asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("CrewId")%>'/> </td>
                    <td><%#Eval("CREWNUMBER")%></td>
                    <td style="text-align:left"><%#Eval("CREWNAME")%></td>
                    <td><%#Eval("CREWSTATUS")%></td>
                    <td><%#Eval("RecruitingOfficeName")%></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                
                </table>
                </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                <div style="padding:5px;text-align:right;background-color:#E2EAFF;">
                       <asp:Label runat="server" ID="lblMessage" Font-Size="20px" Font-Bold="true" style="float:left"></asp:Label>
                    <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" class="btn" width="100px" CausesValidation="true" ValidationGroup="main"/>   &nbsp;
                    <asp:Button runat="server" ID="btnClose" Text="Close" CausesValidation="false" onclick="btnClose_Click" style="background-color:Red; color:White;border:none; padding:5px;"  width="100px"/>   
                </div>
                </center>
                </div>
            </center>
       </div>


<script type="text/javascript" src="eReports/JS/jquery.datetimepicker.js"></script>
<script type="text/javascript">

    function SetCalender() {
        $('.dateonly').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
        $('.datetime').datetimepicker({ format: 'd-M-Y H:i', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
    }
    function Page_CallAfterRefresh() {
        SetCalender();
    }
    SetCalender();
</script>
    </form>
</body>
</html>
