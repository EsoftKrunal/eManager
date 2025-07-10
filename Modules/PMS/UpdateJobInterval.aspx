<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateJobInterval.aspx.cs" Inherits="UpdateJobInterval" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <script src="JS/jquery_v1.10.2.min.js"></script>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    
    <%--<script src="JS/Common.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".selectallvessel").click(function () {
                $(".vesselcss input").prop('checked', $(".selectallvessel input").prop('checked'));
            })
        })
    </script>
    <style type="text/css">
        .bordered tr td{
            border:solid 1px #f5f5f5;
        }
        .cls_A{
            color:green;
            font-weight:bold;
        }
        .cls_I{
            color:red;
            font-weight:bold;
        }
    </style>
</head>
<body style="margin-top:117px;margin-bottom:40px;">
    <form id="form1" runat="server">    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

        <div id="TopDiv" style="position:fixed;width:100%;height:130px; left:0px;top:0px;background-color:#598fe5;">
            <div id="title" style="text-align :center; text-align:center; color:white;font-size:16px;">
                <div style="margin:10px;">
                Copy Job Details to Other Vessels
                    </div>
            </div>
            <div style="padding:8px; background-color:#e7f1ff;color:#333; font-size:15px;text-align:left">                    
                        <table width="100%" border="0" cellpadding="2">
                        <tr>
                            <td style="width:150px;text-align:right"> Job Category :</td>
                            <td><asp:Label ID="lblJobCat" Font-Bold="true" runat="server" ></asp:Label></td>
                            <td style="width:150px;text-align:right"> Job Name :</td>
                            <td><asp:Label ID="lblJobName" Font-Bold="true" runat="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align:right"> Department :</td>
                            <td> <asp:Label ID="lblDepartment" runat="server" Font-Bold="true" ></asp:Label></td>
                            <td style="width:150px;text-align:right">Rank :</td>
                            <td>
                                <asp:Label ID="lblRank" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </div>
            <div id="dvScroll"  onscroll="SetScrollPos(this)" class="scrollbox" >                        
                    <table cellspacing="0" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;" >
                            <colgroup>
                                 <col style="text-align :left" width="60px" />                                 
                                    <col style="text-align :left" />
                                    <col style="text-align :left" width="80px" />                                                            
                                    <col style="text-align :left" width="100px" />                               
                                    <col style="text-align :left" width="80px" />                                
                                    <col style="text-align :left" width="80px" />                                                            
                            </colgroup>
                            <tr class="gridheader" style="background-color:#598fe5;font-size:12px;">
                                    <td><asp:CheckBox ID="chkvessl" runat="server" CssClass="selectallvessel" /> </td>
                                    <td style="text-align:left">Vessel</td>
                                    <td>Job Cost</td>
                                    <td>Assigned Rank</td>
                                    <td>Interval</td>                                            
                                    <td>Status</td>                                                                        
                                </tr>
                        </table>
                    </div>
        </div>
        
        <%-----------------------------------%>                
        <div id="dvScroll" onscroll="SetScrollPos(this)" style="margin-top:135px; margin-bottom:50px;" class="scrollbox" >                        
                <table cellspacing="0" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;" class="bordered">
                    <colgroup>                                     
                        <col style="text-align :left" width="60px" />                                 
                        <col style="text-align :left" />
                        <col style="text-align :left" width="80px" />                                                            
                        <col style="text-align :left" width="100px" />                               
                        <col style="text-align :left" width="80px" />                                
                        <col style="text-align :left" width="80px" />                                
                    </colgroup>                            
                        <asp:Repeater ID="rptVessels" runat="server">
                    <ItemTemplate>
                        <tr >
                            <td style="text-align:center;">
                                <asp:CheckBox ID="chkvessl" runat="server" CssClass="vesselcss" />
                            </td>
                            <td style="text-align:left;"><%#Eval("VesselName") %>
                                <asp:HiddenField ID="hfdVesselID" runat="server" Value='<%#Eval("VESSELID") %>' />
                            </td>                  
                            <td style="text-align:right"><%#Eval("JobCost") %></td>
                            <td><%#Eval("AssignedRank") %></td>
                            <td><%#Eval("Interval") %></td>
                            <td style="text-align:center" class='cls_<%#Eval("Status")%>'><%#(Eval("Status").ToString()=="A")?"Active":((Eval("Status").ToString()=="I")?"InActive":"")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>                                 
                    </table>
          </div>                            
        <%-----------------------------------%>                        
        <div id="bottomDiv" style="position:fixed;width:100%;left:0px;bottom:0px;background-color:#ffef92; text-align:right;">
            <div style="margin:10px;">
            <asp:Label ID="lblMsg" runat="server" style="color:red; float:left;font-size:15px;" Font-Bold="true"></asp:Label>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_OnClick" style="background-color:#598fe5;color:white;padding:5px 15px 5px 15px;border:0px;" />
            </div>
        </div>     
    </form>
</body>
</html>
