var app = angular.module('loginmodule', ['ngLoadingSpinner'])
app.service('Loginservice', function ($http, $location) {
    var Url;

    if ($location.absUrl().indexOf('BS') != -1) {
        Url = '/BS/api/';
    }
    else {
        Url = '/api/';
    }

    this.Login = function (obj) {
        
        return $http.post(Url+'Login/LoginCheck', obj);
    };


});

app.controller('logincontroller', function (Loginservice, $location,  $rootScope, $http, $scope) {

    console.log("loginpage");
    

    $scope.loginpage = true;
    $scope.UserName = "";
    $scope.Password = "";
    $scope.Validation = "";
    $scope.login = function () {
        if (!$scope.UserName) {

            $scope.Validation = "Type User name !";
            return false;
        }
        else if (!$scope.Password) {
            $scope.Validation = "Type Password !";
            return false;

        }
        else { $scope.Validation = "";}

        var obj = {
            UserName: $scope.UserName,
            Password: $scope.Password
        };

        
        Loginservice.Login(obj)
            .then(function (success) {
                console.log(success);
                if (success.data!=null) {
                    sessionStorage.setItem("app", angular.toJson(success.data));
                 //   $rootScope.session = angular.fromJson(sessionStorage.getItem("app")) || {};
                    window.location.assign('./index.html#/');
                    window.location.href = 'index.html'
                }
                else
                    $scope.Validation = "! Check User name Password";
                    //swal("Check UserName or Password");
                
            },
            function (error) {
                console.log(error);
                swal(error.data);
            });
    };


    
    //-------------------------------------------------------------------///
    //$rootScope.$on('$locationChangeSuccess', function () {
    //    $rootScope.actualLocation = $location.path();
    //});


    //$rootScope.$watch(function () { return $location.path() }, function (newLocation, oldLocation) {

    //    //true only for onPopState
    //    if ($rootScope.actualLocation === newLocation) {

    //        var back,
    //            historyState = $window.history.state;

    //        back = !!(historyState && historyState.position <= $rootScope.stackPosition);

    //        if (back) {
    //            //back button
    //            $rootScope.stackPosition--;
    //        } else {
    //            //forward button
    //            $rootScope.stackPosition++;
    //        }

    //    } else {
    //        //normal-way change of page (via link click)

    //        if ($route.current) {

    //            $window.history.replaceState({
    //                position: $rootScope.stackPosition
    //            });

    //            $rootScope.stackPosition++;

    //        }

    //    }

    //});

});