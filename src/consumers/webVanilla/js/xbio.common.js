var XBioCommon = new function() {
    this.navbarContent = function(sender) {
        var html = [];
        html.push('<div class="container-fluid">');
        html.push('<div class="navbar-header">');
        html.push('<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">');
        html.push('<span class="sr-only">Toggle navigation</span>');
        html.push('<span class="icon-bar"></span>');
        html.push('<span class="icon-bar"></span>');
        html.push('<span class="icon-bar"></span>');
        html.push('</button>');
        html.push('<a class="navbar-brand" href="default.html">');
        html.push('<img src="img/genetics-32.png"/> XBio</a>');
        html.push('</div>');
        html.push('<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">');
        html.push('<ul class="nav navbar-nav">');

        if (sender == 'default') {
            html.push('<li class="active"><a href="user.html">Experience <span class="sr-only">(current)</span></a></li>');
        }
        else {
            html.push('<li><a href="user.html">Experience </a></li>');
        }

        if (sender == 'resume') {
            html.push('<li class="active"><a href="resume.html">Resumes <span class="sr-only">(current)</span></a></li>');
        }
        else {
            html.push('<li><a href="resume.html">Resumes</a></li>');
        }

        html.push('</ul>');
        html.push('<ul class="nav navbar-nav navbar-right">');

        if (sender == 'admin') {
            html.push('<li class="active"><a href="admin.html">Admin</a></li>');
        }
        else {
            html.push('<li><a href="admin.html">Admin</a></li>');
        }

        html.push('<li class="dropdown">');
        html.push('<a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Randall (jrandallsexton@gmail.com) <span class="caret"></span></a>');
        html.push('<ul class="dropdown-menu">');
        html.push('<li><a href="profile.html">My Profile</a></li>');
        html.push('<li><a href="#">Linked Accounts</a></li>');
        html.push('<li role="separator" class="divider"></li>');
        html.push('<li><a href="#">Sign Out</a></li>');
        html.push('</ul>');
        html.push('</li>');
        html.push('</ul>');
        html.push('</div>');
        html.push('</div>');
        return html.join('');
    };
    this.user = function() {
        var user = apiWrapper.appDecache('user', true);
        return user;
    };
    this.isUrl = function(s) {
        var regexp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
        return regexp.test(s.toLowerCase());
    }
};