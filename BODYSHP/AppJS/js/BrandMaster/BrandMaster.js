var app = angular.module('BrandMasterModule', []);

app.service('BrandMasterService', function ($http, HomeService) {
    //getCOmplaintTYpe

    //Get Preview DAta
    this.GetData = function () {
        return $http.post(HomeService.Url+'BrandMaster/GetData/', {});
    };

    this.AddBrand = function (BrandName, UserId) {

        return $http.post(HomeService.Url+'BrandMaster/Post/?BrandName=' + BrandName + '&UserId=' + UserId);
    };

    this.Edit = function (BrandId,BrandName) {

        return $http.post(HomeService.Url+'BrandMaster/Update/?BrandName=' + BrandName + '&BrandId=' + BrandId);
    };
    this.Delete = function (BrandId) {

        return $http.post(HomeService.Url+'BrandMaster/Delete/?BrandId=' + BrandId);
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

app.controller('BrandMasterController', function ($scope, $http, $location, $rootScope, myService, BrandMasterService,  $q) {

    $scope.PreviewData = [];
    $scope.ngshow1 = true;
    $scope.ngshow2 = false;
    $scope.EditShow = false;
    $scope.init = function () {

        //get dealer details
        BrandMasterService.GetData().then(function (success) {
            console.log("BrandData");
            $scope.BrandData = success.data;
            console.log($scope.BrandData);
        },
            function (error) {
                console.log(error);
            });

    }

    $scope.AddRow = function (bool) {
        $scope.BrandName="";
        console.log(bool);
        if (bool) {
            $scope.ngshow1 = false ;
            $scope.ngshow2 = true;
            $scope.EditShow = false;
        }
        else
        {
            $scope.ngshow1 = true;
            $scope.ngshow2 = false;
            $scope.EditShow = false;
        }
        
    } 

    $scope.Edit = function (BrandId, BrandName) {
        $scope.EditShow = true;
        $scope.ngshow1 = false;
        $scope.ngshow2 = false;
        console.log(BrandId);
        console.log(BrandName);
        $scope.BrandName = BrandName;
        $scope.BrandId = BrandId;
       
    }

    $scope.Update = function (BrandId, BrandName) {
       
        console.log(BrandId);
        console.log(BrandName);
       
        BrandMasterService.Edit(BrandId, $scope.BrandName).then(function (success) {
           
            window.location.reload();

            $scope.init();
        },
            function (error) {
                console.log(error);
            });


    }

    $scope.Delete = function (BrandId) {
       
        console.log(BrandId);
        BrandMasterService.Delete(BrandId).then(function (success) {
            swal("Deleted !");
            window.location.reload();

            $scope.init();
        },
            function (error) {
                swal(error.data);
                console.log(error);
            });


    }


    $scope.AddBrand = function () {
        
        //get dealer details
        console.log( $rootScope.session.UserId);
        BrandMasterService.AddBrand($scope.BrandName, $rootScope.session.UserId).then(function (success)
        {
            swal("Brand Added!");
            $scope.BrandName = "";
            //window.location.reload();

            $scope.init();
        },
            function (error) {
                swal(error.data);
                console.log(error);
            });

    }

    $scope.init();
});
