var app = angular.module('DetailForJobcardModule', []);

app.service('DetailForJobcardService', function ($http, HomeService) {
    //getCOmplaintTYpe

    //Get Preview DAta
    this.GetData = function (JobcardNo) {
        return $http.get(HomeService.Url + 'GridView/GetDetailForJobcard?JobcardNo=' + JobcardNo);
    };
    
});



app.controller('DetailForJobcardController', function ($scope, $http, $location, $rootScope, myService, $uibModal, DetailForJobcardService, $q, $filter) {
    $scope.ShowDetails = false;

    console.log($rootScope.session.UserId);
    
    $scope.init = function () {
        console.log($scope.JobCardNumber);
        DetailForJobcardService.GetData($scope.JobCardNumber).then(function (success) {
            if (success.data != null)
            {
                $scope.ShowDetails = true;
                $scope.JobcardDetail = success.data;
            }
            else {
                swal("","No Record Found","error");
            }
            
            console.log($scope.JobcardDetail);
        },
            function (error) {
                console.log(error);
            });
    }


    $scope.StatusPopup = function (Sno) {

        console.log("Hello Click Working");
        console.log(Sno);

        DetailForJobcardService.StatusPopup(Sno).then(function (success) {
            console.log("PopupData");
            $rootScope.PopupData = success.data;
            console.log($rootScope.PopupData);

            DetailForJobcardService.Detail(Sno).then(function (success) {
                console.log("jobCardDetails");
                $rootScope.jobCardDetails = success.data;
                console.log($rootScope.jobCardDetails);
            },
                function (error) {
                    console.log(error);
                });


            $uibModal.open({
                templateUrl: 'Status.html',
                controller: 'StatusPopupController',
                backdrop: 'static',
                windowClass: 'app-modal-window',
                size: 'width: 1308px'
            }).result.then(
                function () {

                },
                function () {

                });


        },
            function (error) {
                console.log(error);
            });


    }
    
    //$scope.init();
});


app.controller('StatusPopupController', function ($scope, $uibModalInstance, $http, $location, $rootScope, myService, DetailForJobcardService, $q) {





    $scope.confirm = function () {

        $uibModalInstance.dismiss('cancel')
    }
    //$uidModalInstance.dismiss('confirm');
});