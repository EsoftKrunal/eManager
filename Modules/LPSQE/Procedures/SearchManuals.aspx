<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchManuals.aspx.cs" Inherits="SMS_SearchManuals" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="SMSManualMenu.ascx" tagname="SMSManualMenu" tagprefix="uc3" %>
<%@ Register src="SMSSubTab.ascx" tagname="SMSSubTab" tagprefix="uc4" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="CSS/style.css" />
    <script type="text/javascript" src="JS/Common.js"></script>
    <script type="text/javascript">
        function checkAll(ctl) {
            var chks = document.getElementById("d1").getElementsByTagName("input");
            for (i = 0; i <= chks.length - 1; i++) {
                chks[i].checked = ctl.checked;
            }
        }
        function SelectSection(MId, SID) {
            document.getElementById("txtM").setAttribute("value", MId);
            document.getElementById("txtS").setAttribute("value", SID);
            document.getElementById("btnSelSection").click();
        }
        function ShowChangeRecord(MId) {
            document.getElementById("txtM").setAttribute("value", MId);
            document.getElementById("btnChangeRecord").click();
        }

        function OpenManualSection(MID, SID) {
            window.open('ReadManualSection1.aspx?ManualId=' + MID + '&SectionId=' + SID, '', '');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    <div>
    <uc3:SMSManualMenu ID="ManualMenu2" runat="server" />
    <uc4:SMSSubTab ID="SMSManualMenu1" runat="server" />
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style="vertical-align:top;overflow:hidden;">
     <div style="width:100%; ">
    
        <%--<table width="100%" border="0" cellspacing="0" cellpadding="5" style="margin-bottom:4px;" >
            <col width="380px" />
            <col />
            <tr style="background-color:#4371A5; color:White;font-size:16px;font-weight:bold; text-align:center;">
            <td> Search Manual </td>
            </tr>
        </table>--%>

        <asp:UpdatePanel ID="upMList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" >
             <col width="380px" />
             <col />             
            <tr>
                <td >
                    <div style="width:375px; background-color:#4371a5;border:solid 1px gray; border-right:none;  height:25px;overflow-x:hidden; overflow-y:hidden; color:White;">
                        <div style="padding-top:0px;float:left;padding-left:5px; ">
                        <input type="checkbox" onclick="checkAll(this)" id="chall" runat="server" style="float:left"/>
                        <div style="padding-top:5px">&nbsp;Manual Name &nbsp;&nbsp;&nbsp;&nbsp;</div>                        
                        </div>
                        
                    <span style="float:right;padding-right:20px; padding-top:4px;">VersionNo</span>
                </div>
                    <div style="width:375px; height:150px;border:solid 1px gray;border-right:none;overflow-x:hidden; overflow-y:scroll;" id="d1">
                    <div style="margin:5px">
                        <asp:Repeater runat="server" ID="rptManuals">
                        <ItemTemplate>
                          <div title='<%#Eval("ManualName")%>' style="width:340px; height:18px; overflow:hidden;border-bottom:dotted 1px #4371a5;">
                              <div style="float:left; width:20px; clear:left;height:18px; ">
                                <asp:CheckBox runat="server" id="chkSelect" CommandArgument='<%#Eval("ManualId")%>' />
                              </div>
                              <div style="float:left; width:100px; height:18px; padding-top:3px; padding-left:2px;">
                                <asp:LinkButton runat="server" ID="lnkManual" Width="260px" CommandArgument='<%#Eval("ManualId")%>' Text='<%#Eval("ManualName")%>' OnClick="SelectManual"></asp:LinkButton>
                              </div>
                              <div style="float:right; width:100px; text-align:right;">
                                <%#Eval("VersionNo")%>
                              </div>
                          </div>
                        </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>                 
                </td>
                <td style="padding:2px;vertical-align:top;">

                    <table cellpadding="0" cellspacing="0" border="0">
                        <col width="130px" />
                        <col width="5px" />
                        <col />
                        <tr>
                            <td> Search by text : </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="txtSearchManual" Width="335px" style="float:left; background-color:lightyellow;margin:2px;"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchManual" WatermarkText="Enter Search Text Here.."></asp:TextBoxWatermarkExtender>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td> Search by date :</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtFromDate" runat="server" Width="80px" style="background-color:lightyellow;margin:2px;"></asp:TextBox> :
                                <asp:TextBox ID="txtToDate" runat="server" Width="80px" style="background-color:lightyellow;margin:2px;"></asp:TextBox>

                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td> Search by form no :</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtFormNo" runat="server" Width="120px" style="background-color:lightyellow;margin:2px;"></asp:TextBox> :
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button runat="server" id="btnSearch" onclick="SearchManual" Height="20px" Text=" Search " style="font-size:13px; vertical-align:middle; padding-top:0px;margin:2px;" />
                                <asp:Button runat="server" id="btnClear" onclick="ClearText" Height="20px" Text=" Clear " style="font-size:13px; vertical-align:middle; padding-top:0px;margin:2px;" />
                             </td>
                        </tr>
                        <tr>
                            <td>    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>    </td>
                        </tr>
                    </table>
                </td>
            </tr>            
            </table>           
        </ContentTemplate>
        </asp:UpdatePanel>
        
        <%----------------------------------------------------------------------------------------------------------------------------------%>
        <br />
        <asp:UpdatePanel ID="upHeadings" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <asp:TextBox runat="server" ID="txtM" Text="" style="display:none" /><asp:Button runat="server" ID="btnrel" OnClick="Reload" Text="Reload" style="display:none"/>
            <asp:TextBox runat="server" ID="txtS" Text="" style="display:none" />
            <asp:Button runat="server" ID="btnSelSection" OnClick="btnSelSection_Click" Text="btnSelSection" style="display:none"/>
             <asp:Button runat="server" ID="btnChangeRecord" OnClick="btnShowChangeRecord_Click" Text="btnChangeRecord" style="display:none"/>
        
            <div style="overflow-x:hidden;overflow-y:hidden;width:100% ;border:#4371a5 1px solid;" >
                <table cellpadding="1" cellspacing="0" border="1" width="100%" rules="all" style="border-collapse:collapse;" >
                    <col width="35px" />
                    <col width="250px" />
                    <col width="70px" />
                    <col />
                    <col width="90px" />                    
                    <col width="130px" />
                    
                    <tr style="background-color:#4371A5;color:White;">
                        <td> View </td>
                        <td> Manual Name </td>
                        <td> Section </td>
                        <td> Heading </td>
                        <td> Revision# </td>
                        <td> Approved On</td>
                    </tr>
                </table>
            </div>

            <div style="height:230px; overflow-x:hidden;overflow-y:scroll;width:100% ;border:#4371a5 1px solid;">
                <table cellpadding="1" cellspacing="0" border="1" width="100%" rules="all" style="border-collapse:collapse;" >
                    <col width="35px" />
                    <col width="250px" />
                    <col width="70px" />
                    <col />
                    <col width="90px" />
                    <col width="130px" />
                    <asp:Repeater ID="rptFilteredManuals" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <a onclick="OpenManualSection(<%#Eval("ManualID")%> ,<%#Eval("SectionID")%> )" style="cursor:pointer; color:#4A8CF8;text-decoration:underline;" >
                                        <img src="Images/search.png" />                                            
                                      </a>
                                    <%--<asp:LinkButton ID="lnkViewManual" runat="server" Text="View" ></asp:LinkButton>--%> <%--OnClick="lnkViewManual_OnClick"--%>
                                </td>
                                <td> <%#Eval("ManualName")%> </td>
                                <td> <%#Eval("SectionID")%> </td>
                                <td> <%#Eval("Heading")%> </td>
                                <td> <%#Eval("Sversion")%> </td>
                                <td> <%# Common.ToDateString( Eval("ApprovedOn"))%> </td>                                
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    
        </td>        
    </tr>
    </table> 

    </div>
    </form>
</body>
</html>
