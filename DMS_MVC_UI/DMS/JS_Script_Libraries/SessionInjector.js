function ConfigureSessionInjector(authInfo, appName) {
    var sessionInjector = function () {
        return {
            request: function (config) {
                config.headers['Authorization'] = authInfo.AuthorizationHeader;
                config.headers['Access-Control-Allow-Credentials'] = authInfo.AllowCredentials;
                config.headers['TokenId'] = authInfo.TokenId;
                return config;
            }
        }
    }

    angular.module(appName).config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push(sessionInjector);
    }]);

}