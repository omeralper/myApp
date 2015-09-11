$(function () {
    app.Views.ConversationView = app.Views.MainView.extend({
        attributes: { id: 'conversationView' },
        template: template('conversationTemplate'),
        bannerRentTemplate: template('conversationBannerTemplate'),
        events: {
            'click #sendMessage': 'sendMessage',
            'tap #sendMessage': 'sendMessage'
        },
        name:'Message',
        initialize: function (params) {
            this.fragment = params.fragment;
            var thisView = this;
            this.model = new app.Models.ConversationModel({
                id: params.id,
                toUserId: params.toUserId,
                requestId: params.requestId,
                travelId: params.travelId
            });
            this.model.fetch({
                data: {
                    id: params.id,
                    toUserId: params.toUserId,
                    requestId: params.requestId,
                    travelId: params.travelId
                },
                success: function (model, object, evt) {
                    thisView.render(model);
                }
            });

            
        },
        render: function (model) {
            //$('#bottomBar').animate({
            //    display: 'none'
            //});
            //$('#bottomBar').slideDown();

            //$('#bannerRent').html(this.bannerRentTemplate({ photo: model.get('otherUser').photo, userName: model.get('otherUser').firstName }));
            
            var thisView = this;
            app.Me.get('photo', function (photo) {
                var html = thisView.$el.html(thisView.template({ model: model, photo: photo }));
                $('#container').append(html);
                $('#scroller').scrollTop($('#scroller')[0].scrollHeight);

            });
            return this;
        },
        sendMessage: function () {
            var body = this.$('#messageArea').val();
            var newMessage = new app.Models.MessageModel({
                messageBody: body,
                conversationId: this.model.id,
                toUser: this.model.get('otherUser').userId
            });
            newMessage.save({},{
                success: function () {
                    //debugger;
                }
            });
            //var toUser = this.model.get('otherUser').userId;
            //app.chat.server.sendMessage(body, toUser);
        },
        //remove: function () {
        //    //$('#bottomBar').animate({ display: 'block' });

        //    this.$el.empty().off(); /* off to unbind the events */
        //    this.stopListening();
        //    return this;
        //}
    });
});
