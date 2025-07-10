using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Text.RegularExpressions;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public object expand_module = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string userAgent = Request.ServerVariables["HTTP_USER_AGENT"];
            //Regex OS = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //Regex device = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            //string device_info = string.Empty;
            //if (OS.IsMatch(userAgent))
            //{
            //    device_info = OS.Match(userAgent).Groups[0].Value;
            //}
            //if (device.IsMatch(userAgent.Substring(0, 4)))
            //{
            //    device_info += device.Match(userAgent).Groups[0].Value;
            //}
            //if (!string.IsNullOrEmpty(device_info))
            //{
            //    ProjectECommon.ShowMessage("device_info=" + device_info);
            //}
            //Session["ExpandModule"] = modules["ShortName"].ToString() ;
            // if (pUrl.ToString().ToUpper().Replace("\r\n", "") == Request.Url.AbsolutePath.ToString().ToUpper())
            energiosSecurity.Menu mnu = new energiosSecurity.Menu();
            Session["ExpandModule"] = null;
            Session["ExpandParent"] = null;

            int mid = Convert.ToInt32(Request.QueryString["mid"]);
            if (mid > 0)
            {
                string qry = "SELECT ParentMenuID from appmstr_menu with(nolock) where MenuID=" + mid.ToString();
                object pid = ESqlHelper.ExecuteScalar(ECommon.ConString, CommandType.Text, qry);
                if (pid == DBNull.Value)
                {
                    qry = "SELECT ModuleID from appmstr_menu with(nolock) where MenuID=" + mid.ToString();
                    object expand_module = ESqlHelper.ExecuteScalar(ECommon.ConString, CommandType.Text, qry);
                    Session["ExpandModule"] = expand_module;

                }
                else
                {
                    qry = "SELECT MenuID from appmstr_menu with(nolock) where MenuID=" + pid.ToString();
                    object expand_mid = ESqlHelper.ExecuteScalar(ECommon.ConString, CommandType.Text, qry);
                    qry = "SELECT ModuleID from appmstr_menu with(nolock) where MenuID=" + expand_mid.ToString();
                    object expand_module = ESqlHelper.ExecuteScalar(ECommon.ConString, CommandType.Text, qry);
                    Session["ExpandModule"] = expand_module;
                    Session["ExpandParent"] = expand_mid;
                    // Session["ClikedMID_parent"] = pid;

                }

            }

            //CompanyNameHeader.Text = GetCompanyShortName();
            ComanyLinkFooter.Text = GetCompanyName();
            if (this.FindControl("lblUserName") != null)
            {
                lblUserName.Text = Convert.ToString(Session["UserFullName"]);
                MenuLiteral.Text = CreateMainMenu(GetUserModules());

                energiosSecurity.Pages pgs = new energiosSecurity.Pages();
                 int uid = Convert.ToInt32(Session["loginid"]);
                DataTable tb = new DataTable();
                tb = pgs.GetPageTitle(uid, Request.Url.AbsolutePath.ToString());
                if (tb.Rows.Count > 0)
                {
                    lblPageTitle.Text = tb.Rows[0]["ModuleName"].ToString().ToUpper();

                }
            }
        }
    }
    private string GetCompanyName()
    {
        //string qry = "";
        string GetCompanyName = "e.Soft Technologies Pvt Ltd.";

        //qry = "SELECT COMPANYName FROM COMPANY ";
        //GetCompanyName = Convert.ToString(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings["eMarine_ConnectionString"].ToString(), CommandType.Text, qry));



        return GetCompanyName;
    }
    public DataTable GetUserModules()
    {
        
        energiosSecurity.UserModule uModule = new energiosSecurity.UserModule();


        DataTable tb = new DataTable();
        tb = uModule.GetUserModules(Convert.ToInt32(Session["loginid"]));


        return tb;
    }
    private string CreateMainMenu(DataTable dt)
    {


        energiosSecurity.User usr = new energiosSecurity.User();
        energiosSecurity.Menu mnu = new energiosSecurity.Menu();
        dt.Columns.Add(new DataColumn("SortOrder", typeof(int)));
        //AdminPanel
        foreach (DataRow usermenurow in dt.Rows)
        {
            if (usermenurow["ShortName"].ToString().ToUpper() == "ADMINPANEL")
            {
                usermenurow["SortOrder"] = 0;
            }
            else
            {
                usermenurow["SortOrder"] = 1;
            }
        }
        System.Data.DataTable dtMain = new System.Data.DataTable();
        dtMain = mnu.GetMenu_ByUserId(Convert.ToInt32(Session["loginid"]));

        if (!usr.IsSuperUser(Convert.ToInt32(Session["loginid"])))
        {
            foreach (DataRow menuitem in dtMain.Rows)
            {
                if (menuitem["MenuLocation"] != DBNull.Value)
                {
                    if (IsHavingMenuItemRights(menuitem["MenuLocation"].ToString().Replace("\r\n", "")) == false)
                    {
                        menuitem.Delete();
                    }
                }
            }
            dtMain.AcceptChanges();
            //

            foreach (DataRow menuitem in dtMain.Rows)
            {
                if (menuitem["MenuLocation"] == DBNull.Value)
                {
                    if ((IsMenuHavingChilds(Convert.ToInt32(menuitem["MenuID"]), dtMain) == false))
                    {
                        menuitem.Delete();
                    }
                }
            }
            dtMain.AcceptChanges();
        }




        string DashboardPath = ResolveUrl("~/DashBoard.aspx");
        string menustring;
        menustring = "";
        menustring += "<ul class='sidebar-menu' data-widget='tree'>" + Environment.NewLine;
        menustring += "<li >" + Environment.NewLine;
        //menustring += "<a href='" + DashboardPath + "'><i class='fa fa-dashboard'></i> <span>Dashboard</span> " + Environment.NewLine;// for removing dashboard from menu
        menustring += "</a></li >" + Environment.NewLine;
        foreach (DataRow modules in dt.Rows)
        {
            switch (modules["ShortName"].ToString().ToUpper())
            {
                case "ADMIN":
                    // Session["ExpandParent"] = expand_mid
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }

                    if (modules["MenuIcon"] != null || modules["MenuIcon"].ToString() != "")
                    {
                        menustring += "<a href='#'><img src=" + modules["MenuIcon"].ToString() + " runat='server' style='width: 25px;  height: 25px;' /> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<a href='#'><i class='fa fa-gear'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }          
                    break;
                //case "REST HOURS":
                //    string mUrl = ResolveUrl("~/Modules/HRD/Resthour/CrewList.aspx");
                //    if (mUrl.ToString().ToUpper() == Request.Url.AbsolutePath.ToString().ToUpper())
                //    {
                //        menustring += "<li class='active treeview'  >" + Environment.NewLine;
                //    }
                //    else
                //    {
                //        menustring += "<li >" + Environment.NewLine;
                //    }


                //    menustring += "<a href='" + mUrl + "'><i class='fa fa-circle'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span>  " + Environment.NewLine;

                //    break;
                case "INSPECTION":
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }

                    if (modules["MenuIcon"] != null || modules["MenuIcon"].ToString() != "")
                    {
                        menustring += "<a href='#'><img src=" + modules["MenuIcon"].ToString() + " runat='server' style='width: 25px;  height: 25px;' /> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<a href='#'><i class='fa fa-book'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    break;
                case "PURCHASE":
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }

                    if (modules["MenuIcon"] != null || modules["MenuIcon"].ToString() != "")
                    {
                        menustring += "<a href='#'><img src=" + modules["MenuIcon"].ToString() + " runat='server' style='width: 25px;  height: 25px;' /> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<a href='#'><i class='fa fa-book'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    break;
                case "LPSQE":
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }

                    if (modules["MenuIcon"] != null || modules["MenuIcon"].ToString() != "")
                    {
                        menustring += "<a href='#'><img src=" + modules["MenuIcon"].ToString() + " runat='server' style='width: 25px;  height: 25px;' /> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<a href='#'><i class='fa fa-book'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    break;
                case "ACCOUNTS":
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }

                    if (modules["MenuIcon"] != null || modules["MenuIcon"].ToString() != "")
                    {
                        menustring += "<a href='#'><img src=" + modules["MenuIcon"].ToString() + " runat='server' style='width: 25px;  height: 25px;' /> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<a href='#'><i class='fa fa-book'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    break;
                case "SEMINAR":
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }

                    if (modules["MenuIcon"] != null)
                    {
                        menustring += "<a href='#'><img src=" + modules["MenuIcon"].ToString() + " runat='server' style='width: 25px;  height: 25px;' /> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<a href='#'><i class='fa fa-book'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    break;

                case "PMS":
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }

                    if (modules["MenuIcon"] != null)
                    {
                        menustring += "<a href='#'><img src=" + modules["MenuIcon"].ToString() + " runat='server' style='width: 25px;  height: 25px;' /> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<a href='#'><i class='fa fa-book'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                      

                    break;
                case "HRD":
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }

                    //menustring += "<a href='#'><i class='fa -regular fa-user-pilot'></i><span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    if (modules["MenuIcon"] != null)
                    {
                        menustring += "<a href='#'><img src="+ modules["MenuIcon"].ToString() + " runat='server' style='width: 25px;  height: 25px;' /> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<a href='#'><i class='fa fa-book'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                   
                    break;
                case "MASTERS":
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }

                    //menustring += "<a href='#'><i class='fa -regular fa-user-pilot'></i><span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    if (modules["MenuIcon"] != null)
                    {
                        menustring += "<a href='#'><img src=" + modules["MenuIcon"].ToString() + " runat='server' style='width: 25px;  height: 25px;' /> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<a href='#'><i class='fa fa-book'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }

                    break;
                case "ANALYTIC":
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }

                    //menustring += "<a href='#'><i class='fa -regular fa-user-pilot'></i><span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    if (modules["MenuIcon"] != null)
                    {
                        menustring += "<a href='#'><img src=" + modules["MenuIcon"].ToString() + " runat='server' style='width: 25px;  height: 25px;' /> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<a href='#'><i class='fa fa-book'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }

                    break;
                case "REVENUE":
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }

                    if (modules["MenuIcon"] != null || modules["MenuIcon"].ToString() != "")
                    {
                        menustring += "<a href='#'><img src=" + modules["MenuIcon"].ToString() + " runat='server' style='width: 25px;  height: 25px;' /> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "<a href='#'><i class='fa fa-book'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    break;
                //case "VIMS":
                //    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                //    {
                //        menustring += "<li class='active treeview'>" + Environment.NewLine;
                //    }
                //    else
                //    {
                //        menustring += "<li class='treeview'>" + Environment.NewLine;
                //    }

                //    menustring += "<a href='#'><i class='fa fa-suitcase'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                //    break;
                //case "e-Office":
                //    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                //    {
                //        menustring += "<li class='active treeview'>" + Environment.NewLine;
                //    }
                //    else
                //    {
                //        menustring += "<li class='treeview'>" + Environment.NewLine;
                //    }

                //    menustring += "<a href='#'><i class='fa fa-github'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                //    break;
                default:


                    // Session["ExpandParent"] = expand_mid
                    if (Convert.ToInt32(Session["ExpandModule"]) == Convert.ToInt32(modules["ModuleID"]))
                    {
                        menustring += "<li class='active treeview'>" + Environment.NewLine;

                    }
                    else
                    {
                        menustring += "<li class='treeview'>" + Environment.NewLine;
                    }
                    menustring += "<a href='#'><i class='fa fa-book'></i> <span>" + ProperCase(modules["ShortName"].ToString()) + "</span> " + Environment.NewLine;
                    break;
            }


            Session["ClickedMenuID"] = null;
            // BEGIN - Create First Child
            System.Data.DataTable dtMenuFirst = new System.Data.DataTable();
            dtMenuFirst = AppHelper.FilterDataTable(dtMain, " ModuleId=" + Convert.ToInt32(modules["ModuleID"].ToString()) + " AND ParentMenuID is null");
            //IsHavingMenuItemRights

            // added by sri esoft admin have rights to access for Master, Module, Module Section, Menu -- 20180906

            //if ((modules["ShortName"].ToString().ToUpper() == "SETTINGS") && (Convert.ToInt32(Session["loginid"]) != 1))
            //{
            //    dtMenuFirst = AppHelper.FilterDataTable(dtMenuFirst, "MenuID not in (2,4,67,76)");
            //}


            if (dtMenuFirst.Rows.Count > 0)
            {
                menustring += "  <span class='pull-right-container'>" + Environment.NewLine;
                menustring += "     <i class='fa fa-angle-left pull-right'></i>" + Environment.NewLine;
                menustring += "   </span>" + Environment.NewLine;

                menustring += "</a>" + Environment.NewLine;
                //menustring += "  <ul class='treeview-menu' style='max-height:300px; overflow:auto'>" + Environment.NewLine;
                menustring += "  <ul class='treeview-menu' style='overflow:auto'>" + Environment.NewLine;
                foreach (DataRow menuitem in dtMenuFirst.Rows)
                {
                    string pUrl = "";
                    if (menuitem["MenuLocation"] == null)
                    {
                        pUrl = "#";
                    }
                    else
                    {
                        pUrl = ResolveUrl("~/" + menuitem["MenuLocation"].ToString());
                    }
                    //if (pUrl.ToString().Contains("VesselCertificate.aspx"))
                    //{

                    //}
                    //if (pUrl.ToString().Contains("AccountHead.aspx"))
                    //{

                    //}
                    if (pUrl.ToString().ToUpper().Replace("\r\n", "") == Request.Url.AbsolutePath.ToString().ToUpper())
                    {
                        Session["ClickedMenuID"] = menuitem["MenuID"];
                        continue;
                    }
                }
                foreach (DataRow menuitem in dtMenuFirst.Rows)
                {
                    menustring += " ";
                    string pUrl = "";
                    if (menuitem["MenuLocation"] == null)
                    {
                        pUrl = "#";
                    }
                    else
                    {
                        //ResolveUrl("~/");
                        //pUrl = ResolveUrl("~/" + menuitem["MenuLocation"].ToString() + "?mid=" + menuitem["MenuID"].ToString());
                        pUrl = ResolveUrl("~/" + menuitem["MenuLocation"].ToString());
                    }
                    if (IsMenuHavingChilds(Convert.ToInt32(menuitem["MenuID"]), dtMain) == true)
                    {


                        // Session["ExpandParent"] = expand_mid
                        if (Convert.ToInt32(Session["ExpandParent"]) == Convert.ToInt32(menuitem["MenuID"]))
                        {
                            menustring += "  <li class='active treeview' ><a href='#'><i class='fa fa-circle'></i> <span>" + ProperCase(menuitem["MenuName"].ToString()) + "</span> " + Environment.NewLine;
                        }
                        else
                        {
                            menustring += "  <li class='treeview' ><a href='#'><i class='fa fa-circle'></i> <span>" + ProperCase(menuitem["MenuName"].ToString()) + "</span> " + Environment.NewLine;
                        }
                        menustring += "  <span class='pull-right-container'>" + Environment.NewLine;
                        menustring += "     <i class='fa fa-angle-left pull-right'></i>" + Environment.NewLine;
                        menustring += "   </span>" + Environment.NewLine;
                        menustring += "</a>" + Environment.NewLine;

                        menustring += CreateChildMenu(Convert.ToInt32(menuitem["MenuID"]), dtMain);

                    }
                    else
                    {

                        //if (pUrl.ToString().Contains("VesselCertificate.aspx"))
                        //{

                        //}
                        //if (pUrl.ToString().Contains("AccountHead.aspx"))
                        //{

                        //}

                        string tmpmenustring = "";
                        if (pUrl.ToString().ToUpper().Replace("\r\n", "") == Request.Url.AbsolutePath.ToString().ToUpper())
                        //if (pUrl.ToString().ToUpper().Replace(Session.SessionID.ToString(), "").Replace("(", "").Replace("(", ")").Replace("\r\n", "").Contains(Request.Url.AbsolutePath.ToString().ToUpper()))
                        //if (pUrl.ToString().ToUpper().Replace("\r\n", "").Contains(Request.Url.AbsolutePath.ToString().ToUpper().Replace("","")))
                        {
                            tmpmenustring = "<li class='active'  >" + Environment.NewLine;
                        }
                        else
                        {
                            if (Session["ClickedMenuID"] != null)
                            {
                                if (Convert.ToInt32(Session["ClickedMenuID"].ToString()) == Convert.ToInt32(menuitem["MenuID"].ToString()))
                                {
                                    tmpmenustring = "<li class='active'  >" + Environment.NewLine;

                                }
                                else
                                {
                                    tmpmenustring = "<li >" + Environment.NewLine;
                                }
                            }
                            else
                            {
                                tmpmenustring = "<li >" + Environment.NewLine;

                            }
                        }

                        menustring += tmpmenustring;
                        menustring += "  <a onclick='SetClickedMenuID(" + menuitem["MenuID"].ToString() + ")'  href='" + pUrl + "?mid=" + menuitem["MenuID"].ToString() + "'><i class='fa fa-circle'></i> <span>" + ProperCase(menuitem["MenuName"].ToString()) + "</span> " + Environment.NewLine;
                        menustring += "</a>" + Environment.NewLine;
                    }



                    menustring += "</li>" + Environment.NewLine;
                }

                menustring += " </ul>  " + Environment.NewLine;
            }
            else
            {
                menustring += "</a>" + Environment.NewLine;
            }// ENDS - Create First Child


            menustring += "</li>" + Environment.NewLine;
        }



        menustring += "</ul> " + Environment.NewLine;
        return menustring;
    }
    private string CreateChildMenu(int parentmenuid, DataTable dtMain)
    {

        string menustring = "";
        energiosSecurity.Menu mnu = new energiosSecurity.Menu();
        energiosSecurity.User usr = new energiosSecurity.User();
        System.Data.DataTable dtMenu = new System.Data.DataTable();
        // dtMenu = mnu.GetDetails_ByParentMenuId(parentmenuid);
        dtMenu = AppHelper.FilterDataTable(dtMain, "ParentMenuID=" + parentmenuid);

        if (dtMenu.Rows.Count > 0)
        {
            menustring += "  <ul class='treeview-menu'>" + Environment.NewLine;
            foreach (DataRow menuitem in dtMenu.Rows)
            {
                string pUrl = "";
                if (menuitem["MenuLocation"] == null)
                {
                    pUrl = "#";
                }
                else
                {
                    // pUrl = ResolveUrl("~/" + menuitem["MenuLocation"].ToString() + "?mid=" + menuitem["MenuID"].ToString());
                    pUrl = ResolveUrl("~/" + menuitem["MenuLocation"].ToString());
                }

                //if (pUrl.ToString().Contains("VesselCertificate.aspx"))
                //{

                //}
                //if (pUrl.ToString().Contains("AccountHead.aspx"))
                //{

                //}



                if (IsMenuHavingChilds(Convert.ToInt32(menuitem["MenuID"]), dtMenu) == true)
                {

                    //  menustring += "<li class='treeview'>" + Environment.NewLine;
                    if (Convert.ToInt32(Session["ExpandParent"]) == Convert.ToInt32(menuitem["MenuID"]))
                    {
                        menustring += "  <li class='active treeview' ><a href='#'><i class='fa fa-circle'></i> <span>" + ProperCase(menuitem["MenuName"].ToString()) + "</span> " + Environment.NewLine;
                    }
                    else
                    {
                        menustring += "  <li class='treeview' ><a href='#'><i class='fa fa-circle'></i> <span>" + ProperCase(menuitem["MenuName"].ToString()) + "</span> " + Environment.NewLine;
                    }

                    menustring += "  <a href='#'><i class='fa fa-circle'></i> <span>" + ProperCase(menuitem["MenuName"].ToString()) + "</span> " + Environment.NewLine;
                    menustring += "  <span class='pull-right-container'>" + Environment.NewLine;
                    menustring += "     <i class='fa fa-angle-left pull-right'></i>" + Environment.NewLine;
                    menustring += "   </span>" + Environment.NewLine;
                    menustring += "</a>" + Environment.NewLine;

                    menustring += CreateChildMenu(Convert.ToInt32(menuitem["MenuID"]), dtMain);


                }
                else
                {
                    string tmpmenustring = "";
                    //if (pUrl.ToString().Contains("VesselCertificate.aspx"))
                    //{

                    //}
                    //if (pUrl.ToString().Contains("AccountHead.aspx"))
                    //{

                    //}
                    if (pUrl.ToString().ToUpper().Replace("\r\n", "") == Request.Url.AbsolutePath.ToString().ToUpper())
                    {

                        tmpmenustring = "<li class='active'  >" + Environment.NewLine;



                    }
                    else
                    {
                        if (Session["ClickedMenuID"] != null)
                        {
                            if (Convert.ToInt32(Session["ClickedMenuID"].ToString()) == Convert.ToInt32(menuitem["MenuID"].ToString()))
                            {
                                tmpmenustring = "<li class='active'  >" + Environment.NewLine;

                            }
                            else
                            {
                                tmpmenustring = "<li >" + Environment.NewLine;
                            }
                        }
                        else
                        {
                            tmpmenustring = "<li >" + Environment.NewLine;

                        }
                    }

                    menustring += tmpmenustring;
                    menustring += "  <a onclick='SetClickedMenuID(" + menuitem["MenuID"].ToString() + ")'  href='" + pUrl + "?mid=" + menuitem["MenuID"].ToString() + "'><i class='fa fa-circle'></i> <span>" + ProperCase(menuitem["MenuName"].ToString()) + "</span> " + Environment.NewLine;
                    menustring += "</a> " + Environment.NewLine;
                }
                menustring += "</li>" + Environment.NewLine;
            }
            menustring += "</ul>";
        }
        return menustring;
    }

    public static string ProperCase(string Input)
    {

        return System.Threading.Thread.CurrentThread.
               CurrentCulture.TextInfo.ToTitleCase(Input);
    }

    private bool IsHavingMenuItemRights(string pageurl)
    {
        energiosSecurity.User usr = new energiosSecurity.User();
        DataTable dtActionRights = new DataTable();


        int uid = Convert.ToInt32(Session["loginid"]);
        dtActionRights = usr.GetPageRightsByUserID(uid, pageurl);
        if (dtActionRights.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtActionRights.Rows[0]["CanView"]) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }




        }
        else
        {
            return false;
        }
    }
    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Login.aspx");
    }
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ChangePassword.aspx");
    }
    public bool IsMenuHavingChilds(int mid, DataTable dtMain)
    {

        DataTable tblChild = new DataTable();
        tblChild = AppHelper.FilterDataTable(dtMain, "ParentMenuID=" + mid);
        if (tblChild.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private string GetCompanyShortName()
    {
        string qry = "";
        string GetCompanyName = "";
        GetCompanyName = "eMarine-Office";
        //qry = "SELECT COMPANYABBR FROM COMPANY ";

        // Convert.ToString(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings["eMarine_ConnectionString"].ToString(), CommandType.Text, qry));

        return GetCompanyName;
    }
}
