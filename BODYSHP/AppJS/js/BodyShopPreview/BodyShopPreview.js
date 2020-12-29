var app = angular.module('BodyShopPreviewModule', []);

app.service('BodyShopPreviewService', function ($http, HomeService) {



    //GetCompleteRequest
    this.GetData = function (JobCardId) {
        console.log(JobCardId);
        return $http.post(HomeService.Url+'BodyShopPreview/GetData/?Sno=' + JobCardId);
    };

    this.UpdateBodyShop = function (Upadae) {

        return $http.post(HomeService.Url+'BodyShopUpdate/UpdateBodyShop/', Upadae);
    };

    this.GetStatusData = function () {

        return $http.post(HomeService.Url+'StatusMaster/GetData/', {});
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


app.factory('crypt', function () {
    return {
        hash: function (value) {
            //var str = JSON.stringify(value);
            return CryptoJS.SHA1(value).toString();
        }
    };
});

app.controller('BodyShopPreviewController', function ($scope, $http, $location, $rootScope, myService, BodyShopPreviewService,  $q) {

    $scope.RequestData = [];
    //$scope.UpdatedData = [

    //    NumberOfPanel = '',
    //    PanelReplaced = '',
    //    UpdatedStatus = '',
       
    //];

    $scope.init = function () {
        $scope.JobCardId = myService.get();
        console.log("JobCardId", $scope.JobCardId);
        
       
        $scope.GetData($scope.JobCardId);

    }


    $scope.Back = function () {
        window.location.assign('./index.html#/Grid');
    }


    $scope.GetData = function (JobCardId) {
        
        BodyShopPreviewService.GetData(JobCardId).then(function (success) {
            console.log("inside GetRequestData");
            $scope.RequestData = success.data;
            console.log($scope.RequestData);




            BodyShopPreviewService.GetStatusData().then(function (success) {
                console.log("StatusData");
                $scope.StatusData = success.data;
                console.log($scope.StatusData);
            },
                function (error) {
                    console.log(error);
                });
        },
            function (error) {
                console.log(error);
            });




    }
    $scope.Hello = function (StatusID) {

        console.log("Hello Click Working");
        console.log(StatusID);
    }


    $scope.UpdateData = function () {
        console.log($scope.NumberOfPanel);
        console.log($scope.PanelReplaced);
        console.log($scope.UpdateStatus);
        var Upadae = {
            JobCardId: $scope.JobCardId,
            StatusID: $scope.UpdateStatus,
            NumberOfPanel:$scope.NumberOfPanel ,
            PanelReplaced:$scope.PanelReplaced ,
            UpdatedStatuss: $scope.UpdateStatus,
            Remarks: $scope.Remarks,
            ModifiedBy: $rootScope.session.UserId
            
        };
        console.log(Upadae);
        BodyShopPreviewService.UpdateBodyShop(Upadae).then(function (success) {
            console.log("inside GetRequestData");
            swal("Updated Successfully!");
            $scope.GetData($scope.JobCardId);

        },
            function (error) {
                console.log(error);
                swal(error.data);
            });


    }

    $scope.init();
});








