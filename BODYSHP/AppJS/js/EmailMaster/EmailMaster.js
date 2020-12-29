var app = angular.module('EmailModule', []);

app.service('EmailService', function ($http, HomeService) {
    //getCOmplaintTYpe

    //Get Preview DAta
    this.GetData = function (Obj) {
        return $http.post(HomeService.Url+'EmailMaster/GetData/', Obj);
    };
    
    this.Update = function (Obj) {
        return $http.post(HomeService.Url+'EmailMaster/UpDate/', Obj);
    };
    this.AddEmail = function (Obj) {
        return $http.post(HomeService.Url+'EmailMaster/AddEmail/', Obj);
    }; 
});



app.controller('EmailController', function ($scope, $http, $location, $rootScope, myService, EmailService, $q) {

    $scope.PreviewData = [];
    $scope.ngshow1 = true;
    $scope.ngshow2 = false;

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
        EmailService.GetData(obj).then(function (success) {
            console.log("PreviewData");
            $scope.PreviewData = success.data;
            console.log($scope.PreviewData);
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
        
        console.log(pc);
        $scope.ID = pc.Id;
        $scope.PersonName = pc.PersonName;
        $scope.Email_ID = pc.Email_ID;

    }

    $scope.Update = function (StatusID, StatusName, StatusCode) {

        console.log(StatusID);
        console.log(StatusName);
        console.log(StatusCode);
        var obj = {
            Id: $scope.ID,
            PersonName: $scope.PersonName,
            Email_ID: $scope.Email_ID ,
            ModifiedBy: $rootScope.session.UserId,
            IsDeleted: false,
        };
        EmailService.Update(obj).then(function (success) {
            swal("success", success.data);
            //window.location.reload();

            $scope.init();
        },
            function (error) {
                console.log(error);
                swal("error",error.data);
            });


    }

    $scope.Delete = function (pc) {
        var obj = {
            Id: pc.Id,
            PersonName: pc.PersonName,
            Email_ID: pc.Email_ID,
            ModifiedBy: $rootScope.session.UserId,
            IsDeleted: true,
        };
        //console.log(StatusID);
        EmailService.Update(obj).then(function (success) {
            swal("Success", success.data);
            window.location.reload();

            $scope.init();
        },
            function (error) {
                console.log(error);
                swal(error.data);
            });


    }


    $scope.AddEmail = function () {
        var obj = {
            AccountId: $rootScope.session.AccountId,
            DealerId: $rootScope.session.DealerID,
            Id: '',
            PersonName: $scope.PersonName,
            Email_ID: $scope.Email_ID,
            CreatedBy: $rootScope.session.UserId,
            IsDeleted: false,
        };

        //get dealer details
        //console.log(StatusName);
        EmailService.AddEmail(obj).then(function (success) {
            console.log(success.data);
            swal(success.data);
            $scope.StatusName = "";
            $scope.StatusCode = "";
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


