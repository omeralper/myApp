$(function () {
    app.Views.ConversationsView = app.Views.MainView.extend({
        attributes: { id: 'conversationsView' },
        template: template('conversationsTemplate'),
        events: {
            'click #sendMessage': 'sendMessage',
            'tap #sendMessage': 'sendMessage'
        },
        name:'Messages',
        initialize: function (params) {
            this.fragment = params.fragment;
            var thisView = this;
            this.collection = new app.Collections.ConversationCollection();
            this.collection.fetch({
                success: function (collection, object, evt) {
                    thisView.render(collection);
                }
            });
        },
        render: function (collection) {
            var html = this.$el.html(this.template({ collection: collection }));
            $('#container').append(html);
         

            return this;
        },
        //remove: function () {
        //    this.$el.empty().off(); /* off to unbind the events */
        //    this.stopListening();
        //    return this;
        //}
    });
});
