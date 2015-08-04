$(function () {
    app.Models.TravelModel = Backbone.Model.extend({
        urlRoot: 'http://localhost/appServer/api/travels',
    });

    app.Models.TravelFilterModel = Backbone.Model.extend({
        urlRoot: 'http://localhost/appServer/api/travels',
    });

    app.Collections.TravelCollection = Backbone.Collection.extend({
        model: app.Models.TravelModel,
        url: 'http://localhost/appServer/api/travels',
    });
});