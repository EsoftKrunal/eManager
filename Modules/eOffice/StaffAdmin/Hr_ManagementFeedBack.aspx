<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hr_ManagementFeedBack.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Hr_ManagementFeedBack" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">

     <script language="javascript" type="text/javascript">
         function focusthis(txt) {
             var ctrid = txt.id;
             document.getElementById(ctrid).style.height = "70px";
         }
         function blurthis(txt) {
             var ctrid = txt.id;
             document.getElementById(ctrid).style.height = "30px";
         }
     </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table width="100%">
                <tr>
                   
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                              <table cellpadding="2" cellspacing="0" style="width: 100%;background-color: #f9f9f9">
                            <tr>
                                <td style="background-color: #4371a5; text-align: center; height: 23px; font-size:15px;" CssClass="text">
                                    <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                        <col width="310px" />
                                        <col />
                                        <col width="200px" />
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                              <td style="font-size:16px; font-weight:bold; color:White; text-align:center;"> 
                                            Performance Evaluation & Assessment Of Potential (PEAP)</td>
                                            <td style="font-size:10px; color:White;vertical-align:top;">
                                            <%--<asp:Button ID="btnRefresh" OnClick="btnRefresh_Click" style="display:none;" runat="server" CausesValidation="false" />--%>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                           <tr>
                                <td style="padding:10px">
                                    <span style="font-size:Large; font-weight:bold; color:#336699;">
                                [ 
                                    <asp:Label ID="txtFirstName" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label>
                                    <asp:Label ID="txtLastName" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label> &nbsp;/ <asp:Label ID="lblPeapLevel" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label>
                                &nbsp;]
                                </span>
                                    <span style="font-size:Large; font-weight:bold; color:#6600CC;">
                                ( 
                                    <asp:Label ID="txtOccasion" style="font-size:Large; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                &nbsp;)

                                </span>
                                 <span style="font-size:Large; font-weight:bold; color:#0000FF;">
                                 <asp:Label ID="lblAppraiserName" style="font-size:Large; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                 </span>
                                <span style="float:right"> 
                                    <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Modules/HRD/Images/back_button.gif" CausesValidation="false" AlternateText="Back" OnClick="btnBack_Click" />
                                </span>
                                <br /><br />
                                <div style="padding:5px; background-color:#f2f2f2;">
                                <asp:Label ID="lblPeapStatus" runat="server" CssClass="input_box" Font-Bold="True" Font-Size="Large" ForeColor="#993300"></asp:Label>
                                </div>
                                <hr />
                                </td>
                            </tr>
                            <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                      <ContentTemplate>
                                        <div style="overflow-x:hidden;overflow-y:hidden; width:100%; height:365px; float:left; padding:2px;" >
                                        <table cellpadding="2" cellspacing="2" width="100%" style="text-align:left;vertical-align:top;">
                                            <tr style="background-color:#d2d2d2;">
                                                <td  style="text-align:center; font-weight:bold; font-size:12px;">
                                                   Management Feed Back
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table cellpadding="2" cellspacing="2" border="0" width="100%" style="text-align:left; vertical-align:top;">
                                                       <tr>
                                                            <td colspan="2"> 
                                                                <br />
                                                               <b>Remarks:</b><br />
                                                                <asp:TextBox ID="txtMDRemarks"  runat="server" Width="99%" TextMode="MultiLine"  Height="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td> Name : </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMDName" runat="server" ></asp:TextBox>
                                                            </td>
                                                            <td> </td>
                                                            <td></td>
                                                        </tr>--%>
                                                        <tr>
                                                             <td style="text-align:right;">
                                                               <asp:Label ID="lblMsg" style="color:Red;"  runat="server"></asp:Label>&nbsp;
                                                                <asp:Button ID="btnSave" CssClass="btn" Text="Submit" runat="server" onclick="btnSave_Click" />
                                                             </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>       
                                    </div>
                                    </ContentTemplate>
                                     </asp:UpdatePanel>
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
