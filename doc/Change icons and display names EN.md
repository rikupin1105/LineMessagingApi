# Change icon and display name
When sending a message from the LINE Official Account, you can specify the `sender.name` and the `sender.iconUrl` properties in [Message objects](https://developers.line.biz/en/reference/messaging-api/#message-objects).   
[LINE Decelopers](https://developers.line.biz/en/reference/messaging-api/#icon-nickname-switch)

---

### sender.name `String`   
Display name. Certain words such as LINE may not be used.  
Max character limit: 20

---

### sender.iconUrl `String`
URL of the image to display as an icon when sending a message
- Max character limit: 1000
- URL scheme: https
- Image format: PNG
- Aspect ratio: 1:1
- Data size: Up to 1 MB

---

### Code
```cs
var sender = new MessageSender(name, iconUrl);
```
