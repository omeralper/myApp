(function () {

    var proxiedSync = Backbone.sync;

    Backbone.sync = function (method, model, options) {
        options || (options = {});

        if (!options.crossDomain) {
            options.crossDomain = true;
        }

        if (!options.xhrFields) {
            options.xhrFields = { withCredentials: true };
        }

        var beforeSend = options.beforeSend;
        var error = options.error;

        options.error = function (method, model, options) {
            if (method.status === 401) {
                Backbone.history.navigate('/Login', true);
                //new app.Views.LoginView();
            }
            if (error)
                error.apply(this, arguments);
        };

        options.beforeSend = function (xhr) {
            //var csrfToken = $("input[name='__RequestVerificationToken']").val();
            //xhr.setRequestHeader('__RequestVerificationToken', csrfToken);
            //xhr.headers = { Authorization: 'Bearer ' + localStorage["accessToken"] };
            //FB.getLoginStatus(function (response) {
            //    if (response.status != 'connected') 
            //        delete localStorage["accessToken"];
            //});
            xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage["bearerAccessToken"]);
            if (beforeSend) {
                beforeSend.apply(this, arguments);
            }
        };

        return proxiedSync(method, model, options);
    };
})();