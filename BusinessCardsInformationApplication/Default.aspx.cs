using System;
using System.Web.UI;
using BusinessCardsInformationApplication.Common;
using DataAccess.DB;

namespace BusinessCardsInformationApplication {
    public partial class _Default : Page{
        protected void Page_Load(object sender, EventArgs e){

        }

        protected void BtnLoginClicked(object sender, EventArgs e) {

            Session[Constants.Credentials] = false;

            var userName = txtBxUserName.Text;
            var password = txtBxPassword.Text;

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)) {
                return;
            }

            try {

                var dbMngr = new DbManager(DbConstants.ServerName, DbConstants.DbName, DbConstants.TblBusinessCardsInfoSchema);
                var isEnteredCredentialsValid = dbMngr.IsUserCredentailsValid(userName, password);
                if (!isEnteredCredentialsValid) {
                    Helper.ShowAlert(Page, "Invalid user name & password");
                    return;
                }

                Session[Constants.Credentials] = true;
                Response.Redirect("BusinessCardInsertion.aspx");

            } catch (Exception ex) {
                //If an exception occured, the reached message for the user to read is clear and straight forward
                //The cause of the exception and the message for it are logged in the logger file before
                Helper.ShowAlert(Page, ex.Message);
            }
        }
    }
}