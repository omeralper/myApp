$(function () {
    app.Views.NewTravelView = app.Views.MainView.extend({
        attributes: { id: 'newTravelView' },
        template: template('newTicketTemplate'),
        events: {
            'change input': 'change',
            'change select': 'change',
            'change textarea': 'change',
            'change #fromCountry': 'fromCountryChanged',
            'change #toCountry': 'toCountryChanged',
            'click #submitTicket ': 'ticketSubmit',
            'tap #submitTicket ': 'ticketSubmit'
        },
        name: 'New Ticket',
        initialize: function (params) {
            this.fragment = params.fragment;
            var thisObj = this;
            this.model = new app.Models.TravelModel();
            app.Me.get('photo', function (photo) {
                thisObj.model.set('photo', photo);
            });
            app.Me.get('firstName', function (firstName) {
                thisObj.model.set('firstName', firstName);
                thisObj.model.set('startDate', new Date());
                thisObj.render({ edit: true });
            });
        },
        render: function (params) {
            var html = this.$el.html(this.template({ model: this.model }));
            $('#container').append(html).hide().toggle("slide", { direction: 'up' });
         
            this.$('#startDate').datepicker({
                autoclose: true,
                todayHighlight: true
            })

            app.Views.MainView.prototype.render.apply(this, arguments);
            return this;
        },
        change: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            this.model.set(changed.id, value);
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
        ticketSubmit: function () {
            this.model.save();
        },
        //remove: function () {
        //    this.$el.empty().off(); /* off to unbind the events */
        //    this.stopListening();
        //    return this;
        //}
    });
});
