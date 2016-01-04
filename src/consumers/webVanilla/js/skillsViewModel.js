var skillsViewModel = new function () {
    this.personId = 1;
    this.skills = null;
    this.init = function() {
        apiWrapper.getSkills(this.personId, function(data) {
            displaySkills(data);
        });
    };
    displaySkills = function(data) {
        skills = data;
        var html = [];
        if (skills !== null) {
            $.each(skills, function(index,item) {
                //if (item.Deleted !== null) { return false; }
                //html.push('<tr>');
                //html.push('<td><span id="' + item.Order + '" class="delete" style="margin-left:10px;"><i class="fa fa-minus"></i></span></td>');
                //html.push('<td>' + item.Order + '</td>');
                //html.push('<td>' + item.Title + '</td>');
                //html.push('<td><a href="#" id="val' + item.Order + '" data-id="' + item.Order + '" data-type="textarea" class="detailValue" data-inputclass="some_class">' + item.Value + '</a></td>');
                //html.push('</tr>');
            });
        }
        html.push('<tr><td><span class="add" style="margin-left:10px;"><i class="fa fa-plus"></i></span></td><td></td><td></td><td></td><td></td><td></td></tr>');
        $('#tblBody').html(html.join(''));
    };
    this.delete = function() {

    };
    this.save = function() {

    };
};