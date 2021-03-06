<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessCardInsertion.aspx.cs" Inherits="BusinessCardsInformationApplication.BusinessCardInsertion" %>

<html>
<head runat="server">
    <title>Business Card Information Entry</title>
    <link href="/Content/bootstrap.min.css" rel="stylesheet">
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Scripts/jquery-3.4.1.min.js"></script>
</head>
<body>
    <div class="container">
        <h2>Information Entry</h2>
        <p>Input business card information either by using the form below or by importing data from a CSV or XML file</p>
        <form id="form1" runat="server">
            <div class="container">
                <asp:Button runat="server" class="btn btn-info btn-md" ID="btnGoToCardsLst" OnClick="BtnGoToCardsLstClicked" Text="Go To Business Cards List" />
            </div>

            <br />

            <div class="container">
                <label for="fileUploadXml">Upload XML:</label>
                <asp:FileUpload runat="server" class="form-control" ID="fileUploadXml" />
                <asp:Button runat="server" class="btn btn-info btn-md" Text="Upload" ID="btnUploadXml" OnClick="BtnUploadXmlClicked"/>
                <br />

                <label for="fileUploadCsv">Upload CSV:</label>
                <asp:FileUpload runat="server" class="form-control" ID="fileUploadCsv"/>
                <asp:Button runat="server" class="btn btn-info btn-md" Text="Upload" ID="btnUploadCsv" OnClick="BtnUploadCsvClicked"/>
            </div>
            <div class="form-group">
                <label for="txtBxName">Name:</label>
                <asp:TextBox runat="server" class="form-control" ID="txtBxName"/>
            </div>
            <div class="form-group">
                <label for="lstGender">Gender:</label>
                <asp:DropDownList runat="server" class="form-control" ID="lstGender"/>
            </div>
            <div class="form-group">
                <label for="txtBxDateOfBirth">Date of birth:</label>
                <asp:TextBox runat="server" class="form-control" ID="txtBxDateOfBirth"/>
            </div>
            <div class="form-group">
                <label for="txtBxEmail">Email:</label>
                <asp:TextBox runat="server" class="form-control" ID="txtBxEmail"/>
            </div>
            <div class="form-group">
                <label for="txtBxPhone">Phone:</label>
                <asp:TextBox runat="server" class="form-control" ID="txtBxPhone"/>
            </div>
            <div class="form-group">
                <label for="fileUploadPhoto">Photo (Optional):</label>
                <asp:FileUpload runat="server" class="form-control" ID="fileUploadPhoto"/>
            </div>
            <div class="form-group">
                <label for="txtBxAddress">Address:</label>
                <asp:TextBox runat="server" class="form-control" ID="txtBxAddress"/>
            </div>
            <div class="container">
                <asp:Button runat="server" class="btn btn-info btn-md" ID="btnSubmit" OnClick="BtnSubmitClicked" Text="Submit"/>
            </div>
        </form>
    </div>
</body>
</html>
