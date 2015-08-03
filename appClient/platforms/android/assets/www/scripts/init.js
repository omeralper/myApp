// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397704
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.
(function () {
    "use strict";


    app.Router = Backbone.Router.extend({
        currentViews: [],
        clearViews: function () {
            for (var i = 0 ; i < this.currentViews.length ; i++)
                this.currentViews[i].remove();
        },
        initialize: function () {
            new app.Views.TabListView();
            //new app.Views.UserView();
        },
        routes: {
            '': 'home',
            //'Login': 'loginPopup',
            //'MyDemands': 'myDemands',
            //'Inbox': 'inbox',
            //'Inbox/:id': 'conversation',
            //'Item/:id': 'item',
            //'Item': 'item',
            //'Wanted/:id': 'filter',
            //'Wanted': 'filter',
            //'*user': 'home'
        },
        home: function (userId) {
            //if (!app.Globals.TradeModel)
            this.clearViews();
            //if (tempTrade)
            //    app.Globals.TradeModel = tempTrade;
            //this.currentViews = [new app.Views.SideView({ userId: userId, tradeType: 'filter' })];
        },
        //item: function (id) {
        //    var id = id || undefined;
        //    this.clearViews();
        //    this.currentViews = [new app.Views.TradeView({ id: id, tradeType: 'item' })];
        //},
        //filter: function (id) {
        //    var id = id || undefined;
        //    this.clearViews();
        //    this.currentViews = [new app.Views.TradeView({ id: id, tradeType: 'filter' })];
        //},
        //loginPopup: function () {
        //    new app.Views.LoginView();
        //},
        //myDemands: function () {
        //    this.clearViews();
        //    this.currentViews = [new app.Views.MyDemandsView()];
        //},
        //inbox: function () {
        //    this.clearViews();
        //    this.currentViews = [new app.Views.InboxView()];
        //},
        //conversation: function (id) {
        //    this.clearViews();
        //    this.currentViews = [new app.Views.ConversationView({ id: id })];
        //},
    });


    document.addEventListener('deviceready', onDeviceReady, false);

    function onDeviceReady() {
        document.addEventListener('pause', onPause.bind(this), false);
        document.addEventListener('resume', onResume.bind(this), false);

        app.RouterInstance = new app.Router;
        Backbone.history.start({ pushState: true });
    }

    function onPause() {

    }

    function onResume() {

    }




    //document.addEventListener( 'deviceready', onDeviceReady.bind( this ), false );

    //function onDeviceReady() {
    //    // Handle the Cordova pause and resume events


    //    init();
    //};

    //function onPause() {
    //    // TODO: This application has been suspended. Save application state here.
    //};

    //function onResume() {
    //    // TODO: This application has been reactivated. Restore application state here.
    //};



 


    //function init() {
    //    Router = new app.Router;
    //    Backbone.history.start({ pushState: true });
    //};
})();