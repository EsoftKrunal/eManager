<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReadManualHistorySection.aspx.cs" Inherits="ReadManualHistorySection" %>
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
            window.parent.document.getElementById("btnrel").click();
            
            //top.hdd.location.reload(true);
        }
        function ReloadImages(ManualId, SectionId) {
            top.hdcom.location = './ReadViewManualImages.aspx?ManualId=' + ManualId + "&SectionId=" + SectionId;
        }
        function openwindow() {
            if (FileName != '') {
                //window.open(FileName, '');
                var ManualId = '<%=ob_SectionHistory.ManualId.ToString() %>';
                var SectionId = '<%=ob_SectionHistory.SectionId.ToString() %>';
                window.open('ReadFullScreenSection.aspx?ManualId=' + ManualId + '&SectionId=' + SectionId + '&FileName=' + FileName, '');
            }
        }
        function openwindowWithNoHeader() {
            if (FileName != '') {
                window.open(FileName);
            }
        }
        
    </script>
</head>
<body style="margin:0px 0px 0px 0px">
    <script language="javascript" type="text/javascript" src="JS/thickbox.js"></script>
    <form id="form1" runat="server">
    <div style="width:99%">
        <div style="padding:5px; padding-left:5px;width:100%; border-bottom:solid 1px black; background-color:#e2e2e2; padding-bottom:7px;">
            <div style="text-align:left;">
                <asp:Label runat="server" ID="lblManualName" Font-Bold="true" Font-Names="Arial" Font-Size="16px"></asp:Label>
                <asp:Label runat="server" ID="lblMVersion" Font-Bold="true" ></asp:Label> <br />
                <asp:Label runat="server" ID="lblSVersion" Font-Bold="true" style="float:right"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                <asp:Label runat="server" ID="lblHeading" Font-Bold="true" Font-Names="Arial" Font-Size="14px" ForeColor="#4371a5"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                Tags : <asp:Label runat="server" ID="lblContent" Font-Names="Arial" Font-Size="14px" ForeColor="Black" Font-Italic="true"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                <div runat="server" id="dvForms" class="abtn" title="Open Linked Forms.">   
                    <div><a target="comm" href="ReadViewForms.aspx?HistoryID=<%=HistoryID.ToString()%>" ><img src="Images/form.png" /></a></div>
                </div>
                <div runat="server" id="dvPopUp" class="abtn" title="Maximize this window.">   
                    <div><a href="#" onclick="openwindow();"><img src="Images/maximize.png" title="Maximize this window." /></a></div>
                </div>

                <div runat="server" id="dvdvPopUpWithNoHeader" class="abtn" title="Maximize this window.">   
                    <div><a href="#" onclick="openwindowWithNoHeader();"><img src="Images/book_open.png" title="Open with Original Program." /></a></div>
                </div>

                <div runat="server" id="Div1" >   
                    <div><a href="ViewManualCommentsHistory.aspx?ManualId=<%=ob_SectionHistory.ManualId.ToString()%>&SectionId=<%=ob_SectionHistory.SectionId%>&ManVersion=<%=mb.VersionNo.ToString()%>&SecVersion=<%=ob_SectionHistory.Version%>&PT=His&HID=<%=Request.QueryString["HistoryID"].ToString() %>" >
                        Comments
                     </a></div>
                </div>
            </div>
        </div>
        <iframe runat="server" width="100%" height="450px" scrolling="auto" id="frmFile" frameborder="1">
        
        </iframe>
      
    </div>   
    <div runat="server" id="dvApproval"> 
    </div> 
    </form>
</body>
</html>
