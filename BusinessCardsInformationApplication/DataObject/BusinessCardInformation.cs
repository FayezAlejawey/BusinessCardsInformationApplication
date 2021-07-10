using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CSV;
using DataAccess.XML;
using DataAccess.LogFile;

namespace BusinessCardsInformationApplication.DataObject {
    public class BusinessCardInformation {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public string Address { get; set; }


        public BusinessCardInformation(int id, string name, string gender, string dateTime, string email, 
            string phone, string photo, string address) {

            try {

                Id = id;
                Name = name;
                Gender = gender;
                DateOfBirth = ConvertStringToDateTime(dateTime);
                Email = email;
                Phone = phone;
                Photo = photo;
                Address = address;

            } catch (Exception) {
                throw;
            }
        }

        public BusinessCardInformation(string fileAbsolutePath, bool isXml) {

            try {

                var cardsData = isXml ? XmlReader.GetXmlData(fileAbsolutePath) : CsvReader.GetCsvData(fileAbsolutePath);
                var cardData = cardsData.FirstOrDefault();
                if(cardData is null) {
                    var msg = "No data where found within the imported file.";
                    Logger.LogError(msg);
                    throw new Exception(msg);
                }

                if (isXml) {
                    SetBusinessCardInformationFromXml(cardData);
                    return;
                } 
                
                SetBusinessCardInformationFromCsv(cardData);

            } catch (Exception) {
                throw;
            }
        }

        private void SetBusinessCardInformationFromXml(Dictionary<string, object> cardData) {

            if (!cardData.ContainsKey(XmlConstants.ChildElementName)) {
                var msg = $"No element for [{XmlConstants.ChildElementName}] where found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementGender)) {
                var msg = $"No element for [{XmlConstants.ChildElementGender}] where found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementDob)) {
                var msg = $"No element for [{XmlConstants.ChildElementDob}] where found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementEmail)) {
                var msg = $"No element for [{XmlConstants.ChildElementEmail}] where found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementPhone)) {
                var msg = $"No element for [{XmlConstants.ChildElementPhone}] where found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementPhoto)) {
                var msg = $"No element for [{XmlConstants.ChildElementPhoto}] where found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementAddress)) {
                var msg = $"No element for [{XmlConstants.ChildElementAddress}] where found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            Name = cardData[XmlConstants.ChildElementName].ToString();
            Gender = cardData[XmlConstants.ChildElementGender].ToString();
            DateOfBirth = ConvertStringToDateTime(cardData[XmlConstants.ChildElementDob].ToString());
            Email = cardData[XmlConstants.ChildElementEmail].ToString();
            Phone = cardData[XmlConstants.ChildElementPhone].ToString();
            Photo = cardData[XmlConstants.ChildElementPhoto].ToString();
            Address = cardData[XmlConstants.ChildElementAddress].ToString();

        }

        private void SetBusinessCardInformationFromCsv(Dictionary<string, object> cardData) {

            if (!cardData.ContainsKey(CsvConstants.ColName)) {
                var msg = $"No element for [{CsvConstants.ColName}] where found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColGender)) {
                var msg = $"No element for [{CsvConstants.ColGender}] where found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColDateOfBirth)) {
                var msg = $"No element for [{CsvConstants.ColDateOfBirth}] where found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColEmail)) {
                var msg = $"No element for [{CsvConstants.ColEmail}] where found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColPhone)) {
                var msg = $"No element for [{CsvConstants.ColPhone}] where found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColPhoto)) {
                var msg = $"No element for [{CsvConstants.ColPhoto}] where found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColAddress)) {
                var msg = $"No element for [{CsvConstants.ColAddress}] where found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            Name = cardData[CsvConstants.ColName].ToString();
            Gender = cardData[CsvConstants.ColGender].ToString();
            DateOfBirth = ConvertStringToDateTime(cardData[CsvConstants.ColDateOfBirth].ToString());
            Email = cardData[CsvConstants.ColEmail].ToString();
            Phone = cardData[CsvConstants.ColPhone].ToString();
            Photo = cardData[CsvConstants.ColPhoto].ToString();
            Address = cardData[CsvConstants.ColAddress].ToString();

        }

        private DateTime ConvertStringToDateTime(string date) {

            var dateArr = date.Split('/');
            var dateArrLen = dateArr.Length;
            if(dateArrLen != 3) {
                var msg = $"The sent date [{date}] can't be converted to DateTime data type";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            var day = dateArr[0];
            var month = dateArr[1];
            var year = dateArr[2];

            int dayInt, monthInt, yearInt;
            if(int.TryParse(day, out dayInt) && int.TryParse(month, out monthInt) && int.TryParse(year, out yearInt)) {
                return new DateTime(yearInt, monthInt, dayInt);
            } else {
                var msg = $"The sent date [{date}] can't be converted to DateTime data type";
                Logger.LogError(msg);
                throw new Exception(msg);
            }
        }
    }
}