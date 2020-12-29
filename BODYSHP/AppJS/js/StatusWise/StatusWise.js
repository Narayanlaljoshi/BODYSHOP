var app = angular.module('StatusWiseModule', []);

app.service('StatusWiseService', function ($http, HomeService) {
    //getCOmplaintTYpe

    //Get Preview DAta
    this.GetData = function (obj) {
        return $http.post(HomeService.Url+'StatusWise/GetData/', obj);
    };
    this.GetStatusName = function (obj) {
        return $http.post(HomeService.Url+'StatusMaster/GetStatusName/', obj);
    };

    this.StatusPopup = function (Sno) {
        return $http.post(HomeService.Url+'StatusMaster/StatusPopup/?Sno=' + Sno);
    };
    this.Detail = function (Sno) {
        return $http.post(HomeService.Url+'StatusMaster/Detail/?Sno=' + Sno);
    };

    this.UpdateBodyShop = function (Upadae) {

        return $http.post(HomeService.Url+'BodyShopUpdate/UpdateBodyShop/', Upadae);
    };
    this.GetDateWiseDtl = function (obj) {

        return $http.post(HomeService.Url+'StatusWise/GetDateWiseDtl/', obj);
    };
    this.GetContractorList = function (obj) {

        return $http.post(HomeService.Url+'ContractorMaster/GetContractor/', obj);
    };
    this.GetContractorWiseDtl = function (obj) {

        return $http.post(HomeService.Url+'StatusWise/GetContractorWiseDtl/', obj);
    };
    this.GetContractorWiseReport = function (obj) {

        return $http.post(HomeService.Url+'StatusWise/GetContractorWiseReport/', obj);
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

app.controller('StatusWiseController', function ($scope, $http, $location, $rootScope, myService, $uibModal, StatusWiseService, $q, $filter) {


    console.log($rootScope.session.UserId);
    $scope.PreviewData = [];
    $scope.ngshow1 = false;
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
        
        StatusWiseService.GetStatusName(obj).then(function (success) {
            console.log("PreviewData");
            $scope.StatusNames = success.data;
            console.log($scope.StatusNames);
        },
            function (error) {
                console.log(error);
            });
        //contractor List
        StatusWiseService.GetContractorList(obj).then(function (success) {
            console.log("StatusData");
            $scope.ContractorData = success.data;
            console.log($scope.StatusData);
        },
            function (error) {
                console.log(error);
            });
    }

    $scope.GetContractorWiseDtl = function ()
    {
        var obj = {
            Contractor_Id: $scope.Contractor_Id,
            ContractorCode: $scope.ContractorCode,
            ContractorName: $scope.ContractorName,
            Address: $scope.Address,
            Phone: $scope.Phone,
            DealerID: $rootScope.session.DealerID,
            AccountID: $rootScope.session.AccountId,
            IsDeleted: 'false',
            CreatedBy: '',
            ModifiedBy: $rootScope.session.UserId,
        };
        StatusWiseService.GetContractorWiseDtl(obj).then(function (success) {
            console.log("PreviewData");
            $scope.PreviewData = success.data;
            $scope.ngshow1 = true;
            console.log($scope.PreviewData);
        },
            function (error) {
                console.log(error);
            });

    }

    $scope.GetContractorWiseReport = function () {
        var obj = {
            Contractor_Id:'',
            ContractorCode: '',
            ContractorName: '',
            Address: '',
            Phone: '',
            DealerID: $rootScope.session.DealerID,
            AccountID: $rootScope.session.AccountId,
            IsDeleted: '',
            CreatedBy: '',
            ModifiedBy: $rootScope.session.UserId,
        };
        StatusWiseService.GetContractorWiseReport(obj).then(function (success) {
            console.log("PreviewData");
            $scope.PreviewData = success.data;
            $scope.ngshow1 = true;
            console.log($scope.PreviewData);
        },
            function (error) {
                console.log(error);
            });

    }
    $scope.GetData = function ()
    {
        console.log("StatusID=", $scope.StatusID);
        var obj = {
            DealerID: $rootScope.session.DealerID,
            StatusID: $scope.StatusID,
            AccountId: $rootScope.session.AccountId
        };
        console.log(obj);

        StatusWiseService.GetData(obj).then(function (success) {
            console.log("PreviewData");
            $scope.PreviewData = success.data;
            $scope.ngshow1 = true;
            console.log($scope.PreviewData);
        },
            function (error) {
                console.log(error);
            });


    }





    $scope.StatusPopup = function (Sno) {

        console.log("Hello Click Working");
        console.log(Sno);

        StatusWiseService.StatusPopup(Sno).then(function (success) {
            console.log("PopupData");
            $rootScope.PopupData = success.data;
            console.log($rootScope.PopupData);

            StatusWiseService.Detail(Sno).then(function (success) {
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






    $scope.UpdateData = function (JobCardId, StatusID, NumberOfPanel, PanelReplaced) {




        console.log($scope.NumberOfPanel);
        console.log($scope.PanelReplaced);
        console.log($scope.UpdateStatus);
        var Upadae = {
            JobCardId: JobCardId,
            StatusID: StatusID,
            NumberOfPanel: NumberOfPanel,
            PanelReplaced: PanelReplaced,
            ModifiedBy: $rootScope.session.UserId

        };
        console.log(Upadae);
        StatusWiseService.UpdateBodyShop(Upadae).then(function (success) {
            console.log("inside GetRequestData");
            swal("Updated Successfully!");
            $scope.init();

        },
            function (error) {
                console.log(error);
                swal(error.data);
            });

    }


    $scope.GetDateWiseDtl = function () {

        if (!$scope.StartDate) {
            swal("Select Start date");
            return false;
        }
        else if (!$scope.EndDate) {
            swal("Select End date");
        }
        else if (new Date($scope.StartDate) > new Date($scope.EndDate)) 
        {
            
            swal("Date selection is invalid");
            return false;
        }

        var obj = {
            StartDate: $scope.StartDate,
            EndDate: $scope.EndDate,
            DealerID: $rootScope.session.DealerID,
            AccountId: $rootScope.session.AccountId
        };
        console.log(obj);
        StatusWiseService.GetDateWiseDtl(obj).then(function (success) {
            console.log("Date wise");
            console.log("PreviewData");
            $scope.PreviewData = success.data;
            $scope.ngshow1 = true;
            console.log($scope.PreviewData);
            //if ($scope.PreviewData = !null) {
            //    $scope.ngshow1 = true;
            //    console.log($scope.PreviewData);
            //    //$scope.init();
            //}
           // else { $scope.ngshow1 = false; }

        },
            function (error) {
                console.log(error);
                swal(error.data);
            });

    }
    

    $scope.exportExcel = function () {

        var mystyle = {
            headers: true,
            columns: [
                { columnid: 'JobCardNo', title: 'Job Card No' },
                { columnid: 'DateAndTime', title: 'Date' },
                { columnid: 'CustomerName', title: 'Customer Name' },
                { columnid: 'PromisedDate', title: 'PromisedDate' },
                { columnid: 'RegistrationNo', title: 'RegistrationNo' },
                { columnid: 'Model', title: 'Model' },
                { columnid: 'Status', title: 'Status' },
                { columnid: 'SA', title: 'SA' },
                { columnid: 'Technician', title: 'Technician' },
                { columnid: 'ContractorName', title: 'Group' },
            ],
        };

        alasql('SELECT * INTO XLS("Report.xls",?) FROM ?', [mystyle,$scope.PreviewData]);
        var res = alasql('SELECT *  FROM  ?', $scope.PreviewData);
        console.log(res);
    }

    
    $scope.init();
});


app.controller('StatusPopupController', function ($scope, $uibModalInstance, $http, $location, $rootScope, myService, GridViewService, $q) {





    $scope.confirm = function () {

        $uibModalInstance.dismiss('cancel')
    }
    //$uidModalInstance.dismiss('confirm');
});