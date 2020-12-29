var app = angular.module('ContractorMasterModule', []);

app.service('ContractorMasterService', function ($http, HomeService) {
    //getCOmplaintTYpe

    //Get Preview DAta
    this.GetData = function (obj) {
        return $http.post(HomeService.Url+'ContractorMaster/GetContractor/', obj);
    };

    this.AddContractor = function (Obj) {

        return $http.post(HomeService.Url+'ContractorMaster/AddContractor',Obj);
    };

    this.Update = function (obj) {

        return $http.post(HomeService.Url+'ContractorMaster/UpdateContractor/', obj);
    };
    this.Delete = function (obj) {

        return $http.post(HomeService.Url+'ContractorMaster/Delete/', obj);
    };
    this.UploadExcel = function (fd) {

        return $http.post(HomeService.Url+"ExcelUpload/ContractorData", fd,
            {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }

            });
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

app.controller('ContractorMasterController', function ($scope, $http, $location, $rootScope, $uibModal,myService, ContractorMasterService, $q) {

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
        ContractorMasterService.GetData(obj).then(function (success) {
            console.log("StatusData");
            $scope.ContractorData = success.data;
            console.log($scope.StatusData);
        },
            function (error) {
                console.log(error);
            });

    }

    $scope.AddRow = function (bool) {
        
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
        $scope.ContractorCode = '';
        $scope.ContractorName = '';
        $scope.Address = '';
        $scope.Phone = '';
    }

    $scope.Edit = function (pc) {
        $scope.EditShow = true;
        $scope.ngshow1 = false;
        $scope.ngshow2 = false;
        $scope.Contractor_Id = pc.Contractor_Id;
        $scope.ContractorCode = pc.ContractorCode;
        $scope.ContractorName = pc.ContractorName;
        $scope.Address = pc.Address;
        $scope.Phone = pc.Phone;

    }

    $scope.Update = function () {

        
       
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
        ContractorMasterService.Update(obj).then(function (success) {
            swal("Updated Successful!");
            //window.location.reload();

            $scope.init();
        },
            function (error) {
                console.log(error);
                swal(error.data);
            });


    }

    $scope.Delete = function (Contractor_Id) {

        console.log(Contractor_Id);
        var obj = {
            Contractor_Id: Contractor_Id,
            ContractorCode:'',
            ContractorName: '',
            Address: '',
            Phone: '',
            DealerID: $rootScope.session.DealerID,
            AccountID: $rootScope.session.AccountId,
            IsDeleted: 'false',
            CreatedBy: '',
            ModifiedBy: $rootScope.session.UserId,
        };
        ContractorMasterService.Delete(obj).then(function (success) {
            swal("Deleted Successfully!");
            

            $scope.init();
        },
            function (error) {
                console.log(error);
                swal(error.data);
            });


    }


    $scope.AddContractor= function () {

        //get dealer details
        //console.log(StatusName);
        var obj = {
            Contractor_Id:'',
            ContractorCode:$scope.ContractorCode,
            ContractorName: $scope.ContractorName ,
            Address: $scope.Address,
            Phone: $scope.Phone,
            DealerID: $rootScope.session.DealerID,
            AccountID: $rootScope.session.AccountId,
            IsDeleted :'false',
            CreatedBy :$rootScope.session.UserId,
            ModifiedBy:'',
        

        };
        ContractorMasterService.AddContractor(obj).then(function (success) {
            swal("Added Successfully!");
            $scope.ContractorCode = '';
            $scope.ContractorName = '';
            $scope.Address = '';
            $scope.Phone = '';
            //window.location.reload();

            $scope.init();
        },
            function (error) {
                console.log(error);
                swal(error.data);
            });

    }


    $scope.UploadPopup = function (Sno) {

        console.log("Hello Click Working");
        console.log(Sno);

        $uibModal.open({
            templateUrl: 'Contractor.html',
            controller: 'PopupController',
            backdrop: 'static',
            windowClass: 'app-modal-window',
            size: 'width: 1308px',
            scope: $scope
        }).result.then(
            function () {

            },
            function () {

            });
        
    }


    $scope.init();
});


app.controller('PopupController', function ($scope, $uibModalInstance, $http, $location, $rootScope, myService, ContractorMasterService, $q) {
    
    $scope.confirm = function () {

        if (!$scope.file1)
        { swal("please upload Excel"); return false; }

        var fd = new FormData();
        var obj =
            {
                UserId: $rootScope.session.UserId,
                DealerID: $rootScope.session.DealerID,
                AccountId: $rootScope.session.AccountId,
                EmployeeId: $rootScope.session.EmployeeId,
                DesignationID: $rootScope.session.DesignationID,
                UserName: $rootScope.session.UserName
            };

        console.log(obj);
        fd.append('file', $scope.file1);
        //fd.append('data', $rootScope.session.UserId);
        fd.append('data', angular.toJson(obj));


        swal({
            title: "Are you sure?",
            text: "you are going to create request",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Create",
            cancelButtonText: "No, cancel plx!",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {

                    ContractorMasterService.UploadExcel(fd).then(function (success) {
                        console.log("SUCCESS DATA", success.data);
                        if (success.data == 2) {
                            swal("error!", "Excel sheet not format !", "error");
                        }
                        else if (success.data == 3) {
                            swal("error!", "Column names are different !", "error");
                        }
                        else if (success.data == 0) {
                            swal("Done!", "Process Done successfully!", "success");
                            $uibModalInstance.dismiss('cancel');
                            $scope.init();

                        }
                        else {
                            swal("error!", success, "error");
                        }


                    },
                        function (error) {
                            console.log(error.data);
                            swal("error", error.data, "error");
                            //swal(error.data);
                        });
                }
                else {
                    swal("Cancelled", "", "error");
                    //swal("Cancelled", "Error :"+error.data, "error");
                }
            });





       
    }
    //$uidModalInstance.dismiss('confirm');

    $scope.Cancel = function () {

        $uibModalInstance.dismiss('cancel')
    }
});
