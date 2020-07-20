var form = document.querySelector("#customerForm");
var input = document.querySelector("#PhoneNumber");
var output = document.querySelector("#PhoneNumberValidate");
var dialCode = document.querySelector("#DialCode");
var iti = window.intlTelInput(input, {
    utilsScript: "/lib/intl-tel-input/utils.js?1",
    formatOnDisplay: true,
});
var handleChange = function () {
    //var text = (iti.isValidNumber()) ? "International: " + iti.getNumber() : "Please enter a valid phone number";
    input.value = input.value.replace(/\s/g, '');
    var text = "";
    console.log(iti.isValidNumber());
    if (!iti.isValidNumber()) {
       
        text = "Please enter a valid phone number";
        form.formNoValidate;
    }
    var textNode = document.createTextNode(text);
    dialCode.value = iti.getSelectedCountryData().dialCode;
    output.innerHTML = "";
    output.appendChild(textNode);
};

// listen to "keyup", but also "change" to update when the user selects a country
input.addEventListener('change', handleChange);
input.addEventListener('keyup', handleChange);
input.addEventListener('keydown', handleChange);
input.addEventListener('focusout', handleChange);
form.addEventListener('submit', handleChange);

setTimeout(function () { handleChange(); },2000)