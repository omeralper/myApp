$(function () {
    app.Views.MainView = Backbone.View.extend({
        subViews: [],
        backViewTemplate: template('backViewTemplate'),
        className: 'container-sub',
        slide:function(callback){
            this.$el.toggle("slide", {}, 200, callback);//.delay(300);
            return this;
        },
        hide: function () {
            this.$el.hide();
            return this;
        },
        render:function(){
            app.Events.trigger('newPageRendered', this);
            return this;
        },
        remove: function () {
            $('#bannerRent').empty();
            $('#backView').empty();
            Backbone.View.prototype.remove.apply(this, arguments);
            return this;
        }
    });
});
