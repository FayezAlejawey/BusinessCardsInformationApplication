using System;
using System.Collections.Generic;
using BusinessCardsInformationApplication.Common;
using DataAccess.DB;
using BusinessCardsInformationApplication.DataObject;
using System.Web.UI.WebControls;

namespace BusinessCardsInformationApplication {
    public partial class BusinessCardsList : System.Web.UI.Page {

        private List<BusinessCardInformation> _businessCardsLst;

        protected void Page_Init(object sender, EventArgs e) {

            if (Session[Constants.Credentials] is null || !Equals(Session[Constants.Credentials], true)) {
                Response.Redirect("Default.aspx");
                return;
            }

            try {

                var dbMngr = new DbManager(DbConstants.ServerName, DbConstants.DbName, DbConstants.TblBusinessCardsInfoSchema);
                var cardsLst = dbMngr.GetRecords(DbConstants.TblBusinessCardsInfo);

                _businessCardsLst = new List<BusinessCardInformation>();
                foreach(Dictionary<string, object> record in cardsLst) {
                    var cardData = new BusinessCardInformation(record);
                    _businessCardsLst.Add(cardData);
                }

                repeaterBusinessCards.DataSource = _businessCardsLst;
                repeaterBusinessCards.DataBind();

            } catch (Exception ex) {
                //If an exception occured, the reached message for the user to read is clear and straight forward
                //The cause of the exception and the message for it are logged in the logger file before
                Helper.ShowAlert(Page, ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void BtnDeleteClicked(object sender, EventArgs e) {

            var repItem = sender as RepeaterItem;
            var cardInfo = repItem.DataItem as BusinessCardInformation;
            var id = cardInfo.Id;

            try {

                var dbMngr = new DbManager(DbConstants.ServerName, DbConstants.DbName, DbConstants.TblBusinessCardsInfoSchema);

                var whereClause = $"{DbConstants.FldId} = {id}";
                dbMngr.DeleteRecord(DbConstants.TblBusinessCardsInfo, whereClause);

            } catch (Exception ex) {
                //If an exception occured, the reached message for the user to read is clear and straight forward
                //The cause of the exception and the message for it are logged in the logger file before
                Helper.ShowAlert(Page, ex.Message);
            }
        }
    }
}