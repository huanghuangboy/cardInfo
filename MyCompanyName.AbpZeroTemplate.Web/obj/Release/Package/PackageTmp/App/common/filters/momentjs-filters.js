// need load the moment.js to use these filters. 

(function () {

    appModule.filter('momentFormat', function () {
        return function (date, formatStr) {
            if (!date) {
                return '-';
            }

            return moment(date).format(formatStr);
        };
    })
        .filter('fromNow', function () {
            return function (date) {
                return moment(date).fromNow();
            };
        })
        .filter('moneyFormat', function () {
            return function (money) {
                if (!money) {
                    return '-';
                }
                return '¥ '+(money * 0.01).toFixed(2);
            };
        })
        .filter('typeFormat', function () {
            return function (type) {
                if (type == 0) {
                    return "收入";
                } else { return "支出"; }
            };
        })
        .filter('payTypeFormat', function () {
            return function (type) {
                if (type == 0) {
                    return "系统";
                } else { return "-"; }
            };
        })
        ;

})();