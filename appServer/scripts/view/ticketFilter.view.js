$(function () {
    app.Views.TicketFilterView = Backbone.View.extend({
        attributes: { id: 'ticketFilterView' },
        className:'container-fluid ticket-filter',
        template: template('ticketFilterTemplate'),
        events: {
            'change input': 'change',
            'change select': 'change',
            'click #filterTickets': 'clickSearch',
            'change #fromCountry': 'fromCountryChanged',
            'change #toCountry': 'toCountryChanged',
            'click #ticketFilterReset': 'reset',
            'tap #ticketFilterReset': 'reset',
            'click #ticketFilterCancel': 'cancel',
            'tap #ticketFilterCancel': 'cancel',
        },
        initialize: function (params) {
            this.model = new app.Models.TravelFilterModel();
            this.render(params);
        },
        render: function (params) {
            var html = this.$el.html(this.template()).hide();//
            $('body').prepend(html);
            var thisView = this;
            this.$('#ticketWeightRange').slider({
                range: true,
                min: 0,
                max: 30,
                values: [0, 30],
                slide: function (event, ui) {
                    thisView.$("#ticketWeightAmount").text(ui.values[0] + "Kg - " + ui.values[1] + "Kg");
                    thisView.model.set('weightMin', ui.values[0]);
                    thisView.model.set('weightMax', ui.values[1]);
                }
            });
            this.$("#ticketWeightAmount").text(this.$("#ticketWeightRange").slider("values", 0) + "Kg - " + this.$("#ticketWeightRange").slider("values", 1) + "Kg");
            this.$el.toggle("slide", { direction: 'up' }); //fadeIn();
            $('#container').hide();
            return this;
        },
        reAppear: function () {
            $('#container').hide();
            this.$el.toggle("slide", { direction: 'up' });
        },
        fromCountryChanged: function (evt) {
            this.countryChanged(evt, 'fromCity');
        },
        toCountryChanged: function (evt) {
            this.countryChanged(evt, 'toCity');
        },
        countryChanged: function (evt, city) {
            var city = this.$('#' + city);
            city.empty();
            var cities = new app.Collections.CityCollection();
            cities.fetch({
                data: { 'countryId': evt.currentTarget.value },
                success: function (collection, arr) {
                    city.append("<option value='0'></option>");
                    _.each(arr, function (c) {
                        city.append("<option value='" + c.id + "'>" + c.name + "</option>");
                    });
                }
            });
        },
        change: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            this.model.set(changed.id, value);
        },
        cancel: function () {
            $('#container').show();
            this.$el.toggle("slide", { direction: 'up' });
        },
        reset:function(){
            this.model.clear();
            this.$('select').val('');
            this.$('input').val('');
            this.$("#ticketWeightRange").slider("values",[ 0, 30]);
            this.$("#ticketWeightAmount").text("0 Kg - 30 Kg");
        },
        clickSearch: function () {
            $('#container').show();
            app.Events.trigger('ticketSearch', this.model);
            var thisView = this;
            this.$el.toggle("slide", { direction: 'up' });
            
        }
    });
});
