<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RCAanalysis.aspx.cs" Inherits="RCAanalysis" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
    <link href="./treestyle.css" rel="stylesheet" />
    <link href="./scroll.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=PT+Sans" rel="stylesheet">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" >
    <link rel="stylesheet" type="text/css" href="./jquery.datetimepicker.css"/>
      
</head>
<body runat="server" >
    <form id="form1" runat="server">
        
        <div class="" style="border-bottom:solid 1px #f7f7f7"> 

            <span class="" style="padding:0px;border-bottom:none;color:#2b9de0;font-size:25px;text-align:center;display:block;"> 
                    <strong><asp:Literal ID="litVessel" runat="server"></asp:Literal></strong>
                <asp:Button ID="btnTempClick" runat="server" OnClick="btnTempClick_Click" style="display:none;" />
            </span>

         </div>
        <div class="" style="height:30px;">
            <div style="border-bottom:none;color:white;font-size:14px;color:#333;float:left;border:solid 0px red;width:100%;">
                <table cellpadding="5" cellspacing="0" border="0" width="100%" style="border-collapse:collapse" >
                <col width="33%" />
                <col width="33%" />
                <col />
                <tr>
                    <td>
                        <asp:Literal ID="litReportNo" runat="server"></asp:Literal>
                    </td>
                    <td style="text-align:center;">
                        <asp:Literal ID="litPort" runat="server"></asp:Literal>
                    </td>
                    <td style="text-align:right;">
                        <asp:Literal ID="litReportDate" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            </div>
        </div>

        <div class="boxed" style="background-color:#2b9de0">
            <span class="causeheading" style="padding:10px;border-bottom:none;color:white;font-size:22px;"> Short Description - <asp:Literal ID="litFocalPoint" runat="server"></asp:Literal></span>

            <div style="float:right;margin-top:8px;"> 
                <asp:Button ID="btnSendForApproval" runat="server" CssClass="btn btncancel" Text="Send for approval" OnClick="btnSendForApproval_OnClick" /> 

            </div>
        </div>
        
        <div class="container" onclick="hideaction();">
            <asp:Literal ID="litTree" runat="server"></asp:Literal>            
        </div>
        <!--------->
        <div class="actionpanel" onmouseover="SetOver();" onmouseout="SetOut();">
            <div class="actionrow borderbottom editcause" onclick="editcause();"><i class="fa fa-edit"></i>Edit this Cause</div>
            <div class="actionrow borderbottom removecause" onclick="removecause();"><i class="fa fa-trash"></i>Remove this Cause</div>
            <div class="actionrow borderbottom" onclick="addnewcause();"><i class="fa fa-plus"></i>Add Cause</div>
            <div class="actionrow borderbottom" onclick="terminatecause();"><i class="fa fa-times"></i>Terminate Cause</div>
            <div class="actionrow borderbottom setrootcause" onclick="setrootcause();"><i class="fa fa-exclamation-circle"></i>Set this as Root Cause</div>
            <div class="actionrow borderbottom" onclick="addcomments();"><i class="fa fa-plus"></i>Add Comments</div>
            <div class="actionrow borderbottom addcorraction" onclick="addcorrectiveaction();"><i class="fa fa-plus"></i>Add Corrective Action</div>
        </div>
        <!--------->
        <div class="actiontarget" id="panel_addeditcause">
        <div class="header">
        <h1>&nbsp;-</h1>    
        </div>
        <i class="fa fa-times fa-2x close" ></i>    
        <h3>Select Cause</h3>
        <div style="padding:10px;position:relative;">
            <input id="causeitemfilter" placeholder="Enter your text here .." />
            <span class="inputafter"></span>
        </div>
       <div class="causescrollbar style-9" style="margin:11px;height:400px;overflow-x:hidden; width:96%;border: solid 1px #f1f1f1;text-align:center;">
        <img src="../Images/loading-animation.gif" />
       </div>    
       <div style="position:absolute;left:0px;right:0px;bottom:0px;text-align:center;">
        <%--<div id="dvaddnewcausetrigger">
            <span style="display:inline-block;padding:10px;padding: 10px;border: solid 1px #2b9de0;">
            <i class="fa fa-plus" style="color: #333 ;line-height:25px;font-size:20px;" ></i>
            <span style="line-height:25px;font-size:20px;">Add New Cause</span>
            </span>
        </div>--%>

        <div id="dvaddnewcause">
            
            <h3>Add New Cause</h3>
            <div style="color:red;text-align:left;font-size:10px;padding-left:20px;"> <i> Max 100 characters. </i> </div>
            <div style="margin:15px; text-align:right;">
                <textarea id="txtcausetext" style="background-color:#faeda7;"></textarea>
                <input type="button" value="Save" class="btn" style="margin-top:10px;" onclick="SaveCause();"/>
            </div>   
        </div>
    </div>              
   <div>
   </div> 
</div>
        <!--------->
        <div class="actiontarget" id="panel_addcomments">
        <div class="header">
        <h1>Comments</h1>    
        </div>
        <i class="fa fa-times fa-2x close" ></i>    
            <h3>Add New Comment</h3>
            <div style="margin:15px; text-align:right;">
                <textarea id="txtcommenttext"></textarea>
                <input type="button" value="Save" class="btn" style="margin-top:10px;" onclick="SaveComments();"/>
            </div>  
       <div>
       </div> 
      </div>
        <!--------->
        <div class="actiontarget" id="panel_commentslist">
        <div class="header">
        <h1>Comments</h1>  
             <i class="fa fa-times fa-2x close" ></i>   
        <div class="editComments" style="margin:15px; text-align:right;display:none;">
            <h3>Edit Comment</h3>
            <textarea id="txteditcommenttext"></textarea>
            <input type="hidden" id="hfdEditingCommentID" />
            <input type="button" value="Save" class="btn" style="margin-top:10px;" onclick="UpdateComments();"/>
            <input type="button" value="Cancel" class="btn" style="margin-top:10px;" onclick="CancelEditComments();"/>
        </div>
        <div class="commentscrollbar style-9" style="margin:11px;height:400px;overflow-x:hidden; width:96%;border: solid 1px #f1f1f1;text-align:center;">
        <img src="../../../Images/loading-animation.gif" />
       </div>
       </div> 
      </div>
         <!--------->
        <div class="actiontarget" id="panel_addcorrectiveaction">
        <div class="header">
        <h1>Corrective Actions</h1>    
        </div>
        <i class="fa fa-times fa-2x close" ></i>    
            <h3>Add/Edit Corrective Action</h3>
            <div style="margin:15px; text-align:left;">
                <textarea id="txtcatext"></textarea>
                 <h4>Responsibility</h4>
                <div>
                    <input type="checkbox" id="radO" />
                    <label for="radO">Office</label>
                    <input type="checkbox" id="radV" />
                    <label for="radV" >Vessel</label>
                 </div>
                <h4>Target Closure Date </h4>
                <div>
                    <input type="text" id="txtTCLDate" style="width:100px;" />
                </div>
                <div style="text-align:right;">

                <input type="button" value="Save" class="btn" style="margin-top:10px;" onclick="SaveCA();"/>
                    </div>

            </div> 
           
       <div>
       </div> 
      </div>
        <!--------->
        <div class="actiontarget" id="panel_calist">
        <div class="header">
        <h1>Corrective Actions</h1>  
             <i class="fa fa-times fa-2x close" ></i>   
        <div class="cascrollbar style-9" style="margin:11px;height:400px;overflow-x:hidden; width:96%;border: solid 1px #f1f1f1;text-align:center;">
        <img src="../../../Images/loading-animation.gif" />
       </div>
       </div> 
      </div>
        
</form>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JS/jquery.datetimepicker.js"></script>
    <script src="./MultiNestedList.js" type="text/javascript"></script>    
    <script type="text/javascript">
$(document).ready(function() {
  $.ajaxSetup({ cache: false });
});
        var __caid = 0;
    //----------------------------
    var over = 0;
    var analysisid = 0;
    var paid = 0;
    var status = 0;
    var mode = '';
    var CauseList=''
    //----------------------------
    function SetOver() {
        over = 1;        
    }
    function SetOut() {
        over = 0;
    }
    function hideaction() {
        if (over==0)
            $(".actionpanel").slideUp();
        }
    </script>
    <script type="text/javascript">
        //---------------------------------
        function editcause() {
            Show('panel_addeditcause');
            mode = 'E';
            $("#panel_addeditcause h1").html("Edit Cause");
            $("#causeitemfilter").val($("#txtcausetext").val());
            $("#causeitemfilter").select();
            $(".actionpanel").slideUp();
            LoadCauseList();
            
            if (isCauseOther($("#txtcausetext").val())) 
                $("#dvaddnewcause").show();
            else
                $("#dvaddnewcause").hide();
            

        }
        function isCauseOther(text) {
            var ret = true;
            
            $(CauseList).each(function (i, o) {
                if (o.cause.toLowerCase() == text.toLowerCase())
                    ret =false;
            })
            //$(".causescrollbar .causeitem").each(function (i, o) {
            //    if ($(o).html().toLowerCase() == text.toLowerCase())
            //        ret =false;
            //})
            return ret;
        }
        function removecause() {
            if (confirm('Are you sure to delete this recored?')) {
                $.ajax({
                    url: "RCAanalysis.aspx/removecause",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ aid: analysisid }),
                    type: "POST",
                    success: function (data) {
                        //window.location.reload();
                        $("#btnTempClick").click();
                    }
                });
            }
        }
        function addnewcause() {
            Show('panel_addeditcause');
            mode = 'A';
            $("#panel_addeditcause h1").html("Add Cause");
            $("#txtcausetext").val("");
            $("#causeitemfilter").focus();
            $(".actionpanel").slideUp();
            LoadCauseList();
        }
        function terminatecause() {
            $.ajax({
                url: "RCAanalysis.aspx/terminatecause",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ aid: analysisid }),
                type: "POST",
                success: function (data) {
                    //window.location.reload();
                    $("#btnTempClick").click();
                }
            });
        }
        function setrootcause() {
            $.ajax({
                url: "RCAanalysis.aspx/setrootcause",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ aid: analysisid }),
                type: "POST",
                success: function (data) {
                    //window.location.reload();
                    $("#btnTempClick").click();
                }
            });
        }
        function addcomments()
        {
            Show('panel_addcomments');
            $("#txtcommenttext").focus();  
            $(".actionpanel").slideUp();
        }
        function addcorrectiveaction() {
            //---------------------------
            Hide();
            //---------------------------
            __caid = 0;
            Show('panel_addcorrectiveaction');

            $("#txtcatext").val("");
            $("#radO").prop('checked', false);
            $("#radV").prop('checked', false);
            $("#txtTCLDate").val("");

            $("#txtcatext").focus();  
            $(".actionpanel").slideUp();
        }
        function showcomments(aid) {
            analysisid = aid;
            Show('panel_commentslist');
            $(".actionpanel").slideUp();
            LoadComments();
        }
        function showca(aid) {
            analysisid = aid;
            Show('panel_calist');
            $(".actionpanel").slideUp();
            LoadCA();
        }
        function EditCA(caid) {
            //---------------------------
            Hide();
            //---------------------------
            $.ajax({
                url: "RCAanalysis.aspx/loadcasingle",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ caid: caid }),
                type: "POST",
                success: function (data) {
                    var dd = JSON.parse(data.d);
                    $(".cascrollbar").html("");
                    $.each(dd, function (i, d) {
                        __caid = caid;
                        Show('panel_addcorrectiveaction');
                        $("#txtcatext").focus();
                        $("#txtcatext").val(d.correctiveaction);
                        switch (d.responsibility)
                        {
                            case "O":
                                $("#radO").prop('checked', true);
                                $("#radV").prop('checked', false);
                                break;
                            case "V":
                                $("#radO").prop('checked', false);
                                $("#radV").prop('checked', true);
                                break;
                            case "B":
                                $("#radO").prop('checked', true);
                                $("#radV").prop('checked', true);
                                break;
                        }
                        $("#txtTCLDate").val(d.targetclosuredate);
                        
                    })
                }
            });                       
        }
        function DeleteCA(caid) {
            if (window.confirm('Are you sure to delete this?'))
            {
                $.ajax({
                    url: "RCAanalysis.aspx/deleteca",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ caid: caid }),
                    type: "POST",
                    success: function (data) {
                        var dd = JSON.parse(data.d);
                        if (dd == "ok") {
                            LoadCA();
                        }
                        else {

                        }
                    }
                });
            }            
        }
        </script>
    <script type="text/javascript">
        //---------------------------------
        function SaveCause()
        {
            var breakdownno = '<%=BreakDownNo%>';            
            var vesselCode = '<%=VesselCode %>';
            var userName = '<%=UserName %>';

            $.ajax({
            url: "RCAanalysis.aspx/savecause",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ aid: analysisid, breakdownno: breakdownno, vesselcode: vesselCode, cause: $("#txtcausetext").val(), mode: mode, username: userName}),
            type: "POST",
            success: function (data) {                
                //window.location.reload();
                $("#btnTempClick").click();
            }
        });
        }   
        //---------------------------------
        function selectcause(ctl) {
            var breakdownno = '<%=BreakDownNo%>';            
            var vesselCode = '<%=VesselCode %>';
            var userName = '<%=UserName %>';
            if ($(ctl).attr('key') == '-1') {
                $("#dvaddnewcause").show();
                $("#txtcausetext").focus();
            }
            else {
                $.ajax({
                    url: "RCAanalysis.aspx/savecause",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ aid: analysisid, breakdownno: breakdownno, vesselcode: vesselCode, cause: $(ctl).html(), mode: mode, username: userName }),
                    type: "POST",
                    success: function (data) {
                        //window.location.reload();
                        $("#btnTempClick").click(); }
                });
            }
        }
        //---------------------------------
        function LoadCauseList()
        {
            var breakdownno = '<%=BreakDownNo%>';
            var vesselCode = '<%=VesselCode %>';
             $.ajax({
                 url: "RCAanalysis.aspx/loadcauselist",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 data: JSON.stringify({ aid: analysisid }),
                 type: "POST",
                 cache: false,
                 async: false,
                 success: function (data) {
                     var dd = JSON.parse(data.d);
                     CauseList = dd;
                     $(".causescrollbar").html("");
                     //div = '<div class=''causeitem'' key='' onclick=''selectcause(this);''> '+ d.cause+' </div> '
                     $.each(dd, function (i, d) {
                         var div = document.createElement("div");
                         $(div).addClass("causeitem");
                         $(div).attr("key",d.kk);
                         $(div).on("click", function () {
                             selectcause(this);
                         });
                         $(div).html(d.cause); 
                         $(".causescrollbar").append(div);
                     })
                 }
             });
        }
        //---------------------------------
        function SaveComments() {
            $.ajax({
                url: "RCAanalysis.aspx/savecomments",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ aid: analysisid, comments: $("#txtcommenttext").val(), mode: mode }),
                type: "POST",
                success: function (data) {
                    //window.location.reload();
                    $("#btnTempClick").click();
                }
            });
        } 
        //---------------------------------
        function LoadComments() {
            var rcaStatus = '<%=RcaStatus %>';
            $.ajax({
                url: "RCAanalysis.aspx/loadcomments",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ aid: analysisid }),
                type: "POST",
                success: function (data) {
                    var dd = JSON.parse(data.d);
                    $(".commentscrollbar").html("");
                    $.each(dd, function (i, d) {
                        var div = document.createElement("div");
                        $(div).addClass("commentsitem")
                        $(div).html(d.comments + "<div style='text-align:right;color:orange;font-size:14px;'>   <i class='fa fa-edit' style='float:left;color:#2b9de0;" + ((rcaStatus == "C") ? "display:none;" : "") + "' title='Edit' onclick='EditComments(" + d.commentid + ",\"" + d.comments + "\");' ></i>   <i class='fa fa-trash ' style='float:left;color:red;margin-left:4px;" + ((rcaStatus == "C") ? "display:none;" : "") +"' title='Delete'  onclick='DeleteComments(" + d.commentid + ");'></i> " + d.commentby + " ( " + d.commentdate + " ) </div>  ");
                        //<div style='text-align:right;color:red;font-size:14px;' title='Delete'> <i class='fa fa-times fa-2x' onclick='DeleteComments(" + d.commentid + ");' ></i> </div> <div style='text-align:right;color:red;font-size:14px;' title='Delete'> <i class='fa fa-edit fa-2x' onclick='EditComments(" + d.commentid + ",\"" + d.comments +"\");' ></i> </div>
                        $(".commentscrollbar").append(div);
                    })
                }
            });
        }
        //---------------------------------
        function SaveCA() {
            if ($("#txtcatext").val() == "")
            {
                alert('Please enter corrective action.');
                $("#txtcatext").focus();
                return;
            }

            var respval = "";
            if ($("#radO").is(":checked"))
                respval = "O";
            if ($("#radV").is(":checked"))
                respval += "V";

            if(respval=="") 
            {
                alert('Please select responsibility.');
                $("#radO").focus();
                return;
            }
            if(respval=="OV") 
            {
                respval = "B"; 
            }

            if ($("#txtTCLDate").val() == "") {
                alert('Please enter target closure date.');
                $("#txtcatext").focus();
                return;
            }
            else {
                var cd = new Date();
                var d3m = new Date();
                d3m.setMonth(d3m.getMonth() + 3);

                cd = cd.toDateString();
                cd = new Date(cd);
                var td = new Date($("#txtTCLDate").val());
                if (td < cd) {
                    alert('Target closure date should be more than current data.');
                    $("#txtcatext").focus();
                    return;
                }

                if (td > d3m)
                {
                    alert('Target closure date cant be more than after 3 months.');
                    $("#txtcatext").focus();
                    return;
                }
            }
            
            $.ajax({
                url: "RCAanalysis.aspx/saveca",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ caid: __caid,aid: analysisid, catext: $("#txtcatext").val(), tcldate: $("#txtTCLDate").val(),resp:respval}),
                type: "POST",
                success: function (data) {
                    //window.location.reload();
                    $("#btnTempClick").click();
                }
            });
        } 
        //---------------------------------
        function LoadCA() {
            var rcaStatus = '<%=RcaStatus %>';
            $.ajax({
                url: "RCAanalysis.aspx/loadca",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ aid: analysisid }),
                type: "POST",
                success: function (data) {
                    var dd = JSON.parse(data.d);
                    $(".cascrollbar").html("");
                    $.each(dd, function (i, d) {
                        var div = document.createElement("div");
                        $(div).addClass("commentsitem")

                        $(div).html(d.correctiveaction + "<div style='text-align:right;color:orange;font-size:14px;font-weight:bold;'><span style='float:left;font-weight:bold;'>Responsibility : " + d.responsibility + "</span> Target Closure Dt. : " + d.targetclosuredate + "<br/>  <i class='fa fa-edit' style='float:left;color:#2b9de0;" + ((rcaStatus == "C") ? "display:none;" : "") + "' title='Edit' onclick='EditCA(" + d.caid + ");' ></i>   <i class='fa fa-trash ' style='float:left;color:red;margin-left:4px;" + ((rcaStatus == "C") ? "display:none;" : "") + "' title='Delete'  onclick='DeleteCA(" + d.caid + ");'></i> </div>");
                        $(".cascrollbar").append(div);
                    })
                }
            });
        }

        //---------------------------------
        function DeleteComments(Commentid) {
            if (!confirm('Are you sure to delete?'))
                return false;

            $.ajax({
                url: "RCAanalysis.aspx/deletecomments",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ commentid: Commentid }),
                type: "POST",
                success: function (data) {
                    var dd = JSON.parse(data.d);
                    if (dd == "ok") {
                        LoadComments();
                    }
                    else {
                        
                    }

                    //$(".cascrollbar").html("");
                    //$.each(dd, function (i, d) {
                    //    var div = document.createElement("div");
                    //    $(div).addClass("commentsitem")
                    //    $(div).html(d.correctiveaction + "<div style='text-align:right;color:orange;font-size:14px;font-weight:bold;'><span style='float:left;font-weight:bold;'>Responsibility : " + d.responsibility + "</span> Target Closure Dt. : " + d.targetclosuredate + " </div>");
                    //    $(".cascrollbar").append(div);
                    //})
                }
            });
        }
        //---------------------------------
        function EditComments(Commentid,text) {
            $(".editComments").show();
            $("#txteditcommenttext").val(text);
            $("#hfdEditingCommentID").val(Commentid);
            
        }
        function CancelEditComments() {
            $(".editComments").hide();
            $("#txteditcommenttext").val('');
            $("#hfdEditingCommentID").val('');
        }
        function UpdateComments() {
            var CommentID = parseInt($("#hfdEditingCommentID").val());
            if ($("#txteditcommenttext").val() == '')
            {
                alert('Please enter comment.');
                return;
            }
            $.ajax({
                url: "RCAanalysis.aspx/updatecomments",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ aid: analysisid, comments: $("#txteditcommenttext").val(), commentid: CommentID }),
                type: "POST",
                success: function (data) {
                    //window.location.reload();
                    LoadComments();
                    CancelEditComments();
                    //$("#btnTempClick").click();
                }
            });

        }
        
        //---------------------------------
        function SetCalender() {
            $('#txtTCLDate').datetimepicker({ datepicker: true, timepicker: false, allowBlank: true, defaultSelect: false, validateOnBlur: false, format: 'd-M-Y', formatDate: 'd-M-Y' });
        }
        SetCalender();
        //---------------------------------
       
</script>

</body>
</html>
