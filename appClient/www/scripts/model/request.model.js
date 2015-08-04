$(function () {
    app.Models.RequestModel = Backbone.Model.extend({
        urlRoot: 'http://localhost/appServer/api/requests',
    });

    app.Models.RequestFilterModel = Backbone.Model.extend({
        urlRoot: 'http://localhost/appServer/api/requests',
    });

    app.Collections.RequestCollection = Backbone.Collection.extend({
        model: app.Models.RequestModel,
        url: 'http://localhost/appServer/api/requests'
    });
});