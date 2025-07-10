<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipJobSpareRequirement.aspx.cs" Inherits="ShipJobSpareRequirement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />  
    <script src="JS/jquery_v1.10.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .bordered tr td
        {
            border:solid 1px #efeded;
            padding:5px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ckhSelectAll").click(function () {
                $(".chk_rpt input").attr('checked', $("#ckhSelectAll").is(':checked'));
            })
        })
        
    </script>
    <script type="text/javascript">
        function filter(ctl) {
            var par = $(ctl).val().toLowerCase();
            $(".listitem").each(function (i, o) {
                var txt = $(o).find(".listkey").first().html().toLowerCase();
                if (parseInt(txt.search(par)) >= 0) {
                    $(o).css('display', '');
                }
                else {
                    $(o).css('display', 'none');
                }
            });
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
       <div style="padding:8px;font-weight:bold;text-align:center;font-size:14px;"  class="text headerband">
        Spare Requirement
       </div>
        <table cellpadding="2" cellspacing="" border="0" width="100%">
           <tr>
            <td style="text-align: right; width:150px;">
                <b>Component Details :</b>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblCompName" runat="server" ></asp:Label>
            </td>
                  </tr>
             <tr>

            <td style="text-align: right">
                <b>Job Description :</b>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblShortDesc" runat="server" ></asp:Label>
            </td>
                  
                      </tr>
            <tr>
            <td style="text-align: right">
                <b> Long Description :</b>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblLongDesc" runat="server" ></asp:Label>
            </td>
        </tr>
             <tr>
            <td style="text-align: right">
                <b> Estimated Job Cost :</b>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblJobCost" runat="server"></asp:Label>
            </td>
        </tr>
            </table>
           <div><asp:Label ID="lblMSG" runat="server" ForeColor="Red" Font-Bold="true" /></div>
         <table id="tblAddSpare" runat="server"  cellpadding="2" cellspacing="0" border="0" width="100%"  style="" visible="false">
                <tr>
                 <td >
                     <div style="text-align:center;padding:5px">
                       <asp:Button ID="Button1" Text="Modify" runat="server" onclick="btnModify_Click" Width="80px" style="border:none;padding:7px;" CssClass="btn"/>
                   </div> 
                 </td>
             </tr>
                </table>
           
           <div style="height:25px;overflow-x:hidden;overflow-y:scroll;">
                    <table cellpadding="2" cellspacing="" border="0" width="100%" style=" background-color:#5b8fc9; border-collapse:collapse;color:white;font-weight:bold;" class="bordered">
                        <tr>
                            <td style="width:30px; text-align:center; vertical-align:middle;">Sr#</td>
                            <td style="width:30px; vertical-align:middle;"></td>
                            <td style=" vertical-align:middle;">Spare Details</td>
                            <td style="width:100px; vertical-align:middle;">Qty</td>
                        </tr>
                    </table>
                    </div>
                    <div style="height:400px;overflow-x:hidden;overflow-y:scroll;border-bottom:solid 1px #efeded;">
                    <table cellpadding="2" cellspacing="" border="0" width="100%" style="border-collapse:collapse" class="bordered">
                        <asp:Repeater ID="rptFiles" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:30px; text-align:center">&nbsp;<%#Eval("Sno")%></td>
                                    <td style="width:30px; text-align:center"><asp:ImageButton ID="imgDel" runat="server" Visible='<%#(tblAddSpare.Visible)%>' ImageUrl="~/Images/delete.png" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete?')" CommandArgument='<%#Eval("ComponentId").ToString()+ "$" + Eval("Office_Ship").ToString()+ "$" + Eval("SpareId").ToString()%>' Height="12px" />
                                    <td><a title="View spare details." href="Ship_AddEditSpares.aspx?CompCode= <%#ViewState["ComponentCode"].ToString()%>&&VC=<%#Eval("VesselCode")%>&&SPID=<%#Eval("SpareId")%>&&OffShip=<%#Eval("Office_Ship")%>" target="_blank"><%#Eval("SpareName")%></a></td>
                                    <td style="width:100px;">&nbsp;<%#Eval("Qty")%></td>                                    
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>

        <%-------------------------------------------------------%>

    </div>
        <%----------------------------------------------------------------------------%>
        <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;font-family:Arial;font-size:12px;" runat="server" id="divAddSpares" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:900px;padding :0px; text-align :center; border :solid 1px #948c8c; background : white; z-index:150;top:50px; opacity:1;filter:alpha(opacity=100)">
                <div style="padding:8px; font-weight:bold;text-align:center;font-size:14px;" class="text headerband">
                    Add Spares 
               </div>
                <div style="padding:3px;">
                    <div style='padding:10px 0px 10px  0px; background-color:#99DDF3'>
                        <input type="text" style='width:90%; padding:4px;' onkeyup="filter(this);" />
                    </div>
                    
                <div style="height:25px;overflow-x:hidden;overflow-y:scroll;">
                    <table cellpadding="2" cellspacing="" border="0" width="100%" style=" background-color:#5b8fc9; border-collapse:collapse;color:white;font-weight:bold;" class="bordered">
                        <tr class= "headerstylegrid">
                            <td style="width:30px; text-align:center; vertical-align:middle;">Sr#</td>
                            <td style="width:30px; vertical-align:middle;">
                                <asp:CheckBox ID="ckhSelectAll" runat="server"  />
                            </td>
                            <td style=" vertical-align:middle;">Spare Details</td>
                            <td style="width:100px; vertical-align:middle;">Qty</td>
                        </tr>
                    </table>
                    </div>
                    <div style="height:400px;overflow-x:hidden;overflow-y:scroll;border-bottom:solid 1px #efeded;">
                    <table cellpadding="2" cellspacing="" border="0" width="100%" style="border-collapse:collapse" class="bordered">
                        <asp:Repeater ID="rptSpeareDetails_popup" runat="server">
                            <ItemTemplate>
                                <tr class='listitem'>
                                    <td style="width:30px; text-align:center">&nbsp;<%#Eval("Sno")%></td>
                                    <td style="width:30px; text-align:center">
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="chk_rpt" Checked='<%#Eval("qty").ToString()!="" %>' />
                                        <asp:HiddenField ID="hfdPKID" runat="server" Value='<%#Eval("PKID") %>' />
                                        </td>
                                    <td style="text-align:left;">
                                        <a  class='listkey' title="View spare details." href="Ship_AddEditSpares.aspx?CompCode= <%#ViewState["ComponentId"].ToString()%>&&VC=<%#Eval("VesselCode")%>&&SPID=<%#Eval("SpareId")%>&&OffShip=<%#Eval("Office_Ship")%>" target="_blank"><%#Eval("SpareName")%></a>

                                    </td>
                                    <td style="width:100px;">&nbsp; <asp:TextBox ID="txtQty" runat="server" style="width:50px;" Text='<%#Eval("qty") %>' onkeypress="fncInputNumericValuesOnly(event)"></asp:TextBox> </td>                                    
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>

                <table cellpadding="4" cellspacing="3" width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsgAddSpares" runat="server" style="color:red;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                                <asp:Button ID="btnAssign_PopupAddSpares" runat="server" CssClass="btn" Text="Assign" OnClick="btnAssign_PopupAddSpares_OnClick" />
                                <asp:Button ID="btnClose_PopupAddSpares" runat="server" CssClass="btn" Text="Close" OnClick="btnClose_PopupAddSpares_OnClick" />                                
                            </td>
                        </tr>
                </table>
                </div>
             </div> 
            </center>
         </div>
    </form>
</body>
</html>
