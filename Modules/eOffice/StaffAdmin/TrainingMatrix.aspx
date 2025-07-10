<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="TrainingMatrix.aspx.cs" Inherits="Emtm_TrainingMatrix" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master"%>
<%@ Register Src="~/Modules/eOffice/StaffAdmin/Hr_TrainingMainMenu.ascx" TagName="Hr_TrainingMainMenu" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

     
 <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
 <script type="text/ecmascript">
    var ScreenW=window.screen.availWidth;
    var ScreenH=window.screen.availHeight; 
</script>
    <style type="text/css">
        .bordered tr td
        {
            border:solid 1px #c2c2c2;
            padding:5px;
        }
        .group
        {
            color:maroon;
            font-size:15px;
            font-weight:bold;
            padding:5px;
        }
        .tgroup
        {
            width:55px;
            background-color:#c2c2c2;
        }
        .tname
        {
            width:320px;
            background-color:rgba(243, 250, 229, 1);
        }
        .interval
        {
            width:60px;
            text-align:center;
        }
        .posgroup
        {
            width:100px;
            overflow-wrap:break-word;
            word-break:break-all;
        }
        .posgroup:hover
        {
            background-color:#feeabb;
            cursor:pointer;
        }
        .assigned {
            background-color: #4da312;
        }
        
        .header
        {
            background-color:rgba(181, 218, 253, 1);
            font-weight:bold;
        }
        .data
        {
            font-size:8px;
            color:#c2c2c2;
        }
        .External
        {
            background-color:#f8a4a4 !important;
        }
        .Internal
        {
             background-color:#43d89d !important;
        }
        .assign
        {
            border:none;
            background:#4da312;
            padding:5px;
            width:80px;
            color:white;
        }
        .remove
        {
            border:none;
            background:#f86262;
            padding:5px;
            width:80px;
            color:white;
        }
        .tempprocess
        {
            background:#ffd800;

        }
    </style>

    <div style="font-family:Arial;font-size:12px;">
            <table width="100%">
                    <tr>
                       
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                             <div>
                                <uc1:Hr_TrainingMainMenu ID="Emtm_Hr_TrainingMainMenu1" runat="server" />
                             </div> 
                            </td>
                        </tr>
                        
                        </table> 
                        <div style="height:25px; overflow-y:scroll; overflow-x:hidden">
                        <asp:Literal runat="server" ID="ltheader"></asp:Literal>
                        </div>
                        <div style="height:400px; overflow-y:scroll; overflow-x:hidden">
                            <asp:Literal runat="server" ID="litdata"></asp:Literal>
                        </div>  
                             <div style="padding:5px;">
                                 <asp:Button runat="server" ID="btnAllowedit" OnClick="btnAllowedit_Click" CssClass="btn" Text="Edit" />
                                 </div>
                        <div style="padding:5px; background-color:#ecf880; border-top:solid 1px #e8e2e2">
                                    &nbsp;<asp:Label ID="lbl_msg" runat="server" ForeColor="#C00000"></asp:Label>
                        </div> 
                        </td>
                        </tr>
                </table>
    </div>
    <script type="text/javascript">
        var last;
        $(document).ready(function () {
            $(".action").click(function (event) {
                $(last).removeClass('tempprocess');
                $(this).addClass('tempprocess');
                last = this;
                AskPanel(event,this);
            });
        });

        function AskPanel(event,cell)
        {
            $("#idaction").css('top', event.clientY + 10 + 'px') ;
            $("#idaction").css('left', event.clientX - 150 + 'px');
            $("#idaction").show();
        }

        function Assign_Click() {
            postdata("A");            
        }

        function Remove_Click() {
            postdata("R");
        }
        function postdata(mode)
        {
            var tid=$(last).attr('tid');
            var gid = $(last).attr('gid');

            $.ajax({
                url: 'Emtm_TrainingMatrix.aspx',
                data: {
                    mode: "update",
                    ar: mode,
                    tid: tid,
                    gid: gid
                },
                error: function () {
                    alert('Error.');
                },
                success: function (data) {
                    if (data == "Y")
                    {
                        if (mode=="A")
                            $(last).addClass('assigned');
                        else
                            $(last).removeClass('assigned');
                    }
                },
                complete: function () {
                    $(last).removeClass('tempprocess');
                    $("#idaction").hide();
                    last = null;
                },
                type: 'POST'
            });
        }

        function ShowDetails(Tid, Gid) {
            window.open('Emtm_TrainingMatrixDetails.aspx?Tid='+Tid+'&Gid='+Gid);
        }
    </script> 
        <div style="width:100px; padding:10px; background-color:#feeabb; border:solid 1px #000000; text-align:center; vertical-align:middle; display:none; position:absolute; top:100px; left:100px;" id="idaction">
            <input type="button" value="Assign" class="assign" onclick="Assign_Click()" />
            <br />
            <br />
            <input type="button" value="Remove" class="remove" onclick="Remove_Click()"  />
        </div>
      
   </asp:Content>