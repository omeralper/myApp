$(function () {
    app.Views.RequestView = app.Views.MainView.extend({
        attributes: { id: 'requestView' },
        requestDetailTemplate: template('requestDetailTemplate'),
        events: {
            'change input': 'change',
            'change select': 'change'
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
            var html = this.$el.html(this.requestDetailTemplate({ model: this.model, params: params }));
            $('#container').append(html);
            return this;
        },
        change: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            this.model.set(changed.id, value);
        }
    });
});
