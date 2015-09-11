$(function () {

    app.Models.TravelModel = Backbone.Model.extend({
        urlRoot: app.Root + '/api/travels',
        save: function (attrs, options) {
            this.set('photo', '');
            this.set('firstName', '');
            this.set('userName', '');
            Backbone.Model.prototype.save.call(this, attrs, options);
        }
    });

    app.Models.TravelFilterModel = Backbone.Model.extend({
        urlRoot: app.Root + '/api/travels' 
    });

    app.Collections.TravelCollection = Backbone.Collection.extend({
        model: app.Models.TravelModel,
        url: app.Root + '/api/travels'
    });
});