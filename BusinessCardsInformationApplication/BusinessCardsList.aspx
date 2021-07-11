<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessCardsList.aspx.cs" Inherits="BusinessCardsInformationApplication.BusinessCardsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Business Cards List</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-group">
            <asp:Button runat="server" ID="btnAddCard" Text="Insert New Business Card" class="btn btn-info btn-md" OnClick="BtnAddCardClicked"/>
        </div>
        <div>
            <asp:Repeater runat="server" ID="repeaterBusinessCards">
                <HeaderTemplate>
                    <table class="table">
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Gender</th>
                            <th>Date Of Birth</th>
                            <th>Email</th>
                            <th>Phone</th>
                            <th>Address</th>
                            <th></th>
                        </tr>
                </HeaderTemplate>

                <ItemTemplate>
                        <tr>
                            <td><%#DataBinder.Eval(Container.DataItem, "Id")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "Name")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "Gender")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "DateOfBirth")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "Email")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "Phone")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "Address")%></td>
                            <td>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete the selected row?');" OnClick="BtnDeleteClicked" Width="75px"/>
                            </td>
                        </tr>
                </ItemTemplate>

                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
