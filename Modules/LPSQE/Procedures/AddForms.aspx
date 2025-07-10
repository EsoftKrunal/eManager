<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddForms.aspx.cs" Inherits="AddForms" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="SMSManualMenu.ascx" tagname="SMSManualMenu" tagprefix="uc3" %>
<%@ Register src="SMSAdminSubTab.ascx" tagname="SMSAdminSubTab" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <link rel="Stylesheet" href="CSS/style.css" />
    <style type="text/css">
            .sbtn
            {
                background-color:White;
                color:Black;
                font-size:11px;
                font-weight:bold;
            }
            .sel_sbtn
            {
                background-color:orange;
                color:White;
                font-weight:bold;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
         <uc3:SMSManualMenu ID="SMSManualMenu1" runat="server" />
         <uc4:SMSAdminSubTab ID="ManualMenu2" runat="server" />
    <%--<asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>--%>
        <center>
          <div style="width:99%; border:solid 1px #0099CC; padding:3px;">
             <table> 
                   <tr>
                       <td>
                           Forms Category : 
                       </td>
                       <td>
                           <asp:DropDownList ID="ddlFormsCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormsCategory_SelectedIndexChanged" > </asp:DropDownList>
                       </td>
                       <td>
                           Forms Department : 
                       </td>
                       <td>
                           <asp:DropDownList ID="ddlFormsDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormsDepartment_SelectedIndexChanged"  > </asp:DropDownList>
                       </td>
                   </tr>
               </table>
            <%--<table cellpadding="0" cellspacing="2" border="0">
                <tr >
                    <td> <asp:Button ID="LinkButton1" runat="server" Text="A" CommandArgument="A" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton2" runat="server" Text="B" CommandArgument="B" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton3" runat="server" Text="C" CommandArgument="C" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton4" runat="server" Text="D" CommandArgument="D" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton5" runat="server" Text="E" CommandArgument="E" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton6" runat="server" Text="F" CommandArgument="F" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton7" runat="server" Text="G" CommandArgument="G" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton8" runat="server" Text="H" CommandArgument="H" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton9" runat="server" Text="I" CommandArgument="I" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton10" runat="server" Text="J" CommandArgument="J" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton11" runat="server" Text="K" CommandArgument="K" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton12" runat="server" Text="L" CommandArgument="L" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton13" runat="server" Text="M" CommandArgument="M" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton14" runat="server" Text="N" CommandArgument="N" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton15" runat="server" Text="O" CommandArgument="O" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton16" runat="server" Text="P" CommandArgument="P" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton17" runat="server" Text="Q" CommandArgument="Q" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton18" runat="server" Text="R" CommandArgument="R" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton19" runat="server" Text="S" CommandArgument="S" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton20" runat="server" Text="T" CommandArgument="T" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton21" runat="server" Text="U" CommandArgument="U" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton22" runat="server" Text="V" CommandArgument="V" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton23" runat="server" Text="W" CommandArgument="W" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton24" runat="server" Text="X" CommandArgument="X" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton25" runat="server" Text="Y" CommandArgument="Y" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButton26" runat="server" Text="Z" CommandArgument="Z" OnClick="SearchForms"  CssClass="sbtn"></asp:Button>  </td>
                    <td> <asp:Button ID="LinkButtonAll" runat="server" Text=" All " CommandArgument="" OnClick="SearchForms" style="" CssClass="sel_sbtn"></asp:Button>  </td>

                </tr>
            </table>--%>
             <div style="width:100%; height: 29px; overflow-y:scroll;overflow-x:hidden; border:solid 1px #2F9DBA">
            <table cellpadding="2" cellspacing="0" rules="rows" style="text-align:left; border-collapse:collapse;" width="100%" border="0">

                 <col width="100px" />
                <col width="130px" />
                <col />
                <col width="100px" />
                <col width="120px" />
                <col width="130px" />
                <col width="160px" />
                <%--<col width="200px" />--%>
                <tr class= "headerstylegrid" >
                    <td style="text-align:center;">Old Version</td>
                    <td>Form No.</td>
                    <td>Form Name</td>
                    <td>Last Version</td>
                    <td style="text-align:center;">Last Release Dt.</td>
                     <td>Department</td>
                    <td>Category</td>
                    <%--<td>Release New Version</td>--%>
                </tr>
            </table>
            </div>

            <div style="width:100%; height: 425px; overflow-y:scroll;overflow-x:hidden; border:solid 1px #2F9DBA">
             <table cellpadding="2" cellspacing="0" rules="rows" style="text-align:left; border-collapse:collapse;" width="100%" border="1">
                <col width="100px" />
                <col width="130px" />
                <col />
                <col width="100px" />
                <col width="120px" />
                <col width="130px" />
                <col width="160px" />
                <%--<col width="200px" />--%>
                <asp:Repeater ID="rptForms" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center;"> 
                                <asp:LinkButton ID="lnlViewVersion" runat="server" Text=" View " ToolTip=" View all version. " CssClass='<%#Eval("L_Version")%>' OnClick="lnlViewVersion_OnClick" CommandArgument='<%#Eval("FORMNO")%>' Visible='<%#Auth.IsView %>'></asp:LinkButton>
                            </td>
                            <td> <%#Eval("FORMNO")%> </td>
                            <td> <%#Eval("FormName")%> </td>
                            <td> <%#Eval("L_Version")%> </td>
                            <td style="text-align:center;"> <%# Common.ToDateString(Eval("CreatedOn"))%> </td>
                             <td> <%#Eval("DepartmentName")%> </td>
                            <td> <%#Eval("FormsCatName")%> </td>
                            <%--<td>
                                <asp:LinkButton ID="lnkReleaseNewVersion" runat="server" Text=" Release New Version " OnClick="lnkReleaseNewVersion_OnClick" CommandArgument='<%#Eval("FORMNO")%>'></asp:LinkButton> 
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            </div>
            <asp:Button ID="btnAddNewForm" runat="server" CssClass="btn" style="margin:8px;float:right;" Text=" Add New Form " OnClick="btnAddNew_Click"  />
        </div>
        </center>       
    <%-- Add Edit Forms ----------------------------------------------%>
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvAddEditForms" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:840px; height:350px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                <table width="100%" cellpadding="4" style="text-align:left"> 
                    <tr style="font-weight:bold; text-align:center;" class="text headerband">
                        <td colspan="2"> Add/Edit Forms </td>
                    </tr>
                    <tr>
                        <td style="width:120px"> Form Name : </td>
                        <td> <asp:TextBox runat="server"  ID="txtFromName1" Width="95%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td> Form # : </td>
                        <td> <asp:TextBox runat="server"  ID="txtFromNo1" Width="95%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td> Version : </td>
                        <td> <asp:TextBox runat="server"  ID="txtVersion1" Width="95%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td> Revision Date :</td>
                        <td> 
                            <asp:TextBox runat="server" ID="txtRDte"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRDte" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td> Attachment : </td>
                        <td> <asp:FileUpload runat="server" ID="flpFileUpload1" /></td>
                    </tr>
                     <tr>
                        <td> Department : </td>
                        <td> <asp:DropDownList runat="server" ID="ddlDepartment" /></td>
                    </tr>
                     <tr>
                        <td> Category : </td>
                        <td> <asp:DropDownList runat="server" ID="ddlCategory" /></td>
                    </tr>
                    <tr>
                        <td>  </td>
                        <td> 
                        <asp:Button runat="server" ID="btnSave1" Text="Save Form" onclick="btnSubmit1_Click" CssClass="btn" Width="110px" />                        
                        <asp:Button runat="server" ID="btnCloseAddEditForm" Text=" Close " onclick="btnCloseAddEditForm_Click" CssClass="btn" />
                        </td>
                    </tr>
                    <tr>
                        <td>  </td>
                        <td> <asp:Label runat="server" id="lblMessage1" ForeColor="Red" Font-Size="Larger"></asp:Label></td>
                    </tr>
                    </table>
            </div>
        </center>
    </div>
    
    <%-- Show Version ----------------------------------------------%>
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvFormVersion" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:540px; height:280px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                
                <div style="overflow-y:hidden; overflow-x:hidden;width: 100%;border:#4371a5 1px solid;border-bottom:none;" > 
                <table width="100%" cellpadding="2" style="text-align:center; border-collapse:collapse;text-align:left;" border="1" rules="all"> 
                <col />
                <col width="120px" />
                <tr class= "headerstylegrid">
                    <td>Version</td>
                    <td style="text-align:center;">Release Date</td>
                </tr>
                </table>
                </div>

                <div style=" height:200px; overflow-y:scroll; overflow-x:hidden;" style="width: 100%;border:#4371a5 1px solid;"> 
                <table width="100%" cellpadding="2" style="text-align:center; border-collapse:collapse;text-align:left;" border="1"  rules="all" > <colgroup>
                    <col />
                    <col width="120px" />
                    </colgroup>
                    <asp:Repeater runat="server" ID="rptFormsVersion">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkLink" runat="server" CommandArgument='<%#Eval("FormId")%>' style="float:left" Text='<%#Eval("VersionNo")%>' ToolTip='<%#Eval("FileName")%>' OnClick="btnDownloadClick"></asp:LinkButton>  
                            </td>
                            <td style="text-align:center;"><%# Common.ToDateString(Eval("CreatedOn"))%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
                </div>

                <asp:Button ID="btnInActive" runat="server" Text=" InActive " OnClick="btnInActive_OnClick" CssClass="btn" style="margin:8px;" />
                <asp:Button ID="btnReleaseNewVersion" runat="server" Text=" Release New Version " OnClick="lnkReleaseNewVersion_OnClick" CssClass="btn" style="margin:8px;" />
                <asp:Button ID="btnCloseFormVersion" runat="server" Text=" Close " CssClass="btn" OnClick="btnCloseFormVersion_OnClick" style="margin:8px;" />
                
            </div>
        </center>
    </div>

    <%--</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnSave1" />
    </Triggers>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
