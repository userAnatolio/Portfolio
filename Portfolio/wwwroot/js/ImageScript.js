//$('.delete').on('click', function () {
//    $.post(
//        "https://localhost:44374/UserPortfolio/DeleteImage",
//        {
//            id: val,
//        }
//    );
//});

let arrId = new Array();

$('.delete').on('click', function () {
    var htmlForm;
    var val = $(this).attr("attrSubmit");
    arrId.push(Number.parseInt(val));
    console.log(arrId);
    $('.' + val).hide(500);
    var containForm = $('#LoadImgForm').html();
    $('#LoadImgForm').html(containForm + "<input type='file' class='form-control' name='files' />");
    for (var i = 0; i < arrId.length; i++) {
        htmlForm += "<input name='deleteId' value='" + arrId[i] + "' type='hidden'>"
    }
    $('#htmlForm').html(htmlForm);
});

$('.MakeMain').on('click', function () {
    var val = $(this).attr("attrSubmit");
    $('.textInputMainImg').val(val);
    $(".image").css({ "border": "1px solid black" })
    $("#" + val).css({ "border": "5px solid green" })
});

//function MakeMainImg() {
//    var val = $('.image:first-child').attr("id");
//    val = Number.parseInt(val);

//    if ($('#OneUser_MainImg').html() < 1 || $('#OneUser_MainImg').html() == null) {
//        if (val != null && val > 0)
//            $('#OneUser_MainImg').html(val);
//        else {
//            $('#OneUser_MainImg').html(null);
//            alert($('#OneUser_MainImg').html());
//            alert(val);
//        }
    //}
//}



//MakeMainImg();