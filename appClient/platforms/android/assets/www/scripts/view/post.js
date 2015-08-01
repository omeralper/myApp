$(function () {
    app.Views.PostView = Backbone.View.extend({
        template: template('postTemplate'),
        events: {
            'change input': 'changed',
            'change select': 'changed',
            'click #travelSubmit ': 'onTravelSubmit'
        },
        subViews: [],
        initialize: function () {
            this.subViews = [];
            this.render();
        },
        render: function () {
            this.$el.html(this.template());
            $('#main-container').append(this.$el);

            $('#myTabs a').click(function (e) {
                e.preventDefault();
                $(this).tab('show');
            });

            var cityPicker = new app.Views.CityPicker({ container: this.$el.find('#iliveat') });
            $('#iliveat').append();

            return this;
        },
        onTravelSubmit: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            var obj = {};
            obj[changed.id] = value;
            var travelModel = new app.Models.TravelModel();
            //travelModel.set('id', '1');
            //travelModel.fetch();
            travelModel.save(obj);
        }
    });
});
