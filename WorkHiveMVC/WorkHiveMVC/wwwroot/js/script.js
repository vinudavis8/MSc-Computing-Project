function login() {
    var isValid = true;
    var email = $("#email").val();
    var password = $("#pwd").val();
    if (email == undefined || email == '') {
        $("#errorUsername").css("display", "block");
        isValid = false;
    }
    if (password == undefined || password == '') {
        $("#errorPassword").css("display", "block");
        isValid = false;
    }
    if (isValid) {
        $.ajax({
            type: "GET",
            url: "/User/Login?username=" + email + "&password=" + password,
            //data: data,  
            //data: JSON.stringify("asf"),
            //data: { username: 'may' },
            contentType: 'application/json',
            success: function (result) {
                if (result == "Failed") {
                    alert("Login Failed")
                } else
                    if (result == "Admin") {
                        window.location.href = "/Admin/Dashboard";
                    }
                    else {


                        alert("Login successfull")

                        $('#myModalLogin').hide();
                        $('.modal-backdrop').remove()
                        location.reload();
                    }



            },
            error: function () {
                alert("Error occured!!")
            }
        });
    }
}

function CheckIfEmailExists() {
    var email = $("#regEmail").val();
    $.ajax({
        type: "GET",
        url: "/User/CheckIfEmailExists?email=" + email,
        contentType: 'application/json',
        success: function (data) {
            if (data == true) {
                $("#errorEmailExist").css("display", "block");
                $("#btnRegister").prop('disabled', true);

            } else {
                $("#errorEmailExist").prop('disabled', false);
            }

        },
        error: function () {
            alert("Error occured!!")
        }
    });
}


function register() {
    var name = $("#name").val();
    var phone = $("#phone").val();
    var email = $("#regEmail").val();
    var password = $("#regPassword").val();
    var location = $("#location").val();
    var userType = $('input[name="userTypeRadio"]:checked').val();
    var isValid = true;


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
        var data = JSON.stringify({
            'Name': name,
            'Phone': phone,
            'Email': email,
            'Password': password,
            'Location': location,
            'UserType': userType,
            'ProfileImage': ""
        });

        $.ajax({
            type: "POST",
            url: "/User/Register",
            data: data,
            //data: JSON.stringify("asf"),
            //data: { username: 'may' },
            contentType: 'application/json',
            success: function (data) {
                if (data == true) {
                    alert("Registration successfull")
                    window.location.reload();
                    // $('#myModalRegister').hide();
                    //$('.modal-backdrop').remove()

                }
                else
                    alert("Operation failed")
            },
            error: function () {
                alert("Error occured!!")
            }
        });

    }

}

function searchJob() {

    var searchTitle = $("#searchTitle").val();
    var searchLocation = $("#searchLocation").val();

    window.location.href = "/Jobs/JobSearch?searchLocation=" + searchLocation + "&searchTitle=" + searchTitle;

}
function searchJobByCategory(category) {

    window.location.href = "/Jobs/JobSearch?searchLocation=&searchTitle=&SearchCategory=" + category;
}

function ConfirmAcceptProposal(proposalId) {
    const response = confirm("Please confirm..All other Bids will be rejected..!!!");

    if (response) {
        $.ajax({
            type: "POST",
            url: "/Client/UpdateBidStatus?bidId=" + proposalId,
            success: function (data) {
                if (data == true) {
                    alert("Bid Accepted Successfully")
                    location.reload();
                }
                else
                    alert("Operation failed")
            },
            error: function () {
                alert("Error occured!!")
            }
        });
    }
}