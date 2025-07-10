<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewTraining.master" AutoEventWireup="true" CodeFile="AssignTraining.aspx.cs" Inherits="CrewOperation_AssignTraining" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <div style="font-family:Arial;font-size:12px;">
    <style type="text/css">
        .bordered tr td
        {
            border:solid 1px #e8e4e4;
        }

        tr < td < span < input{
            background-color:rebeccapurple;
        }
    </style>
    <script type="text/javascript">
        function selectallcrew(ctl)
        {
            $("#crew").find("input[type='checkbox']").prop("checked", $(ctl).is(":checked"));
        }
        function selectalltrainings(ctl) {
            $("#tngs").find("input[type='checkbox']").prop("checked", $(ctl).is(":checked"));
        }
    </script>
         <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
<table cellpadding="0" cellspacing="0" width="100%" style=" background-color:#c2c2c2">
                     <tr>
                      <td style=" padding:3px;">
                          <table cellpadding="2" cellspacing="0" width="100%">
                              <tr>
                                  <td style="height: 11px; width: 77px; text-align: right;">
                                      Crew # :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:TextBox ID="txt_MemberId" runat="server" CssClass="input_box" MaxLength="6"
                                          TabIndex="1" Width="50px"></asp:TextBox></td>
                                  <td style="height: 11px">
                                      Crew Status :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:DropDownList ID="ddl_CrewStatus_Search" runat="server" CssClass="input_box"
                                          TabIndex="11" Width="113px">
                                          <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                                          <asp:ListItem Text="New" Value="1" ></asp:ListItem>
                                          <asp:ListItem Text="On Leave" Value="2"></asp:ListItem>
                                          <asp:ListItem Text="On Board" Value="3"  ></asp:ListItem>
                                      </asp:DropDownList></td>
                                  <td style="height: 11px">
                                      Vessel :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" TabIndex="10"
                                          Width="187px">
                                          <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                                      </asp:DropDownList></td>
                                  <td style="height: 11px">
                                      Rank :</td>
                                  <td style="height: 11px; text-align: left;">
                                      <asp:DropDownList ID="ddl_Rank_Search" runat="server" CssClass="input_box" TabIndex="4"
                                          Width="123px">
                                          <asp:ListItem Text="&lt; Select &gt;"></asp:ListItem>
                                      </asp:DropDownList></td>
                                  <td style="height: 11px">
                                      &nbsp;
                                      <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="60px" TabIndex="9" OnClick="btn_Search_Click" CausesValidation="False" />&nbsp;&nbsp; 
                                      </td>
                              </tr>
                          </table>
                          </td>
                 </tr>
                </table>        
<table width="100%" cellpadding="2" cellspacing="1" rules="rows" border="1" style=" border-collapse: collapse;" >
<col width="30px" />
<col width="55px" />
<col />
<col width="80px" />
<col width="80px" />
<col width="80px" />
<col width="80px" />
<col width="100px" />
<col width="100px" />   
<col width="17px" />   
<col  />   
    <tr class="headerstylegrid" style="font-weight:bold;">
        <td>
            <input type="checkbox" id="chk_All" onclick="javascript:CheckAll(this);"/><label for="chk_All" >&nbsp;</label>
        </td>
        <td>Crew #</td>
        <td>Crew Name</td>
        <td>Rank</td>
        <td>Status</td>
        <td>Vessel</td>
        <td>Done Dt</td>
        <td>Next Due Dt</td>
        <td>Planned For</td>                                
        <td></td>
    </tr>
</table>
<div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 395px; border-bottom:solid 1px #c2c2c2;">
    <table width="100%" cellpadding="2" cellspacing="1" rules="rows" border="1" style=" border-collapse: collapse;" >
        <colgroup>
                        <col width="30px" />
                        <col width="55px" />
                        <col />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="80px" />
                        <col width="100px" />
                        <col width="100px" />   
                        <col width="17px" />  
            </colgroup>
                            <asp:Repeater ID="rptAssignTraining" runat="server" >
                                <ItemTemplate>
                                    <tr onmouseover="this.style.historycolor=this.style.backgroundColor;this.style.backgroundColor='#c2c2c2';" onmouseout="this.style.backgroundColor=this.style.historycolor;">
                                        <td>
                                            <asp:CheckBox ID="chkSelect" runat="server"   />
                                        </td>
                                        <td><a target="_blank" title="Show Training Matrix" href='PopupTMatrix.aspx?c=<%#Eval("CrewId")%>'><%#Eval("CREWNUMBER")%></a>
                                            <asp:HiddenField ID="hfdCrewId" runat="server" Value='<%#Eval("CrewId")%>' />
                                        </td>
                                        <td align="left"><%#Eval("FullName")%></td>
                                        <td><%#Eval("RankName")%></td>
                                        <td align="center"><%#Eval("CrewStatusName")%></td>
                                        <td><%#Eval("VesselName")%></td>
                                        <td><%#Eval("DoneDt")%></td>
                                        <td><%#Eval("NextDueDt")%></td>
                                        <td><%#Eval("NextPlanDt")%></td>
                                        <td></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
</div>
<div style="text-align:left; padding:5px; background-color:#fcefa3;overflow:auto;">
       <asp:Label ID="lbl_Message" Font-Bold="true" Font-Size="Larger" runat="server" ForeColor="#C00000" >Same Training already exist For Same Period.</asp:Label>
       <asp:Button ID="btnGo"  runat="server" CssClass="btn" style="padding:5px; float:right; " Text="Go" Width="60px" TabIndex="9" OnClick="btn_Go_Click" CausesValidation="False" />&nbsp;&nbsp; 
</div>
<table style="width: 100%" runat="server" visible="false">
               <tr>
                   <td style="text-align: right">
                       <table cellpadding="2" cellspacing="0" width="100%">
                           <tr style="text-align:left; font-weight:bold;">
                               <td><%--Sire Chapter :--%> Training Name :</td>
                               <td>Due&nbsp; Date :</td>
                               <td></td>
                               <td>&nbsp;</td>
                           </tr>
                           <tr style="text-align:left">
                               <td style="text-align: left">
                                   <%--<asp:DropDownList ID="ddl_SireChap" runat="server" AutoPostBack="True" CssClass="required_box" OnSelectedIndexChanged="ddlSireChap_SelectedIndexChanged"  TabIndex="1" Width="250px"> </asp:DropDownList>--%>

                                   <asp:DropDownList ID="ddl_Training" runat="server" CssClass="required_box" Width="250px"> </asp:DropDownList>
                               </td>
                               <td style="text-align: left">
                                           <asp:TextBox runat="server" ID="txthTid" style="display:none"></asp:TextBox>
                                    <asp:Button ID="btn_Save_PlanTraining" runat="server" CssClass="btn" Text="Assign Training" Width="100px" TabIndex="9" OnClick="btn_Save_PlanTraining_Click" CausesValidation="true" ValidationGroup="vg" OnClientClick="return setOptionValue();"/>
                               </td>
                               <td>
                                   </td>
                               <td>
                          

                               </td>
                           </tr>
                           <tr style="text-align:left">
                               <td style="height: 13px; text-align: left">
                                  <%-- <asp:CompareValidator ID="CompareValidator11" runat="server" ValidationGroup="vg" ControlToValidate="ddl_SireChap" ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>--%>
                                    <asp:RegularExpressionValidator ID="CompareValidator12" ValidationGroup="vg" runat="server" ControlToValidate="txt_DueDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="Req1" ControlToValidate="txt_DueDate" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                               </td>
                               <td style="height: 13px; text-align: left">
                                  
                                   </td>
                               <td></td>
                               <td></td>
                           </tr>
                       </table>
                       </td>
               </tr>
           </table>
    <div style="position:absolute;top:50px;left:100px;  width:100%;" id="dvFrame" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:50px;left:100px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:80%; padding :0px; text-align :center;background : white; z-index:150;top:50px; border:solid 10px black;">
            <asp:UpdatePanel runat="server" ID="uptra">
                <ContentTemplate>
                    <center >   
                        <div>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse">
                            <tr class= "headerstylegrid" style=" font-weight:bold; ">
                                <td style="width:400px; padding:10px; font-size:13px; padding-left:3px;">
                                    <input type="checkbox" onclick="selectallcrew(this);" checked="true" />
                                    Crew Members Selected
                                </td>
                                <td style="width:350px; padding:10px; font-size:13px;">
                                    Select Group
                                </td>
                                 <td style="padding:10px; font-size:13px;padding-left:3px;">
                                     <input type="checkbox" onclick="selectalltrainings(this);" checked="true" />
                                    Select Trainings
                                </td>
                            </tr>
                            <tr>
                                <td id="crew">
                                <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 350px; border-bottom:solid 1px #c2c2c2;">
                                <table width="100%" cellpadding="3" border="0" style=" border-collapse: collapse" class="bordered" >
                                    <colgroup>
                                <col width="30px" />
                                <col width="55px" />
                                <col />
                                        </colgroup>
                                    <asp:Repeater ID="rptCrew" runat="server" >
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkSelect" runat="server" runat="server"  Checked="true" />
                                                </td>
                                                <td>
                                                    <a target="_blank" title="Show Training Matrix" href='PopupTMatrix.aspx?c=<%#Eval("CrewId")%>'><%#Eval("CREWNUMBER")%></a> 
                                                    <asp:HiddenField ID="hfdCrewId" runat="server" Value='<%#Eval("CrewId")%>' />
                                                </td>
                                                <td align="left"><%#Eval("FullName")%> <span style="color:brown">( <%#Eval("RankName")%> )</span></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                        
                                </table>
                        </div>
                                </td>
                                <td>
                                 <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 350px; border-bottom:solid 1px #c2c2c2;">
                                  <table width="100%" cellpadding="3" border="0" style=" border-collapse: collapse" class="bordered" >
                                      <colgroup>              
                                      <col width="30px" />
                                                    <col />
                                          </colgroup>
                                                        <asp:Repeater ID="rptTrainingGroup" runat="server" >
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkSelectGroup" AutoPostBack="true" runat="server"  OnCheckedChanged="chkSelectGroup_CheckedChanged" CssClass='<%#Eval("ChapterNo")%>'  />
                                                                    </td>
                                                                    <td align="left"><%#Eval("ChapterName")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                 </div>
                                </td>
                                <td id="tngs">
                                 <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 350px; border-bottom:solid 1px #c2c2c2;" id='uji6788768' class="ScrollAutoReset">
                                    <table width="100%" cellpadding="3" border="0" style=" border-collapse: collapse" class="bordered" >
                                        <colgroup>
                                    <col width="30px" />
                                    <col />
                                            </colgroup>
                                        <asp:Repeater ID="rptTrainings" runat="server" >
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkSelect" runat="server"  CssClass='<%#Eval("trainingid")%>' />
                                                    </td>
                                                    <td align="left"><%#Eval("TrainingName")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                  </div>
                                </td>
                            </tr>
                        </table>
                        <div style="padding:3px; text-align:right">
                            <table width="100%" border="0" style=" border-collapse: collapse" >
                                <tr>
                                    <td><asp:Label runat="server" ID="lblmsg1" style="float:left" Font-Bold="true" ForeColor="Red"></asp:Label></td>
                                    <td style="width:95px; text-align:right">
                                        Due Date :</td>
                                    <td style="width:125px; text-align:left">
                                        <asp:TextBox ID="txt_DueDate" runat="server" CssClass="required_box" MaxLength="20" TabIndex="3" Width="90px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton5" PopupPosition="TopLeft" TargetControlID="txt_DueDate"></ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="width:210px">
                                        <asp:Button ID="btnFinalSave" runat="server" CssClass="btn" style="padding:5px; " Text="Assign Trainings" Width="120px" TabIndex="9" OnClick="btnFinalSave_Click" CausesValidation="False" />
                                        <asp:Button ID="btnClose" runat="server" CssClass="btn" style="padding:5px; " Text="Close" Width="70px" TabIndex="9" OnClick="btnClose_Click" CausesValidation="False" />
                                    </td>
                                </tr>
                            </table>
                            
                        </div>
                            </div>
                    </center>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnClose" />
                </Triggers>
            </asp:UpdatePanel>
            </div>
        </center>
         </div>
</div>
<script type ="text/javascript" >
function CheckAll(self)
{
    for(i=0;i<=document.getElementsByTagName("input").length-1;i++)  
    {
        if(document.getElementsByTagName("input").item(i).getAttribute("type")=="checkbox" && document.getElementsByTagName("input").item(i).getAttribute("id")!=self.id)
        {
            document.getElementsByTagName("input").item(i).checked=self.checked;
        } 
    }
}
function setOptionValue()
{
    var ddl=document.getElementById("ddl_SireChap");
    if(ddl.selectedIndex==0)
    {
        alert ("Please Select Sire Chapter.");
        ddl.focus();
        return false; 
    }

    var txt=document.getElementById("txtTraining");
    ddl=document.getElementById("ddlTrainings");
    txt.value=ddl.options[ddl.selectedIndex].value;
    if(ddl.selectedIndex==0)
    {
        alert ("Please Select Training.");
        ddl.focus();
        return false; 
    }
}
function setSearch()
{
    var ddl=document.getElementById("ddlTrainings");
    var val=ddl.options[ddl.selectedIndex].value;
    
    alert(val);
    document.getElementById("txthTid").setAttribute("value",val);
    document.getElementById("btnSearch").click();
}
</script>
</asp:Content>