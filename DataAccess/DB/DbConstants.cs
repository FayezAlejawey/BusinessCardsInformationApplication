namespace DataAccess.DB {
    public class DbConstants {

        public const string ServerName = @"DESKTOP-VLS8U5D\SQLEXPRESS";

        #region BUSINESS_CARDS_INFO_APP DB
        public const string DbName = "BUSINESS_CARDS_INFO_APP";

        public const string TblBusinessCardsInfo = "BUSINESS_CARDS_INFO";
        public const string TblBusinessCardsInfoSchema = "dbo";

        public const string FldId = "ID";
        public const string FldName = "NAME";
        public const string FldGender = "GENDER";
        public const string FldDateOfBirth = "DATE_OF_BIRTH";
        public const string FldEmail = "EMAIL";
        public const string FldPhone = "PHONE";
        public const string FldPhoto = "PHOTO";
        public const string FldAddress = "ADDRESS";

        public const string TblUserCredentials = "USER_CREDENTIALS";
        public const string FldUserName = "USER_USERNAME";
        public const string FldPassword = "USER_PASSWORD";
        #endregion

    }
}
