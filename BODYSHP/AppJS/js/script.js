var app = angular.module('homeApp', ['ngRoute',
    'ui.bootstrap',
    'ui.select', 'ngSanitize',
    'ngAnimate',   
    '720kb.datepicker', 'ngLoadingSpinner', 
    'angularjs-dropdown-multiselect', 'BrandMasterModule','StatusMasterModule',
    'angularUtils.directives.dirPagination', 'DashBoardModule',   
    'UploadExcelModule', 'GridViewModule', 'BodyShopPreviewModule', 'ReportModule'
    , 'ContractorReportModule', 'ReadyJobCardsModule', 'StatusWiseModule', 'ContractorMasterModule',
    'EmailModule', 'InsCmpMasterModule', 'MonthWiseModule', 'DetailForJobcardModule', 'MonthAndFuelTypeModule','TESTModule']);

app.factory('httpRequestInterceptor', function () {
    return {
        request: function (config) {
            var session = angular.fromJson(sessionStorage.getItem("app")) || {};
            console.log(session.data.userAuthKey);
            config.headers['Authorization'] = session.data.userAuthKey;
            console.log(config);
            return config;
        },

        responseError: function (response) {

        }
    };
    
    var savedData = {};
    function set(data) {
        savedData = data;
    }
    function get() {
        return savedData;
    }
    
});
app.factory('myService', function () {
   
    var savedData = {}
    function set(data) {
        savedData = data;
    }
    function get() {
        return savedData;
    }

    return {
        set: set,
        get:get
    }
});

app.filter('percentage', ['$filter', function ($filter) {
    return function (input, decimals) {
        return $filter('number')(input * 100, decimals) + '%';
    };
}]);

app.filter('propsGL', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            var keys = Object.keys(props);

            items.forEach(function (item) {
                var itemMatches = false;

                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    // var text1 = props['CostCenter_code'].toLowerCase();
                    var text2 = props['SkuDesc'].toLowerCase();
                    if (item['SkuCode'].toString().toLowerCase().indexOf(text2) !== -1 || item['SkuDesc'].toString().toLowerCase().indexOf(text2) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});

app.filter('propsDL', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            var keys = Object.keys(props);

            items.forEach(function (item) {
                var itemMatches = false;

                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    // var text1 = props['CostCenter_code'].toLowerCase();
                    var text2 = props['DealerName'].toLowerCase();
                    if (item['DealerCode'].toString().toLowerCase().indexOf(text2) !== -1 || item['DealerName'].toString().toLowerCase().indexOf(text2) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});

app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);
app.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});
app.config(function ($routeProvider, $httpProvider) {
    $routeProvider
         .when('/', {
             templateUrl: 'Partial/DashBoard.html',
             controller: 'DashBoardController'
         })
        .when('/Excel', {
            templateUrl: "Partial/UploadExcel.html",
            controller: 'UploadExcelController'
        })
        .when('/Grid', {
            templateUrl: "Partial/GridView.html",
            controller: 'GridViewController'
        })
        .when('/BodyShop', {
            templateUrl: "Partial/BodyShopPreview.html",
            controller: 'BodyShopPreviewController'
              
        })
        .when('/Brand', {
            templateUrl: "Partial/BrandMaster.html",
            controller: 'BrandMasterController'
        })
        .when('/Status', {
            templateUrl: "Partial/StatusMaster.html",
            controller: 'StatusMasterController'
        })
        .when('/Report', {
            templateUrl: "Partial/Report.html",
            controller: 'ReportController'
        })
        .when('/Ready', {
            templateUrl: "Partial/ReadyJobCards.html",
            controller: 'ReadyJobCardsController'
        })
        .when('/StatusWise', {
            templateUrl: "Partial/StatusWise.html",
            controller: 'StatusWiseController'
        })
        .when('/Group', {
            templateUrl: "Partial/ContractorMaster.html",
            controller: 'ContractorMasterController'
        })
        .when('/Cwr', {
            templateUrl: "Partial/ContractorWiseReport.html",
            controller: 'ContractorReportController'
        })
        .when('/Em', {
            templateUrl: "Partial/EmailMaster.html",
            controller: 'EmailController'
        })
        .when('/Icm', {
            templateUrl: "Partial/InsuranceCmpMaster.html",
            controller: 'InsCmpMasterController'
        })
        .when('/Mwr', {
            templateUrl: "Partial/MonthWiseReport.html",
            controller: 'MonthWiseController'
        })
        .when('/Jcd', {
            templateUrl: "Partial/DetailsForJobcard.html",
            controller: 'DetailForJobcardController'
        })
        .when('/Mfs', {
            templateUrl: "Partial/ModelAndFuelTypeStatus.html",
            controller: 'MonthAndFuelTypeController'
        })
        .when('/TestXls', {
            templateUrl: "Partial/TestXls.html",
            controller: 'TESTController'
        })
         .otherwise({ redirectTo: '/' });

});

app.service('HomeService', function ($http, $location) {
    this.Url = '/api/';
    console.log($location.absUrl());
    if ($location.absUrl().indexOf('BS') != -1) {
        this.Url = '/BS/api/';
    }
    else
    {
        this.Url = '/api/';
    }


    //this.getCountries = function () {
    //    return $http.get('/api/master/GetAllCountries');
    //};
    // app.service('HomeService', function ($http) {
    //this.GetUserDetails = function (id) {
    //    return $http.get('/api/Login/GetUserDetails/' + id);
    //};
    //});
    //this.getMenuData = function (data) {
    //    console.log(data);
    //    return $http.post('/api/IOS_WRM/getMenuData', data, {});
    //};
    //this.GetData = function (DealerID) {
        
    //    return $http.get('/api/DashBoard/GetData/?DealerID=',DealerID);
    //};

});



app.controller('HomeController', function ($scope, HomeService, myService, $http, $routeParams, $rootScope, $q) {

    $rootScope.session = angular.fromJson(sessionStorage.getItem("app"));
    console.log($rootScope.session);
    //$rootScope.uid = "";
    $scope.LoggedInUser = '';
    $scope.RoleName = '';
    $scope.AdminSetup = false;
    $scope.SHSetup = false;
    $scope.DHSetup = false;
    $scope.FinanceSetup = false;
    $scope.Report = false;
    $scope.ApprovalScreen = false;
    $scope.ValidUser = false;
    $scope.GlobalMessage = '';
    $rootScope.divGlobalMessage = false;
   
    $rootScope.uid = "";

    $rootScope.PrintLog = true;

    console.log("homecontroller ");


    //-----------------------------------------------
    $rootScope.Maininit = function () {

        
        //HomeService.GetData($rootScope.session.DealerID).then(function (success) {
        //    console.log("SUCCESS DATA", success.data);
        //    swal("Done!", "Process Done successfully!", "success");

        //},
        //    function (error) {
        //        console.log(error.data);
        //        swal("error", error.data, "error");
        //        //swal(error.data);
        //    });


        //var deferred = $q.defer();
        //var array = location.search.substring(1).split('=');
        //var KEy = '';
        //var Value = '';
        //if (array.length > 0) {
        //    KEy = array[0];
        //    Value = array[1];
        //}
        //if (KEy == 'udi') {
        //    $rootScope.uid = Value;

        //}
        //else {
        //    if ($location.search().hasOwnProperty('udi')) {
        //        uid = $location.search()['udi'];
        //        //$rootScope.uid = uid;
        //        window.location.assign('./index.html?udi=' + uid);
        //    }
        //}


        //-------------------------------------

        HomeService.GetUserDetails($rootScope.uid).success(function (retdata) {
            console.log(retdata);
            console.log($rootScope.session.isApprover);
            if (angular.isObject(retdata)) {
                //console.log(retdata);
                sessionStorage.setItem("app", angular.toJson(retdata));
                var session = angular.fromJson(sessionStorage.getItem("app")) || {};
                //HomeService.openPopupModal();
                //$location.search('udi', null);
                //$location.url($location.path());
                // window.location.assign('./guidelines.html');
                $rootScope.session = angular.fromJson(sessionStorage.getItem("app")) || {};
                console.log($rootScope.session);
                if (angular.isObject($rootScope.session)) {
                    $scope.LoggedInUser = $rootScope.session.Emp_First_name;
                    $scope.RoleName = $rootScope.session.RoleName;
                    $scope.Employee_ID = $rootScope.session.EMP_CODE;
                    console.log($scope.LoggedInUser);
                    $scope.ValidUser = true;
                }
                
                if ($scope.RoleName=='Admin') {
                    $scope.AdminAccess = true;               
                    $scope.ValidUser = true;
                 
                }
               
                deferred.resolve($rootScope.session = session);
            }
        }).error(function (retdata) {

            $scope.ValidUser = false;
            deferred.reject($rootScope.session = {});
        });

        return deferred.promise;
    }
    //----------------------------------------



    $scope.signout = function () {
        var data = {};
        sessionStorage.setItem("app", angular.toJson(data));
        $rootScope.session = {};
        window.location.assign('./login.html');
        con.log($rootScope.session);
    };


   // $rootScope.Maininit();
});





