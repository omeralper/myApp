$(function () {
    app.Views.CityPicker = Backbone.View.extend({
        template: template('cityPickerTemplate'),
        events: {
            'change #countryPicker': 'onCountryChange'
        },
        initialize: function (params) {
            this.render(params);
        },
        render: function (params) {
            this.data = { countries: app.Globals.countries, cities: app.Globals.states };
            this.$el.html(this.template(this.data));
            params.container.append(this.$el);

            $('#cityPicker').selectpicker('hide');
            $('#countryPicker').selectpicker('refresh');

            return this;
        },
        onCountryChange: function (e) {
            var cityPicker = $('#cityPicker');
            cityPicker.empty();
            var cities = this.data.cities[e.target.value];
            _.each(cities, function (val) {
                cityPicker.append($('<option></option>').val(val.code).html(val.name));
            });
            $('#cityPicker').selectpicker('show');
            $('#cityPicker').selectpicker('refresh');
            
        }
    });
});
