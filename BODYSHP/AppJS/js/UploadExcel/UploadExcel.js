var app = angular.module('UploadExcelModule', []);

app.service('UploadExcelService', function ($http, HomeService) {
    //getCOmplaintTYpe
    this.UploadExcel = function (fd) {

        return $http.post(HomeService.Url+"ExcelUpload/PostData", fd,
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
app.controller('UploadExcelController', function ($scope, $http, $location, $rootScope, UploadExcelService,  $q) {

  
    $scope.file1 = null;

   
    $scope.init = function () {
   

        console.log("insideinit");   


    }
    
    $scope.UploadExcel = function () {
        
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
            function (isConfirm)
            {
                if (isConfirm) {

                    UploadExcelService.UploadExcel(fd).then(function (success) {
                        console.log("SUCCESS DATA", success.data);
                        if (success.data==2)
                        {
                            swal("error!", "Excel sheet not format !", "error");
                        }
                        else if (success.data == 3)
                        {
                            swal("error!", "Column names are different !", "error");
                        }
                        else if (success.data == 0) {
                            swal("Done!", "Process Done successfully!", "success");
                           
                        }
                        else
                        {
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
    
    $scope.init();
});








