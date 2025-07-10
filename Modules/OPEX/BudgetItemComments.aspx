<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetItemComments.aspx.cs" Inherits="BudgetItemComments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Budget Item Comments</title>
    <link href="CSS/style.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" >
        function ExpandCommPerAcc(obj) 
        {
            obj.parentNode.style.display = 'none';
            obj.parentNode.nextSibling.style.display='Block';
        }
        function CollapsCommPerAcc(obj) 
        {
            obj.parentNode.style.display = 'none';
            obj.parentNode.previousSibling.style.display = 'Block';
        }
        function ReloadPage() 
        {
            document.getElementById('btnReload').click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfCommentID" runat="server" />
    <div>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="header" style=" padding:4px;font-size:medium;" >  Budget Item Comments 
            <asp:Button ID="btnReload" runat="server" OnClick="btnReload_OnClick" style="display:none;" />
            </td>
        </tr>
        <tr>
            <td class="header" style=" padding:4px;">
                <asp:Label ID="lblCompany" runat="server" ></asp:Label> / <asp:Label ID="lblShipID" runat="server" ></asp:Label> / <asp:Label ID="lblMajorCat" runat="server" > </asp:Label> / <asp:Label ID="lblMidCat" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>  
                 <asp:Label ID="lblIComInfo" runat="server" ></asp:Label> 
             </td>
        </tr>
        <tr>
            <td>
            <table cellpadding="0" cellspacing="0" width="100%">
            <col width="65%" />
            <col width="35%" />
            <tr>
                <td>
                    <div class="header" style="height:20px; padding-top:3px;padding-left:3px;border:solid 1px #9abcd7">
                        <div style="width :40%; float :left; text-align:left;"><asp:Label ID="lblYear" runat="server" style="float:left;"></asp:Label></div>
                        <div style="width :20%;text-align :left; float:left;">PO List</div>
                        <div style="float :right; text-align:left; padding-right:4px;"><asp:Label ID="lblRecCount" runat="server"></asp:Label></div>
                    </div>
                    <div class="header" style="height:20px; padding:4px ;border:solid 1px #9abcd7;text-align:left;">
                        Select Month  
                        <asp:DropDownList ID="ddlPoMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPoMonth_OnSelectedIndexChanged" Width="70px" >
                            <asp:ListItem Value="0" >< All ></asp:ListItem>
                            <asp:ListItem Value="1" >Jan</asp:ListItem>
                            <asp:ListItem Value="2" >Feb</asp:ListItem>
                            <asp:ListItem Value="3" >Mar</asp:ListItem>
                            <asp:ListItem Value="4" >Apr</asp:ListItem>
                            <asp:ListItem Value="5" >May</asp:ListItem>
                            <asp:ListItem Value="6" >Jun</asp:ListItem>
                            <asp:ListItem Value="7" >Jul</asp:ListItem>
                            <asp:ListItem Value="8" >Aug</asp:ListItem>
                            <asp:ListItem Value="9" >Sep</asp:ListItem>
                            <asp:ListItem Value="10" >Oct</asp:ListItem>
                            <asp:ListItem Value="11" >Nov</asp:ListItem>
                            <asp:ListItem Value="12" >Dec</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="height:453px;width:99%; overflow-y:scroll;overflow-x:hidden;border:solid 1px #9abcd7" >
                    <table cellpadding="0" cellspacing="0" width="98%" >
                        <asp:Repeater ID="rptCommPerAcc" runat="server">
                        <ItemTemplate>
                            <tr class="row" style="cursor:text;">
                                <td style="padding:5px;">
                                    <a  onclick="window.open('Print.aspx?POID=<%#Eval("BidID") %>')" style="cursor:pointer; text-decoration:underline; color:Red;">
                                    <span style="color:Red;font-weight:bold;" title="Click to view PO report." > <%#Eval("BidPoNum") %>  </span></a>
                                    <b>:&nbsp;</b> 
                                    <span style="color:Blue;font-weight:bold;" title='<%#Eval("AccountName") %>' > <%#Eval("AccountNumber") %>  </span>
                                    <b>:&nbsp;</b> <span style="color:Green; font-weight:bold;" > <%#Eval("CusCommentDate")%> : </span> <span style="color:Blue; font-weight:bold;" >$ <%#ProjectCommon.FormatCurrencyWithoutSign(Eval("UsDTotal"))%>  </span> 
                                    <br />
                                    <div id="DivShortComm"  runat="server" >
                                       <img  src="Images/DownArrow.jpeg" onclick="ExpandCommPerAcc(this)" style='float:right;cursor:pointer; display:<%#Eval("BtnVisiblity")%>'/>
                                       <span style="float:left;"> <%#Eval("ShortComment")%> </span>
                                    </div>
                                    <div id="DivFullComm"  runat="server"  style="display:none;">
                                        <img  src="Images/DownArrow.jpeg" onclick="CollapsCommPerAcc(this)" style="float:right;cursor:pointer;" />
                                        <span style="float:left;"> <%#Eval("comment") %> </span>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="alternaterow">
                                 <td style="padding:5px;cursor:text;" >
                                    <a  onclick="window.open('Print.aspx?POID=<%#Eval("BidID") %>')" style="cursor:pointer; text-decoration:underline; color:Red;"> 
                                    <span style="color:Red;font-weight:bold;" title="Click to view PO report." > <%#Eval("BidPoNum") %>  </span></a>
                                    
                                    <b>:&nbsp;</b> 
                                    <span style="color:Blue;font-weight:bold;" title='<%#Eval("AccountName") %>' > <%#Eval("AccountNumber") %>  </span>
                                    <b>:&nbsp;</b> <span style="color:Green; font-weight:bold;" > <%#Eval("CusCommentDate")%> : </span> <span style="color:Blue; font-weight:bold;" >$ <%#ProjectCommon.FormatCurrencyWithoutSign(Eval("UsDTotal"))%>  </span> 
                                    <br />
                                    <div id="DivShortComm"  runat="server" >
                                       <img  src="Images/DownArrow.jpeg" onclick="ExpandCommPerAcc(this)" style='float:right;cursor:pointer; display:<%#Eval("BtnVisiblity")%>'/>
                                       <span style="float:left;"> <%#Eval("ShortComment")%> </span>
                                    </div>
                                    <div id="DivFullComm"  runat="server"  style="display:none;">
                                        <img  src="Images/DownArrow.jpeg" onclick="CollapsCommPerAcc(this)" style="float:right; cursor:pointer;" />
                                        <span style="float:left;"> <%#Eval("comment") %> </span>
                                    </div>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        </asp:Repeater>
                     </table>
                    </div>
                </td>
                <td>
                    <div class="header" style="height:20px; padding-top:3px;padding-left:3px;border:solid 1px #9abcd7">
                    
                        YTD Comments 
                    </div>
                <table cellpadding="0" cellspacing="0" width="100%">
                 <tr>
                        <td >
                                <table cellspacing="0" rules="all" border="1" cellpadding="7" style="width:100%;border-collapse:collapse;">
                                 <col  width="50px" style="text-align:center;"/>
                                 <col  width="70px" style="text-align:center;"/>
                                 <col  />
                                 <col  width="120px" style="text-align:center;"/>
                                <tr class="header">
                                    <td>
                                        Action
                                    </td>
                                    <td>
                                        Month
                                    </td>
                                    <td>
                                        User Name
                                    </td>
                                    <td>
                                        Comment Date
                                    </td>
                                    
                                </tr>
                                
                                <asp:Repeater ID="rptYTDCommHistory" runat="server" >
                                    <ItemTemplate>
                                        <tr class='<%#(Common.CastAsInt32(Eval("CommentID"))==CommentID && Common.CastAsInt32(Eval("MONTH"))==SelMonth)?"selectedrow":"row"%>' >
                                            <td>
                                                <asp:ImageButton ID="btnViewComment" runat="server" ImageUrl="~/Modules/HRD/Images/Edit.png" Visible="false"  OnClick="btnViewComment_OnClick" ToolTip="View/Edit Comments"  />
                                                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Modules/HRD/Images/add.png" Visible="false"  OnClick="btnAdd_OnClick" ToolTip="Add Comments"/>
                                                <asp:ImageButton ID="imgView" runat="server" ImageUrl="~/Modules/HRD/Images/magnifier.png" Visible="false"  OnClick="imgView_OnClick" ToolTip="View Comments"/>
                                                <asp:HiddenField ID="hfCommentID" runat="server" Value='<%#Eval("CommentID")%> ' />
                                                <asp:HiddenField ID="hfComment" runat="server" Value='<%#Eval("Comment")%> ' />
                                                <asp:HiddenField ID="hfMonth" runat="server" Value=<%#ProjectCommon.GetMonthName(Eval("MONTH").ToString())%> />
                                                <asp:HiddenField ID="hfIntMonth" runat="server" Value=<%#Eval("MONTH")%> />
                                            </td>
                                            <td>
                                                <b> <span style="color:Green;"> <%#ProjectCommon.GetMonthName(Eval("MONTH").ToString())%> </span></b>
                                            </td>
                                            <td style="padding:4px;">
                                                  <b><span style="color:Red;"><%#ProjectCommon.getUserEmailByUserName(Eval("UserName").ToString())%>  </span>   
                                            </td>
                                            <td>
                                                <span style="color:Green;"><%#Eval("CommentDateFormated")%> </span> </b>    
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <%--<AlternatingItemTemplate>
                                        <tr class="alternaterow">
                                             <td style="padding:4px;">
                                                <div id="DivShortYTDComm"  runat="server" >
                                                    <img  src="Images/DownArrow.jpeg" onclick="ExpandCommPerAcc(this)" style='float:right;display:<%#Eval("btnVisiblity")%> '/>
                                                     <span style="float:left;"><b><span style="color:Green;"> <%#Eval("CommMonth")%> </span> :</b>  <%#Eval("ShortComment")%>     </span> 
                                                    
                                                </div>
                                                <div id="DivFullYTDComm"  runat="server"  style="display:none;">
                                                   <img  src="Images/DownArrow.jpeg" onclick="CollapsCommPerAcc(this)" style='float:right;display:<%#Eval("btnVisiblity")%> '/>
                                                   <span style="float:left;"> <b><span style="color:Green;"> <%#Eval("CommMonth")%> </span> :</b>  <%#Eval("Comment") %>  </span>
                                                </div>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>--%>
                                </asp:Repeater>
                            </table>
                        </td>
                        
                    </tr>
                <tr>
                 <td>
                    <table cellpadding="0" cellspacing="0" width="100%" >
                            <tr>
                                    <td style="text-align:right;padding-top:5px;">
                                        <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/Images/close.jpg" OnClientClick="window.close();"/>
                                    </td> 
                                </tr>
                                
                            </table>
                 </td>
                 </tr>
                
                </table>    
                </td>
            </tr>
            <tr>
                                <td class="header" colspan="2"  >&nbsp;</td>
                                </tr>
            </table>
            </td> 
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

