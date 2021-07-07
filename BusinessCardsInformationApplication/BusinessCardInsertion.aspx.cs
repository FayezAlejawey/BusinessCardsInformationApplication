using System;
using BusinessCardsInformationApplication.Common;

namespace BusinessCardsInformationApplication {
    public partial class BusinessCardInsertion : System.Web.UI.Page {

        protected void Page_Init(object sender, EventArgs e) {

            if(Session[Constants.Credentials] is null || !Equals(Session[Constants.Credentials], true)) {
                Response.Redirect("Default.aspx");
                return;
            }

            //TODO:

        }

        protected void Page_Load(object sender, EventArgs e) {

        }
    }
}