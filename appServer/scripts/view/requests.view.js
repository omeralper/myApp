$(function () {
    app.Views.RequestsView = app.Views.MainView.extend({
        attributes: { id: 'requestsView' },
        //el: '#container',
        template: template('requestsTemplate'),
        requestTemplate: template('requestTemplate'),
        events: {
            'change input': 'change',
            'change select': 'change',
            'click #request': 'clickRequest'
        },
        name:'Requests',
        initialize: function (params) {
            this.fragment = params.fragment;
            this.model = new app.Models.RequestFilterModel();
            this.collection = new app.Collections.RequestCollection();
            _.bindAll(this, 'refreshList', 'collectionFetched');
            this.render(params);
        },
        render: function (params) {
            $('#container').append(this.$el.html(this.template()));
            this.refreshList();
            return this;
        },
        refreshList: function (evt) {
            //if (evt && !evt.target.value)
            //    return;
            this.collection.fetch({
                data: this.model.toJSON(),
                success: this.collectionFetched
            });
        },
        collectionFetched: function (collection, resp, opt) {
            var thisView = this;
            this.$el.find('#listOfRequests').empty();
            collection.each(function (model) {
                thisView.$el.find('#listOfRequests').append(thisView.requestTemplate({ model: model, param: {} }));
            });
        },
        change: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            this.model.set(changed.id, value);
            this.refreshList(evt);
        },
        clickRequest: function (evt) {
            evt.preventDefault();
            var requestId = $(evt.currentTarget).data('requestid');
            app.CurrentRequest = this.collection.get(requestId);
            app.RouterInstance.navigate('Request/' + requestId, { trigger: true });
        }
    });
});
