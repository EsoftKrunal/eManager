<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RCA.aspx.cs" Inherits="eReports_S115_eReport_S115" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
      <%--<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>--%>

        <link href="//netdna.bootstrapcdn.com/bootstrap/3.1.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrapcss">

	 <style type="text/css">
    .bs-wizard {margin-top: 15px;}

/*Form Wizard*/
.bs-wizard {border-bottom: solid 1px #e0e0e0; padding: 0 0 10px 0;}
.bs-wizard > .bs-wizard-step {padding: 0; position: relative;}
.bs-wizard > .bs-wizard-step + .bs-wizard-step {}
.bs-wizard > .bs-wizard-step .bs-wizard-stepnum {color: #595959; font-size: 16px; margin-bottom: 5px;}
.bs-wizard > .bs-wizard-step .bs-wizard-info {color: #999; font-size: 14px;}
.bs-wizard > .bs-wizard-step > .bs-wizard-dot {position: absolute; width: 30px; height: 30px; display: block; background: #fbe8aa; top: 45px; left: 50%; margin-top: -15px; margin-left: -15px; border-radius: 50%;} 
.bs-wizard > .bs-wizard-step > .bs-wizard-dot:after {content: ' '; width: 14px; height: 14px; background: #fbbd19; border-radius: 50px; position: absolute; top: 8px; left: 8px; } 
.bs-wizard > .bs-wizard-step > .progress {position: relative; border-radius: 0px; height: 8px; box-shadow: none; margin: 20px 0;}
.bs-wizard > .bs-wizard-step > .progress > .progress-bar {width:0px; box-shadow: none; background: #fbe8aa;}
.bs-wizard > .bs-wizard-step.complete > .progress > .progress-bar {width:100%;}
.bs-wizard > .bs-wizard-step.active > .progress > .progress-bar {width:50%;}
.bs-wizard > .bs-wizard-step:first-child.active > .progress > .progress-bar {width:0%;}
.bs-wizard > .bs-wizard-step:last-child.active > .progress > .progress-bar {width: 100%;}
.bs-wizard > .bs-wizard-step.disabled > .bs-wizard-dot {background-color: #f5f5f5;}
.bs-wizard > .bs-wizard-step.disabled > .bs-wizard-dot:after {opacity: 0;}
.bs-wizard > .bs-wizard-step:first-child  > .progress {left: 50%; width: 50%;}
.bs-wizard > .bs-wizard-step:last-child  > .progress {width: 50%;}
.bs-wizard > .bs-wizard-step.disabled a.bs-wizard-dot{ pointer-events: none; }
/*END Form Wizard*/
	     .tblHeader {
             width:100%;border-collapse:collapse; background-color:#2b9de0; color: #fff;  height:25px;font-weight:bold;
	     }
        

    </style>
     <%--<script src="//code.jquery.com/jquery-1.10.2.min.js"></script>--%>
    <%--<script src="//netdna.bootstrapcdn.com/bootstrap/3.1.0/js/bootstrap.min.js"></script>--%>
    
    <script type="text/javascript" src="../JS/jquery.min.js"></script>

    <script type="text/javascript">
        //window.alert = function () { };
        //var defaultCSS = document.getElementById('bootstrapcss');
        //function changeCSS(css) {
        //    if (css) $('head > link').filter(':first').replaceWith('<link rel="stylesheet" href="' + css + '" type="text/css" />');
        //    else $('head > link').filter(':first').replaceWith(defaultCSS);
        //}
        //$(document).ready(function () {
        //    var iframe_height = parseInt($('html').height());
        //    window.parent.postMessage(iframe_height, 'https://bootsnipp.com');
        //});
    </script>

    <link rel="stylesheet" type="text/css" href="../css/StyleSheet.css"/>
    <link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css"/>
     <link rel="stylesheet" type="text/css" href="../../../HRD/Styles/StyleSheet.css"/>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $(".TL").click(function () {
                $("#hfdTeamLeadValue").val($(this).val());
            });
        });
        </script>
</head>


<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row bs-wizard" style="border-bottom:0;">
                <%--complete,active,disabled--%>
                <div class="col-xs-3 bs-wizard-step <%=(Stage==1)?"active":"complete" %> ">
                  <div class="text-center bs-wizard-stepnum">1. Assign RCA</div>
                  <div class="progress"><div class="progress-bar"></div></div>
                  <a href="#" class="bs-wizard-dot"></a>
                  <%--<div class="bs-wizard-info text-center">Lorem ipsum dolor sit amet.</div>--%>
                </div>
                
                <div class="col-xs-3 bs-wizard-step <%=((Stage==2)?"active": (Stage<=2)?"disabled":"complete") %>"><!-- complete -->
                  <div class="text-center bs-wizard-stepnum">2. Final RCA</div>
                  <div class="progress"><div class="progress-bar"></div></div>
                  <a href="#" class="bs-wizard-dot"></a>
                  <%--<div class="bs-wizard-info text-center">Nam mollis tristique erat vel tristique. Aliquam erat volutpat. Mauris et vestibulum nisi. Duis molestie nisl sed scelerisque vestibulum. Nam placerat tristique placerat</div>--%>
                </div>
                
                <div class="col-xs-3 bs-wizard-step <%=((Stage==3)?"active": (Stage<=3)?"disabled":"complete") %>"><!-- complete -->
                  <div class="text-center bs-wizard-stepnum">3. RCA Approval</div>
                  <div class="progress"><div class="progress-bar"></div></div>
                  <a href="#" class="bs-wizard-dot"></a>
                  <%--<div class="bs-wizard-info text-center">Integer semper dolor ac auctor rutrum. Duis porta ipsum vitae mi bibendum bibendum</div>--%>
                </div>
                
                <div class="col-xs-3 bs-wizard-step <%=((Stage==4)?"active": (Stage<=4)?"disabled":"complete") %>"><!-- active -->
                  <div class="text-center bs-wizard-stepnum">4. RCA Closure</div>
                  <div class="progress"><div class="progress-bar"></div></div>
                  <a href="#" class="bs-wizard-dot"></a>
                  <%--<div class="bs-wizard-info text-center"> Curabitur mollis magna at blandit vestibulum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae</div>--%>
                </div>
            </div>
	</div>


        <div style="height:335px;overflow-x:hidden;overflow-y:scroll;">

        <div class="container" style='<%= (Stage>=1)?"display:block":"display:none" %>'>
            <h4 class="rca-sec"> 1. Assign RCA</h4>

            <table style="width:70%;margin:0px auto;" class="table" >
                <col width="20%" />
                <col />
                <tr>
                    <td> Severity </td>
                    <td>
                        <asp:RadioButtonList ID="rdoListSeverity" runat="server" RepeatDirection="Horizontal" Width="250px">
                            <asp:ListItem Value="1" Text="Minor"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Major"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Severe"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>Focal Point</td>
                    <td>
                        <asp:TextBox ID="txtFocalPoint" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>RCA Team</td>
                    <td>
                        <asp:LinkButton ID="lnkAddTeamMember" runat="server" Text="Add Team Member" OnClick="lnkAddTeamMember_OnClick"></asp:LinkButton>
                        <asp:HiddenField ID="hfdTeamLeadValue" runat="server" />
                        <br />
                        <table cellpadding="3" cellspacing="2" width="100%" class="tblHeader">
                            <col width="17%" />
                            <col width="35%" />
                          <%--  <col />--%>
                            <col width="7%" />
                            <tr class= "headerstylegrid">
                                <td style="color:White; text-align:left;">Team Leader</td>
                                <td style="color:White; text-align:left;">Employee Name</td>
                                <%--<td style="color:White; text-align:left;">Position</td>--%>
                                <td style="color:White; text-align:left;">Delete</td>
                            </tr>
                        </table>
                        <table cellpadding="2" cellspacing="2" width="100%" class="tblTeam">
                            <col width="17%" />
                            <col width="35%" />
                           <%-- <col />--%>
                            <col width="7%" />
                        </table>
                        <asp:Repeater ID="rptTeammembers" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align:center;width:17%;">
                                        <input type="radio"  name="TL" class="TL" value="<%#Eval("EmpID") %>" <%# (Eval("EmpID").ToString()==hfdTeamLeadValue.Value)?"checked":"aa" %> />                                        
                                    </td>
                                    <td style="text-align:left;width:35%;"><%#Eval("Empname") %></td>
                                    <%--<td><%#Eval("PositionName") %></td>--%>
                                    <td style="text-align:center;width:7%;">
                                        <asp:ImageButton ID="btnDeleterTeamMember" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="return confirm('Are you surre to delete?')" OnClick="btnDeleterTeamMember_OnClick" CommandArgument='<%#Eval("EmpID") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        </table>
                    </td>
                </tr>                
                <tr>
                    <td>Target Closure Date</td>
                    <td>
                        <asp:TextBox ID="txtTargetClosureDate" runat="server" CssClass="date_only" Width="85px" MaxLength="11"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSaveRcaAssigne" runat="server" Text="Save" class="btn btn-dark" OnClick="btnSaveRcaAssigne_OnClick" />
                        <asp:Label ID="lblMsg" runat="server" CssClass="has-error" style="color:red;"></asp:Label>
                    </td>
                </tr>
                
            </table>
        </div>

        <div class="container" style='<%= (Stage>=2)?"display:block":"display:none" %>'>
            <h4 class="rca-sec"> 2. Final RCA</h4>

            <table style="width:70%;margin:0px auto;" class="table" >
                <tr>
                    <td>
                        <a target="_blank" href="RCAanalysis.aspx?ReportId=<%=ReportId %>&VesselCode=<%=VesselCode %>&focalpoint=<%=txtFocalPoint.Text.Trim() %>"> Open Root Cause Analysis </a>
                    </td>
                </tr>
            </table>
        </div>

        <div class="container" style='<%= (Stage>=3)?"display:block":"display:none" %>'>
            <h4> 3. RCA Approval</h4>
        </div>
        <div class="container" style='<%= (Stage>=4)?"display:block":"display:none" %>'>
            <h4> 4. RCA Closure</h4>
        </div>

            </div>
        <%--------------------------------------------------------------------------------------------------------------------------------%>
        <div style="position:absolute;top:0px;left:0px; height :560px; width:100%; " id="divAddTeamMember" runat="server" visible="false">
        <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:90%;max-height:400px;overflow-x:hidden;overflow-y:hidden; padding :5px; text-align :center;background : white; z-index:150;top:30px; border:solid 0px black;">
            <center >
            <div class='text headerband'>Add Team Member
            
            </div>
            <div style="height:300px;overflow-x:hidden;overflow-y:scroll;padding:10px;">
            <%--<asp:UpdatePanel runat="server" ID="UpdatePanel7">
            <ContentTemplate>--%>
                
                <table class="table">
                    <col width="100%" />
                 <%--   <col width="25%" />
                    <col width="25%" />
                    <col  />
                    <tr>
                        <td>Fleet Manager</td>
                        <td>Technical Suptd.</td>
                        <td>Marine Suptd.</td>
                        <td>Management</td>
                    </tr>--%>
                     <tr>
                      <%--  <td>
                            <asp:CheckBoxList ID="chkListFleetManager" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkListTechnicalSuptd" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>
                        </td>
                         <td>
                            <asp:CheckBoxList ID="chkListMaringSuptd" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>
                        </td>--%>
                         <td>
                            <asp:CheckBoxList ID="chkListManagement" runat="server" RepeatDirection="Horizontal" RepeatColumns="3"></asp:CheckBoxList>
                        </td>

                    </tr>
                </table>
                </div>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            
            <div style="text-align:center; position:relative;">
                <asp:Button ID="btnSaveTeamMember" runat="server" Text="Save" CssClass="btn btn-dark" OnClick="btnSaveTeamMember_OnClick" />
                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-dark" OnClick="btnClose_OnClick" />
            </div>
            <div style="text-align:center; position:relative;">
                &nbsp;<asp:Label ID="lblMsgAddMembers" runat="server" CssClass="has-error" style="color:red;"></asp:Label>
            </div>
            </center>
         </div>
         </center>
    </div>

        <script type="text/javascript" src="../JS/jquery.datetimepicker.js"></script>
        <script type="text/javascript">

            function SetCalender() {
                $('.date_only').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
                $('.date_time').datetimepicker({ format: 'd-M-Y H:i', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
            }
            SetCalender();
        </script>
        
    </form>
</body>
</html>
