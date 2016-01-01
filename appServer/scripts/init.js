$(function () {
    app.Router = Backbone.Router.extend({
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
            app.topBar = new app.Views.TopBarView();
        },
        home: function (userId) {
            app.Controller.changePage(app.Views.TravelsView, {});
        },
        travel: function (query) {
            app.Controller.changePage(app.Views.TravelView, { query: query, model: app.CurrentTicket });
            app.CurrentTicket = undefined;
        },
        newTravel: function () {
            app.Controller.changePage(app.Views.NewTravelView, {});
        },
        travels: function () {
            app.Controller.changePage(app.Views.TravelsView, { cleanPast: true });
        },
        request: function (query) {
            app.Controller.changePage(app.Views.RequestView, { query: query, model: app.CurrentRequest });
            app.CurrentRequest = undefined;
        },
        newRequest: function () {
            app.Controller.changePage(app.Views.NewRequestView, {});
        },
        requests: function () {
            app.Controller.changePage(app.Views.RequestsView, { cleanPast: true });
        },
        conversation: function (params) {
            var toUserId = getParameterByName('toUserId');
            var requestId = getParameterByName('requestId');
            var travelId = getParameterByName('travelId');
            var id = getParameterByName('id');
            app.Controller.changePage(app.Views.ConversationView, {
                id: id,
                toUserId: toUserId,
                travelId: travelId,
                requestId: requestId
            });
        },
        conversations: function () {
            app.Controller.changePage(app.Views.ConversationsView, { cleanPast: true });
        },
        createNew: function () {
            app.Controller.changePage(app.Views.CreateNewView, { cleanPast: true });
        },
        login: function () {
            app.Controller.changePage(app.Views.LoginView, {});
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

    if (app.isCordova) {
        document.addEventListener('deviceready', onDeviceReady, false);

        function onDeviceReady() {
            document.addEventListener('pause', onPause.bind(this), false);
            document.addEventListener('resume', onResume.bind(this), false);

            app.RouterInstance = new app.Router;
            Backbone.history.start({ pushState: true, hashChange: false });
            facebookInit();
          
        }

        function onPause() {

        }

        function onResume() {

        }
    } else {
        facebookInit();
       // vkInit();
        app.RouterInstance = new app.Router;
        Backbone.history.start({ pushState: true, hashChange: false });
        Backbone.history.on('route', function () {
            // Do your stuff here
        });
    }

    function vkInit() {
        VK.init(function () {
            // API initialization succeeded 
            // Your code here 
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
            js.src = "scripts/facebook/sdk.js"; // "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
    }
});