function CampoOnError(campo) {
    $(campo).addClass('error');
}
function CampoOnSuccess(campo) {
    $(campo).removeClass('error');
    $(campo).addClass('success');
}