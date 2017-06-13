$('.form-modal').submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: this.action,
        type: this.method,
        data: $(this).serialize(),
        success: function (result) {
            //var teste = $(result).find('.field-validation-error')
            //if (teste.length > 0) {
                $(".modal-content").html(result)
            //} else {
            //    $("#myModal").hide();
            //    $('html').html(result);
            //}
        }
    });
});