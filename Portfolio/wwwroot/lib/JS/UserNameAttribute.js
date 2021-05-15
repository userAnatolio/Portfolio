/// <reference path="../jquery/dist/jquery.js" />
/// <reference path="../jquery-validation/dist/jquery.validate.js" />
/// <reference path="../jquery-validation-unobtrusive/jquery.validate.unobtrusive.js" />

jQuery.validator.addMethod("UserName", function () {
    alert("!");

    return ("Работает");
});

jQuery.validator.unobtrusive.adapters.addSingleVal("UserName", "valuetocompare");
