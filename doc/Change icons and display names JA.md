# アイコンおよび表示名の変更
LINE公式アカウントからメッセージを送る際に、メッセージオブジェクトに、 `sender.name` プロパティと `sender.iconUrl` プロパティを指定できます。  
[LINE Developers](https://developers.line.biz/ja/reference/messaging-api/#icon-nickname-switch)

---

### sender.name `String`   
表示名。`LINE` などLINE社のサービスと誤認される可能性があるワードは使用できません。 
最大文字数：20

---

### sender.iconUrl `String`
メッセージ送信時にアイコンとして表示する画像のURL
- 最大文字数：1000
- URLスキーム：https
- 画像フォーマット：PNG
- アスペクト比：1：1
- 最大データサイズ：1MB

---

### コード
```cs
var sender = new MessageSender(name, iconUrl);
```
