$(function () {
    app.Views.LoginView = app.Views.MainView.extend({
        template: template('loginTemplate'),
        events: {
            'click #facebookLogin': 'facebookLogin',
            'click #googleLogin': 'googleLogin',
            'click #twitterLogin': 'twitterLogin',
            'click #vkLogin': 'vkLogin'
        },
        name:'Login',
        initialize: function (params) {
            this.fragment = params.fragment;
            _.bindAll(this, 'tokenCallBack');
            this.model = new app.Models.LoginModel();
            this.render(params);
        },
        render: function (params) {
            $('#container').append(this.$el.html(this.template()));

            //this.$el.bPopup({
            //    position: ['auto', 100],
            //    //speed: 50,
            //    onClose: function () {
            //        thisView.remove();
            //    }
            //}).reposition();
            return this;
        },
        facebookLogin: function () {
            var thisView = this;

            if (app.isCordova) {
                try {
                    var ref = cordova.InAppBrowser.open('https://www.facebook.com/dialog/oauth?client_id=1462573160734571&redirect_uri=https://www.facebook.com/connect/login_success.html', '_blank', {});
                    //alert('before facebook');
                    //facebookConnectPlugin.login(["email"],
                    //    function () {
                    //        alert('success');
                    //    },
                    //    function () {
                    //        alert('fail');
                    //    });
                } catch (e) {
                    alert(e);
                }
            } else {
                FB.login(function (result) {
                    thisView.model.save(
                        {
                            authServer: 'facebook', authAccessToken: result.authResponse.accessToken
                        },
                        {
                            success: thisView.tokenCallBack
                        }
                    );
                }, {
                    scope: 'publish_actions,email,user_friends'
                });
            }
        },
        googleLogin : function(){
            alert('not implemented yet')
        },
        twitterLogin: function () {
            alert('not implemented yet')
        },
        vkLogin: function () {
            alert('not implemented yet')
        },
        tokenCallBack: function (model, obj, evt) {
            localStorage['bearerAccessToken'] = obj.bearerAccessToken;

            app.Me.fetch({
                success: function (model,obj,evt) {
                    app.Events.trigger('loggedIn', model);
                }
            });

            this.$el.bPopup().close();
            this.remove();
        }
    });
});
