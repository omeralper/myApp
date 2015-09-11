$(function () {
    app.Views.TravelView = app.Views.MainView.extend({ // Backbone.View.extend({
        attributes: { id: 'travelView' },
        ticketDetailTemplate: template('ticketDetailTemplate'),
        events: {
            'change input': 'change',
            'change select': 'change',
            'click #submitTicket ': 'ticketSubmit',
            'tap #submitTicket ': 'ticketSubmit'
        },
        name:'Ticket',
        initialize: function (params) {
            this.fragment = params.fragment;
            var params = params;
            if (params.model) {//direct open
                this.model = params.model;
                this.render(params);
            } else  if (params.query) {//travel by id
                    var thisView = this;
                    this.model = new app.Models.TravelModel({ id: params.query });
                    this.model.fetch({
                        success: function (model) {
                            thisView.render(params);
                        }
                    });
            }
        },
        render: function (params) {
            var html = this.$el.html(this.ticketDetailTemplate({ model: this.model }));//.hide().fadeIn().slideDown();;
            $('#container').append(html).hide().toggle("slide", { direction: 'right' });;

            this.$('#startDate').datepicker({
                autoclose: true,
                todayHighlight: true
            })
            return this;
        },
        change: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            this.model.set(changed.id, value);
        },
        ticketSubmit: function () {
            this.model.save();
        },
    });
});
