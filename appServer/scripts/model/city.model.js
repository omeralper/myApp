$(function () {
    app.Collections.CityCollection = Backbone.Collection.extend({
        url: app.SourceUrl + '/api/cities'
    });
    
});