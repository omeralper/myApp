$(function () {
    app.Models.RequestModel = Backbone.Model.extend({
        //defaults: {
        //    loginProvider: '',
        //    providerKey: '',
        //    photoUrl: '',
        //    firstName: ''
        //},
        urlRoot: 'http://localhost/appServer/api/requests',
    });
});