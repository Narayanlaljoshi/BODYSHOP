var app = angular.module('StatusMasterModule', []);

app.service('StatusMasterService', function ($http, HomeService) {
    //getCOmplaintTYpe

    //Get Preview DAta
    this.GetData = function (obj) {
        return $http.post(HomeService.Url+'StatusMaster/GetData/', obj);
    };

    this.AddStatus = function (StatusObj) {

        return $http.post(HomeService.Url+'StatusMaster/Post/',StatusObj);
    };

    this.Edit = function (obj) {

        return $http.post(HomeService.Url+'StatusMaster/Update/', obj);
    };
    this.Delete = function (StatusID) {

        return $http.post(HomeService.Url+'StatusMaster/Delete/?StatusID=' + StatusID);
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

app.controller('StatusMasterController', function ($scope, $http, $location, $rootScope, myService, StatusMasterService,  $q) {

    $scope.PreviewData = [];
    $scope.ngshow1 = true;
    $scope.ngshow2 = false;
    $scope.EditShow = false;
    $scope.init = function () {
        var obj =
            {
                UserId: $rootScope.session.UserId,
                DealerID: $rootScope.session.DealerID,
                AccountId: $rootScope.session.AccountId,
                EmployeeId: $rootScope.session.EmployeeId,
                DesignationID: $rootScope.session.DesignationID,
                UserName: $rootScope.session.UserName
            };
        //get dealer details
        StatusMasterService.GetData(obj).then(function (success) {
            console.log("StatusData");
            $scope.StatusData = success.data;
            console.log($scope.StatusData);
        },
            function (error) {
                console.log(error);
            });

    }

    $scope.AddRow = function (bool) {
        $scope.BrandName = "";
        console.log(bool);
        if (bool) {
            $scope.ngshow1 = false;
            $scope.ngshow2 = true;
            $scope.EditShow = false;
        }
        else {
            $scope.ngshow1 = true;
            $scope.ngshow2 = false;
            $scope.EditShow = false;
        }

    }

    $scope.Edit = function (pc) {
        $scope.EditShow = true;
        $scope.ngshow1 = false;
        $scope.ngshow2 = false;
        
        $scope.StatusID = pc.StatusID;
        $scope.StatusName = pc.StatusName;
        $scope.ShortName = pc.ShortName;
        $scope.StatusCode = pc.StatusCode;

    }

    $scope.Update = function (pc) {

        
        var obj = {
            DealerID: $rootScope.session.DealerID,
            AccountId: $rootScope.session.AccountId,
            StatusID: $scope.StatusID, 
            StatusName: $scope.StatusName,
            ShortName: $scope.ShortName,
            StatusCode: $scope.StatusCode,
            
        };
        StatusMasterService.Edit(obj).then(function (success) {
            swal("Updated Successful!");
            //window.location.reload();

            $scope.init();
        },
            function (error) {
                console.log(error);
                swal(error.data);
            });


    }

    $scope.Delete = function (StatusID) {

        console.log(StatusID);
        StatusMasterService.Delete(StatusID).then(function (success) {
            swal("Deleted Successfully!");
            window.location.reload();

            $scope.init();
        },
            function (error) {
                console.log(error);
                swal(error.data);
            });


    }


    $scope.AddStatus = function () {
        var StatusObj =
            {
                UserId: $rootScope.session.UserId,
                DealerID: $rootScope.session.DealerID,
                AccountId: $rootScope.session.AccountId,
                StatusID:'',
                StatusName: $scope.StatusName,
                ShortName: $scope.ShortName,
                StatusCode: $scope.StatusCode,
                UserName: $rootScope.session.UserName
            };
        
        //get dealer details
        //console.log(StatusName);
        StatusMasterService.AddStatus(StatusObj).then(function (success) {
            swal("Added Successfully!");
            $scope.StatusName = "";
            $scope.ShortName = "";
            $scope.StatusCode = "";
            //window.location.reload();

            $scope.init();
        },
            function (error) {
                console.log(error);
                swal(error.data);
            });

    }

    $scope.init();
});
