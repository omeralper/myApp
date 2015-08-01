$(function () {
    app.Models.TravelModel = Backbone.Model.extend({
        //defaults: {
        //    loginProvider: '',
        //    providerKey: '',
        //    photoUrl: '',
        //    firstName: ''
        //},
        urlRoot: 'http://localhost/appServer/api/travels',
    });
});