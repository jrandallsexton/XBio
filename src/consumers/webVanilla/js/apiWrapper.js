
var apiWrapper = new function () {

    this.rootPath = "http://localhost/xbioApi/";

    this.getData = function(cacheKey, url, callback) {
        var data = this.appDecache(cacheKey, true);
        if (data !== null) {
            callback(data);
            return;
        }

        this.ajaxGet(url, function (values) {
            apiWrapper.appCache(cacheKey, values, true);
            callback(values);
        }, true);
    };

    this.getPersonDetails = function(personId, callback) {
        var url = this.rootPath + "api/person/" + personId + "/detail"
        this.ajaxGet(url, function (values) {
            callback(values);
        }, true);
    };

    this.getCompanies = function (callback) {
        this.getData("getCompanies", this.rootPath + "api/company/lookup", callback); };

    this.getPosition = function(personId, positionId, callback) {
        var url = this.rootPath + "api/person/" + personId + "/position/" + positionId;
        this.ajaxGet(url, function (values) {
            callback(values);
        }, true);
    };

    this.getPositions = function(personId, callback) {
        var url = this.rootPath + "api/person/" + personId + "/position"
        this.ajaxGet(url, function (values) {
            //apiWrapper.appCache(cacheKey, values, true);
            callback(values);
        }, true);
    };

    this.ajaxGet = function(url, callback, returnData) {
        $.ajax({
            data: {},
            type: "GET",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (result) {
                if (callback != null) {
                    if (returnData) {
                        callback(result);
                    }
                    else {
                        callback();
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger;
            }
        });
    };

    this.sessionCache = function(key, value, stringify) {
        if (value == null) {
            sessionStorage.setItem(key, null);
        } else {
            if (stringify) {
                sessionStorage.setItem(key, JSON.stringify(value));
            } else {
                sessionStorage.setItem(key, value);
            }
        };
    }

    this.sessionDecache = function(key, parse) {
        var tempVal = sessionStorage.getItem(key);
        if ((tempVal != null) && (tempVal !== '')) {
            if (parse) {
                return JSON.parse(tempVal);
            } else {
                return tempVal;
            }
        }
        return null;
    };

    this.appCache = function(key, value, stringify) {
        if (value == null) {
            localStorage.setItem(key, null);
        } else {
            if (stringify) {
                localStorage.setItem(key, JSON.stringify(value));
            } else {
                localStorage.setItem(key, value);
            }
        }
    };

    this.appDecache = function (key, parse) {
        var tempVal = localStorage.getItem(key);
        if ((tempVal !== null) && (tempVal !== undefined) && (tempVal !== '')) {
            if (parse) {
                return JSON.parse(tempVal);
            } else {
                return tempVal;
            }
        }
        return null;
    };

    this.clearStorage = function() {
        localStorage.clear();
        sessionStorage.clear();
    };
};