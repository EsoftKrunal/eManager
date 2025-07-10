<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeminarAgenda.aspx.cs" Inherits="SeminarAgenda"
    Title="Seminar Agenda" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MTM-Crew Events</title>
    <link href='http://fonts.googleapis.com/css?family=PT+Sans:400,700' rel='stylesheet' type='text/css'>
    <script src="../JS/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JS/KPIScript.js"></script>
    <script type="text/javascript" src="../JS/jquery.datetimepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="../Styles/jquery.datetimepicker.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/SeminarAgenda.css" />
</head>
<style type="text/css">
    body
    {
        padding: 5px;
        font-family: 'PT Sans';
        margin: 0px;
        padding: 0px;
        color: #333;
    }
    .btn
    {
        background-color: #66a3ff;
        color: #ffffff;
        padding: 5px;
        border: none;
        margin: 3px 0px 3px 0px;
    }
    .btn:hover
    {
        background-color: #4d94ff;
        color: #ffffff;
        padding: 5px;
        border: none;
        margin: 3px 0px 3px 0px;
    }
    .btnred
    {
        background-color: Red;
        color: White;
        border: none;
        padding: 5px;
    }
</style>
<script type="text/javascript">
    function ChangeMenu(c) {
        $("#hfdMenuId").val(c);
        $("#btnPost").click();
    }
</script>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="TopMenu">
       MTM-Crew Events
    </div>
    <div class="Content">
        <table cellpadding="8" cellspacing="0" border="0" class="ContentHeader" width="100%">
            <colgroup>
                <col style="width: 150px" class="colhead" />
                <col class="colvalue" />
                <col style="width: 150px" class="colhead" />
                <col class="colvalue" />
                <col style="width: 150px" class="colhead" />
                <col class="colvalue" />
            </colgroup>
            <tr>
                <td>
                    Recruiting Office :
                </td>
                <td>
                    <asp:Label runat="server" ID="lblOfficeName"></asp:Label>
                </td>
                <td>
                    Category :
                </td>
                <td>
                    <asp:Label runat="server" ID="lblCategoryName"></asp:Label>
                </td>
                <td>
                    Plan Duration :
                </td>
                <td>
                    <asp:Label runat="server" ID="lblDuration"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Event Location :
                </td>
                <td>
                    <asp:Label runat="server" ID="lblEventLocation"></asp:Label>
                </td>
                <td>
                    Remarks :
                </td>
                <td colspan="3">
                    <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="ContentDetail">
            <table cellpadding="0" cellspacing="0" style="width: 100%; border-collapse: collapse;
                text-align: center;">
                <tr>
                    <td style="vertical-align: top; padding-left: 5px;">
                        <div style="padding-top: 15px; margin: 0px;">
                            <div class="tab">
                                <div class="menu active" id="tdPresenter" runat="server" onclick="ChangeMenu(1);"
                                    style="margin-left: 0px; border-left: solid 1px #c2c2c2;">
                                    Presenters</div>
                                <div class="menu" id="tdAttendies" runat="server" onclick="ChangeMenu(2);">
                                    Attendies</div>
                                <asp:Button runat="server" ID="btnPost" OnClick="btnPost_Click" Style="display: none;" />
                                <asp:HiddenField runat="server" ID="hfdMenuId" />
                            </div>
                            <div style="background-color: White; margin: 0px; position: relative; border-left: solid 1px #c2c2c2;
                                background-color: #FAFAFA; border: solid 1px #c2c2c2;">
                                <asp:Panel runat="server" ID="pnl1">
                                    <div style="padding: 5px; text-align: left;">
                                        <div style="text-align: right;">
                                            <asp:Button runat="server" ID="btnAddAgenda" Text=" + Add Agenda" class="btn hanging"
                                                OnClick="btnAddAgenda_Click" />
                                        </div>
                                        <div style="height: 35px; overflow-x: hidden; overflow-y: scroll;">
                                            <table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse;
                                                height: 35px;" width="100%">
                                                <colgroup>
                                                    <col width="40px" />
                                                    <col />
                                                </colgroup>
                                                <tr style="background-color: #555;">
                                                    <td style="color: White; text-align: center;">
                                                        Sr#
                                                    </td>
                                                    <td style="color: White;">
                                                        Presenter
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="height: 500px; overflow-x: hidden; overflow-y: scroll; border-left: solid 1px #c2c2c2;
                                            border-bottom: solid 1px #c2c2c2; font-size: 12px;">
                                            <asp:Repeater ID="rptPresenters" runat="server">
                                                <ItemTemplate>
                                                    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse;"
                                                        width="100%">
                                                        <tr style="background-color: #F2F2F2; color: #6E6E6E;">
                                                            <td style="text-align: center; width: 40px;">
                                                                <%#Eval("SNO")%>.
                                                            </td>
                                                            <td style="text-align: left">
                                                                <%#Eval("PRESENTERNAME")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div>
                                                        <table border="0" bordercolor="#e2e2e2" cellpadding="3" cellspacing="0" style="border-collapse: collapse;
                                                            margin-left: 30px" width="95%">
                                                            <colgroup>
                                                                <col width="40px" />
                                                                <col />
                                                                <col width="160px" />
                                                                <col width="160px" />
                                                                <col width="60px" />
                                                                <col width="60px" />
                                                            </colgroup>
                                                            <asp:Repeater ID="rptAgenda" DataSource='<%#BindAgenda(Eval("PresenterId"))%>' runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center;">
                                                                            <%#Eval("SNO")%>.
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <%#Eval("AGENDA")%>
                                                                        </td>
                                                                        <td>
                                                                            <%#DateTime.Parse(Eval("STARTTIME").ToString()).ToString("dd-MMM-yyyy hh:hh tt")%>
                                                                        </td>
                                                                        <td>
                                                                            <%#DateTime.Parse(Eval("ENDTIME").ToString()).ToString("dd-MMM-yyyy hh:hh tt")%>
                                                                        </td>
                                                                        <td style="text-align: left">
                                                                            <asp:ImageButton runat="server" ID="btEditAgenda" OnClick="btEditAgenda_Click" ToolTip="Modify Agenda"
                                                                                ImageUrl="~/Modules/HRD/Images/editX12.jpg" CommandArgument='<%#(Eval("TableId").ToString())%>' />
                                                                            <asp:ImageButton ID="btnDeleteAgenda" runat="server" CommandArgument='<%#(Eval("TableId").ToString())%>'
                                                                                ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="btnDeleteAgenda_Click" OnClientClick="return confirm('Are you sure to delete?');"
                                                                                ToolTip="Delete Agenda" />
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <asp:ImageButton runat="server" ID="btDownloadAttachment" OnClick="btDownloadAttachment_Click"
                                                                                ToolTip="Download Attachment" Visible='<%#(Eval("AttachmentName").ToString()!="") %>'
                                                                                ImageUrl="~/Modules/HRD/Images/paperclip.png" CommandArgument='<%#Eval("TableId")%>' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnl2" Visible="false">
                                    <div style="padding: 5px; text-align: left;">
                                        <div style="text-align: right;">
                                            <asp:Button runat="server" ID="Button1" Text=" + Invite Crew Members" class="btn" OnClick="btnInvite_Click" />
                                            <asp:Button runat="server" ID="Button2" Text="Send Invitation Mail" class="btn" OnClick="btnInviteMail_Click" />
                                        </div>
                                        <div style="height: 35px; overflow-x: hidden; overflow-y: scroll; text-align: left; font-size:13px;">
                                            <table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse;
                                                height: 35px;" width="100%">
                                                <colgroup>
                                                    <col width="50px" />
                                                    <col width="40px" />
                                                    <col width="100px" />
                                                    <col />
                                                    <col width="100px" />
                                                    <col width="140px" />
                                                    <col width="100px" />
                                                   <%-- <col width="60px" />--%>
                                                   <col width="100px" />
                                                    <col width="100px" />
                                                    <col width="60px" />
                                                <%--    <col width="60px" />--%>
                                                    <col width="30px" />
                                                </colgroup>
                                                <tr style="background-color: #555;">
                                                    <td style="color: White; text-align: center;">
                                                        Select
                                                    </td>
                                                    <td style="color: White; text-align: center;">
                                                        Sr#
                                                    </td>
                                                    <td style="color: White; text-align: center">
                                                        Crew#
                                                    </td>
                                                    <td style="color: White;">
                                                        Crew Name
                                                    </td>
                                                    <td style="color: White;">
                                                        Rank
                                                    </td>
                                                    <td style="color: White;">
                                                        City
                                                    </td>
                                                    <td style="color: White; text-align: center">
                                                        Invite Sent
                                                    </td>
                                                    <%--<td style="color: White; text-align: center">
                                                        Ack.
                                                    </td>--%>
                                                    <td style="color: White; text-align: center">
                                                        Confirm
                                                    </td>
                                                    <td style="color: White; text-align: center">
                                                        Confirmed On
                                                    </td>
                                                    
                                                  <%--  <td style="color: White; text-align: center">
                                                        Joined
                                                    </td>--%>
                                                    <td style="color: White; text-align: center">
                                                        Action
                                                    </td>
                                                    <td style="color: White; text-align: left">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="height: 500px; overflow-x: hidden; overflow-y: scroll; font-size: 12px;">
                                            <table border="1" bordercolor="#e2e2e2" cellpadding="3" cellspacing="0" style="border-collapse: collapse"
                                                width="100%">
                                                <colgroup>
                                                    <col width="50px" />
                                                    <col width="40px" />
                                                    <col width="100px" />
                                                    <col />
                                                    <col width="100px" />
                                                    <col width="140px" />
                                                    <col width="100px" />
                                                    <%--<col width="60px" />--%>
                                                   <col width="100px" />
                                                    <col width="100px" />
                                                    <col width="60px" />
                                                    <%--<col width="60px" />--%>
                                                    <col width="30px" />
                                                </colgroup>
                                                <asp:Repeater ID="rptInvite" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: center;">
                                                                <asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("CrewId")%>' />
                                                                <asp:HiddenField runat="server" ID="hfdGUID" Value='<%#Eval("GUID")%>' />
                                                                <asp:HiddenField runat="server" ID="hfdEmail" Value='<%#Eval("Email")%>' />

                                                                <asp:HiddenField runat="server" ID='lblRankCode' Value='<%#Eval("RankCode")%>'></asp:HiddenField>
                                                                <asp:HiddenField runat="server" ID='lblName' Value='<%#Eval("CREWNAME")%>'></asp:HiddenField>
                                                                <asp:HiddenField runat="server" ID='hfReplyStatusCode' Value='<%#Eval("ReplyStatusCode")%>'></asp:HiddenField>
                                     
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%#Eval("SNO")%>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%#Eval("CREWNUMBER")%>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <%#Eval("CREWNAME")%>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <%#Eval("RANKCODE")%>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <%#Eval("CITY")%>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%#Common.ToDateString(Eval("RequstedOn"))%>
                                                            </td>
                                                          <%--  <td style="text-align: center;">
                                                                <%#(Common.ToDateString(Eval("RepliedOn"))=="")?"No":"Yes"%>
                                                            </td>--%>
                                                             <td style="text-align: center;">
                                                                <%#Eval("ReplyStatus")%>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%#Common.ToDateString(Eval("RepliedOn"))%>
                                                            </td>
                                                           
                                                            <%--<td style="text-align: center;">
                                                                <asp:CheckBox runat="server" ID="CheckBox1" CssClass='<%#Eval("CrewId")%>' />
                                                                <%#Eval("Joined")%>
                                                            </td>--%>
                                                            <td style="text-align: center">
                                                                <asp:ImageButton runat="server" ID="btEditAttendies" ToolTip="Modify Attendies" ImageUrl="~/Modules/HRD/Images/editX12.jpg"
                                                                    CommandArgument='<%#(Eval("TableId").ToString())%>' OnClick="btEditAttendies_OnClick"/>
                                                                
                                                                <asp:ImageButton ID="btnDeleteInvite" runat="server" CommandArgument='<%#(Eval("TableId").ToString())%>'
                                                                    ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="btnDeleteInvite_Click" OnClientClick="return confirm('Are you sure to delete?');"
                                                                    ToolTip="Delete Attendies" />
                                                            </td>
                                                            <td style="color: White; text-align: left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <!-- IFRAME SECTION -->
                        <div style="position: fixed; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvFrame"
                            runat="server" visible="false">
                            <center>
                            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
                            <div style="position:relative;text-align :center;background : white; z-index:150;top:50px; border:solid 5px black; width:900px;" >
                                <center >
                                <div style="">
                <div style="background-color:#7094FF;text-align: center; padding:8px; font-size:13px; color:White;">
                    <b>Add New Agenda</b>&nbsp;
                </div>
                <table border="0" bordercolor="#e2e2e2" cellpadding="2" cellspacing="0" style="border-collapse:collapse" width="100%">
                <tr>
                <td style=" vertical-align:top; width:50%;">
                      <table border="0" bordercolor="#e2e2e2" cellpadding="2" cellspacing="0" style="border-collapse:collapse" width="100%">
                        <tr>
                            <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">
                                Presenter</td>
                            <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">&nbsp;</td>
                        </tr>
                        <tr>
                            
                            <td style="color:White; text-align: left;"> 
                                <asp:DropDownList runat="server" id="ddlPresenter" RepeatColumns="2" RepeatDirection="Horizontal" Width="250px">                                
                            </asp:DropDownList>
                            </td>
                       <td></td>
                      </tr>
                        <tr>
                            <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">Agenda</td>
                            <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="color:White; text-align: left;">
                                <asp:TextBox ID="txtTopic1" runat="server" CssClass="mandate" 
                                    ValidationGroup="addedit" Width="99%" TextMode="MultiLine" Height="110px"></asp:TextBox>
                            </td>
                            <td style="color:White;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtTopic1" ErrorMessage="*" ValidationGroup="addedit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">
                                Start Time</td>
                            <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">
                                &nbsp;</td>
                        </tr>
                         <tr>
                            <td style="color:White; text-align: left;">
                                <asp:TextBox ID="txtTime1" runat="server" CssClass="datetime mandate" ValidationGroup="addedit" Width="120px"></asp:TextBox>
                                </td>
                            <td style="color:White;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtTime1" ErrorMessage="*" ValidationGroup="addedit"></asp:RequiredFieldValidator>
                             </td>
                        </tr>
                        <tr>
                           <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">
                                End Time</td>
                           <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">
                                &nbsp;</td>
                        </tr>
                         <tr>
                            <td style="color:White; text-align: left;">
                                <asp:TextBox ID="txtTime2" runat="server" CssClass="datetime mandate" 
                                    ValidationGroup="addedit" Width="120px"></asp:TextBox>
                                </td>
                            <td style="color:White;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtTime2" ErrorMessage="*" ValidationGroup="addedit"></asp:RequiredFieldValidator>
                             </td>
                        </tr>
                     <tr>
                           <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">
                                Attachment </td>
                           <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">
                                &nbsp;</td>
                        </tr>
                         <tr>
                            <td style="color:White; text-align: left;">
                                <asp:FileUpload runat="server" ID="flpUpload" Width="500px" />
                                </td>
                            <td style="color:White;">
                                
                             </td>
                        </tr>
                        </table>
                    </td>
                </tr>
                </table>
                
                </div>
                                <div style="padding:5px;text-align:right;background-color:#E2EAFF;">
                                    <asp:Label runat="server" ID="lblMessage" Font-Size="20px" Font-Bold="true" style="float:left"></asp:Label>
                                    <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" class="btn" width="100px" CausesValidation="true" ValidationGroup="main"/>   &nbsp;
                                    <asp:Button runat="server" ID="btnClose" Text="Close" CausesValidation="false" onclick="btnClose_Click" CssClass="btnred" width="100px"/>   
                                </div>
                                </center>
                                </div>
                            </center>
                        </div>
                        <%---New ---------------------------------------------------------------------------------------------------%>
                        <div style="position: fixed; top: 0px; left: 0px; height: 100%; width: 100%;z-index:2;" id="divAttendies"
                            runat="server" visible="false">
                            <center>
                              <div style="background-color:#7094FF;text-align: center; padding:8px; font-size:13px; color:White;">
                                <b>Invite Crew Members</b>&nbsp;
                            </div>
                            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
                                <div style="position:relative;text-align :center;background : white; z-index:150;top:50px; border:solid 5px black; width:90%;" >
                                    <center >
                                    <asp:UpdatePanel runat="server" ID="fsdaf">
                                    <ContentTemplate>
                                    <div>
                                    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse; height:35px;" width="100%">
                                     
                                    <tr style=" background:#555; color:white;">
                                        <td style="width:60px">Records</td>
                                        <td style="width:80px">Crew#</td>
                                        <td style="width:80px">Rank</td>
                                        <td style="width:80px">Off / Rat</td>
                                        <td>Crew Name</td>
                                        <td style="width:80px">Status</td>
                                        <td style="width:150px">City</td>
                                        <td style="width:150px">Recruiting Office</td>
                                        <td rowspan="2" style="text-align:center">
                                            <asp:Button runat="server" ID="btnFilter" Text="Search" onclick="btnFilter_Click" class="btn" width="100px" CausesValidation="false"/>
                                        </td>
                                    </tr>
                                   <tr style=" background:#555; color:white;">
                                        <td><asp:DropDownList runat="server" ID="ddlnor">
                                            <asp:ListItem Text="50"></asp:ListItem>
                                            <asp:ListItem Text="100"></asp:ListItem>
                                            <asp:ListItem Text="200"></asp:ListItem>
                                            <asp:ListItem Text="500"></asp:ListItem>
                                            <asp:ListItem Text="ALL"></asp:ListItem>
                                        </asp:DropDownList></td>
                                        <td><asp:TextBox runat="server" ID="txtcrewn" Width="99%" MaxLength="6"></asp:TextBox></td>
                                        <td><asp:DropDownList runat="server" ID="ddlRank" Width="99%"></asp:DropDownList></td>
                                        <td><asp:DropDownList runat="server" ID="ddlOR">
                                            <asp:ListItem Text="ALL"></asp:ListItem>
                                            <asp:ListItem Text="Officer" Value="O"></asp:ListItem>
                                            <asp:ListItem Text="Rating" Value="R"></asp:ListItem>
                                        </asp:DropDownList></td>
                                        <td><asp:TextBox runat="server" ID="txtCrewName" Width="99%" MaxLength="6"></asp:TextBox></td>
                                        <td><asp:DropDownList runat="server" ID="ddlCrewStatus" Width="99%" ></asp:DropDownList></td>
                                        <td><asp:TextBox runat="server" ID="txtCity" Width="99%" Text=""></asp:TextBox></td>
                                        <td><asp:DropDownList runat="server" ID="ddlRecuitingOffice" Width="99%" ></asp:DropDownList></td>
                                    </tr>
                                    </table>
                                    <div style="height:300px; overflow-x:hidden; overflow-y:scroll;font-size:13px;">
                                    <table border="1" bordercolor="#e2e2e2" cellpadding="2" cellspacing="0" style="border-collapse:collapse" width="100%">
                                   
                                    <asp:Repeater runat="server" ID="rptCrewList">
                                    <ItemTemplate>
                                    <tr>
                                        <td style="text-align:left; width:60px"><asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("CrewId")%>'/> </td>
                                        <td style="width:80px"><%#Eval("CREWNUMBER")%></td>
                                        <td style="width:160px"><asp:Label runat="server" ID='lblRankCode' Text='<%#Eval("RankCode")%>'></asp:Label></td>
                                        <td style="text-align:left"><asp:Label runat="server" ID='lblName' Text='<%#Eval("CREWNAME")%>'></asp:Label></td>
                                        <td style="width:80px"><%#Eval("CREWSTATUS")%></td>
                                        <td style="text-align:left; width:150px;"><%#Eval("CITY")%></td>
                                        <td style="width:150px"><%#Eval("RecruitingOfficeName")%></td>
                                        <td  style="width:150px">&nbsp;</td>
                                    </tr>
                                    </ItemTemplate>
                                    </asp:Repeater>
                
                                    </table>
                                    </div>
                                    </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div style="padding:5px;text-align:right;background-color:#E2EAFF;">
                                        <asp:Label runat="server" ID="lblMessage1" Font-Size="20px" Font-Bold="true" style="float:left"></asp:Label>
                                        <asp:Button runat="server" ID="btnSave2" Text="Save" onclick="btnSave2_Click" class="btn" width="100px" CausesValidation="true" ValidationGroup="main"/>   &nbsp;
                                        <asp:Button runat="server" ID="btnClose2" Text="Close" CausesValidation="false" onclick="btnClose2_Click" style="background-color:Red; color:White;border:none; padding:5px;"  width="100px"/>   
                                    </div>
                                    </center>
                                    </div>
                            </center>
                        </div>
                        <%---iframe Edit Attendies ---------------------------------------------------------------------------------------------------%>
                        <div style="position: fixed; top: 0px; left: 0px; height: 100%; width: 100%;" id="divEditAttendies" runat="server" visible="false">
                            <center>
                            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
                            <div style="position:relative;text-align :center;background : white; z-index:150;top:50px; border:solid 5px black; width:500px;height:150px;" >
                                <center>
                                    <br />                                    
                                    <table cellpadding="0" cellspacing="0" width="50%" border="0" style="margin:0px auto;">
                                    <tr>
                                    <td style="width:150px">
                                        <asp:RadioButtonList ID="rdoGrade" RepeatDirection="Horizontal" runat="server" Width="399px" >
                                            <asp:ListItem Text="Attending" Value="P" ></asp:ListItem>
                                            <asp:ListItem Text="Not Attending" Value="A" ></asp:ListItem>
                                            <asp:ListItem Text="To be Decided" Value="N" ></asp:ListItem>
                                        </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdoGrade" InitialValue="" ErrorMessage="*" ForeColor="Red" Display="Dynamic"  runat="server"></asp:RequiredFieldValidator>                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button runat="server" ID="btnUpdateAttendies" Text="Save" CausesValidation="false" onclick="btnUpdateAttendies_Click" CssClass="btn" width="100px"/>   
                                            <asp:Button runat="server" ID="btnCloseAttendies" Text="Close" CausesValidation="false" onclick="btnCloseAttendies_Click" CssClass="btnred" width="100px"/>   
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">                                             
                                            <asp:Label ID="lblMessageAttendies" runat="server" Font-Size="20px" Font-Bold="true" ></asp:Label>
                                        </td>
                                        
                                    </tr>
                                    </table>

                                </center>
                            </div>
                            
                            </center>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
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
