$('#order-form').submit(function () {
    $.ajax({
        url: this.action,
        type: this.method,
        data: $(this).serialize(),
        success: function (result) {
            if (result.success) {
                $('#register-modal').modal('hide');

            }
        }
    });
    return false;
});