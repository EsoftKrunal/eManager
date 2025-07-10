<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Arrival.ascx.cs" Inherits="VesselRecord_Arrival" %>

  <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<%--<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
</head>--%>
<script language="javascript" type="text/javascript">
    function printcv()
    {
   
    var Vesselid=document.getElementById('CrewMatrixl1_HiddenField1').value;
    var fd;
    var td;
    if(document.getElementById('CrewMatrixl1_rbfilter_0').checked==true)
    {
    fd='';
    
    //var td=document.getElementById('CrewMatrixl1_txttodate').value;
    td='';
    
     }
     else
     {
     fd=document.getElementById('CrewMatrixl1_txtfromdate').value;
     td=document.getElementById('CrewMatrixl1_txttodate').value;
     }
    
   window.open('..\\Reporting\\PrintCrewList.aspx?VesselId='+ Vesselid+'&FD='+fd+'&TD='+td,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
   
    }


    </script>
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table width="100%" cellpadding="5" cellspacing="0">
  
      
    <tr>
        <td align="center">
             <asp:HiddenField ID="hiddenfieldarrival" runat="server" />
            <asp:Label ID="lbl_Msg" runat="Server" ForeColor="Red"></asp:Label></td>
    </tr>
                   <tr><td> 
                   <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                    <legend><strong>Arrivals</strong></legend>
                    <div style=" height:100px; overflow-x:hidden; overflow-y:scroll" align="center">
                    <asp:Label ID="lbl_gv" runat="server" Width="144px"></asp:Label>
                    <asp:GridView ID="gv_Arrival" runat="server" AutoGenerateColumns="False" DataKeyNames="ArrivalId" GridLines="Horizontal" Height="9px" PageSize="3" Style="text-align: center" Width="98%" OnSelectedIndexChanged="gv_Arrival_SelectedIndexChanged" OnRowEditing="gv_Arrival_RowEditing" OnPreRender="gv_Arrival_PreRender" OnRowDeleting="gv_Arrival_RowDeleting">
                    <Columns>
                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" ><ItemStyle Width="38px"/></asp:CommandField>
                              <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="ArrivalDate" HeaderText="Arrival Date" />
                               <asp:BoundField DataField="Lat" HeaderText="latitude" />
                               <asp:BoundField DataField="Long" HeaderText="Longitude" />
                               <asp:BoundField DataField="DRAFT_FWD" HeaderText="DRAFT FWD" />
                               <asp:BoundField DataField="DRAFT_AFT" HeaderText="DRAFT AFT" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                  <RowStyle CssClass="rowstyle" />
                                                                  
                                            </asp:GridView>
                    </div>
                    </fieldset></td></tr>
    <tr>
        <td style="padding-top: 15px;">
        <asp:Panel ID="pnl_Arrival" runat="server" Width="100%" >
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                            <legend><strong>Arrival</strong></legend>
                            
                            
                            <table cellpadding="5" cellspacing="0" style="width: 100%; height: 57px">
                            <tr>
                                <td align="left" style="width: 100%; height: 15px; text-align: left">
                           
    <table cellpadding="5" cellspacing="0" width="100%">
    <tr><td>      <%-- <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                                                    <legend><strong></strong></legend>--%>
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                  <td style="text-align: right; width: 303px;">
                                                      DATE :&nbsp;</td>
                                                  <td style="width: 178px" >
                                                      <asp:TextBox ID="txt_Arrival" CssClass="required_box" runat="server" MaxLength="10" TabIndex="1" Width="88px"></asp:TextBox>
                                                      <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />&nbsp;&nbsp;
                                                      &nbsp;
                                                  </td>
                                                  <td style="width: 270px; text-align: right">
                                                      TIME :&nbsp;</td>
                                                  <td style="width: 158px">
                                                      <asp:TextBox ID="txt_ArrivalHour" runat="server" MaxLength="2" Width="18px" CssClass="required_box" TabIndex="2" ></asp:TextBox>
                                                      :
                                                      <asp:TextBox ID="txt_ArrivalMinuts" runat="server" MaxLength="2" Width="18px" CssClass="required_box" TabIndex="3" ></asp:TextBox></td>
                                                  <td style="width: 158px; text-align: right">
                                                      LAT :&nbsp;</td>
                                                  <td style="width: 158px">
                                            <asp:TextBox ID="txt_Lat" CssClass="input_box"  runat="server" MaxLength="30" Width="121px" TabIndex="4"></asp:TextBox></td>
                                              </tr>
                                    <tr>
                                        <td style="width: 303px; text-align: right; height: 19px;">
                                        </td>
                                        <td style="width: 178px; height: 19px;">
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_Arrival"
                                                ErrorMessage="Invalid Date." Operator="DataTypeCheck" Type="Date" CssClass="input_box"></asp:CompareValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Arrival"
                                                ErrorMessage="Required." CssClass="input_box"></asp:RequiredFieldValidator></td>
                                        <td style="width: 270px; text-align: right; height: 19px;">
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txt_ArrivalHour"
                                                ErrorMessage="Required(00-23)." MaximumValue="23" MinimumValue="0" Type="Integer"
                                                Width="115px"></asp:RangeValidator></td>
                                        <td style="width: 158px; height: 19px;">
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txt_ArrivalMinuts"
                                                ErrorMessage="Required(00-59)." MaximumValue="59" MinimumValue="0" Type="Integer"
                                                Width="114px"></asp:RangeValidator></td>
                                        <td style="width: 158px; height: 19px">
                                        </td>
                                        <td style="width: 158px; height: 19px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 303px; text-align: right">
                                            LONG :&nbsp;</td>
                                        <td style="width: 178px">
                                            <asp:TextBox ID="txt_long" CssClass="input_box"  runat="server" MaxLength="30" Width="121px" TabIndex="5"></asp:TextBox></td>
                                        <td style="width: 270px; text-align: right;">
                                                                        FWD : &nbsp;</td>
                                        <td style="width: 158px">
                                                                        <asp:TextBox ID="txt_FWD" CssClass="input_box"  runat="server" MaxLength="30" Width="121px" TabIndex="6"></asp:TextBox></td>
                                        <td style="width: 158px; text-align: right">
                                                                        AFT :&nbsp;
                                        </td>
                                        <td style="width: 158px">
                                                                        <asp:TextBox ID="txt_AFT" CssClass="input_box"  runat="server" MaxLength="30" Width="121px" TabIndex="7"></asp:TextBox></td>
                                    </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                            </td>
                                                            <td style="width: 178px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 270px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                            <td style="width: 158px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                                IFO :&nbsp;
                                                            </td>
                                                            <td style="width: 178px">
                                                                <asp:TextBox ID="txt_Ifo" CssClass="input_box" MaxLength="30" runat="server" Width="121px" TabIndex="8"></asp:TextBox></td>
                                                            <td style="width: 270px; text-align: right">
                                                                MDO :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                                                <asp:TextBox ID="txt_mdo" CssClass="input_box" MaxLength="30" runat="server" Width="121px" TabIndex="9"></asp:TextBox></td>
                                                            <td style="width: 158px; text-align: right">
                                                                      MW :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                                                      <asp:TextBox ID="txt_mw" CssClass="input_box" MaxLength="30" runat="server" Width="121px" TabIndex="10"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                            </td>
                                                            <td style="width: 178px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 270px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                            <td style="width: 158px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                                      OVERALL DIST FROM LAST PORT :&nbsp;
                                                            </td>
                                                            <td style="width: 178px">
                                                                      <asp:TextBox ID="txt_odflp" MaxLength="30" CssClass="input_box"  runat="server" Width="121px" TabIndex="11"></asp:TextBox></td>
                                                            <td style="width: 270px; text-align: right">
                                                                      (BERTH To BERTH)---(BERTH-FAOP) :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                                                      <asp:TextBox ID="txt_btb" CssClass="input_box" MaxLength="30" runat="server" Width="121px" TabIndex="12"></asp:TextBox></td>
                                                            <td style="width: 158px; text-align: right">
                                                                      FAOP-EOSP :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                                                      <asp:TextBox ID="txt_faop_eosp" MaxLength="30" CssClass="input_box" runat="server" Width="121px" TabIndex="13"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                            </td>
                                                            <td style="width: 178px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 270px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                            <td style="width: 158px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                                      AVG.SPEED :&nbsp;
                                                            </td>
                                                            <td style="width: 178px">
                                                                      <asp:TextBox ID="txt_avg_speed" CssClass="input_box" MaxLength="30" runat="server" Width="121px" TabIndex="14"></asp:TextBox></td>
                                                            <td style="width: 270px; text-align: right">
                                        SAILING-TIME :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                        <asp:TextBox ID="txt_KTS" CssClass="input_box" runat="server" MaxLength="30" Width="121px" TabIndex="15"></asp:TextBox></td>
                                                            <td style="width: 158px; text-align: right">
                                        EOSP-BERTH :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                        <asp:TextBox ID="txt_EOSPBerth" CssClass="input_box" runat="server" MaxLength="30" Width="121px" TabIndex="16"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                            </td>
                                                            <td style="width: 178px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 270px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                            <td style="width: 158px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                      DIST BERTH TO BERTH :&nbsp;
                                                            </td>
                                                            <td style="width: 178px">
                                                                <asp:TextBox ID="txt_distbtb" CssClass="input_box" MaxLength="30" runat="server" Width="121px" TabIndex="17"></asp:TextBox></td>
                                                            <td style="width: 270px; text-align: right">
                                                      ME F.O. CONS :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                                                <asp:TextBox ID="txtmefocons" CssClass="input_box" MaxLength="30" runat="server" Width="121px" TabIndex="18"></asp:TextBox></td>
                                                            <td style="width: 158px; text-align: right">
                                                                AVG :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                            <asp:TextBox ID="txtavg" CssClass="input_box" MaxLength="30" runat="server" Width="121px" TabIndex="19"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                            </td>
                                                            <td style="width: 178px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 270px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                            <td style="width: 158px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                      AUX MDO CONS :&nbsp;
                                                            </td>
                                                            <td style="width: 178px">
                                            <asp:TextBox CssClass="input_box" ID="txtauxmdocons" MaxLength="30" runat="server" Width="121px" TabIndex="20"></asp:TextBox></td>
                                                            <td style="width: 270px; text-align: right">
                                                                AVG :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                                                <asp:TextBox CssClass="input_box" ID="txtavg1" MaxLength="30" runat="server" Width="121px" TabIndex="21"></asp:TextBox></td>
                                                            <td style="width: 158px; text-align: right">
                                                      AVG REVS :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                                                <asp:TextBox CssClass="input_box" ID="txt_avg_revs" MaxLength="30" runat="server" Width="121px" TabIndex="22"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                            </td>
                                                            <td style="width: 178px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 270px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                            <td style="width: 158px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                                  SLIP :&nbsp;
                                                            </td>
                                                            <td style="width: 178px">
                                                                  <asp:TextBox ID="txt_slip" MaxLength="30" CssClass="input_box" runat="server" Width="121px" TabIndex="23"></asp:TextBox></td>
                                                            <td style="width: 270px; text-align: right">
                                                                  STOPAGES :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                                                  <asp:TextBox ID="txt_stopages" MaxLength="30" CssClass="input_box" runat="server" Width="121px" TabIndex="24"></asp:TextBox></td>
                                                            <td style="width: 158px; text-align: right">
                                                                REASON :&nbsp;
                                                            </td>
                                                            <td style="width: 158px">
                                                                  <asp:TextBox ID="txt_reason" MaxLength="30" CssClass="input_box" runat="server" Width="121px" TabIndex="25"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right">
                                                            </td>
                                                            <td style="width: 178px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 270px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                            <td style="width: 158px; text-align: right">
                                                            </td>
                                                            <td style="width: 158px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 303px; text-align: right; height: 30px;">
                                                                REASON FOR ANCHORING :&nbsp;
                                                            </td>
                                                            <td style="width: 178px; height: 30px;">
                                                                  <asp:TextBox ID="txt_anchoringreason" MaxLength="30" runat="server" Width="121px" CssClass="input_box" TabIndex="26"></asp:TextBox></td>
                                                            <td style="width: 270px; text-align: right; height: 30px;">
                                                                  </td>
                                                            <td style="width: 158px; height: 30px;">
                                                                  </td>
                                                            <td style="width: 158px; height: 30px">
                                                            </td>
                                                            <td style="width: 158px; height: 30px">
                                                            </td>
                                                        </tr>
                                                    </table>
           
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="true"
                ClearMaskOnLostFocus="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Arrival">
            </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                PopupButtonID="ImageButton1" PopupPosition="TopLeft" TargetControlID="txt_Arrival">
            </ajaxToolkit:CalendarExtender>
                                                    </td></tr>
  
    </table>
      </td></tr>
      </table>
             </fieldset>
        </asp:Panel>
        </td>
    </tr>
   <tr>
        <td style="height: 7px; text-align: right">
            <asp:Button ID="btn_Add" runat="server" CssClass="btn" Text="Add" CausesValidation="False" Width="50px"  OnClick="btn_Add_Click" TabIndex="27" />
            <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" CausesValidation="False" Width="50px"  OnClick="btn_Save_Click" TabIndex="28" />
            <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" Text="Cancel" CausesValidation="False" Width="50px"  OnClick="btn_Cancel_Click" TabIndex="29" /></td>
    </tr>
</table>

