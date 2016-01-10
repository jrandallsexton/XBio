var cvStrapViewModel = new function () {
    this.person = null;
    this.init = function() {
        apiWrapper.getPersonDetails(1, function(data) {
            person = data;
            displayPerson(person);
        });
    };
    displayPerson = function (person) {
        $('#display').text(person.Display);
    };
}