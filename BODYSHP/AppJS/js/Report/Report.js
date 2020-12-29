var app = angular.module('ReportModule', []);

app.service('ReportService', function ($http, HomeService) {
    //getCOmplaintTYpe

    //Get Preview DAta
    this.GetData = function () {
        return $http.get(HomeService.Url+'Report/GetData/', {});
    };
    
});



app.controller('ReportController', function ($scope, $http, $location, $rootScope, myService, ReportService, $q) {

    $scope.PreviewData = [];
    $scope.ngshow1 = true;
    $scope.ngshow2 = false;

    $scope.init = function () {

        //get dealer details
        ReportService.GetData().then(function (success) {
            console.log("PreviewData");
            $scope.PreviewData = success.data;
            console.log($scope.PreviewData);
        },
            function (error) {
                console.log(error);
            });

    }


    $scope.GetData = function () {

      

        ReportService.GetData.then(function (success) {
            console.log("PopupData");
            $rootScope.ReportData = success.data;
            console.log($rootScope.ReportData);
            
        },
            function (error) {
                console.log(error);
            });


    }





    $scope.init();
});


