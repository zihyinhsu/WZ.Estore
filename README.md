# Create a MVC Project
## Add EFModels
- Remove ZH-TW pachage
- Rebuild Project

## Authentication
### Register
- Add Models/Infra/HashUtility.cs
- Add salt in web.config 
- Add Models/ViewModels/RegisterVM.cs
- Add RegisterConfirm viewPage (example:empty)

### membership confirmation
- Add /Member/ActiveRegister
- Add ActiveRegister viewPage (example:empty)

	```
   https://localhost:44313/Members/ActiveRegister?memberId={memberId}&confirmCode={confirmCode}
	```
### Login/Logout
- Modify web.config and add form authentication
- Add Models/ViewModels/LoginVM.cs
- Modify MembersController, add Login action
- Add login viewPage (example:create)
- Modify _Layout.cshtml, add login/logout button to different page
- Add /Members/Index page
- Modify MembersController, add Logout action
- Modify Home/About action and make it only accessable by login user

### Modify Profile
- Modify Members/Index and add EditProfile link
- Add Models/ViewModels/ProfileVM.cs
- Add /Members/EditProfile action
- Add EditProfile viewPage (example:Edit)
- Create Partial View_MemberCenterNavbar.cshtml

### Modify Password
- Modify Members/Index and add ChangePassword link
- Add Models/ViewModels/ChangePasswordVM.cs (include account, email)
- Add /Members/ChangePassword action [Authorize]
- Add ChangePassword viewPage (example:Create)


### Forget Password/Reset Password
- Add Models/ViewModels/ForgetPasswordVM.cs
- Add /Members/ForgetPassword action
- Add ForgetPassword viewPage (example:Create)
  Check if account & email are valid, confirmCode= guid, send email
- return confirm forget password viewPage

- Add  Models/ViewModels/ResetPasswordVM.cs (include password, confirmPassword)
- Add /Members/ResetPassword action
- Add ResetPassword viewPage (example:Create)
  Check if memberId & ConfirmCode & confirmCode are correct, update password, confirmCode = null
- Modify login viewPage, add forget password link

### MailerSend
- Integrate MailerSend after successful registration
