var app = angular.module('ContractorReportModule', []);

app.service('ContractorReportService', function ($http, HomeService) {
    //getCOmplaintTYpe
    this.GetContractorWiseReport = function (obj) {

        return $http.post(HomeService.Url+'StatusWise/GetContractorWiseReport/', obj);
    };
    this.GetStatus = function (SessionObj) {
        return $http.post(HomeService.Url+'StatusMaster/GetData/', SessionObj);
    };
    this.SendEmail = function (Obj) {
        return $http.post(HomeService.Url+'StatusWise/SendEmail/', Obj);
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

app.controller('ContractorReportController', function ($scope, $http, $location, $rootScope, myService, $uibModal, ContractorReportService, $q, $filter) {


    console.log($rootScope.session.UserId);
    $scope.PreviewData = [];
    $scope.ngshow1 = false;
    $scope.ngshow2 = false;
    $scope.CurrentDate = $filter('date')(new Date(), "MM-dd-yyyy");

    console.log($scope.CurrentDate);

    $scope.init = function () {

        var obj = {
            Contractor_Id: '',
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
        var SessionObj =
            {
                UserId: $rootScope.session.UserId,
                DealerID: $rootScope.session.DealerID,
                AccountId: $rootScope.session.AccountId,
                EmployeeId: $rootScope.session.EmployeeId,
                DesignationID: $rootScope.session.DesignationID,
                UserName: $rootScope.session.UserName
            };
        


        ContractorReportService.GetStatus(SessionObj).then(function (success) {
            console.log("PreviewData");
            $scope.StatusList = success.data;
            
            console.log("ContractorReport", $scope.StatusList);
            ContractorReportService.GetContractorWiseReport(obj).then(function (success) {
                console.log("PreviewData");
                $scope.PreviewData = success.data;
                $scope.ngshow1 = true;
                console.log("ContractorReport", $scope.PreviewData);
            },
                function (error) {
                    console.log(error);
                });
        },
            function (error) {
                console.log(error);
            });
    }


    $scope.GetContractorWiseReport = function () {
        var obj = {
            Contractor_Id: '',
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
        ContractorReportService.GetContractorWiseReport(obj).then(function (success) {
            console.log("PreviewData");
            $scope.PreviewData = success.data;
            $scope.ngshow1 = true;
            console.log($scope.PreviewData);
        },
            function (error) {
                console.log(error);
            });

    }
   
    $scope.exportExcel = function () {

                var uri = 'data:application/vnd.ms-excel;base64,'
                    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

                var table = document.getElementById("searchResult");
                var filters = $('.ng-table-filters').remove();

        //pass html table id (e.g.-'Sp_ContractorReport' in this)

                var ctx = { worksheet: name || 'Group Details', table: Sp_ContractorReport.innerHTML };
                $('.ng-table-sort-header').after(filters);
                var url = uri + base64(format(template, ctx));
                var a = document.createElement('a');
                a.href = url;

        //Name the Excel like 'GroupWiseReport.xls' in this

                a.download = 'GroupWiseReport.xls';
                a.click();
        
    }



    $scope.SendEmail = function ()
    {
        var SessionObj =
            {
                UserId: $rootScope.session.UserId,
                DealerID: $rootScope.session.DealerID,
                AccountId: $rootScope.session.AccountId,
                EmployeeId: $rootScope.session.EmployeeId,
                DesignationID: $rootScope.session.DesignationID,
                UserName: $rootScope.session.UserName
            };
        var ReportInput =
            {
                ContractorWiseReport: $scope.PreviewData,
                SessionData: SessionObj
            };

        console.log(ReportInput);
        ContractorReportService.SendEmail(ReportInput).then(function (success) {
            
            swal(success.data);
        },
            function (error) {
                console.log(error);
            });
    };

    $scope.init();
});

