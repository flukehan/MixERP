$(document).ready(function() {
    $('.form').form({
        currentPassword: {
            identifier: 'PasswordInputPassword',
            rules: [
                {
                    type: 'empty',
                    prompt: enterCurrentPasswordLocalized
                }
            ]
        },
        newPassword: {
            identifier: 'NewPasswordInputPassword',
            rules: [
                {
                    type: 'not_matches[PasswordInputPassword]',
                    prompt: newPasswordCannotBeOldPasswordLocalized
                },
                {
                    type: 'empty',
                    prompt: enterNewPasswordLocalized
                }
            ]
        },
        confirmPassword: {
            identifier: 'ConfirmPasswordInputPassword',
            rules: [
                {
                    type: 'match[NewPasswordInputPassword]',
                    prompt: confirmationPasswordDoesNotMatchLocalized
                }
            ]
        }
    }, {
        on: 'blur'
    });
});