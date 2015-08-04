$(function () {
    app.Views.TravelListView = Backbone.View.extend({
        className: 'tab-pane',
        id: 'travelList',
        attributes: { role: 'tabpanel' },
        template: template('travelListTemplate'),
        tableTemplate: template('travelListTableTemplate'),
        events: {
            'change input': 'change',
            'change select': 'change',
            //'focusout #filterTravel' : 'refreshList'  //bu change varken çalışmıyor. şerefsiz, derdi neyse
        },
        subViews: [],
        initialize: function (params) {
            this.model = new app.Models.TravelFilterModel();
            this.collection = new app.Collections.TravelCollection();
            _.bindAll(this, 'refreshList');
            this.render(params);
        },
        render: function (params) {
            this.$el.html(this.template());
            params.container.append(this.$el);
            return this;
        },
        refreshList: function (evt) {
            //if (evt && !evt.target.value)
            //    return;

            var thisView = this;
            this.collection.fetch({
                data: this.model.toJSON(), success: function (collection, resp, opt) {
                    thisView.$el.find('#travelTable').empty().html(thisView.tableTemplate({ values: collection }));
                }
            });
        },
        change: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            this.model.set(changed.id, value);
            this.refreshList(evt);
        }
    });
});
