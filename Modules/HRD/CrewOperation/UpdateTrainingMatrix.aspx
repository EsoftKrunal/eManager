<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewTraining.master" AutoEventWireup="true" CodeFile="UpdateTrainingMatrix.aspx.cs" Inherits="UpdateTrainingMatrix"%>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
     <style type="text/css">
.hd
{
	background-color : #c2c2c2;
}
.hd1
{
	background-color : #4371a5;
	color:White;
}
a img
{
border:none;	
}
</style>
 <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
 <script type="text/ecmascript">
    var ScreenW=window.screen.availWidth;
    var ScreenH=window.screen.availHeight; 
    var seltd;
    function ShowAdd()
    {
        dvAddTr.style.left =event.clientX-600+'px';
        dvAddTr.style.top =(event.clientY+20)+'px';
        $("#dvAddTr").slideDown();
        document.getElementById("ctl00_ctl00_ContentMainMaster_contentPlaceHolder1_ddlCat").focus();
    }
    function hideAdd()
    {
        if(event.keyCode==27 )
            $("#dvAddTr").slideUp();
    }
    function hideAdd1()
    {   
        $("#dvAddTr").slideUp();
    }
    function hideUpdate(val)
    {   
        if(event.keyCode==27 )
            $("#dvSch").slideUp();
    }
    function DeleteTraining(id)
    {
        if(confirm('Are you sure to remove this training?'))
        { 
            document.getElementById("ctl00_ctl00_ContentMainMaster_contentPlaceHolder1_hfdDelId").value=id;
            document.getElementById("ctl00_ctl00_ContentMainMaster_contentPlaceHolder1_btnDelTraining").click();
        }
    }
    function goEdit(id)
    {   //alert('xxxx');
        document.getElementById("ctl00_ctl00_ContentMainMaster_contentPlaceHolder1_hfdEditId").value=id;
        document.getElementById("ctl00_ctl00_ContentMainMaster_contentPlaceHolder1_btnEditTraining").click();
    }
    function ShowMan(ctl,Tid,Rid)
    {
        if(seltd!=null) 
        {
            if(seltd.style.backgroundColor!=null)
            seltd.style.backgroundColor='';
        }
            
        var left=event.clientX;
        var top=event.clientY;
        
        if(left<ScreenW/2)
        {
            left=left+20;
        }
        else
        {
            left=left-20-150;
        }
        
        if(top<200)
        {
            top=top+20;
        }
        else
        {
            top=top-20-60;
        }
        dvMan.style.left =left+'px';
        dvMan.style.top = top+'px';
        $("#dvMan").slideDown();
        seltd=ctl;
        ctl.style.backgroundColor="Red";
        document.getElementById("ctl00_ctl00_ContentMainMaster_contentPlaceHolder1_hfdMTId").value=Tid;
        document.getElementById("ctl00_ctl00_ContentMainMaster_contentPlaceHolder1_hfdMRId").value=Rid;
        //document.getElementById("ctl00_contentPlaceHolder1_radMan").focus();
    }
    function hideMan()
    {
        if(event.keyCode==27 )
        {
            $("#dvMan").slideUp();
            if(seltd!=null) 
            {
            if(seltd.style.backgroundColor!=null)
            seltd.style.backgroundColor='';
            }
        }
    }
    function showUpdate(val)
    {
        document.getElementById("ctl00_ctl00_ContentMainMaster_contentPlaceHolder1_hfdTrainingId").value=val;
        dvSch.style.left =event.clientX-600+'px';
        dvSch.style.top =(event.clientY+20)+'px';
        $("#dvSch").slideDown();
        document.getElementById("ctl00_ctl00_ContentMainMaster_contentPlaceHolder1_txtSc1").focus();
    }
    function OpenViewTrainingMatricPopUp(TID,RankGroupID)
    {
        window.open('ViewTrainingMatrixPopUp.aspx?TID='+TID+'&RankGroupID='+RankGroupID+'','','');
    }
    
    
     
 </script>
      <asp:ScriptManagerProxy ID="ScriptManager1" runat="server"></asp:ScriptManagerProxy>
        <table align="center" width="100%" border="1" cellpadding="2" cellspacing="0" style="border-collapse :collapse">
            <tr>
                <td style=" text-align:left">
                     <table align="center" width="100%" border="0" cellpadding="2" cellspacing="0" style="border-collapse :collapse">
                         <tr>
                             <td style="text-align:right; width:150px;">Select Training Matrix :</td>
                             <td><asp:DropDownList ID="ddlTraining" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTraining_OnSelectedIndexChanged" Width="250px" CssClass="input_box" ></asp:DropDownList></td>
                             <td>
                                
                             </td>
                             <td style="text-align:right">
                                  <a onclick ="ShowAdd();" href ="#">
                                    <span id="divAddTraining" runat="server" ><input type="button" value="Add Trainings" class="btn" title="Add New Training." style="width:120px;"/></span>
                                  </a>
                                 <asp:Button ID="btnEdit" runat="server" Text=" Edit " OnClick="btnEdit_OnClick" CssClass="btn" style="margin-left:5px;" Width="60px" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_OnClick" CssClass="btn" style="margin-left:5px;" />
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_OnClick" CssClass="btn" style="margin-left:5px;" />

                             </td>
                         </tr>
                         </table>

                  
                
                </td>
            </tr>
            <tr>
            <td style="text-align :left;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <div id="divDate" style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 455px; " onscroll="SetScrollPos(this)">
                    <asp:Literal runat="server" ID="litTreaining"></asp:Literal>
                </div>
                 <!-- DIV FOR SHOW BOX TO SELECT MANDITORY / RECOMMENDED/ NA -->
                <div id="dvMan" onkeydown='hideMan();' style="border :solid 1px gray; border-bottom:solid 4px gray;border-right:solid 4px gray; width :200px; height :50px; padding :10px; position :absolute; top:400px; left:200px; background-color:#FFE4B5; display :none; text-align :center">
                    <%--
                    <asp:RadioButton runat="server" id="radMan" GroupName="man" Text="Manditory" AutoPostBack="false" ></asp:RadioButton><br />
                    <asp:RadioButton runat="server" id="radRec" GroupName="man" Text="Recommended" AutoPostBack="false" ></asp:RadioButton><br />
                    <asp:RadioButton runat="server" id="radNA" GroupName="man" Text="NA" AutoPostBack="false" ></asp:RadioButton>
                    --%>
                    <asp:Button ID="btnUnAssign" runat="server" CssClass="input_box" Text="Remove" style="width:70px;text-align:center" OnClick="SelModeCancelled" />
                    <br /><br />
                    <asp:Button ID="btnUpdate" runat="server" CssClass="input_box" Text="Assign" style="width:70px;text-align:center" OnClick="SelModeChanged" />
                    <asp:HiddenField runat="server" ID="hfdMTId" />
                    <asp:HiddenField runat="server" ID="hfdMRId" />
                </div>
                <!-- DIV FOR SHOW UPDATE SCHEDULE -->
                <div id="dvSch" onkeydown='hideUpdate();' style="border :solid 1px gray; border-bottom:solid 4px gray;border-right:solid 4px gray; width :200px; height :60px; padding :10px; position :absolute; top:400px; left:200px; background-color:#FFE4B5; display :none; text-align :center">
                Schedule :
                     <asp:TextBox runat="server" ID="txtSc1" MaxLength="3" Width="30px" CssClass="input_box" style="text-align:center"></asp:TextBox>
                     <asp:DropDownList ID="ddlSchedule1" runat="server" Width="40px" CssClass="input_box" >
                     <asp:ListItem Text="M" Value ="M"></asp:ListItem>
                     </asp:DropDownList><br /><br />
                     <asp:Button runat="server" ID="btnUpdateSch" CssClass="btn" Text="Update" style="width:80px; height :24px; margin :2px;" onclick="btnUpdateSch_Click" />
                     <asp:HiddenField runat="server" ID="hfdTrainingId" />
                     <span style="color:Red;padding-top:20px;float:left;">Press ESC key to Close</span>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            </tr>
        </table>
        
         <!-- DIV FOR SHOW ADD NEW TRAINING SCHEDULE -->
        <div id="dvAddTr" onkeydown='hideAdd();' style="border :solid 1px gray; border-bottom:solid 4px gray;border-right:solid 4px gray; width :800px; height :60px; padding :10px; position :absolute; top:400px; right:0px; background-color:#FFE4B5; display :none;">
        <asp:UpdatePanel runat="server" ID="t1">
        <ContentTemplate>
            <div style="padding:4px 15px 0px 15px; text-align:left">
                <asp:DropDownList ID="ddlCat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCat_OnSelectedIndexChanged" Width="250px" CssClass="input_box" ></asp:DropDownList>
                <asp:DropDownList ID="ddlATraining" runat="server" Width="360px" CssClass="input_box"></asp:DropDownList>
                Schedule :
                <asp:TextBox runat="server" ID="txtSc" MaxLength="3" Width="30px" CssClass="input_box" style="text-align:center"></asp:TextBox>
                <asp:DropDownList ID="ddlSchedule" runat="server" Width="40px" CssClass="input_box" >
                <asp:ListItem Text="M" Value ="M"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <div style="text-align :right; padding-top :5px;padding-right :5px">
                    <asp:Button runat="server" ID="btnAddTraining" CssClass="btn" Text="Save" style="width:70px; height :24px; background-position:3px; text-align:center; margin :2px; padding-right :5px; float:right;" onclick="btnAddTraining_Click" />
                    <asp:Button runat="server" ID="btnCancel" CssClass="btn" Text="Cancel" style="width:70px; height :24px; background-position:3px; text-align:center; margin :2px; float:right" OnClientClick="hideAdd1();return false;" />
                    <asp:Label runat="server" ID="lblMessTraining" Font-Bold="true" Font-Names="Verdana" style="float :left; width:260px; text-align :left "></asp:Label>
                    <asp:Label runat="server" ID="lblMTraining" Font-Bold="true" Font-Names="Verdana" style="float :left;" Text=""></asp:Label>
                    </div>
                </div>
                  <asp:Button runat="server" ID="btnDelTraining" Text ="Delete Training" onclick="btnDelTraining_Click" style="display :none" />
                  <asp:Button runat="server" ID="btnEditTraining" Text ="Edit Training" onclick="btnEditTraining_Click" style="display :none" />
                  <asp:HiddenField runat="server" ID="hfdDelId" />    
                  <asp:HiddenField runat="server" ID="hfdEditId" />    
                  <span style="color:Red;padding-top:47px;float:left;">Press ESC key to Close</span>
        </ContentTemplate>
        </asp:UpdatePanel>
        
        </div>
        
        <script  type="text/javascript">
            function setCookie(c_name, value, exdays) 
     {
         var exdate = new Date();
         exdate.setDate(exdate.getDate() + exdays);
         var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
         document.cookie = c_name + "=" + c_value;
     }
     function SetLastFocus(ctlid) 
     {
         pos = getCookie(ctlid);
         if (isNaN(pos))
         { pos = 0; }
         if (pos > 0) 
         {
             document.getElementById(ctlid).scrollTop = pos;
         }
     }
     function SetScrollPos(ctl) 
     {
         setCookie(ctl.id, ctl.scrollTop, 1);
     }
     function getCookie(c_name) {
     var i, x, y, ARRcookies = document.cookie.split(";");
     for (i = 0; i < ARRcookies.length; i++) {
         x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
         y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
         x = x.replace(/^\s+|\s+$/g, "");
         if (x == c_name) {
             return unescape(y);
         }
     }
 }
        </script>
       </asp:Content>
 