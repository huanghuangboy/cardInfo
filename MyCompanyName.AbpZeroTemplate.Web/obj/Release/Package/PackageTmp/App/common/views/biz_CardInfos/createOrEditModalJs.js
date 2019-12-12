
                            
 
  


//你好，我是ABP代码生成器的作者,欢迎您使用该工具，目前接受付费定制该工具，有需要的可以联系我
   //我的邮箱:werltm@hotmail.com
   // 官方网站:"http://www.yoyocms.com"
 // 交流QQ群：104390185  
 //微信公众号：角落的白板报
// 演示地址:"vue版本：http://vue.yoyocms.com angularJs版本:ng1.yoyocms.com"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>梁桐铭 ,微软MVP</Author-作者>
// Copyright © YoYoCms@China.2019-07-28T17:41:02. All Rights Reserved.
//<生成时间>2019-07-28T17:41:02</生成时间>

 (function () {
    appModule.controller('common.views.biz_CardInfos.createOrEditModal', [
         '$scope', '$uibModalInstance', 'abp.services.app.biz_CardInfo', 'biz_CardInfoId',
        function ($scope, $uibModalInstance, biz_CardInfoService, biz_CardInfoId) {
            var vm = this;
            vm.saving = false;
            //首先将biz_CardInfo数据设置为null
            vm.biz_CardInfo = null;

             

            //触发保存方法
            vm.save = function () {
                vm.saving = true;
                biz_CardInfoService.createOrUpdateBiz_CardInfoAsync({ biz_CardInfoEditDto:vm.biz_CardInfo }).then(function() {
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
             //   console.log(biz_CardInfoId);
                biz_CardInfoService.getBiz_CardInfoForEditAsync({
                    id: biz_CardInfoId
                }).then(function (result) {
              //      console.log(result);
                    //console.log(result.data);
                    vm.biz_CardInfo = result.data.biz_CardInfo;
					
																																																																																							   
		   //日期选择器
                    $("#Birthday").datetimepicker({
                          minDate: new Date(),
                          autoclose: true,
                          isRTL: false,
                          format: "yyyy-mm-dd hh:ii",
                          pickerPosition: ("bottom-left"),
                          //默认为E文按钮要中文，自己去找语言包
                          todayBtn: true,
                          language: "zh-CN",
                          startDate: new Date(),
                          todayHighlight: true
                          });		 
		  																																													   
		   //日期选择器
                    $("#CheckTime").datetimepicker({
                          minDate: new Date(),
                          autoclose: true,
                          isRTL: false,
                          format: "yyyy-mm-dd hh:ii",
                          pickerPosition: ("bottom-left"),
                          //默认为E文按钮要中文，自己去找语言包
                          todayBtn: true,
                          language: "zh-CN",
                          startDate: new Date(),
                          todayHighlight: true
                          });		 
		  																																		 
				 

                });
            }
            //执行初始化方法
            init();
        }
    ]);
})();