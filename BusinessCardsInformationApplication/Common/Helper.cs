using System.Web.UI;

namespace BusinessCardsInformationApplication.Common {
    public class Helper {

        public static void ShowAlert(Page page, string msg) {
            page.ClientScript.RegisterStartupScript(
                page.GetType(),
                "MessageBox",
                "<script language='javascript'>alert('" + msg + "');</script>"
            );
        }
    }
}