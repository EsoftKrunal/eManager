<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RCAanalysis.aspx.cs" Inherits="RCAanalysis" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>EMANAGER</title>
    <link href="../CSS/treestyle.css" rel="stylesheet" />
    <link href="../CSS/scroll.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=PT+Sans" rel="stylesheet">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" >
</head>
<body runat="server" >
    <form id="form1" runat="server">
        <div class="boxed" style="background-color:#2b9de0">
            <span class="causeheading" style="padding:10px;border-bottom:none;color:white;font-size:22px;display;"> Focal Point - <asp:Literal ID="litFocalPoint" runat="server"></asp:Literal></span>
        </div>
        <div class="container" onclick="hideaction();">
            <asp:Literal ID="litTree" runat="server"></asp:Literal>            
       </div>

        <div class="actionpanel" onmouseover="SetOver();" onmouseout="SetOut();">
    <div class="actionrow" onclick="editcause();"><i class="fa fa-edit"></i>Edit this Cause</div>
    <div class="actionrow" onclick="removecause();"><i class="fa fa-trash"></i>Remove this Cause</div>
    <div class="actionrow" onclick="addnewcause();"><i class="fa fa-plus"></i>Add Cause</div>
    <div class="actionrow" onclick="terminatecause();"><i class="fa fa-times"></i>Terminate Cause</div>
    <div class="actionrow" onclick="setrootcause();"><i class="fa fa-exclamation-circle"></i>Set this as Root Cause</div>
</div>
<div id="actiontarget">
    <div class="header">
    <h1>&nbsp;-</h1>    
    </div>
    <i class="fa fa-times fa-2x close" ></i>    
    <h3>Select From Below List</h3>
    <div style="padding:10px;position:relative;">
        <input id="causeitemfilter" placeholder="Enter your text here .." />
        <span class="inputafter"></span>
    </div>
    <div class="scrollbar" id="style-9" style="margin:11px;height:400px;overflow-x:hidden; width:96%;border: solid 1px #f1f1f1;">
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div>
        <div class="causeitem" key='' onclick="selectcause(this);">root cause 0001</div> 
    </div>
    
    <div style="position:absolute;left:0px;right:0px;bottom:0px;">
    <h3>If not found ! Enter Below ..</h3>
    <div style="margin:15px; text-align:right;">
        <textarea id="txtcausetext"></textarea>
        <input type="button" value="Save" class="btn" style="margin-top:10px;" onclick="SaveCause();"/>
    </div>   
    </div>     
   <div>
   </div> 
</div>
</form>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script src="../JS/MultiNestedList.js"></script>    
    <script type="text/javascript">
    //----------------------------
    var over = 0;
        var analysisid = 0;
        var mode = '';
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

        //<div class="actionpanel" onmouseover="SetOver();" onmouseout="SetOut();">
        //    <div class="actionrow" onclick="editcause();"><i class="fa fa-edit"></i>Edit this Cause</div>
        //    <div class="actionrow" onclick="removecause();"><i class="fa fa-trash"></i>Remove this Cause</div>
        //    <div class="actionrow" onclick="addnewcause();"><i class="fa fa-plus"></i>Add New Cause</div>
        //    <div class="actionrow" onclick="terminatecause();"><i class="fa fa-times"></i>Terminate Cause</div>
        //    <div class="actionrow" onclick="setrootcause();"><i class="fa fa-exclamation-circle"></i>Set this as Root Cause</div>
    //----------------------------
        function editcause() {
            Show();
            mode = 'E';
            $("#actiontarget h1").html("Edit Cause");
            $("#causeitemfilter").val($("#txtcausetext").val());
            $("#causeitemfilter").select();
            $(".actionpanel").slideUp();
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
                        window.location.reload();
                    }

                });
            }
        }
        function addnewcause() {
            Show();
            mode = 'A';
            $("#actiontarget h1").html("Add Cause");
            $("#txtcausetext").val("");
            $("#causeitemfilter").focus();
            $(".actionpanel").slideUp();
        }
        function terminatecause() {
            $.ajax({
                url: "RCAanalysis.aspx/terminatecause",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ aid: analysisid }),
                type: "POST",
                success: function (data) {
                    window.location.reload();
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
                    window.location.reload();
                }

            });
        }


    function saveEditedCause()
    {
        $.ajax({
            url: "RCAanalysis.aspx/updatecause",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ aid: analysisid, cause:$("#txtcausetext").val() }),
            type: "POST",
            success: function (data) {
                window.location.reload();
            }

        });
    }
        function SaveCause()
        {

            var reportID =<%=ReportID %>;            
            var vesselCode = '<%=VesselCode %>';
            
        $.ajax({
            url: "RCAanalysis.aspx/savecause",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ aid: analysisid, reportid: reportID, vesselcode: vesselCode, cause: $("#txtcausetext").val(),mode:mode }),
            type: "POST",
            success: function (data) {
                
                window.location.reload();
            },
            error: function (res) {
                
                
            },
            complete: function (res) {
                
            }

        });
    }
    

        function selectcause(ctl) {
            $("#txtcausetext").val($(ctl).html());
        }
    //----------------------------
</script>

</body>
</html>
