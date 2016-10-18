'use strict';

/**
 * @ngdoc function
 * @name frontEndApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the frontEndApp
 */
angular.module('frontEndApp')
  .controller('MainCtrl', function ($scope,$http) {
    this.awesomeThings = [
      'HTML5 Boilerplate',
      'AngularJS',
      'Karma'
    ];

    $scope.request = '';
    $scope.recordID;
    $scope.model = '';

    $scope.requestOptions = ['GET','POST','PUT','DELETE'];
    $scope.modelOptions = ['customers','orders','products','lineitems'];

    $scope.sendRequest = () => {
      let urlPath = `/${$scope.model}`;
      if ($scope.recordID) {
        urlPath = urlPath.concat(`/${$scope.recordID}`)
      }
      let requestBody = ``;
      if ($scope.model == "customers") {
        console.log("customer object");
        requestBody = JSON.stringify($scope.Customer);
      }
      else if ($scope.model == "orders") {
        console.log("orders object");
        requestBody = JSON.stringify($scope.Orders);
      }
      else if ($scope.model == "products") {
        console.log("product object");
        requestBody = JSON.stringify($scope.Products);
      }
      console.log($scope.request,$scope.model)
      $http({
        method: $scope.request,
        url: urlPath,
        data: requestBody
      }).then(function successCallback(response) {
          console.log(response);
          if (response.data[0]) {
            $scope.results = {rows: response.data, cols: Object.keys(response.data[0])};
          } else {
            $scope.results = {rows: [response.data], cols: Object.keys(response.data)};
          }
        }, function errorCallback(response) {
          console.log(response);
        });
    };


  });
