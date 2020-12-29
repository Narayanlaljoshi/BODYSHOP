var app = angular.module('TESTModule', ['angular-js-xlsx']);

app.service('GridViewService', function ($http, HomeService) {
    //getCOmplaintTYpe

   
  
});



app.controller('TESTController', function ($scope, $http, $location, $rootScope, myService, $uibModal, GridViewService, $q, $filter) {

    $scope.ExcelObj = [];
    var Obj = {
        SNo: 'S.No',
        MSPIN: 'MSPIN',
        Name:'Name'
    };
    $scope.ExcelObj.push(Obj);
    console.log($scope.ExcelObj);
    $scope.read = function (workbook) {
        console.log(workbook);
        angular.forEach(workbook.Sheets.Sheet1, function (Value, key) {
            debugger;
            console.log(Value);
        });
      
    };

    $scope.error = function (e) {
        // DO SOMETHING WHEN ERROR IS THROWN /
        console.log(e);
    };
   
});