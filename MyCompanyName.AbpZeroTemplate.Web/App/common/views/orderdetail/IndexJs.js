(function () {
    appModule.controller('common.views.orderDetails.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.orderDetail',
        function ($scope, $uibModal, $stateParams, uiGridConstants, orderDetailService) {
            //$stateParams 获取的是浏览器后面跟的参数
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                //这里应该是当页面加载完毕后，进行信息的初始化
                //实际会去调用 icheck、select2等js的初始化插件。来渲染页面
                App.initAjax();
            });
            //告知页面信息已经下载完毕
            vm.loading = false;
            //默认是关闭高级按钮。我们这里用不到。参考
            //  vm.advancedFiltersAreShown = false;
            //获取传递的参数，判断模糊查询字段，是否为空
            vm.filterText = $stateParams.filterText || '';
            //获取Session中的userId
            //   vm.currentUserId = abp.session.userId;
            //制作权限组，进行页面权限的判断 
            vm.permissions = {
                create: abp.auth.hasPermission("Pages.OrderDetail.CreateOrderDetail"),
                edit: abp.auth.hasPermission("Pages.OrderDetail.EditOrderDetail"),
                'delete': abp.auth.hasPermission("Pages.OrderDetail.DeleteOrderDetail")
            };
            //请求参数，默认用于分页
            vm.requestParams = {
                permission: '',
                role: '',
                skipCount: 0,
                //这里是个常量文件，如果你没有找到这个常量文件，那么就手动改成10吧
                maxResultCount: app.consts.grid.defaultPageSize,
                sorting: null,
                orderNum: '',
                idCard: '',
                realName: '',
                status: '',
                isApiData:''
            };

            //配置表格信息
            vm.orderDetailGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                //这里是个常量文件，如果你没有找到这个常量文件，那么就手动改成[10, 20, 50, 100]吧
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                useExternalSorting: true,
                appScopeProvider: vm,
                rowTemplate: '<div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'text-muted\': !row.entity.isActive }"  ui-grid-cell></div>',

                columnDefs: [
                    {
                        name: app.localize('OrderNum'),
                        field: 'orderNum',
                        //  enableSorting: false,
                        //cellFilter: 'momentFormat: \'L\'',
                        minWidth: 100
                    },
                    {
                        name: "身份证号",
                        field: 'idCard',
                        //  enableSorting: false,
                        //cellFilter: 'momentFormat: \'L\'',
                        minWidth: 100
                    },
                    {
                        name: "姓名",
                        field: 'realName',
                        //  enableSorting: false,
                        //cellFilter: 'momentFormat: \'L\'',
                        minWidth: 100
                    },

                    {
                        name: app.localize('CheckTime'),
                        field: 'checkTime',
                        //  enableSorting: false,
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm:ss\'',
                        minWidth: 100
                    },
                    {
                        name: app.localize('Money'),
                        field: 'money',
                        //  enableSorting: false,
                        cellFilter: 'moneyFormat',
                        minWidth: 100
                    },
                    {
                        name: app.localize('CurrMoney'),
                        field: 'currMoney',
                        //  enableSorting: false,
                        cellFilter: 'moneyFormat',
                        minWidth: 100
                    },
                    {
                        name: app.localize('Status'),
                        field: 'status',
                        //  enableSorting: false,
                        //cellFilter: 'momentFormat: \'L\'',
                        minWidth: 100
                    },
                    {
                        name: app.localize('StatusMsg'),
                        field: 'statusMsg',
                        //  enableSorting: false,
                        //cellFilter: 'momentFormat: \'L\'',
                        minWidth: 100
                    },
                    {
                        name: app.localize('RequestUrl'),
                        field: 'requestUrl',
                        //  enableSorting: false,
                        //cellFilter: 'momentFormat: \'L\'',
                        minWidth: 100
                    }],
                // ui-grid进行API注册渲染数据。
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (!sortColumns.length || !sortColumns[0].field) {
                            vm.requestParams.sorting = null;
                        } else {
                            vm.requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                        }
                        vm.getPagedorderDetails();
                    });
                    //配置UI-grid的 API参数
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                        vm.requestParams.maxResultCount = pageSize;
                        //执行查询语句
                        vm.getPagedorderDetails();
                    });
                },
                data: []
            };

            //声明查询方法
            vm.getPagedorderDetails = function () {
                vm.loading = true;
                orderDetailService.getPagedOrderDetailsAsync($.extend({ filter: vm.filterText }, vm.requestParams))
                    .then(function (result) {
                        vm.orderDetailGridOptions.totalItems = result.data.totalCount;
                        //       console.log(result.data.items);                        
                        vm.orderDetailGridOptions.data = result.data.items;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            //删除方法
            vm.deleteOrderDetail = function (orderDetail) {
                abp.message.confirm(
                    app.localize('OrderDetailDeleteWarningMessage', orderDetail.id),
                    function (isConfirmed) {
                        if (isConfirmed) {
                            orderDetailService.deleteOrderDetailAsync({
                                id: orderDetail.id
                            }).then(function () {
                                vm.getPagedorderDetails();
                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                            });
                        }
                    }
                );
            };

            //导出为excel文档
            vm.exportToExcel = function () {
                orderDetailService.getOrderDetailToExcel($.extend({ filter: vm.filterText }, vm.requestParams))
                    .then(function (result) {
                        app.downloadTempFile(result.data);
                    });
            };


            //编辑功能
            vm.editOrderDetail = function (orderDetail) {
                //     console.log(orderDetail);
                openCreateOrEditOrderDetailModal(orderDetail.id);
            };
            //创建功能
            vm.createOrderDetail = function () {
                openCreateOrEditOrderDetailModal(null);
            };

            //打开模态框，进行创建或者编辑的功能操作
            function openCreateOrEditOrderDetailModal(orderDetailId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/orderDetails/createOrEditModal.cshtml',
                    controller: 'common.views.orderDetails.createOrEditModal as vm',
                    backdrop: 'static',
                    resolve: {
                        orderDetailId: function () {
                            return orderDetailId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getPagedorderDetails();
                });
            }


            //执行查询方法
            vm.getPagedorderDetails();
        }
    ]);
})();











