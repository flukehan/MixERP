/*jshint -W032*/
/*global partyNameParameter*/

$("#first_name_textbox").blur(function () {
    updatePartyName();
});

$("#middle_name_textbox").blur(function () {
    updatePartyName();
});

$("#last_name_textbox").blur(function () {
    updatePartyName();
});

var updatePartyName = function () {
    var firstName = $("#first_name_textbox").val();
    var middleName = $("#middle_name_textbox").val();
    var lastName = $("#last_name_textbox").val();

    var partyName = partyNameParameter.replace("FirstName", firstName);
    partyName = partyName.replace("MiddleName", middleName);
    partyName = partyName.replace("LastName", lastName);

    var partyNameTextBox = $("#party_name_textbox");

    partyNameTextBox.val(partyName.trim().replace(/ +(?= )/g, ''));
};