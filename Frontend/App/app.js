var apiBaseUrl = 'http://localhost/Backend/api/v1';

var app = new Vue({
    el: '#app',
    data: {
        clientId: '',
        searchString: '',
        searchResults: [],
        favorites: [],
        searching: false,
        searchMode: true
    },
    methods: {
        search: function () {

            if (!this.searchString) return;

            this.searching = true;

            this.searchResults = [];

            var searchUrl = apiBaseUrl + '/Search?clientId=' + this.clientId + '&searchString=' + this.searchString;

            this.$http.get(searchUrl).then(function (response) {
                this.searchResults = response.body;
                this.searching = false;

            }, function (response) {
                this.searchResults = [];
                this.searching = false;
            });


        },
        addToFavorites: function (repository) {

            var favoritesUrl = apiBaseUrl + '/Clients/' + this.clientId + '/Favorites/';

            this.$http.post(favoritesUrl, repository).then(function (response) {
                if (response.status == '201') {
                    repository.isFavorite = true;
                    this.$http.get(favoritesUrl).then(function (response) {
                        this.favorites = response.body;
                    }, function (response) {
                    });
                }
            }, function (response) {
            });
        },
        removeFromFavorites: function (repositoryKey) {

            var favoritesUrl = apiBaseUrl + '/Clients/' + this.clientId + '/Favorites/';

            this.$http.delete(favoritesUrl + repositoryKey).then(function (response) {
                if (response.status == '200') {
                    // sucessfully removed  
                    this.$http.get(favoritesUrl).then(function (response) {
                        this.favorites = response.body;
                    }, function (response) {
                    });
                }
            }, function (response) {
            });
        }
    },
    created: function () {

        // authenticate client

        if (localStorage.getItem("clientId")) {
            this.clientId = localStorage.getItem("clientId")
        }
        else {
            this.$http.post(apiBaseUrl + '/Authenticate').then(function (response) {
                this.clientId = response.body;
                localStorage.setItem("clientId", this.clientId);
            }, function (response) {
            });
        }

        this.$http.get(apiBaseUrl + '/Clients/' + this.clientId + '/Favorites/').then(function (response) {
            this.favorites = response.body;
        }, function (response) {
        });
    }
});
