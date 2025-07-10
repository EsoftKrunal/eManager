<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Revision.aspx.cs" Inherits="StaffAdmin_Compensation_Revision" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>
<%@ Register src="~/Modules/eOffice/StaffAdmin/Compensation/CB_Menu.ascx" tagname="CB_Menu" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <title>EMANAGER</title>
    <link href="../../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style>
        .rptAmout {
            border:solid 0px red;list-style:none;margin:0;padding:0;
        }
        .rptAmout li:first-child {
            border-bottom:solid 1px green;padding:4px;width:100%;
        }
        .red {
                color:orangered;padding:2px;text-align:center;font-weight:bold;
        }
        .green {
                color:forestgreen;padding:2px;text-align:center;font-weight:bold;
        }
    </style>


     <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <div style="font-family:Arial;font-size:12px;"><%--onkeydown="javascript:EnterToClick();"--%>
    
                <table width="100%">
                    <tr>
                        
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <div class="text headerband" style=" text-align :center; font-size :14px;  padding :3px; font-weight: bold;">
                            Compensation and Benifits
                        </div>
                        <div style="background-color:white;">
                            <uc2:CB_Menu ID="CBMenu" runat="server" />
                        </div>
                        <%--<asp:UpdatePanel id="UpdatePanel" runat="server" >
                        <ContentTemplate>--%>
                            <table border="0" cellspacing="0" cellpadding="2"  style="text-align: center; padding-right:50px; height :25px;" width="100%">
                                <tr>
                                    <td style="text-align :right;width:80px">
                                        Office :
                                    </td>
                                    <td style="text-align :left;width:200px;">
                                        <asp:DropDownList ID="ddlOffice" runat="server" Width="100px" ></asp:DropDownList>
                                    </td>

                                    <td style="text-align :right;width:80px">
                                        Year :
                                    </td>
                                    <td style="text-align :left">
                                        <asp:DropDownList ID="ddlYear" runat="server" Width="100px" ></asp:DropDownList>
                                    </td>

                                    <td style="text-align :right;">
                                        <asp:Button ID="btn_Search" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Search_Click" Text="Search" Width="75px" TabIndex="0" />
                                        <asp:Button ID="btn_Clear" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Clear_Click" Text="Clear" Width="75px" TabIndex="20" />
                                     
                                    </td>
                                    <td style="text-align :right;width:120px">
                                    <asp:Label ID="EmpCount" runat="server" ></asp:Label> Records.
                                    </td>
                               </tr>
                             </table>
                         
                          <%--  <table cellpadding="0" cellspacing ="0" width="100%">
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="center" colspan="6" style="padding-right: 5px; text-align: right;">
                                            <a style="float:left;font-weight:bold; margin-left :10px; " href="Emtm_HR_LeaveSummaryReport.aspx" target="_blank"></a>
                                            <strong>Total Filterd Records :&nbsp;&nbsp;</strong>
                                                
                                        </td>
                                    </tr>
                              </table>--%>
                            <div id="divTraveldocument" runat="server" style="padding:0px 5px 5px 5px;" >
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%;text-align:center; border-bottom:none;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" >
                                    
                                        <col style="width:30px;" />
                                        <col style="width:80px;" />
                                        <col />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                        <col style="width:125px;" />
                                        <col style="width:125px;" />
                                        <tr class= "headerstylegrid">
                                            <td></td>
                                            <td>Emp Code</td>
                                            <td style="text-align:left;">Employee Name</td>
                                            <td> <%= _years[0].ToString() %> </td>
                                            <td> <%= _years[1].ToString() %> </td>
                                            <td> <%= _years[2].ToString() %> </td>
                                            <td> <%= _years[3].ToString() %> </td>
                                            <td> <%= _years[4].ToString() %> </td>
                                            <td>Last Revision </td>
                                            <td>Revision Amount </td>
                                            <td>Bonus Amount </td>
                                        </tr>
                                    
                                </table>
                                </div>      
                                
                            <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 400px; text-align:center;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" class="gridrow">
                                <colgroup>
                                        <col style="width:30px;" />
                                        <col style="width:80px;" />
                                        <col />
                                        <col style="width:100px;" />
                                        <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:125px;" />
                                    <col style="width:125px;" />
                                     </colgroup>     
                           
                                <asp:Repeater ID="RptEmployee" runat="server" >
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("EmpId"))==SelectedId)?"selectedrow":"row"%>'>
                                            <td align="center">
                                                <a href="CB_Details.aspx?EmpID=<%#Eval("EmpId")%>" target="_blank" title="View" > <img src="../../../HRD/Images/HourGlass.gif" style="border:none;" /> </a>
                                            </td>
                                            <td align="left"><%#Eval("EMPCODE")%>
                                                <asp:HiddenField ID="hfEmpid" runat="server" Value='<%#Eval("Empid")%>' />
                                            </td>
                                            <td align="left"><%#Eval("EMPNAME")%></td>
                                            <td align="center">
                                                <ul class="rptAmout">
                                                    <li><%#Eval("Amount1")%></li>
                                                    <li><%#Eval("Amount1_B")%></li>
                                                </ul>
                                            </td>
                                            <td align="center">
                                                <ul class="rptAmout">
                                                    <li><%#Eval("Amount2")%></li>
                                                    <li><%#Eval("Amount2_B")%></li>
                                                </ul>
                                            </td>
                                            <td align="center">
                                                <ul class="rptAmout">
                                                    <li><%#Eval("Amount3")%></li>
                                                    <li><%#Eval("Amount3_B")%></li>
                                                </ul>
                                            </td>
                                            <td align="center">
                                                <ul class="rptAmout">
                                                    <li><%#Eval("Amount4")%></li>
                                                    <li><%#Eval("Amount4_B")%></li>
                                                </ul>
                                            </td>
                                            <td align="center">
                                                <ul class="rptAmout">
                                                    <li><%#Eval("Amount5")%></li>
                                                    <li><%#Eval("Amount5_B")%></li>
                                                </ul>
                                            </td>
                                            <td align="center"><%#Eval("RevisionDate")%></td>                                            
                                            <td align="right">                                                 
                                                     <input type="text"  value='<%#Eval("RevisionAmount")%>' class="txtRevisionAmount" empid='<%#Eval("EmpId")%>'  onkeypress="javascript:return isNumber(event)" lastamount='<%#Eval("Amount5")%>'  style="text-align:right;width:100px;float:left;"  <%=(ddlYear.SelectedIndex == 0) ? "" : "disabled"  %>/>
                                                     <img src="../../../Images/check.png" style="display:none;float:right;" class="saveimage"  />
                                                    <div style="clear:both;"></div>
                                                    <div class='<%# (( Common.CastAsInt32(Eval("TotIncrement"))>0)?"green":"red") %>'>
                                                        <span> <%#Eval("TotIncrement")%> </span>
                                                    </div>
                                                
                                             </td>
                                            <td align="right">                                                 
                                                     <input type="text"  value='<%#Eval("BonusAmount")%>' class="txtBonusAmount" empid='<%#Eval("EmpId")%>' lastbonus='<%#Eval("Amount5_B")%>'  onkeypress="javascript:return isNumber(event)" style="text-align:right;width:100px;float:left;"  <%=(ddlYear.SelectedIndex==0)?"":"disabled" %> />                                                 
                                                     <img src="../../../Images/check.png" style="display:none;float:right;" class="saveimageBonus"  />                                                 
                                                    <div style="clear:both;"></div>
                                                        <div class='<%# (( Common.CastAsInt32(Eval("TotIncrementBonus"))>0)?"green":"red") %>'>
                                                            <span> <%#Eval("TotIncrementBonus")%> </span>
                                                        </div>
                                             </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                               </table>
                            </div> 
                                </div>
                                <%--</ContentTemplate>
                         </asp:UpdatePanel>--%>
                        </td>
                    </tr>
            </table>      
  
     
    </div>
        <script src="../../../HRD/JS/jquery-1.10.2.js"></script>
        <%--<script src="/JS/jquery-1.3.2.min.js" type="text/javascript"></script>--%>
        
        <script type="text/javascript">
            $(document).ready(function () {                
                $(".txtRevisionAmount").change(function () {
                    var target = $(this);
                    
                    var empid = parseInt( $(this).attr("empid"));
                    var Amount = parseFloat($(this).val());
                    var Year = parseInt($("#ddlYear").val());
                    var Mode = "R";
                    var lastamount = parseFloat($(this).attr("lastamount"));
                    var span = target.parent().find("span");
                    SetspanForIncrement(lastamount, Amount, span);
                    
                        $.ajax({
                        type: "POST",
                        url: "Revision.aspx/UpdateAmount",                        
                        data: '{Empid: "' + empid + '",Amount:"' + Amount + '",Year:"' + Year + '",Mode:"' + Mode + '"}',
                        //data: {Empid: empid ,Amount:Amount },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        
                        success: function (response) {
                            if (response.d == 'Y') {
                                target.next("img").show();
                            }
                            else
                                alert("Error while updating record.");
                        },
                        failure: function(response) {
                            alert("Error while updating record.");
                        }
                    });


                });
            })

            function OnSuccess(elmt,res) {
                if (res.d == 'Y')
                    elmt.next().show();
                else
                    alert("Error while updating record.");
            }
            function SetspanForIncrement(lastamount, newamount, ctrl) 
            {
                if (isNaN(lastamount))
                    lastamount = 0;
                if (isNaN(newamount))
                    newamount = 0;
                var IncrementedAmount = parseFloat(newamount - lastamount).toFixed(2);
                ctrl.html(IncrementedAmount);

                if (IncrementedAmount > 0)
                    ctrl.attr("class", "green");
                else
                    ctrl.attr("class", "red");
            }

        </script>

        <script type="text/javascript">
            $(document).ready(function () {                
                $(".txtBonusAmount").change(function () {
                    var target = $(this);
                    var empid = parseInt( $(this).attr("empid"));
                    var Amount = parseFloat($(this).val());
                    var Year =  $("#ddlYear").val();
                    var Mode = "B";
                    
                    var lastbonus = parseFloat($(this).attr("lastbonus"));
                    var span = target.parent().find("span");
                    SetspanForIncrement(lastbonus, Amount, span);

                        $.ajax({
                        type: "POST",
                        url: "Revision.aspx/UpdateAmount",                        
                        data: '{Empid: "' + empid + '",Amount:"' + Amount + '",Year:"' + Year + '",Mode:"' + Mode + '"}',
                        //data: {Empid: empid ,Amount:Amount },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",                        
                        success: function (response) {
                            if (response.d == 'Y') {
                                target.next("img").show();
                            }
                            else
                                alert("Error while updating record.");
                        },
                        failure: function(response) {
                            alert("Error while updating record.");
                        }
                    });


                });
            })
        </script>

        <script>
    // WRITE THE VALIDATION SCRIPT IN THE HEAD TAG.
    function isNumber(evt) {
        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
            return false;

        return true;
    }    
</script>
            
 </asp:Content>
