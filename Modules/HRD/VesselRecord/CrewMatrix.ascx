<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CrewMatrix.ascx.cs" Inherits="VesselRecord_CrewMatrix" %>
<style type="text/css">
    .style1
    {
        height: 15px;
        width: 234px;
    }
    .style3
    {
        width: 134px;
        height: 15px;
    }
    .style4
    {
        height: 15px;
        width: 262px;
    }
</style>

  <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
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
 <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
<table width="100%" cellpadding="0" cellspacing="0" border ="1" style="font-family:Arial;font-size:12px;">
      <tr><td style=" background-color :#e2e2e2" > 
        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="height: 30px; padding-right :10px;">
              <tr>
                 <td style ="padding-left :30px; text-align :right " >Vessel Name:</td>
                 <td style=" text-align :left "><asp:TextBox ID="txtVesselName" ReadOnly="true" BackColor="#e2e2e2" runat="server" CssClass="input_box" MaxLength="49" Width="184px" TabIndex="1"></asp:TextBox></td>
                 <td style=" text-align :right ">Former Name:</td>
                 <td style=" text-align :left "><asp:TextBox ID="txtFormerVesselName" ReadOnly="true" BackColor="#e2e2e2" runat="server" CssClass="input_box" MaxLength="49" Width="122px"></asp:TextBox></td>
                 <td style=" text-align :right ">Flag:</td>
                 <td style=" text-align :left "><asp:DropDownList ID="ddlFlagStateName" Enabled="false" BackColor="#e2e2e2" runat="server" CssClass="input_box" Width="128px" TabIndex="2"></asp:DropDownList></td>
            </tr>
           </table>
      </td></tr>
              <tr><td style=" background-color :#e2e2e2" >
               <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                    <td align="left" style="width: 6%; height: 15px;" valign="middle">
                        <asp:RadioButtonList ID="rbfilter" runat="server" CssClass="input_box" RepeatDirection="Horizontal" Width="364px" BorderWidth="0px" AutoPostBack="True" OnSelectedIndexChanged="rbfilter_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Current Crew List</asp:ListItem>
                            <asp:ListItem>Crew List With Period</asp:ListItem>
                        </asp:RadioButtonList></td>
                            <td style="padding-left: 5px; width: 144px; height: 15px; text-align: right" valign="middle" id="tdfromdate" runat="server">
                           From Date :</td>
                          <td style="padding-left: 5px; text-align: left" valign="middle" class="style1">
                           <asp:TextBox ID="txtfromdate" runat="server" CssClass="input_box" MaxLength="15" Width="80px"></asp:TextBox>
                           <asp:ImageButton ID="imgfromdate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfromdate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                           </td>
                          <td style="padding-left: 5px; width: 107px; height: 15px; text-align: right" id="tdtodate" runat="server">
                           To Date:</td>
                          <td style="padding-left: 5px; text-align: left; " valign="middle" class="style4">
                              &nbsp;<asp:TextBox ID="txttodate" runat="server" CssClass="input_box" MaxLength="15" Width="80px"></asp:TextBox>
                                    <asp:ImageButton ID="imgtodate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                    <asp:RegularExpressionValidator ID="CompareValidator12" runat="server" ControlToValidate="txttodate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                               </td>
                          <td style="padding-left: 5px; text-align: center" valign="middle" 
                            class="style3">
                              <asp:Button ID="btn_Show" runat="server" OnClick="btn_Show_Click" CssClass="input_box" Text="Show" Width="50px"/></td>
                     </tr>
                   </table>
               </td></tr><tr>
        <td>
           <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                            <legend><strong>Crew List</strong></legend><table cellpadding="0" cellspacing="0" style="width: 100%; height: 57px">
                            <tr><td align="center"><asp:Label ID="Label1" ForeColor="Red"  runat="server"></asp:Label></td></tr>
                                <tr>
                                    <td align="left" style="width: 100%; height: 15px; text-align: left">
                                      <div style="padding-top:1px;overflow-y:scroll;overflow-x:hidden;height:265px; width:100%;" class="divParent" >
                                    <asp:GridView ID="gvmatrix" runat="server" AutoGenerateColumns="False" DataKeyNames="CrewId" GridLines="Horizontal" Height="9px" PageSize="3" Style="text-align: center" Width="98%" OnSelectedIndexChanged="gvmatrix_SelectedIndexChanged" OnRowDataBound="gvmatrix_RowDataBound" OnRowCommand="gvmatrix_RowCommand" OnPreRender="gvmatrix_PreRender">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" HeaderText="View" meta:resourcekey="CommandFieldResource1" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" ItemStyle-Width="40px" />
                                                     <asp:BoundField DataField="CrewNumber" HeaderText="Emp#" meta:resourcekey="BoundFieldResource7">
                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource2">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompanyName" runat="server" meta:resourcekey="lblCompanyNameResource2" Text='<%# Eval("CrewName") %>'></asp:Label>
                                                            <asp:HiddenField ID="HiddenSignOffDate" runat="server" Value='<% # Eval("SignOffDate") %>' />
                                                            <asp:HiddenField ID="HiddenCrewNumber" runat="server" Value='<% # Eval("CrewNumber") %>' />
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="RankName" HeaderText="Rank" meta:resourcekey="BoundFieldResource7">
                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Nationality" HeaderText="Nationality" meta:resourcekey="BoundFieldResource11">
                                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Sign On Date" meta:resourcekey="BoundFieldResource12" DataField="SignOnDate">
                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Sign Off Date" meta:resourcekey="BoundFieldResource12" DataField="SignOffDate">
                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    </asp:BoundField>
                                                     <asp:TemplateField HeaderText="Plan1">
                                                    <ItemTemplate>
                                                        <img src="../Images/group.gif" class="imgClick" style='cursor:pointer; display:<%# showHide(Eval("FirstReliverId").ToString())%> '  />
                                                                    
                                                            
                                                        <div class="divPopup" id='<%#Eval("CrewNumber")%>'  style="display:none; position:absolute;border:solid 1px red;width:170px;height:100px;background-color:white;color:#808080;">
                                                                <%#Eval("Details") %>
                                                            </div>
                                                            
                                                    </ItemTemplate>
                                                        <ItemStyle Width="50px" />
                                                    
                                                    </asp:TemplateField>
                                                  
                                                    
                                                     <asp:TemplateField HeaderText="Plan2">
                                                    <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style=" border-collapse:collapse;width:100%">
                                                    <tr>
                                                    <td style=" text-align:center; cursor:hand" onmouseover="t1.Show(event,'<%# Eval("Details1") %>')" onmouseout="if(t1)t1.Hide(event)">
                                                
                                                        <asp:ImageButton ID="img_rel1"  CommandName="img_plan2" runat="server" ImageUrl="~/Modules/HRD/Images/group.gif"/>
                                                        
                                                    </td>
                                                    </tr></table>
                                                    </ItemTemplate>
                                                         <ItemStyle Width="50px" />
                                                    
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                  <RowStyle CssClass="rowstyle" />
                                                                  
                                            </asp:GridView>
                                                                                      </div>
                                    </td>
                                   
                                </tr>
                
                
             </table></fieldset>
        </td>
    </tr>
       <tr>
        <td style="height: 7px; text-align: right; background-color :#e2e2e2">
            <asp:Label runat="server" ID="lblCount" style=" float : left" ></asp:Label> 
           <asp:Button ID="btn_Print" runat="server" CssClass="btn" Text="Print" CausesValidation="False" Width="50px" OnClientClick="return printcv();" /></td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
</table>
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
    PopupButtonID="imgfromdate" PopupPosition="TopLeft" TargetControlID="txtfromdate">
</ajaxToolkit:CalendarExtender>
<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
    PopupButtonID="imgtodate" PopupPosition="TopLeft" TargetControlID="txttodate">
</ajaxToolkit:CalendarExtender>
<asp:HiddenField ID="HiddenField1" runat="server" />

<script type="text/javascript" src="../Scripts/jquery.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var lasobjID='';

        //$("*:not(.imgClick)").click(function () {
        //    $(".divPopup").hide();
        //})

        $(window).click(function () {
            //Hide the menus if visible
            $(".divPopup").hide(300);
        });

        $('.imgClick').click(function (event) {
            event.stopPropagation();
        });
        
        $(".imgClick").click(function (e) {
            $(".divPopup").hide(300);
            if (lasobjID != $(this).next("div").attr("id")) {
                $(this).next("div").css({ 'top': e.pageY - 50, 'left': e.pageX-200, 'position': 'absolute', 'border': '1px solid black', 'padding': '5px' });
                $(this).next("div").show(300);
                lasobjID = $(this).next("div").attr("id");
                
                
            }
            else {
                lasobjID = '';
            }
        });
    })
</script>