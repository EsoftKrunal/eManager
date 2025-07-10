<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselSetup.aspx.cs" Inherits="VesselSetup" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
      function added()
        {
          alert('Component added to vessel.');
        }
      function addedfailed()
        {
          alert('Unable to add Componenet to vessel.');
        }
     function fncInputIntegerValuesOnly(evnt) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }             
     function refresh()
     {
        window.opener.reloadtree();
     }
     function refreshpage()
     {
        window.opener.reloadunits();
     }
    </script>        
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-size:12px;font-family:Arial;">
        
        <table style="width :100%" cellpadding="0" cellspacing="0" >
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr style=" display:none" >
                <td align="center"  class="text headerband" >
                    
                    Planned Maintenance System ( PMS )
                   
                </td>
            </tr>
            <tr>
                <td align="center"  class="text headerband" >
                     <asp:Label Text="" ID="lblPage" runat ="server" ></asp:Label> 
                </td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                       <tr>
                <td style="padding-right: 10px; padding-left: 10px;">
                <div style="width:100%; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width:50px;padding-top:28px; text-align :center;display:none;">
                    <div class="scrollbox" style=" height :380px; text-align :center " >
                    <br />
                     <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                     <tr style="padding-top:25px">
                     <td >
                        <asp:Label ID="lbl1" Font-Bold="true" Text="Select Vessel" runat="server"></asp:Label></td>                     
                     </tr>
                     <tr style="padding-top:25px;">
                     <td align="center" >
                     <br />
                        <asp:DropDownList ID="ddlVessels" Width="50" runat="server" AutoPostBack="true" onselectedindexchanged="ddlVessels_SelectedIndexChanged"></asp:DropDownList>
                        <br /><br />
                     </td>
                     </tr>  
                      <tr style="padding-top:25px">
                     <td >
                        <%--<asp:Label ID="Label1" Font-Bold="true" Text="Create Component Structure" runat="server"></asp:Label>--%></td>                     
                     </tr> 
                      <tr style="padding-top:25px">
                     <td >
                     <br />
                        <asp:Label ID="Label2" Font-Bold="true" Text="From" runat="server"></asp:Label></td>                     
                     </tr>  
                     <tr style="padding-top:25px">
                     <td style="text-align :center">
                     <center>
                     <br />
                      
                     </center>
                     <br />
                     </td>
                     </tr>
                           <tr style="padding-top:25px">
                     <td >
                     <br />
                        <asp:Label ID="Label3" Font-Bold="true" Text="OR" runat="server"></asp:Label><br /><br /></td>                     
                     </tr>
                      <tr style="padding-top:25px">
                        <td style=" text-align:center" >
                                <center >
                                    <div class="dottedscrollbox" style=" width : 50px; height :34px; padding-top :10px; "  >
                                        <asp:Button ID="btnOpenStructure" Text="Open Component Structure" 
                                            CssClass="btnorange" Width="50px" runat="server" 
                                            onclick="btnOpenStructure_Click" />
                                     </div>
                                </center>
                        </td>                     
                     </tr>     
                     </table>
                     </div>
                    </td>
                    <td style="width:550px;padding-top:28px; text-align :center;">
                    <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center"  class="text headerband" >                    
                    Search Components&nbsp;</td>
           </tr>
        <tr>
         <td>
            
                       
                          <table cellpadding="2" style="float:left" cellspacing="0" width="100%">
                                           <tr>
                                           <td colspan="6"><asp:Label ID="lblMessage" runat="server" CssClass="error_msg"></asp:Label></td>
                                           </tr>                                                                                                                                     
                                               <tr>
                                            <td style="text-align:right">Component Code :</td>
                                            <td style="text-align:left" ><asp:TextBox ID="txtCompCode" onkeypress="fncInputIntegerValuesOnly(event)" runat="server" MaxLength="12"></asp:TextBox></td>
                                             <td style="text-align:right">Component Name :</td>
                                              <td style="text-align:left"><asp:TextBox ID="txtCopmpname" runat="server" MaxLength="50"></asp:TextBox></td>
                                            </tr>                                                                                     
                                            <tr>                                            
                                            <td style=" padding-right:10px;" colspan="4">
                                            <asp:Button runat="server" CssClass="btnorange" style="float:right" ID="btnSearch" Text="Search" OnClick="SearchComponent_Click" />
                                            </td>
                                            </tr>
                                            <tr>
                                            <td colspan="4" style="text-align:center" >
                                            <asp:Label ID="lblSearchRecords" style="color:Red; font-weight:bold; font-size:12px" runat="server"></asp:Label>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td id="tdSearchResult" colspan="4" runat="server">
                                              <table cellspacing="0" rules="all" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                  <col style="text-align :left" width="100px" />
                                                  <col style="text-align :left" width="500px"/>
                                                  <%--<col style="text-align:left" width="300px" />--%>
                                                  <col width="17px" />
                                                  <tr class="headerstylegrid">
                                                    <td style="text-align :left" width="100px">
                                                        Code</td>
                                                    <td style="text-align :left" width="500px">
                                                        Component Name</td>
                                                   <%-- <td> Parent Component </td>--%>
                                                        <td width="17px"></td>
                                                  </tr>
                                                 </colgroup>
                                               </table>
                                               
                                               <div id="div_Search" style="width :100%; overflow-y:scroll; overflow-x:hidden; height :250px;" onscroll="SetScrollPos(this)" >                        
                                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="100px" />
                                                <col style="text-align :left" width="500px"/>
                                                <%--<col style="text-align :left" width="300px" />--%>
                                                <col width="17px" />
                                            </colgroup>
                                            <asp:Repeater ID="rptCompoenents" runat="server">
                                                <ItemTemplate>
                                                    <tr class='<%# (Eval("ComponentCode").ToString()==SelectedCompCode.ToString())?"selectedrow":"row" %>'>
                                                        <td style="text-align:left">
                                                            <asp:LinkButton ID="btnJobName" runat="server" CommandArgument='<%# Eval("ComponentCode") %>' OnClick="btnComponent_Click" Text='<%# Eval("ComponentCode") %>'></asp:LinkButton>
                                                        </td>
                                                        <td style="text-align:left">
                                                            <%# Eval("ComponentName")%>&nbsp;<%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%></td>
                                                        <%--<td>
                                                            <%# Eval("Parent")%></td>--%>
                                                        <td style="width:17px"></td>    
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class='<%# (Eval("ComponentCode").ToString()==SelectedCompCode.ToString())?"selectedrow":"alternaterow" %>'>
                                                        <td style="text-align:left">
                                                            <asp:LinkButton ID="btnJobName" runat="server" 
                                                                CommandArgument='<%# Eval("ComponentCode") %>' OnClick="btnComponent_Click" Text='<%# Eval("ComponentCode") %>'></asp:LinkButton>
                                                        </td>
                                                        <td style="text-align:left">
                                                            <%# Eval("ComponentName")%>&nbsp;<%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%><br /><%--<td>
                                                            <%# Eval("Parent")%></td>--%><%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%></tr>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                            </table>
                                        </div>                                            
                                            </td>
                                            </tr>                                                                                        
                                           </table>
                                            
                       
        </td> 
        </tr>
        </table>
                    
                    
                    </td>
                    <td style="text-align :left; padding:5px; ">
                    <div style="height: 15px; text-align:center; padding:2px 0px 2px 0px;">
                       <asp:Label ID="lblVessel" Font-Bold="true" Text="" runat="server"></asp:Label> 
                    </div>
                    <div id="dvMode" runat="server" class="dottedscrollbox" style=" height :30px; padding-top :5px; "  >
                     <center>
                     <asp:RadioButtonList ID="rdoMode" RepeatDirection="Horizontal" AutoPostBack="true" runat="server" onselectedindexchanged="rdoMode_SelectedIndexChanged">
                     <asp:ListItem Text="Office Master" Value="1"></asp:ListItem>
                     <asp:ListItem Text="Copy Vessel" Value="2"></asp:ListItem>
                     </asp:RadioButtonList>
                     </center>
                     </div>                     
                    <asp:Panel ID="plOfficeMaster" Visible="false" runat="server">
                     <table cellpadding="0" cellspacing="4" width="100%">
                     <tr><td><asp:DropDownList ID="ddlComponentFirstLevel" runat="server" 
                             AutoPostBack="True" 
                             onselectedindexchanged="ddlComponentFirstLevel_SelectedIndexChanged" 
                             Width="285px"></asp:DropDownList>&nbsp;
                             <asp:Button ID="btnRemoveselectedComp" Text="Remove" runat="server" CssClass="btn"
                             onclick="btnRemoveselectedComp_Click" />
                         </td>
                     </tr>                    
                     <tr>
                        <td>
                            <div id="dvscroll_Componenttree" class="scrollbox" onscroll="SetScrollPos(this)" style="overflow-y:scroll; overflow-x: hidden;  height :350px;">
                                    <asp:TreeView ID="tvComponents" runat="server" ImageSet="Arrows" ShowLines="True" 
                                        ShowCheckBoxes="All" OnTreeNodePopulate="tvComponents_TreeNodePopulate" 
                                        ontreenodecheckchanged="tvComponents_TreeNodeCheckChanged">
                                        <LevelStyles>
                                            <asp:TreeNodeStyle Font-Underline="False" ForeColor="DarkGreen" />
                                            <asp:TreeNodeStyle Font-Underline="False"  />
                                            <asp:TreeNodeStyle Font-Underline="False" ForeColor="Black" />
                                        </LevelStyles>
                                        <HoverNodeStyle CssClass="treehovernode" />
                                        <SelectedNodeStyle CssClass="treeselectednode" />
                                    </asp:TreeView>                                    
                                </div>                          
                        </td>
                     </tr>
                     <tr>
                     <td style="text-align :left; vertical-align :middle" >
                      <uc1:MessageBox ID="MessageBox1" runat="server" />
                     <asp:Button ID="btnPlOMSave" CssClass="btn" style="float:right" Text="Save" 
                             runat="server" onclick="btnPlOMSave_Click" />
                     </td>
                     </tr>
                     </table>                     
                    </asp:Panel>
                    <asp:Panel ID="plExcelTemp" runat="server" Visible="false" >
                    <table cellpadding="0" cellspacing="4" width="100%">                    
                     <tr>
                        <td>
                         <asp:FileUpload ID="fulExcelTemp" runat="server" />
                        </td>
                     </tr>
                     <tr>
                     <td>
                     <asp:Button ID="btnPlETSave" CssClass="btn" style="float:right" Text="Save" OnClick="btnPlETSave_Click" runat="server" />
                         &nbsp;</td>
                     </tr>
                     </table>
                    </asp:Panel>
                    <asp:Panel ID="plCopyVessel" runat="server" Visible="false" >
                    <table cellpadding="0" cellspacing="4" width="100%">
                      <tr>
                         <td style="text-align:right">Copy From Vessel :&nbsp;</td>
                         <td style="text-align:left"><asp:DropDownList ID="ddlCopyVessel" runat="server"></asp:DropDownList>&nbsp;To&nbsp;<asp:Label ID="lblToVesselName" Font-Bold="true" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                          <td style="text-align:right; padding-right:130px;" colspan="2">
                              <asp:Button ID="btnPlCopyVessel" Text="Copy Vessel" OnClientClick="javascript:return confirm('All existing data will be removed.\n Are you sure to copy?');" CssClass="btn" Width="100px" runat="server" onclick="btnPlCopyVessel_Click" /></td>
                      </tr>
                      <tr>
                        <td colspan="2"  style="text-align :left; vertical-align :middle">
                            <uc1:MessageBox ID="MessageBox2" runat="server" />                        
                        </td>
                      </tr>
                    </table>
                    </asp:Panel>
                   
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
    <script type="text/javascript" >
    function ss()
    {
    this.nextSibling.click();
    }
    
    var ctls=document.getElementsByTagName("input");
    for(i=0;i<=ctls.length-1;i++)  
    {   
        if(ctls[i].type=="checkbox")
        {
        ctls[i].onclick=ss;
        } 
    }
    
    </script> 
    </form>
    </body>
</html>

