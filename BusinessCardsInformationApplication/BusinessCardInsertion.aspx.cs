using System;
using BusinessCardsInformationApplication.Common;
using BusinessCardsInformationApplication.DataObject;
using DataAccess.DB;

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

        protected void BtnImportXmlClicked(object sender, EventArgs e) {

        }

        protected void BtnImportCsvClicked(object sender, EventArgs e) {

        }

        protected void BtnSubmitClicked(object sender, EventArgs e) {

            try {

                var businessCardObject = GetBusinessCardInformationFromUI();
                DbManager.InsertRecord(DbConstants.TblBusinessCardsInfo, (DbConstants.FldId, businessCardObject.Id),
                    (DbConstants.FldName, businessCardObject.Name), (DbConstants.FldGender, businessCardObject.Gender),
                    (DbConstants.FldDateOfBirth, businessCardObject.DateOfBirth), (DbConstants.FldEmail, businessCardObject.Email),
                    (DbConstants.FldPhone, businessCardObject.Phone), (DbConstants.FldPhoto, businessCardObject.Photo),
                    (DbConstants.FldAddress, businessCardObject.Address));
                Response.Redirect("BusinessCardsList.aspx");

            } catch (Exception ex) {
                //If an exception occured, the reached message for the user to read is clear and straight forward
                //The cause of the exception and the message for it are logged in the logger file before
                Helper.ShowAlert(Page, ex.Message);
            }
        }

        private BusinessCardInformation GetBusinessCardInformationFromUI() {

            var name = txtBxName.Text;
            var gender = txtBxGender.Text;
            var dateOfBirth = txtBxDateOfBirth.Text;
            var email = txtBxEmail.Text;
            var phone = txtBxPhone.Text;
            var photo = GetUploadedPhoto();
            var address = txtBxAddress.Text;

            var businessCardObject = new BusinessCardInformation(0, name, gender, dateOfBirth, email, phone, photo, address);

            return businessCardObject;

        }

        private string GetUploadedPhoto() {

            string imgBase64 = null;
            if (fileUploadPhoto.HasFile) {
                var length = fileUploadPhoto.PostedFile.ContentLength;
                var imgbytes = new byte[length];
                var img = fileUploadPhoto.PostedFile;
                img.InputStream.Read(imgbytes, 0, length);
                imgBase64 = Convert.ToBase64String(imgbytes);
            }

            return imgBase64;

        }
    }
}