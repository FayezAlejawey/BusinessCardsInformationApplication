
function BtnLoginClicked() {

    var userName = document.getElementById('txtBxUserName').value;
    var password = document.getElementById('txtBxPassword').value;

    if (isEmptyOrSpaces(userName)) {
        window.alert("Please fill the user name field");
        return;
    }

    if (isEmptyOrSpaces(password)) {
        window.alert("Please fill the password field");
    }
}

function isEmptyOrSpaces(str) {
    return str === null || str.match(/^ *$/) !== null;
}

function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}