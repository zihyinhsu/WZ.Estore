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


### MailerSend
- Integrate MailerSend after successful registration
