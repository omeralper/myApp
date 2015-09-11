$(function () {
    app.Models.UserModel = Backbone.Model.extend({
        defaults: {
            authServer: '',
            authServerId: ''
        },
        url: app.Root + '/api/user'
    });
    app.Me = new app.Models.UserModel();
    
    app.Me.get = function (attr,callback) {
        if (this.attributes[attr]) {
            return callback(this.attributes[attr]);
        }
        else {
            var thisModel = this;
            if (this.deferred && this.deferred.state() == "pending") {
                this.deferred.then(function () {
                    return callback(thisModel.attributes[attr]);
                });
            } else {
                this.deferred = this.fetch({
                    success: function (model, obj) {
                        callback(obj[attr]);
                    }
                });
            }
        }
        return Backbone.Model.prototype.get.call(this, attr);
    }

});