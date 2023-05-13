function SaveReview() {
    var review = $("#reviewDescription").val();
    var rating = $("#rating").val()

    if (rating == undefined || rating == '0') {
        alert("please add rating")
    } else
        if (review == undefined || review == '') {
            alert("please enter review")
        } else {
            var review = JSON.stringify({
                "Description": review,
                "Rating": rating,
                "FreelancerId": $("#userId").val(),
            });
            $.ajax({
                type: "POST",
                url: "/Freelancer/SaveReview",
                data: review,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                success: function (data) {
                    alert("Review added successfully")
                    $("#reviewDescription").val("")
                    location.reload();
                },

            });
        }
}