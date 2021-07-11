using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CSV;
using DataAccess.LogFile;
using DataAccess.DB;
using DataAccess.XML;

namespace BusinessCardsInformationApplication.DataObject {
    public class BusinessCardInformation {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public string Address { get; set; }


        public BusinessCardInformation(int id, string name, string gender, string dateTime, string email, 
            string phone, string photo, string address) {

            Id = id;
            Name = name;
            Gender = gender;
            DateOfBirth = dateTime;
            Email = email;
            Phone = phone;
            Photo = photo;
            Address = address;

        }

        public BusinessCardInformation(Dictionary<string, object> dbRecord) {

            try {

                SetBusinessCardInformationFromDb(dbRecord);

            } catch (Exception) {
                throw;
            }
        }

        public BusinessCardInformation(System.Xml.XmlDocument loadedXmlDoc) {

            try {

                var businessCardsInfo = XmlReader.GetXmlData(loadedXmlDoc);
                var businessCardInfo = businessCardsInfo.FirstOrDefault();
                if(businessCardInfo is null) {
                    var msg = "The uploaded XML file has no data within it.";
                    Logger.LogError(msg);
                    throw new Exception(msg);
                }

                SetBusinessCardInformationFromXml(businessCardInfo);

            } catch (Exception) {
                throw;
            }
        }

        public BusinessCardInformation(Stream stream) {

            try {

                var businessCardsInfo = CsvReader.GetCsvData(stream);
                var businessCardInfo = businessCardsInfo.FirstOrDefault();
                if(businessCardInfo is null) {
                    var msg = "The uploaded CSV file has no data within it.";
                    Logger.LogError(msg);
                    throw new Exception(msg);
                }

                SetBusinessCardInformationFromCsv(businessCardInfo);

            } catch (Exception) {
                throw;
            }
        }

        private void SetBusinessCardInformationFromDb(Dictionary<string, object> cardData) {

            if (!cardData.ContainsKey(DbConstants.FldId)) {
                var msg = $"No field for [{DbConstants.FldId}] was found within the retrieved database record.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(DbConstants.FldName)) {
                var msg = $"No field for [{DbConstants.FldName}] was found within the retrieved database record.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(DbConstants.FldGender)) {
                var msg = $"No field for [{DbConstants.FldGender}] was found within the retrieved database record.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(DbConstants.FldDateOfBirth)) {
                var msg = $"No field for [{DbConstants.FldDateOfBirth}] was found within the retrieved database record.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(DbConstants.FldEmail)) {
                var msg = $"No field for [{DbConstants.FldEmail}] was found within the retrieved database record.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(DbConstants.FldPhone)) {
                var msg = $"No field for [{DbConstants.FldPhone}] was found within the retrieved database record.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(DbConstants.FldPhoto)) {
                var msg = $"No field for [{DbConstants.FldPhoto}] was found within the retrieved database record.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(DbConstants.FldAddress)) {
                var msg = $"No field for [{DbConstants.FldAddress}] was found within the retrieved database record.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            string idStr = cardData[DbConstants.FldId].ToString();
            int id;
            if(!int.TryParse(idStr, out id)) {
                var msg = $"The retrieved ID [{idStr}] can't be converted to int data type";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            Id = id;
            Name = cardData[DbConstants.FldName].ToString();
            Gender = cardData[DbConstants.FldGender].ToString();
            DateOfBirth = cardData[DbConstants.FldDateOfBirth].ToString();
            Email = cardData[DbConstants.FldEmail].ToString();
            Phone = cardData[DbConstants.FldPhone].ToString();
            Photo = cardData[DbConstants.FldPhoto].ToString();
            Address = cardData[DbConstants.FldAddress].ToString();

        }

        private void SetBusinessCardInformationFromXml(Dictionary<string, object> cardData) {

            if (!cardData.ContainsKey(XmlConstants.ChildElementName)) {
                var msg = $"No element for [{XmlConstants.ChildElementName}] was found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementGender)) {
                var msg = $"No element for [{XmlConstants.ChildElementGender}] was found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementDob)) {
                var msg = $"No element for [{XmlConstants.ChildElementDob}] was found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementEmail)) {
                var msg = $"No element for [{XmlConstants.ChildElementEmail}] was found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementPhone)) {
                var msg = $"No element for [{XmlConstants.ChildElementPhone}] was found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementPhoto)) {
                var msg = $"No element for [{XmlConstants.ChildElementPhoto}] was found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(XmlConstants.ChildElementAddress)) {
                var msg = $"No element for [{XmlConstants.ChildElementAddress}] was found within the imported XML file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            Name = cardData[XmlConstants.ChildElementName].ToString();
            Gender = cardData[XmlConstants.ChildElementGender].ToString();
            DateOfBirth = cardData[XmlConstants.ChildElementDob].ToString();
            Email = cardData[XmlConstants.ChildElementEmail].ToString();
            Phone = cardData[XmlConstants.ChildElementPhone].ToString();
            Photo = cardData[XmlConstants.ChildElementPhoto].ToString();
            Address = cardData[XmlConstants.ChildElementAddress].ToString();

        }

        private void SetBusinessCardInformationFromCsv(Dictionary<string, object> cardData) {

            if (!cardData.ContainsKey(CsvConstants.ColName)) {
                var msg = $"No column for [{CsvConstants.ColName}] was found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColGender)) {
                var msg = $"No column for [{CsvConstants.ColGender}] was found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColDateOfBirth)) {
                var msg = $"No column for [{CsvConstants.ColDateOfBirth}] was found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColEmail)) {
                var msg = $"No column for [{CsvConstants.ColEmail}] was found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColPhone)) {
                var msg = $"No column for [{CsvConstants.ColPhone}] was found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColPhoto)) {
                var msg = $"No column for [{CsvConstants.ColPhoto}] was found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            if (!cardData.ContainsKey(CsvConstants.ColAddress)) {
                var msg = $"No column for [{CsvConstants.ColAddress}] was found within the imported CSV file.";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            Name = cardData[CsvConstants.ColName].ToString();
            Gender = cardData[CsvConstants.ColGender].ToString();
            DateOfBirth = cardData[CsvConstants.ColDateOfBirth].ToString();
            Email = cardData[CsvConstants.ColEmail].ToString();
            Phone = cardData[CsvConstants.ColPhone].ToString();
            Photo = cardData[CsvConstants.ColPhoto].ToString();
            Address = cardData[CsvConstants.ColAddress].ToString();

        }

        //private DateTime ConvertStringToDateTime(string date) {

        //    var dateArr = date.Split('/');
        //    var dateArrLen = dateArr.Length;
        //    if(dateArrLen != 3) {
        //        var msg = $"The sent date [{date}] can't be converted to DateTime data type";
        //        Logger.LogError(msg);
        //        throw new Exception(msg);
        //    }

        //    var day = dateArr[0];
        //    var month = dateArr[1];
        //    var year = dateArr[2];

        //    int dayInt, monthInt, yearInt;
        //    if(int.TryParse(day, out dayInt) && int.TryParse(month, out monthInt) && int.TryParse(year, out yearInt)) {
        //        return new DateTime(yearInt, monthInt, dayInt);
        //    } else {
        //        var msg = $"The sent date [{date}] can't be converted to DateTime data type";
        //        Logger.LogError(msg);
        //        throw new Exception(msg);
        //    }
        //}
    }
}