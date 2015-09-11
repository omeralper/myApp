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

        },
        home: function (userId) {
            this.clearViews();
        },

    });

    document.addEventListener('deviceready', onDeviceReady, false);

    function onDeviceReady() {
        document.addEventListener('pause', onPause.bind(this), false);
        document.addEventListener('resume', onResume.bind(this), false);

        app.RouterInstance = new app.Router;
        Backbone.history.start({ pushState: true });
        //alert('1');
        //facebookConnectPlugin.browserInit("1462573160734571");
   
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