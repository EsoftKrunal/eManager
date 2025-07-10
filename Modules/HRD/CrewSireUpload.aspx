<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewSireUpload.aspx.cs" Inherits="CrewSireUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        input[type=text]
        {
        	width:80px;
        	text-align :center ;
        }
        textarea
        {
        	text-align :left ;
        }
        select
        {
        	font-size:12px; 
        	width:200px; 
        }
        *
        {
        	font-size:12px; 
        	font-family :Verdana; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:1000px;">
            <col/>
            <col style=" text-align:left"  />
            <col/>
            <tr>
                <td style="text-align: right">
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    Vessel :</td>
                <td >
                    <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="true"  onselectedindexchanged="ddlVessel_SelectedIndexChanged" Width="411px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    IMO # : </td>
                <td><asp:Label runat="server" id="lblImo"></asp:Label>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    Crew Matrix :</td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Text="Officer" Value="Officer"></asp:ListItem>
                        <asp:ListItem Text="Engineer" Value="Engineer">Engineer</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" valign="top" style=" border :solid 1px gray" >
                    <div style="width:516px; float :left; " >
                    <div style =" height :300px;">
                    <table cellpadding="2" cellspacing="0" width="516px" style ="background-color : #4371a5; color :White">
                <col width="16px"/>
                <col width="50px"/>
                <col width="290px"/>
                <col width="80px"/>
                <col width="80px"/>
                <tr>
                <td>&nbsp;</td>
                <td>Emp#</td>
                <td>Name</td>
                <td>Rank</td>
                <td>Nationality</td>
                </tr>
                </table>
                    <asp:GridView BorderWidth="1" ShowHeader="false" BorderColor="Black" ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false" GridLines="Horizontal" CellPadding="2" OnSelectedIndexChanged="SelectIndexChanged"  >
                    <Columns>
                        <asp:CommandField ButtonType="Image" ShowSelectButton="true"  SelectImageUrl="~/Modules/HRD/Images/favicon.png" ItemStyle-Width="16px"/>    
                        <asp:BoundField DataField="CrewNumber" HeaderText="Emp#" ItemStyle-Width="50px"/>
                        <asp:BoundField DataField="CrewName" HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="290px"/>
                        <asp:BoundField DataField="Rank" HeaderText="Rank" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80px"/>
                        <asp:TemplateField  ItemStyle-Width="80px">
                        <ItemTemplate>
                           <%#Eval("Nationality")%> 
                            <asp:HiddenField runat="server" ID="hfdcrewid" Value='<%# Eval("crewid")%>'/>
                            <asp:HiddenField runat="server" ID="hfdcrewnumber" Value='<%# Eval("crewnumber")%>'/>
                            <asp:HiddenField runat="server" ID="hfdcrewname" Value='<%# Eval("crewname")%>'/>
                            <asp:HiddenField runat="server" ID="hfdrank" Value='<%# Eval("rank")%>'/>
                            <asp:HiddenField runat="server" ID="hfdnationality" Value='<%# Eval("nationality")%>'/>
                            <asp:HiddenField runat="server" ID="hfdcertcomp" Value='<%# Eval("certcomp")%>'/>
                            <asp:HiddenField runat="server" ID="hfdissuingcountry" Value='<%# Eval("issuingcountry")%>'/>
                            <asp:HiddenField runat="server" ID="hfdadminaccept" Value='<%# Eval("adminaccept")%>'/>
                            <asp:HiddenField runat="server" ID="hfdtankercert" Value='<%# Eval("tankercert")%>'/>
                            <asp:HiddenField runat="server" ID="hfdstcw" Value='<%# Eval("stcw")%>'/>
                            <asp:HiddenField runat="server" ID="hfdradioqual" Value='<%# Eval("radioqual")%>'/>
                            <asp:HiddenField runat="server" ID="hfdoperatorexp" Value='<%# Eval("operatorexp")%>'/>
                            <asp:HiddenField runat="server" ID="hfdrankexp" Value='<%# Eval("rankexp")%>'/>
                            <asp:HiddenField runat="server" ID="hfdtankertypeexp" Value='<%# Eval("tankertypeexp")%>'/>
                            <asp:HiddenField runat="server" ID="hfdalltypeexp" Value='<%# Eval("alltypeexp")%>'/>
                            <asp:HiddenField runat="server" ID="hfdmonthtour" Value='<%# Eval("monthtour")%>'/>
                            <asp:HiddenField runat="server" ID="hfdsignondate" Value='<%# Eval("signondate")%>'/>
                            <asp:HiddenField runat="server" ID="hfdengprof" Value='<%# Eval("engprof")%>'/>
                        </ItemTemplate> 
                        </asp:TemplateField> 
                    </Columns> 
                    <HeaderStyle BackColor="#C2C2C2" ForeColor="Black"/>
                    <SelectedRowStyle BackColor="#C2C2C2" /> 
                    <AlternatingRowStyle BackColor ="White" />  
                    </asp:GridView>
                    </div>
                    <table cellpadding ="0" cellspacing ="0" width="100%" > 
                    <tr><td>
                    <asp:TextBox runat="server" ID="txtComment" Width="98%" Height="100px" ></asp:TextBox> 
                    </td></tr>
                    <tr><td>
                        <asp:Button ID="Button2" runat="server" 
                            style=" background-color : #e2e2e2; border:solid 1px #c2c2c2; height:30px " 
                            Text="Upload Comments" />
                    </td></tr>
                    </table>
                    </div>
                    <div style="float :right;color :#003366  " >
                    <table cellpadding="4">
                    <tr><td>Certificate of competency</td>
                    <td>
                        <asp:HiddenField runat="server" ID="hfdcrewid" />
                        <asp:HiddenField runat="server" ID="hfdrank" />
                        <asp:HiddenField runat="server" ID="hfdnationality" />
                        
                        <asp:DropDownList style=" float :left" runat="server" ID="ddlCertComp">
                            <asp:ListItem Text="" Value=""></asp:ListItem>  
                            <asp:ListItem Text="Class 1" Value="Class 1"></asp:ListItem>  
                            <asp:ListItem Text="Class 2" Value="Class 2"></asp:ListItem>  
                            <asp:ListItem Text="OOW" Value="OOW"></asp:ListItem>  
                            <asp:ListItem Text="EOOW" Value="EOOW"></asp:ListItem>  
                        </asp:DropDownList>                
                                <img src="Images/help1.png" style=" clear:both ;float :right" alt="Help"/> 

                    </td></tr>
                    <tr><td>Issuing country</td><td>
                    <asp:DropDownList runat="server" ID="ddlIssuingCountry" ></asp:DropDownList> 
                    </td></tr>
                    <tr><td>Adminstration aceprance</td>
                    <td>
                    <asp:DropDownList runat="server" ID="ddlAdminAccept">
                            <asp:ListItem Text="" Value=""></asp:ListItem>  
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>  
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>  
                            <asp:ListItem Text="Applied For" Value="Applied For"></asp:ListItem> 
                        </asp:DropDownList> 
                    </td></tr>
                    <tr><td>Tanker certification</td><td>
                    <asp:DropDownList style=" float :left" runat="server" ID="ddlTankerCert">
                            <asp:ListItem Text="" Value=""></asp:ListItem>  
                            <asp:ListItem Text="Oil" Value="Oil"></asp:ListItem>  
                            <asp:ListItem Text="Chemical" Value="Chemical"></asp:ListItem>  
                            <asp:ListItem Text="Gas" Value="Gas"></asp:ListItem> 
                            <asp:ListItem Text="Oil and Gas" Value="Oil and Gas"></asp:ListItem>  
                            <asp:ListItem Text="Oil and Chemical" Value="Oil and Chemical"></asp:ListItem>  
                            <asp:ListItem Text="Gas and Chemical" Value="Gas and Chemical"></asp:ListItem> 
                            <asp:ListItem Text="Oil, Chemical and Gas" Value="Oil, Chemical and Gas"></asp:ListItem>  
                            <asp:ListItem Text="None" Value="None"></asp:ListItem> 
                        </asp:DropDownList>
                    </td></tr>
                    <tr><td>STCW V Para</td><td>
                     <asp:DropDownList runat="server" ID="ddlSTCW">
                            <asp:ListItem Text="" Value=""></asp:ListItem>  
                            <asp:ListItem Text="Para 1" Value="Para 1"></asp:ListItem>  
                            <asp:ListItem Text="Para 2" Value="Para 2"></asp:ListItem>  
                            <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                        </asp:DropDownList> 
                    </td></tr>
                    <tr><td>Radio qualifiation</td><td>
                     <asp:DropDownList runat="server" ID="ddlRadio">
                            <asp:ListItem Text="" Value=""></asp:ListItem>  
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>  
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>  
                            <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                        </asp:DropDownList>
                        </td></tr>
                    <tr>
                        <td>Years with operator</td>
                        <td><asp:TextBox runat="server" ID="txtOpertor"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Years  in rank</td>
                        <td><asp:TextBox runat="server" ID="txtRank"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Years in this typr of tanker</td>
                        <td><asp:TextBox runat="server" ID="txtTanker"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Years on all type ofa tankere</td>
                        <td><asp:TextBox runat="server" ID="txtAllTanker"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Date joined vesel</td>
                        <td><asp:TextBox runat="server" ID="txtJoinVessel" ReadOnly="true" ></asp:TextBox>
                            <asp:Label runat="server" ID="lblMonth" ></asp:Label></td>
                    </tr>
                    <tr>
                        <td>English proeficitncy</td>
                        <td><asp:DropDownList runat="server" ID="ddlEngProf">
                            <asp:ListItem Text="Fair" Value="Fair"></asp:ListItem>  
                            <asp:ListItem Text="Poor" Value="Poor"></asp:ListItem> 
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                                style=" background-color : #e2e2e2; border:solid 1px #c2c2c2; height:30px " 
                                onclick="Upload_Click"  />
                        </td>
                    </tr>
                    </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
