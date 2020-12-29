var app = angular.module('MonthAndFuelTypeModule', []);

app.service('MonthAndFuelTypeService', function ($http, HomeService) {
    //getCOmplaintTYpe

    //Get Preview DAta
    this.GetData = function (Obj) {
        return $http.post(HomeService.Url + 'Reports/MonthAndFuelTypeStatus/', Obj);
    };

});



app.controller('MonthAndFuelTypeController', function ($scope, $http, $location, $rootScope, myService, $uibModal, MonthAndFuelTypeService, $q, $filter) {
    $scope.ShowDetails = false;
    $scope.MonthList = [{ Month: 1, Name: 'Jan' }, { Month: 2, Name: 'Feb' }, { Month: 3, Name: 'Mar' }, { Month: 4, Name: 'Apr' }, { Month: 5, Name: 'May' }, { Month: 6, Name: 'Jun' }, { Month: 7, Name: 'Jul' }, { Month: 8, Name: 'Aug' }, { Month: 9, Name: 'Sep' }, { Month: 10, Name: 'Oct' }, { Month: 11, Name: 'Nov' }, { Month: 12, Name: 'Dec'}];
    $scope.YearList = [{ Year: 2018 }, { Year: 2019 }, { Year: 2020 }];
    $scope.FilterData = {};
    console.log($rootScope.session.UserId);

    $scope.init = function () {
        
        $scope.FilterData = {
            AccountId: $rootScope.session.AccountId,
            EmployeeId: $rootScope.session.EmployeeId,
            DesignationID: $rootScope.session.DesignationID,
            DealerID: $rootScope.session.DealerID,
            Month: null,
            Year: null
        }
        MonthAndFuelTypeService.GetData($scope.FilterData).then(function (success) {
            if (success.data != null) {
                $scope.ShowDetails = true;
                $scope.ReportData = success.data;
            }
            else {
                $scope.ShowDetails = false;
                swal("", "No Record Found", "error");
            }

            //console.log($scope.ReportData);
        },
            function (error) {
                console.log(error);
            });
    }   
    $scope.GetData = function () {
        $scope.FilterData = {
            AccountId: $rootScope.session.AccountId,
            EmployeeId: $rootScope.session.EmployeeId,
            DesignationID: $rootScope.session.DesignationID,
            DealerID: $rootScope.session.DealerID,
            Month: $scope.Month,
            Year: $scope.Year
        }
        console.log($scope.FilterData);
        MonthAndFuelTypeService.GetData($scope.FilterData).then(function (success) {
            if (success.data != null) {
                $scope.ShowDetails = true;
                $scope.ReportData = success.data;
            }
            else {
                $scope.ShowDetails = false;
                swal("", "No Record Found", "error");
            }

            console.log($scope.ReportData);
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

        var ctx = { worksheet: name || 'Colour Details', table: MonthAndFuelTypeTable.innerHTML };
        $('.ng-table-sort-header').after(filters);
        var url = uri + base64(format(template, ctx));
        var a = document.createElement('a');
        a.href = url;

        //Name the Excel like 'GroupWiseReport.xls' in this

        a.download = 'MonthAndFuelType.xls';
        a.click();

    }

    $scope.init();
});


