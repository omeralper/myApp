$(function () {
    app.Router = Backbone.Router.extend({
        activeViews: [],
        clearViews: function () {
            //this.currentViews.push
            //for (var i = 0 ; i < this.currentViews.length ; i++)
            //    this.currentViews[i].remove();
        },
        routes: {
            '': 'home',
            'NewTravel': 'newTravel',
            'Travel/:query': 'travel',
            'Travels': 'travels',
            'NewRequest': 'newRequest',
            'Request/:query': 'request',
            'Requests': 'requests',
            'Conversation': 'conversation',
            'Conversations': 'conversations',
            'CreateNew': 'createNew',
            'Login' : 'login',
            '*notFound': 'home',
        },

        initialize: function () {
            this.topBar = new app.Views.TopBarView();
        },
        changePage: function (view, options) {
            var fragment = Backbone.history.getFragment();
            
            var currentView = this.activeViews[this.activeViews.length - 1];
            var previousView = this.activeViews[this.activeViews.length - 2];
            var backView = this.activeViews[this.activeViews.length - 3];

            if (previousView && previousView.fragment == fragment) {//go back
                var lastView = this.activeViews.pop();
                lastView.slide().remove();
                $('.ui-effects-wrapper').remove();

                if (backView) {
                    var link = backView.fragment ? backView.fragment : app.Root;
                    this.topBar.setBackArrow(link, backView.name);
                } else {
                    this.topBar.removeBackArrow();
                }
                previousView.slide();
            } else {//go forward
                var thisRouter = this;
                if (currentView) {
                    currentView.hide();
                    var link = currentView.fragment ? currentView.fragment : app.Root;
                    this.topBar.setBackArrow(link, currentView.name);
                }

                options.fragment = fragment;
                this.activeViews.push(new view(options));
            }
        },
        home: function (userId) {
            this.changePage(app.Views.TravelsView, {});
        },
        travel: function (query) {
            this.changePage(app.Views.TravelView, { query: query, model: app.CurrentTicket });
            app.CurrentTicket = undefined;
        },
        newTravel : function(){
            this.changePage(app.Views.NewTravelView, {});
        },
        travels: function () {
            this.changePage(app.Views.TravelsView, {});
        },
        request: function (query) {
            this.changePage( app.Views.RequestView, { query: query });
            app.CurrentRequest = undefined;
        },
        newRequest : function(){
            this.changePage(app.Views.NewRequestView, {});
        },
        requests: function () {
            this.changePage(app.Views.RequestsView, {});
        },
        conversation: function (params) {
            var toUserId = getParameterByName('toUserId');
            var requestId = getParameterByName('requestId');
            var travelId = getParameterByName('travelId');
            var id = getParameterByName('id');
            this.changePage(app.Views.ConversationView, {
                id: id,
                toUserId: toUserId,
                travelId: travelId,
                requestId: requestId
            });
        },
        conversations: function () {
            this.changePage(app.Views.ConversationsView, {});
        },
        createNew: function () {
            this.changePage(app.Views.CreateNewView, {});
        },
        login: function () {
            this.changePage(app.Views.LoginView, {});
        }
    });

    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    _.extend(app.Events, Backbone.Events);

    $(document).on("click tap", "a:not([data-bypass])", function (evt) {
        var href = { prop: $(this).prop("href"), attr: $(this).attr("href") };
        var root = location.protocol + "//" + location.host + app.Root;

        if (href.prop && href.prop.slice(0, root.length) === root) {
            evt.preventDefault();
            if (href.attr === app.Root)
                href.attr = '/';
            Backbone.history.navigate(href.attr, true);
        }
    });

    if (app.isMobile) {
        document.addEventListener('deviceready', onDeviceReady, false);

        function onDeviceReady() {
            document.addEventListener('pause', onPause.bind(this), false);
            document.addEventListener('resume', onResume.bind(this), false);

            app.RouterInstance = new app.Router;
            Backbone.history.start({ pushState: true });

            //try {
            //    facebookConnectPlugin.login(["email"],
            //        function () {
            //            alert('success');
            //        },
            //        function () {
            //            alert('fail');
            //        });
            //} catch (e) {
            //    alert(e);
            //}

         
        }

        function onPause() {

        }

        function onResume() {

        }
    } else {
        facebookInit();
        app.RouterInstance = new app.Router;
        Backbone.history.start({ pushState: true, hashChange: false, root: app.Root });
        Backbone.history.on('route', function () {
            // Do your stuff here
        });
    }

    function facebookInit() {
        window.fbAsyncInit = function () {
            FB.init({
                appId: '1462573160734571',
                xfbml: true,
                version: 'v2.4'
            });
        };

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement(s); js.id = id;
            js.src =  app.Root + "/scripts/facebook/sdk.js"; // "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
    }
});