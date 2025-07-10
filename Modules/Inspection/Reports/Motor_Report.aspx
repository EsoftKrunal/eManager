<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Motor_Report.aspx.cs" Inherits="Motor_Report" Title="Motor Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />  
    <script language="javascript" type="text/javascript">
     function FIR_Report(file)
     {
        window.open('FIR_Report_PopUp.aspx?File=' + file);  
     }
     
     function OpenMototReport(Mode,MID)
     {
        window.open('MotorAdd.aspx?Mode='+Mode+'&MID='+MID+'');  
     }
     function Refresh()
     {
      var btn =  document.getElementById("btnRefresh");
      btn.click();
//         __doPostBack('ctl00_ContentPlaceHolder1_btnRefresh', '');
     }
    </script>
     </head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center" valign="top" >
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                           
                            <%--<asp:Button ID="btnRefresh" OnClick="btnRefresh_Click" style="display:none" runat="server" />--%>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="padding-right: 10px; color: red; text-align: center">
                                        <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                            <table cellpadding="3" cellspacing="0" width="100%">
                                            <tr>
                                               <td style="text-align: right" valign="bottom">
                                                    Owner :&nbsp 
                                                </td>
                                                <td valign="bottom">
                                                <asp:DropDownList ID="ddlOwner" AutoPostBack="true" runat="server" CssClass="input_box" onselectedindexchanged="ddlOwner_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                               
                                                <td style="text-align: right" valign="bottom">
                                                    Fleet : </td>
                                                <td valign="bottom">
                                                    <asp:DropDownList runat="server" ID="ddlFleet" CssClass="input_box" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged" >
                                                    </asp:DropDownList> 
                                                    </td>
                                                <td style="text-align: right" valign="bottom">
                                                    Vessel :</td>
                                                <td style="text-align: left" valign="bottom">
                                                    <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" Width="180px"></asp:DropDownList>   
                                                </td>
                                                <td style="text-align: left" valign="bottom">
                                                    Month :</td>
                                                <td style="text-align: left" valign="bottom">
                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="input_box" Width="80px">
                                                        <asp:ListItem Text="< All >" Value="0" ></asp:ListItem>  
                                                        <asp:ListItem Text="Jan" Value="1" ></asp:ListItem>  
                                                        <asp:ListItem Text="Feb" Value="2" ></asp:ListItem>  
                                                        <asp:ListItem Text="Mar" Value="3" ></asp:ListItem>  
                                                        <asp:ListItem Text="Apr" Value="4" ></asp:ListItem>  
                                                        <asp:ListItem Text="May" Value="5" ></asp:ListItem>  
                                                        <asp:ListItem Text="Jun" Value="6" ></asp:ListItem>  
                                                        <asp:ListItem Text="Jul" Value="7" ></asp:ListItem>  
                                                        <asp:ListItem Text="Aug" Value="8" ></asp:ListItem>  
                                                        <asp:ListItem Text="Sep" Value="9" ></asp:ListItem>  
                                                        <asp:ListItem Text="Oct" Value="10" ></asp:ListItem>  
                                                        <asp:ListItem Text="Nov" Value="11" ></asp:ListItem>  
                                                        <asp:ListItem Text="Dec" Value="12" ></asp:ListItem>
                                                    </asp:DropDownList>  
                                                    </td>
                                                <td style="text-align: right" valign="bottom">
                                                    Year :</td>
                                                <td style="text-align: left; padding-left: 4px;" valign="bottom" >
                                                    <asp:DropDownList ID="ddlyear" runat="server" CssClass="input_box"  Width="80px"></asp:DropDownList>
                                                 </td>
                                                
                                                <td style="text-align: left" valign="top">
                                                    <asp:Button ID="btn_Show" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_Show_Click"  />
                                                    &nbsp;<asp:Button ID="btn_Clear" runat="server" CssClass="btn" Text="Clear" OnClick="btn_Clear_Click"  />
                                                     <asp:Button ID="btnRefresh" style="display:none;" runat="server" onclick="btnRefresh_Click" />
                                                </td>
                                            </tr>
                                            </table>
                                            </td>
                                    </tr>
                                 </table>
                                            <asp:GridView style="TEXT-ALIGN: center" id="Grd_MOTOR" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" GridLines="Horizontal" HeaderStyle-Wrap="false" PageSize="15" OnPageIndexChanging="Grd_NearMiss_PageIndexChanging">
                                            <RowStyle CssClass="rowstyle"></RowStyle>
                                            <Columns>
                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%--<img src="../Images/HourGlass.gif"  onclick="OpenMototReport('V',<%#Eval("MID") %>)" style="cursor:pointer;"  />--%>
                                                    <asp:ImageButton ID="imgView" runat="server" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" style="cursor:pointer;" CommandArgument='<%#Eval("MID") %>' Visible='<%#Eval("Published").ToString() == "Y" %>' OnClick="btnReport_Click"/>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
                                            </asp:TemplateField >
                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <img src="../../HRD/Images/edit.jpg" style="cursor:pointer; display:<%# (Eval("Locked").ToString() == "Y") ? "none" : "block" %> " onclick="OpenMototReport('E',<%#Eval("MID") %>)" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="30px" ></ItemStyle>
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="VesselName" HeaderText="Vessel">
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" ></ItemStyle>
                                            </asp:BoundField>
                                            
                                            <asp:TemplateField HeaderText="Report#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReportNo" runat="server" Text='<%#Eval("ReportNo") %>'></asp:Label>
                                                <asp:HiddenField ID="hfdMID" runat="server" Value='<%#Eval("MID") %>' />            
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="150px" ></ItemStyle>
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="PreparedBy" HeaderText="Last Updated By">
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="ReportDate" HeaderText="Report Dt.">
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            <%--<asp:BoundField DataField="SupTdName" HeaderText="SUPTD Name">
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px"></ItemStyle>
                                            </asp:BoundField>--%>
                                            
                                            <asp:TemplateField HeaderText="Publish">
                                            <ItemTemplate>
                                                <asp:Button ID="btnPublish" CssClass="btn" runat="server" Text='Publish'  CommandArgument ='<%#Eval("MID") %>' OnClick="Publish_Report" Visible='<%#Eval("Exclamation_Vesibility").ToString() == "False" && Eval("Locked").ToString() != "Y" %>' ></asp:Button>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="100px" ></ItemStyle>
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="PublishedOn" HeaderText="Published On">
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Image ID="imgDetails" ImageUrl="~/Modules/HRD/Images/exclamation_mark.gif" ToolTip="Report not completed."  Visible='<%#Eval("Exclamation_Vesibility").ToString() == "True" %>' runat="server" />  <%--DetailsCount--%>
                                                <asp:ImageButton ID="btnReport" ImageUrl="~/Modules/HRD/Images/paperclip.gif" CommandArgument='<%#Eval("MID") %>' OnClick="btnReport_Click" runat="server" Visible="false"  /></ItemTemplate>  <%--Visible='<%#Eval("Published").ToString() == "Y" %>' --%>
                                            <ItemStyle HorizontalAlign="Left" Width="60px" ></ItemStyle>
                                            </asp:TemplateField>
                                            
                                            </Columns>
                                            <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                            <PagerStyle HorizontalAlign="Center"></PagerStyle>
                                            <SelectedRowStyle CssClass="selectedtowstyle"></SelectedRowStyle>
                                            <HeaderStyle Wrap="False" CssClass="headerstylefixedheader" ForeColor="#0E64A0"></HeaderStyle>
                                            </asp:GridView>
                                            
                                    </td>
                                </tr>
                                <tr>
                                   <td style="text-align:right; padding-right:3px; padding-top:2px;">
                                    <asp:Button ID="btn_OwnerPublish" CssClass="btn" Text="Print by Owner" runat="server" 
                                           onclick="btn_OwnerPublish_Click" />
                                   </td>
                                
                                </tr>
                            </table> </fieldset>
                        </td>
           
                    </tr>
                </table>
           
    
    <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="DivAckRecieve" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :510px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:400px; height:126px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:150px;opacity:1;filter:alpha(opacity=100)">
                <table cellpadding="5" cellspacing="0" border="1" width="100%" style=" border-collapse:collapse">
                        <tr><td colspan="3" style="height :20px;  text-align :center" class="text headerband"> <b> Motor Report - Publish </b> </td></tr>
                        <tr>
                            <td style="text-align:right ">Suptd Name :</td>
                            <td style="text-align:left "><asp:TextBox ID="txtSupdtName" runat="server" CssClass="input_box" Width="200px" ></asp:TextBox></td>
                        </tr>
                        <%--<tr>
                            <td style="text-align:right ">Prepared By :</td>
                            <td style="text-align:left "><asp:TextBox ID="txtPreparedBy" runat="server" CssClass="input_box" Width="200px"></asp:TextBox></td>
                        </tr>--%>
                        <tr>
                            <td style="text-align:center;" colspan="2">
                                <asp:Button ID="btnPublishMotor" OnClick="btnPublishMotor_Click" runat="server" Text="Publish" CssClass="btn" />
                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn" />
                            </td>
                        </tr>
                </table>
            </div>
         </center>
    </div>
    
    <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="divPublishOwner" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :510px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:400px; height:180px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:150px;opacity:1;filter:alpha(opacity=100)">
                <table cellpadding="5" cellspacing="0" border="1" width="100%" style=" border-collapse:collapse">
                        <tr><td colspan="3" style="height :20px;  text-align :center" class="text headerband"> <b> Publish for owner portal </b> </td></tr>
                        <tr>
                            <td style="text-align:right ">Owner :</td>
                            <td style="text-align:left "><asp:DropDownList ID="ddl_OwnerPublish" AutoPostBack="true" runat="server" CssClass="input_box" OnSelectedIndexChanged="ddl_OwnerPublish_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:DropDownList ID="ddl_OwnerPublishVessel" runat="server" style="display:none" CssClass="input_box" Width="50px"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="text-align:right ">Month :</td>
                            <td style="text-align:left "> 
                                <asp:DropDownList ID="ddl_OwnerMonth" runat="server" CssClass="input_box" Width="80px">
                                    <asp:ListItem Text="< SELECT >" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right">Year :</td>
                            <td style="text-align: left">
                             <asp:DropDownList ID="ddl_OwnerYear" runat="server" CssClass="input_box" Width="80px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;" colspan="2">
                                <asp:Button ID="btn_PublishOwner"  runat="server" Text="Publish" OnClick="btn_PublishOwner_Click" CssClass="btn" />
                                <asp:Button ID="btn_PublishOwnerCancel" OnClick="btn_PublishOwnerCancel_Click" runat="server" Text="Cancel" CssClass="btn" />
                            </td>
                        </tr>
                        <tr>
                           <td style="text-align:center;" colspan="2">
                              <asp:Label ID="lblmsg_PublishOwner" runat="Server" Font-Size="12px" ForeColor="Red" ></asp:Label>
                           </td>
                        </tr>
                </table>
            </div>
         </center>
    </div>
    
</form>
</body>
</html>

