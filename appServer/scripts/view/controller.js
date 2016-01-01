$(function () {
    app.Controller = {
        viewStack: [],
        changePage : function(view, options) {
            var fragment = Backbone.history.getFragment();
            var currentView = this.viewStack[this.viewStack.length - 1];
            var previousView = this.viewStack[this.viewStack.length - 2];

            if (options.cleanPast) {
                this.cleanHistory();
                options.fragment = fragment;
                this.viewStack.push( new view(options));

            } else if (previousView && previousView.fragment == fragment) {//go back
                var lastView = this.viewStack.pop();
                lastView//.slide()
                    .remove();
                //$('.ui-effects-wrapper').remove();
                previousView.slide();
            } else {//go forward
                if (currentView) {
                    currentView.hide();
                }
                options.fragment = fragment;
                this.viewStack.push(new view(options));
            }
            this.setTopBarHistory();
        },
        cleanHistory: function () {
            _.each(this.viewStack, function (thisView) {
                thisView.remove();
            });
            this.viewStack = [];
        },
        setTopBarHistory: function () {
            var currentView = this.viewStack[this.viewStack.length - 1];
            var previousView = this.viewStack[this.viewStack.length - 2];
            if (previousView) {
                var link = previousView.fragment ? previousView.fragment : app.Root;
                app.topBar.setBackArrow(link, previousView.name);
            } else {
                app.topBar.removeBackArrow();
            }
        }
    }
});