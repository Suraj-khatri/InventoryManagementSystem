$("#saveacknowledge").click(function () {
    var $btn = $(this);
    $btn.button('loading');

    var acknowlmessage = $('#AckRemark').val().trim();
    if (!acknowlmessage) {
        // Show alert if empty
        swal({
            title: "",
            text: "Remark is empty. Please add a Remark.",
            icon: "warning", // Display a warning icon
            closeOnClickOutside: false,
            closeOnEsc: false,
        }).then(() => {
            $btn.button('reset');
            $('#AckRemark').focus(); // Focus on the text area for user input
        });
        return; // Stop further execution
    }




    var transferId = $("#hiddenTid").val();
    console.log(transferId, acknowlmessage)
    let finalData = {
        "Flag": 'a',
        "Id": transferId,
        "AckRemark": acknowlmessage
    };
    const jsonData = JSON.stringify(finalData);
    console.log("final", finalData)

    swal({
        titel: "", text: "Are You Sure ?", icon: "info",
        closeOnClickOutside: false,
        closeOnEsc: false,
    }
    ).then(okay => {
        if (okay) {
            $.ajax({
                url: '/InterBranchTransfer/Acknowledge',
                type: 'POST',
                contentType: "application/json",
                data: JSON.stringify({ MvJson: jsonData }),
                success: function (response) {
                    console.log(response)
                    if (response.status === "success") {
                        swal({
                            title: "",
                            text: "Successfully Acknowledged.",
                            icon: "success",
                            closeOnClickOutside: false,
                            closeOnEsc: false,
                        }).then(okay => {
                            if (okay) {
                                if (response.RedirectUrl) {
                                    window.location.href = response.RedirectUrl;
                                } else {
                                    window.location.href = '/InterBranchTransfer/Index';
                                }
                            }
                        });
                        ClearFields();
                    } else {
                        swal({
                            title: "Error",
                            text: response.message || "An unknown error occurred.",
                            icon: "error"
                        });
                    }
                },
                error: function () {
                    swal({
                        title: "Error",
                        text: "An error occurred while processing your request.",
                        icon: "error"
                    });
                }
            });
        }
    });

});