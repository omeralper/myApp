$(function () {
    app.Models.RequestModel = Backbone.Model.extend({
        urlRoot: app.SourceUrl + '/api/requests',
        save: function (attrs, options) {
            this.set('photo', '');
            this.set('firstName', '');
            this.set('userName', '');
            Backbone.Model.prototype.save.call(this, attrs, options);
        }
    });

    app.Models.RequestFilterModel = Backbone.Model.extend({
        urlRoot: app.SourceUrl + '/api/requests'
    });

    app.Collections.RequestCollection = Backbone.Collection.extend({
        model: app.Models.RequestModel,
        url: app.SourceUrl + '/api/requests'
    });
});