<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComponentSuppliers.aspx.cs" Inherits="ComponentSuppliers" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
         .label
         {
             font-weight:bold;
         }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div style="background-color:#4371a5;color:#2b2929;font-weight:bold;padding:5px;color:white;font-size:14px;">
            Component & Spare Suppliers - <asp:Label ID="lblSPVesselCode" runat="server"></asp:Label>
        </div>
        <div style="text-align:left">
            <table border="0" cellpadding="3
                " cellspacing="2" width="100%">
            <col width="200px" style="font-weight:bold;" />
            <col width="10px" />
            <col />
                <tr>
                <td class="label">Component Code/Name</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblSPComponentCode" style="text-align:left" runat="server" ></asp:Label> 
                        /          
                    <asp:Label  ID="lblSPComponentName" runat="server" Width="350px"></asp:Label>                                         
                    </td>                                
                </tr>
                <tr>
                    <td class="label">Maker</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblSPMaker" runat="server"  ></asp:Label>
                    </td>
                    </tr>
                <tr> 
                    <td class="label">Maker Type</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblSPMakerType" runat="server" ></asp:Label>
                                    
                    </td>
                    </tr>
                <tr> 
                        <td class="label">Account Codes</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="lblSPAccountCodes" runat="server" ></asp:Label>
                        </td>
                            </tr>
                <tr>
                    <td class="label">Description</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblSPComponentDesc" runat="server" TextMode="MultiLine"></asp:Label>                                                        
                    </td>
                    </tr>
        </table>
            <div style="text-align:center;padding:5px;">
            <asp:Button ID="btnaddVendor" runat="server" CssClass="btn" Text=" + Add New Supplier " OnClick="btnaddVendor_OnClick" Width="200px" />
                </div>
            <div >
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                            <colgroup>
                                                            <col width="50px" />  
                                                            <col   />
                                                            <col width="100px" />  
                                                            <col width="150px" />  
                                                                <col width="150px" />  
                                                            <col width="120px" />
                                                            <col width="150px" /> 
                                                            <col width="80px" />                                                             
                                                            </colgroup>
                                                                <tr align="left" class= "headerstylegrid">
                                                                <td style="text-align:center">SrNo</td>
                                                                <td style="text-align:center">Supplier / Vendor Name</td>
                                                                <td style="text-align:center">Vendor Code</td>
                                                                <td style="text-align:center">Country</td>
                                                                <td style="text-align:center">City</td>
                                                                <td style="text-align:center">Approval Type</td>
                                                                <td style="text-align:center">Delete</td>                                                                
                                                            </tr>
                                                        </table>        
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                            <colgroup>
                                                            <col width="50px" />  
                                                            <col />
                                                            <col width="100px" />  
                                                            <col width="150px" />  
                                                                <col width="150px" /> 
                                                            <col width="120px" />
                                                            <col width="150px" />  
                                                            <col width="80px" />  
                                                            
                                                            </colgroup>
                                                            <%--OnItemCommand="rptVendor_ItemCommand"--%>
                                                        <asp:Repeater ID="rptAvailableVendor" runat="server"    >
                                                            <ItemTemplate>
                                                                    <tr>
                                                                        <td align="center"> <%#Eval("srno")%>.</td>
                                                                        <td><%#Eval("SupplierName")%>
                                                                            <asp:HiddenField ID="hfSupplierID" runat="server" Value='<%#Eval("SupplierID") %>' />
                                                                            <span style="color:red;" runat="server" visible='<%#Eval("Expiring").ToString()=="Y"%>'> <i>
                                                                                ( Valid till <%#Common.ToDateString(Eval("ValidityDate"))%> ) 
                                                                            </i></span>
                                                                            <span style="color:red;font-weight:bold;" runat="server"> <i>
                                                                                <%#Eval("Blacklist")%>
                                                                            </i></span>

                                    
                                                                        </td>
                                                                        <td align="center"> <%#Eval("TravID")%> </td>
                                                                        <td><%#Eval("CountryName")%> </td>
                                                                        <td><%#Eval("City_State")%> </td>
                                                                        <td style="text-align:center"> <%#(Eval("Active").ToString()=="True")?"Yes":"No"%> </td>
                                                                        <td><%#Eval("ApprovalTypeName")%> </td>
                                                                        <td style="text-align:center">
                                                                                
                                                                            <asp:ImageButton runat="server" ID="btnDelete" OnClientClick="return window.confirm('Are you sure to delete this ?');" ImageUrl="~/Modules/PMS/Images/delete1.png" OnClick="btnDelete_Click" CommandArgument='<%#Eval("SupplierID") %>' />
                                        
                                                                        </td>
                                                                        
                                                                    </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>            
                                   
                                    </div>
            <div style="padding:5px;background-color:#e2d9d9;">
                                        &nbsp;<asp:Label ID="lblMessage" runat="server" CssClass="error_msg"></asp:Label>
                                    </div>
        </div>
        <div id="divAddSuppliers" runat="server" style="position:fixed;top:0px;left:0px; height :100%; width:100%;z-index:100;" visible="false">
                        <center>
                            <div style="position:fixed;top:0px;left:0px; height :100%; width:100%; background-color :#382f2f;z-index:100; opacity:0.7;filter:alpha(opacity=40)">
                            </div>
                            <div style="position : relative; width: 90%; padding : 0px; text-align : center; border : solid 3px #4371a5; background : white; z-index: 150; top: 50px; opacity: 1; filter: alpha(opacity=100)">
                                <center>
                                    <div class="PageHeader" style="font-size:16px;font-weight:bold;padding:8px; text-align:left;line-height:30px;">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/PMS/Images/closewindow.png" OnClick="btnCloseAddVendorPopup_OnClick" style="float:right;width:24px;" />
                                        <span style="font-size:18px;">Add Suppliers</span>
                                    </div>
                                    
                                    <div >
                                         <table cellpadding="2" cellspacing="0"  border="0" width="100%">
        <col width="100"/>
        <col width="250"/>
        <col width="80"/>
        <col width="200"/>
        <col width="150"/>
        <col width="150"/>
        <col width="110"/>
        <col />
        <col />
            <tr  style="padding:5px;">
                <td style="text-align:right"> Vendor Name :</td>
                <td style="text-align:left;">
                    <asp:TextBox ID="txtVendor" runat="server" Width="180px" Text="A" ></asp:TextBox>
                    <asp:AutoCompleteExtender ID="extVendor" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="2" ServiceMethod="GetPortTitles" ServicePath="~/WebService1.asmx" TargetControlID="txtVendor"></asp:AutoCompleteExtender>
                </td>
                <td style="text-align:right"> Port :</td>
                <td  style="text-align:left;">
                    <asp:TextBox ID="txtPort" runat="server" Width="180px" ></asp:TextBox> 
                    <asp:AutoCompleteExtender ID="extPort" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="2" ServiceMethod="GetPort" ServicePath="~/WebService1.asmx" TargetControlID="txtPort"></asp:AutoCompleteExtender>
                </td>
                <td style="text-align:right"> Approval Type :</td>
                <td style="text-align:left;">
                    <asp:DropDownList ID="ddlApprovalType" runat="server" Width="150px">
                        <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Nominated" Value="1"></asp:ListItem>
                        <asp:ListItem Text="OTA" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Owner''s Recommendation" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Maker" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align:right"> Country :</td>
                <td style="text-align:left"><asp:DropDownList ID="ddlCountry" runat="server" Width="150px"></asp:DropDownList></td>
                <td style="text-align:right"> City/State :</td>
                <td style="text-align:left"><asp:TextBox ID="txtcity" runat="server" Width="180px" ></asp:TextBox> </td>
                <td>&nbsp;</td>
                <td style="text-align:left;">
                   <asp:Button runat="server" ID="imgSearch" Text="Search" OnClick="OnClick_imgSearch" CssClass="btn" />
                </td>
            </tr>
        </table>
                                    </div>
                                    <div style="background-color:#aed6ed; vertical-align:middle; padding:5px;text-align:right">
                                        <asp:Label ID="lblTotRec" runat="server" style="font-weight:bold;float:right; padding-right:10px;" ></asp:Label>&nbsp;
                                    </div>
                                    <div style="width:100% ; OVERFLOW-Y: scroll; OVERFLOW-X:hidden ;">
                                        <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                <col width="30px" />
                                                <col width="50px" />  
                                                <col   />
                                                <col width="100px" />  
                                                <col width="150px" />  
                                                 <col width="150px" />  
                                                <col width="120px" />
                                                <col width="150px" /> 
                                                </colgroup>
                                                 <tr align="left" class= "headerstylegrid">
                                                    <td style="text-align:center"></td>
                                                    <td style="text-align:center">SrNo</td>
                                                    <td style="text-align:center">Supplier / Vendor Name</td>
                                                    <td style="text-align:center">Vendor Code</td>
                                                    <td style="text-align:center">Country</td>
                                                    <td style="text-align:center">City / State </td>
                                                    <td style="text-align:center">Active</td>
                                                    <td style="text-align:center">Approval Type</td>
                                                </tr>
                                            </table>        
                                        </div>
                                    <div style="width:100% ; height:300px; OVERFLOW-Y: scroll; OVERFLOW-X:hidden ;">            
                                           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                <col width="30px" />  
                                                <col width="50px" />  
                                                <col />
                                                <col width="100px" />  
                                                <col width="150px" />  
                                                    <col width="150px" />  
                                                <col width="120px" />
                                                <col width="150px" />  
                                                </colgroup>
                                               <%--OnItemCommand="rptVendor_ItemCommand" --%>
                                            <asp:Repeater ID="rptVendor" runat="server"   >
                                                <ItemTemplate>
                                                        <tr >
                                                            <td align="center">
                                                                <asp:CheckBox ID="chkVendor" runat="server" />                                                                
                                                            </td>
                                                            <td align="center"> <%#Eval("srno")%>.</td>
                                                            <td style="text-align:left;"><%#Eval("SupplierName")%>
                                                                <asp:HiddenField ID="hfSupplierID" runat="server" Value='<%#Eval("SupplierID") %>' />
                                                                <span style="color:red;" runat="server" visible='<%#Eval("Expiring").ToString()=="Y"%>'> <i>
                                                                    ( Valid till <%#Common.ToDateString(Eval("ValidityDate"))%> ) 
                                                                </i></span>
                                                                <span style="color:red;font-weight:bold;" runat="server"> <i>
                                                                    <%#Eval("Blacklist")%>
                                                                </i></span>                                    
                                                            </td>
                                                            <td align="center"> <%#Eval("TravID")%> </td>
                                                            <td align="left"><%#Eval("CountryName")%> </td>
                                                            <td align="left"><%#Eval("City_State")%> </td>
                                                            <td style="text-align:center"> <%#(Eval("Active").ToString()=="True")?"Yes":"No"%> </td>
                                                            <td><%#Eval("ApprovalTypeName")%> </td>                                                            
                                                        </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                    <div style="padding:5px;">
                                        <%--OnClick="btnSaveProductLL_OnClick"--%>
                                        <asp:Button ID="btnSaveVendor" runat="server"  Text="Save" OnClick="btnSaveVendor_OnClick" />
                                    </div>
                                    <div style="padding:5px;background-color:#e2d9d9;">
                                        &nbsp;<asp:Label ID="lblMsgAddVendor" runat="server" CssClass="error_msg"></asp:Label>
                                    </div>
                                  
                                </center>
                                </div>
                            </div>
    </div>
    </form>
</body>
</html>
