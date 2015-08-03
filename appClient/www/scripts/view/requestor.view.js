$(function () {
    app.Views.RequestorView = Backbone.View.extend({
        className: 'tab-pane',
        id: 'need',
        attributes: { role: 'tabpanel' },
        template: template('requestorTemplate'),
        events: {
            'change input': 'changed',
            'change select': 'changed',
            'click #requestSubmit ': 'onRequestSubmit'
        },
        subViews: [],
        initialize: function (params) {
            this.model = new app.Models.RequestModel();
            this.render(params);
        },
        render: function (params) {
            
            this.$el.html(this.template());
            params.container.append(this.$el);

            var cityPicker = new app.Views.CityPicker({ container: this.$el.find('#iliveat') });
            this.subViews.push(cityPicker);
            $('#iliveat').append();

            return this;
        },
        changed: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            this.model.set(changed.id, value);
        },
        onRequestSubmit: function () {
            this.model.save(obj);
        }
    });
});
