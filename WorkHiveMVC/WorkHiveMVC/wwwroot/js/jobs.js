function saveBid() {


    var JobId = $("#JobId").val()
    var BidAmount = $("#bidAmount").val()
    var ExpectedDate = $("#days").val()
    var Description = $("#description").val()
    if (BidAmount == "" || Description == "" || ExpectedDate == "") {
        alert("please enter all fields")
    } else {
        var proposal = JSON.stringify({
            "JobId": JobId,
            "BidAmount": BidAmount,
            "ExpectedDate": ExpectedDate,
            "Description": Description,
            "Status": "Pending"
        });
        $.ajax({
            type: "POST",
            url: "/Jobs/SaveBid",
            data: proposal,
            dataType: "json",
            contentType: 'application/json',
            success: function (data) {
                if (data == true) {
                    alert("Bid submitted successfully")
                    $('#myModalproposal').hide();
                    $('.modal-backdrop').remove()
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
function clearSearch() {
    $("#searchTitle").val("");
    $("#searchLocation").val("");
    $("#searchCategory").val("");
    location.reload();
}
function search() {
    var searchTitle = $("#searchTitle").val();
    var searchLocation = $("#searchLocation").val();
    var searchCategory = $("#searchCategory option:selected").text();

    $("#loader").show()
    var search = JSON.stringify({
        "SearchLocation": searchLocation,
        "SearchTitle": searchTitle,
        "SearchCategory": searchCategory,
        "ClientID": "0",

    });
    $.ajax({
        type: "POST",
        url: "/Jobs/Search",
        //dataType: "json",
        data: search,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        success: function (data) {
            $("#search-result").empty();
            $("#search-result").append(data)
            $("#loader").hide()
        },
        error: function (data) {
            console.log(data)
            alert("Error occured!!")
            $("#loader").hide()
        }
    });


}

function loadMapView() {

    $("#loader").show()

    $.ajax({
        type: "POST",
        url: "/Freelancer/GetFreelancers",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        success: function (data) {
            $("#mapView").empty();
            $("#mapView").append(data)
            $("#loader").hide()
        },
    });
}
$(function () {
    $("#formCreateJob").submit(function () {
        var category = $("#CategoryId").val();
        var title = $("#Title").val();
        var description = $("#Description").val();
        var skillTags = $("#SkillTags").val();
        var budget = $("#Budget").val();
        var deadline = $("#Deadline").val().trim()
        var isvalid = true;
        if (category == '') {
            $("#errorCategory").css("display", "block");
            isvalid = false;
        }
        if (title == undefined || title == '') {
            $("#errorTitle").css("display", "block");
            isvalid = false;
        }
        if (description == undefined || description == '') {
            $("#errorDescription").css("display", "block");
            isvalid = false;
        }
        if (skillTags == undefined || skillTags == '') {
            $("#errorSkillTags").css("display", "block");
            isvalid = false;
        }
        if (budget == undefined || budget == '') {
            $("#errorBudget").css("display", "block");
            isvalid = false;
        }
        if (deadline == undefined || deadline == '') {
            $("#errordeadline").css("display", "block");
            isvalid = false;
        }

        return isvalid;
    });
});

