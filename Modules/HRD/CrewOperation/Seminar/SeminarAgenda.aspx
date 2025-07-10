<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeminarAgenda.aspx.cs" Inherits="SeminarAgenda" Title="Seminar Agenda" Async="true" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eMANAGER-HRD</title>
    <link href="https://fonts.googleapis.com/css?family=Ruda" rel="stylesheet" />
    
    <script src="../../JS/jquery-1.10.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../JS/KPIScript.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Styles/jquery.datetimepicker.css" />
    <link rel="stylesheet" type="text/css" href="../../Styles/SeminarAgenda.css?ddd" />
    <link rel="stylesheet" type="text/css" href="../../Styles/StyleSheet.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />
    <%--<script src="KPIScript.js" type="text/javascript"></script>--%>
    <style type="text/css">
    body
    {
       color: #393838;    
        font-family: 'Ruda', sans-serif;
        padding: 0px !important;
        margin: 0px !important;
        font-size: 11px;
    }
    
    </style>
    <script type="text/javascript">
    function EditAgenda(id,type) {
        $("#hfdAgendaID").val(id);
        $("#hfdActionType").val(type);
        $("#btnPost").click();
    }
    function DeleteAgenda(id, type) {

        if (confirm('Are you sure to delete this agenda?')) {
            $("#hfdAgendaID").val(id);
            $("#hfdActionType").val(type);
            $("#btnPost").click();
        }
        else {
            return false;
        }
    }
    function Download(id, type)
    {
        $("#hfdAgendaID").val(id);
        $("#hfdActionType").val(type);
        $("#btnPost").click();
    }
    
</script>
    
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <asp:Button runat="server" ID="btnPost" OnClick="btnPost_Click" Style="display: none;" />
        <asp:HiddenField runat="server" ID="hfdAgendaID" />
        <asp:HiddenField runat="server" ID="hfdActionType" />
    <div class="fixedheader">
            <div class="text headerband">
               Company - Crew Events
            </div>
          <table width="100%" cellpadding="10" cellspacing="0" border="0">
        <tr>
                <td>
                    <div class="topic">
                        <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                        ( <asp:LinkButton ID="lnkEditSeminarMaster" runat="server" Text="Modify event details" OnClick="lnkEditSeminarMaster_OnClick" style="color:#fa3030;font-size:14px;" ></asp:LinkButton> )
                    </div>
                    <div class="category">
                        <asp:Label runat="server" ID="lblCategoryName"></asp:Label> ( <asp:Label runat="server" ID="lblOfficeName"></asp:Label> )
                    </div>
                    <div class="date">
                        <i class="fa fa-calendar"></i>
                        <asp:Label runat="server" ID="lblDuration"></asp:Label>
                    </div>
                    <%--<div class="time">
                        <i class="fa fa-clock-o"></i>
                        <asp:Label runat="server" ID="lblStartTime"></asp:Label>
                    </div>--%>
                    <div class="address">
                        <i class="fa fa-map-marker"></i>
                        <asp:Label runat="server" ID="lblEventLocation"></asp:Label>
                    </div>
                </td>
                <td style="width:50%; text-align:right; vertical-align:top;">
                    <div class="topic">
                        <asp:Label runat="server" ID="a" Text="R.S.V.P. Conatct Information"></asp:Label>
                    </div>
                    <div class="contactname">
                        <asp:Label runat="server" ID="lblContactPerson"></asp:Label>
                    </div>
                    <div class="contactnumber">
                        <asp:Label runat="server" ID="lblContactNumber"></asp:Label>
                        </div>
                    <div class="contactmail">
                        <asp:Label runat="server" ID="lblContactEmail"></asp:Label>
                        </div>
                     <%--<div class="">
                        <asp:LinkButton runat="server" ID="lnkEditSeminar" OnClick="lnkEditSeminar_Click" Text="Edit"></asp:LinkButton>                        </div>--%>
                </td>
            </tr>
        </table>
          <div style="text-align:left" class="listheader">
            <span class="topic">Agenda </span>
            <asp:LinkButton runat="server" ID="btnAddAgenda" Text=" + Add Agenda" OnClick="btnAddAgenda_Click" style="color:#000000;font-weight:bold;font-size:15px; float:right; text-decoration:none; margin-top:2px;"  ></asp:LinkButton>
           </div>
        
    </div>
    <div class="content">
        <div>
        <asp:Repeater ID="rptAgenda"  runat="server">
            <ItemTemplate>
                <div class="row">
                    <table width="100%" cellpadding="2" cellspacing="0" border="0">
                    <tr >
                    <td style="width:40px; vertical-align:top; text-align:center;">
                        <div class="listaction" onclick="EditAgenda(<%#Eval("TableID")%>,'E')"><i class="fa fa-pencil" ></i></div>
                        <div class="listaction" onclick="return DeleteAgenda(<%#Eval("TableID")%>,'D')" ><i class="fa fa-trash" ></i></div>
                    </td>
                    <td style="vertical-align:top;">
                        <div class="agendatime"><i class="fa fa-clock-o"></i> <%#GetAgendaDate(Eval("StartTime"),Eval("EndTime"))%></div>
                        <div style='cursor:pointer;display:<%#Eval("download")%>' class="agendatime" onclick="Download(<%#Eval("TableID")%>,'DW')"><i class="fa fa-download" ></i> Download </div>
                        <div class="agendatext"> <%#Eval("AGENDA")%> </div>
                    </td>
                    <td style="width:250px; vertical-align:top;">
                        <div class="presenterheading" >Presenters </div>
                        <%#  ShowPresenter(Eval("PresenterId").ToString()) %>                        
                    </td>
                    </tr>
                    </table>                                                            
                </div>
            </ItemTemplate>
        </asp:Repeater>                                
        </div>                
        <!-- IFRAME SECTION -->
        <div style="position: fixed; top: 0px; left: 0px; height: 100%; width: 100%;z-index:10;" id="dvFrame"
                runat="server" visible="false">
                <center>
                <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
                <div style="position:relative;text-align :center;background : white; z-index:150;top:50px; border:solid 5px black; width:900px;" >
                    <center >
                    <div style="">
    <div style="text-align: center; padding:8px; font-size:13px; " class="text headerband">
        <b>Add New Agenda</b>&nbsp;
    </div>
    <table border="0" bordercolor="#e2e2e2" cellpadding="0" cellspacing="0" style="border-collapse:collapse" width="100%">
    <tr>
        <td style=" vertical-align:top; width:50%;border-right:solid 1px #ccc;">
            <table border="0" bordercolor="#e2e2e2" cellpadding="2" cellspacing="0" style="border-collapse:collapse" width="100%">
            <tr>
                <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">Agenda</td>
                <td style="padding: 8px; color:#333; background-color:#eee; text-align: left;">&nbsp;</td>
            </tr>
            <tr>
                <td style="color:White; text-align: left;padding: 8px;">
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
                <td style="color:White; text-align: left;padding: 8px;">
                    <asp:TextBox ID="txtTime1" runat="server" CssClass="datetime mandate" ValidationGroup="addedit" Width="125px"></asp:TextBox>
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
                <td style="color:White; text-align: left;padding: 8px;">
                    <asp:TextBox ID="txtTime2" runat="server" CssClass="datetime mandate" 
                        ValidationGroup="addedit" Width="125px"></asp:TextBox>
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
                <td style="color:White; text-align: left;padding: 8px;">
                    <asp:FileUpload runat="server" ID="flpUpload" Width="500px" />
                    </td>
                <td style="color:White;">
                                
                    </td>
            </tr>
            </table>
        </td>
        <td valign="top">
            <div style="padding: 8px; color:#333; background-color:#eee; text-align: left;"> Presenters </div>
            <div style="height:350px;overflow-x:hidden; overflow-y:scroll; text-align:left;">
            <asp:CheckBoxList runat="server" id="chkPresenters" RepeatDirection="Vertical"></asp:CheckBoxList>
        </div>
        </td>
    </tr>
    </table>
                
    </div>
                    <div style="padding:5px;text-align:right;background-color:#E2EAFF;">
                        <asp:Label runat="server" ID="lblMessage" Font-Size="15px" Font-Bold="true" style="float:left"></asp:Label>
                        <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" class="btn" width="100px" CausesValidation="true" ValidationGroup="main"/>   &nbsp;
                        <asp:Button runat="server" ID="btnCloseAgenda" Text="Close" CausesValidation="false" onclick="btnCloseAgenda_Click" CssClass="btnred" width="100px"/>   
                    </div>
                    </center>
                    </div>
                </center>
            </div>
    </div>

     <!-- IFRAME SECTION -->
    <div style="position: fixed; top: 0px; left: 0px; height: 100%; width: 100%;z-index:10;" id="divSeminar"
        runat="server" visible="false">
        <center>
            <div style="position: fixed; top: 0px; left: 0px; min-height: 100%; width: 100%;
                        background-color:black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)">
            </div>
            <div style="position: relative; text-align: center; background: white; z-index: 150;top: 30px; border: solid 5px black; width: 900px;">
                <center>
                    <div style="">
                        <iframe runat="server" id="frame1" width="100%" height="475px" frameborder="0"></iframe>
                    </div>
                    <div style="padding: 5px; text-align: right; background-color: #E2EAFF;">
                        <asp:Button runat="server" ID="Button1" Text="Close" OnClick="btnClose_Click" Style=" border: none; padding: 4px;" Width="100px" CssClass="btn" />
                    </div>
                </center>
            </div>
        </center>
    </div>
        
        <script  type="text/javascript">
            $(document).ready(function () {                
            
                $(window).scroll(function () {
                    document.cookie = "pos=" + $(window).scrollTop();
                });

                $(window).load(function () {
                    $(window).scrollTop(getCookie("pos"));
                });
            })


            function getCookie(cname) {
                var name = cname + "=";
                var ca = document.cookie.split(';');
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i];
                    while (c.charAt(0) == ' ') {
                        c = c.substring(1);
                    }
                    if (c.indexOf(name) == 0) {
                        return c.substring(name.length, c.length);
                    }
                }
                return "";
            }
    </script>

    <script type="text/javascript" src="/JS/jquery.datetimepicker.js"></script>
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
