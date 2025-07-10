<%@ Page Language="C#" MasterPageFile="~/Modules/LPSQE/Vetting/VettingMasterPage.master" AutoEventWireup="true" CodeFile="VetttingPlannerReport.aspx.cs" Inherits="Vettting_VetttingPlannerReport" Title="Untitled Page"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
	td
    {
     word-break: break-all ;
    }
	.alterrow
	{
	    background-color:#FAF5E6;
	}
	</style>
	<script type="text/javascript">
	    function AddInspection(params) {
	        //this.parent.AddInspection(params);
	        document.getElementById("ctl00_ContentPlaceHolder1_hid1").setAttribute('value', params);
	        document.getElementById("ctl00_ContentPlaceHolder1_AddInspection").click();
	    }
	    function EditInspection(inspid) {
	        //this.parent.EditInspection(inspid);
	        document.getElementById("ctl00_ContentPlaceHolder1_hid1").setAttribute('value', inspid);
	        document.getElementById("ctl00_ContentPlaceHolder1_EditInspection").click();
	    }

	    function ConfirmCancel(inspno) {
	        if (window.confirm('Are you sure to cancel Inspection no : ' + inspno)) {
	            //this.parent.CancelInspection(inspno);
	            document.getElementById("ctl00_ContentPlaceHolder1_hid1").setAttribute('value', inspno);
	            document.getElementById("ctl00_ContentPlaceHolder1_CancelInspection").click();
	            return false;
	        }
	        else {
	            return false;
	        }
	    }

	    function Open_Rating(crewid) {

	        document.getElementById("hfd_Args").setAttribute("value", crewid);
	        document.getElementById("Button3").click();
	    }
	</script>
    <div>
    <div style="display:none">
    <asp:LinkButton id="AddInspection" runat="server" Text="Add Inspection" OnClick="AddInspection_Click"></asp:LinkButton>
    <asp:LinkButton id="EditInspection" runat="server" Text="Edit Inspection" OnClick="EditInspection_Click"></asp:LinkButton>
    <asp:LinkButton id="CancelInspection" runat="server" Text="Cancel Inspection" OnClick="CancelInspection_Click"></asp:LinkButton>
    
    <asp:HiddenField runat="server" ID="hid1" />
    </div>
    <table cellpadding="0" cellspacing="0" width="100%" rules="all" border="0"  style="border:solid 1px #bbbbbb;">
        <tr>
            <td style="text-align:right; padding:3px; background-color:#e2e2e2;">
             <table cellpadding="0" cellspacing="0" width="100%">
                <td style="text-align:left">
                    <asp:Label runat="server" ID="lblCount" style="float:left" Font-Bold="true" ></asp:Label>
                </td>
                <td style="text-align:right; width:200px;">
                  <asp:Button runat="server" ID="Button1" OnClick='btnShowFilter_Click' Text="Show Filter" CssClass="Btn1" OnClientClick="DisableMe(this)" />
                   <asp:Button runat="server" ID="Button2" OnClick='btnPrint_Click' Text="Print" CssClass="Btn1" />
                   <asp:Button runat="server" ID="Button3" OnClick='btnShowRating_Click' Text="Show Rating" CssClass="Btn1" style='display:none' />
                   <asp:HiddenField runat="server" ID="hfd_Args" />
                </td>
                <td style="text-align: center; width:20px;"><asp:ImageButton runat="server" ID="btnHome" ImageUrl="~/Images/home.png" PostBackUrl="~/Vetting/VIQHome.aspx" CausesValidation="false"/> </td>
               </table>
            </td>
            </tr>
        <tr>
            <td>
                <div style="oveflow-y:scroll;oveflow-x::hidden; WIDTH: 100%; HEIGHT: 50px; text-align:center; font-weight:bold; background-color:Gray;">
                <table cellpadding="1" cellspacing="0" width="100%" border="1" style="border-collapse:collapse; height:50px;" > 
                    <tr >
                        <td>&nbsp;</td>
                        <td colspan="2" style="text-align :center ;background-color:LightYellow" >Current Status</td>
                        <td colspan="4" style="text-align :center ;background-color:Lavender" >Planning</td>
                        <td colspan="2" style="text-align :center ;background-color:LightGray" >Responsibility</td>
                        <td colspan="3" style="text-align :center ;background-color:LightCyan" >FollowUp</td>
                        <td></td>
                    </tr>
                    <tr >
                        <td style="text-align:center; color:White; width:65px;">Vessel</td>
                        <td style="text-align:center;background-color :LightYellow; width:70px;">SIRE/CDI </td>
                        <td style="text-align:center;background-color :LightYellow; width:85px;">Expiry Dt.</td>
                        <td style="text-align:center;background-color :Lavender; width:60px;">Action</td>
                        <td style="text-align:center;background-color :Lavender; width:90px;">Inspn</td>
                        <td style="text-align:left;background-color :Lavender;">Port </td>
                        <td style="text-align:center;background-color :Lavender; width:85px;">Plan Dt.</td>
                        <td style="text-align:center;background-color :LightGray; width:120px;">Office</td>
                        <td style="text-align:center;background-color :LightGray; width:120px;">Travel</td>
                        <td style="text-align:center;background-color :LightCyan; width:80px;">Done Dt.</td>
                        <td style="text-align:center;background-color :LightCyan; width:100px;">Resp Due Dt.</td> 
                        <td style="text-align:center;background-color :LightCyan; width:70px;">Resp.Up.</td>
                        <td style="width:17px"></td>                                   
                    </tr>
                </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 410px; text-align:center; border:solid 1px black;">
                <table cellpadding="1" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;"> 
                    <asp:Repeater ID="rptVettingPlanner" runat="server" >
                       <ItemTemplate>
                            <tr>
                                <td style=" text-align :center;background-color:#e2e2e2; font-weight:bold; width:65px;" title='<%#Eval("VNAME1")%>' ><%#Eval("VESSELNAME")%></td>
                                <td style=" text-align :center;background-color:#e2e2e2; font-weight:bold; width:70px;"><span style="padding-top:5px;"><%#Eval("SIRE_CDI")%></span></td>
                                <td style=" text-align :center;background-color:<%#Eval("OD")%>;width:85px;"><span style="padding-top:5px"><%#Eval("NEXTDUE")%></span></td>

                                <td style="text-align :left; background-color:#FFFFE0; width:60px;">
                                    <img src="../Images/addx12.jpg" style='<%#DoAction(1,"A",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT"))%>' onclick='<%#"AddInspection(\"" + Eval("VESSELID").ToString() + "_" + Eval("SIRE_CDI").ToString() + "\");"%>' title='Plan new inspection' />
                                    <img src="../Images/editx12.jpg" style='<%#DoAction(1,"E",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT"))%>' onclick='<%#"EditInspection(\"" + Eval("NEXTINSPID").ToString() + "\");"%>' title='Change Planning'/>
                                    <img src="../Images/deletex12.gif"  style='<%#DoAction(1,"D",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT"))%>' title='Cancel Planning'  onclick='return ConfirmCancel(<%# "\"" + Eval("NEXT_INSPNO") + "\"" %>)' />
                                </td>

                                <td style="text-align :left; background-color:#FFFFE0; width:90px;"><span style="padding-top:5px;"><%#Eval("INSPECTOR")%></span></td>
                                <td style="text-align :left; background-color:#FFFFE0"><span style="padding-top:5px;padding-left:2px;"><%#Eval("PORT")%></span></td>
                                <td style="text-align :center; background-color:#FFFFE0; width:85px;"><span style="padding-top:5px;"><%#Eval("PLANDATE")%></span></td>
                                <td style="text-align :left; background-color:#FFFFE0; width:120px;"><span style="padding-top:5px;"><%#Eval("REMOTE")%></span></td>
                                <td style="text-align :left; background-color:#FFFFE0; width:120px;"><span style="padding-top:5px;"><%#Eval("ATTEND")%></span></td>
                                <td style="text-align :center; background-color:#FFFFE0; width:80px;"><span style="padding-top:5px;"><%#Eval("DONEDT")%></span></td>
                                <td style="text-align :center; background-color:#FFFFE0; width:100px;"><span style="padding-top:5px;"><%#Eval("RESPONSEDUEDATE")%></span></td>
                                <td style="text-align :center; background-color:#FFFFE0; width:70px;"><span style="padding-top:5px;"><%#Eval("MASTERSRESP")%></span></td>
                                <td style="width:17px"></td>       
                            </tr>
                            <tr>
                                <td colspan="2" style="display:inline" >
                                    <center><a href='../RiskMatrix.aspx?VesselId=<%#Eval("VESSELID")%>&INSPID=<%#Eval("NEXTINSPID") %>' style='color:Blue' target="_blank">Risk Matrix</a></center>
                                </td>
                                <td style="text-align :center;background-color:<%#Eval("OD")%>"></td>
                                <td colspan="9" style='background-color:#FFFFE0'><span style="color:Red; font-style:italic;"><%#Eval("REMARK1")%></span></td>
                                <td></td>
                            </tr>   
                            <tr>
                                <td colspan="2"></td>
                                <td style="text-align :center;background-color:<%#Eval("OD")%>"></td>
                                
                                <td style="text-align :left;background-color:#FAFAF1;">
                                    <img src="../Images/addx12.jpg" style='<%#DoAction(2,"A",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT1"))%>' onclick='<%#"AddInspection(\"" + Eval("VESSELID").ToString() + "_" + Eval("SIRE_CDI").ToString() + "\");"%>' title='Plan new inspection' />
                                    <img src="../Images/editx12.jpg" style='<%#DoAction(2,"E",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT1"))%>' onclick='<%#"EditInspection(\"" + Eval("NEXT_NEXT_INSPID").ToString() + "\");"%>'  title='Change Planning'/>
                                    <img src="../Images/deletex12.gif"  style='<%#DoAction(2,"D",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT1"))%>' title='Cancel Planning'  onclick='ConfirmCancel(<%#"\"" + Eval("NEXT_NEXT_INSPNO") + "\""%>)' />
                                </td>
                                <td style="text-align :left; background-color:#FAFAF1;"><span style="padding-top:5px;"><%#Eval("INSPECTOR1")%></span></td>
                                <td style="text-align :left; background-color:#FAFAF1;"><span style="padding-top:5px;padding-left:2px;"><%#Eval("PORT1")%></span></td>
                                <td style="text-align :center; background-color:#FAFAF1;"><span style="padding-top:5px;"><%#Eval("PLANDATE1")%></span></td>
                                <td style="text-align :left; background-color:#FAFAF1;"><span style="padding-top:5px;"><%#Eval("REMOTE1")%></span></td>
                                <td style="text-align :left; background-color:#FAFAF1;"><span style="padding-top:5px;"><%#Eval("ATTEND1")%></span></td>
                                <td style="background-color:#FAFAF1;text-align :center;"><span style="padding-top:5px;"><%#Eval("DONEDT1")%></span></td>
                                <td style="background-color:#FAFAF1;text-align :center;"><span style="padding-top:5px;"><%#Eval("RESPONSEDUEDATE1")%></span></td>
                                <td style="background-color:#FAFAF1;text-align :center;"><span style="padding-top:5px;"><%#Eval("MASTERSRESP1")%></span></td>
                                <td style='background-color:#FAFAF1;'>&nbsp;</td>
                            </tr>  
                            <tr>
                                <td colspan="2" style='border-bottom:solid 1px gray;' ></td>
                                <td style="border-bottom:solid 1px gray;text-align :center;background-color:<%#Eval("OD")%>"></td>
                                <td colspan="8" style='border-bottom:solid 1px gray;background-color:#FAFAF1;' >
                                    <span style="color:Red; font-style:italic; background-color:#FAFAF1;"><%#Eval("REMARK")%></span>
                                </td>
                                <td style='border-bottom:solid 1px gray;background-color:#FAFAF1;'></td>
                            </tr>       
                       </ItemTemplate>
                       <AlternatingItemTemplate>
                            <tr class='alterrow'>
                                <td style=" text-align :center;background-color:#e2e2e2; font-weight:bold;" title='<%#Eval("VNAME1")%>' ><%#Eval("VESSELNAME")%></td>
                                <td style=" text-align :center;background-color:#e2e2e2; font-weight:bold;"><span style="padding-top:5px;"><%#Eval("SIRE_CDI")%></span></td>
                                <td style=" text-align :center;background-color:<%#Eval("OD")%>"><span style="padding-top:5px"><%#Eval("NEXTDUE")%></span></td>

                                <td style="text-align :left;">
                                    <img src="../Images/addx12.jpg" style='<%#DoAction(1,"A",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT"))%>' onclick='<%#"AddInspection(\"" + Eval("VESSELID").ToString() + "_" + Eval("SIRE_CDI").ToString() + "\");"%>' title='Plan new inspection' />
                                    <img src="../Images/editx12.jpg" style='<%#DoAction(1,"E",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT"))%>' onclick='<%#"EditInspection(\"" + Eval("NEXTINSPID").ToString() + "\");"%>'  title='Change Planning'/>
                                    <img src="../Images/deletex12.gif"  style='<%#DoAction(1,"D",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT"))%>' title='Cancel Planning' onclick='ConfirmCancel(<%#"\"" + Eval("NEXT_INSPNO") + "\""%>)'/>
                                </td>

                                <td style="text-align :left;"><span style="padding-top:5px;"><%#Eval("INSPECTOR")%></span></td>
                                <td style="text-align :left;"><span style="padding-top:5px;padding-left:2px;"><%#Eval("PORT")%></span></td>
                                <td style="text-align :center;"><span style="padding-top:5px;"><%#Eval("PLANDATE")%></span></td>
                                <td style="text-align :left;"><span style="padding-top:5px;"><%#Eval("REMOTE")%></span></td>
                                <td style="text-align :left;"><span style="padding-top:5px;"><%#Eval("ATTEND")%></span></td>
                                <td style="text-align :center;"><span style="padding-top:5px;"><%#Eval("DONEDT")%></span></td>
                                <td style="text-align :center;"><span style="padding-top:5px;"><%#Eval("RESPONSEDUEDATE")%></span></td>
                                <td style="text-align :center;"><span style="padding-top:5px;"><%#Eval("MASTERSRESP")%></span></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr class='alterrow'>
                                <td colspan="2" style="display:inline" >
                                    <center><a href='../RiskMatrix.aspx?VesselId=<%#Eval("VESSELID")%>&INSPID=<%#Eval("NEXTINSPID") %>' style='color:Blue' target="_blank">Risk Matrix</a></center>
                                </td>
                                <td style="text-align :center;background-color:<%#Eval("OD")%>"></td>
                                <td colspan="9" ><span style="color:Red; font-style:italic;"><%#Eval("REMARK1")%></span></td>
                                <td></td>
                            </tr>   
                            <tr class='alterrow'>
                                <td colspan="2"></td>
                                <td style="text-align :left;background-color:<%#Eval("OD")%>"></td>
                                
                                <td style="text-align :left;">
                                    <img src="../Images/addx12.jpg" style='<%#DoAction(2,"A",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT1"))%>' onclick='<%#"AddInspection(\"" + Eval("VESSELID").ToString() + "_" + Eval("SIRE_CDI").ToString() + "\");"%>' title='Plan new inspection' />
                                    <img src="../Images/editx12.jpg" style='<%#DoAction(2,"E",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT1"))%>' onclick='<%#"EditInspection(\"" + Eval("NEXT_NEXT_INSPID").ToString() + "\");"%>'  title='Change Planning'/>
                                    <img src="../Images/deletex12.gif"  style='<%#DoAction(2,"D",Eval("NEXTINSPID"),Eval("NEXT_NEXT_INSPID"),Eval("DONEDT1"))%>' title='Cancel Planning' onclick='ConfirmCancel(<%#"\"" + Eval("NEXT_NEXT_INSPNO") + "\""%>)' />
                                </td>

                                <td style="text-align :left;"><span style="padding-top:5px;"><%#Eval("INSPECTOR1")%></span></td>
                                <td style="text-align :left;"><span style="padding-top:5px;padding-left:2px;"><%#Eval("PORT1")%></span></td>
                                <td style="text-align :center;"><span style="padding-top:5px;"><%#Eval("PLANDATE1")%></span></td>
                                <td style="text-align :left;"><span style="padding-top:5px;"><%#Eval("REMOTE1")%></span></td>
                                <td style="text-align :left;"><span style="padding-top:5px;"><%#Eval("ATTEND1")%></span></td>
                                <td style="text-align :center;"><span style="padding-top:5px;"><%#Eval("DONEDT1")%></span></td>
                                <td style="text-align :center;"><span style="padding-top:5px;"><%#Eval("RESPONSEDUEDATE1")%></span></td>
                                <td style="text-align :center;"><span style="padding-top:5px;"><%#Eval("MASTERSRESP1")%></span></td>
                                <td style=''>&nbsp;</td>
                            </tr>  
                            <tr class='alterrow'>
                                <td style="border-bottom:solid 1px gray;" colspan="2" ></td>
                                <td style="border-bottom:solid 1px gray;text-align :center;background-color:<%#Eval("OD")%>"></td>
                                <td colspan="8" style="border-bottom:solid 1px gray;" >
                                    <span style="color:Red; font-style:italic;"><%#Eval("REMARK")%></span>
                                </td>
                                <td style="border-bottom:solid 1px gray;"></td>
                            </tr>       
                       </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
            </td>
        </tr>
    </table>
    <div style="position:absolute;top:0px;left:0px; height :360px; width:100%;" id="dv_Rating" runat="server" visible="false">
        <center>
             <div style="position:absolute;top:0px;left:0px; height :450px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
             <div style="position :relative;width:400px; height:250px;padding :3px; text-align :center;background : white; z-index:150;top:50px; border:solid 10px #ADD6FF;">
             <center >
                
                <span style='font-size:15px; color:Gray;'>Crew# / Rank </span>
                <br />
                <asp:Label runat="server" ID="lblCrewNumber" Font-Size="18px" Width="390px" BackColor="#ADD6FF" style='padding:5px;'></asp:Label>
                
                <span style='font-size:15px; color:Gray;'>Name</span>
                <br />
                <asp:Label runat="server" ID="lblCrewName" Font-Size="18px" Width="390px" BackColor="#ADD6FF" style='padding:5px;'></asp:Label>
                <br />
                <span style='font-size:15px; color:Gray;'>Assessment</span>
                <br /><br />
                <div style="border:solid 1px gray; width:300px;">
                    <asp:RadioButton ID="rat_A" GroupName="sae" runat="server" Width='50px' />
                    <asp:RadioButton ID="rat_B" GroupName="sae" runat="server" Width='50px' />
                    <asp:RadioButton ID="rat_C" GroupName="sae" runat="server" Width='50px' />
                    <asp:RadioButton ID="rat_NA" GroupName="sae" runat="server" Width='50px' />
                </div>
                <div style="width:300px; padding:10px; font-size:15px;">
                    <asp:Label ID="l1" runat="server" Width='50px' Text='A' BackColor="Green" ForeColor="White" BorderColor="Gray" BorderWidth="1" BorderStyle="Solid"/>
                    <asp:Label ID="l2" runat="server" Width='50px' Text='B' BackColor="Orange" ForeColor="White" BorderColor="Gray" BorderWidth="1" BorderStyle="Solid"/>
                    <asp:Label ID="l3" runat="server" Width='50px' Text='C' BackColor="Red" ForeColor="White" BorderColor="Gray" BorderWidth="1" BorderStyle="Solid"/>
                    <asp:Label ID="l4" runat="server" Width='50px' Text='NA' BackColor="" ForeColor="White" BorderColor="Gray" BorderWidth="1" BorderStyle="Solid"/>
                </div>
                <br />
                <asp:Button ID="Button4" runat="server" CssClass="btn" OnClick="btnRatingClose_Click" Text="Close" style=' padding:3px; font-size:13px;'/>
                <span style=' background-color:Red ; po'>
                    <asp:Button ID="Button5" runat="server" CssClass="btn" OnClick="btnUpdate_Click" Text="Update" style=' padding:3px; font-size:13px;' OnClientClick="this.value='Please wait..';" />
                </span>
             </center>
             </div>
        </center>
     </div>
    <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
        <div style="position:absolute;top:0px;left:0px; height :380px; width:100%;" id="dvFilter" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :520px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
             <div style="position :relative;width:900px; height:300px;padding :3px; text-align :center;background : white; z-index:150;top:100px; border:solid 10px #FFAD99;">
             <center >
                <table cellpadding="0" cellspacing="0" width="100%" rules="all" border="0"  style="border:solid 1px #bbbbbb;">
                 <tr class="headerstylefixedheader" style="height:25px">
                        <td style=" text-align:right " >&nbsp;</td>
                        <td><b>Fleet</b></td>
                        <td style=" text-align:right " >&nbsp;</td>
                        <td><asp:CheckBox runat="server" ID="cklAll_V" Checked="true" AutoPostBack="true" OnCheckedChanged="cklAll_V_CheckedChanged" />
                            <b>Vessel</b></td>
                        <td style=" text-align:right ">&nbsp;</td>
                        <td><asp:CheckBox runat="server" ID="chkAll_S" Checked="true" AutoPostBack="true" OnCheckedChanged="cklAll_S_CheckedChanged" />
                            <b>Suptd.</b></td>
                        <td style=" text-align:right ">&nbsp;</td>
                        <td>
                            <b>Due In</b>
                        </td>
                        <td style=" text-align:right ">&nbsp;</td>
                        <td style="text-align:left"> 
                            <b>Plan for</b>
                        </td>
                </tr>
                    <tr class="headerstylefixedheader" style="height:25px">
                        <td style=" text-align:right ">
                            &nbsp;</td>
                        <td style="vertical-align:top;">
                            <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="true" 
                                CssClass="input_box" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged" 
                                Width="120px">
                            </asp:DropDownList>
                        </td>
                        <td style=" text-align:right ">
                            &nbsp;</td>
                        <td style="vertical-align:top;">
                            <div style="height:240px; white-space:100px; overflow-y:scroll; overflow-x:hidden; border:solid 1px gray;">
                                <asp:CheckBoxList ID="ddlVessel" runat="server" RepeatColumns="1" 
                                    RepeatDirection="Vertical">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                        <td style=" text-align:right ">
                            &nbsp;</td>
                        <td>
                            <div style="height:240px; white-space:100px; overflow-y:scroll; overflow-x:hidden; border:solid 1px gray;">
                                <asp:CheckBoxList ID="ddlInsRemote" runat="server" RepeatColumns="1" 
                                    RepeatDirection="Vertical">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                        <td style=" text-align:right ">
                            &nbsp;</td>
                        <td style="vertical-align:top;">
                            <asp:TextBox ID="txtdays" runat="server" CssClass="input_box" MaxLength="3" 
                                Text="365" Width="50px"></asp:TextBox>
                            &nbsp;Days
                            <br />
                            <br />
                            <b>Only Planned</b><br />
                            <asp:CheckBox runat="server" ID="chk_PlannedOnly" />
                        </td>
                        <td style=" text-align:right ">
                            &nbsp;</td>
                        <td style="text-align:left; vertical-align:top;">
                            <asp:TextBox ID="txtPDays" runat="server" CssClass="input_box" MaxLength="3" 
                                Text="60" Width="50px"></asp:TextBox>
                            &nbsp;Days &nbsp;&nbsp;&nbsp;
                            

                        </td>
                    </tr>
                    <tr class="headerstylefixedheader" style="height:25px">
                        <td style=" text-align:right ">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style=" text-align:right ">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style=" text-align:right ">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style=" text-align:right ">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style=" text-align:right ">
                            &nbsp;</td>
                        <td style="text-align:right; padding:5px;">
                            <asp:Button ID="btnClose" runat="server" CssClass="Btn1" OnClick="btnClose_Click" Text="Close"/>
                            <asp:Button ID="btnShow" runat="server" CssClass="Btn1" OnClick="btnShow_Click" Text="Apply" OnClientClick="DisableMe(this)"/>
                            <asp:Button ID="btnReset" runat="server" CssClass="Btn1" OnClick="btnReset_Click" Text="Reset" OnClientClick="DisableMe(this)"/>
                        </td>
                    </tr>
        </table>
            </center>
            </div>
        </center>
        </div>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnShow" />
        <asp:PostBackTrigger ControlID="btnReset" />
        </Triggers>
    </asp:UpdatePanel>
    </div>
</asp:Content>
