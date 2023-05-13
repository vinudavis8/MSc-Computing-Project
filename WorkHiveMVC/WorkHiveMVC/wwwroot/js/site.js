$(document).ready(function () {
    $("#jobsearch").click(function () {
        $(this).toggleClass("tab-active"); // toggle class "active"
        $("#freelancersearch").toggleClass("tab-active", !$(this).hasClass("tab-active")); // toggle class "active" on button2 based on button1's class
    });

    $("#freelancersearch").click(function () {
        $(this).toggleClass("tab-active"); // toggle class "active"
        $("#jobsearch").toggleClass("tab-active", !$(this).hasClass("tab-active")); // toggle class "active" on button1 based on button2's class
    });
});

function OpenBidPopup() {
    var status = $("#login-status").val()
    var userType = $("#login-user-type").val()

    if (status == "true" && userType == "Freelancer") {
        $('#myModalproposal').modal('show');
    }
    else {
        alert("please login as freelancer to submit bid")
    }

}