<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MocRequest.aspx.cs" Inherits="HSSQE_MOC_MocRequest" %>
<%--<%@ Register src="~/HSSQE/HSSQEMenu.ascx" tagname="HSSQEMenu" tagprefix="uc1" %>
<%@ Register src="MocMenu.ascx" tagname="MocMenu" tagprefix="uc1" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script src="../../js/Common.js" type="text/javascript"></script>
    <title></title>
</head>

    
 <body style="margin:0 0 0 0">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
    <%--<uc1:HSSQEMenu ID="HSSQEMenu1" runat="server" />
    <uc1:MocMenu ID="MocMenu" runat="server" />--%>
    <table width='100%' cellpadding="0" cellspacing="0">
    <tr>
        <td>
             <div class="box1">  
                     <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                     <tr>
                     <%--<td style="text-align:right; vertical-align:middle;">Vessel :&nbsp;</td>
                     <td style="text-align:left; vertical-align:middle;"><asp:DropDownList ID="ddlVessel" runat="server" Width="150px" /></td>
                     <td style="text-align:right; vertical-align:middle;">Office :&nbsp;</td>
                     <td style="text-align:left; vertical-align:middle;"><asp:DropDownList ID="ddlOffice" runat="server" Width="150px" /></td>
                     <td style="text-align:right; vertical-align:middle;">Period :&nbsp;</td>
                     <td style="text-align:left; width:90px; vertical-align:middle;">
                        <asp:TextBox runat="server" ID="txtEventDate" CssClass="input_box" MaxLength="15" Width="85px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEventDate" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                     </td>
                     <td style="text-align:right;width:90px; vertical-align:middle;">
                        <asp:TextBox runat="server" ID="txtEventDate1" CssClass="input_box" MaxLength="15" Width="85px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEventDate1" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                     </td>--%>

                     <td style="text-align:left; vertical-align:middle;width:50px;">Year :&nbsp;</td>

                     <td style="text-align:left; vertical-align:middle;">
			<asp:DropDownList ID="ddlFYear" AutoPostBack="true" onSelectedIndexChanged="ddlFYear_onSelectIndexChanged" runat="server" Width="80px" >
                     
                     </asp:DropDownList>
                     </td>
                     <td style="text-align:right;">
                        <%--<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />--%>
                        <asp:Button ID="btnCreateNewMOC" runat="server" OnClick="btnCreateNewMOC_Click" Text="New MOC Request" Visible="false"  />
                     </td>
                     </tr>
                     </table>
                </div>
           
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top">
                <div class="dvScrollheader">  
                <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                         <colgroup>
                            <col style="text-align: left" width="20px" />
                            <col style="text-align: left" width="100px" /> 
                            <col style="text-align: left" />
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="200px" /> 
                            <col style="text-align: left" width="150px" /> 
                            <col style="text-align: left" width="90px" />
                            <col style="text-align: left" width="90px" />
                            <col style="text-align: left" width="90px" />
                            <col style="text-align: left" width="90px" />                            
                            <col style="text-align: left" width="90px" />
                            <col width="25px" />
                        </colgroup>
                        <tr >
                            <td>&nbsp;</td>
                            <td>&nbsp;MOC#</td>
                            <td>&nbsp;Topic</td>
                            <td>&nbsp;Source</td>
                            <td>&nbsp;Location</td>
                            <td>&nbsp;Request By</td>
                            <td>&nbsp;MOC Date</td>
                            <td>&nbsp;App. Date</td>
                            <td>&nbsp;Comp. Date</td>
                            <td>&nbsp;Review Date</td>
                            <td>&nbsp;Status</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
                
                <div class="dvScrolldata" style="height: 450px;">
                    <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                            <col style="text-align: left" width="20px" />
                            <col style="text-align: left" width="100px" /> 
                            <col style="text-align: left" />
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="200px" />
                            <col style="text-align: left" width="150px" /> 
                            <col style="text-align: left" width="90px" />
                            <col style="text-align: left" width="90px" />
                            <col style="text-align: left" width="90px" />
                            <col style="text-align: left" width="90px" />                                                        
                            <col style="text-align: left" width="90px" />                            
                            <col width="25px" />
                        </colgroup>
                        <asp:Repeater ID="rptMOC" runat="server">
                            <ItemTemplate>
                                <tr onmouseover="style.backgroundColor='Yellow'" onmouseout="style.backgroundColor=''" >
                                    <td align="center"><asp:ImageButton ID="btnView" OnClick="btnView_Click" ImageUrl="~/HSSQE/Images/search_magnifier_12.png" ToolTip="View" CommandArgument='<%#Eval("MocRequestId")%>' runat="server" />   </td>
                                    <td>&nbsp;<%#Eval("MOCNumber")%></td>
                                    <td>&nbsp;<%#Eval("Topic")%></td>
                                    <td>&nbsp;<%#Eval("Source")%></td>
                                    <td>&nbsp;<%#Eval("VesselOffice")%></td>
                                    <td align="left">&nbsp;<%#Eval("RequestedByName")%></td>
                                    <td>&nbsp;<%# Common.ToDateString(Eval("MOCDate"))%></td>
                                    <td>&nbsp;<%# Common.ToDateString(Eval("Approved1On"))%></td>
                                    <td>&nbsp;<%# Common.ToDateString(Eval("ProposedTimeline"))%></td>
                                    <td>&nbsp;<%# Common.ToDateString(Eval("ReviewedOn"))%></td>
                                    <td>&nbsp;<%#Eval("StatusName")%></td>
                                    <td>&nbsp;</td>                                                                
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
      
       
    </td>
    </tr>
    </table>
    </div>

        <div ID="dv_AddNewMOC" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
        <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:950px;  height:475px;padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 5px black;">
        <center >
                <div class="box3"><b>Create MOC</b></div>
                <div style="height: 380px; padding:3px; ">
                  <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                    <tr>
                    <td style="text-align:right; width:10%; font-weight:bold;">Source : </td>
                    <td style="text-align:left; width:13%;"><asp:DropDownList ID="ddlSource" AutoPostBack="true" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged" runat="server" Width="90%" >
                            <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Vessel" Value="Vessel"></asp:ListItem>
                            <asp:ListItem Text="Office" Value="Office"></asp:ListItem>
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="ddlSource" ErrorMessage="*" Display="Dynamic" InitialValue="0" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:right; width:10%; font-weight:bold;">VSL/ Office : </td>
                    <td style="text-align:left;"><asp:DropDownList ID="ddlVessel_Office" runat="server" Width="60%" >
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlVessel_Office" ErrorMessage="*" Display="Dynamic" InitialValue="0" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:right; width:10%; font-weight:bold; display:none;">MOC Date: </td>
                    <td style="text-align:left; width:13%;display:none;">
                    <%--<asp:TextBox runat="server" ID="txtMOCDate" CssClass="input_box" MaxLength="15" Width="95px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtMOCDate" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMOCDate" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>--%>
                     </td>
                    </tr>
                    <tr>
                    <td colspan="3" style="text-align:right; font-weight:bold;">Topic: </td>
                    <td colspan="3" style="text-align:left;"><asp:TextBox runat="server" ID="txtTopic" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTopic" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                     </td>
                     </tr>
                    <tr>
                    <td colspan="3" style="text-align:right; font-weight:bold;">Impact : </td>
                    <td colspan="3" style="text-align:left;"><asp:CheckBoxList ID="cbImpact" RepeatDirection="Horizontal" runat="server" >
                            <asp:ListItem Text="People" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Process" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Equipment" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Safety" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Environment" Value="5"></asp:ListItem>
                       </asp:CheckBoxList>
                       <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cbImpact" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>--%>
                        
                    </td>
                    </tr>
                    <tr>
                    <td colspan="3" style="text-align:right; font-weight:bold;">Reason for change: </td>
                    <td colspan="3" style="text-align:left;"><asp:TextBox runat="server" ID="txtReasonforChange" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="100px"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMOCDate" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>--%>
                     </td>
                     </tr>
                    <tr>                    
                        <td colspan="3" style="text-align:right; font-weight:bold;">Brief Description of change: </td>
                        <td colspan="3" style="text-align:left;"><asp:TextBox runat="server" ID="txtDescr" CssClass="input_box" TextMode="MultiLine"  Width="99%" Height="100px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMOCDate" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>--%>
                         </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align:right; font-weight:bold; ">Proposed TimeLine for completion of change : </td>
                        <td colspan="3" style="text-align:left; "><asp:TextBox runat="server" ID="txtPropTL" CssClass="input_box" MaxLength="15" Width="90px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtPropTL" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPropTL" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                 </table>
                </div>
          </center>
          <div style="padding:3px; text-align:left;">
               <asp:Label ID="lblMsg" runat="server" ForeColor="Red" ></asp:Label>
          </div>
          <div style="padding:3px; text-align:center; ">
          <asp:Button runat="server" ID="btnSaveNew" Text="Save" ValidationGroup="V1" OnClick="btnSaveNew_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;" />
          <asp:Button runat="server" ID="btnNext" Text="Next >>" CausesValidation="false" OnClick="btnNext_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;" Visible="false" />
          <asp:Button runat="server" ID="btnCloseNew" Text="Close" OnClick="btnCloseNew_Click" CausesValidation="false" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;" />
          </div>
          </div>
        </center>
        </div>


    </form>
</body>
    

</html>
