using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emtm_Mainmenu : System.Web.UI.UserControl
{
    public AuthenticationManager auth; 
    protected void Page_Load(object sender, EventArgs e)
    {
        try {string s= Session["loginid"].ToString();}
        catch{Response.Redirect("../ExceptionPage.aspx");}

        try
        {
            // This is for Personal Details  from getting its value =245
            int UserID = Common.CastAsInt32(Session["loginid"]);
            auth = new AuthenticationManager(24 ,UserID, ObjectType.Module);
            if (!(auth.IsView))
            {
                TreeView1.Nodes.RemoveAt(0);
            }
            else
            {
                for (int i = TreeView1.Nodes[0].ChildNodes.Count - 1; i >= 0; i--)
                {
                    TreeNode tn1 = TreeView1.Nodes[0].ChildNodes[i];
                    auth = new AuthenticationManager(Common.CastAsInt32(tn1.Value), UserID, ObjectType.Page);
                    if (!(auth.IsView))
                    {
                        TreeView1.Nodes[0].ChildNodes.Remove(tn1);
                    }
                }
            }
            

            //TreeNode tnRoot = TreeView1.Nodes[0];
            //TreeNode tn = TreeView1.Nodes[0].ChildNodes[0];

            //if (!(auth.IsView) && tnRoot.Value=="24")
            //{
            //    TreeView1.Nodes[0].ChildNodes.Remove(tn);
            //}

            //auth = new AuthenticationManager(24, Common.CastAsInt32(Session["loginid"]), ObjectType.Module);
            //if (!(auth.IsView) && tnRoot.Value == "24")
            //{
            //    TreeView1.Nodes.Remove(tnRoot);
            //}

            if (Session["ProfileId"].ToString() == "0")
            {
                TreeNode tRoot = TreeView1.Nodes[1];
                TreeNode t = TreeView1.Nodes[1].ChildNodes[0];
                TreeView1.Nodes.Remove(tRoot);
            }
        }
        catch
        {

        }
     }
    //protected TreeNode getNode(string  Id)
    //{
    //    foreach (TreeNode tn in TreeView1.Nodes[0].ChildNodes)
    //    {
    //        if (tn.Value.ToString().Trim() == Id.Trim())
    //        { return tn; }
    //    }
    //    return null;
    //}


    //protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    if (TreeView1.SelectedNode.Text == "Personal Master")
    //    {
    //        string valuepath;
    //        valuepath = TreeView1.SelectedNode.ValuePath;
    //        Session.Add("valuepath", valuepath);
    //        Response.Redirect("Emtm_SearchDetail.aspx");
    //    }

    //    if (TreeView1.SelectedNode.Text == "Personal Details")
    //    {
    //        Response.Redirect("Emtm_SearchDetail.aspx");
    //    }

    //    if (TreeView1.SelectedNode.Text == "Compensation & Benifits")
    //    {
    //        Response.Redirect("Emtm_Comp-Benifit_Search.aspx");
    //    }

    //    if (TreeView1.SelectedNode.Text == "Leave")
    //    {
    //        Response.Redirect("Emtm_HR_Leave_Search.aspx");
    //    }

    //    if (TreeView1.SelectedNode.Text == "Travel")
    //    {
    //        Response.Redirect("Emtm_SearchDetail.aspx");
    //    }

    //    if (TreeView1.SelectedNode.Text == "MTM Assets")
    //    {
    //        Response.Redirect("Emtm_SearchDetail.aspx");
    //    }

    //    if (TreeView1.SelectedNode.Text == "Traning")
    //    {
    //        Response.Redirect("Emtm_HR_Training_Search.aspx");
    //    }

    //    if (TreeView1.SelectedNode.Text == "PEAP")
    //    {
    //        Response.Redirect("Emtm_SearchDetail.aspx");
    //    }



    //    if (TreeView1.SelectedNode.Text == "Experience")
    //    {
    //        Response.Redirect("Emtm_SearchDetail.aspx");
    //    }

    //    if (TreeView1.SelectedNode.Text == "Misc")
    //    {
    //        Response.Redirect("Emtm_SearchDetail.aspx");
    //    }
  
    //}
}
