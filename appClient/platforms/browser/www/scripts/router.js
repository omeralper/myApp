$(function () {
    //MyRouter = Backbone.Router.extend({
    //    constructor: function (options) {
    //        this.on("all", this.storeRoute);
    //        this.history = [];
    //        //Backbone.constructor.apply(this, arguments);
    //        MyRouter.__super__.constructor.call(this, options);
    //    },
    //    storeRoute: function () {
    //        this.history.push(Backbone.history.fragment);
    //    },
    //    previous: function () {
    //        if (this.history.length > 1) {
    //            return this.navigate(this.history[this.history.length - 2], true);
    //        }
    //    }
    //});

    app.Router = Backbone.Router.extend({
        currentViews: [],
        clearViews: function () {
            for (var i = 0 ; i < this.currentViews.length ; i++)
                this.currentViews[i].remove();
        },
        initialize: function () {
            new app.Views.TravelerView();
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
});

