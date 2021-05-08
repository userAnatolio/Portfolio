function AddKeyUser() {
    let name = $('#KeyUser').val();
    if (name.length > 1 && name != " ") {
        let text = $('#TextKeyUser').html();
        $('#TextKeyUser').html(text + " " + name);
        $('#ServerInput').val($('#TextKeyUser').html());
        $('#KeyUser').val("");
    }
}