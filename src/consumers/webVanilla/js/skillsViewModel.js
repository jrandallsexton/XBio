var skillsViewModel = new function () {
    this.personId = 1;
    this.skills = null;
    this.init = function() {
        $.fn.editable.defaults.mode = 'inline';
        apiWrapper.getSkills(this.personId, function(data) {
            displaySkills(data);
        });
    };
    generateYears = function() {
        var data = new Array();
        for (var i=1990; i<=2016; i++) {
            data.push(new Object({value:i, text:toString(i)}));
        }
        return data;
    };
    displaySkills = function(data) {
        skills = data;
        var html = [];
        if (skills !== null) {
            $.each(skills, function(index,item) {
                if (item.Deleted !== null) { return false; }
                html.push('<tr>');
                html.push('<td><span id="' + item.TechnologyId + '" class="delete" style="margin-left:10px;"><i class="fa fa-minus"></i></span></td>');
                html.push('<td><a href="#" class="technology" data-type="select2" data-value="0" data-inputclass="x-select2">[ Select ]</a></td>');
                html.push('<td><a href="#" id="val' + item.TechnologyId + '" data-id="' + item.TechnologyId + '" data-type="select">' + item.TechnologyId + '</a></td>');
                html.push('<td><a href="#" id="val' + item.TechnologyId + '" data-id="' + item.TechnologyId + '" data-type="select">' + item.TechnologyId + '</a></td>');
                html.push('<td><a href="#" id="val' + item.TechnologyId + '" data-id="' + item.TechnologyId + '" data-type="select">' + item.TechnologyId + '</a></td>');
                html.push('<td><input id="input-2" class="rating" data-min="0" data-max="5" data-step="0.1" data-size="sm" data-show-clear="false" data-show-caption="false"></td>');
                html.push('</tr>');
            });
        }
        html.push('<tr><td><span class="add" style="margin-left:10px;"><i class="fa fa-plus"></i></span></td><td></td><td></td><td></td><td></td><td></td></tr>');
        $('#tblBody').html(html.join(''));
        bindAddHandlers();
        bindXEditable();
        $("#input-2").rating({clearCaption: "No stars yet"});
    };
    bindAddHandlers = function() {
        $('.add').click(function() {
            var newSkill = new SkillModel();
            skills.push(newSkill);
            displaySkills(skills);
        });
    };
    bindXEditable = function() {
        apiWrapper.getTechnologies(function(data) {
            $('.technology').editable({ source: data });
        });
    };
    this.delete = function() {

    };
    this.save = function() {

    };
};