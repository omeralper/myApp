$(function () {
    app.Views.TopBarView = Backbone.View.extend({
        el: '#topBar',
        events: {
            'click #openTicketFilter': 'openTicketFilter',
            'click #openRequestFilter': 'openRequestFilter'
        },
        backViewTemplate: template('backViewTemplate'),
        initialize: function (params) {
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
            $.connection.hub.url = "https://192.168.1.29:448/SignalR/signalr";
            $.signalR.ajaxDefaults.headers = { Authorization: "Bearer " + localStorage['bearerAccessToken'] };
            app.chat = $.connection.chatHub;
            $.connection.hub.start();
            app.chat.client.SendMessage = function (message) {
                alert(message);
            };

        },
        render: function (centerHtml, rightHtml) {
            //app.Me.get('photo', function (photo) {
            //    $('#userImage').attr({ 'src': 'data:image/png;base64,' + photo })
            //});
            //app.Me.get('firstName', function (firstName) {
            //    $('#userFirstName').text(firstName);
            //});
            //this.$el.html(this.template());
            this.$('#topBarCenter').empty().html(centerHtml);
            this.$('#topBarRight').empty().html(rightHtml);

            return this;
        },
        setBackArrow:function(link,name){
            this.$('#topBarLeft').html(this.backViewTemplate({ backViewLink: link, backViewName: name }));
        },
        removeBackArrow: function () {
            this.$('#topBarLeft').empty();
        },
        openTicketFilter: function () {
            if (this.ticketFilterView) {
                this.ticketFilterView.reAppear();
            } else {
                this.ticketFilterView = new app.Views.TicketFilterView();
            }
        },
        openRequestFilter : function(){
            if (this.requestFilterView) {
                this.requestFilterView.reAppear();
            } else {
                this.requestFilterView = new app.Views.RequestFilterView();
            }
        },
        pageRemoved: function () {

        },
        //setStatusBar: function (msg) {
        //    this.$('.status-bar').text(msg).slideDown("fast");
        //}
        //login: function () {
        //    new app.Views.LoginView();
        //}
    });
});
