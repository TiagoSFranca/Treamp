$('.form-modal').submit(function (e) {
    $('button[type="submit"]').attr('disabled', true)
    e.preventDefault();
    if ($(this).valid()) {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                $(".modal-content").html(result)
            }
        });
    } else {
        $('button[type="submit"]').attr('disabled', false)
    }
});