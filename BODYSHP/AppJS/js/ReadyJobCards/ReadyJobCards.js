var app = angular.module('ReadyJobCardsModule', []);

app.service('ReadyJobCardsService', function ($http, HomeService) {
    //getCOmplaintTYpe

    //Get Preview DAta
    this.GetData = function (obj) {
        return $http.post(HomeService.Url+'ReadyJobCards/GetData/', obj);
    };
    this.GetStatusName = function (obj) {
        return $http.post(HomeService.Url+'ReadyJobCards/GetStatusName/', obj);
    };
  
    this.StatusPopup = function (Sno) {
        return $http.post(HomeService.Url+'StatusMaster/StatusPopup/?Sno='+Sno);
    };
    this.Detail = function (Sno) {
        return $http.post(HomeService.Url+'StatusMaster/Detail/?Sno=' + Sno);
    };
   
    this.UpdateBodyShop = function (Upadae) {

        return $http.post(HomeService.Url+'BodyShopUpdate/UpdateBodyShop/', Upadae);
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

app.controller('ReadyJobCardsController', function ($scope, $http, $location, $rootScope, myService, $uibModal, ReadyJobCardsService, $q , $filter) {

    
    console.log($rootScope.session.UserId);
    $scope.PreviewData = [];
    $scope.ngshow1 = true;
    $scope.ngshow2 = false;
    $scope.CurrentDate = $filter('date')(new Date(), "MM-dd-yyyy");

    console.log($scope.CurrentDate);

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
        ReadyJobCardsService.GetData(obj).then(function (success) {
            console.log("PreviewData");
            $scope.PreviewData = success.data;
            console.log($scope.PreviewData);
        },
            function (error) {
                console.log(error);
            });

        ReadyJobCardsService.GetStatusName(obj).then(function (success) {
            console.log("PreviewData");
            $scope.StatusNames = success.data;
            console.log($scope.StatusNames);
        },
            function (error) {
                console.log(error);
            });

    }


    $scope.StatusPopup = function (Sno) {

        console.log("Hello Click Working");
        console.log(Sno);

        ReadyJobCardsService.StatusPopup(Sno).then(function (success) {
            console.log("PopupData");
            $rootScope.PopupData = success.data;
            console.log($rootScope.PopupData);

            ReadyJobCardsService.Detail(Sno).then(function (success) {
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




   

    $scope.UpdateData = function (pc) {

        


        console.log($scope.NumberOfPanel);
        console.log($scope.PanelReplaced);
        console.log($scope.UpdateStatus);
        var Upadae = {
            JobCardId: pc.JobCardId,
            StatusID: pc.StatusID,
            NumberOfPanel: pc.NumberOfPanel,
            PanelReplaced: pc.PanelReplaced,
            Contractor_Id: pc.Contractor_Id,
            ModifiedBy: $rootScope.session.UserId

        };
        console.log(Upadae);
        ReadyJobCardsService.UpdateBodyShop(Upadae).then(function (success) {
            console.log("inside GetRequestData");
            swal("Updated Successfully!");
            $scope.init();

        },
            function (error) {
                console.log(error);
                swal(error.data);
            });
        
    }

    $scope.init();
});


app.controller('StatusPopupController', function ($scope, $uibModalInstance, $http, $location, $rootScope, myService, GridViewService, $q) {





    $scope.confirm = function () {

        $uibModalInstance.dismiss('cancel')
    }
    //$uidModalInstance.dismiss('confirm');
});