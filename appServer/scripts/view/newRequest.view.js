$(function () {
    app.Views.NewRequestView = app.Views.MainView.extend({
        attributes: { id: 'newRequestView' },
        template: template('newRequestTemplate'),
        events: {
            'change input': 'change',
            'change select': 'change',
            'change textarea': 'change',
            'change #fromCountry': 'fromCountryChanged',
            'change #toCountry': 'toCountryChanged',
            'click #submitTicket ': 'requestSubmit',
            'tap #submitTicket ': 'requestSubmit'
        },
        name: 'New Request',
        initialize: function (params) {
            this.fragment = params.fragment;
            var thisObj = this;
            this.model = new app.Models.RequestModel();
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
            var html  = this.$el.html(this.template({ model: this.model }));
            $('#container').append(html);

            this.$('#startDate').datepicker({
                autoclose: true,
                todayHighlight: true
            });

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
        requestSubmit: function () {
            this.model.save();
        },
        //remove: function () {
        //    this.$el.empty().off(); /* off to unbind the events */
        //    this.stopListening();
        //    return this;
        //}
    });
});
