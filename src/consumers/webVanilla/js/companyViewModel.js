var companyViewModel = new function () {
    this.company = null;
    this.init = function () {
        //$.fn.editable.defaults.mode = 'inline';
        $("#companies").select2({
            ajax: {
                url: "http://localhost/xbioApi/api/company/search",
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        q: params.term, // search term
                        page: params.page
                    };
                },
                processResults: function (data, params) {
                    // parse the results into the format expected by Select2
                    // since we are using custom formatting functions we do not need to
                    // alter the remote JSON data, except to indicate that infinite
                    // scrolling can be used
                    params.page = params.page || 1;

                    return {
                        results: data,
                        pagination: {
                            more: (params.page * 30) < data.total_count
                        }
                    };
                },
                cache: true
            },
            escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
            minimumInputLength: 3,
            templateResult: formatCompany, // omitted for brevity, see the source of this page
            templateSelection: formatCompanySelection // omitted for brevity, see the source of this page
        });
    };
    formatCompany = function(repo) {
        if (repo.loading) return repo.text;

        var markup = "<div class='select2-result-repository clearfix'>" +
            //"<div class='select2-result-repository__avatar'><img src='" + repo.text + "' /></div>" +
            "<div class='select2-result-repository__meta'>" +
            "<div class='select2-result-repository__title'>" + repo.text + "</div>";

        if (repo.description) {
            markup += "<div class='select2-result-repository__description'>" + repo.description + "</div>";
        }

        markup += "<div class='select2-result-repository__statistics'>" +
            "<div class='select2-result-repository__forks'><i class='fa fa-flash'></i> " + repo.text + " Forks</div>" +
            "<div class='select2-result-repository__stargazers'><i class='fa fa-star'></i> " + repo.text + " Stars</div>" +
            "<div class='select2-result-repository__watchers'><i class='fa fa-eye'></i> " + repo.text + " Watchers</div>" +
            "</div>" +
            "</div></div>";

        return markup;
    };
    formatCompanySelection = function(repo) {
        return repo.text;
    };
}