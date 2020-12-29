var app = angular.module('InsCmpMasterModule', []);

app.service('InsCmpMasterService', function ($http, HomeService) {
    //getCOmplaintTYpe

    //Get Preview DAta
    this.GetData = function (obj) {
        return $http.post(HomeService.Url+'InsuranceCmp/GetInsuranceCmpList/', obj);
    };

    this.AddStatus = function (Obj) {

        return $http.post(HomeService.Url+'InsuranceCmp/AddInsuranceCmp/', Obj);
    };

    this.Edit = function (obj) {

        return $http.post(HomeService.Url+'InsuranceCmp/UpdateInsuranceCmp/', obj);
    };
    //this.Delete = function (StatusID) {

    //    return $http.post(HomeService.Url+'InsuranceCmp/Delete/?StatusID=' + StatusID);
    //};

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

app.controller('InsCmpMasterController', function ($scope, $http, $location, $rootScope, myService, InsCmpMasterService, $q) {

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
        InsCmpMasterService.GetData(obj).then(function (success) {
            console.log("InsuranceCmpList");
            $scope.InsuranceCmpList = success.data;
            console.log($scope.InsuranceCmpList);
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

        $scope.InsuranceCompanyId = pc.InsuranceCompanyId;
        $scope.CompanyName = pc.CompanyName;
        $scope.CompanyCode = pc.CompanyCode;

    }

    $scope.Update = function (pc) {


        var Obj =
            {
                UserId: $rootScope.session.UserId,
                DealerID: $rootScope.session.DealerID,
                AccountId: $rootScope.session.AccountId,
                InsuranceCompanyId: $scope.InsuranceCompanyId ,
                IsDeleted: false,
                CompanyName: $scope.CompanyName,
                CompanyCode: $scope.CompanyCode,
                UserName: $rootScope.session.UserName
            };


        InsCmpMasterService.Edit(Obj).then(function (success) {
            swal(success.data);
            //window.location.reload();

            $scope.init();
        },
            function (error) {
                console.log(error);
                swal(error.data);
            });


    }

    $scope.Delete = function (pc) {

        var Obj =
            {
                UserId: $rootScope.session.UserId,
                DealerID: $rootScope.session.DealerID,
                AccountId: $rootScope.session.AccountId,
                InsuranceCompanyId: pc.InsuranceCompanyId,
                IsDeleted: true,
                CompanyName: $scope.CompanyName,
                CompanyCode: $scope.CompanyCode,
                UserName: $rootScope.session.UserName
            };

        console.log(Obj);
        InsCmpMasterService.Edit(Obj).then(function (success) {
            swal(success.data);
            //window.location.reload();

            $scope.init();
        },
            function (error) {
                console.log(error);
                swal(error.data);
            });



    }


    $scope.AddCompany = function () {
        var Obj =
            {
                UserId: $rootScope.session.UserId,
                DealerID: $rootScope.session.DealerID,
                AccountId: $rootScope.session.AccountId,
                InsuranceCompanyId: '',
                IsDeleted:'',
                CompanyName: $scope.CompanyName,
                CompanyCode: $scope.CompanyCode,
                UserName: $rootScope.session.UserName
            };

        //get dealer details
        //console.log(StatusName);
        InsCmpMasterService.AddStatus(Obj).then(function (success) {
            swal(success.data);
            $scope.CompanyName = "";
            $scope.CompanyCode = "";
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
