var jh = jh || {};
jh.dialog = function (tittle,text){
    $('#dialog title').text(text);
    $('#dialog p').text(text);
    $('#dialog').dialog();
}




