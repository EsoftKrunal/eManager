<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReadForms.aspx.cs" Inherits="ReadForms" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/LPSQE/Procedures/SMSManualMenu.ascx" tagname="SMSManualMenu" tagprefix="uc3" %>
<%@ Register src="~/Modules/LPSQE/Procedures/SMSSubTab.ascx" tagname="SMSSubTab" tagprefix="uc4" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="../../HRD/Styles/StyleSheet.css" />
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
                background-color:#4371A5;
                color:White;
                font-weight:bold;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
        <%-- <uc3:SMSManualMenu ID="ManualMenu2" runat="server" />--%>
    <uc4:SMSSubTab ID="SMSManualMenu1" runat="server" />
    <%--<asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>       --%>

    <div>
    <div style="font-family:Arial;font-size:12px;">
        <div style="width:100%; border:solid 1px #0099CC;">
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
            <div style="overflow-y:hidden; overflow-x:hidden;" style="width: 100%;border:#4371a5 1px solid;"> 
            <table width="100%" cellpadding="2" style="text-align:center; border-collapse:collapse;text-align:left;" border="1">
                <colgroup>
                <col width="100px" />
                <col width="130px" />
                <col />
                 <col width="100px" />
                <col width="120px" />
                <col width="130px" />
                <col width="160px" />
                 <col width="20px" />
                <tr style="background-color:#c2c2c2;font-weight:bold;">
                    <td style="text-align:center;"></td>
                    <td>Form No.</td>
                    <td>Form Name</td>
                    <td>Revision#</td>
                    <td style="text-align:center;">Revision Date</td>
                    <td>Department</td>
                    <td>Category</td>
                    <td></td>
                </tr>
                 </colgroup>
            </table>
            </div>

            <div style=" height:425px; overflow-y:scroll; overflow-x:hidden;" style="width: 100%;border:#4371a5 1px solid;"> 
            <table width="100%" cellpadding="2" style="text-align:center; border-collapse:collapse;text-align:left;" border="1"> 
                <colgroup>

               
                <col width="100px" />
                <col width="130px" />
                <col />
               <col width="100px" />
                <col width="120px" />
                <col width="130px" />
                <col width="160px" />
                 <col />
                <asp:Repeater ID="rptForms" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center;"> 
                                <asp:LinkButton ID="lnlViewVersion" runat="server" Text=" Download " OnClick="lnlViewVersion_OnClick" CommandArgument='<%#Eval("FORMID")%>' ToolTip='<%#Eval("FileName")%>'></asp:LinkButton>
                            </td>
                            <td> <%#Eval("FORMNO")%> </td>
                            <td> <%#Eval("FormName")%> </td>
                            <td> <%#Eval("L_Version")%> </td>
                            <td style="text-align:center;"> <%# Common.ToDateString(Eval("CreatedOn"))%> </td>  
                            <td> <%#Eval("DepartmentName")%> </td>
                            <td> <%#Eval("FormsCatName")%> </td>
                            <td></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                     </colgroup>
            </table>
            </div>
            
        </div>
    </div>
    </div>        

    <%-- Show Version ----------------------------------------------%>
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;font-family:Arial;font-size:12px;" id="dvFormVersion" runat="server" visible="false">
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
                <asp:Button ID="btnCloseFormVersion" runat="server" Text=" Close " CssClass="btn" OnClick="btnCloseFormVersion_OnClick" style="margin:8px;" />
            </div>
        </center>
    </div>

    <%--</ContentTemplate>    
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
