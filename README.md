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
- Integrate MailerSend after successful registration
