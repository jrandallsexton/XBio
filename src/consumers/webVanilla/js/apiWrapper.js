
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

    this.getSkills = function(personId, callback) {
        var url = this.rootPath + "api/person/" + personId + "/skill"
        this.ajaxGet(url, function (values) {
            callback(values);
        }, true);
    };

    this.getTitles = function (callback) {
        this.getData("getTitles", this.rootPath + "api/title/lookup", callback); };

    this.getNumYears = function(callback) {
        var data = [];
        for (var i=0; i<=20; i++) {
            data.push({value: i, text: i.toString()});
        }
        callback(data);
    };

    this.getTechnologies = function(callback) {
        this.getData("getTechnologies", this.rootPath + "api/technology/lookup", callback); };

    this.getYears = function(callback) {
        this.getData("getYears", this.rootPath + "api/years/lookup", callback); };

    this.deletePosition = function(personId, positionId, callback) {
        var url = this.rootPath + "api/person/" + personId + "/position/" + positionId;
        this.ajaxDelete(url, function (values) {
            callback(values);
        }, true);
    };

    this.savePosition = function(position, callback) {
        if (position.PositionId == null) {
            var url = this.rootPath + "api/person/" + position.PersonId + "/position";
            this.ajaxPost(url, position, true, function (values) {
                callback(values);
            }, true);
        }
        else {
            var url = this.rootPath + "api/person/" + position.PersonId + "/position/" + position.Id;
            this.ajaxPut(url, position, true, function (values) {
                callback(values);
            }, true);
        }
    };

    this.saveSkills = function(personId, skills, callback) {
        var url = this.rootPath + "api/person/" + personId + "/skill";
        this.ajaxPut(url, skills, true, function (values) {
            callback(values);
        }, true);
    };

    this.searchCompanies = function(searchTerm, callback) {
        var url = this.rootPath + "api/company/search/" + searchTerm;
        this.ajaxGet(url, function (values) {
            callback(values);
        }, true);
    };

    this.ajaxDelete = function(url, callback, returnData) {
        $.ajax({
            data: {},
            type: "DELETE",
            url: url,
            contentType: "application/json",
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

    this.ajaxPost = function(url, data, returnData, callback) {
        $.ajax({
            data: JSON.stringify(data),
            type: "POST",
            url: url,
            traditional: true,
            contentType: "application/json",
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

    this.ajaxPut = function(url, data, returnData, callback) {
        $.ajax({
            data: JSON.stringify(data),
            type: "PUT",
            url: url,
            traditional: true,
            contentType: "application/json",
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