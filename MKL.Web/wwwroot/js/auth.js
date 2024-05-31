$(document).ready(function () {
    setTimeout(function () {
        $("#successMessage").fadeOut("slow", function () {
            $(this).remove();
        });
    }, 5000); // 5 seconds

    setTimeout(function () {
        $("#errorMessage").fadeOut("slow", function () {
            $(this).remove();
        });
    }, 5000);
});