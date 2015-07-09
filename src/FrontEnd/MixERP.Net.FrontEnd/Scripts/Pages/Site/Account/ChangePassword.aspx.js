$(document).ready(function() {
    $('.form').form({
        currentPassword: {
            identifier: 'PasswordInputPassword',
            rules: [
                {
                    type: 'empty',
                    prompt: Resources.Warnings.PleaseEnterCurrentPassword()
                }
            ]
        },
        newPassword: {
            identifier: 'NewPasswordInputPassword',
            rules: [
                {
                    type: 'not_matches[PasswordInputPassword]',
                    prompt: Resources.Warnings.NewPasswordCannotBeOldPassword()
                },
                {
                    type: 'empty',
                    prompt: Resources.Warnings.PleaseEnterNewPassword()
                }
            ]
        },
        confirmPassword: {
            identifier: 'ConfirmPasswordInputPassword',
            rules: [
                {
                    type: 'match[NewPasswordInputPassword]',
                    prompt: Resources.Warnings.ConfirmationPasswordDoesNotMatch()
                }
            ]
        }
    }, {
        on: 'blur'
    });
});