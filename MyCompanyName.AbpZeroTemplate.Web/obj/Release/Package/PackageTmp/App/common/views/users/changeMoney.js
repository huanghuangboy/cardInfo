(function () {
    appModule.controller('common.views.users.changeMoney', [
        '$scope', '$uibModalInstance', 'abp.services.app.user', 'userId','userName',
        function ($scope, $uibModalInstance, userService, userId, userName) {
            var vm = this;
            vm.saving = false;
            vm.money = '';
            vm.mark = '';
            vm.nowMoney = '';
            vm.name = userName;
            vm.save = function () {
                vm.money = Math.round(vm.money * 100);
                vm.saving = true;
                userService.updateUserMoney({
                    userId: userId,
                    money: vm.money,
                    mark: vm.mark
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            function init() {
                userService.getUserMoney({
                    id: userId
                }).then(function (result) {
                    vm.nowMoney = (result.data * 0.01).toFixed(2);
                });
            }

            init();
        }
    ]);
})();