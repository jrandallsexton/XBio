var companyViewModel = new function () {
    this.company = null;
    this.init = function () {
        //$.fn.editable.defaults.mode = 'inline';
        apiWrapper.getCompanies(function (data) {
            var html = [];
            html.push('<option value="-1">[ Select ]</option>');
            $.each(data, function (index, item) {
                html.push('<option value="' + item.Id + '">' + item.Name + '</option>');
            });
            $('#companies').html(html.join(''));
        });
    };
}