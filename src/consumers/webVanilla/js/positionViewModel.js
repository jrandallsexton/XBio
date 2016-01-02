var positionViewModel = new function () {
    this.personId = 1;
    this.positionId = 2;
    this.position = null;
    this.positionDetail = null;
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
            this.position = new PositionModel(data);
            displayPosition(data);
        });
    };
    displayPosition = function (position) {
        if (position == null)
            return;
        //$('#start').val(moment(position.StartDate, "yyyy-MM-dd"));
        //$('#end').val(position.EndDate);
        $('#companies').val(position.CompanyId);
        $('#titles').val(position.TitleId);
        displayDetails(position.Details);
    };
    displayDetails = function(details) {
        var html = [];
        if (details !== null) {
            $.each(details, function(index,item) {
                if (item.Deleted !== null)
                    return false;
                html.push('<tr>');
                html.push('<td><span id="' + item.Order + '" class="edit"><i class="fa fa-pencil-square-o"></i></span>');
                html.push('<span id="' + item.Order + '" class="delete" style="margin-left:5px;"><i class="fa fa-minus"></i></span></td>');
                html.push('<td>' + item.Order + '</td>');
                html.push('<td>' + item.Title + '</td>');
                html.push('<td><div id="val' + item.Order + '">' + item.Value + '</div></td>');
                html.push('</tr>');
            });
        }
        html.push('<tr><td><span class="add"><i class="fa fa-plus"></i></span></td><td></td><td></td><td></td></tr>');
        $('#tblBody').html(html.join(''));
        $('#positionDetails').show();
        bindEditHandlers();
        bindAddHandlers();
        bindDeleteHandlers();
    };
    bindAddHandlers = function() {
        $('.add').click(function() {
            this.positionDetail = new PositionDetailModel();
            this.positionDetail.Title = "[No Title]";
            this.positionDetail.Value = "[No Value]";
            this.positionDetail.PositionId = this.positionId;
            this.positionDetail.Order = position.Details.length;
            position.Details.push(this.positionDetail);
            displayDetails(position.Details);
        });
    };
    bindEditHandlers = function() {
        $('.edit').click(function() {
            var targetId = '#val' + this.id;
            $(targetId).height(300);
            var basicEditor = new Quill(targetId);
            basicEditor.addModule('toolbar', {
                container: '#basic-toolbar'
            });
        });
    };
    bindDeleteHandlers = function() {
        $('.delete').click(function() {
            // find the positionDetail trying to be deleted
            var detail;
            var targetOrder = this.id;
            $.each(position.Details, function(index,item) {
               if (item.Order == targetOrder) {
                   detail = item;
                   return false;
               }
            });
            // add a deleted timestamp to it
            detail.Deleted = moment();
            // refresh the UI
            displayDetails(position.Details);
        });
    };
    this.bindHandlers = function() {
        $('#positions').change(function() {
            onSelectedPositionChanged(1, $('#positions').val());
        });
    };
    this.save = function() {
        apiWrapper.savePosition(position, function(result) {
            alert(JSON.stringify(result));
        });
    };
};