<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditVIQChapterDetails.aspx.cs" Inherits="VIMS_EditVIQChapterDetails" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <script src="../js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="VettingScript.js" type="text/javascript"></script>
    <link href="Vettingstyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .chk_sel
        {
            background-color:lightgreen;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#chkAll").click(function () {
                var selfvalue = $(this).attr('checked');
                $("[name$='chkAddMe']").each(function (i, o) {

                    $(o).attr('checked', selfvalue);

                    if (selfvalue)
                        $(o).addClass('chk_sel');
                    else
                        $(o).removeClass('chk_sel');
                });
            });
            //----------------------
            $("[name$='btnRemSelQ']").each(function (i, o) {
                $(this).click(function () {
                    $(this).attr('src', "../Images/progress_16.gif");
                    
                });
            });

            //----------------------
            $("#dv_Guidance").keyup(function () {
                if (event.keyCode == 27) {
                    HideGuidanceModel();
                }
            });


            $("#dv_Comments").keyup(function () {
                if (event.keyCode == 27) {
                    HideComments();
                }
            });

            //----------------------

            $("body").scroll(function () {
                var bodystp = $("body").scrollTop();
                if (bodystp == 0) {
                    bodystp = $(document).scrollTop();
                }
                $("#dv_Guidance").css('top', bodystp + 150 + 'px');
                $("#dv_Comments").css('top', bodystp + 150 + 'px');
            });
            //----------------------
            $(document).scroll(function () {
                var bodystp = $("body").scrollTop();
                if (bodystp == 0) {
                    bodystp = $(document).scrollTop();
                }
                $("#dv_Guidance").css('top', bodystp + 150 + 'px');
                $("#dv_Comments").css('top', bodystp + 150 + 'px');
            });
            //----------------------
            $(".tab_1 > a").click(function () {
                $(".c1").hide();
                $(".tab_1").removeClass('tabsel');
                $(this).parent().addClass('tabsel');
                var cc = $(this).attr('ctr');
                $("#" + cc).show();
            });
        });


        function ShowGuidanceModel(ctl) {
            $("#dv_Header").html("VIQ Guidance");
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Guidance").slideDown();
            $("#dv_Guidance").css('top', bodystp + 100 + 'px');
            $("#spn_Guidance").html($(ctl).siblings(".spn_G").html());

            $("#btn_ClosebtnPopUP").focus();
        }
        function HideGuidanceModel() {
            $(".dv_ModalBox").hide();
            $("#dv_Guidance").hide();
        }

        function ShowOfficeRemarkModel(ctl) {
            $("#dv_Header").html("Office Guidance");
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Guidance").slideDown();
            $("#dv_Guidance").css('top', bodystp + 100 + 'px');
            $("#spn_Guidance").html($(ctl).siblings(".spn_OC").html());

            $("#btn_ClosebtnPopUP").focus();
        }
        
     
    </script>
    <style type="text/css">
    .bar
    {
        background-color:LightGreen; 
        height:11px;
    }
    .tab_1
    {
        float:left;
        padding:5px 0px 5px 0px; 
        margin-right:4px;
        border:none;
        color:White;
    }
    
    .tab_1 a
    {
        background-color:grey;
        padding:10px 5px 10px 5px; 
        border:none;
        color:White;
        cursor:pointer;
    }
    tabsel
    {
        float:left;
        padding:5px 0px 5px 0px; 
        margin-right:4px;
        border:none;
        color:White;
    }
     .tabsel a
    {
        background-color:#008AE6;
        padding:10px 5px 10px 5px; 
        border:none;
        color:White;
        cursor:pointer;
    }
    .c1
    {
        padding:5px;
    }
    </style>

    </head>
<body>
    <form id="form1" runat="server">
    <img src="../Images/progress_16.gif" style="display:none" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="background-color:Black; opacity:0.4;filter:alpha(opacity=40); width:100%; min-height:100%; position:absolute; top:0px; left:0px; display:none;" class='dv_ModalBox' onclick="HideGuidanceModel();HideComments();"></div>
    <div>
    <table align="center" width="100%" border="1" cellpadding="0" cellspacing="0" style="border-collapse:collapse">    
    <tr>
    <td style=" padding:5px">
    <table>
    <tr>
    <td>Select Rank : </td>
    <td><asp:DropDownList runat="server" ID="ddlRank" AutoPostBack="true" OnSelectedIndexChanged="ddlRankStatus_OnSelectedIndexChanged"></asp:DropDownList></td>
    <td><asp:Label runat="Server" ID="lblscoreheading" Text="Select Risk Score :" Visible="false" ></asp:Label> </td>
    <td><asp:CheckBoxList runat="server" ID="chkScores" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="ddlRankStatus_OnSelectedIndexChanged"></asp:CheckBoxList></td>
    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Observation count more OR equal to </td>
    <td><asp:TextBox runat="server" ID="txtMinObs" MaxLength="5" Width="50px" style="text-align:center" AutoPostBack="true" OnTextChanged="ddlRankStatus_OnSelectedIndexChanged"></asp:TextBox></td>
    </tr>
    </table>
    </td>
    </tr>
    <tr>
       <td>
       <div style="height:30px; overflow-y:scroll; overflow-x:hidden;">
       <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:30px;' bordercolor="white">
       <thead>
       <tr>
       <td style="width:70px">Question#</td>
       <td>Question</td>
       <td style="width:80px">Resp. Rank</td>
       <td style="width:80px">Risk Score</td>
       <td style="width:80px">Obs.Count</td>
       <td style="width:40px"><img src="../Images/cv.png" title="Show Guidance" style="cursor:pointer; height:12px;" /></td>
       <td style="width:40px"><img src="../Images/icon_comment.gif" title="Show Office Comments" style="cursor:pointer"/></td>
       <td style="width:40px">
        <input type="checkbox" id="chkAll" />
       </td>
       <td style="width:30px">&nbsp;</td>
       </tr>
       </table>
       </div>
       <div style="height:540px; overflow-y:scroll; overflow-x:hidden;" id='dv_Qlist1' class='ScrollAutoReset'>
       <table cellpadding="0" rules="rows" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#E6E6E6">
       <tbody>
          <asp:Repeater runat="server" ID="rpt_ChaptersQuestions">
          <ItemTemplate>
            <tr>
                <td style="width:70px; text-align:left"><span class='clsqno'><%#Eval("QuestionNo")%></span></td>
                <td style=" text-align:left; text-justify:inter-word;">
                    <span style='<%#(Common.CastAsInt32(Eval("PreVetting"))>0)?"font-weight:bold":""%>' class="spn_Question">
                        <%#Eval("Question")%>
                    </span>
                </td>
                <td style="width:80px;"><%#Eval("RankCode")%></td>
                <td style="width:80px"><%#Eval("RiskScore")%></td>
                <td style="width:80px"><%#Eval("ObsCount")%></td>
                <td style="width:40px"><img src="../Images/cv.png" title="VIQ Guidance" onclick="ShowGuidanceModel(this);" style="cursor:pointer; height:12px;" /><span class='spn_G' style='display:none'><%#Eval("Guidance")%></span></td>
                <td style="width:40px"><img src="../Images/icon_comment.gif" title="Office Guidance" onclick="ShowOfficeRemarkModel(this);" style="cursor:pointer"/><span class='spn_OC' style='display:none'><%#Eval("OfficeRemarks")%></span></td>
                <td style="width:40px">
                    <asp:CheckBox runat="server" ID="chkAddMe" Visible='<%#(Eval("SELECTED").ToString()=="N")%>' CssClass='<%#Eval("QuestionId")%>' rankid='<%#Eval("RankId")%>'/>
                    <asp:ImageButton runat="server" ID="btnRemSelQ" ImageUrl="~/Images/delete_12.gif" ToolTip="Remove Selection" OnClick="btnRemSelQ_Click" rankid='<%#Eval("RankId")%>' CssClass='<%#Eval("QuestionId")%>' Visible='<%#(Eval("SELECTED").ToString()=="Y")%>'/>
                </td>
                <td style="width:30px">&nbsp;</td>
            </tr>
          </ItemTemplate>
          </asp:Repeater>
       </tbody>
       </table>
       </div>
         <div style="padding:5px; background-color:#444444; height:35px;" >
            <asp:Button runat="server" ID="btnAddTOVIQ" Text=" Save " CausesValidation="false" CssClass="Btn1" ValidationGroup="gg" OnClick="btnFinalSave_Click" OnClientClick="DisableMe(this);" style="float:right"/>
       </div>
       </td>
    </tr>
    </table>
    </div>
    <div style="position:absolute; z-index:100; top:100px;left :0px; width:100% ; display:none;" id="dv_Guidance">
        <center>
            <div style="border:solid 10px orange; width:800px; background-color:White; text-align:left; padding:5px; padding-top:0px; ">
                <div id="dv_Header" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;"></div>
                <span id="spn_Guidance"></span>
                <br />
                <center>
                    <input type="button" id="btn_ClosebtnPopUP" class="popupclosebtn" value="Close" onclick="HideGuidanceModel();" style="margin-top:10px;"/>
                </center>
            </div>
        </center>
    </div>
</form>
</body>
</html>
