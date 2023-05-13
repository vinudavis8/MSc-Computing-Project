if (name == undefined || name == '') {
    $("#errorName").css("display", "block");
    isValid = false;
} else $("#errorName").css("display", "none");
if (phone == undefined || phone == '') {
    $("#errorPhone").css("display", "block");
    isValid = false;
} else $("#errorPhone").css("display", "none");
if (email == undefined || email == '') {
    $("#errorEmail").css("display", "block");
    isValid = false;
} else $("#errorEmail").css("display", "none");
if (password == undefined || password == '') {
    $("#errorPassword").css("display", "block");
    isValid = false;
} else $("#errorPassword").css("display", "none");
if (location == undefined || location == '') {
    $("#errorLocation").css("display", "block");
    isValid = false;
} else $("#errorLocation").css("display", "none");
if (isValid) {