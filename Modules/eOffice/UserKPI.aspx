<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserKPI.aspx.cs" Inherits="emtm_UserKPI" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script type="text/javascript" src="../JS/jquery-1.10.2.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet">
    <style type="text/css"> 
        .bordered tr td
        {
            border:solid 1px #b4e1db;
            padding:8px;
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
      
     <div class="panel">
                                            <div class="header" style="background-color:#0b9b88">My Job Responsibilities - 2017
                                                <i class="close fa fa-refresh"></i>
                                            </div>
                                            <div class="content" style="padding:10px;">
                                                  <div style="padding:10px;">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="text-align:right">Name :</td>
                                                            <td style="text-align:left;font-weight:bold;"><asp:Label runat="server" ID="lblEmpName"></asp:Label></td>
                                                            <td style="text-align:right">Office :</td>
                                                            <td style="text-align:left;font-weight:bold;"><asp:Label runat="server" ID="lblOffice"></asp:Label></td>
                                                            <td style="text-align:right">Department :</td>
                                                            <td style="text-align:left;font-weight:bold;"><asp:Label runat="server" ID="lblDepartment"></asp:Label></td>
                                                            <td style="text-align:right">Position :</td>
                                                            <td style="text-align:left;font-weight:bold;"><asp:Label runat="server" ID="lblPosGroup"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                        </div>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse" class="bordered" >
                                                     <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse"  class="bordered" >
                                                                         <tr style="font-weight:bold;background-color:#e8e8e8">
                                                            
                                                                                <td style="font-weight:bold;background-color:#e8e8e8;width:30px">SNo</td>
                                                                                <td style="font-weight:bold;background-color:#e8e8e8;">Job Responsibility</td>
                                                                                <td style="font-weight:bold;background-color:#e8e8e8;width:100px;text-align:center;">Weightage</td>
                                                                                <td style="font-weight:bold;background-color:#e8e8e8;width:100px;text-align:center;">No Of KPI</td>
                                                                                <td style="font-weight:bold;background-color:#e8e8e8;width:100px;text-align:center;">Target Score</td>
                                                                                <td style="font-weight:bold;background-color:#e8e8e8;width:100px;text-align:center;">Achieved Score</td>
                                                                                <td style="font-weight:bold;background-color:#e8e8e8;width:100px;text-align:center;">Avg Score</td>
                                                                                <td style="font-weight:bold;background-color:#e8e8e8;width:100px;text-align:center;">Scale</td>
                                                                            </tr>
                                                         </table>
                                                    </table>
                                                <div>
                                                      <asp:Repeater runat="server" ID="rptkra">
                                                        <ItemTemplate>
                                                        <div style="background-color:#e2ca5a;padding:10px;">
                                                            <b><%#Eval("kra_groupname")%></b>
                                                        </div>
                                                            <div>
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse"  class="bordered" >
                                                                         
                                                                         <asp:Repeater runat="server" DataSource='<%#GetKRA(Eval("kra_groupid"))%>'>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="width:30px; text-align:center"><%#Eval("SNo")%>.</td>
                                                                                <td>
                                                                                    <asp:LinkButton runat="server" ID="lnkJS" OnClick="lnkJS_Click" CommandArgument='<%#Eval("JSID")%>' Text='<%#Eval("jobresponsibility")%>'></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width:100px;text-align:center;"><%#Eval("waitage")%> %</td>
                                                                                <td style="width:100px;text-align:center;" style="text-align:center;"><%#Eval("NOQ")%></td>
                                                                                <td style="width:100px;text-align:center;"><%#Eval("Target")%></td>
                                                                                <td style="width:100px;text-align:center;"><%#Eval("Achieved")%></td>
                                                                                <td style="width:100px;text-align:center;"><%#Eval("Avg_Score")%></td>
                                                                                <td style="width:100px;text-align:center;"><%#Eval("Scale")%></td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    </table>
                                                                </div>
                                                        </ItemTemplate>
                                                        </asp:Repeater>
                                                </div>
                                                <div style="padding:10px;text-align:right">
                                                    <asp:Label runat="server" ID="lblTotalScore" Font-Size="X-Large" Font-Bold="true"></asp:Label>
                                                </div>
                                                 <div style="text-align:right;font-weight:bold;">
                                                     <table style="float:right">
                                                         <tr>
                                                             <td style="background-color:red;padding:8px;color:white;">1 - Poor</td>
                                                             <td style="background-color:#f56f6f;padding:8px;color:white;">2 - Below Avg.</td>
                                                             <td style="background-color:#e9e40c;padding:8px;">3 - Average</td>
                                                             <td style="background-color:#3dd43d;padding:8px;color:white;">4 - Good</td>
                                                             <td style="background-color:green;padding:8px;color:white;">5 - Outstanding</td>
                                                         </tr>
                                                     </table>
                                                 </div>
                                                 <div style="text-align:left">
                                                    <h2><a id="krahead" name="krahead" href="#"><asp:Label runat="server" ID="lblJRName"></asp:Label></a></h2>
                                                </div>

                                                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse" class="bordered" >
                                                     <tr style="font-weight:bold;background-color:#d2d8d7">
                                                            <td>KPI Name</td>
                                                            <td style="text-align:center; width:150px;">Total Requirement</td>
                                                            <td style="text-align:center; width:100px;">Success</td>
                                                            <td style="text-align:center; width:100px;">Failed</td>
                                                            <td style="text-align:center; width:100px;">Success(%)</td>
                                                        </tr>
                                                <asp:Repeater runat="server" ID="rptKPI">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><a style="cursor:pointer;" onclick='OpenKPI(<%#Eval("kpiid")%>,<%#Eval("userid")%>)'><%#Eval("KPINAME")%></a></td>                                                            
                                                            <td style="text-align:center"><%#Eval("tOTAL")%></td>
                                                            <td style="text-align:center"><%#Eval("SUCCESS")%></td>
                                                            <td style="text-align:center"><%#Eval("error")%></td>
                                                            <td style="text-align:center"><%#Eval("per")%>%</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                   
                                                </table>
                                            </div>
                                        </div>

      
         <script type="text/javascript">
            function OpenKPI(kpiid,userid)
            {
                window.open('./kpi_result.aspx?key=' + kpiid + '&key1=' + userid, '');
            }
        </script>
    </form>
</body>
</html>
