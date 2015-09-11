$(function () {
    app.Views.TravelsView = app.Views.MainView.extend({
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
            this.model = new app.Models.TravelFilterModel();
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
        refreshList: function (evt) {
            //if (evt && !evt.target.value)
            //    return;
            this.collection.fetch({
                data: this.model.toJSON(),
                success: this.collectionFetched
            });
        },
        collectionFetched: function (collection, resp, opt) {
            var thisView = this;
            this.$el.find('#travelsItems').empty();
            collection.each(function (model) {
                thisView.$el.find('#travelsItems').append(thisView.ticketTemplate({ model: model, param: {} }));
            });
        },
        change: function (evt) {
            var changed = evt.currentTarget;
            var value = $(evt.currentTarget).val();
            this.model.set(changed.id, value);
            this.refreshList(evt);
        },
        clickTicket: function(evt){
            evt.preventDefault();
            var ticketid = $(evt.currentTarget).data('ticketid');
            app.CurrentTicket= this.collection.get(ticketid);
            app.RouterInstance.navigate('Travel/' + ticketid, { trigger: true });
        },
       
    });
});
