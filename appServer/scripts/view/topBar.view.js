$(function () {
    app.Views.TopBarView = Backbone.View.extend({
        //template: template('bannerTemplate'),
        //el: '#topBar',
        //events: {
        //    'click #loginLink': 'login'
        //},
        backViewTemplate: template('backViewTemplate'),
        initialize: function (params) {
            //this.listenTo(app.Events, 'loggedIn', this.loggedIn);
            this.listenTo(app.Events, 'newPageRendered', this.newPageRendered);

            this.render(params);
            $.signalR.ajaxDefaults.headers = { Authorization: "Bearer " + localStorage['bearerAccessToken'] };
            app.chat = $.connection.chatHub;
            $.connection.hub.start();
            app.chat.client.SendMessage = function (message) {
                alert(message);
            };

        },
        render: function (params) {
            app.Me.get('photo', function (photo) {
                $('#userImage').attr({ 'src': 'data:image/png;base64,' + photo })
            });
            app.Me.get('firstName', function (firstName) {
                $('#userFirstName').text(firstName);
            });

            //this.$el.html(this.template());
            return this;
        },
        setBackArrow:function(link,name){
            $('#topBarLeft').html(this.backViewTemplate({ backViewLink: link, backViewName: name }));
        },
        removeBackArrow: function () {
            $('#topBarLeft').empty();
        },
        newPageRendered: function (view) {
            //this.backViewTemplate({backViewLink: view.backViewLink});

            //view.topBarCenter();
            //view.topBarLeft();
        },
        pageRemoved: function () {

        }
        //login: function () {
        //    new app.Views.LoginView();
        //}
    });
});
