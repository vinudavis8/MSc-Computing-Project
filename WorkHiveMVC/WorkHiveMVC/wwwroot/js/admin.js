function saveCategory() {

    var categoryName = $("#category").val().trim()
    if (categoryName == '') {
        $("#cat-validation").css("display", "block")
    } else {
        $.ajax({
            type: "POST",
            url: "/Admin/CreateCategory",
            data: { categoryName: $("#category").val() },

            success: function (data) {
                if (data == true) {
                    alert("Category created successfully")
                    $("#category").val("")
                    window.location.reload();

                    $('#ModalCategory').hide();
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
