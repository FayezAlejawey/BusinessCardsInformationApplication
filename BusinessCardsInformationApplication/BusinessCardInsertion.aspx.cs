using System;
using System.Collections.Generic;
using BusinessCardsInformationApplication.Common;
using BusinessCardsInformationApplication.DataObject;
using DataAccess.DB;
using System.Xml;
using System.IO;
using DataAccess.LogFile;

namespace BusinessCardsInformationApplication {
    public partial class BusinessCardInsertion : System.Web.UI.Page {

        protected void Page_Init(object sender, EventArgs e) {

            if(Session[Constants.Credentials] is null || !Equals(Session[Constants.Credentials], true)) {
                Response.Redirect("Default.aspx");
            }

            lstGender.DataSource = new List<string> { "Male", "Female" };
            lstGender.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e) {

            fileUploadXml.Attributes["onchange"] = "UploadXmlFile(this)";
            fileUploadCsv.Attributes["onchange"] = "UploadCsvFile(this)";

        }

        protected void BtnGoToCardsLstClicked(object sender, EventArgs e) {
            Response.Redirect("BusinessCardsList.aspx");
        }

        protected void BtnUploadXmlClicked(object sender, EventArgs e) {

            try {

                if (!fileUploadXml.HasFile) {
                    return;
                }

                var postedFile = fileUploadXml.PostedFile;

                var postedFileName = postedFile.FileName;
                var xmlExtenstion = Path.GetExtension(postedFileName);
                if (!string.Equals(xmlExtenstion, ".xml", StringComparison.OrdinalIgnoreCase)) {
                    var msg = $"The uploaded file [{postedFileName}] isn't an XML file.";
                    Logger.LogError(msg);
                    throw new Exception(msg);
                }

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(postedFile.InputStream);

                var businessCardInfo = new BusinessCardInformation(xmlDoc);
                SetBusinessCardInformationToUI(businessCardInfo);

            } catch (Exception ex) {
                Helper.ShowAlert(Page, ex.Message);
            }
        }

        protected void BtnUploadCsvClicked(object sender, EventArgs e) {

            try {

                if (!fileUploadCsv.HasFile) {
                    return;
                }

                var postedFile = fileUploadCsv.PostedFile;

                var postedFileName = postedFile.FileName;
                var csvExtenstion = Path.GetExtension(postedFileName);
                if (!string.Equals(csvExtenstion, ".csv", StringComparison.OrdinalIgnoreCase)) {
                    var msg = $"The uploaded file [{postedFileName}] isn't an CSV file.";
                    Logger.LogError(msg);
                    throw new Exception(msg);
                }

                var businessCardInfo = new BusinessCardInformation(postedFile.InputStream);
                SetBusinessCardInformationToUI(businessCardInfo);

            } catch (Exception ex) {
                Helper.ShowAlert(Page, ex.Message);
            }
        }

        protected void BtnSubmitClicked(object sender, EventArgs e) {

            try {

                var businessCardObject = GetBusinessCardInformationFromUI();
                var isEntryValid = CheckEntryValidation(businessCardObject);
                if (!isEntryValid) {
                    return;
                }

                var dbMngr = new DbManager(DbConstants.ServerName, DbConstants.DbName, DbConstants.TblBusinessCardsInfoSchema);
                dbMngr.InsertRecord(DbConstants.TblBusinessCardsInfo,
                    (DbConstants.FldName, businessCardObject.Name), 
                    (DbConstants.FldGender, businessCardObject.Gender),
                    (DbConstants.FldDateOfBirth, businessCardObject.DateOfBirth), 
                    (DbConstants.FldEmail, businessCardObject.Email),
                    (DbConstants.FldPhone, businessCardObject.Phone), 
                    (DbConstants.FldPhoto, businessCardObject.Photo),
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
            var gender = lstGender.SelectedValue;
            var dateOfBirth = txtBxDateOfBirth.Text;
            var email = txtBxEmail.Text;
            var phone = txtBxPhone.Text;
            var photo = GetUploadedPhoto();
            var address = txtBxAddress.Text;

            var businessCardObject = new BusinessCardInformation(0, name, gender, dateOfBirth, email, phone, photo, address);

            return businessCardObject;

        }

        private void SetBusinessCardInformationToUI(BusinessCardInformation cardData) {

            txtBxName.Text = cardData.Name;
            lstGender.SelectedValue = cardData.Gender;
            txtBxDateOfBirth.Text = cardData.DateOfBirth;
            txtBxEmail.Text = cardData.Email;
            txtBxPhone.Text = cardData.Phone;
            txtBxAddress.Text = cardData.Address;

        }

        private bool CheckEntryValidation(BusinessCardInformation cardData) {

            if (string.IsNullOrWhiteSpace(cardData.Name)) {
                Helper.ShowAlert(Page, "[Name] is a mandatory field.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cardData.Gender)) {
                Helper.ShowAlert(Page, "[Gender] is a mandatory field.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cardData.DateOfBirth)) {
                Helper.ShowAlert(Page, "[Date Of Birth] is a mandatory field.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cardData.Email)) {
                Helper.ShowAlert(Page, "[Email] is a mandatory field.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cardData.Phone)) {
                Helper.ShowAlert(Page, "[Phone] is a mandatory field.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cardData.Address)) {
                Helper.ShowAlert(Page, "[Address] is a mandatory field.");
                return false;
            }

            return true;

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