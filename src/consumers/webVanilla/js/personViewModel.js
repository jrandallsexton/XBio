var personViewModel = new function () {
    this.person = null;
    this.init = function() {
        apiWrapper.getPersonDetails(1, function(data) {
            person = data;
            displayPerson(person);
        });
    };
    displayPerson = function (person) {
        $('#display').text(person.Display);

        var html = [];
        $.each(person.Positions, function(index, position) {
            html.push('<h2>' + position.Title + '</h2>');
            if (position.Details !== null) {
                $.each(position.Details, function(subIndex, detail) {
                    html.push('<p>' + detail.Value + '</p>');
                });
            }
        })
        $('#positions').html(html.join(''));
    }
};