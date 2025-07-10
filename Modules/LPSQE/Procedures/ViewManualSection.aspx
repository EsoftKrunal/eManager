<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewManualSection.aspx.cs" Inherits="ViewManualSection" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN" "http://www.w3.org/TR/html4/frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="CSS/style.css" />
    <script language="javascript" type="text/javascript" src="http://code.jquery.com/jquery-1.8.1.js"></script>
    <script type="text/javascript" language="javascript" >
        var FileName = '';
        function ShowHideShortcutMenu()
        {
            if (document.getElementById("smenu").style.display == 'none') 
            {
                document.getElementById("smenu").style.display = 'block';
                document.getElementById("smenu").style.left = window.event.clientX + 20+ 'px';
                document.getElementById("smenu").style.top = window.event.clientY + 20 +'px';
            }
            else 
            {
                document.getElementById("smenu").style.display = 'none';
            }
        }
        function OnHover(ctl) {
            ctl.title = 'Click here to Content.';
            
        }
        function OnOut(ctl) {
            ctl.title = '';
        }

        function OnClick(ctl) {
            //ShowHideShortcutMenu();
        }
        function ReloadHeadings() {

            //top.hdd.document.getElementById("btnrel").click();
           if (window.parent.document.getElementById("btnrel")!=null)
                window.parent.document.getElementById("btnrel").click();
            
            //top.hdd.location.reload(true);
        }
        function ReloadImages(ManualId, SectionId) {
            top.hdcom.location = './ViewManualImages.aspx?ManualId=' + ManualId + "&SectionId=" + SectionId;
        }
        function openwindow() {
            if (FileName != '') {
                //window.open(FileName, '');
                var ManualId='<%=ob_Section.ManualId.ToString() %>';
                var SectionId='<%=ob_Section.SectionId.ToString() %>';
                window.open('FullScreenSection.aspx?ManualId=' + ManualId + '&SectionId=' + SectionId + '&FileName=' + FileName, '');
            }
        }
        function openwindowWithNoHeader() {
            if (FileName != '') {
                //window.open('FullScreenSection.aspx?ManualId=' + ManualId + '&SectionId=' + SectionId + '&FileName=' + FileName, '');
                //                window.open('FullScreenSection.aspx?&FileName=' + FileName+'&FW=Y', '');
                window.open(FileName);
            }
        }
    </script>
</head>
<body style="margin:0px 0px 0px 0px">
    <script language="javascript" type="text/javascript" src="JS/thickbox.js"></script>
    <form id="form1" runat="server">
    <div style="width:99%">
        <div style="padding:5px; padding-left:5px;width:100%; border-bottom:solid 1px black; padding-bottom:7px;">
            <div style="text-align:left;">
                <asp:Label runat="server" ID="lblManualName" Font-Bold="true" Font-Names="Arial" Font-Size="16px"></asp:Label>
                <asp:Label runat="server" ID="lblMVersion" Font-Bold="true" style="float:right"></asp:Label> <br />
                <asp:Label runat="server" ID="lblSVersion" Font-Bold="true" style="float:right"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                <asp:Label runat="server" ID="lblHeading" Font-Bold="true" Font-Names="Arial" Font-Size="14px" ForeColor="#4371a5"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                Tags : <asp:Label runat="server" ID="lblContent" Font-Names="Arial" Font-Size="14px" ForeColor="Black" Font-Italic="true"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                <div runat="server" id="dvAdd" class="abtn" title="Add New Section">   
                    <div><a href='AddSection.aspx?ManualId=<%=ob_Section.ManualId%>&LastSectionId=<%=LastSectionId%>'><img src="Images/w_add2.png" alt="Add New Section"/></a></div>
                </div>
                <div runat="server" id="dvEdit" class="abtn" title="Edit Section">   
                    <div><a href='AddSection.aspx?ManualId=<%=ob_Section.ManualId.ToString()%>&SectionId=<%=ob_Section.SectionId%>&LastSectionId=<%=LastSectionId%>&AddSection=<%= Page.Request.QueryString["AddSection"]%>'><img src="Images/edit.png" alt="Edit Section"/></a></div>
                </div>
                <div runat="server" id="dvDelete" class="abtn" title="Inactive Section">   
                    <div><a href='#' onserverclick="Delete_Section" runat="server" ><img src="Images/delete.png" /></a></div>
                </div>
                <div runat="server" id="dvActivate" class="abtn" title="Activate Section"> 
                    <div><a href='#' onserverclick="Activate_Section" runat="server" ><img src="Images/check.png" /></a></div>
                </div>
                <div runat="server" id="dvPopUp" class="abtn" title="Maximize this window.">   
                    <div><a href="#" onclick="openwindow();"><img src="Images/maximize.png" title="Maximize this window." /></a></div>
                </div>
                <div runat="server" id="dvViewDoc" class="abtn" title="Maximize this window.">   
                    <div><a href="#" onclick="openwindowWithNoHeader();"><img src="Images/book_open.png" title="Open with Original Program." /></a></div>
                </div>


                <div runat="server" id="dvComments" title="Open Comments." style="display:inline-block; width:90px; text-decoration:underline; padding-left:20px;">  
                    <div><a target="comm" href="ViewManualCommentsHistory.aspx?Mode=C&ManualId=<%=ob_Section.ManualId.ToString()%>&SectionId=<%=ob_Section.SectionId%>" >
                        <%--<img src="Images/comment_icon.png" />--%>
                            Comments
                     </a></div>
                </div>

                <div runat="server" id="dvHistory" class="abtn" title="Open History." >   
                    <div><a target="comm" href="ViewManualCommentsHistory.aspx?Mode=H&ManualId=<%=ob_Section.ManualId.ToString()%>&SectionId=<%=ob_Section.SectionId%>" ><img src="Images/icon_history.gif" /></a></div>
                </div>
                <div runat="server" id="dvForms" title="Open Linked Forms." style="display:inline-block; width:55px; text-decoration:underline;">  
                    <div><a target="comm" href="ViewForms.aspx?ManualId=<%=ob_Section.ManualId.ToString()%>&SectionId=<%=ob_Section.SectionId%>" >
                        <%--<img src="Images/form.png" />--%>
                        Forms
                     </a></div>
                </div>
                 <div runat="server" id="dvAppReq" title="Request for Approval." style="display:inline-block; width:140px; text-decoration:underline;">  
                    <div><a id="A1" href='#' onserverclick="ReqApprove_Section" runat="server" >
                        <%--<img src="Images/request_icon.png" />--%>
                        Submit for Approval
                    </a></div>
                </div>


                <%--<div runat="server" id="DvInvCom" title="Invite Comments." style="display:inline-block; width:140px; text-decoration:underline;">  
                    <asp:LinkButton ID="lnkInviteComments" runat="server" Text="Invite Comments" OnClick="btnInviteCommentsPopup_OnClick"></asp:LinkButton>
                </div>--%>
                
            </div>
        </div>
        <iframe runat="server" width="100%" height="450px" scrolling="auto" id="frmFile" frameborder="1">
        
        </iframe>
      

            <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvSendRequestForApproval" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; height:156px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            <div style='background-color:#d2d2d2; padding:5px;'> Please Enter Comments. </div> 
            <div style='padding:5px;'>
            <asp:TextBox runat="server" ID="txtAppReqestComments" Width='100%' Height="70px" TextMode="MultiLine"></asp:TextBox><br />
            <asp:Button runat="server" ID="btnSubmitPopupAdmin" Text=" Submit " OnClick="btnSubmitPopupAdmin_Click" Height="25px" style="padding:2px; margin:2px;" />            
            <asp:Button runat="server" ID="btnCloseSubmitPopupAdmin" Text=" Close " OnClick="btnCloseSubmitPopupAdmin_Click" Height="25px" style="padding:2px; margin:2px;" />
            </div>
                <asp:Label ID="lblMsgAppReq" runat="server"  Font-Bold="True"  ForeColor="red" ></asp:Label>
            </div>
            </center>
            </div>


             <%--------------------------------------%>
            <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvInviteComments" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:840px; height:350px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">

                <table cellpadding="2" cellspacing="0" style="width: 100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
                <tr>
                    <td class="text" style="height: 23px; background-color: #4371a5; text-align :center; font-weight:bold;font-size:large;color:White; ">Invite Comments</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlOffice" runat="server" CssClass="stlselect" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged" style="float:left;width:150px;" ></asp:DropDownList>
                    </td>
                </tr>
                </table>
                <br />

                <%--background-color:#f9f9f9--%>
                <div style="overflow-y:hidden; overflow-x:hidden;width: 100%;border:#4371a5 1px solid;border-bottom:none; " > 
                <table cellpadding="1" cellspacing="0" border="1" style="width: 100%;text-align:left;border-collapse:collapse;" rules="all">
                    <col  />
                    <col width="150px" />
                    <col width="150px" />
                    <col width="60px" />
                    <col width="17px" />
                    <tr style="font-weight:bold;background-color:#c2c2c2;">
                        <td> Emp Name </td>
                        <td> Position </td>
                        <td> Department </td>
                        <td> Select </td>
                        <td></td>
                    </tr>
                </table>
                </div>
                <div style=" height:200px; overflow-y:scroll; overflow-x:hidden;" style="width: 100%;border:#4371a5 1px solid;"> 
                <table cellpadding="1" cellspacing="0" border="1" style="width: 100%;text-align:left;border-collapse:collapse;font-size:11px;" rules="all">
                    <col  />
                    <col width="150px" />
                    <col width="150px" />
                    <col width="60px" />
                    <asp:Repeater runat="server" ID="rptEmployee">
                        <ItemTemplate>
                            <tr class="MO">
                                <td>
                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label> 
                                    <asp:HiddenField ID="hfEmailID" runat="server" Value='<%#Eval("Email") %>' />
                                    <asp:HiddenField ID="hfPositionName" runat="server" Value='<%#Eval("PositionName") %>' />
                                 </td>
                                <td><%#Eval("PositionName")%></td>
                                <td> <%#Eval("DeptName")%></td>
                                <td><asp:CheckBox ID="chkSelectEmp" runat="server" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
                <div style="padding:4px;">
                    <asp:Button ID="btnCloseInvitationPopup" runat="server" CssClass="abtn" OnClick="btnCloseInvitationPopup_OnClick" Text="  Close  " />
                    <asp:Button ID="btnSendInvitation" runat="server" CssClass="abtn" OnClick="btnSendInvitation_OnClick" Text="  Send Invitation  " />
                    <br />
                    <asp:Label ID="lblMsgInvitation" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>
                </div>

            </div>
        </center>
    </div>
    </div>   
    <div runat="server" id="dvApproval"> 
    </div> 
    </form>
</body>
</html>
