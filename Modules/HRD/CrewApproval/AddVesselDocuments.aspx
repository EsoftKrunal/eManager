<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddVesselDocuments.aspx.cs" Inherits="CrewApproval_AddVesselDocuments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css">
    td
    {
        vertical-align:top;
        text-align:left;
        padding-left:3px;
    }
    
    </style>
    <script type="text/javascript">
        function SelectAll(v,self) {
            var parent = document.getElementById("d" + v); 
            var CTLS=parent.getElementsByTagName("input");
            for (i = 0; i <= CTLS.length - 1; i++) {
                CTLS[i].checked = self.checked;
            }
        }
        function ReloadParent() {
            window.opener.RefreshMe1();
        }
    </script>
</head>
<body style="margin: 0 0 0 0" onunload='ReloadParent();'>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="text-align: center">
    <asp:UpdateProgress runat="server" ID="upp1">
    <ProgressTemplate>
        <div style='position:absolute; top:150px; width:100%'>
                <center>
                <div style="background-color:white; border:solid 1px blue; width:200px; height:80px; font-size:14px;">
                    <br />
                          <img src='../Images/progress_Icon.gif' title='Loading..' alt='Loading..'/> 
                    <br />Loading ...
                </div>
                </center>
        </div>    
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width='100%'>
            <tr>
                <td style="text-align:center; color:White;" class="text headerband"  >Add Documents</td>
            </tr>
            <tr>
                <td style="text-align:left; "><asp:Label runat="server" ID="lblRType" Font-Size="Large" ForeColor="Purple"></asp:Label>
                <hr />
                Select Rank : <asp:DropDownList runat="server" ID="ddl_RWRankFilter" CssClass="required_box"  AutoPostBack="true" Width='200px' OnSelectedIndexChanged="ddlRank_OnSelectedIndexChanged"></asp:DropDownList>
                <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr style="font-size:16px; font-family:Calibri; color:Gray;">
                            <td><input type="checkbox" onclick='SelectAll(1,this);' id='chk1' /><lable for='chk1'>Licenses</lable>
                            </td>
                             <td><input type="checkbox" onclick='SelectAll(2,this);' id='chk2' /><lable for='chk2'>Course & Certificates</lable>
                            </td>
                            <td><input type="checkbox" onclick='SelectAll(3,this);' id='chk3' /><lable for='chk3'>Endorsements</lable>
                            </td>
                             <td><input type="checkbox" onclick='SelectAll(4,this);' id='chk4' /><lable for='chk4'>Travel Documents</lable>
                            </td>
                            <td><input type="checkbox" onclick='SelectAll(5,this);' id='chk5' /><lable for='chk5'>Medical Documents</lable>
                            </td>
                        </tr>
                        <tr >
                            <td>
                            <div style="overflow-y:scroll; height:600px; border:solid 1px #d2d2d2;" id="d1">
                            <asp:Repeater runat="server" ID="rptL">
                            <ItemTemplate>
                                <div><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#Common.CastAsInt32(Eval("RVID")) > 0 %>' ToolTip='<%#Eval("VesselDocumentTypeId") %>' /><%#Eval("VesselDocumentName") %></div>
                            </ItemTemplate>
                            </asp:Repeater>
                            </div>
                            </td>
                            <td>
                            <div style="overflow-y:scroll; height:600px; border:solid 1px #d2d2d2;" id="d2">
                            <asp:Repeater runat="server" ID="rptC">
                            <ItemTemplate>
                                <div><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#Common.CastAsInt32(Eval("RVID")) > 0 %>'  ToolTip='<%#Eval("VesselDocumentTypeId") %>' /><%#Eval("VesselDocumentName")%></div>
                            </ItemTemplate>
                            </asp:Repeater>
                             </div>
                            </td>
                            <td>
                            <div style="overflow-y:scroll; height:600px; border:solid 1px #d2d2d2;" id="d3">
                            <asp:Repeater runat="server" ID="rptE">
                            <ItemTemplate>
                                <div><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#Common.CastAsInt32(Eval("RVID")) > 0 %>'  ToolTip='<%#Eval("VesselDocumentTypeId") %>' /><%#Eval("VesselDocumentName")%></div>
                            </ItemTemplate>
                            </asp:Repeater>
                             </div>
                            </td>
                            <td>
                            <div style="overflow-y:scroll; height:600px; border:solid 1px #d2d2d2;" id="d4">
                            <asp:Repeater runat="server" ID="rptT">
                            <ItemTemplate>
                                <div><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#Common.CastAsInt32(Eval("RVID")) > 0 %>'  ToolTip='<%#Eval("VesselDocumentTypeId") %>' /><%#Eval("VesselDocumentName")%></div>
                            </ItemTemplate>
                            </asp:Repeater>
                             </div>
                            </td>
                            <td>
                            <div style="overflow-y:scroll; height:600px; border:solid 1px #d2d2d2;" id="d5">
                            <asp:Repeater runat="server" ID="rptM">
                            <ItemTemplate>
                                <div><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#Common.CastAsInt32(Eval("RVID")) > 0 %>'  ToolTip='<%#Eval("VesselDocumentTypeId") %>' /><%#Eval("VesselDocumentName")%></div>
                            </ItemTemplate>
                            </asp:Repeater>
                             </div>
                            </td>
                        </tr>
                    </table>
                    <center>
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" 
                            style="padding:4px; width:100px" Font-Size="15px" BackColor="Orange" 
                            BorderColor="Gray" BorderWidth="1" onclick="btnSave_Click" />
                    </center>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    </asp:UpdatePanel> 
    </div>
    </form>
</body>
</html>
