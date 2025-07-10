<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registers.aspx.cs" Inherits="emtm_Registers" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <script type="text/javascript" src="../HRD/JS/jquery.min.js"></script>
    <script type="text/javascript" src="../HRD/JS/KPIScript.js"></script>
    <style type="text/css">
    *{
        font-family:Verdana; font-size:12px;
    }
    ul
    {
        list-style-type:none;
    }
    li
    {
        list-style-type:none;
    }
    .tab
    {
        background-color:#80CCFF;
        color:White;
        width:150px;
        text-align:center;
        padding:10px;
        float:left;
        margin-left:5px;
        cursor:pointer;
    }
    .activetab
    {
        background-color:#0099FF;
        color:White;
        width:150px;
        text-align:center;
        padding:10px;
        float:left;
        margin-left:5px;
        cursor:pointer;
    }
    .clear
    {
        clear:both;
        display:none;
    }
    #menu
    {
        overflow:auto;
        margin:0px;
        padding:0px;
    }
    .panel
    {
        border:solid 2px #0099FF;
        min-height:500px;
    }
    .scrollboxheader
    {
        height:40px;
        background-color:#0099FF;
        color:White;
    }
    .scrollboxheader table
    {
        height:40px;
    }
    .pop_box
    {
        position: absolute; 
        padding: 3px; 
        z-index: 150; 
        top: 100px; 
        text-align:center;
        width:100%;
    }
    .pop_frame
    { 
        border: solid 8px black;
        width: 700px; 
        background: white; 
        text-align:left;
        padding:10px;
    }
   /* .btn
    {
        background-color:#0099FF;
        color:White;
        padding:5px;
        border:none;
        margin-left:5px;
    }*/
  /*  .btnred
    {
        background-color:red;
        color:White;
        padding:5px;
        border:none;
        margin-left:5px;
    }*/
    
    .frame_header
    {
        font-size:18px;
        color:#0099FF;
        text-align:left;
    }
    
    </style>
    <script type="text/javascript">
        function ShowPanel(v, ctl) {
            $(".tab").removeClass('activetab');
            $(ctl).addClass('activetab');
            $("#btnReload" + v).focus();
            $("#btnReload" + v).click();
            $(".panel").hide();
            $("#dv" + v).show();
        }

        //---------------------------------

        function Close_POP() {
            $("#modal").hide();
            $("#Postion_Mod_Section").hide();
            $("#btnReload1").focus();
            $("#btnReload1").click();
        }

        function ShowPOPWinAdd(ctl) {
            $("#modal").show();
            $("#Postion_Mod_Section").show();
            $("#lblMessage1").html("");
            $("#hfdId1").val("0");
            $("#btnReload11").focus();
            $("#btnReload11").click();
        }

        function ShowPOPWinEdit(ctl) {
            var Id = $(ctl).attr("RowId");
                $("#modal").show();
                $("#Postion_Mod_Section").show();
                $("#lblMessage1").html("");
                $("#hfdId1").val(Id);
                $("#btnReload11").focus();
                $("#btnReload11").click();
        }
        //-------------------------------------------------

        function Close_POP2() {
            $("#modal").hide();
            $("#Postion_Mod_Section2").hide();
            $("#btnReload2").focus();
            $("#btnReload2").click();
        }

        function ShowPOPWinAdd2(ctl) {
            $("#modal").show();
            $("#Postion_Mod_Section2").show();
            $("#lblMessage2").html("");
            $("#hfdId2").val("0");
            $("#btnReload21").focus();
            $("#btnReload21").click();
        }

        function ShowPOPWinEdit2(ctl) {
            var Id = $(ctl).attr("RowId");
            $("#modal").show();
            $("#Postion_Mod_Section2").show();
            $("#lblMessage2").html("");
            $("#hfdId2").val(Id);
            $("#btnReload21").focus();
            $("#btnReload21").click();
        }
        //-------------------------------------------------

        $(document).ready(function () {
            $("#modal").click(function () { Close_POP(); });
            Close_POP();
            Close_POP2();
        });
    </script>

    <div id="modal" style="display:none;position: fixed; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)"></div>

    
    <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse;">
    <tr>
        
    <td valign="top">

    <div style="">
    <ul id="menu">
        <li class="tab activetab" onclick="ShowPanel(1,this)">Position Master</li>
        <li class="tab" onclick="ShowPanel(2,this)">Position Group</li>
        <li class="tab" onclick="ShowPanel(3,this)">Position Mapping</li>
        <li class="clear"></li>
    </ul>
    <div style="height:10px; background:#0099FF"></div>
    <div id="dv1" class="panel">
    <asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
        <!-- Grid View Section -->
        <div>
            <asp:Button runat="server" ID="btnReload1" OnClick="btnReload1_Click" Text="btnReload" style="display:none" CausesValidation="false" />
            <div style="padding:5px; background-color:#E6F5FF; vertical-align:middle;">
            Select Office :
            <asp:DropDownList ID="ddlOffice" runat="server" Width="120px" AutoPostBack="true" onselectedindexchanged="ddlOffice_SelectedIndexChanged" ></asp:DropDownList>
            <img runat="server" id="btnAddNew" src="../Images/add_16.gif" onclick="ShowPOPWinAdd();" title="Add New Position" style="cursor:pointer" />
            </div>
            <div>
                    <div class="scrollboxheader" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%;text-align:center; border-bottom:none;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="white">
                            <colgroup>
                                <col style="width:25px;" />
                                <col style="width:25px;" />
                                <col style="width:150px;" />
                                <col style="width:100px;" />
                                <col />
                                <col style="width:100px;" />
                                <col style="width:100px;" />
                                <col style="width:200px;" />
                                <col style="width:25px;" />
                                <tr align="left" class="blueheader">
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;Office Location</td>
                                    <td>&nbsp;Position Code</td>
                                    <td>&nbsp;Position Name</td>
                                    <td>&nbsp;Manager</td>
                                    <td>&nbsp;Inspector</td>
                                    <td>&nbsp;Vessel Position</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </colgroup>
                   </table>  
                   </div>         
                    <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; width: 100%; height: 450px ;">
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="#e2e2e2">
                        <colgroup>
                                <col style="width:25px;" />
                                <col style="width:25px;" />
                                <col style="width:150px;" />
                                <col style="width:100px;" />
                                <col />
                                <col style="width:100px;" />
                                <col style="width:100px;" />
                                <col style="width:200px;" />
                                <col style="width:25px;" />
                        </colgroup>
                        <asp:Repeater ID="rptHolidayMaster" runat="server"> 
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <img src="../HRD/Images/edit.jpg" onclick="ShowPOPWinEdit(this);" RowId='<%# Eval("PositionId") %>' ToolTip="Edit Positions" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btndocDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("PositionId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" OnClick="btnDelete_Click" ToolTip="Delete" />
                                    </td>
                                    <td align="left">&nbsp;<%#Eval("OfficeName")%></td>
                                    <td align="left">&nbsp;<%#Eval("PositionCode")%></td>
                                    <td align="left">&nbsp;<%#Eval("PositionName")%></td>
                                    <td align="left">&nbsp;<%#Eval("ManagerText")%></td>
                                    <td align="left">&nbsp;<%#Eval("InspectorText")%></td>
                                    <td align="left">&nbsp;<%#Eval("VesselPositionName")%></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        </table>
                    </div> 
                    </div>
        </div>      
                             
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <div id="dv2" class="panel" style="display:none">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
    <div>
        <!-- Grid View Section -->
        <div>
            <asp:Button runat="server" ID="btnReload2" OnClick="btnReload2_Click" Text="btnReload2" style="display:none" CausesValidation="false" />
            <div style="padding:5px; background-color:#E6F5FF; vertical-align:middle;">
                <img runat="server" id="Img1" src="../Images/add_16.gif" onclick="ShowPOPWinAdd2();" title="Add New Position Group" style="cursor:pointer" />
            </div>
            <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
            <td>
            <div class="scrollboxheader" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%;text-align:center; border-bottom:none;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="white">
                            <colgroup>
                                <col style="width:25px;" />
                                <col style="width:25px;" />
                                <col />
                                <col style="width:25px;" />
                                <tr align="left" class="blueheader">
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;Position Group Name</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </colgroup>
                   </table>  
                   </div>         
            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; width: 100%; height: 450px ;">
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="#e2e2e2">
                        <colgroup>
                                <col style="width:25px;" />
                                <col style="width:25px;" />
                                <col />
                                <col style="width:25px;" />
                        </colgroup>
                        <asp:Repeater ID="rptPositionGroup" runat="server"> 
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <img src="../Images/edit.jpg" onclick="ShowPOPWinEdit2(this);" RowId='<%# Eval("VPId") %>' ToolTip="Edit Positions" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnVPDelete" runat="server" CausesValidation="false" Visible='<%#(Common.CastAsInt32(Eval("VPId"))>7) %>' CommandArgument='<%# Eval("VPId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" OnClick="btnVPDelete_Click" ToolTip="Delete" />
                                    </td>
                                    <td align="left">&nbsp;<%#Eval("PositionName")%></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        </table>
                    </div> 
            </td>
            </tr>
            </table>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <div id="dv3" class="panel" style="display:none">
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
    <ContentTemplate>
    <div style="padding:5px; background-color:#E6F5FF; vertical-align:middle;">
            Select Position Group :
            <asp:DropDownList ID="ddlPosGroup" runat="server" Width="300px" AutoPostBack="true" onselectedindexchanged="ddlPosGroup_SelectedIndexChanged" ></asp:DropDownList>
    </div>
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style="width:45%">
     <div class="scrollboxheader" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%;text-align:center; border-bottom:none;">
        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="white">
            <colgroup>
                <col style="width:25px;" />
                <col style="width:200px;" />
                <col />
                <col style="width:25px;" />
                <tr align="left">
                    <td> </td>
                    <td>&nbsp;Office Name</td>
                    <td>&nbsp;Position Name</td>
                    <td>&nbsp;</td>
                </tr>
            </colgroup>
       </table>  
       </div> 
     <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; width: 100%; height: 450px ;border:solid 1px #f2f2f2;">
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="#e2e2e2">
                <colgroup>
                    <col style="width:25px;" />
                    <col style="width:200px;" />
                    <col />
                    <col style="width:25px;" />
                </colgroup>
                <asp:Repeater ID="rpt_RemainingPositions" runat="server"> 
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <asp:CheckBox runat="server" ID="chkSel" ToolTip='<%#Eval("PositionId")%>' />
                            </td>
                            <td align="left">&nbsp;<%#Eval("OfficeName")%></td>
                            <td align="left">&nbsp;<%#Eval("PositionName")%></td>
                            <td>&nbsp;</td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
            </div>      
    </td>
    <td style="width:10%; text-align:center">
        <asp:Button ID="btnAssign" CssClass="btn" runat="server" Text="Assign >>" onclick="btnAssign_Click" width="95%" OnClientClick="this.value='Processing..';" CausesValidation="false" ></asp:Button>
        <br /><br />
        <asp:Button ID="btnRemove" CssClass="btn" runat="server" Text="<< Remove" onclick="btnRemove_Click" width="95%" OnClientClick="this.value='Processing..';" CausesValidation="false" ></asp:Button>
    </td>
    <td>
    <div class="scrollboxheader" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%;text-align:center; border-bottom:none;">
        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="white">
            <colgroup>
                <col style="width:25px;" />
                <col style="width:200px;" />
                <col />
                <col style="width:25px;" />
                <tr align="left">
                    <td> </td>
                    <td>&nbsp;Office Name</td>
                    <td>&nbsp;Position Name</td>
                    <td>&nbsp;</td>
                </tr>
            </colgroup>
       </table>  
       </div> 
    <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; width: 100%; height: 450px ; border:solid 1px #f2f2f2;">
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="#e2e2e2">
                <colgroup>
                    <col style="width:25px;" />
                    <col style="width:200px;" />
                    <col />
                    <col style="width:25px;" />
                </colgroup>
                <asp:Repeater ID="rpt_Linked" runat="server"> 
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <asp:CheckBox runat="server" ID="chkSel" ToolTip='<%#Eval("PositionId")%>' />
                            </td>
                            <td align="left">&nbsp;<%#Eval("OfficeName")%></td>
                            <td align="left">&nbsp;<%#Eval("PositionName")%></td>
                            <td>&nbsp;</td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
            </div>      
    </td>
    </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <div id="dv4" class="panel" style="display:none">
    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
    <ContentTemplate>
    <div style="padding:5px; background-color:#E6F5FF; vertical-align:middle;">
            Select Position Group :
            <asp:DropDownList ID="DropDownList1" runat="server" Width="300px" AutoPostBack="true" onselectedindexchanged="ddlPosGroup_SelectedIndexChanged" ></asp:DropDownList>
    </div>
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style="width:45%">
     <div class="scrollboxheader" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%;text-align:center; border-bottom:none;">
        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="white">
            <colgroup>
                <col style="width:25px;" />
                <col style="width:200px;" />
                <col />
                <col style="width:25px;" />
                <tr align="left">
                    <td> </td>
                    <td>&nbsp;Office Name</td>
                    <td>&nbsp;Position Name</td>
                    <td>&nbsp;</td>
                </tr>
            </colgroup>
       </table>  
       </div> 
     <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; width: 100%; height: 450px ;border:solid 1px #f2f2f2;">
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="#e2e2e2">
                <colgroup>
                    <col style="width:25px;" />
                    <col style="width:200px;" />
                    <col />
                    <col style="width:25px;" />
                </colgroup>
                <asp:Repeater ID="Repeater1" runat="server"> 
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <asp:CheckBox runat="server" ID="chkSel" ToolTip='<%#Eval("PositionId")%>' />
                            </td>
                            <td align="left">&nbsp;<%#Eval("OfficeName")%></td>
                            <td align="left">&nbsp;<%#Eval("PositionName")%></td>
                            <td>&nbsp;</td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
            </div>      
    </td>
    <td style="width:10%; text-align:center">
        <asp:Button ID="Button1" CssClass="btn" runat="server" Text="Assign >>" onclick="btnAssign_Click" width="95%" OnClientClick="this.value='Processing..';" CausesValidation="false" ></asp:Button>
        <br /><br />
        <asp:Button ID="Button2" CssClass="btn" runat="server" Text="<< Remove" onclick="btnRemove_Click" width="95%" OnClientClick="this.value='Processing..';" CausesValidation="false" ></asp:Button>
    </td>
    <td>
    <div class="scrollboxheader" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%;text-align:center; border-bottom:none;">
        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="white">
            <colgroup>
                <col style="width:25px;" />
                <col style="width:200px;" />
                <col />
                <col style="width:25px;" />
                <tr align="left">
                    <td> </td>
                    <td>&nbsp;Office Name</td>
                    <td>&nbsp;Position Name</td>
                    <td>&nbsp;</td>
                </tr>
            </colgroup>
       </table>  
       </div> 
    <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; width: 100%; height: 450px ; border:solid 1px #f2f2f2;">
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" bordercolor="#e2e2e2">
                <colgroup>
                    <col style="width:25px;" />
                    <col style="width:200px;" />
                    <col />
                    <col style="width:25px;" />
                </colgroup>
                <asp:Repeater ID="Repeater2" runat="server"> 
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <asp:CheckBox runat="server" ID="chkSel" ToolTip='<%#Eval("PositionId")%>' />
                            </td>
                            <td align="left">&nbsp;<%#Eval("OfficeName")%></td>
                            <td align="left">&nbsp;<%#Eval("PositionName")%></td>
                            <td>&nbsp;</td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
            </div>      
    </td>
    </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </div>



    <!-- Add / Edit Section -->
         <div id="Postion_Mod_Section" class="pop_box" style="">
         <center>
            <div class="pop_frame">
            <asp:UpdatePanel runat="server" ID="up2">
            <ContentTemplate>
                <div>
                <asp:Button runat="server" ID="btnReload11" OnClick="btnReload11_Click" Text="btnReload" style="display:none" CausesValidation="false"  />
                <asp:HiddenField runat="server" id="hfdId1" />
                <span class="frame_header">Add / Edit Position</span>
                <br /><br />
                <table width="100%" cellpadding="4" cellspacing ="0" border="1" bordercolor="#f2f2f2" style="border-collapse:collapse">
                        <tr>
                            <td style="text-align :right">Office :  </td>
                            <td style="text-align :left">
                                <asp:DropDownList ID="ddlOffice1" runat="server" Width="200px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOffice1" InitialValue="" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align :right">Position Group : </td>
                            <td style="text-align :left">
                                <asp:DropDownList runat="server" ID="ddlVesselPosition" Width="200px" RepeatDirection="Horizontal"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align :right">Position Code :</td>
                            <td style="text-align :left"><asp:TextBox ID="txtPosCode" runat="server" Width="200px" MaxLength="20"></asp:TextBox></td>
                            <td style="text-align :right">Manager :</td>
                            <td style="text-align :left">
                                <asp:RadioButtonList runat="server" ID="radmangaer" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align :right">
                            Position Name : </td>
                            <td style="text-align :left">
                            <asp:TextBox ID="txtPosName" runat="server" Width="200px" required='yes' MaxLength="50" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPosName" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align :right">
                                Inspector :</td>
                            <td style="text-align :left">
                                <asp:RadioButtonList runat="server" ID="radInspector" 
                                    RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0" Selected></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                       </table>
                <div style="height:20px">
                    <asp:Label runat="server" ID="lblMessage1" ForeColor="Red" Font-Bold="true"></asp:Label>
                </div>
                <center>
                    <asp:Button ID="btnsave" CssClass="btn" runat="server" Text="Save" onclick="btnsave_Click" width="150px" ></asp:Button>
                    <button class="btn btnred" onclick="Close_POP();">Close This Window </button>
                </center>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>
         </center>
         </div>  


    <!-- Add / Edit Section -->

        <div id="Postion_Mod_Section2" class="pop_box" style="">
         <center>
            <div class="pop_frame">
            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div>
                <asp:Button runat="server" ID="btnReload21" OnClick="btnReload21_Click" Text="btnReload" style="display:none" CausesValidation="false"  />
                <asp:HiddenField runat="server" id="hfdId2" />
                <span class="frame_header">Add / Edit Position Group</span>
                <br /><br />
                <table width="100%" cellpadding="4" cellspacing ="0" border="1" bordercolor="#f2f2f2" style="border-collapse:collapse">
                        <tr>
                            <td style="text-align :right">
                            Position Group Name : </td>
                            <td style="text-align :left">
                            <asp:TextBox ID="txtPosgrpName" runat="server" Width="400px" required='yes' MaxLength="50" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPosgrpName" ValidationGroup="vg001" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                       </table>
                <div style="height:20px">
                    <asp:Label runat="server" ID="lblMessage2" ForeColor="Red" Font-Bold="true"></asp:Label>
                </div>
                <center>
                    <asp:Button ID="btnsave2" CssClass="btn" runat="server" Text="Save" onclick="btnsave2_Click" width="150px" ValidationGroup="vg001"></asp:Button>
                    <button class="btn btnred" onclick="Close_POP2();">Close This Window </button>
                </center>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>
         </center>
         </div>  
    </td>
    </tr>
    </table>
    </asp:Content>
