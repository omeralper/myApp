$(function () {
    app.Models.LoginModel = Backbone.Model.extend({
        defaults: {
            authServer: '',
            authAccessToken: ''
        },
        url: app.Root + '/api/account'
    });
});