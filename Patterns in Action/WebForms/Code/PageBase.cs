using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms;

namespace WebForms.Code
{
    
    // base class to all pages in the web site. Provides functionality
    // shared among all pages. functionality offered includes: gridview 
    // sorting, shopping cart access, viewstate provider access, and 
    // Javascript to be sent to the browser.
   
    // GoF Design Patterns: Template Method.

    public class PageBase : Page
    {
        #region Sorting support

        
        // adds an up- or down-arrow image to GridView header.
        
        protected void AddGlyph(GridView grid, GridViewRow row)
        {
            // find the column sorted on

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].SortExpression == SortColumn)
                {
                    // add a space between header and symbol

                    var literal = new Literal();
                    literal.Text = "&nbsp;";
                    row.Cells[i].Controls.Add(literal);

                    var image = new Image();
                    image.ImageUrl = (SortDirection == "ASC" ?
                        "~/images/sortasc.gif" :
                        "~/images/sortdesc.gif");
                    image.Width = 9;
                    image.Height = 5;

                    row.Cells[i].Controls.Add(image);

                    return;
                }
            }
        }

        
        // gets or sets the current column being sorted on.
        
        protected string SortColumn
        {
            get { return ViewState["SortColumn"].ToString(); }
            set { ViewState["SortColumn"] = value; }
        }

        
        // gets or sets the current sort direction (ascending or descending).
        
        protected string SortDirection
        {
            get { return ViewState["SortDirection"].ToString(); }
            set { ViewState["SortDirection"] = value; }
        }

        
        // gets the Sql sort expression for the current sort settings.
        
        protected string SortExpression
        {
            get { return SortColumn + " " + SortDirection; }
        }
        #endregion

        #region ViewState Provider Service Access

        // random number generator 

        static Random _random = new Random(Environment.TickCount);

        
        // saves any view and control state to appropriate viewstate provider.
        // this method shields the client from viewstate key generation issues
        
        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            // create a unique name

            string random = _random.Next(0, int.MaxValue).ToString();
            string name = "ACTION_" + random + "_" + Request.UserHostAddress + "_" + DateTime.Now.Ticks.ToString();

            ViewStateProviderService.SavePageState(name, viewState);
            ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", name);
        }

        
        // retrieves viewstate from appropriate viewstate provider.
        // this method shields the client from viewstate key retrieval issues.
        
        protected override object LoadPageStateFromPersistenceMedium()
        {
            string name = Request.Form["__VIEWSTATE_KEY"];
            return ViewStateProviderService.LoadPageState(name);
        }

        #endregion

        #region Javascript support

        // adds an 'Open Window' Javascript function to page.
        
        protected void RegisterOpenWindowJavaScript()
        {
            string script =
              "<script language='JavaScript' type='text/javascript'>" + "\r\n" +
               " <!--" + "\r\n" +
               " function openwindow(url,name,width,height) " + "\r\n" +
               " { " + "\r\n" +
               "   window.open(url,name,'toolbar=yes,location=yes,scrollbars=yes,status=yes,menubar=yes,resizable=yes,top=50,left=50,width='+width+',height=' + height) " + "\r\n" +
               " } " + "\r\n" +
               " //--> " + "\r\n" +
              "</script>" + "\r\n";

            ClientScript.RegisterClientScriptBlock(typeof(string), "OpenWindowScript", script);
        }

        #endregion

        #region SiteMap support

        // virtual method. occurs when the CurrentNode is called

        public virtual SiteMapNode SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            return e.Provider.CurrentNode;
        }

        #endregion

        #region Menu Selection

        // sets selected menu on master page

        protected string SelectedMenu
        {
            set { ((SiteMaster)Master).TheMenuInMasterPage.SelectedItem = value; }
        }

        #endregion
    }
}
