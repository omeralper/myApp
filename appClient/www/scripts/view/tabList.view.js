$(function () {
    app.Views.TabListView = Backbone.View.extend({
        template: template('tabListTemplate'),
        events: {
        },
        subViews: [],
        initialize: function () {
            this.subViews = [];
            this.render();
        },
        render: function () {
            this.$el.html(this.template());
            $('#main-container').append(this.$el);

            $('#myTabs a').click(function (e) {
                e.preventDefault();
                $(this).tab('show');
            });

            this.addTabs();
            var thisView = this;
            //gerizekali bootstrap'in tablarini boyle dinliyoruz.
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                var target = $(e.target).attr("href");
                if ((target == '#travelList')) {
                    thisView.travelListView.refreshList();
                    //alert('ok');
                } 
            });
            return this;
        },
        addTabs : function(){
            var tabContainer = this.$el.find('#tab');
            var travelView = new app.Views.TravelView({ container: tabContainer });
            this.subViews.push(travelView);

            var requestorView = new app.Views.RequestorView({ container: tabContainer });
            this.subViews.push(requestorView);

            var travelListView = new app.Views.TravelListView({ container: tabContainer });
            this.travelListView = travelListView;
            this.subViews.push(travelListView);
        },
        onTravelSubmit: function () {
          
            var travelModel = new app.Models.TravelModel();
            //travelModel.set('id', '1');
            //travelModel.fetch();
            travelModel.save(obj);
        }
    });
});
