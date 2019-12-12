(function () {
    appModule.controller('common.views.users.showUserInfo', [
        '$scope', '$uibModal', '$stateParams','abp.services.app.commonLookup',
        function ($scope, $uibModal, $stateParams, commonService) {
            var vm = this;
            vm.user = null;
            function init() {
                commonService.getCurrentUserInfo().then(function (result) {
                    vm.user = result.data;
                });
            }
            init();
        }
    ]);
})();