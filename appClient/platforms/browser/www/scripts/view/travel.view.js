$(function () {
    app.Views.TravelView = Backbone.View.extend({
        className: 'tab-pane active',
        id: 'travel',
        attributes: { role: 'tabpanel' },
        template: template('travelTemplate'),
        events: {
            'change input': 'change',
            'change select': 'change',
            'click #travelSubmit ': 'onTravelSubmit'
        },
        subViews: [],
        initialize: function (params) {
            this.model = new app.Models.TravelModel();
            this.render(params);
        },
        render: function (params) {

            this.$el.html(this.template());
            params.container.append(this.$el);

            var now = new Date();
            var day = ("0" + now.getDate()).slice(-2);
            var month = ("0" + (now.getMonth() + 1)).slice(-2);
            var today = now.getFullYear() + "-" + (month) + "-" + (day);
            var nw = ("0" + (now.getDate() + 7 )).slice(-2);  
            var nextWeek = now.getFullYear() + "-" + (month) + "-" + (nw);

            $('#startDate').val(today);
            this.model.set('startDate', today);
            $('#finishDate').val(nextWeek);
            this.model.set('finishDate', nextWeek);
            return this;
        },
        change: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            this.model.set(changed.id, value);
        },
        onTravelSubmit: function () {
            this.model.save();
        }
    });
});
