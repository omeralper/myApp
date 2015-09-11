$(function () {
    app.Views.TravelsView = app.Views.MainView.extend({
        //className:'container-fluid',
        id:'tickets',
        attributes: { id: 'travelsView' },
        template: template('travelsTemplate'),
        ticketTemplate: template('ticketTemplate'),
        //bannerRentTemplate: template('bannerForTicketTemplate'),
        events: {
            'change input': 'change',
            'change select': 'change',
            'click #ticket' : 'clickTicket'
            //'focusout #filterTravel' : 'refreshList'  
        },
        initialize: function (params) {
            this.fragment = params.fragment;
            this.name = 'Tickets';
            //this.model = new app.Models.TravelFilterModel();
            this.listenTo(app.Events, 'ticketSearch', this.refreshList);
            this.collection = new app.Collections.TravelCollection();
            _.bindAll(this, 'refreshList', 'collectionFetched');
            this.render(params);
        },
        render: function (params) {
            var html = this.$el.html(this.template());
            $('#container').append(html);
            //$('#bannerRent').html(this.bannerRentTemplate());

            //if (params.backViewLink)
            //    $('#backView').html(this.backViewTemplate(params));

            this.refreshList();
            return this;
        },
        refreshList: function (model) {
            //if (evt && !evt.target.value)
            //    return;
            var searchData = model ? model.toJSON() : '';
            this.collection.fetch({
                data: searchData,
                success: this.collectionFetched
            });
        },
        collectionFetched: function (collection, resp, opt) {
            var thisView = this;
            this.$('#travelsItems').empty();
            collection.each(function (model) {
                thisView.$el.find('#travelsItems').append(thisView.ticketTemplate({ model: model, param: {} }));
            });
            var message = collection.length == 1 ? '1 ticket found' : collection.length == 0 ? 'No ticket found' :  collection.length + ' tickets found';
            app.Events.trigger('statusBarMessage', message);
        },
        //change: function (evt) {
        //    var changed = evt.currentTarget;
        //    var value = $(evt.currentTarget).val();
        //    this.model.set(changed.id, value);
        //    this.refreshList(evt);
        //},
        clickTicket: function(evt){
            evt.preventDefault();
            var ticketid = this.$(evt.currentTarget).data('ticketid');
            app.CurrentTicket= this.collection.get(ticketid);
            app.RouterInstance.navigate('Travel/' + ticketid, { trigger: true });
        },
       
    });
});
