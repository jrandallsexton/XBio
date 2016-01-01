var positionViewModel = new function () {
    this.personId = 1;
    this.positionId = 2;
    this.position = null;
    this.init = function() {
        apiWrapper.getPositions(this.personId, function(data) {
            var html = [];
            $.each(data, function(index,item) {
                html.push('<option value="' + item.Id + '">' + item.Name + '</option>');
            });
            $('#positions').html(html.join(''));
        });
        this.bindHandlers();
    };
    onSelectedPositionChanged = function(personId, positionId) {
        this.positionId = positionId;
        apiWrapper.getPosition(personId, positionId, function(data) {
            apiWrapper.position = data;
            displayPosition(data);
        });
    };
    displayPosition = function (position) {
        if (position == null)
            return;
        //$('#start').val(position.StartDate);
        //$('#end').val(position.EndDate);
        var html = [];
        if (position.Details !== null) {
            $.each(position.Details, function(index,item) {
                html.push('<tr><td>' + item.Order + 1 + '</td>');
                html.push('<td>' + item.Title + '</td>');
                html.push('<td>' + item.Value + '</td></tr>');
            });
        }
        $('#tblBody').html(html.join(''));
    }
    this.bindHandlers = function() {
        $('#positions').change(function() {
            onSelectedPositionChanged(1, $('#positions').val());
        });
    };
};