<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpenVNA.aspx.cs" Inherits="OpenVNA" Title="Navigation Audit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Circular Form</title>

     
    <style type="text/css">
        .highlight
        {
            margin:3px 0px 3px 0px;
            background-color:#F0F5FF;
        }
        .highlight:hover
        {
           background-color:#FFB2D1;
        }
        
       *{
          font-family:calibri;  
          font-size:14px;
        }
        body
        {
            margin:0px;
        }
        .selectedrow
        {
            background-color : lightgray;
            color :White; 
            cursor:pointer;
        }
        .row
        {
            background-color : White;
            color :Black;
            cursor:pointer; 
        }
        hr
        {
            margin:0px;
            padding:2px;
        }
        .btn
        {
            background-color:#005CE6;
            color:White;
            border:solid 1px #005CE6;
            padding:4px;
        }
        .btnred
        {
            background-color:red;
            color:White;
            border:solid 1px red;
            padding:4px;
        }
        
       .aquaScroll {
          scrollbar-base-color: bisque;
          scrollbar-arrow-color: #7094FF;
          border-color: orange;
          overflow-x:hidden; 
          overflow-y:scroll;
        }
        .changed
        {
            border:solid 1px red;
        }
        .saved
        {
            border:solid 1px green;
        }
    </style>
    <script type="text/javascript" src="../eReports/JS/jquery.min.js" ></script>
    <script type="text/javascript" src="../eReports/JS/KPIScript.js"></script>

    <link rel="stylesheet" type="text/css" href="../eReports/css/jquery.datetimepicker.css"/>
    
     <script type="text/javascript">
         function SetValue(qid) {
             $("#txtQID").val(qid);
             $("#btnPost").click();
         }
         $(window).resize(function () {
             $("#filler").height($("#top").height());
         });
    </script>
     <script type="text/javascript">
        function ShowDiv(ctl) {
            $("#dv_" + $(ctl).attr("qid")).show();
        }

        
        function ShowGuidance(ctl) 
        {
            $("#dvG").slideDown();
            $("#gtext").html(
                    $("#gv_" + $(ctl).attr("qid")).html()
                );
        }

        function SaveData(ctl) {

            var vnaid=<%=VNAID%>;
            var vsl='<%=VESSELCODE%>';
            var qid = $(ctl).attr("qid");
            var rad = $("input[name=comply_" + qid + "]:checked");
            var comply = rad.attr("arg");
            if(comply==null)
                comply='';
            var text = $("#txt_" + qid).val();
            
              $.ajax({
                  type: "POST",
                  url: "OpenVNA.aspx",
                  cache:false,
                  data: { 
                  vnaid: vnaid, 
                  comply: comply, 
                  qid: qid, 
                  vsl: vsl, 
                  text: text 
                  }
                          })
              .done(function (msg) {
//alert(msg);
                   $(ctl).removeClass("changed");
                   $(ctl).addClass("saved");
            
              });

         
        }

        function ChangeClass(ctl)
        {
            $(ctl).removeClass("saved");
            $(ctl).addClass("changed");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sd1"></asp:ScriptManager>
            <div style="position:fixed;top:0px; left:0px;width:100%;" id="top">
                <table cellpadding="0" cellspacing="0" style="width: 100%;border-collapse:collapse; text-align: center;">
            <tr>
                <td style="background-color:#7094FF;text-align: center; padding:8px; font-size:14px; color:White; ">
                    <b>Vessel Navigation Audit : <asp:Label runat="server" ID="lblNANo"></asp:Label> </b>                    
                </td>
            </tr>
            
            <tr>
                <td style="background-color:#E2EAFF;text-align: center; padding:8px; font-size:14px; color:#666666; ">
                    <table style="border-collapse:collapse;width:100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                    <td>Audit Duration :</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFromDate" MaxLength="15" Width='80px' CssClass="withcal" ValidationGroup="bg1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txtFromDate" ValidationGroup="bg1"></asp:RequiredFieldValidator>
                        
                        <asp:TextBox runat="server" ID="txtToDate" MaxLength="15" Width='80px' CssClass="withcal" ValidationGroup="bg1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtToDate" ValidationGroup="bg1"></asp:RequiredFieldValidator>
                    </td> 
                    <td>Port From :</td>
                    <td><asp:TextBox runat="server" ID="txtPortFrom" MaxLength="50" Width='150px' ValidationGroup="bg1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtPortFrom" ValidationGroup="bg1"></asp:RequiredFieldValidator>
                    </td>
                    <td>Port To :</td>
                    <td><asp:TextBox runat="server" ID="txtPortTo" MaxLength="50" Width='150px' ValidationGroup="bg1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtPortTo" ValidationGroup="bg1"></asp:RequiredFieldValidator>
                    </td>
                    <td>Master Crew # :</td>
                    <td><asp:TextBox runat="server" ID="txtMasterCrewNo" MaxLength="6" Width='70px' ValidationGroup="bg1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtMasterCrewNo" ValidationGroup="bg1"></asp:RequiredFieldValidator>
                    </td>
                    <td>Master Name :</td>
                    <td><asp:TextBox runat="server" ID="txtMasterName" MaxLength="50" Width='200px' ValidationGroup="bg1"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="txtMasterName" ValidationGroup="bg1"></asp:RequiredFieldValidator>
                    </td>
                    <td><asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSaveMaster_Click" class="btn" width="100px" ValidationGroup="bg1"/></td>
                    </tr>
                    </table>
                </td>
            </tr>
            </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%;border-collapse:collapse; text-align: center;">
            <tr>
                <td style="background-color:#7094FF;text-align: left; padding:8px; font-size:14px; color:white; ">
                Filter :&nbsp; <asp:DropDownList runat="server" ID="ddlShowMode" AutoPostBack="true" OnSelectedIndexChanged="ddlShowMode_OnSelectedIndexChanged" Width="250px">
                            <asp:ListItem Text="Show All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Questions Incomplete" Value="4" Selected></asp:ListItem>
                            <asp:ListItem Text="Without Ship Remarks" Value="3"></asp:ListItem>
                            <asp:ListItem Text="With Ship Remarks" Value="1"></asp:ListItem>
                            <asp:ListItem Text="With Office Remarks" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                 </td>
            </tr>
            </table>
                <table cellpadding="7" cellspacing="0" style="width: 100%;border-collapse:collapse; text-align:center; border:solid 1px white;" bordercolor="white" border="1">
                <tr>
                    <td style="background-color:#7094FF;border:solid 1px white;text-align: center; font-size:14px; color:white; width:70px;">Sr#</td>
                    <td style="background-color:#7094FF;border:solid 1px white;text-align: left; font-size:14px; color:white; ">Question <span style="color:Red;font-weight:bold;">( If answer is NS , NA , No then remark is required. )</span></td>
                    <td style="background-color:#7094FF;border:solid 1px white;text-align: center; font-size:14px; color:white; width:200px;">Complied</td>
                    <%--<td style="background-color:#7094FF;border:solid 1px white;text-align: center; font-size:14px; color:white; width:80px;">Last Audit</td>--%>
                </tr>
                </table>
            </div>
            <div style="padding-bottom:100px;">
            <div style="height:150px" id="filler">
            &nbsp;
            </div>
            <table cellpadding="7" cellspacing="0" style="width: 100%;border-collapse:collapse; text-align:center; border:solid 1px white;" bordercolor="#dddddd" border="1">
            <asp:Repeater ID="rptData" runat="server">
                <ItemTemplate>
                <tr>
                    <td style="text-align: left; font-size:14px; width:70px; vertical-align:top;">
                        <div><%#Eval("Qno").ToString()%></div>
                    <span style='color:Blue; cursor:pointer;' qid='<%#Eval("QID").ToString()%>' onclick='ShowGuidance(this);'><u>Guidance</u>
                    <span id='gv_<%#Eval("QID").ToString()%>' style='display:none'><%#Eval("Guidance").ToString()%></span>
                    </span>
                    </td>
                    <td style="text-align: left; font-size:14px;vertical-align:top;">
                        <div><%#Eval("QText").ToString()%></div>
                        <div id='dv_<%#Eval("QID").ToString()%>' style='<%#((Eval("ShipComments").ToString().Trim()=="")?"display:none":"")%>'>
                        <div style="padding:2px;overflow:auto; overflow:hidden;">
                               <div runat="server" visible='<%#!Closed%>'>
                                    <textarea class="saved" onclick='ChangeClass(this);' id='<%#"txt_" + Eval("QID").ToString()%>' qid='<%#Eval("QID").ToString()%>' style="background-color:#FFFFE6 ;width:99%; color:#0099FF; height:50px" onblur="SaveData(this);" ><%#Eval("ShipComments").ToString()%></textarea>
                               </div>
                               <div runat="server" visible='<%#Closed%>'>
                                    <div style='<%#((Eval("ShipComments").ToString().Trim()=="")?"display:none":"")%>'><span style='color:green'><i><b>Ship Comments :</b> <%#Eval("ShipComments").ToString()%></i></span></div>
                               </div>
                        </div>
                    </div>
                        <div style='<%#((Eval("OfficeComments").ToString().Trim()=="")?"display:none":"")%>'><span style='color:#E64848'><i><b>Office Comments :</b> <%#Eval("OfficeComments").ToString()%></i></span></div>
                    </td>
                    <td style="text-align: center; font-size:14px; width:200px;vertical-align:top;">
                        <div runat="server" visible='<%#!Closed%>'>
                            <input type="radio" id='<%#"radNS_" + Eval("QID").ToString()%>' arg="NS" qid='<%#Eval("QID").ToString()%>' onclick='ShowDiv(this);SaveData(this);' name='<%#"comply_" + Eval("QID").ToString()%>' <%#((Eval("Comply").ToString()=="NS")?"checked":"")%> style='color:Blue'/>
                            <label for='<%#"radNS_" + Eval("QID").ToString()%>'>NS</label>
                            <input type="radio" id='<%#"radNA_" + Eval("QID").ToString()%>' arg="NA" qid='<%#Eval("QID").ToString()%>' onclick='ShowDiv(this);SaveData(this);' name='<%#"comply_" + Eval("QID").ToString()%>' <%#((Eval("Comply").ToString()=="NA")?"checked":"")%> style='color:Blue'/>
                            <label for="<%#"radNA_" + Eval("QID").ToString()%>">NA</label>
                            <input type="radio" id='<%#"radYES_" + Eval("QID").ToString()%>' arg="YES" qid='<%#Eval("QID").ToString()%>' onclick='ShowDiv(this);SaveData(this);' name='<%#"comply_" + Eval("QID").ToString()%>' <%#((Eval("Comply").ToString()=="YES")?"checked":"")%> style='color:Green'/>
                            <label for="<%#"radYES_" + Eval("QID").ToString()%>">YES</label>
                            <input type="radio" id='<%#"radNo_" + Eval("QID").ToString()%>' arg="NO" qid='<%#Eval("QID").ToString()%>' onclick='ShowDiv(this);SaveData(this);' name='<%#"comply_" + Eval("QID").ToString()%>' <%#((Eval("Comply").ToString()=="NO")?"checked":"")%> style='color:Red'/>
                            <label for="<%#"radNo_" + Eval("QID").ToString()%>">NO</label>
                        </div>
                        <div runat="server" visible='<%#Closed%>'><b><%#Eval("Comply").ToString()%></b></div>
                    </td>
                    <%--<td style="text-align: center; font-size:14px; width:80px; vertical-align:top;"> <%#Common.ToDateString(Eval("lastDate"))%></td>--%>
                </tr>
               
                </ItemTemplate>
            </asp:Repeater>
            </table>
            <div style="background-color:#7094FF;text-align: center; padding:8px; font-size:14px; color:White; margin-top:30px; ">
                Closure Remarks from Office <asp:Label runat="server" ID="lblClosedByOn" ></asp:Label>
            </div>
            <div style=" font-family:Verdana; font-size:11px; border:solid 1px #7094FF; min-height:20px; padding:5px; font-style:italic; ">
                <asp:Label runat="server" ID="txtGenComments" style="margin-top:5px"></asp:Label>
            </div>
            </div>

            <div style="position:fixed;bottom:0px; left:0px; text-align:center;background-color:#E2EAFF; width:100%; padding:5px;">
                 <div style="text-align:left; margin-top:5px;">
                 <asp:Label runat="server" ID="lblMessage" Font-Size="20px" Font-Bold="true" style="float:left"></asp:Label>
                 <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="btnred" Width="73px"  OnClientClick="window.close();" style="float:right; margin-right:10px;"/>
                 <asp:Button ID="btnExport" runat="server" CssClass="btn" OnClick="btnExport_Click" Text=" Export " style="float:right; margin-right:10px;" />
                </div>
            </div>

            <div style="position:fixed;top:80px;left:0px; height :100%; width:100%; display:none;" id="dvG" ">
            <center>
                <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
                <div style="position:relative;width:80%; text-align :center;background : white; z-index:150;top:100px; border:solid 10px black;">
                <center >
                <table cellpadding="0" cellspacing="0" style="width: 100%;border-collapse:collapse; text-align: center;">
                <tr>
                    <td style="background-color:#7094FF;text-align: center; padding:8px; font-size:14px; color:White; ">
                        <b>Guidance</b>
                    </td>
                </tr>
                <tr>
                <td>
                    <div style="height:250px; white-space:95%; overflow-y:scroll; overflow-x:hidden; text-align:left; padding:5px; border-bottom:solid 1px #dddddd" id='gtext'>
                    
                    </div>
                </td>
                </tr>
                <tr>
                <td style="padding:5px">
                    <button value="" class="btnred" Width="90px" onclick='$("#dvG").slideUp();' style="float:right;" >Close</button>
                </td>
                </tr>
                </table>
                </center>
           </div>
           </center>
           </div>
      
        <div style="padding-left:5px;">
        
        </div>
        <script type="text/javascript">
            var qid = 'rad<%=QID%>';
            if (document.getElementById(qid) != null)
                document.getElementById(qid).setAttribute("checked", true);
        </script>
        <script type="text/javascript" src="../eReports/JS/jquery.datetimepicker.js"></script>
        <script type="text/javascript">
            $('.withcal').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
            $("#filler").height($("#top").height());
        </script>
    </form>
</body>
</html>
