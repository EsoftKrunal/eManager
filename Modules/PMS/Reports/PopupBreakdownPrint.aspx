<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupBreakdownPrint.aspx.cs" Inherits="Reports_PopupBreakdownPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center" style="height: 23px; text-align :center; padding-top :3px;" class="pagename" >                     
                    DEFECT REPORT&nbsp;</td>
            </tr>
            <tr>
                <td align="center"><b>Defect No. :&nbsp;<asp:Label ID="lblNo" runat="server"></asp:Label></b></td>
            </tr>
            <tr>
                <td align="left">
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <div style="width:100%; height:530px; border:0px solid #000; OVERFLOW-Y: scroll; OVERFLOW-X: hidden;"  class="scrollbox" >
                        <table border="1" cellpadding="4" cellspacing="0" style="width:100%">
                        <tr>
                          <td colspan="2" align="left">
                               <table cellpadding="4" cellspacing="0" style="width:100%;">
                                 <tr>   
                                     <td style="text-align:left;font-weight:bold;width:120px;">Component Code&nbsp;</td>
                                     <td style="text-align:left;font-weight:bold">Component Name&nbsp;</td>
                                     <td style="text-align:left;font-weight:bold;width:250px;">Component Category&nbsp;</td>
                                 </tr>
                                 <tr>
                                    <td style="text-align:left;"><asp:Label ID="lblCompCode" runat="server"></asp:Label></td>
                                    <td style="text-align:left;"><asp:Label ID="lblCompName" runat="server"></asp:Label></td>
                                    <td style="text-align:left;">
                                        <asp:CheckBoxList ID="chkCritical" runat="server" Enabled="false" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="ClassEquipment" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="CriticalEquipment" Value="2"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>        
                                </table>
                          </td>
                        </tr>
                        
                        <tr>
                            <td align="left">
                                 <table cellpadding="4" cellspacing="0" style="width:100%;">
                                      <tr>
                                          <td style="text-align:left;font-weight:bold">REPORT DATE</td>
                                          <td style="text-align:left;font-weight:bold">TARGET DATE</td>
                                          <td style="text-align:left;font-weight:bold">COMPLETION DATE</td>
                                      </tr>
                                      <tr>
                                         <td style="text-align:left;"><asp:Label ID="lblReportDt"  Width="90px" runat="server"></asp:Label>
                                                                      
                                         </td>
                                         <td style="text-align:left;"><asp:Label ID="lblTargetDt"  Width="90px" runat="server"></asp:Label>
                                                                      
                                         </td>
                                         <td style="text-align:left;"><asp:Label ID="lblCompletionDt"  Width="90px" runat="server"></asp:Label>
                                                                      
                                         </td>
                                      </tr>
                                 </table>
                            </td>
                            <td style="padding:0px;">
                                <table cellpadding="0" cellspacing="0" style="width:100%;">
                                      <tr>
                                           <td style="text-align:center;font-weight:bold">REPAIRS TO BE CARRIED OUT BY</td>
                                      </tr>
                                      <tr>
                                          <td>
                                              <table border="1" cellpadding="0" cellspacing="0" style="width:100%;">
                                                   <tr>
                                                        <td>&nbsp;</td>
                                                        <td colspan="2" style="text-align:center">MTM</td>
                                                        <td ><asp:CheckBox ID="ChkDrydock" runat="server" />Drydock</td>
                                                   </tr>
                                                   <tr>
                                                        <td><asp:CheckBox ID="chkVessel" runat="server" />Vessel</td>
                                                        <td><asp:CheckBox ID="chkSpares" runat="server" />Spares</td>
                                                        <td><asp:CheckBox ID="chkShAssist" runat="server" />Shore Assistance</td>
                                                        <td><asp:CheckBox ID="chkGuarantee" runat="server" />Guarantee</td>
                                                   </tr>
                                              </table>
                                          </td>
                                      </tr>
                                </table>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                              <table cellpadding="4" cellspacing="0" style="width:100%;">
                                  <tr>
                                    <td style="text-align:left;font-weight:bold">DEFECT DETAILS&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td style="text-align:left;">
                                          <asp:Label ID="lblDefectdetails" runat="server" Height="150px" Width="450px"></asp:Label>
                                      </td>
                                  </tr>
                              </table>
                            </td>
                            <td>
                                <table cellpadding="4" cellspacing="0" style="width:100%;">
                                  <tr>
                                    <td style="text-align:left;font-weight:bold">REPAIRS CARRIED OUT&nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td style="text-align:left;">
                                          <asp:Label ID="lblRepairsCarriedout" runat="server" Height="150px" Width="450px"></asp:Label>
                                      </td>
                                  </tr>
                              </table>
                            
                            </td>
                        </tr>
                        
                        <tr id="trCompStatus" runat="server">
                            <td colspan="2">
                               <table cellpadding="4" cellspacing="0" style="width:100%;">
                                  <tr>
                                    <td style="text-align:left;font-weight:bold; width:130px;">Component Status&nbsp;</td>
                                    <td style="text-align:left;">
                                        <%--<asp:DropDownList ID="ddlCompStatus"  runat="server">
                                            <asp:ListItem Text="<Select>" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Equipment Working" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Equipment Not Working" Value="2"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <asp:Label ID="lblCompStatus" runat="server" ></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td style="text-align:left;font-weight:bold;"></td>
                                    <td style="text-align:left;"></td>
                                    <td>&nbsp;</td>
                                    <td style="text-align:left;"></td>
                                    <td style="text-align:left;" >
                                    </td>
                                  </tr>
                                </table>
                            </td>
                        
                        </tr>
                        
                        <tr>
                            <td colspan="2">
                                <table cellpadding="4" cellspacing="0" style="width:100%;">
                                  <tr>
                                    <td style="text-align:left;font-weight:bold; width:130px;">SPARES ON BOARD&nbsp;</td>
                                    <td style="text-align:left;width:50px;">
                                        <%--<asp:DropDownList ID="ddlSOB" Width="50px" runat="server" AutoPostBack="true" onselectedindexchanged="ddlSOB_SelectedIndexChanged">
                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <asp:Label ID="lblSOB" runat="server" ></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td style="text-align:left;font-weight:bold;width:70px;">RQN NO :&nbsp;</td>
                                    <td style="text-align:left;width:100px;"><asp:Label ID="lblRqnNo" runat="server"></asp:Label></td>
                                    <td>&nbsp;</td>
                                    <td style="text-align:left;width:80px; font-weight:bold">RQN DATE :&nbsp;</td>
                                    <td style="text-align:left;" ><asp:Label ID="lblRqnDt" runat="server"></asp:Label>
                                    
                                    </td>
                                  </tr>
                                </table>
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="2"> 
                               <div style="text-align:left; vertical-align:middle; font-weight:bold;">SPARES REQUIRED?&nbsp;
                                    <%--<asp:DropDownList ID="ddlSparesReqd" Width="100px" runat="server" AutoPostBack="true" onselectedindexchanged="ddlSparesReqd_SelectedIndexChanged">
                                           <asp:ListItem Text="< SELECT >" Selected="True" Value="0" ></asp:ListItem> 
                                           <asp:ListItem Text="Yes" Value="1" ></asp:ListItem>
                                           <asp:ListItem Text="No" Value="2" ></asp:ListItem>
                                    </asp:DropDownList> --%>
                                    <asp:Label ID="lblSparesReqd" runat="server"></asp:Label>
                               </div>
                                <table id="tblSpares" runat="server" cellpadding="0" cellspacing="0" visible="false" 
                                                                    style="background-color: #f9f9f9; padding-top:3px; height: 191px;" width="100%">
                                    
                                    <tr>
                                        <td>
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                                <colgroup>
                                                         <col  />
                                                        <col style="width: 150px"/>
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 17px;" />
                                                    <tr align="left" class= "headerstylegrid">
                                                        <td>
                                                            Name
                                                        </td>
                                                        <td>
                                                            Maker
                                                        </td>
                                                        <td>
                                                            Part#
                                                        </td>
                                                        <td>
                                                            Qty(Cons.)
                                                        </td>
                                                        <td>
                                                            Qty(ROB)
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                            <div id="dvSpares" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll;
                                                overflow-x: hidden; width: 100%; height: 100px; text-align: center;">
                                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;
                                                    border-collapse: collapse;">
                                                    <colgroup>
                                                        <col  />
                                                        <col style="width: 150px"/>
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 17px;" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptComponentSpares" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="row" visible='<%#Eval("SpareId").ToString() != "" %>'>
                                                                <td align="left">
                                                                    <%#Eval("SpareName")%><asp:HiddenField ID="hfSpareId" Value='<%#Eval("SpareId") %>' runat="server" />
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("Maker")%><asp:HiddenField ID="hfOffice_Ship" Value='<%#Eval("Office_Ship") %>' runat="server" />
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("PartNo")%>
                                                                </td>
                                                                <td align="center">
                                                                   <%#Eval("QtyCons")%>
                                                                <td align="center">
                                                                   <%#Eval("QtyRob")%>
                                                                </td>
                                                                <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="2">
                                <table cellpadding="4" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td style="text-align: center;font-weight:bold;"><asp:Label ID="lblSupdt" runat="server"></asp:Label></td>
                                        <td>&nbsp;</td>
                                        <td style="text-align: center;font-weight:bold;"><asp:Label ID="lblCO" runat="server"></asp:Label></td>
                                        <td>&nbsp;</td>
                                        <td style="text-align: center;font-weight:bold;"><asp:Label ID="lblCE" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; ">SUPDT&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td style="text-align: center; ">CHIEF OFFICER&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td style="text-align: center; ">CHIEF ENGINEER&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center; padding-top:5px;">
                                    <img src ="../Images/PrintReport.jpg" id='btnprnt' onclick="document.getElementById('btnprnt').style.display='none';window.print();"/>
                            </td>
                        </tr>
                        </table>
                        </div>
                        </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </td> 
        </tr>
        </table>
     </div>
    </form>
</body>
</html>
