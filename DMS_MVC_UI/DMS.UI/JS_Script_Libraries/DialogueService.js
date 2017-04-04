
function DialogueService(ngDialog, $scope) {

    this.showAlert = function (title, msg) {
        ngDialog.openConfirm({
            template: '\
                <div class="dialog">\
                <div class="header"><h6>'+ title + '</h6><div>\
                <p><span>' + msg + '<span></p>\
                <div class="ngdialog-buttons">\
                    <button focus-if type="button" class="ngdialog-button ngdialog-button-primary clsButtonorder" style="line-height:0;" ng-click="closeThisDialog(0)">Ok</button>\
                </div>\
                </div>',
            plain: true,
            className: 'ngdialog-theme-plain',
            preCloseCallback: function () {
                $scope.errorMessage = null;
            }
        });
    }

    this.showConfirmBox = function (confirmBox) {
        if (angular.isUndefined(confirmBox.ActionButtonText) || confirmBox.ActionButtonText == "" || confirmBox.ActionButtonText == null) {
            confirmBox.ActionButtonText = "Ok";
            confirmBox.CancelButtonText = "Cancel";
        }
        ngDialog.openConfirm({
            template: '\
                <div class="dialog">\
                <div class="header"><h6>' + confirmBox.Title + '</h6><div>\
                <p><span>' + confirmBox.Message + '<span></p>\
                <div class="ngdialog-buttons clstd_btnorder">\
                    <button  type="button" class="ngdialog-button ngdialog-button-primary clsButtonorder" style="line-height:0;text-transform:capitalize;font-size:11px" ng-click="CancelAction();closeThisDialog(1)" tabindex="2">' + confirmBox.CancelButtonText + '</button>\
                    <button focus-if type="button" class="ngdialog-button ngdialog-button-primary clsButtonorder" style="line-height:0;text-transform:capitalize;font-size:11px" ng-click="Action();closeThisDialog(0)" tabindex="1">' + confirmBox.ActionButtonText + '</button>\
                </div>\
                </div>',
            plain: true,
            className: 'ngdialog-theme-plain',
            showClose: false,
            closeByEscape: true,
            scope: $scope,
            controller: ['$scope', function ($scope) {
                $scope.Action = confirmBox.Action;
                $scope.CancelAction = confirmBox.CancelAction;
            }]
        });
        $("#mbtn").focus();
    }

    return {
        showAlert: this.showAlert,
        ShowConfirmBox: this.showConfirmBox
    }
}

//// UI Blocker Service
function UIBlocker() {
    this.block = function () {
        $.blockUI();
    };
    this.unblock = function () {
        $.unblockUI();
    };
    return {
        block: this.block,
        unblock: this.unblock
    }
}