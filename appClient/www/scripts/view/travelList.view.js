$(function () {
    app.Views.TravelListView = Backbone.View.extend({
        className: 'tab-pane',
        id: 'travelList',
        attributes: { role: 'tabpanel' },
        template: template('travelListTemplate'),
        events: {
        },
        subViews: [],
        initialize: function (params) {
            this.model = new app.Models.TravelModel();
            _.bindAll(this, 'refreshList');
            this.render(params);
        },
        render: function (params) {

            this.$el.html(this.template());
            params.container.append(this.$el);

           
            return this;
        },
        refreshList: function () {
            debugger;

        },
        changed: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            this.model.set(changed.id, value);
        }
    });
});
