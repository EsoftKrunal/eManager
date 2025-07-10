<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselParticulars.ascx.cs" Inherits="VesselRecord_VesselParticulars" %>
<script type="text/javascript" >
    function fncInputNumericValuesOnly(evnt) {
        if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
            event.returnValue = false;
        }
    }
    </script>

    <style type="text/css">
        input
        {
           border:solid 1px gray; 
        }
    </style>
 <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>

    <table style="width :100%" cellpadding="0" cellspacing="0">        
        <tr>        
        <td style=" text-align :left; vertical-align : top;" > 
                <div style="width:100%;border:solid 1px gray; text-align:left;font-size :12px;padding:3px; height:370px; overflow-y: scroll; overflow-x: hidden;">
                        <table width="100%" cellpadding="4" cellspacing="0" border="0" >
                        <tr>
                            <td style="text-align:right; width:25%;">Veseel Code :</td>
                            <td style="text-align:left;width:25%;"><asp:TextBox runat="server" ID="txtVesselCode" ReadOnly="true"   Width="50px"></asp:TextBox></td>
                            <td style="text-align:right; width:25%;">Vessel Name :</td>
                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtVesselName" ReadOnly="true"   Width="250px" MaxLength="100"></asp:TextBox></td>
                        </tr>                  
                            <tr style="background-color:#DDF2FF;">
                                <td style="text-align:right;">
                                    Vessel Type :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtVesselType"   Width="250px" MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                    Flag Name :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtFlagName"   Width="250px" MaxLength="50"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    Owner Name :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtOwnerName"   Width="250px" MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                    Management Name :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtManagementName"   Width="250px" MaxLength="100"></asp:TextBox></td>
                            </tr>
                             <tr style="background-color:#DDF2FF;">
                                <td style="text-align:right;">
                                    Owner Address :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtOwnerAddress"   Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                    Management Address :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtManagementAddress"   Width="250px" MaxLength="100"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td style="text-align:right;">
                                    Build :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtBuild"   Width="250px" MaxLength="100"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtBuild" ></ajaxToolkit:CalendarExtender>
                                    
                                </td>
                                <td style="text-align:right;">
                                    Builders :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtBuilders"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                            </tr>
                             <tr style="background-color:#DDF2FF;">
                                <td style="text-align:right;">
                                    IMO Number :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtIMONo"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                    Call Sing :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtCall_Sing"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td style="text-align:right;">
                                         Official Number :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtOfficialNo"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                 MMSI Number
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txt_MMSI_No"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                            </tr>
                             <tr style="background-color:#DDF2FF;">
                                <td style="text-align:right;">
                                     
                                         Fax Inmarsat C
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtFaxInmarsatC"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                     
                                         Phone Inmarsat FB
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txt_Phone_Inmarsat_FB"  
                                        Width="250px" MaxLength="100"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td style="text-align:right;">
                                     
                                         Class
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtClass"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                     
                                         Class NK Number
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txt_Class_NK_No"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                            </tr>
                             <tr style="background-color:#DDF2FF;">
                                <td style="text-align:right;">
                                     
                                         Length overall
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtLengthOverall"  Width="250px"  onkeypress="fncInputNumericValuesOnly(event)"
                                        MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                     
                                         Length between PP
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtLength_PP"   Width="250px" onkeypress="fncInputNumericValuesOnly(event)"
                                        MaxLength="100"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td style="text-align:right;">
                                     
                                         Breadth Moulded
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtBreadthMoulded"   onkeypress="fncInputNumericValuesOnly(event)"
                                        Width="250px" MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                     
                                         Depth Moulded
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtDepth_Moulded"   Width="250px" onkeypress="fncInputNumericValuesOnly(event)"
                                        MaxLength="100"></asp:TextBox></td>
                            </tr>
                             <tr style="background-color:#DDF2FF;">
                                <td style="text-align:right;">
                                     
                                         Draught (S)
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtDraughtS"  Width="250px" onkeypress="fncInputNumericValuesOnly(event)"
                                        MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                     
                                         Deadweight (S)
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtDead_Weight_S"   Width="250px" onkeypress="fncInputNumericValuesOnly(event)"
                                        MaxLength="100"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td style="text-align:right;">
                                     
                                         International Gross Tonnage
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtInt_Gross_Tonnage"   onkeypress="fncInputNumericValuesOnly(event)"
                                        Width="250px" MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                     
                                         International Net Tonnage
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtInt_Net_Tonnage"   onkeypress="fncInputNumericValuesOnly(event)"
                                        Width="250px" MaxLength="100"></asp:TextBox></td>
                            </tr>
                             <tr style="background-color:#DDF2FF;">
                                <td style="text-align:right;">
                                     
                                         Suez Canal Net Register Tonnage
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtSuez_Net_Reg_Tonnage"   onkeypress="fncInputNumericValuesOnly(event)" 
                                        Width="250px" MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                     
                                         Panama Canal / UMS Net Tonnage
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtPanama_Net_Tonnage"  onkeypress="fncInputNumericValuesOnly(event)" 
                                        Width="250px" MaxLength="100"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td style="text-align:right;">
                                     
                                         Light Ship
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtLightShip"   Width="250px" onkeypress="fncInputNumericValuesOnly(event)"
                                        MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                     
                                         Main Engine
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtMain_Engine"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                            </tr>
                             <tr style="background-color:#DDF2FF;">
                                <td style="text-align:right;">
                                     Tail shaft oil/water type of seals makers :</td>
                                <td style="text-align:left;">
                                   <asp:TextBox runat="server" ID="txtTail_Shaft_Oil"  Width="250px" MaxLength="100"></asp:TextBox>
                                    </td>
                                <td style="text-align:right;">                                     
                                        Trial Speed : 
                                </td>
                                <td style="text-align:left;">
                                   <asp:TextBox runat="server" ID="txtTrialSpeed"  Width="250px" onkeypress="fncInputNumericValuesOnly(event)" MaxLength="100"></asp:TextBox>
                                    </td>
                            </tr>

                            <tr>
                                <td style="text-align:right;">                                     
                                        Main Generator Engine : 
                                </td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtMain_Generator_Engine"  Width="250px" MaxLength="100"></asp:TextBox>
                                    </td>
                                <td style="text-align:right;">
                                 Main Generators : 
                                </td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtMainGenerators"   Width="250px" MaxLength="100"></asp:TextBox>
                                    </td>
                            </tr>
                             <tr style="background-color:#DDF2FF;">
                                <td style="text-align:right;">
                                Emergency Generator Engine :</td>
                                <td style="text-align:left;">
                                 <asp:TextBox runat="server" ID="txtEmer_Generator_Engine"   Width="250px" MaxLength="100"></asp:TextBox>
                                    </td>
                                <td style="text-align:right;">                                    
                                          Electrical Equipment main switch board :</td>
                                <td style="text-align:left;">
                                   <asp:TextBox runat="server" ID="txtEle_Eqp_Main_Switch_Board"  Width="250px" MaxLength="100"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr style="background-color:#FFFFCC;">
                                <td colspan="4" style="text-align:center; font-weight:bold; font-size:12px;">
                                   Propeller Specifications
                                </td>
                            </tr>
                             <tr style="background-color:#DDF2FF;">
                                <td style="text-align:right;">
                                     
                                         Propeller Size
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtPropellerSize"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                     
                                         Propeller Type
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtPropeller_Type"  
                                        Width="250px" MaxLength="100"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td style="text-align:right;">
                                     
                                         Propeller Material
                                     :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtPropellerMaterial"  
                                        Width="250px" MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                     </td>
                                <td style="text-align:left;">
                                    </td>
                            </tr>
                            
                            <tr style="background-color:#FFFFCC;">
                                <td colspan="4" style="text-align:center; font-weight:bold; font-size:12px;">
                                    Boiler Specifications&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right;">                                     
                                   Maker :
                                </td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtBoiler_Maker"  Width="250px"  
                                        MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                    Model :
                                </td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtBoiler_Model"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                            </tr>
                             <tr style="background-color:#DDF2FF;">
                                <td style="text-align:right;">
                                    Type :</td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtBoiler_Type"  Width="250px" 
                                        MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">                                    
                                         Max. Working Pressure :
                                </td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtBoiler_Max_Working_Pressure"  
                                        Width="250px" MaxLength="100"></asp:TextBox></td>
                            </tr>

                             <tr>
                                <td style="text-align:right;">                                     
                                        Normal Working Pressure  :
                                </td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtBoiler_Nor_Working_Pressure"  
                                        Width="250px" MaxLength="100"></asp:TextBox></td>
                                <td style="text-align:right;">
                                     Steam Evaporation
                                     :
                                </td>
                                <td style="text-align:left;">
                                    <asp:TextBox runat="server" ID="txtBoiler_Steam_Evaporation"  
                                        Width="250px" MaxLength="100"></asp:TextBox></td>
                            </tr>

                        </table>
                        <div style="text-align:center">
                            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;"  />
                        </div>
                </div>
        </td> 
        </tr>
        </table>


