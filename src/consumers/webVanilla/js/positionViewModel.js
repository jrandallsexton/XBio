var positionViewModel = new function () {
    this.personId = 1;
    this.positionId = 2;
    this.position = null;
    this.positionDetail = null;
    this.init = function() {
        console.log('positionViewModel.init');
        $.fn.editable.defaults.mode = 'inline';
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
            html.push('<option value="-99">[ Create New Position ]</option>');
            $.each(data, function(index,item) {
                html.push('<option value="' + item.Id + '">' + item.Name + '</option>');
            });
            $('#positions').html(html.join(''));
        });
        this.bindHandlers();
    };
    onSelectedPositionChanged = function(personId, positionId) {
        if (positionId == -99) {
            position = new PositionModel();
            position.PersonId = personId;
            displayPosition(position);
            return;
        }
        this.positionId = positionId;
        apiWrapper.getPosition(personId, positionId, function(data) {
            this.position = new PositionModel(data);
            displayPosition(data);
        });
    };
    displayPosition = function (position) {
        if (position == null)
            return;
        $('#start').editable('setValue', position.StartDate, true);
        $('#end').editable('setValue', position.EndDate, true);
        $('#companies').val(position.CompanyId);
        $('#titles').val(position.TitleId);
        $('#summary').editable('setValue', position.Summary, true);
        displayDetails(position.Details);
    };
    displayDetails = function(details) {
        var html = [];
        if (details !== null) {
            $.each(details, function(index,item) {
                if (item.Deleted !== null) { return true; }
                html.push('<tr>');
                html.push('<td><span id="' + item.Order + '" class="delete" style="margin-left:10px;" onclick="deleteDetail(this);"><i class="fa fa-minus"></i></span></td>');
                html.push('<td>' + item.Order + '</td>');
                html.push('<td>' + item.Title + '</td>');
                html.push('<td><a href="#" id="val' + item.Order + '" data-id="' + item.Order + '" data-type="textarea" class="detailValue" data-inputclass="some_class">' + item.Value + '</a></td>');
                html.push('</tr>');
            });
        }
        html.push('<tr><td><span class="add" style="margin-left:10px;" onclick="addDetail()"><i class="fa fa-plus"></i></span></td><td></td><td></td><td></td></tr>');
        $('#tblBody').html(html.join(''));
        $('#position').show();
        $('#details').show();
        bindDeleteHandlers();
        bindXEditable();
    };
    addDetail = function() {
        var newDetails = new PositionDetailModel();
        newDetails.PositionId = this.positionId;
        newDetails.Order = position.Details.length;
        this.positionDetail = newDetails;
        position.Details.push(newDetails);
        displayDetails(position.Details);
    };
    deleteDetail = function(sender) {
        //debugger;
        // find the positionDetail trying to be deleted
        var targetOrder = parseInt(sender.id);
        var found = false;
        $.each(position.Details, function(index,item) {
            if (found)
                return false;
            if (item.Order === targetOrder) {
                //detail = item;
                // add a deleted timestamp to it
                item.Deleted = moment();
                found = true;
                return false;
            }
        });
        // refresh the UI
        displayDetails(position.Details);
    };
    bindXEditable = function() {
        $('.detailValue').editable({ placement: "bottom" });
        $('#start').on('save', function(e, params) {
            position.StartDate = params.newValue;
        });
        $('#end').on('save', function(e, params) {
            position.EndDate = params.newValue;
        });
        $('.detailValue').on('save', function(e, params) {
            var dataId = $('#' + this.id).data('id');
            //alert('id: ' + this.id + ' data-id: ' + dataId);
            $.each(position.Details, function(index,item) {
                if (item.Order == dataId) {
                    item.Value = params.newValue;
                    return false;
                }
            });
        });
        $('#summary').on('save', function(e, params) {
            position.Summary = params.newValue;
        });
    };
    bindDeleteHandlers = function() {
/*        $('.delete').click(function() {
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
        });*/
    };
    this.bindHandlers = function() {
        $('#positions').change(function() {
            onSelectedPositionChanged(1, $('#positions').val());
        });
        $('#companies').change(function() {
            position.CompanyId = $('#companies').val();
        });
        $('#titles').change(function() {
            position.TitleId = $('#titles').val();
        });
    };
    this.save = function() {
        $('#save').prop("disabled", true);
        apiWrapper.savePosition(position, function(result) {
            $.bootstrapGrowl("Position and details saved.", { type: 'success' });
            $('#save').prop("disabled", false);
        });
    };
    this.delete = function() {
        $('#delete').prop("disabled", true);
        apiWrapper.deletePosition(this.personId, position.Id, function(result) {
            $.bootstrapGrowl("Position and details deleted.", { type: 'success' });
            $('#delete').prop("disabled", false);
        });
    };
};