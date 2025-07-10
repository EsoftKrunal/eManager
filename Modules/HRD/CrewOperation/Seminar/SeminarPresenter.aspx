<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeminarPresenter.aspx.cs" Inherits="SeminarPresenter" Title="Seminar Presenters" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="https://fonts.googleapis.com/css?family=Ruda" rel="stylesheet" />
    <script src="../../JS/jquery-1.10.2.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="../../JS/KPIScript.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Styles/jquery.datetimepicker.css" />
    <link rel="stylesheet" type="text/css" href="../../Styles/SeminarAgenda.css?ddd" />
    
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../../Styles/StyleSheet.css" />
     <link rel="stylesheet" type="text/css" href="../../../../css/StyleSheet.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" />
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
</head>

<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div  style="border:solid 0px red;">
        <div class="text headerband">Company - Crew Events</div>
        <table width="100%" cellpadding="10" cellspacing="0" border="0">
            <tr>
                <td>
                    <div class="topic">
                        <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                    </div>
                    <div class="category">
                        <asp:Label runat="server" ID="lblCategoryName"></asp:Label> ( <asp:Label runat="server" ID="lblOfficeName"></asp:Label> )
                    </div>
                    <div class="date">
                        <i class="fa fa-calendar"></i>
                        <asp:Label runat="server" ID="lblDuration"></asp:Label>
                    </div>
                    <div class="time">
                        <i class="fa fa-clock-o"></i>
                        <asp:Label runat="server" ID="lblStartTime"></asp:Label>
                    </div>
                    <div class="address">
                        <i class="fa fa-map-marker"></i>
                        <asp:Label runat="server" ID="lblEventLocation"></asp:Label>
                    </div>
                </td>
                <td style="width:50%; text-align:right; vertical-align:top;">
                    <div class="topic">
                        <asp:Label runat="server" ID="a" Text="Contact Details"></asp:Label>
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
                </td>
            </tr>
        </table>     
        
        <div style="text-align:left;padding:7px 7px 0px 7px;" class="listheader" ">
            <table cellpadding="0" cellspacin="0" border="0" width="100%">
                <col width="50%" />
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="170px" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged">
                                <asp:ListItem Text="All" Value="" ></asp:ListItem>
                                <asp:ListItem Text="Attending" Value="P" ></asp:ListItem>
                                <asp:ListItem Text="Not Attending" Value="A" ></asp:ListItem>
                                <asp:ListItem Text="To be Decided" Value="N" ></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Button runat="server" ID="Button1" Text=" + Invite Crew Members" class="btn" OnClick="btnInvite_Click" />
                        <asp:Button runat="server" ID="Button2" Text="Send Invitation Mail" class="btn" OnClick="btnInviteMail_Click" />
                    </td>
                </tr>
            </table>
                
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
                                                <tr class= "headerstylegrid">
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
    </div>
    
        <div  style="border:solid 0px red;margin-left:7px;margin-right:7px;">        
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
                                        
                                                                  
                        <%---New ---------------------------------------------------------------------------------------------------%>
                        <div style="position: fixed; top: 0px; left: 0px; height: 100%; width: 100%;z-index:10;" id="divAttendies" runat="server" visible="false">
                            <center>
                              <div style="text-align: center; padding:8px; font-size:13px; " class="text headerband">
                                <b>Invite Crew Members</b>&nbsp;
                            </div>
                            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
                                <div style="position:relative;text-align :center;background : white; z-index:150;top:50px; border:solid 5px black; width:90%;" >
                                    <center >
                                    <asp:UpdatePanel runat="server" ID="fsdaf">
                                    <ContentTemplate>
                                    <div>
                                    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse; height:35px;" width="100%">
                                        <colgroup>
                                    <col style="width:60px"/>
                                    <col style="width:80px"/>
                                    <col style="width:80px"/>
                                    <col style="width:80px"/>
                                    <col/>
                                    <col style="width:100px"/>
                                    <col style="width:100px"/>
                                    <col style="width:200px"/>
                                    <col style="width:200px"/>
                                    </colgroup>
                                    <tr class= "headerstylegrid">
                                        <td>Records</td>
                                        <td>Crew#</td>
                                        <td>Rank</td>
                                        <td>Off/Rat</td>
                                        <td>Crew Name</td>
                                        <td>Status</td>
                                        <td>City</td>
                                        <td>Recruiting Office</td>
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
                                    <colgroup>
                                    <col style="width:60px"/>
                                    <col style="width:80px"/>
                                    <col style="width:80px"/>
                                    <col/>
                                    <col style="width:100px"/>
                                    <col style="width:100px"/>
                                    <col style="width:200px"/>
                                     </colgroup>
                                    <asp:Repeater runat="server" ID="rptCrewList">
                                    <ItemTemplate>
                                    <tr>
                                        <td style="text-align:left"><asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("CrewId")%>'/> </td>
                                        <td><%#Eval("CREWNUMBER")%></td>
                                        <td><asp:Label runat="server" ID='lblRankCode' Text='<%#Eval("RankCode")%>'></asp:Label></td>
                                        <td style="text-align:left"><asp:Label runat="server" ID='lblName' Text='<%#Eval("CREWNAME")%>'></asp:Label></td>
                                        <td><%#Eval("CREWSTATUS")%></td>
                                        <td style="text-align:left"><%#Eval("CITY")%></td>
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
                                           <asp:Label runat="server" ID="lblMessage1" Font-Size="20px" Font-Bold="true" style="float:left"></asp:Label>
                                        <asp:Button runat="server" ID="btnSave2" Text="Save" onclick="btnSave2_Click" class="btn" width="100px" CausesValidation="true" ValidationGroup="main"/>   &nbsp;
                                        <asp:Button runat="server" ID="btnClose2" Text="Close" CausesValidation="false" onclick="btnClose2_Click" style="background-color:Red; color:White;border:none; padding:5px;"  width="100px"/>   
                                    </div>
                                    </center>
                                    </div>
                            </center>
                        </div>
                        <%---iframe Edit Attendies ---------------------------------------------------------------------------------------------------%>
                      
                    
        
    </div>

          <div style="position: fixed; top: 0px; left: 0px; height: 100%; width: 100%;z-index:10;" id="divEditAttendies" runat="server" visible="false">
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
                                            <asp:Button runat="server" ID="btnCloseAttendies" Text="Close" CausesValidation="false" onclick="btnCloseAttendies_Click" CssClass="btn" width="100px"/>   
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
