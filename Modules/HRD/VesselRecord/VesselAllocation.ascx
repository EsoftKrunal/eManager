<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselAllocation.ascx.cs" Inherits="VesselRecord_VesselAllocation" %>
<%--<script type="text/javascript">
    function ShowPrint() {
        var str;
        str = '<% Response.Write(this.VesselId.ToString()); %>';
        window.open('../Reporting/Vessel_Details_Report.aspx?VID=' + str);
    }
 </script>--%>
<link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <table cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="width: 100%">
                <div id="vessel_Allocation_div" runat="server" style="width:100%;font-family:Arial;font-size:12px;">
                    <table width="100%" cellpadding="4" cellspacing ="4" border ="1" >
                         <tr>
                         <td style="text-align:center">
                          <asp:Label ID="lblMsg" runat="server" ForeColor="#C00000"></asp:Label>
                          </td>
                         </tr>
                        <tr>
                            <td style="background-color: #e2e2e2">
                                <table cellpadding="8" cellspacing="4" width="100%" border="0" >
                                     <tr>
                                        <td style="text-align: right; padding-right: 15px; ">
                                            Vessel Email :
                                        </td>
                                        <td style="text-align: left; ">
                                            <asp:TextBox ID="txtVesselEmail" runat="server" Width="300px" MaxLength="200" CssClass="input_box" ></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; padding-right: 15px; ">
                                            
                                        </td>
                                        <td style="text-align: left; ">

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; padding-right: 15px; ">
                                            System Email :
                                        </td>
                                        <td style="text-align: left; ">
                                            <asp:TextBox ID="txtShipsoftEmail" runat="server" Width="300px" MaxLength="200" CssClass="input_box" ></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; padding-right: 15px; ">
                                            Technincal Group Email :
                                        </td>
                                        <td style="text-align: left; ">
                                            <asp:TextBox ID="txtTechGrpEmail" runat="server" Width="300px" MaxLength="200" CssClass="input_box" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; padding-right: 15px; ">
                                            Crew Group Email : 
                                        </td>
                                        <td style="text-align: left; ">
                                            <asp:TextBox ID="txtCrewGrpEmail" runat="server" Width="300px" MaxLength="200" CssClass="input_box" ></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; padding-right: 15px; ">
                                           Owner Rep :
                                        </td>
                                        <td style="text-align: left; ">
                                            <asp:TextBox ID="txtOwnerRepEmail" runat="server" CssClass="input_box" 
                                                Width="300px" MaxLength="200" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-right: 15px; text-align: right; ">
                                            Charterer Email : 
                                        </td>
                                        <td style="text-align: left; ">
                                            <asp:TextBox ID="txtChartererEmail" runat="server" Width="300px" 
                                                CssClass="input_box" MaxLength="200" ></asp:TextBox>
                                        </td>
                                        <td style="padding-right: 15px; text-align: right; ">
                                           Tech Supdt :
                                        </td>
                                        <td style="text-align: left; ">
                                            <asp:DropDownList ID="ddlTechSupdt" runat="server" Width="300px" 
                                                CssClass="input_box" ></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; padding-right: 15px; ">
                                             Marine Supdt :
                                        </td>
                                        <td colspan="" style="text-align: left">
                                            <asp:DropDownList ID="ddlMarineSupdt" runat="server" CssClass="input_box" Width="300px" ></asp:DropDownList>
                                        </td>
                                        <td style="padding-right: 15px; text-align: right; width: 147px;">
                                             Fleet Manager :</td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlFleetMgr" runat="server" Width="300px" CssClass="input_box" ></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; padding-right: 15px; ">
                                            Tech Assistant : 
                                        </td>
                                        <td colspan="" style="text-align: left">
                                            <asp:DropDownList ID="ddlTechAssistant" runat="server" Width="300px" CssClass="input_box" ></asp:DropDownList>
                                        </td>
                                        <td style="padding-right: 15px; text-align: right; width: 147px;">
                                          Marine Asstistant :</td>
                                        <td style="text-align: left">
                                          <asp:DropDownList ID="ddlMarineAssistant" runat="server" Width="300px" CssClass="input_box" ></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                      <td  style="padding-right: 15px; text-align: right; ">
                                          SPA (Sea presonal asst.) :</td>
                                      <td style="text-align: left; ">
                                           <asp:DropDownList ID="ddlSPA" runat="server" Width="300px" CssClass="input_box" ></asp:DropDownList>    
                                      </td>
                                      <td  style="padding-right: 15px; text-align: right; ">
                                          Acct Officer :</td>
                                      <td style="text-align: left; ">
                                         <asp:DropDownList ID="ddlAcctOfficer" runat="server" Width="300px" CssClass="input_box" ></asp:DropDownList>    
                                      </td>
                                  </tr>
                                    <tr>
                                      <td  style="padding-right: 15px; text-align: right; ">
                                          Applicable for performance bonus :</td>
                                      <td style="text-align: left; ">
                                          <asp:RadioButton id="r1" runat="server" Text="Yes" GroupName="YN"/>
                                          <asp:RadioButton id="r2" runat="server" Checked="true" Text="No" GroupName="YN"/>
                                      </td>
                                      <td  style="padding-right: 15px; text-align: right; ">
                                          &nbsp;</td>
                                      <td style="text-align: left; ">
                                          &nbsp;</td>
                                  </tr>
                                </table>
                            </td>
                        </tr>

                        
                         </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td  style=" padding-top :10px;" >
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn" Text="Save" Width="59px"  />
                    <%--<asp:Button ID="btn_Print_vessel_general" runat="server" CausesValidation="False" CssClass="btn" TabIndex="28" Text="Print" Width="59px" OnClientClick="javascript:ShowPrint();return false;"  Visible="true" />--%>                                                 
                </td>
            </tr>
             <%--<tr>
                <td  style="height: 4px; width: 795px;">
                </td>
            </tr>--%>
        </table>
        