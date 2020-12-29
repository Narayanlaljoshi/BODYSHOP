var app = angular.module('GridViewModule', []);

app.service('GridViewService', function ($http, HomeService) {
    //getCOmplaintTYpe

    //Get Preview DAta
    this.GetData = function (obj) {
        return $http.post(HomeService.Url+'GridView/GetData/', obj);
    };
    this.UpdateDetails = function (fd) {
        return $http.post(HomeService.Url+"BodyShopUpdate/PostData", fd,
            {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
    };
    this.AddQuery = function (obj) {

        return $http.post(HomeService.Url+'AddQuery/Post/', obj);
    };
    this.GetStatusData = function () {
        return $http.post(HomeService.Url+'StatusMaster/GetData/', {});
    };
    this.StatusPopup = function (Sno) {
        return $http.post(HomeService.Url+'StatusMaster/StatusPopup/?Sno='+Sno);
    };
    this.Detail = function (Sno) {
        return $http.post(HomeService.Url+'StatusMaster/Detail/?Sno=' + Sno);
    };
    this.GetStatusName = function (obj) {
        return $http.post(HomeService.Url+'StatusMaster/GetStatusName/', obj);
    };
    this.UpdateBodyShop = function (Upadae) {

        return $http.post(HomeService.Url+'BodyShopUpdate/UpdateBodyShop/', Upadae);
    };
    this.GetContractor = function (obj) {
        return $http.post(HomeService.Url+'ContractorMaster/GetContractor/', obj);
    };
    this.GetInsuranceCmpList = function (obj) {
        return $http.post(HomeService.Url+'InsuranceCmp/GetInsuranceCmpList/', obj);
    };
});



app.controller('GridViewController', function ($scope, $http, $location, $rootScope, myService, $uibModal, GridViewService, $q , $filter) {

    //$scope.NumberOfPanel = null;
    //$scope.PanelReplaced = null;
    console.log($rootScope.session.UserId);
    $scope.PreviewData = [];
    $scope.ngshow1 = true;
    $scope.ngshow2 = false;//
    $scope.CurrentDate = $filter('date')(new Date(), "MM-dd-yyyy");
    $scope.Panels = [{ Id: 0, Name: '0' }, { Id: 1, Name: '1' }, { Id: 2, Name: '2' }, { Id: 3, Name: '3' }, { Id: 4, Name: '4' }, { Id: 5, Name: '5' }, { Id: 6, Name: '6' }, { Id: 7, Name: '7' }, { Id: 8, Name: '8' }, { Id: 9, Name: '9' }, { Id: 10, Name: '10'}];
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
        GridViewService.GetData(obj).then(function (success) {
            console.log("PreviewData");
            $scope.PreviewData = success.data;
            console.log($scope.PreviewData);
        },
            function (error) {
                console.log(error);
            });
        //status DropDown
        GridViewService.GetStatusName(obj).then(function (success) {
            console.log("PreviewData");
            $scope.StatusNames = success.data;
            console.log($scope.StatusNames);
        },
            function (error) {
                console.log(error);
            });

        

        //Get Contractor details
        GridViewService.GetContractor(obj).then(function (success) {
            console.log("StatusData");
            $scope.ContractorData = success.data;
            console.log($scope.StatusData);
        },
            function (error) {
                console.log(error);
            });

        //get insurance cmp list
        GridViewService.GetInsuranceCmpList(obj).then(function (success) {
            console.log("InsuranceCmpList");
            $scope.InsuranceCmpList = success.data;
            console.log($scope.InsuranceCmpList);
        },
            function (error) {
                console.log(error);
            });

    }


    $scope.StatusPopup = function (Sno) {

        console.log("Hello Click Working");
        console.log(Sno);

        GridViewService.StatusPopup(Sno).then(function (success) {
            console.log("PopupData");
            $rootScope.PopupData = success.data;
            console.log($rootScope.PopupData);

            GridViewService.Detail(Sno).then(function (success) {
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




    $scope.AddRow = function (bool) {
        console.log(bool);
        if (bool) {
            $scope.ngshow1 = false ;
            $scope.ngshow2 = true;
        }
        else
        {
            $scope.ngshow1 = true;
            $scope.ngshow2 = false ;
        }
        

    } 

    $scope.View = function (Sno) {

        myService.set(Sno);
        console.log(Sno);
        window.location.assign('./index.html#/BodyShop');
        //window.location.href ='Partial/BodyShopPreview.html'
        console.log("ComplaintNumber=");
        console.log(Sno);

     
    };

    $scope.AddQuery = function () {

        var obj =
            {
                RegistrationNo: $scope.RegistrationNo,
                JobCardNo: $scope.JobCardNo,
                CustomerName: $scope.CustomerName,
                PhoneNo: $scope.PhoneNo,
                CustomerCategory: $scope.CustomerCategory,
                PSFStatus: $scope.PSFStatus,
                Model: $scope.Model,
                SA: $scope.SA,
                Service: $scope.Service,
                PromisedDate: $scope.PromisedDate,
                Technician: $scope.Technician,
                NoOfPanel: $scope.NoOfPanel,
                PanelReplaced: $scope.PanelReplaced,
                Status: $scope.Status
            };

        //get dealer details
        GridViewService.AddQuery(obj).then(function (success) {
            $scope.init();
        },
            function (error) {
                console.log(error);
            });

    }
    //JobCardId,StatusID,NumberOfPanel, PanelReplaced
    $scope.UpdateData = function (pc) {
        console.log(pc.JobCardImage);
        //$scope.NumberOfPanel = NumberOfPanel;

        console.log("Panel",pc.No_Of_Panel);
        console.log("Panel replaced",pc.PanelReplaced);
        //console.log($scope.UpdateStatus);




        if (pc.No_Of_Panel != 0) { 
        if (!pc.No_Of_Panel) {
            swal("Enter no of panel");
            return false;
            }
        }
        else if (pc.No_Of_Panel != 0) { 
             if (!pc.PanelReplaced) {
                    swal("Enter no of panel Replaced");
                    return false;
             }
        }
        else if (!pc.Contractor_Id) {
            swal("Select Group");
            return false;
        }
        //if (!pc.PhotoUrl1)
        //{
        //    swal("Upload Photo");
        //    return false;
        //} 
        if (!pc.PaymentMode) {
            swal("select Payment Mode");
            return false;
        }
        //if (!pc.InsCompId) {
        //    swal("Select Insurance Company");
        //    return false;
        //}
       
        if (pc.Glass != 0) {
            if (!pc.Glass) {
                swal("Enter no of Glass Replaced");
                return false;
            }
        }

        var Upadae = {
            JobCardId: pc.JobCardId,
            StatusID: pc.StatusID,
            NumberOfPanel: pc.No_Of_Panel,
            PanelReplaced: pc.PanelReplaced,
            ModifiedBy: $rootScope.session.UserId,
            Contractor_Id: pc.Contractor_Id,
            PaymentMode: pc.PaymentMode,
            PhotoUrl: '',
            InsCompId: pc.InsCompId,
            Glass: pc.Glass
        };
        console.log(Upadae);
        var fd = new FormData();
        fd.append('file', pc.PhotoUrl1);
        //fd.append('data', $rootScope.session.UserId);
        fd.append('data', angular.toJson(Upadae));


        GridViewService.UpdateDetails(fd).then(function (success) {
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