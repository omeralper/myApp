$(function () {
    app.Views.TopBarView = Backbone.View.extend({
        //template: template('bannerTemplate'),
        el: '#topBar',
        events: {
            'click #openTicketFilter' : 'openTicketFilter'
        },
        ticketsTopBar: template('ticketsTopBar'),
        backViewTemplate: template('backViewTemplate'),
        initialize: function (params) {
            //this.listenTo(app.Events, 'loggedIn', this.loggedIn);
            this.listenTo(app.Events, 'newPageRendered', this.newPageRendered);
            this.listenTo(app.Events, 'statusBarMessage', this.setStatusBar);
            //this.listenTo(app.Events, 'ticketList', this.ticketFilterLoad);

            var lastScrollTop = 0;
            var thisView = this;
            $(window).scroll(function () {
                var st = $(this).scrollTop();
                if (st > lastScrollTop && thisView.$('.status-bar').text()) {
                    thisView.$('.status-bar').removeClass('animated fadeInDown').addClass('animated fadeOutUp');
                    //thisView.$('.status-bar').slideDown("fast");
                } else {
                    thisView.$('.status-bar').removeClass('animated fadeOutUp').addClass('animated fadeInDown');
                        //thisView.$('.status-bar').slideUp("fast");
                }
                lastScrollTop = st;
            });

            this.render(params);
            $.connection.hub.url = "http://192.168.1.29:448/SignalR/signalr";
            $.signalR.ajaxDefaults.headers = { Authorization: "Bearer " + localStorage['bearerAccessToken'] };
            app.chat = $.connection.chatHub;
            $.connection.hub.start();
            app.chat.client.SendMessage = function (message) {
                alert(message);
            };

        },
        render: function (params) {
            //app.Me.get('photo', function (photo) {
            //    $('#userImage').attr({ 'src': 'data:image/png;base64,' + photo })
            //});
            //app.Me.get('firstName', function (firstName) {
            //    $('#userFirstName').text(firstName);
            //});

            //this.$el.html(this.template());
            return this;
        },
        setBackArrow:function(link,name){
            this.$('#topBarLeft').html(this.backViewTemplate({ backViewLink: link, backViewName: name }));
        },
        removeBackArrow: function () {
            this.$('#topBarLeft').empty();
        },
        newPageRendered: function (view) {
            this.$('#topBarCenter').empty().html(view.name);
            this.$('.status-bar').empty().slideUp("fast");;
            this.statusBarActive = false;
            if (view.id == 'tickets') {
                this.$('#topBarRight').html(this.ticketsTopBar());
            }
            //this.backViewTemplate({backViewLink: view.backViewLink});
            //view.topBarCenter();
            //view.topBarLeft();
        },
        openTicketFilter: function () {
            
            if (this.ticketFilterView) {
                this.ticketFilterView.reAppear();
            } else {
                this.ticketFilterView = new app.Views.TicketFilterView();
            }
        },
        pageRemoved: function () {

        },
        setStatusBar: function (msg) {
            this.$('.status-bar').text(msg).slideDown("fast");
        }
        //login: function () {
        //    new app.Views.LoginView();
        //}
    });
});
