using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.Code;
using WebForms.Code.Logging;

namespace ASPNETWebApplication
{
    /// <summary>
    /// Demonstrates use of Log facilities.
    /// </summary>
    public partial class LoggingDemo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Meta data: helpful for SEO (search engine optimization)
            Page.Title = "Patterns in Action Web Forms Application";
            Page.MetaKeywords = "Logging";
            Page.MetaDescription = "Logging demonstration page.";
        }

        protected void Button_Click(object sender, EventArgs e)
        {

            // You consider this not important, and log it as a Debug severity log.
            if (RadioButtonDebug.Checked)
                Logger.Instance.Debug("Log entry is considered of Debug Severity");

            // You consider this important info, and log it as a Info severity log.
            if (RadioButtonInfo.Checked)
                Logger.Instance.Info("Log entry is considered of Info Severity");

            // You consider this warning level, and log it as a Warning severity log.
            if (RadioButtonWarning.Checked)
                Logger.Instance.Warning("Log entry is considered of Warning Severity");

            // You consider this as an error, and log it as a Error severity log.
            if (RadioButtonError.Checked)
                Logger.Instance.Error("Log entry is considered of Error Severity");

            // You consider this as a fatal error, and log it as a Fatal severity log.
            if (RadioButtonFatal.Checked)
                Logger.Instance.Fatal("Log entry is considered of Fatal Severity");

        }
    }
}