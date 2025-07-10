<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewManualHeadings.aspx.cs" Inherits="ViewManualHeadings"   %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<%@ Register src="~/Modules/LPSQE/Procedures/SMSADMIN/SMSManualMenu.ascx" tagname="SMSManualMenu" tagprefix="uc3" %>
<%@ Register src="~/Modules/LPSQE/Procedures/SMSADMIN/SMSAdminSubTab.ascx" tagname="SMSAdminSubTab" tagprefix="uc4" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link rel="Stylesheet" href="../../../HRD/Styles/StyleSheet.css" />
    <link rel="Stylesheet" href="../CSS/style.css" />    
    <script type="text/javascript" src="../../js/Common.js"></script>
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
        function ShowSearch() {
            document.getElementById("dvSearchBox").style.display = "block";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
        <uc3:SMSManualMenu ID="ManualMenu1" runat="server" />
    <uc4:SMSAdminSubTab ID="ManualMenu2" runat="server" />

    
    

    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" width="100%">
   <%-- <tr>
    <td style="width:275px; height:20px; border-right: solid 1px black; font-weight:bold; vertical-align:middle; text-align:center; color:#ffffff; background-color:#9999FF

; ">Manuals</td>
    <td style="width:275px;height:20px; border-right: solid 1px black; font-weight:bold; vertical-align:middle; text-align:center; color:#ffffff; background-color:#9999FF
; ">Index</td>
    <td style="height:20px; border-right: solid 1px black; font-weight:bold; vertical-align:middle; text-align:center; color:#ffffff; background-color:#9999FF; ">Details</td>
    </tr>--%>
    <tr>
    <td style="width:375px;vertical-align:top;border:solid 1px black; border-top:none; overflow:hidden;"> 
     <div style="width:375px; height:455px;"> 
    
        <asp:UpdatePanel ID="upMList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" >             
            <tr>
                <td>
                    <div style="width:100%;border:solid 1px gray; border-top:none; border-right:none;width:375px;  height:25px;overflow-x:hidden; overflow-y:hidden; color:black;">
                        <div style="padding-top:0px;float:left;padding-left:5px; ">
                        <%--<input type="checkbox" onclick="checkAll(this)" id="chall" visible="false" runat="server" style="float:left"/>--%>
                        <input type="checkbox" onclick="checkAll(this)" id="chall" runat="server" style="float:left"/>
                        <div style="padding-top:5px;Width:200px;">&nbsp;<asp:DropDownList ID="ddlManualCategories" Width="175px" runat="server" AutoPostBack="True" onselectedindexchanged="ddlManualCategories_SelectedIndexChanged"></asp:DropDownList> </div>
                        </div>
                    <span style="float:right;padding-right:20px; padding-top:4px;">VersionNo</span>
                </div>
                    <div style="width:375px; height:429px;border:solid 1px gray;border-right:none;overflow-x:hidden; overflow-y:scroll;" id="d1">
                    <div style="margin:5px"> 
                        <asp:Repeater runat="server" ID="rptManuals">
                        <ItemTemplate>
                          <div title='<%#Eval("ManualName")%>' style="width:375px;  overflow:hidden;border-bottom:dotted 1px #4371a5;"> 
                              <div style="float:left; width:20px; clear:left; ">
                                <%--<asp:CheckBox runat="server" id="chkSelect" CommandArgument='<%#Eval("ManualId")%>' Visible="<%#ShowSearch%>"/>--%>
                                <asp:CheckBox runat="server" id="chkSelect" CommandArgument='<%#Eval("ManualId")%>' />

                              </div>
                              <div style="float:left; width:260px; padding-top:3px; padding-left:2px;">
                                <asp:LinkButton runat="server" ID="lnkManual" CommandArgument='<%#Eval("ManualId")%>' Text='<%#Eval("ManualName")%>' OnClick="SelectManual"></asp:LinkButton>
                              </div>
                              <div style="float:left; width:50px; text-align:right;">  
                                <%#Eval("VersionNo")%>
                              </div>
                          </div>
                        </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                 
                </td>
            </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        
        
        </div>
    
        </td>

        <td style="width:380px;vertical-align:top;border:solid 1px black;  border-top:none; overflow:hidden;">
          <div style="width:380px; height:455px;">
             
              <asp:UpdatePanel ID="upHeadings" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
                     <%-- <table width="100%" border="0" cellspacing="0" cellpadding="0">
                          <tr>
                              <td style="text-align: right; padding: 3px;">
                                  <div style="text-align: left">
                                      <asp:Button runat="server" ID="btnSearchC" OnClick="btnSearchC_Click" Height="20px"
                                          Text="Search" Style="font-size: 13px; vertical-align: middle; padding-top: 0px;" />
                                      <asp:TextBox runat="server" ID="txtSearchManual" Width="260px" Visible="false" Style="float: left;
                                          background-color: lightyellow;"></asp:TextBox>
                                      <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchManual"
                                          WatermarkText="Enter Search Text Here..">
                                      </asp:TextBoxWatermarkExtender>
                                      <asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/SMS/Images/search.png"
                                          OnClick="SearchManual" Visible="false" />
                                  </div>
                              </td>
                          </tr>
                      </table>--%>

                      <asp:TextBox runat="server" ID="txtM" Text="" Style="display: none" /><asp:Button
                          runat="server" ID="btnrel" OnClick="Reload" Text="Reload" Style="display: none" />
                      <asp:TextBox runat="server" ID="txtS" Text="" Style="display: none" />
                      <asp:Button runat="server" ID="btnSelSection" OnClick="btnSelSection_Click" Text="btnSelSection"
                          Style="display: none" />
                      <asp:Button runat="server" ID="btnChangeRecord" OnClick="btnShowChangeRecord_Click"
                          Text="btnChangeRecord" Style="display: none" />
                      <%--<div style="height: 450px; overflow-x: hidden; overflow-y: scroll;" id="dvscroll_Order" onscroll="SetScrollPos(this)">--%> 
                          <asp:Literal runat="server" ID="litManual"></asp:Literal>
                      <%--</div>--%>
                  </ContentTemplate>
              </asp:UpdatePanel>

          </div>
        </td>

        <td style="vertical-align:top; border:solid 1px black;">
        <asp:UpdatePanel ID="upSection" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <iframe width="100%" height="455px" src="" runat="server" id="frmSection" frameborder="0" scrolling="no"></iframe>        
        </ContentTemplate>
        </asp:UpdatePanel>
        </td>

    </tr>
    </table> 

    <!-- Section to Search Headings -->    
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;display:none" id="dvSearchBox" runat="server">
    <center>
        <div style="position:absolute;top:0px;left:0px; height :623px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
         <div style="position :relative;width:550px; height:250px;padding :3px; text-align :center;background : white; z-index:150;top:100px;">
         <center>
            <table width="100%" border="0" cellspacing="4" cellpadding="2">
               <tr>
                   <td colspan="2" style="width:100%; background-color:#4371a5; height:22px;color:White;">
                        Search
                   </td>
               </tr>
                <tr>
                   <td style="text-align:right; font-weight:bold;">Search Text :&nbsp</td>
                    <td style="text-align:left;">
                            <asp:Button runat="server" ID="btnSearchC" OnClick="btnSearchC_Click" Height="20px" Visible="false"
                                Text="Search" Style="font-size: 13px; vertical-align: middle; padding-top: 0px;"  />
                            <asp:TextBox runat="server" ID="txtSearchManual" Width="260px" Style="float: left;
                                background-color: lightyellow;"></asp:TextBox>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchManual"
                                WatermarkText="Enter Search Text Here..">
                            </asp:TextBoxWatermarkExtender>                            
                    </td>
                </tr>
                <tr>
                     <td style="text-align:right; font-weight:bold;">Manuals Changed during :&nbsp;</td>
                     <td style="text-align:left;">
                     <asp:TextBox ID="txtFromDate" runat="server" Width="116px" style="background-color:lightyellow;margin:2px;"></asp:TextBox>&nbsp;:&nbsp;<asp:TextBox ID="txtToDate" runat="server" Width="116px" style="background-color:lightyellow;margin:2px;"></asp:TextBox>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFromDate"
                                WatermarkText="Enter From Date">
                            </asp:TextBoxWatermarkExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtToDate"
                                WatermarkText="Enter To Date">
                            </asp:TextBoxWatermarkExtender>     
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy"></asp:CalendarExtender></td>
                </tr> 
                <tr>
                    <td colspan="2" style="text-align:right; padding-right:50px;">
                            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="SearchManual"  />
                            <asp:Button runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click"  />
                    </td>
                </tr>               
            </table>
                 
         </center>
         </div>
    </center>
    </div> 
     
    </form>
</body>
</html>
