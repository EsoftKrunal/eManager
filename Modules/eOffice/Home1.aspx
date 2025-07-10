


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home1.aspx.cs" Inherits="emtm_Emtm_Home1" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script type="text/javascript" src="../JS/jquery-1.10.2.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet">
    <style type="text/css">
.btn{
    background-color:orange;
    color:white;
    border:none;
    padding:5px 10px 5px 10px;

}
.fade {
    animation: fadein 2s;
    -moz-animation: fadein 2s; /* Firefox */
    -webkit-animation: fadein 2s; /* Safari and Chrome */
    -o-animation: fadein 2s; /* Opera */
}
@keyframes fadein {
    from {
        opacity:0;
    }
    to {
        opacity:1;
    }
}
@-moz-keyframes fadein { /* Firefox */
    from {
        opacity:0;
    }
    to {
        opacity:1;
    }
}
@-webkit-keyframes fadein { /* Safari and Chrome */
    from {
        opacity:0;
    }
    to {
        opacity:1;
    }
}
@-o-keyframes fadein { /* Opera */
    from {
        opacity:0;
    }
    to {
        opacity: 1;
    }
}

        select,input[type='text'] 
        {
            height:27px;
            padding:3px;
            line-height:27px;
            border:solid 1px #e9e9e9;
            color:#333;
        }
        .kpiname {
            color: #333;
            cursor:pointer;
        }
        .kpiname:hover {
            color: #03467a;
            font-weight: bold;                
        }
        .nolink
        {
            text-decoration:none;
        }
        .panel {
            margin: 10px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            border:none;  
        }
        .panel .header  {
            -moz-border-radius-topleft: 5px;
            -webkit-border-top-left-radius: 5px;
            border-top-left-radius: 5px;

            -moz-border-radius-topright: 5px;
            -webkit-border-top-right-radius: 5px;
            border-top-right-radius: 5px;
            font-size: large;
            border:none;  
            padding:10px;
            color:#ffffff;
        }
            .panel .header .close{
                float:right;
                margin-top:2px;
            }
        .panel .content  {
            padding:1px;
            -moz-border-radius-bottomleft: 5px;
            -webkit-border-bottom-left-radius: 5px;
            border-bottom-left-radius: 5px;

            -moz-border-radius-bottomright: 5px;
            -webkit-border-bottom-right-radius: 5px;
            border-bottom-right-radius: 5px;
            background-color:white;
        }
        td{
            vertical-align:top;
        }
        .alertrow
        {
            margin:7px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            background-color:#e9e9e9;
            position:relative;

        }
        /*.alertrow p {
              line-height: 17px;
                color: #c22e2e;
                padding: 5px 8px 5px 8px;
                margin:0px;
                font-size:12px;
                
        }
        .alertrow .icon {
            color: #f24242;
            float: left;
            margin: 9px 5px 3px 8px;
            line-height: 9px;
        }*/


        .alertrow p {
            line-height: 17px;
            color: #c22e2e;
            padding: 5px 8px 5px 8px;
            margin-left: 30px;
            font-size: 12px;
        }
        .alertrow .icon {
          color: #f24242;
            position: absolute;
            top: 50%;
            left: 6px;
            margin-top: -6px;
            line-height: 9px;
        }
        .margin1x
        {
            margin:5px;
        }
        .margin2x
        {
            margin:10px;
        }
        .margin3x
        {
            margin:20px;
        }
        .circle {
            -moz-border-radius: 20px;
            -webkit-border-radius: 20px;
            border-radius: 20px;
            height:40px;
        }
        .success
        {
            color:green;
        }
        
        .error
        {
            color:red;
        }
        
    </style>
</head>
<body style="font-family: 'Roboto', sans-serif; margin:0px; background-color:#ebebeb; font-size:12px;">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>
    <div style="height:600px;overflow-y:scroll;">
    <table width="100%">
                                <tr>
                                    <td style="width:33%">
                                        <div class="panel">
                                            <div class="header" style="background-color:#383838">Company KPI
                                                <i class="close fa fa-refresh"></i>
                                            </div>
                                            <div class="content">
 <div style="margin:5px 20px 0px 20px;color:green;font-weight:bold;border-bottom:solid 1px #c2c2c2;padding-bottom:5px;">
 <span style="font-size:13px;color:green;" >KPI Name</span>
<span style='float:right;color:green;'>Achieved</span>
                                                            </div>

                                                <asp:Repeater runat="server" ID="rptCompKPI">
                                                <ItemTemplate>
                                                    <div class="margin3x">
                                                    <div style="margin:5px 0px 5px 0px;cursor:pointer;" onclick="OpenKPI2(<%#Eval("kpiid")%>)" >
                                                        <span style="font-size:13px;" title='<%#Eval("DESCRIPTION")%>'> <%#Eval("KPIName")%></span>
                                                        <span style="float:right;font-size:11px; font-style:italic;color:#03467a">
<span class='<%#((Common.CastAsInt32(Eval("per"))==100)?"success":"error")%>'><%#Eval("per")%> % </span>
</span>
                                                    </div>
                                                    <div style="border:solid 1px #e9e9e9">
                                                        <div style='background-color:#21b9a5; height:7px; width:<%#Eval("per")%>%;'></div>
                                                    </div>
                                                </div>
                                                </ItemTemplate>
                                                </asp:Repeater>


                                                <asp:Repeater runat="server" ID="rptCompKPI_ByVessel">
                                                <ItemTemplate>
                                                    <div class="margin3x">
                                                    <div style="margin:5px 0px 5px 0px;cursor:pointer;" onclick="OpenKPI2(<%#Eval("kpiid")%>)" >
                                                        <span style="font-size:13px;" title='<%#Eval("DESCRIPTION")%>'> <%#Eval("KPIName")%></span>
                                                        <span style="float:right;font-size:11px; font-style:italic;color:#03467a">
<span class='<%#((Common.CastAsInt32(Eval("per"))==100)?"success":"error")%>'><%#Eval("per")%> % </span>
</span>
                                                    </div>
                                                    <div style="border:solid 1px #e9e9e9">
                                                        <div style='background-color:#21b9a5; height:7px; width:<%#Eval("per")%>%;'></div>
                                                    </div>
                                                </div>
                                                </ItemTemplate>
                                                </asp:Repeater>


                                                <div class="margin3x" style='display:none;'>
                                                    <div style="margin:5px 0px 5px 0px; font-size:13px;">
                                                        <span style="font-size:13px;">Technial Inspections Due</span>
                                                    </div>
                                                    <div>
                                                        <div class="margin2x" style="margin-left:0px;">
                                                            <span class="circle" style="background-color:orange;padding:4px;">
                                                               <span style="margin:20px; font-size:12px;color:white;">50</span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="panel">
                                            <div class="header" style="background-color:#0b9b88">My KPI - 2017

                                                <i class="close fa fa-refresh"></i>
                                            </div>
                                            <div class="content">
                                                <asp:Repeater runat="server" ID="rptMyKPI">
                                                    <ItemTemplate>
                                                        <div class="margin3x">
                                                             <div style="margin:5px 0px 5px 0px;">
                                                                <span style="font-size:13px;color:#333;font-weight:bold;" > <i class="fa fa-certificate" style="color:green"></i><span style='color:green'> <%#Eval("JobResponsibility")%></span></span>
<span style='float:right;color:green;;font-weight:bold;'>Achieved</span>


                                                            </div>
                                                             <div style="border-top:solid 1px #c3c3c3">                                                               
								                                                <asp:Repeater runat="server" DataSource='<%#BindKPI(UserId.ToString(),Eval("jsid"))%>'>
                                                    		                    <ItemTemplate>
                                                                                <div class="margin2x">
                                                           	                       <div style="margin:5px 0px 5px 0px;cursor:pointer;" onclick="OpenKPI(<%#Eval("kpiid")%>,<%#Eval("userid")%>)" >
                                                                                       <span class="kpiname"  style="font-size:12px;" title="<%#Eval("DESCRIPTION")%>">  <%#Eval("KPIName")%></span>
                                                                	               <span style="float:right;font-size:11px; font-style:italic;color:#03467a">
<span class='<%#((Common.CastAsInt32(Eval("per"))==100)?"success":"error")%>'><%#Eval("per")%> % </span>
										       </span>
                                                            	                    </div>
                                                            	                </div>
                                                                                </ItemTemplate>
                                                		                        </asp:Repeater>
                                                               
                                                                        </div>

                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                              <div style="text-align:center;padding:5px">
                                                  <asp:Button runat="server" CssClass="btn" ID="btnOpenMyKPI" Text="Open My KPI" OnClick="btnOpenMyKPI_Click" />

                                              </div>

                                            </div>
                                        </div>
                                    </td>
                                    <td style="width:33%">
                                       <div class="panel">
                                            <div class="header" style="background-color:#e78138">Team KPI - 2017
                                                <i class="close fa fa-refresh "></i>
                                            </div>
                                            <div class="content" style="height:500px;">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <div>
                                                            <div style="height:130px;vertical-align:middle; background-color:#ffeee1;padding-top:10px;">
                                                            <table width="90%" cellpadding="5" border="0" style="margin:0 auto">
                                                                <tr>
                                                                    <td style="vertical-align:middle;font-size:14px; width:120px;"> Select Office </td>
                                                                    <td>: <asp:DropDownList runat="server" ID="ddlOffice" Width="93%" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged"></asp:DropDownList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="vertical-align:middle;font-size:14px;"> Select Department </td>
                                                                    <td>: <asp:DropDownList runat="server" ID="ddlDepartments" Width="93%" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartments_OnSelectedIndexChanged"></asp:DropDownList></td>
                                                                </tr>
                                                                 <tr>
                                                                    <td style="vertical-align:middle;font-size:14px;"> Select Position </td>
                                                                    <td>: <asp:DropDownList runat="server" ID="ddlPosition" Width="93%" AutoPostBack="true" OnSelectedIndexChanged="ddlPosition_OnSelectedIndexChanged"></asp:DropDownList></td>
                                                                </tr>
                                                            </table>
                                                            </div>                                                               
                                                            <div style="height:359px;overflow-y:scroll;overflow-x:hidden;" class="fade">
                                                                 
                                                            	<asp:Repeater runat="server" ID="rptEmps">
                                                    		    <ItemTemplate>
                                                                <div class="margin3x kpiname" onclick="ShowTeamKPI(<%#Eval("USERID")%>);">
                                                           	        <div style="margin:5px 0px 5px 0px;">
                                                                	    <span style="font-size:13px;" title="<%#Eval("EMPNAME")%>"><i class="fa fa-arrow-right"></i>  <%#Eval("EMPNAME")%></span> <span style="color:#0b9b88;font-style:italic;font-size:12px;"> ( <%#Eval("PositionName")%> ) </span>
                                                            	    </div>
                                                        	    </div>
                                                                </ItemTemplate>
                                                		    </asp:Repeater>

                                                                <%--<div>
                                                                   <asp:Button runat="server" ID="btnPost" OnClick="btnPost_Click" style="display:none" />
                                                                    <asp:HiddenField runat="server" ID="hfduserid"  />
                                                                <div runat="server" id="dvcrewstat" visible="false">
                                                                    <div style="padding:5px;text-align:center;">
                                                                        <asp:LinkButton runat="server" ID="lnkBack" OnClick="lnkBack_OnClick" Text="Back" Font-Size="15px"></asp:LinkButton>
                                                                    </div>
                                                                    <div style="text-align:center;">
                                                                    <div style="width:90%;background-color:#ebebeb;height:1px;margin:0 auto; margin-bottom:5px;"></div>

                                                                 
                                                                    <div><asp:Label runat="server" ID="lblUserName" style="font-size:19px;color:#383633;font-weight:bold;"></asp:Label></div>
                                                                    <div><asp:Label runat="server" ID="lblPositionName" style="font-size:12px;color:#605e5e;padding:2px;"></asp:Label></div>
                                                                   
                                                                </div>
                                                                   <asp:Repeater runat="server" ID="rptTeamKPI">
                                                                <ItemTemplate>
                                                                    <div class="margin3x">
                                                                        <div style="margin:5px 0px 5px 0px;">
                                                                            <span style="font-size:13px;color:#333;" > <i class="fa fa-certificate" style="color:orange"></i> <%#Eval("JobResponsibility")%></span>
                                                                        </div>
                                                                        <div style="border-top:solid 1px #c3c3c3">                                                               
								                                                <asp:Repeater runat="server" DataSource='<%#BindKPI(Eval("USERID"),Eval("jsid"))%>'>
                                                    		                    <ItemTemplate>
                                                                                <div class="margin2x">
                                                           	                        <div style="margin:5px 0px 5px 0px;"onclick="OpenKPI(<%#Eval("kpiid")%>,<%#Eval("userid")%>)" >
                                                                                       <span class="kpiname"  style="font-size:12px;" title="<%#Eval("DESCRIPTION")%>">  <%#Eval("KPIName")%></span>
                                                                	                   <span style="float:right;font-size:11px; font-style:italic;color:#03467a">
                                                                	                        <span class='<%#((Common.CastAsInt32(Eval("per"))==100)?"success":"error")%>'><%#Eval("per")%> % </span>
                                                                	                    </span>
                                                            	                    </div>
                                                            	                </div>
                                                                                </ItemTemplate>
                                                		                        </asp:Repeater>
                                                               
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                             </div>
                                                            </div>--%>

                                                            </div>
                                                           
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
   </div>
    </div>
        <script type="text/javascript">
            function ShowTeamKPI(userid)
            {
                //$("#hfduserid").val(userid);
                //$("#btnPost").click();
                window.open('UserKpi.aspx?key=' + userid, '');
            }
            function OpenKPI(kpiid,userid)
            {
                window.open('./kpi_result.aspx?key=' + kpiid + '&key1=' + userid, '');
            }
  	    function OpenKPI2(kpiid)
            {
                window.open('./kpi_result_company.aspx?key=' + kpiid, '');
            }
        </script>
    </form>
</body>
</html>
