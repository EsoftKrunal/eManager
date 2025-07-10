using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Image_Zoom : System.Web.UI.UserControl
{
    public int h, w;
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public void Show_Image(String path,int Height,int Width)
    {
        imgPopup.Src = path;
        h = Height;
        w = Width;
        //pnlPopup.Height = Height;
        //pnlPopup.Width = Width; 
        ModalPopupExtender1.Show(); 
        
    }
}
