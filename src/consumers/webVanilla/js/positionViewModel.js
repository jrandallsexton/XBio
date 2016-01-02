var positionViewModel = new function () {
    this.personId = 1;
    this.positionId = 2;
    this.position = null;
    this.init = function() {
        apiWrapper.getCompanies(function(data) {
            var html = [];
            html.push('<option value="-1">[ Select ]</option>');
            $.each(data, function(index,item) {
                html.push('<option value="' + item.Id + '">' + item.Name + '</option>');
            });
            $('#companies').html(html.join(''));
        });
        apiWrapper.getTitles(function(data) {
            var html = [];
            html.push('<option value="-1">[ Select ]</option>');
            $.each(data, function(index,item) {
                html.push('<option value="' + item.Id + '">' + item.Name + '</option>');
            });
            $('#titles').html(html.join(''));
        });
        apiWrapper.getPositions(this.personId, function(data) {
            var html = [];
            html.push('<option value="-1">[ Select ]</option>');
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
        $('#companies').val(position.CompanyId);
        $('#titles').val(position.TitleId);
        var html = [];
        if (position.Details !== null) {
            $.each(position.Details, function(index,item) {
                html.push('<tr>');
                html.push('<td><span id="' + item.Order + '" class="edit"><i class="fa fa-pencil-square-o"></i></span>');
                html.push('<span id="' + item.Order + '" class="delete" style="margin-left:5px;"><i class="fa fa-minus"></i></span></td>');
                html.push('<td>' + item.Order + 1 + '</td>');
                html.push('<td>' + item.Title + '</td>');
                html.push('<td>' + item.Value + '</td>');
                html.push('</tr>');
            });
        }
        html.push('<tr><td><span class="add"><i class="fa fa-plus"></i></span></td><td></td><td></td><td></td></tr>')
        $('#tblBody').html(html.join(''));
        $('#positionDetails').show();
        bindEditHandlers();
        bindAddHandlers();
    }
    bindAddHandlers = function() {
        $('.add').click(function() {
            alert('add now');
        });
    };
    bindEditHandlers = function() {
        $('.edit').click(function() {
            alert('edit now');
        });
    };
    this.bindHandlers = function() {
        $('#positions').change(function() {
            onSelectedPositionChanged(1, $('#positions').val());
        });
    };
};