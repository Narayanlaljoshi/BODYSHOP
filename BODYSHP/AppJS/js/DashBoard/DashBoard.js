var app = angular.module('DashBoardModule', []);
app.service('DashBoardService', function ($http, HomeService) {

    this.GetData = function (obj) {
        return $http.post(HomeService.Url+'DashBoard/GetData/', obj);
    };



});

app.controller('DashBoardController', function ($scope, $http, $location, $rootScope, myService, $uibModal, DashBoardService, $q, $filter) {

    $scope.init = function ()
    {
        var obj =
            {
                UserId: $rootScope.session.UserId,
                DealerID: $rootScope.session.DealerID,
                AccountId: $rootScope.session.AccountId,
                EmployeeId: $rootScope.session.EmployeeId,
                DesignationID: $rootScope.session.DesignationID,
                UserName: $rootScope.session.UserName
            };
        DashBoardService.GetData(obj).then(function (success) {
            console.log("DashBoard  DCealerID", $rootScope.session.DealerID);
            $scope.DashBoardData = success.data;
            console.log($scope.DashBoardData);
        },
            function (error) {
                console.log(error);
            });


    }
    $scope.init();
});