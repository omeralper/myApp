$(function () {
    app.Views.RequestView = app.Views.MainView.extend({
        className: 'container',
        attributes: { id: 'requestView' },
        requestDetailTemplate: template('requestDetailTemplate'),
        events: {
            'change input': 'change',
            'change select': 'change',
            'click #submitTicket ': 'ticketSubmit',
            'tap #submitTicket ': 'ticketSubmit'
        },
        name:'Request',
        initialize: function (params) {
            this.fragment = params.fragment;
            var params = params;
            if (params.model) {//direct open
                this.model = params.model;
                this.render(params);
            } else if (params.query) {//request by id
                var thisView = this;
                this.model = new app.Models.RequestModel({ id: params.query });
                this.model.fetch({
                    success: function (model) {
                        thisView.render(params);
                    }
                });
            }
        },
        render: function (params) {
            $('#container').removeClass('animated fadeInRight');
            var html = this.$el.html(this.requestDetailTemplate({ model: this.model }));
            $('#container').append(html).addClass('animated fadeInRight');
            return this;

            //$('#container').removeClass('animated fadeInRight');
            //var html = this.$el.html(this.ticketDetailTemplate({ model: this.model }));//.hide().fadeIn().slideDown();;
            //$('#container').append(html).addClass('animated fadeInRight');//.delay(1000).removeClass('fadeInRight');//.hide().toggle("slide", { direction: 'right' });;

            //this.$('#startDate').datepicker({
            //    autoclose: true,
            //    todayHighlight: true
            //})
            //return this;
        },
        change: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            this.model.set(changed.id, value);
        }
    });
});
