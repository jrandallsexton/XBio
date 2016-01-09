var skillsViewModel = new function () {
    var personId = 1;
    var skills = null;
    this.init = function() {
        console.log('skillsViewModel.init');
        $.fn.editable.defaults.mode = 'inline';
        skills = [];
        apiWrapper.getSkills(personId, function(data) {
            skills = data;
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
                if (item.Deleted !== null) { return true; }
                html.push('<tr>');
                html.push('<td><span id="' + item.Id + '" class="delete" style="margin-left:10px;" onclick="deleteSkill(this)"><i class="fa fa-minus"></i></span></td>');
                html.push('<td><a href="#" data-id="' + item.Id + '" class="technology" data-type="select2" data-value="' + item.TechnologyId + '" data-inputclass="x-select2"></a></td>');
                html.push('<td><a href="#" data-id="' + item.Id + '" class="years fYear" data-type="select" data-value="' + item.FirstUsedYear + '">' + item.FirstUsedYear + '</a></td>');
                html.push('<td><a href="#" data-id="' + item.Id + '" class="years lYear" data-type="select" data-value="' + item.LastUsedYear + '">' + item.LastUsedYear + '</a></td>');
                html.push('<td><a href="#" data-id="' + item.Id + '" class="numYears" data-type="select" data-value="' + item.NumYearsUsed + '">' + item.NumYearsUsed + '</a></td>');
                html.push('<td><input id="input-2" class="rating" data-min="0" data-max="5" data-step="0.1" data-size="sm" data-show-clear="false" data-show-caption="false"></td>');
                html.push('</tr>');
            });
        }
        html.push('<tr><td><span class="addSkill" style="margin-left:10px;" onclick="addSkill()"><i class="fa fa-plus"></i></span></td><td></td><td></td><td></td><td></td><td></td></tr>');
        $('#tblSkills').html(html.join(''));
        bindXEditable();
        $(".rating").rating({clearCaption: "No stars yet"});
    };
    addSkill = function() {
        var newSkill = new SkillModel();
        newSkill.Id = -(skills.length);
        console.log(newSkill.Id);
        newSkill.PersonId = personId;
        skills.push(newSkill);
        displaySkills(skills);
    };
    deleteSkill = function(sender) {
        // find the skill that we want to delete
        var targetId = parseInt(sender.id);
        var found = false;
        $.each(skills, function(index,item) {
            if (found)
                return false;
            if (item.Id === targetId) {
                item.Deleted = moment();
                found = true;
                return false;
            }
        });
        // refresh the UI
        displaySkills(skills);
    };
    bindXEditable = function() {
        apiWrapper.getTechnologies(function(data) {
            $('.technology').editable({ source: data });
            $('.technology').on('save', function(e, params) {
                var dataId = $(this).data('id');
                $.each(skills, function(index,item) {
                    if (item.Id == dataId) {
                        item.TechnologyId = parseInt(params.newValue);
                        return false;
                    }
                });
            });
        });
        apiWrapper.getYears(function(data) {
            $('.years').editable({ source: data });
            $('.fYear').on('save', function(e, params) {
                var dataId = $(this).data('id');
                $.each(skills, function(index,item) {
                    if (item.Id == dataId) {
                        item.FirstUsedYear = parseInt(params.newValue);
                        return false;
                    }
                });
            });
            $('.lYear').on('save', function(e, params) {
                var dataId = $(this).data('id');
                $.each(skills, function(index,item) {
                    if (item.Id == dataId) {
                        item.LastUsedYear = parseInt(params.newValue);
                        return false;
                    }
                });
            });
        });
        apiWrapper.getNumYears(function(data) {
            $('.numYears').editable({ source: data });
            $('.numYears').on('save', function(e, params) {
                var dataId = $(this).data('id');
                $.each(skills, function(index,item) {
                    if (item.Id == dataId) {
                        item.NumYearsUsed = parseFloat(params.newValue);
                        return false;
                    }
                });
            });
        });
    };
    this.delete = function() {

    };
    this.save = function() {
        apiWrapper.saveSkills(personId, skills, function(data) {
            $.bootstrapGrowl("Skills saved.", { type: 'success' });
            refresh();
        });
    };
    refresh = function() {
        skills = [];
        apiWrapper.getSkills(personId, function(data) {
            skills = data;
            displaySkills(data);
        });
    };
};