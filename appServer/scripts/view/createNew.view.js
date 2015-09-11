$(function () {
    app.Views.CreateNewView = app.Views.MainView.extend({
        attributes: { id: 'createNewView' },
        template: template('createNewTemplate'),
        name: 'New Request',
        initialize: function (params) {
            this.fragment = params.fragment;
            this.render(params);
        },
        render: function (params) {
            var html = this.$el.html(this.template());
            $('#container').append(html);
            return this;
        }
    });
});
