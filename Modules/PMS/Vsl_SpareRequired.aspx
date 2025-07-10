<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vsl_SpareRequired.aspx.cs" Inherits="Vsl_SpareRequired" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">           
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                          <tr style=" padding-top:2px;">
                            <td colspan="2" style="padding-right: 5px; padding-left: 2px;">
                            
                            <div style="width:100%; height:417px; border:0px solid #000;  overflow:auto; overflow-y:hidden" ><%--height:460px;--%>
                            
                    <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;" >
                         <tr>
                            <td style="text-align:left; padding-left: 5px; padding-right: 5px; height:3px">
                              
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding-left: 5px; padding-right: 5px;">
                                <table border="0" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <tr class= "headerstylegrid">
                                 <td>Spares</td>
                            </tr>
                            <tr>
                            <td style=" font-size :14px; color:blue; background-color :tan;">
                               <b>Vessel </b> - <asp:Label runat="server" ID="lblSpVessel"></asp:Label>
                            </td>
                            </tr>
                                    <tr>
                                            <td style=" font-size :14px; color:blue; background-color :tan;">
                                                  <table border="0" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                        <tr>
                                                            <td style="text-align:left;width:50px;  font-weight:bold">Component :&nbsp;</td>
                                                            <td style="text-align:left;"><asp:Label ID="lblSpComponent" runat="server"></asp:Label> </td>
                                                            <td style="font-weight:bold" class="style2">Job :&nbsp;</td>
                                                            <td style="text-align:left;"><b>[ <asp:Label ID="lblSPInterval" ForeColor="Red" runat="server"></asp:Label>
                                                                &nbsp;]&nbsp; </b>
                                                                <asp:Label ID="lblSpJob" runat="server"></asp:Label></td>
                                                        </tr>
                                                 </table>
                                            </td>
                                        </tr>    
                                    </tr>
                                    <tr>
                                       <td>
                                       
                               
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                      <col /> 
                      <col style="width:250px"/>
                      <col style="width:150px;"  />
                      <col style="width:200px;" />
                      <%--<col style="width:100px" />--%>
                      <col style="width:90px;" /> 
                      <col style="width:60px;" />
                      <col style="width:80px;" />                    
                      <col style="width:17px;" />
              <tr align="left" class= "headerstylegrid">
                      <td>Name</td>
                      <td>Maker</td>
                      <td>Model/Type</td>
                      <td>Part#</td>
                      <%--<td>Part Name</td>--%>
                      <td>Drawing#</td>
                      <td>Qty(Min)</td>
                      <td>ROB</td>
                      <td></td>    
              </tr>
              </colgroup>
           </table>
           <div id="dvSpares" onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 315px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all"style="width:100%;border-collapse:collapse;">
             <colgroup>
                     <col /> 
                      <col style="width:250px"/>
                      <col style="width:150px;"  />
                      <col style="width:200px;" />
                      <%--<col style="width:100px" />--%>
                      <col style="width:90px;" /> 
                      <col style="width:60px;" />
                      <col style="width:80px;" />                    
                      <col style="width:17px;" />
             </colgroup>
             <asp:Repeater ID="rptComponentSpares" runat="server">
                 <ItemTemplate>
                         <tr class="row">
                              <td align="left"><%#Eval("SpareName")%></td>
                              <td  align="left">
                              <%#Eval("Maker")%>                                                                                     
                              </td>
                              <td align="left"><%#Eval("MakerType")%></td>
                              <td align="left"><%#Eval("PartNo")%>   </td>
                              <%--<td align="left"><%#Eval("PartName")%>   </td>--%>
                              <td align="left"><%#Eval("DrawingNo")%>   </td>
                              <td ><%#Eval("MinQty")%></td>
                              <td ><%#Eval("ROB")%></td>
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
                        
                        </table> 
                                </div>
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
