<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Seminar.aspx.cs" Inherits="Seminar_Seminar"
    Async="true" MasterPageFile="~/Modules/HRD/CrewTraining.master" Title="EMANAGER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  
   
    <link href="~/Styles/sddm.css" rel="stylesheet" type="text/css" />
   

    
    
    <table cellpadding="5" cellspacing="0" width="100%" style="background-color: #e9e9e9;
        font-weight: bold;">
        <tr>
            <td style="text-align: right; width: 150px">
                Recruiting Office :
            </td>
            <td style="text-align: left">
                <asp:DropDownList runat="server" ID="ddloffice" AutoPostBack="false" OnSelectedIndexChanged="ddloffice_OnSelectedIndexChanged"
                    Width="200px">
                </asp:DropDownList>
            </td>
            <td style="text-align: right; width: 80px">
                From :
            </td>
            <td>
                <asp:TextBox ID="txtFromDate" MaxLength="15" runat="server" CssClass="dateonly" ValidationGroup="addedit"
                    Width="100px" AutoPostBack="false" OnTextChanged="ddloffice_OnSelectedIndexChanged"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 80px">
                To :
            </td>
            <td>
                <asp:TextBox ID="txtTODate" MaxLength="15" runat="server" CssClass="dateonly" ValidationGroup="addedit"
                    Width="100px" AutoPostBack="false" OnTextChanged="ddloffice_OnSelectedIndexChanged"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 80px">
                Category :
            </td>
            <td style="text-align: left">
                <asp:DropDownList runat="server" ID="ddlCategory" AutoPostBack="false" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged"
                    Width="200px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnFilterRecords" runat="server" OnClick="ddloffice_OnSelectedIndexChanged"
                    Text="Show" CssClass="btn" />
            </td>
            <td style="text-align: right">
                <asp:LinkButton Text=" + Add New" runat="server" ID="lnkAdd" OnClick="btnAdd_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <table cellpadding="6" cellspacing="0" width="100%" border="1" style='border-collapse: collapse;
        background-color: #4da6ff;' bordercolor="#fffff">
        <colgroup>
            <col width="40px" />
            <col width="60px" />
            <col width="100px" />
            <col width="100px" />
            <col />
            <col width="200px" />
            <%--<col width="100px" />--%>
            <col width="80px" />
            <col width="80px" />
            <col width="80px" />
            <%--<col width="80px" />--%>
        </colgroup>
        <tr class= "headerstylegrid">
            <td style="color: White; text-align: center;">
                Sr#
            </td>
            <td style="color: White; text-align: center;">
                
            </td>
            <td style="color: White;">
                Category
            </td>
            <td style="color: White;">
                Office
            </td>
            <td style="color: White;">
                Topic
            </td>
            <td style="color: White;">
                Duration
            </td>
            <%--<td style="color: White;">
                Presenters
            </td>--%>
            <td style="color: White;">
                Invited
            </td>
            <td style="color: White;">
                Confirmed
            </td>
            <td style="color: White;">
                Attended
            </td>
            <%--<td style="color: White; text-align: center">
                Action
            </td>--%>
        </tr>
    </table>
    <table cellpadding="6" cellspacing="0" width="100%" border="1" style='border-collapse: collapse' bordercolor="#e2e2e2">
        <colgroup>
            <col width="40px" />
            <col width="60px" />
            <col width="100px" />
            <col width="100px" />
            <col />
            <col width="200px" />
            <%--<col width="100px" />--%>
            <col width="80px" />
            <col width="80px" />
            <col width="80px" />
            <%--<col width="80px" />--%>
        </colgroup>
        <asp:Repeater runat="server" ID="rptSeminars">
            <ItemTemplate>
                <tr>
                    <td style="text-align: center;">
                        <%#Eval("SNO")%>
                    </td>
                    <td style="text-align: center;">
                        <a href='SeminarAgenda.aspx?K=<%#Eval("SeminarId")%>' target="_blank"><img src='../../Images/HourGlass.png' style="border: none" title="View" /></a>
                        &nbsp;
                        <asp:ImageButton runat="server" ID="btnDelete" OnClick="btnDelete_Click" ToolTip="Delete Seminar" ImageUrl="~/Modules/HRD/Images/delete.jpg" CommandArgument='<%#Eval("SeminarId")%>' OnClientClick="return confirm('Are you sure to delete?');" Visible='<%#(Eval("Status").ToString()=="1" && Convert.ToString(Session["loginid"])=="1")%>' />
                    </td>
                    <td><%#Eval("SeminarCatName")%></td>
                    <td><%#Eval("OfficeNAME")%></td>
                    <td style='text-align:left'><%#Eval("Topic")%></td>
                    <td><%#Common.ToDateString(Eval("STARTDATE"))%><b> to </b><%#Common.ToDateString(Eval("ENDDATE"))%></td>
                    <%--<td><%#Eval("Presenters")%></td>--%>
                    <td>
                        <a href="SeminarPresenter.aspx?SeminarId=<%#Eval("SeminarId")%>" target="_blank"> <%#Eval("Invited")%> </a>

                        <asp:LinkButton ID="lnkInviteClick" runat="server" Text='<%#Eval("Invited")%>' Style="color: Red;"
                            OnClick="lnkInviteClick_OnClick" CommandArgument='<%#Eval("SeminarId")%>' Visible="false"></asp:LinkButton>
                    </td>
                    <td><%#Eval("Confirmed")%></td>
                    <td><%#Eval("Attended")%></td>
                    <%--<td style="text-align: center">
                        <asp:ImageButton runat="server" ID="btEdit" OnClick="btnEdit_Click" ToolTip="Modify Seminar" ImageUrl="~/Modules/HRD/Images/editX12.jpg" CommandArgument='<%#Eval("SeminarId")%>' />
                        <asp:ImageButton runat="server" ID="btnExecute" OnClick="btnExecute_Click" ToolTip="Execute Seminar" ImageUrl="~/Modules/HRD/Images/check.gif" CommandArgument='<%#Eval("SeminarId")%>' Visible='<%#(Eval("Status").ToString()=="2")%>' />
                        
                    </td>--%>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <!-- IFRAME SECTION -->
    <div style="position: fixed; top: 40px; left: 0px; height: 100%; width: 100%;" id="dvFrame" runat="server" visible="false">
        <center>
            <div style="position: fixed; top: 40px; left: 0px;height:100%; width: 100%;background-color: black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)">
            </div>
            <div style="position: relative; text-align: center; background: white; z-index: 150;top:0px; border: solid 5px black; width: 900px;">
                <center>
                    <div style="">
                        <iframe runat="server" id="frame1" width="100%" height="470px" frameborder="0"></iframe>
                    </div>
                    <div style="padding: 5px; text-align: right; background-color: #E2EAFF;">
                        <asp:Button runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click" Style="background-color: Red;
                            color: White; border: none; padding: 4px;" Width="100px" />
                    </div>
                </center>
            </div>
        </center>
    </div>
    <!-- IFRAME SECTION Edit Invited Personal -->
    <div style="position: fixed; top: 40px; left: 0px; height: 100%; width: 100%;z-index:10;" id="DivInvitedPerson"
        runat="server" visible="false">
        <center>
            <div style="position: fixed; top: 40px; left: 0px; min-height: 100%; width: 100%;
                background-color: black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)">
            </div>
            <div style="position: relative; text-align: center; background: white; z-index: 150;
                top: 30px; border: solid 5px black; width: 1000px;">
                <div style="height: 35px; overflow-x: hidden; overflow-y: scroll; text-align: left;
                    font-size: 13px;">
                    <table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse;
                        height: 35px;" width="100%">
                        <colgroup>
                            <col width="40px" />
                            <col width="80px" />
                            <col />
                            <col width="80px" />
                            <col width="140px" />
                            <col width="90px" />
                            <%-- <col width="60px" />--%>
                            <col width="60px" />
                            <col width="90px" />
                            <col width="60px" />
                            <%--    <col width="60px" />--%>
                        </colgroup>
                        <tr class= "headerstylegrid">
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
                        </tr>
                    </table>
                </div>
                <div style="height: 300px; overflow-x: hidden; overflow-y: scroll; font-size: 12px;">
                    <table border="1" bordercolor="#e2e2e2" cellpadding="3" cellspacing="0" style="border-collapse: collapse"
                        width="100%">
                        <colgroup>
                            <col width="40px" />
                            <col width="80px" />
                            <col />
                            <col width="80px" />
                            <col width="140px" />
                            <col width="90px" />
                            <%--<col width="60px" />--%>
                            <col width="60px" />
                            <col width="90px" />
                            <col width="60px" />
                            <%--<col width="60px" />--%>
                            <col width="30px" />
                        </colgroup>
                        <asp:Repeater ID="rptInvite" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center;">
                                        <%#Eval("SNO")%>
                                        <asp:HiddenField runat="server" ID="hfdGUID" Value='<%#Eval("GUID")%>' />
                                        <asp:HiddenField runat="server" ID="hfdEmail" Value='<%#Eval("Email")%>' />
                                        <asp:HiddenField runat="server" ID='lblRankCode' Value='<%#Eval("RankCode")%>'></asp:HiddenField>
                                        <asp:HiddenField runat="server" ID='lblName' Value='<%#Eval("CREWNAME")%>'></asp:HiddenField>
                                        <asp:HiddenField runat="server" ID='hfReplyStatusCode' Value='<%#Eval("ReplyStatusCode")%>'></asp:HiddenField>
                                        
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lblCrewNumner" runat="server" Text='<%#Eval("CREWNUMBER")%>' ></asp:Label>                                        
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblCrewName" runat="server" Text='<%#Eval("CREWNAME")%>'></asp:Label>
                                        
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
                                            CommandArgument='<%#(Eval("TableId").ToString())%>' OnClick="btEditAttendies_OnClick" CausesValidation="false" />
                                        <%-- <asp:ImageButton ID="btnDeleteInvite" runat="server" CommandArgument='<%#(Eval("TableId").ToString())%>'
                                                                    ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="btnDeleteInvite_Click" OnClientClick="return confirm('Are you sure to delete?');"
                                                                    ToolTip="Delete Attendies" />--%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div style="overflow-x: hidden; overflow-y: scroll; font-size: 12px;" id="divUpdatePresence" runat="server" visible="false">
                    <table cellpadding="0" cellspacing="0" width="70%" border="0" style="margin:0px auto;">
                        <col  />
                        <col width="100px" />
                        <col width="60px" />
                        <col width="60px" />
                        <col width="120px" />
                                    <tr>
                                        <td colspan="5">
                                                <asp:Label ID="lblCrewNumber" runat="server" style="color:Red;" ></asp:Label>
                                        </td>
                                    </tr>
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
                                        <td>
                                            <asp:Button runat="server" ID="btnUpdateAttendies" Text="Save" onclick="btnUpdateAttendies_Click" CssClass="btn" />
                                        </td>
                                        <td>
                                            &nbsp; <asp:Button runat="server" ID="btnCloseAttendies" Text="Close" CausesValidation="false" onclick="btnCloseAttendies_Click" CssClass="btn" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMessageAttendies" runat="server" Font-Size="13px" style="color:Red;"  ></asp:Label>
                                        </td>
                                    </tr>                                                
                                    </table>
                </div>
                <div style="padding: 5px; text-align: center;">
                    <asp:Button ID="btnClose_DivInvitedPerson" runat="server" CausesValidation="false" OnClick="btnClose_DivInvitedPerson_OnClick" Text="Close" />
                </div>
            </div>
        </center>
    </div>
    <script type="text/javascript" src="../../JS/jquery.datetimepicker.js"></script>
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
</asp:Content>
