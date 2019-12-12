
                            
 
  


//你好，我是ABP代码生成器的作者,欢迎您使用该工具，目前接受付费定制该工具，有需要的可以联系我
   //我的邮箱:werltm@hotmail.com
   // 官方网站:"http://www.yoyocms.com"
 // 交流QQ群：104390185  
 //微信公众号：角落的白板报
// 演示地址:"vue版本：http://vue.yoyocms.com angularJs版本:ng1.yoyocms.com"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>梁桐铭 ,微软MVP</Author-作者>
// Copyright © YoYoCms@China.2019-07-30T10:51:24. All Rights Reserved.
//<生成时间>2019-07-30T10:51:24</生成时间>

 (function () {
    appModule.controller('common.views.financialDetailss.createOrEditModal', [
         '$scope', '$uibModalInstance', 'abp.services.app.financialDetails', 'financialDetailsId',
        function ($scope, $uibModalInstance, financialDetailsService, financialDetailsId) {
            var vm = this;
            vm.saving = false;
            //首先将financialDetails数据设置为null
            vm.financialDetails = null;

             

            //触发保存方法
            vm.save = function () {
                vm.saving = true;
                financialDetailsService.createOrUpdateFinancialDetailsAsync({ financialDetailsEditDto:vm.financialDetails }).then(function() {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close();
                }).finally(function() {
                    vm.saving = false;
                });


            };
            //取消关闭页面
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            //初始化页面
            function init() {
             //   console.log(financialDetailsId);
                financialDetailsService.getFinancialDetailsForEditAsync({
                    id: financialDetailsId
                }).then(function (result) {
              //      console.log(result);
                    //console.log(result.data);
                    vm.financialDetails = result.data.financialDetails;
					
																																																																												 
				 

                });
            }
            //执行初始化方法
            init();
        }
    ]);
})();