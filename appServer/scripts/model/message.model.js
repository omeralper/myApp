$(function () {
    app.Models.ConversationModel = Backbone.Model.extend({
        url: app.SourceUrl + '/api/conversations'
    });

    app.Collections.ConversationCollection = Backbone.Collection.extend({
        model: app.Models.ConversationModel,
        url: app.SourceUrl + '/api/conversations'
    });

    app.Models.MessageModel = Backbone.Model.extend({
        url: app.SourceUrl + '/api/messages'
    });

    //app.Collections.MessageCollection = Backbone.Collection.extend({
    //    model: app.Models.MessageModel,
    //    url: app.Root + '/api/messages'
    //});
});