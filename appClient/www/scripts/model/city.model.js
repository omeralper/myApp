$(function () {
    app.Collections.CityCollection = Backbone.Collection.extend({
        url: app.Root + '/api/cities'
    });
    
});