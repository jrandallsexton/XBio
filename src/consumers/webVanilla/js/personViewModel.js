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
            html.push('<h3>' + position.Title + ' @ ' + position.Company  + '</h3>');
            //html.push('<h3>' + position.StartDate + ' - ' + position.EndDate  + '</h3>');
            if (position.Details !== null) {
                html.push('<ul>');
                $.each(position.Details, function(subIndex, detail) {
                    //html.push('<p>' + detail.Value + '</p>');
                    html.push('<li>' + detail.Value + '</li>');
                });
                html.push('</ul>');
            }
        })
        $('#positions').html(html.join(''));
    };
    this.exportWord = function() {
        $("#container").wordExport();
    };
    this.exportPdf = function() {
        var pdf = new jsPDF('p','pt','a4');

        pdf.addHTML(document.body,function() {
            var string = pdf.output('datauristring');
            $('.preview-pane').attr('src', string);
        });

/*        var doc = new jsPDF();
        // We'll make our own renderer to skip this editor
        var specialElementHandlers = {
            '#editor': function(element, renderer){
                return true;
            }
        };
        // All units are in the set measurement for the document
        // This can be changed to "pt" (points), "mm" (Default), "cm", "in"
        doc.fromHTML($('#container').get(0), 15, 15, {
            'width': 170,
            'elementHandlers': specialElementHandlers
        });*/
    }
};